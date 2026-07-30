using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004B2 RID: 1202
	internal class MouseHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06004BFB RID: 19451 RVA: 0x0012E5C0 File Offset: 0x0012C7C0
		internal MouseHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x0012E5CC File Offset: 0x0012C7CC
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			QDPoint qdpoint = default(QDPoint);
			CGPoint cgpoint = default(CGPoint);
			Rect rect = default(Rect);
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			bool flag = true;
			ushort num = 0;
			MouseHandler.GetEventParameter(eventref, 1835822947U, 1363439732U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(QDPoint)), IntPtr.Zero, ref qdpoint);
			MouseHandler.GetEventParameter(eventref, 1835168878U, 1835168878U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(ushort)), IntPtr.Zero, ref num);
			if (num == 1 && (this.Driver.ModifierKeys & Keys.Control) != Keys.None)
			{
				num = 2;
			}
			cgpoint.x = (float)qdpoint.x;
			cgpoint.y = (float)qdpoint.y;
			if (MouseHandler.FindWindow(qdpoint, ref zero2) == 5)
			{
				return true;
			}
			MouseHandler.GetWindowBounds(handle, 33U, ref rect);
			MouseHandler.HIViewFindByID(MouseHandler.HIViewGetRoot(handle), new HIViewID(2003398244U, 1U), ref zero2);
			cgpoint.x -= (float)rect.left;
			cgpoint.y -= (float)rect.top;
			MouseHandler.HIViewGetSubviewHit(zero2, ref cgpoint, true, ref zero);
			MouseHandler.HIViewConvertPoint(ref cgpoint, zero2, zero);
			Hwnd hwnd = Hwnd.ObjectFromHandle(zero);
			if (hwnd != null)
			{
				flag = hwnd.ClientWindow == zero;
			}
			if (XplatUICarbon.Grab.Hwnd != IntPtr.Zero)
			{
				hwnd = Hwnd.ObjectFromHandle(XplatUICarbon.Grab.Hwnd);
				flag = true;
			}
			if (hwnd == null)
			{
				return true;
			}
			if (flag)
			{
				qdpoint.x = (short)cgpoint.x;
				qdpoint.y = (short)cgpoint.y;
				this.Driver.ScreenToClient(hwnd.Handle, ref qdpoint);
			}
			else
			{
				cgpoint.x = (float)qdpoint.x;
				cgpoint.y = (float)qdpoint.y;
			}
			msg.hwnd = hwnd.Handle;
			msg.lParam = (IntPtr)(((int)((ushort)cgpoint.y) << 16) | (int)((ushort)cgpoint.x));
			switch (kind)
			{
			case 1U:
				this.UpdateMouseState((int)num, true);
				msg.message = ((!flag) ? Msg.WM_NCMOUSEMOVE : Msg.WM_MOUSEMOVE) + (int)((num - 1) * 3) + 1;
				msg.wParam = this.Driver.GetMousewParam(0);
				if (MouseHandler.ClickPending.Pending && DateTime.Now.Ticks - MouseHandler.ClickPending.Time < 7500000L && msg.hwnd == MouseHandler.ClickPending.Hwnd && msg.wParam == MouseHandler.ClickPending.wParam && msg.lParam == MouseHandler.ClickPending.lParam && msg.message == MouseHandler.ClickPending.Message)
				{
					msg.message = ((!flag) ? Msg.WM_NCMOUSEMOVE : Msg.WM_MOUSEMOVE) + (int)((num - 1) * 3) + 3;
					MouseHandler.ClickPending.Pending = false;
				}
				else
				{
					MouseHandler.ClickPending.Pending = true;
					MouseHandler.ClickPending.Hwnd = msg.hwnd;
					MouseHandler.ClickPending.Message = msg.message;
					MouseHandler.ClickPending.wParam = msg.wParam;
					MouseHandler.ClickPending.lParam = msg.lParam;
					MouseHandler.ClickPending.Time = DateTime.Now.Ticks;
				}
				goto IL_058F;
			case 2U:
				this.UpdateMouseState((int)num, false);
				msg.message = ((!flag) ? Msg.WM_NCMOUSEMOVE : Msg.WM_MOUSEMOVE) + (int)((num - 1) * 3) + 2;
				msg.wParam = this.Driver.GetMousewParam(0);
				goto IL_058F;
			case 5U:
			case 6U:
				if (XplatUICarbon.Grab.Hwnd == IntPtr.Zero)
				{
					IntPtr intPtr = IntPtr.Zero;
					if (flag)
					{
						intPtr = (IntPtr)1;
						NativeWindow.WndProc(msg.hwnd, Msg.WM_SETCURSOR, msg.hwnd, (IntPtr)1);
					}
					else
					{
						intPtr = (IntPtr)NativeWindow.WndProc(hwnd.client_window, Msg.WM_NCHITTEST, IntPtr.Zero, msg.lParam).ToInt32();
						NativeWindow.WndProc(hwnd.client_window, Msg.WM_SETCURSOR, msg.hwnd, intPtr);
					}
				}
				msg.message = ((!flag) ? Msg.WM_NCMOUSEMOVE : Msg.WM_MOUSEMOVE);
				msg.wParam = this.Driver.GetMousewParam(0);
				goto IL_058F;
			case 10U:
			case 11U:
			{
				ushort num2 = 0;
				int num3 = 0;
				MouseHandler.GetEventParameter(eventref, 1836540280U, 1836540280U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(ushort)), IntPtr.Zero, ref num2);
				MouseHandler.GetEventParameter(eventref, 1836541036U, 1819242087U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(int)), IntPtr.Zero, ref num3);
				if (num2 == 1)
				{
					msg.hwnd = XplatUICarbon.FocusWindow;
					msg.message = Msg.WM_MOUSEWHEEL;
					msg.wParam = this.Driver.GetMousewParam(num3 * 40);
					return true;
				}
				goto IL_058F;
			}
			}
			return false;
			IL_058F:
			this.Driver.mouse_position.X = (int)cgpoint.x;
			this.Driver.mouse_position.Y = (int)cgpoint.y;
			return true;
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0012EB9C File Offset: 0x0012CD9C
		internal bool TranslateMessage(ref MSG msg)
		{
			if (msg.message == Msg.WM_MOUSEMOVE || msg.message == Msg.WM_NCMOUSEMOVE)
			{
				Hwnd hwnd = Hwnd.ObjectFromHandle(msg.hwnd);
				if (XplatUICarbon.MouseHwnd == null)
				{
					this.Driver.PostMessage(hwnd.Handle, Msg.WM_MOUSE_ENTER, IntPtr.Zero, IntPtr.Zero);
					Cursor.SetCursor(hwnd.Cursor);
				}
				else if (XplatUICarbon.MouseHwnd.Handle != hwnd.Handle)
				{
					this.Driver.PostMessage(XplatUICarbon.MouseHwnd.Handle, Msg.WM_MOUSELEAVE, IntPtr.Zero, IntPtr.Zero);
					this.Driver.PostMessage(hwnd.Handle, Msg.WM_MOUSE_ENTER, IntPtr.Zero, IntPtr.Zero);
					Cursor.SetCursor(hwnd.Cursor);
				}
				XplatUICarbon.MouseHwnd = hwnd;
			}
			return false;
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0012EC84 File Offset: 0x0012CE84
		private void UpdateMouseState(int button, bool down)
		{
			switch (button)
			{
			case 1:
				if (down)
				{
					XplatUICarbon.MouseState |= MouseButtons.Left;
				}
				else
				{
					XplatUICarbon.MouseState &= ~MouseButtons.Left;
				}
				break;
			case 2:
				if (down)
				{
					XplatUICarbon.MouseState |= MouseButtons.Right;
				}
				else
				{
					XplatUICarbon.MouseState &= ~MouseButtons.Right;
				}
				break;
			case 3:
				if (down)
				{
					XplatUICarbon.MouseState |= MouseButtons.Middle;
				}
				else
				{
					XplatUICarbon.MouseState &= ~MouseButtons.Middle;
				}
				break;
			}
		}

		// Token: 0x06004BFF RID: 19455
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref QDPoint data);

		// Token: 0x06004C00 RID: 19456
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref int data);

		// Token: 0x06004C01 RID: 19457
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref ushort data);

		// Token: 0x06004C02 RID: 19458
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern short FindWindow(QDPoint point, ref IntPtr handle);

		// Token: 0x06004C03 RID: 19459
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetWindowBounds(IntPtr handle, uint region, ref Rect bounds);

		// Token: 0x06004C04 RID: 19460
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewConvertPoint(ref CGPoint point, IntPtr source_view, IntPtr target_view);

		// Token: 0x06004C05 RID: 19461
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr HIViewGetRoot(IntPtr handle);

		// Token: 0x06004C06 RID: 19462
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewGetSubviewHit(IntPtr content_view, ref CGPoint point, bool tval, ref IntPtr hit_view);

		// Token: 0x06004C07 RID: 19463
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewFindByID(IntPtr root_window, HIViewID id, ref IntPtr view_handle);

		// Token: 0x06004C08 RID: 19464
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetCurrentEventButtonState();

		// Token: 0x0400294F RID: 10575
		internal const uint kEventMouseDown = 1U;

		// Token: 0x04002950 RID: 10576
		internal const uint kEventMouseUp = 2U;

		// Token: 0x04002951 RID: 10577
		internal const uint kEventMouseMoved = 5U;

		// Token: 0x04002952 RID: 10578
		internal const uint kEventMouseDragged = 6U;

		// Token: 0x04002953 RID: 10579
		internal const uint kEventMouseEntered = 8U;

		// Token: 0x04002954 RID: 10580
		internal const uint kEventMouseExited = 9U;

		// Token: 0x04002955 RID: 10581
		internal const uint kEventMouseWheelMoved = 10U;

		// Token: 0x04002956 RID: 10582
		internal const uint kEventMouseScroll = 11U;

		// Token: 0x04002957 RID: 10583
		internal const uint kEventParamMouseLocation = 1835822947U;

		// Token: 0x04002958 RID: 10584
		internal const uint kEventParamMouseButton = 1835168878U;

		// Token: 0x04002959 RID: 10585
		internal const uint kEventParamMouseWheelAxis = 1836540280U;

		// Token: 0x0400295A RID: 10586
		internal const uint kEventParamMouseWheelDelta = 1836541036U;

		// Token: 0x0400295B RID: 10587
		internal const uint typeLongInteger = 1819242087U;

		// Token: 0x0400295C RID: 10588
		internal const uint typeMouseWheelAxis = 1836540280U;

		// Token: 0x0400295D RID: 10589
		internal const uint typeMouseButton = 1835168878U;

		// Token: 0x0400295E RID: 10590
		internal const uint typeQDPoint = 1363439732U;

		// Token: 0x0400295F RID: 10591
		internal const uint kEventMouseWheelAxisX = 0U;

		// Token: 0x04002960 RID: 10592
		internal const uint kEventMouseWheelAxisY = 1U;

		// Token: 0x04002961 RID: 10593
		internal const uint DoubleClickInterval = 7500000U;

		// Token: 0x04002962 RID: 10594
		internal static ClickStruct ClickPending;
	}
}
