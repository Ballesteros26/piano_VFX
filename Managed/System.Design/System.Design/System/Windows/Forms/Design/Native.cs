using System;
using System.Drawing;
using System.Reflection;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000030 RID: 48
	internal class Native
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00005434 File Offset: 0x00003634
		static Native()
		{
			Assembly assembly = Assembly.Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			if (assembly == null)
			{
				throw new InvalidOperationException("Can't load System.Windows.Forms assembly.");
			}
			Native._xplatuiType = assembly.GetType("System.Windows.Forms.XplatUI");
			if (Native._xplatuiType == null)
			{
				throw new InvalidOperationException("Can't find the System.Windows.Forms.XplatUI type.");
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005486 File Offset: 0x00003686
		private static object InvokeMethod(string methodName, object[] args)
		{
			return Native.InvokeMethod(methodName, args, null);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005490 File Offset: 0x00003690
		private static object InvokeMethod(string methodName, object[] args, Type[] types)
		{
			MethodInfo methodInfo;
			if (types != null)
			{
				methodInfo = Native._xplatuiType.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, types, null);
			}
			else
			{
				methodInfo = Native._xplatuiType.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
			}
			if (methodInfo == null)
			{
				throw new InvalidOperationException(methodName + " not found!");
			}
			return methodInfo.Invoke(null, args);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000054EC File Offset: 0x000036EC
		public static void DefWndProc(ref Message m)
		{
			object[] array = new object[] { m };
			m.Result = (IntPtr)Native.InvokeMethod("DefWndProc", array);
			m = (Message)array[0];
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005534 File Offset: 0x00003734
		public static IntPtr SendMessage(IntPtr hwnd, Native.Msg message, IntPtr wParam, IntPtr lParam)
		{
			Type type = Assembly.Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089").GetType("System.Windows.Forms.Message&");
			object[] array = new object[] { Message.Create(hwnd, (int)message, wParam, lParam) };
			Native.InvokeMethod("SendMessage", array, new Type[] { type });
			return ((Message)array[0]).Result;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005594 File Offset: 0x00003794
		public static Point PointToClient(Control control, Point point)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			object[] array = new object[] { control.Handle, point.X, point.Y };
			Native.InvokeMethod("ScreenToClient", array);
			return new Point((int)array[1], (int)array[2]);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005603 File Offset: 0x00003803
		public static IntPtr SetParent(IntPtr childHandle, IntPtr parentHandle)
		{
			return (IntPtr)Native.InvokeMethod("SetParent", new object[] { childHandle, parentHandle });
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000562C File Offset: 0x0000382C
		public static int HiWord(int dword)
		{
			return (dword >> 16) & 65535;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005638 File Offset: 0x00003838
		public static int LoWord(int dword)
		{
			return dword & 65535;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005641 File Offset: 0x00003841
		public static IntPtr LParam(int hiword, int loword)
		{
			return (IntPtr)((loword << 16) | (hiword & 65535));
		}

		// Token: 0x04000083 RID: 131
		private static Type _xplatuiType;

		// Token: 0x02000031 RID: 49
		public enum Msg
		{
			// Token: 0x04000085 RID: 133
			WM_CREATE = 1,
			// Token: 0x04000086 RID: 134
			WM_SETFOCUS = 7,
			// Token: 0x04000087 RID: 135
			WM_PAINT = 15,
			// Token: 0x04000088 RID: 136
			WM_CANCELMODE = 31,
			// Token: 0x04000089 RID: 137
			WM_SETCURSOR,
			// Token: 0x0400008A RID: 138
			WM_CONTEXTMENU = 123,
			// Token: 0x0400008B RID: 139
			WM_NCHITTEST = 132,
			// Token: 0x0400008C RID: 140
			WM_GETOBJECT = 61,
			// Token: 0x0400008D RID: 141
			WM_MOUSEFIRST = 512,
			// Token: 0x0400008E RID: 142
			WM_MOUSEMOVE = 512,
			// Token: 0x0400008F RID: 143
			WM_LBUTTONDOWN,
			// Token: 0x04000090 RID: 144
			WM_LBUTTONUP,
			// Token: 0x04000091 RID: 145
			WM_LBUTTONDBLCLK,
			// Token: 0x04000092 RID: 146
			WM_RBUTTONDOWN,
			// Token: 0x04000093 RID: 147
			WM_RBUTTONUP,
			// Token: 0x04000094 RID: 148
			WM_RBUTTONDBLCLK,
			// Token: 0x04000095 RID: 149
			WM_MBUTTONDOWN,
			// Token: 0x04000096 RID: 150
			WM_MBUTTONUP,
			// Token: 0x04000097 RID: 151
			WM_MBUTTONDBLCLK,
			// Token: 0x04000098 RID: 152
			WM_MOUSEWHEEL,
			// Token: 0x04000099 RID: 153
			WM_MOUSELAST = 522,
			// Token: 0x0400009A RID: 154
			WM_NCMOUSEHOVER = 672,
			// Token: 0x0400009B RID: 155
			WM_MOUSEHOVER,
			// Token: 0x0400009C RID: 156
			WM_NCMOUSELEAVE,
			// Token: 0x0400009D RID: 157
			WM_MOUSELEAVE,
			// Token: 0x0400009E RID: 158
			WM_NCMOUSEMOVE = 160,
			// Token: 0x0400009F RID: 159
			WM_NCLBUTTONDOWN,
			// Token: 0x040000A0 RID: 160
			WM_NCLBUTTONUP,
			// Token: 0x040000A1 RID: 161
			WM_NCLBUTTONDBLCLK,
			// Token: 0x040000A2 RID: 162
			WM_NCRBUTTONDOWN,
			// Token: 0x040000A3 RID: 163
			WM_NCRBUTTONUP,
			// Token: 0x040000A4 RID: 164
			WM_NCRBUTTONDBLCLK,
			// Token: 0x040000A5 RID: 165
			WM_NCMBUTTONDOWN,
			// Token: 0x040000A6 RID: 166
			WM_NCMBUTTONUP,
			// Token: 0x040000A7 RID: 167
			WM_NCMBUTTONDBLCLK,
			// Token: 0x040000A8 RID: 168
			WM_KEYFIRST = 256,
			// Token: 0x040000A9 RID: 169
			WM_KEYDOWN = 256,
			// Token: 0x040000AA RID: 170
			WM_KEYUP,
			// Token: 0x040000AB RID: 171
			WM_CHAR,
			// Token: 0x040000AC RID: 172
			WM_DEADCHAR,
			// Token: 0x040000AD RID: 173
			WM_SYSKEYDOWN,
			// Token: 0x040000AE RID: 174
			WM_SYSKEYUP,
			// Token: 0x040000AF RID: 175
			WM_SYS1CHAR,
			// Token: 0x040000B0 RID: 176
			WM_SYSDEADCHAR,
			// Token: 0x040000B1 RID: 177
			WM_KEYLAST,
			// Token: 0x040000B2 RID: 178
			WM_HSCROLL = 276,
			// Token: 0x040000B3 RID: 179
			WM_VSCROLL,
			// Token: 0x040000B4 RID: 180
			WM_IME_SETCONTEXT = 641,
			// Token: 0x040000B5 RID: 181
			WM_IME_NOTIFY,
			// Token: 0x040000B6 RID: 182
			WM_IME_CONTROL,
			// Token: 0x040000B7 RID: 183
			WM_IME_COMPOSITIONFULL,
			// Token: 0x040000B8 RID: 184
			WM_IME_SELECT,
			// Token: 0x040000B9 RID: 185
			WM_IME_CHAR,
			// Token: 0x040000BA RID: 186
			WM_IME_REQUEST = 648,
			// Token: 0x040000BB RID: 187
			WM_IME_KEYDOWN = 656,
			// Token: 0x040000BC RID: 188
			WM_IME_KEYUP,
			// Token: 0x040000BD RID: 189
			WM_MOUSE_ENTER = 1025
		}
	}
}
