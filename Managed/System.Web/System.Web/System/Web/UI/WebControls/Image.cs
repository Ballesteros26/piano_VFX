using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays an image on a Web page.</summary>
	// Token: 0x020003B7 RID: 951
	[DefaultProperty("ImageUrl")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Image : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Image" /> class.</summary>
		// Token: 0x060026F4 RID: 9972 RVA: 0x000658C9 File Offset: 0x00063AC9
		public Image()
			: base(HtmlTextWriterTag.Img)
		{
		}

		/// <summary>Gets or sets the alternate text displayed in the <see cref="T:System.Web.UI.WebControls.Image" /> control when the image is unavailable. Browsers that support the ToolTips feature display this text as a ToolTip.</summary>
		/// <returns>The alternate text displayed in the <see cref="T:System.Web.UI.WebControls.Image" /> control when the image is unavailable.</returns>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x000658D4 File Offset: 0x00063AD4
		// (set) Token: 0x060026F6 RID: 9974 RVA: 0x00065901 File Offset: 0x00063B01
		[WebSysDescription("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[Bindable(true)]
		[DefaultValue("")]
		public virtual string AlternateText
		{
			get
			{
				string text = (string)this.ViewState["AlternateText"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("AlternateText");
					return;
				}
				this.ViewState["AlternateText"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is enabled.</summary>
		/// <returns>true if the control is enabled; otherwise false.</returns>
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x00065928 File Offset: 0x00063B28
		// (set) Token: 0x060026F8 RID: 9976 RVA: 0x00065930 File Offset: 0x00063B30
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Gets the font properties for the text associated with the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the properties for the text associated with the control.</returns>
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x00046ECD File Offset: 0x000450CD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.WebControls.Image" /> control in relation to other elements on the Web page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ImageAlign" /> values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.ImageAlign" /> values. </exception>
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x0006593C File Offset: 0x00063B3C
		// (set) Token: 0x060026FB RID: 9979 RVA: 0x00065965 File Offset: 0x00063B65
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(ImageAlign.NotSet)]
		public virtual ImageAlign ImageAlign
		{
			get
			{
				object obj = this.ViewState["ImageAlign"];
				if (obj != null)
				{
					return (ImageAlign)obj;
				}
				return ImageAlign.NotSet;
			}
			set
			{
				if (value < ImageAlign.NotSet || value > ImageAlign.TextTop)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Invalid ImageAlign value."));
				}
				this.ViewState["ImageAlign"] = value;
			}
		}

		/// <summary>Gets or sets the URL that provides the path to an image to display in the <see cref="T:System.Web.UI.WebControls.Image" /> control.</summary>
		/// <returns>The URL that provides the path to an image to display in the <see cref="T:System.Web.UI.WebControls.Image" /> control.</returns>
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x00065998 File Offset: 0x00063B98
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x000659C5 File Offset: 0x00063BC5
		[WebCategory("Appearance")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[Bindable(true)]
		[WebSysDescription("")]
		public virtual string ImageUrl
		{
			get
			{
				string text = (string)this.ViewState["ImageUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("ImageUrl");
					return;
				}
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the location to a detailed description for the image.</summary>
		/// <returns>The URL for the file that contains a detailed description for the image. The default is an empty string ("").</returns>
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x000659EC File Offset: 0x00063BEC
		// (set) Token: 0x060026FF RID: 9983 RVA: 0x00065A19 File Offset: 0x00063C19
		[WebCategory("Accessibility")]
		[WebSysDescription("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string DescriptionUrl
		{
			get
			{
				string text = (string)this.ViewState["DescriptionUrl"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					this.ViewState.Remove("DescriptionUrl");
					return;
				}
				this.ViewState["DescriptionUrl"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control generates an alternate text attribute for an empty string value.</summary>
		/// <returns>true if the control generates the alternate text attribute for an empty string value; otherwise, false. The default is false.</returns>
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x00065A40 File Offset: 0x00063C40
		// (set) Token: 0x06002701 RID: 9985 RVA: 0x00065A69 File Offset: 0x00063C69
		[WebCategory("Accessibility")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public virtual bool GenerateEmptyAlternateText
		{
			get
			{
				object obj = this.ViewState["GenerateEmptyAlternateText"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["GenerateEmptyAlternateText"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds the attributes of an <see cref="T:System.Web.UI.WebControls.Image" /> to the output stream for rendering on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client browser. </param>
		// Token: 0x06002703 RID: 9987 RVA: 0x00065A84 File Offset: 0x00063C84
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, base.ResolveClientUrl(this.ImageUrl));
			string text = this.AlternateText;
			if (text.Length > 0 || this.GenerateEmptyAlternateText)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, text);
			}
			text = this.DescriptionUrl;
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Longdesc, base.ResolveClientUrl(text));
			}
			switch (this.ImageAlign)
			{
			case ImageAlign.Left:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "left", false);
				return;
			case ImageAlign.Right:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "right", false);
				return;
			case ImageAlign.Baseline:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "baseline", false);
				return;
			case ImageAlign.Top:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "top", false);
				return;
			case ImageAlign.Middle:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "middle", false);
				return;
			case ImageAlign.Bottom:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "bottom", false);
				return;
			case ImageAlign.AbsBottom:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "absbottom", false);
				return;
			case ImageAlign.AbsMiddle:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "absmiddle", false);
				return;
			case ImageAlign.TextTop:
				writer.AddAttribute(HtmlTextWriterAttribute.Align, "texttop", false);
				return;
			default:
				return;
			}
		}

		/// <summary>Renders the image control contents to the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002704 RID: 9988 RVA: 0x00065B9C File Offset: 0x00063D9C
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
		}
	}
}
