using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x0200019E RID: 414
	internal class QueryOpeningEnumerator<TOutput> : IEnumerator<TOutput>, IDisposable, IEnumerator
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x000253E3 File Offset: 0x000235E3
		internal QueryOpeningEnumerator(QueryOperator<TOutput> queryOperator, ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			this._queryOperator = queryOperator;
			this._mergeOptions = mergeOptions;
			this._suppressOrderPreservation = suppressOrderPreservation;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00025417 File Offset: 0x00023617
		public TOutput Current
		{
			get
			{
				if (this._openedQueryEnumerator == null)
				{
					throw new InvalidOperationException("Enumeration has not started. MoveNext must be called to initiate enumeration.");
				}
				return this._openedQueryEnumerator.Current;
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00025438 File Offset: 0x00023638
		public void Dispose()
		{
			this._topLevelDisposedFlag.Value = true;
			this._topLevelCancellationTokenSource.Cancel();
			if (this._openedQueryEnumerator != null)
			{
				this._openedQueryEnumerator.Dispose();
				this._querySettings.CleanStateAtQueryEnd();
			}
			QueryLifecycle.LogicalQueryExecutionEnd(this._querySettings.QueryId);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001E2BB File Offset: 0x0001C4BB
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TOutput>)this).Current;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002548C File Offset: 0x0002368C
		public bool MoveNext()
		{
			if (this._topLevelDisposedFlag.Value)
			{
				throw new ObjectDisposedException("enumerator", "The query enumerator has been disposed.");
			}
			if (this._openedQueryEnumerator == null)
			{
				this.OpenQuery();
			}
			bool flag = this._openedQueryEnumerator.MoveNext();
			if ((this._moveNextIteration & 63) == 0)
			{
				CancellationState.ThrowWithStandardMessageIfCanceled(this._querySettings.CancellationState.ExternalCancellationToken);
			}
			this._moveNextIteration++;
			return flag;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00025500 File Offset: 0x00023700
		private void OpenQuery()
		{
			if (this._hasQueryOpeningFailed)
			{
				throw new InvalidOperationException("The query enumerator previously threw an exception.");
			}
			try
			{
				this._querySettings = this._queryOperator.SpecifiedQuerySettings.WithPerExecutionSettings(this._topLevelCancellationTokenSource, this._topLevelDisposedFlag).WithDefaults();
				QueryLifecycle.LogicalQueryExecutionBegin(this._querySettings.QueryId);
				this._openedQueryEnumerator = this._queryOperator.GetOpenedEnumerator(this._mergeOptions, this._suppressOrderPreservation, false, this._querySettings);
				CancellationState.ThrowWithStandardMessageIfCanceled(this._querySettings.CancellationState.ExternalCancellationToken);
			}
			catch
			{
				this._hasQueryOpeningFailed = true;
				throw;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00003CCF File Offset: 0x00001ECF
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040006B1 RID: 1713
		private readonly QueryOperator<TOutput> _queryOperator;

		// Token: 0x040006B2 RID: 1714
		private IEnumerator<TOutput> _openedQueryEnumerator;

		// Token: 0x040006B3 RID: 1715
		private QuerySettings _querySettings;

		// Token: 0x040006B4 RID: 1716
		private readonly ParallelMergeOptions? _mergeOptions;

		// Token: 0x040006B5 RID: 1717
		private readonly bool _suppressOrderPreservation;

		// Token: 0x040006B6 RID: 1718
		private int _moveNextIteration;

		// Token: 0x040006B7 RID: 1719
		private bool _hasQueryOpeningFailed;

		// Token: 0x040006B8 RID: 1720
		private readonly Shared<bool> _topLevelDisposedFlag = new Shared<bool>(false);

		// Token: 0x040006B9 RID: 1721
		private readonly CancellationTokenSource _topLevelCancellationTokenSource = new CancellationTokenSource();
	}
}
