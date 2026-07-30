using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200061C RID: 1564
	internal class XmlDecimalSortKey : XmlSortKey
	{
		// Token: 0x06003D62 RID: 15714 RVA: 0x00153840 File Offset: 0x00151A40
		public XmlDecimalSortKey(decimal value, XmlCollation collation)
		{
			this.decVal = (collation.DescendingOrder ? (-value) : value);
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x00153860 File Offset: 0x00151A60
		public override int CompareTo(object obj)
		{
			XmlDecimalSortKey xmlDecimalSortKey = obj as XmlDecimalSortKey;
			if (xmlDecimalSortKey == null)
			{
				return base.CompareToEmpty(obj);
			}
			int num = decimal.Compare(this.decVal, xmlDecimalSortKey.decVal);
			if (num == 0)
			{
				return base.BreakSortingTie(xmlDecimalSortKey);
			}
			return num;
		}

		// Token: 0x040027CE RID: 10190
		private decimal decVal;
	}
}
