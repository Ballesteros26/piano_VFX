using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000223 RID: 547
	internal sealed class SqlReturnValue : SqlMetaDataPriv
	{
		// Token: 0x06001890 RID: 6288 RVA: 0x0007D5FA File Offset: 0x0007B7FA
		internal SqlReturnValue()
		{
			this.value = new SqlBuffer();
		}

		// Token: 0x040011B2 RID: 4530
		internal string parameter;

		// Token: 0x040011B3 RID: 4531
		internal readonly SqlBuffer value;
	}
}
