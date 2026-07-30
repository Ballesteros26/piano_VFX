using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000306 RID: 774
	internal class XmlFacetComparer : IComparer
	{
		// Token: 0x06001CE1 RID: 7393 RVA: 0x0009D1BC File Offset: 0x0009B3BC
		public int Compare(object o1, object o2)
		{
			XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)o1;
			XmlSchemaFacet xmlSchemaFacet2 = (XmlSchemaFacet)o2;
			return string.Compare(xmlSchemaFacet.GetType().Name + ":" + xmlSchemaFacet.Value, xmlSchemaFacet2.GetType().Name + ":" + xmlSchemaFacet2.Value, StringComparison.Ordinal);
		}
	}
}
