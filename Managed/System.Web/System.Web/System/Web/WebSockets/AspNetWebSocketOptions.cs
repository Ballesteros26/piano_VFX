using System;

namespace System.Web.WebSockets
{
	/// <summary>Specifies configuration settings for an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
	// Token: 0x02000109 RID: 265
	public sealed class AspNetWebSocketOptions
	{
		/// <summary>Gets or sets whether the URL that initiatedthe WebSocket connection corresponds to the current server.</summary>
		/// <returns>true if the URL that initiatedthe WebSocket connection corresponds to the current server; otherwise, false.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00025BF6 File Offset: 0x00023DF6
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x00025BFE File Offset: 0x00023DFE
		public bool RequireSameOrigin { get; set; }

		/// <summary>Gets or sets the name of an application-specific protocol that a remote client and a server can use to exchange data over an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The name of the protocol.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The protocol name assigned to the property contains invalid characters.</exception>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00025C07 File Offset: 0x00023E07
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x00025C0F File Offset: 0x00023E0F
		public string SubProtocol
		{
			get
			{
				return this._subProtocol;
			}
			set
			{
				if (value != null && !SubProtocolUtil.IsValidSubProtocolName(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._subProtocol = value;
			}
		}

		// Token: 0x0400116F RID: 4463
		private string _subProtocol;
	}
}
