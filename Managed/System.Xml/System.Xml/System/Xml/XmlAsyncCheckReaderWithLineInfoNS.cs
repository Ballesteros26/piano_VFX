using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000C5 RID: 197
	internal class XmlAsyncCheckReaderWithLineInfoNS : XmlAsyncCheckReaderWithLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x0001BEC6 File Offset: 0x0001A0C6
		public XmlAsyncCheckReaderWithLineInfoNS(XmlReader reader)
			: base(reader)
		{
			this.readerAsIXmlNamespaceResolver = (IXmlNamespaceResolver)reader;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001BEDB File Offset: 0x0001A0DB
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsIXmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001BEE9 File Offset: 0x0001A0E9
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsIXmlNamespaceResolver.LookupNamespace(prefix);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001BEF7 File Offset: 0x0001A0F7
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsIXmlNamespaceResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040003DF RID: 991
		private readonly IXmlNamespaceResolver readerAsIXmlNamespaceResolver;
	}
}
