using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0001ADED File Offset: 0x00018FED
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001AE08 File Offset: 0x00019008
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

		// Token: 0x04000C2C RID: 3116
		private CompareInfo m_compareInfo;

		// Token: 0x04000C2D RID: 3117
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
