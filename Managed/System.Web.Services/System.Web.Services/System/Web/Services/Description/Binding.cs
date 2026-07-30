using System;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Specifies the concrete data format and protocols used in the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000F2 RID: 242
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class Binding : NamedItem
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x0001C5B5 File Offset: 0x0001A7B5
		internal void SetParent(ServiceDescription parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Binding" /> is a member.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Binding" /> is a member.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001C5BE File Offset: 0x0001A7BE
		public ServiceDescription ServiceDescription
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the collection of extensibility elements used in the XML Web service.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> object that contains the collection of extensibility elements used in the XML Web service.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001C5C6 File Offset: 0x0001A7C6
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

		/// <summary>Gets the collection of specifications for data formats and message protocols used in the action supported by the XML Web service.</summary>
		/// <returns>An <see cref="T:System.Web.Services.Description.OperationBindingCollection" /> object that contains the collection of specifications for data formats and message protocols used in the action supported by the XML Web service.</returns>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001C5E2 File Offset: 0x0001A7E2
		[XmlElement("operation")]
		public OperationBindingCollection Operations
		{
			get
			{
				if (this.operations == null)
				{
					this.operations = new OperationBindingCollection(this);
				}
				return this.operations;
			}
		}

		/// <summary>Gets or sets a value representing the namespace-qualified name of the <see cref="T:System.Web.Services.Description.PortType" /> with which the Binding is associated.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" />  of the <see cref="T:System.Web.Services.Description.PortType" /> with which the Binding is associated.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001C5FE File Offset: 0x0001A7FE
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001C614 File Offset: 0x0001A814
		[XmlAttribute("type")]
		public XmlQualifiedName Type
		{
			get
			{
				if (this.type == null)
				{
					return XmlQualifiedName.Empty;
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x040003F8 RID: 1016
		private ServiceDescriptionFormatExtensionCollection extensions;

		// Token: 0x040003F9 RID: 1017
		private OperationBindingCollection operations;

		// Token: 0x040003FA RID: 1018
		private XmlQualifiedName type = XmlQualifiedName.Empty;

		// Token: 0x040003FB RID: 1019
		private ServiceDescription parent;
	}
}
