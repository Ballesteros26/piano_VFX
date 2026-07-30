using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides a menu system for a form.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000252 RID: 594
	[ComVisible(true)]
	[ClassInterface(1)]
	public class MenuStrip : ToolStrip
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuStrip" /> class. </summary>
		// Token: 0x0600271D RID: 10013 RVA: 0x0009548C File Offset: 0x0009368C
		public MenuStrip()
		{
			base.CanOverflow = false;
			this.GripStyle = ToolStripGripStyle.Hidden;
			this.Stretch = true;
			this.Dock = DockStyle.Top;
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000954BC File Offset: 0x000936BC
		// Note: this type is marked as 'beforefieldinit'.
		static MenuStrip()
		{
			MenuStrip.MenuActivateEvent = new object();
			MenuStrip.MenuDeactivateEvent = new object();
		}

		/// <summary>Occurs when the user accesses the menu with the keyboard or mouse. </summary>
		// Token: 0x14000249 RID: 585
		// (add) Token: 0x0600271F RID: 10015 RVA: 0x000954D4 File Offset: 0x000936D4
		// (remove) Token: 0x06002720 RID: 10016 RVA: 0x000954E8 File Offset: 0x000936E8
		public event EventHandler MenuActivate
		{
			add
			{
				base.Events.AddHandler(MenuStrip.MenuActivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuStrip.MenuActivateEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.MenuStrip" /> is deactivated.</summary>
		// Token: 0x1400024A RID: 586
		// (add) Token: 0x06002721 RID: 10017 RVA: 0x000954FC File Offset: 0x000936FC
		// (remove) Token: 0x06002722 RID: 10018 RVA: 0x00095510 File Offset: 0x00093710
		public event EventHandler MenuDeactivate
		{
			add
			{
				base.Events.AddHandler(MenuStrip.MenuDeactivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuStrip.MenuDeactivateEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuStrip" /> supports overflow functionality. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.MenuStrip" /> supports overflow functionality; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x00095524 File Offset: 0x00093724
		// (set) Token: 0x06002724 RID: 10020 RVA: 0x0009552C File Offset: 0x0009372C
		[DefaultValue(false)]
		[Browsable(false)]
		public new bool CanOverflow
		{
			get
			{
				return base.CanOverflow;
			}
			set
			{
				base.CanOverflow = value;
			}
		}

		/// <summary>Gets or sets the visibility of the grip used to reposition the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Hidden" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x00095538 File Offset: 0x00093738
		// (set) Token: 0x06002726 RID: 10022 RVA: 0x00095540 File Offset: 0x00093740
		[DefaultValue(ToolStripGripStyle.Hidden)]
		public new ToolStripGripStyle GripStyle
		{
			get
			{
				return base.GripStyle;
			}
			set
			{
				base.GripStyle = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> that is used to display a list of Multiple-document interface (MDI) child forms.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> that represents the menu item displaying a list of MDI child forms that are open in the application.</returns>
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x0009554C File Offset: 0x0009374C
		// (set) Token: 0x06002728 RID: 10024 RVA: 0x00095554 File Offset: 0x00093754
		[TypeConverter(typeof(MdiWindowListItemConverter))]
		[MergableProperty(false)]
		[DefaultValue(null)]
		public ToolStripMenuItem MdiWindowListItem
		{
			get
			{
				return this.mdi_window_list_item;
			}
			set
			{
				if (this.mdi_window_list_item != value)
				{
					this.mdi_window_list_item = value;
					this.RefreshMdiItems();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" />. </summary>
		/// <returns>true if ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" />; otherwise, false. The default is false.</returns>
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x00095570 File Offset: 0x00093770
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x00095578 File Offset: 0x00093778
		[DefaultValue(false)]
		public new bool ShowItemToolTips
		{
			get
			{
				return base.ShowItemToolTips;
			}
			set
			{
				base.ShowItemToolTips = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuStrip" /> stretches from end to end in its container. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.MenuStrip" /> stretches from end to end in its container; otherwise, false. The default is true.</returns>
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x00095584 File Offset: 0x00093784
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x0009558C File Offset: 0x0009378C
		[DefaultValue(true)]
		public new bool Stretch
		{
			get
			{
				return base.Stretch;
			}
			set
			{
				base.Stretch = value;
			}
		}

		/// <summary>Gets the default spacing, in pixels, between the sizing grip and the edges of the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Forms.Padding" /> values representing the spacing, in pixels.</returns>
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x00095598 File Offset: 0x00093798
		protected override Padding DefaultGripMargin
		{
			get
			{
				return new Padding(2, 2, 0, 2);
			}
		}

		/// <summary>Gets the spacing, in pixels, between the left, right, top, and bottom edges of the <see cref="T:System.Windows.Forms.MenuStrip" /> from the edges of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the spacing. The default is {Left=6, Top=2, Right=0, Bottom=2}.</returns>
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x000955A4 File Offset: 0x000937A4
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(6, 2, 0, 2);
			}
		}

		/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" /> by default.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x000955B0 File Offset: 0x000937B0
		protected override bool DefaultShowItemToolTips
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the horizontal and vertical dimensions, in pixels, of the <see cref="T:System.Windows.Forms.MenuStrip" /> when it is first created.</summary>
		/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> value representing the <see cref="T:System.Windows.Forms.MenuStrip" /> horizontal and vertical dimensions, in pixels. The default is 200 x 21 pixels.</returns>
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000955B4 File Offset: 0x000937B4
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 24);
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06002731 RID: 10033 RVA: 0x000955C4 File Offset: 0x000937C4
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new MenuStrip.MenuStripAccessibleObject();
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="M:System.Windows.Forms.ToolStripMenuItem.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
		/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</param>
		// Token: 0x06002732 RID: 10034 RVA: 0x000955CC File Offset: 0x000937CC
		protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			return new ToolStripMenuItem(text, image, onClick);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuStrip.MenuActivate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002733 RID: 10035 RVA: 0x000955D8 File Offset: 0x000937D8
		protected virtual void OnMenuActivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuStrip.MenuActivateEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuStrip.MenuDeactivate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002734 RID: 10036 RVA: 0x0009560C File Offset: 0x0009380C
		protected virtual void OnMenuDeactivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MenuStrip.MenuDeactivateEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x06002735 RID: 10037 RVA: 0x00095640 File Offset: 0x00093840
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return base.ProcessCmdKey(ref m, keyData);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06002736 RID: 10038 RVA: 0x0009564C File Offset: 0x0009384C
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x00095658 File Offset: 0x00093858
		// (set) Token: 0x06002738 RID: 10040 RVA: 0x00095660 File Offset: 0x00093860
		internal override bool KeyboardActive
		{
			get
			{
				return base.KeyboardActive;
			}
			set
			{
				if (base.KeyboardActive != value)
				{
					base.KeyboardActive = value;
					if (value)
					{
						this.OnMenuActivate(EventArgs.Empty);
					}
					else
					{
						this.OnMenuDeactivate(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x000956A4 File Offset: 0x000938A4
		// (set) Token: 0x0600273A RID: 10042 RVA: 0x000956AC File Offset: 0x000938AC
		internal bool MenuDroppedDown
		{
			get
			{
				return this.menu_selected;
			}
			set
			{
				this.menu_selected = value;
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000956B8 File Offset: 0x000938B8
		internal override void Dismiss(ToolStripDropDownCloseReason reason)
		{
			this.MenuDroppedDown = false;
			base.Dismiss(reason);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000956C8 File Offset: 0x000938C8
		internal void FireMenuActivate()
		{
			ToolStripManager.AppClicked += new EventHandler(this.ToolStripMenuTracker_AppClicked);
			ToolStripManager.AppFocusChange += new EventHandler(this.ToolStripMenuTracker_AppFocusChange);
			this.OnMenuActivate(EventArgs.Empty);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000956F8 File Offset: 0x000938F8
		internal void FireMenuDeactivate()
		{
			ToolStripManager.AppClicked -= new EventHandler(this.ToolStripMenuTracker_AppClicked);
			ToolStripManager.AppFocusChange -= new EventHandler(this.ToolStripMenuTracker_AppFocusChange);
			this.OnMenuDeactivate(EventArgs.Empty);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x00095728 File Offset: 0x00093928
		internal override bool OnMenuKey()
		{
			ToolStripManager.SetActiveToolStrip(this, true);
			ToolStripItem toolStripItem = this.SelectNextToolStripItem(null, true);
			if (toolStripItem == null)
			{
				return false;
			}
			if (toolStripItem is MdiControlStrip.SystemMenuItem)
			{
				this.SelectNextToolStripItem(toolStripItem, true);
			}
			return true;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x00095764 File Offset: 0x00093964
		private void ToolStripMenuTracker_AppFocusChange(object sender, EventArgs e)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x00095774 File Offset: 0x00093974
		private void ToolStripMenuTracker_AppClicked(object sender, EventArgs e)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppClicked);
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x00095784 File Offset: 0x00093984
		internal void RefreshMdiItems()
		{
			if (this.mdi_window_list_item == null)
			{
				return;
			}
			Form form = base.FindForm();
			if (form == null || form.MainMenuStrip != this)
			{
				return;
			}
			MdiClient mdiContainer = form.MdiContainer;
			if (mdiContainer == null)
			{
				return;
			}
			ToolStripItem[] array = new ToolStripItem[this.mdi_window_list_item.DropDownItems.Count];
			this.mdi_window_list_item.DropDownItems.CopyTo(array, 0);
			foreach (ToolStripItem toolStripItem in array)
			{
				if (toolStripItem is ToolStripMenuItem && (toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry && (!mdiContainer.mdi_child_list.Contains((toolStripItem as ToolStripMenuItem).MdiClientForm) || !(toolStripItem as ToolStripMenuItem).MdiClientForm.Visible))
				{
					this.mdi_window_list_item.DropDownItems.Remove(toolStripItem);
				}
			}
			for (int j = 0; j < mdiContainer.mdi_child_list.Count; j++)
			{
				Form form2 = (Form)mdiContainer.mdi_child_list[j];
				if (form2.Visible)
				{
					ToolStripMenuItem toolStripMenuItem;
					if ((toolStripMenuItem = this.FindMdiMenuItemOfForm(form2)) == null)
					{
						if (this.CountMdiMenuItems() == 0 && this.mdi_window_list_item.DropDownItems.Count > 0 && !(this.mdi_window_list_item.DropDownItems[this.mdi_window_list_item.DropDownItems.Count - 1] is ToolStripSeparator))
						{
							this.mdi_window_list_item.DropDownItems.Add(new ToolStripSeparator());
						}
						toolStripMenuItem = new ToolStripMenuItem();
						toolStripMenuItem.MdiClientForm = form2;
						this.mdi_window_list_item.DropDownItems.Add(toolStripMenuItem);
					}
					toolStripMenuItem.Text = string.Format("&{0} {1}", j + 1, form2.Text);
					toolStripMenuItem.Checked = form.ActiveMdiChild == form2;
				}
			}
			if (this.NeedToReorderMdi())
			{
				this.ReorderMdiMenu();
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0009598C File Offset: 0x00093B8C
		private ToolStripMenuItem FindMdiMenuItemOfForm(Form f)
		{
			foreach (object obj in this.mdi_window_list_item.DropDownItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is ToolStripMenuItem && (toolStripItem as ToolStripMenuItem).MdiClientForm == f)
				{
					return (ToolStripMenuItem)toolStripItem;
				}
			}
			return null;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x00095A24 File Offset: 0x00093C24
		private int CountMdiMenuItems()
		{
			int num = 0;
			foreach (object obj in this.mdi_window_list_item.DropDownItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is ToolStripMenuItem && (toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x00095AB4 File Offset: 0x00093CB4
		private bool NeedToReorderMdi()
		{
			bool flag = false;
			foreach (object obj in this.mdi_window_list_item.DropDownItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is ToolStripMenuItem)
				{
					if (!(toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry)
					{
						if (flag)
						{
							return true;
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x00095B58 File Offset: 0x00093D58
		private void ReorderMdiMenu()
		{
			ToolStripItem[] array = new ToolStripItem[this.mdi_window_list_item.DropDownItems.Count];
			this.mdi_window_list_item.DropDownItems.CopyTo(array, 0);
			this.mdi_window_list_item.DropDownItems.Clear();
			foreach (ToolStripItem toolStripItem in array)
			{
				if (toolStripItem is ToolStripSeparator || !(toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry)
				{
					this.mdi_window_list_item.DropDownItems.Add(toolStripItem);
				}
			}
			int count = this.mdi_window_list_item.DropDownItems.Count;
			if (count > 0 && !(this.mdi_window_list_item.DropDownItems[count - 1] is ToolStripSeparator))
			{
				this.mdi_window_list_item.DropDownItems.Add(new ToolStripSeparator());
			}
			foreach (ToolStripItem toolStripItem2 in array)
			{
				if (toolStripItem2 is ToolStripMenuItem && (toolStripItem2 as ToolStripMenuItem).IsMdiWindowListEntry)
				{
					this.mdi_window_list_item.DropDownItems.Add(toolStripItem2);
				}
			}
		}

		// Token: 0x04001385 RID: 4997
		private ToolStripMenuItem mdi_window_list_item;

		// Token: 0x02000253 RID: 595
		private class MenuStripAccessibleObject : AccessibleObject
		{
		}
	}
}
