using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200021C RID: 540
	internal sealed class SqlLogin
	{
		// Token: 0x0400116C RID: 4460
		internal int timeout;

		// Token: 0x0400116D RID: 4461
		internal bool userInstance;

		// Token: 0x0400116E RID: 4462
		internal string hostName = "";

		// Token: 0x0400116F RID: 4463
		internal string userName = "";

		// Token: 0x04001170 RID: 4464
		internal string password = "";

		// Token: 0x04001171 RID: 4465
		internal string applicationName = "";

		// Token: 0x04001172 RID: 4466
		internal string serverName = "";

		// Token: 0x04001173 RID: 4467
		internal string language = "";

		// Token: 0x04001174 RID: 4468
		internal string database = "";

		// Token: 0x04001175 RID: 4469
		internal string attachDBFilename = "";

		// Token: 0x04001176 RID: 4470
		internal bool useReplication;

		// Token: 0x04001177 RID: 4471
		internal bool useSSPI;

		// Token: 0x04001178 RID: 4472
		internal int packetSize = 8000;

		// Token: 0x04001179 RID: 4473
		internal bool readOnlyIntent;
	}
}
