using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000307 RID: 775
	internal class QNameComparer : IComparer
	{
		// Token: 0x06001CE3 RID: 7395 RVA: 0x0009D214 File Offset: 0x0009B414
		public int Compare(object o1, object o2)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)o1;
			XmlQualifiedName xmlQualifiedName2 = (XmlQualifiedName)o2;
			int num = string.Compare(xmlQualifiedName.Namespace, xmlQualifiedName2.Namespace, StringComparison.Ordinal);
			if (num == 0)
			{
				return string.Compare(xmlQualifiedName.Name, xmlQualifiedName2.Name, StringComparison.Ordinal);
			}
			return num;
		}
	}
}
