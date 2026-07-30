using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the abstract base class that manages events and layout for all the elements that a <see cref="T:System.Windows.Forms.ToolStrip" /> or <see cref="T:System.Windows.Forms.ToolStripDropDown" /> can contain.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000354 RID: 852
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	[Designer("System.Windows.Forms.Design.ToolStripItemDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxItem(false)]
	public abstract class ToolStripItem : Component, IDisposable, IComponent, IDropTarget
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class.</summary>
		// Token: 0x06003CE3 RID: 15587 RVA: 0x000F49E8 File Offset: 0x000F2BE8
		protected ToolStripItem()
			: this(string.Empty, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class with the specified name, image, and event handler.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the name of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x06003CE4 RID: 15588 RVA: 0x000F49FC File Offset: 0x000F2BFC
		protected ToolStripItem(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class with the specified display text, image, event handler, and name. </summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The Image to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x06003CE5 RID: 15589 RVA: 0x000F4A0C File Offset: 0x000F2C0C
		protected ToolStripItem(string text, Image image, EventHandler onClick, string name)
		{
			this.alignment = ToolStripItemAlignment.Left;
			this.anchor = AnchorStyles.Top | AnchorStyles.Left;
			this.auto_size = true;
			this.auto_tool_tip = this.DefaultAutoToolTip;
			this.available = true;
			this.back_color = Color.Empty;
			this.background_image_layout = ImageLayout.Tile;
			this.can_select = true;
			this.display_style = this.DefaultDisplayStyle;
			this.dock = DockStyle.None;
			this.enabled = true;
			this.fore_color = Color.Empty;
			this.image = image;
			this.image_align = 32;
			this.image_index = -1;
			this.image_key = string.Empty;
			this.image_scaling = ToolStripItemImageScaling.SizeToFit;
			this.image_transparent_color = Color.Empty;
			this.margin = this.DefaultMargin;
			this.merge_action = MergeAction.Append;
			this.merge_index = -1;
			this.name = name;
			this.overflow = ToolStripItemOverflow.AsNeeded;
			this.padding = this.DefaultPadding;
			this.placement = ToolStripItemPlacement.None;
			this.right_to_left = RightToLeft.Inherit;
			this.bounds.Size = this.DefaultSize;
			this.text = text;
			this.text_align = 32;
			this.text_direction = this.DefaultTextDirection;
			this.text_image_relation = TextImageRelation.ImageBeforeText;
			this.visible = true;
			this.Click += onClick;
			this.OnLayout(new LayoutEventArgs(null, string.Empty));
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000F4B50 File Offset: 0x000F2D50
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripItem()
		{
			ToolStripItem.AvailableChangedEvent = new object();
			ToolStripItem.BackColorChangedEvent = new object();
			ToolStripItem.ClickEvent = new object();
			ToolStripItem.DisplayStyleChangedEvent = new object();
			ToolStripItem.DoubleClickEvent = new object();
			ToolStripItem.DragDropEvent = new object();
			ToolStripItem.DragEnterEvent = new object();
			ToolStripItem.DragLeaveEvent = new object();
			ToolStripItem.DragOverEvent = new object();
			ToolStripItem.EnabledChangedEvent = new object();
			ToolStripItem.ForeColorChangedEvent = new object();
			ToolStripItem.GiveFeedbackEvent = new object();
			ToolStripItem.LocationChangedEvent = new object();
			ToolStripItem.MouseDownEvent = new object();
			ToolStripItem.MouseEnterEvent = new object();
			ToolStripItem.MouseHoverEvent = new object();
			ToolStripItem.MouseLeaveEvent = new object();
			ToolStripItem.MouseMoveEvent = new object();
			ToolStripItem.MouseUpEvent = new object();
			ToolStripItem.OwnerChangedEvent = new object();
			ToolStripItem.PaintEvent = new object();
			ToolStripItem.QueryAccessibilityHelpEvent = new object();
			ToolStripItem.QueryContinueDragEvent = new object();
			ToolStripItem.RightToLeftChangedEvent = new object();
			ToolStripItem.TextChangedEvent = new object();
			ToolStripItem.VisibleChangedEvent = new object();
			ToolStripItem.UIASelectionChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Available" /> property changes.</summary>
		// Token: 0x140003A8 RID: 936
		// (add) Token: 0x06003CE7 RID: 15591 RVA: 0x000F4C6C File Offset: 0x000F2E6C
		// (remove) Token: 0x06003CE8 RID: 15592 RVA: 0x000F4C80 File Offset: 0x000F2E80
		[Browsable(false)]
		public event EventHandler AvailableChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.AvailableChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.AvailableChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.BackColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003A9 RID: 937
		// (add) Token: 0x06003CE9 RID: 15593 RVA: 0x000F4C94 File Offset: 0x000F2E94
		// (remove) Token: 0x06003CEA RID: 15594 RVA: 0x000F4CA8 File Offset: 0x000F2EA8
		public event EventHandler BackColorChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.BackColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.BackColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003AA RID: 938
		// (add) Token: 0x06003CEB RID: 15595 RVA: 0x000F4CBC File Offset: 0x000F2EBC
		// (remove) Token: 0x06003CEC RID: 15596 RVA: 0x000F4CD0 File Offset: 0x000F2ED0
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.DisplayStyle" /> has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003AB RID: 939
		// (add) Token: 0x06003CED RID: 15597 RVA: 0x000F4CE4 File Offset: 0x000F2EE4
		// (remove) Token: 0x06003CEE RID: 15598 RVA: 0x000F4CF8 File Offset: 0x000F2EF8
		public event EventHandler DisplayStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.DisplayStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.DisplayStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the item is double-clicked with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003AC RID: 940
		// (add) Token: 0x06003CEF RID: 15599 RVA: 0x000F4D0C File Offset: 0x000F2F0C
		// (remove) Token: 0x06003CF0 RID: 15600 RVA: 0x000F4D20 File Offset: 0x000F2F20
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.DoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.DoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the user drags an item and the user releases the mouse button, indicating that the item should be dropped into this item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003AD RID: 941
		// (add) Token: 0x06003CF1 RID: 15601 RVA: 0x000F4D34 File Offset: 0x000F2F34
		// (remove) Token: 0x06003CF2 RID: 15602 RVA: 0x000F4D48 File Offset: 0x000F2F48
		[EditorBrowsable(2)]
		[Browsable(false)]
		[MonoTODO("Event never raised")]
		public event DragEventHandler DragDrop
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.DragDropEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.DragDropEvent, value);
			}
		}

		/// <summary>Occurs when the user drags an item into the client area of this item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003AE RID: 942
		// (add) Token: 0x06003CF3 RID: 15603 RVA: 0x000F4D5C File Offset: 0x000F2F5C
		// (remove) Token: 0x06003CF4 RID: 15604 RVA: 0x000F4D70 File Offset: 0x000F2F70
		[Browsable(false)]
		[MonoTODO("Event never raised")]
		[EditorBrowsable(2)]
		public event DragEventHandler DragEnter
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.DragEnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.DragEnterEvent, value);
			}
		}

		/// <summary>Occurs when the user drags an item and the mouse pointer is no longer over the client area of this item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003AF RID: 943
		// (add) Token: 0x06003CF5 RID: 15605 RVA: 0x000F4D84 File Offset: 0x000F2F84
		// (remove) Token: 0x06003CF6 RID: 15606 RVA: 0x000F4D98 File Offset: 0x000F2F98
		[EditorBrowsable(2)]
		[MonoTODO("Event never raised")]
		[Browsable(false)]
		public event EventHandler DragLeave
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.DragLeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.DragLeaveEvent, value);
			}
		}

		/// <summary>Occurs when the user drags an item over the client area of this item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B0 RID: 944
		// (add) Token: 0x06003CF7 RID: 15607 RVA: 0x000F4DAC File Offset: 0x000F2FAC
		// (remove) Token: 0x06003CF8 RID: 15608 RVA: 0x000F4DC0 File Offset: 0x000F2FC0
		[EditorBrowsable(2)]
		[MonoTODO("Event never raised")]
		[Browsable(false)]
		public event DragEventHandler DragOver
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.DragOverEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.DragOverEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property value has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B1 RID: 945
		// (add) Token: 0x06003CF9 RID: 15609 RVA: 0x000F4DD4 File Offset: 0x000F2FD4
		// (remove) Token: 0x06003CFA RID: 15610 RVA: 0x000F4DE8 File Offset: 0x000F2FE8
		public event EventHandler EnabledChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EnabledChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EnabledChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.ForeColor" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B2 RID: 946
		// (add) Token: 0x06003CFB RID: 15611 RVA: 0x000F4DFC File Offset: 0x000F2FFC
		// (remove) Token: 0x06003CFC RID: 15612 RVA: 0x000F4E10 File Offset: 0x000F3010
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.ForeColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.ForeColorChangedEvent, value);
			}
		}

		/// <summary>Occurs during a drag operation.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B3 RID: 947
		// (add) Token: 0x06003CFD RID: 15613 RVA: 0x000F4E24 File Offset: 0x000F3024
		// (remove) Token: 0x06003CFE RID: 15614 RVA: 0x000F4E38 File Offset: 0x000F3038
		[EditorBrowsable(2)]
		[MonoTODO("Event never raised")]
		[Browsable(false)]
		public event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.GiveFeedbackEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.GiveFeedbackEvent, value);
			}
		}

		/// <summary>Occurs when the location of a <see cref="T:System.Windows.Forms.ToolStripItem" /> is updated.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B4 RID: 948
		// (add) Token: 0x06003CFF RID: 15615 RVA: 0x000F4E4C File Offset: 0x000F304C
		// (remove) Token: 0x06003D00 RID: 15616 RVA: 0x000F4E60 File Offset: 0x000F3060
		public event EventHandler LocationChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.LocationChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.LocationChangedEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the item and a mouse button is pressed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B5 RID: 949
		// (add) Token: 0x06003D01 RID: 15617 RVA: 0x000F4E74 File Offset: 0x000F3074
		// (remove) Token: 0x06003D02 RID: 15618 RVA: 0x000F4E88 File Offset: 0x000F3088
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.MouseDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.MouseDownEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer enters the item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B6 RID: 950
		// (add) Token: 0x06003D03 RID: 15619 RVA: 0x000F4E9C File Offset: 0x000F309C
		// (remove) Token: 0x06003D04 RID: 15620 RVA: 0x000F4EB0 File Offset: 0x000F30B0
		public event EventHandler MouseEnter
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.MouseEnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.MouseEnterEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer hovers over the item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B7 RID: 951
		// (add) Token: 0x06003D05 RID: 15621 RVA: 0x000F4EC4 File Offset: 0x000F30C4
		// (remove) Token: 0x06003D06 RID: 15622 RVA: 0x000F4ED8 File Offset: 0x000F30D8
		public event EventHandler MouseHover
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.MouseHoverEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.MouseHoverEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B8 RID: 952
		// (add) Token: 0x06003D07 RID: 15623 RVA: 0x000F4EEC File Offset: 0x000F30EC
		// (remove) Token: 0x06003D08 RID: 15624 RVA: 0x000F4F00 File Offset: 0x000F3100
		public event EventHandler MouseLeave
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.MouseLeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.MouseLeaveEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is moved over the item.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003B9 RID: 953
		// (add) Token: 0x06003D09 RID: 15625 RVA: 0x000F4F14 File Offset: 0x000F3114
		// (remove) Token: 0x06003D0A RID: 15626 RVA: 0x000F4F28 File Offset: 0x000F3128
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.MouseMoveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.MouseMoveEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the item and a mouse button is released.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003BA RID: 954
		// (add) Token: 0x06003D0B RID: 15627 RVA: 0x000F4F3C File Offset: 0x000F313C
		// (remove) Token: 0x06003D0C RID: 15628 RVA: 0x000F4F50 File Offset: 0x000F3150
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.MouseUpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.MouseUpEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Owner" /> property changes. </summary>
		// Token: 0x140003BB RID: 955
		// (add) Token: 0x06003D0D RID: 15629 RVA: 0x000F4F64 File Offset: 0x000F3164
		// (remove) Token: 0x06003D0E RID: 15630 RVA: 0x000F4F78 File Offset: 0x000F3178
		public event EventHandler OwnerChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.OwnerChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.OwnerChangedEvent, value);
			}
		}

		/// <summary>Occurs when the item is redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003BC RID: 956
		// (add) Token: 0x06003D0F RID: 15631 RVA: 0x000F4F8C File Offset: 0x000F318C
		// (remove) Token: 0x06003D10 RID: 15632 RVA: 0x000F4FA0 File Offset: 0x000F31A0
		public event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.PaintEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.PaintEvent, value);
			}
		}

		/// <summary>Occurs when an accessibility client application invokes help for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003BD RID: 957
		// (add) Token: 0x06003D11 RID: 15633 RVA: 0x000F4FB4 File Offset: 0x000F31B4
		// (remove) Token: 0x06003D12 RID: 15634 RVA: 0x000F4FC8 File Offset: 0x000F31C8
		[MonoTODO("Event never raised")]
		public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.QueryAccessibilityHelpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.QueryAccessibilityHelpEvent, value);
			}
		}

		/// <summary>Occurs during a drag-and-drop operation and allows the drag source to determine whether the drag-and-drop operation should be canceled.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003BE RID: 958
		// (add) Token: 0x06003D13 RID: 15635 RVA: 0x000F4FDC File Offset: 0x000F31DC
		// (remove) Token: 0x06003D14 RID: 15636 RVA: 0x000F4FF0 File Offset: 0x000F31F0
		[EditorBrowsable(2)]
		[MonoTODO("Event never raised")]
		[Browsable(false)]
		public event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.QueryContinueDragEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.QueryContinueDragEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.RightToLeft" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003BF RID: 959
		// (add) Token: 0x06003D15 RID: 15637 RVA: 0x000F5004 File Offset: 0x000F3204
		// (remove) Token: 0x06003D16 RID: 15638 RVA: 0x000F5018 File Offset: 0x000F3218
		public event EventHandler RightToLeftChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.RightToLeftChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.RightToLeftChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003C0 RID: 960
		// (add) Token: 0x06003D17 RID: 15639 RVA: 0x000F502C File Offset: 0x000F322C
		// (remove) Token: 0x06003D18 RID: 15640 RVA: 0x000F5040 File Offset: 0x000F3240
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.TextChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Visible" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003C1 RID: 961
		// (add) Token: 0x06003D19 RID: 15641 RVA: 0x000F5054 File Offset: 0x000F3254
		// (remove) Token: 0x06003D1A RID: 15642 RVA: 0x000F5068 File Offset: 0x000F3268
		public event EventHandler VisibleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.VisibleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.VisibleChangedEvent, value);
			}
		}

		// Token: 0x140003C2 RID: 962
		// (add) Token: 0x06003D1B RID: 15643 RVA: 0x000F507C File Offset: 0x000F327C
		// (remove) Token: 0x06003D1C RID: 15644 RVA: 0x000F5090 File Offset: 0x000F3290
		internal event EventHandler UIASelectionChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.UIASelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.UIASelectionChangedEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragDrop" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06003D1D RID: 15645 RVA: 0x000F50A4 File Offset: 0x000F32A4
		void IDropTarget.OnDragDrop(DragEventArgs dragEvent)
		{
			this.OnDragDrop(dragEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragEnter" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06003D1E RID: 15646 RVA: 0x000F50B0 File Offset: 0x000F32B0
		void IDropTarget.OnDragEnter(DragEventArgs dragEvent)
		{
			this.OnDragEnter(dragEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003D1F RID: 15647 RVA: 0x000F50BC File Offset: 0x000F32BC
		void IDropTarget.OnDragLeave(EventArgs e)
		{
			this.OnDragLeave(e);
		}

		/// <summary>Raises the DragOver event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06003D20 RID: 15648 RVA: 0x000F50C8 File Offset: 0x000F32C8
		void IDropTarget.OnDragOver(DragEventArgs dragEvent)
		{
			this.OnDragOver(dragEvent);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control; if no <see cref="T:System.Windows.Forms.AccessibleObject" /> is currently assigned to the control, a new instance is created when this property is first accessed </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000F50D4 File Offset: 0x000F32D4
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				if (this.accessibility_object == null)
				{
					this.accessibility_object = this.CreateAccessibilityInstance();
				}
				return this.accessibility_object;
			}
		}

		/// <summary>Gets or sets the default action description of the control for use by accessibility client applications.</summary>
		/// <returns>The default action description of the control, for use by accessibility client applications.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000F50F4 File Offset: 0x000F32F4
		// (set) Token: 0x06003D23 RID: 15651 RVA: 0x000F510C File Offset: 0x000F330C
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string AccessibleDefaultActionDescription
		{
			get
			{
				if (this.accessibility_object == null)
				{
					return null;
				}
				return this.accessible_default_action_description;
			}
			set
			{
				this.accessible_default_action_description = value;
			}
		}

		/// <summary>Gets or sets the description that will be reported to accessibility client applications.</summary>
		/// <returns>The description of the control used by accessibility client applications. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x000F5118 File Offset: 0x000F3318
		// (set) Token: 0x06003D25 RID: 15653 RVA: 0x000F5134 File Offset: 0x000F3334
		[Localizable(true)]
		[DefaultValue(null)]
		public string AccessibleDescription
		{
			get
			{
				if (this.accessibility_object == null)
				{
					return null;
				}
				return this.AccessibilityObject.Description;
			}
			set
			{
				this.AccessibilityObject.description = value;
			}
		}

		/// <summary>Gets or sets the name of the control for use by accessibility client applications.</summary>
		/// <returns>The name of the control, for use by accessibility client applications. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000F5144 File Offset: 0x000F3344
		// (set) Token: 0x06003D27 RID: 15655 RVA: 0x000F5160 File Offset: 0x000F3360
		[Localizable(true)]
		[DefaultValue(null)]
		public string AccessibleName
		{
			get
			{
				if (this.accessibility_object == null)
				{
					return null;
				}
				return this.AccessibilityObject.Name;
			}
			set
			{
				this.AccessibilityObject.Name = value;
			}
		}

		/// <summary>Gets or sets the accessible role of the control, which specifies the type of user interface element of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleRole.PushButton" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x000F5170 File Offset: 0x000F3370
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000F518C File Offset: 0x000F338C
		[DefaultValue(AccessibleRole.Default)]
		public AccessibleRole AccessibleRole
		{
			get
			{
				if (this.accessibility_object == null)
				{
					return AccessibleRole.Default;
				}
				return this.AccessibilityObject.Role;
			}
			set
			{
				this.AccessibilityObject.role = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item aligns towards the beginning or end of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000F519C File Offset: 0x000F339C
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x000F51A4 File Offset: 0x000F33A4
		[DefaultValue(ToolStripItemAlignment.Left)]
		public ToolStripItemAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripItemAlignment), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripItemAlignment", value));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					this.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.</summary>
		/// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.ToolStripItem.AllowDrop" /> and <see cref="P:System.Windows.Forms.ToolStrip.AllowItemReorder" /> are both set to true. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x000F51FC File Offset: 0x000F33FC
		// (set) Token: 0x06003D2D RID: 15661 RVA: 0x000F5204 File Offset: 0x000F3404
		[EditorBrowsable(2)]
		[MonoTODO("Stub, does nothing")]
		[Browsable(false)]
		[DefaultValue(false)]
		public virtual bool AllowDrop
		{
			get
			{
				return this.allow_drop;
			}
			set
			{
				this.allow_drop = value;
			}
		}

		/// <summary>Gets or sets the edges of the container to which a <see cref="T:System.Windows.Forms.ToolStripItem" /> is bound and determines how a <see cref="T:System.Windows.Forms.ToolStripItem" />  is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x000F5210 File Offset: 0x000F3410
		// (set) Token: 0x06003D2F RID: 15663 RVA: 0x000F5218 File Offset: 0x000F3418
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		public AnchorStyles Anchor
		{
			get
			{
				return this.anchor;
			}
			set
			{
				this.anchor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is automatically sized.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically sized; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000F5224 File Offset: 0x000F3424
		// (set) Token: 0x06003D31 RID: 15665 RVA: 0x000F522C File Offset: 0x000F342C
		[DefaultValue(true)]
		[Localizable(true)]
		[DesignerSerializationVisibility(1)]
		[RefreshProperties(1)]
		public bool AutoSize
		{
			get
			{
				return this.auto_size;
			}
			set
			{
				this.auto_size = value;
				this.CalculateAutoSize();
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property or the <see cref="P:System.Windows.Forms.ToolStripItem.ToolTipText" /> property for the <see cref="T:System.Windows.Forms.ToolStripItem" /> ToolTip. </summary>
		/// <returns>true to use the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property for the ToolTip; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000F523C File Offset: 0x000F343C
		// (set) Token: 0x06003D33 RID: 15667 RVA: 0x000F5244 File Offset: 0x000F3444
		[DefaultValue(false)]
		public bool AutoToolTip
		{
			get
			{
				return this.auto_tool_tip;
			}
			set
			{
				this.auto_tool_tip = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItem" /> should be placed on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is placed on a <see cref="T:System.Windows.Forms.ToolStrip" />; otherwise, false.</returns>
		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06003D34 RID: 15668 RVA: 0x000F5250 File Offset: 0x000F3450
		// (set) Token: 0x06003D35 RID: 15669 RVA: 0x000F5258 File Offset: 0x000F3458
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool Available
		{
			get
			{
				return this.available;
			}
			set
			{
				if (this.available != value)
				{
					this.available = value;
					this.visible = value;
					if (this.parent != null)
					{
						this.parent.PerformLayout();
					}
					this.OnAvailableChanged(EventArgs.Empty);
					this.OnVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color for the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x000F52AC File Offset: 0x000F34AC
		// (set) Token: 0x06003D37 RID: 15671 RVA: 0x000F52F4 File Offset: 0x000F34F4
		public virtual Color BackColor
		{
			get
			{
				if (this.back_color != Color.Empty)
				{
					return this.back_color;
				}
				if (this.Parent != null)
				{
					return this.parent.BackColor;
				}
				return Control.DefaultBackColor;
			}
			set
			{
				if (this.back_color != value)
				{
					this.back_color = value;
					this.OnBackColorChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the background image displayed in the item.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000F5320 File Offset: 0x000F3520
		// (set) Token: 0x06003D39 RID: 15673 RVA: 0x000F5328 File Offset: 0x000F3528
		[Localizable(true)]
		[DefaultValue(null)]
		public virtual Image BackgroundImage
		{
			get
			{
				return this.background_image;
			}
			set
			{
				if (this.background_image != value)
				{
					this.background_image = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the background image layout used for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values. The default value is <see cref="F:System.Windows.Forms.ImageLayout.Tile" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06003D3A RID: 15674 RVA: 0x000F5344 File Offset: 0x000F3544
		// (set) Token: 0x06003D3B RID: 15675 RVA: 0x000F534C File Offset: 0x000F354C
		[Localizable(true)]
		[DefaultValue(ImageLayout.Tile)]
		public virtual ImageLayout BackgroundImageLayout
		{
			get
			{
				return this.background_image_layout;
			}
			set
			{
				if (this.background_image_layout != value)
				{
					this.background_image_layout = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets the size and location of the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x000F5368 File Offset: 0x000F3568
		[Browsable(false)]
		public virtual Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets a value indicating whether the item can be selected.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x000F5370 File Offset: 0x000F3570
		[Browsable(false)]
		public virtual bool CanSelect
		{
			get
			{
				return this.can_select;
			}
		}

		/// <summary>Gets the area where content, such as text and icons, can be placed within a <see cref="T:System.Windows.Forms.ToolStripItem" /> without overwriting background borders.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> containing four integers that represent the location and size of <see cref="T:System.Windows.Forms.ToolStripItem" /> contents, excluding its border.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x000F5378 File Offset: 0x000F3578
		[Browsable(false)]
		public Rectangle ContentRectangle
		{
			get
			{
				if (this is ToolStripLabel || this is ToolStripStatusLabel)
				{
					return new Rectangle(0, 0, this.bounds.Width, this.bounds.Height);
				}
				if (this is ToolStripDropDownButton && (this as ToolStripDropDownButton).ShowDropDownArrow)
				{
					return new Rectangle(2, 2, this.bounds.Width - 13, this.bounds.Height - 4);
				}
				return new Rectangle(2, 2, this.bounds.Width - 4, this.bounds.Height - 4);
			}
		}

		/// <summary>Gets or sets whether text and images are displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText" /> .</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x000F5418 File Offset: 0x000F3618
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x000F5420 File Offset: 0x000F3620
		public virtual ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return this.display_style;
			}
			set
			{
				if (this.display_style != value)
				{
					this.display_style = value;
					this.CalculateAutoSize();
					this.OnDisplayStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets a value indicating whether the object has been disposed of.</summary>
		/// <returns>true if the control has been disposed of; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000F5454 File Offset: 0x000F3654
		[Browsable(false)]
		public bool IsDisposed
		{
			get
			{
				return this.is_disposed;
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.ToolStripItem" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.ToolStripItem" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x000F545C File Offset: 0x000F365C
		// (set) Token: 0x06003D43 RID: 15683 RVA: 0x000F5464 File Offset: 0x000F3664
		[Browsable(false)]
		[DefaultValue(DockStyle.None)]
		public DockStyle Dock
		{
			get
			{
				return this.dock;
			}
			set
			{
				if (this.dock != value)
				{
					if (!Enum.IsDefined(typeof(DockStyle), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for DockStyle", value));
					}
					this.dock = value;
					this.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be activated by double-clicking the mouse. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be activated by double-clicking the mouse; otherwise, false. The default is false.</returns>
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x000F54BC File Offset: 0x000F36BC
		// (set) Token: 0x06003D45 RID: 15685 RVA: 0x000F54C4 File Offset: 0x000F36C4
		[DefaultValue(false)]
		public bool DoubleClickEnabled
		{
			get
			{
				return this.double_click_enabled;
			}
			set
			{
				this.double_click_enabled = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled. </summary>
		/// <returns>true if the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x000F54D0 File Offset: 0x000F36D0
		// (set) Token: 0x06003D47 RID: 15687 RVA: 0x000F5520 File Offset: 0x000F3720
		[Localizable(true)]
		[DefaultValue(true)]
		public virtual bool Enabled
		{
			get
			{
				return (this.Parent == null || this.Parent.Enabled) && (this.Owner == null || this.Owner.Enabled) && this.enabled;
			}
			set
			{
				if (this.enabled != value)
				{
					this.enabled = value;
					this.OnEnabledChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the item.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the <see cref="T:System.Windows.Forms.ToolStripItem" />. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06003D48 RID: 15688 RVA: 0x000F5554 File Offset: 0x000F3754
		// (set) Token: 0x06003D49 RID: 15689 RVA: 0x000F5590 File Offset: 0x000F3790
		[Localizable(true)]
		public virtual Font Font
		{
			get
			{
				if (this.font != null)
				{
					return this.font;
				}
				if (this.Parent != null)
				{
					return this.Parent.Font;
				}
				return ToolStripItem.DefaultFont;
			}
			set
			{
				if (this.font != value)
				{
					this.font = value;
					this.CalculateAutoSize();
					this.OnFontChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the item.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x000F55C8 File Offset: 0x000F37C8
		// (set) Token: 0x06003D4B RID: 15691 RVA: 0x000F5610 File Offset: 0x000F3810
		public virtual Color ForeColor
		{
			get
			{
				if (this.fore_color != Color.Empty)
				{
					return this.fore_color;
				}
				if (this.Parent != null)
				{
					return this.parent.ForeColor;
				}
				return Control.DefaultForeColor;
			}
			set
			{
				if (this.fore_color != value)
				{
					this.fore_color = value;
					this.OnForeColorChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the height, in pixels, of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the height, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x000F563C File Offset: 0x000F383C
		// (set) Token: 0x06003D4D RID: 15693 RVA: 0x000F5658 File Offset: 0x000F3858
		[EditorBrowsable(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int Height
		{
			get
			{
				return this.Size.Height;
			}
			set
			{
				this.Size = new Size(this.Size.Width, value);
				this.explicit_size.Height = value;
				if (this.Visible)
				{
					this.CalculateAutoSize();
					this.OnBoundsChanged();
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> to be displayed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000F56A8 File Offset: 0x000F38A8
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x000F5794 File Offset: 0x000F3994
		[Localizable(true)]
		public virtual Image Image
		{
			get
			{
				if (this.image != null)
				{
					return this.image;
				}
				if (this.image_index >= 0 && this.owner != null && this.owner.ImageList != null && this.owner.ImageList.Images.Count > this.image_index)
				{
					return this.owner.ImageList.Images[this.image_index];
				}
				if (!string.IsNullOrEmpty(this.image_key) && this.owner != null && this.owner.ImageList != null && this.owner.ImageList.Images.Count > this.image_index)
				{
					return this.owner.ImageList.Images[this.image_key];
				}
				return null;
			}
			set
			{
				if (this.image != value)
				{
					this.StopAnimation();
					this.image = value;
					this.image_index = -1;
					this.image_key = string.Empty;
					this.CalculateAutoSize();
					this.Invalidate();
					this.BeginAnimation();
				}
			}
		}

		/// <summary>Gets or sets the alignment of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000F57D4 File Offset: 0x000F39D4
		// (set) Token: 0x06003D51 RID: 15697 RVA: 0x000F57DC File Offset: 0x000F39DC
		[Localizable(true)]
		[DefaultValue(32)]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.image_align;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ContentAlignment", value));
				}
				if (this.image_align != value)
				{
					this.image_align = value;
					this.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets or sets the index value of the image that is displayed on the item.</summary>
		/// <returns>The zero-based index of the image in the <see cref="P:System.Windows.Forms.ToolStrip.ImageList" /> that is displayed for the item. The default is -1, signifying that the image list is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is less than -1. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x000F5834 File Offset: 0x000F3A34
		// (set) Token: 0x06003D53 RID: 15699 RVA: 0x000F583C File Offset: 0x000F3A3C
		[RefreshProperties(2)]
		[Localizable(true)]
		[Browsable(false)]
		[RelatedImageList("Owner.ImageList")]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (this.image_index != value)
				{
					if (value < -1)
					{
						throw new ArgumentOutOfRangeException("ImageIndex cannot be less than -1");
					}
					this.image_index = value;
					this.image = null;
					this.image_key = string.Empty;
					this.CalculateAutoSize();
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.ToolStrip.ImageList" /> that is displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A string representing the key of the image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06003D54 RID: 15700 RVA: 0x000F588C File Offset: 0x000F3A8C
		// (set) Token: 0x06003D55 RID: 15701 RVA: 0x000F5894 File Offset: 0x000F3A94
		[Editor("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(2)]
		[Browsable(false)]
		[RelatedImageList("Owner.ImageList")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Localizable(true)]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
			set
			{
				if (this.image_key != value)
				{
					this.image = null;
					this.image_index = -1;
					this.image_key = value;
					this.CalculateAutoSize();
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether an image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically resized to fit in a container.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemImageScaling" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemImageScaling.SizeToFit" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06003D56 RID: 15702 RVA: 0x000F58D4 File Offset: 0x000F3AD4
		// (set) Token: 0x06003D57 RID: 15703 RVA: 0x000F58DC File Offset: 0x000F3ADC
		[Localizable(true)]
		[DefaultValue(ToolStripItemImageScaling.SizeToFit)]
		public ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return this.image_scaling;
			}
			set
			{
				if (this.image_scaling != value)
				{
					this.image_scaling = value;
					this.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets or sets the color to treat as transparent in a <see cref="T:System.Windows.Forms.ToolStripItem" /> image.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x000F58F8 File Offset: 0x000F3AF8
		// (set) Token: 0x06003D59 RID: 15705 RVA: 0x000F5900 File Offset: 0x000F3B00
		[Localizable(true)]
		public Color ImageTransparentColor
		{
			get
			{
				return this.image_transparent_color;
			}
			set
			{
				this.image_transparent_color = value;
			}
		}

		/// <summary>Gets a value indicating whether the container of the current <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" />. </summary>
		/// <returns>true if the container of the current <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x000F590C File Offset: 0x000F3B0C
		[Browsable(false)]
		public bool IsOnDropDown
		{
			get
			{
				return this.parent != null && this.parent is ToolStripDropDown;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.ToolStripItem.Placement" /> property is set to <see cref="F:System.Windows.Forms.ToolStripItemPlacement.Overflow" />.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.ToolStripItem.Placement" /> property is set to <see cref="F:System.Windows.Forms.ToolStripItemPlacement.Overflow" />; otherwise, false.</returns>
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x000F592C File Offset: 0x000F3B2C
		[Browsable(false)]
		public bool IsOnOverflow
		{
			get
			{
				return this.placement == ToolStripItemPlacement.Overflow;
			}
		}

		/// <summary>Gets or sets the space between the item and adjacent items.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between the item and adjacent items.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06003D5C RID: 15708 RVA: 0x000F5938 File Offset: 0x000F3B38
		// (set) Token: 0x06003D5D RID: 15709 RVA: 0x000F5940 File Offset: 0x000F3B40
		public Padding Margin
		{
			get
			{
				return this.margin;
			}
			set
			{
				this.margin = value;
				this.CalculateAutoSize();
			}
		}

		/// <summary>Gets or sets how child menus are merged with parent menus. </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MergeAction" /> values. The default is <see cref="F:System.Windows.Forms.MergeAction.MatchOnly" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.MergeAction" /> values.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000F5950 File Offset: 0x000F3B50
		// (set) Token: 0x06003D5F RID: 15711 RVA: 0x000F5958 File Offset: 0x000F3B58
		[DefaultValue(MergeAction.Append)]
		public MergeAction MergeAction
		{
			get
			{
				return this.merge_action;
			}
			set
			{
				if (!Enum.IsDefined(typeof(MergeAction), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for MergeAction", value));
				}
				this.merge_action = value;
			}
		}

		/// <summary>Gets or sets the position of a merged item within the current <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>An integer representing the index of the merged item, if a match is found, or -1 if a match is not found.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06003D60 RID: 15712 RVA: 0x000F5994 File Offset: 0x000F3B94
		// (set) Token: 0x06003D61 RID: 15713 RVA: 0x000F599C File Offset: 0x000F3B9C
		[DefaultValue(-1)]
		public int MergeIndex
		{
			get
			{
				return this.merge_index;
			}
			set
			{
				this.merge_index = value;
			}
		}

		/// <summary>Gets or sets the name of the item.</summary>
		/// <returns>A string representing the name. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x000F59A8 File Offset: 0x000F3BA8
		// (set) Token: 0x06003D63 RID: 15715 RVA: 0x000F59B0 File Offset: 0x000F3BB0
		[DefaultValue(null)]
		[Browsable(false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets whether the item is attached to the <see cref="T:System.Windows.Forms.ToolStrip" /> or <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> or can float between the two.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemOverflow.AsNeeded" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06003D64 RID: 15716 RVA: 0x000F59BC File Offset: 0x000F3BBC
		// (set) Token: 0x06003D65 RID: 15717 RVA: 0x000F59C4 File Offset: 0x000F3BC4
		[DefaultValue(ToolStripItemOverflow.AsNeeded)]
		public ToolStripItemOverflow Overflow
		{
			get
			{
				return this.overflow;
			}
			set
			{
				if (this.overflow != value)
				{
					if (!Enum.IsDefined(typeof(ToolStripItemOverflow), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripItemOverflow", value));
					}
					this.overflow = value;
					if (this.owner != null)
					{
						this.owner.PerformLayout();
					}
				}
			}
		}

		/// <summary>Gets or sets the owner of this item.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> that owns or is to own the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06003D66 RID: 15718 RVA: 0x000F5A2C File Offset: 0x000F3C2C
		// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000F5A34 File Offset: 0x000F3C34
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ToolStrip Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				if (this.owner != value)
				{
					if (this.owner != null)
					{
						this.owner.Items.Remove(this);
					}
					if (value != null)
					{
						value.Items.Add(this);
					}
					else
					{
						this.owner = null;
					}
				}
			}
		}

		/// <summary>Gets the parent <see cref="T:System.Windows.Forms.ToolStripItem" /> of this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The parent <see cref="T:System.Windows.Forms.ToolStripItem" /> of this <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000F5A88 File Offset: 0x000F3C88
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ToolStripItem OwnerItem
		{
			get
			{
				return this.owner_item;
			}
		}

		/// <summary>Gets or sets the internal spacing, in pixels, between the item's contents and its edges.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the item's internal spacing, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x000F5A90 File Offset: 0x000F3C90
		// (set) Token: 0x06003D6A RID: 15722 RVA: 0x000F5A98 File Offset: 0x000F3C98
		public virtual Padding Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				this.padding = value;
				this.CalculateAutoSize();
				this.Invalidate();
			}
		}

		/// <summary>Gets the current layout of the item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemPlacement" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06003D6B RID: 15723 RVA: 0x000F5AB0 File Offset: 0x000F3CB0
		[Browsable(false)]
		public ToolStripItemPlacement Placement
		{
			get
			{
				return this.placement;
			}
		}

		/// <summary>Gets a value indicating whether the state of the item is pressed. </summary>
		/// <returns>true if the state of the item is pressed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000F5AB8 File Offset: 0x000F3CB8
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public virtual bool Pressed
		{
			get
			{
				return this.is_pressed;
			}
		}

		/// <summary>Gets or sets a value indicating whether items are to be placed from right to left and text is to be written from right to left.</summary>
		/// <returns>true if items are to be placed from right to left and text is to be written from right to left; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x000F5AC0 File Offset: 0x000F3CC0
		// (set) Token: 0x06003D6E RID: 15726 RVA: 0x000F5AC8 File Offset: 0x000F3CC8
		[MonoTODO("RTL not implemented")]
		[Localizable(true)]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				return this.right_to_left;
			}
			set
			{
				if (this.right_to_left != value)
				{
					this.right_to_left = value;
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Mirrors automatically the <see cref="T:System.Windows.Forms.ToolStripItem" /> image when the <see cref="P:System.Windows.Forms.ToolStripItem.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />.</summary>
		/// <returns>true to automatically mirror the image; otherwise, false. The default is false.</returns>
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x000F5AE8 File Offset: 0x000F3CE8
		// (set) Token: 0x06003D70 RID: 15728 RVA: 0x000F5AF0 File Offset: 0x000F3CF0
		[Localizable(true)]
		[DefaultValue(false)]
		public bool RightToLeftAutoMirrorImage
		{
			get
			{
				return this.right_to_left_auto_mirror_image;
			}
			set
			{
				if (this.right_to_left_auto_mirror_image != value)
				{
					this.right_to_left_auto_mirror_image = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets a value indicating whether the item is selected.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x000F5B0C File Offset: 0x000F3D0C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public virtual bool Selected
		{
			get
			{
				return this.is_selected;
			}
		}

		/// <summary>Gets or sets the size of the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000F5B14 File Offset: 0x000F3D14
		// (set) Token: 0x06003D73 RID: 15731 RVA: 0x000F5B54 File Offset: 0x000F3D54
		[Localizable(true)]
		public virtual Size Size
		{
			get
			{
				if (!this.AutoSize && this.explicit_size != Size.Empty)
				{
					return this.explicit_size;
				}
				return this.bounds.Size;
			}
			set
			{
				this.bounds.Size = value;
				this.explicit_size = value;
				if (this.Visible)
				{
					this.CalculateAutoSize();
					this.OnBoundsChanged();
				}
			}
		}

		/// <summary>Gets or sets the object that contains data about the item.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x000F5B8C File Offset: 0x000F3D8C
		// (set) Token: 0x06003D75 RID: 15733 RVA: 0x000F5B94 File Offset: 0x000F3D94
		[Localizable(false)]
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the text that is to be displayed on the item.</summary>
		/// <returns>A string representing the item's text. The default value is the empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x000F5BA0 File Offset: 0x000F3DA0
		// (set) Token: 0x06003D77 RID: 15735 RVA: 0x000F5BA8 File Offset: 0x000F3DA8
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (this.text != value)
				{
					this.text = value;
					this.Invalidate();
					this.CalculateAutoSize();
					this.Invalidate();
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the alignment of the text on a <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleRight" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x000F5BEC File Offset: 0x000F3DEC
		// (set) Token: 0x06003D79 RID: 15737 RVA: 0x000F5BF4 File Offset: 0x000F3DF4
		[DefaultValue(32)]
		[Localizable(true)]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.text_align;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ContentAlignment", value));
				}
				if (this.text_align != value)
				{
					this.text_align = value;
					this.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets the orientation of text used on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x000F5C4C File Offset: 0x000F3E4C
		// (set) Token: 0x06003D7B RID: 15739 RVA: 0x000F5C84 File Offset: 0x000F3E84
		public virtual ToolStripTextDirection TextDirection
		{
			get
			{
				if (this.text_direction != ToolStripTextDirection.Inherit)
				{
					return this.text_direction;
				}
				if (this.Parent != null)
				{
					return this.Parent.TextDirection;
				}
				return ToolStripTextDirection.Horizontal;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripTextDirection), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripTextDirection", value));
				}
				if (this.text_direction != value)
				{
					this.text_direction = value;
					this.CalculateAutoSize();
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the position of <see cref="T:System.Windows.Forms.ToolStripItem" /> text and image relative to each other.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TextImageRelation" /> values. The default is <see cref="F:System.Windows.Forms.TextImageRelation.ImageBeforeText" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000F5CE0 File Offset: 0x000F3EE0
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x000F5CE8 File Offset: 0x000F3EE8
		[Localizable(true)]
		[DefaultValue(TextImageRelation.ImageBeforeText)]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this.text_image_relation;
			}
			set
			{
				this.text_image_relation = value;
				this.CalculateAutoSize();
				this.Invalidate();
			}
		}

		/// <summary>Gets or sets the text that appears as a <see cref="T:System.Windows.Forms.ToolTip" /> for a control.</summary>
		/// <returns>A string representing the ToolTip text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x000F5D00 File Offset: 0x000F3F00
		// (set) Token: 0x06003D7F RID: 15743 RVA: 0x000F5D08 File Offset: 0x000F3F08
		[Localizable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ToolTipText
		{
			get
			{
				return this.tool_tip_text;
			}
			set
			{
				this.tool_tip_text = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is displayed.</summary>
		/// <returns>true if the item is displayed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000F5D14 File Offset: 0x000F3F14
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000F5D48 File Offset: 0x000F3F48
		[Localizable(true)]
		public bool Visible
		{
			get
			{
				return this.parent != null && this.visible && this.parent.Visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.available = value;
					this.SetVisibleCore(value);
					if (this.Owner != null)
					{
						this.Owner.PerformLayout();
					}
				}
			}
		}

		/// <summary>Gets or sets the width in pixels of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the width in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x000F5D88 File Offset: 0x000F3F88
		// (set) Token: 0x06003D83 RID: 15747 RVA: 0x000F5DA4 File Offset: 0x000F3FA4
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(0)]
		[Browsable(false)]
		public int Width
		{
			get
			{
				return this.Size.Width;
			}
			set
			{
				this.Size = new Size(value, this.Size.Height);
				this.explicit_size.Width = value;
				if (this.Visible)
				{
					this.CalculateAutoSize();
					this.OnBoundsChanged();
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x000F5DF4 File Offset: 0x000F3FF4
		protected virtual bool DefaultAutoToolTip
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating what is displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText" />.</returns>
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06003D85 RID: 15749 RVA: 0x000F5DF8 File Offset: 0x000F3FF8
		protected virtual ToolStripItemDisplayStyle DefaultDisplayStyle
		{
			get
			{
				return ToolStripItemDisplayStyle.ImageAndText;
			}
		}

		/// <summary>Gets the default margin of an item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the margin.</returns>
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x000F5DFC File Offset: 0x000F3FFC
		protected internal virtual Padding DefaultMargin
		{
			get
			{
				return new Padding(0, 1, 0, 2);
			}
		}

		/// <summary>Gets the internal spacing characteristics of the item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Padding" /> values.</returns>
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06003D87 RID: 15751 RVA: 0x000F5E08 File Offset: 0x000F4008
		protected virtual Padding DefaultPadding
		{
			get
			{
				return default(Padding);
			}
		}

		/// <summary>Gets the default size of the item.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x000F5E20 File Offset: 0x000F4020
		protected virtual Size DefaultSize
		{
			get
			{
				return new Size(23, 23);
			}
		}

		/// <summary>Gets a value indicating whether items on a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> are hidden after they are clicked.</summary>
		/// <returns>true if the item is hidden after it is clicked; otherwise, false.</returns>
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06003D89 RID: 15753 RVA: 0x000F5E2C File Offset: 0x000F402C
		protected internal virtual bool DismissWhenClicked
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the parent container of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStrip" /> that is the parent container of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000F5E30 File Offset: 0x000F4030
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x000F5E38 File Offset: 0x000F4038
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		protected internal ToolStrip Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					ToolStrip toolStrip = this.parent;
					this.parent = value;
					this.OnParentChanged(toolStrip, this.parent);
				}
			}
		}

		/// <summary>Gets a value indicating whether to show or hide shortcut keys.</summary>
		/// <returns>true to show shortcut keys; otherwise, false. The default is true.</returns>
		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000F5E6C File Offset: 0x000F406C
		protected internal virtual bool ShowKeyboardCues
		{
			get
			{
				return false;
			}
		}

		/// <summary>Begins a drag-and-drop operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		/// <param name="data">The object to be dragged. </param>
		/// <param name="allowedEffects">The drag operations that can occur. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003D8D RID: 15757 RVA: 0x000F5E70 File Offset: 0x000F4070
		[MonoTODO("Stub, does nothing")]
		[EditorBrowsable(2)]
		public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
		{
			return allowedEffects;
		}

		/// <summary>Retrieves the <see cref="T:System.Windows.Forms.ToolStrip" /> that is the container of the current <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStrip" /> that is the container of the current <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003D8E RID: 15758 RVA: 0x000F5E74 File Offset: 0x000F4074
		public ToolStrip GetCurrentParent()
		{
			return this.parent;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fit.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> ordered pair, representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D8F RID: 15759 RVA: 0x000F5E7C File Offset: 0x000F407C
		public virtual Size GetPreferredSize(Size constrainingSize)
		{
			return this.CalculatePreferredSize(constrainingSize);
		}

		/// <summary>Invalidates the entire surface of the <see cref="T:System.Windows.Forms.ToolStripItem" /> and causes it to be redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D90 RID: 15760 RVA: 0x000F5E88 File Offset: 0x000F4088
		public void Invalidate()
		{
			if (this.parent != null)
			{
				this.parent.Invalidate(this.bounds);
			}
		}

		/// <summary>Invalidates the specified region of the <see cref="T:System.Windows.Forms.ToolStripItem" /> by adding it to the update region of the <see cref="T:System.Windows.Forms.ToolStripItem" />, which is the area that will be repainted at the next paint operation, and causes a paint message to be sent to the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="r">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D91 RID: 15761 RVA: 0x000F5EA8 File Offset: 0x000F40A8
		public void Invalidate(Rectangle r)
		{
			if (this.parent != null)
			{
				this.parent.Invalidate(r);
			}
		}

		/// <summary>Activates the <see cref="T:System.Windows.Forms.ToolStripItem" /> when it is clicked with the mouse.</summary>
		// Token: 0x06003D92 RID: 15762 RVA: 0x000F5EC4 File Offset: 0x000F40C4
		public void PerformClick()
		{
			this.OnClick(EventArgs.Empty);
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D93 RID: 15763 RVA: 0x000F5ED4 File Offset: 0x000F40D4
		[EditorBrowsable(1)]
		public virtual void ResetBackColor()
		{
			this.BackColor = Color.Empty;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06003D94 RID: 15764 RVA: 0x000F5EE4 File Offset: 0x000F40E4
		[EditorBrowsable(1)]
		public virtual void ResetDisplayStyle()
		{
			this.display_style = this.DefaultDisplayStyle;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06003D95 RID: 15765 RVA: 0x000F5EF4 File Offset: 0x000F40F4
		[EditorBrowsable(1)]
		public virtual void ResetFont()
		{
			this.font = null;
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D96 RID: 15766 RVA: 0x000F5F00 File Offset: 0x000F4100
		[EditorBrowsable(1)]
		public virtual void ResetForeColor()
		{
			this.ForeColor = Color.Empty;
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D97 RID: 15767 RVA: 0x000F5F10 File Offset: 0x000F4110
		[EditorBrowsable(1)]
		public virtual void ResetImage()
		{
			this.image = null;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06003D98 RID: 15768 RVA: 0x000F5F1C File Offset: 0x000F411C
		[EditorBrowsable(1)]
		public void ResetMargin()
		{
			this.margin = this.DefaultMargin;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06003D99 RID: 15769 RVA: 0x000F5F2C File Offset: 0x000F412C
		[EditorBrowsable(1)]
		public void ResetPadding()
		{
			this.padding = this.DefaultPadding;
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D9A RID: 15770 RVA: 0x000F5F3C File Offset: 0x000F413C
		[EditorBrowsable(1)]
		public virtual void ResetRightToLeft()
		{
			this.right_to_left = RightToLeft.Inherit;
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D9B RID: 15771 RVA: 0x000F5F48 File Offset: 0x000F4148
		[EditorBrowsable(1)]
		public virtual void ResetTextDirection()
		{
			this.TextDirection = this.DefaultTextDirection;
		}

		/// <summary>Selects the item.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003D9C RID: 15772 RVA: 0x000F5F58 File Offset: 0x000F4158
		public void Select()
		{
			if (!this.is_selected && this.CanSelect)
			{
				this.is_selected = true;
				if (this.Parent != null)
				{
					if (this.Visible && this.Parent.Focused && this is ToolStripControlHost)
					{
						(this as ToolStripControlHost).Focus();
					}
					this.Invalidate();
					this.Parent.NotifySelectedChanged(this);
				}
				this.OnUIASelectionChanged();
			}
		}

		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D9D RID: 15773 RVA: 0x000F5FD8 File Offset: 0x000F41D8
		public override string ToString()
		{
			return this.text;
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x06003D9E RID: 15774 RVA: 0x000F5FE0 File Offset: 0x000F41E0
		[EditorBrowsable(2)]
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripItem.ToolStripItemAccessibleObject(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripItem" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003D9F RID: 15775 RVA: 0x000F5FE8 File Offset: 0x000F41E8
		protected override void Dispose(bool disposing)
		{
			if (!this.is_disposed && disposing)
			{
				this.is_disposed = true;
			}
			if (this.image != null)
			{
				this.StopAnimation();
				this.image = null;
			}
			if (this.owner != null)
			{
				this.owner.Items.Remove(this);
			}
			base.Dispose(disposing);
		}

		/// <summary>Determines whether a character is an input character that the item recognizes.</summary>
		/// <returns>true if the character should be sent directly to the item and not preprocessed; otherwise, false.</returns>
		/// <param name="charCode">The character to test. </param>
		// Token: 0x06003DA0 RID: 15776 RVA: 0x000F6048 File Offset: 0x000F4248
		protected internal virtual bool IsInputChar(char charCode)
		{
			return false;
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x06003DA1 RID: 15777 RVA: 0x000F604C File Offset: 0x000F424C
		protected internal virtual bool IsInputKey(Keys keyData)
		{
			return false;
		}

		/// <summary>Raises the AvailableChanged event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DA2 RID: 15778 RVA: 0x000F6050 File Offset: 0x000F4250
		protected virtual void OnAvailableChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.AvailableChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DA3 RID: 15779 RVA: 0x000F6084 File Offset: 0x000F4284
		[EditorBrowsable(2)]
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.BackColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Bounds" /> property changes.</summary>
		// Token: 0x06003DA4 RID: 15780 RVA: 0x000F60B8 File Offset: 0x000F42B8
		protected virtual void OnBoundsChanged()
		{
			this.OnLayout(new LayoutEventArgs(null, string.Empty));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DA5 RID: 15781 RVA: 0x000F60CC File Offset: 0x000F42CC
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DisplayStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DA6 RID: 15782 RVA: 0x000F6100 File Offset: 0x000F4300
		[EditorBrowsable(2)]
		protected virtual void OnDisplayStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.DisplayStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DA7 RID: 15783 RVA: 0x000F6134 File Offset: 0x000F4334
		protected virtual void OnDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.DoubleClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			if (!this.double_click_enabled)
			{
				this.OnClick(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragDrop" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DA8 RID: 15784 RVA: 0x000F6178 File Offset: 0x000F4378
		[EditorBrowsable(2)]
		protected virtual void OnDragDrop(DragEventArgs dragEvent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[ToolStripItem.DragDropEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, dragEvent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragEnter" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DA9 RID: 15785 RVA: 0x000F61AC File Offset: 0x000F43AC
		[EditorBrowsable(2)]
		protected virtual void OnDragEnter(DragEventArgs dragEvent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[ToolStripItem.DragEnterEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, dragEvent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DAA RID: 15786 RVA: 0x000F61E0 File Offset: 0x000F43E0
		[EditorBrowsable(2)]
		protected virtual void OnDragLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.DragLeaveEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragOver" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DAB RID: 15787 RVA: 0x000F6214 File Offset: 0x000F4414
		[EditorBrowsable(2)]
		protected virtual void OnDragOver(DragEventArgs dragEvent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[ToolStripItem.DragOverEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, dragEvent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DAC RID: 15788 RVA: 0x000F6248 File Offset: 0x000F4448
		protected virtual void OnEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.EnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DAD RID: 15789 RVA: 0x000F627C File Offset: 0x000F447C
		[EditorBrowsable(2)]
		protected virtual void OnFontChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DAE RID: 15790 RVA: 0x000F6280 File Offset: 0x000F4480
		[EditorBrowsable(2)]
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.ForeColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.GiveFeedback" /> event.</summary>
		/// <param name="giveFeedbackEvent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DAF RID: 15791 RVA: 0x000F62B4 File Offset: 0x000F44B4
		[EditorBrowsable(2)]
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEvent)
		{
			GiveFeedbackEventHandler giveFeedbackEventHandler = (GiveFeedbackEventHandler)base.Events[ToolStripItem.GiveFeedbackEvent];
			if (giveFeedbackEventHandler != null)
			{
				giveFeedbackEventHandler(this, giveFeedbackEvent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003DB0 RID: 15792 RVA: 0x000F62E8 File Offset: 0x000F44E8
		protected virtual void OnLayout(LayoutEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.LocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DB1 RID: 15793 RVA: 0x000F62EC File Offset: 0x000F44EC
		protected virtual void OnLocationChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.LocationChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DB2 RID: 15794 RVA: 0x000F6320 File Offset: 0x000F4520
		protected virtual void OnMouseDown(MouseEventArgs e)
		{
			if (this.Enabled)
			{
				this.is_pressed = true;
				this.Invalidate();
				MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[ToolStripItem.MouseDownEvent];
				if (mouseEventHandler != null)
				{
					mouseEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DB3 RID: 15795 RVA: 0x000F636C File Offset: 0x000F456C
		protected virtual void OnMouseEnter(EventArgs e)
		{
			this.Select();
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.MouseEnterEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DB4 RID: 15796 RVA: 0x000F63A4 File Offset: 0x000F45A4
		protected virtual void OnMouseHover(EventArgs e)
		{
			if (this.Enabled)
			{
				EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.MouseHoverEvent];
				if (eventHandler != null)
				{
					eventHandler.Invoke(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DB5 RID: 15797 RVA: 0x000F63E0 File Offset: 0x000F45E0
		protected virtual void OnMouseLeave(EventArgs e)
		{
			if (this.CanSelect)
			{
				this.is_selected = false;
				this.is_pressed = false;
				this.Invalidate();
				this.OnUIASelectionChanged();
			}
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.MouseLeaveEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseMove" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DB6 RID: 15798 RVA: 0x000F6438 File Offset: 0x000F4638
		protected virtual void OnMouseMove(MouseEventArgs mea)
		{
			if (this.Enabled)
			{
				MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[ToolStripItem.MouseMoveEvent];
				if (mouseEventHandler != null)
				{
					mouseEventHandler(this, mea);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DB7 RID: 15799 RVA: 0x000F6474 File Offset: 0x000F4674
		protected virtual void OnMouseUp(MouseEventArgs e)
		{
			if (this.Enabled)
			{
				this.is_pressed = false;
				this.Invalidate();
				if (this.IsOnDropDown && (!(this is ToolStripDropDownItem) || !(this as ToolStripDropDownItem).HasDropDownItems || !(this as ToolStripDropDownItem).DropDown.Visible))
				{
					if ((this.Parent as ToolStripDropDown).OwnerItem != null)
					{
						((this.Parent as ToolStripDropDown).OwnerItem as ToolStripDropDownItem).HideDropDown();
					}
					else
					{
						(this.Parent as ToolStripDropDown).Hide();
					}
				}
				MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[ToolStripItem.MouseUpEvent];
				if (mouseEventHandler != null)
				{
					mouseEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.OwnerChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000F653C File Offset: 0x000F473C
		protected virtual void OnOwnerChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.OwnerChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.ToolStripItem.Font" /> property has changed on the parent of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000F6570 File Offset: 0x000F4770
		[EditorBrowsable(2)]
		protected internal virtual void OnOwnerFontChanged(EventArgs e)
		{
			this.CalculateAutoSize();
			this.OnFontChanged(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DBA RID: 15802 RVA: 0x000F6584 File Offset: 0x000F4784
		protected virtual void OnPaint(PaintEventArgs e)
		{
			if (this.parent != null)
			{
				this.parent.Renderer.DrawItemBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			}
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[ToolStripItem.PaintEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DBB RID: 15803 RVA: 0x000F65DC File Offset: 0x000F47DC
		[EditorBrowsable(2)]
		protected virtual void OnParentBackColorChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="oldParent">The original parent of the item. </param>
		/// <param name="newParent">The new parent of the item. </param>
		// Token: 0x06003DBC RID: 15804 RVA: 0x000F65E0 File Offset: 0x000F47E0
		protected virtual void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
		{
			this.text_size = TextRenderer.MeasureText((this.Text != null) ? this.text : string.Empty, this.Font, Size.Empty, TextFormatFlags.HidePrefix);
			if (oldParent != null)
			{
				oldParent.PerformLayout();
			}
			if (newParent != null)
			{
				newParent.PerformLayout();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.EnabledChanged" /> event when the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property value of the item's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DBD RID: 15805 RVA: 0x000F663C File Offset: 0x000F483C
		protected internal virtual void OnParentEnabledChanged(EventArgs e)
		{
			this.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DBE RID: 15806 RVA: 0x000F6648 File Offset: 0x000F4848
		[EditorBrowsable(2)]
		protected virtual void OnParentForeColorChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DBF RID: 15807 RVA: 0x000F664C File Offset: 0x000F484C
		[EditorBrowsable(2)]
		protected internal virtual void OnParentRightToLeftChanged(EventArgs e)
		{
			this.OnRightToLeftChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.QueryContinueDrag" /> event.</summary>
		/// <param name="queryContinueDragEvent">A <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> that contains the event data. </param>
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000F6658 File Offset: 0x000F4858
		[EditorBrowsable(2)]
		protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEvent)
		{
			QueryContinueDragEventHandler queryContinueDragEventHandler = (QueryContinueDragEventHandler)base.Events[ToolStripItem.QueryContinueDragEvent];
			if (queryContinueDragEventHandler != null)
			{
				queryContinueDragEventHandler(this, queryContinueDragEvent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DC1 RID: 15809 RVA: 0x000F668C File Offset: 0x000F488C
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.RightToLeftChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DC2 RID: 15810 RVA: 0x000F66C0 File Offset: 0x000F48C0
		[EditorBrowsable(2)]
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003DC3 RID: 15811 RVA: 0x000F66F4 File Offset: 0x000F48F4
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.VisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003DC4 RID: 15812 RVA: 0x000F6728 File Offset: 0x000F4928
		protected internal virtual bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return false;
		}

		/// <summary>Processes a dialog key.</summary>
		/// <returns>true if the key was processed by the item; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003DC5 RID: 15813 RVA: 0x000F672C File Offset: 0x000F492C
		protected internal virtual bool ProcessDialogKey(Keys keyData)
		{
			if (this.Selected && keyData == Keys.Return)
			{
				this.FireEvent(EventArgs.Empty, ToolStripItemEventType.Click);
				return true;
			}
			return false;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06003DC6 RID: 15814 RVA: 0x000F675C File Offset: 0x000F495C
		protected internal virtual bool ProcessMnemonic(char charCode)
		{
			ToolStripManager.SetActiveToolStrip(this.Parent, true);
			this.PerformClick();
			return true;
		}

		/// <summary>Sets the size and location of the item.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripItem" /></param>
		// Token: 0x06003DC7 RID: 15815 RVA: 0x000F6774 File Offset: 0x000F4974
		protected internal virtual void SetBounds(Rectangle bounds)
		{
			if (this.bounds != bounds)
			{
				this.bounds = bounds;
				this.OnBoundsChanged();
			}
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to the specified visible state. </summary>
		/// <param name="visible">true to make the <see cref="T:System.Windows.Forms.ToolStripItem" /> visible; otherwise, false.</param>
		// Token: 0x06003DC8 RID: 15816 RVA: 0x000F6794 File Offset: 0x000F4994
		protected virtual void SetVisibleCore(bool visible)
		{
			this.visible = visible;
			this.OnVisibleChanged(EventArgs.Empty);
			if (this.visible)
			{
				this.BeginAnimation();
			}
			else
			{
				this.StopAnimation();
			}
			this.Invalidate();
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x000F67D8 File Offset: 0x000F49D8
		internal Rectangle AlignInRectangle(Rectangle outer, Size inner, ContentAlignment align)
		{
			int num = 0;
			int num2 = 0;
			if (align == 256 || align == 16 || align == 1)
			{
				num = outer.X;
			}
			else if (align == 512 || align == 32 || align == 2)
			{
				num = Math.Max(outer.X + (outer.Width - inner.Width) / 2, outer.Left);
			}
			else if (align == 1024 || align == 64 || align == 4)
			{
				num = outer.Right - inner.Width;
			}
			if (align == 2 || align == 1 || align == 4)
			{
				num2 = outer.Y;
			}
			else if (align == 32 || align == 16 || align == 64)
			{
				num2 = outer.Y + (outer.Height - inner.Height) / 2;
			}
			else if (align == 512 || align == 1024 || align == 256)
			{
				num2 = outer.Bottom - inner.Height;
			}
			return new Rectangle(num, num2, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000F6938 File Offset: 0x000F4B38
		internal void CalculateAutoSize()
		{
			this.text_size = TextRenderer.MeasureText((this.Text != null) ? this.text : string.Empty, this.Font, Size.Empty, TextFormatFlags.HidePrefix);
			ToolStripTextDirection textDirection = this.TextDirection;
			if (textDirection == ToolStripTextDirection.Vertical270 || textDirection == ToolStripTextDirection.Vertical90)
			{
				this.text_size = new Size(this.text_size.Height, this.text_size.Width);
			}
			if (!this.auto_size || this is ToolStripControlHost)
			{
				return;
			}
			Size size = this.CalculatePreferredSize(Size.Empty);
			if (size != this.Size)
			{
				this.bounds.Width = size.Width;
				if (this.parent != null)
				{
					this.parent.PerformLayout();
				}
			}
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x000F6A10 File Offset: 0x000F4C10
		internal virtual Size CalculatePreferredSize(Size constrainingSize)
		{
			if (!this.auto_size)
			{
				return this.explicit_size;
			}
			Size size = this.DefaultSize;
			switch (this.display_style)
			{
			case ToolStripItemDisplayStyle.Text:
			{
				int num = this.text_size.Width + this.padding.Horizontal;
				int num2 = this.text_size.Height + this.padding.Vertical;
				size..ctor(num, num2);
				break;
			}
			case ToolStripItemDisplayStyle.Image:
				if (this.GetImageSize() == Size.Empty)
				{
					size = this.DefaultSize;
				}
				else
				{
					ToolStripItemImageScaling toolStripItemImageScaling = this.image_scaling;
					if (toolStripItemImageScaling != ToolStripItemImageScaling.None)
					{
						if (toolStripItemImageScaling == ToolStripItemImageScaling.SizeToFit)
						{
							if (this.parent == null)
							{
								size = this.GetImageSize();
							}
							else
							{
								size = this.parent.ImageScalingSize;
							}
						}
					}
					else
					{
						size = this.GetImageSize();
					}
				}
				break;
			case ToolStripItemDisplayStyle.ImageAndText:
			{
				int num3 = this.text_size.Width + this.padding.Horizontal;
				int num4 = this.text_size.Height + this.padding.Vertical;
				if (this.GetImageSize() != Size.Empty)
				{
					Size size2 = this.GetImageSize();
					if (this.image_scaling == ToolStripItemImageScaling.SizeToFit && this.parent != null)
					{
						size2 = this.parent.ImageScalingSize;
					}
					switch (this.text_image_relation)
					{
					case TextImageRelation.Overlay:
						num3 = Math.Max(num3, size2.Width);
						num4 = Math.Max(num4, size2.Height);
						break;
					case TextImageRelation.ImageAboveText:
					case TextImageRelation.TextAboveImage:
						num3 = Math.Max(num3, size2.Width);
						num4 += size2.Height;
						break;
					case TextImageRelation.ImageBeforeText:
					case TextImageRelation.TextBeforeImage:
						num4 = Math.Max(num4, size2.Height);
						num3 += size2.Width;
						break;
					}
				}
				size..ctor(num3, num4);
				break;
			}
			}
			if (!(this is ToolStripLabel))
			{
				size.Height += 4;
				size.Width += 4;
			}
			return size;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000F6C50 File Offset: 0x000F4E50
		internal void CalculateTextAndImageRectangles(out Rectangle text_rect, out Rectangle image_rect)
		{
			this.CalculateTextAndImageRectangles(this.ContentRectangle, out text_rect, out image_rect);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000F6C60 File Offset: 0x000F4E60
		internal void CalculateTextAndImageRectangles(Rectangle contentRectangle, out Rectangle text_rect, out Rectangle image_rect)
		{
			text_rect = Rectangle.Empty;
			image_rect = Rectangle.Empty;
			switch (this.display_style)
			{
			case ToolStripItemDisplayStyle.Text:
				if (this.text != string.Empty)
				{
					text_rect = this.AlignInRectangle(contentRectangle, this.text_size, this.text_align);
				}
				break;
			case ToolStripItemDisplayStyle.Image:
				if (this.Image != null && this.UseImageMargin)
				{
					image_rect = this.AlignInRectangle(contentRectangle, this.GetImageSize(), this.image_align);
				}
				break;
			case ToolStripItemDisplayStyle.ImageAndText:
				if (this.text != string.Empty && (this.Image == null || !this.UseImageMargin))
				{
					text_rect = this.AlignInRectangle(contentRectangle, this.text_size, this.text_align);
				}
				else if (!(this.text == string.Empty) || (this.Image != null && this.UseImageMargin))
				{
					if (this.text == string.Empty && this.Image != null)
					{
						image_rect = this.AlignInRectangle(contentRectangle, this.GetImageSize(), this.image_align);
					}
					else
					{
						switch (this.text_image_relation)
						{
						case TextImageRelation.Overlay:
							text_rect = this.AlignInRectangle(contentRectangle, this.text_size, this.text_align);
							image_rect = this.AlignInRectangle(contentRectangle, this.GetImageSize(), this.image_align);
							break;
						case TextImageRelation.ImageAboveText:
						{
							Rectangle rectangle;
							rectangle..ctor(contentRectangle.Left, contentRectangle.Bottom - (this.text_size.Height - 4), contentRectangle.Width, this.text_size.Height - 4);
							Rectangle rectangle2;
							rectangle2..ctor(contentRectangle.Left, contentRectangle.Top, contentRectangle.Width, contentRectangle.Height - rectangle.Height);
							text_rect = this.AlignInRectangle(rectangle, this.text_size, this.text_align);
							image_rect = this.AlignInRectangle(rectangle2, this.GetImageSize(), this.image_align);
							break;
						}
						case TextImageRelation.TextAboveImage:
						{
							Rectangle rectangle;
							rectangle..ctor(contentRectangle.Left, contentRectangle.Top, contentRectangle.Width, this.text_size.Height - 4);
							Rectangle rectangle2;
							rectangle2..ctor(contentRectangle.Left, rectangle.Bottom, contentRectangle.Width, contentRectangle.Height - rectangle.Height);
							text_rect = this.AlignInRectangle(rectangle, this.text_size, this.text_align);
							image_rect = this.AlignInRectangle(rectangle2, this.GetImageSize(), this.image_align);
							break;
						}
						case TextImageRelation.ImageBeforeText:
							this.LayoutTextBeforeOrAfterImage(contentRectangle, false, this.text_size, this.GetImageSize(), this.text_align, this.image_align, out text_rect, out image_rect);
							break;
						case TextImageRelation.TextBeforeImage:
							this.LayoutTextBeforeOrAfterImage(contentRectangle, true, this.text_size, this.GetImageSize(), this.text_align, this.image_align, out text_rect, out image_rect);
							break;
						}
					}
				}
				break;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000F6FAC File Offset: 0x000F51AC
		private static Font DefaultFont
		{
			get
			{
				return new Font("Tahoma", 8.25f);
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06003DCF RID: 15823 RVA: 0x000F6FC0 File Offset: 0x000F51C0
		internal virtual ToolStripTextDirection DefaultTextDirection
		{
			get
			{
				return ToolStripTextDirection.Inherit;
			}
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000F6FC4 File Offset: 0x000F51C4
		internal virtual void Dismiss(ToolStripDropDownCloseReason reason)
		{
			if (this.is_selected)
			{
				this.is_selected = false;
				this.Invalidate();
				this.OnUIASelectionChanged();
			}
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000F6FE4 File Offset: 0x000F51E4
		internal virtual ToolStrip GetTopLevelToolStrip()
		{
			if (this.Parent != null)
			{
				return this.Parent.GetTopLevelToolStrip();
			}
			return null;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000F7000 File Offset: 0x000F5200
		private void LayoutTextBeforeOrAfterImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, ContentAlignment textAlign, ContentAlignment imageAlign, out Rectangle textRect, out Rectangle imageRect)
		{
			int num = 0;
			int num2 = textSize.Width + num + imageSize.Width;
			int num3 = totalArea.Width - num2;
			int num4 = 0;
			HorizontalAlignment horizontalAlignment = this.GetHorizontalAlignment(textAlign);
			HorizontalAlignment horizontalAlignment2 = this.GetHorizontalAlignment(imageAlign);
			if (horizontalAlignment2 == HorizontalAlignment.Left)
			{
				num4 = 0;
			}
			else if (horizontalAlignment2 == HorizontalAlignment.Right && horizontalAlignment == HorizontalAlignment.Right)
			{
				num4 = num3;
			}
			else if (horizontalAlignment2 == HorizontalAlignment.Center && (horizontalAlignment == HorizontalAlignment.Left || horizontalAlignment == HorizontalAlignment.Center))
			{
				num4 += num3 / 3;
			}
			else
			{
				num4 += 2 * (num3 / 3);
			}
			Rectangle rectangle;
			Rectangle rectangle2;
			if (textFirst)
			{
				rectangle..ctor(totalArea.Left + num4, this.AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
				rectangle2..ctor(rectangle.Right + num, this.AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
			}
			else
			{
				rectangle2..ctor(totalArea.Left + num4, this.AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
				rectangle..ctor(rectangle2.Right + num, this.AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
			}
			textRect = rectangle;
			imageRect = rectangle2;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000F7174 File Offset: 0x000F5374
		private HorizontalAlignment GetHorizontalAlignment(ContentAlignment align)
		{
			switch (align)
			{
			case 1:
				break;
			case 2:
				return HorizontalAlignment.Center;
			default:
				if (align != 16)
				{
					if (align == 32)
					{
						return HorizontalAlignment.Center;
					}
					if (align == 64)
					{
						return HorizontalAlignment.Right;
					}
					if (align != 256)
					{
						if (align == 512)
						{
							return HorizontalAlignment.Center;
						}
						if (align != 1024)
						{
							return HorizontalAlignment.Left;
						}
						return HorizontalAlignment.Right;
					}
				}
				break;
			case 4:
				return HorizontalAlignment.Right;
			}
			return HorizontalAlignment.Left;
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000F71E0 File Offset: 0x000F53E0
		internal Size GetImageSize()
		{
			if (this.image_scaling == ToolStripItemImageScaling.None)
			{
				if (this.image != null)
				{
					return this.image.Size;
				}
				if ((this.image_index >= 0 || !string.IsNullOrEmpty(this.image_key)) && this.owner != null && this.owner.ImageList != null)
				{
					return this.owner.ImageList.ImageSize;
				}
			}
			else
			{
				if (this.Parent == null)
				{
					return Size.Empty;
				}
				if (this.image != null)
				{
					return this.Parent.ImageScalingSize;
				}
				if ((this.image_index >= 0 || !string.IsNullOrEmpty(this.image_key)) && this.owner != null && this.owner.ImageList != null)
				{
					return this.Parent.ImageScalingSize;
				}
			}
			return Size.Empty;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000F72CC File Offset: 0x000F54CC
		internal string GetToolTip()
		{
			if (this.auto_tool_tip && string.IsNullOrEmpty(this.tool_tip_text))
			{
				return this.Text;
			}
			return this.tool_tip_text;
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000F7304 File Offset: 0x000F5504
		internal void FireEvent(EventArgs e, ToolStripItemEventType met)
		{
			if (!this.Enabled && met != ToolStripItemEventType.Paint)
			{
				return;
			}
			switch (met)
			{
			case ToolStripItemEventType.MouseDown:
				this.OnMouseDown((MouseEventArgs)e);
				break;
			case ToolStripItemEventType.MouseEnter:
				this.OnMouseEnter(e);
				break;
			case ToolStripItemEventType.MouseHover:
				this.OnMouseHover(e);
				break;
			case ToolStripItemEventType.MouseLeave:
				this.OnMouseLeave(e);
				break;
			case ToolStripItemEventType.MouseMove:
				this.OnMouseMove((MouseEventArgs)e);
				break;
			case ToolStripItemEventType.MouseUp:
				this.HandleClick(e);
				this.OnMouseUp((MouseEventArgs)e);
				break;
			case ToolStripItemEventType.Paint:
				this.OnPaint((PaintEventArgs)e);
				break;
			case ToolStripItemEventType.Click:
				this.HandleClick(e);
				break;
			}
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000F73D0 File Offset: 0x000F55D0
		internal virtual void HandleClick(EventArgs e)
		{
			this.Parent.HandleItemClick(this);
			this.OnClick(e);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000F73E8 File Offset: 0x000F55E8
		internal virtual void SetPlacement(ToolStripItemPlacement placement)
		{
			this.placement = placement;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000F73F4 File Offset: 0x000F55F4
		private void BeginAnimation()
		{
			if (this.image != null && ImageAnimator.CanAnimate(this.image))
			{
				this.frame_handler = new EventHandler(this.OnAnimateImage);
				ImageAnimator.Animate(this.image, this.frame_handler);
			}
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000F7440 File Offset: 0x000F5640
		private void OnAnimateImage(object sender, EventArgs e)
		{
			if (this.Parent == null || !this.Parent.IsHandleCreated)
			{
				return;
			}
			this.Parent.BeginInvoke(new EventHandler(this.UpdateAnimatedImage), new object[] { this, e });
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000F7490 File Offset: 0x000F5690
		private void StopAnimation()
		{
			if (this.frame_handler == null)
			{
				return;
			}
			ImageAnimator.StopAnimate(this.image, this.frame_handler);
			this.frame_handler = null;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000F74C4 File Offset: 0x000F56C4
		private void UpdateAnimatedImage(object sender, EventArgs e)
		{
			if (this.Parent == null || !this.Parent.IsHandleCreated)
			{
				return;
			}
			ImageAnimator.UpdateFrames(this.image);
			this.Invalidate();
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x000F7500 File Offset: 0x000F5700
		internal bool ShowMargin
		{
			get
			{
				if (!this.IsOnDropDown)
				{
					return true;
				}
				if (!(this.Owner is ToolStripDropDownMenu))
				{
					return false;
				}
				ToolStripDropDownMenu toolStripDropDownMenu = (ToolStripDropDownMenu)this.Owner;
				return toolStripDropDownMenu.ShowCheckMargin || toolStripDropDownMenu.ShowImageMargin;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06003DDE RID: 15838 RVA: 0x000F754C File Offset: 0x000F574C
		internal bool UseImageMargin
		{
			get
			{
				if (!this.IsOnDropDown)
				{
					return true;
				}
				if (!(this.Owner is ToolStripDropDownMenu))
				{
					return false;
				}
				ToolStripDropDownMenu toolStripDropDownMenu = (ToolStripDropDownMenu)this.Owner;
				return toolStripDropDownMenu.ShowImageMargin || toolStripDropDownMenu.ShowCheckMargin;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x000F7598 File Offset: 0x000F5798
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x000F75A0 File Offset: 0x000F57A0
		internal virtual bool InternalVisible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
				this.Invalidate();
			}
		}

		// Token: 0x17001029 RID: 4137
		// (set) Token: 0x06003DE1 RID: 15841 RVA: 0x000F75B0 File Offset: 0x000F57B0
		internal ToolStrip InternalOwner
		{
			set
			{
				if (this.owner != value)
				{
					this.owner = value;
					this.CalculateAutoSize();
					this.OnOwnerChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x000F75E4 File Offset: 0x000F57E4
		// (set) Token: 0x06003DE3 RID: 15843 RVA: 0x000F75F4 File Offset: 0x000F57F4
		internal Point Location
		{
			get
			{
				return this.bounds.Location;
			}
			set
			{
				if (this.bounds.Location != value)
				{
					this.bounds.Location = value;
					this.OnLocationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x000F7624 File Offset: 0x000F5824
		// (set) Token: 0x06003DE5 RID: 15845 RVA: 0x000F7634 File Offset: 0x000F5834
		internal int Top
		{
			get
			{
				return this.bounds.Y;
			}
			set
			{
				if (this.bounds.Y != value)
				{
					this.bounds.Y = value;
					this.OnLocationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x000F766C File Offset: 0x000F586C
		// (set) Token: 0x06003DE7 RID: 15847 RVA: 0x000F767C File Offset: 0x000F587C
		internal int Left
		{
			get
			{
				return this.bounds.X;
			}
			set
			{
				if (this.bounds.X != value)
				{
					this.bounds.X = value;
					this.OnLocationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x000F76B4 File Offset: 0x000F58B4
		internal int Right
		{
			get
			{
				return this.bounds.Right;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x000F76C4 File Offset: 0x000F58C4
		internal int Bottom
		{
			get
			{
				return this.bounds.Bottom;
			}
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000F76D4 File Offset: 0x000F58D4
		internal void OnUIASelectionChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripItem.UIASelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x04001A92 RID: 6802
		private AccessibleObject accessibility_object;

		// Token: 0x04001A93 RID: 6803
		private string accessible_default_action_description;

		// Token: 0x04001A94 RID: 6804
		private bool allow_drop;

		// Token: 0x04001A95 RID: 6805
		private ToolStripItemAlignment alignment;

		// Token: 0x04001A96 RID: 6806
		private AnchorStyles anchor;

		// Token: 0x04001A97 RID: 6807
		private bool available;

		// Token: 0x04001A98 RID: 6808
		private bool auto_size;

		// Token: 0x04001A99 RID: 6809
		private bool auto_tool_tip;

		// Token: 0x04001A9A RID: 6810
		private Color back_color;

		// Token: 0x04001A9B RID: 6811
		private Image background_image;

		// Token: 0x04001A9C RID: 6812
		private ImageLayout background_image_layout;

		// Token: 0x04001A9D RID: 6813
		private Rectangle bounds;

		// Token: 0x04001A9E RID: 6814
		private bool can_select;

		// Token: 0x04001A9F RID: 6815
		private ToolStripItemDisplayStyle display_style;

		// Token: 0x04001AA0 RID: 6816
		private DockStyle dock;

		// Token: 0x04001AA1 RID: 6817
		private bool double_click_enabled;

		// Token: 0x04001AA2 RID: 6818
		private bool enabled;

		// Token: 0x04001AA3 RID: 6819
		private Size explicit_size;

		// Token: 0x04001AA4 RID: 6820
		private Font font;

		// Token: 0x04001AA5 RID: 6821
		private Color fore_color;

		// Token: 0x04001AA6 RID: 6822
		private Image image;

		// Token: 0x04001AA7 RID: 6823
		private ContentAlignment image_align;

		// Token: 0x04001AA8 RID: 6824
		private int image_index;

		// Token: 0x04001AA9 RID: 6825
		private string image_key;

		// Token: 0x04001AAA RID: 6826
		private ToolStripItemImageScaling image_scaling;

		// Token: 0x04001AAB RID: 6827
		private Color image_transparent_color;

		// Token: 0x04001AAC RID: 6828
		private bool is_disposed;

		// Token: 0x04001AAD RID: 6829
		internal bool is_pressed;

		// Token: 0x04001AAE RID: 6830
		private bool is_selected;

		// Token: 0x04001AAF RID: 6831
		private Padding margin;

		// Token: 0x04001AB0 RID: 6832
		private MergeAction merge_action;

		// Token: 0x04001AB1 RID: 6833
		private int merge_index;

		// Token: 0x04001AB2 RID: 6834
		private string name;

		// Token: 0x04001AB3 RID: 6835
		private ToolStripItemOverflow overflow;

		// Token: 0x04001AB4 RID: 6836
		private ToolStrip owner;

		// Token: 0x04001AB5 RID: 6837
		internal ToolStripItem owner_item;

		// Token: 0x04001AB6 RID: 6838
		private Padding padding;

		// Token: 0x04001AB7 RID: 6839
		private ToolStripItemPlacement placement;

		// Token: 0x04001AB8 RID: 6840
		private RightToLeft right_to_left;

		// Token: 0x04001AB9 RID: 6841
		private bool right_to_left_auto_mirror_image;

		// Token: 0x04001ABA RID: 6842
		private object tag;

		// Token: 0x04001ABB RID: 6843
		private string text;

		// Token: 0x04001ABC RID: 6844
		private ContentAlignment text_align;

		// Token: 0x04001ABD RID: 6845
		private ToolStripTextDirection text_direction;

		// Token: 0x04001ABE RID: 6846
		private TextImageRelation text_image_relation;

		// Token: 0x04001ABF RID: 6847
		private string tool_tip_text;

		// Token: 0x04001AC0 RID: 6848
		private bool visible;

		// Token: 0x04001AC1 RID: 6849
		private EventHandler frame_handler;

		// Token: 0x04001AC2 RID: 6850
		private ToolStrip parent;

		// Token: 0x04001AC3 RID: 6851
		private Size text_size;

		/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStripItem" /> for users with impairments.</summary>
		// Token: 0x02000355 RID: 853
		[ComVisible(true)]
		public class ToolStripItemAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" /> class.</summary>
			/// <param name="ownerItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that owns this <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" />. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="ownerItem" /> parameter is null. </exception>
			// Token: 0x06003DEB RID: 15851 RVA: 0x000F770C File Offset: 0x000F590C
			public ToolStripItemAccessibleObject(ToolStripItem ownerItem)
			{
				if (ownerItem == null)
				{
					throw new ArgumentNullException("ownerItem");
				}
				this.owner_item = ownerItem;
				this.default_action = string.Empty;
				this.keyboard_shortcut = string.Empty;
				this.name = string.Empty;
				this.value = string.Empty;
			}

			/// <summary>Gets the bounds of the accessible object, in screen coordinates.</summary>
			/// <returns>An object of type <see cref="T:System.Drawing.Rectangle" /> representing the bounds.</returns>
			// Token: 0x1700102F RID: 4143
			// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000F7764 File Offset: 0x000F5964
			public override Rectangle Bounds
			{
				get
				{
					return (!this.owner_item.Visible) ? Rectangle.Empty : this.owner_item.Bounds;
				}
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
			/// <returns>A description of the default action of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
			// Token: 0x17001030 RID: 4144
			// (get) Token: 0x06003DED RID: 15853 RVA: 0x000F778C File Offset: 0x000F598C
			public override string DefaultAction
			{
				get
				{
					return base.DefaultAction;
				}
			}

			/// <summary>Gets the description of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</summary>
			/// <returns>A string describing the <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" />.</returns>
			// Token: 0x17001031 RID: 4145
			// (get) Token: 0x06003DEE RID: 15854 RVA: 0x000F7794 File Offset: 0x000F5994
			public override string Description
			{
				get
				{
					return base.Description;
				}
			}

			/// <summary>Gets the description of what the object does or how the object is used.</summary>
			/// <returns>A string describing what the object does or how the object is used.</returns>
			// Token: 0x17001032 RID: 4146
			// (get) Token: 0x06003DEF RID: 15855 RVA: 0x000F779C File Offset: 0x000F599C
			public override string Help
			{
				get
				{
					return base.Help;
				}
			}

			/// <summary>Gets the shortcut key or access key for the accessible object.</summary>
			/// <returns>The shortcut key or access key for the accessible object, or null if there is no shortcut key associated with the object.</returns>
			// Token: 0x17001033 RID: 4147
			// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x000F77A4 File Offset: 0x000F59A4
			public override string KeyboardShortcut
			{
				get
				{
					return base.KeyboardShortcut;
				}
			}

			/// <summary>Gets or sets the name of the accessible object.</summary>
			/// <returns>The object name, or null if the property has not been set.</returns>
			// Token: 0x17001034 RID: 4148
			// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x000F77AC File Offset: 0x000F59AC
			// (set) Token: 0x06003DF2 RID: 15858 RVA: 0x000F77D8 File Offset: 0x000F59D8
			public override string Name
			{
				get
				{
					if (this.name == string.Empty)
					{
						return this.owner_item.Text;
					}
					return base.Name;
				}
				set
				{
					base.Name = value;
				}
			}

			/// <summary>Gets or sets the parent of an accessible object.</summary>
			/// <returns>An object of type <see cref="T:System.Windows.Forms.AccessibleObject" /> representing the parent.</returns>
			// Token: 0x17001035 RID: 4149
			// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000F77E4 File Offset: 0x000F59E4
			public override AccessibleObject Parent
			{
				get
				{
					return base.Parent;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</returns>
			// Token: 0x17001036 RID: 4150
			// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x000F77EC File Offset: 0x000F59EC
			public override AccessibleRole Role
			{
				get
				{
					return base.Role;
				}
			}

			/// <summary>Gets the state of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None" /> if no state has been set.</returns>
			// Token: 0x17001037 RID: 4151
			// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x000F77F4 File Offset: 0x000F59F4
			public override AccessibleStates State
			{
				get
				{
					return base.State;
				}
			}

			/// <summary>Adds a <see cref="P:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject.State" /> if <see cref="T:System.Windows.Forms.AccessibleStates" /> is <see cref="F:System.Windows.Forms.AccessibleStates.None" />.</summary>
			/// <param name="state">One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values other than <see cref="F:System.Windows.Forms.AccessibleStates.None" />.</param>
			// Token: 0x06003DF6 RID: 15862 RVA: 0x000F77FC File Offset: 0x000F59FC
			public void AddState(AccessibleStates state)
			{
				this.state = state;
			}

			/// <summary>Performs the default action associated with this accessible object. </summary>
			// Token: 0x06003DF7 RID: 15863 RVA: 0x000F7808 File Offset: 0x000F5A08
			public override void DoDefaultAction()
			{
				base.DoDefaultAction();
			}

			/// <summary>Gets an identifier for a Help topic and the path to the Help file associated with this accessible object.</summary>
			/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter will contain the path to the Help file associated with this accessible object, or null if there is no IAccessible interface specified.</returns>
			/// <param name="fileName">When this method returns, contains a string that represents the path to the Help file associated with this accessible object. This parameter is passed without being initialized. </param>
			// Token: 0x06003DF8 RID: 15864 RVA: 0x000F7810 File Offset: 0x000F5A10
			public override int GetHelpTopic(out string fileName)
			{
				return base.GetHelpTopic(out fileName);
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents one of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</returns>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" />  values.</param>
			// Token: 0x06003DF9 RID: 15865 RVA: 0x000F781C File Offset: 0x000F5A1C
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				return base.Navigate(navigationDirection);
			}

			/// <returns>A string that represents the current object.</returns>
			// Token: 0x06003DFA RID: 15866 RVA: 0x000F7828 File Offset: 0x000F5A28
			public override string ToString()
			{
				return string.Format("ToolStripItemAccessibleObject: Owner = {0}", this.owner_item.ToString());
			}

			// Token: 0x04001ADF RID: 6879
			internal ToolStripItem owner_item;
		}
	}
}
