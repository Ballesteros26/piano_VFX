using System;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Defines an individual endpoint contained in the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000EB RID: 235
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class Port : NamedItem
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0001C3F3 File Offset: 0x0001A5F3
		internal void SetParent(Service parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.Service" /> of which the <see cref="T:System.Web.Services.Description.Port" /> is a member.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.Service" /> of which the <see cref="T:System.Web.Services.Description.Port" /> is a member.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		public Service Service
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the collection of extensibility elements associated with the <see cref="T:System.Web.Services.Description.Port" />.</summary>
		/// <returns>The collection of extensibility elements associated with the port.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001C404 File Offset: 0x0001A604
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

		/// <summary>Gets or sets the value of the XML &lt;binding&gt; attribute of the <see cref="T:System.Web.Services.Description.Port" />.</summary>
		/// <returns>The value of the XML binding.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001C420 File Offset: 0x0001A620
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0001C428 File Offset: 0x0001A628
		[XmlAttribute("binding")]
		public XmlQualifiedName Binding
		{
			get
			{
				return this.binding;
			}
			set
			{
				this.binding = value;
			}
		}

		// Token: 0x040003E9 RID: 1001
		private ServiceDescriptionFormatExtensionCollection extensions;

		// Token: 0x040003EA RID: 1002
		private XmlQualifiedName binding = XmlQualifiedName.Empty;

		// Token: 0x040003EB RID: 1003
		private Service parent;
	}
}
