using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net.WebSockets
{
	/// <summary>Options to use with a  <see cref="T:System.Net.WebSockets.ClientWebSocket" /> object.</summary>
	// Token: 0x020006D6 RID: 1750
	public sealed class ClientWebSocketOptions
	{
		// Token: 0x06003694 RID: 13972 RVA: 0x000C9666 File Offset: 0x000C7866
		internal ClientWebSocketOptions()
		{
			this._requestedSubProtocols = new List<string>();
			this._requestHeaders = new WebHeaderCollection();
		}

		/// <summary>Creates a HTTP request header and its value.</summary>
		/// <param name="headerName">The name of the HTTP header.</param>
		/// <param name="headerValue">The value of the HTTP header.</param>
		// Token: 0x06003695 RID: 13973 RVA: 0x000C96A5 File Offset: 0x000C78A5
		public void SetRequestHeader(string headerName, string headerValue)
		{
			this.ThrowIfReadOnly();
			this._requestHeaders.Set(headerName, headerValue);
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x000C96BA File Offset: 0x000C78BA
		internal WebHeaderCollection RequestHeaders
		{
			get
			{
				return this._requestHeaders;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x000C96C2 File Offset: 0x000C78C2
		internal List<string> RequestedSubProtocols
		{
			get
			{
				return this._requestedSubProtocols;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates if default credentials should be used during WebSocket handshake.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if default credentials should be used during WebSocket handshake; otherwise false. The default is true.</returns>
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000C96CA File Offset: 0x000C78CA
		// (set) Token: 0x06003699 RID: 13977 RVA: 0x000C96D2 File Offset: 0x000C78D2
		public bool UseDefaultCredentials
		{
			get
			{
				return this._useDefaultCredentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._useDefaultCredentials = value;
			}
		}

		/// <summary>Gets or sets the credential information for the client.</summary>
		/// <returns>Returns <see cref="T:System.Net.ICredentials" />.The credential information for the client.</returns>
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x000C96E1 File Offset: 0x000C78E1
		// (set) Token: 0x0600369B RID: 13979 RVA: 0x000C96E9 File Offset: 0x000C78E9
		public ICredentials Credentials
		{
			get
			{
				return this._credentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._credentials = value;
			}
		}

		/// <summary>Gets or sets the proxy for WebSocket requests.</summary>
		/// <returns>Returns <see cref="T:System.Net.IWebProxy" />.The proxy for WebSocket requests.</returns>
		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000C96F8 File Offset: 0x000C78F8
		// (set) Token: 0x0600369D RID: 13981 RVA: 0x000C9700 File Offset: 0x000C7900
		public IWebProxy Proxy
		{
			get
			{
				return this._proxy;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._proxy = value;
			}
		}

		/// <summary>Gets or sets a collection of client side certificates.</summary>
		/// <returns>Returns <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.A collection of client side certificates.</returns>
		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000C970F File Offset: 0x000C790F
		// (set) Token: 0x0600369F RID: 13983 RVA: 0x000C972A File Offset: 0x000C792A
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this._clientCertificates == null)
				{
					this._clientCertificates = new X509CertificateCollection();
				}
				return this._clientCertificates;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._clientCertificates = value;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieContainer" />.The cookies associated with the request.</returns>
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x000C9747 File Offset: 0x000C7947
		// (set) Token: 0x060036A1 RID: 13985 RVA: 0x000C974F File Offset: 0x000C794F
		public CookieContainer Cookies
		{
			get
			{
				return this._cookies;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._cookies = value;
			}
		}

		/// <summary>Adds a sub-protocol to be negotiated during the WebSocket connection handshake.</summary>
		/// <param name="subProtocol">The WebSocket sub-protocol to add.</param>
		// Token: 0x060036A2 RID: 13986 RVA: 0x000C9760 File Offset: 0x000C7960
		public void AddSubProtocol(string subProtocol)
		{
			this.ThrowIfReadOnly();
			WebSocketValidate.ValidateSubprotocol(subProtocol);
			using (List<string>.Enumerator enumerator = this._requestedSubProtocols.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Equals(enumerator.Current, subProtocol, StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(global::SR.Format("Duplicate protocols are not allowed: '{0}'.", subProtocol), "subProtocol");
					}
				}
			}
			this._requestedSubProtocols.Add(subProtocol);
		}

		/// <summary>Gets or sets the WebSocket protocol keep-alive interval in milliseconds.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The WebSocket protocol keep-alive interval in milliseconds.</returns>
		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060036A3 RID: 13987 RVA: 0x000C97E4 File Offset: 0x000C79E4
		// (set) Token: 0x060036A4 RID: 13988 RVA: 0x000C97EC File Offset: 0x000C79EC
		public TimeSpan KeepAliveInterval
		{
			get
			{
				return this._keepAliveInterval;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value != Timeout.InfiniteTimeSpan && value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value", value, global::SR.Format("The argument must be a value greater than {0}.", Timeout.InfiniteTimeSpan.ToString()));
				}
				this._keepAliveInterval = value;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x000C984E File Offset: 0x000C7A4E
		internal int ReceiveBufferSize
		{
			get
			{
				return this._receiveBufferSize;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060036A6 RID: 13990 RVA: 0x000C9856 File Offset: 0x000C7A56
		internal int SendBufferSize
		{
			get
			{
				return this._sendBufferSize;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x000C985E File Offset: 0x000C7A5E
		internal ArraySegment<byte>? Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		/// <summary>Sets the client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		// Token: 0x060036A8 RID: 13992 RVA: 0x000C9868 File Offset: 0x000C7A68
		public void SetBuffer(int receiveBufferSize, int sendBufferSize)
		{
			this.ThrowIfReadOnly();
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, global::SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, global::SR.Format("The argument must be a value greater than {0}.", 1));
			}
			this._receiveBufferSize = receiveBufferSize;
			this._sendBufferSize = sendBufferSize;
			this._buffer = null;
		}

		/// <summary>Sets client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		/// <param name="buffer">The receive buffer to use.</param>
		// Token: 0x060036A9 RID: 13993 RVA: 0x000C98E0 File Offset: 0x000C7AE0
		public void SetBuffer(int receiveBufferSize, int sendBufferSize, ArraySegment<byte> buffer)
		{
			this.ThrowIfReadOnly();
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, global::SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, global::SR.Format("The argument must be a value greater than {0}.", 1));
			}
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			if (buffer.Count == 0)
			{
				throw new ArgumentOutOfRangeException("buffer");
			}
			this._receiveBufferSize = receiveBufferSize;
			this._sendBufferSize = sendBufferSize;
			this._buffer = new ArraySegment<byte>?(buffer);
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000C9976 File Offset: 0x000C7B76
		internal void SetToReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000C997F File Offset: 0x000C7B7F
		private void ThrowIfReadOnly()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The WebSocket has already been started.");
			}
		}

		// Token: 0x04002B7A RID: 11130
		private bool _isReadOnly;

		// Token: 0x04002B7B RID: 11131
		private readonly List<string> _requestedSubProtocols;

		// Token: 0x04002B7C RID: 11132
		private readonly WebHeaderCollection _requestHeaders;

		// Token: 0x04002B7D RID: 11133
		private TimeSpan _keepAliveInterval = WebSocket.DefaultKeepAliveInterval;

		// Token: 0x04002B7E RID: 11134
		private bool _useDefaultCredentials;

		// Token: 0x04002B7F RID: 11135
		private ICredentials _credentials;

		// Token: 0x04002B80 RID: 11136
		private IWebProxy _proxy;

		// Token: 0x04002B81 RID: 11137
		private X509CertificateCollection _clientCertificates;

		// Token: 0x04002B82 RID: 11138
		private CookieContainer _cookies;

		// Token: 0x04002B83 RID: 11139
		private int _receiveBufferSize = 4096;

		// Token: 0x04002B84 RID: 11140
		private int _sendBufferSize = 4096;

		// Token: 0x04002B85 RID: 11141
		private ArraySegment<byte>? _buffer;
	}
}
