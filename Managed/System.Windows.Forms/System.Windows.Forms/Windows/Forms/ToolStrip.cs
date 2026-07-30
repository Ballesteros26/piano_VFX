using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides a container for Windows toolbar objects. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200033C RID: 828
	[DesignerSerializer("System.Windows.Forms.Design.ToolStripCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ClassInterface(1)]
	[DefaultEvent("ItemClicked")]
	[DefaultProperty("Items")]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.ToolStripDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ToolStrip : ScrollableControl, IDisposable, IComponent, IToolStripData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStrip" /> class.</summary>
		// Token: 0x060039C0 RID: 14784 RVA: 0x000ED650 File Offset: 0x000EB850
		public ToolStrip()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStrip" /> class with the specified array of <see cref="T:System.Windows.Forms.ToolStripItem" />s.</summary>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.ToolStripItem" /> objects.</param>
		// Token: 0x060039C1 RID: 14785 RVA: 0x000ED65C File Offset: 0x000EB85C
		public ToolStrip(params ToolStripItem[] items)
		{
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SuspendLayout();
			this.items = new ToolStripItemCollection(this, items, true);
			this.allow_merge = true;
			base.AutoSize = true;
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
			this.back_color = Control.DefaultBackColor;
			this.can_overflow = true;
			base.CausesValidation = false;
			this.default_drop_down_direction = ToolStripDropDownDirection.BelowRight;
			this.displayed_items = new ToolStripItemCollection(this, null, true);
			this.Dock = this.DefaultDock;
			base.Font = new Font("Tahoma", 8.25f);
			this.fore_color = Control.DefaultForeColor;
			this.grip_margin = this.DefaultGripMargin;
			this.grip_style = ToolStripGripStyle.Visible;
			this.image_scaling_size = new Size(16, 16);
			this.layout_style = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.orientation = Orientation.Horizontal;
			if (!(this is ToolStripDropDown))
			{
				this.overflow_button = new ToolStripOverflowButton(this);
			}
			this.renderer = null;
			this.render_mode = ToolStripRenderMode.ManagerRenderMode;
			this.show_item_tool_tips = this.DefaultShowItemToolTips;
			base.TabStop = false;
			this.text_direction = ToolStripTextDirection.Horizontal;
			base.ResumeLayout();
			ToolStripManager.AddToolStrip(this);
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000ED7A0 File Offset: 0x000EB9A0
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStrip()
		{
			ToolStrip.BeginDragEvent = new object();
			ToolStrip.EndDragEvent = new object();
			ToolStrip.ItemAddedEvent = new object();
			ToolStrip.ItemClickedEvent = new object();
			ToolStrip.ItemRemovedEvent = new object();
			ToolStrip.LayoutCompletedEvent = new object();
			ToolStrip.LayoutStyleChangedEvent = new object();
			ToolStrip.PaintGripEvent = new object();
			ToolStrip.RendererChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStrip.AutoSize" /> property has changed.</summary>
		// Token: 0x14000356 RID: 854
		// (add) Token: 0x060039C3 RID: 14787 RVA: 0x000ED808 File Offset: 0x000EBA08
		// (remove) Token: 0x060039C4 RID: 14788 RVA: 0x000ED814 File Offset: 0x000EBA14
		[Browsable(true)]
		[EditorBrowsable(0)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Occurs when the user begins to drag the <see cref="T:System.Windows.Forms.ToolStrip" /> control.</summary>
		// Token: 0x14000357 RID: 855
		// (add) Token: 0x060039C5 RID: 14789 RVA: 0x000ED820 File Offset: 0x000EBA20
		// (remove) Token: 0x060039C6 RID: 14790 RVA: 0x000ED834 File Offset: 0x000EBA34
		[MonoTODO("Event never raised")]
		public event EventHandler BeginDrag
		{
			add
			{
				base.Events.AddHandler(ToolStrip.BeginDragEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.BeginDragEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStrip.CausesValidation" /> property changes.</summary>
		// Token: 0x14000358 RID: 856
		// (add) Token: 0x060039C7 RID: 14791 RVA: 0x000ED848 File Offset: 0x000EBA48
		// (remove) Token: 0x060039C8 RID: 14792 RVA: 0x000ED854 File Offset: 0x000EBA54
		[Browsable(false)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000359 RID: 857
		// (add) Token: 0x060039C9 RID: 14793 RVA: 0x000ED860 File Offset: 0x000EBA60
		// (remove) Token: 0x060039CA RID: 14794 RVA: 0x000ED86C File Offset: 0x000EBA6C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event ControlEventHandler ControlAdded
		{
			add
			{
				base.ControlAdded += value;
			}
			remove
			{
				base.ControlAdded -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400035A RID: 858
		// (add) Token: 0x060039CB RID: 14795 RVA: 0x000ED878 File Offset: 0x000EBA78
		// (remove) Token: 0x060039CC RID: 14796 RVA: 0x000ED884 File Offset: 0x000EBA84
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event ControlEventHandler ControlRemoved
		{
			add
			{
				base.ControlRemoved += value;
			}
			remove
			{
				base.ControlRemoved -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="T:System.Windows.Forms.Cursor" /> property changes.</summary>
		// Token: 0x1400035B RID: 859
		// (add) Token: 0x060039CD RID: 14797 RVA: 0x000ED890 File Offset: 0x000EBA90
		// (remove) Token: 0x060039CE RID: 14798 RVA: 0x000ED89C File Offset: 0x000EBA9C
		[Browsable(false)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>Occurs when the user stops dragging the <see cref="T:System.Windows.Forms.ToolStrip" /> control.</summary>
		// Token: 0x1400035C RID: 860
		// (add) Token: 0x060039CF RID: 14799 RVA: 0x000ED8A8 File Offset: 0x000EBAA8
		// (remove) Token: 0x060039D0 RID: 14800 RVA: 0x000ED8BC File Offset: 0x000EBABC
		[MonoTODO("Event never raised")]
		public event EventHandler EndDrag
		{
			add
			{
				base.Events.AddHandler(ToolStrip.EndDragEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.EndDragEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.ForeColor" /> property changes.</summary>
		// Token: 0x1400035D RID: 861
		// (add) Token: 0x060039D1 RID: 14801 RVA: 0x000ED8D0 File Offset: 0x000EBAD0
		// (remove) Token: 0x060039D2 RID: 14802 RVA: 0x000ED8DC File Offset: 0x000EBADC
		[Browsable(false)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>Occurs when a new <see cref="T:System.Windows.Forms.ToolStripItem" /> is added to the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400035E RID: 862
		// (add) Token: 0x060039D3 RID: 14803 RVA: 0x000ED8E8 File Offset: 0x000EBAE8
		// (remove) Token: 0x060039D4 RID: 14804 RVA: 0x000ED8FC File Offset: 0x000EBAFC
		public event ToolStripItemEventHandler ItemAdded
		{
			add
			{
				base.Events.AddHandler(ToolStrip.ItemAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.ItemAddedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</summary>
		// Token: 0x1400035F RID: 863
		// (add) Token: 0x060039D5 RID: 14805 RVA: 0x000ED910 File Offset: 0x000EBB10
		// (remove) Token: 0x060039D6 RID: 14806 RVA: 0x000ED924 File Offset: 0x000EBB24
		public event ToolStripItemClickedEventHandler ItemClicked
		{
			add
			{
				base.Events.AddHandler(ToolStrip.ItemClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.ItemClickedEvent, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ToolStripItem" /> is removed from the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000360 RID: 864
		// (add) Token: 0x060039D7 RID: 14807 RVA: 0x000ED938 File Offset: 0x000EBB38
		// (remove) Token: 0x060039D8 RID: 14808 RVA: 0x000ED94C File Offset: 0x000EBB4C
		public event ToolStripItemEventHandler ItemRemoved
		{
			add
			{
				base.Events.AddHandler(ToolStrip.ItemRemovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.ItemRemovedEvent, value);
			}
		}

		/// <summary>Occurs when the layout of the <see cref="T:System.Windows.Forms.ToolStrip" /> is complete.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000361 RID: 865
		// (add) Token: 0x060039D9 RID: 14809 RVA: 0x000ED960 File Offset: 0x000EBB60
		// (remove) Token: 0x060039DA RID: 14810 RVA: 0x000ED974 File Offset: 0x000EBB74
		public event EventHandler LayoutCompleted
		{
			add
			{
				base.Events.AddHandler(ToolStrip.LayoutCompletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.LayoutCompletedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.LayoutStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000362 RID: 866
		// (add) Token: 0x060039DB RID: 14811 RVA: 0x000ED988 File Offset: 0x000EBB88
		// (remove) Token: 0x060039DC RID: 14812 RVA: 0x000ED99C File Offset: 0x000EBB9C
		public event EventHandler LayoutStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStrip.LayoutStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.LayoutStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle is painted.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000363 RID: 867
		// (add) Token: 0x060039DD RID: 14813 RVA: 0x000ED9B0 File Offset: 0x000EBBB0
		// (remove) Token: 0x060039DE RID: 14814 RVA: 0x000ED9C4 File Offset: 0x000EBBC4
		public event PaintEventHandler PaintGrip
		{
			add
			{
				base.Events.AddHandler(ToolStrip.PaintGripEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.PaintGripEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.Renderer" /> property changes.</summary>
		// Token: 0x14000364 RID: 868
		// (add) Token: 0x060039DF RID: 14815 RVA: 0x000ED9D8 File Offset: 0x000EBBD8
		// (remove) Token: 0x060039E0 RID: 14816 RVA: 0x000ED9EC File Offset: 0x000EBBEC
		public event EventHandler RendererChanged
		{
			add
			{
				base.Events.AddHandler(ToolStrip.RendererChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStrip.RendererChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.</summary>
		/// <returns>true to control drag-and-drop and item reordering through events that you implement; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.ToolStrip.AllowDrop" /> and <see cref="P:System.Windows.Forms.ToolStrip.AllowItemReorder" /> are both set to true. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000EDA00 File Offset: 0x000EBC00
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x000EDA08 File Offset: 0x000EBC08
		[MonoTODO("Stub, does nothing")]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether drag-and-drop and item reordering are handled privately by the <see cref="T:System.Windows.Forms.ToolStrip" /> class.</summary>
		/// <returns>true to cause the <see cref="T:System.Windows.Forms.ToolStrip" /> class to handle drag-and-drop and item reordering automatically; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.ToolStrip.AllowDrop" /> and <see cref="P:System.Windows.Forms.ToolStrip.AllowItemReorder" /> are both set to true. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000EDA14 File Offset: 0x000EBC14
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x000EDA1C File Offset: 0x000EBC1C
		[DefaultValue(false)]
		[MonoTODO("Stub, does nothing")]
		public bool AllowItemReorder
		{
			get
			{
				return this.allow_item_reorder;
			}
			set
			{
				this.allow_item_reorder = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether multiple <see cref="T:System.Windows.Forms.MenuStrip" />, <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />, <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, and other types can be combined. </summary>
		/// <returns>true if combining of types is allowed; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000EDA28 File Offset: 0x000EBC28
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x000EDA30 File Offset: 0x000EBC30
		[DefaultValue(true)]
		public bool AllowMerge
		{
			get
			{
				return this.allow_merge;
			}
			set
			{
				this.allow_merge = value;
			}
		}

		/// <summary>Gets or sets the edges of the container to which a <see cref="T:System.Windows.Forms.ToolStrip" /> is bound and determines how a <see cref="T:System.Windows.Forms.ToolStrip" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000EDA3C File Offset: 0x000EBC3C
		// (set) Token: 0x060039E8 RID: 14824 RVA: 0x000EDA44 File Offset: 0x000EBC44
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true to automatically scroll; otherwise, false.</returns>
		/// <exception cref="T:System.NotSupportedException">Automatic scrolling is not supported by <see cref="T:System.Windows.Forms.ToolStrip" /> controls.</exception>
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000EDA50 File Offset: 0x000EBC50
		// (set) Token: 0x060039EA RID: 14826 RVA: 0x000EDA58 File Offset: 0x000EBC58
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000EDA64 File Offset: 0x000EBC64
		// (set) Token: 0x060039EC RID: 14828 RVA: 0x000EDA6C File Offset: 0x000EBC6C
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000EDA78 File Offset: 0x000EBC78
		// (set) Token: 0x060039EE RID: 14830 RVA: 0x000EDA80 File Offset: 0x000EBC80
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> value.</returns>
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x000EDA8C File Offset: 0x000EBC8C
		// (set) Token: 0x060039F0 RID: 14832 RVA: 0x000EDA94 File Offset: 0x000EBC94
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new Point AutoScrollPosition
		{
			get
			{
				return base.AutoScrollPosition;
			}
			set
			{
				base.AutoScrollPosition = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
		/// <returns>true if the control adjusts its width to closely fit its contents; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000EDAA0 File Offset: 0x000EBCA0
		// (set) Token: 0x060039F2 RID: 14834 RVA: 0x000EDAA8 File Offset: 0x000EBCA8
		[DefaultValue(true)]
		[Browsable(true)]
		[EditorBrowsable(0)]
		[DesignerSerializationVisibility(1)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the <see cref="T:System.Windows.Forms.ToolStrip" />. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x000EDAB4 File Offset: 0x000EBCB4
		// (set) Token: 0x060039F4 RID: 14836 RVA: 0x000EDABC File Offset: 0x000EBCBC
		public new Color BackColor
		{
			get
			{
				return this.back_color;
			}
			set
			{
				this.back_color = value;
			}
		}

		/// <summary>Gets or sets the binding context for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x000EDAC8 File Offset: 0x000EBCC8
		// (set) Token: 0x060039F6 RID: 14838 RVA: 0x000EDAD0 File Offset: 0x000EBCD0
		public override BindingContext BindingContext
		{
			get
			{
				return base.BindingContext;
			}
			set
			{
				base.BindingContext = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether items in the <see cref="T:System.Windows.Forms.ToolStrip" /> can be sent to an overflow menu.</summary>
		/// <returns>true to send <see cref="T:System.Windows.Forms.ToolStrip" /> items to an overflow menu; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x000EDADC File Offset: 0x000EBCDC
		// (set) Token: 0x060039F8 RID: 14840 RVA: 0x000EDAE4 File Offset: 0x000EBCE4
		[DefaultValue(true)]
		public bool CanOverflow
		{
			get
			{
				return this.can_overflow;
			}
			set
			{
				this.can_overflow = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStrip" /> causes validation to be performed on any controls that require validation when it receives focus.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x000EDAF0 File Offset: 0x000EBCF0
		// (set) Token: 0x060039FA RID: 14842 RVA: 0x000EDAF8 File Offset: 0x000EBCF8
		[Browsable(false)]
		[DefaultValue(false)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" /> representing the collection of controls contained within the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000EDB04 File Offset: 0x000EBD04
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Gets or sets the cursor that is displayed when the mouse pointer is over the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x000EDB0C File Offset: 0x000EBD0C
		// (set) Token: 0x060039FD RID: 14845 RVA: 0x000EDB14 File Offset: 0x000EBD14
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>Gets or sets a value representing the default direction in which a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is displayed relative to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</exception>
		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000EDB20 File Offset: 0x000EBD20
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x000EDB28 File Offset: 0x000EBD28
		[Browsable(false)]
		public virtual ToolStripDropDownDirection DefaultDropDownDirection
		{
			get
			{
				return this.default_drop_down_direction;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripDropDownDirection), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripDropDownDirection", value));
				}
				this.default_drop_down_direction = value;
			}
		}

		/// <summary>Retrieves the current display rectangle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the <see cref="T:System.Windows.Forms.ToolStrip" /> area for item layout.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000EDB64 File Offset: 0x000EBD64
		public override Rectangle DisplayRectangle
		{
			get
			{
				if (this.orientation == Orientation.Horizontal)
				{
					if (this.grip_style == ToolStripGripStyle.Hidden || this.layout_style == ToolStripLayoutStyle.Flow || this.layout_style == ToolStripLayoutStyle.Table)
					{
						return new Rectangle(base.Padding.Left, base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical);
					}
					return new Rectangle(this.GripRectangle.Right + this.GripMargin.Right, base.Padding.Top, base.Width - base.Padding.Horizontal - this.GripRectangle.Right - this.GripMargin.Right, base.Height - base.Padding.Vertical);
				}
				else
				{
					if (this.grip_style == ToolStripGripStyle.Hidden || this.layout_style == ToolStripLayoutStyle.Flow || this.layout_style == ToolStripLayoutStyle.Table)
					{
						return new Rectangle(base.Padding.Left, base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical);
					}
					return new Rectangle(base.Padding.Left, this.GripRectangle.Bottom + this.GripMargin.Bottom + base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical - this.GripRectangle.Bottom - this.GripMargin.Bottom);
				}
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.ToolStrip" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.ToolStrip" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default value is <see cref="F:System.Windows.Forms.DockStyle.Top" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000EDD70 File Offset: 0x000EBF70
		// (set) Token: 0x06003A02 RID: 14850 RVA: 0x000EDD78 File Offset: 0x000EBF78
		[DefaultValue(DockStyle.Top)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (base.Dock != value)
				{
					base.Dock = value;
					switch (value)
					{
					case DockStyle.None:
					case DockStyle.Top:
					case DockStyle.Bottom:
						this.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
						break;
					case DockStyle.Left:
					case DockStyle.Right:
						this.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
						break;
					}
				}
			}
		}

		/// <summary>Gets or sets the font used to display text in the control.</summary>
		/// <returns>The current default font.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000EDDD4 File Offset: 0x000EBFD4
		// (set) Token: 0x06003A04 RID: 14852 RVA: 0x000EDDDC File Offset: 0x000EBFDC
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (base.Font != value)
				{
					base.Font = value;
					foreach (object obj in this.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj;
						toolStripItem.OnOwnerFontChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000EDE64 File Offset: 0x000EC064
		// (set) Token: 0x06003A06 RID: 14854 RVA: 0x000EDE6C File Offset: 0x000EC06C
		[Browsable(false)]
		public new Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
			set
			{
				if (this.fore_color != value)
				{
					this.fore_color = value;
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the orientation of the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values. Possible values are <see cref="F:System.Windows.Forms.ToolStripGripDisplayStyle.Horizontal" /> and <see cref="F:System.Windows.Forms.ToolStripGripDisplayStyle.Vertical" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000EDE94 File Offset: 0x000EC094
		[Browsable(false)]
		public ToolStripGripDisplayStyle GripDisplayStyle
		{
			get
			{
				return (this.orientation != Orientation.Vertical) ? ToolStripGripDisplayStyle.Vertical : ToolStripGripDisplayStyle.Horizontal;
			}
		}

		/// <summary>Gets or sets the space around the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" />, which represents the spacing.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000EDEAC File Offset: 0x000EC0AC
		// (set) Token: 0x06003A09 RID: 14857 RVA: 0x000EDEB4 File Offset: 0x000EC0B4
		public Padding GripMargin
		{
			get
			{
				return this.grip_margin;
			}
			set
			{
				if (this.grip_margin != value)
				{
					this.grip_margin = value;
					base.PerformLayout();
				}
			}
		}

		/// <summary>Gets the boundaries of the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
		/// <returns>An object of type <see cref="T:System.Drawing.Rectangle" />, representing the move handle boundaries. If the boundaries are not visible, the <see cref="P:System.Windows.Forms.ToolStrip.GripRectangle" /> property returns <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x000EDED4 File Offset: 0x000EC0D4
		[Browsable(false)]
		public Rectangle GripRectangle
		{
			get
			{
				if (this.grip_style == ToolStripGripStyle.Hidden)
				{
					return Rectangle.Empty;
				}
				if (this.orientation == Orientation.Horizontal)
				{
					return new Rectangle(this.grip_margin.Left + base.Padding.Left, base.Padding.Top, 3, base.Height);
				}
				return new Rectangle(base.Padding.Left, this.grip_margin.Top + base.Padding.Top, base.Width, 3);
			}
		}

		/// <summary>Gets or sets whether the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle is visible or hidden.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default value is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Visible" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000EDF68 File Offset: 0x000EC168
		// (set) Token: 0x06003A0C RID: 14860 RVA: 0x000EDF70 File Offset: 0x000EC170
		[DefaultValue(ToolStripGripStyle.Visible)]
		public ToolStripGripStyle GripStyle
		{
			get
			{
				return this.grip_style;
			}
			set
			{
				if (this.grip_style != value)
				{
					if (!Enum.IsDefined(typeof(ToolStripGripStyle), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripGripStyle", value));
					}
					this.grip_style = value;
					base.PerformLayout(this, "GripStyle");
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStrip" /> has children; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000EDFCC File Offset: 0x000EC1CC
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new bool HasChildren
		{
			get
			{
				return base.HasChildren;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An instance of the <see cref="T:System.Windows.Forms.HScrollProperties" /> class, which provides basic properties for an <see cref="T:System.Windows.Forms.HScrollBar" />.</returns>
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000EDFD4 File Offset: 0x000EC1D4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new HScrollProperties HorizontalScroll
		{
			get
			{
				return base.HorizontalScroll;
			}
		}

		/// <summary>Gets or sets the image list that contains the image displayed on a <see cref="T:System.Windows.Forms.ToolStrip" /> item.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x000EDFDC File Offset: 0x000EC1DC
		// (set) Token: 0x06003A10 RID: 14864 RVA: 0x000EDFE4 File Offset: 0x000EC1E4
		[DefaultValue(null)]
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
			set
			{
				this.image_list = value;
			}
		}

		/// <summary>Gets or sets the size, in pixels, of an image used on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value representing the size of the image, in pixels. The default is 16 x 16 pixels.</returns>
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000EDFF0 File Offset: 0x000EC1F0
		// (set) Token: 0x06003A12 RID: 14866 RVA: 0x000EDFF8 File Offset: 0x000EC1F8
		[DefaultValue("{Width=16, Height=16}")]
		public Size ImageScalingSize
		{
			get
			{
				return this.image_scaling_size;
			}
			set
			{
				this.image_scaling_size = value;
			}
		}

		/// <summary>Gets a value indicating whether the user is currently moving the <see cref="T:System.Windows.Forms.ToolStrip" /> from one <see cref="T:System.Windows.Forms.ToolStripContainer" /> to another. </summary>
		/// <returns>true if the user is currently moving the <see cref="T:System.Windows.Forms.ToolStrip" /> from one <see cref="T:System.Windows.Forms.ToolStripContainer" /> to another; otherwise, false.</returns>
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06003A13 RID: 14867 RVA: 0x000EE004 File Offset: 0x000EC204
		[MonoTODO("Always returns false, dragging not implemented yet.")]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public bool IsCurrentlyDragging
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStrip" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStrip" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000EE008 File Offset: 0x000EC208
		[Browsable(false)]
		public bool IsDropDown
		{
			get
			{
				return this is ToolStripDropDown;
			}
		}

		/// <summary>Gets all the items that belong to a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.ToolStripItemCollection" />, representing all the elements contained by a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000EE018 File Offset: 0x000EC218
		[DesignerSerializationVisibility(2)]
		[MergableProperty(false)]
		public virtual ToolStripItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Passes a reference to the cached <see cref="P:System.Windows.Forms.Control.LayoutEngine" /> returned by the layout engine interface.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> that represents the cached layout engine returned by the layout engine interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000EE020 File Offset: 0x000EC220
		public override LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new ToolStripSplitStackLayout();
				}
				return this.layout_engine;
			}
		}

		/// <summary>Gets or sets layout scheme characteristics.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LayoutSettings" /> representing the layout scheme characteristics.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06003A17 RID: 14871 RVA: 0x000EE040 File Offset: 0x000EC240
		// (set) Token: 0x06003A18 RID: 14872 RVA: 0x000EE048 File Offset: 0x000EC248
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[DefaultValue(null)]
		public LayoutSettings LayoutSettings
		{
			get
			{
				return this.layout_settings;
			}
			set
			{
				if (this.layout_settings != value)
				{
					this.layout_settings = value;
					base.PerformLayout(this, "LayoutSettings");
				}
			}
		}

		/// <summary>Gets or sets a value indicating how the <see cref="T:System.Windows.Forms.ToolStrip" /> lays out the items collection.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The possible values are <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow" />, and <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="P:System.Windows.Forms.ToolStrip.LayoutStyle" /> is not one of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x000EE06C File Offset: 0x000EC26C
		// (set) Token: 0x06003A1A RID: 14874 RVA: 0x000EE074 File Offset: 0x000EC274
		[AmbientValue(ToolStripLayoutStyle.StackWithOverflow)]
		public ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				return this.layout_style;
			}
			set
			{
				if (this.layout_style != value)
				{
					if (!Enum.IsDefined(typeof(ToolStripLayoutStyle), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripLayoutStyle", value));
					}
					this.layout_style = value;
					if (this.layout_style == ToolStripLayoutStyle.Flow)
					{
						this.layout_engine = new FlowLayout();
					}
					else
					{
						this.layout_engine = new ToolStripSplitStackLayout();
					}
					if (this.layout_style == ToolStripLayoutStyle.StackWithOverflow)
					{
						if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
						{
							this.layout_style = ToolStripLayoutStyle.VerticalStackWithOverflow;
						}
						else
						{
							this.layout_style = ToolStripLayoutStyle.HorizontalStackWithOverflow;
						}
					}
					if (this.layout_style == ToolStripLayoutStyle.HorizontalStackWithOverflow)
					{
						this.orientation = Orientation.Horizontal;
					}
					else if (this.layout_style == ToolStripLayoutStyle.VerticalStackWithOverflow)
					{
						this.orientation = Orientation.Vertical;
					}
					this.layout_settings = this.CreateLayoutSettings(value);
					base.PerformLayout(this, "LayoutStyle");
					this.OnLayoutStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the orientation of the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values. The default is <see cref="F:System.Windows.Forms.Orientation.Horizontal" />.</returns>
		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06003A1B RID: 14875 RVA: 0x000EE170 File Offset: 0x000EC370
		[Browsable(false)]
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the overflow button for a <see cref="T:System.Windows.Forms.ToolStrip" /> with overflow enabled.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> with its <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> set to <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Right" /> and its <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> value set to <see cref="F:System.Windows.Forms.ToolStripItemOverflow.Never" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000EE178 File Offset: 0x000EC378
		[Browsable(false)]
		[EditorBrowsable(2)]
		public ToolStripOverflowButton OverflowButton
		{
			get
			{
				return this.overflow_button;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the look and feel of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the look and feel of a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x000EE180 File Offset: 0x000EC380
		// (set) Token: 0x06003A1E RID: 14878 RVA: 0x000EE19C File Offset: 0x000EC39C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ToolStripRenderer Renderer
		{
			get
			{
				if (this.render_mode == ToolStripRenderMode.ManagerRenderMode)
				{
					return ToolStripManager.Renderer;
				}
				return this.renderer;
			}
			set
			{
				if (this.renderer != value)
				{
					this.renderer = value;
					this.render_mode = ToolStripRenderMode.Custom;
					base.PerformLayout(this, "Renderer");
					this.OnRendererChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates which visual styles will be applied to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A value that indicates the visual style to apply. The default is <see cref="F:System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value being set is not one of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> is set to <see cref="F:System.Windows.Forms.ToolStripRenderMode.Custom" /> without the <see cref="P:System.Windows.Forms.ToolStrip.Renderer" /> property being assigned to a new instance of <see cref="T:System.Windows.Forms.ToolStripRenderer" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000EE1D0 File Offset: 0x000EC3D0
		// (set) Token: 0x06003A20 RID: 14880 RVA: 0x000EE1D8 File Offset: 0x000EC3D8
		public ToolStripRenderMode RenderMode
		{
			get
			{
				return this.render_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripRenderMode), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripRenderMode", value));
				}
				if (value == ToolStripRenderMode.Custom && this.renderer == null)
				{
					throw new NotSupportedException("Must set Renderer property before setting RenderMode to Custom");
				}
				if (value == ToolStripRenderMode.Professional)
				{
					this.Renderer = new ToolStripProfessionalRenderer();
				}
				else if (value == ToolStripRenderMode.System)
				{
					this.Renderer = new ToolStripSystemRenderer();
				}
				this.render_mode = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether ToolTips are to be displayed on <see cref="T:System.Windows.Forms.ToolStrip" /> items. </summary>
		/// <returns>true if ToolTips are to be displayed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000EE264 File Offset: 0x000EC464
		// (set) Token: 0x06003A22 RID: 14882 RVA: 0x000EE26C File Offset: 0x000EC46C
		[DefaultValue(true)]
		public bool ShowItemToolTips
		{
			get
			{
				return this.show_item_tool_tips;
			}
			set
			{
				this.show_item_tool_tips = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStrip" /> stretches from end to end in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStrip" /> stretches from end to end in its <see cref="T:System.Windows.Forms.ToolStripContainer" />; otherwise, false. The default is false.</returns>
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000EE278 File Offset: 0x000EC478
		// (set) Token: 0x06003A24 RID: 14884 RVA: 0x000EE280 File Offset: 0x000EC480
		[DefaultValue(false)]
		public bool Stretch
		{
			get
			{
				return this.stretch;
			}
			set
			{
				this.stretch = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to an item in the <see cref="T:System.Windows.Forms.ToolStrip" /> using the TAB key.</summary>
		/// <returns>true if the user can give the focus to an item in the <see cref="T:System.Windows.Forms.ToolStrip" /> using the TAB key; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000EE28C File Offset: 0x000EC48C
		// (set) Token: 0x06003A26 RID: 14886 RVA: 0x000EE294 File Offset: 0x000EC494
		[DefaultValue(false)]
		[DispId(-516)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
				base.SetStyle(ControlStyles.Selectable, value);
			}
		}

		/// <summary>Gets or sets the direction in which to draw text on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripTextDirection.Horizontal" />. </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000EE2AC File Offset: 0x000EC4AC
		// (set) Token: 0x06003A28 RID: 14888 RVA: 0x000EE2B4 File Offset: 0x000EC4B4
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public virtual ToolStripTextDirection TextDirection
		{
			get
			{
				return this.text_direction;
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
					base.PerformLayout(this, "TextDirection");
					base.Invalidate();
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An instance of the <see cref="T:System.Windows.Forms.VScrollProperties" /> class, which provides basic properties for a <see cref="T:System.Windows.Forms.VScrollBar" />.</returns>
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000EE318 File Offset: 0x000EC518
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new VScrollProperties VerticalScroll
		{
			get
			{
				return base.VerticalScroll;
			}
		}

		/// <summary>Gets the docking location of the <see cref="T:System.Windows.Forms.ToolStrip" />, indicating which borders are docked to the container.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Top" />.</returns>
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000EE320 File Offset: 0x000EC520
		protected virtual DockStyle DefaultDock
		{
			get
			{
				return DockStyle.Top;
			}
		}

		/// <summary>Gets the default spacing, in pixels, between the sizing grip and the edges of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Forms.Padding" /> values representing the spacing, in pixels.</returns>
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000EE324 File Offset: 0x000EC524
		protected virtual Padding DefaultGripMargin
		{
			get
			{
				return new Padding(2);
			}
		}

		/// <summary>Gets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStrip" /> and the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Padding" /> values. The default is <see cref="F:System.Windows.Forms.Padding.Empty" />.</returns>
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000EE32C File Offset: 0x000EC52C
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the contents of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value of (0, 0, 1, 0).</returns>
		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x000EE334 File Offset: 0x000EC534
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(0, 0, 1, 0);
			}
		}

		/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.ToolStrip" /> by default.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x000EE340 File Offset: 0x000EC540
		protected virtual bool DefaultShowItemToolTips
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06003A2F RID: 14895 RVA: 0x000EE344 File Offset: 0x000EC544
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 25);
			}
		}

		/// <summary>Gets the subset of items that are currently displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />, including items that are automatically added into the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> representing the items that are currently displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000EE350 File Offset: 0x000EC550
		protected internal virtual ToolStripItemCollection DisplayedItems
		{
			get
			{
				return this.displayed_items;
			}
		}

		/// <summary>Gets the maximum height and width, in pixels, of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the control, in pixels.</returns>
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003A31 RID: 14897 RVA: 0x000EE358 File Offset: 0x000EC558
		protected internal virtual Size MaxItemSize
		{
			get
			{
				return new Size(base.Width - ((this.GripStyle != ToolStripGripStyle.Hidden) ? 8 : 1), base.Height);
			}
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" />.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.Point" />.</param>
		// Token: 0x06003A32 RID: 14898 RVA: 0x000EE38C File Offset: 0x000EC58C
		[EditorBrowsable(1)]
		public new Control GetChildAtPoint(Point point)
		{
			return base.GetChildAtPoint(point);
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" />.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> value.</param>
		/// <param name="skipValue">A <see cref="T:System.Windows.Forms.GetChildAtPointSkip" />  value.</param>
		// Token: 0x06003A33 RID: 14899 RVA: 0x000EE398 File Offset: 0x000EC598
		[EditorBrowsable(1)]
		public new Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
		{
			return base.GetChildAtPoint(pt, skipValue);
		}

		/// <summary>Returns the item located at the specified point in the client area of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> at the specified location, or null if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is not found.</returns>
		/// <param name="point">The <see cref="T:System.Drawing.Point" /> at which to search for the <see cref="T:System.Windows.Forms.ToolStripItem" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003A34 RID: 14900 RVA: 0x000EE3A4 File Offset: 0x000EC5A4
		public ToolStripItem GetItemAt(Point point)
		{
			foreach (object obj in this.displayed_items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Visible && toolStripItem.Bounds.Contains(point))
				{
					return toolStripItem;
				}
			}
			return null;
		}

		/// <summary>Returns the item located at the specified x- and y-coordinates of the <see cref="T:System.Windows.Forms.ToolStrip" /> client area.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> located at the specified location, or null if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is not found.</returns>
		/// <param name="x">The horizontal coordinate, in pixels, from the left edge of the client area. </param>
		/// <param name="y">The vertical coordinate, in pixels, from the top edge of the client area. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003A35 RID: 14901 RVA: 0x000EE438 File Offset: 0x000EC638
		public ToolStripItem GetItemAt(int x, int y)
		{
			return this.GetItemAt(new Point(x, y));
		}

		/// <summary>Retrieves the next <see cref="T:System.Windows.Forms.ToolStripItem" /> from the specified reference point and moving in the specified direction.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that is specified by the <paramref name="start" /> parameter and is next in the order as specified by the <paramref name="direction" /> parameter.</returns>
		/// <param name="start">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the reference point from which to begin the retrieval of the next item.</param>
		/// <param name="direction">One of the values of <see cref="T:System.Windows.Forms.ArrowDirection" /> that specifies the direction to move.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value of the <paramref name="direction" /> parameter is not one of the values of <see cref="T:System.Windows.Forms.ArrowDirection" />.</exception>
		// Token: 0x06003A36 RID: 14902 RVA: 0x000EE448 File Offset: 0x000EC648
		public virtual ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction)
		{
			if (!Enum.IsDefined(typeof(ArrowDirection), direction))
			{
				throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ArrowDirection", direction));
			}
			ToolStripItem toolStripItem = null;
			if (direction != ArrowDirection.Left)
			{
				if (direction != ArrowDirection.Up)
				{
					if (direction != ArrowDirection.Right)
					{
						if (direction == ArrowDirection.Down)
						{
							int num = int.MaxValue;
							if (start != null)
							{
								foreach (object obj in this.DisplayedItems)
								{
									ToolStripItem toolStripItem2 = (ToolStripItem)obj;
									if (toolStripItem2.Top >= start.Bottom && toolStripItem2.Bottom < num && toolStripItem2.Visible && toolStripItem2.CanSelect)
									{
										toolStripItem = toolStripItem2;
										num = toolStripItem2.Top;
									}
								}
							}
							if (toolStripItem == null)
							{
								foreach (object obj2 in this.DisplayedItems)
								{
									ToolStripItem toolStripItem3 = (ToolStripItem)obj2;
									if (toolStripItem3.Top < num && toolStripItem3.Visible && toolStripItem3.CanSelect)
									{
										toolStripItem = toolStripItem3;
										num = toolStripItem3.Top;
									}
								}
							}
						}
					}
					else
					{
						int num = int.MaxValue;
						if (start != null)
						{
							foreach (object obj3 in this.DisplayedItems)
							{
								ToolStripItem toolStripItem4 = (ToolStripItem)obj3;
								if (toolStripItem4.Left >= start.Right && toolStripItem4.Left < num && toolStripItem4.Visible && toolStripItem4.CanSelect)
								{
									toolStripItem = toolStripItem4;
									num = toolStripItem4.Left;
								}
							}
						}
						if (toolStripItem == null)
						{
							foreach (object obj4 in this.DisplayedItems)
							{
								ToolStripItem toolStripItem5 = (ToolStripItem)obj4;
								if (toolStripItem5.Left < num && toolStripItem5.Visible && toolStripItem5.CanSelect)
								{
									toolStripItem = toolStripItem5;
									num = toolStripItem5.Left;
								}
							}
						}
					}
				}
				else
				{
					int num = int.MinValue;
					if (start != null)
					{
						foreach (object obj5 in this.DisplayedItems)
						{
							ToolStripItem toolStripItem6 = (ToolStripItem)obj5;
							if (toolStripItem6.Bottom <= start.Top && toolStripItem6.Top > num && toolStripItem6.Visible && toolStripItem6.CanSelect)
							{
								toolStripItem = toolStripItem6;
								num = toolStripItem6.Top;
							}
						}
					}
					if (toolStripItem == null)
					{
						foreach (object obj6 in this.DisplayedItems)
						{
							ToolStripItem toolStripItem7 = (ToolStripItem)obj6;
							if (toolStripItem7.Top > num && toolStripItem7.Visible && toolStripItem7.CanSelect)
							{
								toolStripItem = toolStripItem7;
								num = toolStripItem7.Top;
							}
						}
					}
				}
			}
			else
			{
				int num = int.MinValue;
				if (start != null)
				{
					foreach (object obj7 in this.DisplayedItems)
					{
						ToolStripItem toolStripItem8 = (ToolStripItem)obj7;
						if (toolStripItem8.Right <= start.Left && toolStripItem8.Left > num && toolStripItem8.Visible && toolStripItem8.CanSelect)
						{
							toolStripItem = toolStripItem8;
							num = toolStripItem8.Left;
						}
					}
				}
				if (toolStripItem == null)
				{
					foreach (object obj8 in this.DisplayedItems)
					{
						ToolStripItem toolStripItem9 = (ToolStripItem)obj8;
						if (toolStripItem9.Left > num && toolStripItem9.Visible && toolStripItem9.CanSelect)
						{
							toolStripItem = toolStripItem9;
							num = toolStripItem9.Left;
						}
					}
				}
			}
			return toolStripItem;
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003A37 RID: 14903 RVA: 0x000EE9C8 File Offset: 0x000ECBC8
		[EditorBrowsable(1)]
		public void ResetMinimumSize()
		{
			this.MinimumSize = new Size(-1, -1);
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="x">An <see cref="T:System.Int32" />.</param>
		/// <param name="y">An <see cref="T:System.Int32" />.</param>
		// Token: 0x06003A38 RID: 14904 RVA: 0x000EE9D8 File Offset: 0x000ECBD8
		[EditorBrowsable(1)]
		public new void SetAutoScrollMargin(int x, int y)
		{
			base.SetAutoScrollMargin(x, y);
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000EE9E4 File Offset: 0x000ECBE4
		public override string ToString()
		{
			return string.Format("{0}, Name: {1}, Items: {2}", base.ToString(), base.Name, this.items.Count.ToString());
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStrip" /> item.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStrip" /> item.</returns>
		// Token: 0x06003A3A RID: 14906 RVA: 0x000EEA1C File Offset: 0x000ECC1C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStrip.ToolStripAccessibleObject(this);
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000EEA24 File Offset: 0x000ECC24
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return base.CreateControlsInstance();
		}

		/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.ToolStrip" /> instance.</summary>
		/// <returns>A <see cref="M:System.Windows.Forms.ToolStripButton.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
		/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</param>
		// Token: 0x06003A3C RID: 14908 RVA: 0x000EEA2C File Offset: 0x000ECC2C
		protected internal virtual ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			if (text == "-")
			{
				return new ToolStripSeparator();
			}
			if (this is ToolStripDropDown)
			{
				return new ToolStripMenuItem(text, image, onClick);
			}
			return new ToolStripButton(text, image, onClick);
		}

		/// <summary>Specifies the visual arrangement for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The default is null.</returns>
		/// <param name="layoutStyle">The visual arrangement to be applied to the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
		// Token: 0x06003A3D RID: 14909 RVA: 0x000EEA6C File Offset: 0x000ECC6C
		protected virtual LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle layoutStyle)
		{
			switch (layoutStyle)
			{
			case ToolStripLayoutStyle.Flow:
				return new FlowLayoutSettings(this);
			}
			return null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStrip" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003A3E RID: 14910 RVA: 0x000EEAA4 File Offset: 0x000ECCA4
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				for (int i = this.Items.Count - 1; i >= 0; i--)
				{
					this.Items[i].Dispose();
				}
				if (this.overflow_button != null && this.overflow_button.drop_down != null)
				{
					this.overflow_button.drop_down.Dispose();
				}
				ToolStripManager.RemoveToolStrip(this);
				base.Dispose(disposing);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.BeginDrag" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A3F RID: 14911 RVA: 0x000EEB24 File Offset: 0x000ECD24
		[MonoTODO("Stub, never called")]
		protected virtual void OnBeginDrag(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.BeginDragEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A40 RID: 14912 RVA: 0x000EEB58 File Offset: 0x000ECD58
		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.EndDrag" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A41 RID: 14913 RVA: 0x000EEB64 File Offset: 0x000ECD64
		[MonoTODO("Stub, never called")]
		protected virtual void OnEndDrag(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.EndDragEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Determines whether a character is an input character that the item recognizes.</summary>
		/// <returns>true if the character should be sent directly to the item and not preprocessed; otherwise, false.</returns>
		/// <param name="charCode">The character to test.</param>
		// Token: 0x06003A42 RID: 14914 RVA: 0x000EEB98 File Offset: 0x000ECD98
		protected override bool IsInputChar(char charCode)
		{
			return base.IsInputChar(charCode);
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x06003A43 RID: 14915 RVA: 0x000EEBA4 File Offset: 0x000ECDA4
		protected override bool IsInputKey(Keys keyData)
		{
			return base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="P:System.Windows.Forms.Control.Enabled" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A44 RID: 14916 RVA: 0x000EEBB0 File Offset: 0x000ECDB0
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				toolStripItem.OnParentEnabledChanged(EventArgs.Empty);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A45 RID: 14917 RVA: 0x000EEC2C File Offset: 0x000ECE2C
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A46 RID: 14918 RVA: 0x000EEC38 File Offset: 0x000ECE38
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A47 RID: 14919 RVA: 0x000EEC44 File Offset: 0x000ECE44
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <param name="e">An <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A48 RID: 14920 RVA: 0x000EEC50 File Offset: 0x000ECE50
		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemAdded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06003A49 RID: 14921 RVA: 0x000EEC5C File Offset: 0x000ECE5C
		protected internal virtual void OnItemAdded(ToolStripItemEventArgs e)
		{
			if (e.Item.InternalVisible)
			{
				e.Item.Available = true;
			}
			e.Item.SetPlacement(ToolStripItemPlacement.Main);
			if (base.Created)
			{
				base.PerformLayout();
			}
			ToolStripItemEventHandler toolStripItemEventHandler = (ToolStripItemEventHandler)base.Events[ToolStrip.ItemAddedEvent];
			if (toolStripItemEventHandler != null)
			{
				toolStripItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A4A RID: 14922 RVA: 0x000EECC8 File Offset: 0x000ECEC8
		protected virtual void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			if (this.KeyboardActive)
			{
				ToolStripManager.SetActiveToolStrip(null, false);
			}
			ToolStripItemClickedEventHandler toolStripItemClickedEventHandler = (ToolStripItemClickedEventHandler)base.Events[ToolStrip.ItemClickedEvent];
			if (toolStripItemClickedEventHandler != null)
			{
				toolStripItemClickedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemRemoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06003A4B RID: 14923 RVA: 0x000EED0C File Offset: 0x000ECF0C
		protected internal virtual void OnItemRemoved(ToolStripItemEventArgs e)
		{
			ToolStripItemEventHandler toolStripItemEventHandler = (ToolStripItemEventHandler)base.Events[ToolStrip.ItemRemovedEvent];
			if (toolStripItemEventHandler != null)
			{
				toolStripItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A4C RID: 14924 RVA: 0x000EED40 File Offset: 0x000ECF40
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			this.SetDisplayedItems();
			this.OnLayoutCompleted(EventArgs.Empty);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.LayoutCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A4D RID: 14925 RVA: 0x000EED60 File Offset: 0x000ECF60
		protected virtual void OnLayoutCompleted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.LayoutCompletedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.LayoutStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A4E RID: 14926 RVA: 0x000EED94 File Offset: 0x000ECF94
		protected virtual void OnLayoutStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.LayoutStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A4F RID: 14927 RVA: 0x000EEDC8 File Offset: 0x000ECFC8
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A50 RID: 14928 RVA: 0x000EEDD4 File Offset: 0x000ECFD4
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A51 RID: 14929 RVA: 0x000EEDE0 File Offset: 0x000ECFE0
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A52 RID: 14930 RVA: 0x000EEDEC File Offset: 0x000ECFEC
		protected override void OnMouseDown(MouseEventArgs mea)
		{
			if (this.mouse_currently_over != null)
			{
				ToolStripItem currentlyFocusedItem = this.GetCurrentlyFocusedItem();
				if (currentlyFocusedItem != null && currentlyFocusedItem != this.mouse_currently_over)
				{
					this.FocusInternal(true);
				}
				if (this is MenuStrip && !this.menu_selected)
				{
					(this as MenuStrip).FireMenuActivate();
					this.menu_selected = true;
				}
				this.mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseDown);
				if (this is MenuStrip && this.mouse_currently_over is ToolStripMenuItem && !(this.mouse_currently_over as ToolStripMenuItem).HasDropDownItems)
				{
					return;
				}
			}
			else
			{
				this.HideMenus(true, ToolStripDropDownCloseReason.AppClicked);
			}
			if (this is MenuStrip)
			{
				base.Capture = false;
			}
			base.OnMouseDown(mea);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A53 RID: 14931 RVA: 0x000EEEB4 File Offset: 0x000ED0B4
		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.mouse_currently_over != null)
			{
				this.MouseLeftItem(this.mouse_currently_over);
				this.mouse_currently_over.FireEvent(e, ToolStripItemEventType.MouseLeave);
				this.mouse_currently_over = null;
			}
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A54 RID: 14932 RVA: 0x000EEEF4 File Offset: 0x000ED0F4
		protected override void OnMouseMove(MouseEventArgs mea)
		{
			ToolStripItem itemAt;
			if (this.overflow_button != null && this.overflow_button.Visible && this.overflow_button.Bounds.Contains(mea.Location))
			{
				itemAt = this.overflow_button;
			}
			else
			{
				itemAt = this.GetItemAt(mea.X, mea.Y);
			}
			if (itemAt != null)
			{
				if (itemAt == this.mouse_currently_over)
				{
					itemAt.FireEvent(mea, ToolStripItemEventType.MouseMove);
				}
				else
				{
					if (this.mouse_currently_over != null)
					{
						this.MouseLeftItem(itemAt);
						this.mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseLeave);
					}
					this.mouse_currently_over = itemAt;
					itemAt.FireEvent(mea, ToolStripItemEventType.MouseEnter);
					this.MouseEnteredItem(itemAt);
					itemAt.FireEvent(mea, ToolStripItemEventType.MouseMove);
					if (this.menu_selected && this.mouse_currently_over.Enabled && this.mouse_currently_over is ToolStripDropDownItem && (this.mouse_currently_over as ToolStripDropDownItem).HasDropDownItems)
					{
						(this.mouse_currently_over as ToolStripDropDownItem).ShowDropDown();
					}
				}
			}
			else if (this.mouse_currently_over != null)
			{
				this.MouseLeftItem(itemAt);
				this.mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseLeave);
				this.mouse_currently_over = null;
			}
			base.OnMouseMove(mea);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A55 RID: 14933 RVA: 0x000EF038 File Offset: 0x000ED238
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			if (this.mouse_currently_over != null && !(this.mouse_currently_over is ToolStripControlHost) && this.mouse_currently_over.Enabled)
			{
				this.OnItemClicked(new ToolStripItemClickedEventArgs(this.mouse_currently_over));
				if (this.mouse_currently_over != null)
				{
					this.mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseUp);
				}
				if (this.mouse_currently_over == null)
				{
					return;
				}
			}
			base.OnMouseUp(mea);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A56 RID: 14934 RVA: 0x000EF0AC File Offset: 0x000ED2AC
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			this.OnPaintGrip(e);
			for (int i = 0; i < this.displayed_items.Count; i++)
			{
				ToolStripItem toolStripItem = this.displayed_items[i];
				if (toolStripItem.Visible)
				{
					e.Graphics.TranslateTransform((float)toolStripItem.Bounds.Left, (float)toolStripItem.Bounds.Top);
					toolStripItem.FireEvent(e, ToolStripItemEventType.Paint);
					e.Graphics.ResetTransform();
				}
			}
			if (this.overflow_button != null && this.overflow_button.Visible)
			{
				e.Graphics.TranslateTransform((float)this.overflow_button.Bounds.Left, (float)this.overflow_button.Bounds.Top);
				this.overflow_button.FireEvent(e, ToolStripItemEventType.Paint);
				e.Graphics.ResetTransform();
			}
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, base.Size);
			ToolStripRenderEventArgs toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, rectangle, Color.Empty);
			toolStripRenderEventArgs.InternalConnectedArea = this.CalculateConnectedArea();
			this.Renderer.DrawToolStripBorder(toolStripRenderEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event for the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint. </param>
		// Token: 0x06003A57 RID: 14935 RVA: 0x000EF1E0 File Offset: 0x000ED3E0
		[EditorBrowsable(2)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, base.Size);
			ToolStripRenderEventArgs toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, rectangle, SystemColors.Control);
			this.Renderer.DrawToolStripBackground(toolStripRenderEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.PaintGrip" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A58 RID: 14936 RVA: 0x000EF228 File Offset: 0x000ED428
		protected internal virtual void OnPaintGrip(PaintEventArgs e)
		{
			if (this.layout_style == ToolStripLayoutStyle.Flow || this.layout_style == ToolStripLayoutStyle.Table)
			{
				return;
			}
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[ToolStrip.PaintGripEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
			if (!(this is MenuStrip))
			{
				if (this.orientation == Orientation.Horizontal)
				{
					e.Graphics.TranslateTransform(2f, 0f);
				}
				else
				{
					e.Graphics.TranslateTransform(0f, 2f);
				}
			}
			this.Renderer.DrawGrip(new ToolStripGripRenderEventArgs(e.Graphics, this, this.GripRectangle, this.GripDisplayStyle, this.grip_style));
			e.Graphics.ResetTransform();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.RendererChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A59 RID: 14937 RVA: 0x000EF2EC File Offset: 0x000ED4EC
		protected virtual void OnRendererChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStrip.RendererChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A5A RID: 14938 RVA: 0x000EF320 File Offset: 0x000ED520
		[EditorBrowsable(2)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				toolStripItem.OnParentRightToLeftChanged(e);
			}
		}

		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
		// Token: 0x06003A5B RID: 14939 RVA: 0x000EF398 File Offset: 0x000ED598
		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabStopChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A5C RID: 14940 RVA: 0x000EF3A4 File Offset: 0x000ED5A4
		protected override void OnTabStopChanged(EventArgs e)
		{
			base.OnTabStopChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003A5D RID: 14941 RVA: 0x000EF3B0 File Offset: 0x000ED5B0
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x06003A5E RID: 14942 RVA: 0x000EF3BC File Offset: 0x000ED5BC
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return base.ProcessCmdKey(ref m, keyData);
		}

		/// <summary>Processes a dialog box key.</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003A5F RID: 14943 RVA: 0x000EF3C8 File Offset: 0x000ED5C8
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (!this.KeyboardActive)
			{
				return false;
			}
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.ProcessDialogKey(keyData))
				{
					return true;
				}
			}
			if (this.ProcessArrowKey(keyData))
			{
				return true;
			}
			ToolStrip toolStrip = null;
			switch (keyData)
			{
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				if (this.GetCurrentlySelectedItem() is ToolStripControlHost)
				{
					return false;
				}
				break;
			default:
				if (keyData == Keys.Escape)
				{
					this.Dismiss(ToolStripDropDownCloseReason.Keyboard);
					return true;
				}
				if (keyData == (Keys.LButton | Keys.Back | Keys.Control))
				{
					toolStrip = ToolStripManager.GetNextToolStrip(this, true);
					if (toolStrip != null)
					{
						foreach (object obj2 in this.Items)
						{
							ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
							toolStripItem2.Dismiss(ToolStripDropDownCloseReason.Keyboard);
						}
						ToolStripManager.SetActiveToolStrip(toolStrip, true);
						toolStrip.SelectNextToolStripItem(null, true);
					}
					return true;
				}
				if (keyData == (Keys.LButton | Keys.Back | Keys.Shift | Keys.Control))
				{
					toolStrip = ToolStripManager.GetNextToolStrip(this, false);
					if (toolStrip != null)
					{
						foreach (object obj3 in this.Items)
						{
							ToolStripItem toolStripItem3 = (ToolStripItem)obj3;
							toolStripItem3.Dismiss(ToolStripDropDownCloseReason.Keyboard);
						}
						ToolStripManager.SetActiveToolStrip(toolStrip, true);
						toolStrip.SelectNextToolStripItem(null, true);
					}
					return true;
				}
				break;
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06003A60 RID: 14944 RVA: 0x000EF5E0 File Offset: 0x000ED7E0
		protected override bool ProcessMnemonic(char charCode)
		{
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Enabled && toolStripItem.Visible && !string.IsNullOrEmpty(toolStripItem.Text) && Control.IsMnemonic(charCode, toolStripItem.Text))
				{
					return toolStripItem.ProcessMnemonic(charCode);
				}
			}
			string text = char.ToUpper(charCode).ToString();
			if ((Control.ModifierKeys & Keys.Alt) != Keys.None || this is ToolStripDropDownMenu)
			{
				foreach (object obj2 in this.Items)
				{
					ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
					if (toolStripItem2.Enabled && toolStripItem2.Visible && !string.IsNullOrEmpty(toolStripItem2.Text) && toolStripItem2.Text.ToUpper().StartsWith(text) && !(toolStripItem2 is ToolStripControlHost))
					{
						return toolStripItem2.ProcessMnemonic(charCode);
					}
				}
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Controls the return location of the focus.</summary>
		// Token: 0x06003A61 RID: 14945 RVA: 0x000EF778 File Offset: 0x000ED978
		[MonoTODO("Stub, does nothing")]
		[EditorBrowsable(2)]
		protected virtual void RestoreFocus()
		{
		}

		/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
		/// <param name="directed">true to specify the direction of the control to select; otherwise, false.</param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order.</param>
		// Token: 0x06003A62 RID: 14946 RVA: 0x000EF77C File Offset: 0x000ED97C
		protected override void Select(bool directed, bool forward)
		{
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.CanSelect)
				{
					toolStripItem.Select();
					break;
				}
			}
		}

		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x06003A63 RID: 14947 RVA: 0x000EF7FC File Offset: 0x000ED9FC
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
		// Token: 0x06003A64 RID: 14948 RVA: 0x000EF80C File Offset: 0x000EDA0C
		protected virtual void SetDisplayedItems()
		{
			this.displayed_items.Clear();
			foreach (object obj in this.items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Placement == ToolStripItemPlacement.Main && toolStripItem.Available)
				{
					this.displayed_items.AddNoOwnerOrLayout(toolStripItem);
					toolStripItem.Parent = this;
				}
				else if (toolStripItem.Placement == ToolStripItemPlacement.Overflow)
				{
					toolStripItem.Parent = this.OverflowButton.DropDown;
				}
			}
			if (this.OverflowButton != null)
			{
				this.OverflowButton.DropDown.SetDisplayedItems();
			}
		}

		/// <summary>Anchors a <see cref="T:System.Windows.Forms.ToolStripItem" /> to a particular place on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to anchor.</param>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> representing the x and y client coordinates of the <see cref="T:System.Windows.Forms.ToolStripItem" /> location, in pixels.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="item" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Windows.Forms.ToolStrip" /> is not the owner of the <see cref="T:System.Windows.Forms.ToolStripItem" /> referred to by the <paramref name="item" /> parameter.</exception>
		// Token: 0x06003A65 RID: 14949 RVA: 0x000EF8E8 File Offset: 0x000EDAE8
		protected internal void SetItemLocation(ToolStripItem item, Point location)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.Owner != this)
			{
				throw new NotSupportedException("The item is not owned by this ToolStrip");
			}
			item.SetBounds(new Rectangle(location, item.Size));
		}

		/// <summary>Enables you to change the parent <see cref="T:System.Windows.Forms.ToolStrip" /> of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> whose <see cref="P:System.Windows.Forms.Control.Parent" /> property is to be changed. </param>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.ToolStrip" /> that is the parent of the <see cref="T:System.Windows.Forms.ToolStripItem" /> referred to by the <paramref name="item" /> parameter. </param>
		// Token: 0x06003A66 RID: 14950 RVA: 0x000EF930 File Offset: 0x000EDB30
		protected internal static void SetItemParent(ToolStripItem item, ToolStrip parent)
		{
			if (item.Owner != null)
			{
				item.Owner.Items.RemoveNoOwnerOrLayout(item);
				if (item.Owner is ToolStripOverflow)
				{
					(item.Owner as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(item);
				}
			}
			parent.Items.AddNoOwnerOrLayout(item);
			item.Parent = parent;
		}

		/// <summary>Retrieves a value that sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to the specified visibility state.</summary>
		/// <param name="visible">true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is visible; otherwise, false. </param>
		// Token: 0x06003A67 RID: 14951 RVA: 0x000EF998 File Offset: 0x000EDB98
		protected override void SetVisibleCore(bool visible)
		{
			base.SetVisibleCore(visible);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003A68 RID: 14952 RVA: 0x000EF9A4 File Offset: 0x000EDBA4
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000EF9B0 File Offset: 0x000EDBB0
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x000EF9B8 File Offset: 0x000EDBB8
		internal virtual bool KeyboardActive
		{
			get
			{
				return this.keyboard_active;
			}
			set
			{
				if (this.keyboard_active != value)
				{
					this.keyboard_active = value;
					if (value)
					{
						Application.KeyboardCapture = this;
					}
					else if (Application.KeyboardCapture == this)
					{
						Application.KeyboardCapture = null;
						ToolStripManager.ActivatedByKeyboard = false;
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000EFA08 File Offset: 0x000EDC08
		internal virtual Rectangle CalculateConnectedArea()
		{
			return Rectangle.Empty;
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000EFA10 File Offset: 0x000EDC10
		internal void ChangeSelection(ToolStripItem nextItem)
		{
			if (Application.KeyboardCapture != this)
			{
				ToolStripManager.SetActiveToolStrip(this, ToolStripManager.ActivatedByKeyboard);
			}
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem != nextItem)
				{
					toolStripItem.Dismiss(ToolStripDropDownCloseReason.Keyboard);
				}
			}
			ToolStripItem currentlySelectedItem = this.GetCurrentlySelectedItem();
			if (currentlySelectedItem != null && !(currentlySelectedItem is ToolStripControlHost))
			{
				this.FocusInternal(true);
			}
			if (nextItem is ToolStripControlHost)
			{
				(nextItem as ToolStripControlHost).Focus();
			}
			nextItem.Select();
			if (nextItem.Parent is MenuStrip && (nextItem.Parent as MenuStrip).MenuDroppedDown)
			{
				(nextItem as ToolStripMenuItem).HandleAutoExpansion();
			}
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000EFB08 File Offset: 0x000EDD08
		internal virtual void Dismiss()
		{
			this.Dismiss(ToolStripDropDownCloseReason.AppClicked);
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000EFB14 File Offset: 0x000EDD14
		internal virtual void Dismiss(ToolStripDropDownCloseReason reason)
		{
			this.KeyboardActive = false;
			this.menu_selected = false;
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				toolStripItem.Dismiss(reason);
			}
			base.Invalidate();
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000EFB98 File Offset: 0x000EDD98
		internal ToolStripItem GetCurrentlySelectedItem()
		{
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Selected)
				{
					return toolStripItem;
				}
			}
			return null;
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000EFC18 File Offset: 0x000EDE18
		internal ToolStripItem GetCurrentlyFocusedItem()
		{
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem is ToolStripControlHost && (toolStripItem as ToolStripControlHost).Control.Focused)
				{
					return toolStripItem;
				}
			}
			return null;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000EFCAC File Offset: 0x000EDEAC
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			return this.GetToolStripPreferredSize(proposedSize);
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000EFCB8 File Offset: 0x000EDEB8
		internal virtual Size GetToolStripPreferredSize(Size proposedSize)
		{
			Size empty = Size.Empty;
			if (this.LayoutStyle == ToolStripLayoutStyle.Flow)
			{
				Point empty2 = Point.Empty;
				int num = 0;
				foreach (object obj in this.items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (this.DisplayRectangle.Width - empty2.X < toolStripItem.Width + toolStripItem.Margin.Horizontal)
					{
						empty2.Y += num;
						num = 0;
						empty2.X = this.DisplayRectangle.Left;
					}
					empty2.Offset(toolStripItem.Margin.Left, 0);
					num = Math.Max(num, toolStripItem.Height + toolStripItem.Margin.Vertical);
					empty2.X += toolStripItem.Width + toolStripItem.Margin.Right;
				}
				empty2.Y += num;
				return new Size(empty2.X, empty2.Y);
			}
			if (this.orientation == Orientation.Vertical)
			{
				foreach (object obj2 in this.items)
				{
					ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
					if (toolStripItem2.Available)
					{
						Size preferredSize = toolStripItem2.GetPreferredSize(Size.Empty);
						empty.Height += preferredSize.Height + toolStripItem2.Margin.Top + toolStripItem2.Margin.Bottom;
						if (empty.Width < base.Padding.Horizontal + preferredSize.Width + toolStripItem2.Margin.Horizontal)
						{
							empty.Width = base.Padding.Horizontal + preferredSize.Width + toolStripItem2.Margin.Horizontal;
						}
					}
				}
				empty.Height += this.GripRectangle.Height + this.GripMargin.Vertical + base.Padding.Vertical + 4;
				if (empty.Width == 0)
				{
					empty.Width = base.ExplicitBounds.Width;
				}
				return empty;
			}
			foreach (object obj3 in this.items)
			{
				ToolStripItem toolStripItem3 = (ToolStripItem)obj3;
				if (toolStripItem3.Available)
				{
					Size preferredSize2 = toolStripItem3.GetPreferredSize(Size.Empty);
					empty.Width += preferredSize2.Width + toolStripItem3.Margin.Left + toolStripItem3.Margin.Right;
					if (empty.Height < base.Padding.Vertical + preferredSize2.Height + toolStripItem3.Margin.Vertical)
					{
						empty.Height = base.Padding.Vertical + preferredSize2.Height + toolStripItem3.Margin.Vertical;
					}
				}
			}
			empty.Width += this.GripRectangle.Width + this.GripMargin.Horizontal + base.Padding.Horizontal + 4;
			if (empty.Height == 0)
			{
				empty.Height = base.ExplicitBounds.Height;
			}
			if (this is StatusStrip)
			{
				empty.Height = Math.Max(empty.Height, 22);
			}
			return empty;
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000F012C File Offset: 0x000EE32C
		internal virtual ToolStrip GetTopLevelToolStrip()
		{
			return this;
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000F0130 File Offset: 0x000EE330
		internal virtual void HandleItemClick(ToolStripItem dismissingItem)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.ItemClicked);
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000F0140 File Offset: 0x000EE340
		internal void HideMenus(bool release, ToolStripDropDownCloseReason reason)
		{
			if (this is MenuStrip && release && this.menu_selected)
			{
				(this as MenuStrip).FireMenuDeactivate();
			}
			if (release)
			{
				this.menu_selected = false;
			}
			this.NotifySelectedChanged(null);
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000F0180 File Offset: 0x000EE380
		internal void NotifySelectedChanged(ToolStripItem tsi)
		{
			foreach (object obj in this.DisplayedItems)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (tsi != toolStripItem && toolStripItem is ToolStripDropDownItem)
				{
					(toolStripItem as ToolStripDropDownItem).HideDropDown(ToolStripDropDownCloseReason.Keyboard);
				}
			}
			if (this.OverflowButton != null)
			{
				ToolStripItemCollection displayedItems = this.OverflowButton.DropDown.DisplayedItems;
				foreach (object obj2 in displayedItems)
				{
					ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
					if (tsi != toolStripItem2 && toolStripItem2 is ToolStripDropDownItem)
					{
						(toolStripItem2 as ToolStripDropDownItem).HideDropDown(ToolStripDropDownCloseReason.Keyboard);
					}
				}
				this.OverflowButton.HideDropDown();
			}
			foreach (object obj3 in this.Items)
			{
				ToolStripItem toolStripItem3 = (ToolStripItem)obj3;
				if (tsi != toolStripItem3)
				{
					toolStripItem3.Dismiss(ToolStripDropDownCloseReason.Keyboard);
				}
			}
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000F0318 File Offset: 0x000EE518
		internal virtual bool OnMenuKey()
		{
			return false;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000F031C File Offset: 0x000EE51C
		internal virtual bool ProcessArrowKey(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.Left:
			{
				ToolStripItem toolStripItem = this.GetCurrentlySelectedItem();
				if (toolStripItem is ToolStripControlHost)
				{
					return false;
				}
				toolStripItem = this.SelectNextToolStripItem(toolStripItem, false);
				if (toolStripItem is ToolStripControlHost)
				{
					(toolStripItem as ToolStripControlHost).Focus();
				}
				return true;
			}
			default:
			{
				ToolStripItem toolStripItem;
				if (keyData == Keys.Tab)
				{
					toolStripItem = this.GetCurrentlySelectedItem();
					toolStripItem = this.SelectNextToolStripItem(toolStripItem, true);
					if (toolStripItem is ToolStripControlHost)
					{
						(toolStripItem as ToolStripControlHost).Focus();
					}
					return true;
				}
				if (keyData != (Keys.LButton | Keys.Back | Keys.Shift))
				{
					return false;
				}
				toolStripItem = this.GetCurrentlySelectedItem();
				toolStripItem = this.SelectNextToolStripItem(toolStripItem, false);
				if (toolStripItem is ToolStripControlHost)
				{
					(toolStripItem as ToolStripControlHost).Focus();
				}
				return true;
			}
			case Keys.Right:
			{
				ToolStripItem toolStripItem = this.GetCurrentlySelectedItem();
				if (toolStripItem is ToolStripControlHost)
				{
					return false;
				}
				toolStripItem = this.SelectNextToolStripItem(toolStripItem, true);
				if (toolStripItem is ToolStripControlHost)
				{
					(toolStripItem as ToolStripControlHost).Focus();
				}
				return true;
			}
			}
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000F0414 File Offset: 0x000EE614
		internal virtual ToolStripItem SelectNextToolStripItem(ToolStripItem start, bool forward)
		{
			ToolStripItem nextItem = this.GetNextItem(start, (!forward) ? ArrowDirection.Left : ArrowDirection.Right);
			if (nextItem == null)
			{
				return nextItem;
			}
			this.ChangeSelection(nextItem);
			if (nextItem is ToolStripControlHost)
			{
				(nextItem as ToolStripControlHost).Focus();
			}
			return nextItem;
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000F0460 File Offset: 0x000EE660
		private void MouseEnteredItem(ToolStripItem item)
		{
			if (this.show_item_tool_tips && !(item is ToolStripTextBox))
			{
				this.tooltip_currently_showing = item;
				this.ToolTipTimer.Start();
			}
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000F0498 File Offset: 0x000EE698
		private void MouseLeftItem(ToolStripItem item)
		{
			this.ToolTipTimer.Stop();
			this.ToolTipWindow.Hide(this);
			this.tooltip_currently_showing = null;
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x000F04C4 File Offset: 0x000EE6C4
		private Timer ToolTipTimer
		{
			get
			{
				if (this.tooltip_timer == null)
				{
					this.tooltip_timer = new Timer();
					this.tooltip_timer.Enabled = false;
					this.tooltip_timer.Interval = 500;
					this.tooltip_timer.Tick += new EventHandler(this.ToolTipTimer_Tick);
				}
				return this.tooltip_timer;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003A7D RID: 14973 RVA: 0x000F0520 File Offset: 0x000EE720
		private ToolTip ToolTipWindow
		{
			get
			{
				if (this.tooltip_window == null)
				{
					this.tooltip_window = new ToolTip();
				}
				return this.tooltip_window;
			}
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000F0540 File Offset: 0x000EE740
		private void ToolTipTimer_Tick(object o, EventArgs args)
		{
			string toolTip = this.tooltip_currently_showing.GetToolTip();
			if (!string.IsNullOrEmpty(toolTip))
			{
				this.ToolTipWindow.Present(this, toolTip);
			}
			this.tooltip_currently_showing.FireEvent(EventArgs.Empty, ToolStripItemEventType.MouseHover);
			this.ToolTipTimer.Stop();
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000F0590 File Offset: 0x000EE790
		// (set) Token: 0x06003A80 RID: 14976 RVA: 0x000F0598 File Offset: 0x000EE798
		internal ToolStrip CurrentlyMergedWith
		{
			get
			{
				return this.currently_merged_with;
			}
			set
			{
				this.currently_merged_with = value;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000F05A4 File Offset: 0x000EE7A4
		internal List<ToolStripItem> HiddenMergedItems
		{
			get
			{
				if (this.hidden_merged_items == null)
				{
					this.hidden_merged_items = new List<ToolStripItem>();
				}
				return this.hidden_merged_items;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x000F05C4 File Offset: 0x000EE7C4
		// (set) Token: 0x06003A83 RID: 14979 RVA: 0x000F05CC File Offset: 0x000EE7CC
		internal bool IsCurrentlyMerged
		{
			get
			{
				return this.is_currently_merged;
			}
			set
			{
				this.is_currently_merged = value;
				if (!value && this is MenuStrip)
				{
					foreach (object obj in this.Items)
					{
						ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj;
						toolStripMenuItem.DropDown.IsCurrentlyMerged = value;
					}
				}
			}
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000F0658 File Offset: 0x000EE858
		internal void BeginMerge()
		{
			if (!this.IsCurrentlyMerged)
			{
				this.IsCurrentlyMerged = true;
				if (this.pre_merge_items == null)
				{
					this.pre_merge_items = new List<ToolStripItem>();
					foreach (object obj in this.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj;
						this.pre_merge_items.Add(toolStripItem);
					}
				}
			}
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000F06F4 File Offset: 0x000EE8F4
		internal void RevertMergeItem(ToolStripItem item)
		{
			if (item.Parent != null && item.Parent != this)
			{
				if (item.Parent is ToolStripOverflow)
				{
					(item.Parent as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(item);
				}
				else
				{
					item.Parent.Items.RemoveNoOwnerOrLayout(item);
				}
				item.Parent = item.Owner;
			}
			int num = item.Owner.pre_merge_items.IndexOf(item);
			for (int i = num; i < this.pre_merge_items.Count; i++)
			{
				if (this.Items.Contains(this.pre_merge_items[i]))
				{
					item.Owner.Items.InsertNoOwnerOrLayout(this.Items.IndexOf(this.pre_merge_items[i]), item);
					return;
				}
			}
			item.Owner.Items.AddNoOwnerOrLayout(item);
		}

		// Token: 0x04001A14 RID: 6676
		private bool allow_item_reorder;

		// Token: 0x04001A15 RID: 6677
		private bool allow_merge;

		// Token: 0x04001A16 RID: 6678
		private Color back_color;

		// Token: 0x04001A17 RID: 6679
		private bool can_overflow;

		// Token: 0x04001A18 RID: 6680
		private ToolStrip currently_merged_with;

		// Token: 0x04001A19 RID: 6681
		private ToolStripDropDownDirection default_drop_down_direction;

		// Token: 0x04001A1A RID: 6682
		internal ToolStripItemCollection displayed_items;

		// Token: 0x04001A1B RID: 6683
		private Color fore_color;

		// Token: 0x04001A1C RID: 6684
		private Padding grip_margin;

		// Token: 0x04001A1D RID: 6685
		private ToolStripGripStyle grip_style;

		// Token: 0x04001A1E RID: 6686
		private List<ToolStripItem> hidden_merged_items;

		// Token: 0x04001A1F RID: 6687
		private ImageList image_list;

		// Token: 0x04001A20 RID: 6688
		private Size image_scaling_size;

		// Token: 0x04001A21 RID: 6689
		private bool is_currently_merged;

		// Token: 0x04001A22 RID: 6690
		private ToolStripItemCollection items;

		// Token: 0x04001A23 RID: 6691
		private bool keyboard_active;

		// Token: 0x04001A24 RID: 6692
		private LayoutEngine layout_engine;

		// Token: 0x04001A25 RID: 6693
		private LayoutSettings layout_settings;

		// Token: 0x04001A26 RID: 6694
		private ToolStripLayoutStyle layout_style;

		// Token: 0x04001A27 RID: 6695
		private Orientation orientation;

		// Token: 0x04001A28 RID: 6696
		private ToolStripOverflowButton overflow_button;

		// Token: 0x04001A29 RID: 6697
		private List<ToolStripItem> pre_merge_items;

		// Token: 0x04001A2A RID: 6698
		private ToolStripRenderer renderer;

		// Token: 0x04001A2B RID: 6699
		private ToolStripRenderMode render_mode;

		// Token: 0x04001A2C RID: 6700
		private ToolStripTextDirection text_direction;

		// Token: 0x04001A2D RID: 6701
		private Timer tooltip_timer;

		// Token: 0x04001A2E RID: 6702
		private ToolTip tooltip_window;

		// Token: 0x04001A2F RID: 6703
		private bool show_item_tool_tips;

		// Token: 0x04001A30 RID: 6704
		private bool stretch;

		// Token: 0x04001A31 RID: 6705
		private ToolStripItem mouse_currently_over;

		// Token: 0x04001A32 RID: 6706
		internal bool menu_selected;

		// Token: 0x04001A33 RID: 6707
		private ToolStripItem tooltip_currently_showing;

		/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStrip" /> for users with impairments.</summary>
		// Token: 0x0200033D RID: 829
		[ComVisible(true)]
		public class ToolStripAccessibleObject : Control.ControlAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStrip.ToolStripAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStrip" /> that owns this <see cref="T:System.Windows.Forms.ToolStrip.ToolStripAccessibleObject" />. </param>
			// Token: 0x06003A86 RID: 14982 RVA: 0x000F07EC File Offset: 0x000EE9EC
			public ToolStripAccessibleObject(ToolStrip owner)
				: base(owner)
			{
			}

			// Token: 0x17000F40 RID: 3904
			// (get) Token: 0x06003A87 RID: 14983 RVA: 0x000F07F8 File Offset: 0x000EE9F8
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ToolBar;
				}
			}

			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
			/// <param name="index">The zero-based index of the accessible child. </param>
			// Token: 0x06003A88 RID: 14984 RVA: 0x000F07FC File Offset: 0x000EE9FC
			public override AccessibleObject GetChild(int index)
			{
				return base.GetChild(index);
			}

			/// <returns>The number of children belonging to an accessible object.</returns>
			// Token: 0x06003A89 RID: 14985 RVA: 0x000F0808 File Offset: 0x000EEA08
			public override int GetChildCount()
			{
				return (this.owner as ToolStrip).Items.Count;
			}

			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns null if no object is at the tested location.</returns>
			/// <param name="x">The horizontal screen coordinate. </param>
			/// <param name="y">The vertical screen coordinate. </param>
			// Token: 0x06003A8A RID: 14986 RVA: 0x000F0820 File Offset: 0x000EEA20
			public override AccessibleObject HitTest(int x, int y)
			{
				return base.HitTest(x, y);
			}
		}
	}
}
