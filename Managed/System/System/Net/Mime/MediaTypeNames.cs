using System;

namespace System.Net.Mime
{
	/// <summary>Specifies the media type information for an e-mail message attachment.</summary>
	// Token: 0x020005B1 RID: 1457
	public static class MediaTypeNames
	{
		/// <summary>Specifies the type of text data in an e-mail message attachment.</summary>
		// Token: 0x020005B2 RID: 1458
		public static class Text
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in plain text format.</summary>
			// Token: 0x04002581 RID: 9601
			public const string Plain = "text/plain";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in HTML format.</summary>
			// Token: 0x04002582 RID: 9602
			public const string Html = "text/html";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in XML format.</summary>
			// Token: 0x04002583 RID: 9603
			public const string Xml = "text/xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Text" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x04002584 RID: 9604
			public const string RichText = "text/richtext";
		}

		/// <summary>Specifies the kind of application data in an e-mail message attachment.</summary>
		// Token: 0x020005B3 RID: 1459
		public static class Application
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is a SOAP document.</summary>
			// Token: 0x04002585 RID: 9605
			public const string Soap = "application/soap+xml";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is not interpreted.</summary>
			// Token: 0x04002586 RID: 9606
			public const string Octet = "application/octet-stream";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Rich Text Format (RTF).</summary>
			// Token: 0x04002587 RID: 9607
			public const string Rtf = "application/rtf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is in Portable Document Format (PDF).</summary>
			// Token: 0x04002588 RID: 9608
			public const string Pdf = "application/pdf";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Application" /> data is compressed.</summary>
			// Token: 0x04002589 RID: 9609
			public const string Zip = "application/zip";
		}

		/// <summary>Specifies the type of image data in an e-mail message attachment.</summary>
		// Token: 0x020005B4 RID: 1460
		public static class Image
		{
			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Graphics Interchange Format (GIF).</summary>
			// Token: 0x0400258A RID: 9610
			public const string Gif = "image/gif";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Tagged Image File Format (TIFF).</summary>
			// Token: 0x0400258B RID: 9611
			public const string Tiff = "image/tiff";

			/// <summary>Specifies that the <see cref="T:System.Net.Mime.MediaTypeNames.Image" /> data is in Joint Photographic Experts Group (JPEG) format.</summary>
			// Token: 0x0400258C RID: 9612
			public const string Jpeg = "image/jpeg";
		}
	}
}
