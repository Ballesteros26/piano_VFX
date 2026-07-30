using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000196 RID: 406
	internal class QueryExecutionOption<TSource> : QueryOperator<TSource>
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x00024F37 File Offset: 0x00023137
		internal QueryExecutionOption(QueryOperator<TSource> source, QuerySettings settings)
			: base(source.OutputOrdered, settings.Merge(source.SpecifiedQuerySettings))
		{
			this._child = source;
			this._indexState = this._child.OrdinalIndexState;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00024F6A File Offset: 0x0002316A
		internal override QueryResults<TSource> Open(QuerySettings settings, bool preferStriping)
		{
			return this._child.Open(settings, preferStriping);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00024F79 File Offset: 0x00023179
		internal override IEnumerable<TSource> AsSequentialQuery(CancellationToken token)
		{
			return this._child.AsSequentialQuery(token);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00024F87 File Offset: 0x00023187
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return this._indexState;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00024F8F File Offset: 0x0002318F
		internal override bool LimitsParallelism
		{
			get
			{
				return this._child.LimitsParallelism;
			}
		}

		// Token: 0x04000699 RID: 1689
		private QueryOperator<TSource> _child;

		// Token: 0x0400069A RID: 1690
		private OrdinalIndexState _indexState;
	}
}
