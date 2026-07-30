using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020001BF RID: 447
	internal class Hwnd : IDisposable
	{
		// Token: 0x06001D71 RID: 7537 RVA: 0x0006F820 File Offset: 0x0006DA20
		public Hwnd()
		{
			this.x = 0;
			this.y = 0;
			this.width = 0;
			this.height = 0;
			this.visible = false;
			this.menu = null;
			this.border_style = FormBorderStyle.None;
			this.client_window = IntPtr.Zero;
			this.whole_window = IntPtr.Zero;
			this.cursor = IntPtr.Zero;
			this.handle = IntPtr.Zero;
			this.parent = null;
			this.invalid_list = new ArrayList();
			this.expose_pending = false;
			this.nc_expose_pending = false;
			this.enabled = true;
			this.reparented = false;
			this.client_rectangle = Rectangle.Empty;
			this.marshal_free_list = new ArrayList(2);
			this.opacity = uint.MaxValue;
			this.fixed_size = false;
			this.drawing_stack = new Stack();
			this.children = new ArrayList();
			this.resizing_or_moving = false;
			this.whacky_wm = false;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0006F964 File Offset: 0x0006DB64
		public void Dispose()
		{
			this.expose_pending = false;
			this.nc_expose_pending = false;
			this.Parent = null;
			Hashtable hashtable = Hwnd.windows;
			lock (hashtable)
			{
				Hwnd.windows.Remove(this.client_window);
				Hwnd.windows.Remove(this.whole_window);
			}
			this.client_window = IntPtr.Zero;
			this.whole_window = IntPtr.Zero;
			this.zombie = false;
			for (int i = 0; i < this.marshal_free_list.Count; i++)
			{
				Marshal.FreeHGlobal((IntPtr)this.marshal_free_list[i]);
			}
			this.marshal_free_list.Clear();
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0006FA40 File Offset: 0x0006DC40
		public static Hwnd ObjectFromWindow(IntPtr window)
		{
			Hashtable hashtable = Hwnd.windows;
			Hwnd hwnd;
			lock (hashtable)
			{
				hwnd = (Hwnd)Hwnd.windows[window];
			}
			return hwnd;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0006FA98 File Offset: 0x0006DC98
		public static Hwnd ObjectFromHandle(IntPtr handle)
		{
			Hashtable hashtable = Hwnd.windows;
			Hwnd hwnd;
			lock (hashtable)
			{
				hwnd = (Hwnd)Hwnd.windows[handle];
			}
			return hwnd;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0006FAF0 File Offset: 0x0006DCF0
		public static IntPtr HandleFromObject(Hwnd obj)
		{
			return obj.handle;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0006FAF8 File Offset: 0x0006DCF8
		public static Hwnd GetObjectFromWindow(IntPtr window)
		{
			Hashtable hashtable = Hwnd.windows;
			Hwnd hwnd;
			lock (hashtable)
			{
				hwnd = (Hwnd)Hwnd.windows[window];
			}
			return hwnd;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0006FB50 File Offset: 0x0006DD50
		public static IntPtr GetHandleFromWindow(IntPtr window)
		{
			Hashtable hashtable = Hwnd.windows;
			Hwnd hwnd;
			lock (hashtable)
			{
				hwnd = (Hwnd)Hwnd.windows[window];
			}
			if (hwnd != null)
			{
				return hwnd.handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0006FBBC File Offset: 0x0006DDBC
		public static Hwnd.Borders GetBorderWidth(CreateParams cp)
		{
			Hwnd.Borders borders = default(Hwnd.Borders);
			Size borderSize = ThemeEngine.Current.BorderSize;
			Size borderStaticSize = ThemeEngine.Current.BorderStaticSize;
			Size border3DSize = ThemeEngine.Current.Border3DSize;
			Size size;
			size..ctor(2 + borderSize.Width, 2 + borderSize.Height);
			Size borderSizableSize = ThemeEngine.Current.BorderSizableSize;
			if (cp.IsSet(WindowStyles.WS_CAPTION))
			{
				borders.Inflate(borderSizableSize);
			}
			else if (cp.IsSet(WindowStyles.WS_BORDER))
			{
				if (cp.IsSet(WindowExStyles.WS_EX_DLGMODALFRAME))
				{
					if (cp.IsSet(WindowStyles.WS_THICKFRAME) && (cp.IsSet(WindowExStyles.WS_EX_STATICEDGE) || cp.IsSet(WindowExStyles.WS_EX_CLIENTEDGE)))
					{
						borders.Inflate(borderStaticSize);
					}
				}
				else
				{
					borders.Inflate(borderStaticSize);
				}
			}
			else if (cp.IsSet(WindowStyles.WS_DLGFRAME))
			{
				borders.Inflate(borderSizableSize);
			}
			if (cp.IsSet(WindowStyles.WS_THICKFRAME))
			{
				if (cp.IsSet(WindowStyles.WS_DLGFRAME))
				{
					borders.Inflate(borderStaticSize);
				}
				else
				{
					borders.Inflate(size);
				}
			}
			Size size2 = Size.Empty;
			bool flag = cp.IsSet(WindowStyles.WS_THICKFRAME) || cp.IsSet(WindowStyles.WS_DLGFRAME);
			if (flag && cp.IsSet(WindowStyles.WS_THICKFRAME) && !cp.IsSet(WindowStyles.WS_BORDER) && !cp.IsSet(WindowStyles.WS_DLGFRAME))
			{
				size2 = borderStaticSize;
			}
			if (cp.IsSet(WindowExStyles.WS_EX_DLGMODALFRAME | WindowExStyles.WS_EX_CLIENTEDGE))
			{
				borders.Inflate(border3DSize + ((!flag) ? borderSizableSize : size2));
			}
			else if (cp.IsSet(WindowExStyles.WS_EX_DLGMODALFRAME | WindowExStyles.WS_EX_STATICEDGE))
			{
				borders.Inflate((!flag) ? borderSizableSize : size2);
			}
			else if (cp.IsSet(WindowExStyles.WS_EX_CLIENTEDGE | WindowExStyles.WS_EX_STATICEDGE))
			{
				borders.Inflate(borderStaticSize + ((!flag) ? border3DSize : Size.Empty));
			}
			else
			{
				if (cp.IsSet(WindowExStyles.WS_EX_CLIENTEDGE))
				{
					borders.Inflate(border3DSize);
				}
				if (cp.IsSet(WindowExStyles.WS_EX_DLGMODALFRAME) && !cp.IsSet(WindowStyles.WS_DLGFRAME))
				{
					borders.Inflate((!cp.IsSet(WindowStyles.WS_THICKFRAME)) ? borderSizableSize : borderStaticSize);
				}
				if (cp.IsSet(WindowExStyles.WS_EX_STATICEDGE))
				{
					if (cp.IsSet(WindowStyles.WS_THICKFRAME) || cp.IsSet(WindowStyles.WS_DLGFRAME))
					{
						borders.Inflate(new Size(-borderStaticSize.Width, -borderStaticSize.Height));
					}
					else
					{
						borders.Inflate(borderStaticSize);
					}
				}
			}
			return borders;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0006FE8C File Offset: 0x0006E08C
		public static Rectangle GetWindowRectangle(CreateParams cp, Menu menu)
		{
			return Hwnd.GetWindowRectangle(cp, menu, Rectangle.Empty);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0006FE9C File Offset: 0x0006E09C
		public static Rectangle GetWindowRectangle(CreateParams cp, Menu menu, Rectangle client_rect)
		{
			Hwnd.Borders borders = Hwnd.GetBorders(cp, menu);
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, client_rect.Size);
			rectangle.Y -= borders.top;
			rectangle.Height += borders.top + borders.bottom;
			rectangle.X -= borders.left;
			rectangle.Width += borders.left + borders.right;
			return rectangle;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0006FF2C File Offset: 0x0006E12C
		public Rectangle GetClientRectangle(int width, int height)
		{
			return Hwnd.GetClientRectangle(new CreateParams
			{
				WindowStyle = this.initial_style,
				WindowExStyle = this.initial_ex_style
			}, this.menu, width, height);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0006FF68 File Offset: 0x0006E168
		public ArrayList GetClippingRectangles()
		{
			ArrayList arrayList = new ArrayList();
			if (this.x < 0)
			{
				arrayList.Add(new Rectangle(0, 0, this.x * -1, this.Height));
				if (this.y < 0)
				{
					arrayList.Add(new Rectangle(this.x * -1, 0, this.Width, this.y * -1));
				}
			}
			else if (this.y < 0)
			{
				arrayList.Add(new Rectangle(0, 0, this.Width, this.y * -1));
			}
			foreach (object obj in this.children)
			{
				Hwnd hwnd = (Hwnd)obj;
				if (hwnd.visible)
				{
					arrayList.Add(new Rectangle(hwnd.X, hwnd.Y, hwnd.Width, hwnd.Height));
				}
			}
			if (this.parent == null)
			{
				return arrayList;
			}
			ArrayList arrayList2 = this.parent.children;
			foreach (object obj2 in arrayList2)
			{
				Hwnd hwnd2 = (Hwnd)obj2;
				IntPtr previousWindow = this.whole_window;
				if (hwnd2 != this)
				{
					do
					{
						previousWindow = XplatUI.GetPreviousWindow(previousWindow);
						if (previousWindow == hwnd2.WholeWindow && hwnd2.visible)
						{
							Rectangle rectangle = Rectangle.Intersect(new Rectangle(this.X, this.Y, this.Width, this.Height), new Rectangle(hwnd2.X, hwnd2.Y, hwnd2.Width, hwnd2.Height));
							if (!(rectangle == Rectangle.Empty))
							{
								rectangle.X -= this.X;
								rectangle.Y -= this.Y;
								arrayList.Add(rectangle);
							}
						}
					}
					while (previousWindow != IntPtr.Zero);
				}
			}
			return arrayList;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000701F4 File Offset: 0x0006E3F4
		public static Hwnd.Borders GetBorders(CreateParams cp, Menu menu)
		{
			Hwnd.Borders borders = default(Hwnd.Borders);
			if (menu != null)
			{
				int num = menu.Rect.Height;
				if (num == 0)
				{
					num = ThemeEngine.Current.CalcMenuBarSize(Hwnd.GraphicsContext, menu, cp.Width);
				}
				borders.top += num;
			}
			if (cp.IsSet(WindowStyles.WS_CAPTION))
			{
				int num2;
				if (cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
				{
					num2 = ThemeEngine.Current.ToolWindowCaptionHeight;
				}
				else
				{
					num2 = ThemeEngine.Current.CaptionHeight;
				}
				borders.top += num2;
			}
			Hwnd.Borders borderWidth = Hwnd.GetBorderWidth(cp);
			borders.left += borderWidth.left;
			borders.right += borderWidth.right;
			borders.top += borderWidth.top;
			borders.bottom += borderWidth.bottom;
			return borders;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000702F4 File Offset: 0x0006E4F4
		public static Rectangle GetClientRectangle(CreateParams cp, Menu menu, int width, int height)
		{
			Hwnd.Borders borders = Hwnd.GetBorders(cp, menu);
			Rectangle rectangle;
			rectangle..ctor(0, 0, width, height);
			rectangle.Y += borders.top;
			rectangle.Height -= borders.top + borders.bottom;
			rectangle.X += borders.left;
			rectangle.Width -= borders.left + borders.right;
			return rectangle;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x0007037C File Offset: 0x0006E57C
		public static Graphics GraphicsContext
		{
			get
			{
				if (Hwnd.bmp_g == null)
				{
					Hwnd.bmp = new Bitmap(1, 1, 2498570);
					Hwnd.bmp_g = Graphics.FromImage(Hwnd.bmp);
				}
				return Hwnd.bmp_g;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x000703B0 File Offset: 0x0006E5B0
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x000703B8 File Offset: 0x0006E5B8
		public FormBorderStyle BorderStyle
		{
			get
			{
				return this.border_style;
			}
			set
			{
				this.border_style = value;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x000703C4 File Offset: 0x0006E5C4
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x000703F4 File Offset: 0x0006E5F4
		public Rectangle ClientRect
		{
			get
			{
				if (this.client_rectangle == Rectangle.Empty)
				{
					return this.DefaultClientRect;
				}
				return this.client_rectangle;
			}
			set
			{
				this.client_rectangle = value;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x00070400 File Offset: 0x0006E600
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x00070408 File Offset: 0x0006E608
		public IntPtr Cursor
		{
			get
			{
				return this.cursor;
			}
			set
			{
				this.cursor = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x00070414 File Offset: 0x0006E614
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0007041C File Offset: 0x0006E61C
		public IntPtr ClientWindow
		{
			get
			{
				return this.client_window;
			}
			set
			{
				this.client_window = value;
				this.handle = value;
				this.zombie = false;
				if (this.client_window != IntPtr.Zero)
				{
					Hashtable hashtable = Hwnd.windows;
					lock (hashtable)
					{
						if (Hwnd.windows[this.client_window] == null)
						{
							Hwnd.windows[this.client_window] = this;
						}
					}
				}
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x000704B8 File Offset: 0x0006E6B8
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x000704C0 File Offset: 0x0006E6C0
		public Region UserClip
		{
			get
			{
				return this.user_clip;
			}
			set
			{
				this.user_clip = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x000704CC File Offset: 0x0006E6CC
		public Rectangle DefaultClientRect
		{
			get
			{
				return Hwnd.GetClientRectangle(new CreateParams
				{
					WindowStyle = this.initial_style,
					WindowExStyle = this.initial_ex_style
				}, null, this.width, this.height);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x0007050C File Offset: 0x0006E70C
		public bool ExposePending
		{
			get
			{
				return this.expose_pending;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00070514 File Offset: 0x0006E714
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("Handle", "Handle is not yet assigned, need a ClientWindow");
				}
				return this.handle;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x00070544 File Offset: 0x0006E744
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0007054C File Offset: 0x0006E74C
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x00070558 File Offset: 0x0006E758
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x00070560 File Offset: 0x0006E760
		public Menu Menu
		{
			get
			{
				return this.menu;
			}
			set
			{
				this.menu = value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0007056C File Offset: 0x0006E76C
		// (set) Token: 0x06001D93 RID: 7571 RVA: 0x00070574 File Offset: 0x0006E774
		public bool Reparented
		{
			get
			{
				return this.reparented;
			}
			set
			{
				this.reparented = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x00070580 File Offset: 0x0006E780
		// (set) Token: 0x06001D95 RID: 7573 RVA: 0x00070588 File Offset: 0x0006E788
		public uint Opacity
		{
			get
			{
				return this.opacity;
			}
			set
			{
				this.opacity = value;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x00070594 File Offset: 0x0006E794
		// (set) Token: 0x06001D97 RID: 7575 RVA: 0x0007059C File Offset: 0x0006E79C
		public XEventQueue Queue
		{
			get
			{
				return this.queue;
			}
			set
			{
				this.queue = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x000705A8 File Offset: 0x0006E7A8
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x000705D0 File Offset: 0x0006E7D0
		public bool Enabled
		{
			get
			{
				return this.enabled && (this.parent == null || this.parent.Enabled);
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x000705DC File Offset: 0x0006E7DC
		public IntPtr EnabledHwnd
		{
			get
			{
				if (this.Enabled || this.parent == null)
				{
					return this.Handle;
				}
				return this.parent.EnabledHwnd;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00070614 File Offset: 0x0006E814
		public Point MenuOrigin
		{
			get
			{
				Form form = Control.FromHandle(this.handle) as Form;
				if (form != null && form.window_manager != null)
				{
					return form.window_manager.GetMenuOrigin();
				}
				Size border3DSize = ThemeEngine.Current.Border3DSize;
				Point point;
				point..ctor(0, 0);
				if (this.border_style == FormBorderStyle.Fixed3D)
				{
					point.X += border3DSize.Width;
					point.Y += border3DSize.Height;
				}
				else if (this.border_style == FormBorderStyle.FixedSingle)
				{
					point.X++;
					point.Y++;
				}
				if (this.title_style == TitleStyle.Normal)
				{
					point.Y += this.caption_height;
				}
				else if (this.title_style == TitleStyle.Normal)
				{
					point.Y += this.tool_caption_height;
				}
				return point;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x00070710 File Offset: 0x0006E910
		public Rectangle Invalid
		{
			get
			{
				if (this.invalid_list.Count == 0)
				{
					return Rectangle.Empty;
				}
				Rectangle rectangle = (Rectangle)this.invalid_list[0];
				for (int i = 1; i < this.invalid_list.Count; i++)
				{
					rectangle = Rectangle.Union(rectangle, (Rectangle)this.invalid_list[i]);
				}
				return rectangle;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0007077C File Offset: 0x0006E97C
		public Rectangle[] ClipRectangles
		{
			get
			{
				return (Rectangle[])this.invalid_list.ToArray(typeof(Rectangle));
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x00070798 File Offset: 0x0006E998
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x000707A0 File Offset: 0x0006E9A0
		public Rectangle NCInvalid
		{
			get
			{
				return this.nc_invalid;
			}
			set
			{
				this.nc_invalid = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x000707AC File Offset: 0x0006E9AC
		public bool NCExposePending
		{
			get
			{
				return this.nc_expose_pending;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000707B4 File Offset: 0x0006E9B4
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x000707BC File Offset: 0x0006E9BC
		public Hwnd Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != null)
				{
					this.parent.children.Remove(this);
				}
				this.parent = value;
				if (this.parent != null)
				{
					this.parent.children.Add(this);
				}
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0007080C File Offset: 0x0006EA0C
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x00070834 File Offset: 0x0006EA34
		public bool Mapped
		{
			get
			{
				return this.mapped && (this.parent == null || this.parent.Mapped);
			}
			set
			{
				this.mapped = value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x00070840 File Offset: 0x0006EA40
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x00070848 File Offset: 0x0006EA48
		public int CaptionHeight
		{
			get
			{
				return this.caption_height;
			}
			set
			{
				this.caption_height = value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00070854 File Offset: 0x0006EA54
		// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x0007085C File Offset: 0x0006EA5C
		public int ToolCaptionHeight
		{
			get
			{
				return this.tool_caption_height;
			}
			set
			{
				this.tool_caption_height = value;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x00070868 File Offset: 0x0006EA68
		// (set) Token: 0x06001DAA RID: 7594 RVA: 0x00070870 File Offset: 0x0006EA70
		public TitleStyle TitleStyle
		{
			get
			{
				return this.title_style;
			}
			set
			{
				this.title_style = value;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0007087C File Offset: 0x0006EA7C
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x00070884 File Offset: 0x0006EA84
		public object UserData
		{
			get
			{
				return this.user_data;
			}
			set
			{
				this.user_data = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00070890 File Offset: 0x0006EA90
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x00070898 File Offset: 0x0006EA98
		public IntPtr WholeWindow
		{
			get
			{
				return this.whole_window;
			}
			set
			{
				this.whole_window = value;
				this.zombie = false;
				if (this.whole_window != IntPtr.Zero)
				{
					Hashtable hashtable = Hwnd.windows;
					lock (hashtable)
					{
						if (Hwnd.windows[this.whole_window] == null)
						{
							Hwnd.windows[this.whole_window] = this;
						}
					}
				}
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0007092C File Offset: 0x0006EB2C
		// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x00070934 File Offset: 0x0006EB34
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00070940 File Offset: 0x0006EB40
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x00070948 File Offset: 0x0006EB48
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x00070954 File Offset: 0x0006EB54
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0007095C File Offset: 0x0006EB5C
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00070968 File Offset: 0x0006EB68
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x00070970 File Offset: 0x0006EB70
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0007097C File Offset: 0x0006EB7C
		public void AddInvalidArea(int x, int y, int width, int height)
		{
			this.AddInvalidArea(new Rectangle(x, y, width, height));
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00070990 File Offset: 0x0006EB90
		public void AddInvalidArea(Rectangle rect)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.invalid_list)
			{
				Rectangle rectangle = (Rectangle)obj;
				if (!rect.Contains(rectangle))
				{
					arrayList.Add(rectangle);
				}
			}
			arrayList.Add(rect);
			this.invalid_list = arrayList;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00070A2C File Offset: 0x0006EC2C
		public void ClearInvalidArea()
		{
			this.invalid_list.Clear();
			this.expose_pending = false;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00070A40 File Offset: 0x0006EC40
		public void AddNcInvalidArea(int x, int y, int width, int height)
		{
			if (this.nc_invalid == Rectangle.Empty)
			{
				this.nc_invalid = new Rectangle(x, y, width, height);
				return;
			}
			int num = Math.Max(this.nc_invalid.Right, x + width);
			int num2 = Math.Max(this.nc_invalid.Bottom, y + height);
			this.nc_invalid.X = Math.Min(this.nc_invalid.X, x);
			this.nc_invalid.Y = Math.Min(this.nc_invalid.Y, y);
			this.nc_invalid.Width = num - this.nc_invalid.X;
			this.nc_invalid.Height = num2 - this.nc_invalid.Y;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00070B04 File Offset: 0x0006ED04
		public void AddNcInvalidArea(Rectangle rect)
		{
			if (this.nc_invalid == Rectangle.Empty)
			{
				this.nc_invalid = rect;
				return;
			}
			this.nc_invalid = Rectangle.Union(this.nc_invalid, rect);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00070B38 File Offset: 0x0006ED38
		public void ClearNcInvalidArea()
		{
			this.nc_invalid = Rectangle.Empty;
			this.nc_expose_pending = false;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00070B4C File Offset: 0x0006ED4C
		public override string ToString()
		{
			return string.Format("Hwnd, Mapped:{3} ClientWindow:0x{0:X}, WholeWindow:0x{1:X}, Zombie={4}, Parent:[{2:X}]", new object[]
			{
				this.client_window.ToInt32(),
				this.whole_window.ToInt32(),
				(this.parent == null) ? "<null>" : this.parent.ToString(),
				this.Mapped,
				this.zombie
			});
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00070BD0 File Offset: 0x0006EDD0
		public static Point GetNextStackedFormLocation(CreateParams cp, Hwnd parent_hwnd)
		{
			if (cp.control == null)
			{
				return Point.Empty;
			}
			int num = cp.X;
			int num2 = cp.Y;
			Point point;
			Rectangle rectangle;
			if (parent_hwnd != null)
			{
				Control control = cp.control.Parent;
				point = parent_hwnd.previous_child_startup_location;
				if (parent_hwnd.client_rectangle == Rectangle.Empty && control != null)
				{
					rectangle = control.ClientRectangle;
				}
				else
				{
					rectangle = parent_hwnd.client_rectangle;
				}
			}
			else
			{
				point = Hwnd.previous_main_startup_location;
				rectangle = Screen.PrimaryScreen.WorkingArea;
			}
			Point point2;
			if (point.X == -2147483648 || point.Y == -2147483648)
			{
				point2 = Point.Empty;
			}
			else
			{
				point2..ctor(point.X + 22, point.Y + 22);
			}
			if (!rectangle.Contains(point2.X * 3, point2.Y * 3))
			{
				point2 = Point.Empty;
			}
			if (point2 == Point.Empty && cp.Parent == IntPtr.Zero)
			{
				point2..ctor(22, 22);
			}
			if (parent_hwnd != null)
			{
				parent_hwnd.previous_child_startup_location = point2;
			}
			else
			{
				Hwnd.previous_main_startup_location = point2;
			}
			if (num == -2147483648 && num2 == -2147483648)
			{
				num = point2.X;
				num2 = point2.Y;
			}
			return new Point(num, num2);
		}

		// Token: 0x04000F94 RID: 3988
		private static Hashtable windows = new Hashtable(100, 0.5f);

		// Token: 0x04000F95 RID: 3989
		private IntPtr handle;

		// Token: 0x04000F96 RID: 3990
		internal IntPtr client_window;

		// Token: 0x04000F97 RID: 3991
		internal IntPtr whole_window;

		// Token: 0x04000F98 RID: 3992
		internal IntPtr cursor;

		// Token: 0x04000F99 RID: 3993
		internal Menu menu;

		// Token: 0x04000F9A RID: 3994
		internal TitleStyle title_style;

		// Token: 0x04000F9B RID: 3995
		internal FormBorderStyle border_style;

		// Token: 0x04000F9C RID: 3996
		internal bool border_static;

		// Token: 0x04000F9D RID: 3997
		internal int x;

		// Token: 0x04000F9E RID: 3998
		internal int y;

		// Token: 0x04000F9F RID: 3999
		internal int width;

		// Token: 0x04000FA0 RID: 4000
		internal int height;

		// Token: 0x04000FA1 RID: 4001
		internal bool allow_drop;

		// Token: 0x04000FA2 RID: 4002
		internal Hwnd parent;

		// Token: 0x04000FA3 RID: 4003
		internal bool visible;

		// Token: 0x04000FA4 RID: 4004
		internal bool mapped;

		// Token: 0x04000FA5 RID: 4005
		internal uint opacity;

		// Token: 0x04000FA6 RID: 4006
		internal bool enabled;

		// Token: 0x04000FA7 RID: 4007
		internal bool zero_sized;

		// Token: 0x04000FA8 RID: 4008
		internal ArrayList invalid_list;

		// Token: 0x04000FA9 RID: 4009
		internal Rectangle nc_invalid;

		// Token: 0x04000FAA RID: 4010
		internal bool expose_pending;

		// Token: 0x04000FAB RID: 4011
		internal bool nc_expose_pending;

		// Token: 0x04000FAC RID: 4012
		internal bool configure_pending;

		// Token: 0x04000FAD RID: 4013
		internal bool resizing_or_moving;

		// Token: 0x04000FAE RID: 4014
		internal bool reparented;

		// Token: 0x04000FAF RID: 4015
		internal Stack drawing_stack;

		// Token: 0x04000FB0 RID: 4016
		internal object user_data;

		// Token: 0x04000FB1 RID: 4017
		internal Rectangle client_rectangle;

		// Token: 0x04000FB2 RID: 4018
		internal ArrayList marshal_free_list;

		// Token: 0x04000FB3 RID: 4019
		internal int caption_height;

		// Token: 0x04000FB4 RID: 4020
		internal int tool_caption_height;

		// Token: 0x04000FB5 RID: 4021
		internal bool whacky_wm;

		// Token: 0x04000FB6 RID: 4022
		internal bool fixed_size;

		// Token: 0x04000FB7 RID: 4023
		internal bool zombie;

		// Token: 0x04000FB8 RID: 4024
		internal Region user_clip;

		// Token: 0x04000FB9 RID: 4025
		internal XEventQueue queue;

		// Token: 0x04000FBA RID: 4026
		internal WindowExStyles initial_ex_style;

		// Token: 0x04000FBB RID: 4027
		internal WindowStyles initial_style;

		// Token: 0x04000FBC RID: 4028
		internal FormWindowState cached_window_state = (FormWindowState)(-1);

		// Token: 0x04000FBD RID: 4029
		internal Point previous_child_startup_location = new Point(int.MinValue, int.MinValue);

		// Token: 0x04000FBE RID: 4030
		internal static Point previous_main_startup_location = new Point(int.MinValue, int.MinValue);

		// Token: 0x04000FBF RID: 4031
		internal ArrayList children;

		// Token: 0x04000FC0 RID: 4032
		[ThreadStatic]
		private static Bitmap bmp;

		// Token: 0x04000FC1 RID: 4033
		[ThreadStatic]
		private static Graphics bmp_g;

		// Token: 0x04000FC2 RID: 4034
		internal object configure_lock = new object();

		// Token: 0x04000FC3 RID: 4035
		internal object expose_lock = new object();

		// Token: 0x020001C0 RID: 448
		internal struct Borders
		{
			// Token: 0x06001DBF RID: 7615 RVA: 0x00070D40 File Offset: 0x0006EF40
			public void Inflate(Size size)
			{
				this.left += size.Width;
				this.right += size.Width;
				this.top += size.Height;
				this.bottom += size.Height;
			}

			// Token: 0x06001DC0 RID: 7616 RVA: 0x00070DA0 File Offset: 0x0006EFA0
			public override string ToString()
			{
				return string.Format("{{top={0}, bottom={1}, left={2}, right={3}}}", new object[] { this.top, this.bottom, this.left, this.right });
			}

			// Token: 0x06001DC1 RID: 7617 RVA: 0x00070DF8 File Offset: 0x0006EFF8
			public override bool Equals(object obj)
			{
				return base.Equals(obj);
			}

			// Token: 0x06001DC2 RID: 7618 RVA: 0x00070E0C File Offset: 0x0006F00C
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06001DC3 RID: 7619 RVA: 0x00070E20 File Offset: 0x0006F020
			public static bool operator ==(Hwnd.Borders a, Hwnd.Borders b)
			{
				return a.left == b.left && a.right == b.right && a.top == b.top && a.bottom == b.bottom;
			}

			// Token: 0x06001DC4 RID: 7620 RVA: 0x00070E7C File Offset: 0x0006F07C
			public static bool operator !=(Hwnd.Borders a, Hwnd.Borders b)
			{
				return !(a == b);
			}

			// Token: 0x04000FC4 RID: 4036
			public int top;

			// Token: 0x04000FC5 RID: 4037
			public int bottom;

			// Token: 0x04000FC6 RID: 4038
			public int left;

			// Token: 0x04000FC7 RID: 4039
			public int right;
		}
	}
}
