using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020003D5 RID: 981
	internal class X11Keyboard : IDisposable
	{
		// Token: 0x060045D8 RID: 17880 RVA: 0x00111ADC File Offset: 0x0010FCDC
		public X11Keyboard(IntPtr display, IntPtr clientWindow)
		{
			this.display = display;
			this.lookup_buffer = new StringBuilder(24);
			this.EnsureLayoutInitialized();
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x00111B44 File Offset: 0x0010FD44
		// Note: this type is marked as 'beforefieldinit'.
		static X11Keyboard()
		{
			int[] array = new int[256];
			array[8] = 65288;
			array[9] = 65289;
			array[12] = 65291;
			array[13] = 65293;
			array[16] = 65505;
			array[17] = 65507;
			array[18] = 65383;
			array[20] = 65509;
			array[35] = 65367;
			array[36] = 65360;
			array[37] = 65361;
			array[38] = 65362;
			array[39] = 65363;
			array[40] = 65364;
			array[91] = 65511;
			array[92] = 65512;
			array[160] = 65505;
			array[161] = 65506;
			array[162] = 65507;
			array[163] = 65508;
			array[164] = 65513;
			array[165] = 65514;
			X11Keyboard.nonchar_vkey_key = array;
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x00111C6C File Offset: 0x0010FE6C
		void IDisposable.Dispose()
		{
			if (this.xim != IntPtr.Zero)
			{
				foreach (object obj in this.xic_table.Values)
				{
					IntPtr intPtr = (IntPtr)obj;
					X11Keyboard.XDestroyIC(intPtr);
				}
				this.xic_table.Clear();
				X11Keyboard.XCloseIM(this.xim);
				this.xim = IntPtr.Zero;
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x060045DB RID: 17883 RVA: 0x00111D18 File Offset: 0x0010FF18
		public IntPtr ClientWindow
		{
			get
			{
				return this.client_window;
			}
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x00111D20 File Offset: 0x0010FF20
		public void DestroyICForWindow(IntPtr window)
		{
			IntPtr xic = this.GetXic(window);
			if (xic != IntPtr.Zero)
			{
				this.xic_table.Remove((long)window);
				X11Keyboard.XDestroyIC(xic);
			}
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x00111D64 File Offset: 0x0010FF64
		public void EnsureLayoutInitialized()
		{
			if (this.initialized)
			{
				return;
			}
			KeyboardLayouts keyboardLayouts = new KeyboardLayouts();
			KeyboardLayout keyboardLayout = this.DetectLayout(keyboardLayouts);
			this.lcid = keyboardLayout.Lcid;
			this.CreateConversionArray(keyboardLayouts, keyboardLayout);
			this.SetupXIM();
			this.initialized = true;
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x00111DAC File Offset: 0x0010FFAC
		private void SetupXIM()
		{
			this.xim = IntPtr.Zero;
			if (!X11Keyboard.XSupportsLocale())
			{
				Console.Error.WriteLine("X does not support your locale");
				return;
			}
			if (!X11Keyboard.XSetLocaleModifiers(string.Empty))
			{
				Console.Error.WriteLine("Could not set X locale modifiers");
				return;
			}
			if (Environment.GetEnvironmentVariable("MONO_WINFORMS_XIM_STYLE") == "disabled")
			{
				return;
			}
			this.xim = X11Keyboard.XOpenIM(this.display, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (this.xim == IntPtr.Zero)
			{
				Console.Error.WriteLine("Could not get XIM");
			}
			else
			{
				this.utf8_buffer = new byte[100];
			}
			this.initialized = true;
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x00111E74 File Offset: 0x00110074
		private void CreateXicForWindow(IntPtr window)
		{
			IntPtr intPtr = this.CreateXic(window, this.xim);
			this.xic_table[(long)window] = intPtr;
			if (intPtr == IntPtr.Zero)
			{
				Console.Error.WriteLine("Could not get XIC");
			}
			else
			{
				if (X11Keyboard.XGetICValues(intPtr, "filterEvents", out this.xic_event_mask, IntPtr.Zero) != null)
				{
					Console.Error.WriteLine("Could not get XIC values");
				}
				EventMask eventMask = EventMask.KeyPressMask | EventMask.ExposureMask | EventMask.FocusChangeMask;
				if ((this.xic_event_mask | eventMask) == this.xic_event_mask)
				{
					this.xic_event_mask |= eventMask;
					object xlibLock = X11Keyboard.XlibLock;
					lock (xlibLock)
					{
						XplatUIX11.XSelectInput(this.display, window, new IntPtr((int)this.xic_event_mask));
					}
				}
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x00111F6C File Offset: 0x0011016C
		public EventMask KeyEventMask
		{
			get
			{
				return this.xic_event_mask;
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060045E1 RID: 17889 RVA: 0x00111F74 File Offset: 0x00110174
		public Keys ModifierKeys
		{
			get
			{
				Keys keys = Keys.None;
				if ((this.key_state_table[16] & 128) != 0)
				{
					keys |= Keys.Shift;
				}
				if ((this.key_state_table[17] & 128) != 0)
				{
					keys |= Keys.Control;
				}
				if ((this.key_state_table[18] & 128) != 0)
				{
					keys |= Keys.Alt;
				}
				return keys;
			}
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x00111FD8 File Offset: 0x001101D8
		private IntPtr GetXic(IntPtr window)
		{
			if (this.xim != IntPtr.Zero && this.xic_table.ContainsKey((long)window))
			{
				return (IntPtr)this.xic_table[(long)window];
			}
			return IntPtr.Zero;
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x00112038 File Offset: 0x00110238
		private bool FilterKey(XEvent e, int vkey)
		{
			if (XplatUI.key_filters.Count == 0)
			{
				return false;
			}
			KeyFilterData keyFilterData;
			keyFilterData.Down = e.type == XEventName.KeyPress;
			keyFilterData.ModifierKeys = this.ModifierKeys;
			XKeySym xkeySym;
			IntPtr intPtr;
			this.LookupString(ref e, 0, out xkeySym, out intPtr);
			keyFilterData.keysym = (int)xkeySym;
			keyFilterData.keycode = e.KeyEvent.keycode;
			keyFilterData.str = this.lookup_buffer.ToString(0, this.lookup_buffer.Length);
			return XplatUI.FilterKey(keyFilterData);
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x001120C4 File Offset: 0x001102C4
		public void FocusIn(IntPtr window)
		{
			if (this.xim == IntPtr.Zero)
			{
				return;
			}
			this.client_window = window;
			if (!this.xic_table.ContainsKey((long)window))
			{
				this.CreateXicForWindow(window);
			}
			IntPtr xic = this.GetXic(window);
			if (xic != IntPtr.Zero)
			{
				X11Keyboard.XSetICFocus(xic);
			}
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x00112130 File Offset: 0x00110330
		public void FocusOut(IntPtr window)
		{
			if (this.xim == IntPtr.Zero)
			{
				return;
			}
			this.client_window = IntPtr.Zero;
			IntPtr xic = this.GetXic(window);
			if (xic != IntPtr.Zero)
			{
				X11Keyboard.Xutf8ResetIC(xic);
				X11Keyboard.XUnsetICFocus(xic);
			}
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x00112184 File Offset: 0x00110384
		public bool ResetKeyState(IntPtr hwnd, ref MSG msg)
		{
			if ((this.key_state_table[16] & 128) != 0)
			{
				byte[] array = this.key_state_table;
				int num = 16;
				array[num] &= 127;
			}
			if ((this.key_state_table[17] & 128) != 0)
			{
				byte[] array2 = this.key_state_table;
				int num2 = 17;
				array2[num2] &= 127;
			}
			if ((this.key_state_table[18] & 128) != 0)
			{
				byte[] array3 = this.key_state_table;
				int num3 = 18;
				array3[num3] &= 127;
			}
			return false;
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x0011220C File Offset: 0x0011040C
		public void PreFilter(XEvent xevent)
		{
			if (xevent.KeyEvent.keycode >= this.keyc2vkey.Length)
			{
				return;
			}
			int num = this.keyc2vkey[xevent.KeyEvent.keycode];
			XEventName type = xevent.type;
			if (type != XEventName.KeyPress)
			{
				if (type == XEventName.KeyRelease)
				{
					byte[] array = this.key_state_table;
					int num2 = num & 255;
					array[num2] &= 127;
				}
			}
			else
			{
				if ((this.key_state_table[num & 255] & 128) == 0)
				{
					byte[] array2 = this.key_state_table;
					int num3 = num & 255;
					array2[num3] ^= 1;
				}
				byte[] array3 = this.key_state_table;
				int num4 = num & 255;
				array3[num4] |= 128;
			}
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x001122D4 File Offset: 0x001104D4
		public void KeyEvent(IntPtr hwnd, XEvent xevent, ref MSG msg)
		{
			IntPtr zero = IntPtr.Zero;
			XKeySym xkeySym;
			int num = this.LookupString(ref xevent, 24, out xkeySym, out zero);
			if ((xkeySym >= (XKeySym)65025U && xkeySym <= (XKeySym)65039U) || xkeySym == (XKeySym)65406U)
			{
				this.UpdateKeyState(xevent);
				return;
			}
			if (xevent.KeyEvent.keycode >> 8 == 16)
			{
				xevent.KeyEvent.keycode = xevent.KeyEvent.keycode & 255;
			}
			int num2 = (int)xevent.KeyEvent.time;
			if (zero == (IntPtr)2)
			{
				msg = this.SendImeComposition(this.lookup_buffer.ToString(0, this.lookup_buffer.Length));
				msg.hwnd = hwnd;
				return;
			}
			this.AltGrMask = xevent.KeyEvent.state & 24824;
			int num3 = this.EventToVkey(xevent);
			if (num3 == 0 && num != 0)
			{
				num3 = 252;
			}
			if (this.FilterKey(xevent, num3))
			{
				return;
			}
			VirtualKeys virtualKeys = (VirtualKeys)(num3 & 255);
			if (virtualKeys != VirtualKeys.VK_CAPITAL)
			{
				if (virtualKeys != VirtualKeys.VK_NUMLOCK)
				{
					if ((this.key_state_table[144] & 1) == 0 != ((xevent.KeyEvent.state & this.NumLockMask) == 0))
					{
						this.GenerateMessage(VirtualKeys.VK_NUMLOCK, 69, xevent.KeyEvent.keycode, XEventName.KeyPress, num2);
						this.GenerateMessage(VirtualKeys.VK_NUMLOCK, 69, xevent.KeyEvent.keycode, XEventName.KeyRelease, num2);
					}
					if ((this.key_state_table[20] & 1) == 0 != ((xevent.KeyEvent.state & 2) == 0))
					{
						this.GenerateMessage(VirtualKeys.VK_CAPITAL, 58, xevent.KeyEvent.keycode, XEventName.KeyPress, num2);
						this.GenerateMessage(VirtualKeys.VK_CAPITAL, 58, xevent.KeyEvent.keycode, XEventName.KeyRelease, num2);
					}
					this.num_state = false;
					this.cap_state = false;
					int num4 = this.keyc2scan[xevent.KeyEvent.keycode] & 255;
					KeybdEventFlags keybdEventFlags = KeybdEventFlags.None;
					if (xevent.type == XEventName.KeyRelease)
					{
						keybdEventFlags |= KeybdEventFlags.KeyUp;
					}
					if ((num3 & 256) != 0)
					{
						keybdEventFlags |= KeybdEventFlags.ExtendedKey;
					}
					msg = this.SendKeyboardInput((VirtualKeys)(num3 & 255), num4, xevent.KeyEvent.keycode, keybdEventFlags, num2);
					msg.hwnd = hwnd;
				}
				else
				{
					this.GenerateMessage(VirtualKeys.VK_NUMLOCK, 69, xevent.KeyEvent.keycode, xevent.type, num2);
				}
			}
			else
			{
				this.GenerateMessage(VirtualKeys.VK_CAPITAL, 58, xevent.KeyEvent.keycode, xevent.type, num2);
			}
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x0011258C File Offset: 0x0011078C
		public bool TranslateMessage(ref MSG msg)
		{
			bool flag = false;
			if (msg.message >= Msg.WM_KEYDOWN && msg.message <= Msg.WM_KEYLAST)
			{
				flag = true;
			}
			if (msg.message == Msg.WM_SYSKEYUP && msg.wParam == (IntPtr)18 && this.menu_state)
			{
				msg.message = Msg.WM_KEYUP;
				this.menu_state = false;
			}
			if (msg.message != Msg.WM_KEYDOWN && msg.message != Msg.WM_SYSKEYDOWN)
			{
				return flag;
			}
			if ((this.key_state_table[18] & 128) != 0 && msg.wParam != (IntPtr)18)
			{
				this.menu_state = true;
			}
			this.EnsureLayoutInitialized();
			string text;
			int num = this.ToUnicode((int)msg.wParam, Control.HighOrder((long)(int)msg.lParam), out text);
			int num2 = num;
			switch (num2 + 1)
			{
			case 0:
			{
				Msg msg2 = ((msg.message != Msg.WM_KEYDOWN) ? Msg.WM_SYSDEADCHAR : Msg.WM_DEADCHAR);
				XplatUI.PostMessage(msg.hwnd, msg2, (IntPtr)((int)text.get_Chars(0)), msg.lParam);
				return true;
			}
			case 2:
			{
				Msg msg2 = ((msg.message != Msg.WM_KEYDOWN) ? Msg.WM_SYSCHAR : Msg.WM_CHAR);
				XplatUI.PostMessage(msg.hwnd, msg2, (IntPtr)((int)text.get_Chars(0)), msg.lParam);
				break;
			}
			}
			return flag;
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x00112724 File Offset: 0x00110924
		public int ToKeycode(int key)
		{
			int num = 0;
			if (X11Keyboard.nonchar_vkey_key[key] > 0)
			{
				num = X11Keyboard.XKeysymToKeycode(this.display, X11Keyboard.nonchar_vkey_key[key]);
			}
			if (num == 0)
			{
				num = X11Keyboard.XKeysymToKeycode(this.display, key);
			}
			return num;
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00112768 File Offset: 0x00110968
		public int ToUnicode(int vkey, int scan, out string buffer)
		{
			if ((scan & 32768) != 0)
			{
				buffer = string.Empty;
				return 0;
			}
			XEvent xevent = default(XEvent);
			xevent.AnyEvent.type = XEventName.KeyPress;
			xevent.KeyEvent.display = this.display;
			xevent.KeyEvent.keycode = 0;
			xevent.KeyEvent.state = 0;
			if ((this.key_state_table[16] & 128) != 0)
			{
				xevent.KeyEvent.state = xevent.KeyEvent.state | 1;
			}
			if ((this.key_state_table[20] & 1) != 0)
			{
				xevent.KeyEvent.state = xevent.KeyEvent.state | 2;
			}
			if ((this.key_state_table[17] & 128) != 0)
			{
				xevent.KeyEvent.state = xevent.KeyEvent.state | 4;
			}
			if ((this.key_state_table[144] & 1) != 0)
			{
				xevent.KeyEvent.state = xevent.KeyEvent.state | this.NumLockMask;
			}
			xevent.KeyEvent.state = xevent.KeyEvent.state | this.AltGrMask;
			int num = this.min_keycode;
			while (num <= this.max_keycode && xevent.KeyEvent.keycode == 0)
			{
				if ((this.keyc2vkey[num] & 255) == vkey)
				{
					xevent.KeyEvent.keycode = num;
					if ((this.EventToVkey(xevent) & 255) != vkey)
					{
						xevent.KeyEvent.keycode = 0;
					}
				}
				num++;
			}
			if (vkey >= 96 && vkey <= 105)
			{
				xevent.KeyEvent.keycode = X11Keyboard.XKeysymToKeycode(this.display, vkey - 96 + 65456);
			}
			if (vkey == 110)
			{
				xevent.KeyEvent.keycode = X11Keyboard.XKeysymToKeycode(this.display, 65454);
			}
			if (vkey == 108)
			{
				xevent.KeyEvent.keycode = X11Keyboard.XKeysymToKeycode(this.display, 65452);
			}
			if (xevent.KeyEvent.keycode == 0 && vkey != 252)
			{
				Console.Error.WriteLine("unknown virtual key {0:X}", vkey);
				buffer = string.Empty;
				return vkey;
			}
			XKeySym xkeySym;
			IntPtr intPtr;
			int num2 = this.LookupString(ref xevent, 24, out xkeySym, out intPtr);
			int num3 = (int)xkeySym;
			buffer = string.Empty;
			if (num2 == 0)
			{
				int num4 = this.MapDeadKeySym(num3);
				if (num4 != 0)
				{
					byte[] array = new byte[] { (byte)num4 };
					Encoding encoding = Encoding.GetEncoding(new CultureInfo(this.lcid).TextInfo.ANSICodePage);
					buffer = new string(encoding.GetChars(array));
					num2 = -1;
				}
			}
			else
			{
				if ((xevent.KeyEvent.state & this.NumLockMask) == 0 && (xevent.KeyEvent.state & 1) != 0 && num3 >= 65456 && num3 <= 65465)
				{
					buffer = string.Empty;
					num2 = 0;
				}
				if ((xevent.KeyEvent.state & 4) != 0 && ((num3 >= 33 && num3 < 65) || (num3 > 90 && num3 < 97)))
				{
					buffer = string.Empty;
					num2 = 0;
				}
				if (num3 == 65535)
				{
					buffer = string.Empty;
					num2 = 0;
				}
				if (num3 == 65288 && (this.key_state_table[17] & 128) != 0)
				{
					buffer = new string(new char[] { '\u007f' });
					return 1;
				}
				if (num3 == 65288)
				{
					buffer = new string(new char[] { '\b' });
					return 1;
				}
				if (num3 == 65293)
				{
					buffer = new string(new char[] { '\r' });
					return 1;
				}
				if (num2 != 0)
				{
					buffer = this.lookup_buffer.ToString();
					num2 = buffer.Length;
				}
			}
			return num2;
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00112B50 File Offset: 0x00110D50
		internal string GetCompositionString()
		{
			return this.stored_keyevent_string;
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x00112B58 File Offset: 0x00110D58
		private MSG SendImeComposition(string s)
		{
			MSG msg = default(MSG);
			msg.message = Msg.WM_IME_COMPOSITION;
			msg.refobject = s;
			this.stored_keyevent_string = s;
			return msg;
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x00112B8C File Offset: 0x00110D8C
		private MSG SendKeyboardInput(VirtualKeys vkey, int scan, int keycode, KeybdEventFlags dw_flags, int time)
		{
			Msg msg;
			if ((dw_flags & KeybdEventFlags.KeyUp) != KeybdEventFlags.None)
			{
				bool flag = (this.key_state_table[18] & 128) != 0 && (this.key_state_table[17] & 128) == 0;
				byte[] array = this.key_state_table;
				array[(int)vkey] = array[(int)vkey] & 127;
				msg = ((!flag) ? Msg.WM_KEYUP : Msg.WM_SYSKEYUP);
			}
			else
			{
				if ((this.key_state_table[(int)vkey] & 128) == 0)
				{
					byte[] array2 = this.key_state_table;
					array2[(int)vkey] = array2[(int)vkey] ^ 1;
				}
				byte[] array3 = this.key_state_table;
				array3[(int)vkey] = array3[(int)vkey] | 128;
				bool flag2 = (this.key_state_table[18] & 128) != 0 && (this.key_state_table[17] & 128) == 0;
				msg = ((!flag2) ? Msg.WM_KEYDOWN : Msg.WM_SYSKEYDOWN);
			}
			MSG msg2 = default(MSG);
			msg2.message = msg;
			msg2.wParam = (IntPtr)((int)vkey);
			msg2.lParam = this.GenerateLParam(msg2, keycode);
			return msg2;
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x00112CA4 File Offset: 0x00110EA4
		private IntPtr GenerateLParam(MSG m, int keyCode)
		{
			byte b = 0;
			if (m.message == Msg.WM_SYSKEYUP || m.message == Msg.WM_KEYUP)
			{
				b |= 128;
			}
			b |= 64;
			if ((this.key_state_table[165] & 128) != 0 || (this.key_state_table[164] & 128) != 0 || (this.key_state_table[18] & 128) != 0)
			{
				b |= 32;
			}
			if ((this.key_state_table[45] & 128) != 0 || (this.key_state_table[46] & 128) != 0 || (this.key_state_table[36] & 128) != 0 || (this.key_state_table[35] & 128) != 0 || (this.key_state_table[38] & 128) != 0 || (this.key_state_table[40] & 128) != 0 || (this.key_state_table[37] & 128) != 0 || (this.key_state_table[39] & 128) != 0 || (this.key_state_table[17] & 128) != 0 || (this.key_state_table[18] & 128) != 0 || (this.key_state_table[144] & 128) != 0 || (this.key_state_table[42] & 128) != 0 || (this.key_state_table[13] & 128) != 0 || (this.key_state_table[111] & 128) != 0 || (this.key_state_table[33] & 128) != 0 || (this.key_state_table[34] & 128) != 0)
			{
				b |= 1;
			}
			int num = (int)(b & byte.MaxValue) << 24;
			num |= (keyCode & 255) << 16;
			num |= 1;
			return (IntPtr)num;
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x00112E98 File Offset: 0x00111098
		private void GenerateMessage(VirtualKeys vkey, int scan, int key_code, XEventName type, int event_time)
		{
			bool flag = ((vkey != VirtualKeys.VK_NUMLOCK) ? this.cap_state : this.num_state);
			if (flag)
			{
				this.SetState(vkey, false);
			}
			else
			{
				KeybdEventFlags keybdEventFlags = ((vkey != VirtualKeys.VK_NUMLOCK) ? KeybdEventFlags.None : KeybdEventFlags.ExtendedKey);
				KeybdEventFlags keybdEventFlags2 = ((vkey != VirtualKeys.VK_NUMLOCK) ? KeybdEventFlags.None : KeybdEventFlags.ExtendedKey) | KeybdEventFlags.KeyUp;
				if ((this.key_state_table[(int)vkey] & 1) != 0)
				{
					if (type != XEventName.KeyPress)
					{
						this.SendKeyboardInput(vkey, scan, key_code, keybdEventFlags, event_time);
						this.SendKeyboardInput(vkey, scan, key_code, keybdEventFlags2, event_time);
						this.SetState(vkey, false);
						byte[] array = this.key_state_table;
						array[(int)vkey] = array[(int)vkey] & 254;
					}
				}
				else if (type == XEventName.KeyPress)
				{
					this.SendKeyboardInput(vkey, scan, key_code, keybdEventFlags, event_time);
					this.SendKeyboardInput(vkey, scan, key_code, keybdEventFlags2, event_time);
					this.SetState(vkey, true);
					byte[] array2 = this.key_state_table;
					array2[(int)vkey] = array2[(int)vkey] | 1;
				}
			}
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x00112F90 File Offset: 0x00111190
		private void UpdateKeyState(XEvent xevent)
		{
			int num = this.EventToVkey(xevent);
			XEventName type = xevent.type;
			if (type != XEventName.KeyPress)
			{
				if (type == XEventName.KeyRelease)
				{
					byte[] array = this.key_state_table;
					int num2 = num & 255;
					array[num2] &= 127;
				}
			}
			else
			{
				if ((this.key_state_table[num & 255] & 128) == 0)
				{
					byte[] array2 = this.key_state_table;
					int num3 = num & 255;
					array2[num3] ^= 1;
				}
				byte[] array3 = this.key_state_table;
				int num4 = num & 255;
				array3[num4] |= 128;
			}
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x00113030 File Offset: 0x00111230
		private void SetState(VirtualKeys key, bool state)
		{
			if (key == VirtualKeys.VK_NUMLOCK)
			{
				this.num_state = state;
			}
			else
			{
				this.cap_state = state;
			}
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x00113050 File Offset: 0x00111250
		public int EventToVkey(XEvent e)
		{
			XKeySym xkeySym;
			IntPtr intPtr;
			this.LookupString(ref e, 0, out xkeySym, out intPtr);
			int num = (int)xkeySym;
			if ((e.KeyEvent.state & this.NumLockMask) != 0 && (num == 65452 || num == 65454 || (num >= 65456 && num <= 65465)))
			{
				return X11Keyboard.nonchar_key_vkey[num & 255];
			}
			return this.keyc2vkey[e.KeyEvent.keycode];
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x001130D4 File Offset: 0x001112D4
		private void CreateConversionArray(KeyboardLayouts layouts, KeyboardLayout layout)
		{
			XEvent xevent = default(XEvent);
			int[] array = new int[4];
			xevent.KeyEvent.display = this.display;
			xevent.KeyEvent.state = 0;
			for (int i = this.min_keycode; i <= this.max_keycode; i++)
			{
				int num = 0;
				int num2 = 0;
				xevent.KeyEvent.keycode = i;
				XKeySym xkeySym;
				IntPtr intPtr;
				this.LookupString(ref xevent, 0, out xkeySym, out intPtr);
				uint num3 = (uint)xkeySym;
				if (num3 != 0U)
				{
					if (num3 >> 8 == 255U)
					{
						num = X11Keyboard.nonchar_key_vkey[(int)((UIntPtr)(num3 & 255U))];
						num2 = X11Keyboard.nonchar_key_scan[(int)((UIntPtr)(num3 & 255U))];
						if ((num2 & 256) != 0)
						{
							num |= 256;
						}
					}
					else if (num3 == 32U)
					{
						num = 32;
						num2 = 57;
					}
					else
					{
						int num4 = 0;
						int num5 = -1;
						for (int j = 0; j < this.syms; j++)
						{
							num3 = X11Keyboard.XKeycodeToKeysym(this.display, i, j);
							if (num3 < 2048U && num3 != 32U)
							{
								array[j] = (int)((sbyte)(num3 & 255U));
							}
							else
							{
								array[j] = (int)((sbyte)this.MapDeadKeySym((int)num3));
							}
						}
						for (int k = 0; k < layout.Keys.Length; k++)
						{
							int num6 = Math.Min(layout.Keys[k].Length, 4);
							int num7 = -1;
							int num8 = 0;
							while (num7 != 0 && num8 < num6)
							{
								sbyte b = (sbyte)layout.Keys[k][num8];
								if ((int)b != array[num8])
								{
									num7 = 0;
								}
								if (num7 != 0 || num8 > num4)
								{
									num4 = num8;
									num5 = k;
								}
								if (num7 != 0)
								{
									break;
								}
								num8++;
							}
						}
						if (num5 >= 0)
						{
							if (num5 < layouts.scan_table[(int)layout.ScanIndex].Length)
							{
								num2 = (int)layouts.scan_table[(int)layout.ScanIndex][num5];
							}
							if (num5 < layouts.vkey_table[(int)layout.VKeyIndex].Length)
							{
								num = layouts.vkey_table[(int)layout.VKeyIndex][num5];
							}
						}
					}
				}
				this.keyc2vkey[xevent.KeyEvent.keycode] = num;
				this.keyc2scan[xevent.KeyEvent.keycode] = num2;
			}
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x0011332C File Offset: 0x0011152C
		private KeyboardLayout DetectLayout(KeyboardLayouts layouts)
		{
			X11Keyboard.XDisplayKeycodes(this.display, out this.min_keycode, out this.max_keycode);
			IntPtr intPtr = X11Keyboard.XGetKeyboardMapping(this.display, (byte)this.min_keycode, this.max_keycode + 1 - this.min_keycode, out this.keysyms_per_keycode);
			object xlibLock = X11Keyboard.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XFree(intPtr);
			}
			this.syms = this.keysyms_per_keycode;
			if (this.syms > 4)
			{
				this.syms = 2;
			}
			XModifierKeymap xmodifierKeymap = default(XModifierKeymap);
			IntPtr intPtr2 = X11Keyboard.XGetModifierMapping(this.display);
			xmodifierKeymap = (XModifierKeymap)Marshal.PtrToStructure(intPtr2, typeof(XModifierKeymap));
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				int j = 0;
				while (j < xmodifierKeymap.max_keypermod)
				{
					byte b = Marshal.ReadByte(xmodifierKeymap.modifiermap, num);
					if (b != 0)
					{
						for (int k = 0; k < this.keysyms_per_keycode; k++)
						{
							if (X11Keyboard.XKeycodeToKeysym(this.display, (int)b, k) == 65407U)
							{
								this.NumLockMask = 1 << i;
							}
						}
					}
					j++;
					num++;
				}
			}
			X11Keyboard.XFreeModifiermap(intPtr2);
			int[] array = new int[4];
			KeyboardLayout keyboardLayout = null;
			int num2 = 0;
			int num3 = 0;
			foreach (KeyboardLayout keyboardLayout2 in layouts.Layouts)
			{
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				int num9 = -1;
				int m = this.min_keycode;
				for (int n = this.min_keycode; n <= this.max_keycode; n++)
				{
					for (int num10 = 0; num10 < this.syms; num10++)
					{
						uint num11 = X11Keyboard.XKeycodeToKeysym(this.display, n, num10);
						if (num11 < 2048U && num11 != 32U)
						{
							array[num10] = (int)((sbyte)(num11 & 255U));
						}
						else
						{
							array[num10] = (int)((sbyte)this.MapDeadKeySym((int)num11));
						}
					}
					if (array[0] != 0)
					{
						for (m = 0; m < keyboardLayout2.Keys.Length; m++)
						{
							int num12 = Math.Min(this.syms, keyboardLayout2.Keys[m].Length);
							num4 = 0;
							int num10 = 0;
							while (num4 >= 0 && num10 < num12)
							{
								sbyte b2 = (sbyte)keyboardLayout2.Keys[m][num10];
								if ((int)b2 != 0 && (int)b2 == array[num10])
								{
									num4++;
								}
								if ((int)b2 != 0 && (int)b2 != array[num10])
								{
									num4 = -1;
								}
								num10++;
							}
							if (num4 > 0)
							{
								num5 += num4;
								break;
							}
						}
						if (num4 > 0)
						{
							num6++;
							if (m > num9)
							{
								num8++;
							}
							num9 = m;
						}
						else
						{
							num7++;
							num5 -= this.syms;
						}
					}
				}
				if (num5 > num2 || (num5 == num2 && num8 > num3))
				{
					keyboardLayout = keyboardLayout2;
					num2 = num5;
					num3 = num8;
				}
			}
			if (keyboardLayout != null)
			{
				return keyboardLayout;
			}
			Console.WriteLine(Locale.GetText("Keyboard layout not recognized, using default layout: " + layouts.Layouts[0].Name));
			return layouts.Layouts[0];
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x001136B8 File Offset: 0x001118B8
		private int MapDeadKeySym(int val)
		{
			switch (val)
			{
			case 65104:
				return 96;
			case 65105:
				return 180;
			case 65106:
				return 94;
			case 65107:
				break;
			case 65108:
				return 45;
			case 65109:
				return 162;
			case 65110:
				return 255;
			case 65111:
				return 168;
			case 65112:
				return 48;
			case 65113:
				return 189;
			case 65114:
				return 183;
			case 65115:
				return 184;
			case 65116:
				return 178;
			default:
				switch (val)
				{
				case 268500574:
					return 94;
				default:
					if (val == 268500514)
					{
						return 168;
					}
					if (val == 268500519)
					{
						return 180;
					}
					if (val != 268500606)
					{
						return 0;
					}
					break;
				case 268500576:
					return 96;
				}
				break;
			}
			return 126;
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x00113788 File Offset: 0x00111988
		private XIMProperties[] GetSupportedInputStyles(IntPtr xim)
		{
			IntPtr intPtr;
			if (X11Keyboard.XGetIMValues(xim, "queryInputStyle", out intPtr, IntPtr.Zero) != null || intPtr == IntPtr.Zero)
			{
				return new XIMProperties[0];
			}
			XIMStyles ximstyles = (XIMStyles)Marshal.PtrToStructure(intPtr, typeof(XIMStyles));
			XIMProperties[] array = new XIMProperties[(int)ximstyles.count_styles];
			for (int i = 0; i < (int)ximstyles.count_styles; i++)
			{
				array[i] = (XIMProperties)((int)Marshal.PtrToStructure(new IntPtr((long)ximstyles.supported_styles + (long)(i * Marshal.SizeOf(typeof(IntPtr)))), typeof(XIMProperties)));
			}
			object xlibLock = X11Keyboard.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XFree(intPtr);
			}
			return array;
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x00113880 File Offset: 0x00111A80
		private XIMProperties[] GetPreferredStyles()
		{
			string text = Environment.GetEnvironmentVariable("MONO_WINFORMS_XIM_STYLE");
			if (text == null)
			{
				text = "over-the-spot";
			}
			string[] array = text.Split(new char[] { ' ' });
			XIMProperties[] array2 = new XIMProperties[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = text2;
				if (text3 != null)
				{
					if (X11Keyboard.<>f__switch$mapD == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
						dictionary.Add("over-the-spot", 0);
						dictionary.Add("on-the-spot", 1);
						dictionary.Add("root", 2);
						X11Keyboard.<>f__switch$mapD = dictionary;
					}
					int num;
					if (X11Keyboard.<>f__switch$mapD.TryGetValue(text3, ref num))
					{
						switch (num)
						{
						case 0:
							array2[i] = XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing;
							break;
						case 1:
							array2[i] = XIMProperties.XIMPreeditCallbacks | XIMProperties.XIMStatusNothing;
							break;
						case 2:
							array2[i] = XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing;
							break;
						}
					}
				}
			}
			return array2;
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x00113978 File Offset: 0x00111B78
		private IEnumerable GetMatchingStylesInPreferredOrder(IntPtr xim)
		{
			XIMProperties[] supportedStyles = this.GetSupportedInputStyles(xim);
			foreach (XIMProperties p in this.GetPreferredStyles())
			{
				if (Array.IndexOf<XIMProperties>(supportedStyles, p) >= 0)
				{
					yield return p;
				}
			}
			yield break;
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x001139AC File Offset: 0x00111BAC
		private IntPtr CreateXic(IntPtr window, IntPtr xim)
		{
			IntPtr intPtr = IntPtr.Zero;
			foreach (object obj in this.GetMatchingStylesInPreferredOrder(xim))
			{
				XIMProperties ximproperties = (XIMProperties)((int)obj);
				this.ximStyle = ximproperties;
				XIMProperties ximproperties2 = ximproperties;
				switch (ximproperties2)
				{
				case XIMProperties.XIMPreeditCallbacks | XIMProperties.XIMStatusNothing:
					intPtr = this.CreateOnTheSpotXic(window, xim);
					if (intPtr != IntPtr.Zero)
					{
					}
					break;
				default:
					if (ximproperties2 == (XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing))
					{
						intPtr = X11Keyboard.XCreateIC(xim, "inputStyle", XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing, "clientWindow", window, IntPtr.Zero);
					}
					break;
				case XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing:
					intPtr = this.CreateOverTheSpotXic(window, xim);
					if (intPtr != IntPtr.Zero)
					{
					}
					break;
				}
			}
			if (intPtr == IntPtr.Zero)
			{
				this.ximStyle = XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing;
				intPtr = X11Keyboard.XCreateIC(xim, "inputStyle", XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing, "clientWindow", window, IntPtr.Zero);
			}
			return intPtr;
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x00113AF0 File Offset: 0x00111CF0
		private IntPtr CreateOverTheSpotXic(IntPtr window, IntPtr xim)
		{
			Control control = Control.FromHandle(window);
			string text = string.Format("-*-*-*-*-*-*-{0}-*-*-*-*-*-*-*", (int)control.Font.Size);
			IntPtr intPtr2;
			int num;
			IntPtr intPtr = X11Keyboard.XCreateFontSet(this.display, text, out intPtr2, out num, IntPtr.Zero);
			XPoint xpoint = new XPoint();
			xpoint.X = 0;
			xpoint.Y = 0;
			IntPtr intPtr3 = IntPtr.Zero;
			IntPtr intPtr4 = IntPtr.Zero;
			IntPtr intPtr6;
			try
			{
				intPtr3 = Marshal.StringToHGlobalAnsi("spotLocation");
				intPtr4 = Marshal.StringToHGlobalAnsi("fontSet");
				IntPtr intPtr5 = X11Keyboard.XVaCreateNestedList(0, intPtr3, xpoint, intPtr4, intPtr, IntPtr.Zero);
				intPtr6 = X11Keyboard.XCreateIC(xim, "inputStyle", XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing, "clientWindow", window, "preeditAttributes", intPtr5, IntPtr.Zero);
			}
			finally
			{
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
				if (intPtr4 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr4);
				}
				X11Keyboard.XFreeStringList(intPtr2);
			}
			return intPtr6;
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x00113C0C File Offset: 0x00111E0C
		private IntPtr CreateOnTheSpotXic(IntPtr window, IntPtr xim)
		{
			this.callbackContext = new X11Keyboard.XIMCallbackContext(window);
			return this.callbackContext.CreateXic(window, xim);
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x00113C28 File Offset: 0x00111E28
		internal void SetCaretPos(CaretStruct caret, IntPtr handle, int x, int y)
		{
			if (this.ximStyle != (XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing))
			{
				return;
			}
			if (this.positionContext == null)
			{
				this.positionContext = new X11Keyboard.XIMPositionContext();
			}
			this.positionContext.Caret = caret;
			this.positionContext.X = x;
			this.positionContext.Y = y + caret.Height;
			this.MoveCurrentCaretPos();
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x00113C90 File Offset: 0x00111E90
		internal void MoveCurrentCaretPos()
		{
			if (this.positionContext == null || this.ximStyle != (XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing) || this.client_window == IntPtr.Zero)
			{
				return;
			}
			int x = this.positionContext.X;
			int y = this.positionContext.Y;
			CaretStruct caret = this.positionContext.Caret;
			IntPtr xic = this.GetXic(this.client_window);
			if (xic == IntPtr.Zero)
			{
				return;
			}
			Control control = Control.FromHandle(this.client_window);
			if (control == null || !control.IsHandleCreated)
			{
				return;
			}
			control = Control.FromHandle(caret.Hwnd);
			if (control == null || !control.IsHandleCreated)
			{
				return;
			}
			Hwnd hwnd = Hwnd.ObjectFromHandle(this.client_window);
			if (!hwnd.mapped)
			{
				return;
			}
			object xlibLock = X11Keyboard.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(this.display, this.client_window, this.client_window, x, y, out num, out num2, out intPtr);
			}
			XPoint xpoint = new XPoint();
			xpoint.X = (short)num;
			xpoint.Y = (short)num2;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				intPtr2 = Marshal.StringToHGlobalAnsi("spotLocation");
				IntPtr intPtr3 = X11Keyboard.XVaCreateNestedList(0, intPtr2, xpoint, IntPtr.Zero);
				X11Keyboard.XSetICValues(xic, "preeditAttributes", intPtr3, IntPtr.Zero);
			}
			finally
			{
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x00113E4C File Offset: 0x0011204C
		private int LookupString(ref XEvent xevent, int len, out XKeySym keysym, out IntPtr status)
		{
			status = IntPtr.Zero;
			IntPtr xic = this.GetXic(this.client_window);
			IntPtr intPtr;
			int num;
			if (xic != IntPtr.Zero)
			{
				for (;;)
				{
					num = X11Keyboard.Xutf8LookupString(xic, ref xevent, this.utf8_buffer, 100, out intPtr, out status);
					if ((int)status != -1)
					{
						break;
					}
					this.utf8_buffer = new byte[this.utf8_buffer.Length << 1];
				}
				this.lookup_buffer.Length = 0;
				string @string = Encoding.UTF8.GetString(this.utf8_buffer, 0, num);
				this.lookup_buffer.Append(@string);
				keysym = (XKeySym)intPtr.ToInt32();
				return @string.Length;
			}
			this.lookup_buffer.Length = 0;
			num = X11Keyboard.XLookupString(ref xevent, this.lookup_buffer, len, out intPtr, IntPtr.Zero);
			keysym = (XKeySym)intPtr.ToInt32();
			return num;
		}

		// Token: 0x06004600 RID: 17920
		[DllImport("libX11")]
		private static extern IntPtr XOpenIM(IntPtr display, IntPtr rdb, IntPtr res_name, IntPtr res_class);

		// Token: 0x06004601 RID: 17921
		[DllImport("libX11", CallingConvention = 2)]
		private static extern IntPtr XCreateIC(IntPtr xim, string name, XIMProperties im_style, string name2, IntPtr value2, IntPtr terminator);

		// Token: 0x06004602 RID: 17922
		[DllImport("libX11", CallingConvention = 2)]
		private static extern IntPtr XCreateIC(IntPtr xim, string name, XIMProperties im_style, string name2, IntPtr value2, string name3, IntPtr value3, IntPtr terminator);

		// Token: 0x06004603 RID: 17923
		[DllImport("libX11", CallingConvention = 2)]
		private static extern IntPtr XVaCreateNestedList(int dummy, IntPtr name0, XPoint value0, IntPtr terminator);

		// Token: 0x06004604 RID: 17924
		[DllImport("libX11", CallingConvention = 2)]
		private static extern IntPtr XVaCreateNestedList(int dummy, IntPtr name0, XPoint value0, IntPtr name1, IntPtr value1, IntPtr terminator);

		// Token: 0x06004605 RID: 17925
		[DllImport("libX11", CallingConvention = 2)]
		private static extern IntPtr XVaCreateNestedList(int dummy, IntPtr name0, IntPtr value0, IntPtr name1, IntPtr value1, IntPtr name2, IntPtr value2, IntPtr name3, IntPtr value3, IntPtr terminator);

		// Token: 0x06004606 RID: 17926
		[DllImport("libX11")]
		private static extern IntPtr XCreateFontSet(IntPtr display, string name, out IntPtr list, out int count, IntPtr terminator);

		// Token: 0x06004607 RID: 17927
		[DllImport("libX11")]
		internal static extern void XFreeFontSet(IntPtr data);

		// Token: 0x06004608 RID: 17928
		[DllImport("libX11")]
		private static extern void XFreeStringList(IntPtr ptr);

		// Token: 0x06004609 RID: 17929
		[DllImport("libX11")]
		private static extern void XCloseIM(IntPtr xim);

		// Token: 0x0600460A RID: 17930
		[DllImport("libX11")]
		private static extern void XDestroyIC(IntPtr xic);

		// Token: 0x0600460B RID: 17931
		[DllImport("libX11")]
		private static extern string XGetIMValues(IntPtr xim, string name, out IntPtr value, IntPtr terminator);

		// Token: 0x0600460C RID: 17932
		[DllImport("libX11")]
		private static extern string XGetICValues(IntPtr xic, string name, out EventMask value, IntPtr terminator);

		// Token: 0x0600460D RID: 17933
		[DllImport("libX11", CallingConvention = 2)]
		private static extern void XSetICValues(IntPtr xic, string name, IntPtr value, IntPtr terminator);

		// Token: 0x0600460E RID: 17934
		[DllImport("libX11")]
		private static extern void XSetICFocus(IntPtr xic);

		// Token: 0x0600460F RID: 17935
		[DllImport("libX11")]
		private static extern void XUnsetICFocus(IntPtr xic);

		// Token: 0x06004610 RID: 17936
		[DllImport("libX11")]
		private static extern string Xutf8ResetIC(IntPtr xic);

		// Token: 0x06004611 RID: 17937
		[DllImport("libX11")]
		private static extern bool XSupportsLocale();

		// Token: 0x06004612 RID: 17938
		[DllImport("libX11")]
		private static extern bool XSetLocaleModifiers(string mods);

		// Token: 0x06004613 RID: 17939
		[DllImport("libX11")]
		internal static extern int XLookupString(ref XEvent xevent, StringBuilder buffer, int num_bytes, out IntPtr keysym, IntPtr status);

		// Token: 0x06004614 RID: 17940
		[DllImport("libX11")]
		internal static extern int Xutf8LookupString(IntPtr xic, ref XEvent xevent, byte[] buffer, int num_bytes, out IntPtr keysym, out IntPtr status);

		// Token: 0x06004615 RID: 17941
		[DllImport("libX11")]
		private static extern IntPtr XGetKeyboardMapping(IntPtr display, byte first_keycode, int keycode_count, out int keysyms_per_keycode_return);

		// Token: 0x06004616 RID: 17942
		[DllImport("libX11")]
		private static extern void XDisplayKeycodes(IntPtr display, out int min, out int max);

		// Token: 0x06004617 RID: 17943
		[DllImport("libX11")]
		private static extern uint XKeycodeToKeysym(IntPtr display, int keycode, int index);

		// Token: 0x06004618 RID: 17944
		[DllImport("libX11")]
		private static extern int XKeysymToKeycode(IntPtr display, IntPtr keysym);

		// Token: 0x06004619 RID: 17945 RVA: 0x00113F28 File Offset: 0x00112128
		private static int XKeysymToKeycode(IntPtr display, int keysym)
		{
			return X11Keyboard.XKeysymToKeycode(display, (IntPtr)keysym);
		}

		// Token: 0x0600461A RID: 17946
		[DllImport("libX11")]
		internal static extern IntPtr XGetModifierMapping(IntPtr display);

		// Token: 0x0600461B RID: 17947
		[DllImport("libX11")]
		internal static extern int XFreeModifiermap(IntPtr modmap);

		// Token: 0x04001DAA RID: 7594
		private const XIMProperties styleRoot = XIMProperties.XIMPreeditNothing | XIMProperties.XIMStatusNothing;

		// Token: 0x04001DAB RID: 7595
		private const XIMProperties styleOverTheSpot = XIMProperties.XIMPreeditPosition | XIMProperties.XIMStatusNothing;

		// Token: 0x04001DAC RID: 7596
		private const XIMProperties styleOnTheSpot = XIMProperties.XIMPreeditCallbacks | XIMProperties.XIMStatusNothing;

		// Token: 0x04001DAD RID: 7597
		private const string ENV_NAME_XIM_STYLE = "MONO_WINFORMS_XIM_STYLE";

		// Token: 0x04001DAE RID: 7598
		internal static object XlibLock;

		// Token: 0x04001DAF RID: 7599
		private IntPtr display;

		// Token: 0x04001DB0 RID: 7600
		private IntPtr client_window;

		// Token: 0x04001DB1 RID: 7601
		private IntPtr xim;

		// Token: 0x04001DB2 RID: 7602
		private Hashtable xic_table = new Hashtable();

		// Token: 0x04001DB3 RID: 7603
		private X11Keyboard.XIMPositionContext positionContext;

		// Token: 0x04001DB4 RID: 7604
		private X11Keyboard.XIMCallbackContext callbackContext;

		// Token: 0x04001DB5 RID: 7605
		private XIMProperties ximStyle;

		// Token: 0x04001DB6 RID: 7606
		private EventMask xic_event_mask;

		// Token: 0x04001DB7 RID: 7607
		private StringBuilder lookup_buffer;

		// Token: 0x04001DB8 RID: 7608
		private byte[] utf8_buffer;

		// Token: 0x04001DB9 RID: 7609
		private int min_keycode;

		// Token: 0x04001DBA RID: 7610
		private int max_keycode;

		// Token: 0x04001DBB RID: 7611
		private int keysyms_per_keycode;

		// Token: 0x04001DBC RID: 7612
		private int syms;

		// Token: 0x04001DBD RID: 7613
		private int[] keyc2vkey = new int[256];

		// Token: 0x04001DBE RID: 7614
		private int[] keyc2scan = new int[256];

		// Token: 0x04001DBF RID: 7615
		private byte[] key_state_table = new byte[256];

		// Token: 0x04001DC0 RID: 7616
		private int lcid;

		// Token: 0x04001DC1 RID: 7617
		private bool num_state;

		// Token: 0x04001DC2 RID: 7618
		private bool cap_state;

		// Token: 0x04001DC3 RID: 7619
		private bool initialized;

		// Token: 0x04001DC4 RID: 7620
		private bool menu_state;

		// Token: 0x04001DC5 RID: 7621
		private int NumLockMask;

		// Token: 0x04001DC6 RID: 7622
		private int AltGrMask;

		// Token: 0x04001DC7 RID: 7623
		private string stored_keyevent_string;

		// Token: 0x04001DC8 RID: 7624
		private static readonly int[] nonchar_key_vkey = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 8, 9,
			0, 12, 0, 13, 0, 0, 0, 0, 0, 19,
			145, 0, 0, 0, 0, 0, 0, 27, 0, 0,
			0, 0, 0, 0, 29, 28, 0, 0, 0, 0,
			0, 0, 243, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			36, 37, 38, 39, 40, 33, 34, 35, 0, 0,
			0, 0, 0, 0, 0, 0, 41, 44, 43, 45,
			0, 0, 0, 0, 3, 47, 3, 3, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 144, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 13, 0, 0, 0, 0, 0, 0, 0, 36,
			37, 38, 39, 40, 33, 34, 35, 0, 45, 46,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			106, 107, 108, 109, 110, 111, 96, 97, 98, 99,
			100, 101, 102, 103, 104, 105, 0, 0, 0, 0,
			112, 113, 114, 115, 116, 117, 118, 119, 120, 121,
			122, 123, 124, 125, 126, 127, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 16, 16, 17, 17, 20,
			0, 18, 18, 18, 18, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 46
		};

		// Token: 0x04001DC9 RID: 7625
		private static readonly int[] nonchar_key_scan = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 14, 15,
			0, 0, 0, 28, 0, 0, 0, 0, 0, 69,
			70, 0, 0, 0, 0, 0, 0, 1, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			327, 331, 328, 333, 336, 329, 337, 335, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 311, 0, 338,
			0, 0, 0, 0, 0, 0, 56, 326, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 312, 325, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 284, 0, 0, 0, 0, 0, 0, 0, 71,
			75, 72, 77, 80, 73, 81, 79, 76, 82, 83,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			55, 78, 0, 74, 83, 309, 82, 79, 80, 81,
			75, 76, 77, 71, 72, 73, 0, 0, 0, 0,
			59, 60, 61, 62, 63, 64, 65, 66, 67, 68,
			87, 88, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 42, 54, 29, 285, 58,
			0, 56, 312, 56, 312, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 339
		};

		// Token: 0x04001DCA RID: 7626
		private static readonly int[] nonchar_vkey_key;

		// Token: 0x020003D6 RID: 982
		private class XIMCallbackContext
		{
			// Token: 0x0600461C RID: 17948 RVA: 0x00113F38 File Offset: 0x00112138
			public XIMCallbackContext(IntPtr clientWindow)
			{
				this.startCB = new XIMCallback(IntPtr.Zero, new XIMProc(this.DoPreeditStart));
				this.doneCB = new XIMCallback(IntPtr.Zero, new XIMProc(this.DoPreeditDone));
				this.drawCB = new XIMCallback(IntPtr.Zero, new XIMProc(this.DoPreeditDraw));
				this.caretCB = new XIMCallback(IntPtr.Zero, new XIMProc(this.DoPreeditCaret));
				this.pStartCB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(XIMCallback)));
				this.pDoneCB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(XIMCallback)));
				this.pDrawCB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(XIMCallback)));
				this.pCaretCB = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(XIMCallback)));
				this.pStartCBN = Marshal.StringToHGlobalAnsi("preeditStartCallback");
				this.pDoneCBN = Marshal.StringToHGlobalAnsi("preeditDoneCallback");
				this.pDrawCBN = Marshal.StringToHGlobalAnsi("preeditDrawCallback");
				this.pCaretCBN = Marshal.StringToHGlobalAnsi("preeditCaretCallback");
			}

			// Token: 0x0600461D RID: 17949 RVA: 0x001140BC File Offset: 0x001122BC
			~XIMCallbackContext()
			{
				if (this.pStartCBN != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pStartCBN);
				}
				if (this.pDoneCBN != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pDoneCBN);
				}
				if (this.pDrawCBN != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pDrawCBN);
				}
				if (this.pCaretCBN != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pCaretCBN);
				}
				if (this.pStartCB != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pStartCB);
				}
				if (this.pDoneCB != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pDoneCB);
				}
				if (this.pDrawCB != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pDrawCB);
				}
				if (this.pCaretCB != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pCaretCB);
				}
			}

			// Token: 0x0600461E RID: 17950 RVA: 0x001141F4 File Offset: 0x001123F4
			private int DoPreeditStart(IntPtr xic, IntPtr clientData, IntPtr callData)
			{
				Console.WriteLine("DoPreeditStart");
				return 100;
			}

			// Token: 0x0600461F RID: 17951 RVA: 0x00114204 File Offset: 0x00112404
			private int DoPreeditDone(IntPtr xic, IntPtr clientData, IntPtr callData)
			{
				Console.WriteLine("DoPreeditDone");
				return 0;
			}

			// Token: 0x06004620 RID: 17952 RVA: 0x00114214 File Offset: 0x00112414
			private int DoPreeditDraw(IntPtr xic, IntPtr clientData, IntPtr callData)
			{
				Console.WriteLine("DoPreeditDraw");
				return 0;
			}

			// Token: 0x06004621 RID: 17953 RVA: 0x00114224 File Offset: 0x00112424
			private int DoPreeditCaret(IntPtr xic, IntPtr clientData, IntPtr callData)
			{
				Console.WriteLine("DoPreeditCaret");
				return 0;
			}

			// Token: 0x06004622 RID: 17954 RVA: 0x00114234 File Offset: 0x00112434
			public IntPtr CreateXic(IntPtr window, IntPtr xim)
			{
				Marshal.StructureToPtr(this.startCB, this.pStartCB, false);
				Marshal.StructureToPtr(this.doneCB, this.pDoneCB, false);
				Marshal.StructureToPtr(this.drawCB, this.pDrawCB, false);
				Marshal.StructureToPtr(this.caretCB, this.pCaretCB, false);
				IntPtr intPtr = X11Keyboard.XVaCreateNestedList(0, this.pStartCBN, this.pStartCB, this.pDoneCBN, this.pDoneCB, this.pDrawCBN, this.pDrawCB, this.pCaretCBN, this.pCaretCB, IntPtr.Zero);
				return X11Keyboard.XCreateIC(xim, "inputStyle", XIMProperties.XIMPreeditCallbacks | XIMProperties.XIMStatusNothing, "clientWindow", window, "preeditAttributes", intPtr, IntPtr.Zero);
			}

			// Token: 0x04001DCC RID: 7628
			private XIMCallback startCB;

			// Token: 0x04001DCD RID: 7629
			private XIMCallback doneCB;

			// Token: 0x04001DCE RID: 7630
			private XIMCallback drawCB;

			// Token: 0x04001DCF RID: 7631
			private XIMCallback caretCB;

			// Token: 0x04001DD0 RID: 7632
			private IntPtr pStartCB = IntPtr.Zero;

			// Token: 0x04001DD1 RID: 7633
			private IntPtr pDoneCB = IntPtr.Zero;

			// Token: 0x04001DD2 RID: 7634
			private IntPtr pDrawCB = IntPtr.Zero;

			// Token: 0x04001DD3 RID: 7635
			private IntPtr pCaretCB = IntPtr.Zero;

			// Token: 0x04001DD4 RID: 7636
			private IntPtr pStartCBN = IntPtr.Zero;

			// Token: 0x04001DD5 RID: 7637
			private IntPtr pDoneCBN = IntPtr.Zero;

			// Token: 0x04001DD6 RID: 7638
			private IntPtr pDrawCBN = IntPtr.Zero;

			// Token: 0x04001DD7 RID: 7639
			private IntPtr pCaretCBN = IntPtr.Zero;
		}

		// Token: 0x020003D7 RID: 983
		private class XIMPositionContext
		{
			// Token: 0x04001DD8 RID: 7640
			public CaretStruct Caret;

			// Token: 0x04001DD9 RID: 7641
			public int X;

			// Token: 0x04001DDA RID: 7642
			public int Y;
		}
	}
}
