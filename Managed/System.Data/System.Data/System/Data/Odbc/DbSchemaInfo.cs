using System;

namespace System.Data.Odbc
{
	// Token: 0x0200025E RID: 606
	internal sealed class DbSchemaInfo
	{
		// Token: 0x06001ACA RID: 6858 RVA: 0x00005C14 File Offset: 0x00003E14
		internal DbSchemaInfo()
		{
		}

		// Token: 0x0400132D RID: 4909
		internal string _name;

		// Token: 0x0400132E RID: 4910
		internal string _typename;

		// Token: 0x0400132F RID: 4911
		internal Type _type;

		// Token: 0x04001330 RID: 4912
		internal ODBC32.SQL_TYPE? _dbtype;
	}
}
