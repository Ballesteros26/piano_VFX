using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000195 RID: 405
	internal sealed class OrderingQueryOperator<TSource> : QueryOperator<TSource>
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x00024EAB File Offset: 0x000230AB
		public OrderingQueryOperator(QueryOperator<TSource> child, bool orderOn)
			: base(orderOn, child.SpecifiedQuerySettings)
		{
			this._child = child;
			this._ordinalIndexState = this._child.OrdinalIndexState;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00024ED2 File Offset: 0x000230D2
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return this._child.Open(settings, preferStriping);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00024EE4 File Offset: 0x000230E4
		internal override IEnumerator<TSource> GetEnumerator(ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			ScanQueryOperator<TSource> scanQueryOperator = this._child as ScanQueryOperator<TSource>;
			if (scanQueryOperator != null)
			{
				return scanQueryOperator.Data.GetEnumerator();
			}
			return base.GetEnumerator(mergeOptions, suppressOrderPreservation);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00024F14 File Offset: 0x00023114
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return this._child.AsSequentialQuery(token);
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00024F22 File Offset: 0x00023122
		internal override bool LimitsParallelism
		{
			get
			{
				return this._child.LimitsParallelism;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00024F2F File Offset: 0x0002312F
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._ordinalIndexState;
			}
		}

		// Token: 0x04000697 RID: 1687
		private QueryOperator<TSource> _child;

		// Token: 0x04000698 RID: 1688
		private OrdinalIndexState _ordinalIndexState;
	}
}
