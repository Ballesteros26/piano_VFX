using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	// Token: 0x02000346 RID: 838
	public static class DbDataReaderExtensions
	{
		// Token: 0x060027C7 RID: 10183 RVA: 0x000B0EDC File Offset: 0x000AF0DC
		public static ReadOnlyCollection<DbColumn> GetColumnSchema(this DbDataReader reader)
		{
			if (reader.CanGetColumnSchema())
			{
				return ((IDbColumnSchemaGenerator)reader).GetColumnSchema();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000B0EF7 File Offset: 0x000AF0F7
		public static bool CanGetColumnSchema(this DbDataReader reader)
		{
			return reader is IDbColumnSchemaGenerator;
		}
	}
}
