using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000052 RID: 82
	internal class CFString : CFObject
	{
		// Token: 0x06000152 RID: 338 RVA: 0x000043D8 File Offset: 0x000025D8
		public CFString(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x06000153 RID: 339
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringCreateWithCharacters(IntPtr alloc, IntPtr chars, IntPtr length);

		// Token: 0x06000154 RID: 340 RVA: 0x00004664 File Offset: 0x00002864
		public unsafe static CFString Create(string value)
		{
			IntPtr intPtr;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				intPtr = CFString.CFStringCreateWithCharacters(IntPtr.Zero, (IntPtr)((void*)ptr), (IntPtr)value.Length);
			}
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFString(intPtr, true);
		}

		// Token: 0x06000155 RID: 341
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetLength(IntPtr handle);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000046B5 File Offset: 0x000028B5
		public int Length
		{
			get
			{
				if (this.str != null)
				{
					return this.str.Length;
				}
				return (int)CFString.CFStringGetLength(base.Handle);
			}
		}

		// Token: 0x06000157 RID: 343
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetCharactersPtr(IntPtr handle);

		// Token: 0x06000158 RID: 344
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetCharacters(IntPtr handle, CFRange range, IntPtr buffer);

		// Token: 0x06000159 RID: 345 RVA: 0x000046DC File Offset: 0x000028DC
		public unsafe static string AsString(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			int num = (int)CFString.CFStringGetLength(handle);
			if (num == 0)
			{
				return string.Empty;
			}
			IntPtr intPtr = CFString.CFStringGetCharactersPtr(handle);
			IntPtr intPtr2 = IntPtr.Zero;
			if (intPtr == IntPtr.Zero)
			{
				CFRange cfrange = new CFRange(0, num);
				intPtr2 = Marshal.AllocHGlobal(num * 2);
				CFString.CFStringGetCharacters(handle, cfrange, intPtr2);
				intPtr = intPtr2;
			}
			string text = new string((char*)(void*)intPtr, 0, num);
			if (intPtr2 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr2);
			}
			return text;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004764 File Offset: 0x00002964
		public override string ToString()
		{
			if (this.str == null)
			{
				this.str = CFString.AsString(base.Handle);
			}
			return this.str;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004785 File Offset: 0x00002985
		public static implicit operator string(CFString str)
		{
			return str.ToString();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000478D File Offset: 0x0000298D
		public static implicit operator CFString(string str)
		{
			return CFString.Create(str);
		}

		// Token: 0x0400074D RID: 1869
		private string str;
	}
}
