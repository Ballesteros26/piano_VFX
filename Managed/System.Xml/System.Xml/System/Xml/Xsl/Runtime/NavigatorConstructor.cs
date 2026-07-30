using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D7 RID: 1495
	internal sealed class NavigatorConstructor
	{
		// Token: 0x06003B18 RID: 15128 RVA: 0x0014D0F0 File Offset: 0x0014B2F0
		public XPathNavigator GetNavigator(XmlEventCache events, XmlNameTable nameTable)
		{
			if (this.cache == null)
			{
				XPathDocument xpathDocument = new XPathDocument(nameTable);
				XmlRawWriter xmlRawWriter = xpathDocument.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames | (events.HasRootNode ? XPathDocument.LoadFlags.None : XPathDocument.LoadFlags.Fragment), events.BaseUri);
				events.EventsToWriter(xmlRawWriter);
				xmlRawWriter.Close();
				this.cache = xpathDocument;
			}
			return ((XPathDocument)this.cache).CreateNavigator();
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x0014D14C File Offset: 0x0014B34C
		public XPathNavigator GetNavigator(string text, string baseUri, XmlNameTable nameTable)
		{
			if (this.cache == null)
			{
				XPathDocument xpathDocument = new XPathDocument(nameTable);
				XmlRawWriter xmlRawWriter = xpathDocument.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames, baseUri);
				xmlRawWriter.WriteString(text);
				xmlRawWriter.Close();
				this.cache = xpathDocument;
			}
			return ((XPathDocument)this.cache).CreateNavigator();
		}

		// Token: 0x040026B4 RID: 9908
		private object cache;
	}
}
