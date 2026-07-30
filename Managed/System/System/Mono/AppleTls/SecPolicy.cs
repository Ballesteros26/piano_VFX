using System;
using System.Runtime.InteropServices;
using Mono.Net;
using ObjCRuntimeInternal;

namespace Mono.AppleTls
{
	// Token: 0x020000BE RID: 190
	internal class SecPolicy : INativeObject, IDisposable
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x0000E649 File Offset: 0x0000C849
		internal SecPolicy(IntPtr handle, bool owns = false)
		{
			if (handle == IntPtr.Zero)
			{
				throw new Exception("Invalid handle");
			}
			this.handle = handle;
			if (!owns)
			{
				CFObject.CFRetain(handle);
			}
		}

		// Token: 0x0600047A RID: 1146
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SecPolicyCreateSSL(bool server, IntPtr hostname);

		// Token: 0x0600047B RID: 1147 RVA: 0x0000E67C File Offset: 0x0000C87C
		public static SecPolicy CreateSslPolicy(bool server, string hostName)
		{
			CFString cfstring = ((hostName == null) ? null : CFString.Create(hostName));
			IntPtr intPtr = ((cfstring == null) ? IntPtr.Zero : cfstring.Handle);
			SecPolicy secPolicy = new SecPolicy(SecPolicy.SecPolicyCreateSSL(server, intPtr), true);
			if (cfstring != null)
			{
				cfstring.Dispose();
			}
			return secPolicy;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		~SecPolicy()
		{
			this.Dispose(false);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000E6FF File Offset: 0x0000C8FF
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000E707 File Offset: 0x0000C907
		protected virtual void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				CFObject.CFRelease(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x04000AE1 RID: 2785
		private IntPtr handle;
	}
}
