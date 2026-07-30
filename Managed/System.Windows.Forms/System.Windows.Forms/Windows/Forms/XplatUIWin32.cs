using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000468 RID: 1128
	internal class XplatUIWin32 : XplatUIDriver
	{
		// Token: 0x060048BE RID: 18622 RVA: 0x00119D48 File Offset: 0x00117F48
		private XplatUIWin32()
		{
			XplatUIWin32.ref_count = 0;
			XplatUIWin32.mouse_state = MouseButtons.None;
			XplatUIWin32.mouse_position = Point.Empty;
			XplatUIWin32.grab_confined = false;
			XplatUIWin32.grab_area = Rectangle.Empty;
			XplatUIWin32.message_queue = new Queue();
			XplatUIWin32.themes_enabled = false;
			XplatUIWin32.wnd_proc = new XplatUIDriver.WndProc(this.InternalWndProc);
			XplatUIWin32.FosterParent = XplatUIWin32.Win32CreateWindow(WindowExStyles.WS_EX_TOOLWINDOW, "static", "Foster Parent Window", WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (XplatUIWin32.FosterParent == IntPtr.Zero)
			{
				XplatUIWin32.Win32MessageBox(IntPtr.Zero, "Could not create foster window, win32 error " + XplatUIWin32.Win32GetLastError().ToString(), "Oops", 0U);
			}
			XplatUIWin32.scroll_height = XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYHSCROLL);
			XplatUIWin32.scroll_width = XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXVSCROLL);
			this.timer_list = new Hashtable();
			this.registered_classes = new Hashtable();
		}

		// Token: 0x14000471 RID: 1137
		// (add) Token: 0x060048C0 RID: 18624 RVA: 0x00119E54 File Offset: 0x00118054
		// (remove) Token: 0x060048C1 RID: 18625 RVA: 0x00119E70 File Offset: 0x00118070
		internal override event EventHandler Idle;

		// Token: 0x060048C2 RID: 18626 RVA: 0x00119E8C File Offset: 0x0011808C
		private string RegisterWindowClass(int classStyle)
		{
			Hashtable hashtable = this.registered_classes;
			string text;
			lock (hashtable)
			{
				text = (string)this.registered_classes[classStyle];
				if (text != null)
				{
					return text;
				}
				text = string.Format("Mono.WinForms.{0}.{1}", Thread.GetDomainID().ToString(), classStyle);
				XplatUIWin32.WNDCLASS wndclass;
				wndclass.style = classStyle;
				wndclass.lpfnWndProc = XplatUIWin32.wnd_proc;
				wndclass.cbClsExtra = 0;
				wndclass.cbWndExtra = 0;
				wndclass.hbrBackground = (IntPtr)6;
				wndclass.hCursor = XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
				wndclass.hIcon = IntPtr.Zero;
				wndclass.hInstance = IntPtr.Zero;
				wndclass.lpszClassName = text;
				wndclass.lpszMenuName = string.Empty;
				if (!XplatUIWin32.Win32RegisterClass(ref wndclass))
				{
					Console.WriteLine("Oops: Could not register the window class, win32 error {0}", XplatUIWin32.Win32GetLastError().ToString());
				}
				this.registered_classes[classStyle] = text;
			}
			return text;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x00119FC4 File Offset: 0x001181C4
		private static bool RetrieveMessage(ref MSG msg)
		{
			if (XplatUIWin32.message_queue.Count == 0)
			{
				return false;
			}
			MSG msg2 = (MSG)XplatUIWin32.message_queue.Dequeue();
			msg = msg2;
			return true;
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x00119FFC File Offset: 0x001181FC
		private static bool StoreMessage(ref MSG msg)
		{
			MSG msg2 = default(MSG);
			msg2 = msg;
			XplatUIWin32.message_queue.Enqueue(msg2);
			return true;
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x0011A02C File Offset: 0x0011822C
		internal static string AnsiToString(IntPtr ansi_data)
		{
			return Marshal.PtrToStringAnsi(ansi_data);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x0011A034 File Offset: 0x00118234
		internal static string UnicodeToString(IntPtr unicode_data)
		{
			return Marshal.PtrToStringUni(unicode_data);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x0011A03C File Offset: 0x0011823C
		internal static Image DIBtoImage(IntPtr dib_data)
		{
			BITMAPINFOHEADER bitmapinfoheader = (BITMAPINFOHEADER)Marshal.PtrToStructure(dib_data, typeof(BITMAPINFOHEADER));
			int num = (int)bitmapinfoheader.biClrUsed;
			if (num == 0 && bitmapinfoheader.biBitCount < 24)
			{
				num = 1 << (int)bitmapinfoheader.biBitCount;
			}
			if (bitmapinfoheader.biSizeImage == 0U)
			{
				int num2 = (((bitmapinfoheader.biWidth * (int)bitmapinfoheader.biBitCount + 31) & -32) >> 3) * bitmapinfoheader.biHeight;
			}
			ushort biBitCount = bitmapinfoheader.biBitCount;
			Bitmap bitmap;
			int[] array;
			switch (biBitCount)
			{
			case 1:
				bitmap = new Bitmap(bitmapinfoheader.biWidth, bitmapinfoheader.biHeight, 196865);
				array = new int[2];
				break;
			default:
				if (biBitCount != 8)
				{
					if (biBitCount != 24 && biBitCount != 32)
					{
						throw new Exception("Unexpected number of bits:" + bitmapinfoheader.biBitCount.ToString());
					}
					bitmap = new Bitmap(bitmapinfoheader.biWidth, bitmapinfoheader.biHeight, 2498570);
					array = new int[0];
				}
				else
				{
					bitmap = new Bitmap(bitmapinfoheader.biWidth, bitmapinfoheader.biHeight, 198659);
					array = new int[256];
				}
				break;
			case 4:
				bitmap = new Bitmap(bitmapinfoheader.biWidth, bitmapinfoheader.biHeight, 197634);
				array = new int[16];
				break;
			}
			if (bitmapinfoheader.biBitCount < 24)
			{
				ColorPalette palette = bitmap.Palette;
				Marshal.Copy((IntPtr)((int)dib_data + Marshal.SizeOf(typeof(BITMAPINFOHEADER))), array, 0, array.Length);
				for (int i = 0; i < num; i++)
				{
					palette.Entries[i] = Color.FromArgb(array[i] | -16777216);
				}
				bitmap.Palette = palette;
			}
			int num3 = ((bitmapinfoheader.biWidth * (int)bitmapinfoheader.biBitCount + 31) & -32) >> 3;
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 2, bitmap.PixelFormat);
			byte[] array2 = new byte[num3];
			for (int j = 0; j < bitmapinfoheader.biHeight; j++)
			{
				Marshal.Copy((IntPtr)((int)dib_data + Marshal.SizeOf(typeof(BITMAPINFOHEADER)) + array.Length * 4 + num3 * j), array2, 0, num3);
				Marshal.Copy(array2, 0, (IntPtr)((int)bitmapData.Scan0 + bitmapData.Stride * (bitmapinfoheader.biHeight - 1 - j)), array2.Length);
			}
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x0011A2FC File Offset: 0x001184FC
		internal static byte[] ImageToDIB(Image image)
		{
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Bmp);
			byte[] buffer = memoryStream.GetBuffer();
			byte[] array = new byte[buffer.Length];
			Array.Copy(buffer, 14, array, 0, buffer.Length - 14);
			return array;
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x0011A33C File Offset: 0x0011853C
		internal static IntPtr DupGlobalMem(IntPtr mem)
		{
			uint num = XplatUIWin32.Win32GlobalSize(mem);
			IntPtr intPtr = XplatUIWin32.Win32GlobalLock(mem);
			IntPtr intPtr2 = XplatUIWin32.Win32GlobalAlloc(XplatUIWin32.GAllocFlags.GMEM_MOVEABLE, (int)num);
			IntPtr intPtr3 = XplatUIWin32.Win32GlobalLock(intPtr2);
			XplatUIWin32.Win32CopyMemory(intPtr3, intPtr, (int)num);
			XplatUIWin32.Win32GlobalUnlock(mem);
			XplatUIWin32.Win32GlobalUnlock(intPtr2);
			return intPtr2;
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x0011A380 File Offset: 0x00118580
		private int GetSystemParametersInfoInt(XplatUIWin32.SPIAction spi)
		{
			int num = 0;
			XplatUIWin32.Win32SystemParametersInfo(spi, 0U, ref num, 0U);
			return num;
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x0011A39C File Offset: 0x0011859C
		private bool GetSystemParametersInfoBool(XplatUIWin32.SPIAction spi)
		{
			bool flag = false;
			XplatUIWin32.Win32SystemParametersInfo(spi, 0U, ref flag, 0U);
			return flag;
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x0011A3B8 File Offset: 0x001185B8
		internal override int ActiveWindowTrackingDelay
		{
			get
			{
				return this.GetSystemParametersInfoInt(XplatUIWin32.SPIAction.SPI_GETACTIVEWNDTRKTIMEOUT);
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x060048CD RID: 18637 RVA: 0x0011A3C8 File Offset: 0x001185C8
		internal override int CaretWidth
		{
			get
			{
				if (Environment.OSVersion.Version.Major < 5)
				{
					throw new NotSupportedException();
				}
				return this.GetSystemParametersInfoInt(XplatUIWin32.SPIAction.SPI_GETCARETWIDTH);
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x060048CE RID: 18638 RVA: 0x0011A3FC File Offset: 0x001185FC
		internal override int FontSmoothingContrast
		{
			get
			{
				if (Environment.OSVersion.Version.Major < 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0))
				{
					throw new NotSupportedException();
				}
				return this.GetSystemParametersInfoInt(XplatUIWin32.SPIAction.SPI_GETFONTSMOOTHINGCONTRAST);
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x0011A458 File Offset: 0x00118658
		internal override int FontSmoothingType
		{
			get
			{
				if (Environment.OSVersion.Version.Major < 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0))
				{
					throw new NotSupportedException();
				}
				return this.GetSystemParametersInfoInt(XplatUIWin32.SPIAction.SPI_GETFONTSMOOTHINGTYPE);
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x0011A4B4 File Offset: 0x001186B4
		internal override int HorizontalResizeBorderThickness
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXFRAME);
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x0011A4C0 File Offset: 0x001186C0
		internal override bool IsActiveWindowTrackingEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETACTIVEWINDOWTRACKING);
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x060048D2 RID: 18642 RVA: 0x0011A4D0 File Offset: 0x001186D0
		internal override bool IsComboBoxAnimationEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETCOMBOBOXANIMATION);
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x0011A4E0 File Offset: 0x001186E0
		internal override bool IsDropShadowEnabled
		{
			get
			{
				if (Environment.OSVersion.Version.Major < 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0))
				{
					throw new NotSupportedException();
				}
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETDROPSHADOW);
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x060048D4 RID: 18644 RVA: 0x0011A53C File Offset: 0x0011873C
		internal override bool IsFontSmoothingEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETFONTSMOOTHING);
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0011A548 File Offset: 0x00118748
		internal override bool IsHotTrackingEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETHOTTRACKING);
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x060048D6 RID: 18646 RVA: 0x0011A558 File Offset: 0x00118758
		internal override bool IsIconTitleWrappingEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETICONTITLEWRAP);
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x0011A564 File Offset: 0x00118764
		internal override bool IsKeyboardPreferred
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETKEYBOARDPREF);
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x060048D8 RID: 18648 RVA: 0x0011A570 File Offset: 0x00118770
		internal override bool IsListBoxSmoothScrollingEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETLISTBOXSMOOTHSCROLLING);
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x0011A580 File Offset: 0x00118780
		internal override bool IsMenuAnimationEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETMENUANIMATION);
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x060048DA RID: 18650 RVA: 0x0011A590 File Offset: 0x00118790
		internal override bool IsMenuFadeEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETMENUFADE);
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x060048DB RID: 18651 RVA: 0x0011A5A0 File Offset: 0x001187A0
		internal override bool IsMinimizeRestoreAnimationEnabled
		{
			get
			{
				XplatUIWin32.ANIMATIONINFO animationinfo = default(XplatUIWin32.ANIMATIONINFO);
				animationinfo.cbSize = (uint)Marshal.SizeOf(animationinfo);
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETANIMATION, 0U, ref animationinfo, 0U);
				return animationinfo.iMinAnimate != 0;
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x060048DC RID: 18652 RVA: 0x0011A5E8 File Offset: 0x001187E8
		internal override bool IsSelectionFadeEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETSELECTIONFADE);
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x060048DD RID: 18653 RVA: 0x0011A5F8 File Offset: 0x001187F8
		internal override bool IsSnapToDefaultEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETSNAPTODEFBUTTON);
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x0011A604 File Offset: 0x00118804
		internal override bool IsTitleBarGradientEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETGRADIENTCAPTIONS);
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x060048DF RID: 18655 RVA: 0x0011A614 File Offset: 0x00118814
		internal override bool IsToolTipAnimationEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETTOOLTIPANIMATION);
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x0011A624 File Offset: 0x00118824
		internal override Size MenuBarButtonSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMENUSIZE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMENUSIZE));
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x060048E1 RID: 18657 RVA: 0x0011A63C File Offset: 0x0011883C
		public override Size MenuButtonSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMENUSIZE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMENUSIZE));
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x060048E2 RID: 18658 RVA: 0x0011A654 File Offset: 0x00118854
		internal override int MenuShowDelay
		{
			get
			{
				return this.GetSystemParametersInfoInt(XplatUIWin32.SPIAction.SPI_GETMENUSHOWDELAY);
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x0011A660 File Offset: 0x00118860
		internal override int MouseSpeed
		{
			get
			{
				return this.GetSystemParametersInfoInt(XplatUIWin32.SPIAction.SPI_GETMOUSESPEED);
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x0011A66C File Offset: 0x0011886C
		internal override LeftRightAlignment PopupMenuAlignment
		{
			get
			{
				return (!this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETMENUDROPALIGNMENT)) ? LeftRightAlignment.Right : LeftRightAlignment.Left;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x0011A684 File Offset: 0x00118884
		internal override PowerStatus PowerStatus
		{
			get
			{
				XplatUIWin32.SYSTEMPOWERSTATUS systempowerstatus = new XplatUIWin32.SYSTEMPOWERSTATUS();
				XplatUIWin32.Win32GetSystemPowerStatus(systempowerstatus);
				return new PowerStatus((BatteryChargeStatus)systempowerstatus._BatteryFlag, systempowerstatus._BatteryFullLifeTime, (float)systempowerstatus._BatteryLifePercent / 255f, systempowerstatus._BatteryLifeTime, (PowerLineStatus)systempowerstatus._ACLineStatus);
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x060048E6 RID: 18662 RVA: 0x0011A6CC File Offset: 0x001188CC
		internal override int SizingBorderWidth
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXFRAME);
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x0011A6D8 File Offset: 0x001188D8
		internal override Size SmallCaptionButtonSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXSMSIZE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYSMSIZE));
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x060048E8 RID: 18664 RVA: 0x0011A6F0 File Offset: 0x001188F0
		internal override bool UIEffectsEnabled
		{
			get
			{
				return this.GetSystemParametersInfoBool(XplatUIWin32.SPIAction.SPI_GETUIEFFECTS);
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x060048E9 RID: 18665 RVA: 0x0011A700 File Offset: 0x00118900
		internal override int VerticalResizeBorderThickness
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYFRAME);
			}
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x0011A70C File Offset: 0x0011890C
		internal override void RaiseIdle(EventArgs e)
		{
			if (this.Idle != null)
			{
				this.Idle.Invoke(this, e);
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x0011A728 File Offset: 0x00118928
		internal override Keys ModifierKeys
		{
			get
			{
				Keys keys = Keys.None;
				short num = XplatUIWin32.Win32GetKeyState(VirtualKeys.VK_SHIFT);
				if (((int)num & 32768) != 0)
				{
					keys |= Keys.Shift;
				}
				num = XplatUIWin32.Win32GetKeyState(VirtualKeys.VK_CONTROL);
				if (((int)num & 32768) != 0)
				{
					keys |= Keys.Control;
				}
				num = XplatUIWin32.Win32GetKeyState(VirtualKeys.VK_MENU);
				if (((int)num & 32768) != 0)
				{
					keys |= Keys.Alt;
				}
				return keys;
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x0011A78C File Offset: 0x0011898C
		internal override MouseButtons MouseButtons
		{
			get
			{
				return XplatUIWin32.mouse_state;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x0011A794 File Offset: 0x00118994
		internal override Point MousePosition
		{
			get
			{
				return XplatUIWin32.mouse_position;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x060048EE RID: 18670 RVA: 0x0011A79C File Offset: 0x0011899C
		internal override Size MouseHoverSize
		{
			get
			{
				int num = 4;
				int num2 = 4;
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETMOUSEHOVERWIDTH, 0U, ref num, 0U);
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETMOUSEHOVERWIDTH, 0U, ref num2, 0U);
				return new Size(num, num2);
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x0011A7CC File Offset: 0x001189CC
		internal override int MouseHoverTime
		{
			get
			{
				int num = 500;
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETMOUSEHOVERTIME, 0U, ref num, 0U);
				return num;
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x060048F0 RID: 18672 RVA: 0x0011A7EC File Offset: 0x001189EC
		internal override int MouseWheelScrollDelta
		{
			get
			{
				int num = 120;
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETWHEELSCROLLLINES, 0U, ref num, 0U);
				return num;
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x0011A80C File Offset: 0x00118A0C
		internal override int HorizontalScrollBarHeight
		{
			get
			{
				return XplatUIWin32.scroll_height;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x060048F2 RID: 18674 RVA: 0x0011A814 File Offset: 0x00118A14
		internal override bool UserClipWontExposeParent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x0011A818 File Offset: 0x00118A18
		internal override int VerticalScrollBarWidth
		{
			get
			{
				return XplatUIWin32.scroll_width;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x0011A820 File Offset: 0x00118A20
		internal override int MenuHeight
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMENU);
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x060048F5 RID: 18677 RVA: 0x0011A82C File Offset: 0x00118A2C
		internal override Size Border3DSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXEDGE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYEDGE));
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x0011A844 File Offset: 0x00118A44
		internal override Size BorderSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXBORDER), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYBORDER));
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x060048F7 RID: 18679 RVA: 0x0011A858 File Offset: 0x00118A58
		// (set) Token: 0x060048F8 RID: 18680 RVA: 0x0011A85C File Offset: 0x00118A5C
		internal override bool DropTarget
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
				{
				}
			}
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x060048F9 RID: 18681 RVA: 0x0011A864 File Offset: 0x00118A64
		internal override Size CaptionButtonSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXSIZE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYSIZE));
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x0011A87C File Offset: 0x00118A7C
		internal override int CaptionHeight
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYCAPTION);
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x060048FB RID: 18683 RVA: 0x0011A884 File Offset: 0x00118A84
		internal override Size CursorSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYCURSOR));
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x0011A89C File Offset: 0x00118A9C
		internal override bool DragFullWindows
		{
			get
			{
				int num = 0;
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETDRAGFULLWINDOWS, 0U, ref num, 0U);
				return num != 0;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x060048FD RID: 18685 RVA: 0x0011A8C0 File Offset: 0x00118AC0
		internal override Size DragSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXDRAG), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYDRAG));
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x060048FE RID: 18686 RVA: 0x0011A8D8 File Offset: 0x00118AD8
		internal override Size DoubleClickSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXDOUBLECLK), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYDOUBLECLK));
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x060048FF RID: 18687 RVA: 0x0011A8F0 File Offset: 0x00118AF0
		internal override int DoubleClickTime
		{
			get
			{
				return XplatUIWin32.Win32GetDoubleClickTime();
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06004900 RID: 18688 RVA: 0x0011A8F8 File Offset: 0x00118AF8
		internal override Size FixedFrameBorderSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXDLGFRAME), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYDLGFRAME));
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06004901 RID: 18689 RVA: 0x0011A90C File Offset: 0x00118B0C
		internal override Size FrameBorderSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXFRAME), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYFRAME));
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06004902 RID: 18690 RVA: 0x0011A924 File Offset: 0x00118B24
		internal override Size IconSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXICON), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYICON));
			}
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06004903 RID: 18691 RVA: 0x0011A93C File Offset: 0x00118B3C
		internal override Size MaxWindowTrackSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMAXTRACK), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMAXTRACK));
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x0011A954 File Offset: 0x00118B54
		internal override bool MenuAccessKeysUnderlined
		{
			get
			{
				int num = 0;
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETKEYBOARDCUES, 0U, ref num, 0U);
				return num != 0;
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06004905 RID: 18693 RVA: 0x0011A97C File Offset: 0x00118B7C
		internal override Size MinimizedWindowSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMINIMIZED), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMINIMIZED));
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06004906 RID: 18694 RVA: 0x0011A994 File Offset: 0x00118B94
		internal override Size MinimizedWindowSpacingSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMINSPACING), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMINSPACING));
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x0011A9AC File Offset: 0x00118BAC
		internal override Size MinimumWindowSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMIN), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMIN));
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06004908 RID: 18696 RVA: 0x0011A9C4 File Offset: 0x00118BC4
		internal override Size MinWindowTrackSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMINTRACK), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMINTRACK));
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06004909 RID: 18697 RVA: 0x0011A9DC File Offset: 0x00118BDC
		internal override Size SmallIconSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXSMICON), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYSMICON));
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x0600490A RID: 18698 RVA: 0x0011A9F4 File Offset: 0x00118BF4
		internal override int MouseButtonCount
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CMOUSEBUTTONS);
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x0600490B RID: 18699 RVA: 0x0011AA00 File Offset: 0x00118C00
		internal override bool MouseButtonsSwapped
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_SWAPBUTTON) != 0;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x0011AA10 File Offset: 0x00118C10
		internal override bool MouseWheelPresent
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_MOUSEWHEELPRESENT) != 0;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x0600490D RID: 18701 RVA: 0x0011AA20 File Offset: 0x00118C20
		internal override Rectangle VirtualScreen
		{
			get
			{
				return new Rectangle(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_XVIRTUALSCREEN), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_YVIRTUALSCREEN), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXVIRTUALSCREEN), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYVIRTUALSCREEN));
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x0011AA50 File Offset: 0x00118C50
		internal override Rectangle WorkingArea
		{
			get
			{
				XplatUIWin32.RECT rect = default(XplatUIWin32.RECT);
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETWORKAREA, 0U, ref rect, 0U);
				return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x0600490F RID: 18703 RVA: 0x0011AAA4 File Offset: 0x00118CA4
		internal override bool ThemesEnabled
		{
			get
			{
				return XplatUIWin32.themes_enabled;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x0011AAAC File Offset: 0x00118CAC
		internal override bool RequiresPositiveClientAreaSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06004911 RID: 18705 RVA: 0x0011AAB0 File Offset: 0x00118CB0
		public override int ToolWindowCaptionHeight
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYSMCAPTION);
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x0011AABC File Offset: 0x00118CBC
		public override Size ToolWindowCaptionButtonSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXSMSIZE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYSMSIZE));
			}
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x0011AAD4 File Offset: 0x00118CD4
		public static XplatUIWin32 GetInstance()
		{
			if (XplatUIWin32.instance == null)
			{
				XplatUIWin32.instance = new XplatUIWin32();
			}
			XplatUIWin32.ref_count++;
			return XplatUIWin32.instance;
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x0011AAFC File Offset: 0x00118CFC
		public int Reference
		{
			get
			{
				return XplatUIWin32.ref_count;
			}
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x0011AB04 File Offset: 0x00118D04
		internal override IntPtr InitializeDriver()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x0011AB0C File Offset: 0x00118D0C
		internal override void ShutdownDriver(IntPtr token)
		{
			Console.WriteLine("XplatUIWin32 ShutdownDriver called");
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x0011AB18 File Offset: 0x00118D18
		internal void Version()
		{
			Console.WriteLine("Xplat version $revision: $");
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x0011AB24 File Offset: 0x00118D24
		private string GetSoundAlias(AlertType alert)
		{
			switch (alert)
			{
			case AlertType.Error:
				return "SystemHand";
			case AlertType.Question:
				return "SystemQuestion";
			case AlertType.Warning:
				return "SystemExclamation";
			case AlertType.Information:
				return "SystemAsterisk";
			default:
				return "SystemDefault";
			}
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x0011AB70 File Offset: 0x00118D70
		internal override void AudibleAlert(AlertType alert)
		{
			XplatUIWin32.Win32PlaySound(this.GetSoundAlias(alert), IntPtr.Zero, (XplatUIWin32.SndFlags)1122321);
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x0011AB8C File Offset: 0x00118D8C
		internal override void GetDisplaySize(out Size size)
		{
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(XplatUIWin32.Win32GetDesktopWindow(), out rect);
			size..ctor(rect.right - rect.left, rect.bottom - rect.top);
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x0011ABCC File Offset: 0x00118DCC
		internal override void EnableThemes()
		{
			XplatUIWin32.themes_enabled = true;
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x0011ABD4 File Offset: 0x00118DD4
		internal override IntPtr CreateWindow(CreateParams cp)
		{
			Hwnd hwnd = new Hwnd();
			IntPtr intPtr = cp.Parent;
			if (intPtr == IntPtr.Zero && (cp.Style & 1073741824) != 0)
			{
				intPtr = XplatUIWin32.FosterParent;
			}
			if ((cp.Style & -1073741824) == 0 && (cp.ExStyle & 262144) == 0)
			{
				intPtr = XplatUIWin32.FosterParent;
			}
			Point nextStackedFormLocation;
			if (cp.HasWindowManager)
			{
				nextStackedFormLocation = Hwnd.GetNextStackedFormLocation(cp, Hwnd.ObjectFromHandle(cp.Parent));
			}
			else
			{
				nextStackedFormLocation..ctor(cp.X, cp.Y);
			}
			string text = this.RegisterWindowClass(cp.ClassStyle);
			this.HwndCreating = hwnd;
			if ((cp.WindowExStyle & WindowExStyles.WS_EX_MDICHILD) == WindowExStyles.WS_EX_MDICHILD)
			{
				cp.WindowExStyle ^= WindowExStyles.WS_EX_MDICHILD;
			}
			IntPtr intPtr2 = XplatUIWin32.Win32CreateWindow(cp.WindowExStyle, text, cp.Caption, cp.WindowStyle, nextStackedFormLocation.X, nextStackedFormLocation.Y, cp.Width, cp.Height, intPtr, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			this.HwndCreating = null;
			if (intPtr2 == IntPtr.Zero)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				XplatUIWin32.Win32MessageBox(IntPtr.Zero, "Error : " + lastWin32Error.ToString(), "Failed to create window, class '" + cp.ClassName + "'", 0U);
			}
			hwnd.ClientWindow = intPtr2;
			hwnd.Mapped = true;
			XplatUIWin32.Win32SetWindowLong(intPtr2, XplatUIWin32.WindowLong.GWL_USERDATA, (uint)ThemeEngine.Current.DefaultControlBackColor.ToArgb());
			return intPtr2;
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x0011AD68 File Offset: 0x00118F68
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

		// Token: 0x0600491E RID: 18718 RVA: 0x0011ADDC File Offset: 0x00118FDC
		internal override void DestroyWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			XplatUIWin32.Win32DestroyWindow(handle);
			hwnd.Dispose();
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x0011AE00 File Offset: 0x00119000
		internal override void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max)
		{
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x0011AE04 File Offset: 0x00119004
		internal override FormWindowState GetWindowState(IntPtr handle)
		{
			uint num = XplatUIWin32.Win32GetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE);
			if ((num & 16777216U) != 0U)
			{
				return FormWindowState.Maximized;
			}
			if ((num & 536870912U) != 0U)
			{
				return FormWindowState.Minimized;
			}
			return FormWindowState.Normal;
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x0011AE38 File Offset: 0x00119038
		internal override void SetWindowState(IntPtr hwnd, FormWindowState state)
		{
			switch (state)
			{
			case FormWindowState.Normal:
				XplatUIWin32.Win32ShowWindow(hwnd, XplatUIWin32.WindowPlacementFlags.SW_RESTORE);
				return;
			case FormWindowState.Minimized:
				XplatUIWin32.Win32ShowWindow(hwnd, XplatUIWin32.WindowPlacementFlags.SW_MINIMIZE);
				return;
			case FormWindowState.Maximized:
				XplatUIWin32.Win32ShowWindow(hwnd, XplatUIWin32.WindowPlacementFlags.SW_SHOWMAXIMIZED);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x0011AE7C File Offset: 0x0011907C
		internal override void SetWindowStyle(IntPtr handle, CreateParams cp)
		{
			XplatUIWin32.Win32SetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE, (uint)cp.Style);
			XplatUIWin32.Win32SetWindowLong(handle, XplatUIWin32.WindowLong.GWL_EXSTYLE, (uint)cp.ExStyle);
			if (cp.control is Form)
			{
				XplatUI.RequestNCRecalc(handle);
			}
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x0011AEC0 File Offset: 0x001190C0
		internal override double GetWindowTransparency(IntPtr handle)
		{
			XplatUIWin32.COLORREF colorref;
			byte b;
			XplatUIWin32.LayeredWindowAttributes layeredWindowAttributes;
			if (XplatUIWin32.Win32GetLayeredWindowAttributes(handle, out colorref, out b, out layeredWindowAttributes) == 0U)
			{
				return 1.0;
			}
			return (double)b / 255.0;
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x0011AEF4 File Offset: 0x001190F4
		internal override void SetWindowTransparency(IntPtr handle, double transparency, Color key)
		{
			XplatUIWin32.LayeredWindowAttributes layeredWindowAttributes = XplatUIWin32.LayeredWindowAttributes.LWA_ALPHA;
			byte b = (byte)(transparency * 255.0);
			XplatUIWin32.COLORREF colorref = default(XplatUIWin32.COLORREF);
			if (key != Color.Empty)
			{
				colorref.R = key.R;
				colorref.G = key.G;
				colorref.B = key.B;
				layeredWindowAttributes |= XplatUIWin32.LayeredWindowAttributes.LWA_COLORKEY;
			}
			XplatUIWin32.RECT rect;
			rect.right = 1000;
			rect.bottom = 1000;
			XplatUIWin32.Win32SetLayeredWindowAttributes(handle, colorref, b, layeredWindowAttributes);
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x0011AF78 File Offset: 0x00119178
		internal override TransparencySupport SupportsTransparency()
		{
			if (this.queried_transparency_support)
			{
				return this.support;
			}
			this.support = TransparencySupport.None;
			bool flag = true;
			try
			{
				XplatUIWin32.Win32SetLayeredWindowAttributes(IntPtr.Zero, default(XplatUIWin32.COLORREF), byte.MaxValue, XplatUIWin32.LayeredWindowAttributes.LWA_ALPHA);
			}
			catch (EntryPointNotFoundException)
			{
				flag = false;
			}
			catch
			{
			}
			if (flag)
			{
				this.support |= TransparencySupport.Set;
			}
			flag = true;
			try
			{
				XplatUIWin32.COLORREF colorref;
				byte b;
				XplatUIWin32.LayeredWindowAttributes layeredWindowAttributes;
				XplatUIWin32.Win32GetLayeredWindowAttributes(IntPtr.Zero, out colorref, out b, out layeredWindowAttributes);
			}
			catch (EntryPointNotFoundException)
			{
				flag = false;
			}
			catch
			{
			}
			if (flag)
			{
				this.support |= TransparencySupport.Get;
			}
			this.queried_transparency_support = true;
			return this.support;
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x0011B090 File Offset: 0x00119290
		internal override void UpdateWindow(IntPtr handle)
		{
			XplatUIWin32.Win32UpdateWindow(handle);
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x0011B09C File Offset: 0x0011929C
		internal override PaintEventArgs PaintEventStart(ref Message msg, IntPtr handle, bool client)
		{
			Rectangle rectangle = default(Rectangle);
			XplatUIWin32.RECT rect = default(XplatUIWin32.RECT);
			XplatUIWin32.PAINTSTRUCT paintstruct = default(XplatUIWin32.PAINTSTRUCT);
			Hwnd hwnd = Hwnd.ObjectFromHandle(msg.HWnd);
			IntPtr intPtr;
			if (client)
			{
				if (XplatUIWin32.Win32GetUpdateRect(msg.HWnd, ref rect, false))
				{
					if (handle != msg.HWnd)
					{
						XplatUIWin32.Win32GetClientRect(msg.HWnd, out rect);
						XplatUIWin32.Win32ValidateRect(msg.HWnd, ref rect);
						intPtr = XplatUIWin32.Win32GetDC(handle);
					}
					else
					{
						intPtr = XplatUIWin32.Win32BeginPaint(handle, ref paintstruct);
						rect = paintstruct.rcPaint;
					}
				}
				else
				{
					intPtr = XplatUIWin32.Win32GetDC(handle);
				}
				rectangle = rect.ToRectangle();
			}
			else
			{
				intPtr = XplatUIWin32.Win32GetWindowDC(handle);
				XplatUIWin32.Win32GetWindowRect(handle, out rect);
				rectangle..ctor(0, 0, rect.Width, rect.Height);
			}
			if (paintstruct.hdc != IntPtr.Zero)
			{
				hwnd.drawing_stack.Push(paintstruct);
			}
			else
			{
				hwnd.drawing_stack.Push(intPtr);
			}
			Graphics graphics = Graphics.FromHdc(intPtr);
			hwnd.drawing_stack.Push(graphics);
			return new PaintEventArgs(graphics, rectangle);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x0011B1D4 File Offset: 0x001193D4
		internal override void PaintEventEnd(ref Message m, IntPtr handle, bool client)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(m.HWnd);
			Graphics graphics = (Graphics)hwnd.drawing_stack.Pop();
			graphics.Dispose();
			object obj = hwnd.drawing_stack.Pop();
			if (obj is IntPtr)
			{
				IntPtr intPtr = (IntPtr)obj;
				XplatUIWin32.Win32ReleaseDC(handle, intPtr);
			}
			else if (obj is XplatUIWin32.PAINTSTRUCT)
			{
				XplatUIWin32.PAINTSTRUCT paintstruct = (XplatUIWin32.PAINTSTRUCT)obj;
				XplatUIWin32.Win32EndPaint(handle, ref paintstruct);
			}
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x0011B24C File Offset: 0x0011944C
		internal override void SetWindowPos(IntPtr handle, int x, int y, int width, int height)
		{
			XplatUIWin32.Win32MoveWindow(handle, x, y, width, height, true);
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x0011B25C File Offset: 0x0011945C
		internal override void GetWindowPos(IntPtr handle, bool is_toplevel, out int x, out int y, out int width, out int height, out int client_width, out int client_height)
		{
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(handle, out rect);
			width = rect.right - rect.left;
			height = rect.bottom - rect.top;
			POINT point;
			point.x = rect.left;
			point.y = rect.top;
			IntPtr intPtr = XplatUIWin32.Win32GetAncestor(handle, XplatUIWin32.AncestorType.GA_PARENT);
			if (intPtr != IntPtr.Zero && intPtr != XplatUIWin32.Win32GetDesktopWindow())
			{
				XplatUIWin32.Win32ScreenToClient(intPtr, ref point);
			}
			x = point.x;
			y = point.y;
			XplatUIWin32.Win32GetClientRect(handle, out rect);
			client_width = rect.right - rect.left;
			client_height = rect.bottom - rect.top;
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x0011B324 File Offset: 0x00119524
		internal override void Activate(IntPtr handle)
		{
			XplatUIWin32.Win32SetActiveWindow(handle);
			Hashtable hashtable = this.timer_list;
			lock (hashtable)
			{
				foreach (object obj in this.timer_list.Values)
				{
					Timer timer = (Timer)obj;
					if (timer.Enabled && timer.window == IntPtr.Zero)
					{
						timer.window = handle;
						int hashCode = timer.GetHashCode();
						XplatUIWin32.Win32SetTimer(handle, hashCode, (uint)timer.Interval, IntPtr.Zero);
					}
				}
			}
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x0011B410 File Offset: 0x00119610
		internal override void Invalidate(IntPtr handle, Rectangle rc, bool clear)
		{
			XplatUIWin32.RECT rect;
			rect.left = rc.Left;
			rect.top = rc.Top;
			rect.right = rc.Right;
			rect.bottom = rc.Bottom;
			XplatUIWin32.Win32InvalidateRect(handle, ref rect, clear);
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x0011B460 File Offset: 0x00119660
		internal override void InvalidateNC(IntPtr handle)
		{
			XplatUIWin32.Win32SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_DRAWFRAME | XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE | XplatUIWin32.SetWindowPosFlags.SWP_NOZORDER);
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x0011B474 File Offset: 0x00119674
		private IntPtr InternalWndProc(IntPtr hWnd, Msg msg, IntPtr wParam, IntPtr lParam)
		{
			if (this.HwndCreating != null && this.HwndCreating.ClientWindow == IntPtr.Zero)
			{
				this.HwndCreating.ClientWindow = hWnd;
			}
			return NativeWindow.WndProc(hWnd, msg, wParam, lParam);
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x0011B4BC File Offset: 0x001196BC
		internal override IntPtr DefWndProc(ref Message msg)
		{
			msg.Result = XplatUIWin32.Win32DefWindowProc(msg.HWnd, (Msg)msg.Msg, msg.WParam, msg.LParam);
			return msg.Result;
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x0011B4F4 File Offset: 0x001196F4
		internal override void HandleException(Exception e)
		{
			StackTrace stackTrace = new StackTrace(e);
			XplatUIWin32.Win32MessageBox(IntPtr.Zero, e.Message + stackTrace.ToString(), "Exception", 0U);
			Console.WriteLine("{0}{1}", e.Message, stackTrace.ToString());
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x0011B540 File Offset: 0x00119740
		internal override void DoEvents()
		{
			MSG msg = default(MSG);
			while (this.GetMessage(ref msg, IntPtr.Zero, 0, 0, false))
			{
				Message message = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
				if (!Application.FilterMessage(ref message))
				{
					XplatUI.TranslateMessage(ref msg);
					XplatUI.DispatchMessage(ref msg);
				}
			}
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x0011B5B4 File Offset: 0x001197B4
		internal override bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags)
		{
			return XplatUIWin32.Win32PeekMessage(ref msg, hWnd, wFilterMin, wFilterMax, flags);
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x0011B5C4 File Offset: 0x001197C4
		internal override void PostQuitMessage(int exitCode)
		{
			XplatUIWin32.Win32PostQuitMessage(exitCode);
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x0011B5D0 File Offset: 0x001197D0
		internal override void RequestAdditionalWM_NCMessages(IntPtr hwnd, bool hover, bool leave)
		{
			if (XplatUIWin32.wm_nc_registered == null)
			{
				XplatUIWin32.wm_nc_registered = new Hashtable();
			}
			XplatUIWin32.TMEFlags tmeflags = XplatUIWin32.TMEFlags.TME_NONCLIENT;
			if (hover)
			{
				tmeflags |= XplatUIWin32.TMEFlags.TME_HOVER;
			}
			if (leave)
			{
				tmeflags |= XplatUIWin32.TMEFlags.TME_LEAVE;
			}
			if (tmeflags == XplatUIWin32.TMEFlags.TME_NONCLIENT)
			{
				if (XplatUIWin32.wm_nc_registered.Contains(hwnd))
				{
					XplatUIWin32.wm_nc_registered.Remove(hwnd);
				}
			}
			else if (!XplatUIWin32.wm_nc_registered.Contains(hwnd))
			{
				XplatUIWin32.wm_nc_registered.Add(hwnd, tmeflags);
			}
			else
			{
				XplatUIWin32.wm_nc_registered[hwnd] = tmeflags;
			}
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x0011B680 File Offset: 0x00119880
		internal override void RequestNCRecalc(IntPtr handle)
		{
			XplatUIWin32.Win32SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_DRAWFRAME | XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOOWNERZORDER | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE | XplatUIWin32.SetWindowPosFlags.SWP_NOZORDER);
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x0011B698 File Offset: 0x00119898
		internal override void ResetMouseHover(IntPtr handle)
		{
			XplatUIWin32.TRACKMOUSEEVENT trackmouseevent = default(XplatUIWin32.TRACKMOUSEEVENT);
			trackmouseevent.size = Marshal.SizeOf(trackmouseevent);
			trackmouseevent.hWnd = handle;
			trackmouseevent.dwFlags = XplatUIWin32.TMEFlags.TME_HOVER | XplatUIWin32.TMEFlags.TME_LEAVE;
			XplatUIWin32.Win32TrackMouseEvent(ref trackmouseevent);
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0011B6D8 File Offset: 0x001198D8
		internal override bool GetMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax)
		{
			return this.GetMessage(ref msg, hWnd, wFilterMin, wFilterMax, true);
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x0011B6E8 File Offset: 0x001198E8
		private bool GetMessage(ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, bool blocking)
		{
			msg.refobject = 0;
			if (XplatUIWin32.RetrieveMessage(ref msg))
			{
				return true;
			}
			bool flag;
			if (blocking)
			{
				flag = XplatUIWin32.Win32GetMessage(ref msg, hWnd, wFilterMin, wFilterMax);
			}
			else
			{
				flag = XplatUIWin32.Win32PeekMessage(ref msg, hWnd, wFilterMin, wFilterMax, 1U);
				if (!flag)
				{
					return false;
				}
			}
			Msg message = msg.message;
			switch (message)
			{
			case Msg.WM_MOUSEMOVE:
				if (msg.hwnd != XplatUIWin32.prev_mouse_hwnd)
				{
					XplatUIWin32.mouse_state = Control.FromParamToMouseButtons((long)msg.lParam.ToInt32());
					XplatUIWin32.StoreMessage(ref msg);
					msg.message = Msg.WM_MOUSE_ENTER;
					XplatUIWin32.prev_mouse_hwnd = msg.hwnd;
					XplatUIWin32.TRACKMOUSEEVENT trackmouseevent = default(XplatUIWin32.TRACKMOUSEEVENT);
					trackmouseevent.size = Marshal.SizeOf(trackmouseevent);
					trackmouseevent.hWnd = msg.hwnd;
					trackmouseevent.dwFlags = XplatUIWin32.TMEFlags.TME_HOVER | XplatUIWin32.TMEFlags.TME_LEAVE;
					XplatUIWin32.Win32TrackMouseEvent(ref trackmouseevent);
					return flag;
				}
				break;
			case Msg.WM_LBUTTONDOWN:
				XplatUIWin32.mouse_state |= MouseButtons.Left;
				break;
			case Msg.WM_LBUTTONUP:
				XplatUIWin32.mouse_state &= ~MouseButtons.Left;
				break;
			default:
				if (message != Msg.WM_NCMOUSEMOVE)
				{
					if (message != Msg.WM_TIMER)
					{
						if (message == Msg.WM_DROPFILES)
						{
							return Win32DnD.HandleWMDropFiles(ref msg);
						}
						if (message != Msg.WM_MOUSELEAVE)
						{
							if (message == Msg.WM_ASYNC_MESSAGE)
							{
								XplatUIDriverSupport.ExecuteClientMessage((GCHandle)msg.lParam);
							}
						}
						else
						{
							XplatUIWin32.prev_mouse_hwnd = IntPtr.Zero;
						}
					}
					else
					{
						Timer timer = (Timer)this.timer_list[(int)msg.wParam];
						if (timer != null)
						{
							timer.FireTick();
						}
					}
				}
				else if (XplatUIWin32.wm_nc_registered != null && XplatUIWin32.wm_nc_registered.Contains(msg.hwnd))
				{
					XplatUIWin32.mouse_state = Control.FromParamToMouseButtons((long)msg.lParam.ToInt32());
					XplatUIWin32.TRACKMOUSEEVENT trackmouseevent2 = default(XplatUIWin32.TRACKMOUSEEVENT);
					trackmouseevent2.size = Marshal.SizeOf(trackmouseevent2);
					trackmouseevent2.hWnd = msg.hwnd;
					trackmouseevent2.dwFlags = (XplatUIWin32.TMEFlags)((int)XplatUIWin32.wm_nc_registered[msg.hwnd]);
					XplatUIWin32.Win32TrackMouseEvent(ref trackmouseevent2);
					return flag;
				}
				break;
			case Msg.WM_RBUTTONDOWN:
				XplatUIWin32.mouse_state |= MouseButtons.Right;
				break;
			case Msg.WM_RBUTTONUP:
				XplatUIWin32.mouse_state &= ~MouseButtons.Right;
				break;
			case Msg.WM_MBUTTONDOWN:
				XplatUIWin32.mouse_state |= MouseButtons.Middle;
				break;
			case Msg.WM_MBUTTONUP:
				XplatUIWin32.mouse_state &= ~MouseButtons.Middle;
				break;
			}
			return flag;
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x0011B9A8 File Offset: 0x00119BA8
		internal override bool TranslateMessage(ref MSG msg)
		{
			return XplatUIWin32.Win32TranslateMessage(ref msg);
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x0011B9B0 File Offset: 0x00119BB0
		internal override IntPtr DispatchMessage(ref MSG msg)
		{
			return XplatUIWin32.Win32DispatchMessage(ref msg);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x0011B9B8 File Offset: 0x00119BB8
		internal override bool SetZOrder(IntPtr hWnd, IntPtr AfterhWnd, bool Top, bool Bottom)
		{
			if (Top)
			{
				XplatUIWin32.Win32SetWindowPos(hWnd, XplatUIWin32.SetWindowPosZOrder.HWND_TOP, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
				return true;
			}
			if (!Bottom)
			{
				XplatUIWin32.Win32SetWindowPos(hWnd, AfterhWnd, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
				return false;
			}
			XplatUIWin32.Win32SetWindowPos(hWnd, (IntPtr)1, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
			return true;
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x0011BA08 File Offset: 0x00119C08
		internal override bool SetTopmost(IntPtr hWnd, bool Enabled)
		{
			if (Enabled)
			{
				XplatUIWin32.Win32SetWindowPos(hWnd, XplatUIWin32.SetWindowPosZOrder.HWND_TOPMOST, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
				return true;
			}
			XplatUIWin32.Win32SetWindowPos(hWnd, XplatUIWin32.SetWindowPosZOrder.HWND_NOTOPMOST, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
			return true;
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x0011BA3C File Offset: 0x00119C3C
		internal override bool SetOwner(IntPtr hWnd, IntPtr hWndOwner)
		{
			XplatUIWin32.Win32SetWindowLong(hWnd, XplatUIWin32.WindowLong.GWL_HWNDPARENT, (uint)(int)hWndOwner);
			return true;
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x0011BA50 File Offset: 0x00119C50
		internal override bool Text(IntPtr handle, string text)
		{
			XplatUIWin32.Win32SetWindowText(handle, text);
			return true;
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x0011BA5C File Offset: 0x00119C5C
		internal override bool GetText(IntPtr handle, out string text)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			XplatUIWin32.Win32GetWindowText(handle, stringBuilder, stringBuilder.Capacity);
			text = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x0011BA8C File Offset: 0x00119C8C
		internal override bool SetVisible(IntPtr handle, bool visible, bool activate)
		{
			if (visible)
			{
				Control control = Control.FromHandle(handle);
				if (control is Form)
				{
					Form form = (Form)Control.FromHandle(handle);
					XplatUIWin32.WindowPlacementFlags windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_SHOWNORMAL;
					switch (form.WindowState)
					{
					case FormWindowState.Normal:
						windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_SHOWNORMAL;
						break;
					case FormWindowState.Minimized:
						windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_MINIMIZE;
						break;
					case FormWindowState.Maximized:
						windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_SHOWMAXIMIZED;
						break;
					}
					if (!form.ActivateOnShow)
					{
						windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_SHOWNOACTIVATE;
					}
					XplatUIWin32.Win32ShowWindow(handle, windowPlacementFlags);
				}
				else if (control.ActivateOnShow)
				{
					XplatUIWin32.Win32ShowWindow(handle, XplatUIWin32.WindowPlacementFlags.SW_SHOWNORMAL);
				}
				else
				{
					XplatUIWin32.Win32ShowWindow(handle, XplatUIWin32.WindowPlacementFlags.SW_SHOWNOACTIVATE);
				}
			}
			else
			{
				XplatUIWin32.Win32ShowWindow(handle, XplatUIWin32.WindowPlacementFlags.SW_HIDE);
			}
			return true;
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x0011BB3C File Offset: 0x00119D3C
		internal override bool IsEnabled(IntPtr handle)
		{
			return XplatUIWin32.IsWindowEnabled(handle);
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x0011BB44 File Offset: 0x00119D44
		internal override bool IsKeyLocked(VirtualKeys key)
		{
			return (XplatUIWin32.Win32GetKeyState(key) & 1) == 1;
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x0011BB54 File Offset: 0x00119D54
		internal override bool IsVisible(IntPtr handle)
		{
			return XplatUIWin32.IsWindowVisible(handle);
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x0011BB5C File Offset: 0x00119D5C
		internal override IntPtr SetParent(IntPtr handle, IntPtr parent)
		{
			Control control = Control.FromHandle(handle);
			if (parent == IntPtr.Zero)
			{
				if (!(control is Form))
				{
					XplatUIWin32.Win32ShowWindow(handle, XplatUIWin32.WindowPlacementFlags.SW_HIDE);
				}
			}
			else if (!(control is Form))
			{
				this.SetVisible(handle, control.is_visible, true);
			}
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(handle, out rect);
			WindowStyles windowStyles = (WindowStyles)XplatUIWin32.Win32GetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE);
			WindowStyles windowStyles2;
			IntPtr intPtr;
			if (parent == IntPtr.Zero)
			{
				windowStyles2 = windowStyles & ~WindowStyles.WS_CHILD;
				intPtr = XplatUIWin32.Win32SetParent(handle, XplatUIWin32.FosterParent);
			}
			else
			{
				windowStyles2 = windowStyles | WindowStyles.WS_CHILD;
				intPtr = XplatUIWin32.Win32SetParent(handle, parent);
			}
			if (windowStyles != windowStyles2 && control is Form)
			{
				XplatUIWin32.Win32SetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE, (uint)windowStyles2);
			}
			XplatUIWin32.RECT rect2;
			XplatUIWin32.Win32GetWindowRect(handle, out rect2);
			if (rect.top != rect2.top && rect.left != rect2.left && control is Form)
			{
				XplatUIWin32.Win32SetWindowPos(handle, IntPtr.Zero, rect.top, rect.left, rect.Width, rect.Height, XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOOWNERZORDER | XplatUIWin32.SetWindowPosFlags.SWP_NOREDRAW | XplatUIWin32.SetWindowPosFlags.SWP_NOENDSCHANGING | XplatUIWin32.SetWindowPosFlags.SWP_NOZORDER);
			}
			return intPtr;
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x0011BC8C File Offset: 0x00119E8C
		internal override IntPtr GetParent(IntPtr handle)
		{
			return XplatUIWin32.Win32GetParent(handle);
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x0011BC94 File Offset: 0x00119E94
		internal override IntPtr GetPreviousWindow(IntPtr handle)
		{
			return handle;
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x0011BC98 File Offset: 0x00119E98
		internal override void GrabWindow(IntPtr hWnd, IntPtr ConfineToHwnd)
		{
			XplatUIWin32.grab_hwnd = hWnd;
			XplatUIWin32.Win32SetCapture(hWnd);
			if (ConfineToHwnd != IntPtr.Zero)
			{
				XplatUIWin32.RECT rect;
				XplatUIWin32.Win32GetWindowRect(ConfineToHwnd, out rect);
				XplatUIWin32.Win32GetClipCursor(out XplatUIWin32.clipped_cursor_rect);
				XplatUIWin32.Win32ClipCursor(ref rect);
			}
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x0011BCE0 File Offset: 0x00119EE0
		internal override void GrabInfo(out IntPtr hWnd, out bool GrabConfined, out Rectangle GrabArea)
		{
			hWnd = XplatUIWin32.grab_hwnd;
			GrabConfined = XplatUIWin32.grab_confined;
			GrabArea = XplatUIWin32.grab_area;
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x0011BCFC File Offset: 0x00119EFC
		internal override void UngrabWindow(IntPtr hWnd)
		{
			if (XplatUIWin32.clipped_cursor_rect.top != 0 || XplatUIWin32.clipped_cursor_rect.bottom != 0 || XplatUIWin32.clipped_cursor_rect.left != 0 || XplatUIWin32.clipped_cursor_rect.right != 0)
			{
				XplatUIWin32.Win32ClipCursor(ref XplatUIWin32.clipped_cursor_rect);
				XplatUIWin32.clipped_cursor_rect = default(XplatUIWin32.RECT);
			}
			XplatUIWin32.Win32ReleaseCapture();
			XplatUIWin32.grab_hwnd = IntPtr.Zero;
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x0011BD70 File Offset: 0x00119F70
		internal override bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect)
		{
			XplatUIWin32.RECT rect;
			rect.left = ClientRect.Left;
			rect.top = ClientRect.Top;
			rect.right = ClientRect.Right;
			rect.bottom = ClientRect.Bottom;
			if (!XplatUIWin32.Win32AdjustWindowRectEx(ref rect, cp.Style, menu != null, cp.ExStyle))
			{
				WindowRect..ctor(ClientRect.Left, ClientRect.Top, ClientRect.Width, ClientRect.Height);
				return false;
			}
			WindowRect..ctor(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
			return true;
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x0011BE28 File Offset: 0x0011A028
		internal override void SetCursor(IntPtr window, IntPtr cursor)
		{
			XplatUIWin32.Win32SetCursor(cursor);
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x0011BE34 File Offset: 0x0011A034
		internal override void ShowCursor(bool show)
		{
			XplatUIWin32.Win32ShowCursor(show);
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x0011BE40 File Offset: 0x0011A040
		internal override void OverrideCursor(IntPtr cursor)
		{
			XplatUIWin32.Win32SetCursor(cursor);
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x0011BE4C File Offset: 0x0011A04C
		internal override IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			Bitmap bitmap2;
			Bitmap bitmap3;
			if (bitmap.Width != XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR) || bitmap.Width != XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR))
			{
				bitmap2 = new Bitmap(bitmap, new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR)));
				bitmap3 = new Bitmap(mask, new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR)));
			}
			else
			{
				bitmap2 = bitmap;
				bitmap3 = mask;
			}
			int width = bitmap2.Width;
			int height = bitmap2.Height;
			byte[] array = new byte[width / 8 * height];
			byte[] array2 = new byte[width / 8 * height];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color color = bitmap2.GetPixel(j, i);
					if (color == cursor_pixel)
					{
						byte[] array3 = array;
						int num = i * width / 8 + j / 8;
						array3[num] |= (byte)(128 >> j % 8);
					}
					color = bitmap3.GetPixel(j, i);
					if (color == mask_pixel)
					{
						byte[] array4 = array2;
						int num2 = i * width / 8 + j / 8;
						array4[num2] |= (byte)(128 >> j % 8);
					}
				}
			}
			return XplatUIWin32.Win32CreateCursor(IntPtr.Zero, xHotSpot, yHotSpot, width, height, array2, array);
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x0011BFA8 File Offset: 0x0011A1A8
		internal override Bitmap DefineStdCursorBitmap(StdCursor id)
		{
			IntPtr intPtr = this.DefineStdCursor(id);
			int num = XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR);
			int num2 = XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYCURSOR);
			Bitmap bitmap = new Bitmap(num, num2);
			Graphics graphics = Graphics.FromImage(bitmap);
			IntPtr hdc = graphics.GetHdc();
			XplatUIWin32.Win32DrawIcon(hdc, 0, 0, intPtr);
			graphics.ReleaseHdc(hdc);
			graphics.Dispose();
			return bitmap;
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x0011C004 File Offset: 0x0011A204
		[MonoTODO("Define the missing cursors")]
		internal override IntPtr DefineStdCursor(StdCursor id)
		{
			switch (id)
			{
			case StdCursor.Default:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.AppStarting:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_APPSTARTING);
			case StdCursor.Arrow:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.Cross:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_CROSS);
			case StdCursor.Hand:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_HAND);
			case StdCursor.Help:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_HELP);
			case StdCursor.HSplit:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.IBeam:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_IBEAM);
			case StdCursor.No:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_NO);
			case StdCursor.NoMove2D:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.NoMoveHoriz:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.NoMoveVert:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanEast:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanNE:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanNorth:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanNW:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanSE:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanSouth:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanSW:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanWest:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.SizeAll:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZEALL);
			case StdCursor.SizeNESW:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZENESW);
			case StdCursor.SizeNS:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZENS);
			case StdCursor.SizeNWSE:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZENWSE);
			case StdCursor.SizeWE:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZEWE);
			case StdCursor.UpArrow:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_UPARROW);
			case StdCursor.VSplit:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.WaitCursor:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_WAIT);
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x0011C254 File Offset: 0x0011A454
		internal override void DestroyCursor(IntPtr cursor)
		{
			if (cursor.ToInt32() < 32512 || cursor.ToInt32() > 32651)
			{
				XplatUIWin32.Win32DestroyCursor(cursor);
			}
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x0011C280 File Offset: 0x0011A480
		[MonoTODO]
		internal override void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y)
		{
			XplatUIWin32.ICONINFO iconinfo = default(XplatUIWin32.ICONINFO);
			if (!XplatUIWin32.Win32GetIconInfo(cursor, out iconinfo))
			{
				throw new Win32Exception();
			}
			width = 20;
			height = 20;
			hotspot_x = iconinfo.xHotspot;
			hotspot_y = iconinfo.yHotspot;
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x0011C2C4 File Offset: 0x0011A4C4
		internal override void SetCursorPos(IntPtr handle, int x, int y)
		{
			XplatUIWin32.Win32SetCursorPos(x, y);
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x0011C2D0 File Offset: 0x0011A4D0
		internal override Region GetClipRegion(IntPtr hwnd)
		{
			Region region = new Region();
			XplatUIWin32.Win32GetWindowRgn(hwnd, region.GetHrgn(Graphics.FromHwnd(hwnd)));
			return region;
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x0011C2F8 File Offset: 0x0011A4F8
		internal override void SetClipRegion(IntPtr hwnd, Region region)
		{
			if (region == null)
			{
				XplatUIWin32.Win32SetWindowRgn(hwnd, IntPtr.Zero, true);
			}
			else
			{
				XplatUIWin32.Win32SetWindowRgn(hwnd, region.GetHrgn(Graphics.FromHwnd(hwnd)), true);
			}
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x0011C334 File Offset: 0x0011A534
		internal override void EnableWindow(IntPtr handle, bool Enable)
		{
			XplatUIWin32.Win32EnableWindow(handle, Enable);
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0011C340 File Offset: 0x0011A540
		internal override void EndLoop(Thread thread)
		{
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x0011C344 File Offset: 0x0011A544
		internal override object StartLoop(Thread thread)
		{
			return null;
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x0011C348 File Offset: 0x0011A548
		internal override void SetModal(IntPtr handle, bool Modal)
		{
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x0011C34C File Offset: 0x0011A54C
		internal override void GetCursorPos(IntPtr handle, out int x, out int y)
		{
			POINT point;
			XplatUIWin32.Win32GetCursorPos(out point);
			if (handle != IntPtr.Zero)
			{
				XplatUIWin32.Win32ScreenToClient(handle, ref point);
			}
			x = point.x;
			y = point.y;
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x0011C38C File Offset: 0x0011A58C
		internal override void ScreenToClient(IntPtr handle, ref int x, ref int y)
		{
			POINT point = default(POINT);
			point.x = x;
			point.y = y;
			XplatUIWin32.Win32ScreenToClient(handle, ref point);
			x = point.x;
			y = point.y;
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x0011C3D0 File Offset: 0x0011A5D0
		internal override void ClientToScreen(IntPtr handle, ref int x, ref int y)
		{
			POINT point = default(POINT);
			point.x = x;
			point.y = y;
			XplatUIWin32.Win32ClientToScreen(handle, ref point);
			x = point.x;
			y = point.y;
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x0011C414 File Offset: 0x0011A614
		internal override void ScreenToMenu(IntPtr handle, ref int x, ref int y)
		{
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(handle, out rect);
			x -= rect.left + SystemInformation.FrameBorderSize.Width;
			y -= rect.top + SystemInformation.FrameBorderSize.Height;
			WindowStyles windowStyles = (WindowStyles)XplatUIWin32.Win32GetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE);
			if (CreateParams.IsSet(windowStyles, WindowStyles.WS_CAPTION))
			{
				y -= ThemeEngine.Current.CaptionHeight;
			}
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x0011C488 File Offset: 0x0011A688
		internal override void MenuToScreen(IntPtr handle, ref int x, ref int y)
		{
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(handle, out rect);
			x += rect.left + SystemInformation.FrameBorderSize.Width;
			y += rect.top + SystemInformation.FrameBorderSize.Height + ThemeEngine.Current.CaptionHeight;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x0011C4E0 File Offset: 0x0011A6E0
		internal override void SendAsyncMethod(AsyncMethodData method)
		{
			XplatUIWin32.Win32PostMessage(XplatUIWin32.FosterParent, Msg.WM_ASYNC_MESSAGE, IntPtr.Zero, (IntPtr)GCHandle.Alloc(method));
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x0011C510 File Offset: 0x0011A710
		internal override void SetTimer(Timer timer)
		{
			int hashCode = timer.GetHashCode();
			Hashtable hashtable = this.timer_list;
			lock (hashtable)
			{
				this.timer_list[hashCode] = timer;
			}
			if (XplatUIWin32.Win32SetTimer(XplatUIWin32.FosterParent, hashCode, (uint)timer.Interval, IntPtr.Zero) != IntPtr.Zero)
			{
				timer.window = XplatUIWin32.FosterParent;
			}
			else
			{
				timer.window = IntPtr.Zero;
			}
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0011C5AC File Offset: 0x0011A7AC
		internal override void KillTimer(Timer timer)
		{
			int hashCode = timer.GetHashCode();
			XplatUIWin32.Win32KillTimer(timer.window, hashCode);
			Hashtable hashtable = this.timer_list;
			lock (hashtable)
			{
				this.timer_list.Remove(hashCode);
			}
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x0011C614 File Offset: 0x0011A814
		internal override void CreateCaret(IntPtr hwnd, int width, int height)
		{
			XplatUIWin32.Win32CreateCaret(hwnd, IntPtr.Zero, width, height);
			XplatUIWin32.caret_visible = false;
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x0011C62C File Offset: 0x0011A82C
		internal override void DestroyCaret(IntPtr hwnd)
		{
			XplatUIWin32.Win32DestroyCaret();
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x0011C634 File Offset: 0x0011A834
		internal override void SetCaretPos(IntPtr hwnd, int x, int y)
		{
			XplatUIWin32.Win32SetCaretPos(x, y);
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x0011C640 File Offset: 0x0011A840
		internal override void CaretVisible(IntPtr hwnd, bool visible)
		{
			if (visible)
			{
				if (!XplatUIWin32.caret_visible)
				{
					XplatUIWin32.Win32ShowCaret(hwnd);
					XplatUIWin32.caret_visible = true;
				}
			}
			else if (XplatUIWin32.caret_visible)
			{
				XplatUIWin32.Win32HideCaret(hwnd);
				XplatUIWin32.caret_visible = false;
			}
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x0011C67C File Offset: 0x0011A87C
		internal override IntPtr GetFocus()
		{
			return XplatUIWin32.Win32GetFocus();
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x0011C684 File Offset: 0x0011A884
		internal override void SetFocus(IntPtr hwnd)
		{
			XplatUIWin32.Win32SetFocus(hwnd);
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x0011C690 File Offset: 0x0011A890
		internal override IntPtr GetActive()
		{
			return XplatUIWin32.Win32GetActiveWindow();
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x0011C698 File Offset: 0x0011A898
		internal override bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent)
		{
			XplatUIWin32.TEXTMETRIC textmetric = default(XplatUIWin32.TEXTMETRIC);
			IntPtr intPtr = XplatUIWin32.Win32GetDC(IntPtr.Zero);
			IntPtr intPtr2 = XplatUIWin32.Win32SelectObject(intPtr, font.ToHfont());
			if (!XplatUIWin32.Win32GetTextMetrics(intPtr, ref textmetric))
			{
				intPtr2 = XplatUIWin32.Win32SelectObject(intPtr, intPtr2);
				XplatUIWin32.Win32DeleteObject(intPtr2);
				XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr);
				ascent = 0;
				descent = 0;
				return false;
			}
			intPtr2 = XplatUIWin32.Win32SelectObject(intPtr, intPtr2);
			XplatUIWin32.Win32DeleteObject(intPtr2);
			XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr);
			ascent = textmetric.tmAscent;
			descent = textmetric.tmDescent;
			return true;
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x0011C728 File Offset: 0x0011A928
		internal override void ScrollWindow(IntPtr hwnd, Rectangle rectangle, int XAmount, int YAmount, bool with_children)
		{
			XplatUIWin32.RECT rect = default(XplatUIWin32.RECT);
			rect.left = rectangle.X;
			rect.top = rectangle.Y;
			rect.right = rectangle.Right;
			rect.bottom = rectangle.Bottom;
			XplatUIWin32.Win32ScrollWindowEx(hwnd, XAmount, YAmount, IntPtr.Zero, ref rect, IntPtr.Zero, IntPtr.Zero, XplatUIWin32.ScrollWindowExFlags.SW_INVALIDATE | XplatUIWin32.ScrollWindowExFlags.SW_ERASE | ((!with_children) ? XplatUIWin32.ScrollWindowExFlags.SW_NONE : XplatUIWin32.ScrollWindowExFlags.SW_SCROLLCHILDREN));
			XplatUIWin32.Win32UpdateWindow(hwnd);
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x0011C7A8 File Offset: 0x0011A9A8
		internal override void ScrollWindow(IntPtr hwnd, int XAmount, int YAmount, bool with_children)
		{
			XplatUIWin32.Win32ScrollWindowEx(hwnd, XAmount, YAmount, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, XplatUIWin32.ScrollWindowExFlags.SW_INVALIDATE | XplatUIWin32.ScrollWindowExFlags.SW_ERASE | ((!with_children) ? XplatUIWin32.ScrollWindowExFlags.SW_NONE : XplatUIWin32.ScrollWindowExFlags.SW_SCROLLCHILDREN));
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x0011C7E4 File Offset: 0x0011A9E4
		internal override bool SystrayAdd(IntPtr hwnd, string tip, Icon icon, out ToolTip tt)
		{
			XplatUIWin32.NOTIFYICONDATA notifyicondata = default(XplatUIWin32.NOTIFYICONDATA);
			notifyicondata.cbSize = (uint)Marshal.SizeOf(notifyicondata);
			notifyicondata.hWnd = hwnd;
			notifyicondata.uID = 1U;
			notifyicondata.uCallbackMessage = 1024U;
			notifyicondata.uFlags = XplatUIWin32.NotifyIconFlags.NIF_MESSAGE;
			if (tip != null)
			{
				notifyicondata.szTip = tip;
				notifyicondata.uFlags |= XplatUIWin32.NotifyIconFlags.NIF_TIP;
			}
			if (icon != null)
			{
				notifyicondata.hIcon = icon.Handle;
				notifyicondata.uFlags |= XplatUIWin32.NotifyIconFlags.NIF_ICON;
			}
			tt = null;
			return XplatUIWin32.Win32Shell_NotifyIcon(XplatUIWin32.NotifyIconMessage.NIM_ADD, ref notifyicondata);
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x0011C87C File Offset: 0x0011AA7C
		internal override bool SystrayChange(IntPtr hwnd, string tip, Icon icon, ref ToolTip tt)
		{
			XplatUIWin32.NOTIFYICONDATA notifyicondata = default(XplatUIWin32.NOTIFYICONDATA);
			notifyicondata.cbSize = (uint)Marshal.SizeOf(notifyicondata);
			notifyicondata.hIcon = icon.Handle;
			notifyicondata.hWnd = hwnd;
			notifyicondata.uID = 1U;
			notifyicondata.uCallbackMessage = 1024U;
			notifyicondata.uFlags = XplatUIWin32.NotifyIconFlags.NIF_MESSAGE;
			if (tip != null)
			{
				notifyicondata.szTip = tip;
				notifyicondata.uFlags |= XplatUIWin32.NotifyIconFlags.NIF_TIP;
			}
			if (icon != null)
			{
				notifyicondata.hIcon = icon.Handle;
				notifyicondata.uFlags |= XplatUIWin32.NotifyIconFlags.NIF_ICON;
			}
			return XplatUIWin32.Win32Shell_NotifyIcon(XplatUIWin32.NotifyIconMessage.NIM_MODIFY, ref notifyicondata);
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x0011C91C File Offset: 0x0011AB1C
		internal override void SystrayRemove(IntPtr hwnd, ref ToolTip tt)
		{
			XplatUIWin32.NOTIFYICONDATA notifyicondata = default(XplatUIWin32.NOTIFYICONDATA);
			notifyicondata.cbSize = (uint)Marshal.SizeOf(notifyicondata);
			notifyicondata.hWnd = hwnd;
			notifyicondata.uID = 1U;
			notifyicondata.uFlags = (XplatUIWin32.NotifyIconFlags)0;
			XplatUIWin32.Win32Shell_NotifyIcon(XplatUIWin32.NotifyIconMessage.NIM_DELETE, ref notifyicondata);
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x0011C964 File Offset: 0x0011AB64
		internal override void SystrayBalloon(IntPtr hwnd, int timeout, string title, string text, ToolTipIcon icon)
		{
			XplatUIWin32.NOTIFYICONDATA notifyicondata = default(XplatUIWin32.NOTIFYICONDATA);
			notifyicondata.cbSize = (uint)Marshal.SizeOf(notifyicondata);
			notifyicondata.hWnd = hwnd;
			notifyicondata.uID = 1U;
			notifyicondata.uFlags = XplatUIWin32.NotifyIconFlags.NIF_INFO;
			notifyicondata.uTimeoutOrVersion = timeout;
			notifyicondata.szInfoTitle = title;
			notifyicondata.szInfo = text;
			notifyicondata.dwInfoFlags = icon;
			XplatUIWin32.Win32Shell_NotifyIcon(XplatUIWin32.NotifyIconMessage.NIM_MODIFY, ref notifyicondata);
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x0011C9D0 File Offset: 0x0011ABD0
		internal override void SetBorderStyle(IntPtr handle, FormBorderStyle border_style)
		{
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x0011C9D4 File Offset: 0x0011ABD4
		internal override void SetMenu(IntPtr handle, Menu menu)
		{
			XplatUIWin32.Win32SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_DRAWFRAME | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x0011C9E8 File Offset: 0x0011ABE8
		internal override Point GetMenuOrigin(IntPtr handle)
		{
			Form form = Control.FromHandle(handle) as Form;
			if (form == null)
			{
				return new Point(SystemInformation.FrameBorderSize.Width, SystemInformation.FrameBorderSize.Height + ThemeEngine.Current.CaptionHeight);
			}
			if (form.FormBorderStyle == FormBorderStyle.None)
			{
				return Point.Empty;
			}
			int num = (form.Width - form.ClientSize.Width) / 2;
			if (form.FormBorderStyle == FormBorderStyle.FixedToolWindow || form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
			{
				return new Point(num, num + SystemInformation.ToolWindowCaptionHeight);
			}
			return new Point(num, num + SystemInformation.CaptionHeight);
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x0011CA90 File Offset: 0x0011AC90
		internal override void SetIcon(IntPtr hwnd, Icon icon)
		{
			XplatUIWin32.Win32SendMessage(hwnd, Msg.WM_SETICON, (IntPtr)1, (icon != null) ? icon.Handle : IntPtr.Zero);
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0011CAC8 File Offset: 0x0011ACC8
		internal override void ClipboardClose(IntPtr handle)
		{
			if (handle != XplatUIWin32.clip_magic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			XplatUIWin32.Win32CloseClipboard();
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x0011CAEC File Offset: 0x0011ACEC
		internal override int ClipboardGetID(IntPtr handle, string format)
		{
			if (handle != XplatUIWin32.clip_magic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			if (format == "Text")
			{
				return 1;
			}
			if (format == "Bitmap")
			{
				return 2;
			}
			if (format == "MetaFilePict")
			{
				return 3;
			}
			if (format == "SymbolicLink")
			{
				return 4;
			}
			if (format == "DataInterchangeFormat")
			{
				return 5;
			}
			if (format == "Tiff")
			{
				return 6;
			}
			if (format == "OEMText")
			{
				return 7;
			}
			if (format == "DeviceIndependentBitmap")
			{
				return 8;
			}
			if (format == "Palette")
			{
				return 9;
			}
			if (format == "PenData")
			{
				return 10;
			}
			if (format == "RiffAudio")
			{
				return 11;
			}
			if (format == "WaveAudio")
			{
				return 12;
			}
			if (format == "UnicodeText")
			{
				return 13;
			}
			if (format == "EnhancedMetafile")
			{
				return 14;
			}
			if (format == "FileDrop")
			{
				return 15;
			}
			if (format == "Locale")
			{
				return 16;
			}
			return (int)XplatUIWin32.Win32RegisterClipboardFormat(format);
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x0011CC44 File Offset: 0x0011AE44
		internal override IntPtr ClipboardOpen(bool primary_selection)
		{
			XplatUIWin32.Win32OpenClipboard(XplatUIWin32.FosterParent);
			return XplatUIWin32.clip_magic;
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x0011CC58 File Offset: 0x0011AE58
		internal override int[] ClipboardAvailableFormats(IntPtr handle)
		{
			if (handle != XplatUIWin32.clip_magic)
			{
				return null;
			}
			int num = 0;
			uint num2 = 0U;
			do
			{
				num2 = XplatUIWin32.Win32EnumClipboardFormats(num2);
				if (num2 != 0U)
				{
					num++;
				}
			}
			while (num2 != 0U);
			int[] array = new int[num];
			num = 0;
			num2 = 0U;
			do
			{
				num2 = XplatUIWin32.Win32EnumClipboardFormats(num2);
				if (num2 != 0U)
				{
					array[num++] = (int)num2;
				}
			}
			while (num2 != 0U);
			return array;
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x0011CCBC File Offset: 0x0011AEBC
		internal override object ClipboardRetrieve(IntPtr handle, int type, XplatUI.ClipboardToObject converter)
		{
			if (handle != XplatUIWin32.clip_magic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			IntPtr intPtr = XplatUIWin32.Win32GetClipboardData((uint)type);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = XplatUIWin32.Win32GlobalLock(intPtr);
			if (intPtr2 == IntPtr.Zero)
			{
				uint num = XplatUIWin32.Win32GetLastError();
				Console.WriteLine("Error: {0}", num);
				return null;
			}
			object obj = null;
			if (type == DataFormats.GetFormat(DataFormats.Rtf).Id)
			{
				obj = XplatUIWin32.AnsiToString(intPtr2);
			}
			else
			{
				ClipboardFormats clipboardFormats = (ClipboardFormats)type;
				if (clipboardFormats != ClipboardFormats.CF_TEXT)
				{
					if (clipboardFormats != ClipboardFormats.CF_DIB)
					{
						if (clipboardFormats != ClipboardFormats.CF_UNICODETEXT)
						{
							if (converter != null && !converter(type, intPtr2, out obj))
							{
								obj = null;
							}
						}
						else
						{
							obj = XplatUIWin32.UnicodeToString(intPtr2);
						}
					}
					else
					{
						obj = XplatUIWin32.DIBtoImage(intPtr2);
					}
				}
				else
				{
					obj = XplatUIWin32.AnsiToString(intPtr2);
				}
			}
			XplatUIWin32.Win32GlobalUnlock(intPtr);
			return obj;
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x0011CDBC File Offset: 0x0011AFBC
		internal override void ClipboardStore(IntPtr handle, object obj, int type, XplatUI.ObjectToClipboard converter)
		{
			byte[] array = null;
			if (handle != XplatUIWin32.clip_magic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			if (obj != null)
			{
				if (type == -1)
				{
					if (obj is string)
					{
						type = 13;
					}
					else if (obj is Image)
					{
						type = 8;
					}
				}
				if (type == DataFormats.GetFormat(DataFormats.Rtf).Id)
				{
					array = XplatUIWin32.StringToAnsi((string)obj);
				}
				else
				{
					ClipboardFormats clipboardFormats = (ClipboardFormats)type;
					if (clipboardFormats != ClipboardFormats.CF_TEXT)
					{
						if (clipboardFormats != ClipboardFormats.CF_BITMAP && clipboardFormats != ClipboardFormats.CF_DIB)
						{
							if (clipboardFormats != ClipboardFormats.CF_UNICODETEXT)
							{
								if (converter != null && !converter(ref type, obj, out array))
								{
									array = null;
								}
							}
							else
							{
								array = XplatUIWin32.StringToUnicode((string)obj);
							}
						}
						else
						{
							array = XplatUIWin32.ImageToDIB((Image)obj);
							type = 8;
						}
					}
					else
					{
						array = XplatUIWin32.StringToAnsi((string)obj);
					}
				}
				if (array != null)
				{
					this.SetClipboardData((uint)type, array);
				}
				return;
			}
			if (!XplatUIWin32.Win32EmptyClipboard())
			{
				throw new ExternalException("Win32EmptyClipboard");
			}
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x0011CEDC File Offset: 0x0011B0DC
		internal static byte[] StringToUnicode(string text)
		{
			return Encoding.Unicode.GetBytes(text + "\0");
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x0011CEF4 File Offset: 0x0011B0F4
		internal static byte[] StringToAnsi(string text)
		{
			return Encoding.UTF8.GetBytes(text + "\0");
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x0011CF0C File Offset: 0x0011B10C
		private void SetClipboardData(uint type, byte[] data)
		{
			if (data.Length == 0)
			{
				return;
			}
			IntPtr intPtr = XplatUIWin32.CopyToMoveableMemory(data);
			if (intPtr == IntPtr.Zero)
			{
				throw new ExternalException("CopyToMoveableMemory failed.");
			}
			if (XplatUIWin32.Win32SetClipboardData(type, intPtr) == IntPtr.Zero)
			{
				throw new ExternalException("Win32SetClipboardData");
			}
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x0011CF68 File Offset: 0x0011B168
		internal static IntPtr CopyToMoveableMemory(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				throw new ArgumentException("Can't create a zero length memory block.");
			}
			IntPtr intPtr = XplatUIWin32.Win32GlobalAlloc(XplatUIWin32.GAllocFlags.GMEM_MOVEABLE | XplatUIWin32.GAllocFlags.GMEM_SHARE, data.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			IntPtr intPtr2 = XplatUIWin32.Win32GlobalLock(intPtr);
			if (intPtr2 == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			Marshal.Copy(data, 0, intPtr2, data.Length);
			XplatUIWin32.Win32GlobalUnlock(intPtr);
			return intPtr;
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x0011CFE4 File Offset: 0x0011B1E4
		internal override void SetAllowDrop(IntPtr hwnd, bool allowed)
		{
			if (allowed)
			{
				Win32DnD.RegisterDropTarget(hwnd);
			}
			else
			{
				Win32DnD.UnregisterDropTarget(hwnd);
			}
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x0011D000 File Offset: 0x0011B200
		internal override DragDropEffects StartDrag(IntPtr hwnd, object data, DragDropEffects allowedEffects)
		{
			return Win32DnD.StartDrag(hwnd, data, allowedEffects);
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x0011D00C File Offset: 0x0011B20C
		internal override void DrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style)
		{
			XplatUIWin32.COLORREF colorref = default(XplatUIWin32.COLORREF);
			colorref.R = backColor.R;
			colorref.G = backColor.G;
			colorref.B = backColor.B;
			IntPtr intPtr = XplatUIWin32.Win32CreatePen((style != FrameStyle.Thick) ? XplatUIWin32.PenStyle.PS_DASH : XplatUIWin32.PenStyle.PS_SOLID, (style != FrameStyle.Thick) ? 2 : 4, ref colorref);
			IntPtr intPtr2 = XplatUIWin32.Win32GetDC(IntPtr.Zero);
			XplatUIWin32.Win32SetROP2(intPtr2, XplatUIWin32.ROP2DrawMode.R2_NOT);
			IntPtr intPtr3 = XplatUIWin32.Win32SelectObject(intPtr2, intPtr);
			XplatUIWin32.Win32MoveToEx(intPtr2, rectangle.Left, rectangle.Top, IntPtr.Zero);
			if (rectangle.Width > 0 && rectangle.Height > 0)
			{
				XplatUIWin32.Win32LineTo(intPtr2, rectangle.Right, rectangle.Top);
				XplatUIWin32.Win32LineTo(intPtr2, rectangle.Right, rectangle.Bottom);
				XplatUIWin32.Win32LineTo(intPtr2, rectangle.Left, rectangle.Bottom);
				XplatUIWin32.Win32LineTo(intPtr2, rectangle.Left, rectangle.Top);
			}
			else if (rectangle.Width > 0)
			{
				XplatUIWin32.Win32LineTo(intPtr2, rectangle.Right, rectangle.Top);
			}
			else
			{
				XplatUIWin32.Win32LineTo(intPtr2, rectangle.Left, rectangle.Bottom);
			}
			XplatUIWin32.Win32SelectObject(intPtr2, intPtr3);
			XplatUIWin32.Win32DeleteObject(intPtr);
			XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr2);
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x0011D170 File Offset: 0x0011B370
		internal override void DrawReversibleLine(Point start, Point end, Color backColor)
		{
			XplatUIWin32.COLORREF colorref = default(XplatUIWin32.COLORREF);
			POINT point = default(POINT);
			point.x = 0;
			point.y = 0;
			XplatUIWin32.Win32ClientToScreen(IntPtr.Zero, ref point);
			colorref.R = backColor.R;
			colorref.G = backColor.G;
			colorref.B = backColor.B;
			IntPtr intPtr = XplatUIWin32.Win32CreatePen(XplatUIWin32.PenStyle.PS_SOLID, 1, ref colorref);
			IntPtr intPtr2 = XplatUIWin32.Win32GetDC(IntPtr.Zero);
			XplatUIWin32.Win32SetROP2(intPtr2, XplatUIWin32.ROP2DrawMode.R2_NOT);
			IntPtr intPtr3 = XplatUIWin32.Win32SelectObject(intPtr2, intPtr);
			XplatUIWin32.Win32MoveToEx(intPtr2, point.x + start.X, point.y + start.Y, IntPtr.Zero);
			XplatUIWin32.Win32LineTo(intPtr2, point.x + end.X, point.y + end.Y);
			XplatUIWin32.Win32SelectObject(intPtr2, intPtr3);
			XplatUIWin32.Win32DeleteObject(intPtr);
			XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr2);
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x0011D264 File Offset: 0x0011B464
		internal override void FillReversibleRectangle(Rectangle rectangle, Color backColor)
		{
			XplatUIWin32.RECT rect = default(XplatUIWin32.RECT);
			rect.left = rectangle.Left;
			rect.top = rectangle.Top;
			rect.right = rectangle.Right;
			rect.bottom = rectangle.Bottom;
			IntPtr intPtr = XplatUIWin32.Win32CreateSolidBrush(new XplatUIWin32.COLORREF
			{
				R = backColor.R,
				G = backColor.G,
				B = backColor.B
			});
			IntPtr intPtr2 = XplatUIWin32.Win32GetDC(IntPtr.Zero);
			IntPtr intPtr3 = XplatUIWin32.Win32SelectObject(intPtr2, intPtr);
			XplatUIWin32.Win32PatBlt(intPtr2, rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, XplatUIWin32.PatBltRop.DSTINVERT);
			XplatUIWin32.Win32SelectObject(intPtr2, intPtr3);
			XplatUIWin32.Win32DeleteObject(intPtr);
			XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr2);
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x0011D344 File Offset: 0x0011B544
		internal override void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width)
		{
			POINT point = default(POINT);
			point.x = 0;
			point.y = 0;
			XplatUIWin32.Win32ClientToScreen(handle, ref point);
			IntPtr intPtr = XplatUIWin32.Win32CreatePen(XplatUIWin32.PenStyle.PS_SOLID, line_width, IntPtr.Zero);
			IntPtr intPtr2 = XplatUIWin32.Win32GetDC(IntPtr.Zero);
			XplatUIWin32.Win32SetROP2(intPtr2, XplatUIWin32.ROP2DrawMode.R2_NOT);
			IntPtr intPtr3 = XplatUIWin32.Win32SelectObject(intPtr2, intPtr);
			Control control = Control.FromHandle(handle);
			if (control != null)
			{
				XplatUIWin32.RECT rect2;
				XplatUIWin32.Win32GetWindowRect(control.Handle, out rect2);
				Region region = new Region(new Rectangle(rect2.left, rect2.top, rect2.right - rect2.left, rect2.bottom - rect2.top));
				XplatUIWin32.Win32ExtSelectClipRgn(intPtr2, region.GetHrgn(Graphics.FromHdc(intPtr2)), 1);
			}
			XplatUIWin32.Win32MoveToEx(intPtr2, point.x + rect.Left, point.y + rect.Top, IntPtr.Zero);
			if (rect.Width > 0 && rect.Height > 0)
			{
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Right, point.y + rect.Top);
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Right, point.y + rect.Bottom);
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Left, point.y + rect.Bottom);
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Left, point.y + rect.Top);
			}
			else if (rect.Width > 0)
			{
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Right, point.y + rect.Top);
			}
			else
			{
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Left, point.y + rect.Bottom);
			}
			XplatUIWin32.Win32SelectObject(intPtr2, intPtr3);
			XplatUIWin32.Win32DeleteObject(intPtr);
			if (control != null)
			{
				XplatUIWin32.Win32ExtSelectClipRgn(intPtr2, IntPtr.Zero, 5);
			}
			XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr2);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x0011D570 File Offset: 0x0011B770
		internal override SizeF GetAutoScaleSize(Font font)
		{
			string text = "The quick brown fox jumped over the lazy dog.";
			double num = 44.54999694824219;
			Graphics graphics = Graphics.FromHwnd(XplatUIWin32.FosterParent);
			float num2 = (float)((double)graphics.MeasureString(text, font).Width / num);
			return new SizeF(num2, (float)font.Height);
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x0011D5BC File Offset: 0x0011B7BC
		internal override IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return XplatUIWin32.Win32SendMessage(hwnd, message, wParam, lParam);
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x0011D5C8 File Offset: 0x0011B7C8
		internal override bool PostMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return XplatUIWin32.Win32PostMessage(hwnd, message, wParam, lParam);
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x0011D5D4 File Offset: 0x0011B7D4
		internal override int SendInput(IntPtr hwnd, Queue keys)
		{
			XplatUIWin32.INPUT[] array = new XplatUIWin32.INPUT[keys.Count];
			int num = 0;
			while (keys.Count > 0)
			{
				MSG msg = (MSG)keys.Dequeue();
				array[num].ki.wScan = 0;
				array[num].ki.time = 0;
				array[num].ki.dwFlags = ((msg.message != Msg.WM_KEYUP) ? 0 : 2);
				array[num].ki.wVk = (short)msg.wParam.ToInt32();
				array[num].type = 1;
				num++;
			}
			return (int)XplatUIWin32.Win32SendInput((uint)array.Length, array, Marshal.SizeOf(typeof(XplatUIWin32.INPUT)));
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x0011D6A8 File Offset: 0x0011B8A8
		internal override int KeyboardSpeed
		{
			get
			{
				int num = 0;
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETKEYBOARDSPEED, 0U, ref num, 0U);
				return num;
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x0011D6C4 File Offset: 0x0011B8C4
		internal override int KeyboardDelay
		{
			get
			{
				int num = 1;
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETKEYBOARDDELAY, 0U, ref num, 0U);
				return num;
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x0011D6E0 File Offset: 0x0011B8E0
		internal override void CreateOffscreenDrawable(IntPtr handle, int width, int height, out object offscreen_drawable)
		{
			Graphics graphics = Graphics.FromHwnd(handle);
			IntPtr hdc = graphics.GetHdc();
			IntPtr intPtr = XplatUIWin32.Win32CreateCompatibleDC(hdc);
			IntPtr intPtr2 = XplatUIWin32.Win32CreateCompatibleBitmap(hdc, width, height);
			XplatUIWin32.Win32SelectObject(intPtr, intPtr2);
			offscreen_drawable = new XplatUIWin32.WinBuffer(intPtr, intPtr2);
			graphics.ReleaseHdc(hdc);
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x0011D724 File Offset: 0x0011B924
		internal override Graphics GetOffscreenGraphics(object offscreen_drawable)
		{
			return Graphics.FromHdc(((XplatUIWin32.WinBuffer)offscreen_drawable).hdc);
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x0011D738 File Offset: 0x0011B938
		internal override void BlitFromOffscreen(IntPtr dest_handle, Graphics dest_dc, object offscreen_drawable, Graphics offscreen_dc, Rectangle r)
		{
			XplatUIWin32.WinBuffer winBuffer = (XplatUIWin32.WinBuffer)offscreen_drawable;
			IntPtr hdc = dest_dc.GetHdc();
			XplatUIWin32.Win32BitBlt(hdc, r.Left, r.Top, r.Width, r.Height, winBuffer.hdc, r.Left, r.Top, XplatUIWin32.TernaryRasterOperations.SRCCOPY);
			dest_dc.ReleaseHdc(hdc);
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x0011D798 File Offset: 0x0011B998
		internal override void DestroyOffscreenDrawable(object offscreen_drawable)
		{
			XplatUIWin32.WinBuffer winBuffer = (XplatUIWin32.WinBuffer)offscreen_drawable;
			XplatUIWin32.Win32DeleteObject(winBuffer.bitmap);
			XplatUIWin32.Win32DeleteDC(winBuffer.hdc);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x0011D7C4 File Offset: 0x0011B9C4
		internal override void SetForegroundWindow(IntPtr handle)
		{
			XplatUIWin32.Win32SetForegroundWindow(handle);
		}

		// Token: 0x0600498F RID: 18831
		[DllImport("kernel32.dll", CallingConvention = 3, EntryPoint = "GetLastError")]
		private static extern uint Win32GetLastError();

		// Token: 0x06004990 RID: 18832
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "CreateWindowExW")]
		internal static extern IntPtr Win32CreateWindow(WindowExStyles dwExStyle, string lpClassName, string lpWindowName, WindowStyles dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lParam);

		// Token: 0x06004991 RID: 18833
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "DestroyWindow")]
		internal static extern bool Win32DestroyWindow(IntPtr hWnd);

		// Token: 0x06004992 RID: 18834
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "PeekMessageW")]
		internal static extern bool Win32PeekMessage(ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags);

		// Token: 0x06004993 RID: 18835
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "GetMessageW")]
		internal static extern bool Win32GetMessage(ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax);

		// Token: 0x06004994 RID: 18836
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "TranslateMessage")]
		internal static extern bool Win32TranslateMessage(ref MSG msg);

		// Token: 0x06004995 RID: 18837
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "DispatchMessageW")]
		internal static extern IntPtr Win32DispatchMessage(ref MSG msg);

		// Token: 0x06004996 RID: 18838
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "MoveWindow")]
		internal static extern bool Win32MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

		// Token: 0x06004997 RID: 18839
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetWindowPos")]
		internal static extern bool Win32SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, XplatUIWin32.SetWindowPosFlags Flags);

		// Token: 0x06004998 RID: 18840
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetWindowPos")]
		internal static extern bool Win32SetWindowPos(IntPtr hWnd, XplatUIWin32.SetWindowPosZOrder pos, int x, int y, int cx, int cy, XplatUIWin32.SetWindowPosFlags Flags);

		// Token: 0x06004999 RID: 18841
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "SetWindowTextW")]
		internal static extern bool Win32SetWindowText(IntPtr hWnd, string lpString);

		// Token: 0x0600499A RID: 18842
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "GetWindowTextW")]
		internal static extern bool Win32GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x0600499B RID: 18843
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetParent")]
		internal static extern IntPtr Win32SetParent(IntPtr hWnd, IntPtr hParent);

		// Token: 0x0600499C RID: 18844
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "RegisterClassW")]
		private static extern bool Win32RegisterClass(ref XplatUIWin32.WNDCLASS wndClass);

		// Token: 0x0600499D RID: 18845
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "LoadCursorW")]
		private static extern IntPtr Win32LoadCursor(IntPtr hInstance, XplatUIWin32.LoadCursorType type);

		// Token: 0x0600499E RID: 18846
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ShowCursor")]
		private static extern IntPtr Win32ShowCursor(bool bShow);

		// Token: 0x0600499F RID: 18847
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetCursor")]
		private static extern IntPtr Win32SetCursor(IntPtr hCursor);

		// Token: 0x060049A0 RID: 18848
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "CreateCursor")]
		private static extern IntPtr Win32CreateCursor(IntPtr hInstance, int xHotSpot, int yHotSpot, int nWidth, int nHeight, byte[] pvANDPlane, byte[] pvORPlane);

		// Token: 0x060049A1 RID: 18849
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "DestroyCursor")]
		private static extern bool Win32DestroyCursor(IntPtr hCursor);

		// Token: 0x060049A2 RID: 18850
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "DrawIcon")]
		private static extern bool Win32DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

		// Token: 0x060049A3 RID: 18851
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "DefWindowProcW")]
		private static extern IntPtr Win32DefWindowProc(IntPtr hWnd, Msg Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060049A4 RID: 18852
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "PostQuitMessage")]
		private static extern IntPtr Win32PostQuitMessage(int nExitCode);

		// Token: 0x060049A5 RID: 18853
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "UpdateWindow")]
		private static extern IntPtr Win32UpdateWindow(IntPtr hWnd);

		// Token: 0x060049A6 RID: 18854
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetUpdateRect")]
		private static extern bool Win32GetUpdateRect(IntPtr hWnd, ref XplatUIWin32.RECT rect, bool erase);

		// Token: 0x060049A7 RID: 18855
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "BeginPaint")]
		private static extern IntPtr Win32BeginPaint(IntPtr hWnd, ref XplatUIWin32.PAINTSTRUCT ps);

		// Token: 0x060049A8 RID: 18856
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ValidateRect")]
		private static extern IntPtr Win32ValidateRect(IntPtr hWnd, ref XplatUIWin32.RECT rect);

		// Token: 0x060049A9 RID: 18857
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "EndPaint")]
		private static extern bool Win32EndPaint(IntPtr hWnd, ref XplatUIWin32.PAINTSTRUCT ps);

		// Token: 0x060049AA RID: 18858
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetDC")]
		private static extern IntPtr Win32GetDC(IntPtr hWnd);

		// Token: 0x060049AB RID: 18859
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetWindowDC")]
		private static extern IntPtr Win32GetWindowDC(IntPtr hWnd);

		// Token: 0x060049AC RID: 18860
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ReleaseDC")]
		private static extern IntPtr Win32ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x060049AD RID: 18861
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "MessageBoxW")]
		private static extern IntPtr Win32MessageBox(IntPtr hParent, string pText, string pCaption, uint uType);

		// Token: 0x060049AE RID: 18862
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "InvalidateRect")]
		private static extern IntPtr Win32InvalidateRect(IntPtr hWnd, ref XplatUIWin32.RECT lpRect, bool bErase);

		// Token: 0x060049AF RID: 18863
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetCapture")]
		private static extern IntPtr Win32SetCapture(IntPtr hWnd);

		// Token: 0x060049B0 RID: 18864
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ReleaseCapture")]
		private static extern IntPtr Win32ReleaseCapture();

		// Token: 0x060049B1 RID: 18865
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetWindowRect")]
		private static extern IntPtr Win32GetWindowRect(IntPtr hWnd, out XplatUIWin32.RECT rect);

		// Token: 0x060049B2 RID: 18866
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetClientRect")]
		private static extern IntPtr Win32GetClientRect(IntPtr hWnd, out XplatUIWin32.RECT rect);

		// Token: 0x060049B3 RID: 18867
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ScreenToClient")]
		private static extern bool Win32ScreenToClient(IntPtr hWnd, ref POINT pt);

		// Token: 0x060049B4 RID: 18868
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ClientToScreen")]
		private static extern bool Win32ClientToScreen(IntPtr hWnd, ref POINT pt);

		// Token: 0x060049B5 RID: 18869
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetParent")]
		private static extern IntPtr Win32GetParent(IntPtr hWnd);

		// Token: 0x060049B6 RID: 18870
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetAncestor")]
		private static extern IntPtr Win32GetAncestor(IntPtr hWnd, XplatUIWin32.AncestorType flags);

		// Token: 0x060049B7 RID: 18871
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetActiveWindow")]
		private static extern IntPtr Win32SetActiveWindow(IntPtr hWnd);

		// Token: 0x060049B8 RID: 18872
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "AdjustWindowRectEx")]
		private static extern bool Win32AdjustWindowRectEx(ref XplatUIWin32.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

		// Token: 0x060049B9 RID: 18873
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetCursorPos")]
		private static extern bool Win32GetCursorPos(out POINT lpPoint);

		// Token: 0x060049BA RID: 18874
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetCursorPos")]
		private static extern bool Win32SetCursorPos(int x, int y);

		// Token: 0x060049BB RID: 18875
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "TrackMouseEvent")]
		private static extern bool Win32TrackMouseEvent(ref XplatUIWin32.TRACKMOUSEEVENT tme);

		// Token: 0x060049BC RID: 18876
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "CreateSolidBrush")]
		private static extern IntPtr Win32CreateSolidBrush(XplatUIWin32.COLORREF clrRef);

		// Token: 0x060049BD RID: 18877
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "PatBlt")]
		private static extern int Win32PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, XplatUIWin32.PatBltRop dwRop);

		// Token: 0x060049BE RID: 18878
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetWindowLong")]
		private static extern uint Win32SetWindowLong(IntPtr hwnd, XplatUIWin32.WindowLong index, uint value);

		// Token: 0x060049BF RID: 18879
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetWindowLong")]
		private static extern uint Win32GetWindowLong(IntPtr hwnd, XplatUIWin32.WindowLong index);

		// Token: 0x060049C0 RID: 18880
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetLayeredWindowAttributes")]
		private static extern uint Win32SetLayeredWindowAttributes(IntPtr hwnd, XplatUIWin32.COLORREF crKey, byte bAlpha, XplatUIWin32.LayeredWindowAttributes dwFlags);

		// Token: 0x060049C1 RID: 18881
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetLayeredWindowAttributes")]
		private static extern uint Win32GetLayeredWindowAttributes(IntPtr hwnd, out XplatUIWin32.COLORREF pcrKey, out byte pbAlpha, out XplatUIWin32.LayeredWindowAttributes pwdFlags);

		// Token: 0x060049C2 RID: 18882
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "DeleteObject")]
		public static extern bool Win32DeleteObject(IntPtr o);

		// Token: 0x060049C3 RID: 18883
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetKeyState")]
		private static extern short Win32GetKeyState(VirtualKeys nVirtKey);

		// Token: 0x060049C4 RID: 18884
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetDesktopWindow")]
		private static extern IntPtr Win32GetDesktopWindow();

		// Token: 0x060049C5 RID: 18885
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetTimer")]
		private static extern IntPtr Win32SetTimer(IntPtr hwnd, int nIDEvent, uint uElapse, IntPtr timerProc);

		// Token: 0x060049C6 RID: 18886
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "KillTimer")]
		private static extern IntPtr Win32KillTimer(IntPtr hwnd, int nIDEvent);

		// Token: 0x060049C7 RID: 18887
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ShowWindow")]
		private static extern IntPtr Win32ShowWindow(IntPtr hwnd, XplatUIWin32.WindowPlacementFlags nCmdShow);

		// Token: 0x060049C8 RID: 18888
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "EnableWindow")]
		private static extern IntPtr Win32EnableWindow(IntPtr hwnd, bool Enabled);

		// Token: 0x060049C9 RID: 18889
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetFocus")]
		internal static extern IntPtr Win32SetFocus(IntPtr hwnd);

		// Token: 0x060049CA RID: 18890
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetFocus")]
		internal static extern IntPtr Win32GetFocus();

		// Token: 0x060049CB RID: 18891
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "CreateCaret")]
		internal static extern bool Win32CreateCaret(IntPtr hwnd, IntPtr hBitmap, int nWidth, int nHeight);

		// Token: 0x060049CC RID: 18892
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "DestroyCaret")]
		private static extern bool Win32DestroyCaret();

		// Token: 0x060049CD RID: 18893
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ShowCaret")]
		private static extern bool Win32ShowCaret(IntPtr hwnd);

		// Token: 0x060049CE RID: 18894
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "HideCaret")]
		private static extern bool Win32HideCaret(IntPtr hwnd);

		// Token: 0x060049CF RID: 18895
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetCaretPos")]
		private static extern bool Win32SetCaretPos(int X, int Y);

		// Token: 0x060049D0 RID: 18896
		[DllImport("gdi32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "GetTextMetricsW")]
		internal static extern bool Win32GetTextMetrics(IntPtr hdc, ref XplatUIWin32.TEXTMETRIC tm);

		// Token: 0x060049D1 RID: 18897
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "SelectObject")]
		internal static extern IntPtr Win32SelectObject(IntPtr hdc, IntPtr hgdiobject);

		// Token: 0x060049D2 RID: 18898
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ScrollWindowEx")]
		private static extern bool Win32ScrollWindowEx(IntPtr hwnd, int dx, int dy, IntPtr prcScroll, ref XplatUIWin32.RECT prcClip, IntPtr hrgnUpdate, IntPtr prcUpdate, XplatUIWin32.ScrollWindowExFlags flags);

		// Token: 0x060049D3 RID: 18899
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ScrollWindowEx")]
		private static extern bool Win32ScrollWindowEx(IntPtr hwnd, int dx, int dy, IntPtr prcScroll, IntPtr prcClip, IntPtr hrgnUpdate, IntPtr prcUpdate, XplatUIWin32.ScrollWindowExFlags flags);

		// Token: 0x060049D4 RID: 18900
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetActiveWindow")]
		private static extern IntPtr Win32GetActiveWindow();

		// Token: 0x060049D5 RID: 18901
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetSystemMetrics")]
		private static extern int Win32GetSystemMetrics(XplatUIWin32.SystemMetrics nIndex);

		// Token: 0x060049D6 RID: 18902
		[DllImport("shell32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "Shell_NotifyIconW")]
		private static extern bool Win32Shell_NotifyIcon(XplatUIWin32.NotifyIconMessage dwMessage, ref XplatUIWin32.NOTIFYICONDATA lpData);

		// Token: 0x060049D7 RID: 18903
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "CreateRectRgn")]
		internal static extern IntPtr Win32CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		// Token: 0x060049D8 RID: 18904
		[DllImport("user32.dll", CallingConvention = 3)]
		private static extern bool IsWindowEnabled(IntPtr hwnd);

		// Token: 0x060049D9 RID: 18905
		[DllImport("user32.dll", CallingConvention = 3)]
		private static extern bool IsWindowVisible(IntPtr hwnd);

		// Token: 0x060049DA RID: 18906
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "SendMessageW")]
		private static extern IntPtr Win32SendMessage(IntPtr hwnd, Msg msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060049DB RID: 18907
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "PostMessageW")]
		private static extern bool Win32PostMessage(IntPtr hwnd, Msg msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060049DC RID: 18908
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "SendInput")]
		private static extern uint Win32SendInput(uint nInputs, [MarshalAs(42)] XplatUIWin32.INPUT[] inputs, int cbSize);

		// Token: 0x060049DD RID: 18909
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "SystemParametersInfoW")]
		private static extern bool Win32SystemParametersInfo(XplatUIWin32.SPIAction uiAction, uint uiParam, ref XplatUIWin32.RECT rect, uint fWinIni);

		// Token: 0x060049DE RID: 18910
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "SystemParametersInfoW")]
		private static extern bool Win32SystemParametersInfo(XplatUIWin32.SPIAction uiAction, uint uiParam, ref int value, uint fWinIni);

		// Token: 0x060049DF RID: 18911
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "SystemParametersInfoW")]
		private static extern bool Win32SystemParametersInfo(XplatUIWin32.SPIAction uiAction, uint uiParam, ref bool value, uint fWinIni);

		// Token: 0x060049E0 RID: 18912
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "SystemParametersInfoW")]
		private static extern bool Win32SystemParametersInfo(XplatUIWin32.SPIAction uiAction, uint uiParam, ref XplatUIWin32.ANIMATIONINFO value, uint fWinIni);

		// Token: 0x060049E1 RID: 18913
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "OpenClipboard")]
		private static extern bool Win32OpenClipboard(IntPtr hwnd);

		// Token: 0x060049E2 RID: 18914
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "EmptyClipboard")]
		private static extern bool Win32EmptyClipboard();

		// Token: 0x060049E3 RID: 18915
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "RegisterClipboardFormatW")]
		private static extern uint Win32RegisterClipboardFormat(string format);

		// Token: 0x060049E4 RID: 18916
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "CloseClipboard")]
		private static extern bool Win32CloseClipboard();

		// Token: 0x060049E5 RID: 18917
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "EnumClipboardFormats")]
		private static extern uint Win32EnumClipboardFormats(uint format);

		// Token: 0x060049E6 RID: 18918
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetClipboardData")]
		private static extern IntPtr Win32GetClipboardData(uint format);

		// Token: 0x060049E7 RID: 18919
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetClipboardData")]
		private static extern IntPtr Win32SetClipboardData(uint format, IntPtr handle);

		// Token: 0x060049E8 RID: 18920
		[DllImport("kernel32.dll", CallingConvention = 3, EntryPoint = "GlobalAlloc")]
		internal static extern IntPtr Win32GlobalAlloc(XplatUIWin32.GAllocFlags Flags, int dwBytes);

		// Token: 0x060049E9 RID: 18921
		[DllImport("kernel32.dll", CallingConvention = 3, EntryPoint = "CopyMemory")]
		internal static extern void Win32CopyMemory(IntPtr Destination, IntPtr Source, int length);

		// Token: 0x060049EA RID: 18922
		[DllImport("kernel32.dll", CallingConvention = 3, EntryPoint = "GlobalFree")]
		internal static extern IntPtr Win32GlobalFree(IntPtr hMem);

		// Token: 0x060049EB RID: 18923
		[DllImport("kernel32.dll", CallingConvention = 3, EntryPoint = "GlobalSize")]
		internal static extern uint Win32GlobalSize(IntPtr hMem);

		// Token: 0x060049EC RID: 18924
		[DllImport("kernel32.dll", CallingConvention = 3, EntryPoint = "GlobalLock")]
		internal static extern IntPtr Win32GlobalLock(IntPtr hMem);

		// Token: 0x060049ED RID: 18925
		[DllImport("kernel32.dll", CallingConvention = 3, EntryPoint = "GlobalUnlock")]
		internal static extern IntPtr Win32GlobalUnlock(IntPtr hMem);

		// Token: 0x060049EE RID: 18926
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "SetROP2")]
		internal static extern int Win32SetROP2(IntPtr hdc, XplatUIWin32.ROP2DrawMode fnDrawMode);

		// Token: 0x060049EF RID: 18927
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "MoveToEx")]
		internal static extern bool Win32MoveToEx(IntPtr hdc, int x, int y, ref POINT lpPoint);

		// Token: 0x060049F0 RID: 18928
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "MoveToEx")]
		internal static extern bool Win32MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

		// Token: 0x060049F1 RID: 18929
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "LineTo")]
		internal static extern bool Win32LineTo(IntPtr hdc, int x, int y);

		// Token: 0x060049F2 RID: 18930
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "CreatePen")]
		internal static extern IntPtr Win32CreatePen(XplatUIWin32.PenStyle fnPenStyle, int nWidth, ref XplatUIWin32.COLORREF color);

		// Token: 0x060049F3 RID: 18931
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "CreatePen")]
		internal static extern IntPtr Win32CreatePen(XplatUIWin32.PenStyle fnPenStyle, int nWidth, IntPtr color);

		// Token: 0x060049F4 RID: 18932
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "GetStockObject")]
		internal static extern IntPtr Win32GetStockObject(XplatUIWin32.StockObject fnObject);

		// Token: 0x060049F5 RID: 18933
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "CreateHatchBrush")]
		internal static extern IntPtr Win32CreateHatchBrush(XplatUIWin32.HatchStyle fnStyle, IntPtr color);

		// Token: 0x060049F6 RID: 18934
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "CreateHatchBrush")]
		internal static extern IntPtr Win32CreateHatchBrush(XplatUIWin32.HatchStyle fnStyle, ref XplatUIWin32.COLORREF color);

		// Token: 0x060049F7 RID: 18935
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "ExcludeClipRect")]
		internal static extern int Win32ExcludeClipRect(IntPtr hdc, int left, int top, int right, int bottom);

		// Token: 0x060049F8 RID: 18936
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "ExtSelectClipRgn")]
		internal static extern int Win32ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int mode);

		// Token: 0x060049F9 RID: 18937
		[DllImport("winmm.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "PlaySoundW")]
		internal static extern IntPtr Win32PlaySound(string pszSound, IntPtr hmod, XplatUIWin32.SndFlags fdwSound);

		// Token: 0x060049FA RID: 18938
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "GetDoubleClickTime")]
		private static extern int Win32GetDoubleClickTime();

		// Token: 0x060049FB RID: 18939
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "SetWindowRgn")]
		internal static extern int Win32SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

		// Token: 0x060049FC RID: 18940
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3, EntryPoint = "GetWindowRgn")]
		internal static extern IntPtr Win32GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

		// Token: 0x060049FD RID: 18941
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "ClipCursor")]
		internal static extern bool Win32ClipCursor(ref XplatUIWin32.RECT lpRect);

		// Token: 0x060049FE RID: 18942
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetClipCursor")]
		internal static extern bool Win32GetClipCursor(out XplatUIWin32.RECT lpRect);

		// Token: 0x060049FF RID: 18943
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "BitBlt")]
		internal static extern bool Win32BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, XplatUIWin32.TernaryRasterOperations dwRop);

		// Token: 0x06004A00 RID: 18944
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr Win32CreateCompatibleDC(IntPtr hdc);

		// Token: 0x06004A01 RID: 18945
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
		internal static extern bool Win32DeleteDC(IntPtr hdc);

		// Token: 0x06004A02 RID: 18946
		[DllImport("gdi32.dll", CallingConvention = 3, EntryPoint = "CreateCompatibleBitmap")]
		internal static extern IntPtr Win32CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		// Token: 0x06004A03 RID: 18947
		[DllImport("kernel32.dll", CallingConvention = 3, EntryPoint = "GetSystemPowerStatus")]
		internal static extern bool Win32GetSystemPowerStatus(XplatUIWin32.SYSTEMPOWERSTATUS sps);

		// Token: 0x06004A04 RID: 18948
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "GetIconInfo")]
		internal static extern bool Win32GetIconInfo(IntPtr hIcon, out XplatUIWin32.ICONINFO piconinfo);

		// Token: 0x06004A05 RID: 18949
		[DllImport("user32.dll", CallingConvention = 3, EntryPoint = "SetForegroundWindow")]
		private static extern bool Win32SetForegroundWindow(IntPtr hWnd);

		// Token: 0x040025BB RID: 9659
		private static XplatUIWin32 instance;

		// Token: 0x040025BC RID: 9660
		private static int ref_count;

		// Token: 0x040025BD RID: 9661
		private static IntPtr FosterParent;

		// Token: 0x040025BE RID: 9662
		internal static MouseButtons mouse_state;

		// Token: 0x040025BF RID: 9663
		internal static Point mouse_position;

		// Token: 0x040025C0 RID: 9664
		internal static bool grab_confined;

		// Token: 0x040025C1 RID: 9665
		internal static IntPtr grab_hwnd;

		// Token: 0x040025C2 RID: 9666
		internal static Rectangle grab_area;

		// Token: 0x040025C3 RID: 9667
		internal static XplatUIDriver.WndProc wnd_proc;

		// Token: 0x040025C4 RID: 9668
		internal static IntPtr prev_mouse_hwnd;

		// Token: 0x040025C5 RID: 9669
		internal static bool caret_visible;

		// Token: 0x040025C6 RID: 9670
		internal static bool themes_enabled;

		// Token: 0x040025C7 RID: 9671
		private Hashtable timer_list;

		// Token: 0x040025C8 RID: 9672
		private static Queue message_queue;

		// Token: 0x040025C9 RID: 9673
		private static IntPtr clip_magic = new IntPtr(27051977);

		// Token: 0x040025CA RID: 9674
		private static int scroll_width;

		// Token: 0x040025CB RID: 9675
		private static int scroll_height;

		// Token: 0x040025CC RID: 9676
		private static Hashtable wm_nc_registered;

		// Token: 0x040025CD RID: 9677
		private static XplatUIWin32.RECT clipped_cursor_rect;

		// Token: 0x040025CE RID: 9678
		private Hashtable registered_classes;

		// Token: 0x040025CF RID: 9679
		private Hwnd HwndCreating;

		// Token: 0x040025D0 RID: 9680
		private TransparencySupport support;

		// Token: 0x040025D1 RID: 9681
		private bool queried_transparency_support;

		// Token: 0x02000469 RID: 1129
		[StructLayout(0, CharSet = 3)]
		private struct WNDCLASS
		{
			// Token: 0x040025D3 RID: 9683
			internal int style;

			// Token: 0x040025D4 RID: 9684
			internal XplatUIDriver.WndProc lpfnWndProc;

			// Token: 0x040025D5 RID: 9685
			internal int cbClsExtra;

			// Token: 0x040025D6 RID: 9686
			internal int cbWndExtra;

			// Token: 0x040025D7 RID: 9687
			internal IntPtr hInstance;

			// Token: 0x040025D8 RID: 9688
			internal IntPtr hIcon;

			// Token: 0x040025D9 RID: 9689
			internal IntPtr hCursor;

			// Token: 0x040025DA RID: 9690
			internal IntPtr hbrBackground;

			// Token: 0x040025DB RID: 9691
			[MarshalAs(21)]
			internal string lpszMenuName;

			// Token: 0x040025DC RID: 9692
			[MarshalAs(21)]
			internal string lpszClassName;
		}

		// Token: 0x0200046A RID: 1130
		internal struct RECT
		{
			// Token: 0x06004A06 RID: 18950 RVA: 0x0011D7D0 File Offset: 0x0011B9D0
			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			// Token: 0x170012E3 RID: 4835
			// (get) Token: 0x06004A07 RID: 18951 RVA: 0x0011D7F0 File Offset: 0x0011B9F0
			public int Height
			{
				get
				{
					return this.bottom - this.top;
				}
			}

			// Token: 0x170012E4 RID: 4836
			// (get) Token: 0x06004A08 RID: 18952 RVA: 0x0011D800 File Offset: 0x0011BA00
			public int Width
			{
				get
				{
					return this.right - this.left;
				}
			}

			// Token: 0x170012E5 RID: 4837
			// (get) Token: 0x06004A09 RID: 18953 RVA: 0x0011D810 File Offset: 0x0011BA10
			public Size Size
			{
				get
				{
					return new Size(this.Width, this.Height);
				}
			}

			// Token: 0x170012E6 RID: 4838
			// (get) Token: 0x06004A0A RID: 18954 RVA: 0x0011D824 File Offset: 0x0011BA24
			public Point Location
			{
				get
				{
					return new Point(this.left, this.top);
				}
			}

			// Token: 0x06004A0B RID: 18955 RVA: 0x0011D838 File Offset: 0x0011BA38
			public Rectangle ToRectangle()
			{
				return Rectangle.FromLTRB(this.left, this.top, this.right, this.bottom);
			}

			// Token: 0x06004A0C RID: 18956 RVA: 0x0011D858 File Offset: 0x0011BA58
			public static XplatUIWin32.RECT FromRectangle(Rectangle rectangle)
			{
				return new XplatUIWin32.RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
			}

			// Token: 0x06004A0D RID: 18957 RVA: 0x0011D888 File Offset: 0x0011BA88
			public override int GetHashCode()
			{
				return this.left ^ ((this.top << 13) | (this.top >> 19)) ^ ((this.Width << 26) | (this.Width >> 6)) ^ ((this.Height << 7) | (this.Height >> 25));
			}

			// Token: 0x06004A0E RID: 18958 RVA: 0x0011D8D8 File Offset: 0x0011BAD8
			public override string ToString()
			{
				return string.Format("RECT left={0}, top={1}, right={2}, bottom={3}, width={4}, height={5}", new object[]
				{
					this.left,
					this.top,
					this.right,
					this.bottom,
					this.right - this.left,
					this.bottom - this.top
				});
			}

			// Token: 0x06004A0F RID: 18959 RVA: 0x0011D958 File Offset: 0x0011BB58
			public static implicit operator Rectangle(XplatUIWin32.RECT rect)
			{
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}

			// Token: 0x06004A10 RID: 18960 RVA: 0x0011D97C File Offset: 0x0011BB7C
			public static implicit operator XplatUIWin32.RECT(Rectangle rect)
			{
				return new XplatUIWin32.RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
			}

			// Token: 0x040025DD RID: 9693
			internal int left;

			// Token: 0x040025DE RID: 9694
			internal int top;

			// Token: 0x040025DF RID: 9695
			internal int right;

			// Token: 0x040025E0 RID: 9696
			internal int bottom;
		}

		// Token: 0x0200046B RID: 1131
		internal enum SPIAction
		{
			// Token: 0x040025E2 RID: 9698
			SPI_GETACTIVEWINDOWTRACKING = 4096,
			// Token: 0x040025E3 RID: 9699
			SPI_GETACTIVEWNDTRKTIMEOUT = 8194,
			// Token: 0x040025E4 RID: 9700
			SPI_GETANIMATION = 72,
			// Token: 0x040025E5 RID: 9701
			SPI_GETCARETWIDTH = 8198,
			// Token: 0x040025E6 RID: 9702
			SPI_GETCOMBOBOXANIMATION = 4100,
			// Token: 0x040025E7 RID: 9703
			SPI_GETDRAGFULLWINDOWS = 38,
			// Token: 0x040025E8 RID: 9704
			SPI_GETDROPSHADOW = 4132,
			// Token: 0x040025E9 RID: 9705
			SPI_GETFONTSMOOTHING = 74,
			// Token: 0x040025EA RID: 9706
			SPI_GETFONTSMOOTHINGCONTRAST = 8204,
			// Token: 0x040025EB RID: 9707
			SPI_GETFONTSMOOTHINGTYPE = 8202,
			// Token: 0x040025EC RID: 9708
			SPI_GETGRADIENTCAPTIONS = 4104,
			// Token: 0x040025ED RID: 9709
			SPI_GETHOTTRACKING = 4110,
			// Token: 0x040025EE RID: 9710
			SPI_GETICONTITLEWRAP = 25,
			// Token: 0x040025EF RID: 9711
			SPI_GETKEYBOARDSPEED = 10,
			// Token: 0x040025F0 RID: 9712
			SPI_GETKEYBOARDDELAY = 22,
			// Token: 0x040025F1 RID: 9713
			SPI_GETKEYBOARDCUES = 4106,
			// Token: 0x040025F2 RID: 9714
			SPI_GETKEYBOARDPREF = 68,
			// Token: 0x040025F3 RID: 9715
			SPI_GETLISTBOXSMOOTHSCROLLING = 4102,
			// Token: 0x040025F4 RID: 9716
			SPI_GETMENUANIMATION = 4098,
			// Token: 0x040025F5 RID: 9717
			SPI_GETMENUDROPALIGNMENT = 27,
			// Token: 0x040025F6 RID: 9718
			SPI_GETMENUFADE = 4114,
			// Token: 0x040025F7 RID: 9719
			SPI_GETMENUSHOWDELAY = 106,
			// Token: 0x040025F8 RID: 9720
			SPI_GETMOUSESPEED = 112,
			// Token: 0x040025F9 RID: 9721
			SPI_GETSELECTIONFADE = 4116,
			// Token: 0x040025FA RID: 9722
			SPI_GETSNAPTODEFBUTTON = 95,
			// Token: 0x040025FB RID: 9723
			SPI_GETTOOLTIPANIMATION = 4118,
			// Token: 0x040025FC RID: 9724
			SPI_GETWORKAREA = 48,
			// Token: 0x040025FD RID: 9725
			SPI_GETMOUSEHOVERWIDTH = 98,
			// Token: 0x040025FE RID: 9726
			SPI_GETMOUSEHOVERHEIGHT = 100,
			// Token: 0x040025FF RID: 9727
			SPI_GETMOUSEHOVERTIME = 102,
			// Token: 0x04002600 RID: 9728
			SPI_GETUIEFFECTS = 4158,
			// Token: 0x04002601 RID: 9729
			SPI_GETWHEELSCROLLLINES = 104
		}

		// Token: 0x0200046C RID: 1132
		internal enum WindowPlacementFlags
		{
			// Token: 0x04002603 RID: 9731
			SW_HIDE,
			// Token: 0x04002604 RID: 9732
			SW_SHOWNORMAL,
			// Token: 0x04002605 RID: 9733
			SW_NORMAL = 1,
			// Token: 0x04002606 RID: 9734
			SW_SHOWMINIMIZED,
			// Token: 0x04002607 RID: 9735
			SW_SHOWMAXIMIZED,
			// Token: 0x04002608 RID: 9736
			SW_MAXIMIZE = 3,
			// Token: 0x04002609 RID: 9737
			SW_SHOWNOACTIVATE,
			// Token: 0x0400260A RID: 9738
			SW_SHOW,
			// Token: 0x0400260B RID: 9739
			SW_MINIMIZE,
			// Token: 0x0400260C RID: 9740
			SW_SHOWMINNOACTIVE,
			// Token: 0x0400260D RID: 9741
			SW_SHOWNA,
			// Token: 0x0400260E RID: 9742
			SW_RESTORE,
			// Token: 0x0400260F RID: 9743
			SW_SHOWDEFAULT,
			// Token: 0x04002610 RID: 9744
			SW_FORCEMINIMIZE,
			// Token: 0x04002611 RID: 9745
			SW_MAX = 11
		}

		// Token: 0x0200046D RID: 1133
		private struct WINDOWPLACEMENT
		{
			// Token: 0x04002612 RID: 9746
			internal uint length;

			// Token: 0x04002613 RID: 9747
			internal uint flags;

			// Token: 0x04002614 RID: 9748
			internal XplatUIWin32.WindowPlacementFlags showCmd;

			// Token: 0x04002615 RID: 9749
			internal POINT ptMinPosition;

			// Token: 0x04002616 RID: 9750
			internal POINT ptMaxPosition;

			// Token: 0x04002617 RID: 9751
			internal XplatUIWin32.RECT rcNormalPosition;
		}

		// Token: 0x0200046E RID: 1134
		internal struct NCCALCSIZE_PARAMS
		{
			// Token: 0x04002618 RID: 9752
			internal XplatUIWin32.RECT rgrc1;

			// Token: 0x04002619 RID: 9753
			internal XplatUIWin32.RECT rgrc2;

			// Token: 0x0400261A RID: 9754
			internal XplatUIWin32.RECT rgrc3;

			// Token: 0x0400261B RID: 9755
			internal IntPtr lppos;
		}

		// Token: 0x0200046F RID: 1135
		[Flags]
		private enum TMEFlags
		{
			// Token: 0x0400261D RID: 9757
			TME_HOVER = 1,
			// Token: 0x0400261E RID: 9758
			TME_LEAVE = 2,
			// Token: 0x0400261F RID: 9759
			TME_NONCLIENT = 16,
			// Token: 0x04002620 RID: 9760
			TME_QUERY = 1073741824,
			// Token: 0x04002621 RID: 9761
			TME_CANCEL = -2147483648
		}

		// Token: 0x02000470 RID: 1136
		private struct TRACKMOUSEEVENT
		{
			// Token: 0x04002622 RID: 9762
			internal int size;

			// Token: 0x04002623 RID: 9763
			internal XplatUIWin32.TMEFlags dwFlags;

			// Token: 0x04002624 RID: 9764
			internal IntPtr hWnd;

			// Token: 0x04002625 RID: 9765
			internal int dwHoverTime;
		}

		// Token: 0x02000471 RID: 1137
		private struct PAINTSTRUCT
		{
			// Token: 0x04002626 RID: 9766
			internal IntPtr hdc;

			// Token: 0x04002627 RID: 9767
			internal int fErase;

			// Token: 0x04002628 RID: 9768
			internal XplatUIWin32.RECT rcPaint;

			// Token: 0x04002629 RID: 9769
			internal int fRestore;

			// Token: 0x0400262A RID: 9770
			internal int fIncUpdate;

			// Token: 0x0400262B RID: 9771
			internal int Reserved1;

			// Token: 0x0400262C RID: 9772
			internal int Reserved2;

			// Token: 0x0400262D RID: 9773
			internal int Reserved3;

			// Token: 0x0400262E RID: 9774
			internal int Reserved4;

			// Token: 0x0400262F RID: 9775
			internal int Reserved5;

			// Token: 0x04002630 RID: 9776
			internal int Reserved6;

			// Token: 0x04002631 RID: 9777
			internal int Reserved7;

			// Token: 0x04002632 RID: 9778
			internal int Reserved8;
		}

		// Token: 0x02000472 RID: 1138
		internal struct KEYBDINPUT
		{
			// Token: 0x04002633 RID: 9779
			internal short wVk;

			// Token: 0x04002634 RID: 9780
			internal short wScan;

			// Token: 0x04002635 RID: 9781
			internal int dwFlags;

			// Token: 0x04002636 RID: 9782
			internal int time;

			// Token: 0x04002637 RID: 9783
			internal UIntPtr dwExtraInfo;
		}

		// Token: 0x02000473 RID: 1139
		internal struct MOUSEINPUT
		{
			// Token: 0x04002638 RID: 9784
			internal int dx;

			// Token: 0x04002639 RID: 9785
			internal int dy;

			// Token: 0x0400263A RID: 9786
			internal int mouseData;

			// Token: 0x0400263B RID: 9787
			internal int dwFlags;

			// Token: 0x0400263C RID: 9788
			internal int time;

			// Token: 0x0400263D RID: 9789
			internal UIntPtr dwExtraInfo;
		}

		// Token: 0x02000474 RID: 1140
		internal struct HARDWAREINPUT
		{
			// Token: 0x0400263E RID: 9790
			internal int uMsg;

			// Token: 0x0400263F RID: 9791
			internal short wParamL;

			// Token: 0x04002640 RID: 9792
			internal short wParamH;
		}

		// Token: 0x02000475 RID: 1141
		internal struct ICONINFO
		{
			// Token: 0x04002641 RID: 9793
			internal bool fIcon;

			// Token: 0x04002642 RID: 9794
			internal int xHotspot;

			// Token: 0x04002643 RID: 9795
			internal int yHotspot;

			// Token: 0x04002644 RID: 9796
			internal IntPtr hbmMask;

			// Token: 0x04002645 RID: 9797
			internal IntPtr hbmColor;
		}

		// Token: 0x02000476 RID: 1142
		[StructLayout(2)]
		internal struct INPUT
		{
			// Token: 0x04002646 RID: 9798
			[FieldOffset(0)]
			internal int type;

			// Token: 0x04002647 RID: 9799
			[FieldOffset(4)]
			internal XplatUIWin32.MOUSEINPUT mi;

			// Token: 0x04002648 RID: 9800
			[FieldOffset(4)]
			internal XplatUIWin32.KEYBDINPUT ki;

			// Token: 0x04002649 RID: 9801
			[FieldOffset(4)]
			internal XplatUIWin32.HARDWAREINPUT hi;
		}

		// Token: 0x02000477 RID: 1143
		public struct ANIMATIONINFO
		{
			// Token: 0x0400264A RID: 9802
			internal uint cbSize;

			// Token: 0x0400264B RID: 9803
			internal int iMinAnimate;
		}

		// Token: 0x02000478 RID: 1144
		internal enum InputFlags
		{
			// Token: 0x0400264D RID: 9805
			KEYEVENTF_EXTENDEDKEY = 1,
			// Token: 0x0400264E RID: 9806
			KEYEVENTF_KEYUP,
			// Token: 0x0400264F RID: 9807
			KEYEVENTF_SCANCODE,
			// Token: 0x04002650 RID: 9808
			KEYEVENTF_UNICODE
		}

		// Token: 0x02000479 RID: 1145
		internal enum ClassStyle
		{
			// Token: 0x04002652 RID: 9810
			CS_VREDRAW = 1,
			// Token: 0x04002653 RID: 9811
			CS_HREDRAW,
			// Token: 0x04002654 RID: 9812
			CS_KEYCVTWINDOW = 4,
			// Token: 0x04002655 RID: 9813
			CS_DBLCLKS = 8,
			// Token: 0x04002656 RID: 9814
			CS_OWNDC = 32,
			// Token: 0x04002657 RID: 9815
			CS_CLASSDC = 64,
			// Token: 0x04002658 RID: 9816
			CS_PARENTDC = 128,
			// Token: 0x04002659 RID: 9817
			CS_NOKEYCVT = 256,
			// Token: 0x0400265A RID: 9818
			CS_NOCLOSE = 512,
			// Token: 0x0400265B RID: 9819
			CS_SAVEBITS = 2048,
			// Token: 0x0400265C RID: 9820
			CS_BYTEALIGNCLIENT = 4096,
			// Token: 0x0400265D RID: 9821
			CS_BYTEALIGNWINDOW = 8192,
			// Token: 0x0400265E RID: 9822
			CS_GLOBALCLASS = 16384,
			// Token: 0x0400265F RID: 9823
			CS_IME = 65536,
			// Token: 0x04002660 RID: 9824
			CS_DROPSHADOW = 131072
		}

		// Token: 0x0200047A RID: 1146
		internal enum SetWindowPosZOrder
		{
			// Token: 0x04002662 RID: 9826
			HWND_TOP,
			// Token: 0x04002663 RID: 9827
			HWND_BOTTOM,
			// Token: 0x04002664 RID: 9828
			HWND_TOPMOST = -1,
			// Token: 0x04002665 RID: 9829
			HWND_NOTOPMOST = -2
		}

		// Token: 0x0200047B RID: 1147
		[Flags]
		internal enum SetWindowPosFlags
		{
			// Token: 0x04002667 RID: 9831
			SWP_ASYNCWINDOWPOS = 16384,
			// Token: 0x04002668 RID: 9832
			SWP_DEFERERASE = 8192,
			// Token: 0x04002669 RID: 9833
			SWP_DRAWFRAME = 32,
			// Token: 0x0400266A RID: 9834
			SWP_FRAMECHANGED = 32,
			// Token: 0x0400266B RID: 9835
			SWP_HIDEWINDOW = 128,
			// Token: 0x0400266C RID: 9836
			SWP_NOACTIVATE = 16,
			// Token: 0x0400266D RID: 9837
			SWP_NOCOPYBITS = 256,
			// Token: 0x0400266E RID: 9838
			SWP_NOMOVE = 2,
			// Token: 0x0400266F RID: 9839
			SWP_NOOWNERZORDER = 512,
			// Token: 0x04002670 RID: 9840
			SWP_NOREDRAW = 8,
			// Token: 0x04002671 RID: 9841
			SWP_NOREPOSITION = 512,
			// Token: 0x04002672 RID: 9842
			SWP_NOENDSCHANGING = 1024,
			// Token: 0x04002673 RID: 9843
			SWP_NOSIZE = 1,
			// Token: 0x04002674 RID: 9844
			SWP_NOZORDER = 4,
			// Token: 0x04002675 RID: 9845
			SWP_SHOWWINDOW = 64
		}

		// Token: 0x0200047C RID: 1148
		internal enum GetSysColorIndex
		{
			// Token: 0x04002677 RID: 9847
			COLOR_SCROLLBAR,
			// Token: 0x04002678 RID: 9848
			COLOR_BACKGROUND,
			// Token: 0x04002679 RID: 9849
			COLOR_ACTIVECAPTION,
			// Token: 0x0400267A RID: 9850
			COLOR_INACTIVECAPTION,
			// Token: 0x0400267B RID: 9851
			COLOR_MENU,
			// Token: 0x0400267C RID: 9852
			COLOR_WINDOW,
			// Token: 0x0400267D RID: 9853
			COLOR_WINDOWFRAME,
			// Token: 0x0400267E RID: 9854
			COLOR_MENUTEXT,
			// Token: 0x0400267F RID: 9855
			COLOR_WINDOWTEXT,
			// Token: 0x04002680 RID: 9856
			COLOR_CAPTIONTEXT,
			// Token: 0x04002681 RID: 9857
			COLOR_ACTIVEBORDER,
			// Token: 0x04002682 RID: 9858
			COLOR_INACTIVEBORDER,
			// Token: 0x04002683 RID: 9859
			COLOR_APPWORKSPACE,
			// Token: 0x04002684 RID: 9860
			COLOR_HIGHLIGHT,
			// Token: 0x04002685 RID: 9861
			COLOR_HIGHLIGHTTEXT,
			// Token: 0x04002686 RID: 9862
			COLOR_BTNFACE,
			// Token: 0x04002687 RID: 9863
			COLOR_BTNSHADOW,
			// Token: 0x04002688 RID: 9864
			COLOR_GRAYTEXT,
			// Token: 0x04002689 RID: 9865
			COLOR_BTNTEXT,
			// Token: 0x0400268A RID: 9866
			COLOR_INACTIVECAPTIONTEXT,
			// Token: 0x0400268B RID: 9867
			COLOR_BTNHIGHLIGHT,
			// Token: 0x0400268C RID: 9868
			COLOR_3DDKSHADOW,
			// Token: 0x0400268D RID: 9869
			COLOR_3DLIGHT,
			// Token: 0x0400268E RID: 9870
			COLOR_INFOTEXT,
			// Token: 0x0400268F RID: 9871
			COLOR_INFOBK,
			// Token: 0x04002690 RID: 9872
			COLOR_HOTLIGHT = 26,
			// Token: 0x04002691 RID: 9873
			COLOR_GRADIENTACTIVECAPTION,
			// Token: 0x04002692 RID: 9874
			COLOR_GRADIENTINACTIVECAPTION,
			// Token: 0x04002693 RID: 9875
			COLOR_MENUHIGHLIGHT,
			// Token: 0x04002694 RID: 9876
			COLOR_MENUBAR,
			// Token: 0x04002695 RID: 9877
			COLOR_DESKTOP = 1,
			// Token: 0x04002696 RID: 9878
			COLOR_3DFACE = 16,
			// Token: 0x04002697 RID: 9879
			COLOR_3DSHADOW = 16,
			// Token: 0x04002698 RID: 9880
			COLOR_3DHIGHLIGHT = 20,
			// Token: 0x04002699 RID: 9881
			COLOR_3DHILIGHT = 20,
			// Token: 0x0400269A RID: 9882
			COLOR_BTNHILIGHT = 20,
			// Token: 0x0400269B RID: 9883
			COLOR_MAXVALUE = 24
		}

		// Token: 0x0200047D RID: 1149
		private enum LoadCursorType
		{
			// Token: 0x0400269D RID: 9885
			First = 32512,
			// Token: 0x0400269E RID: 9886
			IDC_ARROW = 32512,
			// Token: 0x0400269F RID: 9887
			IDC_IBEAM,
			// Token: 0x040026A0 RID: 9888
			IDC_WAIT,
			// Token: 0x040026A1 RID: 9889
			IDC_CROSS,
			// Token: 0x040026A2 RID: 9890
			IDC_UPARROW,
			// Token: 0x040026A3 RID: 9891
			IDC_SIZE = 32640,
			// Token: 0x040026A4 RID: 9892
			IDC_ICON,
			// Token: 0x040026A5 RID: 9893
			IDC_SIZENWSE,
			// Token: 0x040026A6 RID: 9894
			IDC_SIZENESW,
			// Token: 0x040026A7 RID: 9895
			IDC_SIZEWE,
			// Token: 0x040026A8 RID: 9896
			IDC_SIZENS,
			// Token: 0x040026A9 RID: 9897
			IDC_SIZEALL,
			// Token: 0x040026AA RID: 9898
			IDC_NO = 32648,
			// Token: 0x040026AB RID: 9899
			IDC_HAND,
			// Token: 0x040026AC RID: 9900
			IDC_APPSTARTING,
			// Token: 0x040026AD RID: 9901
			IDC_HELP,
			// Token: 0x040026AE RID: 9902
			Last = 32651
		}

		// Token: 0x0200047E RID: 1150
		private enum AncestorType
		{
			// Token: 0x040026B0 RID: 9904
			GA_PARENT = 1,
			// Token: 0x040026B1 RID: 9905
			GA_ROOT,
			// Token: 0x040026B2 RID: 9906
			GA_ROOTOWNER
		}

		// Token: 0x0200047F RID: 1151
		[Flags]
		private enum WindowLong
		{
			// Token: 0x040026B4 RID: 9908
			GWL_WNDPROC = -4,
			// Token: 0x040026B5 RID: 9909
			GWL_HINSTANCE = -6,
			// Token: 0x040026B6 RID: 9910
			GWL_HWNDPARENT = -8,
			// Token: 0x040026B7 RID: 9911
			GWL_STYLE = -16,
			// Token: 0x040026B8 RID: 9912
			GWL_EXSTYLE = -20,
			// Token: 0x040026B9 RID: 9913
			GWL_USERDATA = -21,
			// Token: 0x040026BA RID: 9914
			GWL_ID = -12
		}

		// Token: 0x02000480 RID: 1152
		[Flags]
		private enum LogBrushStyle
		{
			// Token: 0x040026BC RID: 9916
			BS_SOLID = 0,
			// Token: 0x040026BD RID: 9917
			BS_NULL = 1,
			// Token: 0x040026BE RID: 9918
			BS_HATCHED = 2,
			// Token: 0x040026BF RID: 9919
			BS_PATTERN = 3,
			// Token: 0x040026C0 RID: 9920
			BS_INDEXED = 4,
			// Token: 0x040026C1 RID: 9921
			BS_DIBPATTERN = 5,
			// Token: 0x040026C2 RID: 9922
			BS_DIBPATTERNPT = 6,
			// Token: 0x040026C3 RID: 9923
			BS_PATTERN8X8 = 7,
			// Token: 0x040026C4 RID: 9924
			BS_DIBPATTERN8X8 = 8,
			// Token: 0x040026C5 RID: 9925
			BS_MONOPATTERN = 9
		}

		// Token: 0x02000481 RID: 1153
		[Flags]
		private enum LogBrushHatch
		{
			// Token: 0x040026C7 RID: 9927
			HS_HORIZONTAL = 0,
			// Token: 0x040026C8 RID: 9928
			HS_VERTICAL = 1,
			// Token: 0x040026C9 RID: 9929
			HS_FDIAGONAL = 2,
			// Token: 0x040026CA RID: 9930
			HS_BDIAGONAL = 3,
			// Token: 0x040026CB RID: 9931
			HS_CROSS = 4,
			// Token: 0x040026CC RID: 9932
			HS_DIAGCROSS = 5
		}

		// Token: 0x02000482 RID: 1154
		internal struct COLORREF
		{
			// Token: 0x040026CD RID: 9933
			internal byte R;

			// Token: 0x040026CE RID: 9934
			internal byte G;

			// Token: 0x040026CF RID: 9935
			internal byte B;

			// Token: 0x040026D0 RID: 9936
			internal byte A;
		}

		// Token: 0x02000483 RID: 1155
		private struct LOGBRUSH
		{
			// Token: 0x040026D1 RID: 9937
			internal XplatUIWin32.LogBrushStyle lbStyle;

			// Token: 0x040026D2 RID: 9938
			internal XplatUIWin32.COLORREF lbColor;

			// Token: 0x040026D3 RID: 9939
			internal XplatUIWin32.LogBrushHatch lbHatch;
		}

		// Token: 0x02000484 RID: 1156
		internal struct TEXTMETRIC
		{
			// Token: 0x040026D4 RID: 9940
			internal int tmHeight;

			// Token: 0x040026D5 RID: 9941
			internal int tmAscent;

			// Token: 0x040026D6 RID: 9942
			internal int tmDescent;

			// Token: 0x040026D7 RID: 9943
			internal int tmInternalLeading;

			// Token: 0x040026D8 RID: 9944
			internal int tmExternalLeading;

			// Token: 0x040026D9 RID: 9945
			internal int tmAveCharWidth;

			// Token: 0x040026DA RID: 9946
			internal int tmMaxCharWidth;

			// Token: 0x040026DB RID: 9947
			internal int tmWeight;

			// Token: 0x040026DC RID: 9948
			internal int tmOverhang;

			// Token: 0x040026DD RID: 9949
			internal int tmDigitizedAspectX;

			// Token: 0x040026DE RID: 9950
			internal int tmDigitizedAspectY;

			// Token: 0x040026DF RID: 9951
			internal short tmFirstChar;

			// Token: 0x040026E0 RID: 9952
			internal short tmLastChar;

			// Token: 0x040026E1 RID: 9953
			internal short tmDefaultChar;

			// Token: 0x040026E2 RID: 9954
			internal short tmBreakChar;

			// Token: 0x040026E3 RID: 9955
			internal byte tmItalic;

			// Token: 0x040026E4 RID: 9956
			internal byte tmUnderlined;

			// Token: 0x040026E5 RID: 9957
			internal byte tmStruckOut;

			// Token: 0x040026E6 RID: 9958
			internal byte tmPitchAndFamily;

			// Token: 0x040026E7 RID: 9959
			internal byte tmCharSet;
		}

		// Token: 0x02000485 RID: 1157
		public enum TernaryRasterOperations : uint
		{
			// Token: 0x040026E9 RID: 9961
			SRCCOPY = 13369376U,
			// Token: 0x040026EA RID: 9962
			SRCPAINT = 15597702U,
			// Token: 0x040026EB RID: 9963
			SRCAND = 8913094U,
			// Token: 0x040026EC RID: 9964
			SRCINVERT = 6684742U,
			// Token: 0x040026ED RID: 9965
			SRCERASE = 4457256U,
			// Token: 0x040026EE RID: 9966
			NOTSRCCOPY = 3342344U,
			// Token: 0x040026EF RID: 9967
			NOTSRCERASE = 1114278U,
			// Token: 0x040026F0 RID: 9968
			MERGECOPY = 12583114U,
			// Token: 0x040026F1 RID: 9969
			MERGEPAINT = 12255782U,
			// Token: 0x040026F2 RID: 9970
			PATCOPY = 15728673U,
			// Token: 0x040026F3 RID: 9971
			PATPAINT = 16452105U,
			// Token: 0x040026F4 RID: 9972
			PATINVERT = 5898313U,
			// Token: 0x040026F5 RID: 9973
			DSTINVERT = 5570569U,
			// Token: 0x040026F6 RID: 9974
			BLACKNESS = 66U,
			// Token: 0x040026F7 RID: 9975
			WHITENESS = 16711778U
		}

		// Token: 0x02000486 RID: 1158
		[Flags]
		private enum ScrollWindowExFlags
		{
			// Token: 0x040026F9 RID: 9977
			SW_NONE = 0,
			// Token: 0x040026FA RID: 9978
			SW_SCROLLCHILDREN = 1,
			// Token: 0x040026FB RID: 9979
			SW_INVALIDATE = 2,
			// Token: 0x040026FC RID: 9980
			SW_ERASE = 4,
			// Token: 0x040026FD RID: 9981
			SW_SMOOTHSCROLL = 16
		}

		// Token: 0x02000487 RID: 1159
		internal enum SystemMetrics
		{
			// Token: 0x040026FF RID: 9983
			SM_CXSCREEN,
			// Token: 0x04002700 RID: 9984
			SM_CYSCREEN,
			// Token: 0x04002701 RID: 9985
			SM_CXVSCROLL,
			// Token: 0x04002702 RID: 9986
			SM_CYHSCROLL,
			// Token: 0x04002703 RID: 9987
			SM_CYCAPTION,
			// Token: 0x04002704 RID: 9988
			SM_CXBORDER,
			// Token: 0x04002705 RID: 9989
			SM_CYBORDER,
			// Token: 0x04002706 RID: 9990
			SM_CXDLGFRAME,
			// Token: 0x04002707 RID: 9991
			SM_CYDLGFRAME,
			// Token: 0x04002708 RID: 9992
			SM_CYVTHUMB,
			// Token: 0x04002709 RID: 9993
			SM_CXHTHUMB,
			// Token: 0x0400270A RID: 9994
			SM_CXICON,
			// Token: 0x0400270B RID: 9995
			SM_CYICON,
			// Token: 0x0400270C RID: 9996
			SM_CXCURSOR,
			// Token: 0x0400270D RID: 9997
			SM_CYCURSOR,
			// Token: 0x0400270E RID: 9998
			SM_CYMENU,
			// Token: 0x0400270F RID: 9999
			SM_CXFULLSCREEN,
			// Token: 0x04002710 RID: 10000
			SM_CYFULLSCREEN,
			// Token: 0x04002711 RID: 10001
			SM_CYKANJIWINDOW,
			// Token: 0x04002712 RID: 10002
			SM_MOUSEPRESENT,
			// Token: 0x04002713 RID: 10003
			SM_CYVSCROLL,
			// Token: 0x04002714 RID: 10004
			SM_CXHSCROLL,
			// Token: 0x04002715 RID: 10005
			SM_DEBUG,
			// Token: 0x04002716 RID: 10006
			SM_SWAPBUTTON,
			// Token: 0x04002717 RID: 10007
			SM_RESERVED1,
			// Token: 0x04002718 RID: 10008
			SM_RESERVED2,
			// Token: 0x04002719 RID: 10009
			SM_RESERVED3,
			// Token: 0x0400271A RID: 10010
			SM_RESERVED4,
			// Token: 0x0400271B RID: 10011
			SM_CXMIN,
			// Token: 0x0400271C RID: 10012
			SM_CYMIN,
			// Token: 0x0400271D RID: 10013
			SM_CXSIZE,
			// Token: 0x0400271E RID: 10014
			SM_CYSIZE,
			// Token: 0x0400271F RID: 10015
			SM_CXFRAME,
			// Token: 0x04002720 RID: 10016
			SM_CYFRAME,
			// Token: 0x04002721 RID: 10017
			SM_CXMINTRACK,
			// Token: 0x04002722 RID: 10018
			SM_CYMINTRACK,
			// Token: 0x04002723 RID: 10019
			SM_CXDOUBLECLK,
			// Token: 0x04002724 RID: 10020
			SM_CYDOUBLECLK,
			// Token: 0x04002725 RID: 10021
			SM_CXICONSPACING,
			// Token: 0x04002726 RID: 10022
			SM_CYICONSPACING,
			// Token: 0x04002727 RID: 10023
			SM_MENUDROPALIGNMENT,
			// Token: 0x04002728 RID: 10024
			SM_PENWINDOWS,
			// Token: 0x04002729 RID: 10025
			SM_DBCSENABLED,
			// Token: 0x0400272A RID: 10026
			SM_CMOUSEBUTTONS,
			// Token: 0x0400272B RID: 10027
			SM_CXFIXEDFRAME = 7,
			// Token: 0x0400272C RID: 10028
			SM_CYFIXEDFRAME,
			// Token: 0x0400272D RID: 10029
			SM_CXSIZEFRAME = 32,
			// Token: 0x0400272E RID: 10030
			SM_CYSIZEFRAME,
			// Token: 0x0400272F RID: 10031
			SM_SECURE = 44,
			// Token: 0x04002730 RID: 10032
			SM_CXEDGE,
			// Token: 0x04002731 RID: 10033
			SM_CYEDGE,
			// Token: 0x04002732 RID: 10034
			SM_CXMINSPACING,
			// Token: 0x04002733 RID: 10035
			SM_CYMINSPACING,
			// Token: 0x04002734 RID: 10036
			SM_CXSMICON,
			// Token: 0x04002735 RID: 10037
			SM_CYSMICON,
			// Token: 0x04002736 RID: 10038
			SM_CYSMCAPTION,
			// Token: 0x04002737 RID: 10039
			SM_CXSMSIZE,
			// Token: 0x04002738 RID: 10040
			SM_CYSMSIZE,
			// Token: 0x04002739 RID: 10041
			SM_CXMENUSIZE,
			// Token: 0x0400273A RID: 10042
			SM_CYMENUSIZE,
			// Token: 0x0400273B RID: 10043
			SM_ARRANGE,
			// Token: 0x0400273C RID: 10044
			SM_CXMINIMIZED,
			// Token: 0x0400273D RID: 10045
			SM_CYMINIMIZED,
			// Token: 0x0400273E RID: 10046
			SM_CXMAXTRACK,
			// Token: 0x0400273F RID: 10047
			SM_CYMAXTRACK,
			// Token: 0x04002740 RID: 10048
			SM_CXMAXIMIZED,
			// Token: 0x04002741 RID: 10049
			SM_CYMAXIMIZED,
			// Token: 0x04002742 RID: 10050
			SM_NETWORK,
			// Token: 0x04002743 RID: 10051
			SM_CLEANBOOT = 67,
			// Token: 0x04002744 RID: 10052
			SM_CXDRAG,
			// Token: 0x04002745 RID: 10053
			SM_CYDRAG,
			// Token: 0x04002746 RID: 10054
			SM_SHOWSOUNDS,
			// Token: 0x04002747 RID: 10055
			SM_CXMENUCHECK,
			// Token: 0x04002748 RID: 10056
			SM_CYMENUCHECK,
			// Token: 0x04002749 RID: 10057
			SM_SLOWMACHINE,
			// Token: 0x0400274A RID: 10058
			SM_MIDEASTENABLED,
			// Token: 0x0400274B RID: 10059
			SM_MOUSEWHEELPRESENT,
			// Token: 0x0400274C RID: 10060
			SM_XVIRTUALSCREEN,
			// Token: 0x0400274D RID: 10061
			SM_YVIRTUALSCREEN,
			// Token: 0x0400274E RID: 10062
			SM_CXVIRTUALSCREEN,
			// Token: 0x0400274F RID: 10063
			SM_CYVIRTUALSCREEN,
			// Token: 0x04002750 RID: 10064
			SM_CMONITORS,
			// Token: 0x04002751 RID: 10065
			SM_SAMEDISPLAYFORMAT,
			// Token: 0x04002752 RID: 10066
			SM_IMMENABLED,
			// Token: 0x04002753 RID: 10067
			SM_CXFOCUSBORDER,
			// Token: 0x04002754 RID: 10068
			SM_CYFOCUSBORDER,
			// Token: 0x04002755 RID: 10069
			SM_TABLETPC = 86,
			// Token: 0x04002756 RID: 10070
			SM_MEDIACENTER,
			// Token: 0x04002757 RID: 10071
			SM_CMETRICS
		}

		// Token: 0x02000488 RID: 1160
		internal enum NotifyIconMessage
		{
			// Token: 0x04002759 RID: 10073
			NIM_ADD,
			// Token: 0x0400275A RID: 10074
			NIM_MODIFY,
			// Token: 0x0400275B RID: 10075
			NIM_DELETE
		}

		// Token: 0x02000489 RID: 1161
		[Flags]
		internal enum NotifyIconFlags
		{
			// Token: 0x0400275D RID: 10077
			NIF_MESSAGE = 1,
			// Token: 0x0400275E RID: 10078
			NIF_ICON = 2,
			// Token: 0x0400275F RID: 10079
			NIF_TIP = 4,
			// Token: 0x04002760 RID: 10080
			NIF_STATE = 8,
			// Token: 0x04002761 RID: 10081
			NIF_INFO = 16
		}

		// Token: 0x0200048A RID: 1162
		[StructLayout(0, CharSet = 3)]
		internal struct NOTIFYICONDATA
		{
			// Token: 0x04002762 RID: 10082
			internal uint cbSize;

			// Token: 0x04002763 RID: 10083
			internal IntPtr hWnd;

			// Token: 0x04002764 RID: 10084
			internal uint uID;

			// Token: 0x04002765 RID: 10085
			internal XplatUIWin32.NotifyIconFlags uFlags;

			// Token: 0x04002766 RID: 10086
			internal uint uCallbackMessage;

			// Token: 0x04002767 RID: 10087
			internal IntPtr hIcon;

			// Token: 0x04002768 RID: 10088
			[MarshalAs(23, SizeConst = 128)]
			internal string szTip;

			// Token: 0x04002769 RID: 10089
			internal int dwState;

			// Token: 0x0400276A RID: 10090
			internal int dwStateMask;

			// Token: 0x0400276B RID: 10091
			[MarshalAs(23, SizeConst = 256)]
			internal string szInfo;

			// Token: 0x0400276C RID: 10092
			internal int uTimeoutOrVersion;

			// Token: 0x0400276D RID: 10093
			[MarshalAs(23, SizeConst = 64)]
			internal string szInfoTitle;

			// Token: 0x0400276E RID: 10094
			internal ToolTipIcon dwInfoFlags;
		}

		// Token: 0x0200048B RID: 1163
		[Flags]
		internal enum DCExFlags
		{
			// Token: 0x04002770 RID: 10096
			DCX_WINDOW = 1,
			// Token: 0x04002771 RID: 10097
			DCX_CACHE = 2,
			// Token: 0x04002772 RID: 10098
			DCX_NORESETATTRS = 4,
			// Token: 0x04002773 RID: 10099
			DCX_CLIPCHILDREN = 8,
			// Token: 0x04002774 RID: 10100
			DCX_CLIPSIBLINGS = 16,
			// Token: 0x04002775 RID: 10101
			DCX_PARENTCLIP = 32,
			// Token: 0x04002776 RID: 10102
			DCX_EXCLUDERGN = 64,
			// Token: 0x04002777 RID: 10103
			DCX_INTERSECTRGN = 128,
			// Token: 0x04002778 RID: 10104
			DCX_EXCLUDEUPDATE = 256,
			// Token: 0x04002779 RID: 10105
			DCX_INTERSECTUPDATE = 512,
			// Token: 0x0400277A RID: 10106
			DCX_LOCKWINDOWUPDATE = 1024,
			// Token: 0x0400277B RID: 10107
			DCX_USESTYLE = 65536,
			// Token: 0x0400277C RID: 10108
			DCX_VALIDATE = 2097152
		}

		// Token: 0x0200048C RID: 1164
		[StructLayout(0, CharSet = 3)]
		internal struct CLIENTCREATESTRUCT
		{
			// Token: 0x0400277D RID: 10109
			internal IntPtr hWindowMenu;

			// Token: 0x0400277E RID: 10110
			internal uint idFirstChild;
		}

		// Token: 0x0200048D RID: 1165
		private enum ClassLong
		{
			// Token: 0x04002780 RID: 10112
			GCL_MENUNAME = -8,
			// Token: 0x04002781 RID: 10113
			GCL_HBRBACKGROUND = -10,
			// Token: 0x04002782 RID: 10114
			GCL_HCURSOR = -12,
			// Token: 0x04002783 RID: 10115
			GCL_HICON = -14,
			// Token: 0x04002784 RID: 10116
			GCL_HMODULE = -16,
			// Token: 0x04002785 RID: 10117
			GCL_CBWNDEXTRA = -18,
			// Token: 0x04002786 RID: 10118
			GCL_CBCLSEXTRA = -20,
			// Token: 0x04002787 RID: 10119
			GCL_WNDPROC = -24,
			// Token: 0x04002788 RID: 10120
			GCL_STYLE = -26,
			// Token: 0x04002789 RID: 10121
			GCW_ATOM = -32,
			// Token: 0x0400278A RID: 10122
			GCL_HICONSM = -34
		}

		// Token: 0x0200048E RID: 1166
		[Flags]
		internal enum GAllocFlags : uint
		{
			// Token: 0x0400278C RID: 10124
			GMEM_FIXED = 0U,
			// Token: 0x0400278D RID: 10125
			GMEM_MOVEABLE = 2U,
			// Token: 0x0400278E RID: 10126
			GMEM_NOCOMPACT = 16U,
			// Token: 0x0400278F RID: 10127
			GMEM_NODISCARD = 32U,
			// Token: 0x04002790 RID: 10128
			GMEM_ZEROINIT = 64U,
			// Token: 0x04002791 RID: 10129
			GMEM_MODIFY = 128U,
			// Token: 0x04002792 RID: 10130
			GMEM_DISCARDABLE = 256U,
			// Token: 0x04002793 RID: 10131
			GMEM_NOT_BANKED = 4096U,
			// Token: 0x04002794 RID: 10132
			GMEM_SHARE = 8192U,
			// Token: 0x04002795 RID: 10133
			GMEM_DDESHARE = 8192U,
			// Token: 0x04002796 RID: 10134
			GMEM_NOTIFY = 16384U,
			// Token: 0x04002797 RID: 10135
			GMEM_LOWER = 4096U,
			// Token: 0x04002798 RID: 10136
			GMEM_VALID_FLAGS = 32626U,
			// Token: 0x04002799 RID: 10137
			GMEM_INVALID_HANDLE = 32768U,
			// Token: 0x0400279A RID: 10138
			GHND = 66U,
			// Token: 0x0400279B RID: 10139
			GPTR = 64U
		}

		// Token: 0x0200048F RID: 1167
		internal enum ROP2DrawMode
		{
			// Token: 0x0400279D RID: 10141
			R2_BLACK = 1,
			// Token: 0x0400279E RID: 10142
			R2_NOTMERGEPEN,
			// Token: 0x0400279F RID: 10143
			R2_MASKNOTPEN,
			// Token: 0x040027A0 RID: 10144
			R2_NOTCOPYPEN,
			// Token: 0x040027A1 RID: 10145
			R2_MASKPENNOT,
			// Token: 0x040027A2 RID: 10146
			R2_NOT,
			// Token: 0x040027A3 RID: 10147
			R2_XORPEN,
			// Token: 0x040027A4 RID: 10148
			R2_NOTMASKPEN,
			// Token: 0x040027A5 RID: 10149
			R2_MASKPEN,
			// Token: 0x040027A6 RID: 10150
			R2_NOTXORPEN,
			// Token: 0x040027A7 RID: 10151
			R2_NOP,
			// Token: 0x040027A8 RID: 10152
			R2_MERGENOTPEN,
			// Token: 0x040027A9 RID: 10153
			R2_COPYPEN,
			// Token: 0x040027AA RID: 10154
			R2_MERGEPENNOT,
			// Token: 0x040027AB RID: 10155
			R2_MERGEPEN,
			// Token: 0x040027AC RID: 10156
			R2_WHITE,
			// Token: 0x040027AD RID: 10157
			R2_LAST = 16
		}

		// Token: 0x02000490 RID: 1168
		internal enum PenStyle
		{
			// Token: 0x040027AF RID: 10159
			PS_SOLID,
			// Token: 0x040027B0 RID: 10160
			PS_DASH,
			// Token: 0x040027B1 RID: 10161
			PS_DOT,
			// Token: 0x040027B2 RID: 10162
			PS_DASHDOT,
			// Token: 0x040027B3 RID: 10163
			PS_DASHDOTDOT,
			// Token: 0x040027B4 RID: 10164
			PS_NULL,
			// Token: 0x040027B5 RID: 10165
			PS_INSIDEFRAME,
			// Token: 0x040027B6 RID: 10166
			PS_USERSTYLE,
			// Token: 0x040027B7 RID: 10167
			PS_ALTERNATE
		}

		// Token: 0x02000491 RID: 1169
		internal enum PatBltRop
		{
			// Token: 0x040027B9 RID: 10169
			PATCOPY = 15728673,
			// Token: 0x040027BA RID: 10170
			PATINVERT = 5898313,
			// Token: 0x040027BB RID: 10171
			DSTINVERT = 5570569,
			// Token: 0x040027BC RID: 10172
			BLACKNESS = 66,
			// Token: 0x040027BD RID: 10173
			WHITENESS = 16711778
		}

		// Token: 0x02000492 RID: 1170
		internal enum StockObject
		{
			// Token: 0x040027BF RID: 10175
			WHITE_BRUSH,
			// Token: 0x040027C0 RID: 10176
			LTGRAY_BRUSH,
			// Token: 0x040027C1 RID: 10177
			GRAY_BRUSH,
			// Token: 0x040027C2 RID: 10178
			DKGRAY_BRUSH,
			// Token: 0x040027C3 RID: 10179
			BLACK_BRUSH,
			// Token: 0x040027C4 RID: 10180
			NULL_BRUSH,
			// Token: 0x040027C5 RID: 10181
			HOLLOW_BRUSH = 5,
			// Token: 0x040027C6 RID: 10182
			WHITE_PEN,
			// Token: 0x040027C7 RID: 10183
			BLACK_PEN,
			// Token: 0x040027C8 RID: 10184
			NULL_PEN,
			// Token: 0x040027C9 RID: 10185
			OEM_FIXED_FONT = 10,
			// Token: 0x040027CA RID: 10186
			ANSI_FIXED_FONT,
			// Token: 0x040027CB RID: 10187
			ANSI_VAR_FONT,
			// Token: 0x040027CC RID: 10188
			SYSTEM_FONT,
			// Token: 0x040027CD RID: 10189
			DEVICE_DEFAULT_FONT,
			// Token: 0x040027CE RID: 10190
			DEFAULT_PALETTE,
			// Token: 0x040027CF RID: 10191
			SYSTEM_FIXED_FONT
		}

		// Token: 0x02000493 RID: 1171
		internal enum HatchStyle
		{
			// Token: 0x040027D1 RID: 10193
			HS_HORIZONTAL,
			// Token: 0x040027D2 RID: 10194
			HS_VERTICAL,
			// Token: 0x040027D3 RID: 10195
			HS_FDIAGONAL,
			// Token: 0x040027D4 RID: 10196
			HS_BDIAGONAL,
			// Token: 0x040027D5 RID: 10197
			HS_CROSS,
			// Token: 0x040027D6 RID: 10198
			HS_DIAGCROSS
		}

		// Token: 0x02000494 RID: 1172
		[Flags]
		internal enum SndFlags
		{
			// Token: 0x040027D8 RID: 10200
			SND_SYNC = 0,
			// Token: 0x040027D9 RID: 10201
			SND_ASYNC = 1,
			// Token: 0x040027DA RID: 10202
			SND_NODEFAULT = 2,
			// Token: 0x040027DB RID: 10203
			SND_MEMORY = 4,
			// Token: 0x040027DC RID: 10204
			SND_LOOP = 8,
			// Token: 0x040027DD RID: 10205
			SND_NOSTOP = 16,
			// Token: 0x040027DE RID: 10206
			SND_NOWAIT = 8192,
			// Token: 0x040027DF RID: 10207
			SND_ALIAS = 65536,
			// Token: 0x040027E0 RID: 10208
			SND_ALIAS_ID = 1114112,
			// Token: 0x040027E1 RID: 10209
			SND_FILENAME = 131072,
			// Token: 0x040027E2 RID: 10210
			SND_RESOURCE = 262148,
			// Token: 0x040027E3 RID: 10211
			SND_PURGE = 64,
			// Token: 0x040027E4 RID: 10212
			SND_APPLICATION = 128
		}

		// Token: 0x02000495 RID: 1173
		[Flags]
		internal enum LayeredWindowAttributes
		{
			// Token: 0x040027E6 RID: 10214
			LWA_COLORKEY = 1,
			// Token: 0x040027E7 RID: 10215
			LWA_ALPHA = 2
		}

		// Token: 0x02000496 RID: 1174
		public enum ACLineStatus : byte
		{
			// Token: 0x040027E9 RID: 10217
			Offline,
			// Token: 0x040027EA RID: 10218
			Online,
			// Token: 0x040027EB RID: 10219
			Unknown = 255
		}

		// Token: 0x02000497 RID: 1175
		public enum BatteryFlag : byte
		{
			// Token: 0x040027ED RID: 10221
			High = 1,
			// Token: 0x040027EE RID: 10222
			Low,
			// Token: 0x040027EF RID: 10223
			Critical = 4,
			// Token: 0x040027F0 RID: 10224
			Charging = 8,
			// Token: 0x040027F1 RID: 10225
			NoSystemBattery = 128,
			// Token: 0x040027F2 RID: 10226
			Unknown = 255
		}

		// Token: 0x02000498 RID: 1176
		[StructLayout(0)]
		public class SYSTEMPOWERSTATUS
		{
			// Token: 0x040027F3 RID: 10227
			public XplatUIWin32.ACLineStatus _ACLineStatus;

			// Token: 0x040027F4 RID: 10228
			public XplatUIWin32.BatteryFlag _BatteryFlag;

			// Token: 0x040027F5 RID: 10229
			public byte _BatteryLifePercent;

			// Token: 0x040027F6 RID: 10230
			public byte _Reserved1;

			// Token: 0x040027F7 RID: 10231
			public int _BatteryLifeTime;

			// Token: 0x040027F8 RID: 10232
			public int _BatteryFullLifeTime;
		}

		// Token: 0x02000499 RID: 1177
		private class WinBuffer
		{
			// Token: 0x06004A12 RID: 18962 RVA: 0x0011D9B4 File Offset: 0x0011BBB4
			public WinBuffer(IntPtr hdc, IntPtr bitmap)
			{
				this.hdc = hdc;
				this.bitmap = bitmap;
			}

			// Token: 0x040027F9 RID: 10233
			public IntPtr hdc;

			// Token: 0x040027FA RID: 10234
			public IntPtr bitmap;
		}
	}
}
