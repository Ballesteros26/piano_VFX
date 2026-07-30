using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.Port" /> within an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000BD RID: 189
	[XmlFormatExtension("address", "http://schemas.xmlsoap.org/wsdl/http/", typeof(Port))]
	public sealed class HttpAddressBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets a value representing the URL of the XML Web service.</summary>
		/// <returns>A string specifying the URI for the <see cref="T:System.Web.Services.Description.Port" />. The default value is an empty string ("").</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00017355 File Offset: 0x00015555
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0001736B File Offset: 0x0001556B
		[XmlAttribute("location")]
		public string Location
		{
			get
			{
				if (this.location != null)
				{
					return this.location;
				}
				return string.Empty;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x04000373 RID: 883
		private string location;
	}
}
