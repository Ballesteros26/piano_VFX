using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
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

		// Token: 0x0400002A RID: 42
		private CompareInfo m_compareInfo;

		// Token: 0x0400002B RID: 43
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
