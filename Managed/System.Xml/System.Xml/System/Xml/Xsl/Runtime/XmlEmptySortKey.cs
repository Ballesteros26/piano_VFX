using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200061B RID: 1563
	internal class XmlEmptySortKey : XmlSortKey
	{
		// Token: 0x06003D5F RID: 15711 RVA: 0x001537EA File Offset: 0x001519EA
		public XmlEmptySortKey(XmlCollation collation)
		{
			this.isEmptyGreatest = collation.EmptyGreatest != collation.DescendingOrder;
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06003D60 RID: 15712 RVA: 0x00153809 File Offset: 0x00151A09
		public bool IsEmptyGreatest
		{
			get
			{
				return this.isEmptyGreatest;
			}
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x00153814 File Offset: 0x00151A14
		public override int CompareTo(object obj)
		{
			XmlEmptySortKey xmlEmptySortKey = obj as XmlEmptySortKey;
			if (xmlEmptySortKey == null)
			{
				return -(obj as XmlSortKey).CompareTo(this);
			}
			return base.BreakSortingTie(xmlEmptySortKey);
		}

		// Token: 0x040027CD RID: 10189
		private bool isEmptyGreatest;
	}
}
