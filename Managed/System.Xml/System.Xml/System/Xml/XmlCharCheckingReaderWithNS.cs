using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000D1 RID: 209
	internal class XmlCharCheckingReaderWithNS : XmlCharCheckingReader, IXmlNamespaceResolver
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0001EE2A File Offset: 0x0001D02A
		internal XmlCharCheckingReaderWithNS(XmlReader reader, IXmlNamespaceResolver readerAsNSResolver, bool checkCharacters, bool ignoreWhitespace, bool ignoreComments, bool ignorePis, DtdProcessing dtdProcessing)
			: base(reader, checkCharacters, ignoreWhitespace, ignoreComments, ignorePis, dtdProcessing)
		{
			this.readerAsNSResolver = readerAsNSResolver;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001EE43 File Offset: 0x0001D043
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001EE51 File Offset: 0x0001D051
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001EE5F File Offset: 0x0001D05F
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x0400041E RID: 1054
		internal IXmlNamespaceResolver readerAsNSResolver;
	}
}
