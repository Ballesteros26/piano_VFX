using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x06000376 RID: 886 RVA: 0x0000D2CE File Offset: 0x0000B4CE
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x040001EA RID: 490
		private CompareInfo m_compareInfo;

		// Token: 0x040001EB RID: 491
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
