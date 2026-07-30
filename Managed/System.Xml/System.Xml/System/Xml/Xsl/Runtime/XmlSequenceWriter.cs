using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000617 RID: 1559
	internal abstract class XmlSequenceWriter
	{
		// Token: 0x06003D48 RID: 15688
		public abstract XmlRawWriter StartTree(XPathNodeType rootType, IXmlNamespaceResolver nsResolver, XmlNameTable nameTable);

		// Token: 0x06003D49 RID: 15689
		public abstract void EndTree();

		// Token: 0x06003D4A RID: 15690
		public abstract void WriteItem(XPathItem item);
	}
}
