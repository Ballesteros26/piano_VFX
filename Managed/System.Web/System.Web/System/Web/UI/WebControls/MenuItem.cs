using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a menu item displayed in the <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003D1 RID: 977
	[ParseChildren(true, "ChildItems")]
	public sealed class MenuItem : IStateManager, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class without menu text or a value.</summary>
		// Token: 0x060029A5 RID: 10661 RVA: 0x0006D535 File Offset: 0x0006B735
		public MenuItem()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
		// Token: 0x060029A6 RID: 10662 RVA: 0x0006D54F File Offset: 0x0006B74F
		public MenuItem(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text and value. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
		/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
		// Token: 0x060029A7 RID: 10663 RVA: 0x0006D570 File Offset: 0x0006B770
		public MenuItem(string text, string value)
		{
			this.Text = text;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, and URL to an image. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
		/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
		/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item.</param>
		// Token: 0x060029A8 RID: 10664 RVA: 0x0006D598 File Offset: 0x0006B798
		public MenuItem(string text, string value, string imageUrl)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, image URL, and navigation URL. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
		/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
		/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item.</param>
		/// <param name="navigateUrl">The URL to link to when the menu item is clicked.</param>
		// Token: 0x060029A9 RID: 10665 RVA: 0x0006D5C7 File Offset: 0x0006B7C7
		public MenuItem(string text, string value, string imageUrl, string navigateUrl)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
			this.NavigateUrl = navigateUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, image URL, navigation URL, and target. </summary>
		/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. </param>
		/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events. </param>
		/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item. </param>
		/// <param name="navigateUrl">The URL to link to when the menu item is clicked. </param>
		/// <param name="target">The target window or frame in which to display the Web page content linked to a menu item when the menu item is clicked. </param>
		// Token: 0x060029AA RID: 10666 RVA: 0x0006D5FE File Offset: 0x0006B7FE
		public MenuItem(string text, string value, string imageUrl, string navigateUrl, string target)
		{
			this.Text = text;
			this.Value = value;
			this.ImageUrl = imageUrl;
			this.NavigateUrl = navigateUrl;
			this.Target = target;
		}

		/// <summary>Gets the level at which a menu item is displayed.</summary>
		/// <returns>The level at which a menu item is displayed.</returns>
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x0006D63D File Offset: 0x0006B83D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Depth
		{
			get
			{
				if (this.depth != -1)
				{
					return this.depth;
				}
				if (this.Parent == null)
				{
					this.depth = 0;
				}
				else
				{
					this.depth = this.Parent.Depth + 1;
				}
				return this.depth;
			}
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x0006D679 File Offset: 0x0006B879
		private void ResetPathData()
		{
			this.path = null;
			this.depth = -1;
			this.gotBinding = false;
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x0006D690 File Offset: 0x0006B890
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x0006D698 File Offset: 0x0006B898
		internal Menu Menu
		{
			get
			{
				return this.menu;
			}
			set
			{
				this.menu = value;
				if (this.items != null)
				{
					this.items.SetMenu(this.menu);
				}
				this.ResetPathData();
			}
		}

		/// <summary>Gets a value indicating whether the menu item was created through data binding.</summary>
		/// <returns>true if the menu item was created through data binding; otherwise, false.</returns>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x0006D6C0 File Offset: 0x0006B8C0
		// (set) Token: 0x060029B0 RID: 10672 RVA: 0x0006D6EB File Offset: 0x0006B8EB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		public bool DataBound
		{
			get
			{
				return this.ViewState["DataBound"] != null && (bool)this.ViewState["DataBound"];
			}
			private set
			{
				this.ViewState["DataBound"] = value;
			}
		}

		/// <summary>Gets the data item that is bound to the menu item.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the data item that is bound to the menu item. The default value is null, which indicates that the menu item is not bound to any data item.</returns>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x0006D703 File Offset: 0x0006B903
		[DefaultValue(null)]
		[Browsable(false)]
		public object DataItem
		{
			get
			{
				if (!this.DataBound)
				{
					throw new InvalidOperationException("MenuItem is not data bound.");
				}
				return this.dataItem;
			}
		}

		/// <summary>Gets the path to the data that is bound to the menu item.</summary>
		/// <returns>The path to the data that is bound to the node. This value comes from the hierarchical data source control to which the <see cref="T:System.Web.UI.WebControls.Menu" /> control is bound. The default value is an empty string ("").</returns>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x0006D71E File Offset: 0x0006B91E
		// (set) Token: 0x060029B3 RID: 10675 RVA: 0x0006D74D File Offset: 0x0006B94D
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DataPath
		{
			get
			{
				if (this.ViewState["DataPath"] != null)
				{
					return (string)this.ViewState["DataPath"];
				}
				return string.Empty;
			}
			private set
			{
				this.ViewState["DataPath"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object that contains the submenu items of the current menu item.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> that contains the submenu items of the current menu item. The default is null, which indicates that this menu item does not contain any submenu items.</returns>
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x0006D760 File Offset: 0x0006B960
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		[MergableProperty(false)]
		public MenuItemCollection ChildItems
		{
			get
			{
				if (this.items == null)
				{
					this.items = new MenuItemCollection(this);
					if (((IStateManager)this).IsTrackingViewState)
					{
						((IStateManager)this.items).TrackViewState();
					}
				}
				return this.items;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed next to the text in a menu item.</summary>
		/// <returns>The URL to a custom image that is displayed next to the text of a menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x0006D78F File Offset: 0x0006B98F
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x0006D7BE File Offset: 0x0006B9BE
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				if (this.ViewState["ImageUrl"] != null)
				{
					return (string)this.ViewState["ImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL to navigate to when the menu item is clicked.</summary>
		/// <returns>The URL to navigate to when the menu item is clicked. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x0006D7D1 File Offset: 0x0006B9D1
		// (set) Token: 0x060029B8 RID: 10680 RVA: 0x0006D800 File Offset: 0x0006BA00
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string NavigateUrl
		{
			get
			{
				if (this.ViewState["NavigateUrl"] != null)
				{
					return (string)this.ViewState["NavigateUrl"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that is displayed in a menu item to indicate that the menu item has a dynamic submenu.</summary>
		/// <returns>The URL to an image that is displayed in a menu item to indicate that the menu item has a dynamic submenu. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x0006D813 File Offset: 0x0006BA13
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x0006D842 File Offset: 0x0006BA42
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string PopOutImageUrl
		{
			get
			{
				if (this.ViewState["PopOutImageUrl"] != null)
				{
					return (string)this.ViewState["PopOutImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PopOutImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a menu item.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x0006D855 File Offset: 0x0006BA55
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x0006D884 File Offset: 0x0006BA84
		[DefaultValue("")]
		public string Target
		{
			get
			{
				if (this.ViewState["Target"] != null)
				{
					return (string)this.ViewState["Target"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>The text displayed for the menu item in the <see cref="T:System.Web.UI.WebControls.Menu" /> control. The default is an empty string ("").</returns>
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x0006D898 File Offset: 0x0006BA98
		// (set) Token: 0x060029BE RID: 10686 RVA: 0x0006D8D9 File Offset: 0x0006BAD9
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj == null)
				{
					obj = this.ViewState["Value"];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Gets or sets the ToolTip text for the menu item.</summary>
		/// <returns>The ToolTip text for the menu item. The default is an empty string ("").</returns>
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x060029BF RID: 10687 RVA: 0x0006D8EC File Offset: 0x0006BAEC
		// (set) Token: 0x060029C0 RID: 10688 RVA: 0x0006D91B File Offset: 0x0006BB1B
		[Localizable(true)]
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				if (this.ViewState["ToolTip"] != null)
				{
					return (string)this.ViewState["ToolTip"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		/// <summary>Gets or sets a non-displayed value used to store any additional data about the menu item, such as data used for handling postback events.</summary>
		/// <returns>Supplemental data about the menu item that is not displayed. The default value is an empty string ("").</returns>
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x0006D930 File Offset: 0x0006BB30
		// (set) Token: 0x060029C2 RID: 10690 RVA: 0x0006D971 File Offset: 0x0006BB71
		[DefaultValue("")]
		[Localizable(true)]
		public string Value
		{
			get
			{
				object obj = this.ViewState["Value"];
				if (obj == null)
				{
					obj = this.ViewState["Text"];
				}
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed at the bottom of a menu item to separate it from other menu items.</summary>
		/// <returns>The URL to an image used to separate the current menu item from other menu items.</returns>
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x0006D984 File Offset: 0x0006BB84
		// (set) Token: 0x060029C4 RID: 10692 RVA: 0x0006D9B3 File Offset: 0x0006BBB3
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		public string SeparatorImageUrl
		{
			get
			{
				if (this.ViewState["SeparatorImageUrl"] != null)
				{
					return (string)this.ViewState["SeparatorImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SeparatorImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object can be selected, or is "clickable."</summary>
		/// <returns>true if the menu item can be selected; otherwise, false.</returns>
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x0006D9C6 File Offset: 0x0006BBC6
		// (set) Token: 0x060029C6 RID: 10694 RVA: 0x0006D9F1 File Offset: 0x0006BBF1
		[DefaultValue(true)]
		[Browsable(true)]
		public bool Selectable
		{
			get
			{
				return this.ViewState["Selectable"] == null || (bool)this.ViewState["Selectable"];
			}
			set
			{
				this.ViewState["Selectable"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is enabled, allowing the item to display a pop-out image and any child menu items.</summary>
		/// <returns>true if the menu item is enabled; otherwise, false.</returns>
		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x0006DA09 File Offset: 0x0006BC09
		// (set) Token: 0x060029C8 RID: 10696 RVA: 0x0006DA34 File Offset: 0x0006BC34
		[DefaultValue(true)]
		[Browsable(true)]
		public bool Enabled
		{
			get
			{
				return this.ViewState["Enabled"] == null || (bool)this.ViewState["Enabled"];
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x0006DA4C File Offset: 0x0006BC4C
		internal bool BranchEnabled
		{
			get
			{
				return this.Enabled && (this.parent == null || this.parent.BranchEnabled);
			}
		}

		/// <summary>Gets or sets a value indicating whether the current menu item is selected in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>true to indicate that the current menu item is selected in a <see cref="T:System.Web.UI.WebControls.Menu" /> control; otherwise, false. The default is false.</returns>
		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x0006DA6D File Offset: 0x0006BC6D
		// (set) Token: 0x060029CB RID: 10699 RVA: 0x0006DA87 File Offset: 0x0006BC87
		[DefaultValue(false)]
		[Browsable(true)]
		public bool Selected
		{
			get
			{
				return this.menu != null && this.menu.SelectedItem == this;
			}
			set
			{
				if (this.menu != null)
				{
					if (!value && this.menu.SelectedItem == this)
					{
						this.menu.SetSelectedItem(null);
						return;
					}
					if (value)
					{
						this.menu.SetSelectedItem(this);
					}
				}
			}
		}

		/// <summary>Gets the parent menu item of the current menu item.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the parent menu item of the current menu item.</returns>
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x0006DABE File Offset: 0x0006BCBE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MenuItem Parent
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the path from the root menu item to the current menu item.</summary>
		/// <returns>A delimiter-separated list of menu item values that form a path from the root menu item to the current menu item.</returns>
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x0006DAC8 File Offset: 0x0006BCC8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ValuePath
		{
			get
			{
				if (this.menu == null)
				{
					return this.Value;
				}
				StringBuilder stringBuilder = new StringBuilder(this.Value);
				for (MenuItem menuItem = this.parent; menuItem != null; menuItem = menuItem.Parent)
				{
					stringBuilder.Insert(0, this.menu.PathSeparator);
					stringBuilder.Insert(0, menuItem.Value);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x0006DB2A File Offset: 0x0006BD2A
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x0006DB32 File Offset: 0x0006BD32
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
				this.ResetPathData();
			}
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x0006DB41 File Offset: 0x0006BD41
		internal void SetParent(MenuItem item)
		{
			this.parent = item;
			this.ResetPathData();
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x0006DB50 File Offset: 0x0006BD50
		internal string Path
		{
			get
			{
				if (this.path != null)
				{
					return this.path;
				}
				StringBuilder stringBuilder = new StringBuilder(this.index.ToString());
				for (MenuItem menuItem = this.parent; menuItem != null; menuItem = menuItem.Parent)
				{
					stringBuilder.Insert(0, '_');
					stringBuilder.Insert(0, menuItem.Index.ToString());
				}
				this.path = stringBuilder.ToString();
				return this.path;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x0006DBC2 File Offset: 0x0006BDC2
		internal bool HasChildData
		{
			get
			{
				return this.items != null;
			}
		}

		/// <summary>Loads the menu item's previously saved view state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values.</param>
		// Token: 0x060029D3 RID: 10707 RVA: 0x0006DBD0 File Offset: 0x0006BDD0
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			this.ViewState.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.ChildItems).LoadViewState(array[1]);
			}
		}

		/// <summary>Saves the view-state changes to an <see cref="T:System.Object" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains the view-state changes.</returns>
		// Token: 0x060029D4 RID: 10708 RVA: 0x0006DC0C File Offset: 0x0006BE0C
		object IStateManager.SaveViewState()
		{
			object[] array = new object[]
			{
				this.ViewState.SaveViewState(),
				(this.items == null) ? null : ((IStateManager)this.items).SaveViewState()
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to track changes to its view state.</summary>
		// Token: 0x060029D5 RID: 10709 RVA: 0x0006DC5D File Offset: 0x0006BE5D
		void IStateManager.TrackViewState()
		{
			if (this.marked)
			{
				return;
			}
			this.marked = true;
			this.ViewState.TrackViewState();
			if (this.items != null)
			{
				((IStateManager)this.items).TrackViewState();
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x0006DC8D File Offset: 0x0006BE8D
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0006DC95 File Offset: 0x0006BE95
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
			if (this.items != null)
			{
				this.items.SetDirty();
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Web.UI.WebControls.MenuItem" /> object. </summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</returns>
		// Token: 0x060029D8 RID: 10712 RVA: 0x0006DCB8 File Offset: 0x0006BEB8
		object ICloneable.Clone()
		{
			MenuItem menuItem = new MenuItem();
			foreach (object obj in this.ViewState)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				menuItem.ViewState[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			foreach (object obj2 in this.ChildItems)
			{
				ICloneable cloneable = (ICloneable)obj2;
				menuItem.ChildItems.Add((MenuItem)cloneable.Clone());
			}
			return menuItem;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0006DD8C File Offset: 0x0006BF8C
		internal void Bind(IHierarchyData hierarchyData)
		{
			this.hierarchyData = hierarchyData;
			this.DataBound = true;
			this.DataPath = hierarchyData.Path;
			this.dataItem = hierarchyData.Item;
			MenuItemBinding menuItemBinding = this.GetBinding();
			if (menuItemBinding != null)
			{
				if (menuItemBinding.EnabledField != "")
				{
					try
					{
						this.Enabled = Convert.ToBoolean(this.GetBoundPropertyValue(menuItemBinding.EnabledField));
						goto IL_0079;
					}
					catch
					{
						this.Enabled = menuItemBinding.Enabled;
						goto IL_0079;
					}
				}
				this.Enabled = menuItemBinding.Enabled;
				IL_0079:
				if (menuItemBinding.ImageUrlField.Length > 0)
				{
					this.ImageUrl = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.ImageUrlField));
					if (this.ImageUrl.Length == 0)
					{
						this.ImageUrl = menuItemBinding.ImageUrl;
					}
				}
				else if (menuItemBinding.ImageUrl.Length > 0)
				{
					this.ImageUrl = menuItemBinding.ImageUrl;
				}
				if (menuItemBinding.NavigateUrlField.Length > 0)
				{
					this.NavigateUrl = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.NavigateUrlField));
					if (this.NavigateUrl.Length == 0)
					{
						this.NavigateUrl = menuItemBinding.NavigateUrl;
					}
				}
				else if (menuItemBinding.NavigateUrl.Length > 0)
				{
					this.NavigateUrl = menuItemBinding.NavigateUrl;
				}
				if (menuItemBinding.PopOutImageUrlField.Length > 0)
				{
					this.PopOutImageUrl = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.PopOutImageUrlField));
					if (this.PopOutImageUrl.Length == 0)
					{
						this.PopOutImageUrl = menuItemBinding.PopOutImageUrl;
					}
				}
				else if (menuItemBinding.PopOutImageUrl.Length > 0)
				{
					this.PopOutImageUrl = menuItemBinding.PopOutImageUrl;
				}
				if (menuItemBinding.SelectableField != "")
				{
					try
					{
						this.Selectable = Convert.ToBoolean(this.GetBoundPropertyValue(menuItemBinding.SelectableField));
						goto IL_01CD;
					}
					catch
					{
						this.Selectable = menuItemBinding.Selectable;
						goto IL_01CD;
					}
				}
				this.Selectable = menuItemBinding.Selectable;
				IL_01CD:
				if (menuItemBinding.SeparatorImageUrlField.Length > 0)
				{
					this.SeparatorImageUrl = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.SeparatorImageUrlField));
					if (this.SeparatorImageUrl.Length == 0)
					{
						this.SeparatorImageUrl = menuItemBinding.SeparatorImageUrl;
					}
				}
				else if (menuItemBinding.SeparatorImageUrl.Length > 0)
				{
					this.SeparatorImageUrl = menuItemBinding.SeparatorImageUrl;
				}
				if (menuItemBinding.TargetField.Length > 0)
				{
					this.Target = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.TargetField));
					if (this.Target.Length == 0)
					{
						this.Target = menuItemBinding.Target;
					}
				}
				else if (menuItemBinding.Target.Length > 0)
				{
					this.Target = menuItemBinding.Target;
				}
				if (menuItemBinding.ToolTipField.Length > 0)
				{
					this.ToolTip = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.ToolTipField));
					if (this.ToolTip.Length == 0)
					{
						this.ToolTip = menuItemBinding.ToolTip;
					}
				}
				else if (menuItemBinding.ToolTip.Length > 0)
				{
					this.ToolTip = menuItemBinding.ToolTip;
				}
				string text = null;
				if (menuItemBinding.ValueField.Length > 0)
				{
					text = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.ValueField));
				}
				if (string.IsNullOrEmpty(text))
				{
					if (menuItemBinding.Value.Length > 0)
					{
						text = menuItemBinding.Value;
					}
					else if (menuItemBinding.Text.Length > 0)
					{
						text = menuItemBinding.Text;
					}
					else
					{
						text = string.Empty;
					}
				}
				this.Value = text;
				string text2 = null;
				if (menuItemBinding.TextField.Length > 0)
				{
					text2 = Convert.ToString(this.GetBoundPropertyValue(menuItemBinding.TextField));
					if (menuItemBinding.FormatString.Length > 0)
					{
						text2 = string.Format(menuItemBinding.FormatString, text2);
					}
				}
				if (string.IsNullOrEmpty(text2))
				{
					if (menuItemBinding.Text.Length > 0)
					{
						text2 = menuItemBinding.Text;
					}
					else if (menuItemBinding.Value.Length > 0)
					{
						text2 = menuItemBinding.Value;
					}
					else
					{
						text2 = string.Empty;
					}
				}
				this.Text = text2;
			}
			else
			{
				this.Text = (this.Value = this.GetDefaultBoundText());
			}
			INavigateUIData navigateUIData = hierarchyData as INavigateUIData;
			if (navigateUIData != null)
			{
				this.ToolTip = navigateUIData.Description;
				this.Text = navigateUIData.ToString();
				this.NavigateUrl = navigateUIData.NavigateUrl;
			}
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0006E1BC File Offset: 0x0006C3BC
		internal void SetDataItem(object item)
		{
			this.dataItem = item;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x0006E1C5 File Offset: 0x0006C3C5
		internal void SetDataPath(string path)
		{
			this.DataPath = path;
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0006E1CE File Offset: 0x0006C3CE
		internal void SetDataBound(bool bound)
		{
			this.DataBound = bound;
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0006E1D7 File Offset: 0x0006C3D7
		private string GetDefaultBoundText()
		{
			if (this.hierarchyData != null)
			{
				return this.hierarchyData.ToString();
			}
			if (this.dataItem != null)
			{
				return this.dataItem.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0006E206 File Offset: 0x0006C406
		private string GetDataItemType()
		{
			if (this.hierarchyData != null)
			{
				return this.hierarchyData.Type;
			}
			if (this.dataItem != null)
			{
				return this.dataItem.GetType().ToString();
			}
			return string.Empty;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0006E23C File Offset: 0x0006C43C
		private MenuItemBinding GetBinding()
		{
			if (this.menu == null)
			{
				return null;
			}
			if (this.gotBinding)
			{
				return this.binding;
			}
			this.binding = this.menu.FindBindingForItem(this.GetDataItemType(), this.Depth);
			this.gotBinding = true;
			return this.binding;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x0006E28C File Offset: 0x0006C48C
		private object GetBoundPropertyValue(string name)
		{
			if (this.boundProperties == null)
			{
				if (this.hierarchyData != null)
				{
					this.boundProperties = TypeDescriptor.GetProperties(this.hierarchyData);
				}
				else
				{
					this.boundProperties = TypeDescriptor.GetProperties(this.dataItem);
				}
			}
			PropertyDescriptor propertyDescriptor = this.boundProperties.Find(name, true);
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException("Property '" + name + "' not found in data bound item");
			}
			if (this.hierarchyData != null)
			{
				return propertyDescriptor.GetValue(this.hierarchyData);
			}
			return propertyDescriptor.GetValue(this.dataItem);
		}

		// Token: 0x04001AC2 RID: 6850
		private StateBag ViewState = new StateBag();

		// Token: 0x04001AC3 RID: 6851
		private MenuItemCollection items;

		// Token: 0x04001AC4 RID: 6852
		private bool marked;

		// Token: 0x04001AC5 RID: 6853
		private Menu menu;

		// Token: 0x04001AC6 RID: 6854
		private MenuItem parent;

		// Token: 0x04001AC7 RID: 6855
		private int index;

		// Token: 0x04001AC8 RID: 6856
		private string path;

		// Token: 0x04001AC9 RID: 6857
		private int depth = -1;

		// Token: 0x04001ACA RID: 6858
		private object dataItem;

		// Token: 0x04001ACB RID: 6859
		private IHierarchyData hierarchyData;

		// Token: 0x04001ACC RID: 6860
		private bool gotBinding;

		// Token: 0x04001ACD RID: 6861
		private MenuItemBinding binding;

		// Token: 0x04001ACE RID: 6862
		private PropertyDescriptorCollection boundProperties;
	}
}
