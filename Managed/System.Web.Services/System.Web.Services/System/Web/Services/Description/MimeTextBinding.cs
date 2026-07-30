using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" />, an <see cref="T:System.Web.Services.Description.OutputBinding" />, or a <see cref="T:System.Web.Services.Description.MimePart" />, specifying the text patterns for which to search the HTTP transmission. This class cannot be inherited.</summary>
	// Token: 0x020000D3 RID: 211
	[XmlFormatExtensionPrefix("tm", "http://microsoft.com/wsdl/mime/textMatching/")]
	[XmlFormatExtension("text", "http://microsoft.com/wsdl/mime/textMatching/", typeof(InputBinding), typeof(OutputBinding), typeof(MimePart))]
	public sealed class MimeTextBinding : ServiceDescriptionFormatExtension
	{
		/// <summary>Gets the collection of MIME text patterns for which the HTTP transmission is searched.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> representing the MIME text patterns to search for.</returns>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00018B11 File Offset: 0x00016D11
		[XmlElement("match", typeof(MimeTextMatch))]
		public MimeTextMatchCollection Matches
		{
			get
			{
				return this.matches;
			}
		}

		// Token: 0x0400038A RID: 906
		private MimeTextMatchCollection matches = new MimeTextMatchCollection();

		/// <summary>Specifies the URI for the XML namespace of the <see cref="T:System.Web.Services.Description.MimeTextBinding" /> class. This field is constant.</summary>
		// Token: 0x0400038B RID: 907
		public const string Namespace = "http://microsoft.com/wsdl/mime/textMatching/";
	}
}
