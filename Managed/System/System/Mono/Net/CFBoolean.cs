using System;
using System.Runtime.InteropServices;
using ObjCRuntimeInternal;

namespace Mono.Net
{
	// Token: 0x02000060 RID: 96
	internal class CFBoolean : INativeObject, IDisposable
	{
		// Token: 0x060001BA RID: 442 RVA: 0x000055F8 File Offset: 0x000037F8
		static CFBoolean()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				CFBoolean.True = new CFBoolean(CFObject.GetCFObjectHandle(intPtr, "kCFBooleanTrue"), false);
				CFBoolean.False = new CFBoolean(CFObject.GetCFObjectHandle(intPtr, "kCFBooleanFalse"), false);
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005668 File Offset: 0x00003868
		internal CFBoolean(IntPtr handle, bool owns)
		{
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005684 File Offset: 0x00003884
		~CFBoolean()
		{
			this.Dispose(false);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000056B4 File Offset: 0x000038B4
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000056BC File Offset: 0x000038BC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000056CB File Offset: 0x000038CB
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000056F5 File Offset: 0x000038F5
		public static implicit operator bool(CFBoolean value)
		{
			return value.Value;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000056FD File Offset: 0x000038FD
		public static explicit operator CFBoolean(bool value)
		{
			return CFBoolean.FromBoolean(value);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005705 File Offset: 0x00003905
		public static CFBoolean FromBoolean(bool value)
		{
			if (!value)
			{
				return CFBoolean.False;
			}
			return CFBoolean.True;
		}

		// Token: 0x060001C3 RID: 451
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool CFBooleanGetValue(IntPtr boolean);

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005715 File Offset: 0x00003915
		public bool Value
		{
			get
			{
				return CFBoolean.CFBooleanGetValue(this.handle);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005722 File Offset: 0x00003922
		public static bool GetValue(IntPtr boolean)
		{
			return CFBoolean.CFBooleanGetValue(boolean);
		}

		// Token: 0x0400077A RID: 1914
		private IntPtr handle;

		// Token: 0x0400077B RID: 1915
		public static readonly CFBoolean True;

		// Token: 0x0400077C RID: 1916
		public static readonly CFBoolean False;
	}
}
