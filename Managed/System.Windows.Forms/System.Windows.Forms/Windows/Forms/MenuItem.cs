using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents an individual item that is displayed within a <see cref="T:System.Windows.Forms.MainMenu" /> or <see cref="T:System.Windows.Forms.ContextMenu" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000250 RID: 592
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultEvent("Click")]
	public class MenuItem : Menu
	{
		/// <summary>Initializes a <see cref="T:System.Windows.Forms.MenuItem" /> with a blank caption.</summary>
		// Token: 0x060026AD RID: 9901 RVA: 0x00093CA8 File Offset: 0x00091EA8
		public MenuItem()
			: base(null)
		{
			this.CommonConstructor(string.Empty);
			this.shortcut = Shortcut.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption for the menu item.</summary>
		/// <param name="text">The caption for the menu item. </param>
		// Token: 0x060026AE RID: 9902 RVA: 0x00093CC4 File Offset: 0x00091EC4
		public MenuItem(string text)
			: base(null)
		{
			this.CommonConstructor(text);
			this.shortcut = Shortcut.None;
		}

		/// <summary>Initializes a new instance of the class with a specified caption and event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event of the menu item.</summary>
		/// <param name="text">The caption for the menu item. </param>
		/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item. </param>
		// Token: 0x060026AF RID: 9903 RVA: 0x00093CDC File Offset: 0x00091EDC
		public MenuItem(string text, EventHandler onClick)
			: base(null)
		{
			this.CommonConstructor(text);
			this.shortcut = Shortcut.None;
			this.Click += onClick;
		}

		/// <summary>Initializes a new instance of the class with a specified caption and an array of submenu items defined for the menu item.</summary>
		/// <param name="text">The caption for the menu item. </param>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that contains the submenu items for this menu item. </param>
		// Token: 0x060026B0 RID: 9904 RVA: 0x00093CFC File Offset: 0x00091EFC
		public MenuItem(string text, MenuItem[] items)
			: base(items)
		{
			this.CommonConstructor(text);
			this.shortcut = Shortcut.None;
		}

		/// <summary>Initializes a new instance of the class with a specified caption, event handler, and associated shortcut key for the menu item.</summary>
		/// <param name="text">The caption for the menu item. </param>
		/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item. </param>
		/// <param name="shortcut">One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. </param>
		// Token: 0x060026B1 RID: 9905 RVA: 0x00093D14 File Offset: 0x00091F14
		public MenuItem(string text, EventHandler onClick, Shortcut shortcut)
			: base(null)
		{
			this.CommonConstructor(text);
			this.Click += onClick;
			this.shortcut = shortcut;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption; defined event-handlers for the <see cref="E:System.Windows.Forms.MenuItem.Click" />, <see cref="E:System.Windows.Forms.MenuItem.Select" /> and <see cref="E:System.Windows.Forms.MenuItem.Popup" /> events; a shortcut key; a merge type; and order specified for the menu item.</summary>
		/// <param name="mergeType">One of the <see cref="T:System.Windows.Forms.MenuMerge" /> values. </param>
		/// <param name="mergeOrder">The relative position that this menu item will take in a merged menu. </param>
		/// <param name="shortcut">One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. </param>
		/// <param name="text">The caption for the menu item. </param>
		/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item. </param>
		/// <param name="onPopup">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event for this menu item. </param>
		/// <param name="onSelect">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item. </param>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that contains the submenu items for this menu item. </param>
		// Token: 0x060026B2 RID: 9906 RVA: 0x00093D34 File Offset: 0x00091F34
		public MenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut, string text, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, MenuItem[] items)
			: base(items)
		{
			this.CommonConstructor(text);
			this.shortcut = shortcut;
			this.mergeorder = mergeOrder;
			this.mergetype = mergeType;
			this.Click += onClick;
			this.Popup += onPopup;
			this.Select += onSelect;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x00093D80 File Offset: 0x00091F80
		// Note: this type is marked as 'beforefieldinit'.
		static MenuItem()
		{
			MenuItem.ClickEvent = new object();
			MenuItem.DrawItemEvent = new object();
			MenuItem.MeasureItemEvent = new object();
			MenuItem.PopupEvent = new object();
			MenuItem.SelectEvent = new object();
			MenuItem.UIACheckedChangedEvent = new object();
			MenuItem.UIARadioCheckChangedEvent = new object();
			MenuItem.UIAEnabledChangedEvent = new object();
			MenuItem.UIATextChangedEvent = new object();
		}

		/// <summary>Occurs when the menu item is clicked or selected using a shortcut key or access key defined for the menu item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000240 RID: 576
		// (add) Token: 0x060026B4 RID: 9908 RVA: 0x00093DE8 File Offset: 0x00091FE8
		// (remove) Token: 0x060026B5 RID: 9909 RVA: 0x00093DFC File Offset: 0x00091FFC
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(MenuItem.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MenuItem.OwnerDraw" /> property of a menu item is set to true and a request is made to draw the menu item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000241 RID: 577
		// (add) Token: 0x060026B6 RID: 9910 RVA: 0x00093E10 File Offset: 0x00092010
		// (remove) Token: 0x060026B7 RID: 9911 RVA: 0x00093E24 File Offset: 0x00092024
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(MenuItem.DrawItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.DrawItemEvent, value);
			}
		}

		/// <summary>Occurs when the menu needs to know the size of a menu item before drawing it.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000242 RID: 578
		// (add) Token: 0x060026B8 RID: 9912 RVA: 0x00093E38 File Offset: 0x00092038
		// (remove) Token: 0x060026B9 RID: 9913 RVA: 0x00093E4C File Offset: 0x0009204C
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(MenuItem.MeasureItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.MeasureItemEvent, value);
			}
		}

		/// <summary>Occurs before a menu item's list of menu items is displayed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000243 RID: 579
		// (add) Token: 0x060026BA RID: 9914 RVA: 0x00093E60 File Offset: 0x00092060
		// (remove) Token: 0x060026BB RID: 9915 RVA: 0x00093E74 File Offset: 0x00092074
		public event EventHandler Popup
		{
			add
			{
				base.Events.AddHandler(MenuItem.PopupEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.PopupEvent, value);
			}
		}

		/// <summary>Occurs when the user places the pointer over a menu item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000244 RID: 580
		// (add) Token: 0x060026BC RID: 9916 RVA: 0x00093E88 File Offset: 0x00092088
		// (remove) Token: 0x060026BD RID: 9917 RVA: 0x00093E9C File Offset: 0x0009209C
		public event EventHandler Select
		{
			add
			{
				base.Events.AddHandler(MenuItem.SelectEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.SelectEvent, value);
			}
		}

		// Token: 0x14000245 RID: 581
		// (add) Token: 0x060026BE RID: 9918 RVA: 0x00093EB0 File Offset: 0x000920B0
		// (remove) Token: 0x060026BF RID: 9919 RVA: 0x00093EC4 File Offset: 0x000920C4
		internal event EventHandler UIACheckedChanged
		{
			add
			{
				base.Events.AddHandler(MenuItem.UIACheckedChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.UIACheckedChangedEvent, value);
			}
		}

		// Token: 0x14000246 RID: 582
		// (add) Token: 0x060026C0 RID: 9920 RVA: 0x00093ED8 File Offset: 0x000920D8
		// (remove) Token: 0x060026C1 RID: 9921 RVA: 0x00093EEC File Offset: 0x000920EC
		internal event EventHandler UIARadioCheckChanged
		{
			add
			{
				base.Events.AddHandler(MenuItem.UIARadioCheckChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.UIARadioCheckChangedEvent, value);
			}
		}

		// Token: 0x14000247 RID: 583
		// (add) Token: 0x060026C2 RID: 9922 RVA: 0x00093F00 File Offset: 0x00092100
		// (remove) Token: 0x060026C3 RID: 9923 RVA: 0x00093F14 File Offset: 0x00092114
		internal event EventHandler UIAEnabledChanged
		{
			add
			{
				base.Events.AddHandler(MenuItem.UIAEnabledChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.UIAEnabledChangedEvent, value);
			}
		}

		// Token: 0x14000248 RID: 584
		// (add) Token: 0x060026C4 RID: 9924 RVA: 0x00093F28 File Offset: 0x00092128
		// (remove) Token: 0x060026C5 RID: 9925 RVA: 0x00093F3C File Offset: 0x0009213C
		internal event EventHandler UIATextChanged
		{
			add
			{
				base.Events.AddHandler(MenuItem.UIATextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuItem.UIATextChangedEvent, value);
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00093F50 File Offset: 0x00092150
		private void CommonConstructor(string text)
		{
			this.defaut_item = false;
			this.separator = false;
			this.break_ = false;
			this.bar_break = false;
			this.checked_ = false;
			this.radiocheck = false;
			this.enabled = true;
			this.showshortcut = true;
			this.visible = true;
			this.ownerdraw = false;
			this.menubar = false;
			this.menuheight = 0;
			this.xtab = 0;
			this.index = -1;
			this.mnemonic = '\0';
			this.menuid = -1;
			this.mergeorder = 0;
			this.mergetype = MenuMerge.Add;
			this.Text = text;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x00093FE4 File Offset: 0x000921E4
		internal void OnUIACheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.UIACheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x00094018 File Offset: 0x00092218
		internal void OnUIARadioCheckChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.UIARadioCheckChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0009404C File Offset: 0x0009224C
		internal void OnUIAEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.UIAEnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00094080 File Offset: 0x00092280
		internal void OnUIATextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.UIATextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" /> is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a submenu item or menu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
		/// <returns>true if the menu item is placed on a new line or in a new column; false if the menu item is left in its default placement. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x000940B4 File Offset: 0x000922B4
		// (set) Token: 0x060026CC RID: 9932 RVA: 0x000940BC File Offset: 0x000922BC
		[Browsable(false)]
		[DefaultValue(false)]
		public bool BarBreak
		{
			get
			{
				return this.break_;
			}
			set
			{
				this.break_ = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a menu item or submenu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
		/// <returns>true if the menu item is placed on a new line or in a new column; false if the menu item is left in its default placement. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x000940C8 File Offset: 0x000922C8
		// (set) Token: 0x060026CE RID: 9934 RVA: 0x000940D0 File Offset: 0x000922D0
		[Browsable(false)]
		[DefaultValue(false)]
		public bool Break
		{
			get
			{
				return this.bar_break;
			}
			set
			{
				this.bar_break = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a check mark appears next to the text of the menu item.</summary>
		/// <returns>true if there is a check mark next to the menu item; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.MenuItem" /> is a top-level menu or has children.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x000940DC File Offset: 0x000922DC
		// (set) Token: 0x060026D0 RID: 9936 RVA: 0x000940E4 File Offset: 0x000922E4
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.checked_;
			}
			set
			{
				if (this.checked_ == value)
				{
					return;
				}
				this.checked_ = value;
				this.OnUIACheckedChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is the default menu item.</summary>
		/// <returns>true if the menu item is the default item in a menu; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x00094108 File Offset: 0x00092308
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x00094110 File Offset: 0x00092310
		[DefaultValue(false)]
		public bool DefaultItem
		{
			get
			{
				return this.defaut_item;
			}
			set
			{
				this.defaut_item = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is enabled.</summary>
		/// <returns>true if the menu item is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x0009411C File Offset: 0x0009231C
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x00094124 File Offset: 0x00092324
		[Localizable(true)]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled == value)
				{
					return;
				}
				this.enabled = value;
				this.OnUIAEnabledChanged(EventArgs.Empty);
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating the position of the menu item in its parent menu.</summary>
		/// <returns>The zero-based index representing the position of the menu item in its parent menu.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero or greater than the item count.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x0009414C File Offset: 0x0009234C
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x00094154 File Offset: 0x00092354
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				if (this.Parent != null && this.Parent.MenuItems != null && (value < 0 || value >= this.Parent.MenuItems.Count))
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'value'");
				}
				this.index = value;
			}
		}

		/// <summary>Gets a value indicating whether the menu item contains child menu items.</summary>
		/// <returns>true if the menu item contains child menu items; false if the menu is a standalone menu item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000941BC File Offset: 0x000923BC
		[Browsable(false)]
		public override bool IsParent
		{
			get
			{
				return this.IsPopup;
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item will be populated with a list of the Multiple Document Interface (MDI) child windows that are displayed within the associated form.</summary>
		/// <returns>true if a list of the MDI child windows is displayed in this menu item; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x000941C4 File Offset: 0x000923C4
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x000941CC File Offset: 0x000923CC
		[DefaultValue(false)]
		public bool MdiList
		{
			get
			{
				return this.mdilist;
			}
			set
			{
				if (this.mdilist == value)
				{
					return;
				}
				this.mdilist = value;
				if (this.mdilist || this.mdilist_items == null)
				{
					return;
				}
				foreach (object obj in this.mdilist_items.Keys)
				{
					MenuItem menuItem = (MenuItem)obj;
					base.MenuItems.Remove(menuItem);
				}
				this.mdilist_items.Clear();
				this.mdilist_items = null;
			}
		}

		/// <summary>Gets a value indicating the Windows identifier for this menu item.</summary>
		/// <returns>The Windows identifier for this menu item.</returns>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x00094284 File Offset: 0x00092484
		protected int MenuID
		{
			get
			{
				return this.menuid;
			}
		}

		/// <summary>Gets or sets a value indicating the relative position of the menu item when it is merged with another.</summary>
		/// <returns>A zero-based index representing the merge order position for this menu item. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x0009428C File Offset: 0x0009248C
		// (set) Token: 0x060026DC RID: 9948 RVA: 0x00094294 File Offset: 0x00092494
		[DefaultValue(0)]
		public int MergeOrder
		{
			get
			{
				return this.mergeorder;
			}
			set
			{
				this.mergeorder = value;
			}
		}

		/// <summary>Gets or sets a value indicating the behavior of this menu item when its menu is merged with another.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuMerge" /> value that represents the menu item's merge type.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.MenuMerge" /> values.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x000942A0 File Offset: 0x000924A0
		// (set) Token: 0x060026DE RID: 9950 RVA: 0x000942A8 File Offset: 0x000924A8
		[DefaultValue(MenuMerge.Add)]
		public MenuMerge MergeType
		{
			get
			{
				return this.mergetype;
			}
			set
			{
				if (!Enum.IsDefined(typeof(MenuMerge), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for MenuMerge", value));
				}
				this.mergetype = value;
			}
		}

		/// <summary>Gets a value indicating the mnemonic character that is associated with this menu item.</summary>
		/// <returns>A character that represents the mnemonic character associated with this menu item. Returns the NUL character (ASCII value 0) if no mnemonic character is specified in the text of the <see cref="T:System.Windows.Forms.MenuItem" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x000942E4 File Offset: 0x000924E4
		[Browsable(false)]
		public char Mnemonic
		{
			get
			{
				return this.mnemonic;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code that you provide draws the menu item or Windows draws the menu item.</summary>
		/// <returns>true if the menu item is to be drawn using code; false if the menu item is to be drawn by Windows. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000942EC File Offset: 0x000924EC
		// (set) Token: 0x060026E1 RID: 9953 RVA: 0x000942F4 File Offset: 0x000924F4
		[DefaultValue(false)]
		public bool OwnerDraw
		{
			get
			{
				return this.ownerdraw;
			}
			set
			{
				this.ownerdraw = value;
			}
		}

		/// <summary>Gets a value indicating the menu that contains this menu item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Menu" /> that represents the menu that contains this menu item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x00094300 File Offset: 0x00092500
		[Browsable(false)]
		public Menu Parent
		{
			get
			{
				return this.parent_menu;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" />, if checked, displays a radio-button instead of a check mark.</summary>
		/// <returns>true if a radio-button is to be used instead of a check mark; false if the standard check mark is to be displayed when the menu item is checked. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x00094308 File Offset: 0x00092508
		// (set) Token: 0x060026E4 RID: 9956 RVA: 0x00094310 File Offset: 0x00092510
		[DefaultValue(false)]
		public bool RadioCheck
		{
			get
			{
				return this.radiocheck;
			}
			set
			{
				if (this.radiocheck == value)
				{
					return;
				}
				this.radiocheck = value;
				this.OnUIARadioCheckChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating the shortcut key associated with the menu item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. The default is Shortcut.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x00094334 File Offset: 0x00092534
		// (set) Token: 0x060026E6 RID: 9958 RVA: 0x0009433C File Offset: 0x0009253C
		[DefaultValue(Shortcut.None)]
		[Localizable(true)]
		public Shortcut Shortcut
		{
			get
			{
				return this.shortcut;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Shortcut), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for Shortcut", value));
				}
				this.shortcut = value;
				this.UpdateMenuItem();
			}
		}

		/// <summary>Gets or sets a value indicating whether the shortcut key that is associated with the menu item is displayed next to the menu item caption.</summary>
		/// <returns>true if the shortcut key combination is displayed next to the menu item caption; false if the shortcut key combination is not to be displayed. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x0009437C File Offset: 0x0009257C
		// (set) Token: 0x060026E8 RID: 9960 RVA: 0x00094384 File Offset: 0x00092584
		[Localizable(true)]
		[DefaultValue(true)]
		public bool ShowShortcut
		{
			get
			{
				return this.showshortcut;
			}
			set
			{
				this.showshortcut = value;
			}
		}

		/// <summary>Gets or sets a value indicating the caption of the menu item.</summary>
		/// <returns>The text caption of the menu item.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x00094390 File Offset: 0x00092590
		// (set) Token: 0x060026EA RID: 9962 RVA: 0x00094398 File Offset: 0x00092598
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				if (this.text == "-")
				{
					this.separator = true;
				}
				else
				{
					this.separator = false;
				}
				this.OnUIATextChanged(EventArgs.Empty);
				this.ProcessMnemonic();
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is visible.</summary>
		/// <returns>true if the menu item will be made visible on the menu; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x000943EC File Offset: 0x000925EC
		// (set) Token: 0x060026EC RID: 9964 RVA: 0x000943F4 File Offset: 0x000925F4
		[Localizable(true)]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (value == this.visible)
				{
					return;
				}
				this.visible = value;
				if (this.menu_items != null)
				{
					foreach (object obj in this.menu_items)
					{
						MenuItem menuItem = (MenuItem)obj;
						menuItem.Visible = value;
					}
				}
				if (this.parent_menu != null)
				{
					this.parent_menu.OnMenuChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x000944A0 File Offset: 0x000926A0
		// (set) Token: 0x060026EE RID: 9966 RVA: 0x000944B0 File Offset: 0x000926B0
		internal new int Height
		{
			get
			{
				return this.bounds.Height;
			}
			set
			{
				this.bounds.Height = value;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x000944C0 File Offset: 0x000926C0
		internal bool IsPopup
		{
			get
			{
				return this.menu_items.Count > 0;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x000944D8 File Offset: 0x000926D8
		internal bool MeasureEventDefined
		{
			get
			{
				return this.ownerdraw && base.Events[MenuItem.MeasureItemEvent] != null;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x00094500 File Offset: 0x00092700
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x00094508 File Offset: 0x00092708
		internal bool MenuBar
		{
			get
			{
				return this.menubar;
			}
			set
			{
				this.menubar = value;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x00094514 File Offset: 0x00092714
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x0009451C File Offset: 0x0009271C
		internal int MenuHeight
		{
			get
			{
				return this.menuheight;
			}
			set
			{
				this.menuheight = value;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x00094528 File Offset: 0x00092728
		// (set) Token: 0x060026F6 RID: 9974 RVA: 0x00094530 File Offset: 0x00092730
		internal bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x0009453C File Offset: 0x0009273C
		// (set) Token: 0x060026F8 RID: 9976 RVA: 0x00094544 File Offset: 0x00092744
		internal bool Separator
		{
			get
			{
				return this.separator;
			}
			set
			{
				this.separator = value;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x00094550 File Offset: 0x00092750
		internal DrawItemState Status
		{
			get
			{
				DrawItemState drawItemState = DrawItemState.None;
				MenuTracker tracker = this.Parent.Tracker;
				if (this.Selected)
				{
					drawItemState |= ((!tracker.active && !tracker.Navigating) ? DrawItemState.HotLight : DrawItemState.Selected);
				}
				if (!this.Enabled)
				{
					drawItemState |= DrawItemState.Grayed | DrawItemState.Disabled;
				}
				if (this.Checked)
				{
					drawItemState |= DrawItemState.Checked;
				}
				if (!tracker.Navigating)
				{
					drawItemState |= DrawItemState.NoAccelerator;
				}
				return drawItemState;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x000945CC File Offset: 0x000927CC
		internal bool VisibleItems
		{
			get
			{
				if (this.menu_items != null)
				{
					foreach (object obj in this.menu_items)
					{
						MenuItem menuItem = (MenuItem)obj;
						if (menuItem.Visible)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x00094654 File Offset: 0x00092854
		// (set) Token: 0x060026FC RID: 9980 RVA: 0x00094664 File Offset: 0x00092864
		internal new int Width
		{
			get
			{
				return this.bounds.Width;
			}
			set
			{
				this.bounds.Width = value;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00094674 File Offset: 0x00092874
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x00094684 File Offset: 0x00092884
		internal new int X
		{
			get
			{
				return this.bounds.X;
			}
			set
			{
				this.bounds.X = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x00094694 File Offset: 0x00092894
		// (set) Token: 0x06002700 RID: 9984 RVA: 0x0009469C File Offset: 0x0009289C
		internal int XTab
		{
			get
			{
				return this.xtab;
			}
			set
			{
				this.xtab = value;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000946A8 File Offset: 0x000928A8
		// (set) Token: 0x06002702 RID: 9986 RVA: 0x000946B8 File Offset: 0x000928B8
		internal new int Y
		{
			get
			{
				return this.bounds.Y;
			}
			set
			{
				this.bounds.Y = value;
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the duplicated menu item.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002703 RID: 9987 RVA: 0x000946C8 File Offset: 0x000928C8
		public virtual MenuItem CloneMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.CloneMenu(this);
			return menuItem;
		}

		/// <summary>Creates a copy of the specified <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <param name="itemSrc">The <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item to copy. </param>
		// Token: 0x06002704 RID: 9988 RVA: 0x000946E4 File Offset: 0x000928E4
		protected void CloneMenu(MenuItem itemSrc)
		{
			base.CloneMenu(itemSrc);
			this.MdiList = itemSrc.MdiList;
			this.is_window_menu_item = itemSrc.is_window_menu_item;
			bool flag = false;
			for (int i = base.MenuItems.Count - 1; i >= 0; i--)
			{
				if (base.MenuItems[i].is_window_menu_item)
				{
					base.MenuItems.RemoveAt(i);
					flag = true;
				}
			}
			if (flag)
			{
				this.PopulateWindowMenu();
			}
			this.BarBreak = itemSrc.BarBreak;
			this.Break = itemSrc.Break;
			this.Checked = itemSrc.Checked;
			this.DefaultItem = itemSrc.DefaultItem;
			this.Enabled = itemSrc.Enabled;
			this.MergeOrder = itemSrc.MergeOrder;
			this.MergeType = itemSrc.MergeType;
			this.OwnerDraw = itemSrc.OwnerDraw;
			this.RadioCheck = itemSrc.RadioCheck;
			this.Shortcut = itemSrc.Shortcut;
			this.ShowShortcut = itemSrc.ShowShortcut;
			this.Text = itemSrc.Text;
			this.Visible = itemSrc.Visible;
			base.Name = itemSrc.Name;
			base.Tag = itemSrc.Tag;
			base.Events[MenuItem.ClickEvent] = itemSrc.Events[MenuItem.ClickEvent];
			base.Events[MenuItem.DrawItemEvent] = itemSrc.Events[MenuItem.DrawItemEvent];
			base.Events[MenuItem.MeasureItemEvent] = itemSrc.Events[MenuItem.MeasureItemEvent];
			base.Events[MenuItem.PopupEvent] = itemSrc.Events[MenuItem.PopupEvent];
			base.Events[MenuItem.SelectEvent] = itemSrc.Events[MenuItem.SelectEvent];
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		// Token: 0x06002705 RID: 9989 RVA: 0x000948B4 File Offset: 0x00092AB4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.parent_menu != null)
			{
				this.parent_menu.MenuItems.Remove(this);
			}
			base.Dispose(disposing);
		}

		/// <summary>Merges this <see cref="T:System.Windows.Forms.MenuItem" /> with another <see cref="T:System.Windows.Forms.MenuItem" /> and returns the resulting merged <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the merged menu item.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002706 RID: 9990 RVA: 0x000948E0 File Offset: 0x00092AE0
		public virtual MenuItem MergeMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.CloneMenu(this);
			return menuItem;
		}

		/// <summary>Merges another menu item with this menu item.</summary>
		/// <param name="itemSrc">A <see cref="T:System.Windows.Forms.MenuItem" /> that specifies the menu item to merge with this one. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002707 RID: 9991 RVA: 0x000948FC File Offset: 0x00092AFC
		public void MergeMenu(MenuItem itemSrc)
		{
			base.MergeMenu(itemSrc);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002708 RID: 9992 RVA: 0x00094908 File Offset: 0x00092B08
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data. </param>
		// Token: 0x06002709 RID: 9993 RVA: 0x0009493C File Offset: 0x00092B3C
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[MenuItem.DrawItemEvent];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600270A RID: 9994 RVA: 0x00094970 File Offset: 0x00092B70
		protected virtual void OnInitMenuPopup(EventArgs e)
		{
			this.OnPopup(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.MeasureItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data. </param>
		// Token: 0x0600270B RID: 9995 RVA: 0x0009497C File Offset: 0x00092B7C
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (!this.OwnerDraw)
			{
				return;
			}
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[MenuItem.MeasureItemEvent];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600270C RID: 9996 RVA: 0x000949BC File Offset: 0x00092BBC
		protected virtual void OnPopup(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.PopupEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600270D RID: 9997 RVA: 0x000949F0 File Offset: 0x00092BF0
		protected virtual void OnSelect(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuItem.SelectEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the <see cref="T:System.Windows.Forms.MenuItem" />, simulating a click by a user.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600270E RID: 9998 RVA: 0x00094A24 File Offset: 0x00092C24
		public void PerformClick()
		{
			this.OnClick(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600270F RID: 9999 RVA: 0x00094A34 File Offset: 0x00092C34
		public virtual void PerformSelect()
		{
			this.OnSelect(EventArgs.Empty);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MenuItem" />. The string includes the type and the <see cref="P:System.Windows.Forms.MenuItem.Text" /> property of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002710 RID: 10000 RVA: 0x00094A44 File Offset: 0x00092C44
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				", Items.Count: ",
				base.MenuItems.Count,
				", Text: ",
				this.text
			});
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x00094A94 File Offset: 0x00092C94
		internal virtual void Invalidate()
		{
			if (this.Parent == null || !(this.Parent is MainMenu) || this.Parent.Wnd == null)
			{
				return;
			}
			Form form = this.Parent.Wnd.FindForm();
			if (form == null || !form.IsHandleCreated)
			{
				return;
			}
			XplatUI.RequestNCRecalc(form.Handle);
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x00094AFC File Offset: 0x00092CFC
		internal void PerformPopup()
		{
			this.OnPopup(EventArgs.Empty);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x00094B0C File Offset: 0x00092D0C
		internal void PerformDrawItem(DrawItemEventArgs e)
		{
			this.PopulateWindowMenu();
			if (this.OwnerDraw)
			{
				this.OnDrawItem(e);
			}
			else
			{
				ThemeEngine.Current.DrawMenuItem(this, e);
			}
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00094B44 File Offset: 0x00092D44
		private void PopulateWindowMenu()
		{
			if (this.mdilist)
			{
				if (this.mdilist_items == null)
				{
					this.mdilist_items = new Hashtable();
					this.mdilist_forms = new Hashtable();
				}
				MainMenu mainMenu = base.GetMainMenu();
				if (mainMenu != null && mainMenu.GetForm() != null)
				{
					Form form = mainMenu.GetForm();
					this.mdicontainer = form.MdiContainer;
					if (this.mdicontainer != null)
					{
						MenuItem[] array = new MenuItem[this.mdilist_items.Count];
						this.mdilist_items.Keys.CopyTo(array, 0);
						foreach (MenuItem menuItem in array)
						{
							Form form2 = (Form)this.mdilist_items[menuItem];
							if (!this.mdicontainer.mdi_child_list.Contains(form2))
							{
								this.mdilist_items.Remove(menuItem);
								this.mdilist_forms.Remove(form2);
								base.MenuItems.Remove(menuItem);
							}
						}
						for (int j = 0; j < this.mdicontainer.mdi_child_list.Count; j++)
						{
							Form form3 = (Form)this.mdicontainer.mdi_child_list[j];
							MenuItem menuItem2;
							if (this.mdilist_forms.Contains(form3))
							{
								menuItem2 = (MenuItem)this.mdilist_forms[form3];
							}
							else
							{
								menuItem2 = new MenuItem();
								menuItem2.is_window_menu_item = true;
								menuItem2.Click += new EventHandler(this.MdiWindowClickHandler);
								this.mdilist_items[menuItem2] = form3;
								this.mdilist_forms[form3] = menuItem2;
								base.MenuItems.AddNoEvents(menuItem2);
							}
							menuItem2.Visible = form3.Visible;
							menuItem2.Text = "&" + (j + 1).ToString() + " " + form3.Text;
							menuItem2.Checked = form.ActiveMdiChild == form3;
						}
					}
				}
			}
			else if (this.mdilist_items != null)
			{
				foreach (object obj in this.mdilist_items.Values)
				{
					MenuItem menuItem3 = (MenuItem)obj;
					base.MenuItems.Remove(menuItem3);
				}
				this.mdilist_forms.Clear();
				this.mdilist_items.Clear();
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00094DEC File Offset: 0x00092FEC
		internal void PerformMeasureItem(MeasureItemEventArgs e)
		{
			this.OnMeasureItem(e);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00094DF8 File Offset: 0x00092FF8
		private void ProcessMnemonic()
		{
			if (this.text == null || this.text.Length < 2)
			{
				this.mnemonic = '\0';
				return;
			}
			bool flag = false;
			for (int i = 0; i < this.text.Length - 1; i++)
			{
				if (this.text.get_Chars(i) == '&')
				{
					if (!flag && this.text.get_Chars(i + 1) != '&')
					{
						this.mnemonic = char.ToUpper(this.text.get_Chars(i + 1));
						return;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			this.mnemonic = '\0';
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00094EA4 File Offset: 0x000930A4
		private string GetShortCutTextCtrl()
		{
			return "Ctrl";
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x00094EAC File Offset: 0x000930AC
		private string GetShortCutTextAlt()
		{
			return "Alt";
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x00094EB4 File Offset: 0x000930B4
		private string GetShortCutTextShift()
		{
			return "Shift";
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x00094EBC File Offset: 0x000930BC
		internal string GetShortCutText()
		{
			if (this.Shortcut >= Shortcut.CtrlA && this.Shortcut <= Shortcut.CtrlZ)
			{
				return this.GetShortCutTextCtrl() + "+" + (char)(65 + (this.Shortcut - Shortcut.CtrlA));
			}
			if (this.Shortcut >= Shortcut.Alt0 && this.Shortcut <= Shortcut.Alt9)
			{
				return this.GetShortCutTextAlt() + "+" + (char)(48 + (this.Shortcut - Shortcut.Alt0));
			}
			if (this.Shortcut >= Shortcut.AltF1 && this.Shortcut <= Shortcut.AltF9)
			{
				return this.GetShortCutTextAlt() + "+F" + (char)(49 + (this.Shortcut - Shortcut.AltF1));
			}
			if (this.Shortcut >= Shortcut.Ctrl0 && this.Shortcut <= Shortcut.Ctrl9)
			{
				return this.GetShortCutTextCtrl() + "+" + (char)(48 + (this.Shortcut - Shortcut.Ctrl0));
			}
			if (this.Shortcut >= Shortcut.CtrlF1 && this.Shortcut <= Shortcut.CtrlF9)
			{
				return this.GetShortCutTextCtrl() + "+F" + (char)(49 + (this.Shortcut - Shortcut.CtrlF1));
			}
			if (this.Shortcut >= Shortcut.CtrlShift0 && this.Shortcut <= Shortcut.CtrlShift9)
			{
				return string.Concat(new object[]
				{
					this.GetShortCutTextCtrl(),
					"+",
					this.GetShortCutTextShift(),
					"+",
					(char)(48 + (this.Shortcut - Shortcut.CtrlShift0))
				});
			}
			if (this.Shortcut >= Shortcut.CtrlShiftA && this.Shortcut <= Shortcut.CtrlShiftZ)
			{
				return string.Concat(new object[]
				{
					this.GetShortCutTextCtrl(),
					"+",
					this.GetShortCutTextShift(),
					"+",
					(char)(65 + (this.Shortcut - Shortcut.CtrlShiftA))
				});
			}
			if (this.Shortcut >= Shortcut.CtrlShiftF1 && this.Shortcut <= Shortcut.CtrlShiftF9)
			{
				return string.Concat(new object[]
				{
					this.GetShortCutTextCtrl(),
					"+",
					this.GetShortCutTextShift(),
					"+F",
					(char)(49 + (this.Shortcut - Shortcut.CtrlShiftF1))
				});
			}
			if (this.Shortcut >= Shortcut.F1 && this.Shortcut <= Shortcut.F9)
			{
				return "F" + (char)(49 + (this.Shortcut - Shortcut.F1));
			}
			if (this.Shortcut >= Shortcut.ShiftF1 && this.Shortcut <= Shortcut.ShiftF9)
			{
				return this.GetShortCutTextShift() + "+F" + (char)(49 + (this.Shortcut - Shortcut.ShiftF1));
			}
			Shortcut shortcut = this.Shortcut;
			switch (shortcut)
			{
			case Shortcut.F10:
				return "F10";
			case Shortcut.F11:
				return "F11";
			case Shortcut.F12:
				return "F12";
			default:
				switch (shortcut)
				{
				case Shortcut.ShiftF10:
					return this.GetShortCutTextShift() + "+F10";
				case Shortcut.ShiftF11:
					return this.GetShortCutTextShift() + "+F11";
				case Shortcut.ShiftF12:
					return this.GetShortCutTextShift() + "+F12";
				default:
					switch (shortcut)
					{
					case Shortcut.CtrlF10:
						return this.GetShortCutTextCtrl() + "+F10";
					case Shortcut.CtrlF11:
						return this.GetShortCutTextCtrl() + "+F11";
					case Shortcut.CtrlF12:
						return this.GetShortCutTextCtrl() + "+F12";
					default:
						switch (shortcut)
						{
						case Shortcut.CtrlShiftF10:
							return this.GetShortCutTextCtrl() + "+" + this.GetShortCutTextShift() + "+F10";
						case Shortcut.CtrlShiftF11:
							return this.GetShortCutTextCtrl() + "+" + this.GetShortCutTextShift() + "+F11";
						case Shortcut.CtrlShiftF12:
							return this.GetShortCutTextCtrl() + "+" + this.GetShortCutTextShift() + "+F12";
						default:
							switch (shortcut)
							{
							case Shortcut.AltF10:
								return this.GetShortCutTextAlt() + "+F10";
							case Shortcut.AltF11:
								return this.GetShortCutTextAlt() + "+F11";
							case Shortcut.AltF12:
								return this.GetShortCutTextAlt() + "+F12";
							default:
								if (shortcut == Shortcut.Ins)
								{
									return "Ins";
								}
								if (shortcut == Shortcut.Del)
								{
									return "Del";
								}
								if (shortcut == Shortcut.ShiftIns)
								{
									return this.GetShortCutTextShift() + "+Ins";
								}
								if (shortcut == Shortcut.ShiftDel)
								{
									return this.GetShortCutTextShift() + "+Del";
								}
								if (shortcut == Shortcut.CtrlIns)
								{
									return this.GetShortCutTextCtrl() + "+Ins";
								}
								if (shortcut == Shortcut.CtrlDel)
								{
									return this.GetShortCutTextCtrl() + "+Del";
								}
								if (shortcut == Shortcut.None)
								{
									return "None";
								}
								if (shortcut != Shortcut.AltBksp)
								{
									return string.Empty;
								}
								return "AltBksp";
							}
							break;
						}
						break;
					}
					break;
				}
				break;
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0009540C File Offset: 0x0009360C
		private void MdiWindowClickHandler(object sender, EventArgs e)
		{
			Form form = (Form)this.mdilist_items[sender];
			if (form == null)
			{
				return;
			}
			this.mdicontainer.ActivateChild(form);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x00095440 File Offset: 0x00093640
		private void UpdateMenuItem()
		{
			if (this.parent_menu == null || this.parent_menu.Tracker == null)
			{
				return;
			}
			this.parent_menu.Tracker.RemoveShortcuts(this);
			this.parent_menu.Tracker.AddShortcuts(this);
		}

		// Token: 0x0400135C RID: 4956
		internal bool separator;

		// Token: 0x0400135D RID: 4957
		internal bool break_;

		// Token: 0x0400135E RID: 4958
		internal bool bar_break;

		// Token: 0x0400135F RID: 4959
		private Shortcut shortcut;

		// Token: 0x04001360 RID: 4960
		private string text;

		// Token: 0x04001361 RID: 4961
		private bool checked_;

		// Token: 0x04001362 RID: 4962
		private bool radiocheck;

		// Token: 0x04001363 RID: 4963
		private bool enabled;

		// Token: 0x04001364 RID: 4964
		private char mnemonic;

		// Token: 0x04001365 RID: 4965
		private bool showshortcut;

		// Token: 0x04001366 RID: 4966
		private int index;

		// Token: 0x04001367 RID: 4967
		private bool mdilist;

		// Token: 0x04001368 RID: 4968
		private Hashtable mdilist_items;

		// Token: 0x04001369 RID: 4969
		private Hashtable mdilist_forms;

		// Token: 0x0400136A RID: 4970
		private MdiClient mdicontainer;

		// Token: 0x0400136B RID: 4971
		private bool is_window_menu_item;

		// Token: 0x0400136C RID: 4972
		private bool defaut_item;

		// Token: 0x0400136D RID: 4973
		private bool visible;

		// Token: 0x0400136E RID: 4974
		private bool ownerdraw;

		// Token: 0x0400136F RID: 4975
		private int menuid;

		// Token: 0x04001370 RID: 4976
		private int mergeorder;

		// Token: 0x04001371 RID: 4977
		private int xtab;

		// Token: 0x04001372 RID: 4978
		private int menuheight;

		// Token: 0x04001373 RID: 4979
		private bool menubar;

		// Token: 0x04001374 RID: 4980
		private MenuMerge mergetype;

		// Token: 0x04001375 RID: 4981
		internal Rectangle bounds;

		// Token: 0x0400137F RID: 4991
		private bool selected;
	}
}
