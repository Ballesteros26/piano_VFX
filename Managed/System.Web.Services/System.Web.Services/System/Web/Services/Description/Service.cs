using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Groups together a set of related instances of the <see cref="T:System.Web.Services.Description.Port" /> class that are associated with an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000EC RID: 236
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class Service : NamedItem
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x0001C444 File Offset: 0x0001A644
		internal void SetParent(ServiceDescription parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Service" /> is a member.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Service" /> is a member.</returns>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001C44D File Offset: 0x0001A64D
		public ServiceDescription ServiceDescription
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the collection of extensibility elements associated with the <see cref="T:System.Web.Services.Description.Service" />.</summary>
		/// <returns>The collection of extensibility elements associated with the <see cref="T:System.Web.Services.Description.Service" />.</returns>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001C455 File Offset: 0x0001A655
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

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Port" /> instances contained in the <see cref="T:System.Web.Services.Description.Service" />.</summary>
		/// <returns>A collection of port instances contained in the <see cref="T:System.Web.Services.Description.Service" />.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001C471 File Offset: 0x0001A671
		[XmlElement("port")]
		public PortCollection Ports
		{
			get
			{
				if (this.ports == null)
				{
					this.ports = new PortCollection(this);
				}
				return this.ports;
			}
		}

		// Token: 0x040003EC RID: 1004
		private ServiceDescriptionFormatExtensionCollection extensions;

		// Token: 0x040003ED RID: 1005
		private PortCollection ports;

		// Token: 0x040003EE RID: 1006
		private ServiceDescription parent;
	}
}
