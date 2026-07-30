using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Defines the specifications for error messages returned by the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000F4 RID: 244
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class OperationFault : OperationMessage
	{
		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.OperationFault" />.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.OperationFault" />.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001C665 File Offset: 0x0001A865
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

		// Token: 0x040003FE RID: 1022
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
