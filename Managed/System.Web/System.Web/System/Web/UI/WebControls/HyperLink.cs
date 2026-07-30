using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>A control that displays a link to another Web page.</summary>
	// Token: 0x020003B1 RID: 945
	[Designer("System.Web.UI.Design.WebControls.HyperLinkDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxData("<{0}:HyperLink runat=\"server\">HyperLink</{0}:HyperLink>")]
	[DefaultProperty("Text")]
	[DataBindingHandler("System.Web.UI.Design.HyperLinkDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ControlBuilder(typeof(HyperLinkControlBuilder))]
	[ParseChildren(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLink : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLink" /> class.</summary>
		// Token: 0x060026A5 RID: 9893 RVA: 0x00064FA1 File Offset: 0x000631A1
		public HyperLink()
			: base(HtmlTextWriterTag.A)
		{
		}

		/// <summary>Adds the attributes of a <see cref="T:System.Web.UI.WebControls.HyperLink" /> control to the output stream for rendering.</summary>
		/// <param name="writer">The output stream to render on the client. </param>
		// Token: 0x060026A6 RID: 9894 RVA: 0x00064FAC File Offset: 0x000631AC
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			base.AddDisplayStyleAttribute(writer);
			if (!base.IsEnabled)
			{
				return;
			}
			string target = this.Target;
			string navigateUrl = this.NavigateUrl;
			if (navigateUrl.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, base.ResolveClientUrl(navigateUrl));
			}
			if (target.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, target);
			}
		}

		/// <summary>Notifies the control that an element was parsed, and adds the element to the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. </summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
		// Token: 0x060026A7 RID: 9895 RVA: 0x00065008 File Offset: 0x00063208
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl == null)
			{
				string text = this.Text;
				if (text.Length != 0)
				{
					this.Text = null;
					this.Controls.Add(new LiteralControl(text));
				}
				base.AddParsedSubObject(obj);
				return;
			}
			this.Text = literalControl.Text;
		}

		/// <param name="savedState">The <see cref="T:System.Object" /> that contains the previously saved state.</param>
		// Token: 0x060026A8 RID: 9896 RVA: 0x0006506A File Offset: 0x0006326A
		[global::System.MonoTODO("Why override?")]
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control on a page.</summary>
		/// <param name="writer">The output stream to render on the client. </param>
		// Token: 0x060026A9 RID: 9897 RVA: 0x00065074 File Offset: 0x00063274
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.HasControls() || base.HasRenderMethodDelegate())
			{
				base.RenderContents(writer);
				return;
			}
			string imageUrl = this.ImageUrl;
			if (!string.IsNullOrEmpty(imageUrl))
			{
				string text = this.ToolTip;
				if (!string.IsNullOrEmpty(text))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, text);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(imageUrl));
				text = this.Text;
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, text);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				return;
			}
			writer.Write(this.Text);
		}

		/// <summary>Gets or sets the path to an image to display for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.</summary>
		/// <returns>The path to the image to display for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000650F9 File Offset: 0x000632F9
		// (set) Token: 0x060026AB RID: 9899 RVA: 0x00065110 File Offset: 0x00063310
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Bindable(true)]
		[UrlProperty]
		public virtual string ImageUrl
		{
			get
			{
				return this.ViewState.GetString("ImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL to link to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked.</summary>
		/// <returns>The URL to link to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked. The default value is an empty string ('').</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x00065123 File Offset: 0x00063323
		// (set) Token: 0x060026AD RID: 9901 RVA: 0x0006513A File Offset: 0x0006333A
		[WebCategory("Navigation")]
		[WebSysDescription("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Bindable(true)]
		public string NavigateUrl
		{
			get
			{
				return this.ViewState.GetString("NavigateUrl", string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content linked to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked.</summary>
		/// <returns>The target window or frame to load the Web page linked to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked. Values must begin with a letter in the range of a through z (case-insensitive), except for the special values listed in the following table, which begin with an underscore._blank Renders the content in a new window without frames. _parent Renders the content in the immediate frameset parent. _searchRenders the content in the search pane._self Renders the content in the frame with focus. _top Renders the content in the full window without frames. NoteCheck your browser documentation to determine if the _search value is supported.  For example, Microsoft Internet Explorer 5.0 and later support the _search target value.The default value is an empty string ("").</returns>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x00049F0D File Offset: 0x0004810D
		// (set) Token: 0x060026AF RID: 9903 RVA: 0x00046F16 File Offset: 0x00045116
		[DefaultValue("")]
		[TypeConverter(typeof(TargetConverter))]
		[WebSysDescription("")]
		[WebCategory("Navigation")]
		public string Target
		{
			get
			{
				return this.ViewState.GetString("Target", string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.</summary>
		/// <returns>The text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x0004A013 File Offset: 0x00048213
		// (set) Token: 0x060026B1 RID: 9905 RVA: 0x0006514D File Offset: 0x0006334D
		[WebCategory("Appearance")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Bindable(true)]
		[WebSysDescription("")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Gets or sets the height of the hyperlink when the hyperlink is an image.</summary>
		/// <returns>The height of the hyperlink image.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x00065174 File Offset: 0x00063374
		// (set) Token: 0x060026B4 RID: 9908 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual Unit ImageHeight
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Unit);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the width of the hyperlink when the hyperlink is an image.</summary>
		/// <returns>The width of the hyperlink image.</returns>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x00065190 File Offset: 0x00063390
		// (set) Token: 0x060026B6 RID: 9910 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual Unit ImageWidth
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Unit);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
