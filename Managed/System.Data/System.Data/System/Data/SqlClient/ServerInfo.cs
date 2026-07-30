using System;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x020001CC RID: 460
	internal sealed class ServerInfo
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0006BF04 File Offset: 0x0006A104
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x0006BF0C File Offset: 0x0006A10C
		internal string ExtendedServerName { get; private set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0006BF15 File Offset: 0x0006A115
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x0006BF1D File Offset: 0x0006A11D
		internal string ResolvedServerName { get; private set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0006BF26 File Offset: 0x0006A126
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x0006BF2E File Offset: 0x0006A12E
		internal string ResolvedDatabaseName { get; private set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0006BF37 File Offset: 0x0006A137
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x0006BF3F File Offset: 0x0006A13F
		internal string UserProtocol { get; private set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0006BF48 File Offset: 0x0006A148
		// (set) Token: 0x0600157A RID: 5498 RVA: 0x0006BF50 File Offset: 0x0006A150
		internal string UserServerName
		{
			get
			{
				return this._userServerName;
			}
			private set
			{
				this._userServerName = value;
			}
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0006BF59 File Offset: 0x0006A159
		internal ServerInfo(SqlConnectionString userOptions)
			: this(userOptions, userOptions.DataSource)
		{
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0006BF68 File Offset: 0x0006A168
		internal ServerInfo(SqlConnectionString userOptions, string serverName)
		{
			this.UserServerName = serverName ?? string.Empty;
			this.UserProtocol = string.Empty;
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
			this.PreRoutingServerName = null;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0006BFA0 File Offset: 0x0006A1A0
		internal ServerInfo(SqlConnectionString userOptions, RoutingInfo routing, string preRoutingServerName)
		{
			if (routing == null || routing.ServerName == null)
			{
				this.UserServerName = string.Empty;
			}
			else
			{
				this.UserServerName = string.Format(CultureInfo.InvariantCulture, "{0},{1}", routing.ServerName, routing.Port);
			}
			this.PreRoutingServerName = preRoutingServerName;
			this.UserProtocol = "tcp";
			this.SetDerivedNames(this.UserProtocol, this.UserServerName);
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0006C021 File Offset: 0x0006A221
		internal void SetDerivedNames(string protocol, string serverName)
		{
			if (!string.IsNullOrEmpty(protocol))
			{
				this.ExtendedServerName = protocol + ":" + serverName;
			}
			else
			{
				this.ExtendedServerName = serverName;
			}
			this.ResolvedServerName = serverName;
		}

		// Token: 0x04000E71 RID: 3697
		private string _userServerName;

		// Token: 0x04000E72 RID: 3698
		internal readonly string PreRoutingServerName;
	}
}
