using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Specifies the format for any error messages that might be output as a result of the operation. This class cannot be inherited.</summary>
	// Token: 0x020000ED RID: 237
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class FaultBinding : MessageBinding
	{
		/// <summary>Gets the collection of extensibility elements associated with the current <see cref="T:System.Web.Services.Description.FaultBinding" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />. The default value is a collection with a <see cref="P:System.Collections.CollectionBase.Count" /> of zero.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001C48D File Offset: 0x0001A68D
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

		// Token: 0x040003EF RID: 1007
		private ServiceDescriptionFormatExtensionCollection extensions;
	}
}
