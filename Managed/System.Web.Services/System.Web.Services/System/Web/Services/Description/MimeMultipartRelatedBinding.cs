using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" />, specifying the individual MIME formats for the parts of the HTTP transmission. This class cannot be inherited.</summary>
	// Token: 0x020000D0 RID: 208
	[XmlFormatExtension("multipartRelated", "http://schemas.xmlsoap.org/wsdl/mime/", typeof(InputBinding), typeof(OutputBinding))]
	public sealed class MimeMultipartRelatedBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets the collection of extensibility elements added to the <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" /> to specify the MIME format for the parts of the MIME message.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.MimePartCollection" /> representing extensibility elements added to the <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" />.</returns>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00018AD2 File Offset: 0x00016CD2
		[XmlElement("part")]
		public MimePartCollection Parts
		{
			get
			{
				return this.parts;
			}
		}

		// Token: 0x04000388 RID: 904
		private MimePartCollection parts = new MimePartCollection();
	}
}
