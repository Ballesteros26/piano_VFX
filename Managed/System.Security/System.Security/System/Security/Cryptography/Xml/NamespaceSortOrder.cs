using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200006B RID: 107
	internal class NamespaceSortOrder : IComparer
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x00002050 File Offset: 0x00000250
		internal NamespaceSortOrder()
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00009F54 File Offset: 0x00008154
		public int Compare(object a, object b)
		{
			XmlNode xmlNode = a as XmlNode;
			XmlNode xmlNode2 = b as XmlNode;
			if (xmlNode == null || xmlNode2 == null)
			{
				throw new ArgumentException();
			}
			bool flag = Utils.IsDefaultNamespaceNode(xmlNode);
			bool flag2 = Utils.IsDefaultNamespaceNode(xmlNode2);
			if (flag && flag2)
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			if (flag2)
			{
				return 1;
			}
			return string.CompareOrdinal(xmlNode.LocalName, xmlNode2.LocalName);
		}
	}
}
