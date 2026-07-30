using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200061E RID: 1566
	internal class XmlIntSortKey : XmlSortKey
	{
		// Token: 0x06003D66 RID: 15718 RVA: 0x001538FE File Offset: 0x00151AFE
		public XmlIntSortKey(int value, XmlCollation collation)
		{
			this.intVal = (collation.DescendingOrder ? (~value) : value);
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x0015391C File Offset: 0x00151B1C
		public override int CompareTo(object obj)
		{
			XmlIntSortKey xmlIntSortKey = obj as XmlIntSortKey;
			if (xmlIntSortKey == null)
			{
				return base.CompareToEmpty(obj);
			}
			if (this.intVal == xmlIntSortKey.intVal)
			{
				return base.BreakSortingTie(xmlIntSortKey);
			}
			if (this.intVal >= xmlIntSortKey.intVal)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x040027D0 RID: 10192
		private int intVal;
	}
}
