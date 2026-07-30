using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Provides a set of specifications for data formats and protocols used by the XML Web service for input messages. This class cannot be inherited.</summary>
	// Token: 0x020000EF RID: 239
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class InputBinding : MessageBinding
	{
		/// <summary>Gets the collection of extensibility elements associated with the current <see cref="T:System.Web.Services.Description.InputBinding" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</returns>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001C4C2 File Offset: 0x0001A6C2
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

		// Token: 0x040003F1 RID: 1009
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
