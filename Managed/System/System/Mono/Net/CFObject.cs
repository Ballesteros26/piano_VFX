using System;
using System.Runtime.InteropServices;
using ObjCRuntimeInternal;

namespace Mono.Net
{
	// Token: 0x0200004D RID: 77
	internal class CFObject : IDisposable, INativeObject
	{
		// Token: 0x0600012A RID: 298
		[DllImport("/usr/lib/libSystem.dylib")]
		public static extern IntPtr dlopen(string path, int mode);

		// Token: 0x0600012B RID: 299
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x0600012C RID: 300
		[DllImport("/usr/lib/libSystem.dylib")]
		public static extern void dlclose(IntPtr handle);

		// Token: 0x0600012D RID: 301 RVA: 0x00004283 File Offset: 0x00002483
		public static IntPtr GetIndirect(IntPtr handle, string symbol)
		{
			return CFObject.dlsym(handle, symbol);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000428C File Offset: 0x0000248C
		public static CFString GetStringConstant(IntPtr handle, string symbol)
		{
			IntPtr intPtr = CFObject.dlsym(handle, symbol);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = Marshal.ReadIntPtr(intPtr);
			if (intPtr2 == IntPtr.Zero)
			{
				return null;
			}
			return new CFString(intPtr2, false);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000042D0 File Offset: 0x000024D0
		public static IntPtr GetIntPtr(IntPtr handle, string symbol)
		{
			IntPtr intPtr = CFObject.dlsym(handle, symbol);
			if (intPtr == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			return Marshal.ReadIntPtr(intPtr);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004300 File Offset: 0x00002500
		public static IntPtr GetCFObjectHandle(IntPtr handle, string symbol)
		{
			IntPtr intPtr = CFObject.dlsym(handle, symbol);
			if (intPtr == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			return Marshal.ReadIntPtr(intPtr);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000432E File Offset: 0x0000252E
		public CFObject(IntPtr handle, bool own)
		{
			this.Handle = handle;
			if (!own)
			{
				this.Retain();
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004348 File Offset: 0x00002548
		~CFObject()
		{
			this.Dispose(false);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004378 File Offset: 0x00002578
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00004380 File Offset: 0x00002580
		public IntPtr Handle { get; private set; }

		// Token: 0x06000135 RID: 309
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern IntPtr CFRetain(IntPtr handle);

		// Token: 0x06000136 RID: 310 RVA: 0x00004389 File Offset: 0x00002589
		private void Retain()
		{
			CFObject.CFRetain(this.Handle);
		}

		// Token: 0x06000137 RID: 311
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern void CFRelease(IntPtr handle);

		// Token: 0x06000138 RID: 312 RVA: 0x00004397 File Offset: 0x00002597
		private void Release()
		{
			CFObject.CFRelease(this.Handle);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000043A4 File Offset: 0x000025A4
		protected virtual void Dispose(bool disposing)
		{
			if (this.Handle != IntPtr.Zero)
			{
				this.Release();
				this.Handle = IntPtr.Zero;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000043C9 File Offset: 0x000025C9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000742 RID: 1858
		public const string CoreFoundationLibrary = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x04000743 RID: 1859
		private const string SystemLibrary = "/usr/lib/libSystem.dylib";
	}
}
