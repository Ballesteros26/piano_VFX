using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the navigation and manipulation user interface (UI) for controls on a form that are bound to data.</summary>
	// Token: 0x02000061 RID: 97
	[Designer("System.Windows.Forms.Design.BindingNavigatorDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[DefaultProperty("BindingSource")]
	[DefaultEvent("RefreshItems")]
	[ComVisible(true)]
	public class BindingNavigator : ToolStrip, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class.</summary>
		// Token: 0x060003CD RID: 973 RVA: 0x000134D8 File Offset: 0x000116D8
		[EditorBrowsable(1)]
		public BindingNavigator()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class with the specified <see cref="T:System.Windows.Forms.BindingSource" /> as the data source.</summary>
		/// <param name="bindingSource">The <see cref="T:System.Windows.Forms.BindingSource" /> used as a data source.</param>
		// Token: 0x060003CE RID: 974 RVA: 0x000134E4 File Offset: 0x000116E4
		public BindingNavigator(BindingSource bindingSource)
		{
			this.countItemFormat = Locale.GetText("of {0}");
			base..ctor();
			this.AttachNewSource(bindingSource);
			this.AddStandardItems();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class, indicating whether to display the standard navigation user interface (UI).</summary>
		/// <param name="addStandardItems">true to show the standard navigational UI; otherwise, false.</param>
		// Token: 0x060003CF RID: 975 RVA: 0x0001350C File Offset: 0x0001170C
		public BindingNavigator(bool addStandardItems)
		{
			this.countItemFormat = Locale.GetText("of {0}");
			base..ctor();
			this.bindingSource = null;
			if (addStandardItems)
			{
				this.AddStandardItems();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class and adds this new instance to the specified container.</summary>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to add the new <see cref="T:System.Windows.Forms.BindingNavigator" /> control to.</param>
		// Token: 0x060003D0 RID: 976 RVA: 0x00013538 File Offset: 0x00011738
		[EditorBrowsable(1)]
		public BindingNavigator(IContainer container)
		{
			this.countItemFormat = Locale.GetText("of {0}");
			base..ctor();
			this.bindingSource = null;
			container.Add(this);
		}

		/// <summary>Occurs when the state of the navigational user interface (UI) needs to be refreshed to reflect the current state of the underlying data.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060003D1 RID: 977 RVA: 0x0001356C File Offset: 0x0001176C
		// (remove) Token: 0x060003D2 RID: 978 RVA: 0x00013588 File Offset: 0x00011788
		public event EventHandler RefreshItems;

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Add New button.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Add New button for the <see cref="T:System.Windows.Forms.BindingSource" />. The default is null.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000135A4 File Offset: 0x000117A4
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x000135AC File Offset: 0x000117AC
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripItem AddNewItem
		{
			get
			{
				return this.addNewItem;
			}
			set
			{
				this.ReplaceItem(ref this.addNewItem, value, new EventHandler(this.OnAddNew));
				this.OnRefreshItems();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingSource" /> component that is the source of data.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.BindingSource" /> component associated with this <see cref="T:System.Windows.Forms.BindingNavigator" />. The default is null.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000135D0 File Offset: 0x000117D0
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x000135D8 File Offset: 0x000117D8
		[TypeConverter(typeof(ReferenceConverter))]
		[DefaultValue(null)]
		public BindingSource BindingSource
		{
			get
			{
				return this.bindingSource;
			}
			set
			{
				this.AttachNewSource(value);
				this.OnRefreshItems();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the total number of items in the associated <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the total number of items in the associated <see cref="T:System.Windows.Forms.BindingSource" />. </returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000135E8 File Offset: 0x000117E8
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x000135F0 File Offset: 0x000117F0
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripItem CountItem
		{
			get
			{
				return this.countItem;
			}
			set
			{
				this.countItem = value;
				this.OnRefreshItems();
			}
		}

		/// <summary>Gets or sets a string used to format the information displayed in the <see cref="P:System.Windows.Forms.BindingNavigator.CountItem" /> control. </summary>
		/// <returns>The format <see cref="T:System.String" /> used to format the item count. The default is the string "of {0}".</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00013600 File Offset: 0x00011800
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00013608 File Offset: 0x00011808
		public string CountItemFormat
		{
			get
			{
				return this.countItemFormat;
			}
			set
			{
				this.countItemFormat = value;
				this.OnRefreshItems();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Delete functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Delete button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00013618 File Offset: 0x00011818
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00013620 File Offset: 0x00011820
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripItem DeleteItem
		{
			get
			{
				return this.deleteItem;
			}
			set
			{
				this.ReplaceItem(ref this.deleteItem, value, new EventHandler(this.OnDelete));
				this.OnRefreshItems();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move First functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move First button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00013644 File Offset: 0x00011844
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0001364C File Offset: 0x0001184C
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripItem MoveFirstItem
		{
			get
			{
				return this.moveFirstItem;
			}
			set
			{
				this.ReplaceItem(ref this.moveFirstItem, value, new EventHandler(this.OnMoveFirst));
				this.OnRefreshItems();
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00013670 File Offset: 0x00011870
		private void ReplaceItem(ref ToolStripItem existingItem, ToolStripItem newItem, EventHandler clickHandler)
		{
			if (existingItem != null)
			{
				existingItem.Click -= clickHandler;
			}
			if (newItem != null)
			{
				newItem.Click += clickHandler;
			}
			existingItem = newItem;
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Last functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Last button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00013694 File Offset: 0x00011894
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0001369C File Offset: 0x0001189C
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripItem MoveLastItem
		{
			get
			{
				return this.moveLastItem;
			}
			set
			{
				this.ReplaceItem(ref this.moveLastItem, value, new EventHandler(this.OnMoveLast));
				this.OnRefreshItems();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Next functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Next button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x000136C0 File Offset: 0x000118C0
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x000136C8 File Offset: 0x000118C8
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripItem MoveNextItem
		{
			get
			{
				return this.moveNextItem;
			}
			set
			{
				this.ReplaceItem(ref this.moveNextItem, value, new EventHandler(this.OnMoveNext));
				this.OnRefreshItems();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Previous functionality.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Previous button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x000136EC File Offset: 0x000118EC
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x000136F4 File Offset: 0x000118F4
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripItem MovePreviousItem
		{
			get
			{
				return this.movePreviousItem;
			}
			set
			{
				this.ReplaceItem(ref this.movePreviousItem, value, new EventHandler(this.OnMovePrevious));
				this.OnRefreshItems();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the current position within the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the current position.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00013718 File Offset: 0x00011918
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00013720 File Offset: 0x00011920
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripItem PositionItem
		{
			get
			{
				return this.positionItem;
			}
			set
			{
				this.positionItem = value;
				this.OnRefreshItems();
			}
		}

		/// <summary>Adds the standard set of navigation items to the <see cref="T:System.Windows.Forms.BindingNavigator" /> control.</summary>
		// Token: 0x060003E8 RID: 1000 RVA: 0x00013730 File Offset: 0x00011930
		public virtual void AddStandardItems()
		{
			this.BeginInit();
			this.MoveFirstItem = new ToolStripButton();
			this.moveFirstItem.Image = ResourceImageLoader.Get("nav_first.png");
			this.moveFirstItem.ToolTipText = Locale.GetText("Move first");
			this.Items.Add(this.moveFirstItem);
			this.MovePreviousItem = new ToolStripButton();
			this.movePreviousItem.Image = ResourceImageLoader.Get("nav_previous.png");
			this.movePreviousItem.ToolTipText = Locale.GetText("Move previous");
			this.Items.Add(this.movePreviousItem);
			this.Items.Add(new ToolStripSeparator());
			this.PositionItem = new ToolStripTextBox();
			this.positionItem.Width = 50;
			this.positionItem.Text = ((this.bindingSource != null) ? 1 : 0).ToString();
			this.positionItem.Width = 50;
			this.positionItem.ToolTipText = Locale.GetText("Current position");
			this.Items.Add(this.positionItem);
			this.CountItem = new ToolStripLabel();
			this.countItem.ToolTipText = Locale.GetText("Total number of items");
			this.countItem.Text = Locale.GetText(this.countItemFormat, new object[] { (this.bindingSource != null) ? this.bindingSource.Count : 0 });
			this.Items.Add(this.countItem);
			this.Items.Add(new ToolStripSeparator());
			this.MoveNextItem = new ToolStripButton();
			this.moveNextItem.Image = ResourceImageLoader.Get("nav_next.png");
			this.moveNextItem.ToolTipText = Locale.GetText("Move next");
			this.Items.Add(this.moveNextItem);
			this.MoveLastItem = new ToolStripButton();
			this.moveLastItem.Image = ResourceImageLoader.Get("nav_end.png");
			this.moveLastItem.ToolTipText = Locale.GetText("Move last");
			this.Items.Add(this.moveLastItem);
			this.Items.Add(new ToolStripSeparator());
			this.AddNewItem = new ToolStripButton();
			this.addNewItem.Image = ResourceImageLoader.Get("nav_plus.png");
			this.addNewItem.ToolTipText = Locale.GetText("Add new");
			this.Items.Add(this.addNewItem);
			this.DeleteItem = new ToolStripButton();
			this.deleteItem.Image = ResourceImageLoader.Get("nav_delete.png");
			this.deleteItem.ToolTipText = Locale.GetText("Delete");
			this.Items.Add(this.deleteItem);
			this.EndInit();
		}

		/// <summary>Disables updates to the <see cref="T:System.Windows.Forms.ToolStripItem" /> controls of the <see cref="T:System.Windows.Forms.BindingNavigator" /> during the component's initialization.</summary>
		// Token: 0x060003E9 RID: 1001 RVA: 0x00013A0C File Offset: 0x00011C0C
		public void BeginInit()
		{
			this.initFlag = true;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.BindingNavigator" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060003EA RID: 1002 RVA: 0x00013A18 File Offset: 0x00011C18
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Enables updates to the <see cref="T:System.Windows.Forms.ToolStripItem" /> controls of the <see cref="T:System.Windows.Forms.BindingNavigator" /> after the component's initialization has concluded.</summary>
		// Token: 0x060003EB RID: 1003 RVA: 0x00013A24 File Offset: 0x00011C24
		public void EndInit()
		{
			this.initFlag = false;
			this.OnRefreshItems();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingNavigator.RefreshItems" /> event.</summary>
		// Token: 0x060003EC RID: 1004 RVA: 0x00013A34 File Offset: 0x00011C34
		protected virtual void OnRefreshItems()
		{
			if (this.initFlag)
			{
				return;
			}
			if (this.RefreshItems != null)
			{
				this.RefreshItems.Invoke(this, EventArgs.Empty);
			}
			this.RefreshItemsCore();
		}

		/// <summary>Refreshes the state of the standard items to reflect the current state of the data.</summary>
		// Token: 0x060003ED RID: 1005 RVA: 0x00013A70 File Offset: 0x00011C70
		[EditorBrowsable(2)]
		protected virtual void RefreshItemsCore()
		{
			try
			{
				bool flag = this.bindingSource != null;
				this.initFlag = true;
				if (this.addNewItem != null)
				{
					this.addNewItem.Enabled = flag && this.bindingSource.AllowNew;
				}
				if (this.moveFirstItem != null)
				{
					this.moveFirstItem.Enabled = flag && this.bindingSource.Position > 0;
				}
				if (this.moveLastItem != null)
				{
					this.moveLastItem.Enabled = flag && this.bindingSource.Position < this.bindingSource.Count - 1;
				}
				if (this.moveNextItem != null)
				{
					this.moveNextItem.Enabled = flag && this.bindingSource.Position < this.bindingSource.Count - 1;
				}
				if (this.movePreviousItem != null)
				{
					this.movePreviousItem.Enabled = flag && this.bindingSource.Position > 0;
				}
				if (this.deleteItem != null)
				{
					this.deleteItem.Enabled = flag && this.bindingSource.Count != 0 && this.bindingSource.AllowRemove;
				}
				if (this.countItem != null)
				{
					this.countItem.Text = string.Format(this.countItemFormat, (!flag) ? 0 : this.bindingSource.Count);
					this.countItem.Enabled = flag && this.bindingSource.Count > 0;
				}
				if (this.positionItem != null)
				{
					this.positionItem.Text = string.Format("{0}", (!flag) ? 0 : (this.bindingSource.Position + 1));
					this.positionItem.Enabled = flag && this.bindingSource.Count > 0;
				}
			}
			finally
			{
				this.initFlag = false;
			}
		}

		/// <summary>Causes form validation to occur and returns whether validation was successful.</summary>
		/// <returns>true if validation was successful and focus can shift to the <see cref="T:System.Windows.Forms.BindingNavigator" />; otherwise, false.</returns>
		// Token: 0x060003EE RID: 1006 RVA: 0x00013CAC File Offset: 0x00011EAC
		[MonoTODO("Not implemented, will throw NotImplementedException")]
		public bool Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00013CB4 File Offset: 0x00011EB4
		private void AttachNewSource(BindingSource source)
		{
			if (this.bindingSource != null)
			{
				this.bindingSource.ListChanged -= new ListChangedEventHandler(this.OnListChanged);
				this.bindingSource.PositionChanged -= new EventHandler(this.OnPositionChanged);
				this.bindingSource.AddingNew -= new AddingNewEventHandler(this.OnAddingNew);
			}
			this.bindingSource = source;
			if (this.bindingSource != null)
			{
				this.bindingSource.ListChanged += new ListChangedEventHandler(this.OnListChanged);
				this.bindingSource.PositionChanged += new EventHandler(this.OnPositionChanged);
				this.bindingSource.AddingNew += new AddingNewEventHandler(this.OnAddingNew);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013D68 File Offset: 0x00011F68
		private void OnAddNew(object sender, EventArgs e)
		{
			if (this.bindingSource != null)
			{
				this.bindingSource.AddNew();
			}
			this.OnRefreshItems();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013D88 File Offset: 0x00011F88
		private void OnAddingNew(object sender, AddingNewEventArgs e)
		{
			this.OnRefreshItems();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013D90 File Offset: 0x00011F90
		private void OnDelete(object sender, EventArgs e)
		{
			if (this.bindingSource != null)
			{
				this.bindingSource.RemoveCurrent();
			}
			this.OnRefreshItems();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013DB0 File Offset: 0x00011FB0
		private void OnListChanged(object sender, ListChangedEventArgs e)
		{
			this.OnRefreshItems();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013DB8 File Offset: 0x00011FB8
		private void OnMoveFirst(object sender, EventArgs e)
		{
			if (this.bindingSource != null)
			{
				this.bindingSource.MoveFirst();
			}
			this.OnRefreshItems();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013DD8 File Offset: 0x00011FD8
		private void OnMoveLast(object sender, EventArgs e)
		{
			if (this.bindingSource != null)
			{
				this.bindingSource.MoveLast();
			}
			this.OnRefreshItems();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00013DF8 File Offset: 0x00011FF8
		private void OnMoveNext(object sender, EventArgs e)
		{
			if (this.bindingSource != null)
			{
				this.bindingSource.MoveNext();
			}
			this.OnRefreshItems();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013E18 File Offset: 0x00012018
		private void OnMovePrevious(object sender, EventArgs e)
		{
			if (this.bindingSource != null)
			{
				this.bindingSource.MovePrevious();
			}
			this.OnRefreshItems();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013E38 File Offset: 0x00012038
		private void OnPositionChanged(object sender, EventArgs e)
		{
			this.OnRefreshItems();
		}

		// Token: 0x0400063E RID: 1598
		private ToolStripItem addNewItem;

		// Token: 0x0400063F RID: 1599
		private BindingSource bindingSource;

		// Token: 0x04000640 RID: 1600
		private ToolStripItem countItem;

		// Token: 0x04000641 RID: 1601
		private string countItemFormat;

		// Token: 0x04000642 RID: 1602
		private ToolStripItem deleteItem;

		// Token: 0x04000643 RID: 1603
		private bool initFlag;

		// Token: 0x04000644 RID: 1604
		private ToolStripItem moveFirstItem;

		// Token: 0x04000645 RID: 1605
		private ToolStripItem moveLastItem;

		// Token: 0x04000646 RID: 1606
		private ToolStripItem moveNextItem;

		// Token: 0x04000647 RID: 1607
		private ToolStripItem movePreviousItem;

		// Token: 0x04000648 RID: 1608
		private ToolStripItem positionItem;
	}
}
