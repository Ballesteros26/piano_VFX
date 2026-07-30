using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents a named set of abstract operations and the corresponding abstract messages. This class cannot be inherited.</summary>
	// Token: 0x020000F8 RID: 248
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class PortType : NamedItem
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		internal void SetParent(ServiceDescription parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.PortType" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.PortType" />.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001C8FD File Offset: 0x0001AAFD
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

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.PortType" /> is a member.</summary>
		/// <returns>A service description of which the <see cref="T:System.Web.Services.Description.PortType" /> is a member.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001C919 File Offset: 0x0001AB19
		public ServiceDescription ServiceDescription
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Operation" /> instances defined by the <see cref="T:System.Web.Services.Description.PortType" />.</summary>
		/// <returns>A collection of operation instances defined by the <see cref="T:System.Web.Services.Description.PortType" />.</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001C921 File Offset: 0x0001AB21
		[XmlElement("operation")]
		public OperationCollection Operations
		{
			get
			{
				if (this.operations == null)
				{
					this.operations = new OperationCollection(this);
				}
				return this.operations;
			}
		}

		// Token: 0x04000406 RID: 1030
		private OperationCollection operations;

		// Token: 0x04000407 RID: 1031
		private ServiceDescription parent;

		// Token: 0x04000408 RID: 1032
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
