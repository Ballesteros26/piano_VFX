using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020001D9 RID: 473
	internal class OrdinalCaseInsensitiveComparer : IComparer
	{
		// Token: 0x06000EEA RID: 3818 RVA: 0x0004640C File Offset: 0x0004460C
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x040010E9 RID: 4329
		internal static readonly OrdinalCaseInsensitiveComparer Default = new OrdinalCaseInsensitiveComparer();
	}
}
