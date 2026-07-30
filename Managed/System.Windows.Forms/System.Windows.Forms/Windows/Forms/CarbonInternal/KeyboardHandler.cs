using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004B0 RID: 1200
	internal class KeyboardHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06004BEE RID: 19438 RVA: 0x0012DF18 File Offset: 0x0012C118
		internal KeyboardHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x0012DF24 File Offset: 0x0012C124
		static KeyboardHandler()
		{
			byte[] array = new byte[256];
			array[16] = 1;
			array[28] = 1;
			array[29] = 1;
			array[30] = 1;
			array[31] = 1;
			KeyboardHandler.key_filter_table = array;
			KeyboardHandler.char_translation_table = new byte[]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
				20, 21, 22, 23, 24, 25, 26, 27, 37, 39,
				38, 40, 32, 49, 34, 51, 52, 53, 55, 222,
				57, 48, 56, 187, 188, 189, 190, 191, 48, 49,
				50, 51, 52, 53, 54, 55, 56, 57, 58, 186,
				60, 61, 62, 63, 50, 65, 66, 67, 68, 187,
				70, 71, 72, 73, 74, 75, 76, 77, 78, 79,
				80, 81, 82, 83, 84, 85, 86, 87, 88, 89,
				90, 219, 220, 221, 54, 189, 192, 65, 66, 67,
				68, 69, 70, 71, 72, 73, 74, 75, 76, 77,
				78, 79, 80, 81, 82, 83, 84, 85, 86, 87,
				88, 89, 90, 123, 124, 125, 126, 46, 128, 129,
				130, 131, 132, 133, 134, 135, 136, 137, 138, 139,
				140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
				150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
				160, 161, 162, 163, 164, 165, 166, 167, 168, 169,
				170, 171, 172, 173, 174, 175, 176, 177, 178, 179,
				180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
				190, 191, 192, 193, 194, 195, 196, 197, 198, 199,
				200, 201, 202, 203, 204, 205, 206, 207, 208, 209,
				210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
				220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
				230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
				240, 241, 242, 243, 244, 245, 246, 247, 248, 249,
				250, 251, 252, 253, 254, byte.MaxValue
			};
			KeyboardHandler.key_translation_table = new byte[]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
				20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
				30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
				40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
				50, 51, 52, 53, 54, 55, 56, 57, 58, 59,
				60, 61, 62, 63, 64, 65, 66, 67, 68, 69,
				70, 71, 72, 73, 74, 75, 76, 77, 78, 79,
				80, 81, 82, 83, 84, 85, 86, 87, 88, 89,
				90, 91, 92, 93, 94, 95, 116, 117, 118, 114,
				119, 120, 121, 103, 104, 105, 106, 107, 108, 109,
				122, 123, 112, 113, 114, 115, 116, 117, 115, 119,
				113, 121, 112, 123, 124, 125, 126, 127, 128, 129,
				130, 131, 132, 133, 134, 135, 136, 137, 138, 139,
				140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
				150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
				160, 161, 162, 163, 164, 165, 166, 167, 168, 169,
				170, 171, 172, 173, 174, 175, 176, 177, 178, 179,
				180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
				190, 191, 192, 193, 194, 195, 196, 197, 198, 199,
				200, 201, 202, 203, 204, 205, 206, 207, 208, 209,
				210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
				220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
				230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
				240, 241, 242, 243, 244, 245, 246, 247, 248, 249,
				250, 251, 252, 253, 254, byte.MaxValue
			};
			KeyboardHandler.key_modifier_table = new byte[32];
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x0012DF9C File Offset: 0x0012C19C
		private void ModifierToVirtualKey(int i, ref MSG msg, bool down)
		{
			msg.hwnd = XplatUICarbon.FocusWindow;
			if (i == 9 || i == 13)
			{
				msg.message = ((!down) ? Msg.WM_KEYUP : Msg.WM_KEYDOWN);
				msg.wParam = (IntPtr)16;
				msg.lParam = IntPtr.Zero;
				return;
			}
			if (i == 12 || i == 14)
			{
				msg.message = ((!down) ? Msg.WM_KEYUP : Msg.WM_KEYDOWN);
				msg.wParam = (IntPtr)17;
				msg.lParam = IntPtr.Zero;
				return;
			}
			if (i == 8)
			{
				msg.message = ((!down) ? Msg.WM_SYSKEYUP : Msg.WM_SYSKEYDOWN);
				msg.wParam = (IntPtr)18;
				msg.lParam = new IntPtr(536870912);
				return;
			}
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x0012E07C File Offset: 0x0012C27C
		public void ProcessModifiers(IntPtr eventref, ref MSG msg)
		{
			uint num = 0U;
			KeyboardHandler.GetEventParameter(eventref, 1802334052U, 1835100014U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(uint)), IntPtr.Zero, ref num);
			for (int i = 0; i < 32; i++)
			{
				if (KeyboardHandler.key_modifier_table[i] == 1 && ((ulong)num & (ulong)(1L << (i & 31))) == 0UL)
				{
					this.ModifierToVirtualKey(i, ref msg, false);
					KeyboardHandler.key_modifier_table[i] = 0;
					return;
				}
				if (KeyboardHandler.key_modifier_table[i] == 0 && ((ulong)num & (ulong)(1L << (i & 31))) == (ulong)(1L << (i & 31)))
				{
					this.ModifierToVirtualKey(i, ref msg, true);
					KeyboardHandler.key_modifier_table[i] = 1;
					return;
				}
			}
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x0012E12C File Offset: 0x0012C32C
		public void ProcessText(IntPtr eventref, ref MSG msg)
		{
			uint num = 0U;
			IntPtr intPtr = IntPtr.Zero;
			KeyboardHandler.GetEventParameter(eventref, 1953723512U, 1970567284U, IntPtr.Zero, 0U, ref num, IntPtr.Zero);
			intPtr = Marshal.AllocHGlobal((int)num);
			byte[] array = new byte[num];
			KeyboardHandler.GetEventParameter(eventref, 1953723512U, 1970567284U, IntPtr.Zero, num, IntPtr.Zero, intPtr);
			Marshal.Copy(intPtr, array, 0, (int)num);
			Marshal.FreeHGlobal(intPtr);
			if (KeyboardHandler.key_filter_table[(int)array[0]] == 0)
			{
				if (num == 1U)
				{
					msg.message = Msg.WM_CHAR;
					msg.wParam = ((!BitConverter.IsLittleEndian) ? ((IntPtr)((int)array[(int)((UIntPtr)(num - 1U))])) : ((IntPtr)((int)array[0])));
					msg.lParam = IntPtr.Zero;
					msg.hwnd = XplatUICarbon.FocusWindow;
				}
				else
				{
					msg.message = Msg.WM_IME_COMPOSITION;
					Encoding encoding = ((!BitConverter.IsLittleEndian) ? Encoding.BigEndianUnicode : Encoding.Unicode);
					this.ComposedString = encoding.GetString(array);
					msg.hwnd = XplatUICarbon.FocusWindow;
				}
			}
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x0012E23C File Offset: 0x0012C43C
		public void ProcessKeyPress(IntPtr eventref, ref MSG msg)
		{
			byte b = 0;
			byte b2 = 0;
			KeyboardHandler.GetEventParameter(eventref, 1801676914U, 1413830740U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(byte)), IntPtr.Zero, ref b);
			KeyboardHandler.GetEventParameter(eventref, 1801678692U, 1835100014U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(byte)), IntPtr.Zero, ref b2);
			msg.lParam = (IntPtr)((int)b);
			msg.wParam = ((b != 16) ? ((IntPtr)((int)KeyboardHandler.char_translation_table[(int)b])) : ((IntPtr)((int)KeyboardHandler.key_translation_table[(int)b2])));
			msg.hwnd = XplatUICarbon.FocusWindow;
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x0012E2E8 File Offset: 0x0012C4E8
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			uint eventClass = EventHandler.GetEventClass(eventref);
			bool flag = true;
			if (eventClass == 1952807028U)
			{
				if (kind != 2U)
				{
					Console.WriteLine("WARNING: KeyboardHandler.ProcessEvent default handler for kEventClassTextInput should not be reached");
				}
				else
				{
					this.ProcessText(eventref, ref msg);
				}
			}
			else if (eventClass == 1801812322U)
			{
				switch (kind)
				{
				case 1U:
				case 2U:
					msg.message = Msg.WM_KEYDOWN;
					this.ProcessKeyPress(eventref, ref msg);
					break;
				case 3U:
					msg.message = Msg.WM_KEYUP;
					this.ProcessKeyPress(eventref, ref msg);
					break;
				case 4U:
					this.ProcessModifiers(eventref, ref msg);
					break;
				default:
					Console.WriteLine("WARNING: KeyboardHandler.ProcessEvent default handler for kEventClassKeyboard should not be reached");
					break;
				}
			}
			else
			{
				Console.WriteLine("WARNING: KeyboardHandler.ProcessEvent default handler for kEventClassTextInput should not be reached");
			}
			return flag;
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x0012E3C8 File Offset: 0x0012C5C8
		public bool TranslateMessage(ref MSG msg)
		{
			bool flag = false;
			if (msg.message >= Msg.WM_KEYDOWN && msg.message <= Msg.WM_KEYLAST)
			{
				flag = true;
			}
			if (msg.message != Msg.WM_KEYDOWN && msg.message != Msg.WM_SYSKEYDOWN && msg.message != Msg.WM_KEYUP && msg.message != Msg.WM_SYSKEYUP && msg.message != Msg.WM_CHAR && msg.message != Msg.WM_SYSCHAR)
			{
				return flag;
			}
			if (KeyboardHandler.key_modifier_table[8] == 1 && KeyboardHandler.key_modifier_table[12] == 0 && KeyboardHandler.key_modifier_table[14] == 0)
			{
				if (msg.message == Msg.WM_KEYDOWN)
				{
					msg.message = Msg.WM_SYSKEYDOWN;
				}
				else if (msg.message == Msg.WM_CHAR)
				{
					msg.message = Msg.WM_SYSCHAR;
					KeyboardHandler.translate_modifier = true;
				}
				else
				{
					if (msg.message != Msg.WM_KEYUP)
					{
						return flag;
					}
					msg.message = Msg.WM_SYSKEYUP;
				}
				msg.lParam = new IntPtr(536870912);
			}
			else if (msg.message == Msg.WM_SYSKEYUP && KeyboardHandler.translate_modifier && msg.wParam == (IntPtr)18)
			{
				msg.message = Msg.WM_KEYUP;
				msg.lParam = IntPtr.Zero;
				KeyboardHandler.translate_modifier = false;
			}
			return flag;
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06004BF6 RID: 19446 RVA: 0x0012E550 File Offset: 0x0012C750
		internal Keys ModifierKeys
		{
			get
			{
				Keys keys = Keys.None;
				if (KeyboardHandler.key_modifier_table[9] == 1 || KeyboardHandler.key_modifier_table[13] == 1)
				{
					keys |= Keys.Shift;
				}
				if (KeyboardHandler.key_modifier_table[8] == 1)
				{
					keys |= Keys.Alt;
				}
				if (KeyboardHandler.key_modifier_table[12] == 1 || KeyboardHandler.key_modifier_table[14] == 1)
				{
					keys |= Keys.Control;
				}
				return keys;
			}
		}

		// Token: 0x06004BF7 RID: 19447
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, ref uint outsize, IntPtr data);

		// Token: 0x06004BF8 RID: 19448
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, IntPtr data);

		// Token: 0x06004BF9 RID: 19449
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref byte data);

		// Token: 0x06004BFA RID: 19450
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref uint data);

		// Token: 0x04002930 RID: 10544
		internal const uint kEventRawKeyDown = 1U;

		// Token: 0x04002931 RID: 10545
		internal const uint kEventRawKeyRepeat = 2U;

		// Token: 0x04002932 RID: 10546
		internal const uint kEventRawKeyUp = 3U;

		// Token: 0x04002933 RID: 10547
		internal const uint kEventRawKeyModifiersChanged = 4U;

		// Token: 0x04002934 RID: 10548
		internal const uint kEventHotKeyPressed = 5U;

		// Token: 0x04002935 RID: 10549
		internal const uint kEventHotKeyReleased = 6U;

		// Token: 0x04002936 RID: 10550
		internal const uint kEventParamKeyMacCharCodes = 1801676914U;

		// Token: 0x04002937 RID: 10551
		internal const uint kEventParamKeyCode = 1801678692U;

		// Token: 0x04002938 RID: 10552
		internal const uint kEventParamKeyModifiers = 1802334052U;

		// Token: 0x04002939 RID: 10553
		internal const uint kEventTextInputUnicodeForKeyEvent = 2U;

		// Token: 0x0400293A RID: 10554
		internal const uint kEventParamTextInputSendText = 1953723512U;

		// Token: 0x0400293B RID: 10555
		internal const uint typeChar = 1413830740U;

		// Token: 0x0400293C RID: 10556
		internal const uint typeUInt32 = 1835100014U;

		// Token: 0x0400293D RID: 10557
		internal const uint typeUnicodeText = 1970567284U;

		// Token: 0x0400293E RID: 10558
		internal static byte[] key_filter_table;

		// Token: 0x0400293F RID: 10559
		internal static byte[] key_modifier_table;

		// Token: 0x04002940 RID: 10560
		internal static byte[] key_translation_table;

		// Token: 0x04002941 RID: 10561
		internal static byte[] char_translation_table;

		// Token: 0x04002942 RID: 10562
		internal static bool translate_modifier;

		// Token: 0x04002943 RID: 10563
		internal string ComposedString;
	}
}
