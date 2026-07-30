using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000621 RID: 1569
	internal class XmlDateTimeSortKey : XmlIntegerSortKey
	{
		// Token: 0x06003D6D RID: 15725 RVA: 0x00153B75 File Offset: 0x00151D75
		public XmlDateTimeSortKey(DateTime value, XmlCollation collation)
			: base(value.Ticks, collation)
		{
		}
	}
}
