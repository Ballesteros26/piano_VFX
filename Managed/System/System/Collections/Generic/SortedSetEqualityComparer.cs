using System;

namespace System.Collections.Generic
{
	// Token: 0x02000747 RID: 1863
	internal sealed class SortedSetEqualityComparer<T> : IEqualityComparer<SortedSet<T>>
	{
		// Token: 0x06003B20 RID: 15136 RVA: 0x000D742F File Offset: 0x000D562F
		public SortedSetEqualityComparer(IEqualityComparer<T> memberEqualityComparer)
			: this(null, memberEqualityComparer)
		{
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000D7439 File Offset: 0x000D5639
		private SortedSetEqualityComparer(IComparer<T> comparer, IEqualityComparer<T> memberEqualityComparer)
		{
			this._comparer = comparer ?? Comparer<T>.Default;
			this._memberEqualityComparer = memberEqualityComparer ?? EqualityComparer<T>.Default;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000D7461 File Offset: 0x000D5661
		public bool Equals(SortedSet<T> x, SortedSet<T> y)
		{
			return SortedSet<T>.SortedSetEquals(x, y, this._comparer);
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000D7470 File Offset: 0x000D5670
		public int GetHashCode(SortedSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T t in obj)
				{
					num ^= this._memberEqualityComparer.GetHashCode(t) & int.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000D74D4 File Offset: 0x000D56D4
		public override bool Equals(object obj)
		{
			SortedSetEqualityComparer<T> sortedSetEqualityComparer = obj as SortedSetEqualityComparer<T>;
			return sortedSetEqualityComparer != null && this._comparer == sortedSetEqualityComparer._comparer;
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000D74FB File Offset: 0x000D56FB
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode() ^ this._memberEqualityComparer.GetHashCode();
		}

		// Token: 0x04002D38 RID: 11576
		private readonly IComparer<T> _comparer;

		// Token: 0x04002D39 RID: 11577
		private readonly IEqualityComparer<T> _memberEqualityComparer;
	}
}
