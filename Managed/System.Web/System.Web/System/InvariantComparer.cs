using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002F1B File Offset: 0x0000111B
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F34 File Offset: 0x00001134
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

		// Token: 0x04000D48 RID: 3400
		private CompareInfo m_compareInfo;

		// Token: 0x04000D49 RID: 3401
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
