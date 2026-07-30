using System;
using System.Web.Services.Configuration;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Describes data type definitions relevant to exchanged messages. This class cannot be inherited.</summary>
	// Token: 0x020000FB RID: 251
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class Types : DocumentableItem
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0001CAAC File Offset: 0x0001ACAC
		internal bool HasItems()
		{
			return (this.schemas != null && this.schemas.Count > 0) || (this.extensions != null && this.extensions.Count > 0);
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> elements included in the XML Web service. This property is read-only.</summary>
		/// <returns>A collection of extension elements included in the XML Web service.</returns>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001CADE File Offset: 0x0001ACDE
		[XmlIgnore]
		public override ServiceDescriptionFormatExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new ServiceDescriptionFormatExtensionCollection(this);
				}
				return this.extensions;
			}
		}

		/// <summary>Gets the collection of XML schemas included as data type definitions for the XML Web service. This property is read-only.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSchemas" /> collection.</returns>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001CAFA File Offset: 0x0001ACFA
		[XmlElement("schema", typeof(XmlSchema), Namespace = "http://www.w3.org/2001/XMLSchema")]
		public XmlSchemas Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemas();
				}
				return this.schemas;
			}
		}

		// Token: 0x04000410 RID: 1040
		private XmlSchemas schemas;

		// Token: 0x04000411 RID: 1041
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
