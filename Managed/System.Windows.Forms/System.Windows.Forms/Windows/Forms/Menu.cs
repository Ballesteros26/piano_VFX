using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents the base functionality for all menus. Although <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000249 RID: 585
	[ListBindable(false)]
	[ToolboxItemFilter("System.Windows.Forms", 0)]
	public abstract class Menu : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu" /> class.</summary>
		/// <param name="items">An array of type <see cref="T:System.Windows.Forms.MenuItem" /> containing the objects to add to the menu.</param>
		// Token: 0x06002645 RID: 9797 RVA: 0x00091388 File Offset: 0x0008F588
		protected Menu(MenuItem[] items)
		{
			this.menu_items = new Menu.MenuItemCollection(this);
			if (items != null)
			{
				this.menu_items.AddRange(items);
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000913BC File Offset: 0x0008F5BC
		// Note: this type is marked as 'beforefieldinit'.
		static Menu()
		{
			Menu.MenuChangedEvent = new object();
		}

		// Token: 0x1400023F RID: 575
		// (add) Token: 0x06002647 RID: 9799 RVA: 0x000913C8 File Offset: 0x0008F5C8
		// (remove) Token: 0x06002648 RID: 9800 RVA: 0x000913DC File Offset: 0x0008F5DC
		internal event EventHandler MenuChanged
		{
			add
			{
				base.Events.AddHandler(Menu.MenuChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Menu.MenuChangedEvent, value);
			}
		}

		/// <summary>Gets a value representing the window handle for the menu.</summary>
		/// <returns>The HMENU value of the menu.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000913F0 File Offset: 0x0008F5F0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public IntPtr Handle
		{
			get
			{
				return this.menu_handle;
			}
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000913F8 File Offset: 0x0008F5F8
		internal virtual void OnMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Menu.MenuChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Gets a value indicating whether this menu contains any menu items. This property is read-only.</summary>
		/// <returns>true if this menu contains <see cref="T:System.Windows.Forms.MenuItem" /> objects; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x0009142C File Offset: 0x0008F62C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public virtual bool IsParent
		{
			get
			{
				return this.menu_items != null && this.menu_items.Count > 0;
			}
		}

		/// <summary>Gets a value indicating the <see cref="T:System.Windows.Forms.MenuItem" /> that is used to display a list of multiple document interface (MDI) child forms.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item displaying a list of MDI child forms that are open in the application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x00091450 File Offset: 0x0008F650
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public MenuItem MdiListItem
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating the collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects associated with the menu.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> that represents the list of <see cref="T:System.Windows.Forms.MenuItem" /> objects stored in the menu.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x00091458 File Offset: 0x0008F658
		[MergableProperty(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(2)]
		public Menu.MenuItemCollection MenuItems
		{
			get
			{
				return this.menu_items;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <returns>A string representing the name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x00091460 File Offset: 0x0008F660
		// (set) Token: 0x0600264F RID: 9807 RVA: 0x00091468 File Offset: 0x0008F668
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string Name
		{
			get
			{
				return this.control_name;
			}
			set
			{
				this.control_name = value;
			}
		}

		/// <summary>Gets or sets user-defined data associated with the control.</summary>
		/// <returns>An object representing the data.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x00091474 File Offset: 0x0008F674
		// (set) Token: 0x06002651 RID: 9809 RVA: 0x0009147C File Offset: 0x0008F67C
		[MWFCategory("Data")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		[Bindable(true)]
		[Localizable(false)]
		public object Tag
		{
			get
			{
				return this.control_tag;
			}
			set
			{
				this.control_tag = value;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x00091488 File Offset: 0x0008F688
		internal Rectangle Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x00091490 File Offset: 0x0008F690
		internal MenuItem SelectedItem
		{
			get
			{
				foreach (object obj in this.MenuItems)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (menuItem.Selected)
					{
						return menuItem;
					}
				}
				return null;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00091510 File Offset: 0x0008F710
		// (set) Token: 0x06002655 RID: 9813 RVA: 0x00091520 File Offset: 0x0008F720
		internal int Height
		{
			get
			{
				return this.rect.Height;
			}
			set
			{
				this.rect.Height = value;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x00091530 File Offset: 0x0008F730
		// (set) Token: 0x06002657 RID: 9815 RVA: 0x00091540 File Offset: 0x0008F740
		internal int Width
		{
			get
			{
				return this.rect.Width;
			}
			set
			{
				this.rect.Width = value;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00091550 File Offset: 0x0008F750
		// (set) Token: 0x06002659 RID: 9817 RVA: 0x00091560 File Offset: 0x0008F760
		internal int X
		{
			get
			{
				return this.rect.X;
			}
			set
			{
				this.rect.X = value;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x00091570 File Offset: 0x0008F770
		// (set) Token: 0x0600265B RID: 9819 RVA: 0x00091580 File Offset: 0x0008F780
		internal int Y
		{
			get
			{
				return this.rect.Y;
			}
			set
			{
				this.rect.Y = value;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x00091590 File Offset: 0x0008F790
		internal MenuTracker Tracker
		{
			get
			{
				Menu menu = this;
				while (menu.parent_menu != null)
				{
					menu = menu.parent_menu;
				}
				return menu.tracker;
			}
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.Menu" /> that is passed as a parameter to the current <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> to copy. </param>
		// Token: 0x0600265D RID: 9821 RVA: 0x000915BC File Offset: 0x0008F7BC
		protected void CloneMenu(Menu menuSrc)
		{
			this.Dispose(true);
			this.menu_items = new Menu.MenuItemCollection(this);
			for (int i = 0; i < menuSrc.MenuItems.Count; i++)
			{
				this.menu_items.Add(menuSrc.MenuItems[i].CloneMenu());
			}
		}

		/// <summary>Creates a new handle to the <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <returns>A handle to the menu if the method succeeds; otherwise, null.</returns>
		// Token: 0x0600265E RID: 9822 RVA: 0x00091618 File Offset: 0x0008F818
		protected virtual IntPtr CreateMenuHandle()
		{
			return IntPtr.Zero;
		}

		/// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600265F RID: 9823 RVA: 0x00091620 File Offset: 0x0008F820
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.menu_handle != IntPtr.Zero)
			{
				this.menu_handle = IntPtr.Zero;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.MenuItem" /> that contains the value specified. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> that matches value; otherwise, null.</returns>
		/// <param name="type">The type of item to use to find the <see cref="T:System.Windows.Forms.MenuItem" />.</param>
		/// <param name="value">The item to use to find the <see cref="T:System.Windows.Forms.MenuItem" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002660 RID: 9824 RVA: 0x00091654 File Offset: 0x0008F854
		public MenuItem FindMenuItem(int type, IntPtr value)
		{
			return null;
		}

		/// <summary>Returns the position at which a menu item should be inserted into the menu.</summary>
		/// <returns>The position at which a menu item should be inserted into the menu.</returns>
		/// <param name="mergeOrder">The merge order position for the menu item to be merged.</param>
		// Token: 0x06002661 RID: 9825 RVA: 0x00091658 File Offset: 0x0008F858
		protected int FindMergePosition(int mergeOrder)
		{
			int num = this.MenuItems.Count;
			int i = 0;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				if (this.MenuItems[num2].MergeOrder > mergeOrder)
				{
					num = num2;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return i;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ContextMenu" /> that contains this menu.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> that contains this menu. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002662 RID: 9826 RVA: 0x000916A8 File Offset: 0x0008F8A8
		public ContextMenu GetContextMenu()
		{
			for (Menu menu = this; menu != null; menu = menu.parent_menu)
			{
				if (menu is ContextMenu)
				{
					return (ContextMenu)menu;
				}
			}
			return null;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002663 RID: 9827 RVA: 0x000916DC File Offset: 0x0008F8DC
		public MainMenu GetMainMenu()
		{
			for (Menu menu = this; menu != null; menu = menu.parent_menu)
			{
				if (menu is MainMenu)
				{
					return (MainMenu)menu;
				}
			}
			return null;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00091710 File Offset: 0x0008F910
		internal virtual void InvalidateItem(MenuItem item)
		{
			if (this.Wnd != null)
			{
				this.Wnd.Invalidate(item.bounds);
			}
		}

		/// <summary>Merges the <see cref="T:System.Windows.Forms.MenuItem" /> objects of one menu with the current menu.</summary>
		/// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> whose menu items are merged with the menu items of the current menu. </param>
		/// <exception cref="T:System.ArgumentException">It was attempted to merge the menu with itself. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002665 RID: 9829 RVA: 0x00091730 File Offset: 0x0008F930
		public virtual void MergeMenu(Menu menuSrc)
		{
			if (menuSrc == this)
			{
				throw new ArgumentException("The menu cannot be merged with itself");
			}
			if (menuSrc == null)
			{
				return;
			}
			for (int i = 0; i < menuSrc.MenuItems.Count; i++)
			{
				MenuItem menuItem = menuSrc.MenuItems[i];
				switch (menuItem.MergeType)
				{
				case MenuMerge.Add:
				{
					int num = this.FindMergePosition(menuItem.MergeOrder);
					this.MenuItems.Add(num, menuItem.CloneMenu());
					break;
				}
				case MenuMerge.Replace:
				case MenuMerge.MergeItems:
				{
					for (int j = this.FindMergePosition(menuItem.MergeOrder - 1); j <= this.MenuItems.Count; j++)
					{
						if (j >= this.MenuItems.Count || this.MenuItems[j].MergeOrder != menuItem.MergeOrder)
						{
							this.MenuItems.Add(j, menuItem.CloneMenu());
							break;
						}
						MenuItem menuItem2 = this.MenuItems[j];
						if (menuItem2.MergeType != MenuMerge.Add)
						{
							if (menuItem.MergeType == MenuMerge.MergeItems && menuItem2.MergeType == MenuMerge.MergeItems)
							{
								menuItem2.MergeMenu(menuItem);
							}
							else
							{
								this.MenuItems.Remove(menuItem);
								this.MenuItems.Add(j, menuItem.CloneMenu());
							}
							break;
						}
					}
					break;
				}
				}
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x06002666 RID: 9830 RVA: 0x000918AC File Offset: 0x0008FAAC
		protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return this.tracker != null && this.tracker.ProcessKeys(ref msg, keyData);
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the <see cref="T:System.Windows.Forms.Menu" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Menu" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002667 RID: 9831 RVA: 0x000918C8 File Offset: 0x0008FAC8
		public override string ToString()
		{
			return base.ToString() + ", Items.Count: " + this.MenuItems.Count;
		}

		/// <summary>Specifies that the <see cref="M:System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)" /> method should search for a handle.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001332 RID: 4914
		public const int FindHandle = 0;

		/// <summary>Specifies that the <see cref="M:System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)" /> method should search for a shortcut.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001333 RID: 4915
		public const int FindShortcut = 1;

		// Token: 0x04001334 RID: 4916
		internal Menu.MenuItemCollection menu_items;

		// Token: 0x04001335 RID: 4917
		internal IntPtr menu_handle = IntPtr.Zero;

		// Token: 0x04001336 RID: 4918
		internal Menu parent_menu;

		// Token: 0x04001337 RID: 4919
		private Rectangle rect;

		// Token: 0x04001338 RID: 4920
		internal Control Wnd;

		// Token: 0x04001339 RID: 4921
		internal MenuTracker tracker;

		// Token: 0x0400133A RID: 4922
		private string control_name;

		// Token: 0x0400133B RID: 4923
		private object control_tag;

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
		// Token: 0x0200024A RID: 586
		[ListBindable(false)]
		public class MenuItemCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.Menu" /> that owns this collection. </param>
			// Token: 0x06002668 RID: 9832 RVA: 0x000918F8 File Offset: 0x0008FAF8
			public MenuItemCollection(Menu owner)
			{
				this.owner = owner;
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x1700097C RID: 2428
			// (get) Token: 0x06002669 RID: 9833 RVA: 0x00091914 File Offset: 0x0008FB14
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</returns>
			// Token: 0x1700097D RID: 2429
			// (get) Token: 0x0600266A RID: 9834 RVA: 0x00091918 File Offset: 0x0008FB18
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x1700097E RID: 2430
			// (get) Token: 0x0600266B RID: 9835 RVA: 0x0009191C File Offset: 0x0008FB1C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			// Token: 0x1700097F RID: 2431
			// (get) Token: 0x0600266C RID: 9836 RVA: 0x00091920 File Offset: 0x0008FB20
			// (set) Token: 0x0600266D RID: 9837 RVA: 0x00091930 File Offset: 0x0008FB30
			object IList.Item
			{
				get
				{
					return this.items[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The position into which the <see cref="T:System.Windows.Forms.MenuItem" /> was inserted.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to add to the collection.</param>
			// Token: 0x0600266E RID: 9838 RVA: 0x00091938 File Offset: 0x0008FB38
			int IList.Add(object value)
			{
				return this.Add((MenuItem)value);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <returns>true if the specified object is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise, false.</returns>
			/// <param name="value">The object to locate in the collection.</param>
			// Token: 0x0600266F RID: 9839 RVA: 0x00091948 File Offset: 0x0008FB48
			bool IList.Contains(object value)
			{
				return this.Contains((MenuItem)value);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <returns>The zero-based index if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise -1.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
			// Token: 0x06002670 RID: 9840 RVA: 0x00091958 File Offset: 0x0008FB58
			int IList.IndexOf(object value)
			{
				return this.IndexOf((MenuItem)value);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.MenuItem" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to insert into the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</param>
			// Token: 0x06002671 RID: 9841 RVA: 0x00091968 File Offset: 0x0008FB68
			void IList.Insert(int index, object value)
			{
				this.Insert(index, (MenuItem)value);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
			// Token: 0x06002672 RID: 9842 RVA: 0x00091978 File Offset: 0x0008FB78
			void IList.Remove(object value)
			{
				this.Remove((MenuItem)value);
			}

			/// <summary>Gets a value indicating the total number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</summary>
			/// <returns>The number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</returns>
			// Token: 0x17000980 RID: 2432
			// (get) Token: 0x06002673 RID: 9843 RVA: 0x00091988 File Offset: 0x0008FB88
			public int Count
			{
				get
				{
					return this.items.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x06002674 RID: 9844 RVA: 0x00091998 File Offset: 0x0008FB98
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Retrieves the <see cref="T:System.Windows.Forms.MenuItem" /> at the specified indexed location in the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified location.</returns>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.MenuItem" /> in the collection. </param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is null.or The <paramref name="index" /> parameter is less than zero.or The <paramref name="index" /> parameter is greater than the number of menu items in the collection, and the collection of menu items is not null. </exception>
			// Token: 0x17000982 RID: 2434
			public virtual MenuItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("Index of out range");
					}
					return (MenuItem)this.items[index];
				}
			}

			/// <summary>Gets an item with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> with the specified key.</returns>
			/// <param name="key">The name of the item to retrieve from the collection.</param>
			// Token: 0x17000983 RID: 2435
			public virtual MenuItem this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					foreach (object obj in this.items)
					{
						MenuItem menuItem = (MenuItem)obj;
						if (string.Compare(menuItem.Name, key, true) == 0)
						{
							return menuItem;
						}
					}
					return null;
				}
			}

			/// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu.</summary>
			/// <returns>The zero-based index where the item is stored in the collection.</returns>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add. </param>
			// Token: 0x06002677 RID: 9847 RVA: 0x00091A64 File Offset: 0x0008FC64
			public virtual int Add(MenuItem item)
			{
				if (item.Parent != null)
				{
					item.Parent.MenuItems.Remove(item);
				}
				this.items.Add(item);
				item.Index = this.items.Count - 1;
				this.UpdateItem(item);
				this.owner.OnMenuChanged(EventArgs.Empty);
				if (this.owner.parent_menu != null)
				{
					this.owner.parent_menu.OnMenuChanged(EventArgs.Empty);
				}
				return this.items.Count - 1;
			}

			// Token: 0x06002678 RID: 9848 RVA: 0x00091AF8 File Offset: 0x0008FCF8
			internal void AddNoEvents(MenuItem mi)
			{
				if (mi.Parent != null)
				{
					mi.Parent.MenuItems.Remove(mi);
				}
				this.items.Add(mi);
				mi.Index = this.items.Count - 1;
				mi.parent_menu = this.owner;
			}

			/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" />, to the end of the current menu, with a specified caption.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
			/// <param name="caption">The caption of the menu item. </param>
			// Token: 0x06002679 RID: 9849 RVA: 0x00091B50 File Offset: 0x0008FD50
			public virtual MenuItem Add(string caption)
			{
				MenuItem menuItem = new MenuItem(caption);
				this.Add(menuItem);
				return menuItem;
			}

			/// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index within the menu item collection.</summary>
			/// <returns>The zero-based index where the item is stored in the collection.</returns>
			/// <param name="index">The position to add the new item. </param>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add. </param>
			/// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.MenuItem" /> being added is already in use. </exception>
			/// <exception cref="T:System.ArgumentException">The index supplied in the <paramref name="index" /> parameter is larger than the size of the collection. </exception>
			// Token: 0x0600267A RID: 9850 RVA: 0x00091B70 File Offset: 0x0008FD70
			public virtual int Add(int index, MenuItem item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				ArrayList arrayList = new ArrayList(this.Count + 1);
				for (int i = 0; i < index; i++)
				{
					arrayList.Add(this.items[i]);
				}
				arrayList.Add(item);
				for (int j = index; j < this.Count; j++)
				{
					arrayList.Add(this.items[j]);
				}
				this.items = arrayList;
				this.UpdateItemsIndices();
				this.UpdateItem(item);
				return index;
			}

			// Token: 0x0600267B RID: 9851 RVA: 0x00091C18 File Offset: 0x0008FE18
			private void UpdateItem(MenuItem mi)
			{
				mi.parent_menu = this.owner;
				this.owner.OnMenuChanged(EventArgs.Empty);
				if (this.owner.parent_menu != null)
				{
					this.owner.parent_menu.OnMenuChanged(EventArgs.Empty);
				}
				if (this.owner.Tracker != null)
				{
					this.owner.Tracker.AddShortcuts(mi);
				}
			}

			// Token: 0x0600267C RID: 9852 RVA: 0x00091C88 File Offset: 0x0008FE88
			internal void Insert(int index, MenuItem mi)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				this.items.Insert(index, mi);
				this.UpdateItemsIndices();
				this.UpdateItem(mi);
			}

			/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu with a specified caption and a specified event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
			/// <param name="caption">The caption of the menu item. </param>
			/// <param name="onClick">An <see cref="T:System.EventHandler" /> that represents the event handler that is called when the item is clicked by the user, or when a user presses an accelerator or shortcut key for the menu item. </param>
			// Token: 0x0600267D RID: 9853 RVA: 0x00091CD0 File Offset: 0x0008FED0
			public virtual MenuItem Add(string caption, EventHandler onClick)
			{
				MenuItem menuItem = new MenuItem(caption, onClick);
				this.Add(menuItem);
				return menuItem;
			}

			/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" /> to the end of this menu with the specified caption, <see cref="E:System.Windows.Forms.MenuItem.Click" /> event handler, and items.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
			/// <param name="caption">The caption of the menu item. </param>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that this <see cref="T:System.Windows.Forms.MenuItem" /> will contain. </param>
			// Token: 0x0600267E RID: 9854 RVA: 0x00091CF0 File Offset: 0x0008FEF0
			public virtual MenuItem Add(string caption, MenuItem[] items)
			{
				MenuItem menuItem = new MenuItem(caption, items);
				this.Add(menuItem);
				return menuItem;
			}

			/// <summary>Adds an array of previously created <see cref="T:System.Windows.Forms.MenuItem" /> objects to the collection.</summary>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects representing the menu items to add to the collection. </param>
			// Token: 0x0600267F RID: 9855 RVA: 0x00091D10 File Offset: 0x0008FF10
			public virtual void AddRange(MenuItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (MenuItem menuItem in items)
				{
					this.Add(menuItem);
				}
			}

			/// <summary>Removes all <see cref="T:System.Windows.Forms.MenuItem" /> objects from the menu item collection.</summary>
			// Token: 0x06002680 RID: 9856 RVA: 0x00091D50 File Offset: 0x0008FF50
			public virtual void Clear()
			{
				MenuTracker tracker = this.owner.Tracker;
				foreach (object obj in this.items)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (tracker != null)
					{
						tracker.RemoveShortcuts(menuItem);
					}
					menuItem.parent_menu = null;
				}
				this.items.Clear();
				this.owner.OnMenuChanged(EventArgs.Empty);
			}

			/// <summary>Determines if the specified <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection; otherwise, false.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection. </param>
			// Token: 0x06002681 RID: 9857 RVA: 0x00091DF4 File Offset: 0x0008FFF4
			public bool Contains(MenuItem value)
			{
				return this.items.Contains(value);
			}

			/// <summary>Determines whether the collection contains an item with the specified key.</summary>
			/// <returns>true if the collection contains an item with the specified key, otherwise, false. </returns>
			/// <param name="key">The name of the item to look for.</param>
			// Token: 0x06002682 RID: 9858 RVA: 0x00091E04 File Offset: 0x00090004
			public virtual bool ContainsKey(string key)
			{
				return this[key] != null;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">The destination array. </param>
			/// <param name="index">The index in the destination array at which storing begins. </param>
			// Token: 0x06002683 RID: 9859 RVA: 0x00091E14 File Offset: 0x00090014
			public void CopyTo(Array dest, int index)
			{
				this.items.CopyTo(dest, index);
			}

			/// <summary>Finds the items with the specified key, optionally searching the submenu items</summary>
			/// <returns>An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects whose <see cref="P:System.Windows.Forms.Menu.Name" /> property matches the specified <paramref name="key" />. </returns>
			/// <param name="key">The name of the menu item to search for.</param>
			/// <param name="searchAllChildren">true to search child menu items; otherwise, false. </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="key" /> is null or an empty string.</exception>
			// Token: 0x06002684 RID: 9860 RVA: 0x00091E24 File Offset: 0x00090024
			public MenuItem[] Find(string key, bool searchAllChildren)
			{
				if (string.IsNullOrEmpty(key))
				{
					throw new ArgumentNullException("key");
				}
				List<MenuItem> list = new List<MenuItem>();
				foreach (object obj in this.items)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (string.Compare(menuItem.Name, key, true) == 0)
					{
						list.Add(menuItem);
					}
				}
				if (searchAllChildren)
				{
					foreach (object obj2 in this.items)
					{
						MenuItem menuItem2 = (MenuItem)obj2;
						list.AddRange(menuItem2.MenuItems.Find(key, true));
					}
				}
				return list.ToArray();
			}

			/// <summary>Returns an enumerator that can be used to iterate through the menu item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the menu item collection.</returns>
			// Token: 0x06002685 RID: 9861 RVA: 0x00091F44 File Offset: 0x00090144
			public IEnumerator GetEnumerator()
			{
				return this.items.GetEnumerator();
			}

			/// <summary>Retrieves the index of a specific item in the collection.</summary>
			/// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection. </param>
			// Token: 0x06002686 RID: 9862 RVA: 0x00091F54 File Offset: 0x00090154
			public int IndexOf(MenuItem value)
			{
				return this.items.IndexOf(value);
			}

			/// <summary>Finds the index of the first occurrence of a menu item with the specified key.</summary>
			/// <returns>The zero-based index of the first menu item with the specified key.</returns>
			/// <param name="key">The name of the menu item to search for.</param>
			// Token: 0x06002687 RID: 9863 RVA: 0x00091F64 File Offset: 0x00090164
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				return this.IndexOf(this[key]);
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection.</summary>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove. </param>
			// Token: 0x06002688 RID: 9864 RVA: 0x00091F80 File Offset: 0x00090180
			public virtual void Remove(MenuItem item)
			{
				this.RemoveAt(item.Index);
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection at a specified index.</summary>
			/// <param name="index">The index of the <see cref="T:System.Windows.Forms.MenuItem" /> to remove. </param>
			// Token: 0x06002689 RID: 9865 RVA: 0x00091F90 File Offset: 0x00090190
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				MenuItem menuItem = (MenuItem)this.items[index];
				MenuTracker tracker = this.owner.Tracker;
				if (tracker != null)
				{
					tracker.RemoveShortcuts(menuItem);
				}
				menuItem.parent_menu = null;
				this.items.RemoveAt(index);
				this.UpdateItemsIndices();
				this.owner.OnMenuChanged(EventArgs.Empty);
			}

			/// <summary>Removes the menu item with the specified key from the collection.</summary>
			/// <param name="key">The name of the menu item to remove.</param>
			// Token: 0x0600268A RID: 9866 RVA: 0x00092010 File Offset: 0x00090210
			public virtual void RemoveByKey(string key)
			{
				this.Remove(this[key]);
			}

			// Token: 0x0600268B RID: 9867 RVA: 0x00092020 File Offset: 0x00090220
			private void UpdateItemsIndices()
			{
				for (int i = 0; i < this.Count; i++)
				{
					((MenuItem)this.items[i]).Index = i;
				}
			}

			// Token: 0x0400133D RID: 4925
			private Menu owner;

			// Token: 0x0400133E RID: 4926
			private ArrayList items = new ArrayList();
		}
	}
}
