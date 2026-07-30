using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200011C RID: 284
	internal abstract class MergeEnumerator<TInputOutput> : IEnumerator<TInputOutput>, IDisposable, IEnumerator
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0001E2AC File Offset: 0x0001C4AC
		protected MergeEnumerator(QueryTaskGroupState taskGroupState)
		{
			this._taskGroupState = taskGroupState;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000966 RID: 2406
		public abstract TInputOutput Current { get; }

		// Token: 0x06000967 RID: 2407
		public abstract bool MoveNext();

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001E2BB File Offset: 0x0001C4BB
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TInputOutput>)this).Current;
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00003C4C File Offset: 0x00001E4C
		public virtual void Reset()
		{
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		public virtual void Dispose()
		{
			if (!this._taskGroupState.IsAlreadyEnded)
			{
				this._taskGroupState.QueryEnd(true);
			}
		}

		// Token: 0x04000575 RID: 1397
		protected QueryTaskGroupState _taskGroupState;
	}
}
