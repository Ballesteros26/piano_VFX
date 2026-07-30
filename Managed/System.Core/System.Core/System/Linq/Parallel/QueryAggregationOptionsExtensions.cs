using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000110 RID: 272
	internal static class QueryAggregationOptionsExtensions
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x0001D790 File Offset: 0x0001B990
		public static bool IsValidQueryAggregationOption(this QueryAggregationOptions value)
		{
			return value == QueryAggregationOptions.None || value == QueryAggregationOptions.Associative || value == QueryAggregationOptions.Commutative || value == QueryAggregationOptions.AssociativeCommutative;
		}
	}
}
