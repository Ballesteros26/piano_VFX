using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.OperationBinding" /> within an XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000BF RID: 191
	[XmlFormatExtension("operation", "http://schemas.xmlsoap.org/wsdl/http/", typeof(OperationBinding))]
	public sealed class HttpOperationBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets the URL relative to the location specified by the <see cref="T:System.Web.Services.Description.HttpAddressBinding" />, within the Web Services Description Language (WSDL) document, of the operation supported by the <see cref="T:System.Web.Services.Description.HttpOperationBinding" />.</summary>
		/// <returns>An unencoded string representing the relative path. The default value is an empty string ("").</returns>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001738D File Offset: 0x0001558D
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x000173A3 File Offset: 0x000155A3
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

		// Token: 0x04000376 RID: 886
		private string location;
	}
}
