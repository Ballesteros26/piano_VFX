using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Linq.Parallel
{
	// Token: 0x02000118 RID: 280
	internal class ArrayMergeHelper<TInputOutput> : IMergeHelper<TInputOutput>
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x0001DD18 File Offset: 0x0001BF18
		public ArrayMergeHelper(QuerySettings settings, QueryResults<TInputOutput> queryResults)
		{
			this._settings = settings;
			this._queryResults = queryResults;
			int count = this._queryResults.Count;
			this._outputArray = new TInputOutput[count];
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001DD51 File Offset: 0x0001BF51
		private void ToArrayElement(int index)
		{
			this._outputArray[index] = this._queryResults[index];
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001DD6B File Offset: 0x0001BF6B
		public void Execute()
		{
			new QueryExecutionOption<int>(QueryOperator<int>.AsQueryOperator(ParallelEnumerable.Range(0, this._queryResults.Count)), this._settings).ForAll(new Action<int>(this.ToArrayElement));
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001DD9F File Offset: 0x0001BF9F
		[ExcludeFromCodeCoverage]
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			return this.GetResultsAsArray().GetEnumerator();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		public TInputOutput[] GetResultsAsArray()
		{
			return this._outputArray;
		}

		// Token: 0x04000566 RID: 1382
		private QueryResults<TInputOutput> _queryResults;

		// Token: 0x04000567 RID: 1383
		private TInputOutput[] _outputArray;

		// Token: 0x04000568 RID: 1384
		private QuerySettings _settings;
	}
}
