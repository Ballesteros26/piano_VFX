using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200051C RID: 1308
	internal sealed class HttpConnection
	{
		// Token: 0x060027A4 RID: 10148 RVA: 0x00098EAC File Offset: 0x000970AC
		public HttpConnection(Socket sock, EndPointListener epl, bool secure, X509Certificate cert)
		{
			this.sock = sock;
			this.epl = epl;
			this.secure = secure;
			this.cert = cert;
			if (!secure)
			{
				this.stream = new NetworkStream(sock, false);
			}
			else
			{
				this.ssl_stream = epl.Listener.CreateSslStream(new NetworkStream(sock, false), false, delegate(object t, X509Certificate c, X509Chain ch, SslPolicyErrors e)
				{
					if (c == null)
					{
						return true;
					}
					X509Certificate2 x509Certificate = c as X509Certificate2;
					if (x509Certificate == null)
					{
						x509Certificate = new X509Certificate2(c.GetRawCertData());
					}
					this.client_cert = x509Certificate;
					this.client_cert_errors = new int[] { (int)e };
					return true;
				});
				this.stream = this.ssl_stream;
			}
			this.timer = new Timer(new TimerCallback(this.OnTimeout), null, -1, -1);
			if (this.ssl_stream != null)
			{
				this.ssl_stream.AuthenticateAsServer(cert, true, (SslProtocols)ServicePointManager.SecurityProtocol, false);
			}
			this.Init();
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x00098F66 File Offset: 0x00097166
		internal SslStream SslStream
		{
			get
			{
				return this.ssl_stream;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x00098F6E File Offset: 0x0009716E
		internal int[] ClientCertificateErrors
		{
			get
			{
				return this.client_cert_errors;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x00098F76 File Offset: 0x00097176
		internal X509Certificate2 ClientCertificate
		{
			get
			{
				return this.client_cert;
			}
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x00098F80 File Offset: 0x00097180
		private void Init()
		{
			this.context_bound = false;
			this.i_stream = null;
			this.o_stream = null;
			this.prefix = null;
			this.chunked = false;
			this.ms = new MemoryStream();
			this.position = 0;
			this.input_state = HttpConnection.InputState.RequestLine;
			this.line_state = HttpConnection.LineState.None;
			this.context = new HttpListenerContext(this);
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x00098FDC File Offset: 0x000971DC
		public bool IsClosed
		{
			get
			{
				return this.sock == null;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x00098FE7 File Offset: 0x000971E7
		public int Reuses
		{
			get
			{
				return this.reuses;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00098FEF File Offset: 0x000971EF
		public IPEndPoint LocalEndPoint
		{
			get
			{
				if (this.local_ep != null)
				{
					return this.local_ep;
				}
				this.local_ep = (IPEndPoint)this.sock.LocalEndPoint;
				return this.local_ep;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x0009901C File Offset: 0x0009721C
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return (IPEndPoint)this.sock.RemoteEndPoint;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x0009902E File Offset: 0x0009722E
		public bool IsSecure
		{
			get
			{
				return this.secure;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x00099036 File Offset: 0x00097236
		// (set) Token: 0x060027AF RID: 10159 RVA: 0x0009903E File Offset: 0x0009723E
		public ListenerPrefix Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x00099047 File Offset: 0x00097247
		private void OnTimeout(object unused)
		{
			this.CloseSocket();
			this.Unbind();
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x00099058 File Offset: 0x00097258
		public void BeginReadRequest()
		{
			if (this.buffer == null)
			{
				this.buffer = new byte[8192];
			}
			try
			{
				if (this.reuses == 1)
				{
					this.s_timeout = 15000;
				}
				this.timer.Change(this.s_timeout, -1);
				this.stream.BeginRead(this.buffer, 0, 8192, HttpConnection.onread_cb, this);
			}
			catch
			{
				this.timer.Change(-1, -1);
				this.CloseSocket();
				this.Unbind();
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000990F4 File Offset: 0x000972F4
		public RequestStream GetRequestStream(bool chunked, long contentlength)
		{
			if (this.i_stream == null)
			{
				byte[] array = this.ms.GetBuffer();
				int num = (int)this.ms.Length;
				this.ms = null;
				if (chunked)
				{
					this.chunked = true;
					this.context.Response.SendChunked = true;
					this.i_stream = new ChunkedInputStream(this.context, this.stream, array, this.position, num - this.position);
				}
				else
				{
					this.i_stream = new RequestStream(this.stream, array, this.position, num - this.position, contentlength);
				}
			}
			return this.i_stream;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00099198 File Offset: 0x00097398
		public ResponseStream GetResponseStream()
		{
			if (this.o_stream == null)
			{
				HttpListener listener = this.context.Listener;
				if (listener == null)
				{
					return new ResponseStream(this.stream, this.context.Response, true);
				}
				this.o_stream = new ResponseStream(this.stream, this.context.Response, listener.IgnoreWriteExceptions);
			}
			return this.o_stream;
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000991FC File Offset: 0x000973FC
		private static void OnRead(IAsyncResult ares)
		{
			((HttpConnection)ares.AsyncState).OnReadInternal(ares);
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x00099210 File Offset: 0x00097410
		private void OnReadInternal(IAsyncResult ares)
		{
			this.timer.Change(-1, -1);
			int num = -1;
			try
			{
				num = this.stream.EndRead(ares);
				this.ms.Write(this.buffer, 0, num);
				if (this.ms.Length > 32768L)
				{
					this.SendError("Bad request", 400);
					this.Close(true);
					return;
				}
			}
			catch
			{
				if (this.ms != null && this.ms.Length > 0L)
				{
					this.SendError();
				}
				if (this.sock != null)
				{
					this.CloseSocket();
					this.Unbind();
				}
				return;
			}
			if (num == 0)
			{
				this.CloseSocket();
				this.Unbind();
				return;
			}
			if (this.ProcessInput(this.ms))
			{
				if (!this.context.HaveError)
				{
					this.context.Request.FinishInitialization();
				}
				if (this.context.HaveError)
				{
					this.SendError();
					this.Close(true);
					return;
				}
				if (!this.epl.BindContext(this.context))
				{
					this.SendError("Invalid host", 400);
					this.Close(true);
					return;
				}
				HttpListener listener = this.context.Listener;
				if (this.last_listener != listener)
				{
					this.RemoveConnection();
					listener.AddConnection(this);
					this.last_listener = listener;
				}
				this.context_bound = true;
				listener.RegisterContext(this.context);
				return;
			}
			else
			{
				this.stream.BeginRead(this.buffer, 0, 8192, HttpConnection.onread_cb, this);
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000993A4 File Offset: 0x000975A4
		private void RemoveConnection()
		{
			if (this.last_listener == null)
			{
				this.epl.RemoveConnection(this);
				return;
			}
			this.last_listener.RemoveConnection(this);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000993C8 File Offset: 0x000975C8
		private bool ProcessInput(MemoryStream ms)
		{
			byte[] array = ms.GetBuffer();
			int num = (int)ms.Length;
			int num2 = 0;
			while (!this.context.HaveError)
			{
				if (this.position < num)
				{
					string text;
					try
					{
						text = this.ReadLine(array, this.position, num - this.position, ref num2);
						this.position += num2;
					}
					catch
					{
						this.context.ErrorMessage = "Bad request";
						this.context.ErrorStatus = 400;
						return true;
					}
					if (text == null)
					{
						goto IL_010D;
					}
					if (text == "")
					{
						if (this.input_state != HttpConnection.InputState.RequestLine)
						{
							this.current_line = null;
							ms = null;
							return true;
						}
						continue;
					}
					else
					{
						if (this.input_state == HttpConnection.InputState.RequestLine)
						{
							this.context.Request.SetRequestLine(text);
							this.input_state = HttpConnection.InputState.Headers;
							continue;
						}
						try
						{
							this.context.Request.AddHeader(text);
							continue;
						}
						catch (Exception ex)
						{
							this.context.ErrorMessage = ex.Message;
							this.context.ErrorStatus = 400;
							return true;
						}
						goto IL_010D;
					}
					bool flag;
					return flag;
				}
				IL_010D:
				if (num2 == num)
				{
					ms.SetLength(0L);
					this.position = 0;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00099518 File Offset: 0x00097718
		private string ReadLine(byte[] buffer, int offset, int len, ref int used)
		{
			if (this.current_line == null)
			{
				this.current_line = new StringBuilder(128);
			}
			int num = offset + len;
			used = 0;
			int num2 = offset;
			while (num2 < num && this.line_state != HttpConnection.LineState.LF)
			{
				used++;
				byte b = buffer[num2];
				if (b == 13)
				{
					this.line_state = HttpConnection.LineState.CR;
				}
				else if (b == 10)
				{
					this.line_state = HttpConnection.LineState.LF;
				}
				else
				{
					this.current_line.Append((char)b);
				}
				num2++;
			}
			string text = null;
			if (this.line_state == HttpConnection.LineState.LF)
			{
				this.line_state = HttpConnection.LineState.None;
				text = this.current_line.ToString();
				this.current_line.Length = 0;
			}
			return text;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000995BC File Offset: 0x000977BC
		public void SendError(string msg, int status)
		{
			try
			{
				HttpListenerResponse response = this.context.Response;
				response.StatusCode = status;
				response.ContentType = "text/html";
				string text = HttpStatusDescription.Get(status);
				string text2;
				if (msg != null)
				{
					text2 = string.Format("<h1>{0} ({1})</h1>", text, msg);
				}
				else
				{
					text2 = string.Format("<h1>{0}</h1>", text);
				}
				byte[] bytes = this.context.Response.ContentEncoding.GetBytes(text2);
				response.Close(bytes, false);
			}
			catch
			{
			}
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00099640 File Offset: 0x00097840
		public void SendError()
		{
			this.SendError(this.context.ErrorMessage, this.context.ErrorStatus);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0009965E File Offset: 0x0009785E
		private void Unbind()
		{
			if (this.context_bound)
			{
				this.epl.UnbindContext(this.context);
				this.context_bound = false;
			}
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00099680 File Offset: 0x00097880
		public void Close()
		{
			this.Close(false);
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x0009968C File Offset: 0x0009788C
		private void CloseSocket()
		{
			if (this.sock == null)
			{
				return;
			}
			try
			{
				this.sock.Close();
			}
			catch
			{
			}
			finally
			{
				this.sock = null;
			}
			this.RemoveConnection();
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000996E0 File Offset: 0x000978E0
		internal void Close(bool force_close)
		{
			if (this.sock != null)
			{
				Stream responseStream = this.GetResponseStream();
				if (responseStream != null)
				{
					responseStream.Close();
				}
				this.o_stream = null;
			}
			if (this.sock == null)
			{
				return;
			}
			force_close |= !this.context.Request.KeepAlive;
			if (!force_close)
			{
				force_close = this.context.Response.Headers["connection"] == "close";
			}
			if (force_close || !this.context.Request.FlushInput())
			{
				Socket socket = this.sock;
				this.sock = null;
				try
				{
					if (socket != null)
					{
						socket.Shutdown(SocketShutdown.Both);
					}
				}
				catch
				{
				}
				finally
				{
					if (socket != null)
					{
						socket.Close();
					}
				}
				this.Unbind();
				this.RemoveConnection();
				return;
			}
			if (this.chunked && !this.context.Response.ForceCloseChunked)
			{
				this.reuses++;
				this.Unbind();
				this.Init();
				this.BeginReadRequest();
				return;
			}
			this.reuses++;
			this.Unbind();
			this.Init();
			this.BeginReadRequest();
		}

		// Token: 0x04002181 RID: 8577
		private static AsyncCallback onread_cb = new AsyncCallback(HttpConnection.OnRead);

		// Token: 0x04002182 RID: 8578
		private const int BufferSize = 8192;

		// Token: 0x04002183 RID: 8579
		private Socket sock;

		// Token: 0x04002184 RID: 8580
		private Stream stream;

		// Token: 0x04002185 RID: 8581
		private EndPointListener epl;

		// Token: 0x04002186 RID: 8582
		private MemoryStream ms;

		// Token: 0x04002187 RID: 8583
		private byte[] buffer;

		// Token: 0x04002188 RID: 8584
		private HttpListenerContext context;

		// Token: 0x04002189 RID: 8585
		private StringBuilder current_line;

		// Token: 0x0400218A RID: 8586
		private ListenerPrefix prefix;

		// Token: 0x0400218B RID: 8587
		private RequestStream i_stream;

		// Token: 0x0400218C RID: 8588
		private ResponseStream o_stream;

		// Token: 0x0400218D RID: 8589
		private bool chunked;

		// Token: 0x0400218E RID: 8590
		private int reuses;

		// Token: 0x0400218F RID: 8591
		private bool context_bound;

		// Token: 0x04002190 RID: 8592
		private bool secure;

		// Token: 0x04002191 RID: 8593
		private X509Certificate cert;

		// Token: 0x04002192 RID: 8594
		private int s_timeout = 90000;

		// Token: 0x04002193 RID: 8595
		private Timer timer;

		// Token: 0x04002194 RID: 8596
		private IPEndPoint local_ep;

		// Token: 0x04002195 RID: 8597
		private HttpListener last_listener;

		// Token: 0x04002196 RID: 8598
		private int[] client_cert_errors;

		// Token: 0x04002197 RID: 8599
		private X509Certificate2 client_cert;

		// Token: 0x04002198 RID: 8600
		private SslStream ssl_stream;

		// Token: 0x04002199 RID: 8601
		private HttpConnection.InputState input_state;

		// Token: 0x0400219A RID: 8602
		private HttpConnection.LineState line_state;

		// Token: 0x0400219B RID: 8603
		private int position;

		// Token: 0x0200051D RID: 1309
		private enum InputState
		{
			// Token: 0x0400219D RID: 8605
			RequestLine,
			// Token: 0x0400219E RID: 8606
			Headers
		}

		// Token: 0x0200051E RID: 1310
		private enum LineState
		{
			// Token: 0x040021A0 RID: 8608
			None,
			// Token: 0x040021A1 RID: 8609
			CR,
			// Token: 0x040021A2 RID: 8610
			LF
		}
	}
}
