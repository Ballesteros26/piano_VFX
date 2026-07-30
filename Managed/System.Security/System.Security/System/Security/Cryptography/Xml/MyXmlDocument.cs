using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000069 RID: 105
	internal class MyXmlDocument : XmlDocument
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00009ECB File Offset: 0x000080CB
		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return this.CreateAttribute(prefix, localName, namespaceURI);
		}
	}
}
