using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a control that acts as a container for other controls.</summary>
	// Token: 0x020003E4 RID: 996
	[Designer("System.Web.UI.Design.WebControls.PanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ParseChildren(false)]
	[PersistChildren(true)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Panel : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Panel" /> class.</summary>
		// Token: 0x06002BBD RID: 11197 RVA: 0x000740C8 File Offset: 0x000722C8
		public Panel()
			: base(HtmlTextWriterTag.Div)
		{
		}

		/// <summary>Adds information about the background image, alignment, wrap, and direction to the list of attributes to render.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.Panel.DefaultButton" /> property of the <see cref="T:System.Web.UI.WebControls.Panel" /> control must be the ID of a control of type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
		// Token: 0x06002BBE RID: 11198 RVA: 0x000740D4 File Offset: 0x000722D4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			string text = this.BackImageUrl;
			if (text != "")
			{
				text = base.ResolveClientUrl(text);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, text);
			}
			if (!string.IsNullOrEmpty(this.DefaultButton) && this.Page != null)
			{
				Control control = this.FindControl(this.DefaultButton);
				if (control == null || !(control is IButtonControl))
				{
					throw new InvalidOperationException(string.Format("The DefaultButton of '{0}' must be the ID of a control of type IButtonControl.", this.ID));
				}
				this.Page.ClientScript.RegisterWebFormClientScript();
				writer.AddAttribute("onkeypress", string.Concat(new string[]
				{
					"javascript:return ",
					this.Page.WebFormScriptReference,
					".WebForm_FireDefaultButton(event, '",
					control.ClientID,
					"')"
				}));
			}
			if (this.Direction != ContentDirection.NotSet)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Dir, (this.Direction == ContentDirection.RightToLeft) ? "rtl" : "ltr", false);
			}
			switch (this.ScrollBars)
			{
			case ScrollBars.Horizontal:
				writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowX, "scroll");
				break;
			case ScrollBars.Vertical:
				writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowY, "scroll");
				break;
			case ScrollBars.Both:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
				break;
			case ScrollBars.Auto:
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
				break;
			}
			if (!this.Wrap)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
			string text2 = "";
			switch (this.HorizontalAlign)
			{
			case HorizontalAlign.Left:
				text2 = "left";
				break;
			case HorizontalAlign.Center:
				text2 = "center";
				break;
			case HorizontalAlign.Right:
				text2 = "right";
				break;
			case HorizontalAlign.Justify:
				text2 = "justify";
				break;
			}
			if (text2 != "")
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, text2);
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x0007429D File Offset: 0x0007249D
		private PanelStyle PanelStyle
		{
			get
			{
				return base.ControlStyle as PanelStyle;
			}
		}

		/// <summary>Gets or sets the URL of the background image for the panel control.</summary>
		/// <returns>The URL of the background image for the panel control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000742AA File Offset: 0x000724AA
		// (set) Token: 0x06002BC1 RID: 11201 RVA: 0x000742E3 File Offset: 0x000724E3
		[WebSysDescription("")]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.BackImageUrl;
				}
				return this.ViewState.GetString("BackImageUrl", string.Empty);
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.BackImageUrl = value;
					return;
				}
				this.ViewState["BackImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the contents within the panel.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. </exception>
		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x0007430C File Offset: 0x0007250C
		// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x00074360 File Offset: 0x00072560
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.HorizontalAlign;
				}
				if (this.ViewState["HorizontalAlign"] == null)
				{
					return HorizontalAlign.NotSet;
				}
				return (HorizontalAlign)this.ViewState["HorizontalAlign"];
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.HorizontalAlign = value;
					return;
				}
				this.ViewState["HorizontalAlign"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the content wraps within the panel.</summary>
		/// <returns>true if the content wraps within the panel; otherwise, false. The default is true.</returns>
		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x0007438D File Offset: 0x0007258D
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x000743BE File Offset: 0x000725BE
		[WebCategory("Layout")]
		[DefaultValue(true)]
		[WebSysDescription("")]
		public virtual bool Wrap
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return true;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.Wrap;
				}
				return this.ViewState.GetBool("Wrap", true);
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.Wrap = value;
					return;
				}
				this.ViewState["Wrap"] = value;
			}
		}

		/// <summary>Gets or sets the identifier for the default button that is contained in the <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
		/// <returns>A string value corresponding to the <see cref="P:System.Web.UI.Control.ID" /> for a button control contained in the <see cref="T:System.Web.UI.WebControls.Panel" />. The default is an empty string, indicating that the <see cref="T:System.Web.UI.WebControls.Panel" /> does not have a default button.</returns>
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000743EB File Offset: 0x000725EB
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x00074402 File Offset: 0x00072602
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string DefaultButton
		{
			get
			{
				return this.ViewState.GetString("DefaultButton", string.Empty);
			}
			set
			{
				this.ViewState["DefaultButton"] = value;
			}
		}

		/// <summary>Gets or sets the direction in which to display controls that include text in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> enumeration values. The default is NotSet.</returns>
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x00074418 File Offset: 0x00072618
		// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x0007446C File Offset: 0x0007266C
		[DefaultValue(ContentDirection.NotSet)]
		public virtual ContentDirection Direction
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return ContentDirection.NotSet;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.Direction;
				}
				if (this.ViewState["Direction"] == null)
				{
					return ContentDirection.NotSet;
				}
				return (ContentDirection)this.ViewState["Direction"];
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.Direction = value;
					return;
				}
				this.ViewState["Direction"] = value;
			}
		}

		/// <summary>Gets or sets the caption for the group of controls that is contained in the panel control.</summary>
		/// <returns>The caption text for the child controls contained in the panel control. The default is an empty string ("").</returns>
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x00074499 File Offset: 0x00072699
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x000744B0 File Offset: 0x000726B0
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string GroupingText
		{
			get
			{
				return this.ViewState.GetString("GroupingText", string.Empty);
			}
			set
			{
				this.ViewState["GroupingText"] = value;
			}
		}

		/// <summary>Gets or sets the visibility and position of scroll bars in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> enumeration values. The default is None.</returns>
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000744C4 File Offset: 0x000726C4
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x00074518 File Offset: 0x00072718
		[DefaultValue(ScrollBars.None)]
		public virtual ScrollBars ScrollBars
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return ScrollBars.None;
				}
				if (this.PanelStyle != null)
				{
					return this.PanelStyle.ScrollBars;
				}
				if (this.ViewState["ScrollBars"] == null)
				{
					return ScrollBars.None;
				}
				return (ScrollBars)this.ViewState["Direction"];
			}
			set
			{
				if (this.PanelStyle != null)
				{
					this.PanelStyle.ScrollBars = value;
					return;
				}
				this.ViewState["ScrollBars"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Creates a style object that is used internally by the <see cref="T:System.Web.UI.WebControls.Panel" /> control to implement all style related properties.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.PanelStyle" /> that contains the style properties of the control.</returns>
		// Token: 0x06002BCF RID: 11215 RVA: 0x00074545 File Offset: 0x00072745
		protected override Style CreateControlStyle()
		{
			return new PanelStyle(this.ViewState);
		}

		/// <summary>Renders the HTML opening tag of the <see cref="T:System.Web.UI.WebControls.Panel" /> control to the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002BD0 RID: 11216 RVA: 0x00074552 File Offset: 0x00072752
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			if (!string.IsNullOrEmpty(this.GroupingText))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
				writer.RenderBeginTag(HtmlTextWriterTag.Legend);
				writer.Write(this.GroupingText);
				writer.RenderEndTag();
			}
		}

		/// <summary>Renders the HTML closing tag of the <see cref="T:System.Web.UI.WebControls.Panel" /> control into the specified writer.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06002BD1 RID: 11217 RVA: 0x0007458A File Offset: 0x0007278A
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.GroupingText))
			{
				writer.RenderEndTag();
			}
			base.RenderEndTag(writer);
		}
	}
}
