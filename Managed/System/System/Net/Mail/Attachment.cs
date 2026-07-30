using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an attachment to an e-mail.</summary>
	// Token: 0x02000577 RID: 1399
	public class Attachment : AttachmentBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string. </summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fileName" /> is empty.</exception>
		// Token: 0x06002B6B RID: 11115 RVA: 0x000A9FED File Offset: 0x000A81ED
		public Attachment(string fileName)
			: base(fileName)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and MIME type information. </summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x06002B6C RID: 11116 RVA: 0x000AA008 File Offset: 0x000A8208
		public Attachment(string fileName, string mediaType)
			: base(fileName, mediaType)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified content string and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that contains a file path to use to create this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="string" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x06002B6D RID: 11117 RVA: 0x000AA024 File Offset: 0x000A8224
		public Attachment(string fileName, ContentType contentType)
			: base(fileName, contentType)
		{
			this.InitName(fileName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and content type. </summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="stream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is null.-or-<paramref name="contentStream" /> is null.</exception>
		// Token: 0x06002B6E RID: 11118 RVA: 0x000AA040 File Offset: 0x000A8240
		public Attachment(Stream contentStream, ContentType contentType)
			: base(contentStream, contentType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream and name.</summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		// Token: 0x06002B6F RID: 11119 RVA: 0x000AA055 File Offset: 0x000A8255
		public Attachment(Stream contentStream, string name)
			: base(contentStream)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.Attachment" /> class with the specified stream, name, and MIME type information. </summary>
		/// <param name="contentStream">A readable <see cref="T:System.IO.Stream" /> that contains the content for this attachment.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the value for the <see cref="P:System.Net.Mime.ContentType.Name" /> property of the <see cref="T:System.Net.Mime.ContentType" /> associated with this attachment. This value can be null.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not in the correct format.</exception>
		// Token: 0x06002B70 RID: 11120 RVA: 0x000AA070 File Offset: 0x000A8270
		public Attachment(Stream contentStream, string name, string mediaType)
			: base(contentStream, mediaType)
		{
			this.Name = name;
		}

		/// <summary>Gets the MIME content disposition for this attachment.</summary>
		/// <returns>A <see cref="T:System.Net.Mime.ContentDisposition" /> that provides the presentation information for this attachment. </returns>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x000AA08C File Offset: 0x000A828C
		public ContentDisposition ContentDisposition
		{
			get
			{
				return this.contentDisposition;
			}
		}

		/// <summary>Gets or sets the MIME content type name value in the content type associated with this attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value for the content type <paramref name="name" /> represented by the <see cref="P:System.Net.Mime.ContentType.Name" /> property.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000AA094 File Offset: 0x000A8294
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x000AA0A1 File Offset: 0x000A82A1
		public string Name
		{
			get
			{
				return base.ContentType.Name;
			}
			set
			{
				base.ContentType.Name = value;
			}
		}

		/// <summary>Specifies the encoding for the <see cref="T:System.Net.Mail.Attachment" /><see cref="P:System.Net.Mail.Attachment.Name" />.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> value that specifies the type of name encoding. The default value is determined from the name of the attachment.</returns>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000AA0AF File Offset: 0x000A82AF
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x000AA0B7 File Offset: 0x000A82B7
		public Encoding NameEncoding
		{
			get
			{
				return this.nameEncoding;
			}
			set
			{
				this.nameEncoding = value;
			}
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> object that represents the Multipurpose Internet Mail Exchange (MIME) protocol Content-Type header to be used.</param>
		// Token: 0x06002B76 RID: 11126 RVA: 0x000AA0C0 File Offset: 0x000A82C0
		public static Attachment CreateAttachmentFromString(string content, ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, contentType)
			{
				TransferEncoding = TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a mail attachment using the content from the specified string, and the specified MIME content type name.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		// Token: 0x06002B77 RID: 11127 RVA: 0x000AA0FC File Offset: 0x000A82FC
		public static Attachment CreateAttachmentFromString(string content, string name)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, new ContentType("text/plain"))
			{
				TransferEncoding = TransferEncoding.QuotedPrintable,
				Name = name
			};
		}

		/// <summary>Creates a mail attachment using the content from the specified string, the specified MIME content type name, character encoding, and MIME header information for the attachment.</summary>
		/// <returns>An object of type <see cref="T:System.Net.Mail.Attachment" />.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="name">The MIME content type name value in the content type associated with this attachment.</param>
		/// <param name="contentEncoding">An <see cref="T:System.Text.Encoding" />. This value can be null.</param>
		/// <param name="mediaType">A <see cref="T:System.String" /> that contains the MIME Content-Header information for this attachment. This value can be null.</param>
		// Token: 0x06002B78 RID: 11128 RVA: 0x000AA154 File Offset: 0x000A8354
		public static Attachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, contentEncoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return new Attachment(memoryStream, name, mediaType)
			{
				TransferEncoding = MailMessage.GuessTransferEncoding(contentEncoding),
				ContentType = 
				{
					CharSet = streamWriter.Encoding.BodyName
				}
			};
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000AA1BA File Offset: 0x000A83BA
		private void InitName(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.Name = Path.GetFileName(fileName);
		}

		// Token: 0x04002459 RID: 9305
		private ContentDisposition contentDisposition = new ContentDisposition();

		// Token: 0x0400245A RID: 9306
		private Encoding nameEncoding;
	}
}
