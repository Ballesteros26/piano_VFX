using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001FA RID: 506
	internal class ForAllSpoolingTask<TInputOutput, TIgnoreKey> : SpoolingTaskBase
	{
		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002A5A7 File Offset: 0x000287A7
		internal ForAllSpoolingTask(int taskIndex, QueryTaskGroupState groupState, QueryOperatorEnumerator<TInputOutput, TIgnoreKey> source)
			: base(taskIndex, groupState)
		{
			this._source = source;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002A5B8 File Offset: 0x000287B8
		protected override void SpoolingWork()
		{
			TInputOutput tinputOutput = default(TInputOutput);
			TIgnoreKey tignoreKey = default(TIgnoreKey);
			while (this._source.MoveNext(ref tinputOutput, ref tignoreKey))
			{
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002A5E6 File Offset: 0x000287E6
		protected override void SpoolingFinally()
		{
			base.SpoolingFinally();
			this._source.Dispose();
		}

		// Token: 0x040007E3 RID: 2019
		private QueryOperatorEnumerator<TInputOutput, TIgnoreKey> _source;
	}
}
