using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Serialization
{
	// Token: 0x020002D2 RID: 722
	internal class CaseInsensitiveKeyComparer : CaseInsensitiveComparer, IEqualityComparer
	{
		// Token: 0x06001B2C RID: 6956 RVA: 0x00096F57 File Offset: 0x00095157
		public CaseInsensitiveKeyComparer()
			: base(CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00096F64 File Offset: 0x00095164
		bool IEqualityComparer.Equals(object x, object y)
		{
			return base.Compare(x, y) == 0;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00096F71 File Offset: 0x00095171
		int IEqualityComparer.GetHashCode(object obj)
		{
			string text = obj as string;
			if (text == null)
			{
				throw new ArgumentException(null, "obj");
			}
			return text.ToUpper(CultureInfo.CurrentCulture).GetHashCode();
		}
	}
}
