using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200061D RID: 1565
	internal class XmlIntegerSortKey : XmlSortKey
	{
		// Token: 0x06003D64 RID: 15716 RVA: 0x0015389D File Offset: 0x00151A9D
		public XmlIntegerSortKey(long value, XmlCollation collation)
		{
			this.longVal = (collation.DescendingOrder ? (~value) : value);
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x001538B8 File Offset: 0x00151AB8
		public override int CompareTo(object obj)
		{
			XmlIntegerSortKey xmlIntegerSortKey = obj as XmlIntegerSortKey;
			if (xmlIntegerSortKey == null)
			{
				return base.CompareToEmpty(obj);
			}
			if (this.longVal == xmlIntegerSortKey.longVal)
			{
				return base.BreakSortingTie(xmlIntegerSortKey);
			}
			if (this.longVal >= xmlIntegerSortKey.longVal)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x040027CF RID: 10191
		private long longVal;
	}
}
