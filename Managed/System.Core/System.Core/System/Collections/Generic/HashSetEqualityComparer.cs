using System;

namespace System.Collections.Generic
{
	// Token: 0x02000354 RID: 852
	[Serializable]
	internal sealed class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>>
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x00055FAB File Offset: 0x000541AB
		public HashSetEqualityComparer()
		{
			this._comparer = EqualityComparer<T>.Default;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00055FBE File Offset: 0x000541BE
		public bool Equals(HashSet<T> x, HashSet<T> y)
		{
			return HashSet<T>.HashSetEquals(x, y, this._comparer);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00055FD0 File Offset: 0x000541D0
		public int GetHashCode(HashSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T t in obj)
				{
					num ^= this._comparer.GetHashCode(t) & int.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00056034 File Offset: 0x00054234
		public override bool Equals(object obj)
		{
			HashSetEqualityComparer<T> hashSetEqualityComparer = obj as HashSetEqualityComparer<T>;
			return hashSetEqualityComparer != null && this._comparer == hashSetEqualityComparer._comparer;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0005605B File Offset: 0x0005425B
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode();
		}

		// Token: 0x04000B8B RID: 2955
		private readonly IEqualityComparer<T> _comparer;
	}
}
