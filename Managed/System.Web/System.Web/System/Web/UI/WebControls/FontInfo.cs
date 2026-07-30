using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the font properties of text. This class cannot be inherited.</summary>
	// Token: 0x02000396 RID: 918
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FontInfo
	{
		// Token: 0x06002411 RID: 9233 RVA: 0x0005D68B File Offset: 0x0005B88B
		internal FontInfo(Style owner)
		{
			this._owner = owner;
			this.bag = owner.ViewState;
		}

		/// <summary>Gets or sets a value that indicates whether the font is bold.</summary>
		/// <returns>true if the font is bold; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x0005D6A6 File Offset: 0x0005B8A6
		// (set) Token: 0x06002413 RID: 9235 RVA: 0x0005D6CD File Offset: 0x0005B8CD
		[WebSysDescription("")]
		[WebCategory("Font")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool Bold
		{
			get
			{
				return this._owner.CheckBit(2048) && this.bag.GetBool("Font_Bold", false);
			}
			set
			{
				this.bag["Font_Bold"] = value;
				this._owner.SetBit(2048);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the font is italic.</summary>
		/// <returns>true if the font is italic; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0005D6F5 File Offset: 0x0005B8F5
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x0005D71C File Offset: 0x0005B91C
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public bool Italic
		{
			get
			{
				return this._owner.CheckBit(4096) && this.bag.GetBool("Font_Italic", false);
			}
			set
			{
				this.bag["Font_Italic"] = value;
				this._owner.SetBit(4096);
			}
		}

		/// <summary>Gets or sets the primary font name.</summary>
		/// <returns>The primary font name. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified font name is null. </exception>
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x0005D744 File Offset: 0x0005B944
		// (set) Token: 0x06002417 RID: 9239 RVA: 0x0005D765 File Offset: 0x0005B965
		[WebCategory("Font")]
		[TypeConverter(typeof(FontConverter.FontNameConverter))]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Name
		{
			get
			{
				string[] names = this.Names;
				if (names.Length == 0)
				{
					return string.Empty;
				}
				return names[0];
			}
			set
			{
				if (value == string.Empty)
				{
					this.Names = null;
					return;
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Font name cannot be null");
				}
				this.Names = new string[] { value };
			}
		}

		/// <summary>Gets or sets an ordered array of font names.</summary>
		/// <returns>An ordered array of font names.</returns>
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x0005D7A0 File Offset: 0x0005B9A0
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x0005D7E8 File Offset: 0x0005B9E8
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(FontNamesConverter))]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public string[] Names
		{
			get
			{
				if (!this._owner.CheckBit(512))
				{
					return FontInfo.empty_names;
				}
				string[] array = (string[])this.bag["Font_Names"];
				if (array != null)
				{
					return array;
				}
				return FontInfo.empty_names;
			}
			set
			{
				if (value == null)
				{
					this.bag.Remove("Font_Names");
					this._owner.RemoveBit(512);
					return;
				}
				this.bag["Font_Names"] = value;
				this._owner.SetBit(512);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the font is overlined.</summary>
		/// <returns>true if the font is overlined; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x0005D83A File Offset: 0x0005BA3A
		// (set) Token: 0x0600241B RID: 9243 RVA: 0x0005D861 File Offset: 0x0005BA61
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public bool Overline
		{
			get
			{
				return this._owner.CheckBit(16384) && this.bag.GetBool("Font_Overline", false);
			}
			set
			{
				this.bag["Font_Overline"] = value;
				this._owner.SetBit(16384);
			}
		}

		/// <summary>Gets or sets the font size.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified font size is negative. </exception>
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x0005D889 File Offset: 0x0005BA89
		// (set) Token: 0x0600241D RID: 9245 RVA: 0x0005D8B8 File Offset: 0x0005BAB8
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(typeof(FontUnit), "")]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public FontUnit Size
		{
			get
			{
				if (!this._owner.CheckBit(1024))
				{
					return FontUnit.Empty;
				}
				return (FontUnit)this.bag["Font_Size"];
			}
			set
			{
				if (value.Unit.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", value.Unit.Value, "Font size cannot be negative");
				}
				this.bag["Font_Size"] = value;
				this._owner.SetBit(1024);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the font is strikethrough.</summary>
		/// <returns>true if the font is struck through; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x0005D929 File Offset: 0x0005BB29
		// (set) Token: 0x0600241F RID: 9247 RVA: 0x0005D950 File Offset: 0x0005BB50
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		[WebCategory("Font")]
		public bool Strikeout
		{
			get
			{
				return this._owner.CheckBit(32768) && this.bag.GetBool("Font_Strikeout", false);
			}
			set
			{
				this.bag["Font_Strikeout"] = value;
				this._owner.SetBit(32768);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the font is underlined.</summary>
		/// <returns>true if the font is underlined; otherwise, false. The default value is false.</returns>
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x0005D978 File Offset: 0x0005BB78
		// (set) Token: 0x06002421 RID: 9249 RVA: 0x0005D99F File Offset: 0x0005BB9F
		[WebSysDescription("")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[WebCategory("Font")]
		public bool Underline
		{
			get
			{
				return this._owner.CheckBit(8192) && this.bag.GetBool("Font_Underline", false);
			}
			set
			{
				this.bag["Font_Underline"] = value;
				this._owner.SetBit(8192);
			}
		}

		/// <summary>Duplicates the font properties of the specified <see cref="T:System.Web.UI.WebControls.FontInfo" /> into the instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class that this method is called from.</summary>
		/// <param name="f">A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the font properties to duplicate. </param>
		// Token: 0x06002422 RID: 9250 RVA: 0x0005D9C8 File Offset: 0x0005BBC8
		public void CopyFrom(FontInfo f)
		{
			if (f == null || f.IsEmpty)
			{
				return;
			}
			if (f == this)
			{
				return;
			}
			if (f._owner.CheckBit(2048))
			{
				this.Bold = f.Bold;
			}
			if (f._owner.CheckBit(4096))
			{
				this.Italic = f.Italic;
			}
			this.Names = f.Names;
			if (f._owner.CheckBit(16384))
			{
				this.Overline = f.Overline;
			}
			if (f._owner.CheckBit(1024))
			{
				this.Size = f.Size;
			}
			if (f._owner.CheckBit(32768))
			{
				this.Strikeout = f.Strikeout;
			}
			if (f._owner.CheckBit(8192))
			{
				this.Underline = f.Underline;
			}
		}

		/// <summary>Combines the font properties of the specified <see cref="T:System.Web.UI.WebControls.FontInfo" /> with the instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class that this method is called from.</summary>
		/// <param name="f">A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the font properties to combine. </param>
		// Token: 0x06002423 RID: 9251 RVA: 0x0005DAA8 File Offset: 0x0005BCA8
		public void MergeWith(FontInfo f)
		{
			if (!this._owner.CheckBit(2048) && f._owner.CheckBit(2048))
			{
				this.Bold = f.Bold;
			}
			if (!this._owner.CheckBit(4096) && f._owner.CheckBit(4096))
			{
				this.Italic = f.Italic;
			}
			if (!this._owner.CheckBit(512) && f._owner.CheckBit(512))
			{
				this.Names = f.Names;
			}
			if (!this._owner.CheckBit(16384) && f._owner.CheckBit(16384))
			{
				this.Overline = f.Overline;
			}
			if (!this._owner.CheckBit(1024) && f._owner.CheckBit(1024))
			{
				this.Size = f.Size;
			}
			if (!this._owner.CheckBit(32768) && f._owner.CheckBit(32768))
			{
				this.Strikeout = f.Strikeout;
			}
			if (!this._owner.CheckBit(8192) && f._owner.CheckBit(8192))
			{
				this.Underline = f.Underline;
			}
		}

		/// <summary>Determines whether the <see cref="P:System.Web.UI.WebControls.FontInfo.Names" /> property should be persisted.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.WebControls.FontInfo.Names" /> property has changed from its default value; otherwise, false.</returns>
		// Token: 0x06002424 RID: 9252 RVA: 0x0005DC05 File Offset: 0x0005BE05
		public bool ShouldSerializeNames()
		{
			return this.Names.Length != 0;
		}

		/// <summary>Returns a string that contains the font name and size for an instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class.</summary>
		/// <returns>A string that contains the font name and size for an instance of the <see cref="T:System.Web.UI.WebControls.FontInfo" /> class.</returns>
		// Token: 0x06002425 RID: 9253 RVA: 0x0005DC14 File Offset: 0x0005BE14
		public override string ToString()
		{
			if (this.Names.Length == 0)
			{
				return this.Size.ToString();
			}
			return this.Name + ", " + this.Size.ToString();
		}

		/// <summary>Resets all <see cref="T:System.Web.UI.WebControls.FontInfo" /> properties to the unset state and clears the view state.</summary>
		// Token: 0x06002426 RID: 9254 RVA: 0x0005DC63 File Offset: 0x0005BE63
		public void ClearDefaults()
		{
			this.Reset();
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x0005DC6C File Offset: 0x0005BE6C
		internal void Reset()
		{
			this.bag.Remove("Font_Bold");
			this.bag.Remove("Font_Italic");
			this.bag.Remove("Font_Names");
			this.bag.Remove("Font_Overline");
			this.bag.Remove("Font_Size");
			this.bag.Remove("Font_Strikeout");
			this.bag.Remove("Font_Underline");
			this._owner.RemoveBit(65024);
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0005DCFC File Offset: 0x0005BEFC
		internal void FillStyleAttributes(CssStyleCollection attributes, bool alwaysRenderTextDecoration)
		{
			if (this.IsEmpty)
			{
				if (alwaysRenderTextDecoration)
				{
					attributes.Add(HtmlTextWriterStyle.TextDecoration, "none");
				}
				return;
			}
			string text = string.Join(",", this.Names);
			if (text.Length > 0)
			{
				attributes.Add(HtmlTextWriterStyle.FontFamily, text);
			}
			if (this._owner.CheckBit(2048))
			{
				attributes.Add(HtmlTextWriterStyle.FontWeight, this.Bold ? "bold" : "normal");
			}
			if (this._owner.CheckBit(4096))
			{
				attributes.Add(HtmlTextWriterStyle.FontStyle, this.Italic ? "italic" : "normal");
			}
			if (!this.Size.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.FontSize, this.Size.ToString());
			}
			text = string.Empty;
			bool flag = false;
			if (this._owner.CheckBit(16384))
			{
				if (this.Overline)
				{
					text += "overline ";
				}
				flag = true;
			}
			if (this._owner.CheckBit(32768))
			{
				if (this.Strikeout)
				{
					text += "line-through ";
				}
				flag = true;
			}
			if (this._owner.CheckBit(8192))
			{
				if (this.Underline)
				{
					text += "underline ";
				}
				flag = true;
			}
			text = ((text.Length > 0) ? text.Trim() : ((alwaysRenderTextDecoration || flag) ? "none" : string.Empty));
			if (text.Length > 0)
			{
				attributes.Add(HtmlTextWriterStyle.TextDecoration, text);
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x0005DE80 File Offset: 0x0005C080
		private bool IsEmpty
		{
			get
			{
				return !this._owner.CheckBit(65024);
			}
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal FontInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001998 RID: 6552
		private static string[] empty_names = new string[0];

		// Token: 0x04001999 RID: 6553
		private StateBag bag;

		// Token: 0x0400199A RID: 6554
		private Style _owner;
	}
}
