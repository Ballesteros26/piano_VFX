using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows list view control, which displays a collection of items that can be displayed using one of four different views.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200021C RID: 540
	[DefaultEvent("SelectedIndexChanged")]
	[Docking(DockingBehavior.Ask)]
	[ComVisible(true)]
	[ClassInterface(1)]
	[Designer("System.Windows.Forms.Design.ListViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Items")]
	public class ListView : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView" /> class.</summary>
		// Token: 0x060021FC RID: 8700 RVA: 0x0007EF48 File Offset: 0x0007D148
		public ListView()
		{
			this.background_color = ThemeEngine.Current.ColorWindow;
			this.groups = new ListViewGroupCollection(this);
			this.items = new ListView.ListViewItemCollection(this);
			this.items.Changed += this.OnItemsChanged;
			this.checked_indices = new ListView.CheckedIndexCollection(this);
			this.checked_items = new ListView.CheckedListViewItemCollection(this);
			this.columns = new ListView.ColumnHeaderCollection(this);
			this.foreground_color = SystemColors.WindowText;
			this.selected_indices = new ListView.SelectedIndexCollection(this);
			this.selected_items = new ListView.SelectedListViewItemCollection(this);
			this.items_location = new Point[16];
			this.items_matrix_location = new ListView.ItemMatrixLocation[16];
			this.reordered_items_indices = new int[16];
			this.item_tooltip = new ToolTip();
			this.item_tooltip.Active = false;
			this.insertion_mark = new ListViewInsertionMark(this);
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.header_control = new ListView.HeaderControl(this);
			this.header_control.Visible = false;
			base.Controls.AddImplicit(this.header_control);
			this.item_control = new ListView.ItemControl(this);
			base.Controls.AddImplicit(this.item_control);
			this.h_scroll = new ImplicitHScrollBar();
			base.Controls.AddImplicit(this.h_scroll);
			this.v_scroll = new ImplicitVScrollBar();
			base.Controls.AddImplicit(this.v_scroll);
			this.h_marker = (this.v_marker = 0);
			this.keysearch_tickcnt = 0;
			this.h_scroll.Visible = false;
			this.h_scroll.ValueChanged += new EventHandler(this.HorizontalScroller);
			this.v_scroll.Visible = false;
			this.v_scroll.ValueChanged += new EventHandler(this.VerticalScroller);
			base.KeyDown += this.ListView_KeyDown;
			base.SizeChanged += new EventHandler(this.ListView_SizeChanged);
			base.GotFocus += new EventHandler(this.FocusChanged);
			base.LostFocus += new EventHandler(this.FocusChanged);
			base.MouseWheel += this.ListView_MouseWheel;
			base.MouseEnter += new EventHandler(this.ListView_MouseEnter);
			base.Invalidated += this.ListView_Invalidated;
			this.BackgroundImageTiled = false;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0007F1F0 File Offset: 0x0007D3F0
		// Note: this type is marked as 'beforefieldinit'.
		static ListView()
		{
			ListView.AfterLabelEditEvent = new object();
			ListView.BeforeLabelEditEvent = new object();
			ListView.ColumnClickEvent = new object();
			ListView.ItemActivateEvent = new object();
			ListView.ItemCheckEvent = new object();
			ListView.ItemDragEvent = new object();
			ListView.SelectedIndexChangedEvent = new object();
			ListView.DrawColumnHeaderEvent = new object();
			ListView.DrawItemEvent = new object();
			ListView.DrawSubItemEvent = new object();
			ListView.ItemCheckedEvent = new object();
			ListView.ItemMouseHoverEvent = new object();
			ListView.ItemSelectionChangedEvent = new object();
			ListView.CacheVirtualItemsEvent = new object();
			ListView.RetrieveVirtualItemEvent = new object();
			ListView.RightToLeftLayoutChangedEvent = new object();
			ListView.SearchForVirtualItemEvent = new object();
			ListView.VirtualItemsSelectionRangeChangedEvent = new object();
			ListView.ColumnReorderedEvent = new object();
			ListView.ColumnWidthChangedEvent = new object();
			ListView.ColumnWidthChangingEvent = new object();
			ListView.UIALabelEditChangedEvent = new object();
			ListView.UIAShowGroupsChangedEvent = new object();
			ListView.UIAMultiSelectChangedEvent = new object();
			ListView.UIAViewChangedEvent = new object();
			ListView.UIACheckBoxesChangedEvent = new object();
			ListView.UIAFocusedItemChangedEvent = new object();
		}

		/// <summary>Occurs when the label for an item is edited by the user.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400020F RID: 527
		// (add) Token: 0x060021FE RID: 8702 RVA: 0x0007F318 File Offset: 0x0007D518
		// (remove) Token: 0x060021FF RID: 8703 RVA: 0x0007F32C File Offset: 0x0007D52C
		public event LabelEditEventHandler AfterLabelEdit
		{
			add
			{
				base.Events.AddHandler(ListView.AfterLabelEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.AfterLabelEditEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListView.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000210 RID: 528
		// (add) Token: 0x06002200 RID: 8704 RVA: 0x0007F340 File Offset: 0x0007D540
		// (remove) Token: 0x06002201 RID: 8705 RVA: 0x0007F34C File Offset: 0x0007D54C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Occurs when the user starts editing the label of an item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000211 RID: 529
		// (add) Token: 0x06002202 RID: 8706 RVA: 0x0007F358 File Offset: 0x0007D558
		// (remove) Token: 0x06002203 RID: 8707 RVA: 0x0007F36C File Offset: 0x0007D56C
		public event LabelEditEventHandler BeforeLabelEdit
		{
			add
			{
				base.Events.AddHandler(ListView.BeforeLabelEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.BeforeLabelEditEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks a column header within the list view control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000212 RID: 530
		// (add) Token: 0x06002204 RID: 8708 RVA: 0x0007F380 File Offset: 0x0007D580
		// (remove) Token: 0x06002205 RID: 8709 RVA: 0x0007F394 File Offset: 0x0007D594
		public event ColumnClickEventHandler ColumnClick
		{
			add
			{
				base.Events.AddHandler(ListView.ColumnClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ColumnClickEvent, value);
			}
		}

		/// <summary>Occurs when the details view of a <see cref="T:System.Windows.Forms.ListView" /> is drawn and the <see cref="P:System.Windows.Forms.ListView.OwnerDraw" /> property is set to true. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000213 RID: 531
		// (add) Token: 0x06002206 RID: 8710 RVA: 0x0007F3A8 File Offset: 0x0007D5A8
		// (remove) Token: 0x06002207 RID: 8711 RVA: 0x0007F3BC File Offset: 0x0007D5BC
		public event DrawListViewColumnHeaderEventHandler DrawColumnHeader
		{
			add
			{
				base.Events.AddHandler(ListView.DrawColumnHeaderEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.DrawColumnHeaderEvent, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ListView" /> is drawn and the <see cref="P:System.Windows.Forms.ListView.OwnerDraw" /> property is set to true.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000214 RID: 532
		// (add) Token: 0x06002208 RID: 8712 RVA: 0x0007F3D0 File Offset: 0x0007D5D0
		// (remove) Token: 0x06002209 RID: 8713 RVA: 0x0007F3E4 File Offset: 0x0007D5E4
		public event DrawListViewItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ListView.DrawItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.DrawItemEvent, value);
			}
		}

		/// <summary>Occurs when the details view of a <see cref="T:System.Windows.Forms.ListView" /> is drawn and the <see cref="P:System.Windows.Forms.ListView.OwnerDraw" /> property is set to true.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000215 RID: 533
		// (add) Token: 0x0600220A RID: 8714 RVA: 0x0007F3F8 File Offset: 0x0007D5F8
		// (remove) Token: 0x0600220B RID: 8715 RVA: 0x0007F40C File Offset: 0x0007D60C
		public event DrawListViewSubItemEventHandler DrawSubItem
		{
			add
			{
				base.Events.AddHandler(ListView.DrawSubItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.DrawSubItemEvent, value);
			}
		}

		/// <summary>Occurs when an item is activated.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000216 RID: 534
		// (add) Token: 0x0600220C RID: 8716 RVA: 0x0007F420 File Offset: 0x0007D620
		// (remove) Token: 0x0600220D RID: 8717 RVA: 0x0007F434 File Offset: 0x0007D634
		public event EventHandler ItemActivate
		{
			add
			{
				base.Events.AddHandler(ListView.ItemActivateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ItemActivateEvent, value);
			}
		}

		/// <summary>Occurs when the check state of an item changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000217 RID: 535
		// (add) Token: 0x0600220E RID: 8718 RVA: 0x0007F448 File Offset: 0x0007D648
		// (remove) Token: 0x0600220F RID: 8719 RVA: 0x0007F45C File Offset: 0x0007D65C
		public event ItemCheckEventHandler ItemCheck
		{
			add
			{
				base.Events.AddHandler(ListView.ItemCheckEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ItemCheckEvent, value);
			}
		}

		/// <summary>Occurs when the checked state of an item changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000218 RID: 536
		// (add) Token: 0x06002210 RID: 8720 RVA: 0x0007F470 File Offset: 0x0007D670
		// (remove) Token: 0x06002211 RID: 8721 RVA: 0x0007F484 File Offset: 0x0007D684
		public event ItemCheckedEventHandler ItemChecked
		{
			add
			{
				base.Events.AddHandler(ListView.ItemCheckedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ItemCheckedEvent, value);
			}
		}

		/// <summary>Occurs when the user begins dragging an item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000219 RID: 537
		// (add) Token: 0x06002212 RID: 8722 RVA: 0x0007F498 File Offset: 0x0007D698
		// (remove) Token: 0x06002213 RID: 8723 RVA: 0x0007F4AC File Offset: 0x0007D6AC
		public event ItemDragEventHandler ItemDrag
		{
			add
			{
				base.Events.AddHandler(ListView.ItemDragEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ItemDragEvent, value);
			}
		}

		/// <summary>Occurs when the mouse hovers over an item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400021A RID: 538
		// (add) Token: 0x06002214 RID: 8724 RVA: 0x0007F4C0 File Offset: 0x0007D6C0
		// (remove) Token: 0x06002215 RID: 8725 RVA: 0x0007F4D4 File Offset: 0x0007D6D4
		public event ListViewItemMouseHoverEventHandler ItemMouseHover
		{
			add
			{
				base.Events.AddHandler(ListView.ItemMouseHoverEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ItemMouseHoverEvent, value);
			}
		}

		/// <summary>Occurs when the selection state of an item changes.</summary>
		// Token: 0x1400021B RID: 539
		// (add) Token: 0x06002216 RID: 8726 RVA: 0x0007F4E8 File Offset: 0x0007D6E8
		// (remove) Token: 0x06002217 RID: 8727 RVA: 0x0007F4FC File Offset: 0x0007D6FC
		public event ListViewItemSelectionChangedEventHandler ItemSelectionChanged
		{
			add
			{
				base.Events.AddHandler(ListView.ItemSelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ItemSelectionChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListView.Padding" /> property changes.</summary>
		// Token: 0x1400021C RID: 540
		// (add) Token: 0x06002218 RID: 8728 RVA: 0x0007F510 File Offset: 0x0007D710
		// (remove) Token: 0x06002219 RID: 8729 RVA: 0x0007F51C File Offset: 0x0007D71C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> control is painted.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400021D RID: 541
		// (add) Token: 0x0600221A RID: 8730 RVA: 0x0007F528 File Offset: 0x0007D728
		// (remove) Token: 0x0600221B RID: 8731 RVA: 0x0007F534 File Offset: 0x0007D734
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListView.SelectedIndices" /> collection changes. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400021E RID: 542
		// (add) Token: 0x0600221C RID: 8732 RVA: 0x0007F540 File Offset: 0x0007D740
		// (remove) Token: 0x0600221D RID: 8733 RVA: 0x0007F554 File Offset: 0x0007D754
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListView.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.SelectedIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListView.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400021F RID: 543
		// (add) Token: 0x0600221E RID: 8734 RVA: 0x0007F568 File Offset: 0x0007D768
		// (remove) Token: 0x0600221F RID: 8735 RVA: 0x0007F574 File Offset: 0x0007D774
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Occurs when the contents of the display area for a <see cref="T:System.Windows.Forms.ListView" /> in virtual mode has changed, and the <see cref="T:System.Windows.Forms.ListView" /> determines that a new range of items is needed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000220 RID: 544
		// (add) Token: 0x06002220 RID: 8736 RVA: 0x0007F580 File Offset: 0x0007D780
		// (remove) Token: 0x06002221 RID: 8737 RVA: 0x0007F594 File Offset: 0x0007D794
		public event CacheVirtualItemsEventHandler CacheVirtualItems
		{
			add
			{
				base.Events.AddHandler(ListView.CacheVirtualItemsEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.CacheVirtualItemsEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and requires a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.RetrieveVirtualItemEventArgs.Item" /> property is not set to an item when the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event is handled.  </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000221 RID: 545
		// (add) Token: 0x06002222 RID: 8738 RVA: 0x0007F5A8 File Offset: 0x0007D7A8
		// (remove) Token: 0x06002223 RID: 8739 RVA: 0x0007F5BC File Offset: 0x0007D7BC
		public event RetrieveVirtualItemEventHandler RetrieveVirtualItem
		{
			add
			{
				base.Events.AddHandler(ListView.RetrieveVirtualItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.RetrieveVirtualItemEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListView.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000222 RID: 546
		// (add) Token: 0x06002224 RID: 8740 RVA: 0x0007F5D0 File Offset: 0x0007D7D0
		// (remove) Token: 0x06002225 RID: 8741 RVA: 0x0007F5E4 File Offset: 0x0007D7E4
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(ListView.RightToLeftLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.RightToLeftLayoutChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and a search is taking place.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000223 RID: 547
		// (add) Token: 0x06002226 RID: 8742 RVA: 0x0007F5F8 File Offset: 0x0007D7F8
		// (remove) Token: 0x06002227 RID: 8743 RVA: 0x0007F60C File Offset: 0x0007D80C
		public event SearchForVirtualItemEventHandler SearchForVirtualItem
		{
			add
			{
				base.Events.AddHandler(ListView.SearchForVirtualItemEvent, value);
			}
			remove
			{
				base.Events.AddHandler(ListView.SearchForVirtualItemEvent, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and the selection state of a range of items has changed.</summary>
		// Token: 0x14000224 RID: 548
		// (add) Token: 0x06002228 RID: 8744 RVA: 0x0007F620 File Offset: 0x0007D820
		// (remove) Token: 0x06002229 RID: 8745 RVA: 0x0007F634 File Offset: 0x0007D834
		public event ListViewVirtualItemsSelectionRangeChangedEventHandler VirtualItemsSelectionRangeChanged
		{
			add
			{
				base.Events.AddHandler(ListView.VirtualItemsSelectionRangeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.VirtualItemsSelectionRangeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the column header order is changed.</summary>
		// Token: 0x14000225 RID: 549
		// (add) Token: 0x0600222A RID: 8746 RVA: 0x0007F648 File Offset: 0x0007D848
		// (remove) Token: 0x0600222B RID: 8747 RVA: 0x0007F65C File Offset: 0x0007D85C
		public event ColumnReorderedEventHandler ColumnReordered
		{
			add
			{
				base.Events.AddHandler(ListView.ColumnReorderedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ColumnReorderedEvent, value);
			}
		}

		/// <summary>Occurs after the width of a column is successfully changed.</summary>
		// Token: 0x14000226 RID: 550
		// (add) Token: 0x0600222C RID: 8748 RVA: 0x0007F670 File Offset: 0x0007D870
		// (remove) Token: 0x0600222D RID: 8749 RVA: 0x0007F684 File Offset: 0x0007D884
		public event ColumnWidthChangedEventHandler ColumnWidthChanged
		{
			add
			{
				base.Events.AddHandler(ListView.ColumnWidthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ColumnWidthChangedEvent, value);
			}
		}

		/// <summary>Occurs when the width of a column is changing.</summary>
		// Token: 0x14000227 RID: 551
		// (add) Token: 0x0600222E RID: 8750 RVA: 0x0007F698 File Offset: 0x0007D898
		// (remove) Token: 0x0600222F RID: 8751 RVA: 0x0007F6AC File Offset: 0x0007D8AC
		public event ColumnWidthChangingEventHandler ColumnWidthChanging
		{
			add
			{
				base.Events.AddHandler(ListView.ColumnWidthChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.ColumnWidthChangingEvent, value);
			}
		}

		// Token: 0x14000228 RID: 552
		// (add) Token: 0x06002230 RID: 8752 RVA: 0x0007F6C0 File Offset: 0x0007D8C0
		// (remove) Token: 0x06002231 RID: 8753 RVA: 0x0007F6D4 File Offset: 0x0007D8D4
		internal event EventHandler UIAShowGroupsChanged
		{
			add
			{
				base.Events.AddHandler(ListView.UIAShowGroupsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.UIAShowGroupsChangedEvent, value);
			}
		}

		// Token: 0x14000229 RID: 553
		// (add) Token: 0x06002232 RID: 8754 RVA: 0x0007F6E8 File Offset: 0x0007D8E8
		// (remove) Token: 0x06002233 RID: 8755 RVA: 0x0007F6FC File Offset: 0x0007D8FC
		internal event EventHandler UIACheckBoxesChanged
		{
			add
			{
				base.Events.AddHandler(ListView.UIACheckBoxesChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.UIACheckBoxesChangedEvent, value);
			}
		}

		// Token: 0x1400022A RID: 554
		// (add) Token: 0x06002234 RID: 8756 RVA: 0x0007F710 File Offset: 0x0007D910
		// (remove) Token: 0x06002235 RID: 8757 RVA: 0x0007F724 File Offset: 0x0007D924
		internal event EventHandler UIAMultiSelectChanged
		{
			add
			{
				base.Events.AddHandler(ListView.UIAMultiSelectChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.UIAMultiSelectChangedEvent, value);
			}
		}

		// Token: 0x1400022B RID: 555
		// (add) Token: 0x06002236 RID: 8758 RVA: 0x0007F738 File Offset: 0x0007D938
		// (remove) Token: 0x06002237 RID: 8759 RVA: 0x0007F74C File Offset: 0x0007D94C
		internal event EventHandler UIALabelEditChanged
		{
			add
			{
				base.Events.AddHandler(ListView.UIALabelEditChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.UIALabelEditChangedEvent, value);
			}
		}

		// Token: 0x1400022C RID: 556
		// (add) Token: 0x06002238 RID: 8760 RVA: 0x0007F760 File Offset: 0x0007D960
		// (remove) Token: 0x06002239 RID: 8761 RVA: 0x0007F774 File Offset: 0x0007D974
		internal event EventHandler UIAViewChanged
		{
			add
			{
				base.Events.AddHandler(ListView.UIAViewChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.UIAViewChangedEvent, value);
			}
		}

		// Token: 0x1400022D RID: 557
		// (add) Token: 0x0600223A RID: 8762 RVA: 0x0007F788 File Offset: 0x0007D988
		// (remove) Token: 0x0600223B RID: 8763 RVA: 0x0007F79C File Offset: 0x0007D99C
		internal event EventHandler UIAFocusedItemChanged
		{
			add
			{
				base.Events.AddHandler(ListView.UIAFocusedItemChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.UIAFocusedItemChangedEvent, value);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0007F7B0 File Offset: 0x0007D9B0
		internal Size CheckBoxSize
		{
			get
			{
				if (!this.check_boxes)
				{
					return Size.Empty;
				}
				if (this.state_image_list != null)
				{
					return this.state_image_list.ImageSize;
				}
				return ThemeEngine.Current.ListViewCheckBoxSize;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0007F7F0 File Offset: 0x0007D9F0
		// (set) Token: 0x0600223E RID: 8766 RVA: 0x0007F868 File Offset: 0x0007DA68
		internal Size ItemSize
		{
			get
			{
				if (this.view != View.Details)
				{
					return this.item_size;
				}
				Size size = default(Size);
				size.Height = this.item_size.Height;
				for (int i = 0; i < this.columns.Count; i++)
				{
					size.Width += this.columns[i].Wd;
				}
				return size;
			}
			set
			{
				this.item_size = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x0007F874 File Offset: 0x0007DA74
		// (set) Token: 0x06002240 RID: 8768 RVA: 0x0007F87C File Offset: 0x0007DA7C
		internal int HotItemIndex
		{
			get
			{
				return this.hot_item_index;
			}
			set
			{
				this.hot_item_index = value;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x0007F888 File Offset: 0x0007DA88
		internal bool UsingGroups
		{
			get
			{
				return this.show_groups && this.groups.Count > 0 && this.view != View.List && Application.VisualStylesEnabled;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0007F8C8 File Offset: 0x0007DAC8
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x0007F8CC File Offset: 0x0007DACC
		internal bool UseCustomColumnWidth
		{
			get
			{
				return (this.view == View.List || this.view == View.SmallIcon) && this.columns.Count > 0;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x0007F8F8 File Offset: 0x0007DAF8
		internal ColumnHeader EnteredColumnHeader
		{
			get
			{
				return this.header_control.EnteredColumnHeader;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x0007F908 File Offset: 0x0007DB08
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x0007F910 File Offset: 0x0007DB10
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.ListViewDefaultSize;
			}
		}

		/// <returns>true if the surface of the control should be drawn using double buffering; otherwise, false.</returns>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x0007F91C File Offset: 0x0007DB1C
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x0007F924 File Offset: 0x0007DB24
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		/// <summary>Gets or sets the type of action the user must take to activate an item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ItemActivation" /> values. The default is <see cref="F:System.Windows.Forms.ItemActivation.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ItemActivation" /> members. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x0007F930 File Offset: 0x0007DB30
		// (set) Token: 0x0600224A RID: 8778 RVA: 0x0007F938 File Offset: 0x0007DB38
		[DefaultValue(ItemActivation.Standard)]
		public ItemActivation Activation
		{
			get
			{
				return this.activation;
			}
			set
			{
				if (value != ItemActivation.Standard && value != ItemActivation.OneClick && value != ItemActivation.TwoClick)
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for Activation", value));
				}
				if (this.hot_tracking && value != ItemActivation.OneClick)
				{
					throw new ArgumentException("When HotTracking is on, activation must be ItemActivation.OneClick");
				}
				this.activation = value;
			}
		}

		/// <summary>Gets or sets the alignment of items in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values. The default is <see cref="F:System.Windows.Forms.ListViewAlignment.Top" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0007F994 File Offset: 0x0007DB94
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x0007F99C File Offset: 0x0007DB9C
		[Localizable(true)]
		[DefaultValue(ListViewAlignment.Top)]
		public ListViewAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (value != ListViewAlignment.Default && value != ListViewAlignment.Left && value != ListViewAlignment.SnapToGrid && value != ListViewAlignment.Top)
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for Alignment", value));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					if (this.view == View.LargeIcon || this.View == View.SmallIcon)
					{
						this.Redraw(true);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can drag column headers to reorder columns in the control.</summary>
		/// <returns>true if drag-and-drop column reordering is allowed; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x0007FA0C File Offset: 0x0007DC0C
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x0007FA14 File Offset: 0x0007DC14
		[DefaultValue(false)]
		public bool AllowColumnReorder
		{
			get
			{
				return this.allow_column_reorder;
			}
			set
			{
				this.allow_column_reorder = value;
			}
		}

		/// <summary>Gets or sets whether icons are automatically kept arranged.</summary>
		/// <returns>true if icons are automatically kept arranged and snapped to the grid; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0007FA20 File Offset: 0x0007DC20
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x0007FA28 File Offset: 0x0007DC28
		[DefaultValue(true)]
		public bool AutoArrange
		{
			get
			{
				return this.auto_arrange;
			}
			set
			{
				if (this.auto_arrange != value)
				{
					this.auto_arrange = value;
					if (this.view == View.LargeIcon || this.View == View.SmallIcon)
					{
						this.Redraw(true);
					}
				}
			}
		}

		/// <summary>Gets or sets the background color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of the background.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x0007FA5C File Offset: 0x0007DC5C
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x0007FA80 File Offset: 0x0007DC80
		public override Color BackColor
		{
			get
			{
				if (this.background_color.IsEmpty)
				{
					return ThemeEngine.Current.ColorWindow;
				}
				return this.background_color;
			}
			set
			{
				this.background_color = value;
				this.item_control.BackColor = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Windows.Forms.ImageLayout" /> value.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0007FA98 File Offset: 0x0007DC98
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x0007FAA0 File Offset: 0x0007DCA0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the background image of the <see cref="T:System.Windows.Forms.ListView" /> should be tiled.</summary>
		/// <returns>true if the background image of the <see cref="T:System.Windows.Forms.ListView" /> should be tiled; otherwise, false. The default is false.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0007FAAC File Offset: 0x0007DCAC
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x0007FABC File Offset: 0x0007DCBC
		[DefaultValue(false)]
		public bool BackgroundImageTiled
		{
			get
			{
				return this.item_control.BackgroundImageLayout == ImageLayout.Tile;
			}
			set
			{
				ImageLayout imageLayout = ((!value) ? ImageLayout.None : ImageLayout.Tile);
				if (imageLayout == this.item_control.BackgroundImageLayout)
				{
					return;
				}
				this.item_control.BackgroundImageLayout = imageLayout;
			}
		}

		/// <summary>Gets or sets the border style of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x0007FAF8 File Offset: 0x0007DCF8
		// (set) Token: 0x06002258 RID: 8792 RVA: 0x0007FB00 File Offset: 0x0007DD00
		[DispId(-504)]
		[DefaultValue(BorderStyle.Fixed3D)]
		public BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
			set
			{
				base.InternalBorderStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a check box appears next to each item in the control.</summary>
		/// <returns>true if a check box appears next to each item in the <see cref="T:System.Windows.Forms.ListView" /> control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x0007FB0C File Offset: 0x0007DD0C
		// (set) Token: 0x0600225A RID: 8794 RVA: 0x0007FB14 File Offset: 0x0007DD14
		[DefaultValue(false)]
		public bool CheckBoxes
		{
			get
			{
				return this.check_boxes;
			}
			set
			{
				if (this.check_boxes != value)
				{
					if (value && this.View == View.Tile)
					{
						throw new NotSupportedException("CheckBoxes are not supported in Tile view. Choose a different view or set CheckBoxes to false.");
					}
					this.check_boxes = value;
					this.Redraw(true);
					this.OnUIACheckBoxesChanged();
				}
			}
		}

		/// <summary>Gets the indexes of the currently checked items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> that contains the indexes of the currently checked items. If no items are currently checked, an empty <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x0007FB54 File Offset: 0x0007DD54
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ListView.CheckedIndexCollection CheckedIndices
		{
			get
			{
				return this.checked_indices;
			}
		}

		/// <summary>Gets the currently checked items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> that contains the currently checked items. If no items are currently checked, an empty <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x0007FB5C File Offset: 0x0007DD5C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ListView.CheckedListViewItemCollection CheckedItems
		{
			get
			{
				return this.checked_items;
			}
		}

		/// <summary>Gets the collection of all column headers that appear in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> that represents the column headers that appear when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.Details" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x0007FB64 File Offset: 0x0007DD64
		[DesignerSerializationVisibility(2)]
		[Editor("System.Windows.Forms.Design.ColumnHeaderCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[MergableProperty(false)]
		public ListView.ColumnHeaderCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		/// <summary>Gets or sets the item in the control that currently has focus.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that has focus, or null if no item has the focus in the <see cref="T:System.Windows.Forms.ListView" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0007FB6C File Offset: 0x0007DD6C
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x0007FB88 File Offset: 0x0007DD88
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ListViewItem FocusedItem
		{
			get
			{
				if (this.focused_item_index == -1)
				{
					return null;
				}
				return this.GetItemAtDisplayIndex(this.focused_item_index);
			}
			set
			{
				if (value == null || value.ListView != this || !base.IsHandleCreated)
				{
					return;
				}
				this.SetFocusedItem(value.DisplayIndex);
			}
		}

		/// <summary>Gets or sets the foreground color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that is the foreground color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x0007FBC0 File Offset: 0x0007DDC0
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x0007FBE4 File Offset: 0x0007DDE4
		public override Color ForeColor
		{
			get
			{
				if (this.foreground_color.IsEmpty)
				{
					return ThemeEngine.Current.ColorWindowText;
				}
				return this.foreground_color;
			}
			set
			{
				this.foreground_color = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether clicking an item selects all its subitems.</summary>
		/// <returns>true if clicking an item selects the item and all its subitems; false if clicking an item selects only the item itself. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x0007FBF0 File Offset: 0x0007DDF0
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x0007FBF8 File Offset: 0x0007DDF8
		[DefaultValue(false)]
		public bool FullRowSelect
		{
			get
			{
				return this.full_row_select;
			}
			set
			{
				if (this.full_row_select != value)
				{
					this.full_row_select = value;
					this.InvalidateSelection();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether grid lines appear between the rows and columns containing the items and subitems in the control.</summary>
		/// <returns>true if grid lines are drawn around items and subitems; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x0007FC14 File Offset: 0x0007DE14
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x0007FC1C File Offset: 0x0007DE1C
		[DefaultValue(false)]
		public bool GridLines
		{
			get
			{
				return this.grid_lines;
			}
			set
			{
				if (this.grid_lines != value)
				{
					this.grid_lines = value;
					this.Redraw(false);
				}
			}
		}

		/// <summary>Gets or sets the column header style.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ColumnHeaderStyle" /> values. The default is <see cref="F:System.Windows.Forms.ColumnHeaderStyle.Clickable" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ColumnHeaderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x0007FC38 File Offset: 0x0007DE38
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x0007FC40 File Offset: 0x0007DE40
		[DefaultValue(ColumnHeaderStyle.Clickable)]
		public ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return this.header_style;
			}
			set
			{
				if (this.header_style == value)
				{
					return;
				}
				switch (value)
				{
				case ColumnHeaderStyle.None:
				case ColumnHeaderStyle.Nonclickable:
				case ColumnHeaderStyle.Clickable:
					this.header_style = value;
					if (this.view == View.Details)
					{
						this.Redraw(true);
					}
					return;
				default:
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ColumnHeaderStyle", value));
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected item in the control remains highlighted when the control loses focus.</summary>
		/// <returns>true if the selected item does not appear highlighted when the control loses focus; false if the selected item still appears highlighted when the control loses focus. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0007FCA8 File Offset: 0x0007DEA8
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x0007FCB0 File Offset: 0x0007DEB0
		[DefaultValue(true)]
		public bool HideSelection
		{
			get
			{
				return this.hide_selection;
			}
			set
			{
				if (this.hide_selection != value)
				{
					this.hide_selection = value;
					this.InvalidateSelection();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the text of an item or subitem has the appearance of a hyperlink when the mouse pointer passes over it.</summary>
		/// <returns>true if the item text has the appearance of a hyperlink when the mouse passes over it; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x0007FCCC File Offset: 0x0007DECC
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x0007FCD4 File Offset: 0x0007DED4
		[DefaultValue(false)]
		public bool HotTracking
		{
			get
			{
				return this.hot_tracking;
			}
			set
			{
				if (this.hot_tracking == value)
				{
					return;
				}
				this.hot_tracking = value;
				if (this.hot_tracking)
				{
					this.hover_selection = true;
					this.activation = ItemActivation.OneClick;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether an item is automatically selected when the mouse pointer remains over the item for a few seconds.</summary>
		/// <returns>true if an item is automatically selected when the mouse pointer hovers over it; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x0007FD04 File Offset: 0x0007DF04
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x0007FD0C File Offset: 0x0007DF0C
		[DefaultValue(false)]
		public bool HoverSelection
		{
			get
			{
				return this.hover_selection;
			}
			set
			{
				if (this.hot_tracking && !value)
				{
					throw new ArgumentException("When HotTracking is on, hover selection must be true");
				}
				this.hover_selection = value;
			}
		}

		/// <summary>Gets an object used to indicate the expected drop location when an item is dragged within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewInsertionMark" /> object representing the insertion mark.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x0007FD34 File Offset: 0x0007DF34
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ListViewInsertionMark InsertionMark
		{
			get
			{
				return this.insertion_mark;
			}
		}

		/// <summary>Gets a collection containing all items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that contains all the items in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x0007FD3C File Offset: 0x0007DF3C
		[Editor("System.Windows.Forms.Design.ListViewItemCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(2)]
		[Localizable(true)]
		[MergableProperty(false)]
		public ListView.ListViewItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can edit the labels of items in the control.</summary>
		/// <returns>true if the user can edit the labels of items at run time; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x0007FD44 File Offset: 0x0007DF44
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x0007FD4C File Offset: 0x0007DF4C
		[DefaultValue(false)]
		public bool LabelEdit
		{
			get
			{
				return this.label_edit;
			}
			set
			{
				if (value != this.label_edit)
				{
					this.label_edit = value;
					this.OnUIALabelEditChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether item labels wrap when items are displayed in the control as icons.</summary>
		/// <returns>true if item labels wrap when items are displayed as icons; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0007FD68 File Offset: 0x0007DF68
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x0007FD70 File Offset: 0x0007DF70
		[DefaultValue(true)]
		[Localizable(true)]
		public bool LabelWrap
		{
			get
			{
				return this.label_wrap;
			}
			set
			{
				if (this.label_wrap != value)
				{
					this.label_wrap = value;
					this.Redraw(true);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> to use when displaying items as large icons in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains the icons to use when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.LargeIcon" />. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x0007FD8C File Offset: 0x0007DF8C
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x0007FD94 File Offset: 0x0007DF94
		[DefaultValue(null)]
		public ImageList LargeImageList
		{
			get
			{
				return this.large_image_list;
			}
			set
			{
				this.large_image_list = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the sorting comparer for the control.</summary>
		/// <returns>An <see cref="T:System.Collections.IComparer" /> that represents the sorting comparer for the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0007FDA4 File Offset: 0x0007DFA4
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x0007FDD8 File Offset: 0x0007DFD8
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public IComparer ListViewItemSorter
		{
			get
			{
				if (this.View != View.SmallIcon && this.View != View.LargeIcon && this.item_sorter is ListView.ItemComparer)
				{
					return null;
				}
				return this.item_sorter;
			}
			set
			{
				if (this.item_sorter != value)
				{
					this.item_sorter = value;
					this.Sort();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether multiple items can be selected.</summary>
		/// <returns>true if multiple items in the control can be selected at one time; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0007FDF4 File Offset: 0x0007DFF4
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x0007FDFC File Offset: 0x0007DFFC
		[DefaultValue(true)]
		public bool MultiSelect
		{
			get
			{
				return this.multiselect;
			}
			set
			{
				if (value != this.multiselect)
				{
					this.multiselect = value;
					this.OnUIAMultiSelectChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by the operating system or by code that you provide.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by code that you provide; false if the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by the operating system. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0007FE18 File Offset: 0x0007E018
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x0007FE20 File Offset: 0x0007E020
		[DefaultValue(false)]
		public bool OwnerDraw
		{
			get
			{
				return this.owner_draw;
			}
			set
			{
				this.owner_draw = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the space between the <see cref="T:System.Windows.Forms.ListView" /> control and its contents.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Padding" /> that specifies the space between the <see cref="T:System.Windows.Forms.ListView" /> control and its contents.</returns>
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x0007FE30 File Offset: 0x0007E030
		// (set) Token: 0x0600227D RID: 8829 RVA: 0x0007FE38 File Offset: 0x0007E038
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets a value indicating whether the control is laid out from right to left.</summary>
		/// <returns>true to indicate the <see cref="T:System.Windows.Forms.ListView" /> control is laid out from right to left; otherwise, false. </returns>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0007FE44 File Offset: 0x0007E044
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x0007FE4C File Offset: 0x0007E04C
		[MonoTODO("RTL not supported")]
		[Localizable(true)]
		[DefaultValue(false)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.right_to_left_layout;
			}
			set
			{
				if (this.right_to_left_layout != value)
				{
					this.right_to_left_layout = value;
					this.OnRightToLeftLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a scroll bar is added to the control when there is not enough room to display all items.</summary>
		/// <returns>true if scroll bars are added to the control when necessary to allow the user to see all the items; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x0007FE6C File Offset: 0x0007E06C
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x0007FE74 File Offset: 0x0007E074
		[DefaultValue(true)]
		public bool Scrollable
		{
			get
			{
				return this.scrollable;
			}
			set
			{
				if (this.scrollable != value)
				{
					this.scrollable = value;
					this.Redraw(true);
				}
			}
		}

		/// <summary>Gets the indexes of the selected items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> that contains the indexes of the selected items. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x0007FE90 File Offset: 0x0007E090
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ListView.SelectedIndexCollection SelectedIndices
		{
			get
			{
				return this.selected_indices;
			}
		}

		/// <summary>Gets the items that are selected in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> that contains the items that are selected in the control. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x0007FE98 File Offset: 0x0007E098
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ListView.SelectedListViewItemCollection SelectedItems
		{
			get
			{
				return this.selected_items;
			}
		}

		/// <summary>Gets or sets a value indicating whether items are displayed in groups.</summary>
		/// <returns>true to display items in groups; otherwise, false. The default value is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0007FEA0 File Offset: 0x0007E0A0
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x0007FEA8 File Offset: 0x0007E0A8
		[DefaultValue(true)]
		public bool ShowGroups
		{
			get
			{
				return this.show_groups;
			}
			set
			{
				if (this.show_groups != value)
				{
					this.show_groups = value;
					this.Redraw(true);
					this.OnUIAShowGroupsChanged();
				}
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.ListViewGroup" /> objects assigned to the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> that contains all the groups in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x0007FED8 File Offset: 0x0007E0D8
		[Localizable(true)]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(2)]
		[Editor("System.Windows.Forms.Design.ListViewGroupCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ListViewGroupCollection Groups
		{
			get
			{
				return this.groups;
			}
		}

		/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>true if <see cref="T:System.Windows.Forms.ListViewItem" /> ToolTips should be shown; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x0007FEE0 File Offset: 0x0007E0E0
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x0007FEE8 File Offset: 0x0007E0E8
		[DefaultValue(false)]
		public bool ShowItemToolTips
		{
			get
			{
				return this.show_item_tooltips;
			}
			set
			{
				this.show_item_tooltips = value;
				this.item_tooltip.Active = false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> to use when displaying items as small icons in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains the icons to use when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.SmallIcon" />. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x0007FF00 File Offset: 0x0007E100
		// (set) Token: 0x0600228A RID: 8842 RVA: 0x0007FF08 File Offset: 0x0007E108
		[DefaultValue(null)]
		public ImageList SmallImageList
		{
			get
			{
				return this.small_image_list;
			}
			set
			{
				this.small_image_list = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the sort order for items in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SortOrder" /> values. The default is <see cref="F:System.Windows.Forms.SortOrder.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.SortOrder" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x0007FF18 File Offset: 0x0007E118
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x0007FF20 File Offset: 0x0007E120
		[DefaultValue(SortOrder.None)]
		public SortOrder Sorting
		{
			get
			{
				return this.sort_order;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SortOrder), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SortOrder));
				}
				if (this.sort_order == value)
				{
					return;
				}
				this.sort_order = value;
				if (this.virtual_mode)
				{
					return;
				}
				if (value == SortOrder.None)
				{
					if (this.item_sorter != null && this.View != View.SmallIcon && this.View != View.LargeIcon)
					{
						this.item_sorter = null;
					}
					this.Redraw(false);
				}
				else
				{
					if (this.item_sorter == null)
					{
						this.item_sorter = new ListView.ItemComparer(value);
					}
					if (this.item_sorter is ListView.ItemComparer)
					{
						this.item_sorter = new ListView.ItemComparer(value);
					}
					this.Sort();
				}
			}
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0007FFF4 File Offset: 0x0007E1F4
		private void OnImageListChanged(object sender, EventArgs args)
		{
			this.item_control.Invalidate();
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> associated with application-defined states in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains a set of state images that can be used to indicate an application-defined state of an item. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x00080004 File Offset: 0x0007E204
		// (set) Token: 0x0600228F RID: 8847 RVA: 0x0008000C File Offset: 0x0007E20C
		[DefaultValue(null)]
		public ImageList StateImageList
		{
			get
			{
				return this.state_image_list;
			}
			set
			{
				if (this.state_image_list == value)
				{
					return;
				}
				if (this.state_image_list != null)
				{
					this.state_image_list.Images.Changed -= new EventHandler(this.OnImageListChanged);
				}
				this.state_image_list = value;
				if (this.state_image_list != null)
				{
					this.state_image_list.Images.Changed += new EventHandler(this.OnImageListChanged);
				}
				this.Redraw(true);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The text to display in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x00080084 File Offset: 0x0007E284
		// (set) Token: 0x06002291 RID: 8849 RVA: 0x0008008C File Offset: 0x0007E28C
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value == base.Text)
				{
					return;
				}
				base.Text = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the size of the tiles shown in tile view.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the new tile size.</returns>
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x000800BC File Offset: 0x0007E2BC
		// (set) Token: 0x06002293 RID: 8851 RVA: 0x000800C4 File Offset: 0x0007E2C4
		[Browsable(true)]
		public Size TileSize
		{
			get
			{
				return this.tile_size;
			}
			set
			{
				if (value.Width <= 0 || value.Height <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.tile_size = value;
				if (this.view == View.Tile)
				{
					this.Redraw(true);
				}
			}
		}

		/// <summary>Gets or sets the first visible item in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the first visible item in the control.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.LargeIcon" />,  <see cref="F:System.Windows.Forms.View.SmallIcon" />, or <see cref="F:System.Windows.Forms.View.Tile" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x00080110 File Offset: 0x0007E310
		// (set) Token: 0x06002295 RID: 8853 RVA: 0x000801DC File Offset: 0x0007E3DC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ListViewItem TopItem
		{
			get
			{
				if (this.view == View.LargeIcon || this.view == View.SmallIcon || this.view == View.Tile)
				{
					throw new InvalidOperationException("Cannot get the top item in LargeIcon, SmallIcon or Tile view.");
				}
				if (this.items.Count == 0)
				{
					return null;
				}
				if (this.h_marker == 0 && this.v_marker == 0)
				{
					return this.items[0];
				}
				int height = this.header_control.Height;
				for (int i = 0; i < this.items.Count; i++)
				{
					Point itemLocation = this.GetItemLocation(i);
					if (itemLocation.X >= 0 && itemLocation.Y - height >= 0)
					{
						return this.items[i];
					}
				}
				return null;
			}
			set
			{
				if (this.view == View.LargeIcon || this.view == View.SmallIcon || this.view == View.Tile)
				{
					throw new InvalidOperationException("Cannot set the top item in LargeIcon, SmallIcon or Tile view.");
				}
				if (value == null || value.ListView != this)
				{
					return;
				}
				this.SetScrollValue(this.v_scroll, this.item_size.Height * value.Index);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListView" /> uses state image behavior that is compatible with the .NET Framework 1.1 or the .NET Framework 2.0.</summary>
		/// <returns>true if the state image behavior is compatible with the .NET Framework 1.1; false if the behavior is compatible with the .NET Framework 2.0. The default is true.</returns>
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x00080248 File Offset: 0x0007E448
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x0008024C File Offset: 0x0007E44C
		[MonoInternalNote("Stub, not implemented")]
		[Browsable(false)]
		[DefaultValue(true)]
		[EditorBrowsable(2)]
		public bool UseCompatibleStateImageBehavior
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets how items are displayed in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.View" /> values. The default is <see cref="F:System.Windows.Forms.View.LargeIcon" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.View" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x00080250 File Offset: 0x0007E450
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x00080258 File Offset: 0x0007E458
		[DefaultValue(View.LargeIcon)]
		public View View
		{
			get
			{
				return this.view;
			}
			set
			{
				if (!Enum.IsDefined(typeof(View), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(View));
				}
				if (this.view != value)
				{
					if (this.CheckBoxes && value == View.Tile)
					{
						throw new NotSupportedException("CheckBoxes are not supported in Tile view. Choose a different view or set CheckBoxes to false.");
					}
					if (this.VirtualMode && value == View.Tile)
					{
						throw new NotSupportedException("VirtualMode is not supported in Tile view. Choose a different view or set ViewMode to false.");
					}
					ScrollBar scrollBar = this.h_scroll;
					int num = 0;
					this.v_scroll.Value = num;
					scrollBar.Value = num;
					this.view = value;
					this.Redraw(true);
					this.OnUIAViewChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether you have provided your own data-management operations for the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>true if <see cref="T:System.Windows.Forms.ListView" /> uses data-management operations that you provide; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualMode" /> is set to true and one of the following conditions exist:<see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is greater than 0 and <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> is not handled.-or-<see cref="P:System.Windows.Forms.ListView.Items" />, <see cref="P:System.Windows.Forms.ListView.CheckedItems" />, or <see cref="P:System.Windows.Forms.ListView.SelectedItems" /> contains items.-or-Edits are made to <see cref="P:System.Windows.Forms.ListView.Items" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x0008030C File Offset: 0x0007E50C
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x00080314 File Offset: 0x0007E514
		[DefaultValue(false)]
		[RefreshProperties(2)]
		public bool VirtualMode
		{
			get
			{
				return this.virtual_mode;
			}
			set
			{
				if (this.virtual_mode == value)
				{
					return;
				}
				if (!this.virtual_mode && this.items.Count > 0)
				{
					throw new InvalidOperationException();
				}
				if (value && this.view == View.Tile)
				{
					throw new NotSupportedException("VirtualMode is not supported in Tile view. Choose a different view or set ViewMode to false.");
				}
				this.virtual_mode = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the number of <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the list when in virtual mode.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the <see cref="T:System.Windows.Forms.ListView" /> when in virtual mode.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is set to a value less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualMode" /> is set to true, <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is greater than 0, and <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> is not handled.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x0008037C File Offset: 0x0007E57C
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x00080384 File Offset: 0x0007E584
		[DefaultValue(0)]
		[RefreshProperties(2)]
		public int VirtualListSize
		{
			get
			{
				return this.virtual_list_size;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("value");
				}
				if (this.virtual_list_size == value)
				{
					return;
				}
				this.virtual_list_size = value;
				if (this.virtual_mode)
				{
					this.selected_indices.Reset();
					this.Redraw(true);
				}
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x000803D4 File Offset: 0x0007E5D4
		internal int FirstVisibleIndex
		{
			get
			{
				if (this.items.Count == 0)
				{
					return 0;
				}
				if (this.h_marker == 0 && this.v_marker == 0)
				{
					return 0;
				}
				Size itemSize = this.ItemSize;
				if (this.virtual_mode)
				{
					int num = 0;
					switch (this.view)
					{
					case View.LargeIcon:
					case View.SmallIcon:
						num = this.v_marker / (itemSize.Height + this.y_spacing) * this.cols;
						break;
					case View.Details:
						num = this.v_marker / itemSize.Height;
						break;
					case View.List:
						num = this.h_marker / (itemSize.Width * this.x_spacing) * this.rows;
						break;
					}
					if (num >= this.items.Count)
					{
						num = this.items.Count;
					}
					return num;
				}
				for (int i = 0; i < this.items.Count; i++)
				{
					Rectangle rectangle;
					rectangle..ctor(this.GetItemLocation(i), itemSize);
					if (rectangle.Right >= 0 && rectangle.Bottom >= 0)
					{
						return i;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x00080504 File Offset: 0x0007E704
		internal int LastVisibleIndex
		{
			get
			{
				for (int i = this.FirstVisibleIndex; i < this.Items.Count; i++)
				{
					if (this.View == View.List || this.Alignment == ListViewAlignment.Left)
					{
						if (this.GetItemLocation(i).X > this.item_control.ClientRectangle.Right)
						{
							return i - 1;
						}
					}
					else if (this.GetItemLocation(i).Y > this.item_control.ClientRectangle.Bottom)
					{
						return i - 1;
					}
				}
				return this.Items.Count - 1;
			}
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000805B4 File Offset: 0x0007E7B4
		internal void OnSelectedIndexChanged()
		{
			if (base.IsHandleCreated)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000805CC File Offset: 0x0007E7CC
		internal int TotalWidth
		{
			get
			{
				return Math.Max(base.Width, this.layout_wd);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000805E0 File Offset: 0x0007E7E0
		internal int TotalHeight
		{
			get
			{
				return Math.Max(base.Height, this.layout_ht);
			}
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000805F4 File Offset: 0x0007E7F4
		internal void Redraw(bool recalculate)
		{
			if (this.updating)
			{
				return;
			}
			if (this.virtual_mode && !base.IsHandleCreated)
			{
				return;
			}
			if (recalculate)
			{
				this.CalculateListView(this.alignment);
			}
			base.Invalidate(true);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00080640 File Offset: 0x0007E840
		private void InvalidateSelection()
		{
			foreach (object obj in this.SelectedIndices)
			{
				int num = (int)obj;
				this.items[num].Invalidate();
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000806BC File Offset: 0x0007E8BC
		internal Size GetChildColumnSize(int index)
		{
			Size size = Size.Empty;
			ColumnHeader columnHeader = this.columns[index];
			if (columnHeader.Width == -2)
			{
				Size size2 = Size.Ceiling(TextRenderer.MeasureString(columnHeader.Text, this.Font));
				size2.Width += 15;
				size = this.BiggestItem(index);
				if (size2.Width > size.Width)
				{
					size = size2;
				}
			}
			else
			{
				size = this.BiggestItem(index);
				if (size.IsEmpty)
				{
					size.Width = ThemeEngine.Current.ListViewEmptyColumnWidth;
					if (columnHeader.Text.Length > 0)
					{
						size.Height = Size.Ceiling(TextRenderer.MeasureString(columnHeader.Text, this.Font)).Height;
					}
					else
					{
						size.Height = this.Font.Height;
					}
				}
			}
			size.Height += 15;
			if (index == 0)
			{
				size.Width += this.CheckBoxSize.Width + 4;
				if (this.small_image_list != null)
				{
					size.Width += this.small_image_list.ImageSize.Width;
				}
			}
			return size;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x00080808 File Offset: 0x0007EA08
		private Size BiggestItem(int col)
		{
			Size size = Size.Empty;
			Size size2 = Size.Empty;
			bool flag = this.small_image_list != null;
			if (this.virtual_mode && this.items.Count > 0)
			{
				ListViewItem listViewItem = this.items[0];
				size2 = Size.Ceiling(TextRenderer.MeasureString(listViewItem.SubItems[col].Text, this.Font));
				if (flag)
				{
					size2.Width += listViewItem.IndentCount * this.small_image_list.ImageSize.Width;
				}
			}
			else
			{
				foreach (object obj in this.items)
				{
					ListViewItem listViewItem2 = (ListViewItem)obj;
					if (col < listViewItem2.SubItems.Count)
					{
						size = Size.Ceiling(TextRenderer.MeasureString(listViewItem2.SubItems[col].Text, this.Font));
						if (flag)
						{
							size.Width += listViewItem2.IndentCount * this.small_image_list.ImageSize.Width;
						}
						if (size.Width > size2.Width)
						{
							size2 = size;
						}
					}
				}
			}
			if (!size2.IsEmpty && this.view == View.Details)
			{
				size2.Width += ThemeEngine.Current.ListViewItemPaddingWidth;
			}
			return size2;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000809C0 File Offset: 0x0007EBC0
		private void CalcTextSize()
		{
			this.text_size = Size.Empty;
			if (this.items.Count == 0)
			{
				return;
			}
			this.text_size = this.BiggestItem(0);
			if (this.view == View.LargeIcon && this.label_wrap)
			{
				Size empty = Size.Empty;
				if (this.check_boxes)
				{
					empty.Width += 2 * this.CheckBoxSize.Width;
				}
				int num = ((this.LargeImageList != null) ? this.LargeImageList.ImageSize.Width : 12);
				empty.Width += num + 30;
				if (this.text_size.Width > empty.Width)
				{
					this.text_size.Width = empty.Width;
					this.text_size.Height = this.text_size.Height * 2;
				}
			}
			else if (this.view == View.List)
			{
				int num2 = base.Width - (this.CheckBoxSize.Width - 2);
				if (this.small_image_list != null)
				{
					num2 -= this.small_image_list.ImageSize.Width;
				}
				if (this.text_size.Width > num2)
				{
					this.text_size.Width = num2;
				}
			}
			if (this.text_size.Height <= 0)
			{
				this.text_size.Height = this.Font.Height;
			}
			if (this.text_size.Width <= 0)
			{
				this.text_size.Width = base.Width;
			}
			this.text_size.Width = this.text_size.Width + 2;
			this.text_size.Height = this.text_size.Height + 2;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x00080B88 File Offset: 0x0007ED88
		private void SetScrollValue(ScrollBar scrollbar, int val)
		{
			int num;
			if (scrollbar == this.h_scroll)
			{
				num = this.h_scroll.Maximum - this.item_control.Width;
			}
			else
			{
				num = this.v_scroll.Maximum - this.item_control.Height;
			}
			if (val > num)
			{
				val = num;
			}
			else if (val < scrollbar.Minimum)
			{
				val = scrollbar.Minimum;
			}
			scrollbar.Value = val;
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00080C00 File Offset: 0x0007EE00
		private void Scroll(ScrollBar scrollbar, int delta)
		{
			if (delta == 0 || !scrollbar.Visible)
			{
				return;
			}
			this.SetScrollValue(scrollbar, scrollbar.Value + delta);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00080C30 File Offset: 0x0007EE30
		private void CalculateScrollBars()
		{
			Rectangle clientRectangle = base.ClientRectangle;
			int num = clientRectangle.Height;
			int num2 = clientRectangle.Width;
			if (!this.scrollable)
			{
				this.h_scroll.Visible = false;
				this.v_scroll.Visible = false;
				this.item_control.Size = new Size(num2, num);
				this.header_control.Width = num2;
				return;
			}
			if (clientRectangle.Height < 0 || clientRectangle.Width < 0)
			{
				return;
			}
			if (this.layout_wd > clientRectangle.Right)
			{
				this.h_scroll.Visible = true;
				if (this.layout_ht + this.h_scroll.Height > clientRectangle.Bottom)
				{
					this.v_scroll.Visible = true;
				}
				else
				{
					this.v_scroll.Visible = false;
				}
			}
			else if (this.layout_ht > clientRectangle.Bottom)
			{
				this.v_scroll.Visible = true;
				if (this.layout_wd + this.v_scroll.Width > clientRectangle.Right)
				{
					this.h_scroll.Visible = true;
				}
				else
				{
					this.h_scroll.Visible = false;
				}
			}
			else
			{
				this.h_scroll.Visible = false;
				this.v_scroll.Visible = false;
			}
			if (this.h_scroll.is_visible)
			{
				this.h_scroll.Location = new Point(clientRectangle.X, clientRectangle.Bottom - this.h_scroll.Height);
				this.h_scroll.Minimum = 0;
				if (this.v_scroll.Visible)
				{
					this.h_scroll.Maximum = this.layout_wd + this.v_scroll.Width;
					this.h_scroll.Width = clientRectangle.Width - this.v_scroll.Width;
				}
				else
				{
					this.h_scroll.Maximum = this.layout_wd;
					this.h_scroll.Width = clientRectangle.Width;
				}
				this.h_scroll.LargeChange = clientRectangle.Width;
				this.h_scroll.SmallChange = this.item_size.Width + ThemeEngine.Current.ListViewHorizontalSpacing;
				num -= this.h_scroll.Height;
			}
			if (this.v_scroll.is_visible)
			{
				this.v_scroll.Location = new Point(clientRectangle.Right - this.v_scroll.Width, clientRectangle.Y);
				this.v_scroll.Minimum = 0;
				this.v_scroll.Maximum = this.layout_ht;
				if (this.h_scroll.Visible)
				{
					this.v_scroll.Height = clientRectangle.Height - this.h_scroll.Height;
				}
				else
				{
					this.v_scroll.Height = clientRectangle.Height;
				}
				this.v_scroll.LargeChange = clientRectangle.Height;
				this.v_scroll.SmallChange = this.Font.Height;
				num2 -= this.v_scroll.Width;
			}
			this.item_control.Size = new Size(num2, num);
			if (this.header_control.is_visible)
			{
				this.header_control.Width = num2;
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x00080F80 File Offset: 0x0007F180
		internal int GetReorderedColumnIndex(ColumnHeader column)
		{
			if (this.reordered_column_indices == null)
			{
				return column.Index;
			}
			for (int i = 0; i < this.Columns.Count; i++)
			{
				if (this.reordered_column_indices[i] == column.Index)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x00080FD4 File Offset: 0x0007F1D4
		internal ColumnHeader GetReorderedColumn(int index)
		{
			if (this.reordered_column_indices == null)
			{
				return this.Columns[index];
			}
			return this.Columns[this.reordered_column_indices[index]];
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0008100C File Offset: 0x0007F20C
		internal void ReorderColumn(ColumnHeader col, int index, bool fireEvent)
		{
			if (fireEvent)
			{
				ColumnReorderedEventHandler columnReorderedEventHandler = (ColumnReorderedEventHandler)base.Events[ListView.ColumnReorderedEvent];
				if (columnReorderedEventHandler != null)
				{
					ColumnReorderedEventArgs columnReorderedEventArgs = new ColumnReorderedEventArgs(col.Index, index, col);
					columnReorderedEventHandler(this, columnReorderedEventArgs);
					if (columnReorderedEventArgs.Cancel)
					{
						this.header_control.Invalidate();
						this.item_control.Invalidate();
						return;
					}
				}
			}
			int count = this.Columns.Count;
			if (this.reordered_column_indices == null)
			{
				this.reordered_column_indices = new int[count];
				for (int i = 0; i < count; i++)
				{
					this.reordered_column_indices[i] = i;
				}
			}
			if (this.reordered_column_indices[index] == col.Index)
			{
				return;
			}
			int[] array = this.reordered_column_indices;
			int[] array2 = new int[count];
			int num = 0;
			for (int j = 0; j < count; j++)
			{
				if (num < count && array[num] == col.Index)
				{
					num++;
				}
				if (j == index)
				{
					array2[j] = col.Index;
				}
				else
				{
					array2[j] = array[num++];
				}
			}
			this.ReorderColumns(array2, true);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0008113C File Offset: 0x0007F33C
		internal void ReorderColumns(int[] display_indices, bool redraw)
		{
			this.reordered_column_indices = display_indices;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				ColumnHeader columnHeader = this.Columns[i];
				columnHeader.InternalDisplayIndex = this.reordered_column_indices[i];
			}
			if (redraw && this.view == View.Details && base.IsHandleCreated)
			{
				this.LayoutDetails();
				this.header_control.Invalidate();
				this.item_control.Invalidate();
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000811C0 File Offset: 0x0007F3C0
		internal void AddColumn(ColumnHeader newCol, int index, bool redraw)
		{
			int count = this.Columns.Count;
			newCol.SetListView(this);
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				ColumnHeader columnHeader = this.Columns[i];
				if (i == index)
				{
					array[i] = index;
				}
				else
				{
					int internalDisplayIndex = columnHeader.InternalDisplayIndex;
					if (internalDisplayIndex < index)
					{
						array[i] = internalDisplayIndex;
					}
					else
					{
						array[i] = internalDisplayIndex + 1;
					}
				}
			}
			this.ReorderColumns(array, redraw);
			base.Invalidate();
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00081248 File Offset: 0x0007F448
		private Size LargeIconItemSize
		{
			get
			{
				int num = ((this.LargeImageList != null) ? this.LargeImageList.ImageSize.Width : 12);
				int num2 = ((this.LargeImageList != null) ? this.LargeImageList.ImageSize.Height : 2);
				int num3 = this.text_size.Height + 2 + Math.Max(this.CheckBoxSize.Height, num2);
				int num4 = Math.Max(this.text_size.Width, num);
				if (this.check_boxes)
				{
					num4 += 2 + this.CheckBoxSize.Width;
				}
				return new Size(num4, num3);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x00081300 File Offset: 0x0007F500
		private Size SmallIconItemSize
		{
			get
			{
				int num = ((this.SmallImageList != null) ? this.SmallImageList.ImageSize.Width : 0);
				int num2 = ((this.SmallImageList != null) ? this.SmallImageList.ImageSize.Height : 0);
				int num3 = Math.Max(this.text_size.Height, Math.Max(this.CheckBoxSize.Height, num2));
				int num4 = this.text_size.Width + num;
				if (this.check_boxes)
				{
					num4 += 2 + this.CheckBoxSize.Width;
				}
				return new Size(num4, num3);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x000813B4 File Offset: 0x0007F5B4
		private Size TileItemSize
		{
			get
			{
				if (this.tile_size == Size.Empty)
				{
					int num = ((this.LargeImageList != null) ? this.LargeImageList.ImageSize.Width : 0);
					int num2 = ((this.LargeImageList != null) ? this.LargeImageList.ImageSize.Height : 0);
					int num3 = (int)this.Font.Size * ThemeEngine.Current.ListViewTileWidthFactor + num + 4;
					int num4 = Math.Max((int)this.Font.Size * ThemeEngine.Current.ListViewTileHeightFactor, num2);
					this.tile_size = new Size(num3, num4);
				}
				return this.tile_size;
			}
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x00081470 File Offset: 0x0007F670
		private int GetDetailsItemHeight()
		{
			int num = ((!this.CheckBoxes) ? 0 : this.CheckBoxSize.Height);
			int num2 = ((this.SmallImageList != null) ? this.SmallImageList.ImageSize.Height : 0);
			int num3 = Math.Max(num, this.text_size.Height);
			return Math.Max(num3, num2);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000814E0 File Offset: 0x0007F6E0
		private void SetItemLocation(int index, int x, int y, int row, int col)
		{
			Point point = this.items_location[index];
			if (point.X == x && point.Y == y)
			{
				return;
			}
			this.items_location[index] = new Point(x, y);
			this.items_matrix_location[index] = new ListView.ItemMatrixLocation(row, col);
			this.reordered_items_indices[index] = index;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00081558 File Offset: 0x0007F758
		private void ShiftItemsPositions(int from, int to, bool forward)
		{
			if (forward)
			{
				for (int i = to + 1; i > from; i--)
				{
					this.reordered_items_indices[i] = this.reordered_items_indices[i - 1];
					ListViewItem listViewItem = this.items[this.reordered_items_indices[i]];
					listViewItem.Invalidate();
					listViewItem.DisplayIndex = i;
					listViewItem.Invalidate();
				}
			}
			else
			{
				for (int j = from - 1; j < to; j++)
				{
					this.reordered_items_indices[j] = this.reordered_items_indices[j + 1];
					ListViewItem listViewItem2 = this.items[this.reordered_items_indices[j]];
					listViewItem2.Invalidate();
					listViewItem2.DisplayIndex = j;
					listViewItem2.Invalidate();
				}
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0008160C File Offset: 0x0007F80C
		internal void ChangeItemLocation(int display_index, Point new_pos)
		{
			int displayIndexFromLocation = this.GetDisplayIndexFromLocation(new_pos);
			if (displayIndexFromLocation == display_index)
			{
				return;
			}
			int num = this.reordered_items_indices[display_index];
			ListViewItem listViewItem = this.items[num];
			bool flag = displayIndexFromLocation < display_index;
			int num2;
			int num3;
			if (flag)
			{
				num2 = displayIndexFromLocation;
				num3 = display_index - 1;
			}
			else
			{
				num2 = display_index + 1;
				num3 = displayIndexFromLocation;
			}
			this.ShiftItemsPositions(num2, num3, flag);
			this.reordered_items_indices[displayIndexFromLocation] = num;
			listViewItem.Invalidate();
			listViewItem.DisplayIndex = displayIndexFromLocation;
			listViewItem.Invalidate();
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00081688 File Offset: 0x0007F888
		private int GetDisplayIndexFromLocation(Point loc)
		{
			int num = -1;
			if (loc.X < 0 || loc.Y < 0)
			{
				return 0;
			}
			loc.X -= this.item_size.Width / 2;
			if (loc.X < 0)
			{
				loc.X = 0;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				Rectangle rectangle;
				rectangle..ctor(this.GetItemLocation(i), this.item_size);
				rectangle.Inflate(ThemeEngine.Current.ListViewHorizontalSpacing, ThemeEngine.Current.ListViewVerticalSpacing);
				if (rectangle.Contains(loc))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				num = this.items.Count - 1;
			}
			return num;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x00081758 File Offset: 0x0007F958
		private int GetDefaultGroupItems()
		{
			int num = 0;
			foreach (object obj in this.items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.Group == null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000817D4 File Offset: 0x0007F9D4
		private void CalculateRowsAndCols(Size item_size, bool left_aligned, int x_spacing, int y_spacing)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			if (this.UseCustomColumnWidth)
			{
				this.CalculateCustomColumnWidth();
			}
			if (this.UsingGroups)
			{
				this.rows = 0;
				this.cols = 0;
				int num = 0;
				this.groups.DefaultGroup.ItemCount = this.GetDefaultGroupItems();
				for (int i = 0; i < this.groups.InternalCount; i++)
				{
					ListViewGroup internalGroup = this.groups.GetInternalGroup(i);
					int actualItemCount = internalGroup.GetActualItemCount();
					if (actualItemCount != 0)
					{
						int num2 = (int)Math.Floor((double)(clientRectangle.Width - this.v_scroll.Width + x_spacing) / (double)(item_size.Width + x_spacing));
						if (num2 <= 0)
						{
							num2 = 1;
						}
						int num3 = (int)Math.Ceiling((double)actualItemCount / (double)num2);
						internalGroup.starting_row = this.rows;
						internalGroup.rows = num3;
						internalGroup.starting_item = num;
						internalGroup.current_item = 0;
						this.cols = Math.Max(num2, this.cols);
						this.rows += num3;
						num += actualItemCount;
					}
				}
			}
			else if (left_aligned)
			{
				this.rows = (int)Math.Floor((double)(clientRectangle.Height - this.h_scroll.Height + y_spacing) / (double)(item_size.Height + y_spacing));
				if (this.rows <= 0)
				{
					this.rows = 1;
				}
				this.cols = (int)Math.Ceiling((double)this.items.Count / (double)this.rows);
			}
			else
			{
				if (this.UseCustomColumnWidth)
				{
					this.cols = (int)Math.Floor((double)(clientRectangle.Width - this.v_scroll.Width) / (double)this.custom_column_width);
				}
				else
				{
					this.cols = (int)Math.Floor((double)(clientRectangle.Width - this.v_scroll.Width + x_spacing) / (double)(item_size.Width + x_spacing));
				}
				if (this.cols < 1)
				{
					this.cols = 1;
				}
				this.rows = (int)Math.Ceiling((double)this.items.Count / (double)this.cols);
			}
			this.item_index_matrix = new int[this.rows, this.cols];
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00081A14 File Offset: 0x0007FC14
		private void CalculateCustomColumnWidth()
		{
			int num = int.MaxValue;
			for (int i = 0; i < this.columns.Count; i++)
			{
				int width = this.columns[i].Width;
				if (width < num)
				{
					num = width;
				}
			}
			this.custom_column_width = num;
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00081A68 File Offset: 0x0007FC68
		private void LayoutIcons(Size item_size, bool left_aligned, int x_spacing, int y_spacing)
		{
			this.header_control.Visible = false;
			this.header_control.Size = Size.Empty;
			this.item_control.Visible = true;
			this.item_control.Location = Point.Empty;
			this.ItemSize = item_size;
			this.x_spacing = x_spacing;
			this.y_spacing = y_spacing;
			if (this.items.Count == 0)
			{
				return;
			}
			Size size = item_size;
			this.CalculateRowsAndCols(size, left_aligned, x_spacing, y_spacing);
			this.layout_wd = ((!this.UseCustomColumnWidth) ? (this.cols * (size.Width + x_spacing) - x_spacing) : (this.cols * this.custom_column_width));
			this.layout_ht = this.rows * (size.Height + y_spacing) - y_spacing;
			if (this.virtual_mode)
			{
				this.item_control.Size = new Size(this.layout_wd, this.layout_ht);
				return;
			}
			bool usingGroups = this.UsingGroups;
			if (usingGroups)
			{
				this.CalculateGroupsLayout(size, y_spacing, 0);
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.items.Count; i++)
			{
				ListViewItem listViewItem = this.items[i];
				int num4;
				int num5;
				int num6;
				if (usingGroups)
				{
					ListViewGroup listViewGroup = listViewItem.Group;
					if (listViewGroup == null)
					{
						listViewGroup = this.groups.DefaultGroup;
					}
					Point items_area_location = listViewGroup.items_area_location;
					int num3 = listViewGroup.current_item++;
					int starting_row = listViewGroup.starting_row;
					num4 = listViewGroup.starting_item + num3;
					num = num3 / this.cols;
					num2 = num3 % this.cols;
					num5 = ((!this.UseCustomColumnWidth) ? (num2 * (item_size.Width + x_spacing)) : (num2 * this.custom_column_width));
					num6 = num * (item_size.Height + y_spacing) + items_area_location.Y;
					this.SetItemLocation(num4, num5, num6, num + starting_row, num2);
					this.SetItemAtDisplayIndex(num4, i);
					this.item_index_matrix[num + starting_row, num2] = i;
				}
				else
				{
					num5 = ((!this.UseCustomColumnWidth) ? (num2 * (item_size.Width + x_spacing)) : (num2 * this.custom_column_width));
					num6 = num * (item_size.Height + y_spacing);
					num4 = i;
					this.SetItemLocation(i, num5, num6, num, num2);
					this.item_index_matrix[num, num2] = i;
					if (left_aligned)
					{
						num++;
						if (num == this.rows)
						{
							num = 0;
							num2++;
						}
					}
					else if (++num2 == this.cols)
					{
						num2 = 0;
						num++;
					}
				}
				listViewItem.Layout();
				listViewItem.DisplayIndex = num4;
				listViewItem.SetPosition(new Point(num5, num6));
			}
			this.item_control.Size = new Size(this.layout_wd, this.layout_ht);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00081D4C File Offset: 0x0007FF4C
		private void CalculateGroupsLayout(Size item_size, int y_spacing, int y_origin)
		{
			int num = y_origin;
			bool flag = this.view == View.Details;
			for (int i = 0; i < this.groups.InternalCount; i++)
			{
				ListViewGroup internalGroup = this.groups.GetInternalGroup(i);
				if (internalGroup.ItemCount != 0)
				{
					num += this.LayoutGroupHeader(internalGroup, num, item_size.Height, y_spacing, (!flag) ? internalGroup.rows : internalGroup.ItemCount);
				}
			}
			this.layout_ht = num;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00081DD0 File Offset: 0x0007FFD0
		private int LayoutGroupHeader(ListViewGroup group, int y_origin, int item_height, int y_spacing, int rows)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			int num = this.Font.Height + 15;
			group.HeaderBounds = new Rectangle(0, y_origin, clientRectangle.Width - this.v_scroll.Width, num);
			group.items_area_location = new Point(0, y_origin + num);
			int num2 = (item_height + y_spacing) * rows;
			return num + num2 + 10;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00081E34 File Offset: 0x00080034
		private void CalculateDetailsGroupItemsCount()
		{
			int num = 0;
			this.groups.DefaultGroup.ItemCount = this.GetDefaultGroupItems();
			for (int i = 0; i < this.groups.InternalCount; i++)
			{
				ListViewGroup internalGroup = this.groups.GetInternalGroup(i);
				int actualItemCount = internalGroup.GetActualItemCount();
				if (actualItemCount != 0)
				{
					internalGroup.starting_item = num;
					internalGroup.current_item = 0;
					num += actualItemCount;
				}
			}
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x00081EA8 File Offset: 0x000800A8
		private void LayoutHeader()
		{
			int num = 0;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				ColumnHeader reorderedColumn = this.GetReorderedColumn(i);
				reorderedColumn.X = num;
				reorderedColumn.Y = 0;
				reorderedColumn.CalcColumnHeader();
				num += reorderedColumn.Wd;
			}
			this.layout_wd = num;
			if (num < base.ClientRectangle.Width)
			{
				num = base.ClientRectangle.Width;
			}
			if (this.header_style == ColumnHeaderStyle.None)
			{
				this.header_control.Visible = false;
				this.header_control.Size = Size.Empty;
				this.layout_wd = base.ClientRectangle.Width;
			}
			else
			{
				this.header_control.Width = num;
				this.header_control.Height = ((this.columns.Count <= 0) ? ThemeEngine.Current.ListViewGetHeaderHeight(this, this.Font) : this.columns[0].Ht);
				this.header_control.Visible = true;
			}
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x00081FC0 File Offset: 0x000801C0
		private void LayoutDetails()
		{
			this.LayoutHeader();
			if (this.columns.Count == 0)
			{
				this.item_control.Visible = false;
				this.layout_wd = base.ClientRectangle.Width;
				this.layout_ht = base.ClientRectangle.Height;
				return;
			}
			this.item_control.Visible = true;
			this.item_control.Location = Point.Empty;
			this.item_control.Width = base.ClientRectangle.Width;
			int detailsItemHeight = this.GetDetailsItemHeight();
			this.ItemSize = new Size(0, detailsItemHeight);
			int num = this.header_control.Height;
			this.layout_ht = num + detailsItemHeight * this.items.Count;
			if (this.items.Count > 0 && this.grid_lines)
			{
				this.layout_ht += 2;
			}
			bool usingGroups = this.UsingGroups;
			if (usingGroups)
			{
				this.CalculateDetailsGroupItemsCount();
				this.CalculateGroupsLayout(this.ItemSize, 2, num);
			}
			if (this.virtual_mode)
			{
				return;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				ListViewItem listViewItem = this.items[i];
				int num3;
				int num4;
				if (usingGroups)
				{
					ListViewGroup listViewGroup = listViewItem.Group;
					if (listViewGroup == null)
					{
						listViewGroup = this.groups.DefaultGroup;
					}
					int num2 = listViewGroup.current_item++;
					Point items_area_location = listViewGroup.items_area_location;
					num3 = listViewGroup.starting_item + num2;
					num4 = (num = num2 * (detailsItemHeight + 2) + items_area_location.Y);
					this.SetItemLocation(num3, 0, num4, 0, 0);
					this.SetItemAtDisplayIndex(num3, i);
				}
				else
				{
					num3 = i;
					num4 = num;
					this.SetItemLocation(i, 0, num4, 0, 0);
					num += detailsItemHeight;
				}
				listViewItem.Layout();
				listViewItem.DisplayIndex = num3;
				listViewItem.SetPosition(new Point(0, num4));
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000821B8 File Offset: 0x000803B8
		private void AdjustItemsPositionArray(int count)
		{
			if (this.virtual_mode)
			{
				return;
			}
			if (this.items_location.Length >= count)
			{
				return;
			}
			count = Math.Max(count, this.items_location.Length * 2);
			this.items_location = new Point[count];
			this.items_matrix_location = new ListView.ItemMatrixLocation[count];
			this.reordered_items_indices = new int[count];
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x00082218 File Offset: 0x00080418
		private void CalculateListView(ListViewAlignment align)
		{
			this.CalcTextSize();
			this.AdjustItemsPositionArray(this.items.Count);
			switch (this.view)
			{
			case View.LargeIcon:
				break;
			case View.Details:
				this.LayoutDetails();
				goto IL_00F5;
			case View.SmallIcon:
				this.LayoutIcons(this.SmallIconItemSize, this.alignment == ListViewAlignment.Left, ThemeEngine.Current.ListViewHorizontalSpacing, 2);
				goto IL_00F5;
			case View.List:
				this.LayoutIcons(this.SmallIconItemSize, true, ThemeEngine.Current.ListViewHorizontalSpacing, 2);
				goto IL_00F5;
			case View.Tile:
				if (Application.VisualStylesEnabled)
				{
					this.LayoutIcons(this.TileItemSize, this.alignment == ListViewAlignment.Left, ThemeEngine.Current.ListViewHorizontalSpacing, ThemeEngine.Current.ListViewVerticalSpacing);
					goto IL_00F5;
				}
				break;
			default:
				goto IL_00F5;
			}
			this.LayoutIcons(this.LargeIconItemSize, this.alignment == ListViewAlignment.Left, ThemeEngine.Current.ListViewHorizontalSpacing, ThemeEngine.Current.ListViewVerticalSpacing);
			IL_00F5:
			this.CalculateScrollBars();
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x00082320 File Offset: 0x00080520
		internal Point GetItemLocation(int index)
		{
			Point point = Point.Empty;
			if (this.virtual_mode)
			{
				point = this.GetFixedItemLocation(index);
			}
			else
			{
				point = this.items_location[index];
			}
			point.X -= this.h_marker;
			point.Y -= this.v_marker;
			return point;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x00082388 File Offset: 0x00080588
		private Point GetFixedItemLocation(int index)
		{
			Point empty = Point.Empty;
			switch (this.view)
			{
			case View.LargeIcon:
			case View.SmallIcon:
				empty.X = index % this.cols * (this.item_size.Width + this.x_spacing);
				empty.Y = index / this.cols * (this.item_size.Height + this.y_spacing);
				break;
			case View.Details:
				empty.Y = this.header_control.Height + index * this.item_size.Height;
				break;
			case View.List:
				empty.X = index / this.rows * (this.item_size.Width + this.x_spacing);
				empty.Y = index % this.rows * (this.item_size.Height + this.y_spacing);
				break;
			}
			return empty;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x00082478 File Offset: 0x00080678
		internal int GetItemIndex(int display_index)
		{
			if (this.virtual_mode)
			{
				return display_index;
			}
			return this.reordered_items_indices[display_index];
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00082490 File Offset: 0x00080690
		internal ListViewItem GetItemAtDisplayIndex(int display_index)
		{
			if (this.virtual_mode)
			{
				return this.items[display_index];
			}
			return this.items[this.reordered_items_indices[display_index]];
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000824C0 File Offset: 0x000806C0
		internal void SetItemAtDisplayIndex(int display_index, int index)
		{
			this.reordered_items_indices[display_index] = index;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000824CC File Offset: 0x000806CC
		private bool KeySearchString(KeyEventArgs ke)
		{
			int tickCount = Environment.TickCount;
			if (this.keysearch_tickcnt > 0 && tickCount - this.keysearch_tickcnt > ListView.keysearch_keydelay)
			{
				this.keysearch_text = string.Empty;
			}
			if (!char.IsLetterOrDigit((char)ke.KeyCode))
			{
				return false;
			}
			this.keysearch_text += (char)ke.KeyCode;
			this.keysearch_tickcnt = tickCount;
			int num = ((this.FocusedItem != null) ? this.FocusedItem.DisplayIndex : 0);
			int num2 = ((num + 1 >= this.Items.Count) ? 0 : (num + 1));
			ListViewItem listViewItem = this.FindItemWithText(this.keysearch_text, false, num2, true, true);
			if (listViewItem != null && num != listViewItem.DisplayIndex)
			{
				this.selected_indices.Clear();
				this.SetFocusedItem(listViewItem.DisplayIndex);
				listViewItem.Selected = true;
				this.EnsureVisible(this.GetItemIndex(listViewItem.DisplayIndex));
			}
			return true;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000825D0 File Offset: 0x000807D0
		private void OnItemsChanged()
		{
			this.ResetSearchString();
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000825D8 File Offset: 0x000807D8
		private void ResetSearchString()
		{
			this.keysearch_text = string.Empty;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000825E8 File Offset: 0x000807E8
		private int GetAdjustedIndex(Keys key)
		{
			int num = -1;
			if (this.View == View.Details)
			{
				switch (key)
				{
				case Keys.PageUp:
				{
					int num2 = this.FirstVisibleIndex;
					if (this.GetItemLocation(num2).Y < 0)
					{
						num2++;
					}
					if (this.FocusedItem.DisplayIndex == num2)
					{
						if (num2 > 0)
						{
							int num3 = this.item_control.Height / this.ItemSize.Height - 1;
							num = num2 - num3 + 1;
							if (num < 0)
							{
								num = 0;
							}
						}
					}
					else
					{
						num = num2;
					}
					break;
				}
				case Keys.PageDown:
				{
					int num4 = this.LastVisibleIndex;
					Rectangle rectangle;
					rectangle..ctor(this.GetItemLocation(num4), this.ItemSize);
					if (rectangle.Bottom > this.item_control.ClientRectangle.Bottom)
					{
						num4--;
					}
					if (this.FocusedItem.DisplayIndex == num4)
					{
						if (this.FocusedItem.DisplayIndex < this.Items.Count - 1)
						{
							int num5 = this.item_control.Height / this.ItemSize.Height - 1;
							num = this.FocusedItem.DisplayIndex + num5 - 1;
							if (num >= this.Items.Count)
							{
								num = this.Items.Count - 1;
							}
						}
					}
					else
					{
						num = num4;
					}
					break;
				}
				case Keys.Up:
					num = this.FocusedItem.DisplayIndex - 1;
					break;
				case Keys.Down:
					num = this.FocusedItem.DisplayIndex + 1;
					if (num == this.items.Count)
					{
						num = -1;
					}
					break;
				}
				return num;
			}
			if (this.virtual_mode)
			{
				return this.GetFixedAdjustedIndex(key);
			}
			ListView.ItemMatrixLocation itemMatrixLocation = this.items_matrix_location[this.FocusedItem.DisplayIndex];
			int num6 = itemMatrixLocation.Row;
			int num7 = itemMatrixLocation.Col;
			int num8;
			switch (key)
			{
			case Keys.Left:
				if (num7 == 0)
				{
					return -1;
				}
				num8 = this.item_index_matrix[num6, num7 - 1];
				break;
			case Keys.Up:
				if (num6 == 0)
				{
					return -1;
				}
				while (this.item_index_matrix[num6 - 1, num7] == 0 && num6 != 1)
				{
					num7--;
					if (num7 < 0)
					{
						return -1;
					}
				}
				num8 = this.item_index_matrix[num6 - 1, num7];
				break;
			case Keys.Right:
				if (num7 == this.cols - 1)
				{
					return -1;
				}
				while (this.item_index_matrix[num6, num7 + 1] == 0)
				{
					num6--;
					if (num6 < 0)
					{
						return -1;
					}
				}
				num8 = this.item_index_matrix[num6, num7 + 1];
				break;
			case Keys.Down:
				if (num6 == this.rows - 1 || num6 == this.Items.Count - 1)
				{
					return -1;
				}
				while (this.item_index_matrix[num6 + 1, num7] == 0)
				{
					num7--;
					if (num7 < 0)
					{
						return -1;
					}
				}
				num8 = this.item_index_matrix[num6 + 1, num7];
				break;
			default:
				return -1;
			}
			return this.items[num8].DisplayIndex;
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x00082960 File Offset: 0x00080B60
		private int GetFixedAdjustedIndex(Keys key)
		{
			int num;
			switch (key)
			{
			case Keys.Left:
				if (this.view == View.List)
				{
					num = this.focused_item_index - this.rows;
				}
				else
				{
					num = this.focused_item_index - 1;
				}
				break;
			case Keys.Up:
				if (this.view != View.List)
				{
					num = this.focused_item_index - this.cols;
				}
				else
				{
					num = this.focused_item_index - 1;
				}
				break;
			case Keys.Right:
				if (this.view == View.List)
				{
					num = this.focused_item_index + this.rows;
				}
				else
				{
					num = this.focused_item_index + 1;
				}
				break;
			case Keys.Down:
				if (this.view != View.List)
				{
					num = this.focused_item_index + this.cols;
				}
				else
				{
					num = this.focused_item_index + 1;
				}
				break;
			default:
				return -1;
			}
			if (num < 0 || num >= this.items.Count)
			{
				num = this.focused_item_index;
			}
			return num;
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x00082A64 File Offset: 0x00080C64
		private bool SelectItems(ArrayList sel_items)
		{
			bool flag = false;
			foreach (object obj in this.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (!sel_items.Contains(listViewItem))
				{
					listViewItem.Selected = false;
					flag = true;
				}
			}
			foreach (object obj2 in sel_items)
			{
				ListViewItem listViewItem2 = (ListViewItem)obj2;
				if (!listViewItem2.Selected)
				{
					listViewItem2.Selected = true;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x00082B54 File Offset: 0x00080D54
		private void UpdateMultiSelection(int index, bool reselect)
		{
			bool flag = (XplatUI.State.ModifierKeys & Keys.Shift) != Keys.None;
			bool flag2 = (XplatUI.State.ModifierKeys & Keys.Control) != Keys.None;
			ListViewItem itemAtDisplayIndex = this.GetItemAtDisplayIndex(index);
			if (flag && this.selection_start != null)
			{
				ArrayList arrayList = new ArrayList();
				int displayIndex = this.selection_start.DisplayIndex;
				int num = Math.Min(displayIndex, index);
				int num2 = Math.Max(displayIndex, index);
				if (this.View == View.Details)
				{
					for (int i = num; i <= num2; i++)
					{
						arrayList.Add(this.GetItemAtDisplayIndex(i));
					}
				}
				else
				{
					ListView.ItemMatrixLocation itemMatrixLocation = this.items_matrix_location[num];
					ListView.ItemMatrixLocation itemMatrixLocation2 = this.items_matrix_location[num2];
					int num3 = Math.Min(itemMatrixLocation.Col, itemMatrixLocation2.Col);
					int num4 = Math.Max(itemMatrixLocation.Col, itemMatrixLocation2.Col);
					int num5 = Math.Min(itemMatrixLocation.Row, itemMatrixLocation2.Row);
					int num6 = Math.Max(itemMatrixLocation.Row, itemMatrixLocation2.Row);
					for (int j = 0; j < this.items.Count; j++)
					{
						ListView.ItemMatrixLocation itemMatrixLocation3 = this.items_matrix_location[j];
						if (itemMatrixLocation3.Row >= num5 && itemMatrixLocation3.Row <= num6 && itemMatrixLocation3.Col >= num3 && itemMatrixLocation3.Col <= num4)
						{
							arrayList.Add(this.GetItemAtDisplayIndex(j));
						}
					}
				}
				this.SelectItems(arrayList);
			}
			else if (flag2)
			{
				itemAtDisplayIndex.Selected = !itemAtDisplayIndex.Selected;
				this.selection_start = itemAtDisplayIndex;
			}
			else
			{
				if (!reselect)
				{
					foreach (object obj in this.SelectedIndices)
					{
						int num7 = (int)obj;
						if (index != num7)
						{
							this.items[num7].Selected = false;
						}
					}
				}
				else
				{
					this.SelectedItems.Clear();
					itemAtDisplayIndex.Selected = true;
				}
				this.selection_start = itemAtDisplayIndex;
			}
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00082DCC File Offset: 0x00080FCC
		internal override bool InternalPreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256)
			{
				Keys keys = (Keys)msg.WParam.ToInt32();
				this.HandleNavKeys(keys);
			}
			return base.InternalPreProcessMessage(ref msg);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00082E08 File Offset: 0x00081008
		private bool HandleNavKeys(Keys key_data)
		{
			if (this.Items.Count == 0 || !this.item_control.Visible)
			{
				return false;
			}
			if (this.FocusedItem == null)
			{
				this.SetFocusedItem(0);
			}
			switch (key_data)
			{
			case Keys.Space:
				this.SelectIndex(this.focused_item_index);
				this.ToggleItemsCheckState();
				break;
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				this.SelectIndex(this.GetAdjustedIndex(key_data));
				break;
			case Keys.End:
				this.SelectIndex(this.Items.Count - 1);
				break;
			case Keys.Home:
				this.SelectIndex(0);
				break;
			default:
				if (key_data != Keys.Return)
				{
					return false;
				}
				if (this.selected_indices.Count > 0)
				{
					this.OnItemActivate(EventArgs.Empty);
				}
				break;
			}
			return true;
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00082EF8 File Offset: 0x000810F8
		private void ToggleItemsCheckState()
		{
			if (!this.CheckBoxes)
			{
				return;
			}
			if (this.StateImageList != null && this.StateImageList.Images.Count < 2)
			{
				return;
			}
			if (this.SelectedIndices.Count > 0)
			{
				for (int i = 0; i < this.SelectedIndices.Count; i++)
				{
					ListViewItem listViewItem = this.Items[this.SelectedIndices[i]];
					listViewItem.Checked = !listViewItem.Checked;
				}
				return;
			}
			if (this.FocusedItem != null)
			{
				this.FocusedItem.Checked = !this.FocusedItem.Checked;
				this.SelectIndex(this.FocusedItem.Index);
			}
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x00082FC0 File Offset: 0x000811C0
		private void SelectIndex(int display_index)
		{
			if (display_index == -1)
			{
				return;
			}
			if (this.MultiSelect)
			{
				this.UpdateMultiSelection(display_index, true);
			}
			else if (!this.GetItemAtDisplayIndex(display_index).Selected)
			{
				this.GetItemAtDisplayIndex(display_index).Selected = true;
			}
			this.SetFocusedItem(display_index);
			this.EnsureVisible(this.GetItemIndex(display_index));
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00083020 File Offset: 0x00081220
		private void ListView_KeyDown(object sender, KeyEventArgs ke)
		{
			if (ke.Handled || this.Items.Count == 0 || !this.item_control.Visible)
			{
				return;
			}
			if (ke.Alt || ke.Control)
			{
				return;
			}
			ke.Handled = this.KeySearchString(ke);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00083080 File Offset: 0x00081280
		private MouseEventArgs TranslateMouseEventArgs(MouseEventArgs args)
		{
			Point point = base.PointToClient(Control.MousePosition);
			return new MouseEventArgs(args.Button, args.Clicks, point.X, point.Y, args.Delta);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000830C0 File Offset: 0x000812C0
		internal override void OnPaintInternal(PaintEventArgs pe)
		{
			if (this.updating)
			{
				return;
			}
			this.CalculateScrollBars();
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000830D4 File Offset: 0x000812D4
		private void FocusChanged(object o, EventArgs args)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			if (this.FocusedItem == null)
			{
				this.SetFocusedItem(0);
			}
			ListViewItem focusedItem = this.FocusedItem;
			if (focusedItem.ListView != null)
			{
				focusedItem.Invalidate();
				focusedItem.Layout();
				focusedItem.Invalidate();
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00083128 File Offset: 0x00081328
		private void ListView_Invalidated(object sender, InvalidateEventArgs e)
		{
			this.header_control.Invalidate();
			this.item_control.Invalidate();
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00083140 File Offset: 0x00081340
		private void ListView_MouseEnter(object sender, EventArgs args)
		{
			this.hover_pending = true;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0008314C File Offset: 0x0008134C
		private void ListView_MouseWheel(object sender, MouseEventArgs me)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			int num = me.Delta / 120;
			if (num == 0)
			{
				return;
			}
			switch (this.View)
			{
			case View.LargeIcon:
				break;
			case View.Details:
			case View.SmallIcon:
				this.Scroll(this.v_scroll, -this.ItemSize.Height * SystemInformation.MouseWheelScrollLines * num);
				return;
			case View.List:
				this.Scroll(this.h_scroll, -this.ItemSize.Width * num);
				return;
			case View.Tile:
				if (Application.VisualStylesEnabled)
				{
					this.Scroll(this.v_scroll, -(this.ItemSize.Height + ThemeEngine.Current.ListViewVerticalSpacing) * 2 * num);
					return;
				}
				break;
			default:
				return;
			}
			this.Scroll(this.v_scroll, -(this.ItemSize.Height + ThemeEngine.Current.ListViewVerticalSpacing) * num);
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00083258 File Offset: 0x00081458
		private void ListView_SizeChanged(object sender, EventArgs e)
		{
			this.Redraw(true);
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x00083264 File Offset: 0x00081464
		private void SetFocusedItem(int display_index)
		{
			if (display_index != -1)
			{
				this.GetItemAtDisplayIndex(display_index).Focused = true;
			}
			else if (this.focused_item_index != -1 && this.focused_item_index < this.items.Count)
			{
				this.GetItemAtDisplayIndex(this.focused_item_index).Focused = false;
			}
			this.focused_item_index = display_index;
			if (display_index == -1)
			{
				this.OnUIAFocusedItemChanged();
			}
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000832D4 File Offset: 0x000814D4
		private void HorizontalScroller(object sender, EventArgs e)
		{
			this.item_control.EndEdit(this.item_control.edit_item);
			if (this.h_marker != this.h_scroll.Value)
			{
				int num = this.h_marker - this.h_scroll.Value;
				this.h_marker = this.h_scroll.Value;
				if (this.header_control.Visible)
				{
					XplatUI.ScrollWindow(this.header_control.Handle, num, 0, false);
				}
				XplatUI.ScrollWindow(this.item_control.Handle, num, 0, false);
			}
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x00083368 File Offset: 0x00081568
		private void VerticalScroller(object sender, EventArgs e)
		{
			this.item_control.EndEdit(this.item_control.edit_item);
			if (this.v_marker != this.v_scroll.Value)
			{
				int num = this.v_marker - this.v_scroll.Value;
				Rectangle clientRectangle = this.item_control.ClientRectangle;
				if (this.header_control.Visible)
				{
					clientRectangle.Y += this.header_control.Height;
					clientRectangle.Height -= this.header_control.Height;
				}
				this.v_marker = this.v_scroll.Value;
				XplatUI.ScrollWindow(this.item_control.Handle, clientRectangle, 0, num, false);
			}
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x00083428 File Offset: 0x00081628
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.CreateHandle" />.</summary>
		// Token: 0x060022DF RID: 8927 RVA: 0x0008342C File Offset: 0x0008162C
		protected override void CreateHandle()
		{
			base.CreateHandle();
			for (int i = 0; i < this.SelectedItems.Count; i++)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ListView" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060022E0 RID: 8928 RVA: 0x00083468 File Offset: 0x00081668
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.h_scroll.Dispose();
				this.v_scroll.Dispose();
				this.large_image_list = null;
				this.small_image_list = null;
				this.state_image_list = null;
				foreach (object obj in this.columns)
				{
					ColumnHeader columnHeader = (ColumnHeader)obj;
					columnHeader.SetListView(null);
				}
				if (!this.virtual_mode)
				{
					foreach (object obj2 in this.items)
					{
						ListViewItem listViewItem = (ListViewItem)obj2;
						listViewItem.Owner = null;
					}
				}
			}
			base.Dispose(disposing);
		}

		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x060022E1 RID: 8929 RVA: 0x00083580 File Offset: 0x00081780
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return true;
			default:
				return base.IsInputKey(keyData);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.AfterLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> that contains the event data. </param>
		// Token: 0x060022E2 RID: 8930 RVA: 0x000835CC File Offset: 0x000817CC
		protected virtual void OnAfterLabelEdit(LabelEditEventArgs e)
		{
			LabelEditEventHandler labelEditEventHandler = (LabelEditEventHandler)base.Events[ListView.AfterLabelEditEvent];
			if (labelEditEventHandler != null)
			{
				labelEditEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060022E3 RID: 8931 RVA: 0x00083600 File Offset: 0x00081800
		protected override void OnBackgroundImageChanged(EventArgs e)
		{
			this.item_control.BackgroundImage = this.BackgroundImage;
			base.OnBackgroundImageChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.BeforeLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> that contains the event data. </param>
		// Token: 0x060022E4 RID: 8932 RVA: 0x0008361C File Offset: 0x0008181C
		protected virtual void OnBeforeLabelEdit(LabelEditEventArgs e)
		{
			LabelEditEventHandler labelEditEventHandler = (LabelEditEventHandler)base.Events[ListView.BeforeLabelEditEvent];
			if (labelEditEventHandler != null)
			{
				labelEditEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ColumnClickEventArgs" /> that contains the event data. </param>
		// Token: 0x060022E5 RID: 8933 RVA: 0x00083650 File Offset: 0x00081850
		protected internal virtual void OnColumnClick(ColumnClickEventArgs e)
		{
			ColumnClickEventHandler columnClickEventHandler = (ColumnClickEventHandler)base.Events[ListView.ColumnClickEvent];
			if (columnClickEventHandler != null)
			{
				columnClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs" /> that contains the event data. </param>
		// Token: 0x060022E6 RID: 8934 RVA: 0x00083684 File Offset: 0x00081884
		protected internal virtual void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			DrawListViewColumnHeaderEventHandler drawListViewColumnHeaderEventHandler = (DrawListViewColumnHeaderEventHandler)base.Events[ListView.DrawColumnHeaderEvent];
			if (drawListViewColumnHeaderEventHandler != null)
			{
				drawListViewColumnHeaderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060022E7 RID: 8935 RVA: 0x000836B8 File Offset: 0x000818B8
		protected internal virtual void OnDrawItem(DrawListViewItemEventArgs e)
		{
			DrawListViewItemEventHandler drawListViewItemEventHandler = (DrawListViewItemEventHandler)base.Events[ListView.DrawItemEvent];
			if (drawListViewItemEventHandler != null)
			{
				drawListViewItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawSubItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewSubItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060022E8 RID: 8936 RVA: 0x000836EC File Offset: 0x000818EC
		protected internal virtual void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{
			DrawListViewSubItemEventHandler drawListViewSubItemEventHandler = (DrawListViewSubItemEventHandler)base.Events[ListView.DrawSubItemEvent];
			if (drawListViewSubItemEventHandler != null)
			{
				drawListViewSubItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the FontChanged event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022E9 RID: 8937 RVA: 0x00083720 File Offset: 0x00081920
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.Redraw(true);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022EA RID: 8938 RVA: 0x00083730 File Offset: 0x00081930
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.CalculateListView(this.alignment);
			if (!this.virtual_mode)
			{
				this.Sort();
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022EB RID: 8939 RVA: 0x00083764 File Offset: 0x00081964
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemActivate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022EC RID: 8940 RVA: 0x00083770 File Offset: 0x00081970
		protected virtual void OnItemActivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.ItemActivateEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemCheck" /> event.</summary>
		/// <param name="ice">An <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> that contains the event data. </param>
		// Token: 0x060022ED RID: 8941 RVA: 0x000837A4 File Offset: 0x000819A4
		protected internal virtual void OnItemCheck(ItemCheckEventArgs ice)
		{
			ItemCheckEventHandler itemCheckEventHandler = (ItemCheckEventHandler)base.Events[ListView.ItemCheckEvent];
			if (itemCheckEventHandler != null)
			{
				itemCheckEventHandler(this, ice);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemChecked" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemCheckedEventArgs" /> that contains the event data.</param>
		// Token: 0x060022EE RID: 8942 RVA: 0x000837D8 File Offset: 0x000819D8
		protected internal virtual void OnItemChecked(ItemCheckedEventArgs e)
		{
			ItemCheckedEventHandler itemCheckedEventHandler = (ItemCheckedEventHandler)base.Events[ListView.ItemCheckedEvent];
			if (itemCheckedEventHandler != null)
			{
				itemCheckedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemDrag" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> that contains the event data. </param>
		// Token: 0x060022EF RID: 8943 RVA: 0x0008380C File Offset: 0x00081A0C
		protected virtual void OnItemDrag(ItemDragEventArgs e)
		{
			ItemDragEventHandler itemDragEventHandler = (ItemDragEventHandler)base.Events[ListView.ItemDragEvent];
			if (itemDragEventHandler != null)
			{
				itemDragEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemMouseHover" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewItemMouseHoverEventArgs" /> that contains the event data. </param>
		// Token: 0x060022F0 RID: 8944 RVA: 0x00083840 File Offset: 0x00081A40
		protected virtual void OnItemMouseHover(ListViewItemMouseHoverEventArgs e)
		{
			ListViewItemMouseHoverEventHandler listViewItemMouseHoverEventHandler = (ListViewItemMouseHoverEventHandler)base.Events[ListView.ItemMouseHoverEvent];
			if (listViewItemMouseHoverEventHandler != null)
			{
				listViewItemMouseHoverEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemSelectionChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewItemSelectionChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060022F1 RID: 8945 RVA: 0x00083874 File Offset: 0x00081A74
		protected internal virtual void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
		{
			ListViewItemSelectionChangedEventHandler listViewItemSelectionChangedEventHandler = (ListViewItemSelectionChangedEventHandler)base.Events[ListView.ItemSelectionChangedEvent];
			if (listViewItemSelectionChangedEventHandler != null)
			{
				listViewItemSelectionChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022F2 RID: 8946 RVA: 0x000838A8 File Offset: 0x00081AA8
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022F3 RID: 8947 RVA: 0x000838B4 File Offset: 0x00081AB4
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022F4 RID: 8948 RVA: 0x000838C0 File Offset: 0x00081AC0
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022F5 RID: 8949 RVA: 0x000838F4 File Offset: 0x00081AF4
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.CacheVirtualItems" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.CacheVirtualItemsEventArgs" /> that contains the event data. </param>
		// Token: 0x060022F6 RID: 8950 RVA: 0x00083900 File Offset: 0x00081B00
		protected internal virtual void OnCacheVirtualItems(CacheVirtualItemsEventArgs e)
		{
			CacheVirtualItemsEventHandler cacheVirtualItemsEventHandler = (CacheVirtualItemsEventHandler)base.Events[ListView.CacheVirtualItemsEvent];
			if (cacheVirtualItemsEventHandler != null)
			{
				cacheVirtualItemsEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.RetrieveVirtualItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060022F7 RID: 8951 RVA: 0x00083934 File Offset: 0x00081B34
		protected virtual void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
		{
			RetrieveVirtualItemEventHandler retrieveVirtualItemEventHandler = (RetrieveVirtualItemEventHandler)base.Events[ListView.RetrieveVirtualItemEvent];
			if (retrieveVirtualItemEventHandler != null)
			{
				retrieveVirtualItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.RightToLeftLayoutChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060022F8 RID: 8952 RVA: 0x00083968 File Offset: 0x00081B68
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.RightToLeftLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.SearchForVirtualItem" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SearchForVirtualItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060022F9 RID: 8953 RVA: 0x0008399C File Offset: 0x00081B9C
		protected virtual void OnSearchForVirtualItem(SearchForVirtualItemEventArgs e)
		{
			SearchForVirtualItemEventHandler searchForVirtualItemEventHandler = (SearchForVirtualItemEventHandler)base.Events[ListView.SearchForVirtualItemEvent];
			if (searchForVirtualItemEventHandler != null)
			{
				searchForVirtualItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.VirtualItemsSelectionRangeChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060022FA RID: 8954 RVA: 0x000839D0 File Offset: 0x00081BD0
		protected virtual void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
		{
			ListViewVirtualItemsSelectionRangeChangedEventHandler listViewVirtualItemsSelectionRangeChangedEventHandler = (ListViewVirtualItemsSelectionRangeChangedEventHandler)base.Events[ListView.VirtualItemsSelectionRangeChangedEvent];
			if (listViewVirtualItemsSelectionRangeChangedEventHandler != null)
			{
				listViewVirtualItemsSelectionRangeChangedEventHandler(this, e);
			}
		}

		/// <summary>Initializes the properties of the <see cref="T:System.Windows.Forms.ListView" /> control that manage the appearance of the control.</summary>
		// Token: 0x060022FB RID: 8955 RVA: 0x00083A04 File Offset: 0x00081C04
		protected void RealizeProperties()
		{
		}

		/// <summary>Updates the extended styles applied to the list view control.</summary>
		// Token: 0x060022FC RID: 8956 RVA: 0x00083A08 File Offset: 0x00081C08
		protected void UpdateExtendedStyles()
		{
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060022FD RID: 8957 RVA: 0x00083A0C File Offset: 0x00081C0C
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_SETFOCUS)
			{
				if (msg == Msg.WM_KILLFOCUS)
				{
					Control control = Control.FromHandle(m.WParam);
					if (control == this.item_control)
					{
						this.has_focus = false;
						this.refocusing = true;
						return;
					}
				}
			}
			else if (this.refocusing)
			{
				this.has_focus = true;
				this.refocusing = false;
				return;
			}
			base.WndProc(ref m);
		}

		/// <summary>Arranges items in the control when they are displayed as icons based on the value of the <see cref="P:System.Windows.Forms.ListView.Alignment" /> property.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060022FE RID: 8958 RVA: 0x00083A8C File Offset: 0x00081C8C
		public void ArrangeIcons()
		{
			this.ArrangeIcons(this.alignment);
		}

		/// <summary>Arranges items in the control when they are displayed as icons with a specified alignment setting.</summary>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value specified in the <paramref name="value" /> parameter is not a member of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> enumeration. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060022FF RID: 8959 RVA: 0x00083A9C File Offset: 0x00081C9C
		public void ArrangeIcons(ListViewAlignment value)
		{
			if (this.view == View.LargeIcon || this.view == View.SmallIcon)
			{
				this.Redraw(true);
			}
		}

		/// <summary>Resizes the width of the given column as indicated by the resize style.</summary>
		/// <param name="columnIndex">The zero-based index of the column to resize.</param>
		/// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is greater than 0 when <see cref="P:System.Windows.Forms.ListView.Columns" /> is null-or-<paramref name="columnIndex" /> is less than 0 or greater than the number of columns set.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="headerAutoResize" /> is not a member of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> enumeration.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002300 RID: 8960 RVA: 0x00083ABC File Offset: 0x00081CBC
		public void AutoResizeColumn(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (columnIndex < 0 || columnIndex >= this.columns.Count)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			this.columns[columnIndex].AutoResize(headerAutoResize);
		}

		/// <summary>Resizes the width of the columns as indicated by the resize style.</summary>
		/// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Windows.Forms.ListView.AutoResizeColumn(System.Int32,System.Windows.Forms.ColumnHeaderAutoResizeStyle)" /> is called with a value other than <see cref="F:System.Windows.Forms.ColumnHeaderAutoResizeStyle.None" /> when <see cref="P:System.Windows.Forms.ListView.View" /> is not set to <see cref="F:System.Windows.Forms.View.Details" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002301 RID: 8961 RVA: 0x00083AF4 File Offset: 0x00081CF4
		public void AutoResizeColumns(ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			this.BeginUpdate();
			foreach (object obj in this.columns)
			{
				ColumnHeader columnHeader = (ColumnHeader)obj;
				columnHeader.AutoResize(headerAutoResize);
			}
			this.EndUpdate();
		}

		/// <summary>Prevents the control from drawing until the <see cref="M:System.Windows.Forms.ListView.EndUpdate" /> method is called.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002302 RID: 8962 RVA: 0x00083B70 File Offset: 0x00081D70
		public void BeginUpdate()
		{
			this.updating = true;
		}

		/// <summary>Removes all items and columns from the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002303 RID: 8963 RVA: 0x00083B7C File Offset: 0x00081D7C
		public void Clear()
		{
			this.columns.Clear();
			this.items.Clear();
		}

		/// <summary>Resumes drawing of the list view control after drawing is suspended by the <see cref="M:System.Windows.Forms.ListView.BeginUpdate" /> method.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002304 RID: 8964 RVA: 0x00083B94 File Offset: 0x00081D94
		public void EndUpdate()
		{
			this.updating = false;
			this.Redraw(true);
		}

		/// <summary>Ensures that the specified item is visible within the control, scrolling the contents of the control if necessary.</summary>
		/// <param name="index">The zero-based index of the item to scroll into view. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002305 RID: 8965 RVA: 0x00083BA4 File Offset: 0x00081DA4
		public void EnsureVisible(int index)
		{
			if (index < 0 || index >= this.items.Count || !this.scrollable || this.updating)
			{
				return;
			}
			Rectangle clientRectangle = this.item_control.ClientRectangle;
			Rectangle rectangle = ((!this.virtual_mode) ? this.items[index].Bounds : new Rectangle(this.GetItemLocation(index), this.ItemSize));
			if (this.view == View.Details && this.header_style != ColumnHeaderStyle.None)
			{
				clientRectangle.Y += this.header_control.Height;
				clientRectangle.Height -= this.header_control.Height;
			}
			if (clientRectangle.Contains(rectangle))
			{
				return;
			}
			if (this.View != View.Details)
			{
				if (rectangle.Left < 0)
				{
					this.h_scroll.Value += rectangle.Left;
				}
				else if (rectangle.Right > clientRectangle.Right)
				{
					this.h_scroll.Value += rectangle.Right - clientRectangle.Right;
				}
			}
			if (rectangle.Top < clientRectangle.Y)
			{
				this.v_scroll.Value += rectangle.Top - clientRectangle.Y;
			}
			else if (rectangle.Bottom > clientRectangle.Bottom)
			{
				this.v_scroll.Value += rectangle.Bottom - clientRectangle.Bottom;
			}
		}

		/// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</summary>
		/// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
		/// <param name="text">The text to search for.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002306 RID: 8966 RVA: 0x00083D50 File Offset: 0x00081F50
		public ListViewItem FindItemWithText(string text)
		{
			if (this.items.Count == 0)
			{
				return null;
			}
			return this.FindItemWithText(text, true, 0, true);
		}

		/// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> or <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, if indicated, that begins with the specified text value. The search starts at the specified index.</summary>
		/// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
		/// <param name="text">The text to search for.</param>
		/// <param name="includeSubItemsInSearch">true to include subitems in the search; otherwise, false. </param>
		/// <param name="startIndex">The index of the item at which to start the search.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less 0 or more than the number items in the <see cref="T:System.Windows.Forms.ListView" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002307 RID: 8967 RVA: 0x00083D7C File Offset: 0x00081F7C
		public ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex)
		{
			return this.FindItemWithText(text, includeSubItemsInSearch, startIndex, true, false);
		}

		/// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> or <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, if indicated, that begins with the specified text value. The search starts at the specified index.</summary>
		/// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
		/// <param name="text">The text to search for.</param>
		/// <param name="includeSubItemsInSearch">true to include subitems in the search; otherwise, false. </param>
		/// <param name="startIndex">The index of the item at which to start the search.</param>
		/// <param name="isPrefixSearch">true to match the search text to the prefix of an item; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less 0 or more than the number of items in the <see cref="T:System.Windows.Forms.ListView" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002308 RID: 8968 RVA: 0x00083D8C File Offset: 0x00081F8C
		public ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex, bool isPrefixSearch)
		{
			return this.FindItemWithText(text, includeSubItemsInSearch, startIndex, isPrefixSearch, false);
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x00083D9C File Offset: 0x00081F9C
		internal ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex, bool isPrefixSearch, bool roundtrip)
		{
			if (startIndex < 0 || startIndex >= this.items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (!this.virtual_mode)
			{
				int i = startIndex;
				ListViewItem listViewItem;
				for (;;)
				{
					listViewItem = this.items[i];
					if (isPrefixSearch)
					{
						if (CultureInfo.CurrentCulture.CompareInfo.IsPrefix(listViewItem.Text, text, 1))
						{
							break;
						}
					}
					else if (string.Compare(listViewItem.Text, text, true) == 0)
					{
						return listViewItem;
					}
					if (i + 1 >= this.items.Count)
					{
						if (!roundtrip)
						{
							goto Block_10;
						}
						i = 0;
					}
					else
					{
						i++;
					}
					if (i == startIndex)
					{
						goto Block_11;
					}
				}
				return listViewItem;
				Block_10:
				Block_11:
				if (includeSubItemsInSearch)
				{
					for (i = startIndex; i < this.items.Count; i++)
					{
						ListViewItem listViewItem2 = this.items[i];
						foreach (object obj in listViewItem2.SubItems)
						{
							ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)obj;
							if (isPrefixSearch)
							{
								if (CultureInfo.CurrentCulture.CompareInfo.IsPrefix(listViewSubItem.Text, text, 1))
								{
									return listViewItem2;
								}
							}
							else if (string.Compare(listViewSubItem.Text, text, true) == 0)
							{
								return listViewItem2;
							}
						}
					}
				}
				return null;
			}
			SearchForVirtualItemEventArgs searchForVirtualItemEventArgs = new SearchForVirtualItemEventArgs(true, isPrefixSearch, includeSubItemsInSearch, text, Point.Empty, SearchDirectionHint.Down, startIndex);
			this.OnSearchForVirtualItem(searchForVirtualItemEventArgs);
			int index = searchForVirtualItemEventArgs.Index;
			if (index >= 0 && index < this.virtual_list_size)
			{
				return this.items[index];
			}
			return null;
		}

		/// <summary>Finds the next item from the given x- and y-coordinates, searching in the specified direction. </summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given coordinates, searching in the specified direction.</returns>
		/// <param name="searchDirection">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <param name="x">The x-coordinate for the point at which to begin searching.</param>
		/// <param name="y">The y-coordinate for the point at which to begin searching.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.View" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />. </exception>
		// Token: 0x0600230A RID: 8970 RVA: 0x00083F9C File Offset: 0x0008219C
		public ListViewItem FindNearestItem(SearchDirectionHint searchDirection, int x, int y)
		{
			return this.FindNearestItem(searchDirection, new Point(x, y));
		}

		/// <summary>Finds the next item from the given point, searching in the specified direction</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given point, searching in the specified direction.</returns>
		/// <param name="dir">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <param name="point">The point at which to begin searching.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.View" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />. </exception>
		// Token: 0x0600230B RID: 8971 RVA: 0x00083FAC File Offset: 0x000821AC
		public ListViewItem FindNearestItem(SearchDirectionHint dir, Point point)
		{
			if (dir < SearchDirectionHint.Left || dir > SearchDirectionHint.Down)
			{
				throw new ArgumentOutOfRangeException("searchDirection");
			}
			if (this.view != View.LargeIcon && this.view != View.SmallIcon)
			{
				throw new InvalidOperationException();
			}
			if (!this.virtual_mode)
			{
				ListViewItem listViewItem = null;
				int num = int.MaxValue;
				switch (dir)
				{
				case SearchDirectionHint.Left:
					point.X -= this.item_size.Width;
					break;
				case SearchDirectionHint.Up:
					point.Y -= this.item_size.Height;
					break;
				case SearchDirectionHint.Right:
					point.X += this.item_size.Width;
					break;
				case SearchDirectionHint.Down:
					point.Y += this.item_size.Height;
					break;
				}
				int i = 0;
				while (i < this.items.Count)
				{
					Point itemLocation = this.GetItemLocation(i);
					if (dir == SearchDirectionHint.Up)
					{
						if (point.Y >= itemLocation.Y)
						{
							goto IL_01C7;
						}
					}
					else if (dir == SearchDirectionHint.Down)
					{
						if (point.Y <= itemLocation.Y)
						{
							goto IL_01C7;
						}
					}
					else if (dir == SearchDirectionHint.Left)
					{
						if (point.X >= itemLocation.X)
						{
							goto IL_01C7;
						}
					}
					else if (dir != SearchDirectionHint.Right || point.X <= itemLocation.X)
					{
						goto IL_01C7;
					}
					IL_020F:
					i++;
					continue;
					IL_01C7:
					int num2 = point.X - itemLocation.X;
					int num3 = point.Y - itemLocation.Y;
					int num4 = num2 * num2 + num3 * num3;
					if (num4 < num)
					{
						listViewItem = this.items[i];
						num = num4;
						goto IL_020F;
					}
					goto IL_020F;
				}
				return listViewItem;
			}
			SearchForVirtualItemEventArgs searchForVirtualItemEventArgs = new SearchForVirtualItemEventArgs(false, false, false, string.Empty, point, dir, 0);
			this.OnSearchForVirtualItem(searchForVirtualItemEventArgs);
			int index = searchForVirtualItemEventArgs.Index;
			if (index >= 0 && index < this.virtual_list_size)
			{
				return this.items[index];
			}
			return null;
		}

		/// <summary>Retrieves the item at the specified location.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item at the specified position. If there is no item at the specified location, the method returns null.</returns>
		/// <param name="x">The x-coordinate of the location to search for an item (expressed in client coordinates). </param>
		/// <param name="y">The y-coordinate of the location to search for an item (expressed in client coordinates). </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600230C RID: 8972 RVA: 0x000841E4 File Offset: 0x000823E4
		public ListViewItem GetItemAt(int x, int y)
		{
			Size itemSize = this.ItemSize;
			for (int i = 0; i < this.items.Count; i++)
			{
				Point itemLocation = this.GetItemLocation(i);
				Rectangle rectangle;
				rectangle..ctor(itemLocation, itemSize);
				if (rectangle.Contains(x, y))
				{
					return this.items[i];
				}
			}
			return null;
		}

		/// <summary>Retrieves the bounding rectangle for a specific item within the list view control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle of the specified <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <param name="index">The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> whose bounding rectangle you want to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600230D RID: 8973 RVA: 0x00084244 File Offset: 0x00082444
		public Rectangle GetItemRect(int index)
		{
			return this.GetItemRect(index, ItemBoundsPortion.Entire);
		}

		/// <summary>Retrieves the specified portion of the bounding rectangle for a specific item within the list view control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified portion of the specified <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <param name="index">The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> whose bounding rectangle you want to return. </param>
		/// <param name="portion">One of the <see cref="T:System.Windows.Forms.ItemBoundsPortion" /> values that represents a portion of the <see cref="T:System.Windows.Forms.ListViewItem" /> for which to retrieve the bounding rectangle. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600230E RID: 8974 RVA: 0x00084250 File Offset: 0x00082450
		public Rectangle GetItemRect(int index, ItemBoundsPortion portion)
		{
			if (index < 0 || index >= this.items.Count)
			{
				throw new IndexOutOfRangeException("index");
			}
			return this.items[index].GetBounds(portion);
		}

		/// <summary>Provides item information, given a point.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewHitTestInfo" />.</returns>
		/// <param name="point">The <see cref="T:System.Drawing.Point" /> at which to retrieve the item information. The coordinates are relative to the upper-left corner of the control.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The point contains coordinates that are less than 0.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600230F RID: 8975 RVA: 0x00084288 File Offset: 0x00082488
		public ListViewHitTestInfo HitTest(Point point)
		{
			return this.HitTest(point.X, point.Y);
		}

		/// <summary>Provides item information, given x- and y-coordinates.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewHitTestInfo" />.</returns>
		/// <param name="x">The x-coordinate at which to retrieve the item information. The coordinate is relative to the upper-left corner of the control.</param>
		/// <param name="y">The y-coordinate at which to retrieve the item information. The coordinate is relative to the upper-left corner of the control.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The x- or y-coordinate is less than 0.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002310 RID: 8976 RVA: 0x000842A0 File Offset: 0x000824A0
		public ListViewHitTestInfo HitTest(int x, int y)
		{
			if (x < 0)
			{
				throw new ArgumentOutOfRangeException("x");
			}
			if (y < 0)
			{
				throw new ArgumentOutOfRangeException("y");
			}
			ListViewItem itemAt = this.GetItemAt(x, y);
			if (itemAt == null)
			{
				return new ListViewHitTestInfo(null, null, ListViewHitTestLocations.None);
			}
			ListViewHitTestLocations listViewHitTestLocations = (ListViewHitTestLocations)0;
			if (itemAt.GetBounds(ItemBoundsPortion.Label).Contains(x, y))
			{
				listViewHitTestLocations |= ListViewHitTestLocations.Label;
			}
			else if (itemAt.GetBounds(ItemBoundsPortion.Icon).Contains(x, y))
			{
				listViewHitTestLocations |= ListViewHitTestLocations.Image;
			}
			else if (itemAt.CheckRectReal.Contains(x, y))
			{
				listViewHitTestLocations |= ListViewHitTestLocations.StateImage;
			}
			ListViewItem.ListViewSubItem listViewSubItem = null;
			if (this.view == View.Details)
			{
				foreach (object obj in itemAt.SubItems)
				{
					ListViewItem.ListViewSubItem listViewSubItem2 = (ListViewItem.ListViewSubItem)obj;
					if (listViewSubItem2.Bounds.Contains(x, y))
					{
						listViewSubItem = listViewSubItem2;
						break;
					}
				}
			}
			return new ListViewHitTestInfo(itemAt, listViewSubItem, listViewHitTestLocations);
		}

		/// <summary>Forces a range of <see cref="T:System.Windows.Forms.ListViewItem" /> objects to be redrawn.</summary>
		/// <param name="startIndex">The index for the first item in the range to be redrawn.</param>
		/// <param name="endIndex">The index for the last item of the range to be redrawn.</param>
		/// <param name="invalidateOnly">true to invalidate the range of items; false to invalidate and repaint the items.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="endIndex" /> is less than 0, greater than or equal to the number of items in the <see cref="T:System.Windows.Forms.ListView" /> or, if in virtual mode, greater than the value of <see cref="P:System.Windows.Forms.ListView.VirtualListSize" />.-or-The given <paramref name="startIndex" /> is greater than the <paramref name="endIndex." /></exception>
		// Token: 0x06002311 RID: 8977 RVA: 0x000843E0 File Offset: 0x000825E0
		[EditorBrowsable(2)]
		public void RedrawItems(int startIndex, int endIndex, bool invalidateOnly)
		{
			if (startIndex < 0 || startIndex >= this.items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (endIndex < 0 || endIndex >= this.items.Count)
			{
				throw new ArgumentOutOfRangeException("endIndex");
			}
			if (startIndex > endIndex)
			{
				throw new ArgumentException("startIndex");
			}
			if (this.updating)
			{
				return;
			}
			for (int i = startIndex; i <= endIndex; i++)
			{
				this.items[i].Invalidate();
			}
			if (!invalidateOnly)
			{
				base.Update();
			}
		}

		/// <summary>Sorts the items of the list view.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002312 RID: 8978 RVA: 0x00084480 File Offset: 0x00082680
		public void Sort()
		{
			if (this.virtual_mode)
			{
				throw new InvalidOperationException();
			}
			this.Sort(true);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x0008449C File Offset: 0x0008269C
		private void Sort(bool redraw)
		{
			if (!base.IsHandleCreated || this.item_sorter == null)
			{
				return;
			}
			this.items.Sort(this.item_sorter);
			if (redraw)
			{
				this.Redraw(true);
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>A string that states the control type, the count of items in the <see cref="T:System.Windows.Forms.ListView" /> control, and the type of the first item in the <see cref="T:System.Windows.Forms.ListView" />, if the count is not 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002314 RID: 8980 RVA: 0x000844D4 File Offset: 0x000826D4
		public override string ToString()
		{
			int count = this.Items.Count;
			if (count == 0)
			{
				return string.Format("System.Windows.Forms.ListView, Items.Count: 0", new object[0]);
			}
			return string.Format("System.Windows.Forms.ListView, Items.Count: {0}, Items[0]: {1}", count, this.Items[0].ToString());
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002315 RID: 8981 RVA: 0x00084528 File Offset: 0x00082728
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002316 RID: 8982 RVA: 0x00084534 File Offset: 0x00082734
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnReordered" /> event. </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.ColumnReorderedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002317 RID: 8983 RVA: 0x00084540 File Offset: 0x00082740
		protected virtual void OnColumnReordered(ColumnReorderedEventArgs e)
		{
			ColumnReorderedEventHandler columnReorderedEventHandler = (ColumnReorderedEventHandler)base.Events[ListView.ColumnReorderedEvent];
			if (columnReorderedEventHandler != null)
			{
				columnReorderedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ColumnWidthChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002318 RID: 8984 RVA: 0x00084574 File Offset: 0x00082774
		protected virtual void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
		{
			ColumnWidthChangedEventHandler columnWidthChangedEventHandler = (ColumnWidthChangedEventHandler)base.Events[ListView.ColumnWidthChangedEvent];
			if (columnWidthChangedEventHandler != null)
			{
				columnWidthChangedEventHandler(this, e);
			}
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000845A8 File Offset: 0x000827A8
		private void RaiseColumnWidthChanged(int resize_column)
		{
			ColumnWidthChangedEventArgs columnWidthChangedEventArgs = new ColumnWidthChangedEventArgs(resize_column);
			this.OnColumnWidthChanged(columnWidthChangedEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanging" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ColumnWidthChangingEventArgs" />  that contains the event data. </param>
		// Token: 0x0600231A RID: 8986 RVA: 0x000845C4 File Offset: 0x000827C4
		protected virtual void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
		{
			ColumnWidthChangingEventHandler columnWidthChangingEventHandler = (ColumnWidthChangingEventHandler)base.Events[ListView.ColumnWidthChangingEvent];
			if (columnWidthChangingEventHandler != null)
			{
				columnWidthChangingEventHandler(this, e);
			}
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000845F8 File Offset: 0x000827F8
		private bool CanProceedWithResize(ColumnHeader col, int width)
		{
			ColumnWidthChangingEventHandler columnWidthChangingEventHandler = (ColumnWidthChangingEventHandler)base.Events[ListView.ColumnWidthChangingEvent];
			if (columnWidthChangingEventHandler == null)
			{
				return true;
			}
			ColumnWidthChangingEventArgs columnWidthChangingEventArgs = new ColumnWidthChangingEventArgs(col.Index, width);
			columnWidthChangingEventHandler(this, columnWidthChangingEventArgs);
			return !columnWidthChangingEventArgs.Cancel;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00084644 File Offset: 0x00082844
		internal void RaiseColumnWidthChanged(ColumnHeader column)
		{
			int num = this.Columns.IndexOf(column);
			this.RaiseColumnWidthChanged(num);
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x00084668 File Offset: 0x00082868
		internal Rectangle UIAHeaderControl
		{
			get
			{
				return this.header_control.Bounds;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x00084678 File Offset: 0x00082878
		internal int UIAColumns
		{
			get
			{
				return this.cols;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x00084680 File Offset: 0x00082880
		internal int UIARows
		{
			get
			{
				return this.rows;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x00084688 File Offset: 0x00082888
		internal ListViewGroup UIADefaultListViewGroup
		{
			get
			{
				return this.groups.DefaultGroup;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x00084698 File Offset: 0x00082898
		internal ScrollBar UIAHScrollBar
		{
			get
			{
				return this.h_scroll;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x000846A0 File Offset: 0x000828A0
		internal ScrollBar UIAVScrollBar
		{
			get
			{
				return this.v_scroll;
			}
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000846A8 File Offset: 0x000828A8
		internal Rectangle UIAGetHeaderBounds(ListViewGroup group)
		{
			return group.HeaderBounds;
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x000846B0 File Offset: 0x000828B0
		internal int UIAItemsLocationLength
		{
			get
			{
				return this.items_location.Length;
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000846BC File Offset: 0x000828BC
		private void OnUIACheckBoxesChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIACheckBoxesChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000846F4 File Offset: 0x000828F4
		private void OnUIAShowGroupsChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIAShowGroupsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0008472C File Offset: 0x0008292C
		private void OnUIAMultiSelectChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIAMultiSelectChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00084764 File Offset: 0x00082964
		private void OnUIALabelEditChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIALabelEditChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0008479C File Offset: 0x0008299C
		private void OnUIAViewChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIAViewChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000847D4 File Offset: 0x000829D4
		internal void OnUIAFocusedItemChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.UIAFocusedItemChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x040011EE RID: 4590
		private const int text_padding = 15;

		// Token: 0x040011EF RID: 4591
		private const int max_wrap_padding = 30;

		// Token: 0x040011F0 RID: 4592
		private ItemActivation activation;

		// Token: 0x040011F1 RID: 4593
		private ListViewAlignment alignment = ListViewAlignment.Top;

		// Token: 0x040011F2 RID: 4594
		private bool allow_column_reorder;

		// Token: 0x040011F3 RID: 4595
		private bool auto_arrange = true;

		// Token: 0x040011F4 RID: 4596
		private bool check_boxes;

		// Token: 0x040011F5 RID: 4597
		private readonly ListView.CheckedIndexCollection checked_indices;

		// Token: 0x040011F6 RID: 4598
		private readonly ListView.CheckedListViewItemCollection checked_items;

		// Token: 0x040011F7 RID: 4599
		private readonly ListView.ColumnHeaderCollection columns;

		// Token: 0x040011F8 RID: 4600
		internal int focused_item_index = -1;

		// Token: 0x040011F9 RID: 4601
		private bool full_row_select;

		// Token: 0x040011FA RID: 4602
		private bool grid_lines;

		// Token: 0x040011FB RID: 4603
		private ColumnHeaderStyle header_style = ColumnHeaderStyle.Clickable;

		// Token: 0x040011FC RID: 4604
		private bool hide_selection = true;

		// Token: 0x040011FD RID: 4605
		private bool hover_selection;

		// Token: 0x040011FE RID: 4606
		private IComparer item_sorter;

		// Token: 0x040011FF RID: 4607
		private readonly ListView.ListViewItemCollection items;

		// Token: 0x04001200 RID: 4608
		private readonly ListViewGroupCollection groups;

		// Token: 0x04001201 RID: 4609
		private bool owner_draw;

		// Token: 0x04001202 RID: 4610
		private bool show_groups = true;

		// Token: 0x04001203 RID: 4611
		private bool label_edit;

		// Token: 0x04001204 RID: 4612
		private bool label_wrap = true;

		// Token: 0x04001205 RID: 4613
		private bool multiselect = true;

		// Token: 0x04001206 RID: 4614
		private bool scrollable = true;

		// Token: 0x04001207 RID: 4615
		private bool hover_pending;

		// Token: 0x04001208 RID: 4616
		private readonly ListView.SelectedIndexCollection selected_indices;

		// Token: 0x04001209 RID: 4617
		private readonly ListView.SelectedListViewItemCollection selected_items;

		// Token: 0x0400120A RID: 4618
		private SortOrder sort_order;

		// Token: 0x0400120B RID: 4619
		private ImageList state_image_list;

		// Token: 0x0400120C RID: 4620
		internal bool updating;

		// Token: 0x0400120D RID: 4621
		private View view;

		// Token: 0x0400120E RID: 4622
		private int layout_wd;

		// Token: 0x0400120F RID: 4623
		private int layout_ht;

		// Token: 0x04001210 RID: 4624
		internal ListView.HeaderControl header_control;

		// Token: 0x04001211 RID: 4625
		internal ListView.ItemControl item_control;

		// Token: 0x04001212 RID: 4626
		internal ScrollBar h_scroll;

		// Token: 0x04001213 RID: 4627
		internal ScrollBar v_scroll;

		// Token: 0x04001214 RID: 4628
		internal int h_marker;

		// Token: 0x04001215 RID: 4629
		internal int v_marker;

		// Token: 0x04001216 RID: 4630
		private int keysearch_tickcnt;

		// Token: 0x04001217 RID: 4631
		private string keysearch_text;

		// Token: 0x04001218 RID: 4632
		private static readonly int keysearch_keydelay = 1000;

		// Token: 0x04001219 RID: 4633
		private int[] reordered_column_indices;

		// Token: 0x0400121A RID: 4634
		private int[] reordered_items_indices;

		// Token: 0x0400121B RID: 4635
		private Point[] items_location;

		// Token: 0x0400121C RID: 4636
		private ListView.ItemMatrixLocation[] items_matrix_location;

		// Token: 0x0400121D RID: 4637
		private Size item_size;

		// Token: 0x0400121E RID: 4638
		private int custom_column_width;

		// Token: 0x0400121F RID: 4639
		private int hot_item_index = -1;

		// Token: 0x04001220 RID: 4640
		private bool hot_tracking;

		// Token: 0x04001221 RID: 4641
		private ListViewInsertionMark insertion_mark;

		// Token: 0x04001222 RID: 4642
		private bool show_item_tooltips;

		// Token: 0x04001223 RID: 4643
		private ToolTip item_tooltip;

		// Token: 0x04001224 RID: 4644
		private Size tile_size;

		// Token: 0x04001225 RID: 4645
		private bool virtual_mode;

		// Token: 0x04001226 RID: 4646
		private int virtual_list_size;

		// Token: 0x04001227 RID: 4647
		private bool right_to_left_layout;

		// Token: 0x04001228 RID: 4648
		internal ImageList large_image_list;

		// Token: 0x04001229 RID: 4649
		internal ImageList small_image_list;

		// Token: 0x0400122A RID: 4650
		internal Size text_size = Size.Empty;

		// Token: 0x0400123D RID: 4669
		private int x_spacing;

		// Token: 0x0400123E RID: 4670
		private int y_spacing;

		// Token: 0x0400123F RID: 4671
		private int rows;

		// Token: 0x04001240 RID: 4672
		private int cols;

		// Token: 0x04001241 RID: 4673
		private int[,] item_index_matrix;

		// Token: 0x04001242 RID: 4674
		private ListViewItem selection_start;

		// Token: 0x04001243 RID: 4675
		private bool refocusing;

		// Token: 0x0200021D RID: 541
		internal class ItemControl : Control
		{
			// Token: 0x0600232B RID: 9003 RVA: 0x0008480C File Offset: 0x00082A0C
			public ItemControl(ListView owner)
			{
				this.owner = owner;
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.DoubleClick += new EventHandler(this.ItemsDoubleClick);
				base.MouseDown += this.ItemsMouseDown;
				base.MouseMove += this.ItemsMouseMove;
				base.MouseHover += new EventHandler(this.ItemsMouseHover);
				base.MouseUp += this.ItemsMouseUp;
			}

			// Token: 0x0600232C RID: 9004 RVA: 0x000848A0 File Offset: 0x00082AA0
			private void ItemsDoubleClick(object sender, EventArgs e)
			{
				if (this.owner.activation == ItemActivation.Standard)
				{
					this.owner.OnItemActivate(EventArgs.Empty);
				}
			}

			// Token: 0x170008A6 RID: 2214
			// (get) Token: 0x0600232D RID: 9005 RVA: 0x000848D0 File Offset: 0x00082AD0
			// (set) Token: 0x0600232E RID: 9006 RVA: 0x000848D8 File Offset: 0x00082AD8
			internal Rectangle BoxSelectRectangle
			{
				get
				{
					return this.box_select_rect;
				}
				set
				{
					if (this.box_select_rect == value)
					{
						return;
					}
					this.InvalidateBoxSelectRect();
					this.box_select_rect = value;
					this.InvalidateBoxSelectRect();
				}
			}

			// Token: 0x0600232F RID: 9007 RVA: 0x00084900 File Offset: 0x00082B00
			private void InvalidateBoxSelectRect()
			{
				if (this.BoxSelectRectangle.Size.IsEmpty)
				{
					return;
				}
				Rectangle boxSelectRectangle = this.BoxSelectRectangle;
				boxSelectRectangle.X--;
				boxSelectRectangle.Y--;
				boxSelectRectangle.Width += 2;
				boxSelectRectangle.Height = 2;
				base.Invalidate(boxSelectRectangle);
				boxSelectRectangle.Y = this.BoxSelectRectangle.Bottom - 1;
				base.Invalidate(boxSelectRectangle);
				boxSelectRectangle.Y = this.BoxSelectRectangle.Y - 1;
				boxSelectRectangle.Width = 2;
				boxSelectRectangle.Height = this.BoxSelectRectangle.Height + 2;
				base.Invalidate(boxSelectRectangle);
				boxSelectRectangle.X = this.BoxSelectRectangle.Right - 1;
				base.Invalidate(boxSelectRectangle);
			}

			// Token: 0x06002330 RID: 9008 RVA: 0x000849E8 File Offset: 0x00082BE8
			private Rectangle CalculateBoxSelectRectangle(Point pt)
			{
				int num = Math.Min(this.box_select_start.X, pt.X);
				int num2 = Math.Max(this.box_select_start.X, pt.X);
				int num3 = Math.Min(this.box_select_start.Y, pt.Y);
				int num4 = Math.Max(this.box_select_start.Y, pt.Y);
				return Rectangle.FromLTRB(num, num3, num2, num4);
			}

			// Token: 0x06002331 RID: 9009 RVA: 0x00084A60 File Offset: 0x00082C60
			private bool BoxIntersectsItem(int index)
			{
				Rectangle rectangle;
				rectangle..ctor(this.owner.GetItemLocation(index), this.owner.ItemSize);
				if (this.owner.View != View.Details)
				{
					rectangle.X += rectangle.Width / 4;
					rectangle.Y += rectangle.Height / 4;
					rectangle.Width /= 2;
					rectangle.Height /= 2;
				}
				return this.BoxSelectRectangle.IntersectsWith(rectangle);
			}

			// Token: 0x06002332 RID: 9010 RVA: 0x00084AF8 File Offset: 0x00082CF8
			private bool BoxIntersectsText(int index)
			{
				Rectangle textBounds = this.owner.GetItemAtDisplayIndex(index).TextBounds;
				return this.BoxSelectRectangle.IntersectsWith(textBounds);
			}

			// Token: 0x170008A7 RID: 2215
			// (get) Token: 0x06002333 RID: 9011 RVA: 0x00084B28 File Offset: 0x00082D28
			private ArrayList BoxSelectedItems
			{
				get
				{
					ArrayList arrayList = new ArrayList();
					for (int i = 0; i < this.owner.Items.Count; i++)
					{
						bool flag;
						if (this.owner.View == View.Details && !this.owner.FullRowSelect && !this.owner.VirtualMode)
						{
							flag = this.BoxIntersectsText(i);
						}
						else
						{
							flag = this.BoxIntersectsItem(i);
						}
						if (flag)
						{
							arrayList.Add(this.owner.GetItemAtDisplayIndex(i));
						}
					}
					return arrayList;
				}
			}

			// Token: 0x06002334 RID: 9012 RVA: 0x00084BBC File Offset: 0x00082DBC
			private bool PerformBoxSelection(Point pt)
			{
				if (this.box_select_mode == ListView.ItemControl.BoxSelect.None)
				{
					return false;
				}
				this.BoxSelectRectangle = this.CalculateBoxSelectRectangle(pt);
				ArrayList boxSelectedItems = this.BoxSelectedItems;
				ArrayList arrayList;
				switch (this.box_select_mode)
				{
				case ListView.ItemControl.BoxSelect.Normal:
					arrayList = boxSelectedItems;
					break;
				case ListView.ItemControl.BoxSelect.Shift:
					arrayList = boxSelectedItems;
					foreach (object obj in boxSelectedItems)
					{
						ListViewItem listViewItem = (ListViewItem)obj;
						this.prev_selection.Remove(listViewItem.Index);
					}
					foreach (object obj2 in this.prev_selection)
					{
						int num = (int)obj2;
						arrayList.Add(this.owner.Items[num]);
					}
					break;
				case ListView.ItemControl.BoxSelect.Control:
					arrayList = new ArrayList();
					foreach (object obj3 in this.prev_selection)
					{
						int num2 = (int)obj3;
						if (!boxSelectedItems.Contains(this.owner.Items[num2]))
						{
							arrayList.Add(this.owner.Items[num2]);
						}
					}
					foreach (object obj4 in boxSelectedItems)
					{
						ListViewItem listViewItem2 = (ListViewItem)obj4;
						if (!this.prev_selection.Contains(listViewItem2.Index))
						{
							arrayList.Add(listViewItem2);
						}
					}
					break;
				default:
					throw new Exception("Unexpected Selection mode: " + this.box_select_mode);
				}
				base.SuspendLayout();
				this.owner.SelectItems(arrayList);
				base.ResumeLayout();
				return true;
			}

			// Token: 0x06002335 RID: 9013 RVA: 0x00084E54 File Offset: 0x00083054
			private void ItemsMouseDown(object sender, MouseEventArgs me)
			{
				this.owner.OnMouseDown(this.owner.TranslateMouseEventArgs(me));
				if (this.owner.items.Count == 0)
				{
					return;
				}
				bool flag = false;
				Size itemSize = this.owner.ItemSize;
				Point point;
				point..ctor(me.X, me.Y);
				int i = 0;
				while (i < this.owner.items.Count)
				{
					Rectangle rectangle;
					rectangle..ctor(this.owner.GetItemLocation(i), itemSize);
					if (!rectangle.Contains(point))
					{
						i++;
					}
					else
					{
						ListViewItem itemAtDisplayIndex = this.owner.GetItemAtDisplayIndex(i);
						if (!itemAtDisplayIndex.CheckRectReal.Contains(point))
						{
							if (this.owner.View == View.Details)
							{
								bool flag2 = itemAtDisplayIndex.TextBounds.Contains(point);
								if (this.owner.FullRowSelect)
								{
									this.clicked_item = itemAtDisplayIndex;
									bool flag3 = me.X > this.owner.Columns[0].X && me.X < this.owner.Columns[0].X + this.owner.Columns[0].Width;
									if (!flag2 && flag3 && this.owner.MultiSelect)
									{
										flag = true;
									}
								}
								else if (flag2)
								{
									this.clicked_item = itemAtDisplayIndex;
								}
								else
								{
									this.owner.SetFocusedItem(i);
								}
							}
							else
							{
								this.clicked_item = itemAtDisplayIndex;
							}
							break;
						}
						if (this.owner.StateImageList != null && this.owner.StateImageList.Images.Count < 2)
						{
							return;
						}
						if (me.Clicks == 2)
						{
							itemAtDisplayIndex.Checked = !itemAtDisplayIndex.Checked;
						}
						itemAtDisplayIndex.Checked = !itemAtDisplayIndex.Checked;
						this.checking = true;
						return;
					}
				}
				if (this.clicked_item != null)
				{
					bool flag4 = !this.clicked_item.Selected;
					if (me.Button == MouseButtons.Left || (XplatUI.State.ModifierKeys == Keys.None && flag4))
					{
						this.owner.SetFocusedItem(this.clicked_item.DisplayIndex);
					}
					if (this.owner.MultiSelect)
					{
						bool flag5 = !this.owner.LabelEdit || flag4;
						if (me.Button == MouseButtons.Left || (XplatUI.State.ModifierKeys == Keys.None && flag4))
						{
							this.owner.UpdateMultiSelection(this.clicked_item.DisplayIndex, flag5);
						}
					}
					else
					{
						this.clicked_item.Selected = true;
					}
					if (this.owner.VirtualMode && flag4)
					{
						ListViewVirtualItemsSelectionRangeChangedEventArgs listViewVirtualItemsSelectionRangeChangedEventArgs = new ListViewVirtualItemsSelectionRangeChangedEventArgs(0, this.owner.items.Count - 1, false);
						this.owner.OnVirtualItemsSelectionRangeChanged(listViewVirtualItemsSelectionRangeChangedEventArgs);
					}
					this.clicks = me.Clicks;
					if (me.Clicks > 1)
					{
						if (this.owner.CheckBoxes)
						{
							this.clicked_item.Checked = !this.clicked_item.Checked;
						}
					}
					else if (me.Clicks == 1 && this.owner.LabelEdit && !flag4)
					{
						this.BeginEdit(this.clicked_item);
					}
				}
				else if (this.owner.MultiSelect)
				{
					flag = true;
				}
				else if (this.owner.SelectedItems.Count > 0)
				{
					this.owner.SelectedItems.Clear();
				}
				if (flag)
				{
					Keys modifierKeys = XplatUI.State.ModifierKeys;
					if ((modifierKeys & Keys.Shift) != Keys.None)
					{
						this.box_select_mode = ListView.ItemControl.BoxSelect.Shift;
					}
					else if ((modifierKeys & Keys.Control) != Keys.None)
					{
						this.box_select_mode = ListView.ItemControl.BoxSelect.Control;
					}
					else
					{
						this.box_select_mode = ListView.ItemControl.BoxSelect.Normal;
					}
					this.box_select_start = point;
					this.prev_selection = this.owner.SelectedIndices.List.Clone() as IList;
				}
			}

			// Token: 0x06002336 RID: 9014 RVA: 0x00085290 File Offset: 0x00083490
			private void ItemsMouseMove(object sender, MouseEventArgs me)
			{
				bool flag = this.PerformBoxSelection(new Point(me.X, me.Y));
				this.owner.OnMouseMove(this.owner.TranslateMouseEventArgs(me));
				if (flag)
				{
					return;
				}
				if (me.Button != MouseButtons.Left && me.Button != MouseButtons.Right && !this.hover_processed && this.owner.Activation != ItemActivation.OneClick && !this.owner.ShowItemToolTips)
				{
					return;
				}
				Point point = base.PointToClient(Control.MousePosition);
				ListViewItem itemAt = this.owner.GetItemAt(point.X, point.Y);
				if (this.hover_processed && itemAt != null && itemAt != this.prev_hovered_item)
				{
					this.hover_processed = false;
					XplatUI.ResetMouseHover(this.Handle);
				}
				if (this.owner.Activation == ItemActivation.OneClick)
				{
					if (itemAt == null && this.owner.HotItemIndex != -1)
					{
						if (this.owner.HotTracking)
						{
							base.Invalidate(this.owner.Items[this.owner.HotItemIndex].Bounds);
						}
						this.Cursor = Cursors.Default;
						this.owner.HotItemIndex = -1;
					}
					else if (itemAt != null && this.owner.HotItemIndex == -1)
					{
						if (this.owner.HotTracking)
						{
							base.Invalidate(itemAt.Bounds);
						}
						this.Cursor = Cursors.Hand;
						this.owner.HotItemIndex = itemAt.Index;
					}
				}
				if (me.Button == MouseButtons.Left || me.Button == MouseButtons.Right)
				{
					if (this.drag_begin.X == -1 && this.drag_begin.Y == -1)
					{
						if (itemAt != null)
						{
							this.drag_begin = new Point(me.X, me.Y);
							this.dragged_item_index = itemAt.Index;
						}
					}
					else
					{
						Rectangle rectangle;
						rectangle..ctor(this.drag_begin, SystemInformation.DragSize);
						if (!rectangle.Contains(me.X, me.Y))
						{
							ListViewItem listViewItem = this.owner.items[this.dragged_item_index];
							this.owner.OnItemDrag(new ItemDragEventArgs(me.Button, listViewItem));
							this.drag_begin = new Point(-1, -1);
							this.dragged_item_index = -1;
						}
					}
				}
				if (this.owner.ShowItemToolTips)
				{
					if (itemAt == null)
					{
						this.owner.item_tooltip.Active = false;
						this.prev_tooltip_item = null;
					}
					else if (itemAt != this.prev_tooltip_item && itemAt.ToolTipText.Length > 0)
					{
						this.owner.item_tooltip.Active = true;
						this.owner.item_tooltip.SetToolTip(this.owner, itemAt.ToolTipText);
						this.prev_tooltip_item = itemAt;
					}
				}
			}

			// Token: 0x06002337 RID: 9015 RVA: 0x000855A0 File Offset: 0x000837A0
			private void ItemsMouseHover(object sender, EventArgs e)
			{
				if (this.owner.hover_pending)
				{
					this.owner.OnMouseHover(e);
					this.owner.hover_pending = false;
				}
				if (base.Capture)
				{
					return;
				}
				this.hover_processed = true;
				Point point = base.PointToClient(Control.MousePosition);
				ListViewItem itemAt = this.owner.GetItemAt(point.X, point.Y);
				if (itemAt == null)
				{
					return;
				}
				this.prev_hovered_item = itemAt;
				if (this.owner.HoverSelection)
				{
					if (this.owner.MultiSelect)
					{
						this.owner.UpdateMultiSelection(itemAt.Index, true);
					}
					else
					{
						itemAt.Selected = true;
					}
					this.owner.SetFocusedItem(itemAt.DisplayIndex);
					base.Select();
				}
				this.owner.OnItemMouseHover(new ListViewItemMouseHoverEventArgs(itemAt));
			}

			// Token: 0x06002338 RID: 9016 RVA: 0x00085684 File Offset: 0x00083884
			private void HandleClicks(MouseEventArgs me)
			{
				if (this.clicks > 1)
				{
					this.owner.OnDoubleClick(EventArgs.Empty);
					this.owner.OnMouseDoubleClick(me);
				}
				else if (this.clicks == 1)
				{
					this.owner.OnClick(EventArgs.Empty);
					this.owner.OnMouseClick(me);
				}
				this.clicks = 0;
			}

			// Token: 0x06002339 RID: 9017 RVA: 0x000856F0 File Offset: 0x000838F0
			private void ItemsMouseUp(object sender, MouseEventArgs me)
			{
				MouseEventArgs mouseEventArgs = this.owner.TranslateMouseEventArgs(me);
				this.HandleClicks(mouseEventArgs);
				base.Capture = false;
				if (this.owner.Items.Count == 0)
				{
					this.ResetMouseState();
					this.owner.OnMouseUp(mouseEventArgs);
					return;
				}
				Point point;
				point..ctor(me.X, me.Y);
				Rectangle rectangle = Rectangle.Empty;
				if (this.clicked_item != null)
				{
					if (this.owner.view == View.Details && !this.owner.full_row_select)
					{
						rectangle = this.clicked_item.GetBounds(ItemBoundsPortion.Label);
					}
					else
					{
						rectangle = this.clicked_item.Bounds;
					}
					if (rectangle.Contains(point))
					{
						ItemActivation activation = this.owner.activation;
						if (activation != ItemActivation.OneClick)
						{
							if (activation == ItemActivation.TwoClick)
							{
								if (this.last_clicked_item == this.clicked_item)
								{
									this.owner.OnItemActivate(EventArgs.Empty);
									this.last_clicked_item = null;
								}
								else
								{
									this.last_clicked_item = this.clicked_item;
								}
							}
						}
						else
						{
							this.owner.OnItemActivate(EventArgs.Empty);
						}
					}
				}
				else if (!this.checking && this.owner.SelectedItems.Count > 0 && this.BoxSelectRectangle.Size.IsEmpty)
				{
					this.owner.SelectedItems.Clear();
				}
				this.ResetMouseState();
				this.owner.OnMouseUp(mouseEventArgs);
			}

			// Token: 0x0600233A RID: 9018 RVA: 0x0008588C File Offset: 0x00083A8C
			private void ResetMouseState()
			{
				this.clicked_item = null;
				this.box_select_start = Point.Empty;
				this.BoxSelectRectangle = Rectangle.Empty;
				this.prev_selection = null;
				this.box_select_mode = ListView.ItemControl.BoxSelect.None;
				this.checking = false;
				this.dragged_item_index = -1;
				this.drag_begin = new Point(-1, -1);
			}

			// Token: 0x0600233B RID: 9019 RVA: 0x000858E0 File Offset: 0x00083AE0
			private void LabelEditFinished(object sender, EventArgs e)
			{
				this.EndEdit(this.edit_item);
			}

			// Token: 0x0600233C RID: 9020 RVA: 0x000858F0 File Offset: 0x00083AF0
			private void LabelEditCancelled(object sender, EventArgs e)
			{
				this.edit_args.SetLabel(null);
				this.EndEdit(this.edit_item);
			}

			// Token: 0x0600233D RID: 9021 RVA: 0x0008590C File Offset: 0x00083B0C
			private void LabelTextChanged(object sender, EventArgs e)
			{
				if (this.edit_args != null)
				{
					this.edit_args.SetLabel(this.edit_text_box.Text);
				}
			}

			// Token: 0x0600233E RID: 9022 RVA: 0x00085930 File Offset: 0x00083B30
			internal void BeginEdit(ListViewItem item)
			{
				if (this.edit_item != null)
				{
					this.EndEdit(this.edit_item);
				}
				if (this.edit_text_box == null)
				{
					this.edit_text_box = new ListView.ListViewLabelEditTextBox();
					this.edit_text_box.BorderStyle = BorderStyle.FixedSingle;
					this.edit_text_box.EditingCancelled += new EventHandler(this.LabelEditCancelled);
					this.edit_text_box.EditingFinished += new EventHandler(this.LabelEditFinished);
					this.edit_text_box.TextChanged += new EventHandler(this.LabelTextChanged);
					this.edit_text_box.Visible = false;
					base.Controls.Add(this.edit_text_box);
				}
				item.EnsureVisible();
				this.edit_text_box.Reset();
				switch (this.owner.view)
				{
				case View.LargeIcon:
				{
					this.edit_text_box.TextAlign = HorizontalAlignment.Center;
					this.edit_text_box.Bounds = item.GetBounds(ItemBoundsPortion.Label);
					SizeF sizeF = TextRenderer.MeasureString(item.Text, item.Font);
					this.edit_text_box.Width = (int)sizeF.Width + 4;
					this.edit_text_box.MaxWidth = item.GetBounds(ItemBoundsPortion.Entire).Width;
					this.edit_text_box.MaxHeight = this.owner.ClientRectangle.Height - this.edit_text_box.Bounds.Y;
					this.edit_text_box.WordWrap = true;
					this.edit_text_box.Multiline = true;
					break;
				}
				case View.Details:
				case View.SmallIcon:
				case View.List:
				{
					this.edit_text_box.TextAlign = HorizontalAlignment.Left;
					this.edit_text_box.Bounds = item.GetBounds(ItemBoundsPortion.Label);
					SizeF sizeF = TextRenderer.MeasureString(item.Text, item.Font);
					this.edit_text_box.Width = (int)sizeF.Width + 4;
					this.edit_text_box.MaxWidth = this.owner.ClientRectangle.Width - this.edit_text_box.Bounds.X;
					this.edit_text_box.WordWrap = false;
					this.edit_text_box.Multiline = false;
					break;
				}
				}
				this.edit_item = item;
				this.edit_text_box.Text = item.Text;
				this.edit_text_box.Font = item.Font;
				this.edit_text_box.Visible = true;
				this.edit_text_box.Focus();
				this.edit_text_box.SelectAll();
				this.edit_args = new LabelEditEventArgs(this.owner.Items.IndexOf(this.edit_item));
				this.owner.OnBeforeLabelEdit(this.edit_args);
				if (this.edit_args.CancelEdit)
				{
					this.EndEdit(item);
				}
			}

			// Token: 0x0600233F RID: 9023 RVA: 0x00085BEC File Offset: 0x00083DEC
			internal void CancelEdit(ListViewItem item)
			{
				if (this.edit_item == null || this.edit_item != item)
				{
					return;
				}
				this.edit_args.SetLabel(null);
				this.EndEdit(item);
			}

			// Token: 0x06002340 RID: 9024 RVA: 0x00085C1C File Offset: 0x00083E1C
			internal void EndEdit(ListViewItem item)
			{
				if (this.edit_item == null || this.edit_item != item)
				{
					return;
				}
				if (this.edit_text_box != null)
				{
					if (this.edit_text_box.Visible)
					{
						this.edit_text_box.Visible = false;
					}
					this.owner.Focus();
				}
				Application.DoEvents();
				LabelEditEventArgs labelEditEventArgs = new LabelEditEventArgs(item.Index, this.edit_args.Label);
				this.edit_item = null;
				this.owner.OnAfterLabelEdit(labelEditEventArgs);
				if (!labelEditEventArgs.CancelEdit && labelEditEventArgs.Label != null)
				{
					item.Text = labelEditEventArgs.Label;
				}
			}

			// Token: 0x06002341 RID: 9025 RVA: 0x00085CC8 File Offset: 0x00083EC8
			internal override void OnPaintInternal(PaintEventArgs pe)
			{
				ThemeEngine.Current.DrawListViewItems(pe.Graphics, pe.ClipRectangle, this.owner);
			}

			// Token: 0x06002342 RID: 9026 RVA: 0x00085CF4 File Offset: 0x00083EF4
			protected override void WndProc(ref Message m)
			{
				Msg msg = (Msg)m.Msg;
				switch (msg)
				{
				case Msg.WM_LBUTTONDOWN:
					if (!this.Focused)
					{
						this.owner.Select(false, true);
					}
					break;
				default:
					if (msg != Msg.WM_SETFOCUS)
					{
						if (msg == Msg.WM_KILLFOCUS)
						{
							this.owner.Select(false, true);
						}
					}
					else
					{
						this.owner.Select(false, true);
					}
					break;
				case Msg.WM_RBUTTONDOWN:
					if (!this.Focused)
					{
						this.owner.Select(false, true);
					}
					break;
				}
				base.WndProc(ref m);
			}

			// Token: 0x0400124D RID: 4685
			private ListView owner;

			// Token: 0x0400124E RID: 4686
			private ListViewItem clicked_item;

			// Token: 0x0400124F RID: 4687
			private ListViewItem last_clicked_item;

			// Token: 0x04001250 RID: 4688
			private bool hover_processed;

			// Token: 0x04001251 RID: 4689
			private bool checking;

			// Token: 0x04001252 RID: 4690
			private ListViewItem prev_hovered_item;

			// Token: 0x04001253 RID: 4691
			private ListViewItem prev_tooltip_item;

			// Token: 0x04001254 RID: 4692
			private int clicks;

			// Token: 0x04001255 RID: 4693
			private Point drag_begin = new Point(-1, -1);

			// Token: 0x04001256 RID: 4694
			internal int dragged_item_index = -1;

			// Token: 0x04001257 RID: 4695
			private ListView.ListViewLabelEditTextBox edit_text_box;

			// Token: 0x04001258 RID: 4696
			internal ListViewItem edit_item;

			// Token: 0x04001259 RID: 4697
			private LabelEditEventArgs edit_args;

			// Token: 0x0400125A RID: 4698
			private ListView.ItemControl.BoxSelect box_select_mode;

			// Token: 0x0400125B RID: 4699
			private IList prev_selection;

			// Token: 0x0400125C RID: 4700
			private Point box_select_start;

			// Token: 0x0400125D RID: 4701
			private Rectangle box_select_rect;

			// Token: 0x0200021E RID: 542
			private enum BoxSelect
			{
				// Token: 0x0400125F RID: 4703
				None,
				// Token: 0x04001260 RID: 4704
				Normal,
				// Token: 0x04001261 RID: 4705
				Shift,
				// Token: 0x04001262 RID: 4706
				Control
			}
		}

		// Token: 0x0200021F RID: 543
		internal class ListViewLabelEditTextBox : TextBox
		{
			// Token: 0x06002343 RID: 9027 RVA: 0x00085DA4 File Offset: 0x00083FA4
			public ListViewLabelEditTextBox()
			{
				this.min_height = this.DefaultSize.Height;
				this.text_size_one_char = TextRenderer.MeasureString("B", this.Font);
			}

			// Token: 0x06002344 RID: 9028 RVA: 0x00085E04 File Offset: 0x00084004
			// Note: this type is marked as 'beforefieldinit'.
			static ListViewLabelEditTextBox()
			{
				ListView.ListViewLabelEditTextBox.EditingCancelledEvent = new object();
				ListView.ListViewLabelEditTextBox.EditingFinishedEvent = new object();
			}

			// Token: 0x1400022E RID: 558
			// (add) Token: 0x06002345 RID: 9029 RVA: 0x00085E1C File Offset: 0x0008401C
			// (remove) Token: 0x06002346 RID: 9030 RVA: 0x00085E30 File Offset: 0x00084030
			public event EventHandler EditingCancelled
			{
				add
				{
					base.Events.AddHandler(ListView.ListViewLabelEditTextBox.EditingCancelledEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ListView.ListViewLabelEditTextBox.EditingCancelledEvent, value);
				}
			}

			// Token: 0x1400022F RID: 559
			// (add) Token: 0x06002347 RID: 9031 RVA: 0x00085E44 File Offset: 0x00084044
			// (remove) Token: 0x06002348 RID: 9032 RVA: 0x00085E58 File Offset: 0x00084058
			public event EventHandler EditingFinished
			{
				add
				{
					base.Events.AddHandler(ListView.ListViewLabelEditTextBox.EditingFinishedEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ListView.ListViewLabelEditTextBox.EditingFinishedEvent, value);
				}
			}

			// Token: 0x170008A8 RID: 2216
			// (set) Token: 0x06002349 RID: 9033 RVA: 0x00085E6C File Offset: 0x0008406C
			public int MaxWidth
			{
				set
				{
					if (value < this.min_width)
					{
						this.max_width = this.min_width;
					}
					else
					{
						this.max_width = value;
					}
				}
			}

			// Token: 0x170008A9 RID: 2217
			// (set) Token: 0x0600234A RID: 9034 RVA: 0x00085EA0 File Offset: 0x000840A0
			public int MaxHeight
			{
				set
				{
					if (value < this.min_height)
					{
						this.max_height = this.min_height;
					}
					else
					{
						this.max_height = value;
					}
				}
			}

			// Token: 0x170008AA RID: 2218
			// (get) Token: 0x0600234B RID: 9035 RVA: 0x00085ED4 File Offset: 0x000840D4
			// (set) Token: 0x0600234C RID: 9036 RVA: 0x00085EDC File Offset: 0x000840DC
			public new int Width
			{
				get
				{
					return base.Width;
				}
				set
				{
					this.min_width = value;
					base.Width = value;
				}
			}

			// Token: 0x170008AB RID: 2219
			// (get) Token: 0x0600234D RID: 9037 RVA: 0x00085EEC File Offset: 0x000840EC
			// (set) Token: 0x0600234E RID: 9038 RVA: 0x00085EF4 File Offset: 0x000840F4
			public override Font Font
			{
				get
				{
					return base.Font;
				}
				set
				{
					base.Font = value;
					this.text_size_one_char = TextRenderer.MeasureString("B", this.Font);
				}
			}

			// Token: 0x0600234F RID: 9039 RVA: 0x00085F14 File Offset: 0x00084114
			protected override void OnTextChanged(EventArgs e)
			{
				int num = (int)TextRenderer.MeasureString(this.Text, this.Font).Width + 8;
				if (!this.Multiline)
				{
					this.ResizeTextBoxWidth(num);
				}
				else
				{
					if (this.Width != this.max_width)
					{
						this.ResizeTextBoxWidth(num);
					}
					int num2 = base.Lines.Length;
					if (num2 != this.old_number_lines)
					{
						int num3 = num2 * (int)this.text_size_one_char.Height + 4;
						this.old_number_lines = num2;
						this.ResizeTextBoxHeight(num3);
					}
				}
				base.OnTextChanged(e);
			}

			// Token: 0x06002350 RID: 9040 RVA: 0x00085FA8 File Offset: 0x000841A8
			protected override bool IsInputKey(Keys key_data)
			{
				if ((key_data & Keys.Alt) == Keys.None)
				{
					Keys keys = key_data & Keys.KeyCode;
					if (keys == Keys.Return)
					{
						return true;
					}
					if (keys == Keys.Escape)
					{
						return true;
					}
				}
				return base.IsInputKey(key_data);
			}

			// Token: 0x06002351 RID: 9041 RVA: 0x00085FEC File Offset: 0x000841EC
			protected override void OnKeyDown(KeyEventArgs e)
			{
				if (!base.Visible)
				{
					return;
				}
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					if (keyCode == Keys.Escape)
					{
						base.Visible = false;
						e.Handled = true;
						this.OnEditingCancelled(e);
					}
				}
				else
				{
					base.Visible = false;
					e.Handled = true;
					this.OnEditingFinished(e);
				}
			}

			// Token: 0x06002352 RID: 9042 RVA: 0x00086058 File Offset: 0x00084258
			protected override void OnLostFocus(EventArgs e)
			{
				if (base.Visible)
				{
					this.OnEditingFinished(e);
				}
			}

			// Token: 0x06002353 RID: 9043 RVA: 0x0008606C File Offset: 0x0008426C
			protected void OnEditingCancelled(EventArgs e)
			{
				EventHandler eventHandler = (EventHandler)base.Events[ListView.ListViewLabelEditTextBox.EditingCancelledEvent];
				if (eventHandler != null)
				{
					eventHandler.Invoke(this, e);
				}
			}

			// Token: 0x06002354 RID: 9044 RVA: 0x000860A0 File Offset: 0x000842A0
			protected void OnEditingFinished(EventArgs e)
			{
				EventHandler eventHandler = (EventHandler)base.Events[ListView.ListViewLabelEditTextBox.EditingFinishedEvent];
				if (eventHandler != null)
				{
					eventHandler.Invoke(this, e);
				}
			}

			// Token: 0x06002355 RID: 9045 RVA: 0x000860D4 File Offset: 0x000842D4
			private void ResizeTextBoxWidth(int new_width)
			{
				if (new_width > this.max_width)
				{
					base.Width = this.max_width;
				}
				else if (new_width >= this.min_width)
				{
					base.Width = new_width;
				}
				else
				{
					base.Width = this.min_width;
				}
			}

			// Token: 0x06002356 RID: 9046 RVA: 0x00086124 File Offset: 0x00084324
			private void ResizeTextBoxHeight(int new_height)
			{
				if (new_height > this.max_height)
				{
					base.Height = this.max_height;
				}
				else if (new_height >= this.min_height)
				{
					base.Height = new_height;
				}
				else
				{
					base.Height = this.min_height;
				}
			}

			// Token: 0x06002357 RID: 9047 RVA: 0x00086174 File Offset: 0x00084374
			public void Reset()
			{
				this.max_width = -1;
				this.min_width = -1;
				this.max_height = -1;
				this.old_number_lines = 1;
				this.Text = string.Empty;
				base.Size = this.DefaultSize;
			}

			// Token: 0x04001263 RID: 4707
			private int max_width = -1;

			// Token: 0x04001264 RID: 4708
			private int min_width = -1;

			// Token: 0x04001265 RID: 4709
			private int max_height = -1;

			// Token: 0x04001266 RID: 4710
			private int min_height = -1;

			// Token: 0x04001267 RID: 4711
			private int old_number_lines = 1;

			// Token: 0x04001268 RID: 4712
			private SizeF text_size_one_char;
		}

		// Token: 0x02000220 RID: 544
		internal class HeaderControl : Control
		{
			// Token: 0x06002358 RID: 9048 RVA: 0x000861AC File Offset: 0x000843AC
			public HeaderControl(ListView owner)
			{
				this.owner = owner;
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.MouseDown += this.HeaderMouseDown;
				base.MouseMove += this.HeaderMouseMove;
				base.MouseUp += this.HeaderMouseUp;
				base.MouseLeave += new EventHandler(this.OnMouseLeave);
			}

			// Token: 0x170008AC RID: 2220
			// (get) Token: 0x06002359 RID: 9049 RVA: 0x00086224 File Offset: 0x00084424
			// (set) Token: 0x0600235A RID: 9050 RVA: 0x0008622C File Offset: 0x0008442C
			internal ColumnHeader EnteredColumnHeader
			{
				get
				{
					return this.entered_column_header;
				}
				private set
				{
					if (this.entered_column_header == value)
					{
						return;
					}
					if (ThemeEngine.Current.ListViewHasHotHeaderStyle)
					{
						Region region = new Region();
						region.MakeEmpty();
						if (this.entered_column_header != null)
						{
							region.Union(this.GetColumnHeaderInvalidateArea(this.entered_column_header));
						}
						this.entered_column_header = value;
						if (this.entered_column_header != null)
						{
							region.Union(this.GetColumnHeaderInvalidateArea(this.entered_column_header));
						}
						base.Invalidate(region);
						region.Dispose();
					}
					else
					{
						this.entered_column_header = value;
					}
				}
			}

			// Token: 0x0600235B RID: 9051 RVA: 0x000862BC File Offset: 0x000844BC
			private void OnMouseLeave(object sender, EventArgs e)
			{
				this.EnteredColumnHeader = null;
			}

			// Token: 0x0600235C RID: 9052 RVA: 0x000862C8 File Offset: 0x000844C8
			private ColumnHeader ColumnAtX(int x)
			{
				Point point;
				point..ctor(x, 0);
				ColumnHeader columnHeader = null;
				foreach (object obj in this.owner.Columns)
				{
					ColumnHeader columnHeader2 = (ColumnHeader)obj;
					if (columnHeader2.Rect.Contains(point))
					{
						columnHeader = columnHeader2;
						break;
					}
				}
				return columnHeader;
			}

			// Token: 0x0600235D RID: 9053 RVA: 0x00086360 File Offset: 0x00084560
			private int GetReorderedIndex(ColumnHeader col)
			{
				if (this.owner.reordered_column_indices == null)
				{
					return col.Index;
				}
				for (int i = 0; i < this.owner.Columns.Count; i++)
				{
					if (this.owner.reordered_column_indices[i] == col.Index)
					{
						return i;
					}
				}
				throw new Exception("Column index missing from reordered array");
			}

			// Token: 0x0600235E RID: 9054 RVA: 0x000863CC File Offset: 0x000845CC
			private void HeaderMouseDown(object sender, MouseEventArgs me)
			{
				if (this.resize_column != null)
				{
					this.column_resize_active = true;
					base.Capture = true;
					return;
				}
				this.clicked_column = this.ColumnAtX(me.X + this.owner.h_marker);
				if (this.clicked_column != null)
				{
					base.Capture = true;
					if (this.owner.AllowColumnReorder)
					{
						this.drag_x = me.X;
						this.drag_column = (ColumnHeader)this.clicked_column.Clone();
						this.drag_column.Rect = this.clicked_column.Rect;
						this.drag_to_index = this.GetReorderedIndex(this.clicked_column);
					}
					this.clicked_column.Pressed = true;
					this.Invalidate(this.clicked_column);
					return;
				}
			}

			// Token: 0x0600235F RID: 9055 RVA: 0x00086498 File Offset: 0x00084698
			private void Invalidate(ColumnHeader columnHeader)
			{
				base.Invalidate(this.GetColumnHeaderInvalidateArea(columnHeader));
			}

			// Token: 0x06002360 RID: 9056 RVA: 0x000864A8 File Offset: 0x000846A8
			private Rectangle GetColumnHeaderInvalidateArea(ColumnHeader columnHeader)
			{
				Rectangle rect = columnHeader.Rect;
				rect.X -= this.owner.h_marker;
				return rect;
			}

			// Token: 0x06002361 RID: 9057 RVA: 0x000864D8 File Offset: 0x000846D8
			private void StopResize()
			{
				this.column_resize_active = false;
				this.resize_column = null;
				base.Capture = false;
				this.Cursor = Cursors.Default;
			}

			// Token: 0x06002362 RID: 9058 RVA: 0x00086508 File Offset: 0x00084708
			private void HeaderMouseMove(object sender, MouseEventArgs me)
			{
				Point point;
				point..ctor(me.X + this.owner.h_marker, me.Y);
				if (this.column_resize_active)
				{
					int num = point.X - this.resize_column.X;
					if (num < 0)
					{
						num = 0;
					}
					if (!this.owner.CanProceedWithResize(this.resize_column, num))
					{
						this.StopResize();
						return;
					}
					this.resize_column.Width = num;
					return;
				}
				else
				{
					this.resize_column = null;
					if (this.clicked_column != null)
					{
						if (this.owner.AllowColumnReorder)
						{
							Rectangle rect = this.drag_column.Rect;
							rect.X = this.clicked_column.Rect.X + me.X - this.drag_x;
							this.drag_column.Rect = rect;
							int num2 = me.X + this.owner.h_marker;
							ColumnHeader columnHeader = this.ColumnAtX(num2);
							if (columnHeader == null)
							{
								this.drag_to_index = this.owner.Columns.Count;
							}
							else if (num2 < columnHeader.X + columnHeader.Width / 2)
							{
								this.drag_to_index = this.GetReorderedIndex(columnHeader);
							}
							else
							{
								this.drag_to_index = this.GetReorderedIndex(columnHeader) + 1;
							}
							base.Invalidate();
						}
						else
						{
							ColumnHeader columnHeader2 = this.ColumnAtX(me.X + this.owner.h_marker);
							bool pressed = this.clicked_column.Pressed;
							this.clicked_column.Pressed = columnHeader2 == this.clicked_column;
							if (this.clicked_column.Pressed ^ pressed)
							{
								this.Invalidate(this.clicked_column);
							}
						}
						return;
					}
					for (int i = 0; i < this.owner.Columns.Count; i++)
					{
						Rectangle rect2 = this.owner.Columns[i].Rect;
						if (rect2.Contains(point))
						{
							this.EnteredColumnHeader = this.owner.Columns[i];
						}
						rect2.X = rect2.Right - 5;
						rect2.Width = 10;
						if (rect2.Contains(point))
						{
							if (i < this.owner.Columns.Count - 1 && this.owner.Columns[i + 1].Width == 0)
							{
								i++;
							}
							this.resize_column = this.owner.Columns[i];
							break;
						}
					}
					if (this.resize_column == null)
					{
						this.Cursor = Cursors.Default;
					}
					else
					{
						this.Cursor = Cursors.VSplit;
					}
					return;
				}
			}

			// Token: 0x06002363 RID: 9059 RVA: 0x000867D4 File Offset: 0x000849D4
			private void HeaderMouseUp(object sender, MouseEventArgs me)
			{
				base.Capture = false;
				if (this.column_resize_active)
				{
					int index = this.resize_column.Index;
					this.StopResize();
					this.owner.RaiseColumnWidthChanged(index);
					return;
				}
				if (this.clicked_column != null && this.clicked_column.Pressed)
				{
					this.clicked_column.Pressed = false;
					this.Invalidate(this.clicked_column);
					this.owner.OnColumnClick(new ColumnClickEventArgs(this.clicked_column.Index));
				}
				if (this.drag_column != null && this.owner.AllowColumnReorder)
				{
					this.drag_column = null;
					if (this.drag_to_index > this.GetReorderedIndex(this.clicked_column))
					{
						this.drag_to_index--;
					}
					if (this.owner.GetReorderedColumn(this.drag_to_index) != this.clicked_column)
					{
						this.owner.ReorderColumn(this.clicked_column, this.drag_to_index, true);
					}
					this.drag_to_index = -1;
					base.Invalidate();
				}
				this.clicked_column = null;
			}

			// Token: 0x06002364 RID: 9060 RVA: 0x000868F0 File Offset: 0x00084AF0
			internal override void OnPaintInternal(PaintEventArgs pe)
			{
				if (this.owner.updating)
				{
					return;
				}
				Theme theme = ThemeEngine.Current;
				theme.DrawListViewHeader(pe.Graphics, pe.ClipRectangle, this.owner);
				if (this.drag_column == null)
				{
					return;
				}
				int num;
				if (this.drag_to_index == this.owner.Columns.Count)
				{
					num = this.owner.GetReorderedColumn(this.drag_to_index - 1).Rect.Right - this.owner.h_marker;
				}
				else
				{
					num = this.owner.GetReorderedColumn(this.drag_to_index).Rect.X - this.owner.h_marker;
				}
				theme.DrawListViewHeaderDragDetails(pe.Graphics, this.owner, this.drag_column, num);
			}

			// Token: 0x06002365 RID: 9061 RVA: 0x000869CC File Offset: 0x00084BCC
			protected override void WndProc(ref Message m)
			{
				Msg msg = (Msg)m.Msg;
				if (msg != Msg.WM_SETFOCUS)
				{
					base.WndProc(ref m);
				}
				else
				{
					this.owner.Focus();
				}
			}

			// Token: 0x0400126B RID: 4715
			private ListView owner;

			// Token: 0x0400126C RID: 4716
			private bool column_resize_active;

			// Token: 0x0400126D RID: 4717
			private ColumnHeader resize_column;

			// Token: 0x0400126E RID: 4718
			private ColumnHeader clicked_column;

			// Token: 0x0400126F RID: 4719
			private ColumnHeader drag_column;

			// Token: 0x04001270 RID: 4720
			private int drag_x;

			// Token: 0x04001271 RID: 4721
			private int drag_to_index = -1;

			// Token: 0x04001272 RID: 4722
			private ColumnHeader entered_column_header;
		}

		// Token: 0x02000221 RID: 545
		private class ItemComparer : IComparer
		{
			// Token: 0x06002366 RID: 9062 RVA: 0x00086A0C File Offset: 0x00084C0C
			public ItemComparer(SortOrder sortOrder)
			{
				this.sort_order = sortOrder;
			}

			// Token: 0x06002367 RID: 9063 RVA: 0x00086A1C File Offset: 0x00084C1C
			public int Compare(object x, object y)
			{
				ListViewItem listViewItem = x as ListViewItem;
				ListViewItem listViewItem2 = y as ListViewItem;
				if (this.sort_order == SortOrder.Ascending)
				{
					return string.Compare(listViewItem.Text, listViewItem2.Text);
				}
				return string.Compare(listViewItem2.Text, listViewItem.Text);
			}

			// Token: 0x04001273 RID: 4723
			private readonly SortOrder sort_order;
		}

		/// <summary>Represents the collection containing the indexes to the checked items in a list view control.</summary>
		// Token: 0x02000222 RID: 546
		[ListBindable(false)]
		public class CheckedIndexCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection. </param>
			// Token: 0x06002368 RID: 9064 RVA: 0x00086A68 File Offset: 0x00084C68
			public CheckedIndexCollection(ListView owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170008AD RID: 2221
			// (get) Token: 0x06002369 RID: 9065 RVA: 0x00086A78 File Offset: 0x00084C78
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170008AE RID: 2222
			// (get) Token: 0x0600236A RID: 9066 RVA: 0x00086A7C File Offset: 0x00084C7C
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170008AF RID: 2223
			// (get) Token: 0x0600236B RID: 9067 RVA: 0x00086A80 File Offset: 0x00084C80
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets an object in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</summary>
			/// <returns>The object from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			// Token: 0x170008B0 RID: 2224
			// (get) Token: 0x0600236C RID: 9068 RVA: 0x00086A84 File Offset: 0x00084C84
			// (set) Token: 0x0600236D RID: 9069 RVA: 0x00086A94 File Offset: 0x00084C94
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Copies the collection of checked-item indexes into an array.</summary>
			/// <param name="dest">An array of type <see cref="T:System.Int32" />.</param>
			/// <param name="index">The zero-based index in the array at which copying begins. </param>
			/// <exception cref="T:System.ArrayTypeMismatchException">The array type cannot be cast to an <see cref="T:System.Int32" />.</exception>
			// Token: 0x0600236E RID: 9070 RVA: 0x00086AA0 File Offset: 0x00084CA0
			void ICollection.CopyTo(Array dest, int index)
			{
				int[] indices = this.GetIndices();
				Array.Copy(indices, 0, dest, index, indices.Length);
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <returns>The zero-based index where <paramref name="value" /> is located in the collection.</returns>
			/// <param name="value">The object to add to the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x0600236F RID: 9071 RVA: 0x00086AC0 File Offset: 0x00084CC0
			int IList.Add(object value)
			{
				throw new NotSupportedException("Add operation is not supported.");
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002370 RID: 9072 RVA: 0x00086ACC File Offset: 0x00084CCC
			void IList.Clear()
			{
				throw new NotSupportedException("Clear operation is not supported.");
			}

			/// <summary>Checks whether the index corresponding with the <see cref="T:System.Windows.Forms.ListViewItem" /> is checked.</summary>
			/// <returns>true if the index is found in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, false.</returns>
			/// <param name="checkedIndex">An index to locate in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			// Token: 0x06002371 RID: 9073 RVA: 0x00086AD8 File Offset: 0x00084CD8
			bool IList.Contains(object checkedIndex)
			{
				return checkedIndex is int && this.Contains((int)checkedIndex);
			}

			/// <summary>Returns the index of the specified object in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />. </summary>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located if it is in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, -1.</returns>
			/// <param name="checkedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection.</param>
			// Token: 0x06002372 RID: 9074 RVA: 0x00086AF4 File Offset: 0x00084CF4
			int IList.IndexOf(object checkedIndex)
			{
				if (!(checkedIndex is int))
				{
					return -1;
				}
				return this.IndexOf((int)checkedIndex);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002373 RID: 9075 RVA: 0x00086B10 File Offset: 0x00084D10
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException("Insert operation is not supported.");
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002374 RID: 9076 RVA: 0x00086B1C File Offset: 0x00084D1C
			void IList.Remove(object value)
			{
				throw new NotSupportedException("Remove operation is not supported.");
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002375 RID: 9077 RVA: 0x00086B28 File Offset: 0x00084D28
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException("RemoveAt operation is not supported.");
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170008B1 RID: 2225
			// (get) Token: 0x06002376 RID: 9078 RVA: 0x00086B34 File Offset: 0x00084D34
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.CheckedItems.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170008B2 RID: 2226
			// (get) Token: 0x06002377 RID: 9079 RVA: 0x00086B48 File Offset: 0x00084D48
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the index value at the specified index within the collection.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.CheckedIndexCollection.Count" /> property of <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />. </exception>
			// Token: 0x170008B3 RID: 2227
			public int this[int index]
			{
				get
				{
					int[] indices = this.GetIndices();
					if (index < 0 || index >= indices.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return indices[index];
				}
			}

			/// <summary>Determines whether the specified index is located in the collection.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, false.</returns>
			/// <param name="checkedIndex">The index to locate in the collection. </param>
			// Token: 0x06002379 RID: 9081 RVA: 0x00086B80 File Offset: 0x00084D80
			public bool Contains(int checkedIndex)
			{
				int[] indices = this.GetIndices();
				for (int i = 0; i < indices.Length; i++)
				{
					if (indices[i] == checkedIndex)
					{
						return true;
					}
				}
				return false;
			}

			/// <summary>Returns an enumerator that can be used to iterate through the checked index collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the checked index collection.</returns>
			// Token: 0x0600237A RID: 9082 RVA: 0x00086BB4 File Offset: 0x00084DB4
			public IEnumerator GetEnumerator()
			{
				int[] indices = this.GetIndices();
				return indices.GetEnumerator();
			}

			/// <summary>Returns the index within the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> of the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the list view control.</summary>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located within the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, -1 if the index is not located in the collection.</returns>
			/// <param name="checkedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection. </param>
			// Token: 0x0600237B RID: 9083 RVA: 0x00086BD0 File Offset: 0x00084DD0
			public int IndexOf(int checkedIndex)
			{
				int[] indices = this.GetIndices();
				for (int i = 0; i < indices.Length; i++)
				{
					if (indices[i] == checkedIndex)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x0600237C RID: 9084 RVA: 0x00086C04 File Offset: 0x00084E04
			private int[] GetIndices()
			{
				ArrayList list = this.owner.CheckedItems.List;
				int[] array = new int[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					ListViewItem listViewItem = (ListViewItem)list[i];
					array[i] = listViewItem.Index;
				}
				return array;
			}

			// Token: 0x04001274 RID: 4724
			private readonly ListView owner;
		}

		/// <summary>Represents the collection of checked items in a list view control.</summary>
		// Token: 0x02000223 RID: 547
		[ListBindable(false)]
		public class CheckedListViewItemCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection. </param>
			// Token: 0x0600237D RID: 9085 RVA: 0x00086C5C File Offset: 0x00084E5C
			public CheckedListViewItemCollection(ListView owner)
			{
				this.owner = owner;
				this.owner.Items.Changed += this.ItemsCollection_Changed;
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170008B4 RID: 2228
			// (get) Token: 0x0600237E RID: 9086 RVA: 0x00086C88 File Offset: 0x00084E88
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170008B5 RID: 2229
			// (get) Token: 0x0600237F RID: 9087 RVA: 0x00086C8C File Offset: 0x00084E8C
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x06002380 RID: 9088 RVA: 0x00086C90 File Offset: 0x00084E90
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets an object from the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <exception cref="T:System.NotSupportedException">This property cannot be set.</exception>
			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x06002381 RID: 9089 RVA: 0x00086C94 File Offset: 0x00084E94
			// (set) Token: 0x06002382 RID: 9090 RVA: 0x00086CA0 File Offset: 0x00084EA0
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <returns>The zero-based index where value is located in the collection.</returns>
			/// <param name="value">The item to add to the collection.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002383 RID: 9091 RVA: 0x00086CAC File Offset: 0x00084EAC
			int IList.Add(object value)
			{
				throw new NotSupportedException("Add operation is not supported.");
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002384 RID: 9092 RVA: 0x00086CB8 File Offset: 0x00084EB8
			void IList.Clear()
			{
				throw new NotSupportedException("Clear operation is not supported.");
			}

			/// <summary>Verifies whether the item is checked.</summary>
			/// <returns>true if item is found in the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />; otherwise, false.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> to locate in the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />.</param>
			// Token: 0x06002385 RID: 9093 RVA: 0x00086CC4 File Offset: 0x00084EC4
			bool IList.Contains(object item)
			{
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to locate in the collection.</param>
			// Token: 0x06002386 RID: 9094 RVA: 0x00086CE0 File Offset: 0x00084EE0
			int IList.IndexOf(object item)
			{
				if (!(item is ListViewItem))
				{
					return -1;
				}
				return this.IndexOf((ListViewItem)item);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The index at which value should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002387 RID: 9095 RVA: 0x00086CFC File Offset: 0x00084EFC
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException("Insert operation is not supported.");
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002388 RID: 9096 RVA: 0x00086D08 File Offset: 0x00084F08
			void IList.Remove(object value)
			{
				throw new NotSupportedException("Remove operation is not supported.");
			}

			/// <summary>Removes an item from the collection at the specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002389 RID: 9097 RVA: 0x00086D14 File Offset: 0x00084F14
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException("RemoveAt operation is not supported.");
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x0600238A RID: 9098 RVA: 0x00086D20 File Offset: 0x00084F20
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (!this.owner.CheckBoxes)
					{
						return 0;
					}
					return this.List.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x0600238B RID: 9099 RVA: 0x00086D40 File Offset: 0x00084F40
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the item at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.CheckedListViewItemCollection.Count" /> property of <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />. </exception>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x170008BA RID: 2234
			public ListViewItem this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException();
					}
					ArrayList arrayList = this.List;
					if (index < 0 || index >= arrayList.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return (ListViewItem)arrayList[index];
				}
			}

			/// <summary>Gets an item with the specified key within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item with the specified index within the collection.</returns>
			/// <param name="key">The key of the item in the collection to retrieve.</param>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x170008BB RID: 2235
			public virtual ListViewItem this[string key]
			{
				get
				{
					int num = this.IndexOfKey(key);
					return (num != -1) ? ((ListViewItem)this.List[num]) : null;
				}
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x0600238E RID: 9102 RVA: 0x00086DCC File Offset: 0x00084FCC
			public bool Contains(ListViewItem item)
			{
				return this.owner.CheckBoxes && this.List.Contains(item);
			}

			/// <summary>Determines if a column with the specified key is contained in the collection.</summary>
			/// <returns>true if an item with the specified key is contained in the collection; otherwise, false.</returns>
			/// <param name="key">The name of the item to search for.</param>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x0600238F RID: 9103 RVA: 0x00086DEC File Offset: 0x00084FEC
			public virtual bool ContainsKey(string key)
			{
				return this.IndexOfKey(key) != -1;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x06002390 RID: 9104 RVA: 0x00086DFC File Offset: 0x00084FFC
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (!this.owner.CheckBoxes)
				{
					return;
				}
				this.List.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the checked item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the checked item collection.</returns>
			// Token: 0x06002391 RID: 9105 RVA: 0x00086E40 File Offset: 0x00085040
			public IEnumerator GetEnumerator()
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (!this.owner.CheckBoxes)
				{
					return new ListViewItem[0].GetEnumerator();
				}
				return this.List.GetEnumerator();
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item in the collection; otherwise, -1.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x06002392 RID: 9106 RVA: 0x00086E8C File Offset: 0x0008508C
			public int IndexOf(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (!this.owner.CheckBoxes)
				{
					return -1;
				}
				return this.List.IndexOf(item);
			}

			/// <summary>Determines the index for an item with the specified key.</summary>
			/// <returns>The zero-based index for the <see cref="T:System.Windows.Forms.ListViewItem" /> with the specified name, if found; otherwise, -1.</returns>
			/// <param name="key">The name of the item to retrieve the index for.</param>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x06002393 RID: 9107 RVA: 0x00086ED0 File Offset: 0x000850D0
			public virtual int IndexOfKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (key == null || key.Length == 0)
				{
					return -1;
				}
				ArrayList arrayList = this.List;
				for (int i = 0; i < arrayList.Count; i++)
				{
					ListViewItem listViewItem = (ListViewItem)arrayList[i];
					if (string.Compare(key, listViewItem.Name, true) == 0)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x06002394 RID: 9108 RVA: 0x00086F48 File Offset: 0x00085148
			internal ArrayList List
			{
				get
				{
					if (this.list == null)
					{
						this.list = new ArrayList();
						foreach (object obj in this.owner.Items)
						{
							ListViewItem listViewItem = (ListViewItem)obj;
							if (listViewItem.Checked)
							{
								this.list.Add(listViewItem);
							}
						}
					}
					return this.list;
				}
			}

			// Token: 0x06002395 RID: 9109 RVA: 0x00086FEC File Offset: 0x000851EC
			internal void Reset()
			{
				this.list = null;
			}

			// Token: 0x06002396 RID: 9110 RVA: 0x00086FF8 File Offset: 0x000851F8
			private void ItemsCollection_Changed()
			{
				this.Reset();
			}

			// Token: 0x04001275 RID: 4725
			private readonly ListView owner;

			// Token: 0x04001276 RID: 4726
			private ArrayList list;
		}

		/// <summary>Represents the collection of column headers in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x02000224 RID: 548
		[ListBindable(false)]
		public class ColumnHeaderCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> that owns this collection. </param>
			// Token: 0x06002397 RID: 9111 RVA: 0x00087000 File Offset: 0x00085200
			public ColumnHeaderCollection(ListView owner)
			{
				this.list = new ArrayList();
				this.owner = owner;
			}

			// Token: 0x06002398 RID: 9112 RVA: 0x0008701C File Offset: 0x0008521C
			// Note: this type is marked as 'beforefieldinit'.
			static ColumnHeaderCollection()
			{
				ListView.ColumnHeaderCollection.UIACollectionChangedEvent = new object();
			}

			// Token: 0x14000230 RID: 560
			// (add) Token: 0x06002399 RID: 9113 RVA: 0x00087028 File Offset: 0x00085228
			// (remove) Token: 0x0600239A RID: 9114 RVA: 0x0008704C File Offset: 0x0008524C
			internal event CollectionChangeEventHandler UIACollectionChanged
			{
				add
				{
					if (this.owner != null)
					{
						this.owner.Events.AddHandler(ListView.ColumnHeaderCollection.UIACollectionChangedEvent, value);
					}
				}
				remove
				{
					if (this.owner != null)
					{
						this.owner.Events.RemoveHandler(ListView.ColumnHeaderCollection.UIACollectionChangedEvent, value);
					}
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> is synchronized (thread safe).</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x0600239B RID: 9115 RVA: 0x00087070 File Offset: 0x00085270
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x0600239C RID: 9116 RVA: 0x00087074 File Offset: 0x00085274
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x0600239D RID: 9117 RVA: 0x00087078 File Offset: 0x00085278
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets or sets the column header at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ColumnHeader" /> that represents the column header located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x0600239E RID: 9118 RVA: 0x00087088 File Offset: 0x00085288
			// (set) Token: 0x0600239F RID: 9119 RVA: 0x00087094 File Offset: 0x00085294
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Copies the <see cref="T:System.Windows.Forms.ColumnHeader" /> objects in the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing. </param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			// Token: 0x060023A0 RID: 9120 RVA: 0x000870A0 File Offset: 0x000852A0
			void ICollection.CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.ColumnHeader" /> to the <see cref="T:System.Windows.Forms.ListView" />.</summary>
			/// <returns>The zero-based index indicating the location of the object that was added to the collection</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to be added to the <see cref="T:System.Windows.Forms.ListView" />.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.ColumnHeader" />.</exception>
			// Token: 0x060023A1 RID: 9121 RVA: 0x000870B0 File Offset: 0x000852B0
			int IList.Add(object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				return this.Add((ColumnHeader)value);
			}

			/// <summary>Determines whether the specified column header is located in the collection.</summary>
			/// <returns>true if the object is a column header that is contained in the collection; otherwise, false.</returns>
			/// <param name="value">An object that represents the column header to locate in the collection.</param>
			// Token: 0x060023A2 RID: 9122 RVA: 0x000870DC File Offset: 0x000852DC
			bool IList.Contains(object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				return this.Contains((ColumnHeader)value);
			}

			/// <summary>Returns the index, within the collection, of the specified column header.</summary>
			/// <param name="value">An object that represents the column header to locate in the collection.</param>
			// Token: 0x060023A3 RID: 9123 RVA: 0x00087108 File Offset: 0x00085308
			int IList.IndexOf(object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				return this.IndexOf((ColumnHeader)value);
			}

			/// <summary>Inserts an existing column header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to insert into the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x060023A4 RID: 9124 RVA: 0x00087134 File Offset: 0x00085334
			void IList.Insert(int index, object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				this.Insert(index, (ColumnHeader)value);
			}

			/// <summary>Removes the specified column header from the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> that represents the column header to remove from the collection.</param>
			// Token: 0x060023A5 RID: 9125 RVA: 0x0008716C File Offset: 0x0008536C
			void IList.Remove(object value)
			{
				if (!(value is ColumnHeader))
				{
					throw new ArgumentException("Not of type ColumnHeader", "value");
				}
				this.Remove((ColumnHeader)value);
			}

			// Token: 0x060023A6 RID: 9126 RVA: 0x00087198 File Offset: 0x00085398
			internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
			{
				if (this.owner == null)
				{
					return;
				}
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ListView.ColumnHeaderCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler.Invoke(this.owner, args);
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x060023A7 RID: 9127 RVA: 0x000871E0 File Offset: 0x000853E0
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x060023A8 RID: 9128 RVA: 0x000871F0 File Offset: 0x000853F0
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets the column header at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header located at the specified index within the collection.</returns>
			/// <param name="index">The index of the column header to retrieve from the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </exception>
			// Token: 0x170008C3 RID: 2243
			public virtual ColumnHeader this[int index]
			{
				get
				{
					if (index < 0 || index >= this.list.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return (ColumnHeader)this.list[index];
				}
			}

			/// <summary>Gets the column header with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified key.</returns>
			/// <param name="key">The name of the column header to retrieve from the collection.</param>
			// Token: 0x170008C4 RID: 2244
			public virtual ColumnHeader this[string key]
			{
				get
				{
					int num = this.IndexOfKey(key);
					if (num == -1)
					{
						return null;
					}
					return (ColumnHeader)this.list[num];
				}
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ColumnHeader" /> to the collection.</summary>
			/// <returns>The zero-based index into the collection where the item was added.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection. </param>
			// Token: 0x060023AB RID: 9131 RVA: 0x00087268 File Offset: 0x00085468
			public virtual int Add(ColumnHeader value)
			{
				int num = this.list.Add(value);
				this.owner.AddColumn(value, num, true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, value));
				return num;
			}

			/// <summary>Adds a column header to the collection with specified text, width, and alignment settings.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> that was created and added to the collection.</returns>
			/// <param name="text">The text to display in the column header. </param>
			/// <param name="width">The initial width of the column header. </param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. </param>
			// Token: 0x060023AC RID: 9132 RVA: 0x000872A0 File Offset: 0x000854A0
			public virtual ColumnHeader Add(string text, int width, HorizontalAlignment textAlign)
			{
				ColumnHeader columnHeader = new ColumnHeader(this.owner, text, textAlign, width);
				this.Add(columnHeader);
				return columnHeader;
			}

			/// <summary>Creates and adds a column with the specified text to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified text that was added to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </returns>
			/// <param name="text">The text to display in the column header.</param>
			// Token: 0x060023AD RID: 9133 RVA: 0x000872C8 File Offset: 0x000854C8
			public virtual ColumnHeader Add(string text)
			{
				return this.Add(string.Empty, text);
			}

			/// <summary>Creates and adds a column with the specified text and width to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified text and width that was added to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</returns>
			/// <param name="text">The text of the <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			/// <param name="width">The width of the <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			// Token: 0x060023AE RID: 9134 RVA: 0x000872D8 File Offset: 0x000854D8
			public virtual ColumnHeader Add(string text, int width)
			{
				return this.Add(string.Empty, text, width);
			}

			/// <summary>Creates and adds a column with the specified text and key to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified key and text that was added to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</returns>
			/// <param name="key">The key of the <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			/// <param name="text">The text of the <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			// Token: 0x060023AF RID: 9135 RVA: 0x000872E8 File Offset: 0x000854E8
			public virtual ColumnHeader Add(string key, string text)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Name = key;
				columnHeader.Text = text;
				this.Add(columnHeader);
				return columnHeader;
			}

			/// <summary>Creates and adds a column with the specified text, key, and width to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the given text, key, and width that was added to the collection.</returns>
			/// <param name="key">The key of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
			// Token: 0x060023B0 RID: 9136 RVA: 0x00087314 File Offset: 0x00085514
			public virtual ColumnHeader Add(string key, string text, int width)
			{
				return this.Add(key, text, width, HorizontalAlignment.Left, -1);
			}

			/// <summary>Creates and adds a column with the specified key, aligned text, width, and image index to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified key, aligned text, width, and image index that has been added to the collection.</returns>
			/// <param name="key">The key of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <param name="imageIndex">The index value of the image to display in the column. </param>
			// Token: 0x060023B1 RID: 9137 RVA: 0x00087324 File Offset: 0x00085524
			public virtual ColumnHeader Add(string key, string text, int width, HorizontalAlignment textAlign, int imageIndex)
			{
				ColumnHeader columnHeader = new ColumnHeader(key, text, width, textAlign);
				columnHeader.ImageIndex = imageIndex;
				this.Add(columnHeader);
				return columnHeader;
			}

			/// <summary>Creates and adds a column with the specified key, aligned text, width, and image key to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified key, aligned text, width, and image key that has been added to the collection.</returns>
			/// <param name="key">The key of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <param name="imageKey">The key value of the image to display in the column header.</param>
			// Token: 0x060023B2 RID: 9138 RVA: 0x00087350 File Offset: 0x00085550
			public virtual ColumnHeader Add(string key, string text, int width, HorizontalAlignment textAlign, string imageKey)
			{
				ColumnHeader columnHeader = new ColumnHeader(key, text, width, textAlign);
				columnHeader.ImageKey = imageKey;
				this.Add(columnHeader);
				return columnHeader;
			}

			/// <summary>Adds an array of column headers to the collection.</summary>
			/// <param name="values">An array of <see cref="T:System.Windows.Forms.ColumnHeader" /> objects to add to the collection. </param>
			// Token: 0x060023B3 RID: 9139 RVA: 0x0008737C File Offset: 0x0008557C
			public virtual void AddRange(ColumnHeader[] values)
			{
				foreach (ColumnHeader columnHeader in values)
				{
					int num = this.list.Add(columnHeader);
					this.owner.AddColumn(columnHeader, num, false);
				}
				this.owner.Redraw(true);
			}

			/// <summary>Removes all column headers from the collection.</summary>
			// Token: 0x060023B4 RID: 9140 RVA: 0x000873CC File Offset: 0x000855CC
			public virtual void Clear()
			{
				foreach (object obj in this.list)
				{
					ColumnHeader columnHeader = (ColumnHeader)obj;
					columnHeader.SetListView(null);
				}
				this.list.Clear();
				this.owner.ReorderColumns(new int[0], true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(3, null));
			}

			/// <summary>Determines whether the specified column header is located in the collection.</summary>
			/// <returns>true if the column header is contained in the collection; otherwise, false.</returns>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to locate in the collection. </param>
			// Token: 0x060023B5 RID: 9141 RVA: 0x00087468 File Offset: 0x00085668
			public bool Contains(ColumnHeader value)
			{
				return this.list.Contains(value);
			}

			/// <summary>Determines if a column with the specified key is contained in the collection.</summary>
			/// <returns>true if a column with the specified name is contained in the collection; otherwise, false. </returns>
			/// <param name="key">The name of the column to search for.</param>
			// Token: 0x060023B6 RID: 9142 RVA: 0x00087478 File Offset: 0x00085678
			public virtual bool ContainsKey(string key)
			{
				return this.IndexOfKey(key) != -1;
			}

			/// <summary>Returns an enumerator to use to iterate through the column header collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the column header collection.</returns>
			// Token: 0x060023B7 RID: 9143 RVA: 0x00087488 File Offset: 0x00085688
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			/// <summary>Returns the index, within the collection, of the specified column header.</summary>
			/// <returns>The zero-based index of the column header's location in the collection. If the column header is not located in the collection, the return value is -1.</returns>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to locate in the collection. </param>
			// Token: 0x060023B8 RID: 9144 RVA: 0x00087498 File Offset: 0x00085698
			public int IndexOf(ColumnHeader value)
			{
				return this.list.IndexOf(value);
			}

			/// <summary>Determines the index for a column with the specified key.</summary>
			/// <returns>The zero-based index for the first occurrence of the column with the specified name, if found; otherwise, -1.</returns>
			/// <param name="key">The name of the column to retrieve the index for.</param>
			// Token: 0x060023B9 RID: 9145 RVA: 0x000874A8 File Offset: 0x000856A8
			public virtual int IndexOfKey(string key)
			{
				if (key == null || key.Length == 0)
				{
					return -1;
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					ColumnHeader columnHeader = (ColumnHeader)this.list[i];
					if (string.Compare(key, columnHeader.Name, true) == 0)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Inserts an existing column header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted. </param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to insert into the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </exception>
			// Token: 0x060023BA RID: 9146 RVA: 0x0008750C File Offset: 0x0008570C
			public void Insert(int index, ColumnHeader value)
			{
				if (index < 0 || index > this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.list.Insert(index, value);
				this.owner.AddColumn(value, index, true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, value));
			}

			/// <summary>Creates a new column header with the specified text, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="text">The text to display in the column header. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </exception>
			// Token: 0x060023BB RID: 9147 RVA: 0x00087564 File Offset: 0x00085764
			public void Insert(int index, string text)
			{
				this.Insert(index, string.Empty, text);
			}

			/// <summary>Creates a new column header with the specified text and initial width, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="text">The text to display in the column header. </param>
			/// <param name="width">The initial width, in pixels, of the column header.</param>
			// Token: 0x060023BC RID: 9148 RVA: 0x00087574 File Offset: 0x00085774
			public void Insert(int index, string text, int width)
			{
				this.Insert(index, string.Empty, text, width);
			}

			/// <summary>Creates a new column header with the specified text and key, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="key">The name of the column header. </param>
			/// <param name="text">The text to display in the column header. </param>
			// Token: 0x060023BD RID: 9149 RVA: 0x00087584 File Offset: 0x00085784
			public void Insert(int index, string key, string text)
			{
				this.Insert(index, new ColumnHeader
				{
					Name = key,
					Text = text
				});
			}

			/// <summary>Creates a new column header with the specified text, key, and width, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="key">The name of the column header. </param>
			/// <param name="text">The text to display in the column header. </param>
			/// <param name="width">The initial width, in pixels, of the column header.</param>
			// Token: 0x060023BE RID: 9150 RVA: 0x000875B0 File Offset: 0x000857B0
			public void Insert(int index, string key, string text, int width)
			{
				ColumnHeader columnHeader = new ColumnHeader(key, text, width, HorizontalAlignment.Left);
				this.Insert(index, columnHeader);
			}

			/// <summary>Creates a new column header with the specified aligned text, key, width, and image index, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="key">The name of the column header. </param>
			/// <param name="text">The text to display in the column header. </param>
			/// <param name="width">The initial width, in pixels, of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <param name="imageIndex">The index of the image to display in the column header.</param>
			// Token: 0x060023BF RID: 9151 RVA: 0x000875D0 File Offset: 0x000857D0
			public void Insert(int index, string key, string text, int width, HorizontalAlignment textAlign, int imageIndex)
			{
				this.Insert(index, new ColumnHeader(key, text, width, textAlign)
				{
					ImageIndex = imageIndex
				});
			}

			/// <summary>Creates a new column header with the specified aligned text, key, width, and image key, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="key">The name of the column header. </param>
			/// <param name="text">The text to display in the column header. </param>
			/// <param name="width">The initial width, in pixels, of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <param name="imageKey">The key of the image to display in the column header.</param>
			// Token: 0x060023C0 RID: 9152 RVA: 0x000875FC File Offset: 0x000857FC
			public void Insert(int index, string key, string text, int width, HorizontalAlignment textAlign, string imageKey)
			{
				this.Insert(index, new ColumnHeader(key, text, width, textAlign)
				{
					ImageKey = imageKey
				});
			}

			/// <summary>Creates a new column header and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted. </param>
			/// <param name="text">The text to display in the column header. </param>
			/// <param name="width">The initial width of the column header. Set to -1 to autosize the column header to the size of the largest subitem text in the column or -2 to autosize the column header to the size of the text of the column header. </param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </exception>
			// Token: 0x060023C1 RID: 9153 RVA: 0x00087628 File Offset: 0x00085828
			public void Insert(int index, string text, int width, HorizontalAlignment textAlign)
			{
				ColumnHeader columnHeader = new ColumnHeader(this.owner, text, textAlign, width);
				this.Insert(index, columnHeader);
			}

			/// <summary>Removes the specified column header from the collection.</summary>
			/// <param name="column">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to remove from the collection. </param>
			// Token: 0x060023C2 RID: 9154 RVA: 0x00087650 File Offset: 0x00085850
			public virtual void Remove(ColumnHeader column)
			{
				if (!this.Contains(column))
				{
					return;
				}
				this.list.Remove(column);
				column.SetListView(null);
				int internalDisplayIndex = column.InternalDisplayIndex;
				int[] array = new int[this.list.Count];
				for (int i = 0; i < array.Length; i++)
				{
					ColumnHeader columnHeader = (ColumnHeader)this.list[i];
					int internalDisplayIndex2 = columnHeader.InternalDisplayIndex;
					if (internalDisplayIndex2 < internalDisplayIndex)
					{
						array[i] = internalDisplayIndex2;
					}
					else
					{
						array[i] = internalDisplayIndex2 - 1;
					}
				}
				column.InternalDisplayIndex = -1;
				this.owner.ReorderColumns(array, true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, column));
			}

			/// <summary>Removes the column with the specified key from the collection.</summary>
			/// <param name="key">The name of the column to remove from the collection.</param>
			// Token: 0x060023C3 RID: 9155 RVA: 0x00087700 File Offset: 0x00085900
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the column header at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the column header to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />. </exception>
			// Token: 0x060023C4 RID: 9156 RVA: 0x00087724 File Offset: 0x00085924
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				ColumnHeader columnHeader = (ColumnHeader)this.list[index];
				this.Remove(columnHeader);
			}

			// Token: 0x04001277 RID: 4727
			internal ArrayList list;

			// Token: 0x04001278 RID: 4728
			private ListView owner;
		}

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.ListView" /> control or assigned to a <see cref="T:System.Windows.Forms.ListViewGroup" />. </summary>
		// Token: 0x02000225 RID: 549
		[ListBindable(false)]
		public class ListViewItemCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> that owns the collection. </param>
			// Token: 0x060023C5 RID: 9157 RVA: 0x00087770 File Offset: 0x00085970
			public ListViewItemCollection(ListView owner)
			{
				this.list = new ArrayList(0);
				this.owner = owner;
			}

			// Token: 0x060023C6 RID: 9158 RVA: 0x000877A0 File Offset: 0x000859A0
			internal ListViewItemCollection(ListView owner, ListViewGroup group)
				: this(owner)
			{
				this.group = group;
				this.is_main_collection = false;
			}

			// Token: 0x060023C7 RID: 9159 RVA: 0x000877B8 File Offset: 0x000859B8
			// Note: this type is marked as 'beforefieldinit'.
			static ListViewItemCollection()
			{
				ListView.ListViewItemCollection.UIACollectionChangedEvent = new object();
			}

			// Token: 0x14000231 RID: 561
			// (add) Token: 0x060023C8 RID: 9160 RVA: 0x000877C4 File Offset: 0x000859C4
			// (remove) Token: 0x060023C9 RID: 9161 RVA: 0x000877E8 File Offset: 0x000859E8
			internal event CollectionChangeEventHandler UIACollectionChanged
			{
				add
				{
					if (this.owner != null)
					{
						this.owner.Events.AddHandler(ListView.ListViewItemCollection.UIACollectionChangedEvent, value);
					}
				}
				remove
				{
					if (this.owner != null)
					{
						this.owner.Events.RemoveHandler(ListView.ListViewItemCollection.UIACollectionChangedEvent, value);
					}
				}
			}

			// Token: 0x14000232 RID: 562
			// (add) Token: 0x060023CA RID: 9162 RVA: 0x0008780C File Offset: 0x00085A0C
			// (remove) Token: 0x060023CB RID: 9163 RVA: 0x00087828 File Offset: 0x00085A28
			internal event ListView.CollectionChangedHandler Changed;

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170008C5 RID: 2245
			// (get) Token: 0x060023CC RID: 9164 RVA: 0x00087844 File Offset: 0x00085A44
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x060023CD RID: 9165 RVA: 0x00087848 File Offset: 0x00085A48
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x060023CE RID: 9166 RVA: 0x0008784C File Offset: 0x00085A4C
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewItem" /> at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x060023CF RID: 9167 RVA: 0x0008785C File Offset: 0x00085A5C
			// (set) Token: 0x060023D0 RID: 9168 RVA: 0x00087868 File Offset: 0x00085A68
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, this[index]));
					if (value is ListViewItem)
					{
						this[index] = (ListViewItem)value;
					}
					else
					{
						this[index] = new ListViewItem(value.ToString());
					}
					this.OnChange();
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, value));
				}
			}

			/// <summary>Adds an existing object to the collection.</summary>
			/// <returns>The zero-based index indicating the location of the object if it was added to the collection; otherwise, -1.</returns>
			/// <param name="item">The object to add to the collection.</param>
			// Token: 0x060023D1 RID: 9169 RVA: 0x000878CC File Offset: 0x00085ACC
			int IList.Add(object item)
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				ListViewItem listViewItem;
				if (item is ListViewItem)
				{
					listViewItem = (ListViewItem)item;
					if (this.list.Contains(listViewItem))
					{
						throw new ArgumentException("An item cannot be added more than once. To add an item again, you need to clone it.", "item");
					}
					if (listViewItem.ListView != null && listViewItem.ListView != this.owner)
					{
						throw new ArgumentException("Cannot add or insert the item '" + listViewItem.Text + "' in more than one place. You must first remove it from its current location or clone it.", "item");
					}
				}
				else
				{
					listViewItem = new ListViewItem(item.ToString());
				}
				listViewItem.Owner = this.owner;
				int num = this.list.Add(listViewItem);
				this.CollectionChanged(true);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, listViewItem));
				return num;
			}

			/// <summary>Determines whether the specified item is in the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			// Token: 0x060023D2 RID: 9170 RVA: 0x000879A8 File Offset: 0x00085BA8
			bool IList.Contains(object item)
			{
				return this.Contains((ListViewItem)item);
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to locate in the collection.</param>
			// Token: 0x060023D3 RID: 9171 RVA: 0x000879B8 File Offset: 0x00085BB8
			int IList.IndexOf(object item)
			{
				return this.IndexOf((ListViewItem)item);
			}

			/// <summary>Inserts an object into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">The object that represents the item to insert.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x060023D4 RID: 9172 RVA: 0x000879C8 File Offset: 0x00085BC8
			void IList.Insert(int index, object item)
			{
				if (item is ListViewItem)
				{
					this.Insert(index, (ListViewItem)item);
				}
				else
				{
					this.Insert(index, item.ToString());
				}
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, this[index]));
			}

			/// <summary>Removes the specified item from the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to remove from the collection.</param>
			// Token: 0x060023D5 RID: 9173 RVA: 0x00087A14 File Offset: 0x00085C14
			void IList.Remove(object item)
			{
				this.Remove((ListViewItem)item);
			}

			// Token: 0x060023D6 RID: 9174 RVA: 0x00087A24 File Offset: 0x00085C24
			internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
			{
				if (this.owner == null)
				{
					return;
				}
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ListView.ListViewItemCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler.Invoke(this.owner, args);
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x060023D7 RID: 9175 RVA: 0x00087A6C File Offset: 0x00085C6C
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner != null && this.owner.VirtualMode)
					{
						return this.owner.VirtualListSize;
					}
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x060023D8 RID: 9176 RVA: 0x00087AAC File Offset: 0x00085CAC
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the item at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x170008CB RID: 2251
			public virtual ListViewItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					if (this.owner != null && this.owner.VirtualMode)
					{
						return this.RetrieveVirtualItemFromOwner(index);
					}
					return (ListViewItem)this.list[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					if (this.owner != null && this.owner.VirtualMode)
					{
						throw new InvalidOperationException();
					}
					if (this.list.Contains(value))
					{
						throw new ArgumentException("An item cannot be added more than once. To add an item again, you need to clone it.", "value");
					}
					if (value.ListView != null && value.ListView != this.owner)
					{
						throw new ArgumentException("Cannot add or insert the item '" + value.Text + "' in more than one place. You must first remove it from its current location or clone it.", "value");
					}
					if (this.is_main_collection)
					{
						value.Owner = this.owner;
					}
					else
					{
						if (value.Group != null)
						{
							value.Group.Items.Remove(value);
						}
						value.SetGroup(this.group);
					}
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, this.list[index]));
					this.list[index] = value;
					this.CollectionChanged(true);
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, value));
				}
			}

			/// <summary>Retrieves the item with the specified key.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> whose <see cref="P:System.Windows.Forms.ListViewItem.Name" /> property matches the specified key.</returns>
			/// <param name="key">The name of the item to retrieve.</param>
			// Token: 0x170008CC RID: 2252
			public virtual ListViewItem this[string key]
			{
				get
				{
					int num = this.IndexOfKey(key);
					if (num == -1)
					{
						return null;
					}
					return this[num];
				}
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem" /> to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was added to the collection.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewItem" /> to add to the collection. </param>
			// Token: 0x060023DC RID: 9180 RVA: 0x00087C60 File Offset: 0x00085E60
			public virtual ListViewItem Add(ListViewItem value)
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				this.AddItem(value);
				if (this.is_main_collection || value.ListView != null)
				{
					this.CollectionChanged(true);
				}
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, value));
				return value;
			}

			/// <summary>Creates an item with the specified text and adds it to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was added to the collection.</returns>
			/// <param name="text">The text to display for the item. </param>
			// Token: 0x060023DD RID: 9181 RVA: 0x00087CC0 File Offset: 0x00085EC0
			public virtual ListViewItem Add(string text)
			{
				ListViewItem listViewItem = new ListViewItem(text);
				return this.Add(listViewItem);
			}

			/// <summary>Creates an item with the specified text and image and adds it to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was added to the collection.</returns>
			/// <param name="text">The text of the item. </param>
			/// <param name="imageIndex">The index of the image to display for the item. </param>
			// Token: 0x060023DE RID: 9182 RVA: 0x00087CDC File Offset: 0x00085EDC
			public virtual ListViewItem Add(string text, int imageIndex)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageIndex);
				return this.Add(listViewItem);
			}

			/// <summary>Creates an item with the specified text and image and adds it to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageKey">The key of the image to display for the item.</param>
			// Token: 0x060023DF RID: 9183 RVA: 0x00087CF8 File Offset: 0x00085EF8
			public virtual ListViewItem Add(string text, string imageKey)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageKey);
				return this.Add(listViewItem);
			}

			/// <summary>Creates an item with the specified key, text, and image and adds an item to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <param name="key">The name of the item.</param>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageIndex">The index of the image to display for the item.</param>
			/// <exception cref="T:System.InvalidOperationException">The containing <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x060023E0 RID: 9184 RVA: 0x00087D14 File Offset: 0x00085F14
			public virtual ListViewItem Add(string key, string text, int imageIndex)
			{
				return this.Add(new ListViewItem(text, imageIndex)
				{
					Name = key
				});
			}

			/// <summary>Creates an item with the specified key, text, and image, and adds it to the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <param name="key">The name of the item.</param>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageKey">The key of the image to display for the item.</param>
			// Token: 0x060023E1 RID: 9185 RVA: 0x00087D38 File Offset: 0x00085F38
			public virtual ListViewItem Add(string key, string text, string imageKey)
			{
				return this.Add(new ListViewItem(text, imageKey)
				{
					Name = key
				});
			}

			/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ListViewItem" /> objects to the collection.</summary>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.ListViewItem" /> objects to add to the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="items" /> is null.</exception>
			// Token: 0x060023E2 RID: 9186 RVA: 0x00087D5C File Offset: 0x00085F5C
			public void AddRange(ListViewItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("Argument cannot be null!", "items");
				}
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				this.owner.BeginUpdate();
				foreach (ListViewItem listViewItem in items)
				{
					this.AddItem(listViewItem);
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, listViewItem));
				}
				this.owner.EndUpdate();
				this.CollectionChanged(true);
			}

			/// <summary>Adds a collection of items to the collection.</summary>
			/// <param name="items">The <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="items" /> is null.</exception>
			/// <exception cref="T:System.InvalidOperationException">The containing <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x060023E3 RID: 9187 RVA: 0x00087DEC File Offset: 0x00085FEC
			public void AddRange(ListView.ListViewItemCollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("Argument cannot be null!", "items");
				}
				ListViewItem[] array = new ListViewItem[items.Count];
				items.CopyTo(array, 0);
				this.AddRange(array);
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x060023E4 RID: 9188 RVA: 0x00087E2C File Offset: 0x0008602C
			public virtual void Clear()
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (this.is_main_collection && this.owner != null)
				{
					this.owner.SetFocusedItem(-1);
					ScrollBar h_scroll = this.owner.h_scroll;
					int num = 0;
					this.owner.v_scroll.Value = num;
					h_scroll.Value = num;
					foreach (object obj in this.owner.groups)
					{
						ListViewGroup listViewGroup = (ListViewGroup)obj;
						listViewGroup.Items.ClearItemsWithSameListView();
					}
					foreach (object obj2 in this.list)
					{
						ListViewItem listViewItem = (ListViewItem)obj2;
						this.owner.item_control.CancelEdit(listViewItem);
						listViewItem.Owner = null;
					}
				}
				else
				{
					foreach (object obj3 in this.list)
					{
						ListViewItem listViewItem2 = (ListViewItem)obj3;
						listViewItem2.SetGroup(null);
					}
				}
				this.list.Clear();
				this.CollectionChanged(false);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(3, null));
			}

			// Token: 0x060023E5 RID: 9189 RVA: 0x00088010 File Offset: 0x00086210
			private void ClearItemsWithSameListView()
			{
				if (this.is_main_collection)
				{
					return;
				}
				for (int i = this.list.Count - 1; i >= 0; i--)
				{
					ListViewItem listViewItem = this.list[i] as ListViewItem;
					if (listViewItem.ListView == this.group.ListView)
					{
						this.list.RemoveAt(i);
						listViewItem.SetGroup(null);
					}
				}
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the item is contained in the collection; otherwise, false.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x060023E6 RID: 9190 RVA: 0x00088084 File Offset: 0x00086284
			public bool Contains(ListViewItem item)
			{
				return this.IndexOf(item) != -1;
			}

			/// <summary>Determines whether the collection contains an item with the specified key.</summary>
			/// <returns>true to indicate the collection contains an item with the specified key; otherwise, false. </returns>
			/// <param name="key">The name of the item to search for.</param>
			// Token: 0x060023E7 RID: 9191 RVA: 0x00088094 File Offset: 0x00086294
			public virtual bool ContainsKey(string key)
			{
				return this.IndexOfKey(key) != -1;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x060023E8 RID: 9192 RVA: 0x000880A4 File Offset: 0x000862A4
			public void CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Searches for items whose name matches the specified key, optionally searching subitems.</summary>
			/// <returns>An array of type <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
			/// <param name="key">The item name to search for.</param>
			/// <param name="searchAllSubItems">true to search subitems; otherwise, false. </param>
			// Token: 0x060023E9 RID: 9193 RVA: 0x000880B4 File Offset: 0x000862B4
			public ListViewItem[] Find(string key, bool searchAllSubItems)
			{
				if (key == null)
				{
					return new ListViewItem[0];
				}
				List<ListViewItem> list = new List<ListViewItem>();
				for (int i = 0; i < this.list.Count; i++)
				{
					ListViewItem listViewItem = (ListViewItem)this.list[i];
					if (string.Compare(key, listViewItem.Name, true) == 0)
					{
						list.Add(listViewItem);
					}
				}
				ListViewItem[] array = new ListViewItem[list.Count];
				list.CopyTo(array);
				return array;
			}

			/// <summary>Returns an enumerator to use to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x060023EA RID: 9194 RVA: 0x00088130 File Offset: 0x00086330
			public IEnumerator GetEnumerator()
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				return new Control.ControlCollection.ControlCollectionEnumerator(this.list);
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item's location in the collection; otherwise, -1 if the item is not located in the collection.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x060023EB RID: 9195 RVA: 0x0008816C File Offset: 0x0008636C
			public int IndexOf(ListViewItem item)
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					for (int i = 0; i < this.Count; i++)
					{
						if (this.RetrieveVirtualItemFromOwner(i) == item)
						{
							return i;
						}
					}
					return -1;
				}
				return this.list.IndexOf(item);
			}

			/// <summary>Retrieves the index of the item with the specified key.</summary>
			/// <returns>The zero-based index of the first occurrence of the item with the specified key, if found; otherwise, -1.</returns>
			/// <param name="key">The name of the item to find in the collection.</param>
			// Token: 0x060023EC RID: 9196 RVA: 0x000881C8 File Offset: 0x000863C8
			public virtual int IndexOfKey(string key)
			{
				if (key == null || key.Length == 0)
				{
					return -1;
				}
				for (int i = 0; i < this.Count; i++)
				{
					ListViewItem listViewItem = this[i];
					if (string.Compare(key, listViewItem.Name, true) == 0)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Inserts an existing <see cref="T:System.Windows.Forms.ListViewItem" /> into the collection at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was inserted into the collection.</returns>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to insert. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x060023ED RID: 9197 RVA: 0x0008821C File Offset: 0x0008641C
			public ListViewItem Insert(int index, ListViewItem item)
			{
				if (index < 0 || index > this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				if (this.list.Contains(item))
				{
					throw new ArgumentException("An item cannot be added more than once. To add an item again, you need to clone it.", "item");
				}
				if (item.ListView != null && item.ListView != this.owner)
				{
					throw new ArgumentException("Cannot add or insert the item '" + item.Text + "' in more than one place. You must first remove it from its current location or clone it.", "item");
				}
				if (this.is_main_collection)
				{
					item.Owner = this.owner;
				}
				else
				{
					if (item.Group != null)
					{
						item.Group.Items.Remove(item);
					}
					item.SetGroup(this.group);
				}
				this.list.Insert(index, item);
				if (this.is_main_collection || item.ListView != null)
				{
					this.CollectionChanged(true);
				}
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, item));
				return item;
			}

			/// <summary>Creates a new item and inserts it into the collection at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was inserted into the collection.</returns>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="text">The text to display for the item. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x060023EE RID: 9198 RVA: 0x00088348 File Offset: 0x00086548
			public ListViewItem Insert(int index, string text)
			{
				return this.Insert(index, new ListViewItem(text));
			}

			/// <summary>Creates a new item with the specified image index and inserts it into the collection at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was inserted into the collection.</returns>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="text">The text to display for the item. </param>
			/// <param name="imageIndex">The index of the image to display for the item. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x060023EF RID: 9199 RVA: 0x00088358 File Offset: 0x00086558
			public ListViewItem Insert(int index, string text, int imageIndex)
			{
				return this.Insert(index, new ListViewItem(text, imageIndex));
			}

			/// <summary>Creates a new item with the specified text and image and inserts it in the collection at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="text">The text of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
			/// <param name="imageKey">The key of the image to display for the item.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x060023F0 RID: 9200 RVA: 0x00088368 File Offset: 0x00086568
			public ListViewItem Insert(int index, string text, string imageKey)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageKey);
				return this.Insert(index, listViewItem);
			}

			/// <summary>Creates a new item with the specified key, text, and image, and inserts it in the collection at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <param name="index">The zero-based index location where the item is inserted</param>
			/// <param name="key">The <see cref="P:System.Windows.Forms.ListViewItem.Name" /> of the item.</param>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageIndex">The index of the image to display for the item.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x060023F1 RID: 9201 RVA: 0x00088388 File Offset: 0x00086588
			public virtual ListViewItem Insert(int index, string key, string text, int imageIndex)
			{
				return this.Insert(index, new ListViewItem(text, imageIndex)
				{
					Name = key
				});
			}

			/// <summary>Creates a new item with the specified key, text, and image, and adds it to the collection at the specified index.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="key">The <see cref="P:System.Windows.Forms.ListViewItem.Name" /> of the item. </param>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageKey">The key of the image to display for the item.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x060023F2 RID: 9202 RVA: 0x000883B0 File Offset: 0x000865B0
			public virtual ListViewItem Insert(int index, string key, string text, string imageKey)
			{
				return this.Insert(index, new ListViewItem(text, imageKey)
				{
					Name = key
				});
			}

			/// <summary>Removes the specified item from the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to remove from the collection. </param>
			/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.ListViewItem" /> assigned to the <paramref name="item" /> parameter is null. </exception>
			// Token: 0x060023F3 RID: 9203 RVA: 0x000883D8 File Offset: 0x000865D8
			public virtual void Remove(ListViewItem item)
			{
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				int num = this.list.IndexOf(item);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the item at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />. </exception>
			// Token: 0x060023F4 RID: 9204 RVA: 0x00088424 File Offset: 0x00086624
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.owner != null && this.owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				ListViewItem listViewItem = (ListViewItem)this.list[index];
				bool flag = false;
				if (this.is_main_collection && this.owner != null)
				{
					int displayIndex = listViewItem.DisplayIndex;
					if (listViewItem.Focused && displayIndex + 1 == this.Count)
					{
						this.owner.SetFocusedItem((displayIndex != 0) ? (displayIndex - 1) : (-1));
					}
					flag = this.owner.SelectedIndices.Contains(index);
					this.owner.item_control.CancelEdit(listViewItem);
				}
				this.list.RemoveAt(index);
				if (this.is_main_collection)
				{
					listViewItem.Owner = null;
					if (listViewItem.Group != null)
					{
						listViewItem.Group.Items.Remove(listViewItem);
					}
				}
				else
				{
					listViewItem.SetGroup(null);
				}
				this.CollectionChanged(false);
				if (flag && this.owner != null)
				{
					this.owner.OnSelectedIndexChanged(EventArgs.Empty);
				}
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, listViewItem));
			}

			/// <summary>Removes the item with the specified key from the collection.</summary>
			/// <param name="key">The name of the item to remove from the collection.</param>
			// Token: 0x060023F5 RID: 9205 RVA: 0x00088574 File Offset: 0x00086774
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00088598 File Offset: 0x00086798
			// (set) Token: 0x060023F7 RID: 9207 RVA: 0x000885A0 File Offset: 0x000867A0
			internal ListView Owner
			{
				get
				{
					return this.owner;
				}
				set
				{
					this.owner = value;
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000885AC File Offset: 0x000867AC
			// (set) Token: 0x060023F9 RID: 9209 RVA: 0x000885B4 File Offset: 0x000867B4
			internal ListViewGroup Group
			{
				get
				{
					return this.group;
				}
				set
				{
					this.group = value;
				}
			}

			// Token: 0x060023FA RID: 9210 RVA: 0x000885C0 File Offset: 0x000867C0
			private void AddItem(ListViewItem value)
			{
				if (this.list.Contains(value))
				{
					throw new ArgumentException("An item cannot be added more than once. To add an item again, you need to clone it.", "value");
				}
				if (value.ListView != null && value.ListView != this.owner)
				{
					throw new ArgumentException("Cannot add or insert the item '" + value.Text + "' in more than one place. You must first remove it from its current location or clone it.", "value");
				}
				if (this.is_main_collection)
				{
					value.Owner = this.owner;
				}
				else
				{
					if (value.Group != null)
					{
						value.Group.Items.Remove(value);
					}
					value.SetGroup(this.group);
				}
				this.list.Add(value);
			}

			// Token: 0x060023FB RID: 9211 RVA: 0x0008867C File Offset: 0x0008687C
			private void CollectionChanged(bool sort)
			{
				if (this.owner != null)
				{
					if (sort)
					{
						this.owner.Sort(false);
					}
					this.OnChange();
					this.owner.Redraw(true);
				}
			}

			// Token: 0x060023FC RID: 9212 RVA: 0x000886B0 File Offset: 0x000868B0
			private ListViewItem RetrieveVirtualItemFromOwner(int displayIndex)
			{
				RetrieveVirtualItemEventArgs retrieveVirtualItemEventArgs = new RetrieveVirtualItemEventArgs(displayIndex);
				this.owner.OnRetrieveVirtualItem(retrieveVirtualItemEventArgs);
				ListViewItem item = retrieveVirtualItemEventArgs.Item;
				item.Owner = this.owner;
				item.DisplayIndex = displayIndex;
				return item;
			}

			// Token: 0x060023FD RID: 9213 RVA: 0x000886EC File Offset: 0x000868EC
			internal void Sort(IComparer comparer)
			{
				this.list.Sort(comparer);
				this.OnChange();
			}

			// Token: 0x060023FE RID: 9214 RVA: 0x00088700 File Offset: 0x00086900
			internal void OnChange()
			{
				if (this.Changed != null)
				{
					this.Changed();
				}
			}

			// Token: 0x0400127A RID: 4730
			private readonly ArrayList list;

			// Token: 0x0400127B RID: 4731
			private ListView owner;

			// Token: 0x0400127C RID: 4732
			private ListViewGroup group;

			// Token: 0x0400127E RID: 4734
			private bool is_main_collection = true;
		}

		/// <summary>Represents the collection that contains the indexes to the selected items in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x02000226 RID: 550
		[ListBindable(false)]
		public class SelectedIndexCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection. </param>
			// Token: 0x060023FF RID: 9215 RVA: 0x00088718 File Offset: 0x00086918
			public SelectedIndexCollection(ListView owner)
			{
				this.owner = owner;
				owner.Items.Changed += this.ItemsCollection_Changed;
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170008CF RID: 2255
			// (get) Token: 0x06002400 RID: 9216 RVA: 0x0008874C File Offset: 0x0008694C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170008D0 RID: 2256
			// (get) Token: 0x06002401 RID: 9217 RVA: 0x00088750 File Offset: 0x00086950
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x06002402 RID: 9218 RVA: 0x00088754 File Offset: 0x00086954
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets an object in the collection.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x06002403 RID: 9219 RVA: 0x00088758 File Offset: 0x00086958
			// (set) Token: 0x06002404 RID: 9220 RVA: 0x00088768 File Offset: 0x00086968
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <returns>The location of the added item.</returns>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002405 RID: 9221 RVA: 0x00088774 File Offset: 0x00086974
			int IList.Add(object value)
			{
				throw new NotSupportedException("Add operation is not supported.");
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002406 RID: 9222 RVA: 0x00088780 File Offset: 0x00086980
			void IList.Clear()
			{
				this.Clear();
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, false.</returns>
			/// <param name="selectedIndex">The index to locate in the collection.</param>
			// Token: 0x06002407 RID: 9223 RVA: 0x00088788 File Offset: 0x00086988
			bool IList.Contains(object selectedIndex)
			{
				return selectedIndex is int && this.Contains((int)selectedIndex);
			}

			/// <summary>Returns the index in the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />. The <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> contains the indexes of selected items in the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection.</param>
			// Token: 0x06002408 RID: 9224 RVA: 0x000887A4 File Offset: 0x000869A4
			int IList.IndexOf(object selectedIndex)
			{
				if (!(selectedIndex is int))
				{
					return -1;
				}
				return this.IndexOf((int)selectedIndex);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The item to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <param name="value"></param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002409 RID: 9225 RVA: 0x000887C0 File Offset: 0x000869C0
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException("Insert operation is not supported.");
			}

			/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
			/// <param name="value">The object to remove from the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x0600240A RID: 9226 RVA: 0x000887CC File Offset: 0x000869CC
			void IList.Remove(object value)
			{
				throw new NotSupportedException("Remove operation is not supported.");
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x0600240B RID: 9227 RVA: 0x000887D8 File Offset: 0x000869D8
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException("RemoveAt operation is not supported.");
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x0600240C RID: 9228 RVA: 0x000887E4 File Offset: 0x000869E4
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (!this.owner.IsHandleCreated)
					{
						return 0;
					}
					return this.List.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x0600240D RID: 9229 RVA: 0x00088804 File Offset: 0x00086A04
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets the index value at the specified index within the collection.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.SelectedIndexCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />. </exception>
			// Token: 0x170008D5 RID: 2261
			public int this[int index]
			{
				get
				{
					if (!this.owner.IsHandleCreated || index < 0 || index >= this.List.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return (int)this.List[index];
				}
			}

			/// <summary>Adds the item at the specified index in the <see cref="P:System.Windows.Forms.ListView.Items" /> array to the collection.</summary>
			/// <returns>The number of items in the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</returns>
			/// <param name="itemIndex">The index of the item in the <see cref="P:System.Windows.Forms.ListView.Items" /> collection to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than 0 or greater than or equal to the number of items in the owner <see cref="T:System.Windows.Forms.ListView" />.-or-The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode, and the specified index is less than 0 or greater than or equal to the value of <see cref="P:System.Windows.Forms.ListView.VirtualListSize" />.</exception>
			// Token: 0x0600240F RID: 9231 RVA: 0x0008885C File Offset: 0x00086A5C
			public int Add(int itemIndex)
			{
				if (itemIndex < 0 || itemIndex >= this.owner.Items.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.owner.virtual_mode && !this.owner.IsHandleCreated)
				{
					return -1;
				}
				this.owner.Items[itemIndex].Selected = true;
				if (!this.owner.IsHandleCreated)
				{
					return 0;
				}
				return this.List.Count;
			}

			/// <summary>Clears the items in the collection.</summary>
			// Token: 0x06002410 RID: 9232 RVA: 0x000888E8 File Offset: 0x00086AE8
			public void Clear()
			{
				if (!this.owner.IsHandleCreated)
				{
					return;
				}
				int[] array = (int[])this.List.ToArray(typeof(int));
				foreach (int num in array)
				{
					this.owner.Items[num].Selected = false;
				}
			}

			/// <summary>Determines whether the specified index is located in the collection.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, false.</returns>
			/// <param name="selectedIndex">The index to locate in the collection. </param>
			// Token: 0x06002411 RID: 9233 RVA: 0x00088954 File Offset: 0x00086B54
			public bool Contains(int selectedIndex)
			{
				return this.IndexOf(selectedIndex) != -1;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x06002412 RID: 9234 RVA: 0x00088964 File Offset: 0x00086B64
			public void CopyTo(Array dest, int index)
			{
				this.List.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the selected index collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the selected index collection.</returns>
			// Token: 0x06002413 RID: 9235 RVA: 0x00088974 File Offset: 0x00086B74
			public IEnumerator GetEnumerator()
			{
				return this.List.GetEnumerator();
			}

			/// <summary>Returns the index within the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> of the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located within the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />, or -1 if the index is not located in the collection.</returns>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection. </param>
			// Token: 0x06002414 RID: 9236 RVA: 0x00088984 File Offset: 0x00086B84
			public int IndexOf(int selectedIndex)
			{
				if (!this.owner.IsHandleCreated)
				{
					return -1;
				}
				return this.List.IndexOf(selectedIndex);
			}

			/// <summary>Removes the item at the specified index in the <see cref="P:System.Windows.Forms.ListView.Items" /> collection from the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</summary>
			/// <param name="itemIndex">The index of the item in the <see cref="P:System.Windows.Forms.ListView.Items" /> collection to remove from the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than 0 or greater than or equal to the number of items in the owner <see cref="T:System.Windows.Forms.ListView" />.-or-The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode, and the specified index is less than 0 or greater than or equal to the value of <see cref="P:System.Windows.Forms.ListView.VirtualListSize" />.</exception>
			// Token: 0x06002415 RID: 9237 RVA: 0x000889AC File Offset: 0x00086BAC
			public void Remove(int itemIndex)
			{
				if (itemIndex < 0 || itemIndex >= this.owner.Items.Count)
				{
					throw new ArgumentOutOfRangeException("itemIndex");
				}
				this.owner.Items[itemIndex].Selected = false;
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x06002416 RID: 9238 RVA: 0x000889F8 File Offset: 0x00086BF8
			internal ArrayList List
			{
				get
				{
					if (this.list == null)
					{
						this.list = new ArrayList();
						if (!this.owner.VirtualMode)
						{
							for (int i = 0; i < this.owner.Items.Count; i++)
							{
								if (this.owner.Items[i].Selected)
								{
									this.list.Add(i);
								}
							}
						}
					}
					return this.list;
				}
			}

			// Token: 0x06002417 RID: 9239 RVA: 0x00088A80 File Offset: 0x00086C80
			internal void Reset()
			{
				this.list = null;
			}

			// Token: 0x06002418 RID: 9240 RVA: 0x00088A8C File Offset: 0x00086C8C
			private void ItemsCollection_Changed()
			{
				this.Reset();
			}

			// Token: 0x06002419 RID: 9241 RVA: 0x00088A94 File Offset: 0x00086C94
			internal void RemoveIndex(int index)
			{
				int num = this.List.BinarySearch(index);
				if (num != -1)
				{
					this.List.RemoveAt(num);
				}
			}

			// Token: 0x0600241A RID: 9242 RVA: 0x00088AC8 File Offset: 0x00086CC8
			internal void InsertIndex(int index)
			{
				int i = 0;
				int num = this.List.Count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = (int)this.List[num2];
					if (num3 == index)
					{
						return;
					}
					if (num3 > index)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				this.List.Insert(i, index);
			}

			// Token: 0x04001280 RID: 4736
			private readonly ListView owner;

			// Token: 0x04001281 RID: 4737
			private ArrayList list;
		}

		/// <summary>Represents the collection of selected items in a list view control.</summary>
		// Token: 0x02000227 RID: 551
		[ListBindable(false)]
		public class SelectedListViewItemCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection. </param>
			// Token: 0x0600241B RID: 9243 RVA: 0x00088B38 File Offset: 0x00086D38
			public SelectedListViewItemCollection(ListView owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x0600241C RID: 9244 RVA: 0x00088B48 File Offset: 0x00086D48
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x0600241D RID: 9245 RVA: 0x00088B4C File Offset: 0x00086D4C
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x0600241E RID: 9246 RVA: 0x00088B50 File Offset: 0x00086D50
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets an an object from the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x0600241F RID: 9247 RVA: 0x00088B54 File Offset: 0x00086D54
			// (set) Token: 0x06002420 RID: 9248 RVA: 0x00088B60 File Offset: 0x00086D60
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException("SetItem operation is not supported.");
				}
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <returns>The location of the added item.</returns>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002421 RID: 9249 RVA: 0x00088B6C File Offset: 0x00086D6C
			int IList.Add(object value)
			{
				throw new NotSupportedException("Add operation is not supported.");
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			// Token: 0x06002422 RID: 9250 RVA: 0x00088B78 File Offset: 0x00086D78
			bool IList.Contains(object item)
			{
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			/// <summary>Returns the index, within the collection, of the specified item.</summary>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1</returns>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			// Token: 0x06002423 RID: 9251 RVA: 0x00088B94 File Offset: 0x00086D94
			int IList.IndexOf(object item)
			{
				if (!(item is ListViewItem))
				{
					return -1;
				}
				return this.IndexOf((ListViewItem)item);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to be inserted.</param>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002424 RID: 9252 RVA: 0x00088BB0 File Offset: 0x00086DB0
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException("Insert operation is not supported.");
			}

			/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
			/// <param name="value">The object to remove from the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002425 RID: 9253 RVA: 0x00088BBC File Offset: 0x00086DBC
			void IList.Remove(object value)
			{
				throw new NotSupportedException("Remove operation is not supported.");
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002426 RID: 9254 RVA: 0x00088BC8 File Offset: 0x00086DC8
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException("RemoveAt operation is not supported.");
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x06002427 RID: 9255 RVA: 0x00088BD4 File Offset: 0x00086DD4
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.SelectedIndices.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x170008DC RID: 2268
			// (get) Token: 0x06002428 RID: 9256 RVA: 0x00088BE8 File Offset: 0x00086DE8
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the item at the specified index within the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />. </exception>
			// Token: 0x170008DD RID: 2269
			public ListViewItem this[int index]
			{
				get
				{
					if (!this.owner.IsHandleCreated || index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					int num = this.owner.SelectedIndices[index];
					return this.owner.Items[num];
				}
			}

			/// <summary>Gets an item with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> with the specified key.</returns>
			/// <param name="key">The name of the item to retrieve from the collection.</param>
			// Token: 0x170008DE RID: 2270
			public virtual ListViewItem this[string key]
			{
				get
				{
					int num = this.IndexOfKey(key);
					if (num == -1)
					{
						return null;
					}
					return this[num];
				}
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x0600242B RID: 9259 RVA: 0x00088C74 File Offset: 0x00086E74
			public void Clear()
			{
				this.owner.SelectedIndices.Clear();
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x0600242C RID: 9260 RVA: 0x00088C88 File Offset: 0x00086E88
			public bool Contains(ListViewItem item)
			{
				return this.IndexOf(item) != -1;
			}

			/// <summary>Determines whether an item with the specified key is contained in the collection.</summary>
			/// <returns>true to indicate the specified item is contained in the collection; otherwise, false. </returns>
			/// <param name="key">The name of the item to find in the collection.</param>
			// Token: 0x0600242D RID: 9261 RVA: 0x00088C98 File Offset: 0x00086E98
			public virtual bool ContainsKey(string key)
			{
				return this.IndexOfKey(key) != -1;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x0600242E RID: 9262 RVA: 0x00088CA8 File Offset: 0x00086EA8
			public void CopyTo(Array dest, int index)
			{
				if (!this.owner.IsHandleCreated)
				{
					return;
				}
				if (index > this.Count)
				{
					throw new ArgumentException("index");
				}
				for (int i = 0; i < this.Count; i++)
				{
					dest.SetValue(this[i], index++);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the selected item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the collection of selected items.</returns>
			// Token: 0x0600242F RID: 9263 RVA: 0x00088D08 File Offset: 0x00086F08
			public IEnumerator GetEnumerator()
			{
				if (!this.owner.IsHandleCreated)
				{
					return new ListViewItem[0].GetEnumerator();
				}
				ListViewItem[] array = new ListViewItem[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = this[i];
				}
				return array.GetEnumerator();
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item in the collection. If the item is not located in the collection, the return value is negative one (-1).</returns>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection. </param>
			// Token: 0x06002430 RID: 9264 RVA: 0x00088D64 File Offset: 0x00086F64
			public int IndexOf(ListViewItem item)
			{
				if (!this.owner.IsHandleCreated)
				{
					return -1;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == item)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index of the first occurrence of the item with the specified key.</summary>
			/// <returns>The zero-based index of the first item with the specified key.</returns>
			/// <param name="key">The name of the item to find in the collection.</param>
			// Token: 0x06002431 RID: 9265 RVA: 0x00088DAC File Offset: 0x00086FAC
			public virtual int IndexOfKey(string key)
			{
				if (!this.owner.IsHandleCreated || key == null || key.Length == 0)
				{
					return -1;
				}
				for (int i = 0; i < this.Count; i++)
				{
					ListViewItem listViewItem = this[i];
					if (string.Compare(listViewItem.Name, key, true) == 0)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x04001282 RID: 4738
			private readonly ListView owner;
		}

		// Token: 0x02000228 RID: 552
		private struct ItemMatrixLocation
		{
			// Token: 0x06002432 RID: 9266 RVA: 0x00088E10 File Offset: 0x00087010
			public ItemMatrixLocation(int row, int col)
			{
				this.row = row;
				this.col = col;
			}

			// Token: 0x170008DF RID: 2271
			// (get) Token: 0x06002433 RID: 9267 RVA: 0x00088E20 File Offset: 0x00087020
			// (set) Token: 0x06002434 RID: 9268 RVA: 0x00088E28 File Offset: 0x00087028
			public int Col
			{
				get
				{
					return this.col;
				}
				set
				{
					this.col = value;
				}
			}

			// Token: 0x170008E0 RID: 2272
			// (get) Token: 0x06002435 RID: 9269 RVA: 0x00088E34 File Offset: 0x00087034
			// (set) Token: 0x06002436 RID: 9270 RVA: 0x00088E3C File Offset: 0x0008703C
			public int Row
			{
				get
				{
					return this.row;
				}
				set
				{
					this.row = value;
				}
			}

			// Token: 0x04001283 RID: 4739
			private int row;

			// Token: 0x04001284 RID: 4740
			private int col;
		}

		// Token: 0x02000637 RID: 1591
		// (Invoke) Token: 0x0600508E RID: 20622
		internal delegate void CollectionChangedHandler();
	}
}
