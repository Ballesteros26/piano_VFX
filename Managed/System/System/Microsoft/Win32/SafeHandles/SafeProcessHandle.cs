using System;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020000E0 RID: 224
	[SuppressUnmanagedCodeSecurity]
	public sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x0000F070 File Offset: 0x0000D270
		internal SafeProcessHandle()
			: base(true)
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000F079 File Offset: 0x0000D279
		internal SafeProcessHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000F089 File Offset: 0x0000D289
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public SafeProcessHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000F099 File Offset: 0x0000D299
		internal void InitialSetHandle(IntPtr h)
		{
			this.handle = h;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000F0A2 File Offset: 0x0000D2A2
		protected override bool ReleaseHandle()
		{
			return NativeMethods.CloseProcess(this.handle);
		}

		// Token: 0x04000BCF RID: 3023
		internal static SafeProcessHandle InvalidHandle = new SafeProcessHandle(IntPtr.Zero);
	}
}
