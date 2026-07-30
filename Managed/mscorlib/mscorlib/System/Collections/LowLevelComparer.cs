using System;

namespace System.Collections
{
	// Token: 0x020009AB RID: 2475
	internal sealed class LowLevelComparer : IComparer
	{
		// Token: 0x06005AA8 RID: 23208 RVA: 0x00002111 File Offset: 0x00000311
		private LowLevelComparer()
		{
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x0012C900 File Offset: 0x0012AB00
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			IComparable comparable2 = b as IComparable;
			if (comparable2 != null)
			{
				return -comparable2.CompareTo(a);
			}
			throw new ArgumentException("At least one object must implement IComparable.");
		}

		// Token: 0x04002EFE RID: 12030
		internal static readonly LowLevelComparer Default = new LowLevelComparer();
	}
}
