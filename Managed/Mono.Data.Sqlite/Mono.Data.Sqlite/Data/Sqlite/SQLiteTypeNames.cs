using System;
using System.Data;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200001D RID: 29
	internal struct SQLiteTypeNames
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00009CE5 File Offset: 0x00007EE5
		internal SQLiteTypeNames(string newtypeName, DbType newdataType)
		{
			this.typeName = newtypeName;
			this.dataType = newdataType;
		}

		// Token: 0x04000093 RID: 147
		internal string typeName;

		// Token: 0x04000094 RID: 148
		internal DbType dataType;
	}
}
