using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Mono.Unix.Native;

namespace System.Windows.Forms
{
	// Token: 0x0200049A RID: 1178
	internal class XplatUIX11 : XplatUIDriver
	{
		// Token: 0x06004A13 RID: 18963 RVA: 0x0011D9CC File Offset: 0x0011BBCC
		private XplatUIX11()
		{
			XplatUIX11.RefCount = 0;
			XplatUIX11.in_doevents = false;
			XplatUIX11.XlibLock = new object();
			X11Keyboard.XlibLock = XplatUIX11.XlibLock;
			XplatUIX11.MessageQueues = Hashtable.Synchronized(new Hashtable(7));
			XplatUIX11.unattached_timer_list = ArrayList.Synchronized(new ArrayList(3));
			XplatUIX11.messageHold = Hashtable.Synchronized(new Hashtable(3));
			XplatUIX11.Clipboard = new ClipboardData();
			XplatUIX11.XInitThreads();
			XplatUIX11.ErrorExceptions = false;
			this.SetDisplay(XplatUIX11.XOpenDisplay(IntPtr.Zero));
			X11DesktopColors.Initialize();
			try
			{
				XplatUIX11.XkbSetDetectableAutoRepeat(XplatUIX11.DisplayHandle, true, IntPtr.Zero);
				XplatUIX11.detectable_key_auto_repeat = true;
			}
			catch
			{
				Console.Error.WriteLine("Could not disable keyboard auto repeat, will attempt to disable manually.");
				XplatUIX11.detectable_key_auto_repeat = false;
			}
			XplatUIX11.ErrorHandler = new XErrorHandler(this.HandleError);
			XplatUIX11.XSetErrorHandler(XplatUIX11.ErrorHandler);
		}

		// Token: 0x14000472 RID: 1138
		// (add) Token: 0x06004A15 RID: 18965 RVA: 0x0011DAE0 File Offset: 0x0011BCE0
		// (remove) Token: 0x06004A16 RID: 18966 RVA: 0x0011DAFC File Offset: 0x0011BCFC
		internal override event EventHandler Idle;

