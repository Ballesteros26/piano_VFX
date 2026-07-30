using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200013E RID: 318
	internal static class SNINativeMethodWrapper
	{
		// Token: 0x0200013F RID: 319
		internal enum SniSpecialErrors : uint
		{
			// Token: 0x04000A8D RID: 2701
			LocalDBErrorCode = 50U,
			// Token: 0x04000A8E RID: 2702
			MultiSubnetFailoverWithMoreThan64IPs = 47U,
			// Token: 0x04000A8F RID: 2703
			MultiSubnetFailoverWithInstanceSpecified,
			// Token: 0x04000A90 RID: 2704
			MultiSubnetFailoverWithNonTcpProtocol,
			// Token: 0x04000A91 RID: 2705
			MaxErrorValue = 50157U
		}
	}
}
