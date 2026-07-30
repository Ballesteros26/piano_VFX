using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a menu in an ASP.NET Web page.</summary>
	// Token: 0x020003CE RID: 974
	[SupportsEventValidation]
	[DefaultEvent("MenuItemClick")]
	[ControlValueProperty("SelectedValue")]
	[Designer("System.Web.UI.Design.WebControls.MenuDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class Menu : HierarchicalDataBoundControl, IPostBackEventHandler, INamingContainer
	{
		/// <summary>Occurs when a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control is clicked.</summary>
		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06002900 RID: 10496 RVA: 0x0006B0F0 File Offset: 0x000692F0
		// (remove) Token: 0x06002901 RID: 10497 RVA: 0x0006B103 File Offset: 0x00069303
		public event MenuEventHandler MenuItemClick
		{
			add
			{
				base.Events.AddHandler(Menu.MenuItemClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Menu.MenuItemClickEvent, value);
			}
		}

		/// <summary>Occurs when a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control is bound to data.</summary>
		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06002902 RID: 10498 RVA: 0x0006B116 File Offset: 0x00069316
		// (remove) Token: 0x06002903 RID: 10499 RVA: 0x0006B129 File Offset: 0x00069329
		public event MenuEventHandler MenuItemDataBound
		{
			add
			{
				base.Events.AddHandler(Menu.MenuItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Menu.MenuItemDataBoundEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> that contains the event data.</param>
		// Token: 0x06002904 RID: 10500 RVA: 0x0006B13C File Offset: 0x0006933C
		protected virtual void OnMenuItemClick(MenuEventArgs e)
		{
			if (base.Events != null)
			{
				MenuEventHandler menuEventHandler = (MenuEventHandler)base.Events[Menu.MenuItemClickEvent];
				if (menuEventHandler != null)
				{
					menuEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemDataBound" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> that contains the event data.</param>
		// Token: 0x06002905 RID: 10501 RVA: 0x0006B174 File Offset: 0x00069374
		protected virtual void OnMenuItemDataBound(MenuEventArgs e)
		{
			if (base.Events != null)
			{
				MenuEventHandler menuEventHandler = (MenuEventHandler)base.Events[Menu.MenuItemDataBoundEvent];
				if (menuEventHandler != null)
				{
					menuEventHandler(this, e);
				}
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x0006B1AA File Offset: 0x000693AA
		private IMenuRenderer Renderer
		{
			get
			{
				if (this.renderer == null)
				{
					this.renderer = this.CreateRenderer(null);
				}
				return this.renderer;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x0006B1C8 File Offset: 0x000693C8
		private bool RenderList
		{
			get
			{
				if (this.renderList == null)
				{
					MenuRenderingMode menuRenderingMode = this.RenderingMode;
					if (menuRenderingMode != MenuRenderingMode.Table)
					{
						if (menuRenderingMode == MenuRenderingMode.List)
						{
							this.renderList = new bool?(true);
						}
						else if (base.RenderingCompatibilityLessThan40)
						{
							this.renderList = new bool?(false);
						}
						else
						{
							this.renderList = new bool?(true);
						}
					}
					else
					{
						this.renderList = new bool?(false);
					}
				}
				return this.renderList.Value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether ASP.NET should render a block of cascading style sheet (CSS) definitions for the styles that are used in the menu.</summary>
		/// <returns>A value that indicates whether ASP.NET should render a block of CSS definitions for the styles that are used in the menu. The default value is true.</returns>
		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x0006B23A File Offset: 0x0006943A
		// (set) Token: 0x06002909 RID: 10505 RVA: 0x0006B242 File Offset: 0x00069442
		[DefaultValue(true)]
		[Description("Determines whether or not to render the inline style block (only used in standards compliance mode)")]
		public bool IncludeStyleBlock
		{
			get
			{
				return this.includeStyleBlock;
			}
			set
			{
				this.includeStyleBlock = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Web.UI.WebControls.Menu" /> control renders HTML table elements and inline styles, or listitem elements and cascading style sheet (CSS) styles.</summary>
		/// <returns>A value that specifies whether the <see cref="T:System.Web.UI.WebControls.Menu" /> control renders HTML table elements and inline styles, or listitem elements and cascading style sheet (CSS) styles. The default value is <see cref="F:System.Web.UI.WebControls.MenuRenderingMode.Default" />.</returns>
		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x0006B24B File Offset: 0x0006944B
		// (set) Token: 0x0600290B RID: 10507 RVA: 0x0006B253 File Offset: 0x00069453
		[DefaultValue(MenuRenderingMode.Default)]
		public MenuRenderingMode RenderingMode
		{
			get
			{
				return this.renderingMode;
			}
			set
			{
				if (value < MenuRenderingMode.Default || value > MenuRenderingMode.List)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.renderingMode = value;
				this.renderer = this.CreateRenderer(this.renderer);
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> objects that define the relationship between a data item and the menu item it is binding to. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemBindingCollection" /> that represents the relationship between a data item and the menu item it is binding to.</returns>
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x0006B281 File Offset: 0x00069481
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.MenuBindingsEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MergableProperty(false)]
		[DefaultValue(null)]
		public MenuItemBindingCollection DataBindings
		{
			get
			{
				if (this.dataBindings == null)
				{
					this.dataBindings = new MenuItemBindingCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.dataBindings).TrackViewState();
					}
				}
				return this.dataBindings;
			}
		}

		/// <summary>Gets or sets the duration for which a dynamic menu is displayed after the mouse pointer is no longer positioned over the menu.</summary>
		/// <returns>The amount of time (in milliseconds) a dynamic menu is displayed after the mouse pointer is no longer positioned over the menu. The default is 500.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than -1.</exception>
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x0006B2B0 File Offset: 0x000694B0
		// (set) Token: 0x0600290E RID: 10510 RVA: 0x0006B2DD File Offset: 0x000694DD
		[DefaultValue(500)]
		[Themeable(false)]
		public int DisappearAfter
		{
			get
			{
				object obj = this.ViewState["DisappearAfter"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 500;
			}
			set
			{
				this.ViewState["DisappearAfter"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image to display at the bottom of each dynamic menu item to separate it from other menu items.</summary>
		/// <returns>The URL to a separator image displayed at the bottom of each dynamic menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x0600290F RID: 10511 RVA: 0x0006B2F8 File Offset: 0x000694F8
		// (set) Token: 0x06002910 RID: 10512 RVA: 0x0006B325 File Offset: 0x00069525
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[Themeable(true)]
		[DefaultValue("")]
		public string DynamicBottomSeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["dbsiu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["dbsiu"] = value;
			}
		}

		/// <summary>Gets or sets additional text shown with all menu items that are dynamically displayed.</summary>
		/// <returns>The additional text or characters that appear with all menu items. The default value for this property is "{0}."</returns>
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x0006B338 File Offset: 0x00069538
		// (set) Token: 0x06002912 RID: 10514 RVA: 0x0006B365 File Offset: 0x00069565
		[DefaultValue("")]
		public string DynamicItemFormatString
		{
			get
			{
				object obj = this.ViewState["DynamicItemFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["DynamicItemFormatString"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image to display at the top of each dynamic menu item to separate it from other menu items.</summary>
		/// <returns>The URL to a separator image displayed at the top of each dynamic menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x0006B378 File Offset: 0x00069578
		// (set) Token: 0x06002914 RID: 10516 RVA: 0x0006B3A5 File Offset: 0x000695A5
		[DefaultValue("")]
		[UrlProperty]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DynamicTopSeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["dtsiu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["dtsiu"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed as the separator at the bottom of each static menu item.</summary>
		/// <returns>The URL to an image displayed as the separator at the bottom of each static menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x0006B3B8 File Offset: 0x000695B8
		// (set) Token: 0x06002916 RID: 10518 RVA: 0x0006B3E5 File Offset: 0x000695E5
		[WebCategory("Appearance")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string StaticBottomSeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["sbsiu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["sbsiu"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed as the separator at the top of each static menu item.</summary>
		/// <returns>The URL to an image displayed as the separator at the top of each static menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x0006B3F8 File Offset: 0x000695F8
		// (set) Token: 0x06002918 RID: 10520 RVA: 0x0006B425 File Offset: 0x00069625
		[DefaultValue("")]
		[UrlProperty]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string StaticTopSeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["stsiu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["stsiu"] = value;
			}
		}

		/// <summary>Gets or sets the direction in which to render the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.Orientation" /> enumeration values. The default is Orientation.Vertical.</returns>
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x0006B438 File Offset: 0x00069638
		// (set) Token: 0x0600291A RID: 10522 RVA: 0x0006B461 File Offset: 0x00069661
		[DefaultValue(Orientation.Vertical)]
		public Orientation Orientation
		{
			get
			{
				object obj = this.ViewState["Orientation"];
				if (obj != null)
				{
					return (Orientation)obj;
				}
				return Orientation.Vertical;
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		/// <summary>Gets or sets the number of menu levels to display in a static menu.</summary>
		/// <returns>The number of menu levels to display in a static menu. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 1.</exception>
		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x0006B47C File Offset: 0x0006967C
		// (set) Token: 0x0600291C RID: 10524 RVA: 0x0006B4A5 File Offset: 0x000696A5
		[DefaultValue(1)]
		[Themeable(true)]
		public int StaticDisplayLevels
		{
			get
			{
				object obj = this.ViewState["StaticDisplayLevels"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.ViewState["StaticDisplayLevels"] = value;
			}
		}

		/// <summary>Gets or sets additional text shown with all menu items that are statically displayed.</summary>
		/// <returns>The additional text or characters that appear with all menu items. The default value for this property is "{0}."</returns>
		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x0006B4C8 File Offset: 0x000696C8
		// (set) Token: 0x0600291E RID: 10526 RVA: 0x0006B4F5 File Offset: 0x000696F5
		[DefaultValue("")]
		public string StaticItemFormatString
		{
			get
			{
				object obj = this.ViewState["StaticItemFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["StaticItemFormatString"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space, in pixels, to indent submenus within a static menu.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the amount of space, in pixels, to indent submenus within a static menu. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the selected <see cref="T:System.Web.UI.WebControls.Unit" /> is less than 0.</exception>
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x0006B508 File Offset: 0x00069708
		// (set) Token: 0x06002920 RID: 10528 RVA: 0x0006B535 File Offset: 0x00069735
		[DefaultValue(typeof(Unit), "16px")]
		[Themeable(true)]
		public Unit StaticSubMenuIndent
		{
			get
			{
				object obj = this.ViewState["StaticSubMenuIndent"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				this.ViewState["StaticSubMenuIndent"] = value;
			}
		}

		/// <summary>Gets or sets the number of menu levels to render for a dynamic menu.</summary>
		/// <returns>The number of menu levels to render for a dynamic menu. The default is 3.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Menu.MaximumDynamicDisplayLevels" /> property is set to a value less than 0.</exception>
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x0006B550 File Offset: 0x00069750
		// (set) Token: 0x06002922 RID: 10530 RVA: 0x0006B579 File Offset: 0x00069779
		[Themeable(true)]
		[DefaultValue(3)]
		public int MaximumDynamicDisplayLevels
		{
			get
			{
				object obj = this.ViewState["MaximumDynamicDisplayLevels"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 3;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.ViewState["MaximumDynamicDisplayLevels"] = value;
			}
		}

		/// <summary>Gets or sets the number of pixels to shift a dynamic menu vertically relative to its parent menu item.</summary>
		/// <returns>The number of pixels to shift a dynamic menu vertically relative to its parent menu item. The default is 0.</returns>
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x0006B59C File Offset: 0x0006979C
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x0006B5C5 File Offset: 0x000697C5
		[DefaultValue(0)]
		public int DynamicVerticalOffset
		{
			get
			{
				object obj = this.ViewState["DynamicVerticalOffset"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["DynamicVerticalOffset"] = value;
			}
		}

		/// <summary>Gets or sets the number of pixels to shift a dynamic menu horizontally relative to its parent menu item.</summary>
		/// <returns>The number of pixels to shift a dynamic menu horizontally relative to its parent menu item. The default is 0.</returns>
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x0006B5E0 File Offset: 0x000697E0
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x0006B609 File Offset: 0x00069809
		[DefaultValue(0)]
		public int DynamicHorizontalOffset
		{
			get
			{
				object obj = this.ViewState["DynamicHorizontalOffset"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["DynamicHorizontalOffset"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the built-in image that indicates that a dynamic menu item has a submenu is displayed.</summary>
		/// <returns>true to display the built-in image for dynamic menu items with submenus; otherwise, false. The default is true.</returns>
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x0006B624 File Offset: 0x00069824
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x0006B64D File Offset: 0x0006984D
		[DefaultValue(true)]
		public bool DynamicEnableDefaultPopOutImage
		{
			get
			{
				object obj = this.ViewState["dedpoi"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["dedpoi"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the built-in image is displayed to indicate that a static menu item has a submenu.</summary>
		/// <returns>true to display the built-in image for static menu items with submenus; otherwise, false. The default is true.</returns>
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x0006B668 File Offset: 0x00069868
		// (set) Token: 0x0600292A RID: 10538 RVA: 0x0006B691 File Offset: 0x00069891
		[DefaultValue(true)]
		public bool StaticEnableDefaultPopOutImage
		{
			get
			{
				object obj = this.ViewState["sedpoi"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["sedpoi"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object that contains all menu items in the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> that contains all menu items in the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x0006B6A9 File Offset: 0x000698A9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Editor("System.Web.UI.Design.MenuItemCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public MenuItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new MenuItemCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.items).TrackViewState();
					}
				}
				return this.items;
			}
		}

		/// <summary>Gets or sets the character used to delimit the path of a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>The character used to delimit the path of a menu item. The default value is a slash mark (/).</returns>
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x0006B6D8 File Offset: 0x000698D8
		// (set) Token: 0x0600292D RID: 10541 RVA: 0x0006B702 File Offset: 0x00069902
		[DefaultValue('/')]
		public char PathSeparator
		{
			get
			{
				object obj = this.ViewState["PathSeparator"];
				if (obj != null)
				{
					return (char)obj;
				}
				return '/';
			}
			set
			{
				this.ViewState["PathSeparator"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text for menu items should wrap.</summary>
		/// <returns>true to wrap the menu item text; otherwise, false. The default is false.</returns>
		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x0006B71C File Offset: 0x0006991C
		// (set) Token: 0x0600292F RID: 10543 RVA: 0x0006B745 File Offset: 0x00069945
		[DefaultValue(false)]
		public bool ItemWrap
		{
			get
			{
				object obj = this.ViewState["ItemWrap"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ItemWrap"] = value;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x0006B75D File Offset: 0x0006995D
		internal Style PopOutBoxStyle
		{
			get
			{
				if (this.popOutBoxStyle == null)
				{
					this.popOutBoxStyle = new Style();
					this.popOutBoxStyle.BackColor = Color.White;
				}
				return this.popOutBoxStyle;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x0006B788 File Offset: 0x00069988
		internal Style ControlLinkStyle
		{
			get
			{
				if (this.controlLinkStyle == null)
				{
					this.controlLinkStyle = new Style();
					this.controlLinkStyle.AlwaysRenderTextDecoration = true;
				}
				return this.controlLinkStyle;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x0006B7AF File Offset: 0x000699AF
		internal Style DynamicMenuItemLinkStyle
		{
			get
			{
				if (this.dynamicMenuItemLinkStyle == null)
				{
					this.dynamicMenuItemLinkStyle = new Style();
				}
				return this.dynamicMenuItemLinkStyle;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x0006B7CA File Offset: 0x000699CA
		internal Style StaticMenuItemLinkStyle
		{
			get
			{
				if (this.staticMenuItemLinkStyle == null)
				{
					this.staticMenuItemLinkStyle = new Style();
				}
				return this.staticMenuItemLinkStyle;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06002934 RID: 10548 RVA: 0x0006B7E5 File Offset: 0x000699E5
		internal Style DynamicSelectedLinkStyle
		{
			get
			{
				if (this.dynamicSelectedLinkStyle == null)
				{
					this.dynamicSelectedLinkStyle = new Style();
				}
				return this.dynamicSelectedLinkStyle;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06002935 RID: 10549 RVA: 0x0006B800 File Offset: 0x00069A00
		internal Style StaticSelectedLinkStyle
		{
			get
			{
				if (this.staticSelectedLinkStyle == null)
				{
					this.staticSelectedLinkStyle = new Style();
				}
				return this.staticSelectedLinkStyle;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x0006B81B File Offset: 0x00069A1B
		internal Style DynamicHoverLinkStyle
		{
			get
			{
				if (this.dynamicHoverLinkStyle == null)
				{
					this.dynamicHoverLinkStyle = new Style();
				}
				return this.dynamicHoverLinkStyle;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x0006B836 File Offset: 0x00069A36
		internal Style StaticHoverLinkStyle
		{
			get
			{
				if (this.staticHoverLinkStyle == null)
				{
					this.staticHoverLinkStyle = new Style();
				}
				return this.staticHoverLinkStyle;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x0006B851 File Offset: 0x00069A51
		internal MenuItemStyle StaticMenuItemStyleInternal
		{
			get
			{
				return this.staticMenuItemStyle;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x0006B859 File Offset: 0x00069A59
		internal SubMenuStyle StaticMenuStyleInternal
		{
			get
			{
				return this.staticMenuStyle;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x0006B861 File Offset: 0x00069A61
		internal MenuItemStyle DynamicMenuItemStyleInternal
		{
			get
			{
				return this.dynamicMenuItemStyle;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x0006B869 File Offset: 0x00069A69
		internal SubMenuStyle DynamicMenuStyleInternal
		{
			get
			{
				return this.dynamicMenuStyle;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x0006B871 File Offset: 0x00069A71
		internal MenuItemStyleCollection LevelMenuItemStylesInternal
		{
			get
			{
				return this.levelMenuItemStyles;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600293D RID: 10557 RVA: 0x00003BEA File Offset: 0x00001DEA
		internal List<Style> LevelMenuItemLinkStyles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x0006B879 File Offset: 0x00069A79
		internal SubMenuStyleCollection LevelSubMenuStylesInternal
		{
			get
			{
				return this.levelSubMenuStyles;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x0006B881 File Offset: 0x00069A81
		internal MenuItemStyle StaticSelectedStyleInternal
		{
			get
			{
				return this.staticSelectedStyle;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x0006B889 File Offset: 0x00069A89
		internal MenuItemStyle DynamicSelectedStyleInternal
		{
			get
			{
				return this.dynamicSelectedStyle;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x0006B891 File Offset: 0x00069A91
		internal MenuItemStyleCollection LevelSelectedStylesInternal
		{
			get
			{
				return this.levelSelectedStyles;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x00003BEA File Offset: 0x00001DEA
		internal List<Style> LevelSelectedLinkStyles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x0006B899 File Offset: 0x00069A99
		internal Style StaticHoverStyleInternal
		{
			get
			{
				return this.staticHoverStyle;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x0006B8A1 File Offset: 0x00069AA1
		internal Style DynamicHoverStyleInternal
		{
			get
			{
				return this.dynamicHoverStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of the menu items within a dynamic menu.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of the menu items within a dynamic menu.</returns>
		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x0006B8A9 File Offset: 0x00069AA9
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MenuItemStyle DynamicMenuItemStyle
		{
			get
			{
				if (this.dynamicMenuItemStyle == null)
				{
					this.dynamicMenuItemStyle = new MenuItemStyle();
					if (base.IsTrackingViewState)
					{
						this.dynamicMenuItemStyle.TrackViewState();
					}
				}
				return this.dynamicMenuItemStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of the dynamic menu item selected by the user.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of the selected dynamic menu item.</returns>
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x0006B8D7 File Offset: 0x00069AD7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public MenuItemStyle DynamicSelectedStyle
		{
			get
			{
				if (this.dynamicSelectedStyle == null)
				{
					this.dynamicSelectedStyle = new MenuItemStyle();
					if (base.IsTrackingViewState)
					{
						this.dynamicSelectedStyle.TrackViewState();
					}
				}
				return this.dynamicSelectedStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of a dynamic menu.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of a dynamic menu.</returns>
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x0006B905 File Offset: 0x00069B05
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SubMenuStyle DynamicMenuStyle
		{
			get
			{
				if (this.dynamicMenuStyle == null)
				{
					this.dynamicMenuStyle = new SubMenuStyle();
					if (base.IsTrackingViewState)
					{
						this.dynamicMenuStyle.TrackViewState();
					}
				}
				return this.dynamicMenuStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of the menu items in a static menu.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of the menu items in a static menu.</returns>
		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x0006B933 File Offset: 0x00069B33
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public MenuItemStyle StaticMenuItemStyle
		{
			get
			{
				if (this.staticMenuItemStyle == null)
				{
					this.staticMenuItemStyle = new MenuItemStyle();
					if (base.IsTrackingViewState)
					{
						this.staticMenuItemStyle.TrackViewState();
					}
				}
				return this.staticMenuItemStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of the menu item selected by the user in a static menu.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of the selected menu item in a static menu.</returns>
		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x0006B961 File Offset: 0x00069B61
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public MenuItemStyle StaticSelectedStyle
		{
			get
			{
				if (this.staticSelectedStyle == null)
				{
					this.staticSelectedStyle = new MenuItemStyle();
					if (base.IsTrackingViewState)
					{
						this.staticSelectedStyle.TrackViewState();
					}
				}
				return this.staticSelectedStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> object that allows you to set the appearance of a static menu.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.MenuItemStyle" /> that represents the style of a static menu.</returns>
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x0006B98F File Offset: 0x00069B8F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SubMenuStyle StaticMenuStyle
		{
			get
			{
				if (this.staticMenuStyle == null)
				{
					this.staticMenuStyle = new SubMenuStyle();
					if (base.IsTrackingViewState)
					{
						this.staticMenuStyle.TrackViewState();
					}
				}
				return this.staticMenuStyle;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object that contains the style settings that are applied to menu items based on their level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> that contains the style settings that are applied to menu items based on their level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x0006B9BD File Offset: 0x00069BBD
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.MenuItemStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public MenuItemStyleCollection LevelMenuItemStyles
		{
			get
			{
				if (this.levelMenuItemStyles == null)
				{
					this.levelMenuItemStyles = new MenuItemStyleCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.levelMenuItemStyles).TrackViewState();
					}
				}
				return this.levelMenuItemStyles;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object that contains the style settings that are applied to the selected menu item based on its level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> that contains the style settings that are applied to the selected menu item based on its level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x0006B9EB File Offset: 0x00069BEB
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.MenuItemStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public MenuItemStyleCollection LevelSelectedStyles
		{
			get
			{
				if (this.levelSelectedStyles == null)
				{
					this.levelSelectedStyles = new MenuItemStyleCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.levelSelectedStyles).TrackViewState();
					}
				}
				return this.levelSelectedStyles;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> object that contains the style settings that are applied to the submenu items in the static menu based on their level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemStyleCollection" /> that contains the style settings that are applied to the submenu items in the static menu based on their level in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x0006BA19 File Offset: 0x00069C19
		[Editor("System.Web.UI.Design.WebControls.SubMenuStyleCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public SubMenuStyleCollection LevelSubMenuStyles
		{
			get
			{
				if (this.levelSubMenuStyles == null)
				{
					this.levelSubMenuStyles = new SubMenuStyleCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.levelSubMenuStyles).TrackViewState();
					}
				}
				return this.levelSubMenuStyles;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of a dynamic menu item when the mouse pointer is positioned over it.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of a dynamic menu item when the mouse pointer is positioned over it.</returns>
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x0006BA47 File Offset: 0x00069C47
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style DynamicHoverStyle
		{
			get
			{
				if (this.dynamicHoverStyle == null)
				{
					this.dynamicHoverStyle = new Style();
					if (base.IsTrackingViewState)
					{
						this.dynamicHoverStyle.TrackViewState();
					}
				}
				return this.dynamicHoverStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that allows you to set the appearance of a static menu item when the mouse pointer is positioned over it.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of a static menu item when the mouse pointer is positioned over it.</returns>
		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x0006BA75 File Offset: 0x00069C75
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style StaticHoverStyle
		{
			get
			{
				if (this.staticHoverStyle == null)
				{
					this.staticHoverStyle = new Style();
					if (base.IsTrackingViewState)
					{
						this.staticHoverStyle.TrackViewState();
					}
				}
				return this.staticHoverStyle;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed in a dynamic menu to indicate that the user can scroll down for additional menu items.</summary>
		/// <returns>The URL to an image displayed in a dynamic menu to indicate that the user can scroll down for additional menu items. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x0006BAA4 File Offset: 0x00069CA4
		// (set) Token: 0x06002951 RID: 10577 RVA: 0x0006BAD1 File Offset: 0x00069CD1
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ScrollDownImageUrl
		{
			get
			{
				object obj = this.ViewState["sdiu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["sdiu"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed in a dynamic menu to indicate that the user can scroll up for additional menu items.</summary>
		/// <returns>The URL to an image displayed in a dynamic menu to indicate that the user can scroll up for additional menu items. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x0006BAE4 File Offset: 0x00069CE4
		// (set) Token: 0x06002953 RID: 10579 RVA: 0x0006BB11 File Offset: 0x00069D11
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ScrollUpImageUrl
		{
			get
			{
				object obj = this.ViewState["suiu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["suiu"] = value;
			}
		}

		/// <summary>Gets or sets the alternate text for the image specified in the <see cref="P:System.Web.UI.WebControls.Menu.ScrollDownImageUrl" /> property.</summary>
		/// <returns>The alternate text for the image specified in the <see cref="P:System.Web.UI.WebControls.Menu.ScrollDownImageUrl" /> property. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002954 RID: 10580 RVA: 0x0006BB24 File Offset: 0x00069D24
		// (set) Token: 0x06002955 RID: 10581 RVA: 0x0006BB56 File Offset: 0x00069D56
		[Localizable(true)]
		public string ScrollDownText
		{
			get
			{
				object obj = this.ViewState["ScrollDownText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Scroll down");
			}
			set
			{
				this.ViewState["ScrollDownText"] = value;
			}
		}

		/// <summary>Gets or sets the alternate text for the image specified in the <see cref="P:System.Web.UI.WebControls.Menu.ScrollUpImageUrl" /> property.</summary>
		/// <returns>The alternate text for the image specified in the <see cref="P:System.Web.UI.WebControls.Menu.ScrollUpImageUrl" /> property. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x0006BB6C File Offset: 0x00069D6C
		// (set) Token: 0x06002957 RID: 10583 RVA: 0x0006BB9E File Offset: 0x00069D9E
		[Localizable(true)]
		public string ScrollUpText
		{
			get
			{
				object obj = this.ViewState["ScrollUpText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Scroll up");
			}
			set
			{
				this.ViewState["ScrollUpText"] = value;
			}
		}

		/// <summary>Gets or sets the alternate text for the image used to indicate that a dynamic menu item has a submenu.</summary>
		/// <returns>The alternate text for the image used to indicate that a dynamic menu item has a submenu. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x0006BBB4 File Offset: 0x00069DB4
		// (set) Token: 0x06002959 RID: 10585 RVA: 0x0006BBE6 File Offset: 0x00069DE6
		public string DynamicPopOutImageTextFormatString
		{
			get
			{
				object obj = this.ViewState["dpoitf"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Expand {0}");
			}
			set
			{
				this.ViewState["dpoitf"] = value;
			}
		}

		/// <summary>Gets or sets the URL to a custom image that is displayed in a dynamic menu item when the dynamic menu item has a submenu.</summary>
		/// <returns>The URL to an image used to indicate that a dynamic menu item has a submenu. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600295A RID: 10586 RVA: 0x0006BBFC File Offset: 0x00069DFC
		// (set) Token: 0x0600295B RID: 10587 RVA: 0x0006BC29 File Offset: 0x00069E29
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty]
		public string DynamicPopOutImageUrl
		{
			get
			{
				object obj = this.ViewState["dpoiu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["dpoiu"] = value;
			}
		}

		/// <summary>Gets or sets the alternate text for the pop-out image used to indicate that a static menu item has a submenu.</summary>
		/// <returns>The alternate text for the pop-out image. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x0600295C RID: 10588 RVA: 0x0006BC3C File Offset: 0x00069E3C
		// (set) Token: 0x0600295D RID: 10589 RVA: 0x0006BC6E File Offset: 0x00069E6E
		public string StaticPopOutImageTextFormatString
		{
			get
			{
				object obj = this.ViewState["spoitf"];
				if (obj != null)
				{
					return (string)obj;
				}
				return global::Locale.GetText("Expand {0}");
			}
			set
			{
				this.ViewState["spoitf"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image displayed to indicate that a static menu item has a submenu.</summary>
		/// <returns>The URL to an image displayed to indicate that a static menu item has a submenu. The default value is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x0006BC84 File Offset: 0x00069E84
		// (set) Token: 0x0600295F RID: 10591 RVA: 0x0006BCB1 File Offset: 0x00069EB1
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string StaticPopOutImageUrl
		{
			get
			{
				object obj = this.ViewState["spoiu"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["spoiu"] = value;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a menu item.</summary>
		/// <returns>The target window or frame in which to display the linked Web page content. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x0006BCC4 File Offset: 0x00069EC4
		// (set) Token: 0x06002961 RID: 10593 RVA: 0x00046F16 File Offset: 0x00045116
		[DefaultValue("")]
		public string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the template that contains the custom content to render for a static menu.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for a static menu. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x0006BCF1 File Offset: 0x00069EF1
		// (set) Token: 0x06002963 RID: 10595 RVA: 0x0006BCF9 File Offset: 0x00069EF9
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(MenuItemTemplateContainer), BindingDirection.OneWay)]
		public ITemplate StaticItemTemplate
		{
			get
			{
				return this.staticItemTemplate;
			}
			set
			{
				this.staticItemTemplate = value;
			}
		}

		/// <summary>Gets or sets the template that contains the custom content to render for a dynamic menu.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for a dynamic menu. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x0006BD02 File Offset: 0x00069F02
		// (set) Token: 0x06002965 RID: 10597 RVA: 0x0006BD0A File Offset: 0x00069F0A
		[DefaultValue(null)]
		[TemplateContainer(typeof(MenuItemTemplateContainer), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public ITemplate DynamicItemTemplate
		{
			get
			{
				return this.dynamicItemTemplate;
			}
			set
			{
				this.dynamicItemTemplate = value;
			}
		}

		/// <summary>Gets the selected menu item.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the selected menu item.</returns>
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x0006BD13 File Offset: 0x00069F13
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public MenuItem SelectedItem
		{
			get
			{
				if (this.selectedItem == null && this.selectedItemPath != null)
				{
					this.selectedItem = this.FindItemByPos(this.selectedItemPath);
				}
				return this.selectedItem;
			}
		}

		/// <summary>Gets the value of the selected menu item.</summary>
		/// <returns>The value of the selected menu item. The default is <see cref="F:System.String.Empty" />, which indicates that no menu item is currently selected.</returns>
		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x0006BD3D File Offset: 0x00069F3D
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SelectedValue
		{
			get
			{
				if (this.selectedItem == null)
				{
					return "";
				}
				return this.selectedItem.Value;
			}
		}

		/// <summary>Gets or sets the alternate text for a hidden image read by screen readers to provide the ability to skip the list of links.</summary>
		/// <returns>The alternate text of a hidden image read by screen readers to provide the ability to skip the list of links. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x0006BD58 File Offset: 0x00069F58
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x0006BD85 File Offset: 0x00069F85
		[Localizable(true)]
		public string SkipLinkText
		{
			get
			{
				object obj = this.ViewState["SkipLinkText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "Skip Navigation Links";
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0006BD98 File Offset: 0x00069F98
		private IMenuRenderer CreateRenderer(IMenuRenderer current)
		{
			Type type = null;
			switch (this.RenderingMode)
			{
			case MenuRenderingMode.Default:
				if (base.RenderingCompatibilityLessThan40)
				{
					type = typeof(MenuTableRenderer);
				}
				else
				{
					type = typeof(MenuListRenderer);
				}
				break;
			case MenuRenderingMode.Table:
				type = typeof(MenuTableRenderer);
				break;
			case MenuRenderingMode.List:
				type = typeof(MenuListRenderer);
				break;
			}
			if (type == null)
			{
				return null;
			}
			if (current == null || current.GetType() != type)
			{
				return Activator.CreateInstance(type, new object[] { this }) as IMenuRenderer;
			}
			return current;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x0006BE2F File Offset: 0x0006A02F
		internal void SetSelectedItem(MenuItem item)
		{
			if (this.selectedItem == item)
			{
				return;
			}
			this.selectedItem = item;
			this.selectedItemPath = item.Path;
		}

		/// <summary>Retrieves the menu item at the specified value path.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the menu item at the specified value path.</returns>
		/// <param name="valuePath">The value path to the menu item to retrieve.</param>
		// Token: 0x0600296C RID: 10604 RVA: 0x0006BE50 File Offset: 0x0006A050
		public MenuItem FindItem(string valuePath)
		{
			if (valuePath == null)
			{
				throw new ArgumentNullException("valuePath");
			}
			string[] array = valuePath.Split(new char[] { this.PathSeparator });
			int num = 0;
			MenuItemCollection childItems = this.Items;
			bool flag = true;
			while (childItems.Count > 0 && flag)
			{
				flag = false;
				foreach (object obj in childItems)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (menuItem.Value == array[num])
					{
						if (++num == array.Length)
						{
							return menuItem;
						}
						childItems = menuItem.ChildItems;
						flag = true;
						break;
					}
				}
			}
			return null;
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x0006BF18 File Offset: 0x0006A118
		private string GetBindingKey(string dataMember, int depth)
		{
			return dataMember + " " + depth;
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0006BF2C File Offset: 0x0006A12C
		internal MenuItemBinding FindBindingForItem(string type, int depth)
		{
			if (this.bindings == null)
			{
				return null;
			}
			MenuItemBinding menuItemBinding = (MenuItemBinding)this.bindings[this.GetBindingKey(type, depth)];
			if (menuItemBinding != null)
			{
				return menuItemBinding;
			}
			menuItemBinding = (MenuItemBinding)this.bindings[this.GetBindingKey(type, -1)];
			if (menuItemBinding != null)
			{
				return menuItemBinding;
			}
			menuItemBinding = (MenuItemBinding)this.bindings[this.GetBindingKey("", depth)];
			if (menuItemBinding != null)
			{
				return menuItemBinding;
			}
			return (MenuItemBinding)this.bindings[this.GetBindingKey("", -1)];
		}

		/// <summary>Binds the items from the data source to the menu items in the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		// Token: 0x0600296F RID: 10607 RVA: 0x0006BFC0 File Offset: 0x0006A1C0
		protected internal override void PerformDataBinding()
		{
			base.PerformDataBinding();
			if (!base.IsBoundUsingDataSourceID && this.DataSource == null)
			{
				this.EnsureChildControlsDataBound();
				return;
			}
			this.InitializeDataBindings();
			HierarchicalDataSourceView data = this.GetData("");
			if (data == null)
			{
				throw new InvalidOperationException("No view returned by data source control.");
			}
			this.Items.Clear();
			IHierarchicalEnumerable hierarchicalEnumerable = data.Select();
			this.FillBoundChildrenRecursive(hierarchicalEnumerable, this.Items);
			this.CreateChildControlsForItems();
			base.ChildControlsCreated = true;
			this.EnsureChildControlsDataBound();
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0006C03C File Offset: 0x0006A23C
		private void FillBoundChildrenRecursive(IHierarchicalEnumerable hEnumerable, MenuItemCollection itemCollection)
		{
			if (hEnumerable == null)
			{
				return;
			}
			foreach (object obj in hEnumerable)
			{
				IHierarchyData hierarchyData = hEnumerable.GetHierarchyData(obj);
				MenuItem menuItem = new MenuItem();
				itemCollection.Add(menuItem);
				menuItem.Bind(hierarchyData);
				SiteMapNode siteMapNode = hierarchyData as SiteMapNode;
				if (siteMapNode != null)
				{
					if (this._currSiteMapNode == null)
					{
						this._currSiteMapNode = siteMapNode.Provider.CurrentNode;
					}
					if (siteMapNode == this._currSiteMapNode)
					{
						menuItem.Selected = true;
					}
				}
				this.OnMenuItemDataBound(new MenuEventArgs(menuItem));
				if (hierarchyData != null && hierarchyData.HasChildren)
				{
					IHierarchicalEnumerable children = hierarchyData.GetChildren();
					this.FillBoundChildrenRecursive(children, menuItem.ChildItems);
				}
			}
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.MenuItem.DataBound" /> property of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object with the specified value.</summary>
		/// <param name="node">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to set.</param>
		/// <param name="dataBound">true to set the node as data-bound; otherwise, false.</param>
		// Token: 0x06002971 RID: 10609 RVA: 0x0006C114 File Offset: 0x0006A314
		protected void SetItemDataBound(MenuItem node, bool dataBound)
		{
			node.SetDataBound(dataBound);
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.MenuItem.DataPath" /> property of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object with the specified value.</summary>
		/// <param name="node">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to set.</param>
		/// <param name="dataPath">The data path for the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</param>
		// Token: 0x06002972 RID: 10610 RVA: 0x0006C11D File Offset: 0x0006A31D
		protected void SetItemDataPath(MenuItem node, string dataPath)
		{
			node.SetDataPath(dataPath);
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.MenuItem.DataItem" /> property of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object with the specified value.</summary>
		/// <param name="node">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to set.</param>
		/// <param name="dataItem">The data item for the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</param>
		// Token: 0x06002973 RID: 10611 RVA: 0x0006C126 File Offset: 0x0006A326
		protected void SetItemDataItem(MenuItem node, object dataItem)
		{
			node.SetDataItem(dataItem);
		}

		/// <summary>Processes an event raised when a form is posted to the server.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that represents the event argument passed to the event handler.</param>
		// Token: 0x06002974 RID: 10612 RVA: 0x0006C130 File Offset: 0x0006A330
		protected internal virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (!base.IsEnabled)
			{
				return;
			}
			this.EnsureChildControls();
			MenuItem menuItem = this.FindItemByPos(eventArgument);
			if (menuItem == null)
			{
				return;
			}
			menuItem.Selected = true;
			this.OnMenuItemClick(new MenuEventArgs(menuItem));
		}

		/// <summary>Processes an event raised when a form is posted to the server.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that represents the event argument passed to the event handler.</param>
		// Token: 0x06002975 RID: 10613 RVA: 0x0006C178 File Offset: 0x0006A378
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x0006C184 File Offset: 0x0006A384
		private MenuItem FindItemByPos(string path)
		{
			string[] array = path.Split(new char[] { '_' });
			MenuItem menuItem = null;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int num = int.Parse(array2[i]);
				if (menuItem == null)
				{
					if (num >= this.Items.Count)
					{
						return null;
					}
					menuItem = this.Items[num];
				}
				else
				{
					if (num >= menuItem.ChildItems.Count)
					{
						return null;
					}
					menuItem = menuItem.ChildItems[num];
				}
			}
			return menuItem;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This property is used primarily by control developers.</summary>
		/// <returns>Always returns HtmlTextWriterTag.Table.</returns>
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x0006C1FC File Offset: 0x0006A3FC
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.Tag;
			}
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.Menu" /> control so they can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x06002978 RID: 10616 RVA: 0x0006C20C File Offset: 0x0006A40C
		protected override void TrackViewState()
		{
			this.EnsureDataBound();
			base.TrackViewState();
			if (this.dataBindings != null)
			{
				((IStateManager)this.dataBindings).TrackViewState();
			}
			if (this.items != null)
			{
				((IStateManager)this.items).TrackViewState();
			}
			if (this.dynamicMenuItemStyle != null)
			{
				this.dynamicMenuItemStyle.TrackViewState();
			}
			if (this.dynamicMenuStyle != null)
			{
				this.dynamicMenuStyle.TrackViewState();
			}
			if (this.levelMenuItemStyles != null && this.levelMenuItemStyles.Count > 0)
			{
				((IStateManager)this.levelMenuItemStyles).TrackViewState();
			}
			if (this.levelSelectedStyles != null && this.levelMenuItemStyles.Count > 0)
			{
				((IStateManager)this.levelSelectedStyles).TrackViewState();
			}
			if (this.levelSubMenuStyles != null && this.levelSubMenuStyles.Count > 0)
			{
				((IStateManager)this.levelSubMenuStyles).TrackViewState();
			}
			if (this.dynamicSelectedStyle != null)
			{
				this.dynamicSelectedStyle.TrackViewState();
			}
			if (this.staticMenuItemStyle != null)
			{
				this.staticMenuItemStyle.TrackViewState();
			}
			if (this.staticMenuStyle != null)
			{
				this.staticMenuStyle.TrackViewState();
			}
			if (this.staticSelectedStyle != null)
			{
				this.staticSelectedStyle.TrackViewState();
			}
			if (this.staticHoverStyle != null)
			{
				this.staticHoverStyle.TrackViewState();
			}
			if (this.dynamicHoverStyle != null)
			{
				this.dynamicHoverStyle.TrackViewState();
			}
		}

		/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
		// Token: 0x06002979 RID: 10617 RVA: 0x0006C348 File Offset: 0x0006A548
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this.dataBindings == null) ? null : ((IStateManager)this.dataBindings).SaveViewState(),
				(this.items == null) ? null : ((IStateManager)this.items).SaveViewState(),
				(this.dynamicMenuItemStyle == null) ? null : this.dynamicMenuItemStyle.SaveViewState(),
				(this.dynamicMenuStyle == null) ? null : this.dynamicMenuStyle.SaveViewState(),
				(this.levelMenuItemStyles == null) ? null : ((IStateManager)this.levelMenuItemStyles).SaveViewState(),
				(this.levelSelectedStyles == null) ? null : ((IStateManager)this.levelSelectedStyles).SaveViewState(),
				(this.dynamicSelectedStyle == null) ? null : this.dynamicSelectedStyle.SaveViewState(),
				(this.staticMenuItemStyle == null) ? null : this.staticMenuItemStyle.SaveViewState(),
				(this.staticMenuStyle == null) ? null : this.staticMenuStyle.SaveViewState(),
				(this.staticSelectedStyle == null) ? null : this.staticSelectedStyle.SaveViewState(),
				(this.staticHoverStyle == null) ? null : this.staticHoverStyle.SaveViewState(),
				(this.dynamicHoverStyle == null) ? null : this.dynamicHoverStyle.SaveViewState(),
				(this.levelSubMenuStyles == null) ? null : ((IStateManager)this.levelSubMenuStyles).SaveViewState()
			};
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view-state values for the control.</param>
		// Token: 0x0600297A RID: 10618 RVA: 0x0006C4C8 File Offset: 0x0006A6C8
		protected override void LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.DataBindings).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.Items).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.DynamicMenuItemStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.DynamicMenuStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.LevelMenuItemStyles).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.LevelSelectedStyles).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.DynamicSelectedStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				this.StaticMenuItemStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				this.StaticMenuStyle.LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				this.StaticSelectedStyle.LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				this.StaticHoverStyle.LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				this.DynamicHoverStyle.LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)this.LevelSubMenuStyles).LoadViewState(array[13]);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> that contains the event data.</param>
		// Token: 0x0600297B RID: 10619 RVA: 0x0005FD54 File Offset: 0x0005DF54
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.RegisterRequiresControlState(this);
			base.OnInit(e);
		}

		/// <summary>Loads the state of the properties in the <see cref="T:System.Web.UI.WebControls.Menu" /> control that need to be persisted.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
		// Token: 0x0600297C RID: 10620 RVA: 0x0006C5EC File Offset: 0x0006A7EC
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			this.selectedItemPath = array[1] as string;
		}

		/// <summary>Saves the state of the properties in the <see cref="T:System.Web.UI.WebControls.Menu" /> control that need to be persisted.</summary>
		/// <returns>An object that contains the state data for the control. If there have been no changes to the state, this method returns null.</returns>
		// Token: 0x0600297D RID: 10621 RVA: 0x0006C61C File Offset: 0x0006A81C
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			object obj2 = this.selectedItemPath;
			if (obj != null || obj2 != null)
			{
				return new object[] { obj, obj2 };
			}
			return null;
		}

		/// <summary>Creates the child controls of a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		// Token: 0x0600297E RID: 10622 RVA: 0x0006C64D File Offset: 0x0006A84D
		protected internal override void CreateChildControls()
		{
			if (!base.IsBoundUsingDataSourceID && this.DataSource == null)
			{
				this.CreateChildControlsForItems();
				return;
			}
			this.EnsureDataBound();
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x0006C66C File Offset: 0x0006A86C
		private void CreateChildControlsForItems()
		{
			this.Controls.Clear();
			if (base.HasChildViewState)
			{
				base.ClearChildViewState();
			}
			this._menuItemControls = new Hashtable();
			this.CreateChildControlsForItems(this.Items);
			this._requiresChildControlsDataBinding = true;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
		private void CreateChildControlsForItems(MenuItemCollection items)
		{
			IMenuRenderer menuRenderer = this.Renderer;
			foreach (object obj in items)
			{
				MenuItem menuItem = (MenuItem)obj;
				bool flag = menuRenderer.IsDynamicItem(this, menuItem);
				if (flag && this.dynamicItemTemplate != null)
				{
					MenuItemTemplateContainer menuItemTemplateContainer = new MenuItemTemplateContainer(menuItem.Index, menuItem);
					this.dynamicItemTemplate.InstantiateIn(menuItemTemplateContainer);
					this._menuItemControls[menuItem] = menuItemTemplateContainer;
					this.Controls.Add(menuItemTemplateContainer);
				}
				else if (!flag && this.staticItemTemplate != null)
				{
					MenuItemTemplateContainer menuItemTemplateContainer2 = new MenuItemTemplateContainer(menuItem.Index, menuItem);
					this.staticItemTemplate.InstantiateIn(menuItemTemplateContainer2);
					this._menuItemControls[menuItem] = menuItemTemplateContainer2;
					this.Controls.Add(menuItemTemplateContainer2);
				}
				if (menuItem.HasChildData)
				{
					this.CreateChildControlsForItems(menuItem.ChildItems);
				}
			}
		}

		/// <summary>Verifies that the menu control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.WebControls.Menu.DataBind" /> method.</summary>
		// Token: 0x06002981 RID: 10625 RVA: 0x0006C7A8 File Offset: 0x0006A9A8
		protected override void EnsureDataBound()
		{
			base.EnsureDataBound();
			this.EnsureChildControlsDataBound();
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0006C7B6 File Offset: 0x0006A9B6
		private void EnsureChildControlsDataBound()
		{
			if (!this._requiresChildControlsDataBinding)
			{
				return;
			}
			this.DataBindChildren();
			this._requiresChildControlsDataBinding = false;
		}

		/// <summary>Retrieves the design-time state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the design-time state of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</returns>
		// Token: 0x06002983 RID: 10627 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets design-time data for the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> that contains state data for displaying the control.</param>
		// Token: 0x06002984 RID: 10628 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls of the <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls</returns>
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x00032AC7 File Offset: 0x00030CC7
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Binds the data source to the <see cref="T:System.Web.UI.WebControls.Menu" /> control. This method cannot be inherited.</summary>
		// Token: 0x06002986 RID: 10630 RVA: 0x0006C7CE File Offset: 0x0006A9CE
		public sealed override void DataBind()
		{
			base.DataBind();
		}

		/// <summary>Determines whether the event for the <see cref="T:System.Web.UI.WebControls.Menu" /> control is passed up the page's user interface (UI) server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06002987 RID: 10631 RVA: 0x0006C7D8 File Offset: 0x0006A9D8
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (!(e is CommandEventArgs))
			{
				return false;
			}
			MenuEventArgs menuEventArgs = e as MenuEventArgs;
			if (menuEventArgs != null && string.Equals(menuEventArgs.CommandName, Menu.MenuItemClickCommandName))
			{
				this.OnMenuItemClick(menuEventArgs);
			}
			return true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> that contains the event data.</param>
		// Token: 0x06002988 RID: 10632 RVA: 0x0006C813 File Offset: 0x0006AA13
		protected override void OnDataBinding(EventArgs e)
		{
			this.EnsureChildControls();
			base.OnDataBinding(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002989 RID: 10633 RVA: 0x0006C824 File Offset: 0x0006AA24
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			string text = this.ClientID + "_data";
			StringBuilder stringBuilder = new StringBuilder();
			Page page = this.Page;
			HtmlHead htmlHead;
			ClientScriptManager clientScriptManager;
			if (page != null)
			{
				htmlHead = page.Header;
				clientScriptManager = page.ClientScript;
			}
			else
			{
				htmlHead = null;
				clientScriptManager = null;
			}
			this.Renderer.PreRender(page, htmlHead, clientScriptManager, text, stringBuilder);
			if (clientScriptManager != null)
			{
				clientScriptManager.RegisterWebFormClientScript();
				clientScriptManager.RegisterStartupScript(typeof(Menu), this.ClientID, stringBuilder.ToString(), true);
			}
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x0006C8AC File Offset: 0x0006AAAC
		private void InitializeDataBindings()
		{
			if (this.dataBindings != null && this.dataBindings.Count > 0)
			{
				this.bindings = new Hashtable();
				using (IEnumerator enumerator = this.dataBindings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						MenuItemBinding menuItemBinding = (MenuItemBinding)obj;
						string bindingKey = this.GetBindingKey(menuItemBinding.DataMember, menuItemBinding.Depth);
						this.bindings[bindingKey] = menuItemBinding;
					}
					return;
				}
			}
			this.bindings = null;
		}

		/// <summary>Renders the menu control on the client browser.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page.</param>
		// Token: 0x0600298B RID: 10635 RVA: 0x0006C948 File Offset: 0x0006AB48
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Items.Count > 0)
			{
				base.Render(writer);
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The output stream that renders HTML contents to the client.</param>
		// Token: 0x0600298C RID: 10636 RVA: 0x0006C95F File Offset: 0x0006AB5F
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
			base.AddAttributesToRender(writer);
		}

		/// <summary>Adds tag attributes and writes the markup for the opening tag of the control to the output stream emitted to the browser or device.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output.</param>
		// Token: 0x0600298D RID: 10637 RVA: 0x0006C974 File Offset: 0x0006AB74
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string skipLinkText = this.SkipLinkText;
			if (!string.IsNullOrEmpty(skipLinkText))
			{
				this.Renderer.RenderBeginTag(writer, skipLinkText);
			}
			base.RenderBeginTag(writer);
		}

		/// <summary>Performs final markup and writes the HTML closing tag of the control to the output stream emitted to the browser or device.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output.</param>
		// Token: 0x0600298E RID: 10638 RVA: 0x0006C9A4 File Offset: 0x0006ABA4
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			this.Renderer.RenderEndTag(writer);
			if (!string.IsNullOrEmpty(this.SkipLinkText))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_SkipLink");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderEndTag();
			}
		}

		/// <summary>This member overrides <see cref="M:System.Web.UI.WebControls.WebControl.RenderContents(System.Web.UI.HtmlTextWriter)" />.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output.</param>
		// Token: 0x0600298F RID: 10639 RVA: 0x0006C9F6 File Offset: 0x0006ABF6
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x0006CA04 File Offset: 0x0006AC04
		internal void RenderDynamicMenu(HtmlTextWriter writer, MenuItemCollection items)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (this.DisplayChildren(items[i]))
				{
					this.RenderDynamicMenu(writer, items[i]);
					this.RenderDynamicMenu(writer, items[i].ChildItems);
				}
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0006CA54 File Offset: 0x0006AC54
		private Menu.MenuRenderHtmlTemplate GetDynamicMenuTemplate(MenuItem item)
		{
			if (this._dynamicTemplate != null)
			{
				return this._dynamicTemplate;
			}
			this._dynamicTemplate = new Menu.MenuRenderHtmlTemplate();
			HtmlTextWriter menuTemplateWriter = this._dynamicTemplate.GetMenuTemplateWriter();
			if (this.Page.Header != null)
			{
				menuTemplateWriter.AddAttribute(HtmlTextWriterAttribute.Class, Menu.MenuRenderHtmlTemplate.GetMarker(0));
			}
			else
			{
				menuTemplateWriter.AddAttribute(HtmlTextWriterAttribute.Style, Menu.MenuRenderHtmlTemplate.GetMarker(0));
			}
			menuTemplateWriter.AddStyleAttribute("visibility", "hidden");
			menuTemplateWriter.AddStyleAttribute("position", "absolute");
			menuTemplateWriter.AddStyleAttribute("z-index", "1");
			menuTemplateWriter.AddStyleAttribute("left", "0px");
			menuTemplateWriter.AddStyleAttribute("top", "0px");
			menuTemplateWriter.AddAttribute("id", Menu.MenuRenderHtmlTemplate.GetMarker(1));
			menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
			menuTemplateWriter.AddAttribute("id", Menu.MenuRenderHtmlTemplate.GetMarker(2));
			menuTemplateWriter.AddStyleAttribute("display", "block");
			menuTemplateWriter.AddStyleAttribute("text-align", "center");
			menuTemplateWriter.AddAttribute("onmouseover", string.Concat(new string[]
			{
				"Menu_OverScrollBtn ('",
				this.ClientID,
				"','",
				Menu.MenuRenderHtmlTemplate.GetMarker(3),
				"','u')"
			}));
			menuTemplateWriter.AddAttribute("onmouseout", string.Concat(new string[]
			{
				"Menu_OutScrollBtn ('",
				this.ClientID,
				"','",
				Menu.MenuRenderHtmlTemplate.GetMarker(4),
				"','u')"
			}));
			menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
			menuTemplateWriter.AddAttribute("src", Menu.MenuRenderHtmlTemplate.GetMarker(5));
			menuTemplateWriter.AddAttribute("alt", Menu.MenuRenderHtmlTemplate.GetMarker(6));
			menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Img);
			menuTemplateWriter.RenderEndTag();
			menuTemplateWriter.RenderEndTag();
			menuTemplateWriter.AddAttribute("id", Menu.MenuRenderHtmlTemplate.GetMarker(7));
			menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
			menuTemplateWriter.AddAttribute("id", Menu.MenuRenderHtmlTemplate.GetMarker(8));
			menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
			menuTemplateWriter.Write(Menu.MenuRenderHtmlTemplate.GetMarker(9));
			menuTemplateWriter.RenderEndTag();
			menuTemplateWriter.RenderEndTag();
			menuTemplateWriter.AddAttribute("id", Menu.MenuRenderHtmlTemplate.GetMarker(0));
			menuTemplateWriter.AddStyleAttribute("display", "block");
			menuTemplateWriter.AddStyleAttribute("text-align", "center");
			menuTemplateWriter.AddAttribute("onmouseover", string.Concat(new string[]
			{
				"Menu_OverScrollBtn ('",
				this.ClientID,
				"','",
				Menu.MenuRenderHtmlTemplate.GetMarker(1),
				"','d')"
			}));
			menuTemplateWriter.AddAttribute("onmouseout", string.Concat(new string[]
			{
				"Menu_OutScrollBtn ('",
				this.ClientID,
				"','",
				Menu.MenuRenderHtmlTemplate.GetMarker(2),
				"','d')"
			}));
			menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Div);
			menuTemplateWriter.AddAttribute("src", Menu.MenuRenderHtmlTemplate.GetMarker(3));
			menuTemplateWriter.AddAttribute("alt", Menu.MenuRenderHtmlTemplate.GetMarker(4));
			menuTemplateWriter.RenderBeginTag(HtmlTextWriterTag.Img);
			menuTemplateWriter.RenderEndTag();
			menuTemplateWriter.RenderEndTag();
			menuTemplateWriter.RenderEndTag();
			this._dynamicTemplate.Parse();
			return this._dynamicTemplate;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0006CD5C File Offset: 0x0006AF5C
		private void RenderDynamicMenu(HtmlTextWriter writer, MenuItem item)
		{
			this._dynamicTemplate = this.GetDynamicMenuTemplate(item);
			string text = this.ClientID + "_" + item.Path;
			string[] array = new string[]
			{
				this.GetCssMenuStyle(true, item.Depth + 1),
				text + "s",
				text + "cu",
				item.Path,
				item.Path,
				(this.ScrollUpImageUrl != "") ? this.ScrollUpImageUrl : this.Page.ClientScript.GetWebResourceUrl(typeof(Menu), "arrow_up.gif"),
				this.ScrollUpText,
				text + "cb",
				text + "cc"
			};
			this._dynamicTemplate.RenderTemplate(writer, array, 0, array.Length);
			this.RenderMenu(writer, item.ChildItems, true, true, item.Depth + 1, false);
			string[] array2 = new string[]
			{
				text + "cd",
				item.Path,
				item.Path,
				(this.ScrollDownImageUrl != "") ? this.ScrollDownImageUrl : this.Page.ClientScript.GetWebResourceUrl(typeof(Menu), "arrow_down.gif"),
				this.ScrollDownText
			};
			this._dynamicTemplate.RenderTemplate(writer, array2, array.Length + 1, array2.Length);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0006CEE0 File Offset: 0x0006B0E0
		private string GetCssMenuStyle(bool dynamic, int menuLevel)
		{
			if (this.Page.Header != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (!dynamic && this.staticMenuStyle != null)
				{
					stringBuilder.Append(this.staticMenuStyle.CssClass);
					stringBuilder.Append(' ');
					stringBuilder.Append(this.staticMenuStyle.RegisteredCssClass);
				}
				if (dynamic && this.dynamicMenuStyle != null)
				{
					stringBuilder.Append(this.PopOutBoxStyle.RegisteredCssClass);
					stringBuilder.Append(' ');
					stringBuilder.Append(this.dynamicMenuStyle.CssClass);
					stringBuilder.Append(' ');
					stringBuilder.Append(this.dynamicMenuStyle.RegisteredCssClass);
				}
				if (this.levelSubMenuStyles != null && this.levelSubMenuStyles.Count > menuLevel)
				{
					stringBuilder.Append(this.levelSubMenuStyles[menuLevel].CssClass);
					stringBuilder.Append(' ');
					stringBuilder.Append(this.levelSubMenuStyles[menuLevel].RegisteredCssClass);
				}
				return stringBuilder.ToString();
			}
			SubMenuStyle subMenuStyle = new SubMenuStyle();
			if (!dynamic && this.staticMenuStyle != null)
			{
				subMenuStyle.CopyFrom(this.staticMenuStyle);
			}
			if (dynamic && this.dynamicMenuStyle != null)
			{
				subMenuStyle.CopyFrom(this.PopOutBoxStyle);
				subMenuStyle.CopyFrom(this.dynamicMenuStyle);
			}
			if (this.levelSubMenuStyles != null && this.levelSubMenuStyles.Count > menuLevel)
			{
				subMenuStyle.CopyFrom(this.levelSubMenuStyles[menuLevel]);
			}
			return subMenuStyle.GetStyleAttributes(null).Value;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0006D058 File Offset: 0x0006B258
		internal void RenderMenu(HtmlTextWriter writer, MenuItemCollection items, bool vertical, bool dynamic, int menuLevel, bool notLast)
		{
			IMenuRenderer menuRenderer = this.Renderer;
			menuRenderer.RenderMenuBeginTag(writer, dynamic, menuLevel);
			menuRenderer.RenderMenuBody(writer, items, vertical, dynamic, notLast);
			menuRenderer.RenderMenuEndTag(writer, dynamic, menuLevel);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0006D082 File Offset: 0x0006B282
		internal bool DisplayChildren(MenuItem item)
		{
			return item.Depth + 1 < this.StaticDisplayLevels + this.MaximumDynamicDisplayLevels && item.ChildItems.Count > 0;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0006D0AC File Offset: 0x0006B2AC
		internal void RenderItem(HtmlTextWriter writer, MenuItem item, int position)
		{
			bool flag = false;
			MenuItem menuItem = item;
			MenuItem parent;
			while ((parent = menuItem.Parent) != null)
			{
				if (menuItem.Index != parent.ChildItems.Count - 1)
				{
					flag = true;
					break;
				}
				menuItem = parent;
			}
			this.Renderer.RenderMenuItem(writer, item, flag, position == 0);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0006D0F6 File Offset: 0x0006B2F6
		internal void RenderItemContent(HtmlTextWriter writer, MenuItem item, bool isDynamicItem)
		{
			if (this._menuItemControls != null && this._menuItemControls[item] != null)
			{
				((Control)this._menuItemControls[item]).Render(writer);
			}
			this.Renderer.RenderItemContent(writer, item, isDynamicItem);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0006D134 File Offset: 0x0006B334
		internal Unit GetItemSpacing(MenuItem item, bool dynamic)
		{
			Unit unit = Unit.Empty;
			if (item.Selected)
			{
				if (this.levelSelectedStyles != null && item.Depth < this.levelSelectedStyles.Count)
				{
					unit = this.levelSelectedStyles[item.Depth].ItemSpacing;
					if (unit != Unit.Empty)
					{
						return unit;
					}
				}
				if (dynamic && this.dynamicSelectedStyle != null)
				{
					unit = this.dynamicSelectedStyle.ItemSpacing;
				}
				else if (!dynamic && this.staticSelectedStyle != null)
				{
					unit = this.staticSelectedStyle.ItemSpacing;
				}
				if (unit != Unit.Empty)
				{
					return unit;
				}
			}
			if (this.levelMenuItemStyles != null && item.Depth < this.levelMenuItemStyles.Count)
			{
				unit = this.levelMenuItemStyles[item.Depth].ItemSpacing;
				if (unit != Unit.Empty)
				{
					return unit;
				}
			}
			if (dynamic && this.dynamicMenuItemStyle != null)
			{
				return this.dynamicMenuItemStyle.ItemSpacing;
			}
			if (!dynamic && this.staticMenuItemStyle != null)
			{
				return this.staticMenuItemStyle.ItemSpacing;
			}
			return Unit.Empty;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x0006D255 File Offset: 0x0006B455
		// Note: this type is marked as 'beforefieldinit'.
		static Menu()
		{
			Menu.MenuItemClickEvent = new object();
			Menu.MenuItemDataBoundEvent = new object();
			Menu.MenuItemClickCommandName = "Click";
		}

		// Token: 0x04001A97 RID: 6807
		private IMenuRenderer renderer;

		// Token: 0x04001A98 RID: 6808
		private MenuItemStyle dynamicMenuItemStyle;

		// Token: 0x04001A99 RID: 6809
		private SubMenuStyle dynamicMenuStyle;

		// Token: 0x04001A9A RID: 6810
		private MenuItemStyle dynamicSelectedStyle;

		// Token: 0x04001A9B RID: 6811
		private MenuItemStyle staticMenuItemStyle;

		// Token: 0x04001A9C RID: 6812
		private SubMenuStyle staticMenuStyle;

		// Token: 0x04001A9D RID: 6813
		private MenuItemStyle staticSelectedStyle;

		// Token: 0x04001A9E RID: 6814
		private Style staticHoverStyle;

		// Token: 0x04001A9F RID: 6815
		private Style dynamicHoverStyle;

		// Token: 0x04001AA0 RID: 6816
		private MenuItemStyleCollection levelMenuItemStyles;

		// Token: 0x04001AA1 RID: 6817
		private MenuItemStyleCollection levelSelectedStyles;

		// Token: 0x04001AA2 RID: 6818
		private SubMenuStyleCollection levelSubMenuStyles;

		// Token: 0x04001AA3 RID: 6819
		private ITemplate staticItemTemplate;

		// Token: 0x04001AA4 RID: 6820
		private ITemplate dynamicItemTemplate;

		// Token: 0x04001AA5 RID: 6821
		private MenuItemCollection items;

		// Token: 0x04001AA6 RID: 6822
		private MenuItemBindingCollection dataBindings;

		// Token: 0x04001AA7 RID: 6823
		private MenuItem selectedItem;

		// Token: 0x04001AA8 RID: 6824
		private string selectedItemPath;

		// Token: 0x04001AA9 RID: 6825
		private Hashtable bindings;

		// Token: 0x04001AAA RID: 6826
		private Hashtable _menuItemControls;

		// Token: 0x04001AAB RID: 6827
		private bool _requiresChildControlsDataBinding;

		// Token: 0x04001AAC RID: 6828
		private SiteMapNode _currSiteMapNode;

		// Token: 0x04001AAD RID: 6829
		private Style popOutBoxStyle;

		// Token: 0x04001AAE RID: 6830
		private Style controlLinkStyle;

		// Token: 0x04001AAF RID: 6831
		private Style dynamicMenuItemLinkStyle;

		// Token: 0x04001AB0 RID: 6832
		private Style staticMenuItemLinkStyle;

		// Token: 0x04001AB1 RID: 6833
		private Style dynamicSelectedLinkStyle;

		// Token: 0x04001AB2 RID: 6834
		private Style staticSelectedLinkStyle;

		// Token: 0x04001AB3 RID: 6835
		private Style dynamicHoverLinkStyle;

		// Token: 0x04001AB4 RID: 6836
		private Style staticHoverLinkStyle;

		// Token: 0x04001AB5 RID: 6837
		private bool? renderList;

		// Token: 0x04001AB6 RID: 6838
		private bool includeStyleBlock = true;

		// Token: 0x04001AB7 RID: 6839
		private MenuRenderingMode renderingMode;

		/// <summary>Contains the command name.</summary>
		// Token: 0x04001ABA RID: 6842
		public static readonly string MenuItemClickCommandName;

		// Token: 0x04001ABB RID: 6843
		private Menu.MenuRenderHtmlTemplate _dynamicTemplate;

		// Token: 0x020003CF RID: 975
		private class MenuTemplateWriter : TextWriter
		{
			// Token: 0x0600299B RID: 10651 RVA: 0x0006D275 File Offset: 0x0006B475
			public MenuTemplateWriter(char[] buffer)
			{
				this._buffer = buffer;
			}

			// Token: 0x17000D4E RID: 3406
			// (get) Token: 0x0600299C RID: 10652 RVA: 0x0006D284 File Offset: 0x0006B484
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x0600299D RID: 10653 RVA: 0x0006D28C File Offset: 0x0006B48C
			public override void Write(char value)
			{
				if (this._ptr == this._buffer.Length)
				{
					this.EnsureCapacity();
				}
				char[] buffer = this._buffer;
				int ptr = this._ptr;
				this._ptr = ptr + 1;
				buffer[ptr] = value;
			}

			// Token: 0x0600299E RID: 10654 RVA: 0x0006D2C8 File Offset: 0x0006B4C8
			public override void Write(string value)
			{
				if (value == null)
				{
					return;
				}
				if (this._ptr + value.Length >= this._buffer.Length)
				{
					this.EnsureCapacity();
				}
				for (int i = 0; i < value.Length; i++)
				{
					char[] buffer = this._buffer;
					int ptr = this._ptr;
					this._ptr = ptr + 1;
					buffer[ptr] = value[i];
				}
			}

			// Token: 0x0600299F RID: 10655 RVA: 0x0006D328 File Offset: 0x0006B528
			private void EnsureCapacity()
			{
				char[] array = new char[this._buffer.Length * 2];
				Array.Copy(this._buffer, array, this._buffer.Length);
				this._buffer = array;
			}

			// Token: 0x04001ABC RID: 6844
			private char[] _buffer;

			// Token: 0x04001ABD RID: 6845
			private int _ptr;
		}

		// Token: 0x020003D0 RID: 976
		private class MenuRenderHtmlTemplate
		{
			// Token: 0x060029A0 RID: 10656 RVA: 0x0006D360 File Offset: 0x0006B560
			public MenuRenderHtmlTemplate()
			{
				this._templateHtml = new char[1024];
				this._templateWriter = new Menu.MenuTemplateWriter(this._templateHtml);
			}

			// Token: 0x060029A1 RID: 10657 RVA: 0x0006D398 File Offset: 0x0006B598
			public static string GetMarker(int num)
			{
				char c = (char)(2417 + num);
				return "\u093a\u093bॱ" + c;
			}

			// Token: 0x060029A2 RID: 10658 RVA: 0x0006D3BE File Offset: 0x0006B5BE
			public HtmlTextWriter GetMenuTemplateWriter()
			{
				return new HtmlTextWriter(this._templateWriter);
			}

			// Token: 0x060029A3 RID: 10659 RVA: 0x0006D3CC File Offset: 0x0006B5CC
			public void Parse()
			{
				int num = 0;
				for (int i = 0; i < this._templateHtml.Length; i++)
				{
					if (this._templateHtml[i] == '\0')
					{
						this.idxs.Add(i);
						return;
					}
					if (this._templateHtml[i] != "\u093a\u093bॱ"[num])
					{
						num = 0;
					}
					else
					{
						num++;
						if (num == "\u093a\u093bॱ".Length)
						{
							num = 0;
							this.idxs.Add(i - "\u093a\u093bॱ".Length + 1);
						}
					}
				}
			}

			// Token: 0x060029A4 RID: 10660 RVA: 0x0006D458 File Offset: 0x0006B658
			public void RenderTemplate(HtmlTextWriter writer, string[] dynamicParts, int start, int count)
			{
				if (this.idxs.Count == 0)
				{
					return;
				}
				int num = ((start == 0) ? (-"\u093a\u093bॱ".Length - 1) : ((int)this.idxs[start - 1]));
				int i = start;
				int num2 = start + count;
				int num3;
				while (i < num2)
				{
					num3 = num + "\u093a\u093bॱ".Length + 1;
					num = (int)this.idxs[i];
					writer.Write(this._templateHtml, num3, num - num3);
					int num4 = (int)(this._templateHtml[num + "\u093a\u093bॱ".Length] - 'ॱ');
					writer.Write(dynamicParts[num4]);
					i++;
				}
				num3 = num + "\u093a\u093bॱ".Length + 1;
				num = (int)this.idxs[i];
				writer.Write(this._templateHtml, num3, num - num3);
			}

			// Token: 0x04001ABE RID: 6846
			public const string Marker = "\u093a\u093bॱ";

			// Token: 0x04001ABF RID: 6847
			private char[] _templateHtml;

			// Token: 0x04001AC0 RID: 6848
			private Menu.MenuTemplateWriter _templateWriter;

			// Token: 0x04001AC1 RID: 6849
			private ArrayList idxs = new ArrayList(32);
		}
	}
}
