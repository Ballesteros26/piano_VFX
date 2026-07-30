using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200021B RID: 539
	internal sealed class SqlEnvChange
	{
		// Token: 0x0400115F RID: 4447
		internal byte type;

		// Token: 0x04001160 RID: 4448
		internal byte oldLength;

		// Token: 0x04001161 RID: 4449
		internal int newLength;

		// Token: 0x04001162 RID: 4450
		internal int length;

		// Token: 0x04001163 RID: 4451
		internal string newValue;

		// Token: 0x04001164 RID: 4452
		internal string oldValue;

		// Token: 0x04001165 RID: 4453
		internal byte[] newBinValue;

		// Token: 0x04001166 RID: 4454
		internal byte[] oldBinValue;

		// Token: 0x04001167 RID: 4455
		internal long newLongValue;

		// Token: 0x04001168 RID: 4456
		internal long oldLongValue;

		// Token: 0x04001169 RID: 4457
		internal SqlCollation newCollation;

		// Token: 0x0400116A RID: 4458
		internal SqlCollation oldCollation;

		// Token: 0x0400116B RID: 4459
		internal RoutingInfo newRoutingInfo;
	}
}
