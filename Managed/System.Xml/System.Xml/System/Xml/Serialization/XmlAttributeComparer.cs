using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000305 RID: 773
	internal class XmlAttributeComparer : IComparer
	{
		// Token: 0x06001CDF RID: 7391 RVA: 0x0009D174 File Offset: 0x0009B374
		public int Compare(object o1, object o2)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)o1;
			XmlAttribute xmlAttribute2 = (XmlAttribute)o2;
			int num = string.Compare(xmlAttribute.NamespaceURI, xmlAttribute2.NamespaceURI, StringComparison.Ordinal);
			if (num == 0)
			{
				return string.Compare(xmlAttribute.Name, xmlAttribute2.Name, StringComparison.Ordinal);
			}
			return num;
		}
	}
}
