using System;
using System.Data.SqlClient;

namespace System.Data.Common
{
	// Token: 0x02000383 RID: 899
	internal static class DbConnectionStringDefaults
	{
		// Token: 0x0400198D RID: 6541
		internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

		// Token: 0x0400198E RID: 6542
		internal const string ApplicationName = "Core .Net SqlClient Data Provider";

		// Token: 0x0400198F RID: 6543
		internal const string AttachDBFilename = "";

		// Token: 0x04001990 RID: 6544
		internal const int ConnectTimeout = 15;

		// Token: 0x04001991 RID: 6545
		internal const string CurrentLanguage = "";

		// Token: 0x04001992 RID: 6546
		internal const string DataSource = "";

		// Token: 0x04001993 RID: 6547
		internal const bool Encrypt = false;

		// Token: 0x04001994 RID: 6548
		internal const bool Enlist = true;

		// Token: 0x04001995 RID: 6549
		internal const string FailoverPartner = "";

		// Token: 0x04001996 RID: 6550
		internal const string InitialCatalog = "";

		// Token: 0x04001997 RID: 6551
		internal const bool IntegratedSecurity = false;

		// Token: 0x04001998 RID: 6552
		internal const int LoadBalanceTimeout = 0;

		// Token: 0x04001999 RID: 6553
		internal const bool MultipleActiveResultSets = false;

		// Token: 0x0400199A RID: 6554
		internal const bool MultiSubnetFailover = false;

		// Token: 0x0400199B RID: 6555
		internal const int MaxPoolSize = 100;

		// Token: 0x0400199C RID: 6556
		internal const int MinPoolSize = 0;

		// Token: 0x0400199D RID: 6557
		internal const int PacketSize = 8000;

		// Token: 0x0400199E RID: 6558
		internal const string Password = "";

		// Token: 0x0400199F RID: 6559
		internal const bool PersistSecurityInfo = false;

		// Token: 0x040019A0 RID: 6560
		internal const bool Pooling = true;

		// Token: 0x040019A1 RID: 6561
		internal const bool TrustServerCertificate = false;

		// Token: 0x040019A2 RID: 6562
		internal const string TypeSystemVersion = "Latest";

		// Token: 0x040019A3 RID: 6563
		internal const string UserID = "";

		// Token: 0x040019A4 RID: 6564
		internal const bool UserInstance = false;

		// Token: 0x040019A5 RID: 6565
		internal const bool Replication = false;

		// Token: 0x040019A6 RID: 6566
		internal const string WorkstationID = "";

		// Token: 0x040019A7 RID: 6567
		internal const string TransactionBinding = "Implicit Unbind";

		// Token: 0x040019A8 RID: 6568
		internal const int ConnectRetryCount = 1;

		// Token: 0x040019A9 RID: 6569
		internal const int ConnectRetryInterval = 10;

		// Token: 0x040019AA RID: 6570
		internal const string Dsn = "";

		// Token: 0x040019AB RID: 6571
		internal const string Driver = "";
	}
}
