using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.MimePart" />, an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" />. It specifies the schema for XML messages that are not SOAP compliant. This class cannot be inherited.</summary>
	// Token: 0x020000D1 RID: 209
	[XmlFormatExtension("mimeXml", "http://schemas.xmlsoap.org/wsdl/mime/", typeof(MimePart), typeof(InputBinding), typeof(OutputBinding))]
	public sealed class MimeXmlBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see cref="T:System.Web.Services.Description.MimeXmlBinding" /> applies.</summary>
		/// <returns>The name of the corresponding <see cref="T:System.Web.Services.Description.MessagePart" />. The default value is an empty string ("").</returns>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00018AED File Offset: 0x00016CED
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00018AF5 File Offset: 0x00016CF5
		[XmlAttribute("part")]
		public string Part
		{
			get
			{
				return this.part;
			}
			set
			{
				this.part = value;
			}
		}

		// Token: 0x04000389 RID: 905
		private string part;
	}
}
