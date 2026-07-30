using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A3D RID: 2621
	[Serializable]
	internal class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x0600608D RID: 24717 RVA: 0x0013DFD5 File Offset: 0x0013C1D5
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x0013DFE4 File Offset: 0x0013C1E4
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04003088 RID: 12424
		private readonly Comparison<T> _comparison;
	}
}
