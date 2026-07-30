using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200021D RID: 541
	internal struct WrapperEqualityComparer<T> : IEqualityComparer<Wrapper<T>>
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x0002C3CF File Offset: 0x0002A5CF
		internal WrapperEqualityComparer(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				this._comparer = EqualityComparer<T>.Default;
				return;
			}
			this._comparer = comparer;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002C3E7 File Offset: 0x0002A5E7
		public bool Equals(Wrapper<T> x, Wrapper<T> y)
		{
			return this._comparer.Equals(x.Value, y.Value);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002C400 File Offset: 0x0002A600
		public int GetHashCode(Wrapper<T> x)
		{
			return this._comparer.GetHashCode(x.Value);
		}

		// Token: 0x04000841 RID: 2113
		private IEqualityComparer<T> _comparer;
	}
}
