using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Defines the base class for controls, which are components with visual representation.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000A4 RID: 164
	[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ClassInterface(1)]
	[ToolboxItemFilter("System.Windows.Forms")]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[ComVisible(true)]
	public class Control : Component, IDisposable, IComponent, ISynchronizeInvoke, IBindableComponent, IBounds, IDropTarget, IWin32Window
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with default settings.</summary>
		// Token: 0x060007E9 RID: 2025 RVA: 0x00022D98 File Offset: 0x00020F98
		public Control()
		{
			if (WindowsFormsSynchronizationContext.AutoInstall && !(SynchronizationContext.Current is WindowsFormsSynchronizationContext))
			{
				SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
			}
			this.layout_type = Control.LayoutType.Anchor;
			this.anchor_style = AnchorStyles.Top | AnchorStyles.Left;
			this.is_created = false;
			this.is_visible = true;
			this.is_captured = false;
			this.is_disposed = false;
			this.is_enabled = true;
			this.is_entered = false;
			this.layout_pending = false;
			this.is_toplevel = false;
			this.causes_validation = true;
			this.has_focus = false;
			this.layout_suspended = 0;
			this.mouse_clicks = 1;
			this.tab_index = -1;
			this.cursor = null;
			this.right_to_left = RightToLeft.Inherit;
			this.border_style = BorderStyle.None;
			this.background_color = Color.Empty;
			this.dist_right = 0;
			this.dist_bottom = 0;
			this.tab_stop = true;
			this.ime_mode = ImeMode.Inherit;
			this.use_compatible_text_rendering = true;
			this.show_keyboard_cues = false;
			this.show_focus_cues = SystemInformation.MenuAccessKeysUnderlined;
			this.use_wait_cursor = false;
			this.backgroundimage_layout = ImageLayout.Tile;
			this.use_compatible_text_rendering = Application.use_compatible_text_rendering;
			this.padding = this.DefaultPadding;
			this.maximum_size = default(Size);
			this.minimum_size = default(Size);
			this.margin = this.DefaultMargin;
			this.auto_size_mode = AutoSizeMode.GrowOnly;
			this.control_style = ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.Selectable | ControlStyles.StandardDoubleClick | ControlStyles.AllPaintingInWmPaint;
			this.control_style |= ControlStyles.UseTextForAccessibility;
			this.parent = null;
			this.background_image = null;
			this.text = string.Empty;
			this.name = string.Empty;
			this.window_target = new Control.ControlWindowTarget(this);
			this.window = new Control.ControlNativeWindow(this);
			this.child_controls = this.CreateControlsInstance();
			this.bounds.Size = this.DefaultSize;
			this.client_size = this.ClientSizeFromSize(this.bounds.Size);
			this.client_rect = new Rectangle(Point.Empty, this.client_size);
			this.explicit_bounds = this.bounds;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class as a child control, with specific text.</summary>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.Control" /> to be the parent of the control. </param>
		/// <param name="text">The text displayed by the control. </param>
		// Token: 0x060007EA RID: 2026 RVA: 0x00022FA0 File Offset: 0x000211A0
		public Control(Control parent, string text)
			: this()
		{
			this.Text = text;
			this.Parent = parent;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class as a child control, with specific text, size, and location.</summary>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.Control" /> to be the parent of the control. </param>
		/// <param name="text">The text displayed by the control. </param>
		/// <param name="left">The <see cref="P:System.Drawing.Point.X" /> position of the control, in pixels, from the left edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Left" /> property. </param>
		/// <param name="top">The <see cref="P:System.Drawing.Point.Y" /> position of the control, in pixels, from the top edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Top" /> property. </param>
		/// <param name="width">The width of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Width" /> property. </param>
		/// <param name="height">The height of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Height" /> property. </param>
		// Token: 0x060007EB RID: 2027 RVA: 0x00022FB8 File Offset: 0x000211B8
		public Control(Control parent, string text, int left, int top, int width, int height)
			: this()
		{
			this.Parent = parent;
			this.SetBounds(left, top, width, height, BoundsSpecified.All);
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with specific text.</summary>
		/// <param name="text">The text displayed by the control. </param>
		// Token: 0x060007EC RID: 2028 RVA: 0x00022FE8 File Offset: 0x000211E8
		public Control(string text)
			: this()
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with specific text, size, and location.</summary>
		/// <param name="text">The text displayed by the control. </param>
		/// <param name="left">The <see cref="P:System.Drawing.Point.X" /> position of the control, in pixels, from the left edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Left" /> property. </param>
		/// <param name="top">The <see cref="P:System.Drawing.Point.Y" /> position of the control, in pixels, from the top edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Top" /> property. </param>
		/// <param name="width">The width of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Width" /> property. </param>
		/// <param name="height">The height of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Height" /> property. </param>
		// Token: 0x060007ED RID: 2029 RVA: 0x00022FF8 File Offset: 0x000211F8
		public Control(string text, int left, int top, int width, int height)
			: this()
		{
			this.SetBounds(left, top, width, height, BoundsSpecified.All);
			this.Text = text;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00023020 File Offset: 0x00021220
		// Note: this type is marked as 'beforefieldinit'.
		static Control()
		{
			Control.AutoSizeChangedEvent = new object();
			Control.BackColorChangedEvent = new object();
			Control.BackgroundImageChangedEvent = new object();
			Control.BackgroundImageLayoutChangedEvent = new object();
			Control.BindingContextChangedEvent = new object();
			Control.CausesValidationChangedEvent = new object();
			Control.ChangeUICuesEvent = new object();
			Control.ClickEvent = new object();
			Control.ClientSizeChangedEvent = new object();
			Control.ContextMenuChangedEvent = new object();
			Control.ContextMenuStripChangedEvent = new object();
			Control.ControlAddedEvent = new object();
			Control.ControlRemovedEvent = new object();
			Control.CursorChangedEvent = new object();
			Control.DockChangedEvent = new object();
			Control.DoubleClickEvent = new object();
			Control.DragDropEvent = new object();
			Control.DragEnterEvent = new object();
			Control.DragLeaveEvent = new object();
			Control.DragOverEvent = new object();
			Control.EnabledChangedEvent = new object();
			Control.EnterEvent = new object();
			Control.FontChangedEvent = new object();
			Control.ForeColorChangedEvent = new object();
			Control.GiveFeedbackEvent = new object();
			Control.GotFocusEvent = new object();
			Control.HandleCreatedEvent = new object();
			Control.HandleDestroyedEvent = new object();
			Control.HelpRequestedEvent = new object();
			Control.ImeModeChangedEvent = new object();
			Control.InvalidatedEvent = new object();
			Control.KeyDownEvent = new object();
			Control.KeyPressEvent = new object();
			Control.KeyUpEvent = new object();
			Control.LayoutEvent = new object();
			Control.LeaveEvent = new object();
			Control.LocationChangedEvent = new object();
			Control.LostFocusEvent = new object();
			Control.MarginChangedEvent = new object();
			Control.MouseCaptureChangedEvent = new object();
			Control.MouseClickEvent = new object();
			Control.MouseDoubleClickEvent = new object();
			Control.MouseDownEvent = new object();
			Control.MouseEnterEvent = new object();
			Control.MouseHoverEvent = new object();
			Control.MouseLeaveEvent = new object();
			Control.MouseMoveEvent = new object();
			Control.MouseUpEvent = new object();
			Control.MouseWheelEvent = new object();
			Control.MoveEvent = new object();
			Control.PaddingChangedEvent = new object();
			Control.PaintEvent = new object();
			Control.ParentChangedEvent = new object();
			Control.PreviewKeyDownEvent = new object();
			Control.QueryAccessibilityHelpEvent = new object();
			Control.QueryContinueDragEvent = new object();
			Control.RegionChangedEvent = new object();
			Control.ResizeEvent = new object();
			Control.RightToLeftChangedEvent = new object();
			Control.SizeChangedEvent = new object();
			Control.StyleChangedEvent = new object();
			Control.SystemColorsChangedEvent = new object();
			Control.TabIndexChangedEvent = new object();
			Control.TabStopChangedEvent = new object();
			Control.TextChangedEvent = new object();
			Control.ValidatedEvent = new object();
			Control.ValidatingEvent = new object();
			Control.VisibleChangedEvent = new object();
		}

		/// <summary>This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060007EF RID: 2031 RVA: 0x000232D8 File Offset: 0x000214D8
		// (remove) Token: 0x060007F0 RID: 2032 RVA: 0x000232EC File Offset: 0x000214EC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public event EventHandler AutoSizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.AutoSizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.AutoSizeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060007F1 RID: 2033 RVA: 0x00023300 File Offset: 0x00021500
		// (remove) Token: 0x060007F2 RID: 2034 RVA: 0x00023314 File Offset: 0x00021514
		public event EventHandler BackColorChanged
		{
			add
			{
				base.Events.AddHandler(Control.BackColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.BackColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060007F3 RID: 2035 RVA: 0x00023328 File Offset: 0x00021528
		// (remove) Token: 0x060007F4 RID: 2036 RVA: 0x0002333C File Offset: 0x0002153C
		public event EventHandler BackgroundImageChanged
		{
			add
			{
				base.Events.AddHandler(Control.BackgroundImageChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.BackgroundImageChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060007F5 RID: 2037 RVA: 0x00023350 File Offset: 0x00021550
		// (remove) Token: 0x060007F6 RID: 2038 RVA: 0x00023364 File Offset: 0x00021564
		public event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.Events.AddHandler(Control.BackgroundImageLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.BackgroundImageLayoutChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="T:System.Windows.Forms.BindingContext" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060007F7 RID: 2039 RVA: 0x00023378 File Offset: 0x00021578
		// (remove) Token: 0x060007F8 RID: 2040 RVA: 0x0002338C File Offset: 0x0002158C
		public event EventHandler BindingContextChanged
		{
			add
			{
				base.Events.AddHandler(Control.BindingContextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.BindingContextChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.CausesValidation" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000073 RID: 115
		// (add) Token: 0x060007F9 RID: 2041 RVA: 0x000233A0 File Offset: 0x000215A0
		// (remove) Token: 0x060007FA RID: 2042 RVA: 0x000233B4 File Offset: 0x000215B4
		public event EventHandler CausesValidationChanged
		{
			add
			{
				base.Events.AddHandler(Control.CausesValidationChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.CausesValidationChangedEvent, value);
			}
		}

		/// <summary>Occurs when the focus or keyboard user interface (UI) cues change.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060007FB RID: 2043 RVA: 0x000233C8 File Offset: 0x000215C8
		// (remove) Token: 0x060007FC RID: 2044 RVA: 0x000233DC File Offset: 0x000215DC
		public event UICuesEventHandler ChangeUICues
		{
			add
			{
				base.Events.AddHandler(Control.ChangeUICuesEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ChangeUICuesEvent, value);
			}
		}

		/// <summary>Occurs when the control is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060007FD RID: 2045 RVA: 0x000233F0 File Offset: 0x000215F0
		// (remove) Token: 0x060007FE RID: 2046 RVA: 0x00023404 File Offset: 0x00021604
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(Control.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ClickEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ClientSize" /> property changes. </summary>
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x060007FF RID: 2047 RVA: 0x00023418 File Offset: 0x00021618
		// (remove) Token: 0x06000800 RID: 2048 RVA: 0x0002342C File Offset: 0x0002162C
		public event EventHandler ClientSizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.ClientSizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ClientSizeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenu" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06000801 RID: 2049 RVA: 0x00023440 File Offset: 0x00021640
		// (remove) Token: 0x06000802 RID: 2050 RVA: 0x00023454 File Offset: 0x00021654
		[Browsable(false)]
		public event EventHandler ContextMenuChanged
		{
			add
			{
				base.Events.AddHandler(Control.ContextMenuChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ContextMenuChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenuStrip" /> property changes. </summary>
		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06000803 RID: 2051 RVA: 0x00023468 File Offset: 0x00021668
		// (remove) Token: 0x06000804 RID: 2052 RVA: 0x0002347C File Offset: 0x0002167C
		public event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.Events.AddHandler(Control.ContextMenuStripChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ContextMenuStripChangedEvent, value);
			}
		}

		/// <summary>Occurs when a new control is added to the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06000805 RID: 2053 RVA: 0x00023490 File Offset: 0x00021690
		// (remove) Token: 0x06000806 RID: 2054 RVA: 0x000234A4 File Offset: 0x000216A4
		[Browsable(true)]
		[EditorBrowsable(2)]
		public event ControlEventHandler ControlAdded
		{
			add
			{
				base.Events.AddHandler(Control.ControlAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ControlAddedEvent, value);
			}
		}

		/// <summary>Occurs when a control is removed from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06000807 RID: 2055 RVA: 0x000234B8 File Offset: 0x000216B8
		// (remove) Token: 0x06000808 RID: 2056 RVA: 0x000234CC File Offset: 0x000216CC
		[EditorBrowsable(2)]
		[Browsable(true)]
		public event ControlEventHandler ControlRemoved
		{
			add
			{
				base.Events.AddHandler(Control.ControlRemovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ControlRemovedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Cursor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06000809 RID: 2057 RVA: 0x000234E0 File Offset: 0x000216E0
		// (remove) Token: 0x0600080A RID: 2058 RVA: 0x000234F4 File Offset: 0x000216F4
		[MWFDescription("Fired when the cursor for the control has been changed")]
		[MWFCategory("PropertyChanged")]
		public event EventHandler CursorChanged
		{
			add
			{
				base.Events.AddHandler(Control.CursorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.CursorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Dock" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400007C RID: 124
		// (add) Token: 0x0600080B RID: 2059 RVA: 0x00023508 File Offset: 0x00021708
		// (remove) Token: 0x0600080C RID: 2060 RVA: 0x0002351C File Offset: 0x0002171C
		public event EventHandler DockChanged
		{
			add
			{
				base.Events.AddHandler(Control.DockChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.DockChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is double-clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400007D RID: 125
		// (add) Token: 0x0600080D RID: 2061 RVA: 0x00023530 File Offset: 0x00021730
		// (remove) Token: 0x0600080E RID: 2062 RVA: 0x00023544 File Offset: 0x00021744
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(Control.DoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.DoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when a drag-and-drop operation is completed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400007E RID: 126
		// (add) Token: 0x0600080F RID: 2063 RVA: 0x00023558 File Offset: 0x00021758
		// (remove) Token: 0x06000810 RID: 2064 RVA: 0x0002356C File Offset: 0x0002176C
		public event DragEventHandler DragDrop
		{
			add
			{
				base.Events.AddHandler(Control.DragDropEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.DragDropEvent, value);
			}
		}

		/// <summary>Occurs when an object is dragged into the control's bounds.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06000811 RID: 2065 RVA: 0x00023580 File Offset: 0x00021780
		// (remove) Token: 0x06000812 RID: 2066 RVA: 0x00023594 File Offset: 0x00021794
		public event DragEventHandler DragEnter
		{
			add
			{
				base.Events.AddHandler(Control.DragEnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.DragEnterEvent, value);
			}
		}

		/// <summary>Occurs when an object is dragged out of the control's bounds.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06000813 RID: 2067 RVA: 0x000235A8 File Offset: 0x000217A8
		// (remove) Token: 0x06000814 RID: 2068 RVA: 0x000235BC File Offset: 0x000217BC
		public event EventHandler DragLeave
		{
			add
			{
				base.Events.AddHandler(Control.DragLeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.DragLeaveEvent, value);
			}
		}

		/// <summary>Occurs when an object is dragged over the control's bounds.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06000815 RID: 2069 RVA: 0x000235D0 File Offset: 0x000217D0
		// (remove) Token: 0x06000816 RID: 2070 RVA: 0x000235E4 File Offset: 0x000217E4
		public event DragEventHandler DragOver
		{
			add
			{
				base.Events.AddHandler(Control.DragOverEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.DragOverEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06000817 RID: 2071 RVA: 0x000235F8 File Offset: 0x000217F8
		// (remove) Token: 0x06000818 RID: 2072 RVA: 0x0002360C File Offset: 0x0002180C
		public event EventHandler EnabledChanged
		{
			add
			{
				base.Events.AddHandler(Control.EnabledChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EnabledChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is entered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06000819 RID: 2073 RVA: 0x00023620 File Offset: 0x00021820
		// (remove) Token: 0x0600081A RID: 2074 RVA: 0x00023634 File Offset: 0x00021834
		public event EventHandler Enter
		{
			add
			{
				base.Events.AddHandler(Control.EnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EnterEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Font" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000084 RID: 132
		// (add) Token: 0x0600081B RID: 2075 RVA: 0x00023648 File Offset: 0x00021848
		// (remove) Token: 0x0600081C RID: 2076 RVA: 0x0002365C File Offset: 0x0002185C
		public event EventHandler FontChanged
		{
			add
			{
				base.Events.AddHandler(Control.FontChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.FontChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000085 RID: 133
		// (add) Token: 0x0600081D RID: 2077 RVA: 0x00023670 File Offset: 0x00021870
		// (remove) Token: 0x0600081E RID: 2078 RVA: 0x00023684 File Offset: 0x00021884
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(Control.ForeColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ForeColorChangedEvent, value);
			}
		}

		/// <summary>Occurs during a drag operation.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000086 RID: 134
		// (add) Token: 0x0600081F RID: 2079 RVA: 0x00023698 File Offset: 0x00021898
		// (remove) Token: 0x06000820 RID: 2080 RVA: 0x000236AC File Offset: 0x000218AC
		public event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.Events.AddHandler(Control.GiveFeedbackEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.GiveFeedbackEvent, value);
			}
		}

		/// <summary>Occurs when the control receives focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06000821 RID: 2081 RVA: 0x000236C0 File Offset: 0x000218C0
		// (remove) Token: 0x06000822 RID: 2082 RVA: 0x000236D4 File Offset: 0x000218D4
		[Browsable(false)]
		[EditorBrowsable(2)]
		public event EventHandler GotFocus
		{
			add
			{
				base.Events.AddHandler(Control.GotFocusEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.GotFocusEvent, value);
			}
		}

		/// <summary>Occurs when a handle is created for the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06000823 RID: 2083 RVA: 0x000236E8 File Offset: 0x000218E8
		// (remove) Token: 0x06000824 RID: 2084 RVA: 0x000236FC File Offset: 0x000218FC
		[Browsable(false)]
		[EditorBrowsable(2)]
		public event EventHandler HandleCreated
		{
			add
			{
				base.Events.AddHandler(Control.HandleCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.HandleCreatedEvent, value);
			}
		}

		/// <summary>Occurs when the control's handle is in the process of being destroyed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06000825 RID: 2085 RVA: 0x00023710 File Offset: 0x00021910
		// (remove) Token: 0x06000826 RID: 2086 RVA: 0x00023724 File Offset: 0x00021924
		[EditorBrowsable(2)]
		[Browsable(false)]
		public event EventHandler HandleDestroyed
		{
			add
			{
				base.Events.AddHandler(Control.HandleDestroyedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.HandleDestroyedEvent, value);
			}
		}

		/// <summary>Occurs when the user requests help for a control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06000827 RID: 2087 RVA: 0x00023738 File Offset: 0x00021938
		// (remove) Token: 0x06000828 RID: 2088 RVA: 0x0002374C File Offset: 0x0002194C
		public event HelpEventHandler HelpRequested
		{
			add
			{
				base.Events.AddHandler(Control.HelpRequestedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.HelpRequestedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06000829 RID: 2089 RVA: 0x00023760 File Offset: 0x00021960
		// (remove) Token: 0x0600082A RID: 2090 RVA: 0x00023774 File Offset: 0x00021974
		public event EventHandler ImeModeChanged
		{
			add
			{
				base.Events.AddHandler(Control.ImeModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ImeModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when a control's display requires redrawing.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400008C RID: 140
		// (add) Token: 0x0600082B RID: 2091 RVA: 0x00023788 File Offset: 0x00021988
		// (remove) Token: 0x0600082C RID: 2092 RVA: 0x0002379C File Offset: 0x0002199C
		[Browsable(false)]
		[EditorBrowsable(2)]
		public event InvalidateEventHandler Invalidated
		{
			add
			{
				base.Events.AddHandler(Control.InvalidatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.InvalidatedEvent, value);
			}
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400008D RID: 141
		// (add) Token: 0x0600082D RID: 2093 RVA: 0x000237B0 File Offset: 0x000219B0
		// (remove) Token: 0x0600082E RID: 2094 RVA: 0x000237C4 File Offset: 0x000219C4
		public event KeyEventHandler KeyDown
		{
			add
			{
				base.Events.AddHandler(Control.KeyDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.KeyDownEvent, value);
			}
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400008E RID: 142
		// (add) Token: 0x0600082F RID: 2095 RVA: 0x000237D8 File Offset: 0x000219D8
		// (remove) Token: 0x06000830 RID: 2096 RVA: 0x000237EC File Offset: 0x000219EC
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				base.Events.AddHandler(Control.KeyPressEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.KeyPressEvent, value);
			}
		}

		/// <summary>Occurs when a key is released while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06000831 RID: 2097 RVA: 0x00023800 File Offset: 0x00021A00
		// (remove) Token: 0x06000832 RID: 2098 RVA: 0x00023814 File Offset: 0x00021A14
		public event KeyEventHandler KeyUp
		{
			add
			{
				base.Events.AddHandler(Control.KeyUpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.KeyUpEvent, value);
			}
		}

		/// <summary>Occurs when a control should reposition its child controls.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06000833 RID: 2099 RVA: 0x00023828 File Offset: 0x00021A28
		// (remove) Token: 0x06000834 RID: 2100 RVA: 0x0002383C File Offset: 0x00021A3C
		public event LayoutEventHandler Layout
		{
			add
			{
				base.Events.AddHandler(Control.LayoutEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.LayoutEvent, value);
			}
		}

		/// <summary>Occurs when the input focus leaves the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06000835 RID: 2101 RVA: 0x00023850 File Offset: 0x00021A50
		// (remove) Token: 0x06000836 RID: 2102 RVA: 0x00023864 File Offset: 0x00021A64
		public event EventHandler Leave
		{
			add
			{
				base.Events.AddHandler(Control.LeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.LeaveEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Location" /> property value has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06000837 RID: 2103 RVA: 0x00023878 File Offset: 0x00021A78
		// (remove) Token: 0x06000838 RID: 2104 RVA: 0x0002388C File Offset: 0x00021A8C
		public event EventHandler LocationChanged
		{
			add
			{
				base.Events.AddHandler(Control.LocationChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.LocationChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control loses focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06000839 RID: 2105 RVA: 0x000238A0 File Offset: 0x00021AA0
		// (remove) Token: 0x0600083A RID: 2106 RVA: 0x000238B4 File Offset: 0x00021AB4
		[EditorBrowsable(2)]
		[Browsable(false)]
		public event EventHandler LostFocus
		{
			add
			{
				base.Events.AddHandler(Control.LostFocusEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.LostFocusEvent, value);
			}
		}

		/// <summary>Occurs when the control's margin changes.</summary>
		// Token: 0x14000094 RID: 148
		// (add) Token: 0x0600083B RID: 2107 RVA: 0x000238C8 File Offset: 0x00021AC8
		// (remove) Token: 0x0600083C RID: 2108 RVA: 0x000238DC File Offset: 0x00021ADC
		public event EventHandler MarginChanged
		{
			add
			{
				base.Events.AddHandler(Control.MarginChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MarginChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control loses mouse capture.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000095 RID: 149
		// (add) Token: 0x0600083D RID: 2109 RVA: 0x000238F0 File Offset: 0x00021AF0
		// (remove) Token: 0x0600083E RID: 2110 RVA: 0x00023904 File Offset: 0x00021B04
		public event EventHandler MouseCaptureChanged
		{
			add
			{
				base.Events.AddHandler(Control.MouseCaptureChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseCaptureChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is clicked by the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000096 RID: 150
		// (add) Token: 0x0600083F RID: 2111 RVA: 0x00023918 File Offset: 0x00021B18
		// (remove) Token: 0x06000840 RID: 2112 RVA: 0x0002392C File Offset: 0x00021B2C
		public event MouseEventHandler MouseClick
		{
			add
			{
				base.Events.AddHandler(Control.MouseClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseClickEvent, value);
			}
		}

		/// <summary>Occurs when the control is double clicked by the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000097 RID: 151
		// (add) Token: 0x06000841 RID: 2113 RVA: 0x00023940 File Offset: 0x00021B40
		// (remove) Token: 0x06000842 RID: 2114 RVA: 0x00023954 File Offset: 0x00021B54
		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(Control.MouseDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and a mouse button is pressed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06000843 RID: 2115 RVA: 0x00023968 File Offset: 0x00021B68
		// (remove) Token: 0x06000844 RID: 2116 RVA: 0x0002397C File Offset: 0x00021B7C
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(Control.MouseDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseDownEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer enters the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06000845 RID: 2117 RVA: 0x00023990 File Offset: 0x00021B90
		// (remove) Token: 0x06000846 RID: 2118 RVA: 0x000239A4 File Offset: 0x00021BA4
		public event EventHandler MouseEnter
		{
			add
			{
				base.Events.AddHandler(Control.MouseEnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseEnterEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer rests on the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06000847 RID: 2119 RVA: 0x000239B8 File Offset: 0x00021BB8
		// (remove) Token: 0x06000848 RID: 2120 RVA: 0x000239CC File Offset: 0x00021BCC
		public event EventHandler MouseHover
		{
			add
			{
				base.Events.AddHandler(Control.MouseHoverEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseHoverEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06000849 RID: 2121 RVA: 0x000239E0 File Offset: 0x00021BE0
		// (remove) Token: 0x0600084A RID: 2122 RVA: 0x000239F4 File Offset: 0x00021BF4
		public event EventHandler MouseLeave
		{
			add
			{
				base.Events.AddHandler(Control.MouseLeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseLeaveEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is moved over the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400009C RID: 156
		// (add) Token: 0x0600084B RID: 2123 RVA: 0x00023A08 File Offset: 0x00021C08
		// (remove) Token: 0x0600084C RID: 2124 RVA: 0x00023A1C File Offset: 0x00021C1C
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(Control.MouseMoveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseMoveEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and a mouse button is released.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400009D RID: 157
		// (add) Token: 0x0600084D RID: 2125 RVA: 0x00023A30 File Offset: 0x00021C30
		// (remove) Token: 0x0600084E RID: 2126 RVA: 0x00023A44 File Offset: 0x00021C44
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(Control.MouseUpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseUpEvent, value);
			}
		}

		/// <summary>Occurs when the mouse wheel moves while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400009E RID: 158
		// (add) Token: 0x0600084F RID: 2127 RVA: 0x00023A58 File Offset: 0x00021C58
		// (remove) Token: 0x06000850 RID: 2128 RVA: 0x00023A6C File Offset: 0x00021C6C
		[Browsable(false)]
		[EditorBrowsable(2)]
		public event MouseEventHandler MouseWheel
		{
			add
			{
				base.Events.AddHandler(Control.MouseWheelEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseWheelEvent, value);
			}
		}

		/// <summary>Occurs when the control is moved.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06000851 RID: 2129 RVA: 0x00023A80 File Offset: 0x00021C80
		// (remove) Token: 0x06000852 RID: 2130 RVA: 0x00023A94 File Offset: 0x00021C94
		public event EventHandler Move
		{
			add
			{
				base.Events.AddHandler(Control.MoveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MoveEvent, value);
			}
		}

		/// <summary>Occurs when the control's padding changes.</summary>
		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06000853 RID: 2131 RVA: 0x00023AA8 File Offset: 0x00021CA8
		// (remove) Token: 0x06000854 RID: 2132 RVA: 0x00023ABC File Offset: 0x00021CBC
		public event EventHandler PaddingChanged
		{
			add
			{
				base.Events.AddHandler(Control.PaddingChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.PaddingChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06000855 RID: 2133 RVA: 0x00023AD0 File Offset: 0x00021CD0
		// (remove) Token: 0x06000856 RID: 2134 RVA: 0x00023AE4 File Offset: 0x00021CE4
		public event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(Control.PaintEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.PaintEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Parent" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06000857 RID: 2135 RVA: 0x00023AF8 File Offset: 0x00021CF8
		// (remove) Token: 0x06000858 RID: 2136 RVA: 0x00023B0C File Offset: 0x00021D0C
		public event EventHandler ParentChanged
		{
			add
			{
				base.Events.AddHandler(Control.ParentChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ParentChangedEvent, value);
			}
		}

		/// <summary>Occurs before the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event when a key is pressed while focus is on this control.</summary>
		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06000859 RID: 2137 RVA: 0x00023B20 File Offset: 0x00021D20
		// (remove) Token: 0x0600085A RID: 2138 RVA: 0x00023B34 File Offset: 0x00021D34
		public event PreviewKeyDownEventHandler PreviewKeyDown
		{
			add
			{
				base.Events.AddHandler(Control.PreviewKeyDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.PreviewKeyDownEvent, value);
			}
		}

		/// <summary>Occurs when <see cref="T:System.Windows.Forms.AccessibleObject" /> is providing help to accessibility applications.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x0600085B RID: 2139 RVA: 0x00023B48 File Offset: 0x00021D48
		// (remove) Token: 0x0600085C RID: 2140 RVA: 0x00023B5C File Offset: 0x00021D5C
		public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				base.Events.AddHandler(Control.QueryAccessibilityHelpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.QueryAccessibilityHelpEvent, value);
			}
		}

		/// <summary>Occurs during a drag-and-drop operation and enables the drag source to determine whether the drag-and-drop operation should be canceled.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600085D RID: 2141 RVA: 0x00023B70 File Offset: 0x00021D70
		// (remove) Token: 0x0600085E RID: 2142 RVA: 0x00023B84 File Offset: 0x00021D84
		public event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.Events.AddHandler(Control.QueryContinueDragEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.QueryContinueDragEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Region" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x0600085F RID: 2143 RVA: 0x00023B98 File Offset: 0x00021D98
		// (remove) Token: 0x06000860 RID: 2144 RVA: 0x00023BAC File Offset: 0x00021DAC
		public event EventHandler RegionChanged
		{
			add
			{
				base.Events.AddHandler(Control.RegionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.RegionChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is resized.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06000861 RID: 2145 RVA: 0x00023BC0 File Offset: 0x00021DC0
		// (remove) Token: 0x06000862 RID: 2146 RVA: 0x00023BD4 File Offset: 0x00021DD4
		[EditorBrowsable(2)]
		public event EventHandler Resize
		{
			add
			{
				base.Events.AddHandler(Control.ResizeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ResizeEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06000863 RID: 2147 RVA: 0x00023BE8 File Offset: 0x00021DE8
		// (remove) Token: 0x06000864 RID: 2148 RVA: 0x00023BFC File Offset: 0x00021DFC
		public event EventHandler RightToLeftChanged
		{
			add
			{
				base.Events.AddHandler(Control.RightToLeftChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.RightToLeftChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Size" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06000865 RID: 2149 RVA: 0x00023C10 File Offset: 0x00021E10
		// (remove) Token: 0x06000866 RID: 2150 RVA: 0x00023C24 File Offset: 0x00021E24
		public event EventHandler SizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.SizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.SizeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control style changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06000867 RID: 2151 RVA: 0x00023C38 File Offset: 0x00021E38
		// (remove) Token: 0x06000868 RID: 2152 RVA: 0x00023C4C File Offset: 0x00021E4C
		public event EventHandler StyleChanged
		{
			add
			{
				base.Events.AddHandler(Control.StyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.StyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the system colors change.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06000869 RID: 2153 RVA: 0x00023C60 File Offset: 0x00021E60
		// (remove) Token: 0x0600086A RID: 2154 RVA: 0x00023C74 File Offset: 0x00021E74
		public event EventHandler SystemColorsChanged
		{
			add
			{
				base.Events.AddHandler(Control.SystemColorsChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.SystemColorsChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.TabIndex" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000AC RID: 172
		// (add) Token: 0x0600086B RID: 2155 RVA: 0x00023C88 File Offset: 0x00021E88
		// (remove) Token: 0x0600086C RID: 2156 RVA: 0x00023C9C File Offset: 0x00021E9C
		public event EventHandler TabIndexChanged
		{
			add
			{
				base.Events.AddHandler(Control.TabIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.TabIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.TabStop" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000AD RID: 173
		// (add) Token: 0x0600086D RID: 2157 RVA: 0x00023CB0 File Offset: 0x00021EB0
		// (remove) Token: 0x0600086E RID: 2158 RVA: 0x00023CC4 File Offset: 0x00021EC4
		public event EventHandler TabStopChanged
		{
			add
			{
				base.Events.AddHandler(Control.TabStopChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.TabStopChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Text" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000AE RID: 174
		// (add) Token: 0x0600086F RID: 2159 RVA: 0x00023CD8 File Offset: 0x00021ED8
		// (remove) Token: 0x06000870 RID: 2160 RVA: 0x00023CEC File Offset: 0x00021EEC
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(Control.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.TextChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is finished validating.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06000871 RID: 2161 RVA: 0x00023D00 File Offset: 0x00021F00
		// (remove) Token: 0x06000872 RID: 2162 RVA: 0x00023D14 File Offset: 0x00021F14
		public event EventHandler Validated
		{
			add
			{
				base.Events.AddHandler(Control.ValidatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ValidatedEvent, value);
			}
		}

		/// <summary>Occurs when the control is validating.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06000873 RID: 2163 RVA: 0x00023D28 File Offset: 0x00021F28
		// (remove) Token: 0x06000874 RID: 2164 RVA: 0x00023D3C File Offset: 0x00021F3C
		public event CancelEventHandler Validating
		{
			add
			{
				base.Events.AddHandler(Control.ValidatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ValidatingEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06000875 RID: 2165 RVA: 0x00023D50 File Offset: 0x00021F50
		// (remove) Token: 0x06000876 RID: 2166 RVA: 0x00023D64 File Offset: 0x00021F64
		public event EventHandler VisibleChanged
		{
			add
			{
				base.Events.AddHandler(Control.VisibleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.VisibleChangedEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
		/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000877 RID: 2167 RVA: 0x00023D78 File Offset: 0x00021F78
		void IDropTarget.OnDragDrop(DragEventArgs drgEvent)
		{
			this.OnDragDrop(drgEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
		/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000878 RID: 2168 RVA: 0x00023D84 File Offset: 0x00021F84
		void IDropTarget.OnDragEnter(DragEventArgs drgEvent)
		{
			this.OnDragEnter(drgEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000879 RID: 2169 RVA: 0x00023D90 File Offset: 0x00021F90
		void IDropTarget.OnDragLeave(EventArgs e)
		{
			this.OnDragLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
		/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x0600087A RID: 2170 RVA: 0x00023D9C File Offset: 0x00021F9C
		void IDropTarget.OnDragOver(DragEventArgs drgEvent)
		{
			this.OnDragOver(drgEvent);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600087B RID: 2171 RVA: 0x00023DA8 File Offset: 0x00021FA8
		protected override void Dispose(bool disposing)
		{
			if (!this.is_disposed && disposing)
			{
				this.is_disposing = true;
				this.Capture = false;
				this.DisposeBackBuffer();
				if (this.InvokeRequired)
				{
					if (Application.MessageLoop && this.IsHandleCreated)
					{
						this.BeginInvokeInternal(new MethodInvoker(this.DestroyHandle), null);
					}
				}
				else
				{
					this.DestroyHandle();
				}
				if (this.parent != null)
				{
					this.parent.Controls.Remove(this);
				}
				Control[] allControls = this.child_controls.GetAllControls();
				for (int i = 0; i < allControls.Length; i++)
				{
					allControls[i].parent = null;
					allControls[i].Dispose();
				}
			}
			this.is_disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00023E78 File Offset: 0x00022078
		internal Rectangle PaddingClientRectangle
		{
			get
			{
				return new Rectangle(this.ClientRectangle.Left + this.padding.Left, this.ClientRectangle.Top + this.padding.Top, this.ClientRectangle.Width - this.padding.Horizontal, this.ClientRectangle.Height - this.padding.Vertical);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00023EF4 File Offset: 0x000220F4
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x00023EFC File Offset: 0x000220FC
		internal MenuTracker ActiveTracker
		{
			get
			{
				return this.active_tracker;
			}
			set
			{
				if (value == this.active_tracker)
				{
					return;
				}
				this.Capture = value != null;
				this.active_tracker = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00023F20 File Offset: 0x00022120
		internal bool InternalSelected
		{
			get
			{
				IContainerControl containerControl = this.GetContainerControl();
				return containerControl != null && containerControl.ActiveControl == this;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00023F4C File Offset: 0x0002214C
		internal bool InternalContainsFocus
		{
			get
			{
				IntPtr focus = XplatUI.GetFocus();
				if (this.IsHandleCreated)
				{
					if (focus == this.Handle)
					{
						return true;
					}
					foreach (Control control in this.child_controls.GetAllControls())
					{
						if (control.InternalContainsFocus)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00023FB0 File Offset: 0x000221B0
		internal bool Entered
		{
			get
			{
				return this.is_entered;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00023FB8 File Offset: 0x000221B8
		internal bool VisibleInternal
		{
			get
			{
				return this.is_visible;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00023FC0 File Offset: 0x000221C0
		internal Control.LayoutType ControlLayoutType
		{
			get
			{
				return this.layout_type;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00023FC8 File Offset: 0x000221C8
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x00023FD0 File Offset: 0x000221D0
		internal BorderStyle InternalBorderStyle
		{
			get
			{
				return this.border_style;
			}
			set
			{
				if (!Enum.IsDefined(typeof(BorderStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for BorderStyle", value));
				}
				if (this.border_style != value)
				{
					this.border_style = value;
					if (this.IsHandleCreated)
					{
						XplatUI.SetBorderStyle(this.window.Handle, (FormBorderStyle)this.border_style);
						this.RecreateHandle();
						this.Refresh();
					}
					else
					{
						this.client_size = this.ClientSizeFromSize(this.bounds.Size);
					}
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0002406C File Offset: 0x0002226C
		internal Size InternalClientSize
		{
			set
			{
				this.client_size = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00024078 File Offset: 0x00022278
		internal virtual bool ActivateOnShow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0002407C File Offset: 0x0002227C
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x00024084 File Offset: 0x00022284
		internal Rectangle ExplicitBounds
		{
			get
			{
				return this.explicit_bounds;
			}
			set
			{
				this.explicit_bounds = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00024090 File Offset: 0x00022290
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x000240B4 File Offset: 0x000222B4
		internal bool ValidationFailed
		{
			get
			{
				ContainerControl containerControl = this.InternalGetContainerControl();
				return containerControl != null && containerControl.validation_failed;
			}
			set
			{
				ContainerControl containerControl = this.InternalGetContainerControl();
				if (containerControl != null)
				{
					containerControl.validation_failed = value;
				}
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000240D8 File Offset: 0x000222D8
		internal IAsyncResult BeginInvokeInternal(Delegate method, object[] args)
		{
			return this.BeginInvokeInternal(method, args, this.FindControlToInvokeOn());
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000240E8 File Offset: 0x000222E8
		internal IAsyncResult BeginInvokeInternal(Delegate method, object[] args, Control control)
		{
			AsyncMethodResult asyncMethodResult = new AsyncMethodResult();
			AsyncMethodData asyncMethodData = new AsyncMethodData();
			asyncMethodData.Handle = control.GetInvokableHandle();
			asyncMethodData.Method = method;
			asyncMethodData.Args = args;
			asyncMethodData.Result = asyncMethodResult;
			if (!ExecutionContext.IsFlowSuppressed())
			{
				asyncMethodData.Context = ExecutionContext.Capture();
			}
			XplatUI.SendAsyncMethod(asyncMethodData);
			return asyncMethodResult;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00024140 File Offset: 0x00022340
		private IntPtr GetInvokableHandle()
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			return this.window.Handle;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00024160 File Offset: 0x00022360
		internal void PointToClient(ref int x, ref int y)
		{
			XplatUI.ScreenToClient(this.Handle, ref x, ref y);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00024170 File Offset: 0x00022370
		internal void PointToScreen(ref int x, ref int y)
		{
			XplatUI.ClientToScreen(this.Handle, ref x, ref y);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00024180 File Offset: 0x00022380
		internal bool IsRecreating
		{
			get
			{
				return this.is_recreating;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00024188 File Offset: 0x00022388
		internal Graphics DeviceContext
		{
			get
			{
				return Hwnd.GraphicsContext;
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00024190 File Offset: 0x00022390
		internal virtual int OverrideHeight(int height)
		{
			return height;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00024194 File Offset: 0x00022394
		private void ProcessActiveTracker(ref Message m)
		{
			bool flag = m.Msg == 514 || m.Msg == 517;
			MouseButtons mouseButtons = Control.FromParamToMouseButtons((long)m.WParam.ToInt32());
			if (flag)
			{
				switch (m.Msg)
				{
				case 514:
					mouseButtons |= MouseButtons.Left;
					break;
				case 517:
					mouseButtons |= MouseButtons.Right;
					break;
				}
			}
			MouseEventArgs mouseEventArgs = new MouseEventArgs(mouseButtons, this.mouse_clicks, Control.MousePosition.X, Control.MousePosition.Y, 0);
			if (flag)
			{
				this.active_tracker.OnMouseUp(mouseEventArgs);
				this.mouse_clicks = 1;
			}
			else if (!this.active_tracker.OnMouseDown(mouseEventArgs))
			{
				Control realChildAtPoint = this.GetRealChildAtPoint(Cursor.Position);
				if (realChildAtPoint != null)
				{
					Point point = realChildAtPoint.PointToClient(Cursor.Position);
					XplatUI.SendMessage(realChildAtPoint.Handle, (Msg)m.Msg, m.WParam, Control.MakeParam(point.X, point.Y));
				}
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000242C8 File Offset: 0x000224C8
		private Control FindControlToInvokeOn()
		{
			Control control = this;
			while (!control.IsHandleCreated)
			{
				control = control.parent;
				if (control == null)
				{
					IL_001F:
					if (control == null || !control.IsHandleCreated)
					{
						throw new InvalidOperationException("Cannot call Invoke or BeginInvoke on a control until the window handle is created");
					}
					return control;
				}
			}
			goto IL_001F;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00024314 File Offset: 0x00022514
		private void InvalidateBackBuffer()
		{
			if (this.backbuffer != null)
			{
				this.backbuffer.Invalidate();
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002432C File Offset: 0x0002252C
		private Control.DoubleBuffer GetBackBuffer()
		{
			if (this.backbuffer == null)
			{
				this.backbuffer = new Control.DoubleBuffer(this);
			}
			return this.backbuffer;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002434C File Offset: 0x0002254C
		private void DisposeBackBuffer()
		{
			if (this.backbuffer != null)
			{
				this.backbuffer.Dispose();
				this.backbuffer = null;
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002436C File Offset: 0x0002256C
		internal static void SetChildColor(Control parent)
		{
			for (int i = 0; i < parent.child_controls.Count; i++)
			{
				Control control = parent.child_controls[i];
				if (control.child_controls.Count > 0)
				{
					Control.SetChildColor(control);
				}
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000243BC File Offset: 0x000225BC
		internal bool Select(Control control)
		{
			if (control == null)
			{
				return false;
			}
			IContainerControl containerControl = this.GetContainerControl();
			if (containerControl != null && (Control)containerControl != control)
			{
				containerControl.ActiveControl = control;
				if (containerControl.ActiveControl == control && !control.has_focus && control.IsHandleCreated)
				{
					XplatUI.SetFocus(control.window.Handle);
				}
			}
			else if (control.IsHandleCreated)
			{
				XplatUI.SetFocus(control.window.Handle);
			}
			return true;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00024444 File Offset: 0x00022644
		internal virtual void DoDefaultAction()
		{
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00024448 File Offset: 0x00022648
		internal static IntPtr MakeParam(int low, int high)
		{
			return new IntPtr((high << 16) | (low & 65535));
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002445C File Offset: 0x0002265C
		internal static int LowOrder(int param)
		{
			return (int)((short)(param & 65535));
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00024468 File Offset: 0x00022668
		internal static int HighOrder(long param)
		{
			return (int)((short)(param >> 16));
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00024470 File Offset: 0x00022670
		internal virtual void PaintControlBackground(PaintEventArgs pevent)
		{
			bool flag = (this.CreateParams.Style & 2048) != 0;
			if (((this.BackColor.A != 255 && this.GetStyle(ControlStyles.SupportsTransparentBackColor)) || flag) && this.parent != null)
			{
				PaintEventArgs paintEventArgs = new PaintEventArgs(pevent.Graphics, new Rectangle(pevent.ClipRectangle.X + this.Left, pevent.ClipRectangle.Y + this.Top, pevent.ClipRectangle.Width, pevent.ClipRectangle.Height));
				GraphicsState graphicsState = paintEventArgs.Graphics.Save();
				paintEventArgs.Graphics.TranslateTransform((float)(-(float)this.Left), (float)(-(float)this.Top));
				this.parent.OnPaintBackground(paintEventArgs);
				paintEventArgs.Graphics.Restore(graphicsState);
				graphicsState = paintEventArgs.Graphics.Save();
				paintEventArgs.Graphics.TranslateTransform((float)(-(float)this.Left), (float)(-(float)this.Top));
				this.parent.OnPaint(paintEventArgs);
				paintEventArgs.Graphics.Restore(graphicsState);
				paintEventArgs.SetGraphics(null);
			}
			if (this.clip_region != null && XplatUI.UserClipWontExposeParent && this.parent != null)
			{
				Hwnd hwnd = Hwnd.ObjectFromHandle(this.Handle);
				if (hwnd != null)
				{
					PaintEventArgs paintEventArgs2 = new PaintEventArgs(pevent.Graphics, new Rectangle(pevent.ClipRectangle.X + this.Left, pevent.ClipRectangle.Y + this.Top, pevent.ClipRectangle.Width, pevent.ClipRectangle.Height));
					Region region = new Region();
					region.MakeEmpty();
					region.Union(this.ClientRectangle);
					foreach (Rectangle rectangle in hwnd.ClipRectangles)
					{
						region.Union(rectangle);
					}
					GraphicsState graphicsState2 = paintEventArgs2.Graphics.Save();
					paintEventArgs2.Graphics.Clip = region;
					paintEventArgs2.Graphics.TranslateTransform((float)(-(float)this.Left), (float)(-(float)this.Top));
					this.parent.OnPaintBackground(paintEventArgs2);
					paintEventArgs2.Graphics.Restore(graphicsState2);
					graphicsState2 = paintEventArgs2.Graphics.Save();
					paintEventArgs2.Graphics.Clip = region;
					paintEventArgs2.Graphics.TranslateTransform((float)(-(float)this.Left), (float)(-(float)this.Top));
					this.parent.OnPaint(paintEventArgs2);
					paintEventArgs2.Graphics.Restore(graphicsState2);
					paintEventArgs2.SetGraphics(null);
					region.Intersect(this.clip_region);
					pevent.Graphics.Clip = region;
				}
			}
			if (this.background_image == null)
			{
				if (!flag)
				{
					Rectangle rectangle2;
					rectangle2..ctor(pevent.ClipRectangle.X, pevent.ClipRectangle.Y, pevent.ClipRectangle.Width, pevent.ClipRectangle.Height);
					Brush solidBrush = ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor);
					pevent.Graphics.FillRectangle(solidBrush, rectangle2);
				}
				return;
			}
			this.DrawBackgroundImage(pevent.Graphics);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000247DC File Offset: 0x000229DC
		private void DrawBackgroundImage(Graphics g)
		{
			Rectangle rectangle = default(Rectangle);
			g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), this.ClientRectangle);
			switch (this.backgroundimage_layout)
			{
			case ImageLayout.None:
				rectangle.Location = Point.Empty;
				rectangle.Size = this.background_image.Size;
				break;
			case ImageLayout.Tile:
			{
				using (TextureBrush textureBrush = new TextureBrush(this.background_image, 0))
				{
					g.FillRectangle(textureBrush, this.ClientRectangle);
				}
				return;
			}
			case ImageLayout.Center:
				rectangle.Location = new Point(this.ClientSize.Width / 2 - this.background_image.Width / 2, this.ClientSize.Height / 2 - this.background_image.Height / 2);
				rectangle.Size = this.background_image.Size;
				break;
			case ImageLayout.Stretch:
				rectangle = this.ClientRectangle;
				break;
			case ImageLayout.Zoom:
				rectangle = this.ClientRectangle;
				if ((float)this.background_image.Width / (float)this.background_image.Height < (float)rectangle.Width / (float)rectangle.Height)
				{
					rectangle.Width = (int)((float)this.background_image.Width * ((float)rectangle.Height / (float)this.background_image.Height));
					rectangle.X = (this.ClientRectangle.Width - rectangle.Width) / 2;
				}
				else
				{
					rectangle.Height = (int)((float)this.background_image.Height * ((float)rectangle.Width / (float)this.background_image.Width));
					rectangle.Y = (this.ClientRectangle.Height - rectangle.Height) / 2;
				}
				break;
			default:
				return;
			}
			g.DrawImage(this.background_image, rectangle);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x000249F4 File Offset: 0x00022BF4
		internal virtual void DndEnter(DragEventArgs e)
		{
			try
			{
				this.OnDragEnter(e);
			}
			catch
			{
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00024A30 File Offset: 0x00022C30
		internal virtual void DndOver(DragEventArgs e)
		{
			try
			{
				this.OnDragOver(e);
			}
			catch
			{
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00024A6C File Offset: 0x00022C6C
		internal virtual void DndDrop(DragEventArgs e)
		{
			try
			{
				this.OnDragDrop(e);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("MWF: Exception while dropping:");
				Console.Error.WriteLine(ex);
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00024AC4 File Offset: 0x00022CC4
		internal virtual void DndLeave(EventArgs e)
		{
			try
			{
				this.OnDragLeave(e);
			}
			catch
			{
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00024B00 File Offset: 0x00022D00
		internal virtual void DndFeedback(GiveFeedbackEventArgs e)
		{
			try
			{
				this.OnGiveFeedback(e);
			}
			catch
			{
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00024B3C File Offset: 0x00022D3C
		internal virtual void DndContinueDrag(QueryContinueDragEventArgs e)
		{
			try
			{
				this.OnQueryContinueDrag(e);
			}
			catch
			{
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00024B78 File Offset: 0x00022D78
		internal static MouseButtons FromParamToMouseButtons(long param)
		{
			MouseButtons mouseButtons = MouseButtons.None;
			if ((param & 1L) != 0L)
			{
				mouseButtons |= MouseButtons.Left;
			}
			if ((param & 16L) != 0L)
			{
				mouseButtons |= MouseButtons.Middle;
			}
			if ((param & 2L) != 0L)
			{
				mouseButtons |= MouseButtons.Right;
			}
			return mouseButtons;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00024BBC File Offset: 0x00022DBC
		internal virtual void FireEnter()
		{
			this.OnEnter(EventArgs.Empty);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00024BCC File Offset: 0x00022DCC
		internal virtual void FireLeave()
		{
			this.OnLeave(EventArgs.Empty);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00024BDC File Offset: 0x00022DDC
		internal virtual void FireValidating(CancelEventArgs ce)
		{
			this.OnValidating(ce);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00024BE8 File Offset: 0x00022DE8
		internal virtual void FireValidated()
		{
			this.OnValidated(EventArgs.Empty);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00024BF8 File Offset: 0x00022DF8
		internal virtual bool ProcessControlMnemonic(char charCode)
		{
			return this.ProcessMnemonic(charCode);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00024C04 File Offset: 0x00022E04
		private static Control FindFlatForward(Control container, Control start)
		{
			Control control = null;
			int count = container.child_controls.Count;
			bool flag = false;
			int num;
			if (start != null)
			{
				num = start.tab_index;
			}
			else
			{
				num = -1;
			}
			for (int i = 0; i < count; i++)
			{
				if (start == container.child_controls[i])
				{
					flag = true;
				}
				else if ((control == null || control.tab_index > container.child_controls[i].tab_index) && (container.child_controls[i].tab_index > num || (flag && container.child_controls[i].tab_index == num)))
				{
					control = container.child_controls[i];
				}
			}
			return control;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00024CD0 File Offset: 0x00022ED0
		private static Control FindControlForward(Control container, Control start)
		{
			if (start == null)
			{
				return Control.FindFlatForward(container, start);
			}
			if (start.child_controls != null && start.child_controls.Count > 0 && (start == container || !(start is IContainerControl) || !start.GetStyle(ControlStyles.ContainerControl)))
			{
				return Control.FindControlForward(start, null);
			}
			while (start != container)
			{
				Control control = Control.FindFlatForward(start.parent, start);
				if (control != null)
				{
					return control;
				}
				start = start.parent;
			}
			return null;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00024D5C File Offset: 0x00022F5C
		private static Control FindFlatBackward(Control container, Control start)
		{
			Control control = null;
			int count = container.child_controls.Count;
			bool flag = false;
			int maxValue;
			if (start != null)
			{
				maxValue = start.tab_index;
			}
			else
			{
				maxValue = int.MaxValue;
			}
			for (int i = count - 1; i >= 0; i--)
			{
				if (start == container.child_controls[i])
				{
					flag = true;
				}
				else if ((control == null || control.tab_index < container.child_controls[i].tab_index) && (container.child_controls[i].tab_index < maxValue || (flag && container.child_controls[i].tab_index == maxValue)))
				{
					control = container.child_controls[i];
				}
			}
			return control;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00024E2C File Offset: 0x0002302C
		private static Control FindControlBackward(Control container, Control start)
		{
			Control control = null;
			if (start == null)
			{
				control = Control.FindFlatBackward(container, start);
			}
			else if (start != container && start.parent != null)
			{
				control = Control.FindFlatBackward(start.parent, start);
				if (control == null)
				{
					if (start.parent != container)
					{
						return start.parent;
					}
					return null;
				}
			}
			if (control == null || start.parent == null)
			{
				control = start;
			}
			while (control != null && (control == container || ((!(control is IContainerControl) || !control.GetStyle(ControlStyles.ContainerControl)) && control.child_controls != null && control.child_controls.Count > 0)))
			{
				control = Control.FindFlatBackward(control, null);
			}
			return control;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00024EEC File Offset: 0x000230EC
		internal virtual void HandleClick(int clicks, MouseEventArgs me)
		{
			bool style = this.GetStyle(ControlStyles.StandardClick);
			bool style2 = this.GetStyle(ControlStyles.StandardDoubleClick);
			if (clicks > 1 && style && style2)
			{
				this.OnDoubleClick(me);
				this.OnMouseDoubleClick(me);
			}
			else if (clicks == 1 && style && !this.ValidationFailed)
			{
				this.OnClick(me);
				this.OnMouseClick(me);
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00024F60 File Offset: 0x00023160
		internal void CaptureWithConfine(Control ConfineWindow)
		{
			if (this.IsHandleCreated && !this.is_captured)
			{
				this.is_captured = true;
				XplatUI.GrabWindow(this.window.Handle, ConfineWindow.Handle);
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00024FA0 File Offset: 0x000231A0
		private void CheckDataBindings()
		{
			if (this.data_bindings == null)
			{
				return;
			}
			foreach (object obj in this.data_bindings)
			{
				Binding binding = (Binding)obj;
				binding.Check();
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002501C File Offset: 0x0002321C
		private void ChangeParent(Control new_parent)
		{
			bool enabled = this.Enabled;
			bool visible = this.Visible;
			Font font = this.Font;
			Color foreColor = this.ForeColor;
			Color backColor = this.BackColor;
			RightToLeft rightToLeft = this.RightToLeft;
			this.parent = new_parent;
			Form form = this as Form;
			if (form != null)
			{
				form.ChangingParent(new_parent);
			}
			else if (this.IsHandleCreated)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (new_parent != null && new_parent.IsHandleCreated)
				{
					intPtr = new_parent.Handle;
				}
				XplatUI.SetParent(this.Handle, intPtr);
			}
			this.OnParentChanged(EventArgs.Empty);
			if (enabled != this.Enabled)
			{
				this.OnEnabledChanged(EventArgs.Empty);
			}
			if (visible != this.Visible)
			{
				this.OnVisibleChanged(EventArgs.Empty);
			}
			if (font != this.Font)
			{
				this.OnFontChanged(EventArgs.Empty);
			}
			if (foreColor != this.ForeColor)
			{
				this.OnForeColorChanged(EventArgs.Empty);
			}
			if (backColor != this.BackColor)
			{
				this.OnBackColorChanged(EventArgs.Empty);
			}
			if (rightToLeft != this.RightToLeft)
			{
				this.OnRightToLeftChanged(EventArgs.Empty);
			}
			if (new_parent != null && new_parent.Created && this.is_visible && !this.Created)
			{
				this.CreateControl();
			}
			if (this.binding_context == null && this.Created)
			{
				this.OnBindingContextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000251A0 File Offset: 0x000233A0
		internal Size InternalSizeFromClientSize(Size clientSize)
		{
			Rectangle rectangle;
			rectangle..ctor(0, 0, clientSize.Width, clientSize.Height);
			CreateParams createParams = this.CreateParams;
			Rectangle rectangle2;
			if (XplatUI.CalculateWindowRect(ref rectangle, createParams, null, out rectangle2))
			{
				return new Size(rectangle2.Width, rectangle2.Height);
			}
			return Size.Empty;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000251F4 File Offset: 0x000233F4
		internal Size ClientSizeFromSize(Size size)
		{
			Size size2 = this.InternalSizeFromClientSize(size);
			if (size2 == Size.Empty)
			{
				return Size.Empty;
			}
			return new Size(size.Width - (size2.Width - size.Width), size.Height - (size2.Height - size.Height));
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00025254 File Offset: 0x00023454
		internal CreateParams GetCreateParams()
		{
			return this.CreateParams;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002525C File Offset: 0x0002345C
		internal virtual Size GetPreferredSizeCore(Size proposedSize)
		{
			return this.explicit_bounds.Size;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002526C File Offset: 0x0002346C
		private void UpdateDistances()
		{
			if (this.parent != null)
			{
				if (this.bounds.Width >= 0)
				{
					this.dist_right = this.parent.ClientSize.Width - this.bounds.X - this.bounds.Width;
				}
				if (this.bounds.Height >= 0)
				{
					this.dist_bottom = this.parent.ClientSize.Height - this.bounds.Y - this.bounds.Height;
				}
				this.recalculate_distances = false;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00025310 File Offset: 0x00023510
		private Cursor GetAvailableCursor()
		{
			if (this.Cursor != null && this.Enabled)
			{
				return this.Cursor;
			}
			if (this.Parent != null)
			{
				return this.Parent.GetAvailableCursor();
			}
			return Cursors.Default;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002535C File Offset: 0x0002355C
		private void UpdateCursor()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			if (!this.Enabled)
			{
				XplatUI.SetCursor(this.window.Handle, this.GetAvailableCursor().handle);
				return;
			}
			Point point = this.PointToClient(Cursor.Position);
			if ((!this.bounds.Contains(point) && !this.Capture) || this.GetChildAtPoint(point) != null)
			{
				return;
			}
			if (this.cursor != null || this.use_wait_cursor)
			{
				XplatUI.SetCursor(this.window.Handle, this.Cursor.handle);
			}
			else
			{
				XplatUI.SetCursor(this.window.Handle, this.GetAvailableCursor().handle);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00025428 File Offset: 0x00023628
		private bool UseDoubleBuffering
		{
			get
			{
				return ThemeEngine.Current.DoubleBufferingSupported && (this.force_double_buffer || this.DoubleBuffered || (this.control_style & ControlStyles.DoubleBuffer) != (ControlStyles)0);
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00025474 File Offset: 0x00023674
		internal void OnSizeInitializedOrChanged()
		{
			Form form = this as Form;
			if (form != null && form.WindowManager != null)
			{
				ThemeEngine.Current.ManagedWindowOnSizeInitializedOrChanged(form);
			}
		}

		/// <summary>Gets the default background color of the control.</summary>
		/// <returns>The default background <see cref="T:System.Drawing.Color" /> of the control. The default is <see cref="P:System.Drawing.SystemColors.Control" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000254A4 File Offset: 0x000236A4
		public static Color DefaultBackColor
		{
			get
			{
				return ThemeEngine.Current.DefaultControlBackColor;
			}
		}

		/// <summary>Gets the default font of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Font" /> of the control. The value returned will vary depending on the user's operating system the local culture setting of their system.</returns>
		/// <exception cref="T:System.ArgumentException">The default font or the regional alternative fonts are not installed on the client computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x000254B0 File Offset: 0x000236B0
		public static Font DefaultFont
		{
			get
			{
				return ThemeEngine.Current.DefaultFont;
			}
		}

		/// <summary>Gets the default foreground color of the control.</summary>
		/// <returns>The default foreground <see cref="T:System.Drawing.Color" /> of the control. The default is <see cref="P:System.Drawing.SystemColors.ControlText" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000254BC File Offset: 0x000236BC
		public static Color DefaultForeColor
		{
			get
			{
				return ThemeEngine.Current.DefaultControlForeColor;
			}
		}

		/// <summary>Gets a value indicating which of the modifier keys (SHIFT, CTRL, and ALT) is in a pressed state.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.Keys" /> values. The default is <see cref="F:System.Windows.Forms.Keys.None" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000254C8 File Offset: 0x000236C8
		public static Keys ModifierKeys
		{
			get
			{
				return XplatUI.State.ModifierKeys;
			}
		}

		/// <summary>Gets a value indicating which of the mouse buttons is in a pressed state.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.MouseButtons" /> enumeration values. The default is <see cref="F:System.Windows.Forms.MouseButtons.None" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000254D0 File Offset: 0x000236D0
		public static MouseButtons MouseButtons
		{
			get
			{
				return XplatUI.State.MouseButtons;
			}
		}

		/// <summary>Gets the position of the mouse cursor in screen coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that contains the coordinates of the mouse cursor relative to the upper-left corner of the screen.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000254D8 File Offset: 0x000236D8
		public static Point MousePosition
		{
			get
			{
				return Cursor.Position;
			}
		}

		/// <summary>Gets or sets a value indicating whether to catch calls on the wrong thread that access a control's <see cref="P:System.Windows.Forms.Control.Handle" /> property when an application is being debugged.</summary>
		/// <returns>true if calls on the wrong thread are caught; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000254E0 File Offset: 0x000236E0
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x000254E8 File Offset: 0x000236E8
		[MonoTODO("Stub, value is not used")]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public static bool CheckForIllegalCrossThreadCalls
		{
			get
			{
				return Control.verify_thread_handle;
			}
			set
			{
				Control.verify_thread_handle = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x000254F0 File Offset: 0x000236F0
		[EditorBrowsable(2)]
		[Browsable(false)]
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
		/// <returns>The default action description of the control for use by accessibility client applications.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00025510 File Offset: 0x00023710
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x00025518 File Offset: 0x00023718
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public string AccessibleDefaultActionDescription
		{
			get
			{
				return this.accessible_default_action;
			}
			set
			{
				this.accessible_default_action = value;
			}
		}

		/// <summary>Gets or sets the description of the control used by accessibility client applications.</summary>
		/// <returns>The description of the control used by accessibility client applications. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00025524 File Offset: 0x00023724
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0002552C File Offset: 0x0002372C
		[DefaultValue(null)]
		[Localizable(true)]
		[MWFCategory("Accessibility")]
		public string AccessibleDescription
		{
			get
			{
				return this.accessible_description;
			}
			set
			{
				this.accessible_description = value;
			}
		}

		/// <summary>Gets or sets the name of the control used by accessibility client applications.</summary>
		/// <returns>The name of the control used by accessibility client applications. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00025538 File Offset: 0x00023738
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00025540 File Offset: 0x00023740
		[Localizable(true)]
		[MWFCategory("Accessibility")]
		[DefaultValue(null)]
		public string AccessibleName
		{
			get
			{
				return this.accessible_name;
			}
			set
			{
				this.accessible_name = value;
			}
		}

		/// <summary>Gets or sets the accessible role of the control </summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AccessibleRole" />. The default is Default.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0002554C File Offset: 0x0002374C
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x00025554 File Offset: 0x00023754
		[DefaultValue(AccessibleRole.Default)]
		[MWFCategory("Accessibility")]
		[MWFDescription("Role of the control")]
		public AccessibleRole AccessibleRole
		{
			get
			{
				return this.accessible_role;
			}
			set
			{
				this.accessible_role = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control can accept data that the user drags onto it.</summary>
		/// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00025560 File Offset: 0x00023760
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00025568 File Offset: 0x00023768
		[DefaultValue(false)]
		[MWFCategory("Behavior")]
		public virtual bool AllowDrop
		{
			get
			{
				return this.allow_drop;
			}
			set
			{
				if (this.allow_drop == value)
				{
					return;
				}
				this.allow_drop = value;
				if (this.IsHandleCreated)
				{
					this.UpdateStyles();
					XplatUI.SetAllowDrop(this.Handle, value);
				}
			}
		}

		/// <summary>Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent. </summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values. The default is Top and Left.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x000255A8 File Offset: 0x000237A8
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x000255B0 File Offset: 0x000237B0
		[RefreshProperties(2)]
		[MWFCategory("Layout")]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		[Localizable(true)]
		public virtual AnchorStyles Anchor
		{
			get
			{
				return this.anchor_style;
			}
			set
			{
				this.layout_type = Control.LayoutType.Anchor;
				if (this.anchor_style == value)
				{
					return;
				}
				this.anchor_style = value;
				this.dock_style = DockStyle.None;
				this.UpdateDistances();
				if (this.parent != null)
				{
					this.parent.PerformLayout(this, "Anchor");
				}
			}
		}

		/// <summary>Gets or sets where this control is scrolled to in <see cref="M:System.Windows.Forms.ScrollableControl.ScrollControlIntoView(System.Windows.Forms.Control)" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> specifying the scroll location. The default is the upper-left corner of the control.</returns>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00025604 File Offset: 0x00023804
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0002560C File Offset: 0x0002380C
		[DefaultValue(typeof(Point), "0, 0")]
		[EditorBrowsable(2)]
		[Browsable(false)]
		public virtual Point AutoScrollOffset
		{
			get
			{
				return this.auto_scroll_offset;
			}
			set
			{
				this.auto_scroll_offset = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00025618 File Offset: 0x00023818
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00025620 File Offset: 0x00023820
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DefaultValue(false)]
		[RefreshProperties(1)]
		[Localizable(true)]
		public virtual bool AutoSize
		{
			get
			{
				return this.auto_size;
			}
			set
			{
				if (this.auto_size != value)
				{
					this.auto_size = value;
					if (!value)
					{
						this.Size = this.explicit_bounds.Size;
					}
					else if (this.Parent != null)
					{
						this.Parent.PerformLayout(this, "AutoSize");
					}
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00025684 File Offset: 0x00023884
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0002568C File Offset: 0x0002388C
		[MWFCategory("Layout")]
		[AmbientValue("{Width=0, Height=0}")]
		public virtual Size MaximumSize
		{
			get
			{
				return this.maximum_size;
			}
			set
			{
				if (this.maximum_size != value)
				{
					this.maximum_size = value;
					this.Size = this.PreferredSize;
				}
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000256C0 File Offset: 0x000238C0
		internal bool ShouldSerializeMaximumSize()
		{
			return this.MaximumSize != this.DefaultMaximumSize;
		}

		/// <summary>Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000256D4 File Offset: 0x000238D4
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x000256DC File Offset: 0x000238DC
		[MWFCategory("Layout")]
		public virtual Size MinimumSize
		{
			get
			{
				return this.minimum_size;
			}
			set
			{
				if (this.minimum_size != value)
				{
					this.minimum_size = value;
					this.Size = this.PreferredSize;
				}
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00025710 File Offset: 0x00023910
		internal bool ShouldSerializeMinimumSize()
		{
			return this.MinimumSize != this.DefaultMinimumSize;
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00025724 File Offset: 0x00023924
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x00025788 File Offset: 0x00023988
		[DispId(-501)]
		[MWFCategory("Appearance")]
		public virtual Color BackColor
		{
			get
			{
				if (this.background_color.IsEmpty)
				{
					if (this.parent != null)
					{
						Color backColor = this.parent.BackColor;
						if (backColor.A == 255 || this.GetStyle(ControlStyles.SupportsTransparentBackColor))
						{
							return backColor;
						}
					}
					return Control.DefaultBackColor;
				}
				return this.background_color;
			}
			set
			{
				if (!value.IsEmpty && value.A != 255 && !this.GetStyle(ControlStyles.SupportsTransparentBackColor))
				{
					throw new ArgumentException("Transparent background colors are not supported on this control");
				}
				if (this.background_color != value)
				{
					this.background_color = value;
					Control.SetChildColor(this);
					this.OnBackColorChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000257FC File Offset: 0x000239FC
		internal bool ShouldSerializeBackColor()
		{
			return this.BackColor != Control.DefaultBackColor;
		}

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00025810 File Offset: 0x00023A10
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x00025818 File Offset: 0x00023A18
		[MWFCategory("Appearance")]
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
					this.OnBackgroundImageChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (<see cref="F:System.Windows.Forms.ImageLayout.Center" /> , <see cref="F:System.Windows.Forms.ImageLayout.None" />, <see cref="F:System.Windows.Forms.ImageLayout.Stretch" />, <see cref="F:System.Windows.Forms.ImageLayout.Tile" />, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom" />). <see cref="F:System.Windows.Forms.ImageLayout.Tile" /> is the default value.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified enumeration value does not exist. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0002584C File Offset: 0x00023A4C
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x00025854 File Offset: 0x00023A54
		[Localizable(true)]
		[MWFCategory("Appearance")]
		[DefaultValue(ImageLayout.Tile)]
		public virtual ImageLayout BackgroundImageLayout
		{
			get
			{
				return this.backgroundimage_layout;
			}
			set
			{
				if (Array.IndexOf(Enum.GetValues(typeof(ImageLayout)), value) == -1)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ImageLayout));
				}
				if (value != this.backgroundimage_layout)
				{
					this.backgroundimage_layout = value;
					this.Invalidate();
					this.OnBackgroundImageLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000258BC File Offset: 0x00023ABC
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x00025900 File Offset: 0x00023B00
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		[Browsable(false)]
		public virtual BindingContext BindingContext
		{
			get
			{
				if (this.binding_context != null)
				{
					return this.binding_context;
				}
				if (this.Parent == null)
				{
					return null;
				}
				this.binding_context = this.Parent.BindingContext;
				return this.binding_context;
			}
			set
			{
				if (this.binding_context != value)
				{
					this.binding_context = value;
					this.OnBindingContextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00025920 File Offset: 0x00023B20
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public int Bottom
		{
			get
			{
				return this.bounds.Y + this.bounds.Height;
			}
		}

		/// <summary>Gets or sets the size and location of the control including its nonclient elements, in pixels, relative to the parent control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> in pixels relative to the parent control that represents the size and location of the control including its nonclient elements.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002593C File Offset: 0x00023B3C
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00025944 File Offset: 0x00023B44
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
			set
			{
				this.SetBounds(value.Left, value.Top, value.Width, value.Height, BoundsSpecified.All);
			}
		}

		/// <summary>Gets a value indicating whether the control can receive focus.</summary>
		/// <returns>true if the control can receive focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00025978 File Offset: 0x00023B78
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public bool CanFocus
		{
			get
			{
				return this.IsHandleCreated && this.Visible && this.Enabled;
			}
		}

		/// <summary>Gets a value indicating whether the control can be selected.</summary>
		/// <returns>true if the control can be selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x000259AC File Offset: 0x00023BAC
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public bool CanSelect
		{
			get
			{
				if (!this.GetStyle(ControlStyles.Selectable))
				{
					return false;
				}
				for (Control control = this; control != null; control = control.parent)
				{
					if (!control.is_visible || !control.is_enabled)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000259F8 File Offset: 0x00023BF8
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00025A00 File Offset: 0x00023C00
		internal virtual bool InternalCapture
		{
			get
			{
				return this.Capture;
			}
			set
			{
				this.Capture = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control has captured the mouse.</summary>
		/// <returns>true if the control has captured the mouse; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00025A0C File Offset: 0x00023C0C
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00025A14 File Offset: 0x00023C14
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public bool Capture
		{
			get
			{
				return this.is_captured;
			}
			set
			{
				if (value != this.is_captured)
				{
					if (value)
					{
						this.is_captured = true;
						XplatUI.GrabWindow(this.Handle, IntPtr.Zero);
					}
					else
					{
						if (this.IsHandleCreated)
						{
							XplatUI.UngrabWindow(this.Handle);
						}
						this.is_captured = false;
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control causes validation to be performed on any controls that require validation when it receives focus.</summary>
		/// <returns>true if the control causes validation to be performed on any controls requiring validation when it receives focus; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00025A6C File Offset: 0x00023C6C
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x00025A74 File Offset: 0x00023C74
		[MWFCategory("Focus")]
		[DefaultValue(true)]
		public bool CausesValidation
		{
			get
			{
				return this.causes_validation;
			}
			set
			{
				if (this.causes_validation != value)
				{
					this.causes_validation = value;
					this.OnCausesValidationChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the rectangle that represents the client area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the client area of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00025A94 File Offset: 0x00023C94
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public Rectangle ClientRectangle
		{
			get
			{
				this.client_rect.Width = this.client_size.Width;
				this.client_rect.Height = this.client_size.Height;
				return this.client_rect;
			}
		}

		/// <summary>Gets or sets the height and width of the client area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the dimensions of the client area of the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00025AD4 File Offset: 0x00023CD4
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x00025ADC File Offset: 0x00023CDC
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		[Browsable(false)]
		public Size ClientSize
		{
			get
			{
				return this.client_size;
			}
			set
			{
				this.SetClientSizeCore(value.Width, value.Height);
				this.OnClientSizeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the name of the company or creator of the application containing the control.</summary>
		/// <returns>The company name or creator of the application containing the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00025B08 File Offset: 0x00023D08
		[Browsable(false)]
		[EditorBrowsable(2)]
		[Description("ControlCompanyNameDescr")]
		[DesignerSerializationVisibility(0)]
		public string CompanyName
		{
			get
			{
				return "Mono Project, Novell, Inc.";
			}
		}

		/// <summary>Gets a value indicating whether the control, or one of its child controls, currently has the input focus.</summary>
		/// <returns>true if the control or one of its child controls currently has the input focus; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00025B10 File Offset: 0x00023D10
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool ContainsFocus
		{
			get
			{
				IntPtr focus = XplatUI.GetFocus();
				if (this.IsHandleCreated)
				{
					if (focus == this.Handle)
					{
						return true;
					}
					for (int i = 0; i < this.child_controls.Count; i++)
					{
						if (this.child_controls[i].ContainsFocus)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> that represents the shortcut menu associated with the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00025B78 File Offset: 0x00023D78
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x00025B80 File Offset: 0x00023D80
		[MWFCategory("Behavior")]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ContextMenu ContextMenu
		{
			get
			{
				return this.ContextMenuInternal;
			}
			set
			{
				this.ContextMenuInternal = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00025B8C File Offset: 0x00023D8C
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x00025B94 File Offset: 0x00023D94
		internal virtual ContextMenu ContextMenuInternal
		{
			get
			{
				return this.context_menu;
			}
			set
			{
				if (this.context_menu != value)
				{
					this.context_menu = value;
					this.OnContextMenuChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or null if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is null.</returns>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00025BB4 File Offset: 0x00023DB4
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x00025BBC File Offset: 0x00023DBC
		[MWFCategory("Behavior")]
		[DefaultValue(null)]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.context_menu_strip;
			}
			set
			{
				if (this.context_menu_strip != value)
				{
					this.context_menu_strip = value;
					if (value != null)
					{
						value.container = this;
					}
					this.OnContextMenuStripChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the collection of controls contained within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" /> representing the collection of controls contained within the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00025BEC File Offset: 0x00023DEC
		[Browsable(false)]
		[DesignerSerializationVisibility(2)]
		public Control.ControlCollection Controls
		{
			get
			{
				return this.child_controls;
			}
		}

		/// <summary>Gets a value indicating whether the control has been created.</summary>
		/// <returns>true if the control has been created; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00025BF4 File Offset: 0x00023DF4
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		[Browsable(false)]
		public bool Created
		{
			get
			{
				return !this.is_disposed && this.is_created;
			}
		}

		/// <summary>Gets or sets the cursor that is displayed when the mouse pointer is over the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00025C0C File Offset: 0x00023E0C
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x00025C60 File Offset: 0x00023E60
		[AmbientValue(null)]
		[MWFCategory("Appearance")]
		public virtual Cursor Cursor
		{
			get
			{
				if (this.use_wait_cursor)
				{
					return Cursors.WaitCursor;
				}
				if (this.cursor != null)
				{
					return this.cursor;
				}
				if (this.parent != null)
				{
					return this.parent.Cursor;
				}
				return Cursors.Default;
			}
			set
			{
				if (this.cursor == value)
				{
					return;
				}
				this.cursor = value;
				this.UpdateCursor();
				this.OnCursorChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00025C98 File Offset: 0x00023E98
		internal bool ShouldSerializeCursor()
		{
			return this.Cursor != Cursors.Default;
		}

		/// <summary>Gets the data bindings for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects for the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00025CAC File Offset: 0x00023EAC
		[MWFCategory("Data")]
		[DesignerSerializationVisibility(2)]
		[ParenthesizePropertyName(true)]
		[RefreshProperties(1)]
		public ControlBindingsCollection DataBindings
		{
			get
			{
				if (this.data_bindings == null)
				{
					this.data_bindings = new ControlBindingsCollection(this);
				}
				return this.data_bindings;
			}
		}

		/// <summary>Gets the rectangle that represents the display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00025CCC File Offset: 0x00023ECC
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public virtual Rectangle DisplayRectangle
		{
			get
			{
				return this.ClientRectangle;
			}
		}

		/// <summary>Gets a value indicating whether the base <see cref="T:System.Windows.Forms.Control" /> class is in the process of disposing.</summary>
		/// <returns>true if the base <see cref="T:System.Windows.Forms.Control" /> class is in the process of disposing; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00025CD4 File Offset: 0x00023ED4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public bool Disposing
		{
			get
			{
				return this.is_disposed;
			}
		}

		/// <summary>Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00025CDC File Offset: 0x00023EDC
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x00025CE4 File Offset: 0x00023EE4
		[Localizable(true)]
		[MWFCategory("Layout")]
		[DefaultValue(DockStyle.None)]
		[RefreshProperties(2)]
		public virtual DockStyle Dock
		{
			get
			{
				return this.dock_style;
			}
			set
			{
				if (value != DockStyle.None)
				{
					this.layout_type = Control.LayoutType.Dock;
				}
				if (this.dock_style == value)
				{
					return;
				}
				if (!Enum.IsDefined(typeof(DockStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DockStyle));
				}
				this.dock_style = value;
				this.anchor_style = AnchorStyles.Top | AnchorStyles.Left;
				if (this.dock_style == DockStyle.None)
				{
					this.bounds = this.explicit_bounds;
					this.layout_type = Control.LayoutType.Anchor;
				}
				if (this.parent != null)
				{
					this.parent.PerformLayout(this, "Dock");
				}
				else if (this.Controls.Count > 0)
				{
					this.PerformLayout();
				}
				this.OnDockChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker.</summary>
		/// <returns>true if the surface of the control should be drawn using double buffering; otherwise, false.</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00025DAC File Offset: 0x00023FAC
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00025DC0 File Offset: 0x00023FC0
		protected virtual bool DoubleBuffered
		{
			get
			{
				return (this.control_style & ControlStyles.OptimizedDoubleBuffer) != (ControlStyles)0;
			}
			set
			{
				if (value == this.DoubleBuffered)
				{
					return;
				}
				if (value)
				{
					this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
				}
				else
				{
					this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
				}
			}
		}

		/// <summary>Supports rendering to the specified bitmap.</summary>
		/// <param name="bitmap">The bitmap to be drawn to.</param>
		/// <param name="targetBounds">The bounds within which the control is rendered.</param>
		// Token: 0x06000908 RID: 2312 RVA: 0x00025E00 File Offset: 0x00024000
		public void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.IntersectClip(targetBounds);
			graphics.IntersectClip(this.Bounds);
			PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, targetBounds);
			if (!this.GetStyle(ControlStyles.Opaque))
			{
				this.OnPaintBackground(paintEventArgs);
			}
			this.OnPaintBackgroundInternal(paintEventArgs);
			this.OnPaintInternal(paintEventArgs);
			if (!paintEventArgs.Handled)
			{
				this.OnPaint(paintEventArgs);
			}
			graphics.Dispose();
		}

		/// <summary>Gets or sets a value indicating whether the control can respond to user interaction.</summary>
		/// <returns>true if the control can respond to user interaction; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00025E68 File Offset: 0x00024068
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00025E90 File Offset: 0x00024090
		[Localizable(true)]
		[MWFCategory("Behavior")]
		[DispId(-514)]
		public bool Enabled
		{
			get
			{
				return this.is_enabled && (this.parent == null || this.parent.Enabled);
			}
			set
			{
				if (this.is_enabled == value)
				{
					return;
				}
				bool flag = this.is_enabled;
				this.is_enabled = value;
				if (!value)
				{
					this.UpdateCursor();
				}
				if (flag != value && !value && this.has_focus)
				{
					this.SelectNextControl(this, true, true, true, true);
				}
				this.OnEnabledChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00025EF4 File Offset: 0x000240F4
		internal bool ShouldSerializeEnabled()
		{
			return !this.Enabled;
		}

		/// <summary>Gets a value indicating whether the control has input focus.</summary>
		/// <returns>true if the control has focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00025F04 File Offset: 0x00024104
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public virtual bool Focused
		{
			get
			{
				return this.has_focus;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00025F0C File Offset: 0x0002410C
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00025F50 File Offset: 0x00024150
		[MWFCategory("Appearance")]
		[Localizable(true)]
		[DispId(-512)]
		[AmbientValue(null)]
		public virtual Font Font
		{
			[return: MarshalAs(44, MarshalTypeRef = System.Drawing.Font)]
			get
			{
				if (this.font != null)
				{
					return this.font;
				}
				if (this.parent != null)
				{
					Font font = this.parent.Font;
					if (font != null)
					{
						return font;
					}
				}
				return Control.DefaultFont;
			}
			[param: MarshalAs(44, MarshalTypeRef = System.Drawing.Font)]
			set
			{
				if (this.font != null && this.font == value)
				{
					return;
				}
				this.font = value;
				this.Invalidate();
				this.OnFontChanged(EventArgs.Empty);
				this.PerformLayout();
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00025F94 File Offset: 0x00024194
		internal bool ShouldSerializeFont()
		{
			return !this.Font.Equals(Control.DefaultFont);
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00025FAC File Offset: 0x000241AC
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x00025FEC File Offset: 0x000241EC
		[DispId(-513)]
		[MWFCategory("Appearance")]
		public virtual Color ForeColor
		{
			get
			{
				if (!this.foreground_color.IsEmpty)
				{
					return this.foreground_color;
				}
				if (this.parent != null)
				{
					return this.parent.ForeColor;
				}
				return Control.DefaultForeColor;
			}
			set
			{
				if (this.foreground_color != value)
				{
					this.foreground_color = value;
					this.Invalidate();
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00026018 File Offset: 0x00024218
		internal bool ShouldSerializeForeColor()
		{
			return this.ForeColor != Control.DefaultForeColor;
		}

		/// <summary>Gets the window handle that the control is bound to.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle (HWND) of the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0002602C File Offset: 0x0002422C
		[DispId(-515)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public IntPtr Handle
		{
			get
			{
				if (Control.verify_thread_handle && this.InvokeRequired)
				{
					throw new InvalidOperationException("Cross-thread access of handle detected. Handle access only valid on thread that created the control");
				}
				if (!this.IsHandleCreated)
				{
					this.CreateHandle();
				}
				return this.window.Handle;
			}
		}

		/// <summary>Gets a value indicating whether the control contains one or more child controls.</summary>
		/// <returns>true if the control contains one or more child controls; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00026078 File Offset: 0x00024278
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public bool HasChildren
		{
			get
			{
				return this.child_controls.Count > 0;
			}
		}

		/// <summary>Gets or sets the height of the control.</summary>
		/// <returns>The height of the control in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00026090 File Offset: 0x00024290
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x000260A0 File Offset: 0x000242A0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(0)]
		public int Height
		{
			get
			{
				return this.bounds.Height;
			}
			set
			{
				this.SetBounds(this.bounds.X, this.bounds.Y, this.bounds.Width, value, BoundsSpecified.Height);
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values. The default is <see cref="F:System.Windows.Forms.ImeMode.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ImeMode" /> enumeration values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x000260D8 File Offset: 0x000242D8
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00026108 File Offset: 0x00024308
		[Localizable(true)]
		[MWFCategory("Behavior")]
		[AmbientValue(ImeMode.Inherit)]
		public ImeMode ImeMode
		{
			get
			{
				if (this.ime_mode != ImeMode.Inherit)
				{
					return this.ime_mode;
				}
				if (this.parent != null)
				{
					return this.parent.ImeMode;
				}
				return ImeMode.NoControl;
			}
			set
			{
				if (this.ime_mode != value)
				{
					this.ime_mode = value;
					this.OnImeModeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00026128 File Offset: 0x00024328
		internal bool ShouldSerializeImeMode()
		{
			return this.ImeMode != ImeMode.NoControl;
		}

		/// <summary>Gets a value indicating whether the caller must call an invoke method when making method calls to the control because the caller is on a different thread than the one the control was created on.</summary>
		/// <returns>true if the control's <see cref="P:System.Windows.Forms.Control.Handle" /> was created on a different thread than the calling thread (indicating that you must make calls to the control through an invoke method); otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00026138 File Offset: 0x00024338
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public bool InvokeRequired
		{
			get
			{
				return this.creator_thread != null && this.creator_thread != Thread.CurrentThread;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is visible to accessibility applications.</summary>
		/// <returns>true if the control is visible to accessibility applications; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00026158 File Offset: 0x00024358
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00026160 File Offset: 0x00024360
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public bool IsAccessible
		{
			get
			{
				return this.is_accessible;
			}
			set
			{
				this.is_accessible = value;
			}
		}

		/// <summary>Gets a value indicating whether the control has been disposed of.</summary>
		/// <returns>true if the control has been disposed of; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002616C File Offset: 0x0002436C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public bool IsDisposed
		{
			get
			{
				return this.is_disposed;
			}
		}

		/// <summary>Gets a value indicating whether the control has a handle associated with it.</summary>
		/// <returns>true if a handle has been assigned to the control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00026174 File Offset: 0x00024374
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public bool IsHandleCreated
		{
			get
			{
				if (this.window == null || this.window.Handle == IntPtr.Zero)
				{
					return false;
				}
				Hwnd hwnd = Hwnd.ObjectFromHandle(this.window.Handle);
				return hwnd == null || !hwnd.zombie;
			}
		}

		/// <summary>Gets a value indicating whether the control is mirrored.</summary>
		/// <returns>true if the control is mirrored; otherwise, false.</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000261D0 File Offset: 0x000243D0
		[EditorBrowsable(2)]
		[MonoNotSupported("RTL is not supported")]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool IsMirrored
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a cached instance of the control's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the control's contents.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x000261D4 File Offset: 0x000243D4
		[Browsable(false)]
		[EditorBrowsable(2)]
		public virtual LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new DefaultLayout();
				}
				return this.layout_engine;
			}
		}

		/// <summary>Gets or sets the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x000261F4 File Offset: 0x000243F4
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00026204 File Offset: 0x00024404
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(0)]
		[Browsable(false)]
		public int Left
		{
			get
			{
				return this.bounds.X;
			}
			set
			{
				this.SetBounds(value, this.bounds.Y, this.bounds.Width, this.bounds.Height, BoundsSpecified.X);
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0002623C File Offset: 0x0002443C
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0002625C File Offset: 0x0002445C
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Point Location
		{
			get
			{
				return new Point(this.bounds.X, this.bounds.Y);
			}
			set
			{
				this.SetBounds(value.X, value.Y, this.bounds.Width, this.bounds.Height, BoundsSpecified.Location);
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00026294 File Offset: 0x00024494
		internal bool ShouldSerializeLocation()
		{
			return this.Location != new Point(0, 0);
		}

		/// <summary>Gets or sets the space between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between controls.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000262A8 File Offset: 0x000244A8
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x000262B0 File Offset: 0x000244B0
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Padding Margin
		{
			get
			{
				return this.margin;
			}
			set
			{
				if (this.margin != value)
				{
					this.margin = value;
					if (this.Parent != null)
					{
						this.Parent.PerformLayout(this, "Margin");
					}
					this.OnMarginChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000262FC File Offset: 0x000244FC
		internal bool ShouldSerializeMargin()
		{
			return this.Margin != this.DefaultMargin;
		}

		/// <summary>Gets or sets the name of the control.</summary>
		/// <returns>The name of the control. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00026310 File Offset: 0x00024510
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x00026318 File Offset: 0x00024518
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

		/// <summary>Gets or sets padding within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00026324 File Offset: 0x00024524
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0002632C File Offset: 0x0002452C
		[MWFCategory("Layout")]
		[Localizable(true)]
		public Padding Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				if (this.padding != value)
				{
					this.padding = value;
					this.OnPaddingChanged(EventArgs.Empty);
					if (this.AutoSize && this.Parent != null)
					{
						this.parent.PerformLayout(this, "Padding");
					}
					else
					{
						this.PerformLayout(this, "Padding");
					}
				}
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00026394 File Offset: 0x00024594
		internal bool ShouldSerializePadding()
		{
			return this.Padding != this.DefaultPadding;
		}

		/// <summary>Gets or sets the parent container of the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the parent or container control of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000263A8 File Offset: 0x000245A8
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x000263B0 File Offset: 0x000245B0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Control Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (value == this)
				{
					throw new ArgumentException("A circular control reference has been made. A control cannot be owned or parented to itself.");
				}
				if (this.parent != value)
				{
					if (value == null)
					{
						this.parent.Controls.Remove(this);
						this.parent = null;
						return;
					}
					value.Controls.Add(this);
				}
			}
		}

		/// <summary>Gets the size of a rectangular area into which the control can fit.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> containing the height and width, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00026408 File Offset: 0x00024608
		[Browsable(false)]
		public Size PreferredSize
		{
			get
			{
				return this.GetPreferredSize(Size.Empty);
			}
		}

		/// <summary>Gets the product name of the assembly containing the control.</summary>
		/// <returns>The product name of the assembly containing the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00026418 File Offset: 0x00024618
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string ProductName
		{
			get
			{
				Type typeFromHandle = typeof(AssemblyProductAttribute);
				Assembly assembly = base.GetType().Module.Assembly;
				object[] customAttributes = assembly.GetCustomAttributes(typeFromHandle, false);
				AssemblyProductAttribute assemblyProductAttribute = null;
				if (customAttributes != null && customAttributes.Length > 0)
				{
					assemblyProductAttribute = (AssemblyProductAttribute)customAttributes[0];
				}
				if (assemblyProductAttribute == null)
				{
					return base.GetType().Namespace;
				}
				return assemblyProductAttribute.Product;
			}
		}

		/// <summary>Gets the version of the assembly containing the control.</summary>
		/// <returns>The file version of the assembly containing the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0002647C File Offset: 0x0002467C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public string ProductVersion
		{
			get
			{
				Type typeFromHandle = typeof(AssemblyVersionAttribute);
				Assembly assembly = base.GetType().Module.Assembly;
				object[] customAttributes = assembly.GetCustomAttributes(typeFromHandle, false);
				if (customAttributes == null || customAttributes.Length < 1)
				{
					return "1.0.0.0";
				}
				return ((AssemblyVersionAttribute)customAttributes[0]).Version;
			}
		}

		/// <summary>Gets a value indicating whether the control is currently re-creating its handle.</summary>
		/// <returns>true if the control is currently re-creating its handle; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000264D0 File Offset: 0x000246D0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public bool RecreatingHandle
		{
			get
			{
				return this.is_recreating;
			}
		}

		/// <summary>Gets or sets the window region associated with the control.</summary>
		/// <returns>The window <see cref="T:System.Drawing.Region" /> associated with the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000264D8 File Offset: 0x000246D8
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x000264E0 File Offset: 0x000246E0
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Region Region
		{
			get
			{
				return this.clip_region;
			}
			set
			{
				if (this.clip_region != value)
				{
					if (this.IsHandleCreated)
					{
						XplatUI.SetClipRegion(this.Handle, value);
					}
					this.clip_region = value;
					this.OnRegionChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the distance, in pixels, between the right edge of the control and the left edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the right edge of the control and the left edge of its container's client area.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00026518 File Offset: 0x00024718
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public int Right
		{
			get
			{
				return this.bounds.X + this.bounds.Width;
			}
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00026534 File Offset: 0x00024734
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00026564 File Offset: 0x00024764
		[MWFCategory("Appearance")]
		[Localizable(true)]
		[AmbientValue(RightToLeft.Inherit)]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				if (this.right_to_left != RightToLeft.Inherit)
				{
					return this.right_to_left;
				}
				if (this.parent != null)
				{
					return this.parent.RightToLeft;
				}
				return RightToLeft.No;
			}
			set
			{
				if (value != this.right_to_left)
				{
					this.right_to_left = value;
					this.OnRightToLeftChanged(EventArgs.Empty);
					this.PerformLayout();
				}
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00026598 File Offset: 0x00024798
		internal bool ShouldSerializeRightToLeft()
		{
			return this.RightToLeft != RightToLeft.No;
		}

		/// <summary>Gets or sets the site of the control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x000265A8 File Offset: 0x000247A8
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x000265B0 File Offset: 0x000247B0
		[EditorBrowsable(2)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (value != null)
				{
					AmbientProperties ambientProperties = (AmbientProperties)value.GetService(typeof(AmbientProperties));
					if (ambientProperties != null)
					{
						this.BackColor = ambientProperties.BackColor;
						this.ForeColor = ambientProperties.ForeColor;
						this.Cursor = ambientProperties.Cursor;
						this.Font = ambientProperties.Font;
					}
				}
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00026618 File Offset: 0x00024818
		internal bool ShouldSerializeSite()
		{
			return false;
		}

		/// <summary>Gets or sets the height and width of the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that represents the height and width of the control in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0002661C File Offset: 0x0002481C
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x00026630 File Offset: 0x00024830
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
			set
			{
				this.SetBounds(this.bounds.X, this.bounds.Y, value.Width, value.Height, BoundsSpecified.Size);
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002666C File Offset: 0x0002486C
		internal virtual bool ShouldSerializeSize()
		{
			return this.Size != this.DefaultSize;
		}

		/// <summary>Gets or sets the tab order of the control within its container.</summary>
		/// <returns>The index value of the control within the set of controls within its container. The controls in the container are included in the tab order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00026680 File Offset: 0x00024880
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00026698 File Offset: 0x00024898
		[Localizable(true)]
		[MergableProperty(false)]
		[MWFCategory("Behavior")]
		public int TabIndex
		{
			get
			{
				if (this.tab_index != -1)
				{
					return this.tab_index;
				}
				return 0;
			}
			set
			{
				if (this.tab_index != value)
				{
					this.tab_index = value;
					this.OnTabIndexChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>true if the user can give the focus to the control using the TAB key; otherwise, false. The default is true.Note:This property will always return true for an instance of the <see cref="T:System.Windows.Forms.Form" /> class.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x000266B8 File Offset: 0x000248B8
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x000266C0 File Offset: 0x000248C0
		[DispId(-516)]
		[MWFCategory("Behavior")]
		[DefaultValue(true)]
		public bool TabStop
		{
			get
			{
				return this.tab_stop;
			}
			set
			{
				if (this.tab_stop != value)
				{
					this.tab_stop = value;
					this.OnTabStopChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the object that contains data about the control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000266E0 File Offset: 0x000248E0
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x000266E8 File Offset: 0x000248E8
		[Bindable(true)]
		[MWFCategory("Data")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
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

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x000266F4 File Offset: 0x000248F4
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x000266FC File Offset: 0x000248FC
		[DispId(-517)]
		[Localizable(true)]
		[Bindable(true)]
		[MWFCategory("Appearance")]
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.text != value)
				{
					this.text = value;
					this.UpdateWindowText();
					this.OnTextChanged(EventArgs.Empty);
					if (this.AutoSize && this.Parent != null && !(this is Label))
					{
						this.Parent.PerformLayout(this, "Text");
					}
				}
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00026774 File Offset: 0x00024974
		internal virtual void UpdateWindowText()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			XplatUI.Text(this.Handle, this.text);
		}

		/// <summary>Gets or sets the distance, in pixels, between the top edge of the control and the top edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00026794 File Offset: 0x00024994
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x000267A4 File Offset: 0x000249A4
		[EditorBrowsable(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int Top
		{
			get
			{
				return this.bounds.Y;
			}
			set
			{
				this.SetBounds(this.bounds.X, value, this.bounds.Width, this.bounds.Height, BoundsSpecified.Y);
			}
		}

		/// <summary>Gets the parent control that is not parented by another Windows Forms control. Typically, this is the outermost <see cref="T:System.Windows.Forms.Form" /> that the control is contained in.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the top-level control that contains the current control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x000267DC File Offset: 0x000249DC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public Control TopLevelControl
		{
			get
			{
				Control control = this;
				while (control.parent != null)
				{
					control = control.parent;
				}
				return (!(control is Form)) ? null : control;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the wait cursor for the current control and all child controls.</summary>
		/// <returns>true to use the wait cursor for the current control and all child controls; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00026814 File Offset: 0x00024A14
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0002681C File Offset: 0x00024A1C
		[DefaultValue(false)]
		[Browsable(true)]
		[EditorBrowsable(0)]
		[MWFCategory("Appearance")]
		public bool UseWaitCursor
		{
			get
			{
				return this.use_wait_cursor;
			}
			set
			{
				if (this.use_wait_cursor != value)
				{
					this.use_wait_cursor = value;
					this.UpdateCursor();
					this.OnCursorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control and all its child controls are displayed.</summary>
		/// <returns>true if the control and all its child controls are displayed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00026850 File Offset: 0x00024A50
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00026878 File Offset: 0x00024A78
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public bool Visible
		{
			get
			{
				return this.is_visible && (this.parent == null || this.parent.Visible);
			}
			set
			{
				if (this.is_visible != value)
				{
					this.SetVisibleCore(value);
					if (this.parent != null)
					{
						this.parent.PerformLayout(this, "Visible");
					}
				}
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000268AC File Offset: 0x00024AAC
		internal bool ShouldSerializeVisible()
		{
			return !this.Visible;
		}

		/// <summary>Gets or sets the width of the control.</summary>
		/// <returns>The width of the control in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x000268BC File Offset: 0x00024ABC
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x000268CC File Offset: 0x00024ACC
		[Browsable(false)]
		[EditorBrowsable(0)]
		[DesignerSerializationVisibility(0)]
		public int Width
		{
			get
			{
				return this.bounds.Width;
			}
			set
			{
				this.SetBounds(this.bounds.X, this.bounds.Y, value, this.bounds.Height, BoundsSpecified.Width);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IWindowTarget" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00026904 File Offset: 0x00024B04
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0002690C File Offset: 0x00024B0C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public IWindowTarget WindowTarget
		{
			get
			{
				return this.window_target;
			}
			set
			{
				this.window_target = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00026918 File Offset: 0x00024B18
		protected virtual bool CanEnableIme
		{
			get
			{
				return false;
			}
		}

		/// <summary>Determines if events can be raised on the control.</summary>
		/// <returns>true if the control is hosted as an ActiveX control whose events are not frozen; otherwise, false.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0002691C File Offset: 0x00024B1C
		protected override bool CanRaiseEvents
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00026920 File Offset: 0x00024B20
		protected virtual CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = new CreateParams();
				try
				{
					createParams.Caption = this.Text;
				}
				catch
				{
					createParams.Caption = this.text;
				}
				try
				{
					createParams.X = this.Left;
				}
				catch
				{
					createParams.X = this.bounds.X;
				}
				try
				{
					createParams.Y = this.Top;
				}
				catch
				{
					createParams.Y = this.bounds.Y;
				}
				try
				{
					createParams.Width = this.Width;
				}
				catch
				{
					createParams.Width = this.bounds.Width;
				}
				try
				{
					createParams.Height = this.Height;
				}
				catch
				{
					createParams.Height = this.bounds.Height;
				}
				createParams.ClassName = XplatUI.DefaultClassName;
				createParams.ClassStyle = 40;
				createParams.ExStyle = 0;
				createParams.Param = 0;
				if (this.allow_drop)
				{
					createParams.ExStyle |= 16;
				}
				if (this.parent != null && this.parent.IsHandleCreated)
				{
					createParams.Parent = this.parent.Handle;
				}
				createParams.Style = 1174405120;
				if (this.is_visible)
				{
					createParams.Style |= 268435456;
				}
				if (!this.is_enabled)
				{
					createParams.Style |= 134217728;
				}
				BorderStyle borderStyle = this.border_style;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				createParams.control = this;
				return createParams;
			}
		}

		/// <summary>Gets or sets the default cursor for the control.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00026B74 File Offset: 0x00024D74
		protected virtual Cursor DefaultCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00026B7C File Offset: 0x00024D7C
		protected virtual ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Inherit;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the default space between controls.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00026B80 File Offset: 0x00024D80
		protected virtual Padding DefaultMargin
		{
			get
			{
				return new Padding(3);
			}
		}

		/// <summary>Gets the length and height, in pixels, that is specified as the default maximum size of a control.</summary>
		/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> representing the size of the control.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00026B88 File Offset: 0x00024D88
		protected virtual Size DefaultMaximumSize
		{
			get
			{
				return default(Size);
			}
		}

		/// <summary>Gets the length and height, in pixels, that is specified as the default minimum size of a control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the size of the control.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00026BA0 File Offset: 0x00024DA0
		protected virtual Size DefaultMinimumSize
		{
			get
			{
				return default(Size);
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the contents of a control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the internal spacing of the contents of a control.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00026BB8 File Offset: 0x00024DB8
		protected virtual Padding DefaultPadding
		{
			get
			{
				return default(Padding);
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00026BD0 File Offset: 0x00024DD0
		protected virtual Size DefaultSize
		{
			get
			{
				return new Size(0, 0);
			}
		}

		/// <summary>Gets or sets the height of the font of the control.</summary>
		/// <returns>The height of the <see cref="T:System.Drawing.Font" /> of the control in pixels.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00026BDC File Offset: 0x00024DDC
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00026BEC File Offset: 0x00024DEC
		protected int FontHeight
		{
			get
			{
				return this.Font.Height;
			}
			set
			{
			}
		}

		/// <summary>This property is now obsolete.</summary>
		/// <returns>true if the control is rendered from right to left; otherwise, false. The default is false.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00026BF0 File Offset: 0x00024DF0
		[Obsolete]
		protected bool RenderRightToLeft
		{
			get
			{
				return this.right_to_left == RightToLeft.Yes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control redraws itself when resized.</summary>
		/// <returns>true if the control redraws itself when resized; otherwise, false.</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00026BFC File Offset: 0x00024DFC
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00026C08 File Offset: 0x00024E08
		protected bool ResizeRedraw
		{
			get
			{
				return this.GetStyle(ControlStyles.ResizeRedraw);
			}
			set
			{
				this.SetStyle(ControlStyles.ResizeRedraw, value);
			}
		}

		/// <summary>Gets a value that determines the scaling of child controls. </summary>
		/// <returns>true if child controls will be scaled when the <see cref="M:System.Windows.Forms.Control.Scale(System.Single)" /> method on this control is called; otherwise, false. The default is true.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00026C14 File Offset: 0x00024E14
		[EditorBrowsable(2)]
		protected virtual bool ScaleChildren
		{
			get
			{
				return this.ScaleChildrenInternal;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00026C1C File Offset: 0x00024E1C
		internal virtual bool ScaleChildrenInternal
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the control should display focus rectangles.</summary>
		/// <returns>true if the control should display focus rectangles; otherwise, false.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00026C20 File Offset: 0x00024E20
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		protected internal virtual bool ShowFocusCues
		{
			get
			{
				if (this is Form)
				{
					return this.show_focus_cues;
				}
				if (this.parent == null)
				{
					return false;
				}
				Form form = this.FindForm();
				return form != null && form.show_focus_cues;
			}
		}

		/// <summary>Gets a value indicating whether the user interface is in the appropriate state to show or hide keyboard accelerators.</summary>
		/// <returns>true if the keyboard accelerators are visible; otherwise, false.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00026C64 File Offset: 0x00024E64
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		protected internal virtual bool ShowKeyboardCues
		{
			get
			{
				return this.ShowKeyboardCuesInternal;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00026C6C File Offset: 0x00024E6C
		internal bool ShowKeyboardCuesInternal
		{
			get
			{
				return SystemInformation.MenuAccessKeysUnderlined || base.DesignMode || this.show_keyboard_cues;
			}
		}

		/// <summary>Retrieves the control that contains the specified handle.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the control associated with the specified handle; returns null if no control with the specified handle is found.</returns>
		/// <param name="handle">The window handle (HWND) to search for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000969 RID: 2409 RVA: 0x00026C8C File Offset: 0x00024E8C
		[EditorBrowsable(2)]
		public static Control FromChildHandle(IntPtr handle)
		{
			return Control.ControlNativeWindow.ControlFromChildHandle(handle);
		}

		/// <summary>Returns the control that is currently associated with the specified handle.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control associated with the specified handle; returns null if no control with the specified handle is found.</returns>
		/// <param name="handle">The window handle (HWND) to search for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600096A RID: 2410 RVA: 0x00026C94 File Offset: 0x00024E94
		[EditorBrowsable(2)]
		public static Control FromHandle(IntPtr handle)
		{
			return Control.ControlNativeWindow.ControlFromHandle(handle);
		}

		/// <summary>Determines whether the CAPS LOCK, NUM LOCK, or SCROLL LOCK key is in effect.</summary>
		/// <returns>true if the specified key or keys are in effect; otherwise, false.</returns>
		/// <param name="keyVal">The CAPS LOCK, NUM LOCK, or SCROLL LOCK member of the <see cref="T:System.Windows.Forms.Keys" /> enumeration. </param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="keyVal" /> parameter refers to a key other than the CAPS LOCK, NUM LOCK, or SCROLL LOCK key. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600096B RID: 2411 RVA: 0x00026C9C File Offset: 0x00024E9C
		[MonoTODO("Only implemented for Win32, others always return false")]
		public static bool IsKeyLocked(Keys keyVal)
		{
			if (keyVal != Keys.NumLock && keyVal != Keys.Scroll && keyVal != Keys.CapsLock)
			{
				throw new NotSupportedException("keyVal must be CapsLock, NumLock, or ScrollLock");
			}
			return XplatUI.IsKeyLocked((VirtualKeys)keyVal);
		}

		/// <summary>Determines if the specified character is the mnemonic character assigned to the control in the specified string.</summary>
		/// <returns>true if the <paramref name="charCode" /> character is the mnemonic character assigned to the control; otherwise, false.</returns>
		/// <param name="charCode">The character to test. </param>
		/// <param name="text">The string to search. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600096C RID: 2412 RVA: 0x00026CE0 File Offset: 0x00024EE0
		public static bool IsMnemonic(char charCode, string text)
		{
			int num = text.IndexOf('&');
			return num != -1 && num + 1 < text.Length && text.get_Chars(num + 1) != '&' && char.ToUpper(charCode) == char.ToUpper(text.ToCharArray(num + 1, 1)[0]);
		}

		/// <summary>Reflects the specified message to the control that is bound to the specified handle.</summary>
		/// <returns>true if the message was reflected; otherwise, false.</returns>
		/// <param name="hWnd">An <see cref="T:System.IntPtr" /> representing the handle of the control to reflect the message to. </param>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> representing the Windows message to reflect. </param>
		// Token: 0x0600096D RID: 2413 RVA: 0x00026D3C File Offset: 0x00024F3C
		[EditorBrowsable(2)]
		protected static bool ReflectMessage(IntPtr hWnd, ref Message m)
		{
			Control control = Control.FromHandle(hWnd);
			if (control != null)
			{
				control.WndProc(ref m);
				return true;
			}
			return false;
		}

		/// <summary>Executes the specified delegate asynchronously on the thread that the control's underlying handle was created on.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the result of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> operation.</returns>
		/// <param name="method">A delegate to a method that takes no parameters. </param>
		/// <exception cref="T:System.InvalidOperationException">No appropriate window handle can be found.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600096E RID: 2414 RVA: 0x00026D60 File Offset: 0x00024F60
		[EditorBrowsable(2)]
		public IAsyncResult BeginInvoke(Delegate method)
		{
			object[] array = null;
			if (method is EventHandler)
			{
				array = new object[]
				{
					this,
					EventArgs.Empty
				};
			}
			return this.BeginInvokeInternal(method, array);
		}

		/// <summary>Executes the specified delegate asynchronously with the specified arguments, on the thread that the control's underlying handle was created on.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the result of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> operation.</returns>
		/// <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the <paramref name="args" /> parameter. </param>
		/// <param name="args">An array of objects to pass as arguments to the given method. This can be null if no arguments are needed. </param>
		/// <exception cref="T:System.InvalidOperationException">No appropriate window handle can be found.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600096F RID: 2415 RVA: 0x00026D98 File Offset: 0x00024F98
		[EditorBrowsable(2)]
		public IAsyncResult BeginInvoke(Delegate method, params object[] args)
		{
			return this.BeginInvokeInternal(method, args);
		}

		/// <summary>Brings the control to the front of the z-order.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000970 RID: 2416 RVA: 0x00026DA4 File Offset: 0x00024FA4
		public void BringToFront()
		{
			if (this.parent != null)
			{
				this.parent.child_controls.SetChildIndex(this, 0);
			}
			else if (this.IsHandleCreated)
			{
				XplatUI.SetZOrder(this.Handle, IntPtr.Zero, false, false);
			}
		}

		/// <summary>Retrieves a value indicating whether the specified control is a child of the control.</summary>
		/// <returns>true if the specified control is a child of the control; otherwise, false.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000971 RID: 2417 RVA: 0x00026DF4 File Offset: 0x00024FF4
		public bool Contains(Control ctl)
		{
			while (ctl != null)
			{
				ctl = ctl.parent;
				if (ctl == this)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Forces the creation of the visible control, including the creation of the handle and any visible child controls.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000972 RID: 2418 RVA: 0x00026E14 File Offset: 0x00025014
		public void CreateControl()
		{
			if (this.is_created)
			{
				return;
			}
			if (this.is_disposing)
			{
				return;
			}
			if (!this.is_visible)
			{
				return;
			}
			if (this.parent != null && !this.parent.Created)
			{
				return;
			}
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			if (!this.is_created)
			{
				this.is_created = true;
				foreach (Control control in this.Controls.GetAllControls())
				{
					if (!control.Created && !control.IsDisposed)
					{
						control.CreateControl();
					}
				}
				this.OnCreateControl();
			}
		}

		/// <summary>Creates the <see cref="T:System.Drawing.Graphics" /> for the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> for the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000973 RID: 2419 RVA: 0x00026ECC File Offset: 0x000250CC
		public Graphics CreateGraphics()
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			return Graphics.FromHwnd(this.window.Handle);
		}

		/// <summary>Begins a drag-and-drop operation.</summary>
		/// <returns>A value from the <see cref="T:System.Windows.Forms.DragDropEffects" /> enumeration that represents the final effect that was performed during the drag-and-drop operation.</returns>
		/// <param name="data">The data to drag. </param>
		/// <param name="allowedEffects">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000974 RID: 2420 RVA: 0x00026EF0 File Offset: 0x000250F0
		public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
		{
			DragDropEffects dragDropEffects = DragDropEffects.None;
			if (this.IsHandleCreated)
			{
				dragDropEffects = XplatUI.StartDrag(this.Handle, data, allowedEffects);
			}
			this.OnDragDropEnd(dragDropEffects);
			return dragDropEffects;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00026F20 File Offset: 0x00025120
		internal virtual void OnDragDropEnd(DragDropEffects effects)
		{
		}

		/// <summary>Retrieves the return value of the asynchronous operation represented by the <see cref="T:System.IAsyncResult" /> passed.</summary>
		/// <returns>The <see cref="T:System.Object" /> generated by the asynchronous operation.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents a specific invoke asynchronous operation, returned when calling <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter value is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> object was not created by a preceding call of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> method from the same control. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000976 RID: 2422 RVA: 0x00026F24 File Offset: 0x00025124
		[EditorBrowsable(2)]
		public object EndInvoke(IAsyncResult asyncResult)
		{
			AsyncMethodResult asyncMethodResult = (AsyncMethodResult)asyncResult;
			return asyncMethodResult.EndInvoke();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00026F40 File Offset: 0x00025140
		internal Control FindRootParent()
		{
			Control control = this;
			while (control.Parent != null)
			{
				control = control.Parent;
			}
			return control;
		}

		/// <summary>Retrieves the form that the control is on.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> that the control is on.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x06000978 RID: 2424 RVA: 0x00026F68 File Offset: 0x00025168
		public Form FindForm()
		{
			for (Control control = this; control != null; control = control.Parent)
			{
				if (control is Form)
				{
					return (Form)control;
				}
			}
			return null;
		}

		/// <summary>Sets input focus to the control.</summary>
		/// <returns>true if the input focus request was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000979 RID: 2425 RVA: 0x00026F9C File Offset: 0x0002519C
		[EditorBrowsable(2)]
		public bool Focus()
		{
			return this.FocusInternal(false);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00026FA8 File Offset: 0x000251A8
		internal virtual bool FocusInternal(bool skip_check)
		{
			if (skip_check || (this.CanFocus && this.IsHandleCreated && !this.has_focus && !this.is_focusing))
			{
				this.is_focusing = true;
				this.Select(this);
				this.is_focusing = false;
			}
			return this.has_focus;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00027004 File Offset: 0x00025204
		internal Control GetRealChildAtPoint(Point pt)
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			Control[] allControls = this.child_controls.GetAllControls();
			int i = 0;
			while (i < allControls.Length)
			{
				Control control = allControls[i];
				if (control.Bounds.Contains(this.PointToClient(pt)))
				{
					Control realChildAtPoint = control.GetRealChildAtPoint(pt);
					if (realChildAtPoint == null)
					{
						return control;
					}
					return realChildAtPoint;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		/// <summary>Retrieves the child control that is located at the specified coordinates.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control that is located at the specified point.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that contains the coordinates where you want to look for a control. Coordinates are expressed relative to the upper-left corner of the control's client area. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600097C RID: 2428 RVA: 0x00027074 File Offset: 0x00025274
		public Control GetChildAtPoint(Point pt)
		{
			return this.GetChildAtPoint(pt, GetChildAtPointSkip.None);
		}

		/// <summary>Retrieves the child control that is located at the specified coordinates, specifying whether to ignore child controls of a certain type.</summary>
		/// <returns>The child <see cref="T:System.Windows.Forms.Control" /> at the specified coordinates.</returns>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that contains the coordinates where you want to look for a control. Coordinates are expressed relative to the upper-left corner of the control's client area.</param>
		/// <param name="skipValue">One of the values of <see cref="T:System.Windows.Forms.GetChildAtPointSkip" />, determining whether to ignore child controls of a certain type.</param>
		// Token: 0x0600097D RID: 2429 RVA: 0x00027080 File Offset: 0x00025280
		public Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if ((skipValue & GetChildAtPointSkip.Disabled) != GetChildAtPointSkip.Disabled || control.Enabled)
				{
					if ((skipValue & GetChildAtPointSkip.Invisible) != GetChildAtPointSkip.Invisible || control.Visible)
					{
						if ((skipValue & GetChildAtPointSkip.Transparent) != GetChildAtPointSkip.Transparent || control.BackColor.A != 0)
						{
							if (control.Bounds.Contains(pt))
							{
								return control;
							}
						}
					}
				}
			}
			return null;
		}

		/// <summary>Returns the next <see cref="T:System.Windows.Forms.ContainerControl" /> up the control's chain of parent controls.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IContainerControl" />, that represents the parent of the <see cref="T:System.Windows.Forms.Control" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600097E RID: 2430 RVA: 0x00027170 File Offset: 0x00025370
		public IContainerControl GetContainerControl()
		{
			for (Control control = this; control != null; control = control.parent)
			{
				if (control is IContainerControl && (control.control_style & ControlStyles.ContainerControl) != (ControlStyles)0)
				{
					return (IContainerControl)control;
				}
			}
			return null;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000271B4 File Offset: 0x000253B4
		internal ContainerControl InternalGetContainerControl()
		{
			for (Control control = this; control != null; control = control.parent)
			{
				if (control is ContainerControl && (control.control_style & ControlStyles.ContainerControl) != (ControlStyles)0)
				{
					return control as ContainerControl;
				}
			}
			return null;
		}

		/// <summary>Retrieves the next control forward or back in the tab order of child controls.</summary>
		/// <returns>The next <see cref="T:System.Windows.Forms.Control" /> in the tab order.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> to start the search with. </param>
		/// <param name="forward">true to search forward in the tab order; false to search backward. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000980 RID: 2432 RVA: 0x000271F8 File Offset: 0x000253F8
		public Control GetNextControl(Control ctl, bool forward)
		{
			if (!this.Contains(ctl))
			{
				ctl = this;
			}
			if (forward)
			{
				ctl = Control.FindControlForward(this, ctl);
			}
			else
			{
				ctl = Control.FindControlBackward(this, ctl);
			}
			if (ctl != this)
			{
				return ctl;
			}
			return null;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="proposedSize">The custom-sized area for a control. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000981 RID: 2433 RVA: 0x0002723C File Offset: 0x0002543C
		[EditorBrowsable(2)]
		public virtual Size GetPreferredSize(Size proposedSize)
		{
			Size preferredSizeCore = this.GetPreferredSizeCore(proposedSize);
			if (this.maximum_size.Width != 0 && preferredSizeCore.Width > this.maximum_size.Width)
			{
				preferredSizeCore.Width = this.maximum_size.Width;
			}
			if (this.maximum_size.Height != 0 && preferredSizeCore.Height > this.maximum_size.Height)
			{
				preferredSizeCore.Height = this.maximum_size.Height;
			}
			if (this.minimum_size.Width != 0 && preferredSizeCore.Width < this.minimum_size.Width)
			{
				preferredSizeCore.Width = this.minimum_size.Width;
			}
			if (this.minimum_size.Height != 0 && preferredSizeCore.Height < this.minimum_size.Height)
			{
				preferredSizeCore.Height = this.minimum_size.Height;
			}
			return preferredSizeCore;
		}

		/// <summary>Conceals the control from the user.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000982 RID: 2434 RVA: 0x00027338 File Offset: 0x00025538
		public void Hide()
		{
			this.Visible = false;
		}

		/// <summary>Invalidates the entire surface of the control and causes the control to be redrawn.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000983 RID: 2435 RVA: 0x00027344 File Offset: 0x00025544
		public void Invalidate()
		{
			this.Invalidate(this.ClientRectangle, false);
		}

		/// <summary>Invalidates a specific region of the control and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000984 RID: 2436 RVA: 0x00027354 File Offset: 0x00025554
		public void Invalidate(bool invalidateChildren)
		{
			this.Invalidate(this.ClientRectangle, invalidateChildren);
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control.</summary>
		/// <param name="rc">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000985 RID: 2437 RVA: 0x00027364 File Offset: 0x00025564
		public void Invalidate(Rectangle rc)
		{
			this.Invalidate(rc, false);
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="rc">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
		/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000986 RID: 2438 RVA: 0x00027370 File Offset: 0x00025570
		public void Invalidate(Rectangle rc, bool invalidateChildren)
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			if (rc == Rectangle.Empty)
			{
				rc = this.ClientRectangle;
			}
			if (rc.Width > 0 && rc.Height > 0)
			{
				this.NotifyInvalidate(rc);
				XplatUI.Invalidate(this.Handle, rc, false);
				if (invalidateChildren)
				{
					Control[] allControls = this.child_controls.GetAllControls();
					for (int i = 0; i < allControls.Length; i++)
					{
						allControls[i].Invalidate();
					}
				}
				else
				{
					foreach (object obj in this.Controls)
					{
						Control control = (Control)obj;
						if (control.BackColor.A != 255)
						{
							control.Invalidate();
						}
					}
				}
			}
			this.OnInvalidated(new InvalidateEventArgs(rc));
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000987 RID: 2439 RVA: 0x0002748C File Offset: 0x0002568C
		public void Invalidate(Region region)
		{
			this.Invalidate(region, false);
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate. </param>
		/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000988 RID: 2440 RVA: 0x00027498 File Offset: 0x00025698
		public void Invalidate(Region region, bool invalidateChildren)
		{
			RectangleF rectangleF = region.GetBounds(this.CreateGraphics());
			this.Invalidate(new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), invalidateChildren);
		}

		/// <summary>Executes the specified delegate on the thread that owns the control's underlying window handle.</summary>
		/// <returns>The return value from the delegate being invoked, or null if the delegate has no return value.</returns>
		/// <param name="method">A delegate that contains a method to be called in the control's thread context. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000989 RID: 2441 RVA: 0x000274E0 File Offset: 0x000256E0
		public object Invoke(Delegate method)
		{
			object[] array = null;
			if (method is EventHandler)
			{
				array = new object[]
				{
					this,
					EventArgs.Empty
				};
			}
			return this.Invoke(method, array);
		}

		/// <summary>Executes the specified delegate, on the thread that owns the control's underlying window handle, with the specified list of arguments.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the return value from the delegate being invoked, or null if the delegate has no return value.</returns>
		/// <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the <paramref name="args" /> parameter. </param>
		/// <param name="args">An array of objects to pass as arguments to the specified method. This parameter can be null if the method takes no arguments. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600098A RID: 2442 RVA: 0x00027518 File Offset: 0x00025718
		public object Invoke(Delegate method, params object[] args)
		{
			Control control = this.FindControlToInvokeOn();
			if (!this.InvokeRequired)
			{
				return method.DynamicInvoke(args);
			}
			IAsyncResult asyncResult = this.BeginInvokeInternal(method, args, control);
			return this.EndInvoke(asyncResult);
		}

		/// <summary>Forces the control to apply layout logic to all its child controls.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600098B RID: 2443 RVA: 0x00027550 File Offset: 0x00025750
		[EditorBrowsable(2)]
		public void PerformLayout()
		{
			this.PerformLayout(null, null);
		}

		/// <summary>Forces the control to apply layout logic to all its child controls.</summary>
		/// <param name="affectedControl">A <see cref="T:System.Windows.Forms.Control" /> that represents the most recently changed control. </param>
		/// <param name="affectedProperty">The name of the most recently changed property on the control. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600098C RID: 2444 RVA: 0x0002755C File Offset: 0x0002575C
		[EditorBrowsable(2)]
		public void PerformLayout(Control affectedControl, string affectedProperty)
		{
			LayoutEventArgs layoutEventArgs = new LayoutEventArgs(affectedControl, affectedProperty);
			foreach (Control control in this.Controls.GetAllControls())
			{
				if (control.recalculate_distances)
				{
					control.UpdateDistances();
				}
			}
			if (this.layout_suspended > 0)
			{
				this.layout_pending = true;
				return;
			}
			this.layout_pending = false;
			this.layout_suspended++;
			try
			{
				this.OnLayout(layoutEventArgs);
			}
			finally
			{
				this.layout_suspended--;
			}
		}

		/// <summary>Computes the location of the specified screen point into client coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Point" />, <paramref name="p" />, in client coordinates.</returns>
		/// <param name="p">The screen coordinate <see cref="T:System.Drawing.Point" /> to convert. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600098D RID: 2445 RVA: 0x00027608 File Offset: 0x00025808
		public Point PointToClient(Point p)
		{
			int x = p.X;
			int y = p.Y;
			XplatUI.ScreenToClient(this.Handle, ref x, ref y);
			return new Point(x, y);
		}

		/// <summary>Computes the location of the specified client point into screen coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Point" />, <paramref name="p" />, in screen coordinates.</returns>
		/// <param name="p">The client coordinate <see cref="T:System.Drawing.Point" /> to convert. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600098E RID: 2446 RVA: 0x0002763C File Offset: 0x0002583C
		public Point PointToScreen(Point p)
		{
			int x = p.X;
			int y = p.Y;
			XplatUI.ClientToScreen(this.Handle, ref x, ref y);
			return new Point(x, y);
		}

		/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PreProcessControlState" /> values, depending on whether <see cref="M:System.Windows.Forms.Control.PreProcessMessage(System.Windows.Forms.Message@)" /> is true or false and whether <see cref="M:System.Windows.Forms.Control.IsInputKey(System.Windows.Forms.Keys)" /> or <see cref="M:System.Windows.Forms.Control.IsInputChar(System.Char)" /> are true or false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" /> that represents the message to process.</param>
		// Token: 0x0600098F RID: 2447 RVA: 0x00027670 File Offset: 0x00025870
		[EditorBrowsable(2)]
		public PreProcessControlState PreProcessControlMessage(ref Message msg)
		{
			return this.PreProcessControlMessageInternal(ref msg);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002767C File Offset: 0x0002587C
		internal PreProcessControlState PreProcessControlMessageInternal(ref Message msg)
		{
			switch (msg.Msg)
			{
			case 256:
			case 260:
			{
				PreviewKeyDownEventArgs previewKeyDownEventArgs = new PreviewKeyDownEventArgs((Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys));
				this.OnPreviewKeyDown(previewKeyDownEventArgs);
				if (previewKeyDownEventArgs.IsInputKey)
				{
					return PreProcessControlState.MessageNeeded;
				}
				if (this.PreProcessMessage(ref msg))
				{
					return PreProcessControlState.MessageProcessed;
				}
				if (this.IsInputKey((Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys)))
				{
					return PreProcessControlState.MessageNeeded;
				}
				break;
			}
			case 258:
			case 262:
				if (this.PreProcessMessage(ref msg))
				{
					return PreProcessControlState.MessageProcessed;
				}
				if (this.IsInputChar((char)(int)msg.WParam))
				{
					return PreProcessControlState.MessageNeeded;
				}
				break;
			}
			return PreProcessControlState.MessageNotNeeded;
		}

		/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000991 RID: 2449 RVA: 0x00027754 File Offset: 0x00025954
		public virtual bool PreProcessMessage(ref Message msg)
		{
			return this.InternalPreProcessMessage(ref msg);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00027760 File Offset: 0x00025960
		internal virtual bool InternalPreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256 || msg.Msg == 260)
			{
				Keys keys = (Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys);
				return this.ProcessCmdKey(ref msg, keys) || (!this.IsInputKey(keys) && this.ProcessDialogKey(keys));
			}
			if (msg.Msg == 258)
			{
				return !this.IsInputChar((char)(int)msg.WParam) && this.ProcessDialogChar((char)(int)msg.WParam);
			}
			return msg.Msg == 262 && (this.ProcessDialogChar((char)(int)msg.WParam) || ToolStripManager.ProcessMenuKey(ref msg));
		}

		/// <summary>Computes the size and location of the specified screen rectangle in client coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Rectangle" />, <paramref name="r" />, in client coordinates.</returns>
		/// <param name="r">The screen coordinate <see cref="T:System.Drawing.Rectangle" /> to convert. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000993 RID: 2451 RVA: 0x00027834 File Offset: 0x00025A34
		public Rectangle RectangleToClient(Rectangle r)
		{
			return new Rectangle(this.PointToClient(r.Location), r.Size);
		}

		/// <summary>Computes the size and location of the specified client rectangle in screen coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Rectangle" />, <paramref name="p" />, in screen coordinates.</returns>
		/// <param name="r">The client coordinate <see cref="T:System.Drawing.Rectangle" /> to convert. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000994 RID: 2452 RVA: 0x0002785C File Offset: 0x00025A5C
		public Rectangle RectangleToScreen(Rectangle r)
		{
			return new Rectangle(this.PointToScreen(r.Location), r.Size);
		}

		/// <summary>Forces the control to invalidate its client area and immediately redraw itself and any child controls.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000995 RID: 2453 RVA: 0x00027884 File Offset: 0x00025A84
		public virtual void Refresh()
		{
			if (this.IsHandleCreated && this.Visible)
			{
				this.Invalidate(true);
				this.Update();
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.BackColor" /> property to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000996 RID: 2454 RVA: 0x000278AC File Offset: 0x00025AAC
		[EditorBrowsable(1)]
		public virtual void ResetBackColor()
		{
			this.BackColor = Color.Empty;
		}

		/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread all the items in the list and refresh their displayed values.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000997 RID: 2455 RVA: 0x000278BC File Offset: 0x00025ABC
		[EditorBrowsable(1)]
		public void ResetBindings()
		{
			if (this.data_bindings != null)
			{
				this.data_bindings.Clear();
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Cursor" /> property to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000998 RID: 2456 RVA: 0x000278D4 File Offset: 0x00025AD4
		[EditorBrowsable(1)]
		public virtual void ResetCursor()
		{
			this.Cursor = null;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Font" /> property to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000999 RID: 2457 RVA: 0x000278E0 File Offset: 0x00025AE0
		[EditorBrowsable(1)]
		public virtual void ResetFont()
		{
			this.font = null;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600099A RID: 2458 RVA: 0x000278EC File Offset: 0x00025AEC
		[EditorBrowsable(1)]
		public virtual void ResetForeColor()
		{
			this.foreground_color = Color.Empty;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600099B RID: 2459 RVA: 0x000278FC File Offset: 0x00025AFC
		[EditorBrowsable(1)]
		public void ResetImeMode()
		{
			this.ime_mode = this.DefaultImeMode;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600099C RID: 2460 RVA: 0x0002790C File Offset: 0x00025B0C
		[EditorBrowsable(1)]
		public virtual void ResetRightToLeft()
		{
			this.right_to_left = RightToLeft.Inherit;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Text" /> property to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600099D RID: 2461 RVA: 0x00027918 File Offset: 0x00025B18
		public virtual void ResetText()
		{
			this.Text = string.Empty;
		}

		/// <summary>Resumes usual layout logic.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600099E RID: 2462 RVA: 0x00027928 File Offset: 0x00025B28
		public void ResumeLayout()
		{
			this.ResumeLayout(true);
		}

		/// <summary>Resumes usual layout logic, optionally forcing an immediate layout of pending layout requests.</summary>
		/// <param name="performLayout">true to execute pending layout requests; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600099F RID: 2463 RVA: 0x00027934 File Offset: 0x00025B34
		public void ResumeLayout(bool performLayout)
		{
			if (this.layout_suspended > 0)
			{
				this.layout_suspended--;
			}
			if (this.layout_suspended == 0)
			{
				if (this is ContainerControl)
				{
					(this as ContainerControl).PerformDelayedAutoScale();
				}
				if (!performLayout)
				{
					foreach (Control control in this.Controls.GetAllControls())
					{
						control.UpdateDistances();
					}
				}
				if (performLayout && this.layout_pending)
				{
					this.PerformLayout();
				}
			}
		}

		/// <summary>Scales the control and any child controls.</summary>
		/// <param name="ratio">The ratio to use for scaling.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009A0 RID: 2464 RVA: 0x000279C4 File Offset: 0x00025BC4
		[EditorBrowsable(1)]
		[Obsolete]
		public void Scale(float ratio)
		{
			this.ScaleCore(ratio, ratio);
		}

		/// <summary>Scales the entire control and any child controls.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009A1 RID: 2465 RVA: 0x000279D0 File Offset: 0x00025BD0
		[Obsolete]
		[EditorBrowsable(1)]
		public void Scale(float dx, float dy)
		{
			this.ScaleCore(dx, dy);
		}

		/// <summary>Scales the control and all child controls by the specified scaling factor.</summary>
		/// <param name="factor">A <see cref="T:System.Drawing.SizeF" /> containing the horizontal and vertical scaling factors.</param>
		// Token: 0x060009A2 RID: 2466 RVA: 0x000279DC File Offset: 0x00025BDC
		[EditorBrowsable(2)]
		public void Scale(SizeF factor)
		{
			BoundsSpecified boundsSpecified = BoundsSpecified.All;
			this.SuspendLayout();
			if (this is ContainerControl)
			{
				if ((this as ContainerControl).IsAutoScaling)
				{
					boundsSpecified = BoundsSpecified.Size;
				}
				else if (this.IsContainerAutoScaling(this.Parent))
				{
					boundsSpecified = BoundsSpecified.Location;
				}
			}
			this.ScaleControl(factor, boundsSpecified);
			if (boundsSpecified != BoundsSpecified.Location && this.ScaleChildren)
			{
				foreach (Control control in this.Controls.GetAllControls())
				{
					control.Scale(factor);
					if (control is ContainerControl)
					{
						ContainerControl containerControl = control as ContainerControl;
						if (containerControl.AutoScaleMode == AutoScaleMode.Inherit && this.IsContainerAutoScaling(this))
						{
							containerControl.PerformAutoScale(true);
						}
					}
				}
			}
			this.ResumeLayout();
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00027AA8 File Offset: 0x00025CA8
		internal ContainerControl FindContainer(Control c)
		{
			while (c != null && !(c is ContainerControl))
			{
				c = c.Parent;
			}
			return c as ContainerControl;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00027ADC File Offset: 0x00025CDC
		private bool IsContainerAutoScaling(Control c)
		{
			ContainerControl containerControl = this.FindContainer(c);
			return containerControl != null && containerControl.IsAutoScaling;
		}

		/// <summary>Activates the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009A5 RID: 2469 RVA: 0x00027B00 File Offset: 0x00025D00
		public void Select()
		{
			this.Select(false, false);
		}

		/// <summary>Activates the next control.</summary>
		/// <returns>true if a control was activated; otherwise, false.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> at which to start the search. </param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
		/// <param name="tabStopOnly">true to ignore the controls with the <see cref="P:System.Windows.Forms.Control.TabStop" /> property set to false; otherwise, false. </param>
		/// <param name="nested">true to include nested (children of child controls) child controls; otherwise, false. </param>
		/// <param name="wrap">true to continue searching from the first control in the tab order after the last control has been reached; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009A6 RID: 2470 RVA: 0x00027B0C File Offset: 0x00025D0C
		public bool SelectNextControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			if (!this.Contains(ctl) || (!nested && ctl.parent != this))
			{
				ctl = null;
			}
			Control control = ctl;
			do
			{
				control = this.GetNextControl(control, forward);
				if (control == null)
				{
					if (!wrap)
					{
						break;
					}
					wrap = false;
				}
				else if (control.CanSelect && (control.parent == this || nested) && (control.tab_stop || !tabStopOnly))
				{
					goto IL_0076;
				}
			}
			while (control != ctl);
			return false;
			IL_0076:
			control.Select(true, true);
			return true;
		}

		/// <summary>Sends the control to the back of the z-order.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009A7 RID: 2471 RVA: 0x00027BA4 File Offset: 0x00025DA4
		public void SendToBack()
		{
			if (this.parent != null)
			{
				this.parent.child_controls.SetChildIndex(this, this.parent.child_controls.Count);
			}
		}

		/// <summary>Sets the bounds of the control to the specified location and size.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009A8 RID: 2472 RVA: 0x00027BE0 File Offset: 0x00025DE0
		public void SetBounds(int x, int y, int width, int height)
		{
			this.SetBounds(x, y, width, height, BoundsSpecified.All);
		}

		/// <summary>Sets the specified bounds of the control to the specified location and size.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. For any parameter not specified, the current value will be used. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009A9 RID: 2473 RVA: 0x00027BF0 File Offset: 0x00025DF0
		public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.X) == BoundsSpecified.None)
			{
				x = this.Left;
			}
			if ((specified & BoundsSpecified.Y) == BoundsSpecified.None)
			{
				y = this.Top;
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.None)
			{
				width = this.Width;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.None)
			{
				height = this.Height;
			}
			this.SetBoundsInternal(x, y, width, height, specified);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00027C50 File Offset: 0x00025E50
		internal void SetBoundsInternal(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.bounds.X != x || (this.explicit_bounds.X != x && (specified & BoundsSpecified.X) == BoundsSpecified.X))
			{
				this.SetBoundsCore(x, y, width, height, specified);
			}
			else if (this.bounds.Y != y || (this.explicit_bounds.Y != y && (specified & BoundsSpecified.Y) == BoundsSpecified.Y))
			{
				this.SetBoundsCore(x, y, width, height, specified);
			}
			else if (this.bounds.Width != width || (this.explicit_bounds.Width != width && (specified & BoundsSpecified.Width) == BoundsSpecified.Width))
			{
				this.SetBoundsCore(x, y, width, height, specified);
			}
			else
			{
				if (this.bounds.Height == height && (this.explicit_bounds.Height == height || (specified & BoundsSpecified.Height) != BoundsSpecified.Height))
				{
					return;
				}
				this.SetBoundsCore(x, y, width, height, specified);
			}
			if (specified != BoundsSpecified.None)
			{
				this.UpdateDistances();
			}
			if (this.parent != null)
			{
				this.parent.PerformLayout(this, "Bounds");
			}
		}

		/// <summary>Displays the control to the user.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009AB RID: 2475 RVA: 0x00027D84 File Offset: 0x00025F84
		public void Show()
		{
			this.Visible = true;
		}

		/// <summary>Temporarily suspends the layout logic for the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060009AC RID: 2476 RVA: 0x00027D90 File Offset: 0x00025F90
		public void SuspendLayout()
		{
			this.layout_suspended++;
		}

		/// <summary>Causes the control to redraw the invalidated regions within its client area.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009AD RID: 2477 RVA: 0x00027DA0 File Offset: 0x00025FA0
		public void Update()
		{
			if (this.IsHandleCreated)
			{
				XplatUI.UpdateWindow(this.window.Handle);
			}
		}

		/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control.</summary>
		/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of. </param>
		/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event. </param>
		// Token: 0x060009AE RID: 2478 RVA: 0x00027DC0 File Offset: 0x00025FC0
		[EditorBrowsable(2)]
		protected void AccessibilityNotifyClients(AccessibleEvents accEvent, int childID)
		{
			if (this.accessibility_object != null && this.accessibility_object is Control.ControlAccessibleObject)
			{
				((Control.ControlAccessibleObject)this.accessibility_object).NotifyClients(accEvent, childID);
			}
		}

		/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control .</summary>
		/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
		/// <param name="objectID">The identifier of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
		/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event.</param>
		// Token: 0x060009AF RID: 2479 RVA: 0x00027DF0 File Offset: 0x00025FF0
		[EditorBrowsable(2)]
		protected void AccessibilityNotifyClients(AccessibleEvents accEvent, int objectID, int childID)
		{
			if (this.accessibility_object != null && this.accessibility_object is Control.ControlAccessibleObject)
			{
				((Control.ControlAccessibleObject)this.accessibility_object).NotifyClients(accEvent, objectID, childID);
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x060009B0 RID: 2480 RVA: 0x00027E2C File Offset: 0x0002602C
		[EditorBrowsable(2)]
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			this.CreateControl();
			return new Control.ControlAccessibleObject(this);
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x060009B1 RID: 2481 RVA: 0x00027E3C File Offset: 0x0002603C
		[EditorBrowsable(2)]
		protected virtual Control.ControlCollection CreateControlsInstance()
		{
			return new Control.ControlCollection(this);
		}

		/// <summary>Creates a handle for the control.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object is in a disposed state. </exception>
		// Token: 0x060009B2 RID: 2482 RVA: 0x00027E44 File Offset: 0x00026044
		[EditorBrowsable(2)]
		protected virtual void CreateHandle()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.IsHandleCreated && !this.is_recreating)
			{
				return;
			}
			CreateParams createParams = this.CreateParams;
			this.window.CreateHandle(createParams);
			if (this.window.Handle != IntPtr.Zero)
			{
				this.creator_thread = Thread.CurrentThread;
				XplatUI.EnableWindow(this.window.Handle, this.is_enabled);
				if (this.clip_region != null)
				{
					XplatUI.SetClipRegion(this.window.Handle, this.clip_region);
				}
				if (this.parent != null && this.parent.IsHandleCreated)
				{
					XplatUI.SetParent(this.window.Handle, this.parent.Handle);
				}
				this.UpdateStyles();
				XplatUI.SetAllowDrop(this.window.Handle, this.allow_drop);
				if ((this.CreateParams.Style & 1073741824) != 0)
				{
					XplatUI.SetBorderStyle(this.window.Handle, (FormBorderStyle)this.border_style);
				}
				Rectangle rectangle = this.explicit_bounds;
				this.UpdateBounds();
				this.explicit_bounds = rectangle;
			}
		}

		/// <summary>Sends the specified message to the default window procedure.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x060009B3 RID: 2483 RVA: 0x00027F88 File Offset: 0x00026188
		[EditorBrowsable(2)]
		protected virtual void DefWndProc(ref Message m)
		{
			this.window.DefWndProc(ref m);
		}

		/// <summary>Destroys the handle associated with the control.</summary>
		// Token: 0x060009B4 RID: 2484 RVA: 0x00027F98 File Offset: 0x00026198
		[EditorBrowsable(2)]
		protected virtual void DestroyHandle()
		{
			if (this.IsHandleCreated && this.window != null)
			{
				this.window.DestroyHandle();
			}
		}

		/// <summary>Retrieves the specified <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" />.</returns>
		/// <param name="objectId">An Int32 that identifies the <see cref="T:System.Windows.Forms.AccessibleObject" /> to retrieve.</param>
		// Token: 0x060009B5 RID: 2485 RVA: 0x00027FBC File Offset: 0x000261BC
		protected virtual AccessibleObject GetAccessibilityObjectById(int objectId)
		{
			return null;
		}

		/// <summary>Retrieves a value indicating how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. </returns>
		// Token: 0x060009B6 RID: 2486 RVA: 0x00027FC0 File Offset: 0x000261C0
		protected internal AutoSizeMode GetAutoSizeMode()
		{
			return this.auto_size_mode;
		}

		/// <summary>Retrieves the bounds within which the control is scaled.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x060009B7 RID: 2487 RVA: 0x00027FC8 File Offset: 0x000261C8
		[EditorBrowsable(2)]
		protected virtual Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if (!this.is_toplevel)
			{
				if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
				{
					bounds.X = (int)Math.Round((double)((float)bounds.X * factor.Width));
				}
				if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
				{
					bounds.Y = (int)Math.Round((double)((float)bounds.Y * factor.Height));
				}
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width && !this.GetStyle(ControlStyles.FixedWidth))
			{
				int num = ((!(this is ComboBox)) ? (this.bounds.Width - this.client_size.Width) : (ThemeEngine.Current.Border3DSize.Width * 2));
				bounds.Width = (int)Math.Round((double)((float)(bounds.Width - num) * factor.Width + (float)num));
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height && !this.GetStyle(ControlStyles.FixedHeight))
			{
				int num2 = ((!(this is ComboBox)) ? (this.bounds.Height - this.client_size.Height) : (ThemeEngine.Current.Border3DSize.Height * 2));
				bounds.Height = (int)Math.Round((double)((float)(bounds.Height - num2) * factor.Height + (float)num2));
			}
			return bounds;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002811C File Offset: 0x0002631C
		private Rectangle GetScaledBoundsOld(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			RectangleF rectangleF;
			rectangleF..ctor(bounds.Location, bounds.Size);
			if (!this.is_toplevel)
			{
				if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
				{
					rectangleF.X *= factor.Width;
				}
				if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
				{
					rectangleF.Y *= factor.Height;
				}
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width && !this.GetStyle(ControlStyles.FixedWidth))
			{
				int num = ((!(this is Form)) ? 0 : (this.bounds.Width - this.client_size.Width));
				rectangleF.Width = (rectangleF.Width - (float)num) * factor.Width + (float)num;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height && !this.GetStyle(ControlStyles.FixedHeight))
			{
				int num2 = ((!(this is Form)) ? 0 : (this.bounds.Height - this.client_size.Height));
				rectangleF.Height = (rectangleF.Height - (float)num2) * factor.Height + (float)num2;
			}
			bounds.X = (int)Math.Round((double)rectangleF.X);
			bounds.Y = (int)Math.Round((double)rectangleF.Y);
			bounds.Width = (int)Math.Round((double)rectangleF.Right) - bounds.X;
			bounds.Height = (int)Math.Round((double)rectangleF.Bottom) - bounds.Y;
			return bounds;
		}

		/// <summary>Retrieves the value of the specified control style bit for the control.</summary>
		/// <returns>true if the specified control style bit is set to true; otherwise, false.</returns>
		/// <param name="flag">The <see cref="T:System.Windows.Forms.ControlStyles" /> bit to return the value from. </param>
		// Token: 0x060009B9 RID: 2489 RVA: 0x000282AC File Offset: 0x000264AC
		protected internal bool GetStyle(ControlStyles flag)
		{
			return (this.control_style & flag) != (ControlStyles)0;
		}

		/// <summary>Determines if the control is a top-level control.</summary>
		/// <returns>true if the control is a top-level control; otherwise, false.</returns>
		// Token: 0x060009BA RID: 2490 RVA: 0x000282BC File Offset: 0x000264BC
		protected bool GetTopLevel()
		{
			return this.is_toplevel;
		}

		/// <summary>Called after the control has been added to another container.</summary>
		// Token: 0x060009BB RID: 2491 RVA: 0x000282C4 File Offset: 0x000264C4
		[EditorBrowsable(2)]
		protected virtual void InitLayout()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event for the specified control.</summary>
		/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the event to. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060009BC RID: 2492 RVA: 0x000282C8 File Offset: 0x000264C8
		[EditorBrowsable(2)]
		protected void InvokeGotFocus(Control toInvoke, EventArgs e)
		{
			toInvoke.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event for the specified control.</summary>
		/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the event to. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060009BD RID: 2493 RVA: 0x000282D4 File Offset: 0x000264D4
		[EditorBrowsable(2)]
		protected void InvokeLostFocus(Control toInvoke, EventArgs e)
		{
			toInvoke.OnLostFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event for the specified control.</summary>
		/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Click" /> event to. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060009BE RID: 2494 RVA: 0x000282E0 File Offset: 0x000264E0
		[EditorBrowsable(2)]
		protected void InvokeOnClick(Control toInvoke, EventArgs e)
		{
			toInvoke.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event for the specified control.</summary>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Paint" /> event to. </param>
		/// <param name="e">An <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060009BF RID: 2495 RVA: 0x000282EC File Offset: 0x000264EC
		protected void InvokePaint(Control c, PaintEventArgs e)
		{
			c.OnPaint(e);
		}

		/// <summary>Raises the PaintBackground event for the specified control.</summary>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Paint" /> event to. </param>
		/// <param name="e">An <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060009C0 RID: 2496 RVA: 0x000282F8 File Offset: 0x000264F8
		protected void InvokePaintBackground(Control c, PaintEventArgs e)
		{
			c.OnPaintBackground(e);
		}

		/// <summary>Determines if a character is an input character that the control recognizes.</summary>
		/// <returns>true if the character should be sent directly to the control and not preprocessed; otherwise, false.</returns>
		/// <param name="charCode">The character to test. </param>
		// Token: 0x060009C1 RID: 2497 RVA: 0x00028304 File Offset: 0x00026504
		protected virtual bool IsInputChar(char charCode)
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			return this.IsInputCharInternal(charCode);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00028320 File Offset: 0x00026520
		internal virtual bool IsInputCharInternal(char charCode)
		{
			return false;
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x060009C3 RID: 2499 RVA: 0x00028324 File Offset: 0x00026524
		protected virtual bool IsInputKey(Keys keyData)
		{
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event with a specified region of the control to invalidate.</summary>
		/// <param name="invalidatedArea">A <see cref="T:System.Drawing.Rectangle" /> representing the area to invalidate. </param>
		// Token: 0x060009C4 RID: 2500 RVA: 0x00028328 File Offset: 0x00026528
		[EditorBrowsable(2)]
		protected virtual void NotifyInvalidate(Rectangle invalidatedArea)
		{
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x060009C5 RID: 2501 RVA: 0x0002832C File Offset: 0x0002652C
		protected virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return (this.context_menu != null && this.context_menu.ProcessCmdKey(ref msg, keyData)) || (this.parent != null && this.parent.ProcessCmdKey(ref msg, keyData));
		}

		/// <summary>Processes a dialog character.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x060009C6 RID: 2502 RVA: 0x00028368 File Offset: 0x00026568
		protected virtual bool ProcessDialogChar(char charCode)
		{
			return this.parent != null && this.parent.ProcessDialogChar(charCode);
		}

		/// <summary>Processes a dialog key.</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x060009C7 RID: 2503 RVA: 0x00028384 File Offset: 0x00026584
		protected virtual bool ProcessDialogKey(Keys keyData)
		{
			return this.parent != null && this.parent.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a key message and generates the appropriate control events.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x060009C8 RID: 2504 RVA: 0x000283A0 File Offset: 0x000265A0
		protected virtual bool ProcessKeyEventArgs(ref Message m)
		{
			switch (m.Msg)
			{
			case 256:
			case 260:
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs((Keys)m.WParam.ToInt32());
				this.OnKeyDown(keyEventArgs);
				this.suppressing_key_press = keyEventArgs.SuppressKeyPress;
				return keyEventArgs.Handled;
			}
			case 257:
			case 261:
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs((Keys)m.WParam.ToInt32());
				this.OnKeyUp(keyEventArgs);
				return keyEventArgs.Handled;
			}
			case 258:
			case 262:
			{
				if (this.suppressing_key_press)
				{
					return true;
				}
				KeyPressEventArgs keyPressEventArgs = new KeyPressEventArgs((char)(int)m.WParam);
				this.OnKeyPress(keyPressEventArgs);
				m.WParam = (IntPtr)((int)keyPressEventArgs.KeyChar);
				return keyPressEventArgs.Handled;
			}
			}
			return false;
		}

		/// <summary>Processes a keyboard message.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x060009C9 RID: 2505 RVA: 0x00028478 File Offset: 0x00026678
		protected internal virtual bool ProcessKeyMessage(ref Message m)
		{
			return (this.parent != null && this.parent.ProcessKeyPreview(ref m)) || this.ProcessKeyEventArgs(ref m);
		}

		/// <summary>Previews a keyboard message.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x060009CA RID: 2506 RVA: 0x000284A0 File Offset: 0x000266A0
		protected virtual bool ProcessKeyPreview(ref Message m)
		{
			return this.parent != null && this.parent.ProcessKeyPreview(ref m);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x060009CB RID: 2507 RVA: 0x000284BC File Offset: 0x000266BC
		protected virtual bool ProcessMnemonic(char charCode)
		{
			return false;
		}

		/// <summary>Raises the appropriate drag event.</summary>
		/// <param name="key">The event to raise. </param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x060009CC RID: 2508 RVA: 0x000284C0 File Offset: 0x000266C0
		[EditorBrowsable(2)]
		protected void RaiseDragEvent(object key, DragEventArgs e)
		{
		}

		/// <summary>Raises the appropriate key event.</summary>
		/// <param name="key">The event to raise. </param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x060009CD RID: 2509 RVA: 0x000284C4 File Offset: 0x000266C4
		[EditorBrowsable(2)]
		protected void RaiseKeyEvent(object key, KeyEventArgs e)
		{
		}

		/// <summary>Raises the appropriate mouse event.</summary>
		/// <param name="key">The event to raise. </param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060009CE RID: 2510 RVA: 0x000284C8 File Offset: 0x000266C8
		[EditorBrowsable(2)]
		protected void RaiseMouseEvent(object key, MouseEventArgs e)
		{
		}

		/// <summary>Raises the appropriate paint event.</summary>
		/// <param name="key">The event to raise. </param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060009CF RID: 2511 RVA: 0x000284CC File Offset: 0x000266CC
		[EditorBrowsable(2)]
		protected void RaisePaintEvent(object key, PaintEventArgs e)
		{
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000284D0 File Offset: 0x000266D0
		private void SetIsRecreating()
		{
			this.is_recreating = true;
			foreach (Control control in this.Controls.GetAllControls())
			{
				control.SetIsRecreating();
			}
		}

		/// <summary>Forces the re-creation of the handle for the control.</summary>
		// Token: 0x060009D1 RID: 2513 RVA: 0x00028510 File Offset: 0x00026710
		[EditorBrowsable(2)]
		protected void RecreateHandle()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			this.SetIsRecreating();
			if (this.IsHandleCreated)
			{
				this.DestroyHandle();
			}
			else
			{
				if (!this.is_created)
				{
					this.CreateControl();
				}
				else
				{
					this.CreateHandle();
				}
				this.is_recreating = false;
			}
		}

		/// <summary>Resets the control to handle the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		// Token: 0x060009D2 RID: 2514 RVA: 0x00028568 File Offset: 0x00026768
		[EditorBrowsable(2)]
		protected void ResetMouseEventArgs()
		{
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.ContentAlignment" /> to the appropriate <see cref="T:System.Drawing.ContentAlignment" /> to support right-to-left text.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</returns>
		/// <param name="align">One of the <see cref="T:System.Drawing.ContentAlignment" /> values. </param>
		// Token: 0x060009D3 RID: 2515 RVA: 0x0002856C File Offset: 0x0002676C
		[EditorBrowsable(2)]
		protected ContentAlignment RtlTranslateAlignment(ContentAlignment align)
		{
			if (this.right_to_left == RightToLeft.No)
			{
				return align;
			}
			switch (align)
			{
			case 1:
				return 4;
			default:
				if (align == 16)
				{
					return 64;
				}
				if (align == 64)
				{
					return 16;
				}
				if (align == 256)
				{
					return 1024;
				}
				if (align != 1024)
				{
					return align;
				}
				return 256;
			case 4:
				return 1;
			}
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to support right-to-left text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
		/// <param name="align">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. </param>
		// Token: 0x060009D4 RID: 2516 RVA: 0x000285E4 File Offset: 0x000267E4
		[EditorBrowsable(2)]
		protected HorizontalAlignment RtlTranslateAlignment(HorizontalAlignment align)
		{
			if (this.right_to_left == RightToLeft.No || align == HorizontalAlignment.Center)
			{
				return align;
			}
			if (align == HorizontalAlignment.Left)
			{
				return HorizontalAlignment.Right;
			}
			return HorizontalAlignment.Left;
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to support right-to-left text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</returns>
		/// <param name="align">One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. </param>
		// Token: 0x060009D5 RID: 2517 RVA: 0x00028604 File Offset: 0x00026804
		[EditorBrowsable(2)]
		protected LeftRightAlignment RtlTranslateAlignment(LeftRightAlignment align)
		{
			if (this.right_to_left == RightToLeft.No)
			{
				return align;
			}
			if (align == LeftRightAlignment.Left)
			{
				return LeftRightAlignment.Right;
			}
			return LeftRightAlignment.Left;
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.ContentAlignment" /> to the appropriate <see cref="T:System.Drawing.ContentAlignment" /> to support right-to-left text.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</returns>
		/// <param name="align">One of the <see cref="T:System.Drawing.ContentAlignment" /> values. </param>
		// Token: 0x060009D6 RID: 2518 RVA: 0x0002861C File Offset: 0x0002681C
		[EditorBrowsable(2)]
		protected ContentAlignment RtlTranslateContent(ContentAlignment align)
		{
			return this.RtlTranslateAlignment(align);
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to support right-to-left text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
		/// <param name="align">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. </param>
		// Token: 0x060009D7 RID: 2519 RVA: 0x00028628 File Offset: 0x00026828
		[EditorBrowsable(2)]
		protected HorizontalAlignment RtlTranslateHorizontal(HorizontalAlignment align)
		{
			return this.RtlTranslateAlignment(align);
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to support right-to-left text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</returns>
		/// <param name="align">One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. </param>
		// Token: 0x060009D8 RID: 2520 RVA: 0x00028634 File Offset: 0x00026834
		[EditorBrowsable(2)]
		protected LeftRightAlignment RtlTranslateLeftRight(LeftRightAlignment align)
		{
			return this.RtlTranslateAlignment(align);
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x060009D9 RID: 2521 RVA: 0x00028640 File Offset: 0x00026840
		[EditorBrowsable(2)]
		protected virtual void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			Rectangle scaledBounds = this.GetScaledBounds(this.bounds, factor, specified);
			this.SetBounds(scaledBounds.X, scaledBounds.Y, scaledBounds.Width, scaledBounds.Height, specified);
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x060009DA RID: 2522 RVA: 0x00028680 File Offset: 0x00026880
		[EditorBrowsable(1)]
		protected virtual void ScaleCore(float dx, float dy)
		{
			Rectangle scaledBoundsOld = this.GetScaledBoundsOld(this.bounds, new SizeF(dx, dy), BoundsSpecified.All);
			this.SuspendLayout();
			this.SetBounds(scaledBoundsOld.X, scaledBoundsOld.Y, scaledBoundsOld.Width, scaledBoundsOld.Height, BoundsSpecified.All);
			if (this.ScaleChildrenInternal)
			{
				foreach (Control control in this.Controls.GetAllControls())
				{
					control.Scale(dx, dy);
				}
			}
			this.ResumeLayout();
		}

		/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
		/// <param name="directed">true to specify the direction of the control to select; otherwise, false. </param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
		// Token: 0x060009DB RID: 2523 RVA: 0x0002870C File Offset: 0x0002690C
		protected virtual void Select(bool directed, bool forward)
		{
			IContainerControl containerControl = this.GetContainerControl();
			if (containerControl != null && (Control)containerControl != this)
			{
				containerControl.ActiveControl = this;
			}
		}

		/// <summary>Sets a value indicating how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
		/// <param name="mode">One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</param>
		// Token: 0x060009DC RID: 2524 RVA: 0x0002873C File Offset: 0x0002693C
		protected void SetAutoSizeMode(AutoSizeMode mode)
		{
			if (this.auto_size_mode != mode)
			{
				this.auto_size_mode = mode;
				this.PerformLayout(this, "AutoSizeMode");
			}
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x060009DD RID: 2525 RVA: 0x00028760 File Offset: 0x00026960
		[EditorBrowsable(2)]
		protected virtual void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			this.SetBoundsCoreInternal(x, y, width, height, specified);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00028770 File Offset: 0x00026970
		internal virtual void SetBoundsCoreInternal(int x, int y, int width, int height, BoundsSpecified specified)
		{
			height = this.OverrideHeight(height);
			Rectangle rectangle = this.explicit_bounds;
			Rectangle rectangle2;
			rectangle2..ctor(x, y, width, height);
			if (this.IsHandleCreated)
			{
				XplatUI.SetWindowPos(this.Handle, x, y, width, height);
				int num;
				int num2;
				int num3;
				int num4;
				XplatUI.GetWindowPos(this.Handle, this is Form, out num, out num2, out width, out height, out num3, out num4);
			}
			if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
			{
				this.explicit_bounds.X = rectangle2.X;
			}
			else
			{
				this.explicit_bounds.X = rectangle.X;
			}
			if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
			{
				this.explicit_bounds.Y = rectangle2.Y;
			}
			else
			{
				this.explicit_bounds.Y = rectangle.Y;
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
			{
				this.explicit_bounds.Width = rectangle2.Width;
			}
			else
			{
				this.explicit_bounds.Width = rectangle.Width;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				this.explicit_bounds.Height = rectangle2.Height;
			}
			else
			{
				this.explicit_bounds.Height = rectangle.Height;
			}
			Rectangle rectangle3 = this.explicit_bounds;
			this.UpdateBounds(x, y, width, height);
			if (this.explicit_bounds.X == x)
			{
				this.explicit_bounds.X = rectangle3.X;
			}
			if (this.explicit_bounds.Y == y)
			{
				this.explicit_bounds.Y = rectangle3.Y;
			}
			if (this.explicit_bounds.Width == width)
			{
				this.explicit_bounds.Width = rectangle3.Width;
			}
			if (this.explicit_bounds.Height == height)
			{
				this.explicit_bounds.Height = rectangle3.Height;
			}
		}

		/// <summary>Sets the size of the client area of the control.</summary>
		/// <param name="x">The client area width, in pixels. </param>
		/// <param name="y">The client area height, in pixels. </param>
		// Token: 0x060009DF RID: 2527 RVA: 0x00028944 File Offset: 0x00026B44
		[EditorBrowsable(2)]
		protected virtual void SetClientSizeCore(int x, int y)
		{
			Size size = this.InternalSizeFromClientSize(new Size(x, y));
			if (size != Size.Empty)
			{
				this.SetBounds(this.bounds.X, this.bounds.Y, size.Width, size.Height, BoundsSpecified.Size);
			}
		}

		/// <summary>Sets a specified <see cref="T:System.Windows.Forms.ControlStyles" /> flag to either true or false.</summary>
		/// <param name="flag">The <see cref="T:System.Windows.Forms.ControlStyles" /> bit to set. </param>
		/// <param name="value">true to apply the specified style to the control; otherwise, false. </param>
		// Token: 0x060009E0 RID: 2528 RVA: 0x0002899C File Offset: 0x00026B9C
		[EditorBrowsable(2)]
		protected internal void SetStyle(ControlStyles flag, bool value)
		{
			if (value)
			{
				this.control_style |= flag;
			}
			else
			{
				this.control_style &= ~flag;
			}
		}

		/// <summary>Sets the control as the top-level control.</summary>
		/// <param name="value">true to set the control as the top-level control; otherwise, false. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="value" /> parameter is set to true and the control is an ActiveX control. </exception>
		/// <exception cref="T:System.Exception">The <see cref="M:System.Windows.Forms.Control.GetTopLevel" /> return value is not equal to the <paramref name="value" /> parameter and the <see cref="P:System.Windows.Forms.Control.Parent" /> property is not null. </exception>
		// Token: 0x060009E1 RID: 2529 RVA: 0x000289D4 File Offset: 0x00026BD4
		protected void SetTopLevel(bool value)
		{
			if (this.GetTopLevel() != value && this.parent != null)
			{
				throw new ArgumentException("Cannot change toplevel style of a parented control.");
			}
			if (this is Form)
			{
				if (this.IsHandleCreated && value != this.Visible)
				{
					this.Visible = value;
				}
			}
			else if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			this.is_toplevel = value;
		}

		/// <summary>Sets the control to the specified visible state.</summary>
		/// <param name="value">true to make the control visible; otherwise, false. </param>
		// Token: 0x060009E2 RID: 2530 RVA: 0x00028A4C File Offset: 0x00026C4C
		protected virtual void SetVisibleCore(bool value)
		{
			if (value != this.is_visible)
			{
				this.is_visible = value;
				if (this.is_visible && (this.window.Handle == IntPtr.Zero || !this.is_created))
				{
					this.CreateControl();
					if (!(this is Form))
					{
						this.UpdateZOrder();
					}
				}
				if (this.IsHandleCreated)
				{
					XplatUI.SetVisible(this.Handle, this.is_visible, true);
					if (!this.is_visible)
					{
						if (this.parent != null && this.parent.IsHandleCreated)
						{
							this.parent.Invalidate(this.bounds);
							this.parent.Update();
						}
						else
						{
							this.Refresh();
						}
					}
					else if (this.is_visible && this is Form)
					{
						if ((this as Form).WindowState != FormWindowState.Normal)
						{
							this.OnVisibleChanged(EventArgs.Empty);
						}
						else
						{
							XplatUI.SetWindowPos(this.window.Handle, this.bounds.X, this.bounds.Y, this.bounds.Width, this.bounds.Height);
						}
					}
					else if (this.parent != null)
					{
						this.parent.UpdateZOrderOfChild(this);
					}
					if (!(this is Form))
					{
						this.OnVisibleChanged(EventArgs.Empty);
					}
				}
				else
				{
					this.OnVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Determines the size of the entire control from the height and width of its client area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value representing the height and width of the entire control.</returns>
		/// <param name="clientSize">A <see cref="T:System.Drawing.Size" /> value representing the height and width of the control's client area.</param>
		// Token: 0x060009E3 RID: 2531 RVA: 0x00028BD8 File Offset: 0x00026DD8
		[EditorBrowsable(2)]
		protected virtual Size SizeFromClientSize(Size clientSize)
		{
			return this.InternalSizeFromClientSize(clientSize);
		}

		/// <summary>Updates the bounds of the control with the current size and location.</summary>
		// Token: 0x060009E4 RID: 2532 RVA: 0x00028BE4 File Offset: 0x00026DE4
		[EditorBrowsable(2)]
		protected void UpdateBounds()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			XplatUI.GetWindowPos(this.Handle, this is Form, out num, out num2, out num3, out num4, out num5, out num6);
			this.UpdateBounds(num, num2, num3, num4, num5, num6);
		}

		/// <summary>Updates the bounds of the control with the specified size and location.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the control. </param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the control. </param>
		/// <param name="width">The <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
		/// <param name="height">The <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
		// Token: 0x060009E5 RID: 2533 RVA: 0x00028C2C File Offset: 0x00026E2C
		[EditorBrowsable(2)]
		protected void UpdateBounds(int x, int y, int width, int height)
		{
			Rectangle rectangle;
			rectangle..ctor(0, 0, 0, 0);
			CreateParams createParams = this.CreateParams;
			XplatUI.CalculateWindowRect(ref rectangle, createParams, createParams.menu, out rectangle);
			this.UpdateBounds(x, y, width, height, width - (rectangle.Right - rectangle.Left), height - (rectangle.Bottom - rectangle.Top));
		}

		/// <summary>Updates the bounds of the control with the specified size, location, and client size.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the control. </param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the control. </param>
		/// <param name="width">The <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
		/// <param name="height">The <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
		/// <param name="clientWidth">The client <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
		/// <param name="clientHeight">The client <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
		// Token: 0x060009E6 RID: 2534 RVA: 0x00028C8C File Offset: 0x00026E8C
		[EditorBrowsable(2)]
		protected void UpdateBounds(int x, int y, int width, int height, int clientWidth, int clientHeight)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.bounds.X != x || this.bounds.Y != y)
			{
				flag = true;
			}
			if (this.Bounds.Width != width || this.Bounds.Height != height)
			{
				flag2 = true;
			}
			this.bounds.X = x;
			this.bounds.Y = y;
			this.bounds.Width = width;
			this.bounds.Height = height;
			this.explicit_bounds = this.bounds;
			this.client_size.Width = clientWidth;
			this.client_size.Height = clientHeight;
			if (flag)
			{
				this.OnLocationChanged(EventArgs.Empty);
				if (!this.background_color.IsEmpty && this.background_color.A < 255)
				{
					this.Invalidate();
				}
			}
			if (flag2)
			{
				this.OnSizeInitializedOrChanged();
				this.OnSizeChanged(EventArgs.Empty);
				this.OnClientSizeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Forces the assigned styles to be reapplied to the control.</summary>
		// Token: 0x060009E7 RID: 2535 RVA: 0x00028DA4 File Offset: 0x00026FA4
		[EditorBrowsable(2)]
		protected void UpdateStyles()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			XplatUI.SetWindowStyle(this.window.Handle, this.CreateParams);
			this.OnStyleChanged(EventArgs.Empty);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00028DE0 File Offset: 0x00026FE0
		private void UpdateZOrderOfChild(Control child)
		{
			if (this.IsHandleCreated && child.IsHandleCreated && child.parent == this && Hwnd.ObjectFromHandle(child.Handle).Mapped)
			{
				Control[] allControls = this.child_controls.GetAllControls();
				int i;
				for (i = Array.IndexOf<Control>(allControls, child); i > 0; i--)
				{
					if (allControls[i - 1].IsHandleCreated && allControls[i - 1].VisibleInternal && Hwnd.ObjectFromHandle(allControls[i - 1].Handle).Mapped)
					{
						break;
					}
				}
				if (i > 0)
				{
					XplatUI.SetZOrder(child.Handle, allControls[i - 1].Handle, false, false);
				}
				else
				{
					IntPtr intPtr = this.AfterTopMostControl();
					if (intPtr != IntPtr.Zero && intPtr != child.Handle)
					{
						XplatUI.SetZOrder(child.Handle, intPtr, false, false);
					}
					else
					{
						XplatUI.SetZOrder(child.Handle, IntPtr.Zero, true, false);
					}
				}
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00028EFC File Offset: 0x000270FC
		internal virtual IntPtr AfterTopMostControl()
		{
			return IntPtr.Zero;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00028F04 File Offset: 0x00027104
		internal void UpdateChildrenZOrder()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			Control[] array;
			if (this.child_controls.ImplicitControls == null)
			{
				array = new Control[this.child_controls.Count];
				this.child_controls.CopyTo(array, 0);
			}
			else
			{
				array = new Control[this.child_controls.Count + this.child_controls.ImplicitControls.Count];
				this.child_controls.CopyTo(array, 0);
				this.child_controls.ImplicitControls.CopyTo(array, this.child_controls.Count);
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IsHandleCreated && array[i].VisibleInternal)
				{
					Hwnd hwnd = Hwnd.ObjectFromHandle(array[i].Handle);
					if (!hwnd.zero_sized)
					{
						arrayList.Add(array[i]);
					}
				}
			}
			for (int j = 1; j < arrayList.Count; j++)
			{
				Control control = (Control)arrayList[j - 1];
				Control control2 = (Control)arrayList[j];
				XplatUI.SetZOrder(control2.Handle, control.Handle, false, false);
			}
		}

		/// <summary>Updates the control in its parent's z-order.</summary>
		// Token: 0x060009EB RID: 2539 RVA: 0x00029050 File Offset: 0x00027250
		[EditorBrowsable(2)]
		protected void UpdateZOrder()
		{
			if (this.parent != null)
			{
				this.parent.UpdateZOrderOfChild(this);
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x060009EC RID: 2540 RVA: 0x0002906C File Offset: 0x0002726C
		protected virtual void WndProc(ref Message m)
		{
			if ((this.control_style & ControlStyles.EnableNotifyMessage) != (ControlStyles)0)
			{
				this.OnNotifyMessage(m);
			}
			Msg msg = (Msg)m.Msg;
			switch (msg)
			{
			case Msg.WM_MOUSEMOVE:
				this.WmMouseMove(ref m);
				return;
			case Msg.WM_LBUTTONDOWN:
				this.WmLButtonDown(ref m);
				return;
			case Msg.WM_LBUTTONUP:
				this.WmLButtonUp(ref m);
				return;
			case Msg.WM_LBUTTONDBLCLK:
				this.WmLButtonDblClick(ref m);
				return;
			case Msg.WM_RBUTTONDOWN:
				this.WmRButtonDown(ref m);
				return;
			case Msg.WM_RBUTTONUP:
				this.WmRButtonUp(ref m);
				return;
			case Msg.WM_RBUTTONDBLCLK:
				this.WmRButtonDblClick(ref m);
				return;
			case Msg.WM_MBUTTONDOWN:
				this.WmMButtonDown(ref m);
				return;
			case Msg.WM_MBUTTONUP:
				this.WmMButtonUp(ref m);
				return;
			case Msg.WM_MBUTTONDBLCLK:
				this.WmMButtonDblClick(ref m);
				return;
			case Msg.WM_MOUSEWHEEL:
				this.WmMouseWheel(ref m);
				return;
			default:
				switch (msg)
				{
				case Msg.WM_CREATE:
					this.WmCreate(ref m);
					return;
				case Msg.WM_DESTROY:
					this.WmDestroy(ref m);
					return;
				default:
					switch (msg)
					{
					case Msg.WM_KEYDOWN:
					case Msg.WM_KEYUP:
					case Msg.WM_CHAR:
					case Msg.WM_SYSKEYDOWN:
					case Msg.WM_SYSCHAR:
						this.WmKeys(ref m);
						return;
					default:
						switch (msg)
						{
						case Msg.WM_ERASEBKGND:
							this.WmEraseBackground(ref m);
							return;
						case Msg.WM_SYSCOLORCHANGE:
							this.WmSysColorChange(ref m);
							return;
						default:
							switch (msg)
							{
							case Msg.WM_MOUSEHOVER:
								this.WmMouseHover(ref m);
								return;
							default:
								if (msg == Msg.WM_CHANGEUISTATE)
								{
									this.WmChangeUIState(ref m);
									return;
								}
								if (msg == Msg.WM_UPDATEUISTATE)
								{
									this.WmUpdateUIState(ref m);
									return;
								}
								if (msg == Msg.WM_PAINT)
								{
									this.WmPaint(ref m);
									return;
								}
								if (msg == Msg.WM_SETCURSOR)
								{
									this.WmSetCursor(ref m);
									return;
								}
								if (msg == Msg.WM_WINDOWPOSCHANGED)
								{
									this.WmWindowPosChanged(ref m);
									return;
								}
								if (msg == Msg.WM_HELP)
								{
									this.WmHelp(ref m);
									return;
								}
								if (msg == Msg.WM_CONTEXTMENU)
								{
									this.WmContextMenu(ref m);
									return;
								}
								if (msg != Msg.WM_MOUSE_ENTER)
								{
									this.DefWndProc(ref m);
									return;
								}
								this.WmMouseEnter(ref m);
								return;
							case Msg.WM_MOUSELEAVE:
								this.WmMouseLeave(ref m);
								return;
							}
							break;
						case Msg.WM_SHOWWINDOW:
							this.WmShowWindow(ref m);
							return;
						}
						break;
					case Msg.WM_SYSKEYUP:
						this.WmSysKeyUp(ref m);
						return;
					}
					break;
				case Msg.WM_SETFOCUS:
					this.WmSetFocus(ref m);
					return;
				case Msg.WM_KILLFOCUS:
					this.WmKillFocus(ref m);
					return;
				}
				break;
			case Msg.WM_CAPTURECHANGED:
				this.WmCaptureChanged(ref m);
				return;
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000292D4 File Offset: 0x000274D4
		private void WmDestroy(ref Message m)
		{
			this.OnHandleDestroyed(EventArgs.Empty);
			this.window.InvalidateHandle();
			this.is_created = false;
			if (this.is_recreating)
			{
				this.CreateHandle();
				this.is_recreating = false;
			}
			if (this.is_disposing)
			{
				this.is_disposing = false;
				this.is_visible = false;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00029330 File Offset: 0x00027530
		private void WmWindowPosChanged(ref Message m)
		{
			if (this.Visible)
			{
				Rectangle rectangle = this.explicit_bounds;
				this.UpdateBounds();
				this.explicit_bounds = rectangle;
				if (this.GetStyle(ControlStyles.ResizeRedraw))
				{
					this.Invalidate();
				}
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00029370 File Offset: 0x00027570
		private void WmPaint(ref Message m)
		{
			IntPtr handle = this.Handle;
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, handle, true);
			if (paintEventArgs == null)
			{
				return;
			}
			Control.DoubleBuffer doubleBuffer = null;
			if (this.UseDoubleBuffering)
			{
				doubleBuffer = this.GetBackBuffer();
				doubleBuffer.Start(paintEventArgs);
			}
			if (this.GetStyle(ControlStyles.OptimizedDoubleBuffer))
			{
				paintEventArgs.Graphics.SetClip(Rectangle.Intersect(paintEventArgs.ClipRectangle, this.ClientRectangle));
			}
			if (!this.GetStyle(ControlStyles.Opaque))
			{
				this.OnPaintBackground(paintEventArgs);
			}
			this.OnPaintBackgroundInternal(paintEventArgs);
			this.OnPaintInternal(paintEventArgs);
			if (!paintEventArgs.Handled)
			{
				this.OnPaint(paintEventArgs);
			}
			if (doubleBuffer != null)
			{
				doubleBuffer.End(paintEventArgs);
			}
			XplatUI.PaintEventEnd(ref m, handle, true);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00029424 File Offset: 0x00027624
		private void WmEraseBackground(ref Message m)
		{
			m.Result = (IntPtr)1;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00029434 File Offset: 0x00027634
		private void WmLButtonUp(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				this.ProcessActiveTracker(ref m);
				return;
			}
			MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()) | MouseButtons.Left, this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0);
			this.HandleClick(this.mouse_clicks, mouseEventArgs);
			this.OnMouseUp(mouseEventArgs);
			if (this.InternalCapture)
			{
				this.InternalCapture = false;
			}
			if (this.mouse_clicks > 1)
			{
				this.mouse_clicks = 1;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x000294EC File Offset: 0x000276EC
		private void WmLButtonDown(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				this.ProcessActiveTracker(ref m);
				return;
			}
			this.ValidationFailed = false;
			if (this.CanSelect)
			{
				this.Select(true, true);
			}
			if (!this.ValidationFailed)
			{
				this.InternalCapture = true;
				this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00029598 File Offset: 0x00027798
		private void WmLButtonDblClick(ref Message m)
		{
			this.InternalCapture = true;
			this.mouse_clicks++;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00029608 File Offset: 0x00027808
		private void WmMButtonUp(ref Message m)
		{
			MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()) | MouseButtons.Middle, this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0);
			this.HandleClick(this.mouse_clicks, mouseEventArgs);
			this.OnMouseUp(mouseEventArgs);
			if (this.InternalCapture)
			{
				this.InternalCapture = false;
			}
			if (this.mouse_clicks > 1)
			{
				this.mouse_clicks = 1;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002969C File Offset: 0x0002789C
		private void WmMButtonDown(ref Message m)
		{
			this.InternalCapture = true;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00029700 File Offset: 0x00027900
		private void WmMButtonDblClick(ref Message m)
		{
			this.InternalCapture = true;
			this.mouse_clicks++;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00029770 File Offset: 0x00027970
		private void WmRButtonUp(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				this.ProcessActiveTracker(ref m);
				return;
			}
			Point point;
			point..ctor(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			point = this.PointToScreen(point);
			MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()) | MouseButtons.Right, this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0);
			this.HandleClick(this.mouse_clicks, mouseEventArgs);
			XplatUI.SendMessage(m.HWnd, Msg.WM_CONTEXTMENU, m.HWnd, (IntPtr)(point.X + (point.Y << 16)));
			this.OnMouseUp(mouseEventArgs);
			if (this.InternalCapture)
			{
				this.InternalCapture = false;
			}
			if (this.mouse_clicks > 1)
			{
				this.mouse_clicks = 1;
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002988C File Offset: 0x00027A8C
		private void WmRButtonDown(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				this.ProcessActiveTracker(ref m);
				return;
			}
			this.InternalCapture = true;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00029910 File Offset: 0x00027B10
		private void WmRButtonDblClick(ref Message m)
		{
			this.InternalCapture = true;
			this.mouse_clicks++;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00029980 File Offset: 0x00027B80
		private void WmContextMenu(ref Message m)
		{
			if (this.context_menu != null)
			{
				Point point;
				point..ctor(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
				if (point.X == -1 || point.Y == -1)
				{
					point.X = this.Width / 2 + this.Left;
					point.Y = this.Height / 2 + this.Top;
					point = this.PointToScreen(point);
				}
				this.context_menu.Show(this, this.PointToClient(point));
				return;
			}
			if (this.context_menu == null && this.context_menu_strip != null)
			{
				Point point2;
				point2..ctor(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
				if (point2.X == -1 || point2.Y == -1)
				{
					point2.X = this.Width / 2 + this.Left;
					point2.Y = this.Height / 2 + this.Top;
					point2 = this.PointToScreen(point2);
				}
				this.context_menu_strip.SetSourceControl(this);
				this.context_menu_strip.Show(this, this.PointToClient(point2));
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00029AE4 File Offset: 0x00027CE4
		private void WmCreate(ref Message m)
		{
			this.OnHandleCreated(EventArgs.Empty);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00029AF4 File Offset: 0x00027CF4
		private void WmMouseWheel(ref Message m)
		{
			this.DefWndProc(ref m);
			this.OnMouseWheel(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), Control.HighOrder((long)m.WParam)));
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00029B60 File Offset: 0x00027D60
		private void WmMouseMove(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.MousePosition.X, Control.MousePosition.Y, 0);
				this.active_tracker.OnMotion(mouseEventArgs);
				return;
			}
			this.OnMouseMove(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00029C20 File Offset: 0x00027E20
		private void WmMouseEnter(ref Message m)
		{
			if (this.is_entered)
			{
				return;
			}
			this.is_entered = true;
			this.OnMouseEnter(EventArgs.Empty);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00029C40 File Offset: 0x00027E40
		private void WmMouseLeave(ref Message m)
		{
			this.is_entered = false;
			this.OnMouseLeave(EventArgs.Empty);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00029C54 File Offset: 0x00027E54
		private void WmMouseHover(ref Message m)
		{
			this.OnMouseHover(EventArgs.Empty);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00029C64 File Offset: 0x00027E64
		private void WmShowWindow(ref Message m)
		{
			if (this.IsDisposed)
			{
				return;
			}
			Form form = this as Form;
			if (m.WParam.ToInt32() != 0)
			{
				if (m.LParam.ToInt32() == 0)
				{
					this.CreateControl();
					Control[] allControls = this.child_controls.GetAllControls();
					for (int i = 0; i < allControls.Length; i++)
					{
						if (allControls[i].is_visible && allControls[i].IsHandleCreated && XplatUI.GetParent(allControls[i].Handle) != this.window.Handle)
						{
							XplatUI.SetParent(allControls[i].Handle, this.window.Handle);
						}
					}
					this.UpdateChildrenZOrder();
				}
			}
			else if (this.parent != null && this.Focused)
			{
				Control control = (Control)this.parent.GetContainerControl();
				if (control != null && (form == null || !form.IsMdiChild))
				{
					control.SelectNextControl(this, true, true, true, true);
				}
			}
			if (form != null)
			{
				form.waiting_showwindow = false;
			}
			if (form != null)
			{
				if (!this.IsRecreating && (form.IsMdiChild || form.WindowState == FormWindowState.Normal))
				{
					this.OnVisibleChanged(EventArgs.Empty);
				}
			}
			else if (this.is_toplevel)
			{
				this.OnVisibleChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00029DD8 File Offset: 0x00027FD8
		private void WmSysKeyUp(ref Message m)
		{
			if (this.ProcessKeyMessage(ref m))
			{
				m.Result = IntPtr.Zero;
				return;
			}
			if ((m.WParam.ToInt32() & 65535) == 18)
			{
				Form form = this.FindForm();
				if (form != null && form.ActiveMenu != null)
				{
					form.ActiveMenu.ProcessCmdKey(ref m, (Keys)m.WParam.ToInt32());
				}
				else if (ToolStripManager.ProcessMenuKey(ref m))
				{
					return;
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00029E64 File Offset: 0x00028064
		private void WmKeys(ref Message m)
		{
			if (this.ProcessKeyMessage(ref m))
			{
				m.Result = IntPtr.Zero;
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00029E88 File Offset: 0x00028088
		private void WmHelp(ref Message m)
		{
			Point mousePosition;
			if (m.LParam != IntPtr.Zero)
			{
				HELPINFO helpinfo = default(HELPINFO);
				helpinfo = (HELPINFO)Marshal.PtrToStructure(m.LParam, typeof(HELPINFO));
				mousePosition..ctor(helpinfo.MousePos.x, helpinfo.MousePos.y);
			}
			else
			{
				mousePosition = Control.MousePosition;
			}
			this.OnHelpRequested(new HelpEventArgs(mousePosition));
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00029F10 File Offset: 0x00028110
		private void WmKillFocus(ref Message m)
		{
			this.has_focus = false;
			this.OnLostFocus(EventArgs.Empty);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00029F24 File Offset: 0x00028124
		private void WmSetFocus(ref Message m)
		{
			if (!this.has_focus)
			{
				this.has_focus = true;
				this.OnGotFocus(EventArgs.Empty);
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00029F44 File Offset: 0x00028144
		private void WmSysColorChange(ref Message m)
		{
			ThemeEngine.Current.ResetDefaults();
			this.OnSystemColorsChanged(EventArgs.Empty);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00029F5C File Offset: 0x0002815C
		private void WmSetCursor(ref Message m)
		{
			if ((this.cursor == null && !this.use_wait_cursor) || (m.LParam.ToInt32() & 65535) != 1)
			{
				this.DefWndProc(ref m);
				return;
			}
			XplatUI.SetCursor(this.window.Handle, this.Cursor.handle);
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00029FD0 File Offset: 0x000281D0
		private void WmCaptureChanged(ref Message m)
		{
			this.is_captured = false;
			this.OnMouseCaptureChanged(EventArgs.Empty);
			m.Result = (IntPtr)0;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00029FF0 File Offset: 0x000281F0
		private void WmChangeUIState(ref Message m)
		{
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				XplatUI.SendMessage(control.Handle, Msg.WM_UPDATEUISTATE, m.WParam, m.LParam);
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002A078 File Offset: 0x00028278
		private void WmUpdateUIState(ref Message m)
		{
			int num = Control.LowOrder(m.WParam.ToInt32());
			int num2 = Control.HighOrder((long)m.WParam.ToInt32());
			if (num == 3)
			{
				return;
			}
			UICues uicues = UICues.None;
			if ((num2 & 2) != 0 && num == 2 != this.show_keyboard_cues)
			{
				uicues |= UICues.ChangeKeyboard;
				this.show_keyboard_cues = num == 2;
			}
			if ((num2 & 1) != 0 && num == 2 != this.show_focus_cues)
			{
				uicues |= UICues.ChangeFocus;
				this.show_focus_cues = num == 2;
			}
			if ((uicues & UICues.Changed) != UICues.None)
			{
				this.OnChangeUICues(new UICuesEventArgs(uicues));
				this.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.AutoSizeChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A0C RID: 2572 RVA: 0x0002A120 File Offset: 0x00028320
		protected virtual void OnAutoSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.AutoSizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A0D RID: 2573 RVA: 0x0002A154 File Offset: 0x00028354
		[EditorBrowsable(2)]
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.BackColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentBackColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A0E RID: 2574 RVA: 0x0002A1B4 File Offset: 0x000283B4
		[EditorBrowsable(2)]
		protected virtual void OnBackgroundImageChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.BackgroundImageChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentBackgroundImageChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A0F RID: 2575 RVA: 0x0002A214 File Offset: 0x00028414
		[EditorBrowsable(2)]
		protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.BackgroundImageLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A10 RID: 2576 RVA: 0x0002A248 File Offset: 0x00028448
		[EditorBrowsable(2)]
		protected virtual void OnBindingContextChanged(EventArgs e)
		{
			this.CheckDataBindings();
			EventHandler eventHandler = (EventHandler)base.Events[Control.BindingContextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentBindingContextChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CausesValidationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A11 RID: 2577 RVA: 0x0002A2B0 File Offset: 0x000284B0
		[EditorBrowsable(2)]
		protected virtual void OnCausesValidationChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.CausesValidationChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.UICuesEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A12 RID: 2578 RVA: 0x0002A2E4 File Offset: 0x000284E4
		[EditorBrowsable(2)]
		protected virtual void OnChangeUICues(UICuesEventArgs e)
		{
			UICuesEventHandler uicuesEventHandler = (UICuesEventHandler)base.Events[Control.ChangeUICuesEvent];
			if (uicuesEventHandler != null)
			{
				uicuesEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A13 RID: 2579 RVA: 0x0002A318 File Offset: 0x00028518
		[EditorBrowsable(2)]
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ClientSizeChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A14 RID: 2580 RVA: 0x0002A34C File Offset: 0x0002854C
		[EditorBrowsable(2)]
		protected virtual void OnClientSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ClientSizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ContextMenuChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A15 RID: 2581 RVA: 0x0002A380 File Offset: 0x00028580
		[EditorBrowsable(2)]
		protected virtual void OnContextMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ContextMenuChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ContextMenuStripChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A16 RID: 2582 RVA: 0x0002A3B4 File Offset: 0x000285B4
		[EditorBrowsable(2)]
		protected virtual void OnContextMenuStripChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ContextMenuStripChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A17 RID: 2583 RVA: 0x0002A3E8 File Offset: 0x000285E8
		[EditorBrowsable(2)]
		protected virtual void OnControlAdded(ControlEventArgs e)
		{
			ControlEventHandler controlEventHandler = (ControlEventHandler)base.Events[Control.ControlAddedEvent];
			if (controlEventHandler != null)
			{
				controlEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A18 RID: 2584 RVA: 0x0002A41C File Offset: 0x0002861C
		[EditorBrowsable(2)]
		protected virtual void OnControlRemoved(ControlEventArgs e)
		{
			ControlEventHandler controlEventHandler = (ControlEventHandler)base.Events[Control.ControlRemovedEvent];
			if (controlEventHandler != null)
			{
				controlEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.</summary>
		// Token: 0x06000A19 RID: 2585 RVA: 0x0002A450 File Offset: 0x00028650
		[EditorBrowsable(2)]
		protected virtual void OnCreateControl()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A1A RID: 2586 RVA: 0x0002A454 File Offset: 0x00028654
		[EditorBrowsable(2)]
		protected virtual void OnCursorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.CursorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentCursorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A1B RID: 2587 RVA: 0x0002A4B4 File Offset: 0x000286B4
		[EditorBrowsable(2)]
		protected virtual void OnDockChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.DockChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A1C RID: 2588 RVA: 0x0002A4E8 File Offset: 0x000286E8
		[EditorBrowsable(2)]
		protected virtual void OnDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.DoubleClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A1D RID: 2589 RVA: 0x0002A51C File Offset: 0x0002871C
		[EditorBrowsable(2)]
		protected virtual void OnDragDrop(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.DragDropEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A1E RID: 2590 RVA: 0x0002A550 File Offset: 0x00028750
		[EditorBrowsable(2)]
		protected virtual void OnDragEnter(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.DragEnterEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A1F RID: 2591 RVA: 0x0002A584 File Offset: 0x00028784
		[EditorBrowsable(2)]
		protected virtual void OnDragLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.DragLeaveEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A20 RID: 2592 RVA: 0x0002A5B8 File Offset: 0x000287B8
		[EditorBrowsable(2)]
		protected virtual void OnDragOver(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.DragOverEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A21 RID: 2593 RVA: 0x0002A5EC File Offset: 0x000287EC
		[EditorBrowsable(2)]
		protected virtual void OnEnabledChanged(EventArgs e)
		{
			if (this.IsHandleCreated)
			{
				if (this is Form)
				{
					if (((Form)this).context == null)
					{
						XplatUI.EnableWindow(this.window.Handle, this.Enabled);
					}
				}
				else
				{
					XplatUI.EnableWindow(this.window.Handle, this.Enabled);
				}
				this.Refresh();
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			foreach (Control control in this.Controls.GetAllControls())
			{
				control.OnParentEnabledChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A22 RID: 2594 RVA: 0x0002A6A8 File Offset: 0x000288A8
		[EditorBrowsable(2)]
		protected virtual void OnEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EnterEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A23 RID: 2595 RVA: 0x0002A6DC File Offset: 0x000288DC
		[EditorBrowsable(2)]
		protected virtual void OnFontChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.FontChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentFontChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A24 RID: 2596 RVA: 0x0002A73C File Offset: 0x0002893C
		[EditorBrowsable(2)]
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ForeColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentForeColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GiveFeedback" /> event.</summary>
		/// <param name="gfbevent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A25 RID: 2597 RVA: 0x0002A79C File Offset: 0x0002899C
		[EditorBrowsable(2)]
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
		{
			GiveFeedbackEventHandler giveFeedbackEventHandler = (GiveFeedbackEventHandler)base.Events[Control.GiveFeedbackEvent];
			if (giveFeedbackEventHandler != null)
			{
				giveFeedbackEventHandler(this, gfbevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A26 RID: 2598 RVA: 0x0002A7D0 File Offset: 0x000289D0
		[EditorBrowsable(2)]
		protected virtual void OnGotFocus(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.GotFocusEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A27 RID: 2599 RVA: 0x0002A804 File Offset: 0x00028A04
		[EditorBrowsable(2)]
		protected virtual void OnHandleCreated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.HandleCreatedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A28 RID: 2600 RVA: 0x0002A838 File Offset: 0x00028A38
		[EditorBrowsable(2)]
		protected virtual void OnHandleDestroyed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.HandleDestroyedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002A86C File Offset: 0x00028A6C
		internal void RaiseHelpRequested(HelpEventArgs hevent)
		{
			this.OnHelpRequested(hevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
		/// <param name="hevent">A <see cref="T:System.Windows.Forms.HelpEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A2A RID: 2602 RVA: 0x0002A878 File Offset: 0x00028A78
		[EditorBrowsable(2)]
		protected virtual void OnHelpRequested(HelpEventArgs hevent)
		{
			HelpEventHandler helpEventHandler = (HelpEventHandler)base.Events[Control.HelpRequestedEvent];
			if (helpEventHandler != null)
			{
				helpEventHandler(this, hevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ImeModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A2B RID: 2603 RVA: 0x0002A8AC File Offset: 0x00028AAC
		protected virtual void OnImeModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ImeModeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A2C RID: 2604 RVA: 0x0002A8E0 File Offset: 0x00028AE0
		[EditorBrowsable(2)]
		protected virtual void OnInvalidated(InvalidateEventArgs e)
		{
			if (this.UseDoubleBuffering)
			{
				if (e.InvalidRect == this.ClientRectangle)
				{
					this.InvalidateBackBuffer();
				}
				else if (this.backbuffer != null)
				{
					Rectangle rectangle = Rectangle.Inflate(e.InvalidRect, 1, 1);
					this.backbuffer.InvalidRegion.Union(rectangle);
				}
			}
			InvalidateEventHandler invalidateEventHandler = (InvalidateEventHandler)base.Events[Control.InvalidatedEvent];
			if (invalidateEventHandler != null)
			{
				invalidateEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A2D RID: 2605 RVA: 0x0002A968 File Offset: 0x00028B68
		[EditorBrowsable(2)]
		protected virtual void OnKeyDown(KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[Control.KeyDownEvent];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A2E RID: 2606 RVA: 0x0002A99C File Offset: 0x00028B9C
		[EditorBrowsable(2)]
		protected virtual void OnKeyPress(KeyPressEventArgs e)
		{
			KeyPressEventHandler keyPressEventHandler = (KeyPressEventHandler)base.Events[Control.KeyPressEvent];
			if (keyPressEventHandler != null)
			{
				keyPressEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A2F RID: 2607 RVA: 0x0002A9D0 File Offset: 0x00028BD0
		[EditorBrowsable(2)]
		protected virtual void OnKeyUp(KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[Control.KeyUpEvent];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A30 RID: 2608 RVA: 0x0002AA04 File Offset: 0x00028C04
		[EditorBrowsable(2)]
		protected virtual void OnLayout(LayoutEventArgs levent)
		{
			LayoutEventHandler layoutEventHandler = (LayoutEventHandler)base.Events[Control.LayoutEvent];
			if (layoutEventHandler != null)
			{
				layoutEventHandler(this, levent);
			}
			Size size = this.Size;
			if (this.Parent != null && this.AutoSize && !this.nested_layout && this.PreferredSize != size)
			{
				this.nested_layout = true;
				this.Parent.PerformLayout();
				this.nested_layout = false;
			}
			this.LayoutEngine.Layout(this, levent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A31 RID: 2609 RVA: 0x0002AA98 File Offset: 0x00028C98
		[EditorBrowsable(2)]
		protected virtual void OnLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.LeaveEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A32 RID: 2610 RVA: 0x0002AACC File Offset: 0x00028CCC
		[EditorBrowsable(2)]
		protected virtual void OnLocationChanged(EventArgs e)
		{
			this.OnMove(e);
			EventHandler eventHandler = (EventHandler)base.Events[Control.LocationChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A33 RID: 2611 RVA: 0x0002AB04 File Offset: 0x00028D04
		[EditorBrowsable(2)]
		protected virtual void OnLostFocus(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.LostFocusEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MarginChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A34 RID: 2612 RVA: 0x0002AB38 File Offset: 0x00028D38
		protected virtual void OnMarginChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MarginChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseCaptureChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A35 RID: 2613 RVA: 0x0002AB6C File Offset: 0x00028D6C
		[EditorBrowsable(2)]
		protected virtual void OnMouseCaptureChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MouseCaptureChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A36 RID: 2614 RVA: 0x0002ABA0 File Offset: 0x00028DA0
		[EditorBrowsable(2)]
		protected virtual void OnMouseClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseClickEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A37 RID: 2615 RVA: 0x0002ABD4 File Offset: 0x00028DD4
		[EditorBrowsable(2)]
		protected virtual void OnMouseDoubleClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseDoubleClickEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A38 RID: 2616 RVA: 0x0002AC08 File Offset: 0x00028E08
		[EditorBrowsable(2)]
		protected virtual void OnMouseDown(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseDownEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A39 RID: 2617 RVA: 0x0002AC3C File Offset: 0x00028E3C
		[EditorBrowsable(2)]
		protected virtual void OnMouseEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MouseEnterEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A3A RID: 2618 RVA: 0x0002AC70 File Offset: 0x00028E70
		[EditorBrowsable(2)]
		protected virtual void OnMouseHover(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MouseHoverEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A3B RID: 2619 RVA: 0x0002ACA4 File Offset: 0x00028EA4
		[EditorBrowsable(2)]
		protected virtual void OnMouseLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MouseLeaveEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A3C RID: 2620 RVA: 0x0002ACD8 File Offset: 0x00028ED8
		[EditorBrowsable(2)]
		protected virtual void OnMouseMove(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseMoveEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A3D RID: 2621 RVA: 0x0002AD0C File Offset: 0x00028F0C
		[EditorBrowsable(2)]
		protected virtual void OnMouseUp(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseUpEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A3E RID: 2622 RVA: 0x0002AD40 File Offset: 0x00028F40
		[EditorBrowsable(2)]
		protected virtual void OnMouseWheel(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseWheelEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Move" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A3F RID: 2623 RVA: 0x0002AD74 File Offset: 0x00028F74
		[EditorBrowsable(2)]
		protected virtual void OnMove(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MoveEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Notifies the control of Windows messages.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that represents the Windows message. </param>
		// Token: 0x06000A40 RID: 2624 RVA: 0x0002ADA8 File Offset: 0x00028FA8
		[EditorBrowsable(2)]
		protected virtual void OnNotifyMessage(Message m)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A41 RID: 2625 RVA: 0x0002ADAC File Offset: 0x00028FAC
		protected virtual void OnPaddingChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.PaddingChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A42 RID: 2626 RVA: 0x0002ADE0 File Offset: 0x00028FE0
		[EditorBrowsable(2)]
		protected virtual void OnPaint(PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[Control.PaintEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002AE14 File Offset: 0x00029014
		internal virtual void OnPaintBackgroundInternal(PaintEventArgs e)
		{
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002AE18 File Offset: 0x00029018
		internal virtual void OnPaintInternal(PaintEventArgs e)
		{
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint. </param>
		// Token: 0x06000A45 RID: 2629 RVA: 0x0002AE1C File Offset: 0x0002901C
		[EditorBrowsable(2)]
		protected virtual void OnPaintBackground(PaintEventArgs pevent)
		{
			this.PaintControlBackground(pevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BackColor" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A46 RID: 2630 RVA: 0x0002AE28 File Offset: 0x00029028
		[EditorBrowsable(2)]
		protected virtual void OnParentBackColorChanged(EventArgs e)
		{
			if (this.background_color.IsEmpty && this.background_image == null)
			{
				this.Invalidate();
				this.OnBackColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A47 RID: 2631 RVA: 0x0002AE60 File Offset: 0x00029060
		[EditorBrowsable(2)]
		protected virtual void OnParentBackgroundImageChanged(EventArgs e)
		{
			this.Invalidate();
			this.OnBackgroundImageChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BindingContext" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A48 RID: 2632 RVA: 0x0002AE70 File Offset: 0x00029070
		[EditorBrowsable(2)]
		protected virtual void OnParentBindingContextChanged(EventArgs e)
		{
			if (this.binding_context == null && this.Parent != null)
			{
				this.binding_context = this.Parent.binding_context;
				this.OnBindingContextChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A49 RID: 2633 RVA: 0x0002AEAC File Offset: 0x000290AC
		[EditorBrowsable(2)]
		protected virtual void OnParentChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ParentChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A4A RID: 2634 RVA: 0x0002AEE0 File Offset: 0x000290E0
		[EditorBrowsable(2)]
		protected virtual void OnParentCursorChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A4B RID: 2635 RVA: 0x0002AEE4 File Offset: 0x000290E4
		[EditorBrowsable(2)]
		protected virtual void OnParentEnabledChanged(EventArgs e)
		{
			if (this.is_enabled)
			{
				this.OnEnabledChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Font" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A4C RID: 2636 RVA: 0x0002AEF8 File Offset: 0x000290F8
		[EditorBrowsable(2)]
		protected virtual void OnParentFontChanged(EventArgs e)
		{
			if (this.font == null)
			{
				this.Invalidate();
				this.OnFontChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A4D RID: 2637 RVA: 0x0002AF14 File Offset: 0x00029114
		[EditorBrowsable(2)]
		protected virtual void OnParentForeColorChanged(EventArgs e)
		{
			if (this.foreground_color.IsEmpty)
			{
				this.Invalidate();
				this.OnForeColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A4E RID: 2638 RVA: 0x0002AF34 File Offset: 0x00029134
		[EditorBrowsable(2)]
		protected virtual void OnParentRightToLeftChanged(EventArgs e)
		{
			if (this.right_to_left == RightToLeft.Inherit)
			{
				this.Invalidate();
				this.OnRightToLeftChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A4F RID: 2639 RVA: 0x0002AF50 File Offset: 0x00029150
		[EditorBrowsable(2)]
		protected virtual void OnParentVisibleChanged(EventArgs e)
		{
			if (this.is_visible)
			{
				this.OnVisibleChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.QueryContinueDrag" /> event.</summary>
		/// <param name="qcdevent">A <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A50 RID: 2640 RVA: 0x0002AF64 File Offset: 0x00029164
		[EditorBrowsable(2)]
		protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
		{
			QueryContinueDragEventHandler queryContinueDragEventHandler = (QueryContinueDragEventHandler)base.Events[Control.QueryContinueDragEvent];
			if (queryContinueDragEventHandler != null)
			{
				queryContinueDragEventHandler(this, qcdevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs" /> that contains the event data.</param>
		// Token: 0x06000A51 RID: 2641 RVA: 0x0002AF98 File Offset: 0x00029198
		[EditorBrowsable(2)]
		protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			PreviewKeyDownEventHandler previewKeyDownEventHandler = (PreviewKeyDownEventHandler)base.Events[Control.PreviewKeyDownEvent];
			if (previewKeyDownEventHandler != null)
			{
				previewKeyDownEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null.</exception>
		// Token: 0x06000A52 RID: 2642 RVA: 0x0002AFCC File Offset: 0x000291CC
		[EditorBrowsable(2)]
		protected virtual void OnPrint(PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[Control.PaintEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RegionChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A53 RID: 2643 RVA: 0x0002B000 File Offset: 0x00029200
		[EditorBrowsable(2)]
		protected virtual void OnRegionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.RegionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A54 RID: 2644 RVA: 0x0002B034 File Offset: 0x00029234
		[EditorBrowsable(2)]
		protected virtual void OnResize(EventArgs e)
		{
			this.OnResizeInternal(e);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002B040 File Offset: 0x00029240
		internal virtual void OnResizeInternal(EventArgs e)
		{
			this.PerformLayout(this, "Bounds");
			EventHandler eventHandler = (EventHandler)base.Events[Control.ResizeEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A56 RID: 2646 RVA: 0x0002B080 File Offset: 0x00029280
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.RightToLeftChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentRightToLeftChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A57 RID: 2647 RVA: 0x0002B0E0 File Offset: 0x000292E0
		[EditorBrowsable(2)]
		protected virtual void OnSizeChanged(EventArgs e)
		{
			this.DisposeBackBuffer();
			this.OnResize(e);
			EventHandler eventHandler = (EventHandler)base.Events[Control.SizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.StyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A58 RID: 2648 RVA: 0x0002B120 File Offset: 0x00029320
		[EditorBrowsable(2)]
		protected virtual void OnStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.StyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A59 RID: 2649 RVA: 0x0002B154 File Offset: 0x00029354
		[EditorBrowsable(2)]
		protected virtual void OnSystemColorsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.SystemColorsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A5A RID: 2650 RVA: 0x0002B188 File Offset: 0x00029388
		[EditorBrowsable(2)]
		protected virtual void OnTabIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.TabIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabStopChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A5B RID: 2651 RVA: 0x0002B1BC File Offset: 0x000293BC
		[EditorBrowsable(2)]
		protected virtual void OnTabStopChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.TabStopChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A5C RID: 2652 RVA: 0x0002B1F0 File Offset: 0x000293F0
		[EditorBrowsable(2)]
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A5D RID: 2653 RVA: 0x0002B224 File Offset: 0x00029424
		[EditorBrowsable(2)]
		protected virtual void OnValidated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ValidatedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06000A5E RID: 2654 RVA: 0x0002B258 File Offset: 0x00029458
		[EditorBrowsable(2)]
		protected virtual void OnValidating(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Control.ValidatingEvent];
			if (cancelEventHandler != null)
			{
				cancelEventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000A5F RID: 2655 RVA: 0x0002B28C File Offset: 0x0002948C
		[EditorBrowsable(2)]
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			if (this.Visible)
			{
				this.CreateControl();
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.VisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
			foreach (Control control in this.Controls.GetAllControls())
			{
				if (control.Visible)
				{
					control.OnParentVisibleChanged(e);
				}
			}
		}

		// Token: 0x04000799 RID: 1945
		internal Rectangle bounds;

		// Token: 0x0400079A RID: 1946
		private Rectangle explicit_bounds;

		// Token: 0x0400079B RID: 1947
		internal object creator_thread;

		// Token: 0x0400079C RID: 1948
		internal Control.ControlNativeWindow window;

		// Token: 0x0400079D RID: 1949
		private IWindowTarget window_target;

		// Token: 0x0400079E RID: 1950
		private string name;

		// Token: 0x0400079F RID: 1951
		private bool is_created;

		// Token: 0x040007A0 RID: 1952
		internal bool has_focus;

		// Token: 0x040007A1 RID: 1953
		internal bool is_visible;

		// Token: 0x040007A2 RID: 1954
		internal bool is_entered;

		// Token: 0x040007A3 RID: 1955
		internal bool is_enabled;

		// Token: 0x040007A4 RID: 1956
		private bool is_accessible;

		// Token: 0x040007A5 RID: 1957
		private bool is_captured;

		// Token: 0x040007A6 RID: 1958
		internal bool is_toplevel;

		// Token: 0x040007A7 RID: 1959
		private bool is_recreating;

		// Token: 0x040007A8 RID: 1960
		private bool causes_validation;

		// Token: 0x040007A9 RID: 1961
		private bool is_focusing;

		// Token: 0x040007AA RID: 1962
		private int tab_index;

		// Token: 0x040007AB RID: 1963
		private bool tab_stop;

		// Token: 0x040007AC RID: 1964
		private bool is_disposed;

		// Token: 0x040007AD RID: 1965
		private bool is_disposing;

		// Token: 0x040007AE RID: 1966
		private Size client_size;

		// Token: 0x040007AF RID: 1967
		private Rectangle client_rect;

		// Token: 0x040007B0 RID: 1968
		private ControlStyles control_style;

		// Token: 0x040007B1 RID: 1969
		private ImeMode ime_mode;

		// Token: 0x040007B2 RID: 1970
		private object control_tag;

		// Token: 0x040007B3 RID: 1971
		internal int mouse_clicks;

		// Token: 0x040007B4 RID: 1972
		private Cursor cursor;

		// Token: 0x040007B5 RID: 1973
		internal bool allow_drop;

		// Token: 0x040007B6 RID: 1974
		private Region clip_region;

		// Token: 0x040007B7 RID: 1975
		internal Color foreground_color;

		// Token: 0x040007B8 RID: 1976
		internal Color background_color;

		// Token: 0x040007B9 RID: 1977
		private Image background_image;

		// Token: 0x040007BA RID: 1978
		internal Font font;

		// Token: 0x040007BB RID: 1979
		private string text;

		// Token: 0x040007BC RID: 1980
		internal BorderStyle border_style;

		// Token: 0x040007BD RID: 1981
		private bool show_keyboard_cues;

		// Token: 0x040007BE RID: 1982
		internal bool show_focus_cues;

		// Token: 0x040007BF RID: 1983
		internal bool force_double_buffer;

		// Token: 0x040007C0 RID: 1984
		private LayoutEngine layout_engine;

		// Token: 0x040007C1 RID: 1985
		internal int layout_suspended;

		// Token: 0x040007C2 RID: 1986
		private bool layout_pending;

		// Token: 0x040007C3 RID: 1987
		internal AnchorStyles anchor_style;

		// Token: 0x040007C4 RID: 1988
		internal DockStyle dock_style;

		// Token: 0x040007C5 RID: 1989
		private Control.LayoutType layout_type;

		// Token: 0x040007C6 RID: 1990
		private bool recalculate_distances = true;

		// Token: 0x040007C7 RID: 1991
		internal int dist_right;

		// Token: 0x040007C8 RID: 1992
		internal int dist_bottom;

		// Token: 0x040007C9 RID: 1993
		private Control.ControlCollection child_controls;

		// Token: 0x040007CA RID: 1994
		private Control parent;

		// Token: 0x040007CB RID: 1995
		private BindingContext binding_context;

		// Token: 0x040007CC RID: 1996
		private RightToLeft right_to_left;

		// Token: 0x040007CD RID: 1997
		private ContextMenu context_menu;

		// Token: 0x040007CE RID: 1998
		internal bool use_compatible_text_rendering;

		// Token: 0x040007CF RID: 1999
		private bool use_wait_cursor;

		// Token: 0x040007D0 RID: 2000
		private string accessible_name;

		// Token: 0x040007D1 RID: 2001
		private string accessible_description;

		// Token: 0x040007D2 RID: 2002
		private string accessible_default_action;

		// Token: 0x040007D3 RID: 2003
		private AccessibleRole accessible_role = AccessibleRole.Default;

		// Token: 0x040007D4 RID: 2004
		private AccessibleObject accessibility_object;

		// Token: 0x040007D5 RID: 2005
		private Control.DoubleBuffer backbuffer;

		// Token: 0x040007D6 RID: 2006
		private ControlBindingsCollection data_bindings;

		// Token: 0x040007D7 RID: 2007
		private static bool verify_thread_handle;

		// Token: 0x040007D8 RID: 2008
		private Padding padding;

		// Token: 0x040007D9 RID: 2009
		private ImageLayout backgroundimage_layout;

		// Token: 0x040007DA RID: 2010
		private Size maximum_size;

		// Token: 0x040007DB RID: 2011
		private Size minimum_size;

		// Token: 0x040007DC RID: 2012
		private Padding margin;

		// Token: 0x040007DD RID: 2013
		private ContextMenuStrip context_menu_strip;

		// Token: 0x040007DE RID: 2014
		private bool nested_layout;

		// Token: 0x040007DF RID: 2015
		private Point auto_scroll_offset;

		// Token: 0x040007E0 RID: 2016
		private AutoSizeMode auto_size_mode;

		// Token: 0x040007E1 RID: 2017
		private bool suppressing_key_press;

		// Token: 0x040007E2 RID: 2018
		private MenuTracker active_tracker;

		// Token: 0x040007E3 RID: 2019
		private bool auto_size;

		// Token: 0x020000A5 RID: 165
		internal enum LayoutType
		{
			// Token: 0x04000829 RID: 2089
			Anchor,
			// Token: 0x0400082A RID: 2090
			Dock
		}

		// Token: 0x020000A6 RID: 166
		internal class ControlNativeWindow : NativeWindow
		{
			// Token: 0x06000A60 RID: 2656 RVA: 0x0002B304 File Offset: 0x00029504
			public ControlNativeWindow(Control control)
			{
				this.owner = control;
			}

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002B314 File Offset: 0x00029514
			public Control Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x06000A62 RID: 2658 RVA: 0x0002B31C File Offset: 0x0002951C
			protected override void OnHandleChange()
			{
				this.owner.WindowTarget.OnHandleChange(this.owner.Handle);
			}

			// Token: 0x06000A63 RID: 2659 RVA: 0x0002B33C File Offset: 0x0002953C
			internal static Control ControlFromHandle(IntPtr hWnd)
			{
				Control.ControlNativeWindow controlNativeWindow = (Control.ControlNativeWindow)NativeWindow.FromHandle(hWnd);
				if (controlNativeWindow != null)
				{
					return controlNativeWindow.owner;
				}
				return null;
			}

			// Token: 0x06000A64 RID: 2660 RVA: 0x0002B364 File Offset: 0x00029564
			internal static Control ControlFromChildHandle(IntPtr handle)
			{
				for (Hwnd hwnd = Hwnd.ObjectFromHandle(handle); hwnd != null; hwnd = hwnd.Parent)
				{
					Control.ControlNativeWindow controlNativeWindow = (Control.ControlNativeWindow)NativeWindow.FromHandle(handle);
					if (controlNativeWindow != null)
					{
						return controlNativeWindow.owner;
					}
				}
				return null;
			}

			// Token: 0x06000A65 RID: 2661 RVA: 0x0002B3A4 File Offset: 0x000295A4
			protected override void WndProc(ref Message m)
			{
				this.owner.WindowTarget.OnMessage(ref m);
			}

			// Token: 0x0400082B RID: 2091
			private Control owner;
		}

		// Token: 0x020000A7 RID: 167
		private class ControlWindowTarget : IWindowTarget
		{
			// Token: 0x06000A66 RID: 2662 RVA: 0x0002B3B8 File Offset: 0x000295B8
			public ControlWindowTarget(Control control)
			{
				this.control = control;
			}

			// Token: 0x06000A67 RID: 2663 RVA: 0x0002B3C8 File Offset: 0x000295C8
			public void OnHandleChange(IntPtr newHandle)
			{
			}

			// Token: 0x06000A68 RID: 2664 RVA: 0x0002B3CC File Offset: 0x000295CC
			public void OnMessage(ref Message m)
			{
				this.control.WndProc(ref m);
			}

			// Token: 0x0400082C RID: 2092
			private Control control;
		}

		/// <summary>Provides information about a control that can be used by an accessibility application.</summary>
		// Token: 0x020000A8 RID: 168
		[ComVisible(true)]
		public class ControlAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" /> class.</summary>
			/// <param name="ownerControl">The <see cref="T:System.Windows.Forms.Control" /> that owns the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="ownerControl" /> parameter value is null. </exception>
			// Token: 0x06000A69 RID: 2665 RVA: 0x0002B3DC File Offset: 0x000295DC
			public ControlAccessibleObject(Control ownerControl)
				: base(ownerControl)
			{
				if (ownerControl == null)
				{
					throw new ArgumentNullException("owner");
				}
				this.handle = ownerControl.Handle;
			}

			/// <returns>A description of the default action for an object, or null if this object has no default action.</returns>
			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0002B410 File Offset: 0x00029610
			public override string DefaultAction
			{
				get
				{
					return base.DefaultAction;
				}
			}

			/// <summary>Gets the description of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</summary>
			/// <returns>A string describing the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</returns>
			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0002B418 File Offset: 0x00029618
			public override string Description
			{
				get
				{
					return base.Description;
				}
			}

			/// <summary>Gets or sets the handle of the accessible object.</summary>
			/// <returns>An <see cref="T:System.IntPtr" /> that represents the handle of the control.</returns>
			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0002B420 File Offset: 0x00029620
			// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0002B428 File Offset: 0x00029628
			public IntPtr Handle
			{
				get
				{
					return this.handle;
				}
				set
				{
				}
			}

			/// <summary>Gets the description of what the object does or how the object is used.</summary>
			/// <returns>The description of what the object does or how the object is used.</returns>
			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0002B42C File Offset: 0x0002962C
			public override string Help
			{
				get
				{
					return base.Help;
				}
			}

			/// <summary>Gets the object shortcut key or access key for an accessible object.</summary>
			/// <returns>The object shortcut key or access key for an accessible object, or null if there is no shortcut key associated with the object.</returns>
			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0002B434 File Offset: 0x00029634
			public override string KeyboardShortcut
			{
				get
				{
					return base.KeyboardShortcut;
				}
			}

			/// <summary>Gets or sets the accessible object name.</summary>
			/// <returns>The accessible object name.</returns>
			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0002B43C File Offset: 0x0002963C
			// (set) Token: 0x06000A71 RID: 2673 RVA: 0x0002B444 File Offset: 0x00029644
			public override string Name
			{
				get
				{
					return base.Name;
				}
				set
				{
					base.Name = value;
				}
			}

			/// <summary>Gets the owner of the accessible object.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that owns the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</returns>
			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002B450 File Offset: 0x00029650
			public Control Owner
			{
				get
				{
					return this.owner;
				}
			}

			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or null if there is no parent object.</returns>
			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002B458 File Offset: 0x00029658
			public override AccessibleObject Parent
			{
				get
				{
					return base.Parent;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</returns>
			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002B460 File Offset: 0x00029660
			public override AccessibleRole Role
			{
				get
				{
					return base.Role;
				}
			}

			/// <summary>Gets an identifier for a Help topic and the path to the Help file associated with this accessible object.</summary>
			/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter will contain the path to the Help file associated with this accessible object, or null if there is no IAccessible interface specified.</returns>
			/// <param name="fileName">When this method returns, contains a string that represents the path to the Help file associated with this accessible object. This parameter is passed uninitialized. </param>
			// Token: 0x06000A75 RID: 2677 RVA: 0x0002B468 File Offset: 0x00029668
			public override int GetHelpTopic(out string fileName)
			{
				return base.GetHelpTopic(out fileName);
			}

			/// <summary>Notifies accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" />.</summary>
			/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of. </param>
			// Token: 0x06000A76 RID: 2678 RVA: 0x0002B474 File Offset: 0x00029674
			[MonoTODO("Stub, does nothing")]
			public void NotifyClients(AccessibleEvents accEvent)
			{
			}

			/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control.</summary>
			/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of. </param>
			/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event. </param>
			// Token: 0x06000A77 RID: 2679 RVA: 0x0002B478 File Offset: 0x00029678
			[MonoTODO("Stub, does nothing")]
			public void NotifyClients(AccessibleEvents accEvent, int childID)
			{
			}

			/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control, giving the identification of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
			/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
			/// <param name="objectID">The identifier of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
			/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event.</param>
			// Token: 0x06000A78 RID: 2680 RVA: 0x0002B47C File Offset: 0x0002967C
			[MonoTODO("Stub, does nothing")]
			public void NotifyClients(AccessibleEvents accEvent, int objectID, int childID)
			{
			}

			/// <returns>A string that represents the current object.</returns>
			// Token: 0x06000A79 RID: 2681 RVA: 0x0002B480 File Offset: 0x00029680
			public override string ToString()
			{
				return "ControlAccessibleObject: Owner = " + this.owner.ToString() + ", Text: " + this.owner.text;
			}

			// Token: 0x0400082D RID: 2093
			private IntPtr handle;
		}

		// Token: 0x020000A9 RID: 169
		private class DoubleBuffer : IDisposable
		{
			// Token: 0x06000A7A RID: 2682 RVA: 0x0002B4A8 File Offset: 0x000296A8
			public DoubleBuffer(Control parent)
			{
				this.parent = parent;
				this.real_graphics = new Stack();
				int num = parent.Width;
				int num2 = parent.Height;
				if (num < 1)
				{
					num = 1;
				}
				if (num2 < 1)
				{
					num2 = 1;
				}
				XplatUI.CreateOffscreenDrawable(parent.Handle, num, num2, out this.back_buffer);
				this.Invalidate();
			}

			// Token: 0x06000A7B RID: 2683 RVA: 0x0002B508 File Offset: 0x00029708
			void IDisposable.Dispose()
			{
				this.Dispose();
			}

			// Token: 0x06000A7C RID: 2684 RVA: 0x0002B510 File Offset: 0x00029710
			public void Blit(PaintEventArgs pe)
			{
				Graphics offscreenGraphics = XplatUI.GetOffscreenGraphics(this.back_buffer);
				XplatUI.BlitFromOffscreen(this.parent.Handle, pe.Graphics, this.back_buffer, offscreenGraphics, pe.ClipRectangle);
				offscreenGraphics.Dispose();
			}

			// Token: 0x06000A7D RID: 2685 RVA: 0x0002B554 File Offset: 0x00029754
			public void Start(PaintEventArgs pe)
			{
				this.real_graphics.Push(pe.SetGraphics(XplatUI.GetOffscreenGraphics(this.back_buffer)));
			}

			// Token: 0x06000A7E RID: 2686 RVA: 0x0002B574 File Offset: 0x00029774
			public void End(PaintEventArgs pe)
			{
				Graphics graphics = pe.SetGraphics((Graphics)this.real_graphics.Pop());
				if (this.pending_disposal)
				{
					this.Dispose();
				}
				else
				{
					XplatUI.BlitFromOffscreen(this.parent.Handle, pe.Graphics, this.back_buffer, graphics, pe.ClipRectangle);
					this.InvalidRegion.Exclude(pe.ClipRectangle);
				}
				graphics.Dispose();
			}

			// Token: 0x06000A7F RID: 2687 RVA: 0x0002B5E8 File Offset: 0x000297E8
			public void Invalidate()
			{
				if (this.InvalidRegion != null)
				{
					this.InvalidRegion.Dispose();
				}
				this.InvalidRegion = new Region(this.parent.ClientRectangle);
			}

			// Token: 0x06000A80 RID: 2688 RVA: 0x0002B624 File Offset: 0x00029824
			public void Dispose()
			{
				if (this.real_graphics.Count > 0)
				{
					this.pending_disposal = true;
					return;
				}
				XplatUI.DestroyOffscreenDrawable(this.back_buffer);
				if (this.InvalidRegion != null)
				{
					this.InvalidRegion.Dispose();
				}
				this.InvalidRegion = null;
				this.back_buffer = null;
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000A81 RID: 2689 RVA: 0x0002B680 File Offset: 0x00029880
			~DoubleBuffer()
			{
				this.Dispose();
			}

			// Token: 0x0400082E RID: 2094
			public Region InvalidRegion;

			// Token: 0x0400082F RID: 2095
			private Stack real_graphics;

			// Token: 0x04000830 RID: 2096
			private object back_buffer;

			// Token: 0x04000831 RID: 2097
			private Control parent;

			// Token: 0x04000832 RID: 2098
			private bool pending_disposal;
		}

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Control" /> objects.</summary>
		// Token: 0x020000AA RID: 170
		[ListBindable(false)]
		[ComVisible(false)]
		public class ControlCollection : ArrangedElementCollection, ICollection, IEnumerable, IList, ICloneable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.Control" /> representing the control that owns the control collection. </param>
			// Token: 0x06000A82 RID: 2690 RVA: 0x0002B6BC File Offset: 0x000298BC
			public ControlCollection(Control owner)
			{
				this.owner = owner;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			// Token: 0x06000A83 RID: 2691 RVA: 0x0002B6CC File Offset: 0x000298CC
			int IList.Add(object control)
			{
				if (!(control is Control))
				{
					throw new ArgumentException("Object of type Control required", "control");
				}
				if (control == null)
				{
					throw new ArgumentException("control", "Cannot add null controls");
				}
				bool flag = this.owner is MdiClient || (this.owner is Form && ((Form)this.owner).IsMdiContainer);
				bool topLevel = ((Control)control).GetTopLevel();
				bool flag2 = control is Form && ((Form)control).IsMdiChild;
				if (topLevel && (!flag || !flag2))
				{
					throw new ArgumentException("Cannot add a top level control to a control.", "control");
				}
				return this.list.Add(control);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			// Token: 0x06000A84 RID: 2692 RVA: 0x0002B798 File Offset: 0x00029998
			void IList.Remove(object control)
			{
				if (!(control is Control))
				{
					throw new ArgumentException("Object of type Control required", "control");
				}
				this.all_controls = null;
				this.list.Remove(control);
			}

			/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
			// Token: 0x06000A85 RID: 2693 RVA: 0x0002B7D4 File Offset: 0x000299D4
			object ICloneable.Clone()
			{
				return new Control.ControlCollection(this.owner)
				{
					list = (ArrayList)this.list.Clone()
				};
			}

			/// <summary>Gets the control that owns this <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that owns this <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			// Token: 0x1700024D RID: 589
			// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002B804 File Offset: 0x00029A04
			public Control Owner
			{
				get
				{
					return this.owner;
				}
			}

			/// <summary>Indicates a <see cref="T:System.Windows.Forms.Control" /> with the specified key in the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> with the specified key within the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			/// <param name="key">The name of the control to retrieve from the control collection.</param>
			// Token: 0x1700024E RID: 590
			public virtual Control this[string key]
			{
				get
				{
					int num = this.IndexOfKey(key);
					if (num >= 0)
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Indicates the <see cref="T:System.Windows.Forms.Control" /> at the specified indexed location in the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> located at the specified index location within the control collection.</returns>
			/// <param name="index">The index of the control to retrieve from the control collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than zero or is greater than or equal to the number of controls in the collection. </exception>
			// Token: 0x1700024F RID: 591
			public virtual Control this[int index]
			{
				get
				{
					if (index < 0 || index >= this.list.Count)
					{
						throw new ArgumentOutOfRangeException("index", index, "ControlCollection does not have that many controls");
					}
					return (Control)this.list[index];
				}
			}

			/// <summary>Adds the specified control to the control collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add to the control collection. </param>
			/// <exception cref="T:System.Exception">The specified control is a top-level control, or a circular control reference would result if this control were added to the control collection. </exception>
			/// <exception cref="T:System.ArgumentException">The object assigned to the <paramref name="value" /> parameter is not a <see cref="T:System.Windows.Forms.Control" />. </exception>
			// Token: 0x06000A89 RID: 2697 RVA: 0x0002B880 File Offset: 0x00029A80
			public virtual void Add(Control value)
			{
				if (value == null)
				{
					return;
				}
				Form form = value as Form;
				Form form2 = this.owner as Form;
				bool flag = this.owner is MdiClient || (form2 != null && form2.IsMdiContainer);
				bool topLevel = value.GetTopLevel();
				bool flag2 = form != null && form.IsMdiChild;
				if (topLevel && (!flag || !flag2))
				{
					throw new ArgumentException("Cannot add a top level control to a control.", "value");
				}
				if (flag2 && form.MdiParent != null && form.MdiParent != this.owner && form.MdiParent != this.owner.Parent)
				{
					throw new ArgumentException("Form cannot be added to the Controls collection that has a valid MDI parent.", "value");
				}
				value.recalculate_distances = true;
				if (this.Contains(value))
				{
					this.owner.PerformLayout();
					return;
				}
				if (value.tab_index == -1)
				{
					int num = 0;
					int count = this.owner.child_controls.Count;
					for (int i = 0; i < count; i++)
					{
						int tab_index = this.owner.child_controls[i].tab_index;
						if (tab_index >= num)
						{
							num = tab_index + 1;
						}
					}
					value.tab_index = num;
				}
				if (value.parent != null)
				{
					value.parent.Controls.Remove(value);
				}
				this.all_controls = null;
				this.list.Add(value);
				value.ChangeParent(this.owner);
				value.InitLayout();
				if (this.owner.Visible)
				{
					this.owner.UpdateChildrenZOrder();
				}
				this.owner.PerformLayout(value, "Parent");
				this.owner.OnControlAdded(new ControlEventArgs(value));
			}

			// Token: 0x06000A8A RID: 2698 RVA: 0x0002BA54 File Offset: 0x00029C54
			internal void AddToList(Control c)
			{
				this.all_controls = null;
				this.list.Add(c);
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x0002BA6C File Offset: 0x00029C6C
			internal virtual void AddImplicit(Control control)
			{
				if (this.impl_list == null)
				{
					this.impl_list = new ArrayList();
				}
				if (this.AllContains(control))
				{
					this.owner.PerformLayout();
					return;
				}
				if (control.parent != null)
				{
					control.parent.Controls.Remove(control);
				}
				this.all_controls = null;
				this.impl_list.Add(control);
				control.ChangeParent(this.owner);
				control.InitLayout();
				if (this.owner.Visible)
				{
					this.owner.UpdateChildrenZOrder();
				}
				if (control.VisibleInternal)
				{
					this.owner.PerformLayout(control, "Parent");
				}
			}

			/// <summary>Adds an array of control objects to the collection.</summary>
			/// <param name="controls">An array of <see cref="T:System.Windows.Forms.Control" /> objects to add to the collection. </param>
			// Token: 0x06000A8C RID: 2700 RVA: 0x0002BB20 File Offset: 0x00029D20
			[DesignerSerializationVisibility(0)]
			public virtual void AddRange(Control[] controls)
			{
				if (controls == null)
				{
					throw new ArgumentNullException("controls");
				}
				this.owner.SuspendLayout();
				try
				{
					for (int i = 0; i < controls.Length; i++)
					{
						this.Add(controls[i]);
					}
				}
				finally
				{
					this.owner.ResumeLayout();
				}
			}

			// Token: 0x06000A8D RID: 2701 RVA: 0x0002BB94 File Offset: 0x00029D94
			internal virtual void AddRangeImplicit(Control[] controls)
			{
				if (controls == null)
				{
					throw new ArgumentNullException("controls");
				}
				this.owner.SuspendLayout();
				try
				{
					for (int i = 0; i < controls.Length; i++)
					{
						this.AddImplicit(controls[i]);
					}
				}
				finally
				{
					this.owner.ResumeLayout(false);
				}
			}

			/// <summary>Removes all controls from the collection.</summary>
			// Token: 0x06000A8E RID: 2702 RVA: 0x0002BC08 File Offset: 0x00029E08
			public new virtual void Clear()
			{
				this.all_controls = null;
				while (this.list.Count > 0)
				{
					this.Remove((Control)this.list[this.list.Count - 1]);
				}
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x0002BC58 File Offset: 0x00029E58
			internal virtual void ClearImplicit()
			{
				if (this.impl_list == null)
				{
					return;
				}
				this.all_controls = null;
				this.impl_list.Clear();
			}

			/// <summary>Determines whether the specified control is a member of the collection.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.Control" /> is a member of the collection; otherwise, false.</returns>
			/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to locate in the collection. </param>
			// Token: 0x06000A90 RID: 2704 RVA: 0x0002BC78 File Offset: 0x00029E78
			public bool Contains(Control control)
			{
				return this.list.Contains(control);
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x0002BC88 File Offset: 0x00029E88
			internal bool ImplicitContains(Control value)
			{
				return this.impl_list != null && this.impl_list.Contains(value);
			}

			// Token: 0x06000A92 RID: 2706 RVA: 0x0002BCA4 File Offset: 0x00029EA4
			internal bool AllContains(Control value)
			{
				return this.Contains(value) || this.ImplicitContains(value);
			}

			/// <summary>Determines whether the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> contains an item with the specified key.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> contains an item with the specified key; otherwise, false.</returns>
			/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </param>
			// Token: 0x06000A93 RID: 2707 RVA: 0x0002BCBC File Offset: 0x00029EBC
			public virtual bool ContainsKey(string key)
			{
				return this.IndexOfKey(key) >= 0;
			}

			/// <summary>Searches for controls by their <see cref="P:System.Windows.Forms.Control.Name" /> property and builds an array of all the controls that match.</summary>
			/// <returns>An array of type <see cref="T:System.Windows.Forms.Control" /> containing the matching controls.</returns>
			/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </param>
			/// <param name="searchAllChildren">true to search all child controls; otherwise, false. </param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="key" /> parameter is null or the empty string (""). </exception>
			// Token: 0x06000A94 RID: 2708 RVA: 0x0002BCCC File Offset: 0x00029ECC
			public Control[] Find(string key, bool searchAllChildren)
			{
				if (string.IsNullOrEmpty(key))
				{
					throw new ArgumentNullException("key");
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.list)
				{
					Control control = (Control)obj;
					if (control.Name.Equals(key, 1))
					{
						arrayList.Add(control);
					}
					if (searchAllChildren)
					{
						arrayList.AddRange(control.Controls.Find(key, true));
					}
				}
				return (Control[])arrayList.ToArray(typeof(Control));
			}

			/// <summary>Retrieves the index of the specified child control within the control collection.</summary>
			/// <returns>A zero-based index value that represents the location of the specified child control within the control collection.</returns>
			/// <param name="child">The <see cref="T:System.Windows.Forms.Control" /> to search for in the control collection. </param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </exception>
			// Token: 0x06000A95 RID: 2709 RVA: 0x0002BD98 File Offset: 0x00029F98
			public int GetChildIndex(Control child)
			{
				return this.GetChildIndex(child, false);
			}

			/// <summary>Retrieves the index of the specified child control within the control collection, and optionally raises an exception if the specified control is not within the control collection.</summary>
			/// <returns>A zero-based index value that represents the location of the specified child control within the control collection; otherwise -1 if the specified <see cref="T:System.Windows.Forms.Control" /> is not found in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			/// <param name="child">The <see cref="T:System.Windows.Forms.Control" /> to search for in the control collection. </param>
			/// <param name="throwException">true to throw an exception if the <see cref="T:System.Windows.Forms.Control" /> specified in the <paramref name="child" /> parameter is not a control in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />; otherwise, false. </param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />, and the <paramref name="throwException" /> parameter value is true. </exception>
			// Token: 0x06000A96 RID: 2710 RVA: 0x0002BDA4 File Offset: 0x00029FA4
			public virtual int GetChildIndex(Control child, bool throwException)
			{
				int num = this.list.IndexOf(child);
				if (num == -1 && throwException)
				{
					throw new ArgumentException("Not a child control", "child");
				}
				return num;
			}

			/// <summary>Retrieves a reference to an enumerator object that is used to iterate over a <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x06000A97 RID: 2711 RVA: 0x0002BDDC File Offset: 0x00029FDC
			public override IEnumerator GetEnumerator()
			{
				return new Control.ControlCollection.ControlCollectionEnumerator(this.list);
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x0002BDEC File Offset: 0x00029FEC
			internal IEnumerator GetAllEnumerator()
			{
				Control[] allControls = this.GetAllControls();
				return allControls.GetEnumerator();
			}

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002BE08 File Offset: 0x0002A008
			internal ArrayList ImplicitControls
			{
				get
				{
					return this.impl_list;
				}
			}

			// Token: 0x06000A9A RID: 2714 RVA: 0x0002BE10 File Offset: 0x0002A010
			internal Control[] GetAllControls()
			{
				if (this.all_controls != null)
				{
					return this.all_controls;
				}
				if (this.impl_list == null)
				{
					this.all_controls = (Control[])this.list.ToArray(typeof(Control));
					return this.all_controls;
				}
				this.all_controls = new Control[this.list.Count + this.impl_list.Count];
				this.impl_list.CopyTo(this.all_controls);
				this.list.CopyTo(this.all_controls, this.impl_list.Count);
				return this.all_controls;
			}

			/// <summary>Retrieves the index of the specified control in the control collection.</summary>
			/// <returns>A zero-based index value that represents the position of the specified <see cref="T:System.Windows.Forms.Control" /> in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to locate in the collection. </param>
			// Token: 0x06000A9B RID: 2715 RVA: 0x0002BEB8 File Offset: 0x0002A0B8
			public int IndexOf(Control control)
			{
				return this.list.IndexOf(control);
			}

			/// <summary>Retrieves the index of the first occurrence of the specified item within the collection.</summary>
			/// <returns>The zero-based index of the first occurrence of the control with the specified name in the collection.</returns>
			/// <param name="key">The name of the control to search for. </param>
			// Token: 0x06000A9C RID: 2716 RVA: 0x0002BEC8 File Offset: 0x0002A0C8
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					if (((Control)this.list[i]).Name.Equals(key, 1))
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Removes the specified control from the control collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to remove from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </param>
			// Token: 0x06000A9D RID: 2717 RVA: 0x0002BF24 File Offset: 0x0002A124
			public virtual void Remove(Control value)
			{
				if (value == null)
				{
					return;
				}
				this.all_controls = null;
				this.list.Remove(value);
				this.owner.PerformLayout(value, "Parent");
				this.owner.OnControlRemoved(new ControlEventArgs(value));
				ContainerControl containerControl = this.owner.InternalGetContainerControl();
				if (containerControl != null)
				{
					containerControl.ChildControlRemoved(value);
				}
				value.ChangeParent(null);
				this.owner.UpdateChildrenZOrder();
			}

			// Token: 0x06000A9E RID: 2718 RVA: 0x0002BF98 File Offset: 0x0002A198
			internal virtual void RemoveImplicit(Control control)
			{
				if (this.impl_list != null)
				{
					this.all_controls = null;
					this.impl_list.Remove(control);
					this.owner.PerformLayout(control, "Parent");
					this.owner.OnControlRemoved(new ControlEventArgs(control));
				}
				control.ChangeParent(null);
				this.owner.UpdateChildrenZOrder();
			}

			/// <summary>Removes a control from the control collection at the specified indexed location.</summary>
			/// <param name="index">The index value of the <see cref="T:System.Windows.Forms.Control" /> to remove. </param>
			// Token: 0x06000A9F RID: 2719 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index", index, "ControlCollection does not have that many controls");
				}
				this.Remove((Control)this.list[index]);
			}

			/// <summary>Removes the child control with the specified key.</summary>
			/// <param name="key">The name of the child control to remove. </param>
			// Token: 0x06000AA0 RID: 2720 RVA: 0x0002C04C File Offset: 0x0002A24C
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Sets the index of the specified child control in the collection to the specified index value.</summary>
			/// <param name="child">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> to search for. </param>
			/// <param name="newIndex">The new index value of the control. </param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="child" /> control is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </exception>
			// Token: 0x06000AA1 RID: 2721 RVA: 0x0002C070 File Offset: 0x0002A270
			public virtual void SetChildIndex(Control child, int newIndex)
			{
				if (child == null)
				{
					throw new ArgumentNullException("child");
				}
				int num = this.list.IndexOf(child);
				if (num == -1)
				{
					throw new ArgumentException("Not a child control", "child");
				}
				if (num == newIndex)
				{
					return;
				}
				this.all_controls = null;
				this.list.RemoveAt(num);
				if (newIndex > this.list.Count)
				{
					this.list.Add(child);
				}
				else
				{
					this.list.Insert(newIndex, child);
				}
				child.UpdateZOrder();
				this.owner.PerformLayout();
			}

			// Token: 0x04000833 RID: 2099
			private ArrayList impl_list;

			// Token: 0x04000834 RID: 2100
			private Control[] all_controls;

			// Token: 0x04000835 RID: 2101
			private Control owner;

			// Token: 0x020000AB RID: 171
			internal class ControlCollectionEnumerator : IEnumerator
			{
				// Token: 0x06000AA2 RID: 2722 RVA: 0x0002C110 File Offset: 0x0002A310
				public ControlCollectionEnumerator(ArrayList collection)
				{
					this.list = collection;
				}

				// Token: 0x17000251 RID: 593
				// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002C128 File Offset: 0x0002A328
				public object Current
				{
					get
					{
						object obj;
						try
						{
							obj = this.list[this.position];
						}
						catch (IndexOutOfRangeException)
						{
							throw new InvalidOperationException();
						}
						return obj;
					}
				}

				// Token: 0x06000AA4 RID: 2724 RVA: 0x0002C17C File Offset: 0x0002A37C
				public bool MoveNext()
				{
					this.position++;
					return this.position < this.list.Count;
				}

				// Token: 0x06000AA5 RID: 2725 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
				public void Reset()
				{
					this.position = -1;
				}

				// Token: 0x04000836 RID: 2102
				private ArrayList list;

				// Token: 0x04000837 RID: 2103
				private int position = -1;
			}
		}

		// Token: 0x02000633 RID: 1587
		// (Invoke) Token: 0x0600507E RID: 20606
		private delegate void RemoveDelegate(object c);
	}
}
