using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Web.Mail
{
	/// <summary>Provides properties and methods for constructing an e-mail attachment. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	// Token: 0x020000F3 RID: 243
	[Obsolete("The recommended alternative is System.Net.Mail.Attachment. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class MailAttachment
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Mail.MailAttachment" /> class with the specified file name for the attachment. Sets the <see cref="T:System.Text.Encoding" /> property to <see cref="F:System.Web.Mail.MailEncoding.UUEncode" /> by default. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <param name="filename">The name of the attachment file. </param>
		// Token: 0x06000D04 RID: 3332 RVA: 0x000235E9 File Offset: 0x000217E9
		public MailAttachment(string filename)
			: this(filename, MailEncoding.Base64)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Mail.MailAttachment" /> class with the specified file name and encoding type for the attachment. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <param name="filename">The name of the attachment file. </param>
		/// <param name="encoding">The type of <see cref="T:System.Web.Mail.MailEncoding" /> for the attachment. </param>
		// Token: 0x06000D05 RID: 3333 RVA: 0x000235F4 File Offset: 0x000217F4
		public MailAttachment(string filename, MailEncoding encoding)
		{
			if (SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.Read, filename).Demand();
			}
			if (!File.Exists(filename))
			{
				throw new HttpException(string.Format(global::Locale.GetText("Cannot find file: '{0}'."), filename));
			}
			this.filename = filename;
			this.encoding = encoding;
		}

		/// <summary>Gets the name of the attachment file. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The name of the attachment file.</returns>
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00023646 File Offset: 0x00021846
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets the type of encoding for the e-mail attachment. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.Mail.MailEncoding" /> values.</returns>
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0002364E File Offset: 0x0002184E
		public MailEncoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x04001127 RID: 4391
		private string filename;

		// Token: 0x04001128 RID: 4392
		private MailEncoding encoding;
	}
}
