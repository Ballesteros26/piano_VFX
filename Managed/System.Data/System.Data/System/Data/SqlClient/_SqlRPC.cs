using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000222 RID: 546
	internal sealed class _SqlRPC
	{
		// Token: 0x0600188E RID: 6286 RVA: 0x0007D5D5 File Offset: 0x0007B7D5
		internal string GetCommandTextOrRpcName()
		{
			if (10 == this.ProcID)
			{
				return (string)this.parameters[0].Value;
			}
			return this.rpcName;
		}

		// Token: 0x040011A5 RID: 4517
		internal string rpcName;

		// Token: 0x040011A6 RID: 4518
		internal ushort ProcID;

		// Token: 0x040011A7 RID: 4519
		internal ushort options;

		// Token: 0x040011A8 RID: 4520
		internal SqlParameter[] parameters;

		// Token: 0x040011A9 RID: 4521
		internal byte[] paramoptions;

		// Token: 0x040011AA RID: 4522
		internal int? recordsAffected;

		// Token: 0x040011AB RID: 4523
		internal int cumulativeRecordsAffected;

		// Token: 0x040011AC RID: 4524
		internal int errorsIndexStart;

		// Token: 0x040011AD RID: 4525
		internal int errorsIndexEnd;

		// Token: 0x040011AE RID: 4526
		internal SqlErrorCollection errors;

		// Token: 0x040011AF RID: 4527
		internal int warningsIndexStart;

		// Token: 0x040011B0 RID: 4528
		internal int warningsIndexEnd;

		// Token: 0x040011B1 RID: 4529
		internal SqlErrorCollection warnings;
	}
}
