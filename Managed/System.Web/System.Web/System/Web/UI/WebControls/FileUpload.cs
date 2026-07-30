using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a text box control and a browse button that enable users to select a file to upload to the server.</summary>
	// Token: 0x02000395 RID: 917
	[Designer("DesignerBaseTypeNameSystem.ComponentModel.Design.IDesignerDesignerTypeNameSystem.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ValidationProperty("FileName")]
	[ControlValueProperty("FileBytes")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal, Unrestricted = false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal, Unrestricted = false)]
	public class FileUpload : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FileUpload" /> class.</summary>
		// Token: 0x06002403 RID: 9219 RVA: 0x00049F8B File Offset: 0x0004818B
		public FileUpload()
			: base(HtmlTextWriterTag.Input)
		{
		}

		/// <summary>Gets an array of the bytes in a file that is specified by using a <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the contents of the specified file.</returns>
		/// <exception cref="T:System.Web.HttpException">The entire file was not read.</exception>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0005D480 File Offset: 0x0005B680
		[Browsable(false)]
		[Bindable(true, BindingDirection.OneWay)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte[] FileBytes
		{
			get
			{
				if (this.cachedBytes == null)
				{
					this.cachedBytes = new byte[this.FileContent.Length];
					this.FileContent.Read(this.cachedBytes, 0, this.cachedBytes.Length);
				}
				return (byte[])this.cachedBytes.Clone();
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that points to a file to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that points to a file to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x0005D4D8 File Offset: 0x0005B6D8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream FileContent
		{
			get
			{
				if (this.PostedFile == null)
				{
					return Stream.Null;
				}
				Stream inputStream = this.PostedFile.InputStream;
				if (inputStream != null)
				{
					inputStream.Position = 0L;
				}
				return inputStream;
			}
		}

		/// <summary>Gets the name of a file on a client to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <returns>A string that specifies the name of a file on a client to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x0005D50B File Offset: 0x0005B70B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string FileName
		{
			get
			{
				if (this.PostedFile == null)
				{
					return string.Empty;
				}
				return Path.GetFileName(this.PostedFile.FileName);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control contains a file.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.FileUpload" /> contains a file; otherwise, false.</returns>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x0005D52C File Offset: 0x0005B72C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasFile
		{
			get
			{
				HttpPostedFile postedFile = this.PostedFile;
				return postedFile != null && !string.IsNullOrEmpty(postedFile.FileName);
			}
		}

		/// <summary>Gets the underlying <see cref="T:System.Web.HttpPostedFile" /> object for a file that is uploaded by using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> for a file uploaded by using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0005D554 File Offset: 0x0005B754
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpPostedFile PostedFile
		{
			get
			{
				Page page = this.Page;
				if (page == null || !page.IsPostBack)
				{
					return null;
				}
				if (this.Context == null || this.Context.Request == null)
				{
					return null;
				}
				return this.Context.Request.Files[this.UniqueID];
			}
		}

		/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.FileUpload" /> control to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002409 RID: 9225 RVA: 0x0005D5A7 File Offset: 0x0005B7A7
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "file", false);
			if (!string.IsNullOrEmpty(this.UniqueID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x0600240A RID: 9226 RVA: 0x0005D5DC File Offset: 0x0005B7DC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null)
			{
				page.Form.Enctype = "multipart/form-data";
			}
		}

		/// <summary>Sends the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to render on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control content. </param>
		// Token: 0x0600240B RID: 9227 RVA: 0x0005D60C File Offset: 0x0005B80C
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			base.Render(writer);
		}

		/// <summary>Saves the contents of an uploaded file to a specified path on the Web server.</summary>
		/// <param name="filename">A string that specifies the full path of the location of the server on which to save the uploaded file. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="filename" /> is not a full path.</exception>
		// Token: 0x0600240C RID: 9228 RVA: 0x0005D634 File Offset: 0x0005B834
		public void SaveAs(string filename)
		{
			HttpPostedFile postedFile = this.PostedFile;
			if (postedFile != null)
			{
				postedFile.SaveAs(filename);
			}
		}

		/// <summary>Gets or sets a value that specifies whether multiple files can be selected for upload.</summary>
		/// <returns>true if multiple files can be selected; otherwise, false.</returns>
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x0005D654 File Offset: 0x0005B854
		// (set) Token: 0x0600240E RID: 9230 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual bool AllowMultiple
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether any files have been uploaded.</summary>
		/// <returns>true if any files have been uploaded; otherwise, false.</returns>
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x0005D670 File Offset: 0x0005B870
		public bool HasFiles
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the collection of uploaded files.</summary>
		/// <returns>The collection of uploaded files.</returns>
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public IList<HttpPostedFile> PostedFiles
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x04001997 RID: 6551
		private byte[] cachedBytes;
	}
}
