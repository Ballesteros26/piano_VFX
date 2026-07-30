using System;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x020005A7 RID: 1447
	internal class MultiAsyncResult : LazyAsyncResult
	{
		// Token: 0x06002D21 RID: 11553 RVA: 0x000B2A28 File Offset: 0x000B0C28
		internal MultiAsyncResult(object context, AsyncCallback callback, object state)
			: base(context, state, callback)
		{
			this.context = context;
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x000B2A3A File Offset: 0x000B0C3A
		internal object Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000B2A42 File Offset: 0x000B0C42
		internal void Enter()
		{
			this.Increment();
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000B2A4A File Offset: 0x000B0C4A
		internal void Leave()
		{
			this.Decrement();
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000B2A52 File Offset: 0x000B0C52
		internal void Leave(object result)
		{
			base.Result = result;
			this.Decrement();
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000B2A61 File Offset: 0x000B0C61
		private void Decrement()
		{
			if (Interlocked.Decrement(ref this.outstanding) == -1)
			{
				base.InvokeCallback(base.Result);
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000B2A7D File Offset: 0x000B0C7D
		private void Increment()
		{
			Interlocked.Increment(ref this.outstanding);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000B2A4A File Offset: 0x000B0C4A
		internal void CompleteSequence()
		{
			this.Decrement();
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000B2A8B File Offset: 0x000B0C8B
		internal static object End(IAsyncResult result)
		{
			MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result;
			multiAsyncResult.InternalWaitForCompletion();
			return multiAsyncResult.Result;
		}

		// Token: 0x04002543 RID: 9539
		private int outstanding;

		// Token: 0x04002544 RID: 9540
		private object context;
	}
}
