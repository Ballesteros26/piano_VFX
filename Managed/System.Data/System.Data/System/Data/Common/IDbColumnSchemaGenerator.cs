using System;
using System.Collections.ObjectModel;

namespace System.Data.Common
{
	// Token: 0x02000356 RID: 854
	public interface IDbColumnSchemaGenerator
	{
		// Token: 0x06002877 RID: 10359
		ReadOnlyCollection<DbColumn> GetColumnSchema();
	}
}
