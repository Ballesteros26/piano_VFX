using System;
using System.Collections.Generic;
using System.Linq.Parallel;

namespace System.Linq
{
	// Token: 0x02000090 RID: 144
	internal static class AggregationMinMaxHelpers<T>
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000850C File Offset: 0x0000670C
		private static T Reduce(IEnumerable<T> source, int sign)
		{
			Func<Pair<bool, T>, T, Pair<bool, T>> func = AggregationMinMaxHelpers<T>.MakeIntermediateReduceFunction(sign);
			Func<Pair<bool, T>, Pair<bool, T>, Pair<bool, T>> func2 = AggregationMinMaxHelpers<T>.MakeFinalReduceFunction(sign);
			Func<Pair<bool, T>, T> func3 = AggregationMinMaxHelpers<T>.MakeResultSelectorFunction();
			return new AssociativeAggregationOperator<T, Pair<bool, T>, T>(source, new Pair<bool, T>(false, default(T)), null, true, func, func2, func3, default(T) != null, QueryAggregationOptions.AssociativeCommutative).Aggregate();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000855E File Offset: 0x0000675E
		internal static T ReduceMin(IEnumerable<T> source)
		{
			return AggregationMinMaxHelpers<T>.Reduce(source, -1);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00008567 File Offset: 0x00006767
		internal static T ReduceMax(IEnumerable<T> source)
		{
			return AggregationMinMaxHelpers<T>.Reduce(source, 1);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00008570 File Offset: 0x00006770
		private static Func<Pair<bool, T>, T, Pair<bool, T>> MakeIntermediateReduceFunction(int sign)
		{
			Comparer<T> comparer = Util.GetDefaultComparer<T>();
			return delegate(Pair<bool, T> accumulator, T element)
			{
				if ((default(T) != null || element != null) && (!accumulator.First || Util.Sign(comparer.Compare(element, accumulator.Second)) == sign))
				{
					return new Pair<bool, T>(true, element);
				}
				return accumulator;
			};
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008594 File Offset: 0x00006794
		private static Func<Pair<bool, T>, Pair<bool, T>, Pair<bool, T>> MakeFinalReduceFunction(int sign)
		{
			Comparer<T> comparer = Util.GetDefaultComparer<T>();
			return delegate(Pair<bool, T> accumulator, Pair<bool, T> element)
			{
				if (element.First && (!accumulator.First || Util.Sign(comparer.Compare(element.Second, accumulator.Second)) == sign))
				{
					return new Pair<bool, T>(true, element.Second);
				}
				return accumulator;
			};
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000085B8 File Offset: 0x000067B8
		private static Func<Pair<bool, T>, T> MakeResultSelectorFunction()
		{
			return (Pair<bool, T> accumulator) => accumulator.Second;
		}
	}
}
