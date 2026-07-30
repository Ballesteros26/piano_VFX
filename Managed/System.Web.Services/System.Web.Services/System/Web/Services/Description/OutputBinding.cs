using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Provides a set of specifications for data formats and protocols used by the XML Web service for output messages. This class cannot be inherited.</summary>
	// Token: 0x020000F0 RID: 240
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class OutputBinding : MessageBinding
	{
		/// <summary>Gets the collection of extensibility elements associated with the current <see cref="T:System.Web.Services.Description.OutputBinding" />.</summary>
		/// <returns>A collection of service description format extension.</returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001C4DE File Offset: 0x0001A6DE
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

		// Token: 0x040003F2 RID: 1010
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
