using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200010A RID: 266
	internal class EnumerableWrapperWeakToStrong : IEnumerable<object>, IEnumerable
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x0001D6B2 File Offset: 0x0001B8B2
		internal EnumerableWrapperWeakToStrong(IEnumerable wrappedEnumerable)
		{
			this._wrappedEnumerable = wrappedEnumerable;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001D6C1 File Offset: 0x0001B8C1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<object>)this).GetEnumerator();
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001D6C9 File Offset: 0x0001B8C9
		public IEnumerator<object> GetEnumerator()
		{
			return new EnumerableWrapperWeakToStrong.WrapperEnumeratorWeakToStrong(this._wrappedEnumerable.GetEnumerator());
		}

		// Token: 0x04000548 RID: 1352
		private readonly IEnumerable _wrappedEnumerable;

		// Token: 0x0200010B RID: 267
		private class WrapperEnumeratorWeakToStrong : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06000931 RID: 2353 RVA: 0x0001D6DB File Offset: 0x0001B8DB
			internal WrapperEnumeratorWeakToStrong(IEnumerator wrappedEnumerator)
			{
				this._wrappedEnumerator = wrappedEnumerator;
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001D6EA File Offset: 0x0001B8EA
			object IEnumerator.Current
			{
				get
				{
					return this._wrappedEnumerator.Current;
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001D6EA File Offset: 0x0001B8EA
			object IEnumerator<object>.Current
			{
				get
				{
					return this._wrappedEnumerator.Current;
				}
			}

			// Token: 0x06000934 RID: 2356 RVA: 0x0001D6F8 File Offset: 0x0001B8F8
			void IDisposable.Dispose()
			{
				IDisposable disposable = this._wrappedEnumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x06000935 RID: 2357 RVA: 0x0001D71A File Offset: 0x0001B91A
			bool IEnumerator.MoveNext()
			{
				return this._wrappedEnumerator.MoveNext();
			}

			// Token: 0x06000936 RID: 2358 RVA: 0x0001D727 File Offset: 0x0001B927
			void IEnumerator.Reset()
			{
				this._wrappedEnumerator.Reset();
			}

			// Token: 0x04000549 RID: 1353
			private IEnumerator _wrappedEnumerator;
		}
	}
}
