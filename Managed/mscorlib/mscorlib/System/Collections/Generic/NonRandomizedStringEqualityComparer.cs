using System;

namespace System.Collections.Generic
{
	// Token: 0x02000A37 RID: 2615
	[Serializable]
	internal sealed class NonRandomizedStringEqualityComparer : EqualityComparer<string>
	{
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06006073 RID: 24691 RVA: 0x0013DD88 File Offset: 0x0013BF88
		internal new static IEqualityComparer<string> Default
		{
			get
			{
				IEqualityComparer<string> equalityComparer;
				if ((equalityComparer = NonRandomizedStringEqualityComparer.s_nonRandomizedComparer) == null)
				{
					equalityComparer = (NonRandomizedStringEqualityComparer.s_nonRandomizedComparer = new NonRandomizedStringEqualityComparer());
				}
				return equalityComparer;
			}
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x00077BEF File Offset: 0x00075DEF
		public sealed override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x0013DDA2 File Offset: 0x0013BFA2
		public sealed override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetLegacyNonRandomizedHashCode();
		}

		// Token: 0x04003085 RID: 12421
		private static volatile IEqualityComparer<string> s_nonRandomizedComparer;
	}
}
