using System;

namespace System.Data.Common
{
	/// <summary>Specifies the relationship between the columns in a GROUP BY clause and the non-aggregated columns in the select-list of a SELECT statement.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000355 RID: 853
	public enum GroupByBehavior
	{
		/// <summary>The support for the GROUP BY clause is unknown.</summary>
		// Token: 0x04001909 RID: 6409
		Unknown,
		/// <summary>The GROUP BY clause is not supported.</summary>
		// Token: 0x0400190A RID: 6410
		NotSupported,
		/// <summary>There is no relationship between the columns in the GROUP BY clause and the nonaggregated columns in the SELECT list. You may group by any column.</summary>
		// Token: 0x0400190B RID: 6411
		Unrelated,
		/// <summary>The GROUP BY clause must contain all nonaggregated columns in the select list, and can contain other columns not in the select list.</summary>
		// Token: 0x0400190C RID: 6412
		MustContainAll,
		/// <summary>The GROUP BY clause must contain all nonaggregated columns in the select list, and must not contain other columns not in the select list.</summary>
		// Token: 0x0400190D RID: 6413
		ExactMatch
	}
}
