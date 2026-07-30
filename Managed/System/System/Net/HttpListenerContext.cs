using System;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace System.Net
{
	/// <summary>Provides access to the request and response objects used by the <see cref="T:System.Net.HttpListener" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000522 RID: 1314
	public sealed class HttpListenerContext
	{
		// Token: 0x060027F0 RID: 10224 RVA: 0x0009A2B2 File Offset: 0x000984B2
		internal HttpListenerContext(HttpConnection cnc)
		{
			this.err_status = 400;
			base..ctor();
			this.cnc = cnc;
			this.request = new HttpListenerRequest(this);
			this.response = new HttpListenerResponse(this);
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x0009A2E4 File Offset: 0x000984E4
		// (set) Token: 0x060027F2 RID: 10226 RVA: 0x0009A2EC File Offset: 0x000984EC
		internal int ErrorStatus
		{
			get
			{
				return this.err_status;
			}
			set
			{
				this.err_status = value;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x0009A2F5 File Offset: 0x000984F5
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x0009A2FD File Offset: 0x000984FD
		internal string ErrorMessage
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x0009A306 File Offset: 0x00098506
		internal bool HaveError
		{
			get
			{
				return this.error != null;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x0009A311 File Offset: 0x00098511
		internal HttpConnection Connection
		{
			get
			{
				return this.cnc;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerRequest" /> that represents a client's request for a resource.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerRequest" /> object that represents the client request.</returns>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x0009A319 File Offset: 0x00098519
		public HttpListenerRequest Request
		{
			get
			{
				return this.request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerResponse" /> object that will be sent to the client in response to the client's request. </summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerResponse" /> object used to send a response back to the client.</returns>
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x0009A321 File Offset: 0x00098521
		public HttpListenerResponse Response
		{
			get
			{
				return this.response;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the client whose request is represented by this <see cref="T:System.Net.HttpListenerContext" /> object. </summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> object that describes the client, or null if the <see cref="T:System.Net.HttpListener" /> that supplied this <see cref="T:System.Net.HttpListenerContext" /> does not require authentication.</returns>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x0009A329 File Offset: 0x00098529
		public IPrincipal User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0009A334 File Offset: 0x00098534
		internal void ParseAuthentication(AuthenticationSchemes expectedSchemes)
		{
			if (expectedSchemes == AuthenticationSchemes.Anonymous)
			{
				return;
			}
			string text = this.request.Headers["Authorization"];
			if (text == null || text.Length < 2)
			{
				return;
			}
			string[] array = text.Split(new char[] { ' ' }, 2);
			if (string.Compare(array[0], "basic", true) == 0)
			{
				this.user = this.ParseBasicAuthentication(array[1]);
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0009A3A0 File Offset: 0x000985A0
		internal IPrincipal ParseBasicAuthentication(string authData)
		{
			IPrincipal principal;
			try
			{
				string text = Encoding.Default.GetString(Convert.FromBase64String(authData));
				int num = text.IndexOf(':');
				string text2 = text.Substring(num + 1);
				text = text.Substring(0, num);
				num = text.IndexOf('\\');
				string text3;
				if (num > 0)
				{
					text3 = text.Substring(num);
				}
				else
				{
					text3 = text;
				}
				principal = new GenericPrincipal(new HttpListenerBasicIdentity(text3, text2), new string[0]);
			}
			catch (Exception)
			{
				principal = null;
			}
			return principal;
		}

		/// <summary>Accept a WebSocket connection as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060027FC RID: 10236 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol  and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060027FD RID: 10237 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, TimeSpan keepAliveInterval)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.-or- <paramref name="receiveBufferSize" /> is less than 16 bytes-or- <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060027FE RID: 10238 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, WebSocket keep-alive interval, and the internal buffer as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <param name="internalBuffer">An internal buffer to use for this operation.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.-or- <paramref name="receiveBufferSize" /> is less than 16 bytes-or- <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060027FF RID: 10239 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal HttpListenerContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040021B7 RID: 8631
		private HttpListenerRequest request;

		// Token: 0x040021B8 RID: 8632
		private HttpListenerResponse response;

		// Token: 0x040021B9 RID: 8633
		private IPrincipal user;

		// Token: 0x040021BA RID: 8634
		private HttpConnection cnc;

		// Token: 0x040021BB RID: 8635
		private string error;

		// Token: 0x040021BC RID: 8636
		private int err_status;

		// Token: 0x040021BD RID: 8637
		internal HttpListener Listener;
	}
}
