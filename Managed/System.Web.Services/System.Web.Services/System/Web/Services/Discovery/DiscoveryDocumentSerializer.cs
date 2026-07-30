using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000A4 RID: 164
	internal class DiscoveryDocumentSerializer : XmlSerializer
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00013B91 File Offset: 0x00011D91
		protected override XmlSerializationReader CreateReader()
		{
			return new DiscoveryDocumentSerializationReader();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013B98 File Offset: 0x00011D98
		protected override XmlSerializationWriter CreateWriter()
		{
			return new DiscoveryDocumentSerializationWriter();
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013B9F File Offset: 0x00011D9F
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("discovery", "http://schemas.xmlsoap.org/disco/");
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013BB1 File Offset: 0x00011DB1
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((DiscoveryDocumentSerializationWriter)writer).Write10_discovery(objectToSerialize);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00013BBF File Offset: 0x00011DBF
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((DiscoveryDocumentSerializationReader)reader).Read10_discovery();
		}
	}
}
