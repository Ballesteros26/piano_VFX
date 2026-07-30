using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200021A RID: 538
	internal class RoutingInfo
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0007D080 File Offset: 0x0007B280
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x0007D088 File Offset: 0x0007B288
		internal byte Protocol { get; private set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0007D091 File Offset: 0x0007B291
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0007D099 File Offset: 0x0007B299
		internal ushort Port { get; private set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0007D0A2 File Offset: 0x0007B2A2
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0007D0AA File Offset: 0x0007B2AA
		internal string ServerName { get; private set; }

		// Token: 0x06001876 RID: 6262 RVA: 0x0007D0B3 File Offset: 0x0007B2B3
		internal RoutingInfo(byte protocol, ushort port, string servername)
		{
			this.Protocol = protocol;
			this.Port = port;
			this.ServerName = servername;
		}
	}
}
