using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;

namespace System.Net.WebSockets
{
	/// <summary>Used for accessing the information in the WebSocket handshake.</summary>
	// Token: 0x020006E0 RID: 1760
	public abstract class WebSocketContext
	{
		/// <summary>The URI requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.</returns>
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060036E4 RID: 14052
		public abstract Uri RequestUri { get; }

		/// <summary>The HTTP headers that were sent to the server during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060036E5 RID: 14053
		public abstract NameValueCollection Headers { get; }

		/// <summary>The value of the Origin HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060036E6 RID: 14054
		public abstract string Origin { get; }

		/// <summary>The value of the SecWebSocketKey HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060036E7 RID: 14055
		public abstract IEnumerable<string> SecWebSocketProtocols { get; }

		/// <summary>The list of subprotocols requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060036E8 RID: 14056
		public abstract string SecWebSocketVersion { get; }

		/// <summary>The value of the SecWebSocketKey HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060036E9 RID: 14057
		public abstract string SecWebSocketKey { get; }

		/// <summary>The cookies that were passed to the server during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060036EA RID: 14058
		public abstract CookieCollection CookieCollection { get; }

		/// <summary>An object used to obtain identity, authentication information, and security roles for the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Security.Principal.IPrincipal" />.</returns>
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060036EB RID: 14059
		public abstract IPrincipal User { get; }

		/// <summary>Whether the WebSocket client is authenticated.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060036EC RID: 14060
		public abstract bool IsAuthenticated { get; }

		/// <summary>Whether the WebSocket client connected from the local machine.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060036ED RID: 14061
		public abstract bool IsLocal { get; }

		/// <summary>Whether the WebSocket connection is secured using Secure Sockets Layer (SSL).</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060036EE RID: 14062
		public abstract bool IsSecureConnection { get; }

		/// <summary>The WebSocket instance used to interact (send/receive/close/etc) with the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060036EF RID: 14063
		public abstract WebSocket WebSocket { get; }
	}
}