		// Token: 0x06004A17 RID: 18967 RVA: 0x0011DB18 File Offset: 0x0011BD18
		~XplatUIX11()
		{
			Graphics.FromHdcInternal(IntPtr.Zero);
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x0011DB58 File Offset: 0x0011BD58
		public static XplatUIX11 GetInstance()
		{
			object obj = XplatUIX11.lockobj;
			lock (obj)
			{
				if (XplatUIX11.Instance == null)
				{
					XplatUIX11.Instance = new XplatUIX11();
				}
				XplatUIX11.RefCount++;
			}
			return XplatUIX11.Instance;
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06004A19 RID: 18969 RVA: 0x0011DBC4 File Offset: 0x0011BDC4
		public int Reference
		{
			get
			{
				return XplatUIX11.RefCount;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06004A1A RID: 18970 RVA: 0x0011DBCC File Offset: 0x0011BDCC
		// (set) Token: 0x06004A1B RID: 18971 RVA: 0x0011DBD4 File Offset: 0x0011BDD4
		internal static IntPtr Display
		{
			get
			{
				return XplatUIX11.DisplayHandle;
			}
			set
			{
				XplatUIX11.GetInstance().SetDisplay(value);
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06004A1C RID: 18972 RVA: 0x0011DBE4 File Offset: 0x0011BDE4
		// (set) Token: 0x06004A1D RID: 18973 RVA: 0x0011DBEC File Offset: 0x0011BDEC
		internal static int Screen
		{
			get
			{
				return XplatUIX11.ScreenNo;
			}
			set
			{
				XplatUIX11.ScreenNo = value;
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x0011DBF4 File Offset: 0x0011BDF4
		// (set) Token: 0x06004A1F RID: 18975 RVA: 0x0011DBFC File Offset: 0x0011BDFC
		internal static IntPtr RootWindowHandle
		{
			get
			{
				return XplatUIX11.RootWindow;
			}
			set
			{
				XplatUIX11.RootWindow = value;
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06004A20 RID: 18976 RVA: 0x0011DC04 File Offset: 0x0011BE04
		// (set) Token: 0x06004A21 RID: 18977 RVA: 0x0011DC0C File Offset: 0x0011BE0C
		internal static IntPtr Visual
		{
			get
			{
				return XplatUIX11.CustomVisual;
			}
			set
			{
				XplatUIX11.CustomVisual = value;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06004A22 RID: 18978 RVA: 0x0011DC14 File Offset: 0x0011BE14
		// (set) Token: 0x06004A23 RID: 18979 RVA: 0x0011DC1C File Offset: 0x0011BE1C
		internal static IntPtr ColorMap
		{
			get
			{
				return XplatUIX11.CustomColormap;
			}
			set
			{
				XplatUIX11.CustomColormap = value;
			}
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x0011DC24 File Offset: 0x0011BE24
		internal void SetDisplay(IntPtr display_handle)
		{
			if (display_handle != IntPtr.Zero)
			{
				Hwnd hwnd;
				if (XplatUIX11.DisplayHandle != IntPtr.Zero && XplatUIX11.FosterParent != IntPtr.Zero)
				{
					hwnd = Hwnd.ObjectFromHandle(XplatUIX11.FosterParent);
					XplatUIX11.XDestroyWindow(XplatUIX11.DisplayHandle, XplatUIX11.FosterParent);
					hwnd.Dispose();
				}
				if (XplatUIX11.DisplayHandle != IntPtr.Zero)
				{
					XplatUIX11.XCloseDisplay(XplatUIX11.DisplayHandle);
				}
				XplatUIX11.DisplayHandle = display_handle;
				Graphics.FromHdcInternal(XplatUIX11.DisplayHandle);
				XplatUIX11.XQueryExtension(XplatUIX11.DisplayHandle, "RENDER", ref this.render_major_opcode, ref this.render_first_event, ref this.render_first_error);
				if (Environment.GetEnvironmentVariable("MONO_XSYNC") != null)
				{
					XplatUIX11.XSynchronize(XplatUIX11.DisplayHandle, true);
				}
				if (Environment.GetEnvironmentVariable("MONO_XEXCEPTIONS") != null)
				{
					XplatUIX11.ErrorExceptions = true;
				}
				XplatUIX11.ScreenNo = XplatUIX11.XDefaultScreen(XplatUIX11.DisplayHandle);
				XplatUIX11.RootWindow = XplatUIX11.XRootWindow(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
				XplatUIX11.DefaultColormap = XplatUIX11.XDefaultColormap(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
				XplatUIX11.FosterParent = XplatUIX11.XCreateSimpleWindow(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, 0, 0, 1, 1, 0, UIntPtr.Zero, UIntPtr.Zero);
				if (XplatUIX11.FosterParent == IntPtr.Zero)
				{
					Console.WriteLine("XplatUIX11 Constructor failed to create FosterParent");
				}
				hwnd = new Hwnd();
				hwnd.Queue = this.ThreadQueue(Thread.CurrentThread);
				hwnd.WholeWindow = XplatUIX11.FosterParent;
				hwnd.ClientWindow = XplatUIX11.FosterParent;
				hwnd = new Hwnd();
				hwnd.Queue = this.ThreadQueue(Thread.CurrentThread);
				hwnd.whole_window = XplatUIX11.RootWindow;
				hwnd.ClientWindow = XplatUIX11.RootWindow;
				XplatUIX11.listen = new Socket(2, 1, 0);
				IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Loopback, 0);
				XplatUIX11.listen.Bind(ipendPoint);
				XplatUIX11.listen.Listen(1);
				XplatUIX11.network_buffer = new byte[10];
				XplatUIX11.wake = new Socket(2, 1, 0);
				XplatUIX11.wake.Connect(XplatUIX11.listen.LocalEndPoint);
				XplatUIX11.wake_receive = XplatUIX11.listen.Accept();
				XplatUIX11.pollfds = new Pollfd[2];
				XplatUIX11.pollfds[0] = default(Pollfd);
				XplatUIX11.pollfds[0].fd = XplatUIX11.XConnectionNumber(XplatUIX11.DisplayHandle);
				XplatUIX11.pollfds[0].events = 1;
				XplatUIX11.pollfds[1] = default(Pollfd);
				XplatUIX11.pollfds[1].fd = XplatUIX11.wake_receive.Handle.ToInt32();
				XplatUIX11.pollfds[1].events = 1;
				XplatUIX11.Keyboard = new X11Keyboard(XplatUIX11.DisplayHandle, XplatUIX11.FosterParent);
				XplatUIX11.Dnd = new X11Dnd(XplatUIX11.DisplayHandle, XplatUIX11.Keyboard);
				XplatUIX11.DoubleClickInterval = 500;
				XplatUIX11.HoverState.Interval = 500;
				XplatUIX11.HoverState.Timer = new Timer();
				XplatUIX11.HoverState.Timer.Enabled = false;
				XplatUIX11.HoverState.Timer.Interval = XplatUIX11.HoverState.Interval;
				XplatUIX11.HoverState.Timer.Tick += new EventHandler(this.MouseHover);
				XplatUIX11.HoverState.Size = new Size(4, 4);
				XplatUIX11.HoverState.X = -1;
				XplatUIX11.HoverState.Y = -1;
				XplatUIX11.ActiveWindow = IntPtr.Zero;
				XplatUIX11.FocusWindow = IntPtr.Zero;
				XplatUIX11.ModalWindows = new Stack(3);
				XplatUIX11.MouseState = MouseButtons.None;
				this.mouse_position = new Point(0, 0);
				XplatUIX11.Caret.Timer = new Timer();
				XplatUIX11.Caret.Timer.Interval = 500;
				XplatUIX11.Caret.Timer.Tick += new EventHandler(this.CaretCallback);
				XplatUIX11.SetupAtoms();
				XplatUIX11.XSelectInput(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, new IntPtr((int)(EventMask.PropertyChangeMask | XplatUIX11.Keyboard.KeyEventMask)));
				XplatUIX11.ErrorHandler = new XErrorHandler(this.HandleError);
				XplatUIX11.XSetErrorHandler(XplatUIX11.ErrorHandler);
				return;
			}
			throw new ArgumentNullException("Display", "Could not open display (X-Server required. Check you DISPLAY environment variable)");
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x0011E06C File Offset: 0x0011C26C
		private int unixtime()
		{
			return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x0011E098 File Offset: 0x0011C298
		private static void SetupAtoms()
		{
			string[] array = new string[]
			{
				"WM_PROTOCOLS", "WM_DELETE_WINDOW", "WM_TAKE_FOCUS", "_NET_DESKTOP_GEOMETRY", "_NET_CURRENT_DESKTOP", "_NET_ACTIVE_WINDOW", "_NET_WORKAREA", "_NET_WM_NAME", "_NET_WM_WINDOW_TYPE", "_NET_WM_STATE",
				"_NET_WM_ICON", "_NET_WM_USER_TIME", "_NET_FRAME_EXTENTS", "_NET_SYSTEM_TRAY_OPCODE", "_NET_WM_STATE_MAXIMIZED_HORZ", "_NET_WM_STATE_MAXIMIZED_VERT", "_NET_WM_STATE_HIDDEN", "_XEMBED", "_XEMBED_INFO", "_MOTIF_WM_HINTS",
				"_NET_WM_STATE_SKIP_TASKBAR", "_NET_WM_STATE_ABOVE", "_NET_WM_STATE_MODAL", "_NET_WM_CONTEXT_HELP", "_NET_WM_WINDOW_OPACITY", "_NET_WM_WINDOW_TYPE_UTILITY", "_NET_WM_WINDOW_TYPE_NORMAL", "CLIPBOARD", "PRIMARY", "COMPOUND_TEXT",
				"UTF8_STRING", "UTF16_STRING", "RICHTEXTFORMAT", "TARGETS", "_SWF_AsyncAtom", "_SWF_PostMessageAtom", "_SWF_HoverAtom"
			};
			IntPtr[] array2 = new IntPtr[array.Length];
			XplatUIX11.XInternAtoms(XplatUIX11.DisplayHandle, array, array.Length, false, array2);
			int num = 0;
			XplatUIX11.WM_PROTOCOLS = array2[num++];
			XplatUIX11.WM_DELETE_WINDOW = array2[num++];
			XplatUIX11.WM_TAKE_FOCUS = array2[num++];
			XplatUIX11._NET_DESKTOP_GEOMETRY = array2[num++];
			XplatUIX11._NET_CURRENT_DESKTOP = array2[num++];
			XplatUIX11._NET_ACTIVE_WINDOW = array2[num++];
			XplatUIX11._NET_WORKAREA = array2[num++];
			XplatUIX11._NET_WM_NAME = array2[num++];
			XplatUIX11._NET_WM_WINDOW_TYPE = array2[num++];
			XplatUIX11._NET_WM_STATE = array2[num++];
			XplatUIX11._NET_WM_ICON = array2[num++];
			XplatUIX11._NET_WM_USER_TIME = array2[num++];
			XplatUIX11._NET_FRAME_EXTENTS = array2[num++];
			XplatUIX11._NET_SYSTEM_TRAY_OPCODE = array2[num++];
			XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ = array2[num++];
			XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT = array2[num++];
			XplatUIX11._NET_WM_STATE_HIDDEN = array2[num++];
			XplatUIX11._XEMBED = array2[num++];
			XplatUIX11._XEMBED_INFO = array2[num++];
			XplatUIX11._MOTIF_WM_HINTS = array2[num++];
			XplatUIX11._NET_WM_STATE_SKIP_TASKBAR = array2[num++];
			XplatUIX11._NET_WM_STATE_ABOVE = array2[num++];
			XplatUIX11._NET_WM_STATE_MODAL = array2[num++];
			XplatUIX11._NET_WM_CONTEXT_HELP = array2[num++];
			XplatUIX11._NET_WM_WINDOW_OPACITY = array2[num++];
			XplatUIX11._NET_WM_WINDOW_TYPE_UTILITY = array2[num++];
			XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL = array2[num++];
			XplatUIX11.CLIPBOARD = array2[num++];
			XplatUIX11.PRIMARY = array2[num++];
			XplatUIX11.OEMTEXT = array2[num++];
			XplatUIX11.UTF8_STRING = array2[num++];
			XplatUIX11.UTF16_STRING = array2[num++];
			XplatUIX11.RICHTEXTFORMAT = array2[num++];
			XplatUIX11.TARGETS = array2[num++];
			XplatUIX11.AsyncAtom = array2[num++];
			XplatUIX11.PostAtom = array2[num++];
			XplatUIX11.HoverState.Atom = array2[num++];
			XplatUIX11._NET_SYSTEM_TRAY_S = XplatUIX11.XInternAtom(XplatUIX11.DisplayHandle, "_NET_SYSTEM_TRAY_S" + XplatUIX11.ScreenNo.ToString(), false);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x0011E3F4 File Offset: 0x0011C5F4
		private void GetSystrayManagerWindow()
		{
			XplatUIX11.XGrabServer(XplatUIX11.DisplayHandle);
			XplatUIX11.SystrayMgrWindow = XplatUIX11.XGetSelectionOwner(XplatUIX11.DisplayHandle, XplatUIX11._NET_SYSTEM_TRAY_S);
			XplatUIX11.XUngrabServer(XplatUIX11.DisplayHandle);
			XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x0011E42C File Offset: 0x0011C62C
		private void SendNetWMMessage(IntPtr window, IntPtr message_type, IntPtr l0, IntPtr l1, IntPtr l2)
		{
			XEvent xevent = default(XEvent);
			xevent.ClientMessageEvent.type = XEventName.ClientMessage;
			xevent.ClientMessageEvent.send_event = true;
			xevent.ClientMessageEvent.window = window;
			xevent.ClientMessageEvent.message_type = message_type;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = l0;
			xevent.ClientMessageEvent.ptr2 = l1;
			xevent.ClientMessageEvent.ptr3 = l2;
			XplatUIX11.XSendEvent(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, false, new IntPtr(1572864), ref xevent);
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x0011E4CC File Offset: 0x0011C6CC
		private void SendNetClientMessage(IntPtr window, IntPtr message_type, IntPtr l0, IntPtr l1, IntPtr l2)
		{
			XEvent xevent = default(XEvent);
			xevent.ClientMessageEvent.type = XEventName.ClientMessage;
			xevent.ClientMessageEvent.send_event = true;
			xevent.ClientMessageEvent.window = window;
			xevent.ClientMessageEvent.message_type = message_type;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = l0;
			xevent.ClientMessageEvent.ptr2 = l1;
			xevent.ClientMessageEvent.ptr3 = l2;
			XplatUIX11.XSendEvent(XplatUIX11.DisplayHandle, window, false, new IntPtr(0), ref xevent);
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x0011E564 File Offset: 0x0011C764
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

		// Token: 0x06004A2B RID: 18987 RVA: 0x0011E650 File Offset: 0x0011C850
		private bool StyleSet(int s, WindowStyles ws)
		{
			return (s & (int)ws) == (int)ws;
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0011E658 File Offset: 0x0011C858
		private bool ExStyleSet(int ex, WindowExStyles exws)
		{
			return (ex & (int)exws) == (int)exws;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0011E660 File Offset: 0x0011C860
		internal static Rectangle TranslateClientRectangleToXClientRectangle(Hwnd hwnd)
		{
			return XplatUIX11.TranslateClientRectangleToXClientRectangle(hwnd, Control.FromHandle(hwnd.Handle));
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x0011E674 File Offset: 0x0011C874
		internal static Rectangle TranslateClientRectangleToXClientRectangle(Hwnd hwnd, Control ctrl)
		{
			Rectangle rectangle = hwnd.ClientRect;
			Form form = ctrl as Form;
			CreateParams createParams = null;
			if (form != null)
			{
				createParams = form.GetCreateParams();
			}
			if (form != null && form.window_manager == null && !createParams.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
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

		// Token: 0x06004A2F RID: 18991 RVA: 0x0011E770 File Offset: 0x0011C970
		internal static Size TranslateWindowSizeToXWindowSize(CreateParams cp)
		{
			return XplatUIX11.TranslateWindowSizeToXWindowSize(cp, new Size(cp.Width, cp.Height));
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x0011E78C File Offset: 0x0011C98C
		internal static Size TranslateWindowSizeToXWindowSize(CreateParams cp, Size size)
		{
			Form form = cp.control as Form;
			if (form != null && form.window_manager == null && !cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
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

		// Token: 0x06004A31 RID: 18993 RVA: 0x0011E838 File Offset: 0x0011CA38
		internal static Size TranslateXWindowSizeToWindowSize(CreateParams cp, int xWidth, int xHeight)
		{
			Size size;
			size..ctor(xWidth, xHeight);
			Form form = cp.control as Form;
			if (form != null && form.window_manager == null && !cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
			{
				Hwnd.Borders borders = Hwnd.GetBorders(cp, null);
				Size size2 = size;
				size2.Width += borders.left + borders.right;
				size2.Height += borders.top + borders.bottom;
				size = size2;
			}
			return size;
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x0011E8C4 File Offset: 0x0011CAC4
		internal static Point GetTopLevelWindowLocation(Hwnd hwnd)
		{
			int num;
			int num2;
			IntPtr intPtr;
			XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.RootWindow, 0, 0, out num, out num2, out intPtr);
			Hwnd.Borders borders = XplatUIX11.FrameExtents(hwnd.whole_window);
			num -= borders.left;
			num2 -= borders.top;
			return new Point(num, num2);
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x0011E918 File Offset: 0x0011CB18
		private void DeriveStyles(int Style, int ExStyle, out FormBorderStyle border_style, out bool border_static, out TitleStyle title_style, out int caption_height, out int tool_caption_height)
		{
			caption_height = 0;
			tool_caption_height = 19;
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
					caption_height = 19;
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
					caption_height = 19;
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

		// Token: 0x06004A34 RID: 18996 RVA: 0x0011EB20 File Offset: 0x0011CD20
		private void SetHwndStyles(Hwnd hwnd, CreateParams cp)
		{
			this.DeriveStyles(cp.Style, cp.ExStyle, out hwnd.border_style, out hwnd.border_static, out hwnd.title_style, out hwnd.caption_height, out hwnd.tool_caption_height);
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x0011EB60 File Offset: 0x0011CD60
		private void SetWMStyles(Hwnd hwnd, CreateParams cp)
		{
			if (cp.HasWindowManager && !cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
			{
				return;
			}
			int[] array = new int[8];
			MotifWmHints motifWmHints = default(MotifWmHints);
			MotifFunctions motifFunctions = (MotifFunctions)0;
			MotifDecorations motifDecorations = (MotifDecorations)0;
			IntPtr intPtr = XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL;
			IntPtr intPtr2 = IntPtr.Zero;
			motifWmHints.flags = (IntPtr)3;
			motifWmHints.functions = (IntPtr)0;
			motifWmHints.decorations = (IntPtr)0;
			Form form = cp.control as Form;
			if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
			{
				motifFunctions |= MotifFunctions.Resize | MotifFunctions.Move | MotifFunctions.Minimize | MotifFunctions.Maximize;
			}
			else if (form != null && form.FormBorderStyle == FormBorderStyle.None)
			{
				motifFunctions |= MotifFunctions.All | MotifFunctions.Resize;
			}
			else
			{
				if (this.StyleSet(cp.Style, WindowStyles.WS_CAPTION))
				{
					motifFunctions |= MotifFunctions.Move;
					motifDecorations |= MotifDecorations.Title | MotifDecorations.Menu;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_THICKFRAME))
				{
					motifFunctions |= MotifFunctions.Resize | MotifFunctions.Move;
					motifDecorations |= MotifDecorations.Border | MotifDecorations.ResizeH;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_GROUP))
				{
					motifFunctions |= MotifFunctions.Minimize;
					motifDecorations |= MotifDecorations.Minimize;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_TABSTOP))
				{
					motifFunctions |= MotifFunctions.Maximize;
					motifDecorations |= MotifDecorations.Maximize;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_THICKFRAME))
				{
					motifFunctions |= MotifFunctions.Resize;
					motifDecorations |= MotifDecorations.ResizeH;
				}
				if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_DLGMODALFRAME))
				{
					motifDecorations |= MotifDecorations.Border;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_BORDER))
				{
					motifDecorations |= MotifDecorations.Border;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_DLGFRAME))
				{
					motifDecorations |= MotifDecorations.Border;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_SYSMENU))
				{
					motifFunctions |= MotifFunctions.Close;
				}
				else
				{
					motifFunctions &= ~(MotifFunctions.Minimize | MotifFunctions.Maximize | MotifFunctions.Close);
					motifDecorations &= ~(MotifDecorations.Menu | MotifDecorations.Minimize | MotifDecorations.Maximize);
					if (cp.Caption == string.Empty)
					{
						motifFunctions &= ~MotifFunctions.Move;
						motifDecorations &= ~(MotifDecorations.ResizeH | MotifDecorations.Title);
					}
				}
			}
			if ((motifFunctions & MotifFunctions.Resize) == (MotifFunctions)0)
			{
				hwnd.fixed_size = true;
				Rectangle rectangle;
				rectangle..ctor(cp.X, cp.Y, cp.Width, cp.Height);
				this.SetWindowMinMax(hwnd.Handle, rectangle, rectangle.Size, rectangle.Size, cp);
			}
			else
			{
				hwnd.fixed_size = false;
			}
			motifWmHints.functions = (IntPtr)((int)motifFunctions);
			motifWmHints.decorations = (IntPtr)((int)motifDecorations);
			if (cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
			{
				intPtr = XplatUIX11._NET_WM_WINDOW_TYPE_UTILITY;
			}
			else
			{
				intPtr = XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL;
			}
			bool flag = !cp.IsSet(WindowExStyles.WS_EX_APPWINDOW) || (cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW) && form != null && form.Parent != null && !form.ShowInTaskbar);
			if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_TOOLWINDOW) && form != null && !hwnd.reparented && form.Owner != null && form.Owner.Handle != IntPtr.Zero)
			{
				Hwnd hwnd2 = Hwnd.ObjectFromHandle(form.Owner.Handle);
				if (hwnd2 != null)
				{
					intPtr2 = hwnd2.whole_window;
				}
			}
			if (this.StyleSet(cp.Style, WindowStyles.WS_POPUP) && hwnd.parent != null && hwnd.parent.whole_window != IntPtr.Zero)
			{
				intPtr2 = hwnd.parent.whole_window;
			}
			FormWindowState formWindowState = this.GetWindowState(hwnd.Handle);
			if (formWindowState == (FormWindowState)(-1))
			{
				formWindowState = FormWindowState.Normal;
			}
			Rectangle rectangle2 = XplatUIX11.TranslateClientRectangleToXClientRectangle(hwnd);
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				int num = 0;
				array[0] = intPtr.ToInt32();
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_WINDOW_TYPE, (IntPtr)4, 32, PropertyMode.Replace, array, 1);
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._MOTIF_WM_HINTS, XplatUIX11._MOTIF_WM_HINTS, 32, PropertyMode.Replace, ref motifWmHints, 5);
				if (intPtr2 != IntPtr.Zero)
				{
					XplatUIX11.XSetTransientForHint(XplatUIX11.DisplayHandle, hwnd.whole_window, intPtr2);
				}
				XplatUIX11.MoveResizeWindow(XplatUIX11.DisplayHandle, hwnd.client_window, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height);
				if (flag)
				{
					array[num++] = XplatUIX11._NET_WM_STATE_SKIP_TASKBAR.ToInt32();
				}
				if (formWindowState == FormWindowState.Maximized)
				{
					array[num++] = XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ.ToInt32();
					array[num++] = XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT.ToInt32();
				}
				if (form != null && form.Modal)
				{
					array[num++] = XplatUIX11._NET_WM_STATE_MODAL.ToInt32();
				}
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)4, 32, PropertyMode.Replace, array, num);
				num = 0;
				IntPtr[] array2 = new IntPtr[2];
				array2[num++] = XplatUIX11.WM_DELETE_WINDOW;
				if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_CONTEXTHELP))
				{
					array2[num++] = XplatUIX11._NET_WM_CONTEXT_HELP;
				}
				XplatUIX11.XSetWMProtocols(XplatUIX11.DisplayHandle, hwnd.whole_window, array2, num);
			}
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x0011F0D8 File Offset: 0x0011D2D8
		private void SetIcon(Hwnd hwnd, Icon icon)
		{
			if (icon == null)
			{
				XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_ICON);
			}
			else
			{
				Bitmap bitmap = icon.ToBitmap();
				int num = 0;
				int num2 = bitmap.Width * bitmap.Height + 2;
				IntPtr[] array = new IntPtr[num2];
				array[num++] = (IntPtr)bitmap.Width;
				array[num++] = (IntPtr)bitmap.Height;
				for (int i = 0; i < bitmap.Height; i++)
				{
					for (int j = 0; j < bitmap.Width; j++)
					{
						array[num++] = (IntPtr)bitmap.GetPixel(j, i).ToArgb();
					}
				}
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_ICON, (IntPtr)6, 32, PropertyMode.Replace, array, num2);
			}
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x0011F1DC File Offset: 0x0011D3DC
		private void WakeupMain()
		{
			XplatUIX11.wake.Send(new byte[] { byte.MaxValue });
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x0011F1F8 File Offset: 0x0011D3F8
		private XEventQueue ThreadQueue(Thread thread)
		{
			XEventQueue xeventQueue = (XEventQueue)XplatUIX11.MessageQueues[thread];
			if (xeventQueue == null)
			{
				xeventQueue = new XEventQueue(thread);
				XplatUIX11.MessageQueues[thread] = xeventQueue;
			}
			return xeventQueue;
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x0011F230 File Offset: 0x0011D430
		private void TranslatePropertyToClipboard(IntPtr property)
		{
			IntPtr zero = IntPtr.Zero;
			XplatUIX11.Clipboard.Item = null;
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.FosterParent, property, IntPtr.Zero, new IntPtr(int.MaxValue), true, (IntPtr)0, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
			if ((long)intPtr2 > 0L)
			{
				if (property == (IntPtr)31)
				{
					XplatUIX11.Clipboard.Item = Marshal.PtrToStringAnsi(zero);
				}
				else if (!(property == (IntPtr)5))
				{
					if (!(property == (IntPtr)20))
					{
						if (property == XplatUIX11.OEMTEXT)
						{
							XplatUIX11.Clipboard.Item = Marshal.PtrToStringAnsi(zero);
						}
						else if (property == XplatUIX11.UTF8_STRING)
						{
							byte[] array = new byte[(int)intPtr2];
							for (int i = 0; i < (int)intPtr2; i++)
							{
								array[i] = Marshal.ReadByte(zero, i);
							}
							XplatUIX11.Clipboard.Item = Encoding.UTF8.GetString(array);
						}
						else if (property == XplatUIX11.UTF16_STRING)
						{
							XplatUIX11.Clipboard.Item = Marshal.PtrToStringUni(zero, Encoding.Unicode.GetMaxCharCount((int)intPtr2));
						}
						else if (property == XplatUIX11.RICHTEXTFORMAT)
						{
							XplatUIX11.Clipboard.Item = Marshal.PtrToStringAnsi(zero);
						}
						else if (DataFormats.ContainsFormat(property.ToInt32()) && DataFormats.GetFormat(property.ToInt32()).is_serializable)
						{
							MemoryStream memoryStream = new MemoryStream((int)intPtr2);
							for (int j = 0; j < (int)intPtr2; j++)
							{
								memoryStream.WriteByte(Marshal.ReadByte(zero, j));
							}
							memoryStream.Position = 0L;
							BinaryFormatter binaryFormatter = new BinaryFormatter();
							XplatUIX11.Clipboard.Item = binaryFormatter.Deserialize(memoryStream);
							memoryStream.Close();
						}
					}
				}
				XplatUIX11.XFree(zero);
			}
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0011F458 File Offset: 0x0011D658
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
				if (!hwnd.expose_pending)
				{
					if (!hwnd.nc_expose_pending)
					{
						hwnd.Queue.Paint.Enqueue(hwnd);
					}
					hwnd.expose_pending = true;
				}
			}
			else
			{
				hwnd.AddNcInvalidArea(x, y, width, height);
				if (!hwnd.nc_expose_pending)
				{
					if (!hwnd.expose_pending)
					{
						hwnd.Queue.Paint.Enqueue(hwnd);
					}
					hwnd.nc_expose_pending = true;
				}
			}
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x0011F550 File Offset: 0x0011D750
		private static Hwnd.Borders FrameExtents(IntPtr window)
		{
			IntPtr zero = IntPtr.Zero;
			Hwnd.Borders borders = default(Hwnd.Borders);
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, window, XplatUIX11._NET_FRAME_EXTENTS, IntPtr.Zero, new IntPtr(16), false, (IntPtr)6, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
			if (zero != IntPtr.Zero)
			{
				if (intPtr2.ToInt32() == 4)
				{
					borders.left = Marshal.ReadInt32(zero, 0);
					borders.right = Marshal.ReadInt32(zero, IntPtr.Size);
					borders.top = Marshal.ReadInt32(zero, 2 * IntPtr.Size);
					borders.bottom = Marshal.ReadInt32(zero, 3 * IntPtr.Size);
				}
				XplatUIX11.XFree(zero);
			}
			return borders;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x0011F610 File Offset: 0x0011D810
		private void AddConfigureNotify(XEvent xevent)
		{
			Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(xevent.ConfigureEvent.window);
			if (objectFromWindow == null || objectFromWindow.zombie)
			{
				return;
			}
			if (xevent.ConfigureEvent.window == objectFromWindow.whole_window)
			{
				if (objectFromWindow.parent == null)
				{
					Point topLevelWindowLocation = XplatUIX11.GetTopLevelWindowLocation(objectFromWindow);
					objectFromWindow.x = topLevelWindowLocation.X;
					objectFromWindow.y = topLevelWindowLocation.Y;
				}
				Control control = Control.FromHandle(objectFromWindow.Handle);
				Size size;
				if (control != null)
				{
					size = XplatUIX11.TranslateXWindowSizeToWindowSize(control.GetCreateParams(), xevent.ConfigureEvent.width, xevent.ConfigureEvent.height);
				}
				else
				{
					size..ctor(xevent.ConfigureEvent.width, xevent.ConfigureEvent.height);
				}
				objectFromWindow.width = size.Width;
				objectFromWindow.height = size.Height;
				objectFromWindow.ClientRect = Rectangle.Empty;
				object configure_lock = objectFromWindow.configure_lock;
				lock (configure_lock)
				{
					if (!objectFromWindow.configure_pending)
					{
						objectFromWindow.Queue.EnqueueLocked(xevent);
						objectFromWindow.configure_pending = true;
					}
				}
			}
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x0011F760 File Offset: 0x0011D960
		private void ShowCaret()
		{
			if (XplatUIX11.Caret.gc == IntPtr.Zero || XplatUIX11.Caret.On)
			{
				return;
			}
			XplatUIX11.Caret.On = true;
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, XplatUIX11.Caret.Window, XplatUIX11.Caret.gc, XplatUIX11.Caret.X, XplatUIX11.Caret.Y, XplatUIX11.Caret.X, XplatUIX11.Caret.Y + XplatUIX11.Caret.Height);
			}
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0011F828 File Offset: 0x0011DA28
		private void HideCaret()
		{
			if (XplatUIX11.Caret.gc == IntPtr.Zero || !XplatUIX11.Caret.On)
			{
				return;
			}
			XplatUIX11.Caret.On = false;
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, XplatUIX11.Caret.Window, XplatUIX11.Caret.gc, XplatUIX11.Caret.X, XplatUIX11.Caret.Y, XplatUIX11.Caret.X, XplatUIX11.Caret.Y + XplatUIX11.Caret.Height);
			}
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0011F8F0 File Offset: 0x0011DAF0
		private int NextTimeout(ArrayList timers, DateTime now)
		{
			int num = 0;
			foreach (object obj in timers)
			{
				Timer timer = (Timer)obj;
				int num2 = (int)(timer.Expires - now).TotalMilliseconds;
				if (num2 < 0)
				{
					return 0;
				}
				if (num2 < num)
				{
					num = num2;
				}
			}
			if (num < Timer.Minimum)
			{
				num = Timer.Minimum;
			}
			if (num > 1000)
			{
				num = 1000;
			}
			return num;
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0011F9B0 File Offset: 0x0011DBB0
		private void CheckTimers(ArrayList timers, DateTime now)
		{
			if (timers.Count == 0)
			{
				return;
			}
			for (int i = 0; i < timers.Count; i++)
			{
				Timer timer = (Timer)timers[i];
				if (timer.Enabled && timer.Expires <= now && !timer.Busy && (XplatUIX11.in_doevents || (Application.MWFThread.Current.Context != null && (Application.MWFThread.Current.Context.MainForm == null || Application.MWFThread.Current.Context.MainForm.IsLoaded))))
				{
					timer.Busy = true;
					timer.Update(now);
					timer.FireTick();
					timer.Busy = false;
				}
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x0011FA78 File Offset: 0x0011DC78
		private void WaitForHwndMessage(Hwnd hwnd, Msg message)
		{
			this.WaitForHwndMessage(hwnd, message, false);
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x0011FA84 File Offset: 0x0011DC84
		private void WaitForHwndMessage(Hwnd hwnd, Msg message, bool process)
		{
			MSG msg = default(MSG);
			XEventQueue xeventQueue = this.ThreadQueue(Thread.CurrentThread);
			xeventQueue.DispatchIdle = false;
			bool flag = false;
			string text = hwnd.Handle + ":" + message;
			if (!XplatUIX11.messageHold.ContainsKey(text))
			{
				XplatUIX11.messageHold.Add(text, 1);
			}
			else
			{
				XplatUIX11.messageHold[text] = (int)XplatUIX11.messageHold[text] + 1;
			}
			for (;;)
			{
				if (this.PeekMessage(xeventQueue, ref msg, IntPtr.Zero, 0, 0, 1U))
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
				flag = !XplatUIX11.messageHold.ContainsKey(text) || (int)XplatUIX11.messageHold[text] < 1 || flag;
				if (flag)
				{
					goto IL_0146;
				}
			}
			if (process)
			{
				this.TranslateMessage(ref msg);
				this.DispatchMessage(ref msg);
			}
			IL_0146:
			XplatUIX11.messageHold.Remove(text);
			xeventQueue.DispatchIdle = true;
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0011FBEC File Offset: 0x0011DDEC
		private void MapWindow(Hwnd hwnd, WindowType windows)
		{
			if (!hwnd.mapped)
			{
				Form form = Control.FromHandle(hwnd.Handle) as Form;
				if (form != null && form.WindowState == FormWindowState.Normal)
				{
					form.waiting_showwindow = true;
					this.SendMessage(hwnd.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
				}
				if (hwnd.zombie)
				{
					return;
				}
				if ((windows & WindowType.Whole) != (WindowType)0)
				{
					XplatUIX11.XMapWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				if ((windows & WindowType.Client) != (WindowType)0)
				{
					XplatUIX11.XMapWindow(XplatUIX11.DisplayHandle, hwnd.client_window);
				}
				hwnd.mapped = true;
				if (form != null && form.waiting_showwindow)
				{
					this.WaitForHwndMessage(hwnd, Msg.WM_SHOWWINDOW);
					CreateParams createParams = form.GetCreateParams();
					if (!this.ExStyleSet(createParams.ExStyle, WindowExStyles.WS_EX_MDICHILD) && !this.StyleSet(createParams.Style, WindowStyles.WS_CHILD))
					{
						this.WaitForHwndMessage(hwnd, Msg.WM_ACTIVATE, true);
					}
				}
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x0011FCE0 File Offset: 0x0011DEE0
		private void UnmapWindow(Hwnd hwnd, WindowType windows)
		{
			if (hwnd.mapped)
			{
				Form form = null;
				if (Control.FromHandle(hwnd.Handle) is Form)
				{
					form = Control.FromHandle(hwnd.Handle) as Form;
					if (form.WindowState == FormWindowState.Normal)
					{
						form.waiting_showwindow = true;
						this.SendMessage(hwnd.Handle, Msg.WM_SHOWWINDOW, IntPtr.Zero, IntPtr.Zero);
					}
				}
				if (hwnd.zombie)
				{
					return;
				}
				if ((windows & WindowType.Client) != (WindowType)0)
				{
					XplatUIX11.XUnmapWindow(XplatUIX11.DisplayHandle, hwnd.client_window);
				}
				if ((windows & WindowType.Whole) != (WindowType)0)
				{
					XplatUIX11.XUnmapWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				hwnd.mapped = false;
				if (form != null && form.waiting_showwindow)
				{
					this.WaitForHwndMessage(hwnd, Msg.WM_SHOWWINDOW);
					CreateParams createParams = form.GetCreateParams();
					if (!this.ExStyleSet(createParams.ExStyle, WindowExStyles.WS_EX_MDICHILD) && !this.StyleSet(createParams.Style, WindowStyles.WS_CHILD))
					{
						this.WaitForHwndMessage(hwnd, Msg.WM_ACTIVATE, true);
					}
				}
			}
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x0011FDE4 File Offset: 0x0011DFE4
		private void UpdateMessageQueue(XEventQueue queue)
		{
			DateTime utcNow = DateTime.UtcNow;
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			lock (xlibLock)
			{
				num = XplatUIX11.XPending(XplatUIX11.DisplayHandle);
			}
			if (num == 0)
			{
				if ((queue == null || queue.DispatchIdle) && this.Idle != null)
				{
					this.Idle.Invoke(this, EventArgs.Empty);
				}
				object xlibLock2 = XplatUIX11.XlibLock;
				lock (xlibLock2)
				{
					num = XplatUIX11.XPending(XplatUIX11.DisplayHandle);
				}
			}
			if (num == 0)
			{
				int num2 = 0;
				if (queue != null)
				{
					if (queue.Paint.Count > 0)
					{
						return;
					}
					num2 = this.NextTimeout(queue.timer_list, utcNow);
				}
				if (num2 > 0)
				{
					int num3 = XplatUIX11.pollfds.Length - 1;
					object obj = XplatUIX11.wake_waiting_lock;
					lock (obj)
					{
						if (!XplatUIX11.wake_waiting)
						{
							num3++;
							XplatUIX11.wake_waiting = true;
						}
					}
					Syscall.poll(XplatUIX11.pollfds, (uint)num3, num2);
					if (num3 == XplatUIX11.pollfds.Length)
					{
						if (XplatUIX11.pollfds[1].revents != null)
						{
							XplatUIX11.wake_receive.Receive(XplatUIX11.network_buffer, 0, 1, 0);
						}
						object obj2 = XplatUIX11.wake_waiting_lock;
						lock (obj2)
						{
							XplatUIX11.wake_waiting = false;
						}
					}
					object xlibLock3 = XplatUIX11.XlibLock;
					lock (xlibLock3)
					{
						num = XplatUIX11.XPending(XplatUIX11.DisplayHandle);
					}
				}
			}
			if (queue != null)
			{
				this.CheckTimers(queue.timer_list, utcNow);
			}
			XEvent xevent;
			Hwnd objectFromWindow;
			for (;;)
			{
				xevent = default(XEvent);
				object xlibLock4 = XplatUIX11.XlibLock;
				lock (xlibLock4)
				{
					if (XplatUIX11.XPending(XplatUIX11.DisplayHandle) == 0)
					{
						return;
					}
					XplatUIX11.XNextEvent(XplatUIX11.DisplayHandle, ref xevent);
					if (xevent.AnyEvent.type == XEventName.KeyPress || xevent.AnyEvent.type == XEventName.KeyRelease)
					{
						XplatUIX11.Keyboard.PreFilter(xevent);
						if (XplatUIX11.XFilterEvent(ref xevent, XplatUIX11.Keyboard.ClientWindow))
						{
							continue;
						}
					}
					else if (XplatUIX11.XFilterEvent(ref xevent, IntPtr.Zero))
					{
						continue;
					}
				}
				objectFromWindow = Hwnd.GetObjectFromWindow(xevent.AnyEvent.window);
				if (objectFromWindow != null)
				{
					switch (xevent.type)
					{
					case XEventName.KeyPress:
						goto IL_0ABA;
					case XEventName.KeyRelease:
					{
						if (XplatUIX11.detectable_key_auto_repeat || XplatUIX11.XPending(XplatUIX11.DisplayHandle) == 0)
						{
							goto IL_0A66;
						}
						XEvent xevent2 = default(XEvent);
						XplatUIX11.XPeekEvent(XplatUIX11.DisplayHandle, ref xevent2);
						if (xevent2.type != XEventName.KeyPress || xevent2.KeyEvent.keycode != xevent.KeyEvent.keycode || !(xevent2.KeyEvent.time == xevent.KeyEvent.time))
						{
							goto IL_0A66;
						}
						break;
					}
					case XEventName.ButtonPress:
					case XEventName.ButtonRelease:
					case XEventName.EnterNotify:
					case XEventName.LeaveNotify:
					case XEventName.FocusIn:
					case XEventName.FocusOut:
					case XEventName.CreateNotify:
					case XEventName.DestroyNotify:
					case XEventName.UnmapNotify:
					case XEventName.MapNotify:
					case XEventName.ReparentNotify:
					case XEventName.ClientMessage:
						objectFromWindow.Queue.EnqueueLocked(xevent);
						break;
					case XEventName.MotionNotify:
						if (Thread.CurrentThread != objectFromWindow.Queue.Thread || objectFromWindow.Queue.Count <= 0 || objectFromWindow.Queue.Peek().AnyEvent.type != XEventName.MotionNotify)
						{
							goto IL_0AB5;
						}
						break;
					case XEventName.Expose:
						this.AddExpose(objectFromWindow, xevent.ExposeEvent.window == objectFromWindow.ClientWindow, xevent.ExposeEvent.x, xevent.ExposeEvent.y, xevent.ExposeEvent.width, xevent.ExposeEvent.height);
						break;
					case XEventName.ConfigureNotify:
						this.AddConfigureNotify(xevent);
						break;
					case XEventName.PropertyNotify:
						if (xevent.PropertyEvent.atom == XplatUIX11._NET_ACTIVE_WINDOW)
						{
							IntPtr zero = IntPtr.Zero;
							IntPtr activeWindow = XplatUIX11.ActiveWindow;
							IntPtr intPtr;
							int num4;
							IntPtr intPtr2;
							IntPtr intPtr3;
							XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_ACTIVE_WINDOW, IntPtr.Zero, new IntPtr(1), false, (IntPtr)33, out intPtr, out num4, out intPtr2, out intPtr3, ref zero);
							if ((long)intPtr2 > 0L && zero != IntPtr.Zero)
							{
								XplatUIX11.ActiveWindow = Hwnd.GetHandleFromWindow((IntPtr)Marshal.ReadInt32(zero));
								XplatUIX11.XFree(zero);
								if (activeWindow != XplatUIX11.ActiveWindow)
								{
									if (activeWindow != IntPtr.Zero)
									{
										this.PostMessage(activeWindow, Msg.WM_ACTIVATE, (IntPtr)0, IntPtr.Zero);
									}
									if (XplatUIX11.ActiveWindow != IntPtr.Zero)
									{
										this.PostMessage(XplatUIX11.ActiveWindow, Msg.WM_ACTIVATE, (IntPtr)1, IntPtr.Zero);
									}
								}
								if (XplatUIX11.ModalWindows.Count != 0)
								{
									Form form = Control.FromHandle(XplatUIX11.ActiveWindow) as Form;
									if (form != null)
									{
										Form form2 = Control.FromHandle((IntPtr)XplatUIX11.ModalWindows.Peek()) as Form;
										if (XplatUIX11.ActiveWindow != (IntPtr)XplatUIX11.ModalWindows.Peek() && (form2 == null || form.context == form2.context))
										{
											this.Activate((IntPtr)XplatUIX11.ModalWindows.Peek());
										}
									}
								}
							}
						}
						else if (xevent.PropertyEvent.atom == XplatUIX11._NET_WM_STATE)
						{
							objectFromWindow.cached_window_state = (FormWindowState)(-1);
							this.PostMessage(objectFromWindow.Handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
						}
						break;
					case XEventName.SelectionRequest:
						if (!XplatUIX11.Dnd.HandleSelectionRequestEvent(ref xevent))
						{
							XEvent xevent3 = default(XEvent);
							xevent3.SelectionEvent.type = XEventName.SelectionNotify;
							xevent3.SelectionEvent.send_event = true;
							xevent3.SelectionEvent.display = XplatUIX11.DisplayHandle;
							xevent3.SelectionEvent.selection = xevent.SelectionRequestEvent.selection;
							xevent3.SelectionEvent.target = xevent.SelectionRequestEvent.target;
							xevent3.SelectionEvent.requestor = xevent.SelectionRequestEvent.requestor;
							xevent3.SelectionEvent.time = xevent.SelectionRequestEvent.time;
							xevent3.SelectionEvent.property = IntPtr.Zero;
							IntPtr target = xevent.SelectionRequestEvent.target;
							if (target == XplatUIX11.TARGETS)
							{
								int[] array = new int[5];
								int num5 = 0;
								if (XplatUIX11.Clipboard.IsSourceText)
								{
									array[num5++] = 31;
									array[num5++] = (int)XplatUIX11.OEMTEXT;
									array[num5++] = (int)XplatUIX11.UTF8_STRING;
									array[num5++] = (int)XplatUIX11.UTF16_STRING;
									array[num5++] = (int)XplatUIX11.RICHTEXTFORMAT;
								}
								else if (XplatUIX11.Clipboard.IsSourceImage)
								{
									array[num5++] = 20;
									array[num5++] = 5;
								}
								XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 32, PropertyMode.Replace, array, num5);
								xevent3.SelectionEvent.property = xevent.SelectionRequestEvent.property;
							}
							else if (target == XplatUIX11.RICHTEXTFORMAT)
							{
								string rtfText = XplatUIX11.Clipboard.GetRtfText();
								if (rtfText != null)
								{
									byte[] bytes = Encoding.ASCII.GetBytes(rtfText);
									int num6 = bytes.Length;
									IntPtr intPtr4 = Marshal.AllocHGlobal(num6);
									for (int i = 0; i < num6; i++)
									{
										Marshal.WriteByte(intPtr4, i, bytes[i]);
									}
									XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 8, PropertyMode.Replace, intPtr4, num6);
									xevent3.SelectionEvent.property = xevent.SelectionRequestEvent.property;
									Marshal.FreeHGlobal(intPtr4);
								}
							}
							else if (XplatUIX11.Clipboard.IsSourceText && (target == (IntPtr)31 || target == XplatUIX11.OEMTEXT || target == XplatUIX11.UTF16_STRING || target == XplatUIX11.UTF8_STRING))
							{
								IntPtr intPtr5 = IntPtr.Zero;
								Encoding encoding = null;
								IntPtr target2 = xevent.SelectionRequestEvent.target;
								if (target2 == (IntPtr)31 || target2 == XplatUIX11.OEMTEXT)
								{
									encoding = Encoding.ASCII;
								}
								else if (target2 == XplatUIX11.UTF16_STRING)
								{
									encoding = Encoding.Unicode;
								}
								else if (target2 == XplatUIX11.UTF8_STRING)
								{
									encoding = Encoding.UTF8;
								}
								byte[] bytes2 = encoding.GetBytes(XplatUIX11.Clipboard.GetPlainText());
								intPtr5 = Marshal.AllocHGlobal(bytes2.Length);
								int num7 = bytes2.Length;
								for (int j = 0; j < num7; j++)
								{
									Marshal.WriteByte(intPtr5, j, bytes2[j]);
								}
								if (intPtr5 != IntPtr.Zero)
								{
									XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 8, PropertyMode.Replace, intPtr5, num7);
									xevent3.SelectionEvent.property = xevent.SelectionRequestEvent.property;
									Marshal.FreeHGlobal(intPtr5);
								}
							}
							else if (XplatUIX11.Clipboard.GetSource(target.ToInt32()) != null)
							{
								if (DataFormats.GetFormat(target.ToInt32()).is_serializable)
								{
									object source = XplatUIX11.Clipboard.GetSource(target.ToInt32());
									BinaryFormatter binaryFormatter = new BinaryFormatter();
									MemoryStream memoryStream = new MemoryStream();
									binaryFormatter.Serialize(memoryStream, source);
									int num8 = (int)memoryStream.Length;
									IntPtr intPtr6 = Marshal.AllocHGlobal(num8);
									memoryStream.Position = 0L;
									for (int k = 0; k < num8; k++)
									{
										Marshal.WriteByte(intPtr6, k, (byte)memoryStream.ReadByte());
									}
									memoryStream.Close();
									XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 8, PropertyMode.Replace, intPtr6, num8);
									xevent3.SelectionEvent.property = xevent.SelectionRequestEvent.property;
									Marshal.FreeHGlobal(intPtr6);
								}
							}
							else if (XplatUIX11.Clipboard.IsSourceImage)
							{
								if (!(xevent.SelectionEvent.target == (IntPtr)20))
								{
									if (xevent.SelectionEvent.target == (IntPtr)20)
									{
									}
								}
							}
							XplatUIX11.XSendEvent(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, false, new IntPtr(0), ref xevent3);
						}
						break;
					case XEventName.SelectionNotify:
						if (XplatUIX11.Clipboard.Enumerating)
						{
							XplatUIX11.Clipboard.Enumerating = false;
							if (xevent.SelectionEvent.property != IntPtr.Zero)
							{
								XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, XplatUIX11.FosterParent, xevent.SelectionEvent.property);
								if (!XplatUIX11.Clipboard.Formats.Contains(xevent.SelectionEvent.property))
								{
									XplatUIX11.Clipboard.Formats.Add(xevent.SelectionEvent.property);
								}
							}
						}
						else if (XplatUIX11.Clipboard.Retrieving)
						{
							XplatUIX11.Clipboard.Retrieving = false;
							if (xevent.SelectionEvent.property != IntPtr.Zero)
							{
								this.TranslatePropertyToClipboard(xevent.SelectionEvent.property);
							}
							else
							{
								XplatUIX11.Clipboard.ClearSources();
								XplatUIX11.Clipboard.Item = null;
							}
						}
						else
						{
							XplatUIX11.Dnd.HandleSelectionNotifyEvent(ref xevent);
						}
						break;
					}
				}
			}
			IL_0A66:
			IL_0AB5:
			IL_0ABA:
			objectFromWindow.Queue.EnqueueLocked(xevent);
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x00120B4C File Offset: 0x0011ED4C
		private IntPtr GetMousewParam(int Delta)
		{
			int num = 0;
			if ((XplatUIX11.MouseState & MouseButtons.Left) != MouseButtons.None)
			{
				num |= 1;
			}
			if ((XplatUIX11.MouseState & MouseButtons.Middle) != MouseButtons.None)
			{
				num |= 16;
			}
			if ((XplatUIX11.MouseState & MouseButtons.Right) != MouseButtons.None)
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

		// Token: 0x06004A47 RID: 19015 RVA: 0x00120BCC File Offset: 0x0011EDCC
		private IntPtr XGetParent(IntPtr handle)
		{
			object xlibLock = XplatUIX11.XlibLock;
			IntPtr intPtr2;
			IntPtr intPtr3;
			lock (xlibLock)
			{
				IntPtr intPtr;
				int num;
				XplatUIX11.XQueryTree(XplatUIX11.DisplayHandle, handle, out intPtr, out intPtr2, out intPtr3, out num);
			}
			if (intPtr3 != IntPtr.Zero)
			{
				object xlibLock2 = XplatUIX11.XlibLock;
				lock (xlibLock2)
				{
					XplatUIX11.XFree(intPtr3);
				}
			}
			return intPtr2;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x00120C70 File Offset: 0x0011EE70
		private int HandleError(IntPtr display, ref XErrorEvent error_event)
		{
			if (error_event.request_code == (XRequest)this.render_major_opcode && error_event.minor_code == 7 && (int)error_event.error_code == this.render_first_error + 1)
			{
				return 0;
			}
			if (XplatUIX11.ErrorExceptions)
			{
				XplatUIX11.XUngrabPointer(display, IntPtr.Zero);
				throw new XplatUIX11.XException(error_event.display, error_event.resourceid, error_event.serial, error_event.error_code, error_event.request_code, error_event.minor_code);
			}
			Console.WriteLine("X11 Error encountered: {0}{1}\n", XplatUIX11.XException.GetMessage(error_event.display, error_event.resourceid, error_event.serial, error_event.error_code, error_event.request_code, error_event.minor_code), Environment.StackTrace);
			return 0;
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x00120D2C File Offset: 0x0011EF2C
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

		// Token: 0x06004A4A RID: 19018 RVA: 0x00120D9C File Offset: 0x0011EF9C
		private void CleanupCachedWindows(Hwnd hwnd)
		{
			if (XplatUIX11.ActiveWindow == hwnd.Handle)
			{
				this.SendMessage(hwnd.client_window, Msg.WM_ACTIVATE, (IntPtr)0, IntPtr.Zero);
				XplatUIX11.ActiveWindow = IntPtr.Zero;
			}
			if (XplatUIX11.FocusWindow == hwnd.Handle)
			{
				this.SendMessage(hwnd.client_window, Msg.WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
				XplatUIX11.FocusWindow = IntPtr.Zero;
			}
			if (XplatUIX11.Grab.Hwnd == hwnd.Handle)
			{
				XplatUIX11.Grab.Hwnd = IntPtr.Zero;
				XplatUIX11.Grab.Confined = false;
			}
			this.DestroyCaret(hwnd.Handle);
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00120E58 File Offset: 0x0011F058
		private void PerformNCCalc(Hwnd hwnd)
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
			rectangle = XplatUIX11.TranslateClientRectangleToXClientRectangle(hwnd);
			if (hwnd.visible)
			{
				XplatUIX11.MoveResizeWindow(XplatUIX11.DisplayHandle, hwnd.client_window, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			}
			this.AddExpose(hwnd, hwnd.WholeWindow == hwnd.ClientWindow, 0, 0, hwnd.Width, hwnd.Height);
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x00120FE0 File Offset: 0x0011F1E0
		private void MouseHover(object sender, EventArgs e)
		{
			XplatUIX11.HoverState.Timer.Enabled = false;
			if (XplatUIX11.HoverState.Window != IntPtr.Zero)
			{
				Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(XplatUIX11.HoverState.Window);
				if (objectFromWindow != null)
				{
					XEvent xevent = default(XEvent);
					xevent.type = XEventName.ClientMessage;
					xevent.ClientMessageEvent.display = XplatUIX11.DisplayHandle;
					xevent.ClientMessageEvent.window = XplatUIX11.HoverState.Window;
					xevent.ClientMessageEvent.message_type = XplatUIX11.HoverState.Atom;
					xevent.ClientMessageEvent.format = 32;
					xevent.ClientMessageEvent.ptr1 = (IntPtr)((XplatUIX11.HoverState.Y << 16) | XplatUIX11.HoverState.X);
					objectFromWindow.Queue.EnqueueLocked(xevent);
					this.WakeupMain();
				}
			}
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x001210C4 File Offset: 0x0011F2C4
		private void CaretCallback(object sender, EventArgs e)
		{
			if (XplatUIX11.Caret.Paused)
			{
				return;
			}
			XplatUIX11.Caret.On = !XplatUIX11.Caret.On;
			XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, XplatUIX11.Caret.Hwnd, XplatUIX11.Caret.gc, XplatUIX11.Caret.X, XplatUIX11.Caret.Y, XplatUIX11.Caret.X, XplatUIX11.Caret.Y + XplatUIX11.Caret.Height);
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x0012114C File Offset: 0x0011F34C
		internal override int CaptionHeight
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x00121150 File Offset: 0x0011F350
		internal override Size CursorSize
		{
			get
			{
				int num;
				int num2;
				if (XplatUIX11.XQueryBestCursor(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, 32, 32, out num, out num2) != 0)
				{
					return new Size(num, num2);
				}
				return new Size(16, 16);
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x0012118C File Offset: 0x0011F38C
		internal override bool DragFullWindows
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x00121190 File Offset: 0x0011F390
		internal override Size DragSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x0012119C File Offset: 0x0011F39C
		internal override Size FrameBorderSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x001211A8 File Offset: 0x0011F3A8
		internal override Size IconSize
		{
			get
			{
				IntPtr intPtr;
				int num;
				if (XplatUIX11.XGetIconSizes(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, out intPtr, out num) != 0)
				{
					long num2 = (long)intPtr;
					int num3 = 0;
					XIconSize xiconSize = default(XIconSize);
					for (int i = 0; i < num; i++)
					{
						xiconSize = (XIconSize)Marshal.PtrToStructure((IntPtr)num2, xiconSize.GetType());
						num2 += (long)Marshal.SizeOf(xiconSize);
						if (xiconSize.min_width == 32)
						{
							XplatUIX11.XFree(intPtr);
							return new Size(32, 32);
						}
						if (xiconSize.max_width == 32)
						{
							XplatUIX11.XFree(intPtr);
							return new Size(32, 32);
						}
						if (xiconSize.min_width < 32 && xiconSize.max_width > 32)
						{
							int j = xiconSize.min_width;
							while (j < xiconSize.max_width)
							{
								j += xiconSize.width_inc;
								if (j == 32)
								{
									XplatUIX11.XFree(intPtr);
									return new Size(32, 32);
								}
							}
						}
						if (num3 < xiconSize.max_width)
						{
							num3 = xiconSize.max_width;
						}
					}
					return new Size(num3, num3);
				}
				return new Size(32, 32);
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x001212E8 File Offset: 0x0011F4E8
		internal override int KeyboardSpeed
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x001212EC File Offset: 0x0011F4EC
		internal override int KeyboardDelay
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x001212F0 File Offset: 0x0011F4F0
		internal override Size MaxWindowTrackSize
		{
			get
			{
				return new Size(this.WorkingArea.Width, this.WorkingArea.Height);
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x00121320 File Offset: 0x0011F520
		internal override bool MenuAccessKeysUnderlined
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06004A58 RID: 19032 RVA: 0x00121324 File Offset: 0x0011F524
		internal override Size MinimizedWindowSpacingSize
		{
			get
			{
				return new Size(1, 1);
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06004A59 RID: 19033 RVA: 0x00121330 File Offset: 0x0011F530
		internal override Size MinimumWindowSize
		{
			get
			{
				return new Size(110, 22);
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06004A5A RID: 19034 RVA: 0x0012133C File Offset: 0x0011F53C
		internal override Size MinimumFixedToolWindowSize
		{
			get
			{
				return new Size(27, 22);
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x00121348 File Offset: 0x0011F548
		internal override Size MinimumSizeableToolWindowSize
		{
			get
			{
				return new Size(37, 22);
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x00121354 File Offset: 0x0011F554
		internal override Size MinimumNoBorderWindowSize
		{
			get
			{
				return new Size(2, 2);
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x00121360 File Offset: 0x0011F560
		internal override Keys ModifierKeys
		{
			get
			{
				return XplatUIX11.Keyboard.ModifierKeys;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x0012136C File Offset: 0x0011F56C
		internal override Size SmallIconSize
		{
			get
			{
				IntPtr intPtr;
				int num;
				if (XplatUIX11.XGetIconSizes(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, out intPtr, out num) != 0)
				{
					long num2 = (long)intPtr;
					int num3 = 0;
					XIconSize xiconSize = default(XIconSize);
					for (int i = 0; i < num; i++)
					{
						xiconSize = (XIconSize)Marshal.PtrToStructure((IntPtr)num2, xiconSize.GetType());
						num2 += (long)Marshal.SizeOf(xiconSize);
						if (xiconSize.min_width == 16)
						{
							XplatUIX11.XFree(intPtr);
							return new Size(16, 16);
						}
						if (xiconSize.max_width == 16)
						{
							XplatUIX11.XFree(intPtr);
							return new Size(16, 16);
						}
						if (xiconSize.min_width < 16 && xiconSize.max_width > 16)
						{
							int j = xiconSize.min_width;
							while (j < xiconSize.max_width)
							{
								j += xiconSize.width_inc;
								if (j == 16)
								{
									XplatUIX11.XFree(intPtr);
									return new Size(16, 16);
								}
							}
						}
						if (num3 == 0 || num3 > xiconSize.min_width)
						{
							num3 = xiconSize.min_width;
						}
					}
					return new Size(num3, num3);
				}
				return new Size(16, 16);
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x001214B4 File Offset: 0x0011F6B4
		internal override int MouseButtonCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x001214B8 File Offset: 0x0011F6B8
		internal override bool MouseButtonsSwapped
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x001214BC File Offset: 0x0011F6BC
		internal override Point MousePosition
		{
			get
			{
				return this.mouse_position;
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x001214C4 File Offset: 0x0011F6C4
		internal override Size MouseHoverSize
		{
			get
			{
				return new Size(1, 1);
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06004A63 RID: 19043 RVA: 0x001214D0 File Offset: 0x0011F6D0
		internal override int MouseHoverTime
		{
			get
			{
				return XplatUIX11.HoverState.Interval;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x001214DC File Offset: 0x0011F6DC
		internal override bool MouseWheelPresent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x001214E0 File Offset: 0x0011F6E0
		internal override MouseButtons MouseButtons
		{
			get
			{
				return XplatUIX11.MouseState;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004A66 RID: 19046 RVA: 0x001214E8 File Offset: 0x0011F6E8
		internal override Rectangle VirtualScreen
		{
			get
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr intPtr;
				int num;
				IntPtr intPtr2;
				IntPtr intPtr3;
				XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_DESKTOP_GEOMETRY, IntPtr.Zero, new IntPtr(256), false, (IntPtr)6, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
				if ((long)intPtr2 < 2L)
				{
					XWindowAttributes xwindowAttributes = default(XWindowAttributes);
					object xlibLock = XplatUIX11.XlibLock;
					lock (xlibLock)
					{
						XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, XplatUIX11.XRootWindow(XplatUIX11.DisplayHandle, 0), ref xwindowAttributes);
					}
					return new Rectangle(0, 0, xwindowAttributes.width, xwindowAttributes.height);
				}
				int num2 = Marshal.ReadIntPtr(zero, 0).ToInt32();
				int num3 = Marshal.ReadIntPtr(zero, IntPtr.Size).ToInt32();
				XplatUIX11.XFree(zero);
				return new Rectangle(0, 0, num2, num3);
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x001215F0 File Offset: 0x0011F7F0
		internal override Rectangle WorkingArea
		{
			get
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr intPtr;
				int num;
				IntPtr intPtr2;
				IntPtr intPtr3;
				XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_CURRENT_DESKTOP, IntPtr.Zero, new IntPtr(1), false, (IntPtr)6, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
				if ((long)intPtr2 >= 1L)
				{
					int num2 = Marshal.ReadIntPtr(zero, 0).ToInt32();
					XplatUIX11.XFree(zero);
					XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_WORKAREA, IntPtr.Zero, new IntPtr(256), false, (IntPtr)6, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
					if ((long)intPtr2 >= (long)(4 * num2))
					{
						int num3 = Marshal.ReadIntPtr(zero, IntPtr.Size * 4 * num2).ToInt32();
						int num4 = Marshal.ReadIntPtr(zero, IntPtr.Size * 4 * num2 + IntPtr.Size).ToInt32();
						int num5 = Marshal.ReadIntPtr(zero, IntPtr.Size * 4 * num2 + IntPtr.Size * 2).ToInt32();
						int num6 = Marshal.ReadIntPtr(zero, IntPtr.Size * 4 * num2 + IntPtr.Size * 3).ToInt32();
						XplatUIX11.XFree(zero);
						return new Rectangle(num3, num4, num5, num6);
					}
				}
				XWindowAttributes xwindowAttributes = default(XWindowAttributes);
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, XplatUIX11.XRootWindow(XplatUIX11.DisplayHandle, 0), ref xwindowAttributes);
				}
				return new Rectangle(0, 0, xwindowAttributes.width, xwindowAttributes.height);
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x001217B4 File Offset: 0x0011F9B4
		internal override bool ThemesEnabled
		{
			get
			{
				return XplatUIX11.themes_enabled;
			}
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x001217BC File Offset: 0x0011F9BC
		internal override void RaiseIdle(EventArgs e)
		{
			if (this.Idle != null)
			{
				this.Idle.Invoke(this, e);
			}
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x001217D8 File Offset: 0x0011F9D8
		internal override IntPtr InitializeDriver()
		{
			lock (this)
			{
				if (XplatUIX11.DisplayHandle == IntPtr.Zero)
				{
					this.SetDisplay(XplatUIX11.XOpenDisplay(IntPtr.Zero));
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x00121840 File Offset: 0x0011FA40
		internal override void ShutdownDriver(IntPtr token)
		{
			lock (this)
			{
				if (XplatUIX11.DisplayHandle != IntPtr.Zero)
				{
					XplatUIX11.XCloseDisplay(XplatUIX11.DisplayHandle);
					XplatUIX11.DisplayHandle = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x001218A8 File Offset: 0x0011FAA8
		internal override void EnableThemes()
		{
			XplatUIX11.themes_enabled = true;
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x001218B0 File Offset: 0x0011FAB0
		internal override void Activate(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_ACTIVE_WINDOW, (IntPtr)1, IntPtr.Zero, IntPtr.Zero);
					XEventQueue xeventQueue = null;
					ArrayList arrayList = XplatUIX11.unattached_timer_list;
					lock (arrayList)
					{
						foreach (object obj in XplatUIX11.unattached_timer_list)
						{
							Timer timer = (Timer)obj;
							if (xeventQueue == null)
							{
								xeventQueue = (XEventQueue)XplatUIX11.MessageQueues[Thread.CurrentThread];
							}
							timer.thread = xeventQueue.Thread;
							xeventQueue.timer_list.Add(timer);
						}
						XplatUIX11.unattached_timer_list.Clear();
					}
				}
			}
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x001219F0 File Offset: 0x0011FBF0
		internal override void AudibleAlert(AlertType alert)
		{
			XplatUIX11.XBell(XplatUIX11.DisplayHandle, 0);
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x00121A00 File Offset: 0x0011FC00
		internal override void CaretVisible(IntPtr handle, bool visible)
		{
			if (XplatUIX11.Caret.Hwnd == handle)
			{
				if (visible)
				{
					if (!XplatUIX11.Caret.Visible)
					{
						XplatUIX11.Caret.Visible = true;
						this.ShowCaret();
						XplatUIX11.Caret.Timer.Start();
					}
				}
				else
				{
					XplatUIX11.Caret.Visible = false;
					XplatUIX11.Caret.Timer.Stop();
					this.HideCaret();
				}
			}
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x00121A7C File Offset: 0x0011FC7C
		internal override bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect)
		{
			WindowRect = Hwnd.GetWindowRectangle(cp, menu, ClientRect);
			return true;
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x00121A94 File Offset: 0x0011FC94
		internal override void ClientToScreen(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, hwnd.client_window, XplatUIX11.RootWindow, x, y, out num, out num2, out intPtr);
			}
			x = num;
			y = num2;
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x00121B08 File Offset: 0x0011FD08
		internal override int[] ClipboardAvailableFormats(IntPtr handle)
		{
			DataFormats.Format format = DataFormats.Format.List;
			if (XplatUIX11.XGetSelectionOwner(XplatUIX11.DisplayHandle, XplatUIX11.CLIPBOARD) == IntPtr.Zero)
			{
				return null;
			}
			XplatUIX11.Clipboard.Formats = new ArrayList();
			while (format != null)
			{
				XplatUIX11.XConvertSelection(XplatUIX11.DisplayHandle, XplatUIX11.CLIPBOARD, (IntPtr)format.Id, (IntPtr)format.Id, XplatUIX11.FosterParent, IntPtr.Zero);
				XplatUIX11.Clipboard.Enumerating = true;
				while (XplatUIX11.Clipboard.Enumerating)
				{
					this.UpdateMessageQueue(null);
				}
				format = format.Next;
			}
			int[] array = new int[XplatUIX11.Clipboard.Formats.Count];
			for (int i = 0; i < XplatUIX11.Clipboard.Formats.Count; i++)
			{
				array[i] = ((IntPtr)XplatUIX11.Clipboard.Formats[i]).ToInt32();
			}
			XplatUIX11.Clipboard.Formats = null;
			return array;
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00121C14 File Offset: 0x0011FE14
		internal override void ClipboardClose(IntPtr handle)
		{
			if (handle != XplatUIX11.ClipMagic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x00121C34 File Offset: 0x0011FE34
		internal override int ClipboardGetID(IntPtr handle, string format)
		{
			if (handle != XplatUIX11.ClipMagic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			if (format == "Text")
			{
				return 31;
			}
			if (format == "Bitmap")
			{
				return 5;
			}
			if (format == "OEMText")
			{
				return XplatUIX11.OEMTEXT.ToInt32();
			}
			if (format == "DeviceIndependentBitmap")
			{
				return 20;
			}
			if (format == "Palette")
			{
				return 7;
			}
			if (format == "UnicodeText")
			{
				return XplatUIX11.UTF16_STRING.ToInt32();
			}
			if (format == "Rich Text Format")
			{
				return XplatUIX11.RICHTEXTFORMAT.ToInt32();
			}
			return XplatUIX11.XInternAtom(XplatUIX11.DisplayHandle, format, false).ToInt32();
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x00121D0C File Offset: 0x0011FF0C
		internal override IntPtr ClipboardOpen(bool primary_selection)
		{
			if (!primary_selection)
			{
				XplatUIX11.ClipMagic = XplatUIX11.CLIPBOARD;
			}
			else
			{
				XplatUIX11.ClipMagic = XplatUIX11.PRIMARY;
			}
			return XplatUIX11.ClipMagic;
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x00121D40 File Offset: 0x0011FF40
		internal override object ClipboardRetrieve(IntPtr handle, int type, XplatUI.ClipboardToObject converter)
		{
			XplatUIX11.XConvertSelection(XplatUIX11.DisplayHandle, handle, (IntPtr)type, (IntPtr)type, XplatUIX11.FosterParent, IntPtr.Zero);
			XplatUIX11.Clipboard.Retrieving = true;
			while (XplatUIX11.Clipboard.Retrieving)
			{
				this.UpdateMessageQueue(null);
			}
			return XplatUIX11.Clipboard.Item;
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x00121DA0 File Offset: 0x0011FFA0
		internal override void ClipboardStore(IntPtr handle, object obj, int type, XplatUI.ObjectToClipboard converter)
		{
			XplatUIX11.Clipboard.Converter = converter;
			if (obj != null)
			{
				XplatUIX11.Clipboard.AddSource(type, obj);
				XplatUIX11.XSetSelectionOwner(XplatUIX11.DisplayHandle, XplatUIX11.CLIPBOARD, XplatUIX11.FosterParent, IntPtr.Zero);
			}
			else
			{
				XplatUIX11.Clipboard.ClearSources();
				XplatUIX11.XSetSelectionOwner(XplatUIX11.DisplayHandle, XplatUIX11.CLIPBOARD, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x00121E10 File Offset: 0x00120010
		internal override void CreateCaret(IntPtr handle, int width, int height)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (XplatUIX11.Caret.Hwnd != IntPtr.Zero)
			{
				this.DestroyCaret(XplatUIX11.Caret.Hwnd);
			}
			XplatUIX11.Caret.Hwnd = handle;
			XplatUIX11.Caret.Window = hwnd.client_window;
			XplatUIX11.Caret.Width = width;
			XplatUIX11.Caret.Height = height;
			XplatUIX11.Caret.Visible = false;
			XplatUIX11.Caret.On = false;
			XGCValues xgcvalues = default(XGCValues);
			xgcvalues.line_width = width;
			XplatUIX11.Caret.gc = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, XplatUIX11.Caret.Window, new IntPtr(16), ref xgcvalues);
			if (XplatUIX11.Caret.gc == IntPtr.Zero)
			{
				XplatUIX11.Caret.Hwnd = IntPtr.Zero;
				return;
			}
			XplatUIX11.XSetFunction(XplatUIX11.DisplayHandle, XplatUIX11.Caret.gc, GXFunction.GXinvert);
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00121F0C File Offset: 0x0012010C
		internal override IntPtr CreateWindow(CreateParams cp)
		{
			Hwnd hwnd = null;
			Hwnd hwnd2 = new Hwnd();
			XSetWindowAttributes xsetWindowAttributes = default(XSetWindowAttributes);
			int num = cp.X;
			int num2 = cp.Y;
			int num3 = cp.Width;
			int num4 = cp.Height;
			if (num3 < 1)
			{
				num3 = 1;
			}
			if (num4 < 1)
			{
				num4 = 1;
			}
			IntPtr intPtr;
			if (cp.Parent != IntPtr.Zero)
			{
				hwnd = Hwnd.ObjectFromHandle(cp.Parent);
				intPtr = hwnd.client_window;
			}
			else if (this.StyleSet(cp.Style, WindowStyles.WS_CHILD))
			{
				intPtr = XplatUIX11.FosterParent;
			}
			else
			{
				intPtr = XplatUIX11.RootWindow;
			}
			if (cp.control is Form)
			{
				Point nextStackedFormLocation = Hwnd.GetNextStackedFormLocation(cp, hwnd);
				num = nextStackedFormLocation.X;
				num2 = nextStackedFormLocation.Y;
			}
			SetWindowValuemask setWindowValuemask = SetWindowValuemask.BitGravity | SetWindowValuemask.WinGravity;
			xsetWindowAttributes.bit_gravity = Gravity.NorthWestGravity;
			xsetWindowAttributes.win_gravity = Gravity.NorthWestGravity;
			if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
			{
				xsetWindowAttributes.save_under = true;
				setWindowValuemask |= SetWindowValuemask.SaveUnder;
			}
			if (this.StyleSet(cp.Style, WindowStyles.WS_POPUP) && !this.StyleSet(cp.Style, WindowStyles.WS_CAPTION))
			{
				xsetWindowAttributes.override_redirect = true;
				setWindowValuemask |= SetWindowValuemask.OverrideRedirect;
			}
			hwnd2.x = num;
			hwnd2.y = num2;
			hwnd2.width = num3;
			hwnd2.height = num4;
			hwnd2.parent = Hwnd.ObjectFromHandle(cp.Parent);
			hwnd2.initial_style = cp.WindowStyle;
			hwnd2.initial_ex_style = cp.WindowExStyle;
			if (this.StyleSet(cp.Style, WindowStyles.WS_DISABLED))
			{
				hwnd2.enabled = false;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			Size size = XplatUIX11.TranslateWindowSizeToXWindowSize(cp);
			Rectangle rectangle = XplatUIX11.TranslateClientRectangleToXClientRectangle(hwnd2, cp.control);
			object xlibLock = XplatUIX11.XlibLock;
			IntPtr intPtr3;
			lock (xlibLock)
			{
				intPtr3 = XplatUIX11.XCreateWindow(XplatUIX11.DisplayHandle, intPtr, num, num2, size.Width, size.Height, 0, 0, 1, IntPtr.Zero, new UIntPtr((uint)setWindowValuemask), ref xsetWindowAttributes);
				if (intPtr3 != IntPtr.Zero)
				{
					setWindowValuemask &= ~(SetWindowValuemask.OverrideRedirect | SetWindowValuemask.SaveUnder);
					if (XplatUIX11.CustomVisual != IntPtr.Zero && XplatUIX11.CustomColormap != IntPtr.Zero)
					{
						setWindowValuemask = SetWindowValuemask.ColorMap;
						xsetWindowAttributes.colormap = XplatUIX11.CustomColormap;
					}
					intPtr2 = XplatUIX11.XCreateWindow(XplatUIX11.DisplayHandle, intPtr3, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 0, 0, 1, XplatUIX11.CustomVisual, new UIntPtr((uint)setWindowValuemask), ref xsetWindowAttributes);
				}
			}
			if (intPtr3 == IntPtr.Zero || intPtr2 == IntPtr.Zero)
			{
				throw new Exception("Could not create X11 windows");
			}
			hwnd2.Queue = this.ThreadQueue(Thread.CurrentThread);
			hwnd2.WholeWindow = intPtr3;
			hwnd2.ClientWindow = intPtr2;
			if (!this.StyleSet(cp.Style, WindowStyles.WS_CHILD) && num != -2147483648 && num2 != -2147483648)
			{
				XSizeHints xsizeHints = default(XSizeHints);
				xsizeHints.x = num;
				xsizeHints.y = num2;
				xsizeHints.flags = (IntPtr)5;
				XplatUIX11.XSetWMNormalHints(XplatUIX11.DisplayHandle, intPtr3, ref xsizeHints);
			}
			object xlibLock2 = XplatUIX11.XlibLock;
			lock (xlibLock2)
			{
				XplatUIX11.XSelectInput(XplatUIX11.DisplayHandle, hwnd2.whole_window, new IntPtr((int)(EventMask.KeyPressMask | EventMask.KeyReleaseMask | EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.EnterWindowMask | EventMask.LeaveWindowMask | EventMask.PointerMotionMask | EventMask.PointerMotionHintMask | EventMask.ExposureMask | EventMask.StructureNotifyMask | EventMask.SubstructureNotifyMask | EventMask.FocusChangeMask | EventMask.PropertyChangeMask | XplatUIX11.Keyboard.KeyEventMask)));
				if (hwnd2.whole_window != hwnd2.client_window)
				{
					XplatUIX11.XSelectInput(XplatUIX11.DisplayHandle, hwnd2.client_window, new IntPtr((int)(EventMask.KeyPressMask | EventMask.KeyReleaseMask | EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.EnterWindowMask | EventMask.LeaveWindowMask | EventMask.PointerMotionMask | EventMask.PointerMotionHintMask | EventMask.ExposureMask | EventMask.StructureNotifyMask | EventMask.SubstructureNotifyMask | EventMask.FocusChangeMask | XplatUIX11.Keyboard.KeyEventMask)));
				}
			}
			if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_TOPMOST))
			{
				int[] array = new int[2];
				array[0] = XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL.ToInt32();
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd2.whole_window, XplatUIX11._NET_WM_WINDOW_TYPE, (IntPtr)4, 32, PropertyMode.Replace, array, 1);
				XplatUIX11.XSetTransientForHint(XplatUIX11.DisplayHandle, hwnd2.whole_window, XplatUIX11.RootWindow);
			}
			this.SetWMStyles(hwnd2, cp);
			XWMHints xwmhints = default(XWMHints);
			xwmhints.flags = (IntPtr)67;
			xwmhints.input = !this.StyleSet(cp.Style, WindowStyles.WS_DISABLED);
			xwmhints.initial_state = ((!this.StyleSet(cp.Style, WindowStyles.WS_MINIMIZE)) ? XInitialState.NormalState : XInitialState.IconicState);
			if (intPtr != XplatUIX11.RootWindow)
			{
				xwmhints.window_group = hwnd2.whole_window;
			}
			else
			{
				xwmhints.window_group = intPtr;
			}
			object xlibLock3 = XplatUIX11.XlibLock;
			lock (xlibLock3)
			{
				XplatUIX11.XSetWMHints(XplatUIX11.DisplayHandle, hwnd2.whole_window, ref xwmhints);
			}
			if (this.StyleSet(cp.Style, WindowStyles.WS_MINIMIZE))
			{
				this.SetWindowState(hwnd2.Handle, FormWindowState.Minimized);
			}
			else if (this.StyleSet(cp.Style, WindowStyles.WS_MAXIMIZE))
			{
				this.SetWindowState(hwnd2.Handle, FormWindowState.Maximized);
			}
			XplatUIX11.Dnd.SetAllowDrop(hwnd2, true);
			this.Text(hwnd2.Handle, cp.Caption);
			this.SendMessage(hwnd2.Handle, Msg.WM_CREATE, (IntPtr)1, IntPtr.Zero);
			this.SendParentNotify(hwnd2.Handle, Msg.WM_CREATE, int.MaxValue, int.MaxValue);
			if (this.StyleSet(cp.Style, WindowStyles.WS_VISIBLE))
			{
				hwnd2.visible = true;
				this.MapWindow(hwnd2, WindowType.Both);
				if (!(Control.FromHandle(hwnd2.Handle) is Form))
				{
					this.SendMessage(hwnd2.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
				}
			}
			return hwnd2.Handle;
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x0012253C File Offset: 0x0012073C
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

		// Token: 0x06004A7B RID: 19067 RVA: 0x001225B0 File Offset: 0x001207B0
		internal override IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			int width;
			int height;
			if (XplatUIX11.XQueryBestCursor(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, bitmap.Width, bitmap.Height, out width, out height) == 0)
			{
				return IntPtr.Zero;
			}
			Bitmap bitmap2;
			Bitmap bitmap3;
			if (bitmap.Width != width || bitmap.Width != height)
			{
				bitmap2 = new Bitmap(bitmap, new Size(width, height));
				bitmap3 = new Bitmap(mask, new Size(width, height));
			}
			else
			{
				bitmap2 = bitmap;
				bitmap3 = mask;
			}
			width = bitmap2.Width;
			height = bitmap2.Height;
			byte[] array = new byte[width / 8 * height];
			byte[] array2 = new byte[width / 8 * height];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color pixel = bitmap2.GetPixel(j, i);
					Color pixel2 = bitmap3.GetPixel(j, i);
					bool flag = pixel == cursor_pixel;
					bool flag2 = pixel2 == mask_pixel;
					if (!flag && !flag2)
					{
						byte[] array3 = array2;
						int num = i * width / 8 + j / 8;
						array3[num] |= (byte)(1 << j % 8);
					}
					else if (flag && !flag2)
					{
						byte[] array4 = array;
						int num2 = i * width / 8 + j / 8;
						array4[num2] |= (byte)(1 << j % 8);
						byte[] array5 = array2;
						int num3 = i * width / 8 + j / 8;
						array5[num3] |= (byte)(1 << j % 8);
					}
				}
			}
			IntPtr intPtr = XplatUIX11.XCreatePixmapFromBitmapData(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, array, width, height, (IntPtr)1, (IntPtr)0, 1);
			IntPtr intPtr2 = XplatUIX11.XCreatePixmapFromBitmapData(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, array2, width, height, (IntPtr)1, (IntPtr)0, 1);
			XColor xcolor = default(XColor);
			XColor xcolor2 = default(XColor);
			xcolor.pixel = XplatUIX11.XWhitePixel(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
			xcolor.red = ushort.MaxValue;
			xcolor.green = ushort.MaxValue;
			xcolor.blue = ushort.MaxValue;
			xcolor2.pixel = XplatUIX11.XBlackPixel(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
			IntPtr intPtr3 = XplatUIX11.XCreatePixmapCursor(XplatUIX11.DisplayHandle, intPtr, intPtr2, ref xcolor, ref xcolor2, xHotSpot, yHotSpot);
			XplatUIX11.XFreePixmap(XplatUIX11.DisplayHandle, intPtr);
			XplatUIX11.XFreePixmap(XplatUIX11.DisplayHandle, intPtr2);
			return intPtr3;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x00122810 File Offset: 0x00120A10
		internal override Bitmap DefineStdCursorBitmap(StdCursor id)
		{
			Bitmap bitmap = null;
			try
			{
				CursorFontShape cursorFontShape = XplatUIX11.StdCursorToFontShape(id);
				string text = cursorFontShape.ToString().Replace("XC_", string.Empty);
				int num = XplatUIX11.XcursorGetDefaultSize(XplatUIX11.DisplayHandle);
				IntPtr intPtr = XplatUIX11.XcursorGetTheme(XplatUIX11.DisplayHandle);
				IntPtr intPtr2 = XplatUIX11.XcursorLibraryLoadImages(text, intPtr, num);
				if (intPtr2 == IntPtr.Zero)
				{
					return null;
				}
				XcursorImages xcursorImages = (XcursorImages)Marshal.PtrToStructure(intPtr2, typeof(XcursorImages));
				if (xcursorImages.nimage > 0)
				{
					XcursorImage xcursorImage = (XcursorImage)Marshal.PtrToStructure(Marshal.ReadIntPtr(xcursorImages.images), typeof(XcursorImage));
					if (xcursorImage.width <= 32767 && xcursorImage.height <= 32767)
					{
						int[] array = new int[xcursorImage.width * xcursorImage.height];
						Marshal.Copy(xcursorImage.pixels, array, 0, array.Length);
						bitmap = new Bitmap(xcursorImage.width, xcursorImage.height);
						for (int i = 0; i < xcursorImage.width; i++)
						{
							for (int j = 0; j < xcursorImage.height; j++)
							{
								bitmap.SetPixel(i, j, Color.FromArgb(array[j * xcursorImage.width + i]));
							}
						}
					}
				}
				XplatUIX11.XcursorImagesDestroy(intPtr2);
			}
			catch (DllNotFoundException ex)
			{
				Console.WriteLine(string.Concat(new string[]
				{
					"Could not load libXcursor: ",
					ex.Message,
					" (",
					ex.GetType().Name,
					")"
				}));
				return null;
			}
			return bitmap;
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x001229F8 File Offset: 0x00120BF8
		internal override IntPtr DefineStdCursor(StdCursor id)
		{
			CursorFontShape cursorFontShape = XplatUIX11.StdCursorToFontShape(id);
			object xlibLock = XplatUIX11.XlibLock;
			IntPtr intPtr;
			lock (xlibLock)
			{
				intPtr = XplatUIX11.XCreateFontCursor(XplatUIX11.DisplayHandle, cursorFontShape);
			}
			return intPtr;
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00122A50 File Offset: 0x00120C50
		internal static CursorFontShape StdCursorToFontShape(StdCursor id)
		{
			CursorFontShape cursorFontShape;
			switch (id)
			{
			case StdCursor.Default:
				cursorFontShape = CursorFontShape.XC_top_left_arrow;
				break;
			case StdCursor.AppStarting:
				cursorFontShape = CursorFontShape.XC_watch;
				break;
			case StdCursor.Arrow:
				cursorFontShape = CursorFontShape.XC_top_left_arrow;
				break;
			case StdCursor.Cross:
				cursorFontShape = CursorFontShape.XC_crosshair;
				break;
			case StdCursor.Hand:
				cursorFontShape = CursorFontShape.XC_hand1;
				break;
			case StdCursor.Help:
				cursorFontShape = CursorFontShape.XC_question_arrow;
				break;
			case StdCursor.HSplit:
				cursorFontShape = CursorFontShape.XC_sb_v_double_arrow;
				break;
			case StdCursor.IBeam:
				cursorFontShape = CursorFontShape.XC_xterm;
				break;
			case StdCursor.No:
				cursorFontShape = CursorFontShape.XC_circle;
				break;
			case StdCursor.NoMove2D:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.NoMoveHoriz:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.NoMoveVert:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanEast:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanNE:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanNorth:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanNW:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanSE:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanSouth:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanSW:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanWest:
				cursorFontShape = CursorFontShape.XC_sizing;
				break;
			case StdCursor.SizeAll:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.SizeNESW:
				cursorFontShape = CursorFontShape.XC_top_right_corner;
				break;
			case StdCursor.SizeNS:
				cursorFontShape = CursorFontShape.XC_sb_v_double_arrow;
				break;
			case StdCursor.SizeNWSE:
				cursorFontShape = CursorFontShape.XC_top_left_corner;
				break;
			case StdCursor.SizeWE:
				cursorFontShape = CursorFontShape.XC_sb_h_double_arrow;
				break;
			case StdCursor.UpArrow:
				cursorFontShape = CursorFontShape.XC_center_ptr;
				break;
			case StdCursor.VSplit:
				cursorFontShape = CursorFontShape.XC_sb_h_double_arrow;
				break;
			case StdCursor.WaitCursor:
				cursorFontShape = CursorFontShape.XC_watch;
				break;
			default:
				cursorFontShape = CursorFontShape.XC_X_cursor;
				break;
			}
			return cursorFontShape;
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x00122BD8 File Offset: 0x00120DD8
		internal override IntPtr DefWndProc(ref Message msg)
		{
			Msg msg2 = (Msg)msg.Msg;
			switch (msg2)
			{
			case Msg.WM_NCCALCSIZE:
				if (msg.WParam == (IntPtr)1)
				{
					Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(msg.HWnd);
					XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(msg.LParam, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
					Control control = Control.FromHandle(objectFromWindow.Handle);
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
				return IntPtr.Zero;
			default:
			{
				if (msg2 != Msg.WM_PAINT)
				{
					if (msg2 != Msg.WM_SETCURSOR)
					{
						if (msg2 == Msg.WM_CONTEXTMENU)
						{
							Hwnd objectFromWindow2 = Hwnd.GetObjectFromWindow(msg.HWnd);
							if (objectFromWindow2 != null && objectFromWindow2.parent != null)
							{
								this.SendMessage(objectFromWindow2.parent.client_window, Msg.WM_CONTEXTMENU, msg.WParam, msg.LParam);
							}
							return IntPtr.Zero;
						}
						if (msg2 == Msg.WM_IME_COMPOSITION)
						{
							string compositionString = XplatUIX11.Keyboard.GetCompositionString();
							string text = compositionString;
							for (int i = 0; i < text.Length; i++)
							{
								char c = text.get_Chars(i);
								this.SendMessage(msg.HWnd, Msg.WM_IME_CHAR, (IntPtr)((int)c), msg.LParam);
							}
							return IntPtr.Zero;
						}
						if (msg2 == Msg.WM_MOUSEWHEEL)
						{
							Hwnd objectFromWindow3 = Hwnd.GetObjectFromWindow(msg.HWnd);
							if (objectFromWindow3 != null && objectFromWindow3.parent != null)
							{
								this.SendMessage(objectFromWindow3.parent.client_window, Msg.WM_MOUSEWHEEL, msg.WParam, msg.LParam);
								if (msg.Result == IntPtr.Zero)
								{
									return IntPtr.Zero;
								}
							}
							return IntPtr.Zero;
						}
						if (msg2 == Msg.WM_IME_CHAR)
						{
							this.SendMessage(msg.HWnd, Msg.WM_CHAR, msg.WParam, msg.LParam);
							return IntPtr.Zero;
						}
					}
					else
					{
						Hwnd hwnd = Hwnd.GetObjectFromWindow(msg.HWnd);
						if (hwnd != null)
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
											this.AudibleAlert(AlertType.Default);
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
					}
					return IntPtr.Zero;
				}
				Hwnd objectFromWindow4 = Hwnd.GetObjectFromWindow(msg.HWnd);
				if (objectFromWindow4 != null)
				{
					objectFromWindow4.expose_pending = false;
				}
				return IntPtr.Zero;
			}
			case Msg.WM_NCPAINT:
			{
				Hwnd objectFromWindow5 = Hwnd.GetObjectFromWindow(msg.HWnd);
				if (objectFromWindow5 != null)
				{
					objectFromWindow5.nc_expose_pending = false;
				}
				return IntPtr.Zero;
			}
			}
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00123078 File Offset: 0x00121278
		internal override void DestroyCaret(IntPtr handle)
		{
			if (XplatUIX11.Caret.Hwnd == handle)
			{
				if (XplatUIX11.Caret.Visible)
				{
					this.HideCaret();
					XplatUIX11.Caret.Timer.Stop();
				}
				if (XplatUIX11.Caret.gc != IntPtr.Zero)
				{
					XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, XplatUIX11.Caret.gc);
					XplatUIX11.Caret.gc = IntPtr.Zero;
				}
				XplatUIX11.Caret.Hwnd = IntPtr.Zero;
				XplatUIX11.Caret.Visible = false;
				XplatUIX11.Caret.On = false;
			}
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00123120 File Offset: 0x00121320
		internal override void DestroyCursor(IntPtr cursor)
		{
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XFreeCursor(XplatUIX11.DisplayHandle, cursor);
			}
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x00123170 File Offset: 0x00121370
		internal override void DestroyWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null || hwnd.zombie)
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
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				if (hwnd.whole_window != IntPtr.Zero)
				{
					XplatUIX11.Keyboard.DestroyICForWindow(hwnd.whole_window);
					XplatUIX11.XDestroyWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				else if (hwnd.client_window != IntPtr.Zero)
				{
					XplatUIX11.Keyboard.DestroyICForWindow(hwnd.client_window);
					XplatUIX11.XDestroyWindow(XplatUIX11.DisplayHandle, hwnd.client_window);
				}
			}
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x001232EC File Offset: 0x001214EC
		internal override IntPtr DispatchMessage(ref MSG msg)
		{
			return NativeWindow.WndProc(msg.hwnd, msg.message, msg.wParam, msg.lParam);
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x0012330C File Offset: 0x0012150C
		private IntPtr GetReversibleScreenGC(Color backColor)
		{
			XColor xcolor = default(XColor);
			xcolor.red = (ushort)((int)backColor.R * 257);
			xcolor.green = (ushort)((int)backColor.G * 257);
			xcolor.blue = (ushort)((int)backColor.B * 257);
			XplatUIX11.XAllocColor(XplatUIX11.DisplayHandle, XplatUIX11.DefaultColormap, ref xcolor);
			uint num = (uint)xcolor.pixel.ToInt32();
			XGCValues xgcvalues = default(XGCValues);
			xgcvalues.subwindow_mode = GCSubwindowMode.IncludeInferiors;
			xgcvalues.foreground = (IntPtr)((long)((ulong)num));
			IntPtr intPtr = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, new IntPtr(32772), ref xgcvalues);
			XplatUIX11.XSetForeground(XplatUIX11.DisplayHandle, intPtr, (UIntPtr)num);
			XplatUIX11.XSetFunction(XplatUIX11.DisplayHandle, intPtr, GXFunction.GXxor);
			return intPtr;
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x001233DC File Offset: 0x001215DC
		private IntPtr GetReversibleControlGC(Control control, int line_width)
		{
			XGCValues xgcvalues = default(XGCValues);
			xgcvalues.subwindow_mode = GCSubwindowMode.IncludeInferiors;
			xgcvalues.line_width = line_width;
			xgcvalues.foreground = XplatUIX11.XBlackPixel(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
			IntPtr intPtr = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, control.Handle, new IntPtr(32788), ref xgcvalues);
			XColor xcolor = default(XColor);
			xcolor.red = (ushort)((int)control.ForeColor.R * 257);
			xcolor.green = (ushort)((int)control.ForeColor.G * 257);
			xcolor.blue = (ushort)((int)control.ForeColor.B * 257);
			XplatUIX11.XAllocColor(XplatUIX11.DisplayHandle, XplatUIX11.DefaultColormap, ref xcolor);
			uint num = (uint)xcolor.pixel.ToInt32();
			xcolor.red = (ushort)((int)control.BackColor.R * 257);
			xcolor.green = (ushort)((int)control.BackColor.G * 257);
			xcolor.blue = (ushort)((int)control.BackColor.B * 257);
			XplatUIX11.XAllocColor(XplatUIX11.DisplayHandle, XplatUIX11.DefaultColormap, ref xcolor);
			uint num2 = (uint)xcolor.pixel.ToInt32();
			uint num3 = num ^ num2;
			XplatUIX11.XSetForeground(XplatUIX11.DisplayHandle, intPtr, (UIntPtr)uint.MaxValue);
			XplatUIX11.XSetBackground(XplatUIX11.DisplayHandle, intPtr, (UIntPtr)num2);
			XplatUIX11.XSetFunction(XplatUIX11.DisplayHandle, intPtr, GXFunction.GXxor);
			XplatUIX11.XSetPlaneMask(XplatUIX11.DisplayHandle, intPtr, (IntPtr)((long)((ulong)num3)));
			return intPtr;
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x00123574 File Offset: 0x00121774
		internal override void DrawReversibleLine(Point start, Point end, Color backColor)
		{
			if ((double)backColor.GetBrightness() < 0.5)
			{
				backColor = Color.FromArgb((int)(byte.MaxValue - backColor.R), (int)(byte.MaxValue - backColor.G), (int)(byte.MaxValue - backColor.B));
			}
			IntPtr reversibleScreenGC = this.GetReversibleScreenGC(backColor);
			XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, reversibleScreenGC, start.X, start.Y, end.X, end.Y);
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, reversibleScreenGC);
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00123608 File Offset: 0x00121808
		internal override void DrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style)
		{
			if ((double)backColor.GetBrightness() < 0.5)
			{
				backColor = Color.FromArgb((int)(byte.MaxValue - backColor.R), (int)(byte.MaxValue - backColor.G), (int)(byte.MaxValue - backColor.B));
			}
			IntPtr reversibleScreenGC = this.GetReversibleScreenGC(backColor);
			if (rectangle.Width < 0)
			{
				rectangle.X += rectangle.Width;
				rectangle.Width = -rectangle.Width;
			}
			if (rectangle.Height < 0)
			{
				rectangle.Y += rectangle.Height;
				rectangle.Height = -rectangle.Height;
			}
			int num = 1;
			GCLineStyle gclineStyle = GCLineStyle.LineSolid;
			GCCapStyle gccapStyle = GCCapStyle.CapButt;
			GCJoinStyle gcjoinStyle = GCJoinStyle.JoinMiter;
			if (style != FrameStyle.Dashed)
			{
				if (style == FrameStyle.Thick)
				{
					num = 2;
				}
			}
			else
			{
				gclineStyle = GCLineStyle.LineOnOffDash;
			}
			XplatUIX11.XSetLineAttributes(XplatUIX11.DisplayHandle, reversibleScreenGC, num, gclineStyle, gccapStyle, gcjoinStyle);
			XplatUIX11.XDrawRectangle(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, reversibleScreenGC, rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, reversibleScreenGC);
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x0012373C File Offset: 0x0012193C
		internal override void FillReversibleRectangle(Rectangle rectangle, Color backColor)
		{
			if ((double)backColor.GetBrightness() < 0.5)
			{
				backColor = Color.FromArgb((int)(byte.MaxValue - backColor.R), (int)(byte.MaxValue - backColor.G), (int)(byte.MaxValue - backColor.B));
			}
			IntPtr reversibleScreenGC = this.GetReversibleScreenGC(backColor);
			if (rectangle.Width < 0)
			{
				rectangle.X += rectangle.Width;
				rectangle.Width = -rectangle.Width;
			}
			if (rectangle.Height < 0)
			{
				rectangle.Y += rectangle.Height;
				rectangle.Height = -rectangle.Height;
			}
			XplatUIX11.XFillRectangle(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, reversibleScreenGC, rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, reversibleScreenGC);
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x00123830 File Offset: 0x00121A30
		internal override void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width)
		{
			Control control = Control.FromHandle(handle);
			IntPtr reversibleControlGC = this.GetReversibleControlGC(control, line_width);
			if (rect.Width > 0 && rect.Height > 0)
			{
				XplatUIX11.XDrawRectangle(XplatUIX11.DisplayHandle, control.Handle, reversibleControlGC, rect.Left, rect.Top, rect.Width, rect.Height);
			}
			else if (rect.Width > 0)
			{
				XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, control.Handle, reversibleControlGC, rect.X, rect.Y, rect.Right, rect.Y);
			}
			else
			{
				XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, control.Handle, reversibleControlGC, rect.X, rect.Y, rect.X, rect.Bottom);
			}
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, reversibleControlGC);
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x00123914 File Offset: 0x00121B14
		internal override void DoEvents()
		{
			MSG msg = default(MSG);
			if (XplatUIX11.OverrideCursorHandle != IntPtr.Zero)
			{
				XplatUIX11.OverrideCursorHandle = IntPtr.Zero;
			}
			XEventQueue xeventQueue = this.ThreadQueue(Thread.CurrentThread);
			xeventQueue.DispatchIdle = false;
			XplatUIX11.in_doevents = true;
			while (this.PeekMessage(xeventQueue, ref msg, IntPtr.Zero, 0, 0, 1U))
			{
				Message message = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
				if (!Application.FilterMessage(ref message))
				{
					this.TranslateMessage(ref msg);
					this.DispatchMessage(ref msg);
					string text = msg.hwnd + ":" + msg.message;
					if (XplatUIX11.messageHold[text] != null)
					{
						XplatUIX11.messageHold[text] = (int)XplatUIX11.messageHold[text] - 1;
					}
				}
			}
			XplatUIX11.in_doevents = false;
			xeventQueue.DispatchIdle = true;
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x00123A24 File Offset: 0x00121C24
		internal override void EnableWindow(IntPtr handle, bool Enable)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				hwnd.Enabled = Enable;
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x00123A48 File Offset: 0x00121C48
		internal override void EndLoop(Thread thread)
		{
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x00123A4C File Offset: 0x00121C4C
		internal override IntPtr GetActive()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2;
			int num;
			IntPtr intPtr3;
			IntPtr intPtr4;
			XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_ACTIVE_WINDOW, IntPtr.Zero, new IntPtr(1), false, (IntPtr)33, out intPtr2, out num, out intPtr3, out intPtr4, ref zero);
			if ((long)intPtr3 > 0L && zero != IntPtr.Zero)
			{
				intPtr = (IntPtr)Marshal.ReadInt32(zero);
				XplatUIX11.XFree(zero);
			}
			if (intPtr != IntPtr.Zero)
			{
				Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(intPtr);
				if (objectFromWindow != null)
				{
					intPtr = objectFromWindow.Handle;
				}
				else
				{
					intPtr = IntPtr.Zero;
				}
			}
			return intPtr;
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x00123B08 File Offset: 0x00121D08
		internal override Region GetClipRegion(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				return hwnd.UserClip;
			}
			return null;
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x00123B2C File Offset: 0x00121D2C
		internal override void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y)
		{
			width = 20;
			height = 20;
			hotspot_x = 0;
			hotspot_y = 0;
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x00123B40 File Offset: 0x00121D40
		internal override void GetDisplaySize(out Size size)
		{
			XWindowAttributes xwindowAttributes = default(XWindowAttributes);
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, XplatUIX11.XRootWindow(XplatUIX11.DisplayHandle, 0), ref xwindowAttributes);
			}
			size..ctor(xwindowAttributes.width, xwindowAttributes.height);
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x00123BB8 File Offset: 0x00121DB8
		internal override SizeF GetAutoScaleSize(Font font)
		{
			string text = "The quick brown fox jumped over the lazy dog.";
			double num = 44.54999694824219;
			Graphics graphics = Graphics.FromHwnd(XplatUIX11.FosterParent);
			float num2 = (float)((double)graphics.MeasureString(text, font).Width / num);
			return new SizeF(num2, (float)font.Height);
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x00123C04 File Offset: 0x00121E04
		internal override IntPtr GetParent(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null && hwnd.parent != null)
			{
				return hwnd.parent.Handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x00123C3C File Offset: 0x00121E3C
		internal override IntPtr GetPreviousWindow(IntPtr handle)
		{
			return handle;
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x00123C40 File Offset: 0x00121E40
		internal override void GetCursorPos(IntPtr handle, out int x, out int y)
		{
			IntPtr intPtr;
			if (handle != IntPtr.Zero)
			{
				intPtr = Hwnd.ObjectFromHandle(handle).client_window;
			}
			else
			{
				intPtr = XplatUIX11.RootWindow;
			}
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			int num3;
			int num4;
			lock (xlibLock)
			{
				IntPtr intPtr2;
				IntPtr intPtr3;
				int num5;
				this.QueryPointer(XplatUIX11.DisplayHandle, intPtr, out intPtr2, out intPtr3, out num, out num2, out num3, out num4, out num5);
			}
			if (handle != IntPtr.Zero)
			{
				x = num3;
				y = num4;
			}
			else
			{
				x = num;
				y = num2;
			}
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x00123CEC File Offset: 0x00121EEC
		internal override IntPtr GetFocus()
		{
			return XplatUIX11.FocusWindow;
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x00123CF4 File Offset: 0x00121EF4
		internal override bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent)
		{
			FontFamily fontFamily = font.FontFamily;
			ascent = fontFamily.GetCellAscent(font.Style);
			descent = fontFamily.GetCellDescent(font.Style);
			return true;
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x00123D28 File Offset: 0x00121F28
		internal override Point GetMenuOrigin(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				return hwnd.MenuOrigin;
			}
			return Point.Empty;
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00123D50 File Offset: 0x00121F50
		[MonoTODO("Implement filtering")]
		internal override bool GetMessage(object queue_id, ref MSG msg, IntPtr handle, int wFilterMin, int wFilterMax)
		{
			XEvent xevent;
			Hwnd hwnd;
			bool flag;
			for (;;)
			{
				IL_0000:
				if (((XEventQueue)queue_id).Count > 0)
				{
					xevent = ((XEventQueue)queue_id).Dequeue();
				}
				else
				{
					this.UpdateMessageQueue((XEventQueue)queue_id);
					if (((XEventQueue)queue_id).Count > 0)
					{
						xevent = ((XEventQueue)queue_id).Dequeue();
					}
					else
					{
						if (((XEventQueue)queue_id).Paint.Count <= 0)
						{
							break;
						}
						xevent = ((XEventQueue)queue_id).Paint.Dequeue();
					}
				}
				hwnd = Hwnd.GetObjectFromWindow(xevent.AnyEvent.window);
				if (hwnd != null && hwnd.zombie && xevent.type == XEventName.Expose)
				{
					hwnd.expose_pending = (hwnd.nc_expose_pending = false);
					hwnd.Queue.Paint.Remove(hwnd);
				}
				else if (hwnd != null && (!hwnd.zombie || xevent.AnyEvent.type == XEventName.ClientMessage))
				{
					if (hwnd.zombie)
					{
						hwnd.resizing_or_moving = false;
					}
					flag = hwnd.client_window == xevent.AnyEvent.window;
					msg.hwnd = hwnd.Handle;
					if (hwnd.resizing_or_moving)
					{
						IntPtr intPtr;
						IntPtr intPtr2;
						int num;
						int num2;
						int num3;
						int num4;
						int num5;
						XplatUIX11.XQueryPointer(XplatUIX11.DisplayHandle, hwnd.Handle, out intPtr, out intPtr2, out num, out num2, out num3, out num4, out num5);
						if ((num5 & 256) == 0 && (num5 & 512) == 0 && (num5 & 1024) == 0)
						{
							hwnd.resizing_or_moving = false;
							this.SendMessage(hwnd.Handle, Msg.WM_EXITSIZEMOVE, IntPtr.Zero, IntPtr.Zero);
						}
					}
					switch (xevent.type)
					{
					case XEventName.KeyPress:
						goto IL_0264;
					case XEventName.KeyRelease:
						goto IL_0310;
					case XEventName.ButtonPress:
						goto IL_0326;
					case XEventName.ButtonRelease:
						switch (xevent.ButtonEvent.button)
						{
						case 1:
							goto IL_088A;
						case 2:
							goto IL_0921;
						case 3:
							goto IL_09B8;
						case 4:
							continue;
						case 5:
							continue;
						}
						goto Block_33;
					case XEventName.MotionNotify:
						goto IL_0C17;
					case XEventName.EnterNotify:
						if (hwnd.Enabled)
						{
							if (xevent.CrossingEvent.mode != NotifyMode.NotifyGrab && !(xevent.AnyEvent.window != hwnd.client_window))
							{
								if (xevent.CrossingEvent.mode != NotifyMode.NotifyUngrab)
								{
									goto IL_118D;
								}
								if (!(XplatUIX11.LastPointerWindow == xevent.AnyEvent.window))
								{
									if (XplatUIX11.LastPointerWindow != IntPtr.Zero)
									{
										Point point;
										point..ctor(xevent.ButtonEvent.x, xevent.ButtonEvent.y);
										Control control = Control.FromHandle(hwnd.client_window);
										foreach (Control control2 in control.Controls.GetAllControls())
										{
											if (control2.Bounds.Contains(point))
											{
												goto IL_0000;
											}
										}
										goto Block_57;
									}
									goto IL_118D;
								}
							}
						}
						break;
					case XEventName.LeaveNotify:
						if (xevent.CrossingEvent.mode == NotifyMode.NotifyUngrab)
						{
							this.WindowUngrabbed(hwnd.Handle);
						}
						else if (hwnd.Enabled)
						{
							if (xevent.CrossingEvent.mode == NotifyMode.NotifyNormal && !(xevent.CrossingEvent.window != hwnd.client_window))
							{
								if (!(XplatUIX11.Grab.Hwnd != IntPtr.Zero))
								{
									goto IL_12F9;
								}
							}
						}
						break;
					case XEventName.FocusIn:
						if (xevent.FocusChangeEvent.detail == NotifyDetail.NotifyNonlinear)
						{
							if (XplatUIX11.FocusWindow == IntPtr.Zero)
							{
								Control control3 = Control.FromHandle(hwnd.client_window);
								if (control3 != null)
								{
									Form form = control3.FindForm();
									if (form != null)
									{
										if (XplatUIX11.ActiveWindow != form.Handle)
										{
											XplatUIX11.ActiveWindow = form.Handle;
											this.SendMessage(XplatUIX11.ActiveWindow, Msg.WM_ACTIVATE, (IntPtr)1, IntPtr.Zero);
										}
									}
								}
							}
							else
							{
								this.SendMessage(XplatUIX11.FocusWindow, Msg.WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
								XplatUIX11.Keyboard.FocusIn(XplatUIX11.FocusWindow);
							}
						}
						break;
					case XEventName.FocusOut:
						if (xevent.FocusChangeEvent.detail == NotifyDetail.NotifyNonlinear)
						{
							while (XplatUIX11.Keyboard.ResetKeyState(XplatUIX11.FocusWindow, ref msg))
							{
								this.SendMessage(XplatUIX11.FocusWindow, msg.message, msg.wParam, msg.lParam);
							}
							XplatUIX11.Keyboard.FocusOut(hwnd.client_window);
							this.SendMessage(XplatUIX11.FocusWindow, Msg.WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
						}
						break;
					case XEventName.Expose:
						if (!hwnd.Mapped)
						{
							if (flag)
							{
								hwnd.expose_pending = false;
							}
							else
							{
								hwnd.nc_expose_pending = false;
							}
						}
						else if (flag)
						{
							if (hwnd.expose_pending)
							{
								goto IL_1732;
							}
						}
						else if (hwnd.nc_expose_pending)
						{
							goto IL_1747;
						}
						break;
					case XEventName.DestroyNotify:
						hwnd = Hwnd.ObjectFromHandle(xevent.DestroyWindowEvent.window);
						if (hwnd != null && hwnd.client_window == xevent.DestroyWindowEvent.window)
						{
							goto Block_88;
						}
						break;
					case XEventName.ReparentNotify:
						if (hwnd.parent == null)
						{
							if (xevent.ReparentEvent.parent != IntPtr.Zero && xevent.ReparentEvent.window == hwnd.whole_window)
							{
								hwnd.Reparented = true;
								Point topLevelWindowLocation = XplatUIX11.GetTopLevelWindowLocation(hwnd);
								hwnd.X = topLevelWindowLocation.X;
								hwnd.Y = topLevelWindowLocation.Y;
								if (hwnd.opacity != 4294967295U)
								{
									IntPtr intPtr3 = (IntPtr)((int)hwnd.opacity);
									XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, this.XGetParent(hwnd.whole_window), XplatUIX11._NET_WM_WINDOW_OPACITY, (IntPtr)6, 32, PropertyMode.Replace, ref intPtr3, 1);
								}
								this.SendMessage(msg.hwnd, Msg.WM_WINDOWPOSCHANGED, msg.wParam, msg.lParam);
							}
							else
							{
								hwnd.Reparented = false;
							}
						}
						break;
					case XEventName.ConfigureNotify:
						if (!flag && xevent.ConfigureEvent.xevent == xevent.ConfigureEvent.window)
						{
							object configure_lock = hwnd.configure_lock;
							lock (configure_lock)
							{
								Form form2 = Control.FromHandle(hwnd.client_window) as Form;
								if (form2 != null && !hwnd.resizing_or_moving)
								{
									if (hwnd.x != form2.Bounds.X || hwnd.y != form2.Bounds.Y)
									{
										this.SendMessage(form2.Handle, Msg.WM_SYSCOMMAND, (IntPtr)61456, IntPtr.Zero);
										hwnd.resizing_or_moving = true;
									}
									else if (hwnd.width != form2.Bounds.Width || hwnd.height != form2.Bounds.Height)
									{
										this.SendMessage(form2.Handle, Msg.WM_SYSCOMMAND, (IntPtr)61440, IntPtr.Zero);
										hwnd.resizing_or_moving = true;
									}
									if (hwnd.resizing_or_moving)
									{
										this.SendMessage(form2.Handle, Msg.WM_ENTERSIZEMOVE, IntPtr.Zero, IntPtr.Zero);
									}
								}
								this.SendMessage(msg.hwnd, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
								hwnd.configure_pending = false;
								if (hwnd.whole_window != hwnd.client_window)
								{
									this.PerformNCCalc(hwnd);
								}
							}
						}
						break;
					case XEventName.ClientMessage:
						if (!XplatUIX11.Dnd.HandleClientMessage(ref xevent))
						{
							if (xevent.ClientMessageEvent.message_type == XplatUIX11.AsyncAtom)
							{
								XplatUIDriverSupport.ExecuteClientMessage((GCHandle)xevent.ClientMessageEvent.ptr1);
							}
							else
							{
								if (xevent.ClientMessageEvent.message_type == XplatUIX11.HoverState.Atom)
								{
									goto Block_91;
								}
								if (xevent.ClientMessageEvent.message_type == XplatUIX11.PostAtom)
								{
									goto Block_92;
								}
								if (xevent.ClientMessageEvent.message_type == XplatUIX11._XEMBED && xevent.ClientMessageEvent.ptr2.ToInt32() == 0)
								{
									XSizeHints xsizeHints = default(XSizeHints);
									IntPtr intPtr4;
									XplatUIX11.XGetWMNormalHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints, out intPtr4);
									hwnd.width = xsizeHints.max_width;
									hwnd.height = xsizeHints.max_height;
									hwnd.ClientRect = Rectangle.Empty;
									this.SendMessage(msg.hwnd, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
								}
								if (xevent.ClientMessageEvent.message_type == XplatUIX11.WM_PROTOCOLS)
								{
									if (xevent.ClientMessageEvent.ptr1 == XplatUIX11.WM_DELETE_WINDOW)
									{
										goto Block_97;
									}
									if (xevent.ClientMessageEvent.ptr1 == XplatUIX11.WM_TAKE_FOCUS)
									{
									}
								}
							}
						}
						break;
					}
				}
			}
			msg.hwnd = IntPtr.Zero;
			msg.message = Msg.WM_ENTERIDLE;
			return true;
			IL_0264:
			XplatUIX11.Keyboard.KeyEvent(XplatUIX11.FocusWindow, xevent, ref msg);
			if (msg.wParam == (IntPtr)112 || msg.wParam == (IntPtr)47)
			{
				HELPINFO helpinfo = default(HELPINFO);
				this.GetCursorPos(IntPtr.Zero, out helpinfo.MousePos.x, out helpinfo.MousePos.y);
				IntPtr intPtr5 = Marshal.AllocHGlobal(Marshal.SizeOf(helpinfo));
				Marshal.StructureToPtr(helpinfo, intPtr5, true);
				NativeWindow.WndProc(XplatUIX11.FocusWindow, Msg.WM_HELP, IntPtr.Zero, intPtr5);
				Marshal.FreeHGlobal(intPtr5);
			}
			return true;
			IL_0310:
			XplatUIX11.Keyboard.KeyEvent(XplatUIX11.FocusWindow, xevent, ref msg);
			return true;
			IL_0326:
			switch (xevent.ButtonEvent.button)
			{
			case 1:
				XplatUIX11.MouseState |= MouseButtons.Left;
				if (flag)
				{
					msg.message = Msg.WM_LBUTTONDOWN;
					msg.wParam = this.GetMousewParam(0);
				}
				else
				{
					msg.message = Msg.WM_NCLBUTTONDOWN;
					msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
					this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
				}
				break;
			case 2:
				XplatUIX11.MouseState |= MouseButtons.Middle;
				if (flag)
				{
					msg.message = Msg.WM_MBUTTONDOWN;
					msg.wParam = this.GetMousewParam(0);
				}
				else
				{
					msg.message = Msg.WM_NCMBUTTONDOWN;
					msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
					this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
				}
				break;
			case 3:
				XplatUIX11.MouseState |= MouseButtons.Right;
				if (flag)
				{
					msg.message = Msg.WM_RBUTTONDOWN;
					msg.wParam = this.GetMousewParam(0);
				}
				else
				{
					msg.message = Msg.WM_NCRBUTTONDOWN;
					msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
					this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
				}
				break;
			case 4:
				msg.hwnd = XplatUIX11.FocusWindow;
				msg.message = Msg.WM_MOUSEWHEEL;
				msg.wParam = this.GetMousewParam(120);
				break;
			case 5:
				msg.hwnd = XplatUIX11.FocusWindow;
				msg.message = Msg.WM_MOUSEWHEEL;
				msg.wParam = this.GetMousewParam(-120);
				break;
			}
			msg.lParam = (IntPtr)((xevent.ButtonEvent.y << 16) | xevent.ButtonEvent.x);
			this.mouse_position.X = xevent.ButtonEvent.x;
			this.mouse_position.Y = xevent.ButtonEvent.y;
			if (!hwnd.Enabled)
			{
				msg.hwnd = hwnd.EnabledHwnd;
				IntPtr intPtr6;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, Hwnd.ObjectFromHandle(msg.hwnd).ClientWindow, xevent.ButtonEvent.x, xevent.ButtonEvent.y, out xevent.ButtonEvent.x, out xevent.ButtonEvent.y, out intPtr6);
				msg.lParam = (IntPtr)((this.mouse_position.Y << 16) | this.mouse_position.X);
			}
			if (XplatUIX11.Grab.Hwnd != IntPtr.Zero)
			{
				msg.hwnd = XplatUIX11.Grab.Hwnd;
			}
			if (XplatUIX11.ClickPending.Pending && (long)xevent.ButtonEvent.time - XplatUIX11.ClickPending.Time < (long)XplatUIX11.DoubleClickInterval && msg.wParam == XplatUIX11.ClickPending.wParam && msg.lParam == XplatUIX11.ClickPending.lParam && msg.message == XplatUIX11.ClickPending.Message)
			{
				switch (xevent.ButtonEvent.button)
				{
				case 1:
					msg.message = ((!flag) ? Msg.WM_NCLBUTTONDBLCLK : Msg.WM_LBUTTONDBLCLK);
					break;
				case 2:
					msg.message = ((!flag) ? Msg.WM_NCMBUTTONDBLCLK : Msg.WM_MBUTTONDBLCLK);
					break;
				case 3:
					msg.message = ((!flag) ? Msg.WM_NCRBUTTONDBLCLK : Msg.WM_RBUTTONDBLCLK);
					break;
				}
				XplatUIX11.ClickPending.Pending = false;
			}
			else
			{
				XplatUIX11.ClickPending.Pending = true;
				XplatUIX11.ClickPending.Hwnd = msg.hwnd;
				XplatUIX11.ClickPending.Message = msg.message;
				XplatUIX11.ClickPending.wParam = msg.wParam;
				XplatUIX11.ClickPending.lParam = msg.lParam;
				XplatUIX11.ClickPending.Time = (long)xevent.ButtonEvent.time;
			}
			if (msg.message == Msg.WM_LBUTTONDOWN || msg.message == Msg.WM_MBUTTONDOWN || msg.message == Msg.WM_RBUTTONDOWN)
			{
				this.SendParentNotify(msg.hwnd, msg.message, this.mouse_position.X, this.mouse_position.Y);
			}
			return true;
			Block_33:
			goto IL_0A59;
			IL_088A:
			if (flag)
			{
				msg.message = Msg.WM_LBUTTONUP;
			}
			else
			{
				msg.message = Msg.WM_NCLBUTTONUP;
				msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
				this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
			}
			XplatUIX11.MouseState &= ~MouseButtons.Left;
			msg.wParam = this.GetMousewParam(0);
			goto IL_0A59;
			IL_0921:
			if (flag)
			{
				msg.message = Msg.WM_MBUTTONUP;
			}
			else
			{
				msg.message = Msg.WM_NCMBUTTONUP;
				msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
				this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
			}
			XplatUIX11.MouseState &= ~MouseButtons.Middle;
			msg.wParam = this.GetMousewParam(0);
			goto IL_0A59;
			IL_09B8:
			if (flag)
			{
				msg.message = Msg.WM_RBUTTONUP;
			}
			else
			{
				msg.message = Msg.WM_NCRBUTTONUP;
				msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
				this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
			}
			XplatUIX11.MouseState &= ~MouseButtons.Right;
			msg.wParam = this.GetMousewParam(0);
			IL_0A59:
			if (!hwnd.Enabled)
			{
				msg.hwnd = hwnd.EnabledHwnd;
				IntPtr intPtr7;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, Hwnd.ObjectFromHandle(msg.hwnd).ClientWindow, xevent.ButtonEvent.x, xevent.ButtonEvent.y, out xevent.ButtonEvent.x, out xevent.ButtonEvent.y, out intPtr7);
				msg.lParam = (IntPtr)((this.mouse_position.Y << 16) | this.mouse_position.X);
			}
			if (XplatUIX11.Grab.Hwnd != IntPtr.Zero)
			{
				msg.hwnd = XplatUIX11.Grab.Hwnd;
			}
			msg.lParam = (IntPtr)((xevent.ButtonEvent.y << 16) | xevent.ButtonEvent.x);
			this.mouse_position.X = xevent.ButtonEvent.x;
			this.mouse_position.Y = xevent.ButtonEvent.y;
			if (msg.message == Msg.WM_LBUTTONUP || msg.message == Msg.WM_MBUTTONUP || msg.message == Msg.WM_RBUTTONUP)
			{
				XEvent xevent2 = default(XEvent);
				xevent2.type = XEventName.MotionNotify;
				xevent2.MotionEvent.display = XplatUIX11.DisplayHandle;
				xevent2.MotionEvent.window = xevent.ButtonEvent.window;
				xevent2.MotionEvent.x = xevent.ButtonEvent.x;
				xevent2.MotionEvent.y = xevent.ButtonEvent.y;
				hwnd.Queue.EnqueueLocked(xevent2);
			}
			return true;
			IL_0C17:
			if (flag)
			{
				if (XplatUIX11.Grab.Hwnd != IntPtr.Zero)
				{
					msg.hwnd = XplatUIX11.Grab.Hwnd;
				}
				else if (hwnd.Enabled)
				{
					NativeWindow.WndProc(msg.hwnd, Msg.WM_SETCURSOR, msg.hwnd, (IntPtr)1);
				}
				if (xevent.MotionEvent.is_hint != 0)
				{
					IntPtr intPtr8;
					IntPtr intPtr9;
					int num6;
					XplatUIX11.XQueryPointer(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, out intPtr8, out intPtr9, out xevent.MotionEvent.x_root, out xevent.MotionEvent.y_root, out xevent.MotionEvent.x, out xevent.MotionEvent.y, out num6);
				}
				msg.message = Msg.WM_MOUSEMOVE;
				msg.wParam = this.GetMousewParam(0);
				msg.lParam = (IntPtr)((xevent.MotionEvent.y << 16) | (xevent.MotionEvent.x & 65535));
				if (!hwnd.Enabled)
				{
					msg.hwnd = hwnd.EnabledHwnd;
					IntPtr intPtr10;
					XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, Hwnd.ObjectFromHandle(msg.hwnd).ClientWindow, xevent.MotionEvent.x, xevent.MotionEvent.y, out xevent.MotionEvent.x, out xevent.MotionEvent.y, out intPtr10);
					msg.lParam = (IntPtr)((this.mouse_position.Y << 16) | this.mouse_position.X);
				}
				this.mouse_position.X = xevent.MotionEvent.x;
				this.mouse_position.Y = xevent.MotionEvent.y;
				if (XplatUIX11.HoverState.Timer.Enabled && (this.mouse_position.X + XplatUIX11.HoverState.Size.Width < XplatUIX11.HoverState.X || this.mouse_position.X - XplatUIX11.HoverState.Size.Width > XplatUIX11.HoverState.X || this.mouse_position.Y + XplatUIX11.HoverState.Size.Height < XplatUIX11.HoverState.Y || this.mouse_position.Y - XplatUIX11.HoverState.Size.Height > XplatUIX11.HoverState.Y))
				{
					XplatUIX11.HoverState.Timer.Stop();
					XplatUIX11.HoverState.Timer.Start();
					XplatUIX11.HoverState.X = this.mouse_position.X;
					XplatUIX11.HoverState.Y = this.mouse_position.Y;
				}
				return true;
			}
			msg.message = Msg.WM_NCMOUSEMOVE;
			if (!hwnd.Enabled)
			{
				msg.hwnd = hwnd.EnabledHwnd;
				IntPtr intPtr11;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, Hwnd.ObjectFromHandle(msg.hwnd).ClientWindow, xevent.MotionEvent.x, xevent.MotionEvent.y, out xevent.MotionEvent.x, out xevent.MotionEvent.y, out intPtr11);
				msg.lParam = (IntPtr)((this.mouse_position.Y << 16) | this.mouse_position.X);
			}
			HitTest hitTest = this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y);
			NativeWindow.WndProc(hwnd.client_window, Msg.WM_SETCURSOR, msg.hwnd, (IntPtr)((int)hitTest));
			this.mouse_position.X = xevent.MotionEvent.x;
			this.mouse_position.Y = xevent.MotionEvent.y;
			return true;
			Block_57:
			int x_root = xevent.CrossingEvent.x_root;
			int y_root = xevent.CrossingEvent.y_root;
			this.ScreenToClient(XplatUIX11.LastPointerWindow, ref x_root, ref y_root);
			XEvent xevent3 = default(XEvent);
			xevent3.type = XEventName.LeaveNotify;
			xevent3.CrossingEvent.display = XplatUIX11.DisplayHandle;
			xevent3.CrossingEvent.window = XplatUIX11.LastPointerWindow;
			xevent3.CrossingEvent.x = x_root;
			xevent3.CrossingEvent.y = y_root;
			xevent3.CrossingEvent.mode = NotifyMode.NotifyNormal;
			Hwnd hwnd2 = Hwnd.ObjectFromHandle(XplatUIX11.LastPointerWindow);
			hwnd2.Queue.EnqueueLocked(xevent3);
			IL_118D:
			XplatUIX11.LastPointerWindow = xevent.AnyEvent.window;
			msg.message = Msg.WM_MOUSE_ENTER;
			XplatUIX11.HoverState.X = xevent.CrossingEvent.x;
			XplatUIX11.HoverState.Y = xevent.CrossingEvent.y;
			XplatUIX11.HoverState.Timer.Enabled = true;
			XplatUIX11.HoverState.Window = xevent.CrossingEvent.window;
			XEvent xevent4 = default(XEvent);
			xevent4.type = XEventName.MotionNotify;
			xevent4.MotionEvent.display = XplatUIX11.DisplayHandle;
			xevent4.MotionEvent.window = xevent.ButtonEvent.window;
			xevent4.MotionEvent.x = xevent.ButtonEvent.x;
			xevent4.MotionEvent.y = xevent.ButtonEvent.y;
			hwnd.Queue.EnqueueLocked(xevent4);
			return true;
			IL_12F9:
			this.SetCursor(hwnd.client_window, IntPtr.Zero);
			msg.message = Msg.WM_MOUSELEAVE;
			XplatUIX11.HoverState.Timer.Enabled = false;
			XplatUIX11.HoverState.Window = IntPtr.Zero;
			return true;
			IL_1732:
			if (XplatUIX11.Caret.Visible)
			{
				XplatUIX11.Caret.Paused = true;
				this.HideCaret();
			}
			if (XplatUIX11.Caret.Visible)
			{
				this.ShowCaret();
				XplatUIX11.Caret.Paused = false;
			}
			msg.message = Msg.WM_PAINT;
			return true;
			IL_1747:
			FormBorderStyle border_style = hwnd.border_style;
			if (border_style != FormBorderStyle.FixedSingle)
			{
				if (border_style == FormBorderStyle.Fixed3D)
				{
					Graphics graphics = Graphics.FromHwnd(hwnd.whole_window);
					if (hwnd.border_static)
					{
						ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Border3DStyle.SunkenOuter);
					}
					else
					{
						ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Border3DStyle.Sunken);
					}
					graphics.Dispose();
				}
			}
			else
			{
				Graphics graphics2 = Graphics.FromHwnd(hwnd.whole_window);
				ControlPaint.DrawBorder(graphics2, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Color.Black, ButtonBorderStyle.Solid);
				graphics2.Dispose();
			}
			Rectangle rectangle;
			rectangle..ctor(xevent.ExposeEvent.x, xevent.ExposeEvent.y, xevent.ExposeEvent.width, xevent.ExposeEvent.height);
			Region region = new Region(rectangle);
			IntPtr hrgn = region.GetHrgn(null);
			msg.message = Msg.WM_NCPAINT;
			msg.wParam = ((!(hrgn == IntPtr.Zero)) ? hrgn : ((IntPtr)1));
			msg.refobject = region;
			return true;
			Block_88:
			this.CleanupCachedWindows(hwnd);
			msg.hwnd = hwnd.client_window;
			msg.message = Msg.WM_DESTROY;
			hwnd.Dispose();
			return true;
			Block_91:
			msg.message = Msg.WM_MOUSEHOVER;
			msg.wParam = this.GetMousewParam(0);
			msg.lParam = xevent.ClientMessageEvent.ptr1;
			return true;
			Block_92:
			msg.hwnd = xevent.ClientMessageEvent.ptr1;
			msg.message = (Msg)xevent.ClientMessageEvent.ptr2.ToInt32();
			msg.wParam = xevent.ClientMessageEvent.ptr3;
			msg.lParam = xevent.ClientMessageEvent.ptr4;
			return msg.message != Msg.WM_QUIT;
			Block_97:
			this.SendMessage(msg.hwnd, Msg.WM_SYSCOMMAND, (IntPtr)61536, IntPtr.Zero);
			msg.message = Msg.WM_CLOSE;
			return true;
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x001258D4 File Offset: 0x00123AD4
		private HitTest NCHitTest(Hwnd hwnd, int x, int y)
		{
			int num;
			int num2;
			IntPtr intPtr;
			XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, hwnd.WholeWindow, XplatUIX11.RootWindow, x, y, out num, out num2, out intPtr);
			return (HitTest)(int)NativeWindow.WndProc(hwnd.client_window, Msg.WM_NCHITTEST, IntPtr.Zero, (IntPtr)((num2 << 16) | (num & 65535)));
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x0012592C File Offset: 0x00123B2C
		internal override bool GetText(IntPtr handle, out string text)
		{
			object xlibLock = XplatUIX11.XlibLock;
			bool flag;
			lock (xlibLock)
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr intPtr;
				int num;
				IntPtr intPtr2;
				IntPtr intPtr3;
				XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, handle, XplatUIX11._NET_WM_NAME, IntPtr.Zero, new IntPtr(1), false, XplatUIX11.UTF8_STRING, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
				if ((long)intPtr2 > 0L && zero != IntPtr.Zero)
				{
					text = Marshal.PtrToStringUni(zero, (int)intPtr2);
					XplatUIX11.XFree(zero);
					flag = true;
				}
				else
				{
					IntPtr zero2 = IntPtr.Zero;
					XplatUIX11.XFetchName(XplatUIX11.DisplayHandle, Hwnd.ObjectFromHandle(handle).whole_window, ref zero2);
					if (zero2 != IntPtr.Zero)
					{
						text = Marshal.PtrToStringAnsi(zero2);
						XplatUIX11.XFree(zero2);
						flag = true;
					}
					else
					{
						text = string.Empty;
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00125A38 File Offset: 0x00123C38
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

		// Token: 0x06004A9C RID: 19100 RVA: 0x00125AB8 File Offset: 0x00123CB8
		internal override FormWindowState GetWindowState(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd.cached_window_state == (FormWindowState)(-1))
			{
				hwnd.cached_window_state = this.UpdateWindowState(handle);
			}
			return hwnd.cached_window_state;
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00125AEC File Offset: 0x00123CEC
		private FormWindowState UpdateWindowState(IntPtr handle)
		{
			IntPtr zero = IntPtr.Zero;
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			int num = 0;
			bool flag = false;
			IntPtr intPtr;
			int num2;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_STATE, IntPtr.Zero, new IntPtr(256), false, (IntPtr)4, out intPtr, out num2, out intPtr2, out intPtr3, ref zero);
			if ((long)intPtr2 > 0L && zero != IntPtr.Zero)
			{
				int num3 = 0;
				while ((long)num3 < (long)intPtr2)
				{
					IntPtr intPtr4 = (IntPtr)Marshal.ReadInt32(zero, num3 * 4);
					if (intPtr4 == XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ || intPtr4 == XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT)
					{
						num++;
					}
					else if (intPtr4 == XplatUIX11._NET_WM_STATE_HIDDEN)
					{
						flag = true;
					}
					num3++;
				}
				XplatUIX11.XFree(zero);
			}
			if (flag)
			{
				return FormWindowState.Minimized;
			}
			if (num == 2)
			{
				return FormWindowState.Maximized;
			}
			XWindowAttributes xwindowAttributes = default(XWindowAttributes);
			XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, hwnd.client_window, ref xwindowAttributes);
			if (xwindowAttributes.map_state == MapState.IsUnmapped)
			{
				return (FormWindowState)(-1);
			}
			return FormWindowState.Normal;
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x00125C18 File Offset: 0x00123E18
		internal override void GrabInfo(out IntPtr handle, out bool GrabConfined, out Rectangle GrabArea)
		{
			handle = XplatUIX11.Grab.Hwnd;
			GrabConfined = XplatUIX11.Grab.Confined;
			GrabArea = XplatUIX11.Grab.Area;
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x00125C50 File Offset: 0x00123E50
		internal override void GrabWindow(IntPtr handle, IntPtr confine_to_handle)
		{
			IntPtr intPtr = IntPtr.Zero;
			Hwnd hwnd;
			if (confine_to_handle != IntPtr.Zero)
			{
				XWindowAttributes xwindowAttributes = default(XWindowAttributes);
				hwnd = Hwnd.ObjectFromHandle(confine_to_handle);
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, hwnd.client_window, ref xwindowAttributes);
				}
				XplatUIX11.Grab.Area.X = xwindowAttributes.x;
				XplatUIX11.Grab.Area.Y = xwindowAttributes.y;
				XplatUIX11.Grab.Area.Width = xwindowAttributes.width;
				XplatUIX11.Grab.Area.Height = xwindowAttributes.height;
				XplatUIX11.Grab.Confined = true;
				intPtr = hwnd.client_window;
			}
			XplatUIX11.Grab.Hwnd = handle;
			hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock2 = XplatUIX11.XlibLock;
			lock (xlibLock2)
			{
				XplatUIX11.XGrabPointer(XplatUIX11.DisplayHandle, hwnd.client_window, false, EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.LeaveWindowMask | EventMask.PointerMotionMask | EventMask.PointerMotionHintMask | EventMask.ButtonMotionMask, GrabMode.GrabModeAsync, GrabMode.GrabModeAsync, intPtr, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x00125D9C File Offset: 0x00123F9C
		internal override void UngrabWindow(IntPtr hwnd)
		{
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XUngrabPointer(XplatUIX11.DisplayHandle, IntPtr.Zero);
				XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
			}
			this.WindowUngrabbed(hwnd);
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00125E00 File Offset: 0x00124000
		private void WindowUngrabbed(IntPtr hwnd)
		{
			bool flag = XplatUIX11.Grab.Hwnd != IntPtr.Zero;
			XplatUIX11.Grab.Hwnd = IntPtr.Zero;
			XplatUIX11.Grab.Confined = false;
			if (flag)
			{
				this.SendMessage(hwnd, Msg.WM_CAPTURECHANGED, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x00125E5C File Offset: 0x0012405C
		internal override void HandleException(Exception e)
		{
			StackTrace stackTrace = new StackTrace(e, true);
			Console.WriteLine("Exception '{0}'", e.Message + stackTrace.ToString());
			Console.WriteLine("{0}{1}", e.Message, stackTrace.ToString());
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x00125EA4 File Offset: 0x001240A4
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

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00125F08 File Offset: 0x00124108
		internal override void InvalidateNC(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.AddExpose(hwnd, hwnd.WholeWindow == hwnd.ClientWindow, 0, 0, hwnd.Width, hwnd.Height);
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x00125F44 File Offset: 0x00124144
		internal override bool IsEnabled(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			return hwnd != null && hwnd.Enabled;
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x00125F68 File Offset: 0x00124168
		internal override bool IsVisible(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			return hwnd != null && hwnd.visible;
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x00125F8C File Offset: 0x0012418C
		internal override void KillTimer(Timer timer)
		{
			XEventQueue xeventQueue = (XEventQueue)XplatUIX11.MessageQueues[timer.thread];
			if (xeventQueue == null)
			{
				ArrayList arrayList = XplatUIX11.unattached_timer_list;
				lock (arrayList)
				{
					if (XplatUIX11.unattached_timer_list.Contains(timer))
					{
						XplatUIX11.unattached_timer_list.Remove(timer);
					}
				}
				return;
			}
			xeventQueue.timer_list.Remove(timer);
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00126014 File Offset: 0x00124214
		internal override void MenuToScreen(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.RootWindow, x, y, out num, out num2, out intPtr);
			}
			x = num;
			y = num2;
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00126088 File Offset: 0x00124288
		internal override void OverrideCursor(IntPtr cursor)
		{
			if (XplatUIX11.Grab.Hwnd != IntPtr.Zero)
			{
				XplatUIX11.XChangeActivePointerGrab(XplatUIX11.DisplayHandle, EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.PointerMotionMask | EventMask.PointerMotionHintMask | EventMask.ButtonMotionMask, cursor, IntPtr.Zero);
				return;
			}
			XplatUIX11.OverrideCursorHandle = cursor;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x001260CC File Offset: 0x001242CC
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
			if (XplatUIX11.Caret.Visible)
			{
				XplatUIX11.Caret.Paused = true;
				this.HideCaret();
			}
			Graphics graphics;
			PaintEventArgs paintEventArgs;
			if (client)
			{
				graphics = Graphics.FromHwnd(hwnd2.client_window);
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
				return paintEventArgs;
			}
			graphics = Graphics.FromHwnd(hwnd2.whole_window);
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
			return paintEventArgs;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x0012624C File Offset: 0x0012444C
		internal override void PaintEventEnd(ref Message msg, IntPtr handle, bool client)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(msg.HWnd);
			Graphics graphics = (Graphics)hwnd.drawing_stack.Pop();
			graphics.Flush();
			graphics.Dispose();
			PaintEventArgs paintEventArgs = (PaintEventArgs)hwnd.drawing_stack.Pop();
			paintEventArgs.SetGraphics(null);
			paintEventArgs.Dispose();
			if (XplatUIX11.Caret.Visible)
			{
				this.ShowCaret();
				XplatUIX11.Caret.Paused = false;
			}
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x001262C4 File Offset: 0x001244C4
		[MonoTODO("Implement filtering and PM_NOREMOVE")]
		internal override bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags)
		{
			XEventQueue xeventQueue = (XEventQueue)queue_id;
			if ((flags & 1U) == 0U)
			{
				throw new NotImplementedException("PeekMessage PM_NOREMOVE is not implemented yet");
			}
			bool flag = false;
			if (xeventQueue.Count > 0)
			{
				flag = true;
			}
			else if (XplatUIX11.XPending(XplatUIX11.DisplayHandle) != 0)
			{
				this.UpdateMessageQueue((XEventQueue)queue_id);
				flag = true;
			}
			else if (((XEventQueue)queue_id).Paint.Count > 0)
			{
				flag = true;
			}
			this.CheckTimers(xeventQueue.timer_list, DateTime.UtcNow);
			return flag && this.GetMessage(queue_id, ref msg, hWnd, wFilterMin, wFilterMax);
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x00126364 File Offset: 0x00124564
		internal override bool PostMessage(IntPtr handle, Msg message, IntPtr wparam, IntPtr lparam)
		{
			XEvent xevent = default(XEvent);
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			xevent.type = XEventName.ClientMessage;
			xevent.ClientMessageEvent.display = XplatUIX11.DisplayHandle;
			if (hwnd != null)
			{
				xevent.ClientMessageEvent.window = hwnd.whole_window;
			}
			else
			{
				xevent.ClientMessageEvent.window = IntPtr.Zero;
			}
			xevent.ClientMessageEvent.message_type = XplatUIX11.PostAtom;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = handle;
			xevent.ClientMessageEvent.ptr2 = (IntPtr)((int)message);
			xevent.ClientMessageEvent.ptr3 = wparam;
			xevent.ClientMessageEvent.ptr4 = lparam;
			if (hwnd != null)
			{
				hwnd.Queue.EnqueueLocked(xevent);
			}
			else
			{
				this.ThreadQueue(Thread.CurrentThread).EnqueueLocked(xevent);
			}
			return true;
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x0012644C File Offset: 0x0012464C
		internal override void PostQuitMessage(int exitCode)
		{
			ApplicationContext context = Application.MWFThread.Current.Context;
			Form form = ((context == null) ? null : context.MainForm);
			if (form != null)
			{
				this.PostMessage(Application.MWFThread.Current.Context.MainForm.window.Handle, Msg.WM_QUIT, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				this.PostMessage(XplatUIX11.FosterParent, Msg.WM_QUIT, IntPtr.Zero, IntPtr.Zero);
			}
			XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x001264D4 File Offset: 0x001246D4
		internal override void RequestAdditionalWM_NCMessages(IntPtr hwnd, bool hover, bool leave)
		{
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x001264D8 File Offset: 0x001246D8
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

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00126518 File Offset: 0x00124718
		internal override void ResetMouseHover(IntPtr handle)
		{
			if (Hwnd.ObjectFromHandle(handle) == null)
			{
				return;
			}
			XplatUIX11.HoverState.Timer.Enabled = true;
			XplatUIX11.HoverState.X = this.mouse_position.X;
			XplatUIX11.HoverState.Y = this.mouse_position.Y;
			XplatUIX11.HoverState.Window = handle;
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x00126578 File Offset: 0x00124778
		internal override void ScreenToClient(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, hwnd.client_window, x, y, out num, out num2, out intPtr);
			}
			x = num;
			y = num2;
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x001265EC File Offset: 0x001247EC
		internal override void ScreenToMenu(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, hwnd.whole_window, x, y, out num, out num2, out intPtr);
			}
			Form form = Control.FromHandle(handle) as Form;
			if (form != null && form.window_manager != null)
			{
				num2 -= form.window_manager.TitleBarHeight;
			}
			x = num;
			y = num2;
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0012668C File Offset: 0x0012488C
		private bool GraphicsExposePredicate(IntPtr display, ref XEvent xevent, IntPtr arg)
		{
			return (xevent.type == XEventName.GraphicsExpose || xevent.type == XEventName.NoExpose) && arg == xevent.GraphicsExposeEvent.drawable;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001266C8 File Offset: 0x001248C8
		private void ProcessGraphicsExpose(Hwnd hwnd)
		{
			XEvent xevent = default(XEvent);
			IntPtr intPtr = Hwnd.HandleFromObject(hwnd);
			XplatUIX11.EventPredicate eventPredicate = new XplatUIX11.EventPredicate(this.GraphicsExposePredicate);
			do
			{
				XplatUIX11.XIfEvent(XplatUIX11.Display, ref xevent, eventPredicate, intPtr);
				if (xevent.type != XEventName.GraphicsExpose)
				{
					break;
				}
				this.AddExpose(hwnd, xevent.ExposeEvent.window == hwnd.ClientWindow, xevent.GraphicsExposeEvent.x, xevent.GraphicsExposeEvent.y, xevent.GraphicsExposeEvent.width, xevent.GraphicsExposeEvent.height);
			}
			while (xevent.GraphicsExposeEvent.count != 0);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00126780 File Offset: 0x00124980
		internal override void ScrollWindow(IntPtr handle, Rectangle area, int XAmount, int YAmount, bool with_children)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Rectangle rectangle = Rectangle.Intersect(hwnd.Invalid, area);
			if (!rectangle.IsEmpty)
			{
				rectangle.X += XAmount;
				rectangle.Y += YAmount;
				if (rectangle.X < 0)
				{
					rectangle.Width += rectangle.X;
					rectangle.X = 0;
				}
				if (rectangle.Y < 0)
				{
					rectangle.Height += rectangle.Y;
					rectangle.Y = 0;
				}
				if (area.Contains(hwnd.Invalid))
				{
					hwnd.ClearInvalidArea();
				}
				hwnd.AddInvalidArea(rectangle);
			}
			XGCValues xgcvalues = default(XGCValues);
			if (with_children)
			{
				xgcvalues.subwindow_mode = GCSubwindowMode.IncludeInferiors;
			}
			IntPtr intPtr = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, hwnd.client_window, IntPtr.Zero, ref xgcvalues);
			Rectangle totalVisibleArea = this.GetTotalVisibleArea(hwnd.client_window);
			totalVisibleArea.Intersect(area);
			Rectangle rectangle2 = totalVisibleArea;
			rectangle2.Y += YAmount;
			rectangle2.X += XAmount;
			rectangle2.Intersect(area);
			Point point;
			point..ctor(rectangle2.X - XAmount, rectangle2.Y - YAmount);
			XplatUIX11.XCopyArea(XplatUIX11.DisplayHandle, hwnd.client_window, hwnd.client_window, intPtr, point.X, point.Y, rectangle2.Width, rectangle2.Height, rectangle2.X, rectangle2.Y);
			Rectangle dirtyArea = this.GetDirtyArea(area, rectangle2, XAmount, YAmount);
			this.AddExpose(hwnd, true, dirtyArea.X, dirtyArea.Y, dirtyArea.Width, dirtyArea.Height);
			this.ProcessGraphicsExpose(hwnd);
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, intPtr);
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x00126950 File Offset: 0x00124B50
		internal override void ScrollWindow(IntPtr handle, int XAmount, int YAmount, bool with_children)
		{
			Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(handle);
			Rectangle clientRect = objectFromWindow.ClientRect;
			clientRect.X = 0;
			clientRect.Y = 0;
			this.ScrollWindow(handle, clientRect, XAmount, YAmount, with_children);
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x00126988 File Offset: 0x00124B88
		private Rectangle GetDirtyArea(Rectangle total_area, Rectangle valid_area, int XAmount, int YAmount)
		{
			Rectangle rectangle = total_area;
			if (YAmount > 0)
			{
				rectangle.Height -= valid_area.Height;
			}
			else if (YAmount < 0)
			{
				rectangle.Height -= valid_area.Height;
				rectangle.Y += valid_area.Height;
			}
			if (XAmount > 0)
			{
				rectangle.Width -= valid_area.Width;
			}
			else if (XAmount < 0)
			{
				rectangle.Width -= valid_area.Width;
				rectangle.X += valid_area.Width;
			}
			return rectangle;
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x00126A40 File Offset: 0x00124C40
		private Rectangle GetTotalVisibleArea(IntPtr handle)
		{
			Control control = Control.FromHandle(handle);
			Rectangle clientRectangle = control.ClientRectangle;
			clientRectangle.Location = control.PointToScreen(Point.Empty);
			for (Control control2 = control.Parent; control2 != null; control2 = control2.Parent)
			{
				if (!control2.IsHandleCreated || !control2.Visible)
				{
					return clientRectangle;
				}
				Rectangle clientRectangle2 = control2.ClientRectangle;
				clientRectangle2.Location = control2.PointToScreen(Point.Empty);
				clientRectangle.Intersect(clientRectangle2);
			}
			clientRectangle.Location = control.PointToClient(clientRectangle.Location);
			return clientRectangle;
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00126AD4 File Offset: 0x00124CD4
		internal override void SendAsyncMethod(AsyncMethodData method)
		{
			XEvent xevent = default(XEvent);
			Hwnd hwnd = Hwnd.ObjectFromHandle(method.Handle);
			xevent.type = XEventName.ClientMessage;
			xevent.ClientMessageEvent.display = XplatUIX11.DisplayHandle;
			xevent.ClientMessageEvent.window = method.Handle;
			xevent.ClientMessageEvent.message_type = XplatUIX11.AsyncAtom;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = (IntPtr)GCHandle.Alloc(method);
			hwnd.Queue.EnqueueLocked(xevent);
			this.WakeupMain();
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00126B6C File Offset: 0x00124D6C
		internal override IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			Hwnd hwnd2 = Hwnd.ObjectFromHandle(hwnd);
			if (hwnd2 != null && hwnd2.queue != this.ThreadQueue(Thread.CurrentThread))
			{
				AsyncMethodResult asyncMethodResult = new AsyncMethodResult();
				this.SendAsyncMethod(new AsyncMethodData
				{
					Handle = hwnd,
					Method = new XplatUIX11.WndProcDelegate(NativeWindow.WndProc),
					Args = new object[] { hwnd, message, wParam, lParam },
					Result = asyncMethodResult
				});
				return IntPtr.Zero;
			}
			string text = hwnd + ":" + message;
			if (XplatUIX11.messageHold[text] != null)
			{
				XplatUIX11.messageHold[text] = (int)XplatUIX11.messageHold[text] - 1;
			}
			return NativeWindow.WndProc(hwnd, message, wParam, lParam);
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00126C5C File Offset: 0x00124E5C
		internal override int SendInput(IntPtr handle, Queue keys)
		{
			if (handle == IntPtr.Zero)
			{
				return 0;
			}
			int count = keys.Count;
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			while (keys.Count > 0)
			{
				MSG msg = (MSG)keys.Dequeue();
				XEvent xevent = default(XEvent);
				xevent.type = ((msg.message != Msg.WM_KEYUP) ? XEventName.KeyPress : XEventName.KeyRelease);
				xevent.KeyEvent.display = XplatUIX11.DisplayHandle;
				if (hwnd != null)
				{
					xevent.KeyEvent.window = hwnd.whole_window;
				}
				else
				{
					xevent.KeyEvent.window = IntPtr.Zero;
				}
				xevent.KeyEvent.keycode = XplatUIX11.Keyboard.ToKeycode((int)msg.wParam);
				hwnd.Queue.EnqueueLocked(xevent);
			}
			return count;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00126D3C File Offset: 0x00124F3C
		internal override void SetAllowDrop(IntPtr handle, bool value)
		{
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x00126D40 File Offset: 0x00124F40
		internal override DragDropEffects StartDrag(IntPtr handle, object data, DragDropEffects allowed_effects)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				throw new ArgumentException("Attempt to begin drag from invalid window handle (" + handle.ToInt32() + ").");
			}
			return XplatUIX11.Dnd.StartDrag(hwnd.client_window, data, allowed_effects);
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x00126D90 File Offset: 0x00124F90
		internal override void SetBorderStyle(IntPtr handle, FormBorderStyle border_style)
		{
			Form form = Control.FromHandle(handle) as Form;
			if (form != null && form.window_manager == null)
			{
				CreateParams createParams = form.GetCreateParams();
				if (border_style == FormBorderStyle.FixedToolWindow || border_style == FormBorderStyle.SizableToolWindow || createParams.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
				{
					form.window_manager = new ToolWindowManager(form);
				}
			}
			this.RequestNCRecalc(handle);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x00126DF4 File Offset: 0x00124FF4
		internal override void SetCaretPos(IntPtr handle, int x, int y)
		{
			if (XplatUIX11.Caret.Hwnd == handle)
			{
				XplatUIX11.Caret.Timer.Stop();
				this.HideCaret();
				XplatUIX11.Caret.X = x;
				XplatUIX11.Caret.Y = y;
				XplatUIX11.Keyboard.SetCaretPos(XplatUIX11.Caret, handle, x, y);
				if (XplatUIX11.Caret.Visible)
				{
					this.ShowCaret();
					XplatUIX11.Caret.Timer.Start();
				}
			}
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x00126E78 File Offset: 0x00125078
		internal override void SetClipRegion(IntPtr handle, Region region)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			hwnd.UserClip = region;
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x00126E9C File Offset: 0x0012509C
		internal override void SetCursor(IntPtr handle, IntPtr cursor)
		{
			Hwnd hwnd;
			if (!(XplatUIX11.OverrideCursorHandle == IntPtr.Zero))
			{
				hwnd = Hwnd.ObjectFromHandle(handle);
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					XplatUIX11.XDefineCursor(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.OverrideCursorHandle);
				}
				return;
			}
			if (XplatUIX11.LastCursorWindow == handle && XplatUIX11.LastCursorHandle == cursor)
			{
				return;
			}
			XplatUIX11.LastCursorHandle = cursor;
			XplatUIX11.LastCursorWindow = handle;
			hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock2 = XplatUIX11.XlibLock;
			lock (xlibLock2)
			{
				if (cursor != IntPtr.Zero)
				{
					XplatUIX11.XDefineCursor(XplatUIX11.DisplayHandle, hwnd.whole_window, cursor);
				}
				else
				{
					XplatUIX11.XUndefineCursor(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
			}
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00126FB8 File Offset: 0x001251B8
		private void QueryPointer(IntPtr display, IntPtr w, out IntPtr root, out IntPtr child, out int root_x, out int root_y, out int child_x, out int child_y, out int mask)
		{
			XplatUIX11.XGrabServer(display);
			IntPtr intPtr;
			XplatUIX11.XQueryPointer(display, w, out root, out intPtr, out root_x, out root_y, out child_x, out child_y, out mask);
			if (root != w)
			{
				intPtr = root;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			while (intPtr != IntPtr.Zero)
			{
				intPtr2 = intPtr;
				XplatUIX11.XQueryPointer(display, intPtr, out root, out intPtr, out root_x, out root_y, out child_x, out child_y, out mask);
			}
			XplatUIX11.XUngrabServer(display);
			XplatUIX11.XFlush(display);
			child = intPtr2;
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x00127034 File Offset: 0x00125234
		internal override void SetCursorPos(IntPtr handle, int x, int y)
		{
			if (handle == IntPtr.Zero)
			{
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					IntPtr intPtr;
					IntPtr intPtr2;
					int num;
					int num2;
					int num3;
					int num4;
					int num5;
					this.QueryPointer(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, out intPtr, out intPtr2, out num, out num2, out num3, out num4, out num5);
					XplatUIX11.XWarpPointer(XplatUIX11.DisplayHandle, IntPtr.Zero, IntPtr.Zero, 0, 0, 0U, 0U, x - num, y - num2);
					XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
					this.QueryPointer(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, out intPtr, out intPtr2, out num, out num2, out num3, out num4, out num5);
					Hwnd hwnd = Hwnd.ObjectFromHandle(intPtr2);
					if (hwnd != null)
					{
						XEvent xevent = default(XEvent);
						xevent.type = XEventName.MotionNotify;
						xevent.MotionEvent.display = XplatUIX11.DisplayHandle;
						xevent.MotionEvent.window = hwnd.client_window;
						xevent.MotionEvent.root = XplatUIX11.RootWindow;
						xevent.MotionEvent.x = num3;
						xevent.MotionEvent.y = num4;
						xevent.MotionEvent.x_root = num;
						xevent.MotionEvent.y_root = num2;
						xevent.MotionEvent.state = num5;
						hwnd.Queue.EnqueueLocked(xevent);
					}
				}
			}
			else
			{
				Hwnd hwnd2 = Hwnd.ObjectFromHandle(handle);
				object xlibLock2 = XplatUIX11.XlibLock;
				lock (xlibLock2)
				{
					XplatUIX11.XWarpPointer(XplatUIX11.DisplayHandle, IntPtr.Zero, hwnd2.client_window, 0, 0, 0U, 0U, x, y);
				}
			}
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x001271F8 File Offset: 0x001253F8
		internal override void SetFocus(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd.client_window == XplatUIX11.FocusWindow)
			{
				return;
			}
			if (!hwnd.enabled)
			{
				return;
			}
			IntPtr focusWindow = XplatUIX11.FocusWindow;
			XplatUIX11.FocusWindow = hwnd.client_window;
			if (focusWindow != IntPtr.Zero)
			{
				this.SendMessage(focusWindow, Msg.WM_KILLFOCUS, XplatUIX11.FocusWindow, IntPtr.Zero);
			}
			this.SendMessage(XplatUIX11.FocusWindow, Msg.WM_SETFOCUS, focusWindow, IntPtr.Zero);
			XplatUIX11.Keyboard.FocusIn(XplatUIX11.FocusWindow);
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00127284 File Offset: 0x00125484
		internal override void SetIcon(IntPtr handle, Icon icon)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				this.SetIcon(hwnd, icon);
			}
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x001272A8 File Offset: 0x001254A8
		internal override void SetMenu(IntPtr handle, Menu menu)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.menu = menu;
			this.RequestNCRecalc(handle);
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x001272CC File Offset: 0x001254CC
		internal override void SetModal(IntPtr handle, bool Modal)
		{
			if (Modal)
			{
				XplatUIX11.ModalWindows.Push(handle);
			}
			else
			{
				if (XplatUIX11.ModalWindows.Contains(handle))
				{
					XplatUIX11.ModalWindows.Pop();
				}
				if (XplatUIX11.ModalWindows.Count > 0)
				{
					this.Activate((IntPtr)XplatUIX11.ModalWindows.Peek());
				}
			}
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Control control = Control.FromHandle(handle);
			this.SetWMStyles(hwnd, control.GetCreateParams());
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x00127354 File Offset: 0x00125554
		internal override IntPtr SetParent(IntPtr handle, IntPtr parent)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.parent = Hwnd.ObjectFromHandle(parent);
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XReparentWindow(XplatUIX11.DisplayHandle, hwnd.whole_window, (hwnd.parent != null) ? hwnd.parent.client_window : XplatUIX11.FosterParent, hwnd.x, hwnd.y);
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x001273EC File Offset: 0x001255EC
		internal override void SetTimer(Timer timer)
		{
			XEventQueue xeventQueue = (XEventQueue)XplatUIX11.MessageQueues[timer.thread];
			if (xeventQueue == null)
			{
				XplatUIX11.unattached_timer_list.Add(timer);
				return;
			}
			xeventQueue.timer_list.Add(timer);
			this.WakeupMain();
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x00127438 File Offset: 0x00125638
		internal override bool SetTopmost(IntPtr handle, bool enabled)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (enabled)
			{
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					if (hwnd.Mapped)
					{
						this.SendNetWMMessage(hwnd.WholeWindow, XplatUIX11._NET_WM_STATE, (IntPtr)1, XplatUIX11._NET_WM_STATE_ABOVE, IntPtr.Zero);
					}
					else
					{
						int[] array = new int[8];
						array[0] = XplatUIX11._NET_WM_STATE_ABOVE.ToInt32();
						XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)4, 32, PropertyMode.Replace, array, 1);
					}
				}
			}
			else
			{
				object xlibLock2 = XplatUIX11.XlibLock;
				lock (xlibLock2)
				{
					if (hwnd.Mapped)
					{
						this.SendNetWMMessage(hwnd.WholeWindow, XplatUIX11._NET_WM_STATE, (IntPtr)0, XplatUIX11._NET_WM_STATE_ABOVE, IntPtr.Zero);
					}
					else
					{
						XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_STATE);
					}
				}
			}
			return true;
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x0012756C File Offset: 0x0012576C
		internal override bool SetOwner(IntPtr handle, IntPtr handle_owner)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (handle_owner != IntPtr.Zero)
			{
				Hwnd hwnd2 = Hwnd.ObjectFromHandle(handle_owner);
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					int[] array = new int[8];
					array[0] = XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL.ToInt32();
					XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_WINDOW_TYPE, (IntPtr)4, 32, PropertyMode.Replace, array, 1);
					if (hwnd2 != null)
					{
						XplatUIX11.XSetTransientForHint(XplatUIX11.DisplayHandle, hwnd.whole_window, hwnd2.whole_window);
					}
					else
					{
						XplatUIX11.XSetTransientForHint(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.RootWindow);
					}
				}
			}
			else
			{
				object xlibLock2 = XplatUIX11.XlibLock;
				lock (xlibLock2)
				{
					XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, (IntPtr)68);
				}
			}
			return true;
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x0012768C File Offset: 0x0012588C
		internal override bool SetVisible(IntPtr handle, bool visible, bool activate)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.visible = visible;
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				if (visible)
				{
					this.MapWindow(hwnd, WindowType.Both);
					if (Control.FromHandle(handle) is Form)
					{
						FormWindowState windowState = ((Form)Control.FromHandle(handle)).WindowState;
						FormWindowState formWindowState = windowState;
						if (formWindowState != FormWindowState.Minimized)
						{
							if (formWindowState == FormWindowState.Maximized)
							{
								this.SetWindowState(handle, FormWindowState.Maximized);
							}
						}
						else
						{
							this.SetWindowState(handle, FormWindowState.Minimized);
						}
					}
					this.SendMessage(handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
				}
				else
				{
					this.UnmapWindow(hwnd, WindowType.Both);
				}
			}
			return true;
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x0012775C File Offset: 0x0012595C
		internal override void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max)
		{
			Control control = Control.FromHandle(handle);
			this.SetWindowMinMax(handle, maximized, min, max, (control == null) ? null : control.GetCreateParams());
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x00127790 File Offset: 0x00125990
		internal void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max, CreateParams cp)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			min.Width = Math.Max(min.Width, SystemInformation.MinimumWindowSize.Width);
			min.Height = Math.Max(min.Height, SystemInformation.MinimumWindowSize.Height);
			XSizeHints xsizeHints = default(XSizeHints);
			IntPtr intPtr;
			XplatUIX11.XGetWMNormalHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints, out intPtr);
			if (min != Size.Empty && min.Width > 0 && min.Height > 0)
			{
				if (cp != null)
				{
					min = XplatUIX11.TranslateWindowSizeToXWindowSize(cp, min);
				}
				xsizeHints.flags = (IntPtr)((int)xsizeHints.flags | 16);
				xsizeHints.min_width = min.Width;
				xsizeHints.min_height = min.Height;
			}
			if (max != Size.Empty && max.Width > 0 && max.Height > 0)
			{
				if (cp != null)
				{
					max = XplatUIX11.TranslateWindowSizeToXWindowSize(cp, max);
				}
				xsizeHints.flags = (IntPtr)((int)xsizeHints.flags | 32);
				xsizeHints.max_width = max.Width;
				xsizeHints.max_height = max.Height;
			}
			if (xsizeHints.flags != IntPtr.Zero)
			{
				XplatUIX11.XSetWMNormalHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints);
			}
			if (maximized != Rectangle.Empty && maximized.Width > 0 && maximized.Height > 0)
			{
				if (cp != null)
				{
					maximized.Size = XplatUIX11.TranslateWindowSizeToXWindowSize(cp);
				}
				xsizeHints.flags = (IntPtr)4;
				xsizeHints.x = maximized.X;
				xsizeHints.y = maximized.Y;
				xsizeHints.width = maximized.Width;
				xsizeHints.height = maximized.Height;
				XplatUIX11.XSetZoomHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints);
			}
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x001279AC File Offset: 0x00125BAC
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
					this.MapWindow(hwnd, WindowType.Whole);
				}
				hwnd.zero_sized = false;
			}
			if (width < 1 || height < 1)
			{
				hwnd.zero_sized = true;
				this.UnmapWindow(hwnd, WindowType.Whole);
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
				if (hwnd.fixed_size)
				{
					this.SetWindowMinMax(handle, Rectangle.Empty, new Size(width, height), new Size(width, height));
				}
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					Control control = Control.FromHandle(handle);
					Size size = XplatUIX11.TranslateWindowSizeToXWindowSize(control.GetCreateParams(), new Size(width, height));
					XplatUIX11.MoveResizeWindow(XplatUIX11.DisplayHandle, hwnd.whole_window, x, y, size.Width, size.Height);
					this.PerformNCCalc(hwnd);
				}
			}
			hwnd.x = x;
			hwnd.y = y;
			hwnd.width = width;
			hwnd.height = height;
			hwnd.ClientRect = Rectangle.Empty;
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00127B70 File Offset: 0x00125D70
		internal override void SetWindowState(IntPtr handle, FormWindowState state)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			FormWindowState windowState = this.GetWindowState(handle);
			if (windowState == state)
			{
				return;
			}
			switch (state)
			{
			case FormWindowState.Normal:
			{
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					if (windowState == FormWindowState.Minimized)
					{
						this.MapWindow(hwnd, WindowType.Both);
					}
					else if (windowState == FormWindowState.Maximized)
					{
						this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)2, XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ, XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT);
					}
				}
				this.Activate(handle);
				return;
			}
			case FormWindowState.Minimized:
			{
				object xlibLock2 = XplatUIX11.XlibLock;
				lock (xlibLock2)
				{
					if (windowState == FormWindowState.Maximized)
					{
						this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)2, XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ, XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT);
					}
					XplatUIX11.XIconifyWindow(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.ScreenNo);
				}
				return;
			}
			case FormWindowState.Maximized:
			{
				object xlibLock3 = XplatUIX11.XlibLock;
				lock (xlibLock3)
				{
					if (windowState == FormWindowState.Minimized)
					{
						this.MapWindow(hwnd, WindowType.Both);
					}
					this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)1, XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ, XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT);
				}
				this.Activate(handle);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x00127D04 File Offset: 0x00125F04
		internal override void SetWindowStyle(IntPtr handle, CreateParams cp)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.SetHwndStyles(hwnd, cp);
			this.SetWMStyles(hwnd, cp);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00127D28 File Offset: 0x00125F28
		internal override double GetWindowTransparency(IntPtr handle)
		{
			return 1.0;
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00127D34 File Offset: 0x00125F34
		internal override void SetWindowTransparency(IntPtr handle, double transparency, Color key)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			hwnd.opacity = (uint)(4294967295.0 * transparency);
			IntPtr intPtr = (IntPtr)((int)hwnd.opacity);
			IntPtr intPtr2 = hwnd.whole_window;
			if (hwnd.reparented)
			{
				intPtr2 = this.XGetParent(hwnd.whole_window);
			}
			XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, intPtr2, XplatUIX11._NET_WM_WINDOW_OPACITY, (IntPtr)6, 32, PropertyMode.Replace, ref intPtr, 1);
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00127DAC File Offset: 0x00125FAC
		internal override bool SetZOrder(IntPtr handle, IntPtr after_handle, bool top, bool bottom)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (!hwnd.mapped)
			{
				return false;
			}
			if (top)
			{
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					XplatUIX11.XRaiseWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				return true;
			}
			if (bottom)
			{
				object xlibLock2 = XplatUIX11.XlibLock;
				lock (xlibLock2)
				{
					XplatUIX11.XLowerWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				return true;
			}
			Hwnd hwnd2 = null;
			if (after_handle != IntPtr.Zero)
			{
				hwnd2 = Hwnd.ObjectFromHandle(after_handle);
			}
			XWindowChanges xwindowChanges = default(XWindowChanges);
			if (hwnd2 == null)
			{
				int[] array = new int[2];
				array[0] = this.unixtime();
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_USER_TIME, (IntPtr)6, 32, PropertyMode.Replace, array, 1);
				XplatUIX11.XRaiseWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_ACTIVE_WINDOW, (IntPtr)1, IntPtr.Zero, IntPtr.Zero);
				return true;
			}
			xwindowChanges.sibling = hwnd2.whole_window;
			xwindowChanges.stack_mode = StackMode.Below;
			object xlibLock3 = XplatUIX11.XlibLock;
			lock (xlibLock3)
			{
				XplatUIX11.XConfigureWindow(XplatUIX11.DisplayHandle, hwnd.whole_window, ChangeWindowFlags.CWSibling | ChangeWindowFlags.CWStackMode, ref xwindowChanges);
			}
			return false;
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x00127F5C File Offset: 0x0012615C
		internal override void ShowCursor(bool show)
		{
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00127F60 File Offset: 0x00126160
		internal override object StartLoop(Thread thread)
		{
			return this.ThreadQueue(thread);
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00127F78 File Offset: 0x00126178
		internal override TransparencySupport SupportsTransparency()
		{
			return TransparencySupport.Set;
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00127F7C File Offset: 0x0012617C
		internal override bool SystrayAdd(IntPtr handle, string tip, Icon icon, out ToolTip tt)
		{
			this.GetSystrayManagerWindow();
			if (XplatUIX11.SystrayMgrWindow != IntPtr.Zero)
			{
				Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
				if (hwnd.client_window != hwnd.whole_window)
				{
					XplatUIX11.Keyboard.DestroyICForWindow(hwnd.client_window);
					XplatUIX11.XDestroyWindow(XplatUIX11.DisplayHandle, hwnd.client_window);
					hwnd.client_window = hwnd.whole_window;
				}
				if (hwnd.nc_expose_pending)
				{
					hwnd.nc_expose_pending = false;
					if (!hwnd.expose_pending)
					{
						hwnd.Queue.Paint.Remove(hwnd);
					}
				}
				XSizeHints xsizeHints = default(XSizeHints);
				xsizeHints.flags = (IntPtr)304;
				xsizeHints.min_width = 24;
				xsizeHints.min_height = 24;
				xsizeHints.max_width = 24;
				xsizeHints.max_height = 24;
				xsizeHints.base_width = 24;
				xsizeHints.base_height = 24;
				XplatUIX11.XSetWMNormalHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints);
				int[] array = new int[] { 1, 1 };
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._XEMBED_INFO, XplatUIX11._XEMBED_INFO, 32, PropertyMode.Replace, array, 2);
				tt = new ToolTip();
				tt.AutomaticDelay = 350;
				tt.InitialDelay = 250;
				tt.ReshowDelay = 250;
				tt.ShowAlways = true;
				if (tip != null && tip != string.Empty)
				{
					tt.SetToolTip(Control.FromHandle(handle), tip);
					tt.Active = true;
				}
				else
				{
					tt.Active = false;
				}
				this.SendNetClientMessage(XplatUIX11.SystrayMgrWindow, XplatUIX11._NET_SYSTEM_TRAY_OPCODE, IntPtr.Zero, (IntPtr)0, hwnd.whole_window);
				return true;
			}
			tt = null;
			return false;
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00128148 File Offset: 0x00126348
		internal override bool SystrayChange(IntPtr handle, string tip, Icon icon, ref ToolTip tt)
		{
			Control control = Control.FromHandle(handle);
			if (control != null && tt != null)
			{
				tt.SetToolTip(control, tip);
				tt.Active = true;
				this.SendMessage(handle, Msg.WM_PAINT, IntPtr.Zero, IntPtr.Zero);
				return true;
			}
			return false;
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x00128194 File Offset: 0x00126394
		internal override void SystrayRemove(IntPtr handle, ref ToolTip tt)
		{
			this.SetVisible(handle, false, false);
			if (tt != null)
			{
				tt.Dispose();
				tt = null;
			}
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x001281B4 File Offset: 0x001263B4
		internal override void SystrayBalloon(IntPtr handle, int timeout, string title, string text, ToolTipIcon icon)
		{
			ThemeEngine.Current.ShowBalloonWindow(handle, timeout, title, text, icon);
			this.SendMessage(handle, Msg.WM_USER, IntPtr.Zero, (IntPtr)1026);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x001281F0 File Offset: 0x001263F0
		internal override bool Text(IntPtr handle, string text)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_NAME, XplatUIX11.UTF8_STRING, 8, PropertyMode.Replace, text, Encoding.UTF8.GetByteCount(text));
				XplatUIX11.XStoreName(XplatUIX11.DisplayHandle, Hwnd.ObjectFromHandle(handle).whole_window, text);
			}
			return true;
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x0012827C File Offset: 0x0012647C
		internal override bool TranslateMessage(ref MSG msg)
		{
			return XplatUIX11.Keyboard.TranslateMessage(ref msg);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x0012828C File Offset: 0x0012648C
		internal override void UpdateWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (!hwnd.visible || !hwnd.expose_pending || !hwnd.Mapped)
			{
				return;
			}
			this.SendMessage(handle, Msg.WM_PAINT, IntPtr.Zero, IntPtr.Zero);
			hwnd.Queue.Paint.Remove(hwnd);
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x001282E8 File Offset: 0x001264E8
		internal override void CreateOffscreenDrawable(IntPtr handle, int width, int height, out object offscreen_drawable)
		{
			IntPtr intPtr;
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			XplatUIX11.XGetGeometry(XplatUIX11.DisplayHandle, handle, out intPtr, out num, out num2, out num3, out num4, out num5, out num6);
			IntPtr intPtr2 = XplatUIX11.XCreatePixmap(XplatUIX11.DisplayHandle, handle, width, height, num6);
			offscreen_drawable = intPtr2;
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x0012832C File Offset: 0x0012652C
		internal override void DestroyOffscreenDrawable(object offscreen_drawable)
		{
			XplatUIX11.XFreePixmap(XplatUIX11.DisplayHandle, (IntPtr)offscreen_drawable);
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x00128340 File Offset: 0x00126540
		internal override Graphics GetOffscreenGraphics(object offscreen_drawable)
		{
			return Graphics.FromHwnd((IntPtr)offscreen_drawable);
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x00128350 File Offset: 0x00126550
		internal override void BlitFromOffscreen(IntPtr dest_handle, Graphics dest_dc, object offscreen_drawable, Graphics offscreen_dc, Rectangle r)
		{
			XGCValues xgcvalues = default(XGCValues);
			IntPtr intPtr = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, dest_handle, IntPtr.Zero, ref xgcvalues);
			XplatUIX11.XCopyArea(XplatUIX11.DisplayHandle, (IntPtr)offscreen_drawable, dest_handle, intPtr, r.X, r.Y, r.Width, r.Height, r.X, r.Y);
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, intPtr);
		}

		// Token: 0x06004AE4 RID: 19172
		[DllImport("libXcursor")]
		internal static extern IntPtr XcursorLibraryLoadCursor(IntPtr display, [MarshalAs(20)] string name);

		// Token: 0x06004AE5 RID: 19173
		[DllImport("libXcursor")]
		internal static extern IntPtr XcursorLibraryLoadImages([MarshalAs(20)] string file, IntPtr theme, int size);

		// Token: 0x06004AE6 RID: 19174
		[DllImport("libXcursor")]
		internal static extern void XcursorImagesDestroy(IntPtr images);

		// Token: 0x06004AE7 RID: 19175
		[DllImport("libXcursor")]
		internal static extern int XcursorGetDefaultSize(IntPtr display);

		// Token: 0x06004AE8 RID: 19176
		[DllImport("libXcursor")]
		internal static extern IntPtr XcursorImageLoadCursor(IntPtr display, IntPtr image);

		// Token: 0x06004AE9 RID: 19177
		[DllImport("libXcursor")]
		internal static extern IntPtr XcursorGetTheme(IntPtr display);

		// Token: 0x06004AEA RID: 19178
		[DllImport("libX11")]
		internal static extern IntPtr XOpenDisplay(IntPtr display);

		// Token: 0x06004AEB RID: 19179
		[DllImport("libX11")]
		internal static extern int XCloseDisplay(IntPtr display);

		// Token: 0x06004AEC RID: 19180
		[DllImport("libX11")]
		internal static extern IntPtr XSynchronize(IntPtr display, bool onoff);

		// Token: 0x06004AED RID: 19181
		[DllImport("libX11")]
		internal static extern IntPtr XCreateWindow(IntPtr display, IntPtr parent, int x, int y, int width, int height, int border_width, int depth, int xclass, IntPtr visual, UIntPtr valuemask, ref XSetWindowAttributes attributes);

		// Token: 0x06004AEE RID: 19182
		[DllImport("libX11")]
		internal static extern IntPtr XCreateSimpleWindow(IntPtr display, IntPtr parent, int x, int y, int width, int height, int border_width, UIntPtr border, UIntPtr background);

		// Token: 0x06004AEF RID: 19183
		[DllImport("libX11")]
		internal static extern int XMapWindow(IntPtr display, IntPtr window);

		// Token: 0x06004AF0 RID: 19184
		[DllImport("libX11")]
		internal static extern int XUnmapWindow(IntPtr display, IntPtr window);

		// Token: 0x06004AF1 RID: 19185
		[DllImport("libX11", EntryPoint = "XMapSubwindows")]
		internal static extern int XMapSubindows(IntPtr display, IntPtr window);

		// Token: 0x06004AF2 RID: 19186
		[DllImport("libX11")]
		internal static extern int XUnmapSubwindows(IntPtr display, IntPtr window);

		// Token: 0x06004AF3 RID: 19187
		[DllImport("libX11")]
		internal static extern IntPtr XRootWindow(IntPtr display, int screen_number);

		// Token: 0x06004AF4 RID: 19188
		[DllImport("libX11")]
		internal static extern IntPtr XNextEvent(IntPtr display, ref XEvent xevent);

		// Token: 0x06004AF5 RID: 19189
		[DllImport("libX11")]
		internal static extern int XConnectionNumber(IntPtr display);

		// Token: 0x06004AF6 RID: 19190
		[DllImport("libX11")]
		internal static extern int XPending(IntPtr display);

		// Token: 0x06004AF7 RID: 19191
		[DllImport("libX11")]
		internal static extern IntPtr XSelectInput(IntPtr display, IntPtr window, IntPtr mask);

		// Token: 0x06004AF8 RID: 19192
		[DllImport("libX11")]
		internal static extern int XDestroyWindow(IntPtr display, IntPtr window);

		// Token: 0x06004AF9 RID: 19193
		[DllImport("libX11")]
		internal static extern int XReparentWindow(IntPtr display, IntPtr window, IntPtr parent, int x, int y);

		// Token: 0x06004AFA RID: 19194
		[DllImport("libX11")]
		private static extern int XMoveResizeWindow(IntPtr display, IntPtr window, int x, int y, int width, int height);

		// Token: 0x06004AFB RID: 19195 RVA: 0x001283C4 File Offset: 0x001265C4
		internal static int MoveResizeWindow(IntPtr display, IntPtr window, int x, int y, int width, int height)
		{
			int num = XplatUIX11.XMoveResizeWindow(display, window, x, y, width, height);
			XplatUIX11.Keyboard.MoveCurrentCaretPos();
			return num;
		}

		// Token: 0x06004AFC RID: 19196
		[DllImport("libX11")]
		internal static extern int XResizeWindow(IntPtr display, IntPtr window, int width, int height);

		// Token: 0x06004AFD RID: 19197
		[DllImport("libX11")]
		internal static extern int XGetWindowAttributes(IntPtr display, IntPtr window, ref XWindowAttributes attributes);

		// Token: 0x06004AFE RID: 19198
		[DllImport("libX11")]
		internal static extern int XFlush(IntPtr display);

		// Token: 0x06004AFF RID: 19199
		[DllImport("libX11")]
		internal static extern int XSetWMName(IntPtr display, IntPtr window, ref XTextProperty text_prop);

		// Token: 0x06004B00 RID: 19200
		[DllImport("libX11")]
		internal static extern int XStoreName(IntPtr display, IntPtr window, string window_name);

		// Token: 0x06004B01 RID: 19201
		[DllImport("libX11")]
		internal static extern int XFetchName(IntPtr display, IntPtr window, ref IntPtr window_name);

		// Token: 0x06004B02 RID: 19202
		[DllImport("libX11")]
		internal static extern int XSendEvent(IntPtr display, IntPtr window, bool propagate, IntPtr event_mask, ref XEvent send_event);

		// Token: 0x06004B03 RID: 19203
		[DllImport("libX11")]
		internal static extern int XQueryTree(IntPtr display, IntPtr window, out IntPtr root_return, out IntPtr parent_return, out IntPtr children_return, out int nchildren_return);

		// Token: 0x06004B04 RID: 19204
		[DllImport("libX11")]
		internal static extern int XFree(IntPtr data);

		// Token: 0x06004B05 RID: 19205
		[DllImport("libX11")]
		internal static extern int XRaiseWindow(IntPtr display, IntPtr window);

		// Token: 0x06004B06 RID: 19206
		[DllImport("libX11")]
		internal static extern uint XLowerWindow(IntPtr display, IntPtr window);

		// Token: 0x06004B07 RID: 19207
		[DllImport("libX11")]
		internal static extern uint XConfigureWindow(IntPtr display, IntPtr window, ChangeWindowFlags value_mask, ref XWindowChanges values);

		// Token: 0x06004B08 RID: 19208
		[DllImport("libX11")]
		internal static extern IntPtr XInternAtom(IntPtr display, string atom_name, bool only_if_exists);

		// Token: 0x06004B09 RID: 19209
		[DllImport("libX11")]
		internal static extern int XInternAtoms(IntPtr display, string[] atom_names, int atom_count, bool only_if_exists, IntPtr[] atoms);

		// Token: 0x06004B0A RID: 19210
		[DllImport("libX11")]
		internal static extern int XSetWMProtocols(IntPtr display, IntPtr window, IntPtr[] protocols, int count);

		// Token: 0x06004B0B RID: 19211
		[DllImport("libX11")]
		internal static extern int XGrabPointer(IntPtr display, IntPtr window, bool owner_events, EventMask event_mask, GrabMode pointer_mode, GrabMode keyboard_mode, IntPtr confine_to, IntPtr cursor, IntPtr timestamp);

		// Token: 0x06004B0C RID: 19212
		[DllImport("libX11")]
		internal static extern int XUngrabPointer(IntPtr display, IntPtr timestamp);

		// Token: 0x06004B0D RID: 19213
		[DllImport("libX11")]
		internal static extern bool XQueryPointer(IntPtr display, IntPtr window, out IntPtr root, out IntPtr child, out int root_x, out int root_y, out int win_x, out int win_y, out int keys_buttons);

		// Token: 0x06004B0E RID: 19214
		[DllImport("libX11")]
		internal static extern bool XTranslateCoordinates(IntPtr display, IntPtr src_w, IntPtr dest_w, int src_x, int src_y, out int intdest_x_return, out int dest_y_return, out IntPtr child_return);

		// Token: 0x06004B0F RID: 19215
		[DllImport("libX11")]
		internal static extern bool XGetGeometry(IntPtr display, IntPtr window, out IntPtr root, out int x, out int y, out int width, out int height, out int border_width, out int depth);

		// Token: 0x06004B10 RID: 19216
		[DllImport("libX11")]
		internal static extern bool XGetGeometry(IntPtr display, IntPtr window, IntPtr root, out int x, out int y, out int width, out int height, IntPtr border_width, IntPtr depth);

		// Token: 0x06004B11 RID: 19217
		[DllImport("libX11")]
		internal static extern bool XGetGeometry(IntPtr display, IntPtr window, IntPtr root, out int x, out int y, IntPtr width, IntPtr height, IntPtr border_width, IntPtr depth);

		// Token: 0x06004B12 RID: 19218
		[DllImport("libX11")]
		internal static extern bool XGetGeometry(IntPtr display, IntPtr window, IntPtr root, IntPtr x, IntPtr y, out int width, out int height, IntPtr border_width, IntPtr depth);

		// Token: 0x06004B13 RID: 19219
		[DllImport("libX11")]
		internal static extern uint XWarpPointer(IntPtr display, IntPtr src_w, IntPtr dest_w, int src_x, int src_y, uint src_width, uint src_height, int dest_x, int dest_y);

		// Token: 0x06004B14 RID: 19220
		[DllImport("libX11")]
		internal static extern int XClearWindow(IntPtr display, IntPtr window);

		// Token: 0x06004B15 RID: 19221
		[DllImport("libX11")]
		internal static extern int XClearArea(IntPtr display, IntPtr window, int x, int y, int width, int height, bool exposures);

		// Token: 0x06004B16 RID: 19222
		[DllImport("libX11")]
		internal static extern IntPtr XDefaultScreenOfDisplay(IntPtr display);

		// Token: 0x06004B17 RID: 19223
		[DllImport("libX11")]
		internal static extern int XScreenNumberOfScreen(IntPtr display, IntPtr Screen);

		// Token: 0x06004B18 RID: 19224
		[DllImport("libX11")]
		internal static extern IntPtr XDefaultVisual(IntPtr display, int screen_number);

		// Token: 0x06004B19 RID: 19225
		[DllImport("libX11")]
		internal static extern uint XDefaultDepth(IntPtr display, int screen_number);

		// Token: 0x06004B1A RID: 19226
		[DllImport("libX11")]
		internal static extern int XDefaultScreen(IntPtr display);

		// Token: 0x06004B1B RID: 19227
		[DllImport("libX11")]
		internal static extern IntPtr XDefaultColormap(IntPtr display, int screen_number);

		// Token: 0x06004B1C RID: 19228
		[DllImport("libX11")]
		internal static extern int XLookupColor(IntPtr display, IntPtr Colormap, string Coloranem, ref XColor exact_def_color, ref XColor screen_def_color);

		// Token: 0x06004B1D RID: 19229
		[DllImport("libX11")]
		internal static extern int XAllocColor(IntPtr display, IntPtr Colormap, ref XColor colorcell_def);

		// Token: 0x06004B1E RID: 19230
		[DllImport("libX11")]
		internal static extern int XSetTransientForHint(IntPtr display, IntPtr window, IntPtr prop_window);

		// Token: 0x06004B1F RID: 19231
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, ref MotifWmHints data, int nelements);

		// Token: 0x06004B20 RID: 19232
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, ref uint value, int nelements);

		// Token: 0x06004B21 RID: 19233
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, ref IntPtr value, int nelements);

		// Token: 0x06004B22 RID: 19234
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, uint[] data, int nelements);

		// Token: 0x06004B23 RID: 19235
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, int[] data, int nelements);

		// Token: 0x06004B24 RID: 19236
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, IntPtr[] data, int nelements);

		// Token: 0x06004B25 RID: 19237
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, IntPtr atoms, int nelements);

		// Token: 0x06004B26 RID: 19238
		[DllImport("libX11", CharSet = 2)]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, string text, int text_length);

		// Token: 0x06004B27 RID: 19239
		[DllImport("libX11")]
		internal static extern int XDeleteProperty(IntPtr display, IntPtr window, IntPtr property);

		// Token: 0x06004B28 RID: 19240
		[DllImport("libX11")]
		internal static extern IntPtr XCreateGC(IntPtr display, IntPtr window, IntPtr valuemask, ref XGCValues values);

		// Token: 0x06004B29 RID: 19241
		[DllImport("libX11")]
		internal static extern int XFreeGC(IntPtr display, IntPtr gc);

		// Token: 0x06004B2A RID: 19242
		[DllImport("libX11")]
		internal static extern int XSetFunction(IntPtr display, IntPtr gc, GXFunction function);

		// Token: 0x06004B2B RID: 19243
		[DllImport("libX11")]
		internal static extern int XSetLineAttributes(IntPtr display, IntPtr gc, int line_width, GCLineStyle line_style, GCCapStyle cap_style, GCJoinStyle join_style);

		// Token: 0x06004B2C RID: 19244
		[DllImport("libX11")]
		internal static extern int XDrawLine(IntPtr display, IntPtr drawable, IntPtr gc, int x1, int y1, int x2, int y2);

		// Token: 0x06004B2D RID: 19245
		[DllImport("libX11")]
		internal static extern int XDrawRectangle(IntPtr display, IntPtr drawable, IntPtr gc, int x1, int y1, int width, int height);

		// Token: 0x06004B2E RID: 19246
		[DllImport("libX11")]
		internal static extern int XFillRectangle(IntPtr display, IntPtr drawable, IntPtr gc, int x1, int y1, int width, int height);

		// Token: 0x06004B2F RID: 19247
		[DllImport("libX11")]
		internal static extern int XSetWindowBackground(IntPtr display, IntPtr window, IntPtr background);

		// Token: 0x06004B30 RID: 19248
		[DllImport("libX11")]
		internal static extern int XCopyArea(IntPtr display, IntPtr src, IntPtr dest, IntPtr gc, int src_x, int src_y, int width, int height, int dest_x, int dest_y);

		// Token: 0x06004B31 RID: 19249
		[DllImport("libX11")]
		internal static extern int XGetWindowProperty(IntPtr display, IntPtr window, IntPtr atom, IntPtr long_offset, IntPtr long_length, bool delete, IntPtr req_type, out IntPtr actual_type, out int actual_format, out IntPtr nitems, out IntPtr bytes_after, ref IntPtr prop);

		// Token: 0x06004B32 RID: 19250
		[DllImport("libX11")]
		internal static extern int XSetInputFocus(IntPtr display, IntPtr window, RevertTo revert_to, IntPtr time);

		// Token: 0x06004B33 RID: 19251
		[DllImport("libX11")]
		internal static extern int XIconifyWindow(IntPtr display, IntPtr window, int screen_number);

		// Token: 0x06004B34 RID: 19252
		[DllImport("libX11")]
		internal static extern int XDefineCursor(IntPtr display, IntPtr window, IntPtr cursor);

		// Token: 0x06004B35 RID: 19253
		[DllImport("libX11")]
		internal static extern int XUndefineCursor(IntPtr display, IntPtr window);

		// Token: 0x06004B36 RID: 19254
		[DllImport("libX11")]
		internal static extern int XFreeCursor(IntPtr display, IntPtr cursor);

		// Token: 0x06004B37 RID: 19255
		[DllImport("libX11")]
		internal static extern IntPtr XCreateFontCursor(IntPtr display, CursorFontShape shape);

		// Token: 0x06004B38 RID: 19256
		[DllImport("libX11")]
		internal static extern IntPtr XCreatePixmapCursor(IntPtr display, IntPtr source, IntPtr mask, ref XColor foreground_color, ref XColor background_color, int x_hot, int y_hot);

		// Token: 0x06004B39 RID: 19257
		[DllImport("libX11")]
		internal static extern IntPtr XCreatePixmapFromBitmapData(IntPtr display, IntPtr drawable, byte[] data, int width, int height, IntPtr fg, IntPtr bg, int depth);

		// Token: 0x06004B3A RID: 19258
		[DllImport("libX11")]
		internal static extern IntPtr XCreatePixmap(IntPtr display, IntPtr d, int width, int height, int depth);

		// Token: 0x06004B3B RID: 19259
		[DllImport("libX11")]
		internal static extern IntPtr XFreePixmap(IntPtr display, IntPtr pixmap);

		// Token: 0x06004B3C RID: 19260
		[DllImport("libX11")]
		internal static extern int XQueryBestCursor(IntPtr display, IntPtr drawable, int width, int height, out int best_width, out int best_height);

		// Token: 0x06004B3D RID: 19261
		[DllImport("libX11")]
		internal static extern int XQueryExtension(IntPtr display, string extension_name, ref int major, ref int first_event, ref int first_error);

		// Token: 0x06004B3E RID: 19262
		[DllImport("libX11")]
		internal static extern IntPtr XWhitePixel(IntPtr display, int screen_no);

		// Token: 0x06004B3F RID: 19263
		[DllImport("libX11")]
		internal static extern IntPtr XBlackPixel(IntPtr display, int screen_no);

		// Token: 0x06004B40 RID: 19264
		[DllImport("libX11")]
		internal static extern void XGrabServer(IntPtr display);

		// Token: 0x06004B41 RID: 19265
		[DllImport("libX11")]
		internal static extern void XUngrabServer(IntPtr display);

		// Token: 0x06004B42 RID: 19266
		[DllImport("libX11")]
		internal static extern void XGetWMNormalHints(IntPtr display, IntPtr window, ref XSizeHints hints, out IntPtr supplied_return);

		// Token: 0x06004B43 RID: 19267
		[DllImport("libX11")]
		internal static extern void XSetWMNormalHints(IntPtr display, IntPtr window, ref XSizeHints hints);

		// Token: 0x06004B44 RID: 19268
		[DllImport("libX11")]
		internal static extern void XSetZoomHints(IntPtr display, IntPtr window, ref XSizeHints hints);

		// Token: 0x06004B45 RID: 19269
		[DllImport("libX11")]
		internal static extern void XSetWMHints(IntPtr display, IntPtr window, ref XWMHints wmhints);

		// Token: 0x06004B46 RID: 19270
		[DllImport("libX11")]
		internal static extern int XGetIconSizes(IntPtr display, IntPtr window, out IntPtr size_list, out int count);

		// Token: 0x06004B47 RID: 19271
		[DllImport("libX11")]
		internal static extern IntPtr XSetErrorHandler(XErrorHandler error_handler);

		// Token: 0x06004B48 RID: 19272
		[DllImport("libX11")]
		internal static extern IntPtr XGetErrorText(IntPtr display, byte code, StringBuilder buffer, int length);

		// Token: 0x06004B49 RID: 19273
		[DllImport("libX11")]
		internal static extern int XInitThreads();

		// Token: 0x06004B4A RID: 19274
		[DllImport("libX11")]
		internal static extern int XConvertSelection(IntPtr display, IntPtr selection, IntPtr target, IntPtr property, IntPtr requestor, IntPtr time);

		// Token: 0x06004B4B RID: 19275
		[DllImport("libX11")]
		internal static extern IntPtr XGetSelectionOwner(IntPtr display, IntPtr selection);

		// Token: 0x06004B4C RID: 19276
		[DllImport("libX11")]
		internal static extern int XSetSelectionOwner(IntPtr display, IntPtr selection, IntPtr owner, IntPtr time);

		// Token: 0x06004B4D RID: 19277
		[DllImport("libX11")]
		internal static extern int XSetPlaneMask(IntPtr display, IntPtr gc, IntPtr mask);

		// Token: 0x06004B4E RID: 19278
		[DllImport("libX11")]
		internal static extern int XSetForeground(IntPtr display, IntPtr gc, UIntPtr foreground);

		// Token: 0x06004B4F RID: 19279
		[DllImport("libX11")]
		internal static extern int XSetBackground(IntPtr display, IntPtr gc, UIntPtr background);

		// Token: 0x06004B50 RID: 19280
		[DllImport("libX11")]
		internal static extern int XBell(IntPtr display, int percent);

		// Token: 0x06004B51 RID: 19281
		[DllImport("libX11")]
		internal static extern int XChangeActivePointerGrab(IntPtr display, EventMask event_mask, IntPtr cursor, IntPtr time);

		// Token: 0x06004B52 RID: 19282
		[DllImport("libX11")]
		internal static extern bool XFilterEvent(ref XEvent xevent, IntPtr window);

		// Token: 0x06004B53 RID: 19283
		[DllImport("libX11")]
		internal static extern void XkbSetDetectableAutoRepeat(IntPtr display, bool detectable, IntPtr supported);

		// Token: 0x06004B54 RID: 19284
		[DllImport("libX11")]
		internal static extern void XPeekEvent(IntPtr display, ref XEvent xevent);

		// Token: 0x06004B55 RID: 19285
		[DllImport("libX11")]
		internal static extern void XIfEvent(IntPtr display, ref XEvent xevent, Delegate event_predicate, IntPtr arg);

		// Token: 0x040027FB RID: 10235
		private const EventMask SelectInputMask = EventMask.KeyPressMask | EventMask.KeyReleaseMask | EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.EnterWindowMask | EventMask.LeaveWindowMask | EventMask.PointerMotionMask | EventMask.PointerMotionHintMask | EventMask.ExposureMask | EventMask.SubstructureNotifyMask | EventMask.FocusChangeMask;

		// Token: 0x040027FC RID: 10236
		private static volatile XplatUIX11 Instance;

		// Token: 0x040027FD RID: 10237
		private static int RefCount;

		// Token: 0x040027FE RID: 10238
		private static object XlibLock;

		// Token: 0x040027FF RID: 10239
		private static bool themes_enabled;

		// Token: 0x04002800 RID: 10240
		private static IntPtr DisplayHandle;

		// Token: 0x04002801 RID: 10241
		private static int ScreenNo;

		// Token: 0x04002802 RID: 10242
		private static IntPtr DefaultColormap;

		// Token: 0x04002803 RID: 10243
		private static IntPtr CustomVisual;

		// Token: 0x04002804 RID: 10244
		private static IntPtr CustomColormap;

		// Token: 0x04002805 RID: 10245
		private static IntPtr RootWindow;

		// Token: 0x04002806 RID: 10246
		private static IntPtr FosterParent;

		// Token: 0x04002807 RID: 10247
		private static XErrorHandler ErrorHandler;

		// Token: 0x04002808 RID: 10248
		private static bool ErrorExceptions;

		// Token: 0x04002809 RID: 10249
		private int render_major_opcode;

		// Token: 0x0400280A RID: 10250
		private int render_first_event;

		// Token: 0x0400280B RID: 10251
		private int render_first_error;

		// Token: 0x0400280C RID: 10252
		private static IntPtr ClipMagic;

		// Token: 0x0400280D RID: 10253
		private static ClipboardData Clipboard;

		// Token: 0x0400280E RID: 10254
		private static IntPtr PostAtom;

		// Token: 0x0400280F RID: 10255
		private static IntPtr AsyncAtom;

		// Token: 0x04002810 RID: 10256
		private static Hashtable MessageQueues;

		// Token: 0x04002811 RID: 10257
		private static ArrayList unattached_timer_list;

		// Token: 0x04002812 RID: 10258
		private static Pollfd[] pollfds;

		// Token: 0x04002813 RID: 10259
		private static bool wake_waiting;

		// Token: 0x04002814 RID: 10260
		private static object wake_waiting_lock = new object();

		// Token: 0x04002815 RID: 10261
		private static X11Keyboard Keyboard;

		// Token: 0x04002816 RID: 10262
		private static X11Dnd Dnd;

		// Token: 0x04002817 RID: 10263
		private static Socket listen;

		// Token: 0x04002818 RID: 10264
		private static Socket wake;

		// Token: 0x04002819 RID: 10265
		private static Socket wake_receive;

		// Token: 0x0400281A RID: 10266
		private static byte[] network_buffer;

		// Token: 0x0400281B RID: 10267
		private static bool detectable_key_auto_repeat;

		// Token: 0x0400281C RID: 10268
		private static IntPtr ActiveWindow;

		// Token: 0x0400281D RID: 10269
		private static IntPtr FocusWindow;

		// Token: 0x0400281E RID: 10270
		private static Stack ModalWindows;

		// Token: 0x0400281F RID: 10271
		private static IntPtr SystrayMgrWindow;

		// Token: 0x04002820 RID: 10272
		private static IntPtr LastCursorWindow;

		// Token: 0x04002821 RID: 10273
		private static IntPtr LastCursorHandle;

		// Token: 0x04002822 RID: 10274
		private static IntPtr OverrideCursorHandle;

		// Token: 0x04002823 RID: 10275
		private static CaretStruct Caret;

		// Token: 0x04002824 RID: 10276
		private static IntPtr LastPointerWindow;

		// Token: 0x04002825 RID: 10277
		private static IntPtr WM_PROTOCOLS;

		// Token: 0x04002826 RID: 10278
		private static IntPtr WM_DELETE_WINDOW;

		// Token: 0x04002827 RID: 10279
		private static IntPtr WM_TAKE_FOCUS;

		// Token: 0x04002828 RID: 10280
		private static IntPtr _NET_DESKTOP_GEOMETRY;

		// Token: 0x04002829 RID: 10281
		private static IntPtr _NET_CURRENT_DESKTOP;

		// Token: 0x0400282A RID: 10282
		private static IntPtr _NET_ACTIVE_WINDOW;

		// Token: 0x0400282B RID: 10283
		private static IntPtr _NET_WORKAREA;

		// Token: 0x0400282C RID: 10284
		private static IntPtr _NET_WM_NAME;

		// Token: 0x0400282D RID: 10285
		private static IntPtr _NET_WM_WINDOW_TYPE;

		// Token: 0x0400282E RID: 10286
		private static IntPtr _NET_WM_STATE;

		// Token: 0x0400282F RID: 10287
		private static IntPtr _NET_WM_ICON;

		// Token: 0x04002830 RID: 10288
		private static IntPtr _NET_WM_USER_TIME;

		// Token: 0x04002831 RID: 10289
		private static IntPtr _NET_FRAME_EXTENTS;

		// Token: 0x04002832 RID: 10290
		private static IntPtr _NET_SYSTEM_TRAY_S;

		// Token: 0x04002833 RID: 10291
		private static IntPtr _NET_SYSTEM_TRAY_OPCODE;

		// Token: 0x04002834 RID: 10292
		private static IntPtr _NET_WM_STATE_MAXIMIZED_HORZ;

		// Token: 0x04002835 RID: 10293
		private static IntPtr _NET_WM_STATE_MAXIMIZED_VERT;

		// Token: 0x04002836 RID: 10294
		private static IntPtr _XEMBED;

		// Token: 0x04002837 RID: 10295
		private static IntPtr _XEMBED_INFO;

		// Token: 0x04002838 RID: 10296
		private static IntPtr _MOTIF_WM_HINTS;

		// Token: 0x04002839 RID: 10297
		private static IntPtr _NET_WM_STATE_SKIP_TASKBAR;

		// Token: 0x0400283A RID: 10298
		private static IntPtr _NET_WM_STATE_ABOVE;

		// Token: 0x0400283B RID: 10299
		private static IntPtr _NET_WM_STATE_MODAL;

		// Token: 0x0400283C RID: 10300
		private static IntPtr _NET_WM_STATE_HIDDEN;

		// Token: 0x0400283D RID: 10301
		private static IntPtr _NET_WM_CONTEXT_HELP;

		// Token: 0x0400283E RID: 10302
		private static IntPtr _NET_WM_WINDOW_OPACITY;

		// Token: 0x0400283F RID: 10303
		private static IntPtr _NET_WM_WINDOW_TYPE_UTILITY;

		// Token: 0x04002840 RID: 10304
		private static IntPtr _NET_WM_WINDOW_TYPE_NORMAL;

		// Token: 0x04002841 RID: 10305
		private static IntPtr CLIPBOARD;

		// Token: 0x04002842 RID: 10306
		private static IntPtr PRIMARY;

		// Token: 0x04002843 RID: 10307
		private static IntPtr OEMTEXT;

		// Token: 0x04002844 RID: 10308
		private static IntPtr UTF8_STRING;

		// Token: 0x04002845 RID: 10309
		private static IntPtr UTF16_STRING;

		// Token: 0x04002846 RID: 10310
		private static IntPtr RICHTEXTFORMAT;

		// Token: 0x04002847 RID: 10311
		private static IntPtr TARGETS;

		// Token: 0x04002848 RID: 10312
		private static HoverStruct HoverState;

		// Token: 0x04002849 RID: 10313
		private static ClickStruct ClickPending;

		// Token: 0x0400284A RID: 10314
		private static GrabStruct Grab;

		// Token: 0x0400284B RID: 10315
		private Point mouse_position;

		// Token: 0x0400284C RID: 10316
		internal static MouseButtons MouseState;

		// Token: 0x0400284D RID: 10317
		internal static bool in_doevents;

		// Token: 0x0400284E RID: 10318
		private static int DoubleClickInterval;

		// Token: 0x0400284F RID: 10319
		private static readonly object lockobj = new object();

		// Token: 0x04002850 RID: 10320
		private static Hashtable messageHold;

		// Token: 0x0200049B RID: 1179
		internal class XException : ApplicationException
		{
			// Token: 0x06004B56 RID: 19286 RVA: 0x001283EC File Offset: 0x001265EC
			public XException(IntPtr Display, IntPtr ResourceID, IntPtr Serial, byte ErrorCode, XRequest RequestCode, byte MinorCode)
			{
				this.Display = Display;
				this.ResourceID = ResourceID;
				this.Serial = Serial;
				this.RequestCode = RequestCode;
				this.ErrorCode = ErrorCode;
				this.MinorCode = MinorCode;
			}

			// Token: 0x17001308 RID: 4872
			// (get) Token: 0x06004B57 RID: 19287 RVA: 0x00128424 File Offset: 0x00126624
			public override string Message
			{
				get
				{
					return XplatUIX11.XException.GetMessage(this.Display, this.ResourceID, this.Serial, this.ErrorCode, this.RequestCode, this.MinorCode);
				}
			}

			// Token: 0x06004B58 RID: 19288 RVA: 0x00128450 File Offset: 0x00126650
			public static string GetMessage(IntPtr Display, IntPtr ResourceID, IntPtr Serial, byte ErrorCode, XRequest RequestCode, byte MinorCode)
			{
				StringBuilder stringBuilder = new StringBuilder(160);
				XplatUIX11.XGetErrorText(Display, ErrorCode, stringBuilder, stringBuilder.Capacity);
				string text = stringBuilder.ToString();
				Hwnd hwnd = Hwnd.ObjectFromHandle(ResourceID);
				string text2;
				string text3;
				if (hwnd != null)
				{
					text2 = hwnd.ToString();
					Control control = Control.FromHandle(hwnd.Handle);
					if (control != null)
					{
						text3 = control.ToString();
					}
					else
					{
						text3 = string.Format("<handle {0:X} non-existant>", hwnd.Handle.ToInt32());
					}
				}
				else
				{
					text2 = "<null>";
					text3 = "<null>";
				}
				return string.Format("\n  Error: {0}\n  Request:     {1:D} ({2})\n  Resource ID: 0x{3:X}\n  Serial:      {4}\n  Hwnd:        {5}\n  Control:     {6}", new object[]
				{
					text,
					RequestCode,
					MinorCode,
					ResourceID.ToInt32(),
					Serial,
					text2,
					text3
				});
			}

			// Token: 0x04002852 RID: 10322
			private IntPtr Display;

			// Token: 0x04002853 RID: 10323
			private IntPtr ResourceID;

			// Token: 0x04002854 RID: 10324
			private IntPtr Serial;

			// Token: 0x04002855 RID: 10325
			private XRequest RequestCode;

			// Token: 0x04002856 RID: 10326
			private byte ErrorCode;

			// Token: 0x04002857 RID: 10327
			private byte MinorCode;
		}

		// Token: 0x0200064F RID: 1615
		// (Invoke) Token: 0x060050EE RID: 20718
		private delegate bool EventPredicate(IntPtr display, ref XEvent xevent, IntPtr arg);

		// Token: 0x02000650 RID: 1616
		// (Invoke) Token: 0x060050F2 RID: 20722
		private delegate IntPtr WndProcDelegate(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam);
	}
}
