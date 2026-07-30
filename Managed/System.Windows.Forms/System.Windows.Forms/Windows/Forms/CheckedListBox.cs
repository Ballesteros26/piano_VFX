using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Displays a <see cref="T:System.Windows.Forms.ListBox" /> in which a check box is displayed to the left of each item.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000077 RID: 119
	[LookupBindingProperties]
	[ClassInterface(1)]
	[ComVisible(true)]
	public class CheckedListBox : ListBox
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckedListBox" /> class.</summary>
		// Token: 0x0600053D RID: 1341 RVA: 0x000173E8 File Offset: 0x000155E8
		public CheckedListBox()
		{
			this.checked_indices = new CheckedListBox.CheckedIndexCollection(this);
			this.checked_items = new CheckedListBox.CheckedItemCollection(this);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00017424 File Offset: 0x00015624
		// Note: this type is marked as 'beforefieldinit'.
		static CheckedListBox()
		{
			CheckedListBox.ItemCheckEvent = new object();
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x0600053F RID: 1343 RVA: 0x00017430 File Offset: 0x00015630
		// (remove) Token: 0x06000540 RID: 1344 RVA: 0x0001743C File Offset: 0x0001563C
		[EditorBrowsable(0)]
		[Browsable(true)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.DataSource" /> property changes. This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000541 RID: 1345 RVA: 0x00017448 File Offset: 0x00015648
		// (remove) Token: 0x06000542 RID: 1346 RVA: 0x00017454 File Offset: 0x00015654
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DataSourceChanged
		{
			add
			{
				base.DataSourceChanged += value;
			}
			remove
			{
				base.DataSourceChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.DisplayMember" /> property changes. This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000543 RID: 1347 RVA: 0x00017460 File Offset: 0x00015660
		// (remove) Token: 0x06000544 RID: 1348 RVA: 0x0001746C File Offset: 0x0001566C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DisplayMemberChanged
		{
			add
			{
				base.DisplayMemberChanged += value;
			}
			remove
			{
				base.DisplayMemberChanged -= value;
			}
		}

		/// <summary>Occurs when a visual aspect of an owner-drawn <see cref="T:System.Windows.Forms.CheckedListBox" /> changes. This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000545 RID: 1349 RVA: 0x00017478 File Offset: 0x00015678
		// (remove) Token: 0x06000546 RID: 1350 RVA: 0x00017484 File Offset: 0x00015684
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event DrawItemEventHandler DrawItem
		{
			add
			{
				base.DrawItem += value;
			}
			remove
			{
				base.DrawItem -= value;
			}
		}

		/// <summary>Occurs when an owner-drawn <see cref="T:System.Windows.Forms.ListBox" /> is created and the sizes of the list items are determined. This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000547 RID: 1351 RVA: 0x00017490 File Offset: 0x00015690
		// (remove) Token: 0x06000548 RID: 1352 RVA: 0x0001749C File Offset: 0x0001569C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.MeasureItem += value;
			}
			remove
			{
				base.MeasureItem -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.ValueMember" /> property changes. This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06000549 RID: 1353 RVA: 0x000174A8 File Offset: 0x000156A8
		// (remove) Token: 0x0600054A RID: 1354 RVA: 0x000174B4 File Offset: 0x000156B4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler ValueMemberChanged
		{
			add
			{
				base.ValueMemberChanged += value;
			}
			remove
			{
				base.ValueMemberChanged -= value;
			}
		}

		/// <summary>Occurs when the checked state of an item changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x0600054B RID: 1355 RVA: 0x000174C0 File Offset: 0x000156C0
		// (remove) Token: 0x0600054C RID: 1356 RVA: 0x000174D4 File Offset: 0x000156D4
		public event ItemCheckEventHandler ItemCheck
		{
			add
			{
				base.Events.AddHandler(CheckedListBox.ItemCheckEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckedListBox.ItemCheckEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.CheckedListBox" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x0600054D RID: 1357 RVA: 0x000174E8 File Offset: 0x000156E8
		// (remove) Token: 0x0600054E RID: 1358 RVA: 0x000174F4 File Offset: 0x000156F4
		[EditorBrowsable(0)]
		[Browsable(true)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		/// <summary>Collection of checked indexes in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> collection for the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00017500 File Offset: 0x00015700
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public CheckedListBox.CheckedIndexCollection CheckedIndices
		{
			get
			{
				return this.checked_indices;
			}
		}

		/// <summary>Collection of checked items in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> collection for the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00017508 File Offset: 0x00015708
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public CheckedListBox.CheckedItemCollection CheckedItems
		{
			get
			{
				return this.checked_items;
			}
		}

		/// <summary>Gets or sets a value indicating whether the check box should be toggled when an item is selected.</summary>
		/// <returns>true if the check mark is applied immediately; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00017510 File Offset: 0x00015710
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00017518 File Offset: 0x00015718
		[DefaultValue(false)]
		public bool CheckOnClick
		{
			get
			{
				return this.check_onclick;
			}
			set
			{
				this.check_onclick = value;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required parameters.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00017524 File Offset: 0x00015724
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets or sets the data source for the control. This property is not relevant for this class.</summary>
		/// <returns>An object representing the source of the data.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001752C File Offset: 0x0001572C
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00017534 File Offset: 0x00015734
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
			}
		}

		/// <summary>Gets or sets a string that specifies a property of the objects contained in the list box whose contents you want to display.</summary>
		/// <returns>A string that specifies the name of a property of the objects contained in the list box. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00017540 File Offset: 0x00015740
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00017548 File Offset: 0x00015748
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new string DisplayMember
		{
			get
			{
				return base.DisplayMember;
			}
			set
			{
				base.DisplayMember = value;
			}
		}

		/// <summary>Gets a value indicating the mode for drawing elements of the <see cref="T:System.Windows.Forms.CheckedListBox" />. This property is not relevant to this class.</summary>
		/// <returns>Always a <see cref="T:System.Windows.Forms.DrawMode" /> of Normal.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00017554 File Offset: 0x00015754
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00017558 File Offset: 0x00015758
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override DrawMode DrawMode
		{
			get
			{
				return DrawMode.Normal;
			}
			set
			{
			}
		}

		/// <summary>Gets the height of the item area.</summary>
		/// <returns>The height, in pixels, of the item area.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001755C File Offset: 0x0001575C
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00017564 File Offset: 0x00015764
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override int ItemHeight
		{
			get
			{
				return base.ItemHeight;
			}
			set
			{
			}
		}

		/// <summary>Gets the collection of items in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> collection representing the items in the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00017568 File Offset: 0x00015768
		[Localizable(true)]
		[DesignerSerializationVisibility(2)]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new CheckedListBox.ObjectCollection Items
		{
			get
			{
				return (CheckedListBox.ObjectCollection)base.Items;
			}
		}

		/// <summary>Gets or sets a value specifying the selection mode.</summary>
		/// <returns>Either the One or None value of <see cref="T:System.Windows.Forms.SelectionMode" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to assign a value that is not a <see cref="T:System.Windows.Forms.SelectionMode" /> value of One or None. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An attempt was made to assign the MultiExtended value of <see cref="T:System.Windows.Forms.SelectionMode" /> to the control.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00017578 File Offset: 0x00015778
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00017580 File Offset: 0x00015780
		public override SelectionMode SelectionMode
		{
			get
			{
				return base.SelectionMode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SelectionMode), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SelectionMode));
				}
				if (value == SelectionMode.MultiSimple || value == SelectionMode.MultiExtended)
				{
					throw new ArgumentException("Multi selection not supported on CheckedListBox");
				}
				base.SelectionMode = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the check boxes have a <see cref="T:System.Windows.Forms.ButtonState" /> of Flat or Normal.</summary>
		/// <returns>true if the check box has a flat appearance; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000175E0 File Offset: 0x000157E0
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x000175E8 File Offset: 0x000157E8
		[DefaultValue(false)]
		public bool ThreeDCheckBoxes
		{
			get
			{
				return this.three_dcheckboxes;
			}
			set
			{
				if (this.three_dcheckboxes == value)
				{
					return;
				}
				this.three_dcheckboxes = value;
				this.Refresh();
			}
		}

		/// <summary>Gets or sets a string that specifies the property of the data source from which to draw the value. This property is not relevant to this class.</summary>
		/// <returns>A string that specifies the property of the data source from which to draw the value.</returns>
		/// <exception cref="T:System.ArgumentException">The specified property cannot be found on the object specified by the <see cref="P:System.Windows.Forms.CheckedListBox.DataSource" /> property.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00017604 File Offset: 0x00015804
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001760C File Offset: 0x0001580C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new string ValueMember
		{
			get
			{
				return base.ValueMember;
			}
			set
			{
				base.ValueMember = value;
			}
		}

		/// <summary>Gets or sets padding within the <see cref="T:System.Windows.Forms.CheckedListBox" />. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00017618 File Offset: 0x00015818
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00017620 File Offset: 0x00015820
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06000565 RID: 1381 RVA: 0x0001762C File Offset: 0x0001582C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return base.CreateAccessibilityInstance();
		}

		/// <returns>A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that represents the new item collection.</returns>
		// Token: 0x06000566 RID: 1382 RVA: 0x00017634 File Offset: 0x00015834
		protected override ListBox.ObjectCollection CreateItemCollection()
		{
			return new CheckedListBox.ObjectCollection(this);
		}

		/// <summary>Returns a value indicating whether the specified item is checked.</summary>
		/// <returns>true if the item is checked; otherwise, false.</returns>
		/// <param name="index">The index of the item. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> specified is less than zero.-or- The <paramref name="index" /> specified is greater than or equal to the count of items in the list. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000567 RID: 1383 RVA: 0x0001763C File Offset: 0x0001583C
		public bool GetItemChecked(int index)
		{
			return this.check_states.Contains(this.Items[index]);
		}

		/// <summary>Returns a value indicating the check state of the current item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
		/// <param name="index">The index of the item to get the checked value of. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is less than zero.-or- The <paramref name="index" /> specified is greater than or equal to the count of items in the list. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000568 RID: 1384 RVA: 0x00017658 File Offset: 0x00015858
		public CheckState GetItemCheckState(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			object obj = this.Items[index];
			if (this.check_states.Contains(obj))
			{
				return (CheckState)((int)this.check_states[obj]);
			}
			return CheckState.Unchecked;
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000569 RID: 1385 RVA: 0x000176BC File Offset: 0x000158BC
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600056A RID: 1386 RVA: 0x000176C8 File Offset: 0x000158C8
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.DrawItem" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> object with the details </param>
		// Token: 0x0600056B RID: 1387 RVA: 0x000176D4 File Offset: 0x000158D4
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.check_states.Contains(this.Items[e.Index]))
			{
				DrawItemState drawItemState = e.State | DrawItemState.Checked;
				if ((int)this.check_states[this.Items[e.Index]] == 2)
				{
					drawItemState |= DrawItemState.Inactive;
				}
				e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, drawItemState, e.ForeColor, e.BackColor);
			}
			ThemeEngine.Current.DrawCheckedListBoxItem(this, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600056C RID: 1388 RVA: 0x00017774 File Offset: 0x00015974
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600056D RID: 1389 RVA: 0x00017780 File Offset: 0x00015980
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.ItemCheck" /> event.</summary>
		/// <param name="ice">An <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> that contains the event data.</param>
		// Token: 0x0600056E RID: 1390 RVA: 0x0001778C File Offset: 0x0001598C
		protected virtual void OnItemCheck(ItemCheckEventArgs ice)
		{
			ItemCheckEventHandler itemCheckEventHandler = (ItemCheckEventHandler)base.Events[CheckedListBox.ItemCheckEvent];
			if (itemCheckEventHandler != null)
			{
				itemCheckEventHandler(this, ice);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that was raised. </param>
		// Token: 0x0600056F RID: 1391 RVA: 0x000177C0 File Offset: 0x000159C0
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (e.KeyChar == ' ' && base.FocusedItem != -1)
			{
				this.SetItemChecked(base.FocusedItem, !this.GetItemChecked(base.FocusedItem));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.MeasureItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data. </param>
		// Token: 0x06000570 RID: 1392 RVA: 0x00017808 File Offset: 0x00015A08
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			base.OnMeasureItem(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListBox.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000571 RID: 1393 RVA: 0x00017814 File Offset: 0x00015A14
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
		}

		/// <summary>Parses all <see cref="T:System.Windows.Forms.CheckedListBox" /> items again and gets new text strings for the items.</summary>
		// Token: 0x06000572 RID: 1394 RVA: 0x00017820 File Offset: 0x00015A20
		protected override void RefreshItems()
		{
			base.RefreshItems();
		}

		/// <summary>Sets <see cref="T:System.Windows.Forms.CheckState" /> for the item at the specified index to Checked.</summary>
		/// <param name="index">The index of the item to set the check state for. </param>
		/// <param name="value">true to set the item as checked; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The index specified is less than zero.-or- The index is greater than the count of items in the list. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000573 RID: 1395 RVA: 0x00017828 File Offset: 0x00015A28
		public void SetItemChecked(int index, bool value)
		{
			this.SetItemCheckState(index, (!value) ? CheckState.Unchecked : CheckState.Checked);
		}

		/// <summary>Sets the check state of the item at the specified index.</summary>
		/// <param name="index">The index of the item to set the state for. </param>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.CheckState" /> values. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is less than zero.-or- The <paramref name="index" /> is greater than or equal to the count of items in the list. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="value" /> is not one of the <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000574 RID: 1396 RVA: 0x00017840 File Offset: 0x00015A40
		public void SetItemCheckState(int index, CheckState value)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			if (!Enum.IsDefined(typeof(CheckState), value))
			{
				throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for CheckState", value));
			}
			CheckState itemCheckState = this.GetItemCheckState(index);
			if (itemCheckState == value)
			{
				return;
			}
			ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(index, value, itemCheckState);
			this.OnItemCheck(itemCheckEventArgs);
			switch (itemCheckEventArgs.NewValue)
			{
			case CheckState.Unchecked:
				this.check_states.Remove(this.Items[index]);
				break;
			case CheckState.Checked:
			case CheckState.Indeterminate:
				this.check_states[this.Items[index]] = itemCheckEventArgs.NewValue;
				break;
			}
			this.UpdateCollections();
			this.InvalidateCheckbox(index);
		}

		/// <summary>Processes the command message the <see cref="T:System.Windows.Forms.CheckedListBox" /> control receives from the top-level window.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> the top-level window sent to the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</param>
		// Token: 0x06000575 RID: 1397 RVA: 0x00017934 File Offset: 0x00015B34
		protected override void WmReflectCommand(ref Message m)
		{
			base.WmReflectCommand(ref m);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000576 RID: 1398 RVA: 0x00017940 File Offset: 0x00015B40
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001794C File Offset: 0x00015B4C
		internal override void OnItemClick(int index)
		{
			if ((this.CheckOnClick || this.last_clicked_index == index) && index > -1)
			{
				if (this.GetItemChecked(index))
				{
					this.SetItemCheckState(index, CheckState.Unchecked);
				}
				else
				{
					this.SetItemCheckState(index, CheckState.Checked);
				}
			}
			this.last_clicked_index = index;
			base.OnItemClick(index);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000179A8 File Offset: 0x00015BA8
		internal override void CollectionChanged()
		{
			base.CollectionChanged();
			this.UpdateCollections();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000179B8 File Offset: 0x00015BB8
		private void InvalidateCheckbox(int index)
		{
			Rectangle itemDisplayRectangle = base.GetItemDisplayRectangle(index, base.TopIndex);
			itemDisplayRectangle.X += 2;
			itemDisplayRectangle.Y += (itemDisplayRectangle.Height - 11) / 2;
			itemDisplayRectangle.Width = 11;
			itemDisplayRectangle.Height = 11;
			base.Invalidate(itemDisplayRectangle);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00017A18 File Offset: 0x00015C18
		private void UpdateCollections()
		{
			this.CheckedItems.Refresh();
			this.CheckedIndices.Refresh();
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00017A30 File Offset: 0x00015C30
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00017A38 File Offset: 0x00015C38
		[DefaultValue(false)]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return this.use_compatible_text_rendering;
			}
			set
			{
				this.use_compatible_text_rendering = value;
			}
		}

		// Token: 0x040006C7 RID: 1735
		private CheckedListBox.CheckedIndexCollection checked_indices;

		// Token: 0x040006C8 RID: 1736
		private CheckedListBox.CheckedItemCollection checked_items;

		// Token: 0x040006C9 RID: 1737
		private Hashtable check_states = new Hashtable();

		// Token: 0x040006CA RID: 1738
		private bool check_onclick;

		// Token: 0x040006CB RID: 1739
		private bool three_dcheckboxes;

		// Token: 0x040006CD RID: 1741
		private int last_clicked_index = -1;

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		// Token: 0x02000078 RID: 120
		public new class ObjectCollection : ListBox.ObjectCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.CheckedListBox" /> that owns the collection. </param>
			// Token: 0x0600057D RID: 1405 RVA: 0x00017A44 File Offset: 0x00015C44
			public ObjectCollection(CheckedListBox owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.CheckedListBox" />, specifying the object to add and whether it is checked.</summary>
			/// <returns>The index of the newly added item.</returns>
			/// <param name="item">An object representing the item to add to the collection. </param>
			/// <param name="isChecked">true to check the item; otherwise, false. </param>
			// Token: 0x0600057E RID: 1406 RVA: 0x00017A54 File Offset: 0x00015C54
			public int Add(object item, bool isChecked)
			{
				return this.Add(item, (!isChecked) ? CheckState.Unchecked : CheckState.Checked);
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.CheckedListBox" />, specifying the object to add and the initial checked value.</summary>
			/// <returns>The index of the newly added item.</returns>
			/// <param name="item">An object representing the item to add to the collection. </param>
			/// <param name="check">The initial <see cref="T:System.Windows.Forms.CheckState" /> for the checked portion of the item. </param>
			/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="check" /> parameter is not one of the valid <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
			// Token: 0x0600057F RID: 1407 RVA: 0x00017A6C File Offset: 0x00015C6C
			public int Add(object item, CheckState check)
			{
				int num = this.Add(item);
				ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(num, check, CheckState.Unchecked);
				if (check == CheckState.Checked)
				{
					this.owner.OnItemCheck(itemCheckEventArgs);
				}
				if (itemCheckEventArgs.NewValue != CheckState.Unchecked)
				{
					this.owner.check_states[item] = itemCheckEventArgs.NewValue;
				}
				this.owner.UpdateCollections();
				return num;
			}

			// Token: 0x040006CE RID: 1742
			private CheckedListBox owner;
		}

		/// <summary>Encapsulates the collection of indexes of checked items (including items in an indeterminate state) in a <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		// Token: 0x02000079 RID: 121
		public class CheckedIndexCollection : ICollection, IEnumerable, IList
		{
			// Token: 0x06000580 RID: 1408 RVA: 0x00017AD0 File Offset: 0x00015CD0
			internal CheckedIndexCollection(CheckedListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000581 RID: 1409 RVA: 0x00017AEC File Offset: 0x00015CEC
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000582 RID: 1410 RVA: 0x00017AF0 File Offset: 0x00015CF0
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls. For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>The <see cref="T:System.Object" /> used to synchronize to the collection.</returns>
			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000583 RID: 1411 RVA: 0x00017AF4 File Offset: 0x00015CF4
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Adds an item to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000584 RID: 1412 RVA: 0x00017AF8 File Offset: 0x00015CF8
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000585 RID: 1413 RVA: 0x00017B00 File Offset: 0x00015D00
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>Determines whether the specified index is located within the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> for the <see cref="T:System.Windows.Forms.CheckedListBox" /> is an item in this collection; otherwise, false.</returns>
			/// <param name="index">The index to locate in the collection.</param>
			// Token: 0x06000586 RID: 1414 RVA: 0x00017B08 File Offset: 0x00015D08
			bool IList.Contains(object index)
			{
				return this.Contains((int)index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <returns>This member is an explicit interface member implementation. It can be used only when the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> instance is cast to an <see cref="T:System.Collections.IList" /> interface.</returns>
			/// <param name="index">The zero-based index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> to locate in this collection.</param>
			// Token: 0x06000587 RID: 1415 RVA: 0x00017B18 File Offset: 0x00015D18
			int IList.IndexOf(object index)
			{
				return this.IndexOf((int)index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The index at which value should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000588 RID: 1416 RVA: 0x00017B28 File Offset: 0x00015D28
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06000589 RID: 1417 RVA: 0x00017B30 File Offset: 0x00015D30
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>or a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x0600058A RID: 1418 RVA: 0x00017B38 File Offset: 0x00015D38
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The zero-based index of the item to get.</param>
			// Token: 0x17000147 RID: 327
			// (get) Token: 0x0600058B RID: 1419 RVA: 0x00017B40 File Offset: 0x00015D40
			// (set) Token: 0x0600058C RID: 1420 RVA: 0x00017B50 File Offset: 0x00015D50
			object IList.Item
			{
				get
				{
					return this.indices[index];
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			/// <summary>Gets the number of checked items.</summary>
			/// <returns>The number of indexes in the collection.</returns>
			// Token: 0x17000148 RID: 328
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x00017B58 File Offset: 0x00015D58
			public int Count
			{
				get
				{
					return this.indices.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600058E RID: 1422 RVA: 0x00017B68 File Offset: 0x00015D68
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the index of a checked item in the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
			/// <returns>The index of the checked item. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> class overview.</returns>
			/// <param name="index">An index into the checked indexes collection. This index specifies the index of the checked item you want to retrieve. </param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> is less than zero.-or- The <paramref name="index" /> is not in the collection. </exception>
			// Token: 0x1700014A RID: 330
			[DesignerSerializationVisibility(0)]
			[Browsable(false)]
			public int this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("Index of out range");
					}
					return (int)this.indices[index];
				}
			}

			/// <summary>Determines whether the specified index is located in the collection.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> is an item in this collection; otherwise, false.</returns>
			/// <param name="index">The index to locate in the collection. </param>
			// Token: 0x06000590 RID: 1424 RVA: 0x00017BA0 File Offset: 0x00015DA0
			public bool Contains(int index)
			{
				return this.indices.Contains(index);
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">The destination array. </param>
			/// <param name="index">The zero-based relative index in <paramref name="dest" /> at which copying begins. </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.RankException">
			///   <paramref name="array" /> is multidimensional. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero. </exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from index to the end of the destination <see cref="T:System.Array" />. </exception>
			/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <see cref="T:System.Array" />. </exception>
			// Token: 0x06000591 RID: 1425 RVA: 0x00017BB4 File Offset: 0x00015DB4
			public void CopyTo(Array dest, int index)
			{
				this.indices.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the <see cref="P:System.Windows.Forms.CheckedListBox.CheckedIndices" /> collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
			// Token: 0x06000592 RID: 1426 RVA: 0x00017BC4 File Offset: 0x00015DC4
			public IEnumerator GetEnumerator()
			{
				return this.indices.GetEnumerator();
			}

			/// <summary>Returns an index into the collection of checked indexes.</summary>
			/// <returns>The index that specifies the index of the checked item or -1 if the <paramref name="index" /> parameter is not in the checked indexes collection. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> class overview.</returns>
			/// <param name="index">The index of the checked item. </param>
			// Token: 0x06000593 RID: 1427 RVA: 0x00017BD4 File Offset: 0x00015DD4
			public int IndexOf(int index)
			{
				return this.indices.IndexOf(index);
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x00017BE8 File Offset: 0x00015DE8
			internal void Refresh()
			{
				this.indices.Clear();
				for (int i = 0; i < this.owner.Items.Count; i++)
				{
					if (this.owner.check_states.Contains(this.owner.Items[i]))
					{
						this.indices.Add(i);
					}
				}
			}

			// Token: 0x040006CF RID: 1743
			private CheckedListBox owner;

			// Token: 0x040006D0 RID: 1744
			private ArrayList indices = new ArrayList();
		}

		/// <summary>Encapsulates the collection of checked items, including items in an indeterminate state, in a <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
		// Token: 0x0200007A RID: 122
		public class CheckedItemCollection : ICollection, IEnumerable, IList
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x00017C5C File Offset: 0x00015E5C
			internal CheckedItemCollection(CheckedListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000596 RID: 1430 RVA: 0x00017C78 File Offset: 0x00015E78
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>The <see cref="T:System.Object" /> used to synchronize to the collection.</returns>
			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000597 RID: 1431 RVA: 0x00017C7C File Offset: 0x00015E7C
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000598 RID: 1432 RVA: 0x00017C80 File Offset: 0x00015E80
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The zero-based index of the item to add.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06000599 RID: 1433 RVA: 0x00017C84 File Offset: 0x00015E84
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			// Token: 0x0600059A RID: 1434 RVA: 0x00017C8C File Offset: 0x00015E8C
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The item to insert into the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" />.</param>
			// Token: 0x0600059B RID: 1435 RVA: 0x00017C94 File Offset: 0x00015E94
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The item to remove from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" />.</param>
			// Token: 0x0600059C RID: 1436 RVA: 0x00017C9C File Offset: 0x00015E9C
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			// Token: 0x0600059D RID: 1437 RVA: 0x00017CA4 File Offset: 0x00015EA4
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x1700014E RID: 334
			// (get) Token: 0x0600059E RID: 1438 RVA: 0x00017CAC File Offset: 0x00015EAC
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating if the collection is read-only.</summary>
			/// <returns>Always true.</returns>
			// Token: 0x1700014F RID: 335
			// (get) Token: 0x0600059F RID: 1439 RVA: 0x00017CBC File Offset: 0x00015EBC
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets an object in the checked items collection.</summary>
			/// <returns>The object at the specified index. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> class overview.</returns>
			/// <param name="index">An index into the collection of checked items. This collection index corresponds to the index of the checked item. </param>
			/// <exception cref="T:System.NotSupportedException">The object cannot be set.</exception>
			// Token: 0x17000150 RID: 336
			[DesignerSerializationVisibility(0)]
			[Browsable(false)]
			public object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("Index of out range");
					}
					return this.list[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if item is in the collection; otherwise, false.</returns>
			/// <param name="item">An object from the items collection. </param>
			// Token: 0x060005A2 RID: 1442 RVA: 0x00017D00 File Offset: 0x00015F00
			public bool Contains(object item)
			{
				return this.list.Contains(item);
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">The destination array. </param>
			/// <param name="index">The zero-based relative index in <paramref name="dest" /> at which copying begins. </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.RankException">
			///   <paramref name="array" /> is multidimensional. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero. </exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from index to the end of the destination <see cref="T:System.Array" />. </exception>
			/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <see cref="T:System.Array" />. </exception>
			// Token: 0x060005A3 RID: 1443 RVA: 0x00017D10 File Offset: 0x00015F10
			public void CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Returns an index into the collection of checked items.</summary>
			/// <returns>The index of the object in the checked item collection or -1 if the object is not in the collection. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> class overview.</returns>
			/// <param name="item">The object whose index you want to retrieve. This object must belong to the checked items collection. </param>
			// Token: 0x060005A4 RID: 1444 RVA: 0x00017D20 File Offset: 0x00015F20
			public int IndexOf(object item)
			{
				return this.list.IndexOf(item);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the <see cref="P:System.Windows.Forms.CheckedListBox.CheckedItems" /> collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
			// Token: 0x060005A5 RID: 1445 RVA: 0x00017D30 File Offset: 0x00015F30
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x00017D40 File Offset: 0x00015F40
			internal void Refresh()
			{
				this.list.Clear();
				for (int i = 0; i < this.owner.Items.Count; i++)
				{
					if (this.owner.check_states.Contains(this.owner.Items[i]))
					{
						this.list.Add(this.owner.Items[i]);
					}
				}
			}

			// Token: 0x040006D1 RID: 1745
			private CheckedListBox owner;

			// Token: 0x040006D2 RID: 1746
			private ArrayList list = new ArrayList();
		}
	}
}
