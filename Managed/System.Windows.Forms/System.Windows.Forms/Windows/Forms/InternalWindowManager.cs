using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020001E7 RID: 487
	internal abstract class InternalWindowManager
	{
		// Token: 0x06001EA0 RID: 7840 RVA: 0x000730E0 File Offset: 0x000712E0
		public InternalWindowManager(Form form)
		{
			this.form = form;
			form.SizeChanged += new EventHandler(this.FormSizeChangedHandler);
			this.title_buttons = new TitleButtons(form);
			ThemeEngine.Current.ManagedWindowSetButtonLocations(this);
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x00073124 File Offset: 0x00071324
		public Form Form
		{
			get
			{
				return this.form;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0007312C File Offset: 0x0007132C
		public int IconWidth
		{
			get
			{
				return this.TitleBarHeight - 5;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00073138 File Offset: 0x00071338
		public TitleButtons TitleButtons
		{
			get
			{
				return this.title_buttons;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x00073140 File Offset: 0x00071340
		// (set) Token: 0x06001EA5 RID: 7845 RVA: 0x00073148 File Offset: 0x00071348
		internal Rectangle NormalBounds
		{
			get
			{
				return this.normal_bounds;
			}
			set
			{
				this.normal_bounds = value;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x00073154 File Offset: 0x00071354
		internal Size IconicSize
		{
			get
			{
				return SystemInformation.MinimizedWindowSize;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0007315C File Offset: 0x0007135C
		// (set) Token: 0x06001EA8 RID: 7848 RVA: 0x000731B8 File Offset: 0x000713B8
		internal Rectangle IconicBounds
		{
			get
			{
				if (this.iconic_bounds == Rectangle.Empty)
				{
					return Rectangle.Empty;
				}
				Rectangle rectangle = this.iconic_bounds;
				rectangle.Y = this.Form.Parent.ClientRectangle.Bottom - this.iconic_bounds.Y;
				return rectangle;
			}
			set
			{
				this.iconic_bounds = value;
				this.iconic_bounds.Y = this.Form.Parent.ClientRectangle.Bottom - this.iconic_bounds.Y;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x000731FC File Offset: 0x000713FC
		internal virtual Rectangle MaximizedBounds
		{
			get
			{
				return this.Form.Parent.ClientRectangle;
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00073210 File Offset: 0x00071410
		public virtual void UpdateWindowState(FormWindowState old_window_state, FormWindowState new_window_state, bool force)
		{
			if (old_window_state == FormWindowState.Normal)
			{
				this.NormalBounds = this.form.Bounds;
			}
			else if (old_window_state == FormWindowState.Minimized)
			{
				this.IconicBounds = this.form.Bounds;
			}
			switch (new_window_state)
			{
			case FormWindowState.Normal:
				this.form.Bounds = this.NormalBounds;
				break;
			case FormWindowState.Minimized:
				if (this.IconicBounds == Rectangle.Empty)
				{
					Size iconicSize = this.IconicSize;
					Point point;
					point..ctor(0, this.Form.Parent.ClientSize.Height - iconicSize.Height);
					this.IconicBounds = new Rectangle(point, iconicSize);
				}
				this.form.Bounds = this.IconicBounds;
				break;
			case FormWindowState.Maximized:
				this.form.Bounds = this.MaximizedBounds;
				break;
			}
			this.UpdateWindowDecorations(new_window_state);
			this.form.ResetCursor();
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00073310 File Offset: 0x00071510
		public virtual void UpdateWindowDecorations(FormWindowState window_state)
		{
			ThemeEngine.Current.ManagedWindowSetButtonLocations(this);
			if (this.form.IsHandleCreated)
			{
				XplatUI.RequestNCRecalc(this.form.Handle);
			}
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00073348 File Offset: 0x00071548
		public virtual bool WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			switch (msg)
			{
			case Msg.WM_MOUSEMOVE:
				return this.HandleMouseMove(this.form, ref m);
			case Msg.WM_LBUTTONDOWN:
				return this.HandleLButtonDown(ref m);
			case Msg.WM_LBUTTONUP:
				this.HandleLButtonUp(ref m);
				break;
			case Msg.WM_LBUTTONDBLCLK:
				return this.HandleLButtonDblClick(ref m);
			case Msg.WM_RBUTTONDOWN:
				return this.HandleRButtonDown(ref m);
			default:
				switch (msg)
				{
				case Msg.WM_NCMOUSEMOVE:
					this.HandleNCMouseMove(ref m);
					return true;
				case Msg.WM_NCLBUTTONDOWN:
					this.HandleNCLButtonDown(ref m);
					return true;
				case Msg.WM_NCLBUTTONUP:
					this.HandleNCLButtonUp(ref m);
					return true;
				case Msg.WM_NCLBUTTONDBLCLK:
					this.HandleNCLButtonDblClick(ref m);
					break;
				default:
					switch (msg)
					{
					case Msg.WM_NCCALCSIZE:
						return this.HandleNCCalcSize(ref m);
					case Msg.WM_NCHITTEST:
						return this.HandleNCHitTest(ref m);
					case Msg.WM_NCPAINT:
						return this.HandleNCPaint(ref m);
					default:
						if (msg != Msg.WM_NCMOUSELEAVE)
						{
							if (msg != Msg.WM_MOUSELEAVE)
							{
								if (msg == Msg.WM_PARENTNOTIFY)
								{
									if (Control.LowOrder(m.WParam.ToInt32()) == 513)
									{
										this.Activate();
									}
								}
							}
							else
							{
								this.HandleMouseLeave(ref m);
							}
						}
						else
						{
							this.HandleNCMouseLeave(ref m);
						}
						break;
					}
					break;
				}
				break;
			}
			return false;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0007348C File Offset: 0x0007168C
		protected virtual bool HandleNCPaint(ref Message m)
		{
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, this.form.Handle, false);
			Rectangle rectangle;
			if (this.form.ActiveMenu != null)
			{
				Point menuOrigin = this.GetMenuOrigin();
				rectangle..ctor(menuOrigin.X, menuOrigin.Y, this.form.ClientSize.Width, 0);
				rectangle = Rectangle.Union(rectangle, paintEventArgs.ClipRectangle);
				paintEventArgs.SetClip(rectangle);
				paintEventArgs.Graphics.SetClip(rectangle);
				this.form.ActiveMenu.Draw(paintEventArgs, new Rectangle(menuOrigin.X, menuOrigin.Y, this.form.ClientSize.Width, 0));
			}
			if (this.HasBorders || (this.IsMinimized && (!this.Form.IsMdiChild || !this.IsMaximized)))
			{
				rectangle..ctor(0, 0, this.form.Width, this.form.Height);
				ThemeEngine.Current.DrawManagedWindowDecorations(paintEventArgs.Graphics, rectangle, this);
			}
			XplatUI.PaintEventEnd(ref m, this.form.Handle, false);
			return true;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000735BC File Offset: 0x000717BC
		protected virtual bool HandleNCCalcSize(ref Message m)
		{
			if (m.WParam == (IntPtr)1)
			{
				XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
				nccalcsize_PARAMS.rgrc1 = this.NCCalcSize(nccalcsize_PARAMS.rgrc1);
				Marshal.StructureToPtr(nccalcsize_PARAMS, m.LParam, true);
			}
			else
			{
				XplatUIWin32.RECT rect = (XplatUIWin32.RECT)Marshal.PtrToStructure(m.LParam, typeof(XplatUIWin32.RECT));
				rect = this.NCCalcSize(rect);
				Marshal.StructureToPtr(rect, m.LParam, true);
			}
			return true;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0007365C File Offset: 0x0007185C
		protected virtual XplatUIWin32.RECT NCCalcSize(XplatUIWin32.RECT proposed_window_rect)
		{
			int num = ThemeEngine.Current.ManagedWindowBorderWidth(this);
			if (this.HasBorders)
			{
				proposed_window_rect.top += this.TitleBarHeight + num;
				proposed_window_rect.bottom -= num;
				proposed_window_rect.left += num;
				proposed_window_rect.right -= num;
			}
			if (XplatUI.RequiresPositiveClientAreaSize)
			{
				if (proposed_window_rect.right <= proposed_window_rect.left)
				{
					proposed_window_rect.right += proposed_window_rect.left - proposed_window_rect.right + 1;
				}
				if (proposed_window_rect.top >= proposed_window_rect.bottom)
				{
					proposed_window_rect.bottom += proposed_window_rect.top - proposed_window_rect.bottom + 1;
				}
			}
			return proposed_window_rect;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00073734 File Offset: 0x00071934
		protected virtual bool HandleNCHitTest(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (formPos == InternalWindowManager.FormPos.TitleBar)
			{
				m.Result = new IntPtr(2);
				return true;
			}
			if (!this.IsSizable)
			{
				return false;
			}
			InternalWindowManager.FormPos formPos2 = formPos;
			switch (formPos2)
			{
			case InternalWindowManager.FormPos.Top:
				m.Result = new IntPtr(12);
				break;
			default:
				if (formPos2 != InternalWindowManager.FormPos.Bottom)
				{
					if (formPos2 != InternalWindowManager.FormPos.BottomLeft)
					{
						if (formPos2 != InternalWindowManager.FormPos.BottomRight)
						{
							return false;
						}
						m.Result = new IntPtr(17);
					}
					else
					{
						m.Result = new IntPtr(16);
					}
				}
				else
				{
					m.Result = new IntPtr(15);
				}
				break;
			case InternalWindowManager.FormPos.Left:
				m.Result = new IntPtr(10);
				break;
			case InternalWindowManager.FormPos.TopLeft:
				m.Result = new IntPtr(13);
				break;
			case InternalWindowManager.FormPos.Right:
				m.Result = new IntPtr(11);
				break;
			case InternalWindowManager.FormPos.TopRight:
				m.Result = new IntPtr(14);
				break;
			}
			return true;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00073884 File Offset: 0x00071A84
		public virtual void UpdateBorderStyle(FormBorderStyle border_style)
		{
			if (this.form.IsHandleCreated)
			{
				XplatUI.SetBorderStyle(this.form.Handle, border_style);
			}
			if (this.ShouldRemoveWindowManager(border_style))
			{
				this.form.RemoveWindowManager();
				return;
			}
			ThemeEngine.Current.ManagedWindowSetButtonLocations(this);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000738D8 File Offset: 0x00071AD8
		public virtual void SetWindowState(FormWindowState old_state, FormWindowState window_state)
		{
			this.UpdateWindowState(old_state, window_state, false);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000738E4 File Offset: 0x00071AE4
		public virtual FormWindowState GetWindowState()
		{
			return this.form.window_state;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000738F4 File Offset: 0x00071AF4
		public virtual void PointToClient(ref int x, ref int y)
		{
			Rectangle workingArea = SystemInformation.WorkingArea;
			if (x > workingArea.Right)
			{
				x = workingArea.Right;
			}
			if (x < workingArea.Left)
			{
				x = workingArea.Left;
			}
			if (y < workingArea.Top)
			{
				y = workingArea.Top;
			}
			if (y > workingArea.Bottom)
			{
				y = workingArea.Bottom;
			}
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00073964 File Offset: 0x00071B64
		public virtual void PointToScreen(ref int x, ref int y)
		{
			XplatUI.ClientToScreen(this.form.Handle, ref x, ref y);
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00073978 File Offset: 0x00071B78
		protected virtual bool ShouldRemoveWindowManager(FormBorderStyle style)
		{
			return style != FormBorderStyle.FixedToolWindow && style != FormBorderStyle.SizableToolWindow;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0007398C File Offset: 0x00071B8C
		public bool IconRectangleContains(int x, int y)
		{
			return this.ShowIcon && ThemeEngine.Current.ManagedWindowGetTitleBarIconArea(this).Contains(x, y);
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000739BC File Offset: 0x00071BBC
		public bool ShowIcon
		{
			get
			{
				return this.Form.ShowIcon && this.HasBorders && (this.IsMinimized || (!this.IsToolWindow && this.Form.FormBorderStyle != FormBorderStyle.FixedDialog));
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00073A14 File Offset: 0x00071C14
		protected virtual void Activate()
		{
			this.form.Invalidate(true);
			this.form.Update();
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00073A30 File Offset: 0x00071C30
		public virtual bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00073A34 File Offset: 0x00071C34
		private void FormSizeChangedHandler(object sender, EventArgs e)
		{
			if (this.form.IsHandleCreated)
			{
				ThemeEngine.Current.ManagedWindowSetButtonLocations(this);
				XplatUI.InvalidateNC(this.form.Handle);
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00073A6C File Offset: 0x00071C6C
		protected virtual bool HandleRButtonDown(ref Message m)
		{
			this.Activate();
			return false;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00073A78 File Offset: 0x00071C78
		protected virtual bool HandleLButtonDown(ref Message m)
		{
			this.Activate();
			return false;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00073A84 File Offset: 0x00071C84
		protected virtual bool HandleLButtonDblClick(ref Message m)
		{
			return false;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00073A88 File Offset: 0x00071C88
		protected virtual bool HandleNCMouseLeave(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (formPos != InternalWindowManager.FormPos.TitleBar)
			{
				this.HandleTitleBarLeave(num, num2);
				return true;
			}
			return true;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00073AE4 File Offset: 0x00071CE4
		protected virtual bool HandleNCMouseMove(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (formPos == InternalWindowManager.FormPos.TitleBar)
			{
				this.HandleTitleBarMouseMove(num, num2);
				return true;
			}
			if (this.form.ActiveMenu != null && XplatUI.IsEnabled(this.form.Handle))
			{
				MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.form.mouse_clicks, num, num2, 0);
				this.form.ActiveMenu.OnMouseMove(this.form, mouseEventArgs);
			}
			return true;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00073BA8 File Offset: 0x00071DA8
		protected virtual bool HandleNCLButtonDown(ref Message m)
		{
			this.Activate();
			this.start = Cursor.Position;
			this.virtual_position = this.form.Bounds;
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (this.form.ActiveMenu != null && XplatUI.IsEnabled(this.form.Handle))
			{
				MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.form.mouse_clicks, num, num2 - this.TitleBarHeight, 0);
				this.form.ActiveMenu.OnMouseDown(this.form, mouseEventArgs);
			}
			if (formPos == InternalWindowManager.FormPos.TitleBar)
			{
				this.HandleTitleBarDown(num, num2);
				return true;
			}
			if (!this.IsSizable)
			{
				return false;
			}
			if ((formPos & InternalWindowManager.FormPos.AnyEdge) == InternalWindowManager.FormPos.None)
			{
				return false;
			}
			this.virtual_position = this.form.Bounds;
			this.state = InternalWindowManager.State.Sizing;
			this.sizing_edge = formPos;
			this.form.Capture = true;
			return true;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00073CD8 File Offset: 0x00071ED8
		protected virtual void HandleNCLButtonDblClick(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (formPos == InternalWindowManager.FormPos.TitleBar || formPos == InternalWindowManager.FormPos.Top)
			{
				this.HandleTitleBarDoubleClick(num, num2);
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00073D38 File Offset: 0x00071F38
		protected virtual void HandleTitleBarDoubleClick(int x, int y)
		{
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00073D3C File Offset: 0x00071F3C
		protected virtual void HandleTitleBarLeave(int x, int y)
		{
			this.title_buttons.MouseLeave(x, y);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00073D4C File Offset: 0x00071F4C
		protected virtual void HandleTitleBarMouseMove(int x, int y)
		{
			if (this.title_buttons.MouseMove(x, y))
			{
				XplatUI.InvalidateNC(this.form.Handle);
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00073D7C File Offset: 0x00071F7C
		protected virtual void HandleTitleBarUp(int x, int y)
		{
			this.title_buttons.MouseUp(x, y);
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00073D8C File Offset: 0x00071F8C
		protected virtual void HandleTitleBarDown(int x, int y)
		{
			this.title_buttons.MouseDown(x, y);
			if (!this.TitleButtons.AnyPushedTitleButtons && !this.IsMaximized)
			{
				this.state = InternalWindowManager.State.Moving;
				this.clicked_point = new Point(x, y);
				if (this.form.Parent != null)
				{
					this.form.CaptureWithConfine(this.form.Parent);
				}
				else
				{
					this.form.Capture = true;
				}
			}
			XplatUI.InvalidateNC(this.form.Handle);
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00073E1C File Offset: 0x0007201C
		private bool HandleMouseMove(Form form, ref Message m)
		{
			InternalWindowManager.State state = this.state;
			if (state == InternalWindowManager.State.Moving)
			{
				this.HandleWindowMove(m);
				return true;
			}
			if (state != InternalWindowManager.State.Sizing)
			{
				return false;
			}
			this.HandleSizing(m);
			return true;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00073E60 File Offset: 0x00072060
		private void HandleMouseLeave(ref Message m)
		{
			this.form.ResetCursor();
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00073E70 File Offset: 0x00072070
		protected virtual void HandleWindowMove(Message m)
		{
			Point point = this.MouseMove(Cursor.Position);
			this.UpdateVP(this.virtual_position.X + point.X, this.virtual_position.Y + point.Y, this.virtual_position.Width, this.virtual_position.Height);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00073ECC File Offset: 0x000720CC
		private void HandleSizing(Message m)
		{
			Rectangle rectangle = this.virtual_position;
			int num;
			int num2;
			if (this.IsToolWindow)
			{
				int borderWidth = this.BorderWidth;
				num = 2 * (borderWidth + 2) + ThemeEngine.Current.ManagedWindowButtonSize(this).Width;
				num2 = 2 * borderWidth + this.TitleBarHeight;
			}
			else
			{
				Size minWindowTrackSize = SystemInformation.MinWindowTrackSize;
				num = minWindowTrackSize.Width;
				num2 = minWindowTrackSize.Height;
			}
			int num3 = Cursor.Position.X;
			int num4 = Cursor.Position.Y;
			this.PointToClient(ref num3, ref num4);
			if ((this.sizing_edge & InternalWindowManager.FormPos.Top) != InternalWindowManager.FormPos.None)
			{
				if (rectangle.Bottom - num4 < num2)
				{
					num4 = rectangle.Bottom - num2;
				}
				rectangle.Height = rectangle.Bottom - num4;
				rectangle.Y = num4;
			}
			else if ((this.sizing_edge & InternalWindowManager.FormPos.Bottom) != InternalWindowManager.FormPos.None)
			{
				int num5 = num4 - rectangle.Top;
				if (num5 <= num2)
				{
					num5 = num2;
				}
				rectangle.Height = num5;
			}
			if ((this.sizing_edge & InternalWindowManager.FormPos.Left) != InternalWindowManager.FormPos.None)
			{
				if (rectangle.Right - num3 < num)
				{
					num3 = rectangle.Right - num;
				}
				rectangle.Width = rectangle.Right - num3;
				rectangle.X = num3;
			}
			else if ((this.sizing_edge & InternalWindowManager.FormPos.Right) != InternalWindowManager.FormPos.None)
			{
				int num6 = num3 - this.form.Left;
				if (num6 <= num)
				{
					num6 = num;
				}
				rectangle.Width = num6;
			}
			this.UpdateVP(rectangle);
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00074054 File Offset: 0x00072254
		public bool IsMaximized
		{
			get
			{
				return this.GetWindowState() == FormWindowState.Maximized;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x00074060 File Offset: 0x00072260
		public bool IsMinimized
		{
			get
			{
				return this.GetWindowState() == FormWindowState.Minimized;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0007406C File Offset: 0x0007226C
		public bool IsSizable
		{
			get
			{
				switch (this.form.FormBorderStyle)
				{
				case FormBorderStyle.Sizable:
				case FormBorderStyle.SizableToolWindow:
					return this.form.window_state != FormWindowState.Minimized;
				}
				return false;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000740B4 File Offset: 0x000722B4
		public bool HasBorders
		{
			get
			{
				return this.form.FormBorderStyle != FormBorderStyle.None;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x000740C8 File Offset: 0x000722C8
		public bool IsToolWindow
		{
			get
			{
				return this.form.FormBorderStyle == FormBorderStyle.SizableToolWindow || this.form.FormBorderStyle == FormBorderStyle.FixedToolWindow || this.form.GetCreateParams().IsSet(WindowExStyles.WS_EX_TOOLWINDOW);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x00074114 File Offset: 0x00072314
		public int TitleBarHeight
		{
			get
			{
				return ThemeEngine.Current.ManagedWindowTitleBarHeight(this);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x00074124 File Offset: 0x00072324
		public int BorderWidth
		{
			get
			{
				return ThemeEngine.Current.ManagedWindowBorderWidth(this);
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x00074134 File Offset: 0x00072334
		public virtual int MenuHeight
		{
			get
			{
				return (this.form.Menu == null) ? 0 : ThemeEngine.Current.MenuHeight;
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00074164 File Offset: 0x00072364
		protected void UpdateVP(Rectangle r)
		{
			this.UpdateVP(r.X, r.Y, r.Width, r.Height);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00074194 File Offset: 0x00072394
		protected void UpdateVP(Point loc, int w, int h)
		{
			this.UpdateVP(loc.X, loc.Y, w, h);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000741B8 File Offset: 0x000723B8
		protected void UpdateVP(int x, int y, int w, int h)
		{
			this.virtual_position.X = x;
			this.virtual_position.Y = y;
			this.virtual_position.Width = w;
			this.virtual_position.Height = h;
			this.DrawVirtualPosition(this.virtual_position);
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00074204 File Offset: 0x00072404
		protected virtual void HandleLButtonUp(ref Message m)
		{
			if (this.state == InternalWindowManager.State.Idle)
			{
				return;
			}
			this.ClearVirtualPosition();
			this.form.Capture = false;
			if (this.state == InternalWindowManager.State.Moving && this.form.Location != this.virtual_position.Location)
			{
				this.form.Location = this.virtual_position.Location;
			}
			else if (this.state == InternalWindowManager.State.Sizing && this.form.Bounds != this.virtual_position)
			{
				this.form.Bounds = this.virtual_position;
			}
			this.state = InternalWindowManager.State.Idle;
			this.OnWindowFinishedMoving();
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000742BC File Offset: 0x000724BC
		private bool HandleNCLButtonUp(ref Message m)
		{
			if (this.form.Capture)
			{
				this.ClearVirtualPosition();
				this.form.Capture = false;
				this.state = InternalWindowManager.State.Idle;
				if (this.form.MdiContainer != null)
				{
					this.form.MdiContainer.SizeScrollBars();
				}
			}
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (formPos == InternalWindowManager.FormPos.TitleBar)
			{
				this.HandleTitleBarUp(num, num2);
				return true;
			}
			return true;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00074364 File Offset: 0x00072564
		protected void DrawTitleButton(Graphics dc, TitleButton button, Rectangle clip)
		{
			if (!button.Rectangle.IntersectsWith(clip))
			{
				return;
			}
			ThemeEngine.Current.ManagedWindowDrawMenuButton(dc, button, clip, this);
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00074394 File Offset: 0x00072594
		public virtual void DrawMaximizedButtons(object sender, PaintEventArgs pe)
		{
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00074398 File Offset: 0x00072598
		protected Point MouseMove(Point pos)
		{
			return new Point(pos.X - this.start.X, pos.Y - this.start.Y);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000743D0 File Offset: 0x000725D0
		protected virtual void DrawVirtualPosition(Rectangle virtual_position)
		{
			this.form.Bounds = virtual_position;
			this.start = Cursor.Position;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000743EC File Offset: 0x000725EC
		protected virtual void ClearVirtualPosition()
		{
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000743F0 File Offset: 0x000725F0
		protected virtual void OnWindowFinishedMoving()
		{
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000743F4 File Offset: 0x000725F4
		protected virtual void NCPointToClient(ref int x, ref int y)
		{
			this.form.PointToClient(ref x, ref y);
			this.NCClientToNC(ref x, ref y);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0007440C File Offset: 0x0007260C
		protected virtual void NCClientToNC(ref int x, ref int y)
		{
			y += this.TitleBarHeight;
			y += this.BorderWidth;
			y += this.MenuHeight;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0007443C File Offset: 0x0007263C
		internal Point GetMenuOrigin()
		{
			return new Point(this.BorderWidth, this.BorderWidth + this.TitleBarHeight);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00074464 File Offset: 0x00072664
		protected InternalWindowManager.FormPos FormPosForCoords(int x, int y)
		{
			int borderWidth = this.BorderWidth;
			if (y < this.TitleBarHeight + borderWidth)
			{
				if (y > borderWidth && x > borderWidth && x < this.form.Width - borderWidth)
				{
					return InternalWindowManager.FormPos.TitleBar;
				}
				if (x < borderWidth || (x < 20 && y < borderWidth))
				{
					return InternalWindowManager.FormPos.TopLeft;
				}
				if (x > this.form.Width - borderWidth || (x > this.form.Width - 20 && y < borderWidth))
				{
					return InternalWindowManager.FormPos.TopRight;
				}
				if (y < borderWidth)
				{
					return InternalWindowManager.FormPos.Top;
				}
			}
			else if (y > this.form.Height - 20)
			{
				if (x < borderWidth || (x < 20 && y > this.form.Height - borderWidth))
				{
					return InternalWindowManager.FormPos.BottomLeft;
				}
				if (x > this.form.Width - borderWidth * 2 || (x > this.form.Width - 20 && y > this.form.Height - borderWidth))
				{
					return InternalWindowManager.FormPos.BottomRight;
				}
				if (y > this.form.Height - borderWidth * 2)
				{
					return InternalWindowManager.FormPos.Bottom;
				}
			}
			else
			{
				if (x < borderWidth)
				{
					return InternalWindowManager.FormPos.Left;
				}
				if (x > this.form.Width - borderWidth * 2)
				{
					return InternalWindowManager.FormPos.Right;
				}
			}
			return InternalWindowManager.FormPos.None;
		}

		// Token: 0x0400100E RID: 4110
		private TitleButtons title_buttons;

		// Token: 0x0400100F RID: 4111
		internal Form form;

		// Token: 0x04001010 RID: 4112
		internal Point start;

		// Token: 0x04001011 RID: 4113
		internal InternalWindowManager.State state;

		// Token: 0x04001012 RID: 4114
		protected Point clicked_point;

		// Token: 0x04001013 RID: 4115
		private InternalWindowManager.FormPos sizing_edge;

		// Token: 0x04001014 RID: 4116
		internal Rectangle virtual_position;

		// Token: 0x04001015 RID: 4117
		private Rectangle normal_bounds;

		// Token: 0x04001016 RID: 4118
		private Rectangle iconic_bounds;

		// Token: 0x020001E8 RID: 488
		public enum State
		{
			// Token: 0x04001018 RID: 4120
			Idle,
			// Token: 0x04001019 RID: 4121
			Moving,
			// Token: 0x0400101A RID: 4122
			Sizing
		}

		// Token: 0x020001E9 RID: 489
		[Flags]
		public enum FormPos
		{
			// Token: 0x0400101C RID: 4124
			None = 0,
			// Token: 0x0400101D RID: 4125
			TitleBar = 1,
			// Token: 0x0400101E RID: 4126
			Top = 2,
			// Token: 0x0400101F RID: 4127
			Left = 4,
			// Token: 0x04001020 RID: 4128
			Right = 8,
			// Token: 0x04001021 RID: 4129
			Bottom = 16,
			// Token: 0x04001022 RID: 4130
			TopLeft = 6,
			// Token: 0x04001023 RID: 4131
			TopRight = 10,
			// Token: 0x04001024 RID: 4132
			BottomLeft = 20,
			// Token: 0x04001025 RID: 4133
			BottomRight = 24,
			// Token: 0x04001026 RID: 4134
			AnyEdge = 30
		}
	}
}
