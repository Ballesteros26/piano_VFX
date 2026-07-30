using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;

namespace System.Net
{
	// Token: 0x02000552 RID: 1362
	internal class WebConnection : IDisposable
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x000A380E File Offset: 0x000A1A0E
		public ServicePoint ServicePoint { get; }

		// Token: 0x06002A6A RID: 10858 RVA: 0x000A3816 File Offset: 0x000A1A16
		public WebConnection(ServicePoint sPoint)
		{
			this.ServicePoint = sPoint;
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("MONO_WEB_DEBUG")]
		internal static void Debug(string message, params object[] args)
		{
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("MONO_WEB_DEBUG")]
		internal static void Debug(string message)
		{
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000A3825 File Offset: 0x000A1A25
		private bool CanReuse()
		{
			return !this.socket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000A3838 File Offset: 0x000A1A38
		private bool CheckReusable()
		{
			if (this.socket != null && this.socket.Connected)
			{
				try
				{
					if (this.CanReuse())
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000A3880 File Offset: 0x000A1A80
		private async Task Connect(WebOperation operation, CancellationToken cancellationToken)
		{
			IPHostEntry hostEntry = this.ServicePoint.HostEntry;
			if (hostEntry == null || hostEntry.AddressList.Length == 0)
			{
				throw WebConnection.GetException(this.ServicePoint.UsesProxy ? WebExceptionStatus.ProxyNameResolutionFailure : WebExceptionStatus.NameResolutionFailure, null);
			}
			Exception connectException = null;
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				operation.ThrowIfDisposed(cancellationToken);
				try
				{
					this.socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				}
				catch (Exception ex)
				{
					throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
				}
				IPEndPoint ipendPoint = new IPEndPoint(ipaddress, this.ServicePoint.Address.Port);
				this.socket.NoDelay = !this.ServicePoint.UseNagleAlgorithm;
				try
				{
					this.ServicePoint.KeepAliveSetup(this.socket);
				}
				catch
				{
				}
				if (!this.ServicePoint.CallEndPointDelegate(this.socket, ipendPoint))
				{
					Socket socket = Interlocked.Exchange<Socket>(ref this.socket, null);
					if (socket != null)
					{
						socket.Close();
					}
				}
				else
				{
					try
					{
						operation.ThrowIfDisposed(cancellationToken);
						await this.socket.ConnectAsync(ipendPoint).ConfigureAwait(false);
					}
					catch (ObjectDisposedException)
					{
						throw;
					}
					catch (Exception ex2)
					{
						Socket socket2 = Interlocked.Exchange<Socket>(ref this.socket, null);
						if (socket2 != null)
						{
							socket2.Close();
						}
						connectException = WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex2);
						goto IL_01DA;
					}
					if (this.socket != null)
					{
						return;
					}
				}
				IL_01DA:;
			}
			IPAddress[] array = null;
			if (connectException == null)
			{
				connectException = WebConnection.GetException(WebExceptionStatus.ConnectFailure, null);
			}
			throw connectException;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000A38D8 File Offset: 0x000A1AD8
		private async Task<bool> CreateStream(WebOperation operation, bool reused, CancellationToken cancellationToken)
		{
			bool flag;
			try
			{
				NetworkStream stream = new NetworkStream(this.socket, false);
				if (operation.Request.Address.Scheme == Uri.UriSchemeHttps)
				{
					if (!reused || this.monoTlsStream == null)
					{
						if (this.ServicePoint.UseConnect)
						{
							if (this.tunnel == null)
							{
								this.tunnel = new WebConnectionTunnel(operation.Request, this.ServicePoint.Address);
							}
							await this.tunnel.Initialize(stream, cancellationToken).ConfigureAwait(false);
							if (!this.tunnel.Success)
							{
								return false;
							}
						}
						this.monoTlsStream = new MonoTlsStream(operation.Request, stream);
						this.networkStream = await this.monoTlsStream.CreateStream(this.tunnel, cancellationToken).ConfigureAwait(false);
					}
					flag = true;
				}
				else
				{
					this.networkStream = stream;
					flag = true;
				}
			}
			catch (Exception ex)
			{
				ex = HttpWebRequest.FlattenException(ex);
				if (operation.Aborted || this.monoTlsStream == null)
				{
					throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
				}
				throw WebConnection.GetException(this.monoTlsStream.ExceptionStatus, ex);
			}
			finally
			{
			}
			return flag;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000A3938 File Offset: 0x000A1B38
		internal async Task<WebRequestStream> InitConnection(WebOperation operation, CancellationToken cancellationToken)
		{
			bool flag = true;
			for (;;)
			{
				operation.ThrowIfClosedOrDisposed(cancellationToken);
				bool reused = this.CheckReusable();
				if (!reused)
				{
					this.CloseSocket();
					if (flag)
					{
						this.Reset();
					}
					try
					{
						await this.Connect(operation, cancellationToken).ConfigureAwait(false);
					}
					catch (Exception)
					{
						throw;
					}
				}
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.CreateStream(operation, reused, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					goto IL_0180;
				}
				WebConnectionTunnel webConnectionTunnel = this.tunnel;
				if (((webConnectionTunnel != null) ? webConnectionTunnel.Challenge : null) == null)
				{
					break;
				}
				if (this.tunnel.CloseConnection)
				{
					this.CloseSocket();
				}
				flag = false;
			}
			throw WebConnection.GetException(WebExceptionStatus.ProtocolError, null);
			IL_0180:
			return new WebRequestStream(this, operation, this.networkStream, this.tunnel);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000A3990 File Offset: 0x000A1B90
		internal static WebException GetException(WebExceptionStatus status, Exception error)
		{
			if (error == null)
			{
				return new WebException(string.Format("Error: {0}", status), status);
			}
			WebException ex;
			if ((ex = error as WebException) != null)
			{
				return ex;
			}
			return new WebException(string.Format("Error: {0} ({1})", status, error.Message), status, WebExceptionInternalStatus.RequestFatal, error);
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000A39E4 File Offset: 0x000A1BE4
		internal static bool ReadLine(byte[] buffer, ref int start, int max, ref string output)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (start < max)
			{
				int num2 = start;
				start = num2 + 1;
				num = (int)buffer[num2];
				if (num == 10)
				{
					if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\r')
					{
						StringBuilder stringBuilder2 = stringBuilder;
						num2 = stringBuilder2.Length;
						stringBuilder2.Length = num2 - 1;
					}
					flag = false;
					break;
				}
				if (flag)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					num2 = stringBuilder3.Length;
					stringBuilder3.Length = num2 - 1;
					break;
				}
				if (num == 13)
				{
					flag = true;
				}
				stringBuilder.Append((char)num);
			}
			if (num != 10 && num != 13)
			{
				return false;
			}
			if (stringBuilder.Length == 0)
			{
				output = null;
				return num == 10 || num == 13;
			}
			if (flag)
			{
				StringBuilder stringBuilder4 = stringBuilder;
				int num2 = stringBuilder4.Length;
				stringBuilder4.Length = num2 - 1;
			}
			output = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000A3AA8 File Offset: 0x000A1CA8
		internal bool CanReuseConnection(WebOperation operation)
		{
			bool flag2;
			lock (this)
			{
				if (this.Closed || this.currentOperation != null)
				{
					flag2 = false;
				}
				else if (!this.NtlmAuthenticated)
				{
					flag2 = true;
				}
				else
				{
					NetworkCredential ntlmCredential = this.NtlmCredential;
					HttpWebRequest request = operation.Request;
					ICredentials credentials = ((request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials);
					NetworkCredential networkCredential = ((credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null);
					if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
					{
						flag2 = false;
					}
					else
					{
						bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
						bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
						flag2 = unsafeAuthenticatedConnectionSharing && unsafeAuthenticatedConnectionSharing == unsafeAuthenticatedConnectionSharing2;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000A3BD0 File Offset: 0x000A1DD0
		private bool PrepareSharingNtlm(WebOperation operation)
		{
			if (operation == null || !this.NtlmAuthenticated)
			{
				return true;
			}
			bool flag = false;
			NetworkCredential ntlmCredential = this.NtlmCredential;
			HttpWebRequest request = operation.Request;
			ICredentials credentials = ((request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials);
			NetworkCredential networkCredential = ((credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null);
			if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
			{
				flag = true;
			}
			if (!flag)
			{
				bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
				bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
				flag = !unsafeAuthenticatedConnectionSharing || unsafeAuthenticatedConnectionSharing != unsafeAuthenticatedConnectionSharing2;
			}
			return flag;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000A3CB4 File Offset: 0x000A1EB4
		private void Reset()
		{
			lock (this)
			{
				this.tunnel = null;
				this.ResetNtlm();
			}
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000A3CF8 File Offset: 0x000A1EF8
		private void Close(bool reset)
		{
			lock (this)
			{
				this.CloseSocket();
				if (reset)
				{
					this.Reset();
				}
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000A3D3C File Offset: 0x000A1F3C
		private void CloseSocket()
		{
			lock (this)
			{
				if (this.networkStream != null)
				{
					try
					{
						this.networkStream.Dispose();
					}
					catch
					{
					}
					this.networkStream = null;
				}
				if (this.socket != null)
				{
					try
					{
						this.socket.Dispose();
					}
					catch
					{
					}
					this.socket = null;
				}
				this.monoTlsStream = null;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x000A3DD0 File Offset: 0x000A1FD0
		public bool Closed
		{
			get
			{
				return this.disposed != 0;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000A3DDB File Offset: 0x000A1FDB
		public bool Busy
		{
			get
			{
				return this.currentOperation != null;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x000A3DE6 File Offset: 0x000A1FE6
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000A3DF0 File Offset: 0x000A1FF0
		public bool StartOperation(WebOperation operation, bool reused)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, operation, null) != null)
				{
					return false;
				}
				this.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
				if (reused && !this.PrepareSharingNtlm(operation))
				{
					this.Close(true);
				}
				operation.RegisterRequest(this.ServicePoint, this);
			}
			operation.Run();
			return true;
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000A3E8C File Offset: 0x000A208C
		public bool Continue(WebOperation next)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (this.socket == null || !this.socket.Connected || !this.PrepareSharingNtlm(next))
				{
					this.Close(true);
					return false;
				}
				this.currentOperation = next;
				if (next == null)
				{
					return true;
				}
				next.RegisterRequest(this.ServicePoint, this);
			}
			next.Run();
			return true;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000A3F1C File Offset: 0x000A211C
		private void Dispose(bool disposing)
		{
			if (Interlocked.CompareExchange(ref this.disposed, 1, 0) != 0)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000A3F35 File Offset: 0x000A2135
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000A3F3E File Offset: 0x000A213E
		private void ResetNtlm()
		{
			this.ntlm_authenticated = false;
			this.ntlm_credentials = null;
			this.unsafe_sharing = false;
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x000A3F55 File Offset: 0x000A2155
		// (set) Token: 0x06002A82 RID: 10882 RVA: 0x000A3F5D File Offset: 0x000A215D
		internal bool NtlmAuthenticated
		{
			get
			{
				return this.ntlm_authenticated;
			}
			set
			{
				this.ntlm_authenticated = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x000A3F66 File Offset: 0x000A2166
		// (set) Token: 0x06002A84 RID: 10884 RVA: 0x000A3F6E File Offset: 0x000A216E
		internal NetworkCredential NtlmCredential
		{
			get
			{
				return this.ntlm_credentials;
			}
			set
			{
				this.ntlm_credentials = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x000A3F77 File Offset: 0x000A2177
		// (set) Token: 0x06002A86 RID: 10886 RVA: 0x000A3F7F File Offset: 0x000A217F
		internal bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_sharing;
			}
			set
			{
				this.unsafe_sharing = value;
			}
		}

		// Token: 0x0400230F RID: 8975
		private NetworkCredential ntlm_credentials;

		// Token: 0x04002310 RID: 8976
		private bool ntlm_authenticated;

		// Token: 0x04002311 RID: 8977
		private bool unsafe_sharing;

		// Token: 0x04002312 RID: 8978
		private Stream networkStream;

		// Token: 0x04002313 RID: 8979
		private Socket socket;

		// Token: 0x04002314 RID: 8980
		private MonoTlsStream monoTlsStream;

		// Token: 0x04002315 RID: 8981
		private WebConnectionTunnel tunnel;

		// Token: 0x04002316 RID: 8982
		private int disposed;

		// Token: 0x04002318 RID: 8984
		internal readonly int ID;

		// Token: 0x04002319 RID: 8985
		private DateTime idleSince;

		// Token: 0x0400231A RID: 8986
		private WebOperation currentOperation;
	}
}
