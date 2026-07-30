using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using Unity;

namespace System.Net.WebSockets
{
	/// <summary>Provides access to information received by the <see cref="T:System.Net.HttpListener" /> class when accepting WebSocket connections.</summary>
	// Token: 0x020006D2 RID: 1746
	public class HttpListenerWebSocketContext : WebSocketContext
	{
		// Token: 0x06003674 RID: 13940 RVA: 0x000C9114 File Offset: 0x000C7314
		internal HttpListenerWebSocketContext(Uri requestUri, NameValueCollection headers, CookieCollection cookieCollection, IPrincipal user, bool isAuthenticated, bool isLocal, bool isSecureConnection, string origin, IEnumerable<string> secWebSocketProtocols, string secWebSocketVersion, string secWebSocketKey, WebSocket webSocket)
		{
			this._cookieCollection = new CookieCollection();
			this._cookieCollection.Add(cookieCollection);
			this._headers = new NameValueCollection(headers);
			this._user = HttpListenerWebSocketContext.CopyPrincipal(user);
			this._requestUri = requestUri;
			this._isAuthenticated = isAuthenticated;
			this._isLocal = isLocal;
			this._isSecureConnection = isSecureConnection;
			this._origin = origin;
			this._secWebSocketProtocols = secWebSocketProtocols;
			this._secWebSocketVersion = secWebSocketVersion;
			this._secWebSocketKey = secWebSocketKey;
			this._webSocket = webSocket;
		}

		/// <summary>Gets the URI requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.The URI requested by the WebSocket client.</returns>
		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000C919E File Offset: 0x000C739E
		public override Uri RequestUri
		{
			get
			{
				return this._requestUri;
			}
		}

		/// <summary>Gets the HTTP headers received by the <see cref="T:System.Net.HttpListener" /> object in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Specialized.NameValueCollection" />.The HTTP headers received by the <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06003676 RID: 13942 RVA: 0x000C91A6 File Offset: 0x000C73A6
		public override NameValueCollection Headers
		{
			get
			{
				return this._headers;
			}
		}

		/// <summary>Gets the value of the Origin HTTP header included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The value of the Origin HTTP header.</returns>
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000C91AE File Offset: 0x000C73AE
		public override string Origin
		{
			get
			{
				return this._origin;
			}
		}

		/// <summary>Gets the list of the Secure WebSocket protocols included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerable`1" />.The list of the Secure WebSocket protocols.</returns>
		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000C91B6 File Offset: 0x000C73B6
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				return this._secWebSocketProtocols;
			}
		}

		/// <summary>Gets the list of sub-protocols requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The list of sub-protocols requested by the WebSocket client.</returns>
		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000C91BE File Offset: 0x000C73BE
		public override string SecWebSocketVersion
		{
			get
			{
				return this._secWebSocketVersion;
			}
		}

		/// <summary>Gets the value of the SecWebSocketKey HTTP header included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The value of the SecWebSocketKey HTTP header.</returns>
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x000C91C6 File Offset: 0x000C73C6
		public override string SecWebSocketKey
		{
			get
			{
				return this._secWebSocketKey;
			}
		}

		/// <summary>Gets the cookies received by the <see cref="T:System.Net.HttpListener" /> object in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieCollection" />.The cookies received by the <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000C91CE File Offset: 0x000C73CE
		public override CookieCollection CookieCollection
		{
			get
			{
				return this._cookieCollection;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Security.Principal.IPrincipal" />.The identity, authentication information, and security roles for the WebSocket client.</returns>
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x000C91D6 File Offset: 0x000C73D6
		public override IPrincipal User
		{
			get
			{
				return this._user;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket client is authenticated.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket client is authenticated; otherwise false.</returns>
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000C91DE File Offset: 0x000C73DE
		public override bool IsAuthenticated
		{
			get
			{
				return this._isAuthenticated;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket client connected from the local machine.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket client connected from the local machine; otherwise false.</returns>
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x0600367E RID: 13950 RVA: 0x000C91E6 File Offset: 0x000C73E6
		public override bool IsLocal
		{
			get
			{
				return this._isLocal;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket connection is secured using Secure Sockets Layer (SSL).</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket connection is secured using SSL; otherwise false.</returns>
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000C91EE File Offset: 0x000C73EE
		public override bool IsSecureConnection
		{
			get
			{
				return this._isSecureConnection;
			}
		}

		/// <summary>Gets the WebSocket instance used to send and receive data over the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.The WebSocket instance.</returns>
		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06003680 RID: 13952 RVA: 0x000C91F6 File Offset: 0x000C73F6
		public override WebSocket WebSocket
		{
			get
			{
				return this._webSocket;
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000C9200 File Offset: 0x000C7400
		private static IPrincipal CopyPrincipal(IPrincipal user)
		{
			if (user != null)
			{
				if (user is WindowsPrincipal)
				{
					throw new PlatformNotSupportedException();
				}
				HttpListenerBasicIdentity httpListenerBasicIdentity;
				if ((httpListenerBasicIdentity = user.Identity as HttpListenerBasicIdentity) != null)
				{
					return new GenericPrincipal(new HttpListenerBasicIdentity(httpListenerBasicIdentity.Name, httpListenerBasicIdentity.Password), null);
				}
			}
			return null;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal HttpListenerWebSocketContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002B60 RID: 11104
		private readonly Uri _requestUri;

		// Token: 0x04002B61 RID: 11105
		private readonly NameValueCollection _headers;

		// Token: 0x04002B62 RID: 11106
		private readonly CookieCollection _cookieCollection;

		// Token: 0x04002B63 RID: 11107
		private readonly IPrincipal _user;

		// Token: 0x04002B64 RID: 11108
		private readonly bool _isAuthenticated;

		// Token: 0x04002B65 RID: 11109
		private readonly bool _isLocal;

		// Token: 0x04002B66 RID: 11110
		private readonly bool _isSecureConnection;

		// Token: 0x04002B67 RID: 11111
		private readonly string _origin;

		// Token: 0x04002B68 RID: 11112
		private readonly IEnumerable<string> _secWebSocketProtocols;

		// Token: 0x04002B69 RID: 11113
		private readonly string _secWebSocketVersion;

		// Token: 0x04002B6A RID: 11114
		private readonly string _secWebSocketKey;

		// Token: 0x04002B6B RID: 11115
		private readonly WebSocket _webSocket;
	}
}
