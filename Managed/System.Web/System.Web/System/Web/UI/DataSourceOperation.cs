using System;

namespace System.Web.UI
{
	/// <summary>Specifies a data operation performed by a data source control.</summary>
	// Token: 0x0200015E RID: 350
	public enum DataSourceOperation
	{
		/// <summary>The operation deletes records from a data source.</summary>
		// Token: 0x0400123D RID: 4669
		Delete,
		/// <summary>The operation inserts one or more records into a data source.</summary>
		// Token: 0x0400123E RID: 4670
		Insert,
		/// <summary>The operation retrieves records from a data source.</summary>
		// Token: 0x0400123F RID: 4671
		Select,
		/// <summary>The operation updates records in a data source.</summary>
		// Token: 0x04001240 RID: 4672
		Update,
		/// <summary>The operation retrieves the total number of records for a query from the data source.</summary>
		// Token: 0x04001241 RID: 4673
		SelectCount
	}
}
