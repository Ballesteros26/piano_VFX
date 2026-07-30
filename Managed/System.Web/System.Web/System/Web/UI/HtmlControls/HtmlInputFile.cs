using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= file&gt; element on the server.</summary>
	// Token: 0x02000264 RID: 612
	[ValidationProperty("Value")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputFile : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> class.</summary>
		// Token: 0x060018F4 RID: 6388 RVA: 0x0004345A File Offset: 0x0004165A
		public HtmlInputFile()
			: base("file")
		{
		}

		/// <summary>Gets or sets a comma-separated list of MIME encodings used to constrain the file types the user can select.</summary>
		/// <returns>The comma-separated list of MIME encodings.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x00043468 File Offset: 0x00041668
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x00043490 File Offset: 0x00041690
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[DefaultValue("")]
		public string Accept
		{
			get
			{
				string text = base.Attributes["accept"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("accept");
					return;
				}
				base.Attributes["accept"] = value;
			}
		}

		/// <summary>Gets or sets the maximum length of the file path for the file to upload from the client computer.</summary>
		/// <returns>The maximum length of the file path. The default value is -1, which indicates that the property has not been set.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x000434B8 File Offset: 0x000416B8
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x000434E1 File Offset: 0x000416E1
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public int MaxLength
		{
			get
			{
				string text = base.Attributes["maxlength"];
				if (text == null)
				{
					return -1;
				}
				return Convert.ToInt32(text);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("maxlength");
					return;
				}
				base.Attributes["maxlength"] = value.ToString();
			}
		}

		/// <summary>Gets access to the uploaded file specified by a client.</summary>
		/// <returns>A <see cref="T:System.Web.HttpPostedFile" /> that accesses the file to be uploaded.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0004350F File Offset: 0x0004170F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public HttpPostedFile PostedFile
		{
			get
			{
				return this.posted_file;
			}
		}

		/// <summary>Gets or sets the width of the text box in which the file path is entered.</summary>
		/// <returns>The width of the file-path text box. The default value is -1, which indicates that the property has not been set.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x00043518 File Offset: 0x00041718
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x00043541 File Offset: 0x00041741
		[DefaultValue("-1")]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		public int Size
		{
			get
			{
				string text = base.Attributes["size"];
				if (text == null)
				{
					return -1;
				}
				return Convert.ToInt32(text);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("size");
					return;
				}
				base.Attributes["size"] = value.ToString();
			}
		}

		/// <summary>Gets the full path of the file on the client's computer.</summary>
		/// <returns>The full path of the client's file.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to this property. </exception>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x00043570 File Offset: 0x00041770
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x00043593 File Offset: 0x00041793
		[Browsable(false)]
		public override string Value
		{
			get
			{
				HttpPostedFile postedFile = this.PostedFile;
				if (postedFile == null)
				{
					return string.Empty;
				}
				return postedFile.FileName;
			}
			set
			{
				throw new NotSupportedException("The value property on HtmlInputFile is not settable.");
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060018FE RID: 6398 RVA: 0x000435A0 File Offset: 0x000417A0
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && !base.Disabled)
			{
				page.RegisterRequiresPostBack(this);
				page.RegisterEnabledControl(this);
			}
			HtmlForm htmlForm = (HtmlForm)this.SearchParentByType(typeof(HtmlForm));
			if (htmlForm != null && htmlForm.Enctype == string.Empty)
			{
				htmlForm.Enctype = "multipart/form-data";
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0004360C File Offset: 0x0004180C
		private Control SearchParentByType(Type type)
		{
			for (Control control = this.Parent; control != null; control = control.Parent)
			{
				if (type.IsAssignableFrom(control.GetType()))
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00043640 File Offset: 0x00041840
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			Page page = this.Page;
			if (page != null)
			{
				this.posted_file = page.Request.Files[postDataKey];
			}
			return false;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0000393A File Offset: 0x00001B3A
		private void RaisePostDataChangedEventInternal()
		{
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control.</summary>
		/// <returns>This method always returns false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001902 RID: 6402 RVA: 0x0004366F File Offset: 0x0004186F
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control that the state of the control has changed.</summary>
		// Token: 0x06001903 RID: 6403 RVA: 0x00043679 File Offset: 0x00041879
		protected virtual void RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001904 RID: 6404 RVA: 0x00043681 File Offset: 0x00041881
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
		// Token: 0x06001905 RID: 6405 RVA: 0x0004368B File Offset: 0x0004188B
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x04001638 RID: 5688
		private HttpPostedFile posted_file;
	}
}
