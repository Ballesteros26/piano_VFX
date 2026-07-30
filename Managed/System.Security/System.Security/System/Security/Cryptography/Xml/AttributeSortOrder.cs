using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003D RID: 61
	internal class AttributeSortOrder : IComparer
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00002050 File Offset: 0x00000250
		internal AttributeSortOrder()
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004D80 File Offset: 0x00002F80
		public int Compare(object a, object b)
		{
			XmlNode xmlNode = a as XmlNode;
			XmlNode xmlNode2 = b as XmlNode;
			if (xmlNode == null || xmlNode2 == null)
			{
				throw new ArgumentException();
			}
			int num = string.CompareOrdinal(xmlNode.NamespaceURI, xmlNode2.NamespaceURI);
			if (num != 0)
			{
				return num;
			}
			return string.CompareOrdinal(xmlNode.LocalName, xmlNode2.LocalName);
		}
	}
}
