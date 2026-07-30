using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms.CarbonInternal;

namespace System.Windows.Forms
{
	// Token: 0x02000448 RID: 1096
	internal class XplatUICarbon : XplatUIDriver
	{
		// Token: 0x0600470A RID: 18186 RVA: 0x00115AB8 File Offset: 0x00113CB8
		private XplatUICarbon()
		{
			XplatUICarbon.RefCount = 0;
			this.TimerList = new ArrayList();
			XplatUICarbon.in_doevents = false;
			XplatUICarbon.MessageQueue = new Queue();
			this.Initialize();
		}

		// Token: 0x1400046F RID: 1135
		// (add) Token: 0x0600470C RID: 18188 RVA: 0x00115B10 File Offset: 0x00113D10
		// (remove) Token: 0x0600470D RID: 18189 RVA: 0x00115B2C File Offset: 0x00113D2C
		internal override event EventHandler Idle;

		// Token: 0x0600470E RID: 18190 RVA: 0x00115B48 File Offset: 0x00113D48
		~XplatUICarbon()
		{
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x00115B80 File Offset: 0x00113D80
		public static XplatUICarbon GetInstance()
		{
			object obj = XplatUICarbon.instancelock;
			lock (obj)
			{
				if (XplatUICarbon.Instance == null)
				{
					XplatUICarbon.Instance = new XplatUICarbon();
				}
				XplatUICarbon.RefCount++;
			}
			return XplatUICarbon.Instance;
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06004710 RID: 18192 RVA: 0x00115BE8 File Offset: 0x00113DE8
		public int Reference
		{
			get
			{
				return XplatUICarbon.RefCount;
			}
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x00115BF0 File Offset: 0x00113DF0
		internal void AddExpose(Hwnd hwnd, bool client, HIRect rect)
		{
			this.AddExpose(hwnd, client, (int)rect.origin.x, (int)rect.origin.y, (int)rect.size.width, (int)rect.size.height);
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x00115C3C File Offset: 0x00113E3C
		internal void AddExpose(Hwnd hwnd, bool client, Rectangle rect)
		{
			this.AddExpose(hwnd, client, rect.X, rect.Y, rect.Width, rect.Height);
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x00115C70 File Offset: 0x00113E70
		internal void FlushQueue()
		{
			this.CheckTimers(DateTime.UtcNow);
			object obj = XplatUICarbon.queuelock;
			lock (obj)
			{
				while (XplatUICarbon.MessageQueue.Count > 0)
				{
					object obj2 = XplatUICarbon.MessageQueue.Dequeue();
					if (obj2 is GCHandle)
					{
						XplatUIDriverSupport.ExecuteClientMessage((GCHandle)obj2);
					}
					else
					{
						MSG msg = (MSG)obj2;
						NativeWindow.WndProc(msg.hwnd, msg.message, msg.wParam, msg.lParam);
					}
				}
			}
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00115D20 File Offset: 0x00113F20
		internal static Rectangle[] GetClippingRectangles(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return null;
			}
			if (hwnd.Handle != handle)
			{
				return new Rectangle[] { hwnd.ClientRect };
			}
			return (Rectangle[])hwnd.GetClippingRectangles().ToArray(typeof(Rectangle));
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x00115D80 File Offset: 0x00113F80
		internal IntPtr GetMousewParam(int Delta)
		{
			int num = 0;
			if ((XplatUICarbon.MouseState & MouseButtons.Left) != MouseButtons.None)
			{
				num |= 1;
			}
			if ((XplatUICarbon.MouseState & MouseButtons.Middle) != MouseButtons.None)
			{
				num |= 16;
			}
			if ((XplatUICarbon.MouseState & MouseButtons.Right) != MouseButtons.None)
			{
				num |= 2;
			}
			Keys modifierKeys = this.ModifierKeys;
			if ((modifierKeys & Keys.Control) != Keys.None)
			{
				num |= 8;
			}
			if ((modifierKeys & Keys.Shift) != Keys.None)
			{
				num |= 4;
			}
			num |= Delta << 16;
			return (IntPtr)num;
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x00115E00 File Offset: 0x00114000
		internal IntPtr HandleToWindow(IntPtr handle)
		{
			if (XplatUICarbon.HandleMapping[handle] != null)
			{
				return (IntPtr)XplatUICarbon.HandleMapping[handle];
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x00115E40 File Offset: 0x00114040
		internal void Initialize()
		{
			EventHandler.Driver = this;
			this.ApplicationHandler = new ApplicationHandler(this);
			this.ControlHandler = new ControlHandler(this);
			this.HIObjectHandler = new HIObjectHandler(this);
			this.KeyboardHandler = new KeyboardHandler(this);
			this.MouseHandler = new MouseHandler(this);
			this.WindowHandler = new WindowHandler(this);
			XplatUICarbon.Hover.Interval = 500;
			XplatUICarbon.Hover.Timer = new Timer();
			XplatUICarbon.Hover.Timer.Enabled = false;
			XplatUICarbon.Hover.Timer.Interval = XplatUICarbon.Hover.Interval;
			XplatUICarbon.Hover.Timer.Tick += new EventHandler(this.HoverCallback);
			XplatUICarbon.Hover.X = -1;
			XplatUICarbon.Hover.Y = -1;
			XplatUICarbon.MouseState = MouseButtons.None;
			this.mouse_position = Point.Empty;
			XplatUICarbon.Caret.Timer = new Timer();
			XplatUICarbon.Caret.Timer.Interval = 500;
			XplatUICarbon.Caret.Timer.Tick += new EventHandler(this.CaretCallback);
			XplatUICarbon.Dnd = new Dnd();
			XplatUICarbon.WindowMapping = new Hashtable();
			XplatUICarbon.HandleMapping = new Hashtable();
			XplatUICarbon.UtilityWindows = new ArrayList();
			Rect rect = default(Rect);
			XplatUICarbon.SetRect(ref rect, 0, 0, 0, 0);
			ProcessSerialNumber processSerialNumber = default(ProcessSerialNumber);
			XplatUICarbon.GetCurrentProcess(ref processSerialNumber);
			XplatUICarbon.TransformProcessType(ref processSerialNumber, 1U);
			XplatUICarbon.SetFrontProcess(ref processSerialNumber);
			XplatUICarbon.HIObjectRegisterSubclass(XplatUICarbon.__CFStringMakeConstantString("com.novell.mwfview"), XplatUICarbon.__CFStringMakeConstantString("com.apple.hiview"), 0U, EventHandler.EventHandlerDelegate, (uint)EventHandler.HIObjectEvents.Length, EventHandler.HIObjectEvents, IntPtr.Zero, ref XplatUICarbon.Subclass);
			EventHandler.InstallApplicationHandler();
			XplatUICarbon.CreateNewWindow(WindowClass.kDocumentWindowClass, (WindowAttributes)34078751U, ref rect, ref XplatUICarbon.FosterParent);
			XplatUICarbon.CreateNewWindow(WindowClass.kOverlayWindowClass, (WindowAttributes)196608U, ref rect, ref XplatUICarbon.ReverseWindow);
			XplatUICarbon.CreateNewWindow(WindowClass.kOverlayWindowClass, (WindowAttributes)196608U, ref rect, ref XplatUICarbon.CaretWindow);
			Rect rect2 = default(Rect);
			Rect rect3 = default(Rect);
			XplatUICarbon.GetWindowBounds(XplatUICarbon.FosterParent, 32U, ref rect2);
			XplatUICarbon.GetWindowBounds(XplatUICarbon.FosterParent, 33U, ref rect3);
			XplatUICarbon.MenuBarHeight = (int)XplatUICarbon.GetMBarHeight();
			XplatUICarbon.FocusWindow = IntPtr.Zero;
			XplatUICarbon.GetMessageResult = true;
			XplatUICarbon.ReverseWindowMapped = false;
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x00116084 File Offset: 0x00114284
		internal void PerformNCCalc(Hwnd hwnd)
		{
			Rectangle rectangle;
			rectangle..ctor(0, 0, hwnd.Width, hwnd.Height);
			XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = default(XplatUIWin32.NCCALCSIZE_PARAMS);
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(nccalcsize_PARAMS));
			nccalcsize_PARAMS.rgrc1.left = rectangle.Left;
			nccalcsize_PARAMS.rgrc1.top = rectangle.Top;
			nccalcsize_PARAMS.rgrc1.right = rectangle.Right;
			nccalcsize_PARAMS.rgrc1.bottom = rectangle.Bottom;
			Marshal.StructureToPtr(nccalcsize_PARAMS, intPtr, true);
			NativeWindow.WndProc(hwnd.client_window, Msg.WM_NCCALCSIZE, (IntPtr)1, intPtr);
			nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(intPtr, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
			Marshal.FreeHGlobal(intPtr);
			rectangle..ctor(nccalcsize_PARAMS.rgrc1.left, nccalcsize_PARAMS.rgrc1.top, nccalcsize_PARAMS.rgrc1.right - nccalcsize_PARAMS.rgrc1.left, nccalcsize_PARAMS.rgrc1.bottom - nccalcsize_PARAMS.rgrc1.top);
			hwnd.ClientRect = rectangle;
			rectangle = XplatUICarbon.TranslateClientRectangleToQuartzClientRectangle(hwnd);
			if (hwnd.visible)
			{
				HIRect hirect = new HIRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
				XplatUICarbon.HIViewSetFrame(hwnd.client_window, ref hirect);
			}
			this.AddExpose(hwnd, false, 0, 0, hwnd.Width, hwnd.Height);
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x00116200 File Offset: 0x00114400
		internal void ScreenToClient(IntPtr handle, ref QDPoint point)
		{
			int x = (int)point.x;
			int y = (int)point.y;
			this.ScreenToClient(handle, ref x, ref y);
			point.x = (short)x;
			point.y = (short)y;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x00116238 File Offset: 0x00114438
		internal static Rectangle TranslateClientRectangleToQuartzClientRectangle(Hwnd hwnd)
		{
			return XplatUICarbon.TranslateClientRectangleToQuartzClientRectangle(hwnd, Control.FromHandle(hwnd.Handle));
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x0011624C File Offset: 0x0011444C
		internal static Rectangle TranslateClientRectangleToQuartzClientRectangle(Hwnd hwnd, Control ctrl)
		{
			Rectangle rectangle = hwnd.ClientRect;
			Form form = ctrl as Form;
			CreateParams createParams = null;
			if (form != null)
			{
				createParams = form.GetCreateParams();
			}
			if (form != null && (form.window_manager == null || createParams.IsSet(WindowExStyles.WS_EX_TOOLWINDOW)))
			{
				Hwnd.Borders borders = Hwnd.GetBorders(createParams, null);
				Rectangle rectangle2 = rectangle;
				rectangle2.Y -= borders.top;
				rectangle2.X -= borders.left;
				rectangle2.Width += borders.left + borders.right;
				rectangle2.Height += borders.top + borders.bottom;
				rectangle = rectangle2;
			}
			if (rectangle.Width < 1 || rectangle.Height < 1)
			{
				rectangle.Width = 1;
				rectangle.Height = 1;
				rectangle.X = -5;
				rectangle.Y = -5;
			}
			return rectangle;
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x00116348 File Offset: 0x00114548
		internal static Size TranslateWindowSizeToQuartzWindowSize(CreateParams cp)
		{
			return XplatUICarbon.TranslateWindowSizeToQuartzWindowSize(cp, new Size(cp.Width, cp.Height));
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x00116364 File Offset: 0x00114564
		internal static Size TranslateWindowSizeToQuartzWindowSize(CreateParams cp, Size size)
		{
			Form form = cp.control as Form;
			if (form != null && (form.window_manager == null || cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW)))
			{
				Hwnd.Borders borders = Hwnd.GetBorders(cp, null);
				Size size2 = size;
				size2.Width -= borders.left + borders.right;
				size2.Height -= borders.top + borders.bottom;
				size = size2;
			}
			if (size.Height == 0)
			{
				size.Height = 1;
			}
			if (size.Width == 0)
			{
				size.Width = 1;
			}
			return size;
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00116410 File Offset: 0x00114610
		internal static Size TranslateQuartzWindowSizeToWindowSize(CreateParams cp, int width, int height)
		{
			Size size;
			size..ctor(width, height);
			Form form = cp.control as Form;
			if (form != null && (form.window_manager == null || cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW)))
			{
				Hwnd.Borders borders = Hwnd.GetBorders(cp, null);
				Size size2 = size;
				size2.Width += borders.left + borders.right;
				size2.Height += borders.top + borders.bottom;
				size = size2;
			}
			return size;
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x0011649C File Offset: 0x0011469C
		private void CaretCallback(object sender, EventArgs e)
		{
			if (XplatUICarbon.Caret.Paused)
			{
				return;
			}
			if (!XplatUICarbon.Caret.On)
			{
				this.ShowCaret();
			}
			else
			{
				this.HideCaret();
			}
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x001164DC File Offset: 0x001146DC
		private void HoverCallback(object sender, EventArgs e)
		{
			if (XplatUICarbon.Hover.X == this.mouse_position.X && XplatUICarbon.Hover.Y == this.mouse_position.Y)
			{
				this.EnqueueMessage(new MSG
				{
					hwnd = XplatUICarbon.Hover.Hwnd,
					message = Msg.WM_MOUSEHOVER,
					wParam = this.GetMousewParam(0),
					lParam = (IntPtr)(((int)((ushort)XplatUICarbon.Hover.X) << 16) | (int)((ushort)XplatUICarbon.Hover.X))
				});
			}
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x00116580 File Offset: 0x00114780
		private Point ConvertScreenPointToClient(IntPtr handle, Point point)
		{
			Point point2 = default(Point);
			Rect rect = default(Rect);
			CGPoint cgpoint = default(CGPoint);
			XplatUICarbon.GetWindowBounds(XplatUICarbon.HIViewGetWindow(handle), 32U, ref rect);
			cgpoint.x = (float)(point.X - (int)rect.left);
			cgpoint.y = (float)(point.Y - (int)rect.top);
			XplatUICarbon.HIViewConvertPoint(ref cgpoint, IntPtr.Zero, handle);
			point2.X = (int)cgpoint.x;
			point2.Y = (int)cgpoint.y;
			return point2;
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x00116610 File Offset: 0x00114810
		private Point ConvertClientPointToScreen(IntPtr handle, Point point)
		{
			Point point2 = default(Point);
			Rect rect = default(Rect);
			CGPoint cgpoint = default(CGPoint);
			XplatUICarbon.GetWindowBounds(XplatUICarbon.HIViewGetWindow(handle), 32U, ref rect);
			cgpoint.x = (float)point.X;
			cgpoint.y = (float)point.Y;
			XplatUICarbon.HIViewConvertPoint(ref cgpoint, handle, IntPtr.Zero);
			point2.X = (int)(cgpoint.x + (float)rect.left);
			point2.Y = (int)(cgpoint.y + (float)rect.top);
			return point2;
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x001166A4 File Offset: 0x001148A4
		private double NextTimeout()
		{
			DateTime utcNow = DateTime.UtcNow;
			int num = 134217727;
			ArrayList timerList = this.TimerList;
			lock (timerList)
			{
				foreach (object obj in this.TimerList)
				{
					Timer timer = (Timer)obj;
					int num2 = (int)(timer.Expires - utcNow).TotalMilliseconds;
					if (num2 < 0)
					{
						return 0.0;
					}
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			if (num < Timer.Minimum)
			{
				num = Timer.Minimum;
			}
			return (double)num / 1000.0;
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x001167AC File Offset: 0x001149AC
		private void CheckTimers(DateTime now)
		{
			ArrayList timerList = this.TimerList;
			lock (timerList)
			{
				if (this.TimerList.Count != 0)
				{
					for (int i = 0; i < this.TimerList.Count; i++)
					{
						Timer timer = (Timer)this.TimerList[i];
						if (timer.Enabled && timer.Expires <= now && (XplatUICarbon.in_doevents || (Application.MWFThread.Current.Context != null && Application.MWFThread.Current.Context.MainForm != null && Application.MWFThread.Current.Context.MainForm.IsLoaded)))
						{
							timer.FireTick();
							timer.Update(now);
						}
					}
				}
			}
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x001168A4 File Offset: 0x00114AA4
		private void WaitForHwndMessage(Hwnd hwnd, Msg message)
		{
			MSG msg = default(MSG);
			bool flag = false;
			do
			{
				if (this.GetMessage(null, ref msg, IntPtr.Zero, 0, 0))
				{
					if (msg.message == Msg.WM_QUIT)
					{
						this.PostQuitMessage(0);
						flag = true;
					}
					else
					{
						if (msg.hwnd == hwnd.Handle)
						{
							if (msg.message == message)
							{
								break;
							}
							if (msg.message == Msg.WM_DESTROY)
							{
								flag = true;
							}
						}
						this.TranslateMessage(ref msg);
						this.DispatchMessage(ref msg);
					}
				}
			}
			while (!flag);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x0011693C File Offset: 0x00114B3C
		private void SendParentNotify(IntPtr child, Msg cause, int x, int y)
		{
			if (child == IntPtr.Zero)
			{
				return;
			}
			Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(child);
			if (objectFromWindow == null)
			{
				return;
			}
			if (objectFromWindow.Handle == IntPtr.Zero)
			{
				return;
			}
			if (this.ExStyleSet((int)objectFromWindow.initial_ex_style, WindowExStyles.WS_EX_NOPARENTNOTIFY))
			{
				return;
			}
			if (objectFromWindow.Parent == null)
			{
				return;
			}
			if (objectFromWindow.Parent.Handle == IntPtr.Zero)
			{
				return;
			}
			if (cause == Msg.WM_CREATE || cause == Msg.WM_DESTROY)
			{
				this.SendMessage(objectFromWindow.Parent.Handle, Msg.WM_PARENTNOTIFY, Control.MakeParam((int)cause, 0), child);
			}
			else
			{
				this.SendMessage(objectFromWindow.Parent.Handle, Msg.WM_PARENTNOTIFY, Control.MakeParam((int)cause, 0), Control.MakeParam(x, y));
			}
			this.SendParentNotify(objectFromWindow.Parent.Handle, cause, x, y);
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x00116A28 File Offset: 0x00114C28
		private bool StyleSet(int s, WindowStyles ws)
		{
			return (s & (int)ws) == (int)ws;
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x00116A30 File Offset: 0x00114C30
		private bool ExStyleSet(int ex, WindowExStyles exws)
		{
			return (ex & (int)exws) == (int)exws;
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x00116A38 File Offset: 0x00114C38
		private void DeriveStyles(int Style, int ExStyle, out FormBorderStyle border_style, out bool border_static, out TitleStyle title_style, out int caption_height, out int tool_caption_height)
		{
			caption_height = 0;
			tool_caption_height = 0;
			border_static = false;
			if (this.StyleSet(Style, WindowStyles.WS_CHILD))
			{
				if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_CLIENTEDGE))
				{
					border_style = FormBorderStyle.Fixed3D;
				}
				else if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_STATICEDGE))
				{
					border_style = FormBorderStyle.Fixed3D;
					border_static = true;
				}
				else if (!this.StyleSet(Style, WindowStyles.WS_BORDER))
				{
					border_style = FormBorderStyle.None;
				}
				else
				{
					border_style = FormBorderStyle.FixedSingle;
				}
				title_style = TitleStyle.None;
				if (this.StyleSet(Style, WindowStyles.WS_CAPTION))
				{
					caption_height = 0;
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						title_style = TitleStyle.Tool;
					}
					else
					{
						title_style = TitleStyle.Normal;
					}
				}
				if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_MDICHILD))
				{
					caption_height = 0;
					if (this.StyleSet(Style, WindowStyles.WS_OVERLAPPEDWINDOW) || this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						border_style = (FormBorderStyle)65535;
					}
					else
					{
						border_style = FormBorderStyle.None;
					}
				}
			}
			else
			{
				title_style = TitleStyle.None;
				if (this.StyleSet(Style, WindowStyles.WS_CAPTION))
				{
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						title_style = TitleStyle.Tool;
					}
					else
					{
						title_style = TitleStyle.Normal;
					}
				}
				border_style = FormBorderStyle.None;
				if (this.StyleSet(Style, WindowStyles.WS_THICKFRAME))
				{
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						border_style = FormBorderStyle.SizableToolWindow;
					}
					else
					{
						border_style = FormBorderStyle.Sizable;
					}
				}
				else if (this.StyleSet(Style, WindowStyles.WS_CAPTION))
				{
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_CLIENTEDGE))
					{
						border_style = FormBorderStyle.Fixed3D;
					}
					else if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_STATICEDGE))
					{
						border_style = FormBorderStyle.Fixed3D;
						border_static = true;
					}
					else if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_DLGMODALFRAME))
					{
						border_style = FormBorderStyle.FixedDialog;
					}
					else if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						border_style = FormBorderStyle.FixedToolWindow;
					}
					else if (this.StyleSet(Style, WindowStyles.WS_BORDER))
					{
						border_style = FormBorderStyle.FixedSingle;
					}
				}
				else if (this.StyleSet(Style, WindowStyles.WS_BORDER))
				{
					border_style = FormBorderStyle.FixedSingle;
				}
			}
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x00116C40 File Offset: 0x00114E40
		private void SetHwndStyles(Hwnd hwnd, CreateParams cp)
		{
			this.DeriveStyles(cp.Style, cp.ExStyle, out hwnd.border_style, out hwnd.border_static, out hwnd.title_style, out hwnd.caption_height, out hwnd.tool_caption_height);
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x00116C80 File Offset: 0x00114E80
		private void ShowCaret()
		{
			if (XplatUICarbon.Caret.On)
			{
				return;
			}
			XplatUICarbon.Caret.On = true;
			XplatUICarbon.ShowWindow(XplatUICarbon.CaretWindow);
			Graphics graphics = Graphics.FromHwnd(XplatUICarbon.HIViewGetRoot(XplatUICarbon.CaretWindow));
			graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, XplatUICarbon.Caret.Width, XplatUICarbon.Caret.Height));
			graphics.Dispose();
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x00116CF4 File Offset: 0x00114EF4
		private void HideCaret()
		{
			if (!XplatUICarbon.Caret.On)
			{
				return;
			}
			XplatUICarbon.Caret.On = false;
			XplatUICarbon.HideWindow(XplatUICarbon.CaretWindow);
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x00116D28 File Offset: 0x00114F28
		private void AccumulateDestroyedHandles(Control c, ArrayList list)
		{
			if (c != null)
			{
				Control[] allControls = c.Controls.GetAllControls();
				if (c.IsHandleCreated && !c.IsDisposed)
				{
					Hwnd hwnd = Hwnd.ObjectFromHandle(c.Handle);
					list.Add(hwnd);
					this.CleanupCachedWindows(hwnd);
				}
				for (int i = 0; i < allControls.Length; i++)
				{
					this.AccumulateDestroyedHandles(allControls[i], list);
				}
			}
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00116D98 File Offset: 0x00114F98
		private void CleanupCachedWindows(Hwnd hwnd)
		{
			if (XplatUICarbon.ActiveWindow == hwnd.Handle)
			{
				this.SendMessage(hwnd.client_window, Msg.WM_ACTIVATE, (IntPtr)0, IntPtr.Zero);
				XplatUICarbon.ActiveWindow = IntPtr.Zero;
			}
			if (XplatUICarbon.FocusWindow == hwnd.Handle)
			{
				this.SendMessage(hwnd.client_window, Msg.WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
				XplatUICarbon.FocusWindow = IntPtr.Zero;
			}
			if (XplatUICarbon.Grab.Hwnd == hwnd.Handle)
			{
				XplatUICarbon.Grab.Hwnd = IntPtr.Zero;
				XplatUICarbon.Grab.Confined = false;
			}
			this.DestroyCaret(hwnd.Handle);
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x00116E54 File Offset: 0x00115054
		private void AddExpose(Hwnd hwnd, bool client, int x, int y, int width, int height)
		{
			if (hwnd == null || x > hwnd.Width || y > hwnd.Height || x + width < 0 || y + height < 0)
			{
				return;
			}
			if (x + width > hwnd.width)
			{
				width = hwnd.width - x;
			}
			if (y + height > hwnd.height)
			{
				height = hwnd.height - y;
			}
			if (client)
			{
				hwnd.AddInvalidArea(x, y, width, height);
				if (!hwnd.expose_pending && hwnd.visible)
				{
					this.EnqueueMessage(new MSG
					{
						message = Msg.WM_PAINT,
						hwnd = hwnd.Handle
					});
					hwnd.expose_pending = true;
				}
			}
			else
			{
				hwnd.AddNcInvalidArea(x, y, width, height);
				if (!hwnd.nc_expose_pending && hwnd.visible)
				{
					MSG msg = default(MSG);
					Region region = new Region(hwnd.Invalid);
					IntPtr hrgn = region.GetHrgn(null);
					msg.message = Msg.WM_NCPAINT;
					msg.wParam = ((!(hrgn == IntPtr.Zero)) ? hrgn : ((IntPtr)1));
					msg.refobject = region;
					msg.hwnd = hwnd.Handle;
					this.EnqueueMessage(msg);
					hwnd.nc_expose_pending = true;
				}
			}
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x00116FB8 File Offset: 0x001151B8
		internal void EnqueueMessage(MSG msg)
		{
			object obj = XplatUICarbon.queuelock;
			lock (obj)
			{
				XplatUICarbon.MessageQueue.Enqueue(msg);
			}
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x0011700C File Offset: 0x0011520C
		internal override void RaiseIdle(EventArgs e)
		{
			if (this.Idle != null)
			{
				this.Idle.Invoke(this, e);
			}
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x00117028 File Offset: 0x00115228
		internal override IntPtr InitializeDriver()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x00117030 File Offset: 0x00115230
		internal override void ShutdownDriver(IntPtr token)
		{
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x00117034 File Offset: 0x00115234
		internal override void EnableThemes()
		{
			XplatUICarbon.themes_enabled = true;
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x0011703C File Offset: 0x0011523C
		internal override void Activate(IntPtr handle)
		{
			if (XplatUICarbon.ActiveWindow != IntPtr.Zero)
			{
				XplatUICarbon.ActivateWindow(XplatUICarbon.HIViewGetWindow(XplatUICarbon.ActiveWindow), false);
			}
			XplatUICarbon.ActivateWindow(XplatUICarbon.HIViewGetWindow(handle), true);
			XplatUICarbon.ActiveWindow = handle;
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x00117084 File Offset: 0x00115284
		internal override void AudibleAlert(AlertType alert)
		{
			XplatUICarbon.AlertSoundPlay();
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x0011708C File Offset: 0x0011528C
		internal override void CaretVisible(IntPtr hwnd, bool visible)
		{
			if (XplatUICarbon.Caret.Hwnd == hwnd)
			{
				if (visible)
				{
					if (XplatUICarbon.Caret.Visible < 1)
					{
						XplatUICarbon.Caret.Visible = XplatUICarbon.Caret.Visible + 1;
						XplatUICarbon.Caret.On = false;
						if (XplatUICarbon.Caret.Visible == 1)
						{
							this.ShowCaret();
							XplatUICarbon.Caret.Timer.Start();
						}
					}
				}
				else
				{
					XplatUICarbon.Caret.Visible = XplatUICarbon.Caret.Visible - 1;
					if (XplatUICarbon.Caret.Visible == 0)
					{
						XplatUICarbon.Caret.Timer.Stop();
						this.HideCaret();
					}
				}
			}
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x00117144 File Offset: 0x00115344
		internal override bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect)
		{
			WindowRect = Hwnd.GetWindowRectangle(cp, menu, ClientRect);
			return true;
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x0011715C File Offset: 0x0011535C
		internal override void ClientToScreen(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Point point = this.ConvertClientPointToScreen(hwnd.ClientWindow, new Point(x, y));
			x = point.X;
			y = point.Y;
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x00117198 File Offset: 0x00115398
		internal override void MenuToScreen(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Point point = this.ConvertClientPointToScreen(hwnd.ClientWindow, new Point(x, y));
			x = point.X;
			y = point.Y;
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x001171D4 File Offset: 0x001153D4
		internal override int[] ClipboardAvailableFormats(IntPtr handle)
		{
			ArrayList arrayList = new ArrayList();
			for (DataFormats.Format format = DataFormats.Format.List; format != null; format = format.Next)
			{
				arrayList.Add(format.Id);
			}
			return (int[])arrayList.ToArray(typeof(int));
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x00117228 File Offset: 0x00115428
		internal override void ClipboardClose(IntPtr handle)
		{
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x0011722C File Offset: 0x0011542C
		internal override int ClipboardGetID(IntPtr handle, string format)
		{
			return (int)XplatUICarbon.__CFStringMakeConstantString(format);
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x0011723C File Offset: 0x0011543C
		internal override IntPtr ClipboardOpen(bool primary_selection)
		{
			if (primary_selection)
			{
				return Pasteboard.Primary;
			}
			return Pasteboard.Application;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00117250 File Offset: 0x00115450
		internal override object ClipboardRetrieve(IntPtr handle, int type, XplatUI.ClipboardToObject converter)
		{
			return Pasteboard.Retrieve(handle, type);
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x0011725C File Offset: 0x0011545C
		internal override void ClipboardStore(IntPtr handle, object obj, int type, XplatUI.ObjectToClipboard converter)
		{
			Pasteboard.Store(handle, obj, type);
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x00117268 File Offset: 0x00115468
		internal override void CreateCaret(IntPtr hwnd, int width, int height)
		{
			if (XplatUICarbon.Caret.Hwnd != IntPtr.Zero)
			{
				this.DestroyCaret(XplatUICarbon.Caret.Hwnd);
			}
			XplatUICarbon.Caret.Hwnd = hwnd;
			XplatUICarbon.Caret.Width = width;
			XplatUICarbon.Caret.Height = height;
			XplatUICarbon.Caret.Visible = 0;
			XplatUICarbon.Caret.On = false;
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x001172D8 File Offset: 0x001154D8
		internal override IntPtr CreateWindow(CreateParams cp)
		{
			Hwnd hwnd = null;
			Hwnd hwnd2 = new Hwnd();
			int num = cp.X;
			int num2 = cp.Y;
			int num3 = cp.Width;
			int num4 = cp.Height;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			IntPtr zero4 = IntPtr.Zero;
			if (num3 < 1)
			{
				num3 = 1;
			}
			if (num4 < 1)
			{
				num4 = 1;
			}
			if (cp.Parent != IntPtr.Zero)
			{
				hwnd = Hwnd.ObjectFromHandle(cp.Parent);
				intPtr = hwnd.client_window;
			}
			else if (this.StyleSet(cp.Style, WindowStyles.WS_CHILD))
			{
				XplatUICarbon.HIViewFindByID(XplatUICarbon.HIViewGetRoot(XplatUICarbon.FosterParent), new HIViewID(2003398244U, 1U), ref intPtr);
			}
			if (cp.control is Form)
			{
				Point nextStackedFormLocation = Hwnd.GetNextStackedFormLocation(cp, hwnd);
				num = nextStackedFormLocation.X;
				num2 = nextStackedFormLocation.Y;
			}
			hwnd2.x = num;
			hwnd2.y = num2;
			hwnd2.width = num3;
			hwnd2.height = num4;
			hwnd2.Parent = Hwnd.ObjectFromHandle(cp.Parent);
			hwnd2.initial_style = cp.WindowStyle;
			hwnd2.initial_ex_style = cp.WindowExStyle;
			hwnd2.visible = false;
			if (this.StyleSet(cp.Style, WindowStyles.WS_DISABLED))
			{
				hwnd2.enabled = false;
			}
			intPtr2 = IntPtr.Zero;
			Size size = XplatUICarbon.TranslateWindowSizeToQuartzWindowSize(cp);
			Rectangle rectangle = XplatUICarbon.TranslateClientRectangleToQuartzClientRectangle(hwnd2, cp.control);
			this.SetHwndStyles(hwnd2, cp);
			if (intPtr == IntPtr.Zero)
			{
				IntPtr zero5 = IntPtr.Zero;
				IntPtr zero6 = IntPtr.Zero;
				WindowClass windowClass = WindowClass.kOverlayWindowClass;
				WindowAttributes windowAttributes = (WindowAttributes)34078720U;
				if (this.StyleSet(cp.Style, WindowStyles.WS_GROUP))
				{
					windowAttributes |= WindowAttributes.kWindowCollapseBoxAttribute;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_TABSTOP))
				{
					windowAttributes |= (WindowAttributes)22U;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_SYSMENU))
				{
					windowAttributes |= WindowAttributes.kWindowCloseBoxAttribute;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_CAPTION))
				{
					windowClass = WindowClass.kDocumentWindowClass;
				}
				if (hwnd2.border_style == FormBorderStyle.FixedToolWindow)
				{
					windowClass = WindowClass.kUtilityWindowClass;
				}
				else if (hwnd2.border_style == FormBorderStyle.SizableToolWindow)
				{
					windowAttributes |= WindowAttributes.kWindowResizableAttribute;
					windowClass = WindowClass.kUtilityWindowClass;
				}
				if (windowClass == WindowClass.kOverlayWindowClass)
				{
					windowAttributes = (WindowAttributes)34078720U;
				}
				windowAttributes |= WindowAttributes.kWindowLiveResizeAttribute;
				Rect rect = default(Rect);
				if (this.StyleSet(cp.Style, WindowStyles.WS_POPUP))
				{
					XplatUICarbon.SetRect(ref rect, (short)num, (short)num2, (short)(num + size.Width), (short)(num2 + size.Height));
				}
				else
				{
					XplatUICarbon.SetRect(ref rect, (short)num, (short)(num2 + XplatUICarbon.MenuBarHeight), (short)(num + size.Width), (short)(num2 + XplatUICarbon.MenuBarHeight + size.Height));
				}
				XplatUICarbon.CreateNewWindow(windowClass, windowAttributes, ref rect, ref zero);
				EventHandler.InstallWindowHandler(zero);
				XplatUICarbon.HIViewFindByID(XplatUICarbon.HIViewGetRoot(zero), new HIViewID(2003398244U, 1U), ref zero5);
				XplatUICarbon.HIViewFindByID(XplatUICarbon.HIViewGetRoot(zero), new HIViewID(2003398244U, 7U), ref zero6);
				XplatUICarbon.HIGrowBoxViewSetTransparent(zero6, true);
				XplatUICarbon.SetAutomaticControlDragTrackingEnabledForWindow(zero, true);
				intPtr = zero5;
			}
			XplatUICarbon.HIObjectCreate(XplatUICarbon.__CFStringMakeConstantString("com.novell.mwfview"), 0U, ref zero2);
			XplatUICarbon.HIObjectCreate(XplatUICarbon.__CFStringMakeConstantString("com.novell.mwfview"), 0U, ref intPtr2);
			EventHandler.InstallControlHandler(zero2);
			EventHandler.InstallControlHandler(intPtr2);
			XplatUICarbon.HIViewChangeFeatures(zero2, 2UL, 0UL);
			XplatUICarbon.HIViewChangeFeatures(intPtr2, 2UL, 0UL);
			XplatUICarbon.HIViewNewTrackingArea(zero2, IntPtr.Zero, (ulong)(long)zero2, ref zero3);
			XplatUICarbon.HIViewNewTrackingArea(intPtr2, IntPtr.Zero, (ulong)(long)intPtr2, ref zero4);
			HIRect hirect;
			if (zero != IntPtr.Zero)
			{
				hirect = new HIRect(0, 0, size.Width, size.Height);
			}
			else
			{
				hirect = new HIRect(num, num2, size.Width, size.Height);
			}
			HIRect hirect2 = new HIRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			XplatUICarbon.HIViewSetFrame(zero2, ref hirect);
			XplatUICarbon.HIViewSetFrame(intPtr2, ref hirect2);
			XplatUICarbon.HIViewAddSubview(intPtr, zero2);
			XplatUICarbon.HIViewAddSubview(zero2, intPtr2);
			hwnd2.WholeWindow = zero2;
			hwnd2.ClientWindow = intPtr2;
			if (zero != IntPtr.Zero)
			{
				XplatUICarbon.WindowMapping[hwnd2.Handle] = zero;
				XplatUICarbon.HandleMapping[zero] = hwnd2.Handle;
				if (hwnd2.border_style == FormBorderStyle.FixedToolWindow || hwnd2.border_style == FormBorderStyle.SizableToolWindow)
				{
					XplatUICarbon.UtilityWindows.Add(zero);
				}
			}
			XplatUICarbon.Dnd.SetAllowDrop(hwnd2, true);
			this.Text(hwnd2.Handle, cp.Caption);
			this.SendMessage(hwnd2.Handle, Msg.WM_CREATE, (IntPtr)1, IntPtr.Zero);
			this.SendParentNotify(hwnd2.Handle, Msg.WM_CREATE, int.MaxValue, int.MaxValue);
			if (this.StyleSet(cp.Style, WindowStyles.WS_VISIBLE))
			{
				if (zero != IntPtr.Zero)
				{
					if (Control.FromHandle(hwnd2.Handle) is Form)
					{
						Form form = Control.FromHandle(hwnd2.Handle) as Form;
						if (form.WindowState == FormWindowState.Normal)
						{
							this.SendMessage(hwnd2.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
						}
					}
					XplatUICarbon.ShowWindow(zero);
					this.WaitForHwndMessage(hwnd2, Msg.WM_SHOWWINDOW);
				}
				XplatUICarbon.HIViewSetVisible(zero2, true);
				XplatUICarbon.HIViewSetVisible(intPtr2, true);
				hwnd2.visible = true;
				if (!(Control.FromHandle(hwnd2.Handle) is Form))
				{
					this.SendMessage(hwnd2.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
				}
			}
			if (this.StyleSet(cp.Style, WindowStyles.WS_MINIMIZE))
			{
				this.SetWindowState(hwnd2.Handle, FormWindowState.Minimized);
			}
			else if (this.StyleSet(cp.Style, WindowStyles.WS_MAXIMIZE))
			{
				this.SetWindowState(hwnd2.Handle, FormWindowState.Maximized);
			}
			return hwnd2.Handle;
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x00117904 File Offset: 0x00115B04
		internal override IntPtr CreateWindow(IntPtr Parent, int X, int Y, int Width, int Height)
		{
			return this.CreateWindow(new CreateParams
			{
				Caption = string.Empty,
				X = X,
				Y = Y,
				Width = Width,
				Height = Height,
				ClassName = XplatUI.DefaultClassName,
				ClassStyle = 0,
				ExStyle = 0,
				Parent = IntPtr.Zero,
				Param = 0
			});
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x00117978 File Offset: 0x00115B78
		internal override Bitmap DefineStdCursorBitmap(StdCursor id)
		{
			return Cursor.DefineStdCursorBitmap(id);
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x00117980 File Offset: 0x00115B80
		internal override IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			return Cursor.DefineCursor(bitmap, mask, cursor_pixel, mask_pixel, xHotSpot, yHotSpot);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00117990 File Offset: 0x00115B90
		internal override IntPtr DefineStdCursor(StdCursor id)
		{
			return Cursor.DefineStdCursor(id);
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x00117998 File Offset: 0x00115B98
		internal override IntPtr DefWndProc(ref Message msg)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(msg.HWnd);
			Msg msg2 = (Msg)msg.Msg;
			switch (msg2)
			{
			case Msg.WM_PAINT:
				hwnd.expose_pending = false;
				break;
			default:
				switch (msg2)
				{
				case Msg.WM_NCCALCSIZE:
					if (msg.WParam == (IntPtr)1)
					{
						XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(msg.LParam, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
						Control control = Control.FromHandle(hwnd.Handle);
						if (control != null)
						{
							Hwnd.Borders borders = Hwnd.GetBorders(control.GetCreateParams(), null);
							nccalcsize_PARAMS.rgrc1.top = nccalcsize_PARAMS.rgrc1.top + borders.top;
							nccalcsize_PARAMS.rgrc1.bottom = nccalcsize_PARAMS.rgrc1.bottom - borders.bottom;
							nccalcsize_PARAMS.rgrc1.left = nccalcsize_PARAMS.rgrc1.left + borders.left;
							nccalcsize_PARAMS.rgrc1.right = nccalcsize_PARAMS.rgrc1.right - borders.right;
							Marshal.StructureToPtr(nccalcsize_PARAMS, msg.LParam, true);
						}
					}
					break;
				default:
					if (msg2 == Msg.WM_SETCURSOR)
					{
						while (hwnd.parent != null && msg.Result == IntPtr.Zero)
						{
							hwnd = hwnd.parent;
							msg.Result = NativeWindow.WndProc(hwnd.Handle, Msg.WM_SETCURSOR, msg.HWnd, msg.LParam);
						}
						if (msg.Result == IntPtr.Zero)
						{
							HitTest hitTest = (HitTest)(msg.LParam.ToInt32() & 65535);
							IntPtr intPtr;
							switch (hitTest)
							{
							case HitTest.HTLEFT:
								intPtr = Cursors.SizeWE.handle;
								break;
							case HitTest.HTRIGHT:
								intPtr = Cursors.SizeWE.handle;
								break;
							case HitTest.HTTOP:
								intPtr = Cursors.SizeNS.handle;
								break;
							case HitTest.HTTOPLEFT:
								intPtr = Cursors.SizeNWSE.handle;
								break;
							case HitTest.HTTOPRIGHT:
								intPtr = Cursors.SizeNESW.handle;
								break;
							case HitTest.HTBOTTOM:
								intPtr = Cursors.SizeNS.handle;
								break;
							case HitTest.HTBOTTOMLEFT:
								intPtr = Cursors.SizeNESW.handle;
								break;
							case HitTest.HTBOTTOMRIGHT:
								intPtr = Cursors.SizeNWSE.handle;
								break;
							case HitTest.HTBORDER:
								intPtr = Cursors.SizeNS.handle;
								break;
							default:
								if (hitTest != HitTest.HTERROR)
								{
									intPtr = Cursors.Default.handle;
								}
								else
								{
									if (msg.LParam.ToInt32() >> 16 == 513)
									{
									}
									intPtr = Cursors.Default.handle;
								}
								break;
							case HitTest.HTHELP:
								intPtr = Cursors.Help.handle;
								break;
							}
							this.SetCursor(msg.HWnd, intPtr);
						}
						return (IntPtr)1;
					}
					if (msg2 != Msg.WM_IME_COMPOSITION)
					{
						if (msg2 == Msg.WM_IME_CHAR)
						{
							this.SendMessage(msg.HWnd, Msg.WM_CHAR, msg.WParam, msg.LParam);
							return IntPtr.Zero;
						}
					}
					else
					{
						string composedString = this.KeyboardHandler.ComposedString;
						string text = composedString;
						for (int i = 0; i < text.Length; i++)
						{
							char c = text.get_Chars(i);
							this.SendMessage(msg.HWnd, Msg.WM_IME_CHAR, (IntPtr)((int)c), msg.LParam);
						}
					}
					break;
				case Msg.WM_NCPAINT:
					hwnd.nc_expose_pending = false;
					break;
				}
				break;
			case Msg.WM_QUIT:
				if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
				{
					this.Exit();
				}
				break;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x00117D60 File Offset: 0x00115F60
		internal override void DestroyCaret(IntPtr hwnd)
		{
			if (XplatUICarbon.Caret.Hwnd == hwnd)
			{
				if (XplatUICarbon.Caret.Visible == 1)
				{
					XplatUICarbon.Caret.Timer.Stop();
					this.HideCaret();
				}
				XplatUICarbon.Caret.Hwnd = IntPtr.Zero;
				XplatUICarbon.Caret.Visible = 0;
				XplatUICarbon.Caret.On = false;
			}
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x00117DCC File Offset: 0x00115FCC
		[MonoTODO]
		internal override void DestroyCursor(IntPtr cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00117DD4 File Offset: 0x00115FD4
		internal override void DestroyWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			this.SendParentNotify(hwnd.Handle, Msg.WM_DESTROY, int.MaxValue, int.MaxValue);
			this.CleanupCachedWindows(hwnd);
			ArrayList arrayList = new ArrayList();
			this.AccumulateDestroyedHandles(Control.ControlNativeWindow.ControlFromHandle(hwnd.Handle), arrayList);
			foreach (object obj in arrayList)
			{
				Hwnd hwnd2 = (Hwnd)obj;
				this.SendMessage(hwnd2.Handle, Msg.WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
				hwnd2.zombie = true;
			}
			if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
			{
				XplatUICarbon.DisposeWindow((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle]);
				XplatUICarbon.WindowMapping.Remove(hwnd.Handle);
			}
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00117EEC File Offset: 0x001160EC
		internal override IntPtr DispatchMessage(ref MSG msg)
		{
			return NativeWindow.WndProc(msg.hwnd, msg.message, msg.wParam, msg.lParam);
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x00117F0C File Offset: 0x0011610C
		internal override void DoEvents()
		{
			MSG msg = default(MSG);
			XplatUICarbon.in_doevents = true;
			while (this.PeekMessage(null, ref msg, IntPtr.Zero, 0, 0, 1U))
			{
				this.TranslateMessage(ref msg);
				this.DispatchMessage(ref msg);
			}
			XplatUICarbon.in_doevents = false;
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x00117F5C File Offset: 0x0011615C
		internal override void EnableWindow(IntPtr handle, bool Enable)
		{
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x00117F60 File Offset: 0x00116160
		internal override void EndLoop(Thread thread)
		{
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x00117F64 File Offset: 0x00116164
		internal void Exit()
		{
			XplatUICarbon.GetMessageResult = false;
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x00117F6C File Offset: 0x0011616C
		internal override IntPtr GetActive()
		{
			return XplatUICarbon.ActiveWindow;
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x00117F74 File Offset: 0x00116174
		internal override Region GetClipRegion(IntPtr hwnd)
		{
			return null;
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00117F78 File Offset: 0x00116178
		[MonoTODO]
		internal override void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y)
		{
			width = 12;
			height = 12;
			hotspot_x = 0;
			hotspot_y = 0;
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x00117F8C File Offset: 0x0011618C
		internal override void GetDisplaySize(out Size size)
		{
			HIRect hirect = XplatUICarbon.CGDisplayBounds(XplatUICarbon.CGMainDisplayID());
			size..ctor((int)hirect.size.width, (int)hirect.size.height);
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00117FC4 File Offset: 0x001161C4
		internal override IntPtr GetParent(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null && hwnd.Parent != null)
			{
				return hwnd.Parent.Handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x00117FFC File Offset: 0x001161FC
		internal override IntPtr GetPreviousWindow(IntPtr handle)
		{
			return XplatUICarbon.HIViewGetPreviousView(handle);
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x00118004 File Offset: 0x00116204
		internal override void GetCursorPos(IntPtr handle, out int x, out int y)
		{
			QDPoint qdpoint = default(QDPoint);
			XplatUICarbon.GetGlobalMouse(ref qdpoint);
			x = (int)qdpoint.x;
			y = (int)qdpoint.y;
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x00118034 File Offset: 0x00116234
		internal override IntPtr GetFocus()
		{
			return XplatUICarbon.FocusWindow;
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x0011803C File Offset: 0x0011623C
		internal override bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent)
		{
			FontFamily fontFamily = font.FontFamily;
			ascent = fontFamily.GetCellAscent(font.Style);
			descent = fontFamily.GetCellDescent(font.Style);
			return true;
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x00118070 File Offset: 0x00116270
		internal override Point GetMenuOrigin(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				return hwnd.MenuOrigin;
			}
			return Point.Empty;
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x00118098 File Offset: 0x00116298
		internal override bool GetMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr eventDispatcherTarget = XplatUICarbon.GetEventDispatcherTarget();
			this.CheckTimers(DateTime.UtcNow);
			XplatUICarbon.ReceiveNextEvent(0U, IntPtr.Zero, 0.0, true, ref zero);
			if (zero != IntPtr.Zero && eventDispatcherTarget != IntPtr.Zero)
			{
				XplatUICarbon.SendEventToEventTarget(zero, eventDispatcherTarget);
				XplatUICarbon.ReleaseEvent(zero);
			}
			object obj = XplatUICarbon.queuelock;
			lock (obj)
			{
				while (XplatUICarbon.MessageQueue.Count > 0)
				{
					object obj2 = XplatUICarbon.MessageQueue.Dequeue();
					if (!(obj2 is GCHandle))
					{
						msg = (MSG)obj2;
						goto IL_019B;
					}
					XplatUIDriverSupport.ExecuteClientMessage((GCHandle)obj2);
				}
				if (this.Idle != null)
				{
					this.Idle.Invoke(this, EventArgs.Empty);
				}
				else if (this.TimerList.Count == 0)
				{
					XplatUICarbon.ReceiveNextEvent(0U, IntPtr.Zero, 0.15, true, ref zero);
					if (zero != IntPtr.Zero && eventDispatcherTarget != IntPtr.Zero)
					{
						XplatUICarbon.SendEventToEventTarget(zero, eventDispatcherTarget);
						XplatUICarbon.ReleaseEvent(zero);
					}
				}
				else
				{
					XplatUICarbon.ReceiveNextEvent(0U, IntPtr.Zero, this.NextTimeout(), true, ref zero);
					if (zero != IntPtr.Zero && eventDispatcherTarget != IntPtr.Zero)
					{
						XplatUICarbon.SendEventToEventTarget(zero, eventDispatcherTarget);
						XplatUICarbon.ReleaseEvent(zero);
					}
				}
				msg.hwnd = IntPtr.Zero;
				msg.message = Msg.WM_ENTERIDLE;
				return XplatUICarbon.GetMessageResult;
			}
			IL_019B:
			return XplatUICarbon.GetMessageResult;
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x00118264 File Offset: 0x00116464
		[MonoTODO]
		internal override bool GetText(IntPtr handle, out string text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x0011826C File Offset: 0x0011646C
		internal override void GetWindowPos(IntPtr handle, bool is_toplevel, out int x, out int y, out int width, out int height, out int client_width, out int client_height)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				x = hwnd.x;
				y = hwnd.y;
				width = hwnd.width;
				height = hwnd.height;
				this.PerformNCCalc(hwnd);
				client_width = hwnd.ClientRect.Width;
				client_height = hwnd.ClientRect.Height;
				return;
			}
			x = 0;
			y = 0;
			width = 0;
			height = 0;
			client_width = 0;
			client_height = 0;
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x001182EC File Offset: 0x001164EC
		internal override FormWindowState GetWindowState(IntPtr hwnd)
		{
			IntPtr intPtr = XplatUICarbon.HIViewGetWindow(hwnd);
			if (XplatUICarbon.IsWindowCollapsed(intPtr))
			{
				return FormWindowState.Minimized;
			}
			if (XplatUICarbon.IsWindowInStandardState(intPtr, IntPtr.Zero, IntPtr.Zero))
			{
				return FormWindowState.Maximized;
			}
			return FormWindowState.Normal;
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x00118328 File Offset: 0x00116528
		internal override void GrabInfo(out IntPtr handle, out bool GrabConfined, out Rectangle GrabArea)
		{
			handle = XplatUICarbon.Grab.Hwnd;
			GrabConfined = XplatUICarbon.Grab.Confined;
			GrabArea = XplatUICarbon.Grab.Area;
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00118360 File Offset: 0x00116560
		internal override void GrabWindow(IntPtr handle, IntPtr confine_to_handle)
		{
			XplatUICarbon.Grab.Hwnd = handle;
			XplatUICarbon.Grab.Confined = confine_to_handle != IntPtr.Zero;
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x00118390 File Offset: 0x00116590
		internal override void UngrabWindow(IntPtr hwnd)
		{
			bool flag = XplatUICarbon.Grab.Hwnd != IntPtr.Zero;
			XplatUICarbon.Grab.Hwnd = IntPtr.Zero;
			XplatUICarbon.Grab.Confined = false;
			if (flag)
			{
				this.SendMessage(hwnd, Msg.WM_CAPTURECHANGED, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x001183EC File Offset: 0x001165EC
		internal override void HandleException(Exception e)
		{
			StackTrace stackTrace = new StackTrace(e);
			Console.WriteLine("Exception '{0}'", e.Message + stackTrace.ToString());
			Console.WriteLine("{0}{1}", e.Message, stackTrace.ToString());
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x00118434 File Offset: 0x00116634
		internal override void Invalidate(IntPtr handle, Rectangle rc, bool clear)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (clear)
			{
				this.AddExpose(hwnd, true, hwnd.X, hwnd.Y, hwnd.Width, hwnd.Height);
			}
			else
			{
				this.AddExpose(hwnd, true, rc.X, rc.Y, rc.Width, rc.Height);
			}
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x00118498 File Offset: 0x00116698
		internal override void InvalidateNC(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.AddExpose(hwnd, false, 0, 0, hwnd.Width, hwnd.Height);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x001184C4 File Offset: 0x001166C4
		internal override bool IsEnabled(IntPtr handle)
		{
			return Hwnd.ObjectFromHandle(handle).Enabled;
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x001184D4 File Offset: 0x001166D4
		internal override bool IsVisible(IntPtr handle)
		{
			return Hwnd.ObjectFromHandle(handle).visible;
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x001184E4 File Offset: 0x001166E4
		internal override void KillTimer(Timer timer)
		{
			ArrayList timerList = this.TimerList;
			lock (timerList)
			{
				this.TimerList.Remove(timer);
			}
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x00118534 File Offset: 0x00116734
		internal override void OverrideCursor(IntPtr cursor)
		{
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x00118538 File Offset: 0x00116738
		internal override PaintEventArgs PaintEventStart(ref Message msg, IntPtr handle, bool client)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(msg.HWnd);
			Hwnd hwnd2;
			if (msg.HWnd == handle)
			{
				hwnd2 = hwnd;
			}
			else
			{
				hwnd2 = Hwnd.ObjectFromHandle(handle);
			}
			if (XplatUICarbon.Caret.Visible == 1)
			{
				XplatUICarbon.Caret.Paused = true;
				this.HideCaret();
			}
			PaintEventArgs paintEventArgs;
			if (client)
			{
				Graphics graphics = Graphics.FromHwnd(hwnd2.client_window);
				Region region = new Region();
				region.MakeEmpty();
				foreach (Rectangle rectangle in hwnd.ClipRectangles)
				{
					region.Union(rectangle);
				}
				if (hwnd.UserClip != null)
				{
					region.Intersect(hwnd.UserClip);
				}
				graphics.Clip = region;
				paintEventArgs = new PaintEventArgs(graphics, hwnd.Invalid);
				hwnd.expose_pending = false;
				hwnd.ClearInvalidArea();
				hwnd.drawing_stack.Push(paintEventArgs);
				hwnd.drawing_stack.Push(graphics);
			}
			else
			{
				Graphics graphics = Graphics.FromHwnd(hwnd2.whole_window);
				if (!hwnd.nc_invalid.IsEmpty)
				{
					graphics.SetClip(hwnd.nc_invalid);
					paintEventArgs = new PaintEventArgs(graphics, hwnd.nc_invalid);
				}
				else
				{
					paintEventArgs = new PaintEventArgs(graphics, new Rectangle(0, 0, hwnd.width, hwnd.height));
				}
				hwnd.nc_expose_pending = false;
				hwnd.ClearNcInvalidArea();
				hwnd.drawing_stack.Push(paintEventArgs);
				hwnd.drawing_stack.Push(graphics);
			}
			return paintEventArgs;
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x001186BC File Offset: 0x001168BC
		internal override void PaintEventEnd(ref Message msg, IntPtr handle, bool client)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			try
			{
				Graphics graphics = (Graphics)hwnd.drawing_stack.Pop();
				graphics.Flush();
				graphics.Dispose();
				PaintEventArgs paintEventArgs = (PaintEventArgs)hwnd.drawing_stack.Pop();
				paintEventArgs.SetGraphics(null);
				paintEventArgs.Dispose();
			}
			catch
			{
			}
			if (XplatUICarbon.Caret.Visible == 1)
			{
				this.ShowCaret();
				XplatUICarbon.Caret.Paused = false;
			}
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x00118754 File Offset: 0x00116954
		internal override bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr eventDispatcherTarget = XplatUICarbon.GetEventDispatcherTarget();
			this.CheckTimers(DateTime.UtcNow);
			XplatUICarbon.ReceiveNextEvent(0U, IntPtr.Zero, 0.0, true, ref zero);
			if (zero != IntPtr.Zero && eventDispatcherTarget != IntPtr.Zero)
			{
				XplatUICarbon.SendEventToEventTarget(zero, eventDispatcherTarget);
				XplatUICarbon.ReleaseEvent(zero);
			}
			object obj = XplatUICarbon.queuelock;
			bool flag;
			lock (obj)
			{
				if (XplatUICarbon.MessageQueue.Count <= 0)
				{
					flag = false;
				}
				else
				{
					object obj2;
					if (flags == 1U)
					{
						obj2 = XplatUICarbon.MessageQueue.Dequeue();
					}
					else
					{
						obj2 = XplatUICarbon.MessageQueue.Peek();
					}
					if (obj2 is GCHandle)
					{
						XplatUIDriverSupport.ExecuteClientMessage((GCHandle)obj2);
						flag = false;
					}
					else
					{
						msg = (MSG)obj2;
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x00118864 File Offset: 0x00116A64
		internal override bool PostMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			this.EnqueueMessage(new MSG
			{
				hwnd = hwnd,
				message = message,
				wParam = wParam,
				lParam = lParam
			});
			return true;
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x001188A4 File Offset: 0x00116AA4
		internal override void PostQuitMessage(int exitCode)
		{
			this.PostMessage(XplatUICarbon.FosterParent, Msg.WM_QUIT, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x001188C0 File Offset: 0x00116AC0
		internal override void RequestAdditionalWM_NCMessages(IntPtr hwnd, bool hover, bool leave)
		{
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x001188C4 File Offset: 0x00116AC4
		internal override void RequestNCRecalc(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			this.PerformNCCalc(hwnd);
			this.SendMessage(handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
			this.InvalidateNC(handle);
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00118904 File Offset: 0x00116B04
		[MonoTODO]
		internal override void ResetMouseHover(IntPtr handle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x0011890C File Offset: 0x00116B0C
		internal override void ScreenToClient(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Point point = this.ConvertScreenPointToClient(hwnd.ClientWindow, new Point(x, y));
			x = point.X;
			y = point.Y;
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x00118948 File Offset: 0x00116B48
		internal override void ScreenToMenu(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Point point = this.ConvertScreenPointToClient(hwnd.WholeWindow, new Point(x, y));
			x = point.X;
			y = point.Y;
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x00118984 File Offset: 0x00116B84
		internal override void ScrollWindow(IntPtr handle, Rectangle area, int XAmount, int YAmount, bool clear)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.Invalidate(handle, new Rectangle(0, 0, hwnd.Width, hwnd.Height), false);
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x001189B4 File Offset: 0x00116BB4
		internal override void ScrollWindow(IntPtr handle, int XAmount, int YAmount, bool clear)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.Invalidate(handle, new Rectangle(0, 0, hwnd.Width, hwnd.Height), false);
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x001189E4 File Offset: 0x00116BE4
		[MonoTODO]
		internal override void SendAsyncMethod(AsyncMethodData method)
		{
			object obj = XplatUICarbon.queuelock;
			lock (obj)
			{
				XplatUICarbon.MessageQueue.Enqueue(GCHandle.Alloc(method));
			}
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x00118A3C File Offset: 0x00116C3C
		[MonoTODO]
		internal override IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return NativeWindow.WndProc(hwnd, message, wParam, lParam);
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x00118A48 File Offset: 0x00116C48
		internal override int SendInput(IntPtr hwnd, Queue keys)
		{
			return 0;
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x00118A4C File Offset: 0x00116C4C
		internal override void SetCaretPos(IntPtr hwnd, int x, int y)
		{
			if (hwnd != IntPtr.Zero && hwnd == XplatUICarbon.Caret.Hwnd)
			{
				XplatUICarbon.Caret.X = x;
				XplatUICarbon.Caret.Y = y;
				this.ClientToScreen(hwnd, ref x, ref y);
				this.SizeWindow(new Rectangle(x, y, XplatUICarbon.Caret.Width, XplatUICarbon.Caret.Height), XplatUICarbon.CaretWindow);
				XplatUICarbon.Caret.Timer.Stop();
				this.HideCaret();
				if (XplatUICarbon.Caret.Visible == 1)
				{
					this.ShowCaret();
					XplatUICarbon.Caret.Timer.Start();
				}
			}
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x00118B00 File Offset: 0x00116D00
		internal override void SetClipRegion(IntPtr hwnd, Region region)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x00118B08 File Offset: 0x00116D08
		internal override void SetCursor(IntPtr window, IntPtr cursor)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(window);
			hwnd.Cursor = cursor;
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x00118B24 File Offset: 0x00116D24
		internal override void SetCursorPos(IntPtr handle, int x, int y)
		{
			XplatUICarbon.CGDisplayMoveCursorToPoint(XplatUICarbon.CGMainDisplayID(), new CGPoint(x, y));
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x00118B38 File Offset: 0x00116D38
		internal override void SetFocus(IntPtr handle)
		{
			if (XplatUICarbon.FocusWindow != IntPtr.Zero)
			{
				this.PostMessage(XplatUICarbon.FocusWindow, Msg.WM_KILLFOCUS, handle, IntPtr.Zero);
			}
			this.PostMessage(handle, Msg.WM_SETFOCUS, XplatUICarbon.FocusWindow, IntPtr.Zero);
			XplatUICarbon.FocusWindow = handle;
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x00118B88 File Offset: 0x00116D88
		internal override void SetIcon(IntPtr handle, Icon icon)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
			{
				if (icon == null)
				{
					XplatUICarbon.RestoreApplicationDockTileImage();
				}
				else
				{
					Bitmap bitmap = new Bitmap(128, 128);
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						graphics.DrawImage(icon.ToBitmap(), 0, 0, 128, 128);
					}
					int num = 0;
					int num2 = bitmap.Width * bitmap.Height;
					IntPtr[] array = new IntPtr[num2];
					for (int i = 0; i < bitmap.Height; i++)
					{
						for (int j = 0; j < bitmap.Width; j++)
						{
							int num3 = bitmap.GetPixel(j, i).ToArgb();
							if (BitConverter.IsLittleEndian)
							{
								byte b = (byte)((num3 >> 24) & 255);
								byte b2 = (byte)((num3 >> 16) & 255);
								byte b3 = (byte)((num3 >> 8) & 255);
								byte b4 = (byte)(num3 & 255);
								array[num++] = (IntPtr)((int)b + ((int)b2 << 8) + ((int)b3 << 16) + ((int)b4 << 24));
							}
							else
							{
								array[num++] = (IntPtr)num3;
							}
						}
					}
					IntPtr intPtr = XplatUICarbon.CGDataProviderCreateWithData(IntPtr.Zero, array, num2 * 4, IntPtr.Zero);
					IntPtr intPtr2 = XplatUICarbon.CGImageCreate(128, 128, 8, 32, 512, XplatUICarbon.CGColorSpaceCreateDeviceRGB(), 4U, intPtr, IntPtr.Zero, 0, 0);
					XplatUICarbon.SetApplicationDockTileImage(intPtr2);
				}
			}
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x00118D54 File Offset: 0x00116F54
		internal override void SetModal(IntPtr handle, bool Modal)
		{
			IntPtr intPtr = XplatUICarbon.HIViewGetWindow(Hwnd.ObjectFromHandle(handle).WholeWindow);
			if (Modal)
			{
				XplatUICarbon.BeginAppModalStateForWindow(intPtr);
			}
			else
			{
				XplatUICarbon.EndAppModalStateForWindow(intPtr);
			}
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x00118D8C File Offset: 0x00116F8C
		internal override IntPtr SetParent(IntPtr handle, IntPtr parent)
		{
			IntPtr zero = IntPtr.Zero;
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.Parent = Hwnd.ObjectFromHandle(parent);
			if (XplatUICarbon.HIViewGetSuperview(hwnd.whole_window) != IntPtr.Zero)
			{
				XplatUICarbon.HIViewRemoveFromSuperview(hwnd.whole_window);
			}
			if (hwnd.parent == null)
			{
				XplatUICarbon.HIViewFindByID(XplatUICarbon.HIViewGetRoot(XplatUICarbon.FosterParent), new HIViewID(2003398244U, 1U), ref zero);
			}
			XplatUICarbon.HIViewAddSubview((hwnd.parent != null) ? hwnd.Parent.client_window : zero, hwnd.whole_window);
			XplatUICarbon.HIViewPlaceInSuperviewAt(hwnd.whole_window, (float)hwnd.X, (float)hwnd.Y);
			XplatUICarbon.HIViewAddSubview(hwnd.whole_window, hwnd.client_window);
			XplatUICarbon.HIViewPlaceInSuperviewAt(hwnd.client_window, (float)hwnd.ClientRect.X, (float)hwnd.ClientRect.Y);
			return IntPtr.Zero;
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x00118E84 File Offset: 0x00117084
		internal override void SetTimer(Timer timer)
		{
			ArrayList timerList = this.TimerList;
			lock (timerList)
			{
				this.TimerList.Add(timer);
			}
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x00118ED4 File Offset: 0x001170D4
		internal override bool SetTopmost(IntPtr hWnd, bool Enabled)
		{
			XplatUICarbon.HIViewSetZOrder(hWnd, 1, IntPtr.Zero);
			return true;
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00118EE4 File Offset: 0x001170E4
		internal override bool SetOwner(IntPtr hWnd, IntPtr hWndOwner)
		{
			return true;
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x00118EE8 File Offset: 0x001170E8
		internal override bool SetVisible(IntPtr handle, bool visible, bool activate)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object obj = XplatUICarbon.WindowMapping[hwnd.Handle];
			if (obj != null)
			{
				if (visible)
				{
					XplatUICarbon.ShowWindow((IntPtr)obj);
				}
				else
				{
					XplatUICarbon.HideWindow((IntPtr)obj);
				}
			}
			if (visible)
			{
				this.SendMessage(handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
			}
			XplatUICarbon.HIViewSetVisible(hwnd.whole_window, visible);
			XplatUICarbon.HIViewSetVisible(hwnd.client_window, visible);
			hwnd.visible = visible;
			hwnd.Mapped = true;
			return true;
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x00118F80 File Offset: 0x00117180
		internal override void SetAllowDrop(IntPtr handle, bool value)
		{
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x00118F84 File Offset: 0x00117184
		internal override DragDropEffects StartDrag(IntPtr handle, object data, DragDropEffects allowed_effects)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				throw new ArgumentException("Attempt to begin drag from invalid window handle (" + handle.ToInt32() + ").");
			}
			return XplatUICarbon.Dnd.StartDrag(hwnd.client_window, data, allowed_effects);
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x00118FD4 File Offset: 0x001171D4
		internal override void SetBorderStyle(IntPtr handle, FormBorderStyle border_style)
		{
			Form form = Control.FromHandle(handle) as Form;
			if (form != null && form.window_manager == null && (border_style == FormBorderStyle.FixedToolWindow || border_style == FormBorderStyle.SizableToolWindow))
			{
				form.window_manager = new ToolWindowManager(form);
			}
			this.RequestNCRecalc(handle);
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x00119020 File Offset: 0x00117220
		internal override void SetMenu(IntPtr handle, Menu menu)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.menu = menu;
			this.RequestNCRecalc(handle);
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x00119044 File Offset: 0x00117244
		internal override void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max)
		{
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x00119048 File Offset: 0x00117248
		internal override void SetWindowPos(IntPtr handle, int x, int y, int width, int height)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			if (width < 0)
			{
				width = 0;
			}
			if (height < 0)
			{
				height = 0;
			}
			if (hwnd.zero_sized && width > 0 && height > 0)
			{
				if (hwnd.visible)
				{
					XplatUICarbon.HIViewSetVisible(hwnd.WholeWindow, true);
				}
				hwnd.zero_sized = false;
			}
			if (width < 1 || height < 1)
			{
				hwnd.zero_sized = true;
				XplatUICarbon.HIViewSetVisible(hwnd.WholeWindow, false);
			}
			if (hwnd.x == x && hwnd.y == y && hwnd.width == width && hwnd.height == height)
			{
				return;
			}
			if (!hwnd.zero_sized)
			{
				hwnd.x = x;
				hwnd.y = y;
				hwnd.width = width;
				hwnd.height = height;
				this.SendMessage(hwnd.client_window, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
				Control control = Control.FromHandle(handle);
				CreateParams createParams = control.GetCreateParams();
				Size size = XplatUICarbon.TranslateWindowSizeToQuartzWindowSize(createParams, new Size(width, height));
				Rect rect = default(Rect);
				if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
				{
					if (this.StyleSet(createParams.Style, WindowStyles.WS_POPUP))
					{
						XplatUICarbon.SetRect(ref rect, (short)x, (short)y, (short)(x + size.Width), (short)(y + size.Height));
					}
					else
					{
						XplatUICarbon.SetRect(ref rect, (short)x, (short)(y + XplatUICarbon.MenuBarHeight), (short)(x + size.Width), (short)(y + XplatUICarbon.MenuBarHeight + size.Height));
					}
					XplatUICarbon.SetWindowBounds((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], 33U, ref rect);
					HIRect hirect = new HIRect(0, 0, size.Width, size.Height);
					XplatUICarbon.HIViewSetFrame(hwnd.whole_window, ref hirect);
					this.SetCaretPos(XplatUICarbon.Caret.Hwnd, XplatUICarbon.Caret.X, XplatUICarbon.Caret.Y);
				}
				else
				{
					HIRect hirect2 = new HIRect(x, y, size.Width, size.Height);
					XplatUICarbon.HIViewSetFrame(hwnd.whole_window, ref hirect2);
				}
				this.PerformNCCalc(hwnd);
			}
			hwnd.x = x;
			hwnd.y = y;
			hwnd.width = width;
			hwnd.height = height;
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x001192B0 File Offset: 0x001174B0
		internal override void SetWindowState(IntPtr handle, FormWindowState state)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			IntPtr intPtr = XplatUICarbon.HIViewGetWindow(handle);
			switch (state)
			{
			case FormWindowState.Normal:
				XplatUICarbon.ZoomWindow(intPtr, 7, false);
				break;
			case FormWindowState.Minimized:
				XplatUICarbon.CollapseWindow(intPtr, true);
				break;
			case FormWindowState.Maximized:
			{
				Form form = Control.FromHandle(hwnd.Handle) as Form;
				if (form != null && form.FormBorderStyle == FormBorderStyle.None)
				{
					Rect rect = default(Rect);
					HIRect hirect = XplatUICarbon.CGDisplayBounds(XplatUICarbon.CGMainDisplayID());
					XplatUICarbon.SetRect(ref rect, 0, 0, (short)hirect.size.width, (short)hirect.size.height);
					XplatUICarbon.SetWindowBounds((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], 33U, ref rect);
					XplatUICarbon.HIViewSetFrame(hwnd.whole_window, ref hirect);
				}
				else
				{
					XplatUICarbon.ZoomWindow(intPtr, 8, false);
				}
				break;
			}
			}
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x0011939C File Offset: 0x0011759C
		internal override void SetWindowStyle(IntPtr handle, CreateParams cp)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.SetHwndStyles(hwnd, cp);
			if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
			{
				WindowAttributes windowAttributes = (WindowAttributes)34078720U;
				if ((cp.Style & 131072) != 0)
				{
					windowAttributes |= WindowAttributes.kWindowCollapseBoxAttribute;
				}
				if ((cp.Style & 65536) != 0)
				{
					windowAttributes |= (WindowAttributes)22U;
				}
				if ((cp.Style & 524288) != 0)
				{
					windowAttributes |= WindowAttributes.kWindowCloseBoxAttribute;
				}
				if ((cp.ExStyle & 128) != 0)
				{
					windowAttributes = (WindowAttributes)34078720U;
				}
				windowAttributes |= WindowAttributes.kWindowLiveResizeAttribute;
				WindowAttributes windowAttributes2 = WindowAttributes.kWindowNoAttributes;
				XplatUICarbon.GetWindowAttributes((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], ref windowAttributes2);
				XplatUICarbon.ChangeWindowAttributes((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], windowAttributes, windowAttributes2);
			}
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x00119480 File Offset: 0x00117680
		internal override void SetWindowTransparency(IntPtr handle, double transparency, Color key)
		{
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00119484 File Offset: 0x00117684
		internal override double GetWindowTransparency(IntPtr handle)
		{
			return 1.0;
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x00119490 File Offset: 0x00117690
		internal override TransparencySupport SupportsTransparency()
		{
			return TransparencySupport.None;
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x00119494 File Offset: 0x00117694
		internal override bool SetZOrder(IntPtr handle, IntPtr after_handle, bool Top, bool Bottom)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (Top)
			{
				XplatUICarbon.HIViewSetZOrder(hwnd.whole_window, 2, IntPtr.Zero);
				return true;
			}
			if (!Bottom)
			{
				Hwnd hwnd2 = Hwnd.ObjectFromHandle(after_handle);
				XplatUICarbon.HIViewSetZOrder(hwnd.whole_window, 2, (!(after_handle == IntPtr.Zero)) ? hwnd2.whole_window : IntPtr.Zero);
				return false;
			}
			XplatUICarbon.HIViewSetZOrder(hwnd.whole_window, 1, IntPtr.Zero);
			return true;
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x00119518 File Offset: 0x00117718
		internal override void ShowCursor(bool show)
		{
			if (show)
			{
				XplatUICarbon.CGDisplayShowCursor(XplatUICarbon.CGMainDisplayID());
			}
			else
			{
				XplatUICarbon.CGDisplayHideCursor(XplatUICarbon.CGMainDisplayID());
			}
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x0011953C File Offset: 0x0011773C
		internal override object StartLoop(Thread thread)
		{
			return new object();
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x00119544 File Offset: 0x00117744
		[MonoTODO]
		internal override bool SystrayAdd(IntPtr hwnd, string tip, Icon icon, out ToolTip tt)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x0011954C File Offset: 0x0011774C
		[MonoTODO]
		internal override bool SystrayChange(IntPtr hwnd, string tip, Icon icon, ref ToolTip tt)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x00119554 File Offset: 0x00117754
		[MonoTODO]
		internal override void SystrayRemove(IntPtr hwnd, ref ToolTip tt)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x0011955C File Offset: 0x0011775C
		[MonoTODO]
		internal override void SystrayBalloon(IntPtr hwnd, int timeout, string title, string text, ToolTipIcon icon)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x00119564 File Offset: 0x00117764
		internal override bool Text(IntPtr handle, string text)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
			{
				XplatUICarbon.SetWindowTitleWithCFString((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], XplatUICarbon.__CFStringMakeConstantString(text));
			}
			XplatUICarbon.SetControlTitleWithCFString(hwnd.whole_window, XplatUICarbon.__CFStringMakeConstantString(text));
			XplatUICarbon.SetControlTitleWithCFString(hwnd.client_window, XplatUICarbon.__CFStringMakeConstantString(text));
			return true;
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x001195E0 File Offset: 0x001177E0
		internal override void UpdateWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (!hwnd.visible || !XplatUICarbon.HIViewIsVisible(handle))
			{
				return;
			}
			this.SendMessage(handle, Msg.WM_PAINT, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00119620 File Offset: 0x00117820
		internal override bool TranslateMessage(ref MSG msg)
		{
			return EventHandler.TranslateMessage(ref msg);
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00119628 File Offset: 0x00117828
		internal void SizeWindow(Rectangle rect, IntPtr window)
		{
			Rect rect2 = default(Rect);
			XplatUICarbon.SetRect(ref rect2, (short)rect.X, (short)rect.Y, (short)(rect.X + rect.Width), (short)(rect.Y + rect.Height));
			XplatUICarbon.SetWindowBounds(window, 33U, ref rect2);
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00119680 File Offset: 0x00117880
		internal override void DrawReversibleLine(Point start, Point end, Color backColor)
		{
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x00119684 File Offset: 0x00117884
		internal override void FillReversibleRectangle(Rectangle rectangle, Color backColor)
		{
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x00119688 File Offset: 0x00117888
		internal override void DrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style)
		{
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x0011968C File Offset: 0x0011788C
		internal override void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width)
		{
			Rectangle rectangle = rect;
			int num = 0;
			int num2 = 0;
			if (XplatUICarbon.ReverseWindowMapped)
			{
				XplatUICarbon.HideWindow(XplatUICarbon.ReverseWindow);
				XplatUICarbon.ReverseWindowMapped = false;
			}
			else
			{
				this.ClientToScreen(handle, ref num, ref num2);
				rectangle.X += num;
				rectangle.Y += num2;
				this.SizeWindow(rectangle, XplatUICarbon.ReverseWindow);
				XplatUICarbon.ShowWindow(XplatUICarbon.ReverseWindow);
				rect.X = 0;
				rect.Y = 0;
				rect.Width--;
				rect.Height--;
				Graphics graphics = Graphics.FromHwnd(XplatUICarbon.HIViewGetRoot(XplatUICarbon.ReverseWindow));
				for (int i = 0; i < line_width; i++)
				{
					graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(Color.Black), rect);
					rect.X++;
					rect.Y++;
					rect.Width--;
					rect.Height--;
				}
				graphics.Flush();
				graphics.Dispose();
				XplatUICarbon.ReverseWindowMapped = true;
			}
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x001197BC File Offset: 0x001179BC
		internal override SizeF GetAutoScaleSize(Font font)
		{
			string text = "The quick brown fox jumped over the lazy dog.";
			double num = 44.54999694824219;
			Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
			float num2 = (float)((double)graphics.MeasureString(text, font).Width / num);
			return new SizeF(num2, (float)font.Height);
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x0600479E RID: 18334 RVA: 0x0011980C File Offset: 0x00117A0C
		internal override Point MousePosition
		{
			get
			{
				return this.mouse_position;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x0600479F RID: 18335 RVA: 0x00119814 File Offset: 0x00117A14
		internal override int KeyboardSpeed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x060047A0 RID: 18336 RVA: 0x0011981C File Offset: 0x00117A1C
		internal override int KeyboardDelay
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x060047A1 RID: 18337 RVA: 0x00119824 File Offset: 0x00117A24
		internal override int CaptionHeight
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x00119828 File Offset: 0x00117A28
		internal override Size CursorSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x060047A3 RID: 18339 RVA: 0x00119830 File Offset: 0x00117A30
		internal override bool DragFullWindows
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x060047A4 RID: 18340 RVA: 0x00119838 File Offset: 0x00117A38
		internal override Size DragSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x060047A5 RID: 18341 RVA: 0x00119844 File Offset: 0x00117A44
		internal override Size FrameBorderSize
		{
			get
			{
				return new Size(2, 2);
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x060047A6 RID: 18342 RVA: 0x00119850 File Offset: 0x00117A50
		internal override Size IconSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x060047A7 RID: 18343 RVA: 0x00119858 File Offset: 0x00117A58
		internal override Size MaxWindowTrackSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x060047A8 RID: 18344 RVA: 0x00119860 File Offset: 0x00117A60
		internal override bool MenuAccessKeysUnderlined
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x060047A9 RID: 18345 RVA: 0x00119864 File Offset: 0x00117A64
		internal override Size MinimizedWindowSpacingSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x060047AA RID: 18346 RVA: 0x0011986C File Offset: 0x00117A6C
		internal override Size MinimumWindowSize
		{
			get
			{
				return new Size(110, 22);
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x060047AB RID: 18347 RVA: 0x00119878 File Offset: 0x00117A78
		internal override Keys ModifierKeys
		{
			get
			{
				return this.KeyboardHandler.ModifierKeys;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x060047AC RID: 18348 RVA: 0x00119888 File Offset: 0x00117A88
		internal override Size SmallIconSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x060047AD RID: 18349 RVA: 0x00119890 File Offset: 0x00117A90
		internal override int MouseButtonCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x060047AE RID: 18350 RVA: 0x00119898 File Offset: 0x00117A98
		internal override bool MouseButtonsSwapped
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x060047AF RID: 18351 RVA: 0x001198A0 File Offset: 0x00117AA0
		internal override bool MouseWheelPresent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x060047B0 RID: 18352 RVA: 0x001198A8 File Offset: 0x00117AA8
		internal override MouseButtons MouseButtons
		{
			get
			{
				return XplatUICarbon.MouseState;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x060047B1 RID: 18353 RVA: 0x001198B0 File Offset: 0x00117AB0
		internal override Rectangle VirtualScreen
		{
			get
			{
				return this.WorkingArea;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x060047B2 RID: 18354 RVA: 0x001198B8 File Offset: 0x00117AB8
		internal override Rectangle WorkingArea
		{
			get
			{
				HIRect hirect = XplatUICarbon.CGDisplayBounds(XplatUICarbon.CGMainDisplayID());
				return new Rectangle((int)hirect.origin.x, (int)hirect.origin.y, (int)hirect.size.width, (int)hirect.size.height);
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x060047B3 RID: 18355 RVA: 0x0011990C File Offset: 0x00117B0C
		internal override bool ThemesEnabled
		{
			get
			{
				return XplatUICarbon.themes_enabled;
			}
		}

		// Token: 0x060047B4 RID: 18356
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewConvertPoint(ref CGPoint point, IntPtr pView, IntPtr cView);

		// Token: 0x060047B5 RID: 18357
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewChangeFeatures(IntPtr aView, ulong bitsin, ulong bitsout);

		// Token: 0x060047B6 RID: 18358
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewFindByID(IntPtr rootWnd, HIViewID id, ref IntPtr outPtr);

		// Token: 0x060047B7 RID: 18359
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIGrowBoxViewSetTransparent(IntPtr GrowBox, bool transparency);

		// Token: 0x060047B8 RID: 18360
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr HIViewGetRoot(IntPtr hWnd);

		// Token: 0x060047B9 RID: 18361
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIObjectCreate(IntPtr cfStr, uint what, ref IntPtr hwnd);

		// Token: 0x060047BA RID: 18362
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIObjectRegisterSubclass(IntPtr classid, IntPtr superclassid, uint options, EventDelegate upp, uint count, EventTypeSpec[] list, IntPtr state, ref IntPtr cls);

		// Token: 0x060047BB RID: 18363
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewPlaceInSuperviewAt(IntPtr view, float x, float y);

		// Token: 0x060047BC RID: 18364
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewAddSubview(IntPtr parentHnd, IntPtr childHnd);

		// Token: 0x060047BD RID: 18365
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr HIViewGetPreviousView(IntPtr aView);

		// Token: 0x060047BE RID: 18366
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr HIViewGetSuperview(IntPtr aView);

		// Token: 0x060047BF RID: 18367
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewRemoveFromSuperview(IntPtr aView);

		// Token: 0x060047C0 RID: 18368
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewSetVisible(IntPtr vHnd, bool visible);

		// Token: 0x060047C1 RID: 18369
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool HIViewIsVisible(IntPtr vHnd);

		// Token: 0x060047C2 RID: 18370
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewGetBounds(IntPtr vHnd, ref HIRect r);

		// Token: 0x060047C3 RID: 18371
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewScrollRect(IntPtr vHnd, ref HIRect rect, float x, float y);

		// Token: 0x060047C4 RID: 18372
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewSetZOrder(IntPtr hWnd, int cmd, IntPtr oHnd);

		// Token: 0x060047C5 RID: 18373
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewNewTrackingArea(IntPtr inView, IntPtr inShape, ulong inID, ref IntPtr outRef);

		// Token: 0x060047C6 RID: 18374
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr HIViewGetWindow(IntPtr aView);

		// Token: 0x060047C7 RID: 18375
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewSetFrame(IntPtr view_handle, ref HIRect bounds);

		// Token: 0x060047C8 RID: 18376
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewSetNeedsDisplayInRect(IntPtr view_handle, ref HIRect rect, bool needs_display);

		// Token: 0x060047C9 RID: 18377
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void SetRect(ref Rect r, short left, short top, short right, short bottom);

		// Token: 0x060047CA RID: 18378
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int ActivateWindow(IntPtr windowHnd, bool inActivate);

		// Token: 0x060047CB RID: 18379
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool IsWindowActive(IntPtr windowHnd);

		// Token: 0x060047CC RID: 18380
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetAutomaticControlDragTrackingEnabledForWindow(IntPtr window, bool enabled);

		// Token: 0x060047CD RID: 18381
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr GetEventDispatcherTarget();

		// Token: 0x060047CE RID: 18382
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SendEventToEventTarget(IntPtr evt, IntPtr target);

		// Token: 0x060047CF RID: 18383
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int ReleaseEvent(IntPtr evt);

		// Token: 0x060047D0 RID: 18384
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int ReceiveNextEvent(uint evtCount, IntPtr evtTypes, double timeout, bool processEvt, ref IntPtr evt);

		// Token: 0x060047D1 RID: 18385
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool IsWindowCollapsed(IntPtr hWnd);

		// Token: 0x060047D2 RID: 18386
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool IsWindowInStandardState(IntPtr hWnd, IntPtr a, IntPtr b);

		// Token: 0x060047D3 RID: 18387
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void CollapseWindow(IntPtr hWnd, bool collapse);

		// Token: 0x060047D4 RID: 18388
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void ZoomWindow(IntPtr hWnd, short partCode, bool front);

		// Token: 0x060047D5 RID: 18389
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetWindowAttributes(IntPtr hWnd, ref WindowAttributes outAttributes);

		// Token: 0x060047D6 RID: 18390
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int ChangeWindowAttributes(IntPtr hWnd, WindowAttributes inAttributes, WindowAttributes outAttributes);

		// Token: 0x060047D7 RID: 18391
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetGlobalMouse(ref QDPoint outData);

		// Token: 0x060047D8 RID: 18392
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int BeginAppModalStateForWindow(IntPtr window);

		// Token: 0x060047D9 RID: 18393
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int EndAppModalStateForWindow(IntPtr window);

		// Token: 0x060047DA RID: 18394
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int CreateNewWindow(WindowClass klass, WindowAttributes attributes, ref Rect r, ref IntPtr window);

		// Token: 0x060047DB RID: 18395
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int DisposeWindow(IntPtr wHnd);

		// Token: 0x060047DC RID: 18396
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int ShowWindow(IntPtr wHnd);

		// Token: 0x060047DD RID: 18397
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HideWindow(IntPtr wHnd);

		// Token: 0x060047DE RID: 18398
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern bool IsWindowVisible(IntPtr wHnd);

		// Token: 0x060047DF RID: 18399
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetWindowBounds(IntPtr wHnd, uint reg, ref Rect rect);

		// Token: 0x060047E0 RID: 18400
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetWindowBounds(IntPtr wHnd, uint reg, ref Rect rect);

		// Token: 0x060047E1 RID: 18401
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetControlTitleWithCFString(IntPtr hWnd, IntPtr titleCFStr);

		// Token: 0x060047E2 RID: 18402
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetWindowTitleWithCFString(IntPtr hWnd, IntPtr titleCFStr);

		// Token: 0x060047E3 RID: 18403
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr __CFStringMakeConstantString(string cString);

		// Token: 0x060047E4 RID: 18404
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int CFRelease(IntPtr wHnd);

		// Token: 0x060047E5 RID: 18405
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern short GetMBarHeight();

		// Token: 0x060047E6 RID: 18406
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void AlertSoundPlay();

		// Token: 0x060047E7 RID: 18407
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern HIRect CGDisplayBounds(IntPtr displayID);

		// Token: 0x060047E8 RID: 18408
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CGMainDisplayID();

		// Token: 0x060047E9 RID: 18409
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void CGDisplayShowCursor(IntPtr display);

		// Token: 0x060047EA RID: 18410
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void CGDisplayHideCursor(IntPtr display);

		// Token: 0x060047EB RID: 18411
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void CGDisplayMoveCursorToPoint(IntPtr display, CGPoint point);

		// Token: 0x060047EC RID: 18412
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetCurrentProcess(ref ProcessSerialNumber psn);

		// Token: 0x060047ED RID: 18413
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int TransformProcessType(ref ProcessSerialNumber psn, uint type);

		// Token: 0x060047EE RID: 18414
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetFrontProcess(ref ProcessSerialNumber psn);

		// Token: 0x060047EF RID: 18415
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CGColorSpaceCreateDeviceRGB();

		// Token: 0x060047F0 RID: 18416
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CGDataProviderCreateWithData(IntPtr info, IntPtr[] data, int size, IntPtr releasefunc);

		// Token: 0x060047F1 RID: 18417
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CGImageCreate(int width, int height, int bitsPerComponent, int bitsPerPixel, int bytesPerRow, IntPtr colorspace, uint bitmapInfo, IntPtr provider, IntPtr decode, int shouldInterpolate, int intent);

		// Token: 0x060047F2 RID: 18418
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void SetApplicationDockTileImage(IntPtr imageRef);

		// Token: 0x060047F3 RID: 18419
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void RestoreApplicationDockTileImage();

		// Token: 0x040022CD RID: 8909
		private static XplatUICarbon Instance;

		// Token: 0x040022CE RID: 8910
		private static int RefCount;

		// Token: 0x040022CF RID: 8911
		private static bool themes_enabled;

		// Token: 0x040022D0 RID: 8912
		internal static IntPtr FocusWindow;

		// Token: 0x040022D1 RID: 8913
		internal static IntPtr ActiveWindow;

		// Token: 0x040022D2 RID: 8914
		internal static IntPtr ReverseWindow;

		// Token: 0x040022D3 RID: 8915
		internal static IntPtr CaretWindow;

		// Token: 0x040022D4 RID: 8916
		internal static Hwnd MouseHwnd;

		// Token: 0x040022D5 RID: 8917
		internal static MouseButtons MouseState;

		// Token: 0x040022D6 RID: 8918
		internal static Hover Hover;

		// Token: 0x040022D7 RID: 8919
		internal static HwndDelegate HwndDelegate = new HwndDelegate(XplatUICarbon.GetClippingRectangles);

		// Token: 0x040022D8 RID: 8920
		internal Point mouse_position;

		// Token: 0x040022D9 RID: 8921
		internal ApplicationHandler ApplicationHandler;

		// Token: 0x040022DA RID: 8922
		internal ControlHandler ControlHandler;

		// Token: 0x040022DB RID: 8923
		internal HIObjectHandler HIObjectHandler;

		// Token: 0x040022DC RID: 8924
		internal KeyboardHandler KeyboardHandler;

		// Token: 0x040022DD RID: 8925
		internal MouseHandler MouseHandler;

		// Token: 0x040022DE RID: 8926
		internal WindowHandler WindowHandler;

		// Token: 0x040022DF RID: 8927
		internal static GrabStruct Grab;

		// Token: 0x040022E0 RID: 8928
		internal static Caret Caret;

		// Token: 0x040022E1 RID: 8929
		private static Dnd Dnd;

		// Token: 0x040022E2 RID: 8930
		private static Hashtable WindowMapping;

		// Token: 0x040022E3 RID: 8931
		private static Hashtable HandleMapping;

		// Token: 0x040022E4 RID: 8932
		private static IntPtr FosterParent;

		// Token: 0x040022E5 RID: 8933
		private static IntPtr Subclass;

		// Token: 0x040022E6 RID: 8934
		private static int MenuBarHeight;

		// Token: 0x040022E7 RID: 8935
		internal static ArrayList UtilityWindows;

		// Token: 0x040022E8 RID: 8936
		private static Queue MessageQueue;

		// Token: 0x040022E9 RID: 8937
		private static bool GetMessageResult;

		// Token: 0x040022EA RID: 8938
		private static bool ReverseWindowMapped;

		// Token: 0x040022EB RID: 8939
		private ArrayList TimerList;

		// Token: 0x040022EC RID: 8940
		private static bool in_doevents;

		// Token: 0x040022ED RID: 8941
		private static readonly object instancelock = new object();

		// Token: 0x040022EE RID: 8942
		private static readonly object queuelock = new object();
	}
}
