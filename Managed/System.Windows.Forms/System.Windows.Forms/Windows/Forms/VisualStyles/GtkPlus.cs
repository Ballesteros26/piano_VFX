using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x020004E2 RID: 1250
	internal class GtkPlus
	{
		// Token: 0x06004CBF RID: 19647 RVA: 0x00133B4C File Offset: 0x00131D4C
		protected GtkPlus()
		{
			this.widgets = new IntPtr[this.WidgetTypeCount];
			this.styles = new IntPtr[this.WidgetTypeCount];
			this.window = GtkPlus.gtk_window_new(GtkPlus.GtkWindowType.GTK_WINDOW_TOPLEVEL);
			this.@fixed = GtkPlus.gtk_fixed_new();
			GtkPlus.gtk_container_add(this.window, this.@fixed);
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[0] = GtkPlus.gtk_button_new());
			GtkPlus.GTK_WIDGET_SET_FLAGS(this.widgets[0], GtkPlus.GtkWidgetFlags.GTK_CAN_DEFAULT);
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[1] = GtkPlus.gtk_check_button_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[2] = GtkPlus.gtk_combo_box_entry_new());
			GtkPlus.gtk_widget_realize(this.widgets[2]);
			this.combo_box_drop_down_toggle_button = GtkPlus.GetFirstChildWidgetOfType.Get(this.widgets[2], GtkPlus.gtk_toggle_button_get_type());
			GtkPlus.gtk_widget_realize(this.combo_box_drop_down_toggle_button);
			this.combo_box_drop_down_arrow = GtkPlus.GetFirstChildWidgetOfType.Get(this.combo_box_drop_down_toggle_button, GtkPlus.gtk_arrow_get_type());
			GtkPlus.g_object_ref(this.combo_box_drop_down_toggle_button_style = GtkPlus.GetWidgetStyle(this.combo_box_drop_down_toggle_button));
			GtkPlus.g_object_ref(this.combo_box_drop_down_arrow_style = GtkPlus.GetWidgetStyle(this.combo_box_drop_down_arrow));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[3] = GtkPlus.gtk_frame_new(null));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[4] = GtkPlus.gtk_progress_bar_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[5] = GtkPlus.gtk_radio_button_new(IntPtr.Zero));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[6] = GtkPlus.gtk_hscrollbar_new(IntPtr.Zero));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[7] = GtkPlus.gtk_vscrollbar_new(IntPtr.Zero));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[8] = GtkPlus.gtk_statusbar_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[9] = GtkPlus.gtk_notebook_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[10] = GtkPlus.gtk_entry_new());
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[11] = GtkPlus.gtk_toolbar_new());
			IntPtr intPtr = GtkPlus.gtk_tool_button_new(IntPtr.Zero, null);
			GtkPlus.gtk_toolbar_insert(this.widgets[11], intPtr, -1);
			this.tool_bar_button = GtkPlus.gtk_bin_get_child(intPtr);
			GtkPlus.g_object_ref(this.tool_bar_button_style = GtkPlus.GetWidgetStyle(this.tool_bar_button));
			IntPtr intPtr2 = GtkPlus.gtk_toggle_tool_button_new();
			GtkPlus.gtk_toolbar_insert(this.widgets[11], intPtr2, -1);
			this.tool_bar_toggle_button = GtkPlus.gtk_bin_get_child(intPtr2);
			GtkPlus.g_object_ref(this.tool_bar_toggle_button_style = GtkPlus.GetWidgetStyle(this.tool_bar_toggle_button));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[12] = GtkPlus.gtk_hscale_new_with_range(0.0, 1.0, 1.0));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[13] = GtkPlus.gtk_vscale_new_with_range(0.0, 1.0, 1.0));
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[14] = GtkPlus.gtk_tree_view_new());
			this.tree_view_column = GtkPlus.gtk_tree_view_column_new();
			GtkPlus.gtk_tree_view_insert_column(this.widgets[14], this.tree_view_column, -1);
			this.tree_view_column_button = ((GtkPlus.GtkTreeViewColumn)Marshal.PtrToStructure(this.tree_view_column, typeof(GtkPlus.GtkTreeViewColumn))).button;
			GtkPlus.g_object_ref(this.tree_view_column_button_style = GtkPlus.GetWidgetStyle(this.tree_view_column_button));
			IntPtr intPtr3 = GtkPlus.gtk_adjustment_new(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
			GtkPlus.gtk_container_add(this.@fixed, this.widgets[15] = GtkPlus.gtk_spin_button_new(intPtr3, 0.0, 0U));
			for (int i = 0; i < this.WidgetTypeCount; i++)
			{
				GtkPlus.g_object_ref(this.styles[i] = GtkPlus.GetWidgetStyle(this.widgets[i]));
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06004CC0 RID: 19648 RVA: 0x00134170 File Offset: 0x00132370
		public static GtkPlus Instance
		{
			get
			{
				return GtkPlus.instance;
			}
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x00134178 File Offset: 0x00132378
		public static bool Initialize()
		{
			bool flag;
			try
			{
				if (GtkPlus.gtk_check_version(2U, 10U, 0U) != IntPtr.Zero)
				{
					flag = false;
				}
				else
				{
					int num = 0;
					string[] array = new string[1];
					bool flag2 = GtkPlus.gtk_init_check(ref num, ref array);
					if (flag2)
					{
						GtkPlus.instance = new GtkPlus();
					}
					flag = flag2;
				}
			}
			catch (DllNotFoundException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x00134200 File Offset: 0x00132400
		protected override void Finalize()
		{
			try
			{
				GtkPlus.gtk_object_destroy(this.window);
				for (int i = 0; i < this.WidgetTypeCount; i++)
				{
					GtkPlus.g_object_unref(this.styles[i]);
				}
				GtkPlus.g_object_unref(this.combo_box_drop_down_toggle_button_style);
				GtkPlus.g_object_unref(this.combo_box_drop_down_arrow_style);
				GtkPlus.g_object_unref(this.tool_bar_button_style);
				GtkPlus.g_object_unref(this.tool_bar_toggle_button_style);
				GtkPlus.g_object_unref(this.tree_view_column_button_style);
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0013429C File Offset: 0x0013249C
		public void ButtonPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, bool @default, GtkPlusState state)
		{
			this.button_painter.Configure(@default, state);
			this.Paint(GtkPlus.WidgetType.Button, bounds, dc, clippingArea, this.button_painter);
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x001342C8 File Offset: 0x001324C8
		public void CheckBoxPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, GtkPlusToggleButtonValue value)
		{
			this.check_box_painter.Configure(state, value);
			this.Paint(GtkPlus.WidgetType.CheckBox, bounds, dc, clippingArea, this.check_box_painter);
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x001342F4 File Offset: 0x001324F4
		private Size GetGtkCheckButtonIndicatorSize(GtkPlus.WidgetType widgetType)
		{
			int widgetStyleInteger = GtkPlus.GetWidgetStyleInteger(this.widgets[(int)widgetType], "indicator-size");
			return new Size(widgetStyleInteger, widgetStyleInteger);
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0013431C File Offset: 0x0013251C
		public Size CheckBoxGetSize()
		{
			return this.GetGtkCheckButtonIndicatorSize(GtkPlus.WidgetType.CheckBox);
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x00134328 File Offset: 0x00132528
		public void ComboBoxPaintDropDownButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state)
		{
			this.combo_box_drop_down_button_painter.Configure(state);
			this.Paint(GtkPlus.WidgetType.ComboBox, bounds, dc, clippingArea, this.combo_box_drop_down_button_painter);
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x00134354 File Offset: 0x00132554
		public void ComboBoxPaintBorder(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.ComboBox, bounds, dc, clippingArea, this.combo_box_border_painter);
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x00134368 File Offset: 0x00132568
		public void GroupBoxPaint(IDeviceContext dc, Rectangle bounds, Rectangle excludedArea, GtkPlusState state)
		{
			this.group_box_painter.Configure(state);
			this.PaintExcludingArea(GtkPlus.WidgetType.GroupBox, bounds, dc, excludedArea, this.group_box_painter);
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x00134394 File Offset: 0x00132594
		public void HeaderPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state)
		{
			this.header_painter.Configure(state);
			this.Paint(GtkPlus.WidgetType.TreeView, bounds, dc, clippingArea, this.header_painter);
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x001343C0 File Offset: 0x001325C0
		public void ProgressBarPaintBar(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.ProgressBar, bounds, dc, clippingArea, this.progress_bar_bar_painter);
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x001343D4 File Offset: 0x001325D4
		public void ProgressBarPaintChunk(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.ProgressBar, bounds, dc, clippingArea, this.progress_bar_chunk_painter);
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x001343E8 File Offset: 0x001325E8
		public Rectangle ProgressBarGetBackgroundContentRectagle(Rectangle bounds)
		{
			GtkPlus.GtkStyle gtkStyle = (GtkPlus.GtkStyle)Marshal.PtrToStructure(GtkPlus.gtk_widget_get_style(this.widgets[4]), typeof(GtkPlus.GtkStyle));
			bounds.Inflate(-gtkStyle.xthickness, -gtkStyle.ythickness);
			return bounds;
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x00134430 File Offset: 0x00132630
		public void RadioButtonPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, GtkPlusToggleButtonValue value)
		{
			this.radio_button_painter.Configure(state, value);
			this.Paint(GtkPlus.WidgetType.RadioButton, bounds, dc, clippingArea, this.radio_button_painter);
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x0013445C File Offset: 0x0013265C
		public Size RadioButtonGetSize()
		{
			return this.GetGtkCheckButtonIndicatorSize(GtkPlus.WidgetType.RadioButton);
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x00134468 File Offset: 0x00132668
		public void ScrollBarPaintArrowButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, bool horizontal, bool upOrLeft)
		{
			this.scroll_bar_arrow_button_painter.Configure(state, horizontal, upOrLeft);
			this.Paint((!horizontal) ? GtkPlus.WidgetType.VScrollBar : GtkPlus.WidgetType.HScrollBar, bounds, dc, clippingArea, this.scroll_bar_arrow_button_painter);
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x001344A4 File Offset: 0x001326A4
		public void ScrollBarPaintThumbButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, bool horizontal)
		{
			this.scroll_bar_thumb_button_painter.Configure(state, horizontal);
			this.Paint((!horizontal) ? GtkPlus.WidgetType.VScrollBar : GtkPlus.WidgetType.HScrollBar, bounds, dc, clippingArea, this.scroll_bar_thumb_button_painter);
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x001344E0 File Offset: 0x001326E0
		public void ScrollBarPaintTrack(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, bool horizontal, bool upOrLeft)
		{
			this.scroll_bar_track_painter.Configure(state, upOrLeft);
			this.Paint((!horizontal) ? GtkPlus.WidgetType.VScrollBar : GtkPlus.WidgetType.HScrollBar, bounds, dc, clippingArea, this.scroll_bar_track_painter);
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x0013451C File Offset: 0x0013271C
		public void StatusBarPaintGripper(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.StatusBar, bounds, dc, clippingArea, this.status_bar_gripper_painter);
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x00134530 File Offset: 0x00132730
		public void TabControlPaintPane(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.TabControl, bounds, dc, clippingArea, this.tab_control_pane_painter);
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x00134544 File Offset: 0x00132744
		public void TabControlPaintTabItem(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state)
		{
			this.tab_control_tab_item_painter.Configure(state);
			this.Paint(GtkPlus.WidgetType.TabControl, bounds, dc, clippingArea, this.tab_control_tab_item_painter);
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x00134570 File Offset: 0x00132770
		public void TextBoxPaint(IDeviceContext dc, Rectangle bounds, Rectangle excludedArea, GtkPlusState state)
		{
			this.text_box_painter.Configure(state);
			this.PaintExcludingArea(GtkPlus.WidgetType.TextBox, bounds, dc, excludedArea, this.text_box_painter);
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0013459C File Offset: 0x0013279C
		public void ToolBarPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.ToolBar, bounds, dc, clippingArea, this.tool_bar_painter);
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x001345B0 File Offset: 0x001327B0
		public void ToolBarPaintButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state)
		{
			this.tool_bar_button_painter.Configure(state);
			this.Paint(GtkPlus.WidgetType.Button, bounds, dc, clippingArea, this.tool_bar_button_painter);
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x001345DC File Offset: 0x001327DC
		public void ToolBarPaintCheckedButton(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea)
		{
			this.Paint(GtkPlus.WidgetType.Button, bounds, dc, clippingArea, this.tool_bar_checked_button_painter);
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x001345F0 File Offset: 0x001327F0
		public void TrackBarPaintTrack(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, bool horizontal)
		{
			this.Paint((!horizontal) ? GtkPlus.WidgetType.VerticalTrackBar : GtkPlus.WidgetType.HorizontalTrackBar, bounds, dc, clippingArea, this.track_bar_track_painter);
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x00134614 File Offset: 0x00132814
		public void TrackBarPaintThumb(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, GtkPlusState state, bool horizontal)
		{
			this.track_bar_thumb_painter.Configure(state, horizontal);
			this.Paint((!horizontal) ? GtkPlus.WidgetType.VerticalTrackBar : GtkPlus.WidgetType.HorizontalTrackBar, bounds, dc, clippingArea, this.track_bar_thumb_painter);
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x00134650 File Offset: 0x00132850
		public void TreeViewPaintGlyph(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, bool closed)
		{
			this.tree_view_glyph_painter.Configure(closed);
			this.Paint(GtkPlus.WidgetType.TreeView, bounds, dc, clippingArea, this.tree_view_glyph_painter);
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0013467C File Offset: 0x0013287C
		public void UpDownPaint(IDeviceContext dc, Rectangle bounds, Rectangle clippingArea, bool up, GtkPlusState state)
		{
			this.up_down_painter.Configure(up, state);
			this.Paint(GtkPlus.WidgetType.UpDown, bounds, dc, clippingArea, this.up_down_painter);
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x001346AC File Offset: 0x001328AC
		private void Paint(GtkPlus.WidgetType widgetType, Rectangle bounds, IDeviceContext dc, Rectangle clippingArea, GtkPlus.Painter painter)
		{
			this.Paint(widgetType, bounds, dc, GtkPlus.TransparencyType.Alpha, Color.Black, GtkPlus.DeviceContextType.Native, clippingArea, painter, Rectangle.Empty);
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x001346D4 File Offset: 0x001328D4
		private void PaintExcludingArea(GtkPlus.WidgetType widgetType, Rectangle bounds, IDeviceContext dc, Rectangle excludedArea, GtkPlus.Painter painter)
		{
			this.Paint(widgetType, bounds, dc, GtkPlus.TransparencyType.Alpha, Color.Black, GtkPlus.DeviceContextType.Native, bounds, painter, excludedArea);
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x001346F8 File Offset: 0x001328F8
		private unsafe void Paint(GtkPlus.WidgetType widgetType, Rectangle bounds, IDeviceContext dc, GtkPlus.TransparencyType transparencyType, Color background, GtkPlus.DeviceContextType deviceContextType, Rectangle clippingArea, GtkPlus.Painter painter, Rectangle excludedArea)
		{
			Rectangle rectangle = Rectangle.Intersect(bounds, clippingArea);
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				return;
			}
			rectangle.Offset(-bounds.X, -bounds.Y);
			excludedArea.Offset(-bounds.X, -bounds.Y);
			IntPtr intPtr = GtkPlus.gdk_pixmap_new(IntPtr.Zero, bounds.Width, bounds.Height, 24);
			painter.AttachStyle(widgetType, intPtr, this);
			IntPtr intPtr2 = GtkPlus.gdk_gc_new(intPtr);
			GtkPlus.GdkColor gdkColor = new GtkPlus.GdkColor(background);
			GtkPlus.gdk_gc_set_rgb_fg_color(intPtr2, ref gdkColor);
			IntPtr intPtr3;
			IntPtr intPtr4;
			int num;
			this.Paint(intPtr, intPtr2, bounds, widgetType, out intPtr3, out intPtr4, out num, rectangle, painter, excludedArea);
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			int num2 = 0;
			GtkPlus.GdkColor gdkColor2 = default(GtkPlus.GdkColor);
			if (transparencyType == GtkPlus.TransparencyType.Alpha)
			{
				gdkColor2.red = ushort.MaxValue;
				gdkColor2.green = ushort.MaxValue;
				gdkColor2.blue = ushort.MaxValue;
				GtkPlus.gdk_gc_set_rgb_fg_color(intPtr2, ref gdkColor2);
				this.Paint(intPtr, intPtr2, bounds, widgetType, out zero, out zero2, out num2, rectangle, painter, excludedArea);
			}
			GtkPlus.g_object_unref(intPtr2);
			byte* ptr = (byte*)(void*)intPtr4;
			byte* ptr2 = (byte*)(void*)zero2;
			for (int i = 0; i < rectangle.Height; i++)
			{
				byte* ptr3 = ptr;
				byte* ptr4 = ptr2;
				for (int j = 0; j < rectangle.Width; j++)
				{
					if (transparencyType != GtkPlus.TransparencyType.Color)
					{
						if (transparencyType == GtkPlus.TransparencyType.Alpha)
						{
							ptr3[3] = *ptr3 - *ptr4 + byte.MaxValue;
						}
					}
					else if (*ptr3 == background.R && ptr3[1] == background.G && ptr3[2] == background.B)
					{
						ptr3[3] = 0;
					}
					byte b = *ptr3;
					*ptr3 = ptr3[2];
					ptr3[2] = b;
					ptr3 += 4;
					ptr4 += 4;
				}
				ptr += num;
				ptr2 += num2;
			}
			if (transparencyType == GtkPlus.TransparencyType.Alpha)
			{
				GtkPlus.g_object_unref(zero);
			}
			GtkPlus.g_object_unref(intPtr);
			Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, num, 925707, intPtr4);
			bool flag = false;
			Graphics graphics;
			if (deviceContextType != GtkPlus.DeviceContextType.Graphics)
			{
				if (deviceContextType != GtkPlus.DeviceContextType.Native)
				{
					graphics = dc as Graphics;
					if (graphics == null)
					{
						flag = true;
						graphics = Graphics.FromHdc(dc.GetHdc());
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					graphics = Graphics.FromHdc(dc.GetHdc());
				}
			}
			else
			{
				graphics = (Graphics)dc;
			}
			rectangle.Offset(bounds.X, bounds.Y);
			graphics.DrawImage(bitmap, rectangle.Location);
			if (deviceContextType != GtkPlus.DeviceContextType.Graphics)
			{
				if (deviceContextType != GtkPlus.DeviceContextType.Native)
				{
					if (flag)
					{
						graphics.Dispose();
						dc.ReleaseHdc();
					}
				}
				else
				{
					graphics.Dispose();
					dc.ReleaseHdc();
				}
			}
			bitmap.Dispose();
			GtkPlus.g_object_unref(intPtr3);
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x00134A18 File Offset: 0x00132C18
		private void Paint(IntPtr drawable, IntPtr gc, Rectangle rectangle, GtkPlus.WidgetType widgetType, out IntPtr pixbuf, out IntPtr pixelData, out int rowstride, Rectangle clippingArea, GtkPlus.Painter painter, Rectangle excludedArea)
		{
			GtkPlus.gdk_draw_rectangle(drawable, gc, true, clippingArea.X, clippingArea.Y, clippingArea.Width, clippingArea.Height);
			painter.Paint(this.styles[(int)widgetType], drawable, new GtkPlus.GdkRectangle(clippingArea), this.widgets[(int)widgetType], 0, 0, rectangle.Width, rectangle.Height, this);
			if (excludedArea.Width != 0)
			{
				GtkPlus.gdk_draw_rectangle(drawable, gc, true, excludedArea.X, excludedArea.Y, excludedArea.Width, excludedArea.Height);
			}
			if ((pixbuf = GtkPlus.gdk_pixbuf_new(GtkPlus.GdkColorspace.GDK_COLORSPACE_RGB, true, 8, clippingArea.Width, clippingArea.Height)) == IntPtr.Zero || GtkPlus.gdk_pixbuf_get_from_drawable(pixbuf, drawable, IntPtr.Zero, clippingArea.X, clippingArea.Y, 0, 0, clippingArea.Width, clippingArea.Height) == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			pixelData = GtkPlus.gdk_pixbuf_get_pixels(pixbuf);
			rowstride = GtkPlus.gdk_pixbuf_get_rowstride(pixbuf);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x00134B30 File Offset: 0x00132D30
		private static GtkPlus.GtkShadowType GetWidgetStyleShadowType(IntPtr widget)
		{
			GtkPlus.GtkShadowType gtkShadowType;
			GtkPlus.gtk_widget_style_get(widget, "shadow-type", out gtkShadowType, IntPtr.Zero);
			return gtkShadowType;
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x00134B50 File Offset: 0x00132D50
		private static int GetWidgetStyleInteger(IntPtr widget, string propertyName)
		{
			int num;
			GtkPlus.gtk_widget_style_get(widget, propertyName, out num, IntPtr.Zero);
			return num;
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x00134B6C File Offset: 0x00132D6C
		private static float GetWidgetStyleSingle(IntPtr widget, string propertyName)
		{
			float num;
			GtkPlus.gtk_widget_style_get(widget, propertyName, out num, IntPtr.Zero);
			return num;
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x00134B88 File Offset: 0x00132D88
		private static bool GetWidgetStyleBoolean(IntPtr widget, string propertyName)
		{
			bool flag;
			GtkPlus.gtk_widget_style_get(widget, propertyName, out flag, IntPtr.Zero);
			return flag;
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x00134BA4 File Offset: 0x00132DA4
		private static IntPtr GetWidgetStyle(IntPtr widget)
		{
			return GtkPlus.gtk_rc_get_style(widget);
		}

		// Token: 0x06004CE7 RID: 19687
		[DllImport("libgdk-x11-2.0.so")]
		private static extern void gdk_draw_rectangle(IntPtr drawable, IntPtr gc, bool filled, int x, int y, int width, int height);

		// Token: 0x06004CE8 RID: 19688
		[DllImport("libgdk-x11-2.0.so")]
		private static extern IntPtr gdk_gc_new(IntPtr drawable);

		// Token: 0x06004CE9 RID: 19689
		[DllImport("libgdk-x11-2.0.so")]
		private static extern void gdk_gc_set_rgb_fg_color(IntPtr gc, ref GtkPlus.GdkColor color);

		// Token: 0x06004CEA RID: 19690
		[DllImport("libgdk-x11-2.0.so")]
		private static extern IntPtr gdk_pixbuf_get_from_drawable(IntPtr dest, IntPtr src, IntPtr cmap, int src_x, int src_y, int dest_x, int dest_y, int width, int height);

		// Token: 0x06004CEB RID: 19691
		[DllImport("libgdk-x11-2.0.so")]
		private static extern IntPtr gdk_pixmap_new(IntPtr drawable, int width, int height, int depth);

		// Token: 0x06004CEC RID: 19692
		[DllImport("libgdk_pixbuf-2.0.so")]
		private static extern IntPtr gdk_pixbuf_get_pixels(IntPtr pixbuf);

		// Token: 0x06004CED RID: 19693
		[DllImport("libgdk_pixbuf-2.0.so")]
		private static extern int gdk_pixbuf_get_rowstride(IntPtr pixbuf);

		// Token: 0x06004CEE RID: 19694
		[DllImport("libgdk_pixbuf-2.0.so")]
		private static extern IntPtr gdk_pixbuf_new(GtkPlus.GdkColorspace colorspace, bool has_alpha, int bits_per_sample, int width, int height);

		// Token: 0x06004CEF RID: 19695
		[DllImport("libgtk-x11-2.0.so")]
		private static extern bool gtk_init_check(ref int argc, ref string[] argv);

		// Token: 0x06004CF0 RID: 19696
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_check_version(uint required_major, uint required_minor, uint required_micro);

		// Token: 0x06004CF1 RID: 19697
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_container_add(IntPtr container, IntPtr widget);

		// Token: 0x06004CF2 RID: 19698
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_container_forall(IntPtr container, GtkPlus.GtkCallback callback, IntPtr callback_data);

		// Token: 0x06004CF3 RID: 19699
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_object_destroy(IntPtr @object);

		// Token: 0x06004CF4 RID: 19700
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_rc_get_style(IntPtr widget);

		// Token: 0x06004CF5 RID: 19701
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_style_attach(IntPtr style, IntPtr window);

		// Token: 0x06004CF6 RID: 19702
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_realize(IntPtr widget);

		// Token: 0x06004CF7 RID: 19703
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property, out int value, IntPtr nullTerminator);

		// Token: 0x06004CF8 RID: 19704
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property, out float value, IntPtr nullTerminator);

		// Token: 0x06004CF9 RID: 19705
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property1, out int value1, string property2, out int value2, IntPtr nullTerminator);

		// Token: 0x06004CFA RID: 19706
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property, out GtkPlus.GtkShadowType value, IntPtr nullTerminator);

		// Token: 0x06004CFB RID: 19707
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_widget_style_get(IntPtr widget, string property, out bool value, IntPtr nullTerminator);

		// Token: 0x06004CFC RID: 19708
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_window_new(GtkPlus.GtkWindowType type);

		// Token: 0x06004CFD RID: 19709
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_window_set_default(IntPtr window, IntPtr default_widget);

		// Token: 0x06004CFE RID: 19710
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_adjustment_new(double value, double lower, double upper, double step_increment, double page_increment, double page_size);

		// Token: 0x06004CFF RID: 19711
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_widget_get_style(IntPtr widget);

		// Token: 0x06004D00 RID: 19712
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_tree_view_column_new();

		// Token: 0x06004D01 RID: 19713
		[DllImport("libgtk-x11-2.0.so")]
		private static extern int gtk_tree_view_insert_column(IntPtr tree_view, IntPtr column, int position);

		// Token: 0x06004D02 RID: 19714
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_toolbar_insert(IntPtr toolbar, IntPtr item, int pos);

		// Token: 0x06004D03 RID: 19715
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_bin_get_child(IntPtr bin);

		// Token: 0x06004D04 RID: 19716
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_arrow_get_type();

		// Token: 0x06004D05 RID: 19717
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_container_get_type();

		// Token: 0x06004D06 RID: 19718
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_toggle_button_get_type();

		// Token: 0x06004D07 RID: 19719
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_button_new();

		// Token: 0x06004D08 RID: 19720
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_check_button_new();

		// Token: 0x06004D09 RID: 19721
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_combo_box_entry_new();

		// Token: 0x06004D0A RID: 19722
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_entry_new();

		// Token: 0x06004D0B RID: 19723
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_fixed_new();

		// Token: 0x06004D0C RID: 19724
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_frame_new(string label);

		// Token: 0x06004D0D RID: 19725
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_hscale_new_with_range(double min, double max, double step);

		// Token: 0x06004D0E RID: 19726
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_hscrollbar_new(IntPtr adjustment);

		// Token: 0x06004D0F RID: 19727
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_notebook_new();

		// Token: 0x06004D10 RID: 19728
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_progress_bar_new();

		// Token: 0x06004D11 RID: 19729
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_radio_button_new(IntPtr group);

		// Token: 0x06004D12 RID: 19730
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_spin_button_new(IntPtr adjustment, double climb_rate, uint digits);

		// Token: 0x06004D13 RID: 19731
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_statusbar_new();

		// Token: 0x06004D14 RID: 19732
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_toggle_tool_button_new();

		// Token: 0x06004D15 RID: 19733
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_toolbar_new();

		// Token: 0x06004D16 RID: 19734
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_tool_button_new(IntPtr icon_widget, string label);

		// Token: 0x06004D17 RID: 19735
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_tree_view_new();

		// Token: 0x06004D18 RID: 19736
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_vscale_new_with_range(double min, double max, double step);

		// Token: 0x06004D19 RID: 19737
		[DllImport("libgtk-x11-2.0.so")]
		private static extern IntPtr gtk_vscrollbar_new(IntPtr adjustment);

		// Token: 0x06004D1A RID: 19738
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_arrow(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, GtkPlus.GtkArrowType arrow_type, bool fill, int x, int y, int width, int height);

		// Token: 0x06004D1B RID: 19739
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_box(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06004D1C RID: 19740
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_box_gap(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height, GtkPlus.GtkPositionType gap_side, int gap_x, int gap_width);

		// Token: 0x06004D1D RID: 19741
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_check(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06004D1E RID: 19742
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_expander(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, GtkPlus.GtkExpanderStyle expander_style);

		// Token: 0x06004D1F RID: 19743
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_extension(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height, GtkPlus.GtkPositionType gap_side);

		// Token: 0x06004D20 RID: 19744
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_flat_box(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06004D21 RID: 19745
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_option(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06004D22 RID: 19746
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_resize_grip(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, GtkPlus.GdkWindowEdge edge, int x, int y, int width, int height);

		// Token: 0x06004D23 RID: 19747
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_shadow(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x06004D24 RID: 19748
		[DllImport("libgtk-x11-2.0.so")]
		private static extern void gtk_paint_slider(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height, GtkPlus.GtkOrientation orientation);

		// Token: 0x06004D25 RID: 19749 RVA: 0x00134BAC File Offset: 0x00132DAC
		private static void GTK_WIDGET_SET_FLAGS(IntPtr wid, GtkPlus.GtkWidgetFlags flag)
		{
			GtkPlus.GtkObject gtkObject = (GtkPlus.GtkObject)Marshal.PtrToStructure(wid, typeof(GtkPlus.GtkObject));
			gtkObject.flags |= (uint)flag;
			Marshal.StructureToPtr(gtkObject, wid, false);
		}

		// Token: 0x06004D26 RID: 19750
		[DllImport("libgobject-2.0.so")]
		private static extern IntPtr g_object_ref(IntPtr @object);

		// Token: 0x06004D27 RID: 19751
		[DllImport("libgobject-2.0.so")]
		private static extern void g_object_unref(IntPtr @object);

		// Token: 0x06004D28 RID: 19752
		[DllImport("libgobject-2.0.so")]
		private static extern bool g_type_check_instance_is_a(IntPtr type_instance, IntPtr iface_type);

		// Token: 0x06004D29 RID: 19753
		[DllImport("libgobject-2.0.so")]
		private static extern void g_object_get(IntPtr @object, string property_name, out bool value, IntPtr nullTerminator);

		// Token: 0x04002A84 RID: 10884
		private const GtkPlus.WidgetType WidgetTypeNotNeeded = GtkPlus.WidgetType.Button;

		// Token: 0x04002A85 RID: 10885
		private const string GobjectLibraryName = "libgobject-2.0.so";

		// Token: 0x04002A86 RID: 10886
		private const string GdkLibraryName = "libgdk-x11-2.0.so";

		// Token: 0x04002A87 RID: 10887
		private const string GdkPixbufLibraryName = "libgdk_pixbuf-2.0.so";

		// Token: 0x04002A88 RID: 10888
		private const string GtkLibraryName = "libgtk-x11-2.0.so";

		// Token: 0x04002A89 RID: 10889
		private const int G_TYPE_FUNDAMENTAL_SHIFT = 2;

		// Token: 0x04002A8A RID: 10890
		private static GtkPlus instance;

		// Token: 0x04002A8B RID: 10891
		private readonly int WidgetTypeCount = Enum.GetNames(typeof(GtkPlus.WidgetType)).Length;

		// Token: 0x04002A8C RID: 10892
		private readonly IntPtr[] widgets;

		// Token: 0x04002A8D RID: 10893
		private readonly IntPtr window;

		// Token: 0x04002A8E RID: 10894
		private readonly IntPtr @fixed;

		// Token: 0x04002A8F RID: 10895
		private readonly IntPtr[] styles;

		// Token: 0x04002A90 RID: 10896
		private readonly IntPtr combo_box_drop_down_toggle_button;

		// Token: 0x04002A91 RID: 10897
		private readonly IntPtr combo_box_drop_down_arrow;

		// Token: 0x04002A92 RID: 10898
		private IntPtr combo_box_drop_down_toggle_button_style;

		// Token: 0x04002A93 RID: 10899
		private IntPtr combo_box_drop_down_arrow_style;

		// Token: 0x04002A94 RID: 10900
		private readonly IntPtr tool_bar_button;

		// Token: 0x04002A95 RID: 10901
		private readonly IntPtr tool_bar_toggle_button;

		// Token: 0x04002A96 RID: 10902
		private IntPtr tool_bar_button_style;

		// Token: 0x04002A97 RID: 10903
		private IntPtr tool_bar_toggle_button_style;

		// Token: 0x04002A98 RID: 10904
		private readonly IntPtr tree_view_column;

		// Token: 0x04002A99 RID: 10905
		private readonly IntPtr tree_view_column_button;

		// Token: 0x04002A9A RID: 10906
		private IntPtr tree_view_column_button_style;

		// Token: 0x04002A9B RID: 10907
		private readonly GtkPlus.ButtonPainter button_painter = new GtkPlus.ButtonPainter();

		// Token: 0x04002A9C RID: 10908
		private readonly GtkPlus.CheckBoxPainter check_box_painter = new GtkPlus.CheckBoxPainter();

		// Token: 0x04002A9D RID: 10909
		private readonly GtkPlus.RadioButtonPainter radio_button_painter = new GtkPlus.RadioButtonPainter();

		// Token: 0x04002A9E RID: 10910
		private readonly GtkPlus.ComboBoxDropDownButtonPainter combo_box_drop_down_button_painter = new GtkPlus.ComboBoxDropDownButtonPainter();

		// Token: 0x04002A9F RID: 10911
		private readonly GtkPlus.ComboBoxBorderPainter combo_box_border_painter = new GtkPlus.ComboBoxBorderPainter();

		// Token: 0x04002AA0 RID: 10912
		private readonly GtkPlus.GroupBoxPainter group_box_painter = new GtkPlus.GroupBoxPainter();

		// Token: 0x04002AA1 RID: 10913
		private readonly GtkPlus.HeaderPainter header_painter = new GtkPlus.HeaderPainter();

		// Token: 0x04002AA2 RID: 10914
		private readonly GtkPlus.ProgressBarBarPainter progress_bar_bar_painter = new GtkPlus.ProgressBarBarPainter();

		// Token: 0x04002AA3 RID: 10915
		private readonly GtkPlus.ProgressBarChunkPainter progress_bar_chunk_painter = new GtkPlus.ProgressBarChunkPainter();

		// Token: 0x04002AA4 RID: 10916
		private readonly GtkPlus.ScrollBarArrowButtonPainter scroll_bar_arrow_button_painter = new GtkPlus.ScrollBarArrowButtonPainter();

		// Token: 0x04002AA5 RID: 10917
		private readonly GtkPlus.ScrollBarThumbButtonPainter scroll_bar_thumb_button_painter = new GtkPlus.ScrollBarThumbButtonPainter();

		// Token: 0x04002AA6 RID: 10918
		private readonly GtkPlus.ScrollBarTrackPainter scroll_bar_track_painter = new GtkPlus.ScrollBarTrackPainter();

		// Token: 0x04002AA7 RID: 10919
		private readonly GtkPlus.StatusBarGripperPainter status_bar_gripper_painter = new GtkPlus.StatusBarGripperPainter();

		// Token: 0x04002AA8 RID: 10920
		private readonly GtkPlus.TabControlPanePainter tab_control_pane_painter = new GtkPlus.TabControlPanePainter();

		// Token: 0x04002AA9 RID: 10921
		private readonly GtkPlus.TabControlTabItemPainter tab_control_tab_item_painter = new GtkPlus.TabControlTabItemPainter();

		// Token: 0x04002AAA RID: 10922
		private readonly GtkPlus.TextBoxPainter text_box_painter = new GtkPlus.TextBoxPainter();

		// Token: 0x04002AAB RID: 10923
		private readonly GtkPlus.ToolBarPainter tool_bar_painter = new GtkPlus.ToolBarPainter();

		// Token: 0x04002AAC RID: 10924
		private readonly GtkPlus.ToolBarButtonPainter tool_bar_button_painter = new GtkPlus.ToolBarButtonPainter();

		// Token: 0x04002AAD RID: 10925
		private readonly GtkPlus.ToolBarCheckedButtonPainter tool_bar_checked_button_painter = new GtkPlus.ToolBarCheckedButtonPainter();

		// Token: 0x04002AAE RID: 10926
		private readonly GtkPlus.TrackBarTrackPainter track_bar_track_painter = new GtkPlus.TrackBarTrackPainter();

		// Token: 0x04002AAF RID: 10927
		private readonly GtkPlus.TrackBarThumbPainter track_bar_thumb_painter = new GtkPlus.TrackBarThumbPainter();

		// Token: 0x04002AB0 RID: 10928
		private readonly GtkPlus.TreeViewGlyphPainter tree_view_glyph_painter = new GtkPlus.TreeViewGlyphPainter();

		// Token: 0x04002AB1 RID: 10929
		private readonly GtkPlus.UpDownPainter up_down_painter = new GtkPlus.UpDownPainter();

		// Token: 0x020004E3 RID: 1251
		private abstract class Painter
		{
			// Token: 0x06004D2B RID: 19755 RVA: 0x00134BF4 File Offset: 0x00132DF4
			public virtual void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.styles[(int)widgetType] = GtkPlus.gtk_style_attach(gtkPlus.styles[(int)widgetType], drawable);
			}

			// Token: 0x06004D2C RID: 19756
			public abstract void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus);
		}

		// Token: 0x020004E4 RID: 1252
		private enum TransparencyType
		{
			// Token: 0x04002AB3 RID: 10931
			None,
			// Token: 0x04002AB4 RID: 10932
			Color,
			// Token: 0x04002AB5 RID: 10933
			Alpha
		}

		// Token: 0x020004E5 RID: 1253
		private enum DeviceContextType
		{
			// Token: 0x04002AB7 RID: 10935
			Unknown,
			// Token: 0x04002AB8 RID: 10936
			Graphics,
			// Token: 0x04002AB9 RID: 10937
			Native
		}

		// Token: 0x020004E6 RID: 1254
		private class ButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06004D2E RID: 19758 RVA: 0x00134C20 File Offset: 0x00132E20
			public void Configure(bool @default, GtkPlusState state)
			{
				this.@default = @default;
				this.state = state;
			}

			// Token: 0x06004D2F RID: 19759 RVA: 0x00134C30 File Offset: 0x00132E30
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				if (this.@default)
				{
					GtkPlus.gtk_window_set_default(gtkPlus.window, widget);
					GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "buttondefault", x, y, width, height);
					GtkPlus.gtk_window_set_default(gtkPlus.window, IntPtr.Zero);
				}
				else
				{
					GtkPlus.gtk_paint_box(style, window, (GtkPlus.GtkStateType)this.state, (this.state != GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_OUT : GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "button", x, y, width, height);
				}
			}

			// Token: 0x04002ABA RID: 10938
			private bool @default;

			// Token: 0x04002ABB RID: 10939
			private GtkPlusState state;
		}

		// Token: 0x020004E7 RID: 1255
		private abstract class ToggleButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06004D31 RID: 19761 RVA: 0x00134CC0 File Offset: 0x00132EC0
			public void Configure(GtkPlusState state, GtkPlusToggleButtonValue value)
			{
				this.state = state;
				this.value = value;
			}

			// Token: 0x06004D32 RID: 19762 RVA: 0x00134CD0 File Offset: 0x00132ED0
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				this.PaintFunction(style, window, (GtkPlus.GtkStateType)this.state, (GtkPlus.GtkShadowType)this.value, ref area, widget, this.Detail, x, y, width, height);
			}

			// Token: 0x1700134A RID: 4938
			// (get) Token: 0x06004D33 RID: 19763
			protected abstract string Detail { get; }

			// Token: 0x1700134B RID: 4939
			// (get) Token: 0x06004D34 RID: 19764
			protected abstract GtkPlus.ToggleButtonPaintFunction PaintFunction { get; }

			// Token: 0x04002ABC RID: 10940
			private GtkPlusState state;

			// Token: 0x04002ABD RID: 10941
			private GtkPlusToggleButtonValue value;
		}

		// Token: 0x020004E8 RID: 1256
		private class CheckBoxPainter : GtkPlus.ToggleButtonPainter
		{
			// Token: 0x1700134C RID: 4940
			// (get) Token: 0x06004D36 RID: 19766 RVA: 0x00134D10 File Offset: 0x00132F10
			protected override string Detail
			{
				get
				{
					return "checkbutton";
				}
			}

			// Token: 0x1700134D RID: 4941
			// (get) Token: 0x06004D37 RID: 19767 RVA: 0x00134D18 File Offset: 0x00132F18
			protected override GtkPlus.ToggleButtonPaintFunction PaintFunction
			{
				get
				{
					return new GtkPlus.ToggleButtonPaintFunction(GtkPlus.gtk_paint_check);
				}
			}
		}

		// Token: 0x020004E9 RID: 1257
		private class RadioButtonPainter : GtkPlus.ToggleButtonPainter
		{
			// Token: 0x1700134E RID: 4942
			// (get) Token: 0x06004D39 RID: 19769 RVA: 0x00134D30 File Offset: 0x00132F30
			protected override string Detail
			{
				get
				{
					return "radiobutton";
				}
			}

			// Token: 0x1700134F RID: 4943
			// (get) Token: 0x06004D3A RID: 19770 RVA: 0x00134D38 File Offset: 0x00132F38
			protected override GtkPlus.ToggleButtonPaintFunction PaintFunction
			{
				get
				{
					return new GtkPlus.ToggleButtonPaintFunction(GtkPlus.gtk_paint_option);
				}
			}
		}

		// Token: 0x020004EA RID: 1258
		private class ComboBoxDropDownButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06004D3C RID: 19772 RVA: 0x00134D50 File Offset: 0x00132F50
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06004D3D RID: 19773 RVA: 0x00134D5C File Offset: 0x00132F5C
			public override void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.combo_box_drop_down_toggle_button_style = GtkPlus.gtk_style_attach(gtkPlus.combo_box_drop_down_toggle_button_style, drawable);
				gtkPlus.combo_box_drop_down_arrow_style = GtkPlus.gtk_style_attach(gtkPlus.combo_box_drop_down_arrow_style, drawable);
			}

			// Token: 0x06004D3E RID: 19774 RVA: 0x00134D90 File Offset: 0x00132F90
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.GtkShadowType gtkShadowType;
				switch (this.state)
				{
				case GtkPlusState.Pressed:
					gtkShadowType = GtkPlus.GtkShadowType.GTK_SHADOW_IN;
					goto IL_0039;
				case GtkPlusState.Disabled:
					gtkShadowType = GtkPlus.GtkShadowType.GTK_SHADOW_ETCHED_IN;
					goto IL_0039;
				}
				gtkShadowType = GtkPlus.GtkShadowType.GTK_SHADOW_OUT;
				IL_0039:
				GtkPlus.gtk_paint_box(gtkPlus.combo_box_drop_down_toggle_button_style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, gtkPlus.combo_box_drop_down_toggle_button, "button", x, y, width, height);
				GtkPlus.GtkMisc gtkMisc = (GtkPlus.GtkMisc)Marshal.PtrToStructure(gtkPlus.combo_box_drop_down_arrow, typeof(GtkPlus.GtkMisc));
				int num = (int)((float)Math.Min(width - (int)(gtkMisc.xpad * 2), height - (int)(gtkMisc.ypad * 2)) * GtkPlus.GetWidgetStyleSingle(gtkPlus.combo_box_drop_down_arrow, "arrow-scaling"));
				GtkPlus.gtk_paint_arrow(gtkPlus.combo_box_drop_down_arrow_style, window, (GtkPlus.GtkStateType)this.state, GtkPlus.GtkShadowType.GTK_SHADOW_NONE, ref area, gtkPlus.combo_box_drop_down_arrow, "arrow", GtkPlus.GtkArrowType.GTK_ARROW_DOWN, true, (int)Math.Floor((double)((float)(x + (int)gtkMisc.xpad) + (float)(width - num) * gtkMisc.xalign)), (int)Math.Floor((double)((float)(y + (int)gtkMisc.ypad) + (float)(height - num) * gtkMisc.yalign)), num, num);
			}

			// Token: 0x04002ABE RID: 10942
			private GtkPlusState state;
		}

		// Token: 0x020004EB RID: 1259
		private class ComboBoxBorderPainter : GtkPlus.Painter
		{
			// Token: 0x06004D40 RID: 19776 RVA: 0x00134EBC File Offset: 0x001330BC
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_shadow(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "combobox", x, y, width, height);
			}
		}

		// Token: 0x020004EC RID: 1260
		private class GroupBoxPainter : GtkPlus.Painter
		{
			// Token: 0x06004D42 RID: 19778 RVA: 0x00134EEC File Offset: 0x001330EC
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06004D43 RID: 19779 RVA: 0x00134EF8 File Offset: 0x001330F8
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_shadow(style, window, (GtkPlus.GtkStateType)this.state, GtkPlus.GtkShadowType.GTK_SHADOW_ETCHED_IN, ref area, widget, "frame", x, y, width, height);
			}

			// Token: 0x04002ABF RID: 10943
			private GtkPlusState state;
		}

		// Token: 0x020004ED RID: 1261
		private class HeaderPainter : GtkPlus.Painter
		{
			// Token: 0x06004D45 RID: 19781 RVA: 0x00134F2C File Offset: 0x0013312C
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06004D46 RID: 19782 RVA: 0x00134F38 File Offset: 0x00133138
			public override void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.tree_view_column_button_style = GtkPlus.gtk_style_attach(gtkPlus.tree_view_column_button_style, drawable);
			}

			// Token: 0x06004D47 RID: 19783 RVA: 0x00134F4C File Offset: 0x0013314C
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(gtkPlus.tree_view_column_button_style, window, (GtkPlus.GtkStateType)this.state, (this.state != GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_OUT : GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, gtkPlus.tree_view_column_button, "button", x, y, width, height);
			}

			// Token: 0x04002AC0 RID: 10944
			private GtkPlusState state;
		}

		// Token: 0x020004EE RID: 1262
		private class ProgressBarBarPainter : GtkPlus.Painter
		{
			// Token: 0x06004D49 RID: 19785 RVA: 0x00134FA0 File Offset: 0x001331A0
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "trough", x, y, width, height);
			}
		}

		// Token: 0x020004EF RID: 1263
		private class ProgressBarChunkPainter : GtkPlus.Painter
		{
			// Token: 0x06004D4B RID: 19787 RVA: 0x00134FD0 File Offset: 0x001331D0
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_PRELIGHT, GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, widget, "bar", x, y, width, height);
			}
		}

		// Token: 0x020004F0 RID: 1264
		private class ScrollBarArrowButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06004D4D RID: 19789 RVA: 0x00135000 File Offset: 0x00133200
			public void Configure(GtkPlusState state, bool horizontal, bool upOrLeft)
			{
				this.state = state;
				this.horizontal = horizontal;
				this.up_or_left = upOrLeft;
			}

			// Token: 0x06004D4E RID: 19790 RVA: 0x00135018 File Offset: 0x00133218
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				bool flag;
				GtkPlus.g_object_get(widget, "can-focus", out flag, IntPtr.Zero);
				if (flag)
				{
					int num;
					int num2;
					GtkPlus.gtk_widget_style_get(widget, "focus-line-width", out num, "focus-padding", out num2, IntPtr.Zero);
					int num3 = num + num2;
					if (this.horizontal)
					{
						y -= num3;
						height -= 2 * num3;
					}
					else
					{
						x -= num3;
						width -= 2 * num3;
					}
				}
				GtkPlus.GtkShadowType gtkShadowType = ((this.state != GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_OUT : GtkPlus.GtkShadowType.GTK_SHADOW_IN);
				string text = ((!this.horizontal) ? "vscrollbar" : "hscrollbar");
				GtkPlus.gtk_paint_box(style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, widget, text, x, y, width, height);
				width /= 2;
				height /= 2;
				x += width / 2;
				y += height / 2;
				if (this.state == GtkPlusState.Pressed)
				{
					int num4;
					int num5;
					GtkPlus.gtk_widget_style_get(widget, "arrow-displacement-x", out num4, "arrow-displacement-y", out num5, IntPtr.Zero);
					x += num4;
					y += num5;
				}
				GtkPlus.gtk_paint_arrow(style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, widget, text, (!this.horizontal) ? ((!this.up_or_left) ? GtkPlus.GtkArrowType.GTK_ARROW_DOWN : GtkPlus.GtkArrowType.GTK_ARROW_UP) : ((!this.up_or_left) ? GtkPlus.GtkArrowType.GTK_ARROW_RIGHT : GtkPlus.GtkArrowType.GTK_ARROW_LEFT), true, x, y, width, height);
			}

			// Token: 0x04002AC1 RID: 10945
			private GtkPlusState state;

			// Token: 0x04002AC2 RID: 10946
			private bool horizontal;

			// Token: 0x04002AC3 RID: 10947
			private bool up_or_left;
		}

		// Token: 0x020004F1 RID: 1265
		private abstract class RangeThumbButtonPainter : GtkPlus.Painter
		{
			// Token: 0x17001350 RID: 4944
			// (get) Token: 0x06004D50 RID: 19792 RVA: 0x00135184 File Offset: 0x00133384
			protected bool Horizontal
			{
				get
				{
					return this.horizontal;
				}
			}

			// Token: 0x06004D51 RID: 19793 RVA: 0x0013518C File Offset: 0x0013338C
			public void Configure(GtkPlusState state, bool horizontal)
			{
				this.state = state;
				this.horizontal = horizontal;
			}

			// Token: 0x06004D52 RID: 19794 RVA: 0x0013519C File Offset: 0x0013339C
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_slider(style, window, (GtkPlus.GtkStateType)this.state, (this.state != GtkPlusState.Pressed || !GtkPlus.GetWidgetStyleBoolean(widget, "activate-slider")) ? GtkPlus.GtkShadowType.GTK_SHADOW_OUT : GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, this.Detail, x, y, width, height, (!this.horizontal) ? GtkPlus.GtkOrientation.GTK_ORIENTATION_VERTICAL : GtkPlus.GtkOrientation.GTK_ORIENTATION_HORIZONTAL);
			}

			// Token: 0x17001351 RID: 4945
			// (get) Token: 0x06004D53 RID: 19795
			protected abstract string Detail { get; }

			// Token: 0x04002AC4 RID: 10948
			private GtkPlusState state;

			// Token: 0x04002AC5 RID: 10949
			private bool horizontal;
		}

		// Token: 0x020004F2 RID: 1266
		private class ScrollBarThumbButtonPainter : GtkPlus.RangeThumbButtonPainter
		{
			// Token: 0x17001352 RID: 4946
			// (get) Token: 0x06004D55 RID: 19797 RVA: 0x00135208 File Offset: 0x00133408
			protected override string Detail
			{
				get
				{
					return "slider";
				}
			}
		}

		// Token: 0x020004F3 RID: 1267
		private class ScrollBarTrackPainter : GtkPlus.Painter
		{
			// Token: 0x06004D57 RID: 19799 RVA: 0x00135218 File Offset: 0x00133418
			public void Configure(GtkPlusState state, bool upOrLeft)
			{
				this.state = state;
				this.up_or_left = upOrLeft;
			}

			// Token: 0x06004D58 RID: 19800 RVA: 0x00135228 File Offset: 0x00133428
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, (this.state != GtkPlusState.Pressed) ? GtkPlus.GtkStateType.GTK_STATE_INSENSITIVE : GtkPlus.GtkStateType.GTK_STATE_ACTIVE, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, (!GtkPlus.GetWidgetStyleBoolean(widget, "trough-side-details")) ? "trough" : ((!this.up_or_left) ? "trough-lower" : "trough-upper"), x, y, width, height);
			}

			// Token: 0x04002AC6 RID: 10950
			private GtkPlusState state;

			// Token: 0x04002AC7 RID: 10951
			private bool up_or_left;
		}

		// Token: 0x020004F4 RID: 1268
		private class StatusBarGripperPainter : GtkPlus.Painter
		{
			// Token: 0x06004D5A RID: 19802 RVA: 0x0013529C File Offset: 0x0013349C
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_resize_grip(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, ref area, widget, "statusbar", GtkPlus.GdkWindowEdge.GDK_WINDOW_EDGE_SOUTH_EAST, x, y, width, height);
			}
		}

		// Token: 0x020004F5 RID: 1269
		private class TabControlPanePainter : GtkPlus.Painter
		{
			// Token: 0x06004D5C RID: 19804 RVA: 0x001352CC File Offset: 0x001334CC
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box_gap(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, widget, "notebook", x, y, width, height, GtkPlus.GtkPositionType.GTK_POS_TOP, 0, 0);
			}
		}

		// Token: 0x020004F6 RID: 1270
		private class TabControlTabItemPainter : GtkPlus.Painter
		{
			// Token: 0x06004D5E RID: 19806 RVA: 0x00135300 File Offset: 0x00133500
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06004D5F RID: 19807 RVA: 0x0013530C File Offset: 0x0013350C
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_extension(style, window, (GtkPlus.GtkStateType)this.state, GtkPlus.GtkShadowType.GTK_SHADOW_OUT, ref area, widget, "tab", x, y, width, height, GtkPlus.GtkPositionType.GTK_POS_BOTTOM);
			}

			// Token: 0x04002AC8 RID: 10952
			private GtkPlusState state;
		}

		// Token: 0x020004F7 RID: 1271
		private class TextBoxPainter : GtkPlus.Painter
		{
			// Token: 0x06004D61 RID: 19809 RVA: 0x00135344 File Offset: 0x00133544
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06004D62 RID: 19810 RVA: 0x00135350 File Offset: 0x00133550
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_shadow(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "entry", x, y, width, height);
				GtkPlus.GtkStyle gtkStyle = (GtkPlus.GtkStyle)Marshal.PtrToStructure(style, typeof(GtkPlus.GtkStyle));
				x += gtkStyle.xthickness;
				y += gtkStyle.ythickness;
				width -= 2 * gtkStyle.xthickness;
				height -= 2 * gtkStyle.ythickness;
				GtkPlus.gtk_paint_flat_box(style, window, (GtkPlus.GtkStateType)this.state, GtkPlus.GtkShadowType.GTK_SHADOW_NONE, ref area, widget, "entry_bg", x, y, width, height);
			}

			// Token: 0x04002AC9 RID: 10953
			private GtkPlusState state;
		}

		// Token: 0x020004F8 RID: 1272
		private class ToolBarPainter : GtkPlus.Painter
		{
			// Token: 0x06004D64 RID: 19812 RVA: 0x001353E8 File Offset: 0x001335E8
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, GtkPlus.GetWidgetStyleShadowType(widget), ref area, widget, "toolbar", x, y, width, height);
			}
		}

		// Token: 0x020004F9 RID: 1273
		private class ToolBarButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06004D66 RID: 19814 RVA: 0x00135420 File Offset: 0x00133620
			public void Configure(GtkPlusState state)
			{
				this.state = state;
			}

			// Token: 0x06004D67 RID: 19815 RVA: 0x0013542C File Offset: 0x0013362C
			public override void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.tool_bar_button_style = GtkPlus.gtk_style_attach(gtkPlus.tool_bar_button_style, drawable);
			}

			// Token: 0x06004D68 RID: 19816 RVA: 0x00135440 File Offset: 0x00133640
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(gtkPlus.tool_bar_button_style, window, (GtkPlus.GtkStateType)this.state, (this.state != GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_OUT : GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, gtkPlus.tool_bar_button, "button", x, y, width, height);
			}

			// Token: 0x04002ACA RID: 10954
			private GtkPlusState state;
		}

		// Token: 0x020004FA RID: 1274
		private class ToolBarCheckedButtonPainter : GtkPlus.Painter
		{
			// Token: 0x06004D6A RID: 19818 RVA: 0x00135494 File Offset: 0x00133694
			public override void AttachStyle(GtkPlus.WidgetType widgetType, IntPtr drawable, GtkPlus gtkPlus)
			{
				gtkPlus.tool_bar_toggle_button_style = GtkPlus.gtk_style_attach(gtkPlus.tool_bar_toggle_button_style, drawable);
			}

			// Token: 0x06004D6B RID: 19819 RVA: 0x001354A8 File Offset: 0x001336A8
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(gtkPlus.tool_bar_toggle_button_style, window, GtkPlus.GtkStateType.GTK_STATE_ACTIVE, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, gtkPlus.tool_bar_toggle_button, "button", x, y, width, height);
			}
		}

		// Token: 0x020004FB RID: 1275
		private class TrackBarTrackPainter : GtkPlus.Painter
		{
			// Token: 0x06004D6D RID: 19821 RVA: 0x001354E4 File Offset: 0x001336E4
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_ACTIVE, GtkPlus.GtkShadowType.GTK_SHADOW_IN, ref area, widget, "trough", x, y, width, height);
			}
		}

		// Token: 0x020004FC RID: 1276
		private class TrackBarThumbPainter : GtkPlus.RangeThumbButtonPainter
		{
			// Token: 0x17001353 RID: 4947
			// (get) Token: 0x06004D6F RID: 19823 RVA: 0x00135514 File Offset: 0x00133714
			protected override string Detail
			{
				get
				{
					return (!base.Horizontal) ? "vscale" : "hscale";
				}
			}
		}

		// Token: 0x020004FD RID: 1277
		private class TreeViewGlyphPainter : GtkPlus.Painter
		{
			// Token: 0x06004D71 RID: 19825 RVA: 0x00135538 File Offset: 0x00133738
			public void Configure(bool closed)
			{
				this.closed = closed;
			}

			// Token: 0x06004D72 RID: 19826 RVA: 0x00135544 File Offset: 0x00133744
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.gtk_paint_expander(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, ref area, widget, "treeview", x + width / 2, y + height / 2, (!this.closed) ? GtkPlus.GtkExpanderStyle.GTK_EXPANDER_EXPANDED : GtkPlus.GtkExpanderStyle.GTK_EXPANDER_COLLAPSED);
			}

			// Token: 0x04002ACB RID: 10955
			private bool closed;
		}

		// Token: 0x020004FE RID: 1278
		private class UpDownPainter : GtkPlus.Painter
		{
			// Token: 0x06004D74 RID: 19828 RVA: 0x0013558C File Offset: 0x0013378C
			public void Configure(bool up, GtkPlusState state)
			{
				this.up = up;
				this.state = state;
			}

			// Token: 0x06004D75 RID: 19829 RVA: 0x0013559C File Offset: 0x0013379C
			public override void Paint(IntPtr style, IntPtr window, GtkPlus.GdkRectangle area, IntPtr widget, int x, int y, int width, int height, GtkPlus gtkPlus)
			{
				GtkPlus.GtkShadowType gtkShadowType = GtkPlus.GetWidgetStyleShadowType(widget);
				if (gtkShadowType != GtkPlus.GtkShadowType.GTK_SHADOW_NONE)
				{
					GtkPlus.gtk_paint_box(style, window, GtkPlus.GtkStateType.GTK_STATE_NORMAL, gtkShadowType, ref area, widget, "spinbutton", x, y - ((!this.up) ? height : 0), width, height * 2);
				}
				gtkShadowType = ((this.state != GtkPlusState.Pressed) ? GtkPlus.GtkShadowType.GTK_SHADOW_OUT : GtkPlus.GtkShadowType.GTK_SHADOW_IN);
				GtkPlus.gtk_paint_box(style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, widget, (!this.up) ? "spinbutton_down" : "spinbutton_up", x, y, width, height);
				if (this.up)
				{
					y += 2;
				}
				height -= 2;
				width -= 3;
				x++;
				int num = width / 2;
				num -= num % 2 - 1;
				int num2 = (num + 1) / 2;
				x += (width - num) / 2;
				y += (height - num2) / 2;
				height = num2;
				width = num;
				GtkPlus.gtk_paint_arrow(style, window, (GtkPlus.GtkStateType)this.state, gtkShadowType, ref area, widget, "spinbutton", (!this.up) ? GtkPlus.GtkArrowType.GTK_ARROW_DOWN : GtkPlus.GtkArrowType.GTK_ARROW_UP, true, x, y, width, height);
			}

			// Token: 0x04002ACC RID: 10956
			private bool up;

			// Token: 0x04002ACD RID: 10957
			private GtkPlusState state;
		}

		// Token: 0x020004FF RID: 1279
		private enum WidgetType
		{
			// Token: 0x04002ACF RID: 10959
			Button,
			// Token: 0x04002AD0 RID: 10960
			CheckBox,
			// Token: 0x04002AD1 RID: 10961
			ComboBox,
			// Token: 0x04002AD2 RID: 10962
			GroupBox,
			// Token: 0x04002AD3 RID: 10963
			ProgressBar,
			// Token: 0x04002AD4 RID: 10964
			RadioButton,
			// Token: 0x04002AD5 RID: 10965
			HScrollBar,
			// Token: 0x04002AD6 RID: 10966
			VScrollBar,
			// Token: 0x04002AD7 RID: 10967
			StatusBar,
			// Token: 0x04002AD8 RID: 10968
			TabControl,
			// Token: 0x04002AD9 RID: 10969
			TextBox,
			// Token: 0x04002ADA RID: 10970
			ToolBar,
			// Token: 0x04002ADB RID: 10971
			HorizontalTrackBar,
			// Token: 0x04002ADC RID: 10972
			VerticalTrackBar,
			// Token: 0x04002ADD RID: 10973
			TreeView,
			// Token: 0x04002ADE RID: 10974
			UpDown
		}

		// Token: 0x02000500 RID: 1280
		private static class GetFirstChildWidgetOfType
		{
			// Token: 0x06004D76 RID: 19830 RVA: 0x001356B4 File Offset: 0x001338B4
			public static IntPtr Get(IntPtr parent, IntPtr childType)
			{
				GtkPlus.GetFirstChildWidgetOfType.Type = childType;
				GtkPlus.GetFirstChildWidgetOfType.Result = IntPtr.Zero;
				GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch = new ArrayList();
				GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch.Add(parent);
				do
				{
					ArrayList containersToSearch = GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch;
					GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch = new ArrayList();
					foreach (object obj in containersToSearch)
					{
						IntPtr intPtr = (IntPtr)obj;
						GtkPlus.gtk_widget_realize(intPtr);
						GtkPlus.gtk_container_forall(intPtr, new GtkPlus.GtkCallback(GtkPlus.GetFirstChildWidgetOfType.Callback), IntPtr.Zero);
						if (GtkPlus.GetFirstChildWidgetOfType.Result != IntPtr.Zero)
						{
							return GtkPlus.GetFirstChildWidgetOfType.Result;
						}
					}
				}
				while (GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch.Count != 0);
				return IntPtr.Zero;
			}

			// Token: 0x06004D77 RID: 19831 RVA: 0x001357AC File Offset: 0x001339AC
			private static void Callback(IntPtr widget, IntPtr data)
			{
				if (GtkPlus.GetFirstChildWidgetOfType.Result != IntPtr.Zero)
				{
					return;
				}
				if (GtkPlus.g_type_check_instance_is_a(widget, GtkPlus.GetFirstChildWidgetOfType.Type))
				{
					GtkPlus.GetFirstChildWidgetOfType.Result = widget;
				}
				else if (GtkPlus.g_type_check_instance_is_a(widget, GtkPlus.gtk_container_get_type()))
				{
					GtkPlus.GetFirstChildWidgetOfType.ContainersToSearch.Add(widget);
				}
			}

			// Token: 0x04002ADF RID: 10975
			private static IntPtr Type;

			// Token: 0x04002AE0 RID: 10976
			private static IntPtr Result;

			// Token: 0x04002AE1 RID: 10977
			private static ArrayList ContainersToSearch;
		}

		// Token: 0x02000501 RID: 1281
		private struct GdkColor
		{
			// Token: 0x06004D78 RID: 19832 RVA: 0x0013580C File Offset: 0x00133A0C
			public GdkColor(Color value)
			{
				this.pixel = 0U;
				this.red = (ushort)(value.R << 8);
				this.green = (ushort)(value.G << 8);
				this.blue = (ushort)(value.B << 8);
			}

			// Token: 0x04002AE2 RID: 10978
			public uint pixel;

			// Token: 0x04002AE3 RID: 10979
			public ushort red;

			// Token: 0x04002AE4 RID: 10980
			public ushort green;

			// Token: 0x04002AE5 RID: 10981
			public ushort blue;
		}

		// Token: 0x02000502 RID: 1282
		internal struct GdkRectangle
		{
			// Token: 0x06004D79 RID: 19833 RVA: 0x00135850 File Offset: 0x00133A50
			public GdkRectangle(Rectangle value)
			{
				this.x = value.X;
				this.y = value.Y;
				this.width = value.Width;
				this.height = value.Height;
			}

			// Token: 0x04002AE6 RID: 10982
			public int x;

			// Token: 0x04002AE7 RID: 10983
			public int y;

			// Token: 0x04002AE8 RID: 10984
			public int width;

			// Token: 0x04002AE9 RID: 10985
			public int height;
		}

		// Token: 0x02000503 RID: 1283
		private enum GdkColorspace
		{
			// Token: 0x04002AEB RID: 10987
			GDK_COLORSPACE_RGB
		}

		// Token: 0x02000504 RID: 1284
		internal enum GtkShadowType
		{
			// Token: 0x04002AED RID: 10989
			GTK_SHADOW_NONE,
			// Token: 0x04002AEE RID: 10990
			GTK_SHADOW_IN,
			// Token: 0x04002AEF RID: 10991
			GTK_SHADOW_OUT,
			// Token: 0x04002AF0 RID: 10992
			GTK_SHADOW_ETCHED_IN,
			// Token: 0x04002AF1 RID: 10993
			GTK_SHADOW_ETCHED_OUT
		}

		// Token: 0x02000505 RID: 1285
		private enum GtkStateType
		{
			// Token: 0x04002AF3 RID: 10995
			GTK_STATE_NORMAL,
			// Token: 0x04002AF4 RID: 10996
			GTK_STATE_ACTIVE,
			// Token: 0x04002AF5 RID: 10997
			GTK_STATE_PRELIGHT,
			// Token: 0x04002AF6 RID: 10998
			GTK_STATE_SELECTED,
			// Token: 0x04002AF7 RID: 10999
			GTK_STATE_INSENSITIVE
		}

		// Token: 0x02000506 RID: 1286
		private enum GtkWindowType
		{
			// Token: 0x04002AF9 RID: 11001
			GTK_WINDOW_TOPLEVEL,
			// Token: 0x04002AFA RID: 11002
			GTK_WINDOW_POPUP
		}

		// Token: 0x02000507 RID: 1287
		private enum GtkArrowType
		{
			// Token: 0x04002AFC RID: 11004
			GTK_ARROW_UP,
			// Token: 0x04002AFD RID: 11005
			GTK_ARROW_DOWN,
			// Token: 0x04002AFE RID: 11006
			GTK_ARROW_LEFT,
			// Token: 0x04002AFF RID: 11007
			GTK_ARROW_RIGHT,
			// Token: 0x04002B00 RID: 11008
			GTK_ARROW_NONE
		}

		// Token: 0x02000508 RID: 1288
		private enum GtkOrientation
		{
			// Token: 0x04002B02 RID: 11010
			GTK_ORIENTATION_HORIZONTAL,
			// Token: 0x04002B03 RID: 11011
			GTK_ORIENTATION_VERTICAL
		}

		// Token: 0x02000509 RID: 1289
		private enum GtkExpanderStyle
		{
			// Token: 0x04002B05 RID: 11013
			GTK_EXPANDER_COLLAPSED,
			// Token: 0x04002B06 RID: 11014
			GTK_EXPANDER_SEMI_COLLAPSED,
			// Token: 0x04002B07 RID: 11015
			GTK_EXPANDER_SEMI_EXPANDED,
			// Token: 0x04002B08 RID: 11016
			GTK_EXPANDER_EXPANDED
		}

		// Token: 0x0200050A RID: 1290
		private enum GtkPositionType
		{
			// Token: 0x04002B0A RID: 11018
			GTK_POS_LEFT,
			// Token: 0x04002B0B RID: 11019
			GTK_POS_RIGHT,
			// Token: 0x04002B0C RID: 11020
			GTK_POS_TOP,
			// Token: 0x04002B0D RID: 11021
			GTK_POS_BOTTOM
		}

		// Token: 0x0200050B RID: 1291
		private enum GtkWidgetFlags : uint
		{
			// Token: 0x04002B0F RID: 11023
			GTK_CAN_DEFAULT = 8192U
		}

		// Token: 0x0200050C RID: 1292
		private enum GdkWindowEdge
		{
			// Token: 0x04002B11 RID: 11025
			GDK_WINDOW_EDGE_NORTH_WEST,
			// Token: 0x04002B12 RID: 11026
			GDK_WINDOW_EDGE_NORTH,
			// Token: 0x04002B13 RID: 11027
			GDK_WINDOW_EDGE_NORTH_EAST,
			// Token: 0x04002B14 RID: 11028
			GDK_WINDOW_EDGE_WEST,
			// Token: 0x04002B15 RID: 11029
			GDK_WINDOW_EDGE_EAST,
			// Token: 0x04002B16 RID: 11030
			GDK_WINDOW_EDGE_SOUTH_WEST,
			// Token: 0x04002B17 RID: 11031
			GDK_WINDOW_EDGE_SOUTH,
			// Token: 0x04002B18 RID: 11032
			GDK_WINDOW_EDGE_SOUTH_EAST
		}

		// Token: 0x0200050D RID: 1293
		private struct GtkStyle
		{
			// Token: 0x04002B19 RID: 11033
			private GtkPlus.GObject parent_instance;

			// Token: 0x04002B1A RID: 11034
			[MarshalAs(30, SizeConst = 5)]
			private GtkPlus.GdkColor[] fg;

			// Token: 0x04002B1B RID: 11035
			[MarshalAs(30, SizeConst = 5)]
			private GtkPlus.GdkColor[] bg;

			// Token: 0x04002B1C RID: 11036
			[MarshalAs(30, SizeConst = 5)]
			private GtkPlus.GdkColor[] light;

			// Token: 0x04002B1D RID: 11037
			[MarshalAs(30, SizeConst = 5)]
			private GtkPlus.GdkColor[] dark;

			// Token: 0x04002B1E RID: 11038
			[MarshalAs(30, SizeConst = 5)]
			private GtkPlus.GdkColor[] mid;

			// Token: 0x04002B1F RID: 11039
			[MarshalAs(30, SizeConst = 5)]
			private GtkPlus.GdkColor[] text;

			// Token: 0x04002B20 RID: 11040
			[MarshalAs(30, SizeConst = 5)]
			private GtkPlus.GdkColor[] @base;

			// Token: 0x04002B21 RID: 11041
			[MarshalAs(30, SizeConst = 5)]
			private GtkPlus.GdkColor[] text_aa;

			// Token: 0x04002B22 RID: 11042
			private GtkPlus.GdkColor black;

			// Token: 0x04002B23 RID: 11043
			private GtkPlus.GdkColor white;

			// Token: 0x04002B24 RID: 11044
			private IntPtr font_desc;

			// Token: 0x04002B25 RID: 11045
			public int xthickness;

			// Token: 0x04002B26 RID: 11046
			public int ythickness;
		}

		// Token: 0x0200050E RID: 1294
		private struct GtkWidget
		{
			// Token: 0x04002B27 RID: 11047
			private GtkPlus.GtkObject @object;

			// Token: 0x04002B28 RID: 11048
			private ushort private_flags;

			// Token: 0x04002B29 RID: 11049
			private byte state;

			// Token: 0x04002B2A RID: 11050
			private byte saved_state;

			// Token: 0x04002B2B RID: 11051
			private string name;

			// Token: 0x04002B2C RID: 11052
			private IntPtr style;

			// Token: 0x04002B2D RID: 11053
			private GtkPlus.GtkRequisition requisition;

			// Token: 0x04002B2E RID: 11054
			public GtkPlus.GdkRectangle allocation;

			// Token: 0x04002B2F RID: 11055
			private IntPtr window;

			// Token: 0x04002B30 RID: 11056
			private IntPtr parent;
		}

		// Token: 0x0200050F RID: 1295
		private struct GtkObject
		{
			// Token: 0x04002B31 RID: 11057
			private GtkPlus.GObject parent_instance;

			// Token: 0x04002B32 RID: 11058
			public uint flags;
		}

		// Token: 0x02000510 RID: 1296
		private struct GtkRequisition
		{
			// Token: 0x04002B33 RID: 11059
			private int width;

			// Token: 0x04002B34 RID: 11060
			private int height;
		}

		// Token: 0x02000511 RID: 1297
		private struct GtkMisc
		{
			// Token: 0x04002B35 RID: 11061
			private GtkPlus.GtkWidget widget;

			// Token: 0x04002B36 RID: 11062
			public float xalign;

			// Token: 0x04002B37 RID: 11063
			public float yalign;

			// Token: 0x04002B38 RID: 11064
			public ushort xpad;

			// Token: 0x04002B39 RID: 11065
			public ushort ypad;
		}

		// Token: 0x02000512 RID: 1298
		private struct GtkTreeViewColumn
		{
			// Token: 0x04002B3A RID: 11066
			private GtkPlus.GtkObject parent;

			// Token: 0x04002B3B RID: 11067
			private IntPtr tree_view;

			// Token: 0x04002B3C RID: 11068
			public IntPtr button;
		}

		// Token: 0x02000513 RID: 1299
		private enum G_TYPE
		{

		}

		// Token: 0x02000514 RID: 1300
		private struct GTypeInstance
		{
			// Token: 0x04002B3E RID: 11070
			private IntPtr g_class;
		}

		// Token: 0x02000515 RID: 1301
		internal struct GObject
		{
			// Token: 0x04002B3F RID: 11071
			private GtkPlus.GTypeInstance g_type_instance;

			// Token: 0x04002B40 RID: 11072
			private uint ref_count;

			// Token: 0x04002B41 RID: 11073
			private IntPtr qdata;
		}

		// Token: 0x02000651 RID: 1617
		// (Invoke) Token: 0x060050F6 RID: 20726
		private delegate void ToggleButtonPaintFunction(IntPtr style, IntPtr window, GtkPlus.GtkStateType state_type, GtkPlus.GtkShadowType shadow_type, ref GtkPlus.GdkRectangle area, IntPtr widget, string detail, int x, int y, int width, int height);

		// Token: 0x02000652 RID: 1618
		// (Invoke) Token: 0x060050FA RID: 20730
		[UnmanagedFunctionPointer(2)]
		private delegate void GtkCallback(IntPtr widget, IntPtr data);
	}
}
