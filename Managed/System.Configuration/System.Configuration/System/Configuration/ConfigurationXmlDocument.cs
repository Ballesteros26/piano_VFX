using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000013 RID: 19
	internal class ConfigurationXmlDocument : XmlDocument
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000026E0 File Offset: 0x000008E0
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			if (namespaceURI == "http://schemas.microsoft.com/.NetConfiguration/v2.0")
			{
				return base.CreateElement(string.Empty, localName, string.Empty);
			}
			return base.CreateElement(prefix, localName, namespaceURI);
		}
	}
}
