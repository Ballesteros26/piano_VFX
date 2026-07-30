using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000C3 RID: 195
	internal class XmlAsyncCheckReaderWithNS : XmlAsyncCheckReader, IXmlNamespaceResolver
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001BE4B File Offset: 0x0001A04B
		public XmlAsyncCheckReaderWithNS(XmlReader reader)
			: base(reader)
		{
			this.readerAsIXmlNamespaceResolver = (IXmlNamespaceResolver)reader;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001BE60 File Offset: 0x0001A060
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsIXmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001BE6E File Offset: 0x0001A06E
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsIXmlNamespaceResolver.LookupNamespace(prefix);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001BE7C File Offset: 0x0001A07C
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsIXmlNamespaceResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040003DD RID: 989
		private readonly IXmlNamespaceResolver readerAsIXmlNamespaceResolver;
	}
}
