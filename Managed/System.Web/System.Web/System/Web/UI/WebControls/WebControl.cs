using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class that defines the methods, properties and events common to all controls in the <see cref="N:System.Web.UI.WebControls" /> namespace.</summary>
	// Token: 0x0200043E RID: 1086
	[Themeable(true)]
	[PersistChildren(false, false)]
	[ParseChildren(true)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebControl : Control, IAttributeAccessor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class using the specified HTML tag.</summary>
		/// <param name="tag">One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> values. </param>
		// Token: 0x06003208 RID: 12808 RVA: 0x00085B27 File Offset: 0x00083D27
		public WebControl(HtmlTextWriterTag tag)
		{
			this.tag = tag;
			this.enabled = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class that represents a Span HTML tag.</summary>
		// Token: 0x06003209 RID: 12809 RVA: 0x00085B3D File Offset: 0x00083D3D
		protected WebControl()
			: this(HtmlTextWriterTag.Span)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class using the specified HTML tag.</summary>
		/// <param name="tag">An HTML tag. </param>
		// Token: 0x0600320A RID: 12810 RVA: 0x00085B47 File Offset: 0x00083D47
		protected WebControl(string tag)
		{
			this.tag = HtmlTextWriterTag.Unknown;
			this.tag_name = tag;
			this.enabled = true;
		}

		/// <summary>Gets or sets the access key that allows you to quickly navigate to the Web server control.</summary>
		/// <returns>The access key for quick navigation to the Web server control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified access key is neither null, <see cref="F:System.String.Empty" /> nor a single character string. </exception>
		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x0600320B RID: 12811 RVA: 0x00085B64 File Offset: 0x00083D64
		// (set) Token: 0x0600320C RID: 12812 RVA: 0x00085B7B File Offset: 0x00083D7B
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public virtual string AccessKey
		{
			get
			{
				return this.ViewState.GetString("AccessKey", string.Empty);
			}
			set
			{
				if (value == null || value.Length < 2)
				{
					this.ViewState["AccessKey"] = value;
					return;
				}
				throw new ArgumentException("AccessKey can only be null, empty or a single character", "value");
			}
		}

		/// <summary>Gets the collection of arbitrary attributes (for rendering only) that do not correspond to properties on the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> of name and value pairs.</returns>
		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x00085BAA File Offset: 0x00083DAA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public AttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attribute_state = new StateBag(true);
					if (base.IsTrackingViewState)
					{
						this.attribute_state.TrackViewState();
					}
					this.attributes = new AttributeCollection(this.attribute_state);
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the background color of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x00085BEA File Offset: 0x00083DEA
		// (set) Token: 0x0600320F RID: 12815 RVA: 0x00085C05 File Offset: 0x00083E05
		[DefaultValue(typeof(Color), "")]
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[TypeConverter(typeof(WebColorConverter))]
		public virtual Color BackColor
		{
			get
			{
				if (this.style == null)
				{
					return Color.Empty;
				}
				return this.style.BackColor;
			}
			set
			{
				this.ControlStyle.BackColor = value;
			}
		}

		/// <summary>Gets or sets the border color of the Web control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x00085C13 File Offset: 0x00083E13
		// (set) Token: 0x06003211 RID: 12817 RVA: 0x00085C2E File Offset: 0x00083E2E
		[WebSysDescription("")]
		[TypeConverter(typeof(WebColorConverter))]
		[DefaultValue(typeof(Color), "")]
		[WebCategory("Appearance")]
		public virtual Color BorderColor
		{
			get
			{
				if (this.style == null)
				{
					return Color.Empty;
				}
				return this.style.BorderColor;
			}
			set
			{
				this.ControlStyle.BorderColor = value;
			}
		}

		/// <summary>Gets or sets the border style of the Web server control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values. The default is NotSet.</returns>
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x00085C3C File Offset: 0x00083E3C
		// (set) Token: 0x06003213 RID: 12819 RVA: 0x00085C53 File Offset: 0x00083E53
		[DefaultValue(BorderStyle.NotSet)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				if (this.style == null)
				{
					return BorderStyle.NotSet;
				}
				return this.style.BorderStyle;
			}
			set
			{
				if (value < BorderStyle.NotSet || value > BorderStyle.Outset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ControlStyle.BorderStyle = value;
			}
		}

		/// <summary>Gets or sets the border width of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width of a Web server control. The default value is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentException">The specified border width is a negative value. </exception>
		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x00085C75 File Offset: 0x00083E75
		// (set) Token: 0x06003215 RID: 12821 RVA: 0x00085C90 File Offset: 0x00083E90
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual Unit BorderWidth
		{
			get
			{
				if (this.style == null)
				{
					return Unit.Empty;
				}
				return this.style.BorderWidth;
			}
			set
			{
				this.ControlStyle.BorderWidth = value;
			}
		}

		/// <summary>Gets the style of the Web server control. This property is used primarily by control developers.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that encapsulates the appearance properties of the Web server control.</returns>
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x00085C9E File Offset: 0x00083E9E
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Style ControlStyle
		{
			get
			{
				if (this.style == null)
				{
					this.style = this.CreateControlStyle();
					if (base.IsTrackingViewState)
					{
						this.style.TrackViewState();
					}
				}
				return this.style;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Web.UI.WebControls.Style" /> object has been created for the <see cref="P:System.Web.UI.WebControls.WebControl.ControlStyle" /> property. This property is primarily used by control developers.</summary>
		/// <returns>true if a <see cref="T:System.Web.UI.WebControls.Style" /> object has been created for the <see cref="P:System.Web.UI.WebControls.WebControl.ControlStyle" /> property; otherwise, false.</returns>
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x00085CCD File Offset: 0x00083ECD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ControlStyleCreated
		{
			get
			{
				return this.style != null;
			}
		}

		/// <summary>Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.</summary>
		/// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x00085CD8 File Offset: 0x00083ED8
		// (set) Token: 0x06003219 RID: 12825 RVA: 0x00085CF3 File Offset: 0x00083EF3
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[CssClassProperty]
		public virtual string CssClass
		{
			get
			{
				if (this.style == null)
				{
					return string.Empty;
				}
				return this.style.CssClass;
			}
			set
			{
				this.ControlStyle.CssClass = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Web server control is enabled.</summary>
		/// <returns>true if control is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x00085D01 File Offset: 0x00083F01
		// (set) Token: 0x0600321B RID: 12827 RVA: 0x00085D09 File Offset: 0x00083F09
		[Themeable(false)]
		[DefaultValue(true)]
		[Bindable(true)]
		public virtual bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled != value)
				{
					if (base.IsTrackingViewState)
					{
						this.track_enabled_state = true;
					}
					this.enabled = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is true.</returns>
		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x00070DE4 File Offset: 0x0006EFE4
		// (set) Token: 0x0600321D RID: 12829 RVA: 0x00070DEC File Offset: 0x0006EFEC
		[Browsable(true)]
		public new virtual bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		/// <summary>Gets the font properties associated with the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that represents the font properties of the Web server control.</returns>
		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x00085D2A File Offset: 0x00083F2A
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		public virtual FontInfo Font
		{
			get
			{
				return this.ControlStyle.Font;
			}
		}

		/// <summary>Gets or sets the foreground color (typically the color of the text) of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x00085D37 File Offset: 0x00083F37
		// (set) Token: 0x06003220 RID: 12832 RVA: 0x00085D52 File Offset: 0x00083F52
		[TypeConverter(typeof(WebColorConverter))]
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual Color ForeColor
		{
			get
			{
				if (this.style == null)
				{
					return Color.Empty;
				}
				return this.style.ForeColor;
			}
			set
			{
				this.ControlStyle.ForeColor = value;
			}
		}

		/// <summary>Gets a value indicating whether the control has attributes set.</summary>
		/// <returns>true if the control has attributes set; otherwise, false.</returns>
		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x00085D60 File Offset: 0x00083F60
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasAttributes
		{
			get
			{
				return this.attributes != null && this.attributes.Count > 0;
			}
		}

		/// <summary>Gets or sets the height of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the height of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentException">The height was set to a negative value.</exception>
		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x00085D7A File Offset: 0x00083F7A
		// (set) Token: 0x06003223 RID: 12835 RVA: 0x00085D95 File Offset: 0x00083F95
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("")]
		public virtual Unit Height
		{
			get
			{
				if (this.style == null)
				{
					return Unit.Empty;
				}
				return this.style.Height;
			}
			set
			{
				this.ControlStyle.Height = value;
			}
		}

		/// <summary>Gets or sets the skin to apply to the control.</summary>
		/// <returns>The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentException">The skin specified in the <see cref="P:System.Web.UI.WebControls.WebControl.SkinID" /> property does not exist in the theme.</exception>
		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x00032ACF File Offset: 0x00030CCF
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x00032AD7 File Offset: 0x00030CD7
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		/// <summary>Gets a collection of text attributes that will be rendered as a style attribute on the outer tag of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> that contains the HTML style attributes to render on the outer tag of the Web server control.</returns>
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x00085DA3 File Offset: 0x00083FA3
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public CssStyleCollection Style
		{
			get
			{
				return this.Attributes.CssStyle;
			}
		}

		/// <summary>Gets or sets the tab index of the Web server control.</summary>
		/// <returns>The tab index of the Web server control. The default is 0, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified tab index is not between -32768 and 32767. </exception>
		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x00085DB0 File Offset: 0x00083FB0
		// (set) Token: 0x06003228 RID: 12840 RVA: 0x00085DC3 File Offset: 0x00083FC3
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(0)]
		public virtual short TabIndex
		{
			get
			{
				return this.ViewState.GetShort("TabIndex", 0);
			}
			set
			{
				this.ViewState["TabIndex"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed when the mouse pointer hovers over the Web server control.</summary>
		/// <returns>The text displayed when the mouse pointer hovers over the Web server control. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x00085DDB File Offset: 0x00083FDB
		// (set) Token: 0x0600322A RID: 12842 RVA: 0x00085DF2 File Offset: 0x00083FF2
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public virtual string ToolTip
		{
			get
			{
				return this.ViewState.GetString("ToolTip", string.Empty);
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		/// <summary>Gets or sets the width of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentException">The width of the Web server control was set to a negative value. </exception>
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x00085E05 File Offset: 0x00084005
		// (set) Token: 0x0600322C RID: 12844 RVA: 0x00085E20 File Offset: 0x00084020
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual Unit Width
		{
			get
			{
				if (this.style == null)
				{
					return Unit.Empty;
				}
				return this.style.Width;
			}
			set
			{
				this.ControlStyle.Width = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x00085E2E File Offset: 0x0008402E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return this.tag;
			}
		}

		/// <summary>Gets the name of the control tag. This property is used primarily by control developers.</summary>
		/// <returns>The name of the control tag.</returns>
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x00085E36 File Offset: 0x00084036
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual string TagName
		{
			get
			{
				if (this.tag_name == null)
				{
					this.tag_name = HtmlTextWriter.StaticGetTagName(this.TagKey);
				}
				return this.tag_name;
			}
		}

		/// <summary>Gets a value indicating whether the control is enabled.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebControl" /> object is enabled; otherwise, false.</returns>
		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x00085E58 File Offset: 0x00084058
		protected internal bool IsEnabled
		{
			get
			{
				for (WebControl webControl = this; webControl != null; webControl = webControl.Parent as WebControl)
				{
					if (!webControl.Enabled)
					{
						return false;
					}
				}
				return true;
			}
		}

		/// <summary>Gets or sets the CSS class to apply to the rendered HTML element when the control is disabled.</summary>
		/// <returns>The CSS class that should be applied to the rendered HTML element when the control is disabled. The default value is "aspNetDisabled".</returns>
		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x00085E83 File Offset: 0x00084083
		// (set) Token: 0x06003231 RID: 12849 RVA: 0x00085E8A File Offset: 0x0008408A
		public static string DisabledCssClass { get; set; } = "aspNetDisabled";

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x00008B66 File Offset: 0x00006D66
		[Browsable(false)]
		public virtual bool SupportsDisabledAttribute
		{
			get
			{
				return true;
			}
		}

		/// <summary>Copies any nonblank elements of the specified style to the Web control, overwriting any existing style elements of the control. This method is primarily used by control developers.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to be copied. </param>
		// Token: 0x06003233 RID: 12851 RVA: 0x00085E92 File Offset: 0x00084092
		public void ApplyStyle(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				this.ControlStyle.CopyFrom(s);
			}
		}

		/// <summary>Copies the properties not encapsulated by the <see cref="P:System.Web.UI.WebControls.WebControl.Style" /> object from the specified Web server control to the Web server control that this method is called from. This method is used primarily by control developers.</summary>
		/// <param name="controlSrc">A <see cref="T:System.Web.UI.WebControls.WebControl" /> that represents the source control with properties to be copied to the Web server control that this method is called from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="controlSrc" /> is null.</exception>
		// Token: 0x06003234 RID: 12852 RVA: 0x00085EAC File Offset: 0x000840AC
		public void CopyBaseAttributes(WebControl controlSrc)
		{
			if (controlSrc == null)
			{
				return;
			}
			this.Enabled = controlSrc.Enabled;
			object obj = controlSrc.ViewState["AccessKey"];
			if (obj != null)
			{
				this.ViewState["AccessKey"] = obj;
			}
			obj = controlSrc.ViewState["TabIndex"];
			if (obj != null)
			{
				this.ViewState["TabIndex"] = obj;
			}
			obj = controlSrc.ViewState["ToolTip"];
			if (obj != null)
			{
				this.ViewState["ToolTip"] = obj;
			}
			if (controlSrc.attributes != null)
			{
				AttributeCollection attributeCollection = this.Attributes;
				foreach (object obj2 in controlSrc.attributes.Keys)
				{
					string text = (string)obj2;
					attributeCollection[text] = controlSrc.attributes[text];
				}
			}
		}

		/// <summary>Copies any nonblank elements of the specified style to the Web control, but will not overwrite any existing style elements of the control. This method is used primarily by control developers.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to be copied. </param>
		// Token: 0x06003235 RID: 12853 RVA: 0x00085FA8 File Offset: 0x000841A8
		public void MergeStyle(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				this.ControlStyle.MergeWith(s);
			}
		}

		/// <summary>Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06003236 RID: 12854 RVA: 0x00085FC1 File Offset: 0x000841C1
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			if (this.TagKey == HtmlTextWriterTag.Unknown)
			{
				writer.RenderBeginTag(this.TagName);
				return;
			}
			writer.RenderBeginTag(this.TagKey);
		}

		/// <summary>Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06003237 RID: 12855 RVA: 0x00045C5D File Offset: 0x00043E5D
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x00085FEC File Offset: 0x000841EC
		internal string BuildScriptAttribute(string name, string tail)
		{
			AttributeCollection attributeCollection = this.Attributes;
			string text = attributeCollection[name];
			if (text == null || text.Length == 0)
			{
				return tail;
			}
			if (text[text.Length - 1] == ';')
			{
				text = text.TrimEnd(WebControl._script_trim_chars);
			}
			text = text + ";" + tail;
			attributeCollection.Remove(name);
			return text;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x00086048 File Offset: 0x00084248
		internal void AddDisplayStyleAttribute(HtmlTextWriter writer)
		{
			if (!this.ControlStyleCreated)
			{
				return;
			}
			if (!this.ControlStyle.BorderWidth.IsEmpty || (this.ControlStyle.BorderStyle != BorderStyle.None && this.ControlStyle.BorderStyle != BorderStyle.NotSet) || !this.ControlStyle.Height.IsEmpty || !this.ControlStyle.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
			}
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000860C5 File Offset: 0x000842C5
		private void RenderDisabled(HtmlTextWriter writer)
		{
			if (!this.IsEnabled)
			{
				if (!this.SupportsDisabledAttribute)
				{
					this.ControlStyle.PrependCssClass(WebControl.DisabledCssClass);
					return;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" />. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x0600323B RID: 12859 RVA: 0x000860F8 File Offset: 0x000842F8
		protected virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.RenderDisabled(writer);
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			if (this.AccessKey != string.Empty)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (this.ToolTip != string.Empty)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			if (this.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString());
			}
			if (this.style != null && !this.style.IsEmpty)
			{
				if (this.TagKey == HtmlTextWriterTag.Span)
				{
					this.AddDisplayStyleAttribute(writer);
				}
				this.style.AddAttributesToRender(writer, this);
			}
			if (this.attributes != null)
			{
				foreach (object obj in this.attributes.Keys)
				{
					string text = (string)obj;
					writer.AddAttribute(text, this.attributes[text]);
				}
			}
		}

		/// <summary>Creates the style object that is used internally by the <see cref="T:System.Web.UI.WebControls.WebControl" /> class to implement all style related properties. This method is used primarily by control developers.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that is used to implement all style-related properties of the control.</returns>
		// Token: 0x0600323C RID: 12860 RVA: 0x00086218 File Offset: 0x00084418
		protected virtual Style CreateControlStyle()
		{
			return new Style(this.ViewState);
		}

		/// <summary>Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState" /> method. </summary>
		/// <param name="savedState">An object that represents the control state to restore. </param>
		// Token: 0x0600323D RID: 12861 RVA: 0x00086228 File Offset: 0x00084428
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null || !(savedState is Pair))
			{
				base.LoadViewState(null);
				return;
			}
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			if (this.ViewState["_!SB"] != null)
			{
				this.ControlStyle.LoadBitState();
			}
			if (pair.Second != null)
			{
				if (this.attribute_state == null)
				{
					this.attribute_state = new StateBag();
					if (base.IsTrackingViewState)
					{
						this.attribute_state.TrackViewState();
					}
				}
				this.attribute_state.LoadViewState(pair.Second);
				this.attributes = new AttributeCollection(this.attribute_state);
			}
			this.enabled = this.ViewState.GetBool("Enabled", this.enabled);
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000862E4 File Offset: 0x000844E4
		internal virtual string InlinePropertiesSet()
		{
			List<string> list = new List<string>();
			if (this.BackColor != Color.Empty)
			{
				list.Add("BackColor");
			}
			if (this.BorderColor != Color.Empty)
			{
				list.Add("BorderColor");
			}
			if (this.BorderStyle != BorderStyle.NotSet)
			{
				list.Add("BorderStyle");
			}
			if (this.BorderWidth != Unit.Empty)
			{
				list.Add("BorderWidth");
			}
			if (this.CssClass != string.Empty)
			{
				list.Add("CssClass");
			}
			if (this.ForeColor != Color.Empty)
			{
				list.Add("ForeColor");
			}
			if (this.Height != Unit.Empty)
			{
				list.Add("Height");
			}
			if (this.Width != Unit.Empty)
			{
				list.Add("Width");
			}
			if (list.Count == 0)
			{
				return null;
			}
			return string.Join(", ", list);
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000863EC File Offset: 0x000845EC
		internal void VerifyInlinePropertiesNotSet()
		{
			IRenderOuterTable renderOuterTable = this as IRenderOuterTable;
			if (renderOuterTable == null || renderOuterTable.RenderOuterTable)
			{
				return;
			}
			string text = this.InlinePropertiesSet();
			if (!string.IsNullOrEmpty(text))
			{
				bool flag = text.IndexOf(',') > -1;
				throw new InvalidOperationException(string.Format("The style propert{0} '{1}' cannot be used while RenderOuterTable is disabled on the {2} control with ID '{3}'", new object[]
				{
					flag ? "ies" : "y",
					text,
					base.GetType().Name,
					this.ID
				}));
			}
		}

		/// <summary>Renders the control to the specified HTML writer.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content. </param>
		// Token: 0x06003240 RID: 12864 RVA: 0x0008646A File Offset: 0x0008466A
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.Adapter != null)
			{
				base.Adapter.Render(writer);
				return;
			}
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06003241 RID: 12865 RVA: 0x0006F10D File Offset: 0x0006D30D
		protected internal virtual void RenderContents(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		/// <summary>Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
		/// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, null.</returns>
		// Token: 0x06003242 RID: 12866 RVA: 0x00086498 File Offset: 0x00084698
		protected override object SaveViewState()
		{
			if (this.track_enabled_state)
			{
				this.ViewState["Enabled"] = this.enabled;
			}
			object obj = null;
			if (this.style != null)
			{
				this.style.SaveBitState();
			}
			object obj2 = base.SaveViewState();
			if (this.attribute_state != null)
			{
				obj = this.attribute_state.SaveViewState();
			}
			if (obj2 == null && obj == null)
			{
				return null;
			}
			return new Pair(obj2, obj);
		}

		/// <summary>Causes the control to track changes to its view state so they can be stored in the object's <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x06003243 RID: 12867 RVA: 0x00086507 File Offset: 0x00084707
		protected override void TrackViewState()
		{
			if (this.style != null)
			{
				this.style.TrackViewState();
			}
			if (this.attribute_state != null)
			{
				this.attribute_state.TrackViewState();
				this.attribute_state.SetDirty(true);
			}
			base.TrackViewState();
		}

		/// <summary>Gets an attribute of the Web control with the specified name.</summary>
		/// <returns>The value of the attribute.</returns>
		/// <param name="name">The name of the attribute.</param>
		// Token: 0x06003244 RID: 12868 RVA: 0x00086541 File Offset: 0x00084741
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this.attributes != null)
			{
				return this.attributes[key];
			}
			return null;
		}

		/// <summary>Sets an attribute of the Web control to the specified name and value.</summary>
		/// <param name="name">The name component of the attribute's name/value pair.</param>
		/// <param name="value">The value component of the attribute's name/value pair.</param>
		// Token: 0x06003245 RID: 12869 RVA: 0x00086559 File Offset: 0x00084759
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x04001C65 RID: 7269
		private const string DEFAULT_DISABLED_CSS_CLASS = "aspNetDisabled";

		// Token: 0x04001C66 RID: 7270
		private Style style;

		// Token: 0x04001C67 RID: 7271
		private HtmlTextWriterTag tag;

		// Token: 0x04001C68 RID: 7272
		private string tag_name;

		// Token: 0x04001C69 RID: 7273
		private AttributeCollection attributes;

		// Token: 0x04001C6A RID: 7274
		private StateBag attribute_state;

		// Token: 0x04001C6B RID: 7275
		private bool enabled;

		// Token: 0x04001C6C RID: 7276
		private bool track_enabled_state;

		// Token: 0x04001C6E RID: 7278
		private static char[] _script_trim_chars = new char[] { ';' };
	}
}
