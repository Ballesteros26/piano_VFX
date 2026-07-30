using System;
using System.Runtime.InteropServices;
using Mono.Net;
using ObjCRuntimeInternal;

namespace Mono.AppleTls
{
	// Token: 0x020000AD RID: 173
	internal class SecKey : INativeObject, IDisposable
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0000DB0F File Offset: 0x0000BD0F
		public SecKey(IntPtr handle, bool owns = false)
		{
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000DB28 File Offset: 0x0000BD28
		internal SecKey(IntPtr handle, IntPtr owner)
		{
			this.handle = handle;
			this.owner = owner;
			CFObject.CFRetain(owner);
		}

		// Token: 0x0600043D RID: 1085
		[DllImport("/System/Library/Frameworks/Security.framework/Security", EntryPoint = "SecKeyGetTypeID")]
		public static extern IntPtr GetTypeID();

		// Token: 0x0600043E RID: 1086 RVA: 0x0000DB48 File Offset: 0x0000BD48
		~SecKey()
		{
			this.Dispose(false);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000DB80 File Offset: 0x0000BD80
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000DB90 File Offset: 0x0000BD90
		protected virtual void Dispose(bool disposing)
		{
			if (this.owner != IntPtr.Zero)
			{
				CFObject.CFRelease(this.owner);
				this.owner = (this.handle = IntPtr.Zero);
				return;
			}
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x04000933 RID: 2355
		internal IntPtr handle;

		// Token: 0x04000934 RID: 2356
		internal IntPtr owner;
	}
}
