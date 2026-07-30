using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the style of a Web server control.</summary>
	// Token: 0x02000411 RID: 1041
	[ToolboxItem("")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Style : Component, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class using default values.</summary>
		// Token: 0x06002ED1 RID: 11985 RVA: 0x0007BBEF File Offset: 0x00079DEF
		public Style()
		{
			this.viewstate = new StateBag();
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class with the specified state bag information.</summary>
		/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which to store style information. </param>
		// Token: 0x06002ED2 RID: 11986 RVA: 0x0007BC08 File Offset: 0x00079E08
		public Style(StateBag bag)
		{
			this.viewstate = bag;
			if (this.viewstate == null)
			{
				this.viewstate = new StateBag();
			}
			this._isSharedViewState = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets the background color of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x0007BC37 File Offset: 0x00079E37
		// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x0007BC5D File Offset: 0x00079E5D
		[DefaultValue(typeof(Color), "")]
		[WebSysDescription("")]
		[TypeConverter(typeof(WebColorConverter))]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		public Color BackColor
		{
			get
			{
				if (!this.CheckBit(8))
				{
					return Color.Empty;
				}
				return (Color)this.viewstate["BackColor"];
			}
			set
			{
				this.viewstate["BackColor"] = value;
				this.SetBit(8);
			}
		}

		/// <summary>Gets or sets the border color of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x0007BC7C File Offset: 0x00079E7C
		// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x0007BCA3 File Offset: 0x00079EA3
		[DefaultValue(typeof(Color), "")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(WebColorConverter))]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public Color BorderColor
		{
			get
			{
				if (!this.CheckBit(16))
				{
					return Color.Empty;
				}
				return (Color)this.viewstate["BorderColor"];
			}
			set
			{
				this.viewstate["BorderColor"] = value;
				this.SetBit(16);
			}
		}

		/// <summary>Gets or sets the border style of the Web server control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values. The default is NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values.</exception>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x0007BCC3 File Offset: 0x00079EC3
		// (set) Token: 0x06002ED8 RID: 11992 RVA: 0x0007BCE6 File Offset: 0x00079EE6
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue(BorderStyle.NotSet)]
		[NotifyParentProperty(true)]
		public BorderStyle BorderStyle
		{
			get
			{
				if (!this.CheckBit(64))
				{
					return BorderStyle.NotSet;
				}
				return (BorderStyle)this.viewstate["BorderStyle"];
			}
			set
			{
				if (value < BorderStyle.NotSet || value > BorderStyle.Outset)
				{
					throw new ArgumentOutOfRangeException("value", "The selected value is not one of the BorderStyle enumeration values.");
				}
				this.viewstate["BorderStyle"] = value;
				this.SetBit(64);
			}
		}

		/// <summary>Gets or sets the border width of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width of a Web server control. The default value is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified border width is a negative value. </exception>
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x0007BD1F File Offset: 0x00079F1F
		// (set) Token: 0x06002EDA RID: 11994 RVA: 0x0007BD48 File Offset: 0x00079F48
		[DefaultValue(typeof(Unit), "")]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public Unit BorderWidth
		{
			get
			{
				if (!this.CheckBit(32))
				{
					return Unit.Empty;
				}
				return (Unit)this.viewstate["BorderWidth"];
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", value.Value, "BorderWidth must not be negative");
				}
				this.viewstate["BorderWidth"] = value;
				this.SetBit(32);
			}
		}

		/// <summary>Gets or sets the cascading style sheet (CSS) class rendered by the Web server control on the client.</summary>
		/// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x0007BDA4 File Offset: 0x00079FA4
		// (set) Token: 0x06002EDC RID: 11996 RVA: 0x0007BDE0 File Offset: 0x00079FE0
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[CssClassProperty]
		public string CssClass
		{
			get
			{
				if (!this.CheckBit(2))
				{
					return string.Empty;
				}
				string text = this.viewstate["CssClass"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.viewstate["CssClass"] = value;
				this.SetBit(2);
			}
		}

		/// <summary>Gets the font properties associated with the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that represents the font properties of the Web server control.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x0007BDFA File Offset: 0x00079FFA
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FontInfo Font
		{
			get
			{
				if (this.fontinfo == null)
				{
					this.fontinfo = new FontInfo(this);
				}
				return this.fontinfo;
			}
		}

		/// <summary>Gets or sets the foreground color (typically the color of the text) of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x0007BE16 File Offset: 0x0007A016
		// (set) Token: 0x06002EDF RID: 11999 RVA: 0x0007BE3C File Offset: 0x0007A03C
		[WebSysDescription("")]
		[TypeConverter(typeof(WebColorConverter))]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "")]
		[WebCategory("Appearance")]
		public Color ForeColor
		{
			get
			{
				if (!this.CheckBit(4))
				{
					return Color.Empty;
				}
				return (Color)this.viewstate["ForeColor"];
			}
			set
			{
				this.viewstate["ForeColor"] = value;
				this.SetBit(4);
			}
		}

		/// <summary>Gets or sets the height of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the height of the Web server control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Unit.Value" /> property of the <see cref="T:System.Web.UI.WebControls.Unit" /> is negative. </exception>
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x0007BE5B File Offset: 0x0007A05B
		// (set) Token: 0x06002EE1 RID: 12001 RVA: 0x0007BE88 File Offset: 0x0007A088
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Appearance")]
		public Unit Height
		{
			get
			{
				if (!this.CheckBit(128))
				{
					return Unit.Empty;
				}
				return (Unit)this.viewstate["Height"];
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", value.Value, "Height must not be negative");
				}
				this.viewstate["Height"] = value;
				this.SetBit(128);
			}
		}

		/// <summary>Gets or sets the width of the Web server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the Web server control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Unit.Value" /> property of the <see cref="T:System.Web.UI.WebControls.Unit" /> is negative. </exception>
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x0007BEE4 File Offset: 0x0007A0E4
		// (set) Token: 0x06002EE3 RID: 12003 RVA: 0x0007BF10 File Offset: 0x0007A110
		[WebSysDescription("")]
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		public Unit Width
		{
			get
			{
				if (!this.CheckBit(256))
				{
					return Unit.Empty;
				}
				return (Unit)this.viewstate["Width"];
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", value.Value, "Width must not be negative");
				}
				this.viewstate["Width"] = value;
				this.SetBit(256);
			}
		}

		/// <summary>A protected property. Gets a value indicating whether any style elements have been defined in the state bag.</summary>
		/// <returns>true if the state bag has no style elements defined; otherwise, false.</returns>
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x0007BF6C File Offset: 0x0007A16C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool IsEmpty
		{
			get
			{
				return this.styles == 0 && this.RegisteredCssClass.Length == 0;
			}
		}

		/// <summary>Returns a value indicating whether any style elements have been defined in the state bag.</summary>
		/// <returns>true if there are style elements defined in the state bag; otherwise, false.</returns>
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x0007BF86 File Offset: 0x0007A186
		protected bool IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		/// <summary>Gets the state bag that holds the style elements.</summary>
		/// <returns>A state bag that holds the style elements defined in it.</returns>
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x0007BF8E File Offset: 0x0007A18E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected internal StateBag ViewState
		{
			get
			{
				return this.viewstate;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x0007BF96 File Offset: 0x0007A196
		// (set) Token: 0x06002EE8 RID: 12008 RVA: 0x0007BFC1 File Offset: 0x0007A1C1
		internal bool AlwaysRenderTextDecoration
		{
			get
			{
				return this.viewstate["AlwaysRenderTextDecoration"] != null && (bool)this.viewstate["AlwaysRenderTextDecoration"];
			}
			set
			{
				this.viewstate["AlwaysRenderTextDecoration"] = value;
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />. This method is primarily used by control developers.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06002EE9 RID: 12009 RVA: 0x0007BFD9 File Offset: 0x0007A1D9
		public void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer, null);
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> and Web server control. This method is primarily used by control developers.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.WebControl" /> or <see cref="T:System.Web.UI.WebControls.WebControl" /> derived object that represents the Web server control associated with the <see cref="T:System.Web.UI.WebControls.Style" />. </param>
		// Token: 0x06002EEA RID: 12010 RVA: 0x0007BFE4 File Offset: 0x0007A1E4
		public virtual void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			if (this.RegisteredCssClass.Length <= 0)
			{
				string cssClass = this.CssClass;
				if (cssClass != null && cssClass.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
				}
				CssStyleCollection cssStyleCollection = new CssStyleCollection();
				this.FillStyleAttributes(cssStyleCollection, owner);
				foreach (object obj in cssStyleCollection.Keys)
				{
					string text = (string)obj;
					writer.AddStyleAttribute(text, cssStyleCollection[text]);
				}
				return;
			}
			string cssClass2 = this.CssClass;
			if (!string.IsNullOrEmpty(cssClass2))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass2 + " " + this.RegisteredCssClass);
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.RegisteredCssClass);
		}

		/// <summary>Adds the specified object's style properties to a <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
		/// <param name="attributes">The <see cref="T:System.Web.UI.CssStyleCollection" /> object to which to add the style properties. </param>
		/// <param name="urlResolver">A <see cref="T:System.Web.UI.IUrlResolutionService" /> -implemented object that contains the context information for the current location (URL). </param>
		// Token: 0x06002EEB RID: 12011 RVA: 0x0007C0BC File Offset: 0x0007A2BC
		protected virtual void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			if (this.CheckBit(8))
			{
				Color color = (Color)this.viewstate["BackColor"];
				if (!color.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(color));
				}
			}
			if (this.CheckBit(16))
			{
				Color color = (Color)this.viewstate["BorderColor"];
				if (!color.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(color));
				}
			}
			bool flag = false;
			if (this.CheckBit(32))
			{
				Unit unit = (Unit)this.viewstate["BorderWidth"];
				if (!unit.IsEmpty)
				{
					if (unit.Value > 0.0)
					{
						flag = true;
					}
					attributes.Add(HtmlTextWriterStyle.BorderWidth, unit.ToString());
				}
			}
			if (this.CheckBit(64))
			{
				BorderStyle borderStyle = (BorderStyle)this.viewstate["BorderStyle"];
				if (borderStyle != BorderStyle.NotSet)
				{
					attributes.Add(HtmlTextWriterStyle.BorderStyle, borderStyle.ToString());
				}
				else if (flag)
				{
					attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
				}
			}
			else if (flag)
			{
				attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
			}
			if (this.CheckBit(4))
			{
				Color color = (Color)this.viewstate["ForeColor"];
				if (!color.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(color));
				}
			}
			if (this.CheckBit(128))
			{
				Unit unit = (Unit)this.viewstate["Height"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Height, unit.ToString());
				}
			}
			if (this.CheckBit(256))
			{
				Unit unit = (Unit)this.viewstate["Width"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Width, unit.ToString());
				}
			}
			this.Font.FillStyleAttributes(attributes, this.AlwaysRenderTextDecoration);
		}

		/// <summary>Duplicates the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> into the instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class that this method is called from.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to copy. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.UI.WebControls.Style.RegisteredCssClass" /> has been set.</exception>
		// Token: 0x06002EEC RID: 12012 RVA: 0x0007C2AC File Offset: 0x0007A4AC
		public virtual void CopyFrom(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (s.fontinfo != null)
			{
				this.Font.CopyFrom(s.fontinfo);
			}
			if (s.CheckBit(8) && s.BackColor != Color.Empty)
			{
				this.BackColor = s.BackColor;
			}
			if (s.CheckBit(16) && s.BorderColor != Color.Empty)
			{
				this.BorderColor = s.BorderColor;
			}
			if (s.CheckBit(64) && s.BorderStyle != BorderStyle.NotSet)
			{
				this.BorderStyle = s.BorderStyle;
			}
			if (s.CheckBit(32) && !s.BorderWidth.IsEmpty)
			{
				this.BorderWidth = s.BorderWidth;
			}
			if (s.CheckBit(2) && s.CssClass != string.Empty)
			{
				this.CssClass = s.CssClass;
			}
			if (s.CheckBit(4) && s.ForeColor != Color.Empty)
			{
				this.ForeColor = s.ForeColor;
			}
			if (s.CheckBit(128) && !s.Height.IsEmpty)
			{
				this.Height = s.Height;
			}
			if (s.CheckBit(256) && !s.Width.IsEmpty)
			{
				this.Width = s.Width;
			}
		}

		/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> with the instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class that this method is called from.</summary>
		/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to combine. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.UI.WebControls.Style.RegisteredCssClass" /> has been set.</exception>
		// Token: 0x06002EED RID: 12013 RVA: 0x0007C414 File Offset: 0x0007A614
		public virtual void MergeWith(Style s)
		{
			if (s == null || s.IsEmpty)
			{
				return;
			}
			if (s.fontinfo != null)
			{
				this.Font.MergeWith(s.fontinfo);
			}
			if (!this.CheckBit(8) && s.CheckBit(8) && s.BackColor != Color.Empty)
			{
				this.BackColor = s.BackColor;
			}
			if (!this.CheckBit(16) && s.CheckBit(16) && s.BorderColor != Color.Empty)
			{
				this.BorderColor = s.BorderColor;
			}
			if (!this.CheckBit(64) && s.CheckBit(64) && s.BorderStyle != BorderStyle.NotSet)
			{
				this.BorderStyle = s.BorderStyle;
			}
			if (!this.CheckBit(32) && s.CheckBit(32) && !s.BorderWidth.IsEmpty)
			{
				this.BorderWidth = s.BorderWidth;
			}
			if (!this.CheckBit(2) && s.CheckBit(2) && s.CssClass != string.Empty)
			{
				this.CssClass = s.CssClass;
			}
			if (!this.CheckBit(4) && s.CheckBit(4) && s.ForeColor != Color.Empty)
			{
				this.ForeColor = s.ForeColor;
			}
			if (!this.CheckBit(128) && s.CheckBit(128) && !s.Height.IsEmpty)
			{
				this.Height = s.Height;
			}
			if (!this.CheckBit(256) && s.CheckBit(256) && !s.Width.IsEmpty)
			{
				this.Width = s.Width;
			}
		}

		/// <summary>Removes any defined style elements from the state bag.</summary>
		// Token: 0x06002EEE RID: 12014 RVA: 0x0007C5CC File Offset: 0x0007A7CC
		public virtual void Reset()
		{
			this.viewstate.Remove("BackColor");
			this.viewstate.Remove("BorderColor");
			this.viewstate.Remove("BorderStyle");
			this.viewstate.Remove("BorderWidth");
			this.viewstate.Remove("CssClass");
			this.viewstate.Remove("ForeColor");
			this.viewstate.Remove("Height");
			this.viewstate.Remove("Width");
			if (this.fontinfo != null)
			{
				this.fontinfo.Reset();
			}
			this.styles = 0;
			this.viewstate.Remove("_!SB");
			this.stylesTraked = 0;
		}

		/// <summary>Loads the previously saved state.</summary>
		/// <param name="state">The previously saved state. </param>
		// Token: 0x06002EEF RID: 12015 RVA: 0x0007C68A File Offset: 0x0007A88A
		protected internal void LoadViewState(object state)
		{
			this.viewstate.LoadViewState(state);
			this.LoadBitState();
		}

		/// <summary>A protected method. Saves any state that has been modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
		/// <returns>An object that represents the saved state. The default is null.</returns>
		// Token: 0x06002EF0 RID: 12016 RVA: 0x0007C69E File Offset: 0x0007A89E
		protected internal virtual object SaveViewState()
		{
			this.SaveBitState();
			if (this._isSharedViewState)
			{
				return null;
			}
			return this.viewstate.SaveViewState();
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x0007C6BB File Offset: 0x0007A8BB
		internal void SaveBitState()
		{
			if (this.stylesTraked != 0)
			{
				this.viewstate["_!SB"] = this.stylesTraked;
			}
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x0007C6E0 File Offset: 0x0007A8E0
		internal void LoadBitState()
		{
			if (this.viewstate["_!SB"] == null)
			{
				return;
			}
			int num = (int)this.viewstate["_!SB"];
			this.styles |= num;
			this.stylesTraked |= num;
		}

		/// <summary>A protected internal method. Sets an internal bitmask field that indicates the style properties that are stored in the state bag.</summary>
		/// <param name="bit">A bitmask value.</param>
		// Token: 0x06002EF3 RID: 12019 RVA: 0x0007C732 File Offset: 0x0007A932
		protected internal virtual void SetBit(int bit)
		{
			this.styles |= bit;
			if (this.tracking)
			{
				this.stylesTraked |= bit;
			}
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x0007C758 File Offset: 0x0007A958
		internal void RemoveBit(int bit)
		{
			this.styles &= ~bit;
			if (this.tracking)
			{
				this.stylesTraked &= ~bit;
				if (this.stylesTraked == 0)
				{
					this.viewstate.Remove("_!SB");
				}
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0007C798 File Offset: 0x0007A998
		internal bool CheckBit(int bit)
		{
			return (this.styles & bit) != 0;
		}

		/// <summary>A protected method. Marks the beginning for tracking state changes on the control. Any changes made after tracking has begun will be tracked and saved as part of the control view state.</summary>
		// Token: 0x06002EF6 RID: 12022 RVA: 0x0007C7A5 File Offset: 0x0007A9A5
		protected internal virtual void TrackViewState()
		{
			this.tracking = true;
			this.viewstate.TrackViewState();
		}

		/// <summary>Loads the previously saved state.</summary>
		/// <param name="state">The previously saved state.</param>
		// Token: 0x06002EF7 RID: 12023 RVA: 0x0007C7B9 File Offset: 0x0007A9B9
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		/// <summary>Returns the object containing state changes.</summary>
		/// <returns>An object that represents the saved state. The default is null.</returns>
		// Token: 0x06002EF8 RID: 12024 RVA: 0x0007C7C2 File Offset: 0x0007A9C2
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Starts tracking state changes.</summary>
		// Token: 0x06002EF9 RID: 12025 RVA: 0x0007C7CA File Offset: 0x0007A9CA
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether a server control is tracking its view state changes.</summary>
		/// <returns>true if there are style elements defined in the view state bag; otherwise, false.</returns>
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x0007C7D2 File Offset: 0x0007A9D2
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x0007C7DA File Offset: 0x0007A9DA
		internal void SetRegisteredCssClass(string name)
		{
			this.registered_class = name;
		}

		/// <summary>Retrieves the <see cref="T:System.Web.UI.CssStyleCollection" /> object for the specified <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> object.</returns>
		/// <param name="urlResolver">A <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object that contains the context information for the current location (URL). </param>
		// Token: 0x06002EFC RID: 12028 RVA: 0x0007C7E4 File Offset: 0x0007A9E4
		public CssStyleCollection GetStyleAttributes(IUrlResolutionService urlResolver)
		{
			CssStyleCollection cssStyleCollection = new CssStyleCollection();
			this.FillStyleAttributes(cssStyleCollection, urlResolver);
			return cssStyleCollection;
		}

		/// <summary>Gets the cascading style sheet (CSS) class that is registered with the control.</summary>
		/// <returns>The CSS class name with which the current instance was registered on the page.</returns>
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x0007C800 File Offset: 0x0007AA00
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string RegisteredCssClass
		{
			get
			{
				if (this.registered_class == null)
				{
					this.registered_class = string.Empty;
				}
				return this.registered_class;
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0007C81B File Offset: 0x0007AA1B
		internal void CopyTextStylesFrom(Style source)
		{
			if (source.CheckBit(4))
			{
				this.ForeColor = source.ForeColor;
			}
			if (source.CheckBit(65024))
			{
				this.Font.CopyFrom(source.Font);
			}
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x0007C850 File Offset: 0x0007AA50
		internal void RemoveTextStyles()
		{
			this.ForeColor = Color.Empty;
			this.fontinfo = null;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x0007C864 File Offset: 0x0007AA64
		internal void AddCssClass(string cssClass)
		{
			if (string.IsNullOrEmpty(cssClass))
			{
				return;
			}
			string text = this.CssClass;
			if (text.Length > 0)
			{
				text += " ";
			}
			text += cssClass;
			this.CssClass = text;
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0007C8A8 File Offset: 0x0007AAA8
		internal void PrependCssClass(string cssClass)
		{
			if (string.IsNullOrEmpty(cssClass))
			{
				return;
			}
			string cssClass2 = this.CssClass;
			if (cssClass2.Length > 0)
			{
				cssClass += " ";
			}
			this.CssClass = cssClass + cssClass2;
		}

		/// <summary>Marks the <see cref="T:System.Web.UI.WebControls.Style" /> so that its state will be recorded in view state.</summary>
		// Token: 0x06002F02 RID: 12034 RVA: 0x0007C8E8 File Offset: 0x0007AAE8
		public void SetDirty()
		{
			if (this.viewstate != null)
			{
				this.viewstate.SetDirty(true);
			}
			this.stylesTraked = this.styles;
		}

		// Token: 0x04001BC8 RID: 7112
		internal const string BitStateKey = "_!SB";

		// Token: 0x04001BC9 RID: 7113
		private int styles;

		// Token: 0x04001BCA RID: 7114
		private int stylesTraked;

		// Token: 0x04001BCB RID: 7115
		internal StateBag viewstate;

		// Token: 0x04001BCC RID: 7116
		private FontInfo fontinfo;

		// Token: 0x04001BCD RID: 7117
		private bool tracking;

		// Token: 0x04001BCE RID: 7118
		private bool _isSharedViewState;

		// Token: 0x04001BCF RID: 7119
		private string registered_class;

		// Token: 0x02000412 RID: 1042
		[Flags]
		internal enum Styles
		{
			// Token: 0x04001BD1 RID: 7121
			BackColor = 8,
			// Token: 0x04001BD2 RID: 7122
			BorderColor = 16,
			// Token: 0x04001BD3 RID: 7123
			BorderStyle = 64,
			// Token: 0x04001BD4 RID: 7124
			BorderWidth = 32,
			// Token: 0x04001BD5 RID: 7125
			CssClass = 2,
			// Token: 0x04001BD6 RID: 7126
			Font = 1,
			// Token: 0x04001BD7 RID: 7127
			ForeColor = 4,
			// Token: 0x04001BD8 RID: 7128
			Height = 128,
			// Token: 0x04001BD9 RID: 7129
			Width = 256,
			// Token: 0x04001BDA RID: 7130
			FontAll = 65024,
			// Token: 0x04001BDB RID: 7131
			FontBold = 2048,
			// Token: 0x04001BDC RID: 7132
			FontItalic = 4096,
			// Token: 0x04001BDD RID: 7133
			FontNames = 512,
			// Token: 0x04001BDE RID: 7134
			FontOverline = 16384,
			// Token: 0x04001BDF RID: 7135
			FontSize = 1024,
			// Token: 0x04001BE0 RID: 7136
			FontStrikeout = 32768,
			// Token: 0x04001BE1 RID: 7137
			FontUnderline = 8192
		}
	}
}
