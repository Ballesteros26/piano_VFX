using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines the relationship between a data item and the menu item it is binding to in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited. </summary>
	// Token: 0x020003D2 RID: 978
	[DefaultProperty("TextField")]
	public sealed class MenuItemBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
	{
		/// <summary>Gets or sets the data member to bind to a menu item.</summary>
		/// <returns>The data member to bind to a menu item. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x0006E315 File Offset: 0x0006C515
		// (set) Token: 0x060029E2 RID: 10722 RVA: 0x0006E32C File Offset: 0x0006C52C
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				return this.ViewState.GetString("DataMember", string.Empty);
			}
			set
			{
				this.ViewState["DataMember"] = value;
			}
		}

		/// <summary>Gets or sets the menu depth to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The menu depth to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x0006E33F File Offset: 0x0006C53F
		// (set) Token: 0x060029E4 RID: 10724 RVA: 0x0006E352 File Offset: 0x0006C552
		[DefaultValue(-1)]
		public int Depth
		{
			get
			{
				return this.ViewState.GetInt("Depth", -1);
			}
			set
			{
				this.ViewState["Depth"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied is enabled, allowing the item to display a pop-out image and any child menu items.</summary>
		/// <returns>true if the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is enabled; otherwise, false.</returns>
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x0006E36A File Offset: 0x0006C56A
		// (set) Token: 0x060029E6 RID: 10726 RVA: 0x0006E37D File Offset: 0x0006C57D
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.ViewState.GetBool("Enabled", true);
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Enabled" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied. </summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Enabled" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x0006E395 File Offset: 0x0006C595
		// (set) Token: 0x060029E8 RID: 10728 RVA: 0x0006E3AC File Offset: 0x0006C5AC
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string EnabledField
		{
			get
			{
				return this.ViewState.GetString("EnabledField", string.Empty);
			}
			set
			{
				this.ViewState["EnabledField"] = value;
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>A formatting string that specifies the display format for the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x0006E3BF File Offset: 0x0006C5BF
		// (set) Token: 0x060029EA RID: 10730 RVA: 0x0006E3D6 File Offset: 0x0006C5D6
		[Localizable(true)]
		[DefaultValue("")]
		public string FormatString
		{
			get
			{
				return this.ViewState.GetString("FormatString", string.Empty);
			}
			set
			{
				this.ViewState["FormatString"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The URL to an image that is displayed next to the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x0006E3E9 File Offset: 0x0006C5E9
		// (set) Token: 0x060029EC RID: 10732 RVA: 0x0006E400 File Offset: 0x0006C600
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ImageUrl
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

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ImageUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x0006E413 File Offset: 0x0006C613
		// (set) Token: 0x060029EE RID: 10734 RVA: 0x0006E42A File Offset: 0x0006C62A
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ImageUrlField
		{
			get
			{
				return this.ViewState.GetString("ImageUrlField", string.Empty);
			}
			set
			{
				this.ViewState["ImageUrlField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to link to when a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied is clicked.</summary>
		/// <returns>The URL to link to when a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060029EF RID: 10735 RVA: 0x0006E43D File Offset: 0x0006C63D
		// (set) Token: 0x060029F0 RID: 10736 RVA: 0x0006E454 File Offset: 0x0006C654
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.NavigateUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x0006E467 File Offset: 0x0006C667
		// (set) Token: 0x060029F2 RID: 10738 RVA: 0x0006E47E File Offset: 0x0006C67E
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string NavigateUrlField
		{
			get
			{
				return this.ViewState.GetString("NavigateUrlField", string.Empty);
			}
			set
			{
				this.ViewState["NavigateUrlField"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied can be selected, or is "clickable."</summary>
		/// <returns>true if the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is selectable; otherwise, false.</returns>
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x0006E491 File Offset: 0x0006C691
		// (set) Token: 0x060029F4 RID: 10740 RVA: 0x0006E4A4 File Offset: 0x0006C6A4
		[DefaultValue(true)]
		public bool Selectable
		{
			get
			{
				return this.ViewState.GetBool("Selectable", true);
			}
			set
			{
				this.ViewState["Selectable"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Selectable" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Selectable" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x0006E4BC File Offset: 0x0006C6BC
		// (set) Token: 0x060029F6 RID: 10742 RVA: 0x0006E4D3 File Offset: 0x0006C6D3
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string SelectableField
		{
			get
			{
				return this.ViewState.GetString("SelectableField", string.Empty);
			}
			set
			{
				this.ViewState["SelectableField"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x0006E4E6 File Offset: 0x0006C6E6
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x0006E4FD File Offset: 0x0006C6FD
		[DefaultValue("")]
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

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Target" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Target" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x0006E510 File Offset: 0x0006C710
		// (set) Token: 0x060029FA RID: 10746 RVA: 0x0006E527 File Offset: 0x0006C727
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TargetField
		{
			get
			{
				return this.ViewState.GetString("TargetField", string.Empty);
			}
			set
			{
				this.ViewState["TargetField"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The text displayed for the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x0006E53A File Offset: 0x0006C73A
		// (set) Token: 0x060029FC RID: 10748 RVA: 0x0006E551 File Offset: 0x0006C751
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("The display text of the menu item.")]
		public string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Text" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Text" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x0006E564 File Offset: 0x0006C764
		// (set) Token: 0x060029FE RID: 10750 RVA: 0x0006E57B File Offset: 0x0006C77B
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TextField
		{
			get
			{
				return this.ViewState.GetString("TextField", string.Empty);
			}
			set
			{
				this.ViewState["TextField"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The ToolTip text for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x0006E58E File Offset: 0x0006C78E
		// (set) Token: 0x06002A00 RID: 10752 RVA: 0x0006E5A5 File Offset: 0x0006C7A5
		[DefaultValue("")]
		[Localizable(true)]
		public string ToolTip
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

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ToolTip" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x0006E5B8 File Offset: 0x0006C7B8
		// (set) Token: 0x06002A02 RID: 10754 RVA: 0x0006E5CF File Offset: 0x0006C7CF
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ToolTipField
		{
			get
			{
				return this.ViewState.GetString("ToolTipField", string.Empty);
			}
			set
			{
				this.ViewState["ToolTipField"] = value;
			}
		}

		/// <summary>Gets or sets a nondisplayed value used to store any additional data about a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied, such as data used for handling postback events.</summary>
		/// <returns>Supplemental data about a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied; this data is not displayed. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x0006E5E2 File Offset: 0x0006C7E2
		// (set) Token: 0x06002A04 RID: 10756 RVA: 0x0006E5F9 File Offset: 0x0006C7F9
		[DefaultValue("")]
		[Localizable(true)]
		public string Value
		{
			get
			{
				return this.ViewState.GetString("Value", string.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Value" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Value" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x0006E60C File Offset: 0x0006C80C
		// (set) Token: 0x06002A06 RID: 10758 RVA: 0x0006E623 File Offset: 0x0006C823
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ValueField
		{
			get
			{
				return this.ViewState.GetString("ValueField", string.Empty);
			}
			set
			{
				this.ViewState["ValueField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that indicates the presence of a dynamic submenu for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The URL to an image that indicates the presence of a dynamic submenu for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied.</returns>
		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x0006E636 File Offset: 0x0006C836
		// (set) Token: 0x06002A08 RID: 10760 RVA: 0x0006E64D File Offset: 0x0006C84D
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		public string PopOutImageUrl
		{
			get
			{
				return this.ViewState.GetString("PopOutImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["PopOutImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.PopOutImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.PopOutImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x0006E660 File Offset: 0x0006C860
		// (set) Token: 0x06002A0A RID: 10762 RVA: 0x0006E677 File Offset: 0x0006C877
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string PopOutImageUrlField
		{
			get
			{
				return this.ViewState.GetString("PopOutImageUrlField", string.Empty);
			}
			set
			{
				this.ViewState["PopOutImageUrlField"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed below the text of a menu item (to separate it from other menu items) for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The URL to an image displayed below the text of a menu item for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied.</returns>
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x0006E68A File Offset: 0x0006C88A
		// (set) Token: 0x06002A0C RID: 10764 RVA: 0x0006E6A1 File Offset: 0x0006C8A1
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public string SeparatorImageUrl
		{
			get
			{
				return this.ViewState.GetString("SeparatorImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["SeparatorImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.SeparatorImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
		/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.SeparatorImageUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x0006E6B4 File Offset: 0x0006C8B4
		// (set) Token: 0x06002A0E RID: 10766 RVA: 0x0006E6CB File Offset: 0x0006C8CB
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string SeparatorImageUrlField
		{
			get
			{
				return this.ViewState.GetString("SeparatorImageUrlField", string.Empty);
			}
			set
			{
				this.ViewState["SeparatorImageUrlField"] = value;
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object.</summary>
		// Token: 0x06002A0F RID: 10767 RVA: 0x0006E6E0 File Offset: 0x0006C8E0
		public override string ToString()
		{
			string dataMember = this.DataMember;
			if (string.IsNullOrEmpty(dataMember))
			{
				return "(Empty)";
			}
			return dataMember;
		}

		/// <summary>Loads the node's previously saved view state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values.</param>
		// Token: 0x06002A10 RID: 10768 RVA: 0x0006E703 File Offset: 0x0006C903
		void IStateManager.LoadViewState(object savedState)
		{
			this.ViewState.LoadViewState(savedState);
		}

		/// <summary>Saves the view state changes to an <see cref="T:System.Object" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the view state changes.</returns>
		// Token: 0x06002A11 RID: 10769 RVA: 0x0006E711 File Offset: 0x0006C911
		object IStateManager.SaveViewState()
		{
			return this.ViewState.SaveViewState();
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to track changes to its view state.</summary>
		// Token: 0x06002A12 RID: 10770 RVA: 0x0006E71E File Offset: 0x0006C91E
		void IStateManager.TrackViewState()
		{
			this.ViewState.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x0006E72B File Offset: 0x0006C92B
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.ViewState.IsTrackingViewState;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataSourceViewSchemaAccessor.DataSourceViewSchema" />.</summary>
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x06002A15 RID: 10773 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" />.</returns>
		// Token: 0x06002A16 RID: 10774 RVA: 0x0006E738 File Offset: 0x0006C938
		object ICloneable.Clone()
		{
			MenuItemBinding menuItemBinding = new MenuItemBinding();
			foreach (object obj in this.ViewState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				menuItemBinding.ViewState[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return menuItemBinding;
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x0006E7B0 File Offset: 0x0006C9B0
		internal void SetDirty()
		{
			StateBag viewState = this.ViewState;
			foreach (object obj in viewState.Keys)
			{
				string text = (string)obj;
				viewState.SetItemDirty(text, true);
			}
		}

		// Token: 0x04001ACF RID: 6863
		private StateBag ViewState = new StateBag();
	}
}
