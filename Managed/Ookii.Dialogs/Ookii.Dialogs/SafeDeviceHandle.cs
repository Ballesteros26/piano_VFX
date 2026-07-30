using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(2, UnmanagedCode = true)]
	internal class SafeDeviceHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005673 File Offset: 0x00003873
		internal SafeDeviceHandle()
			: base(true)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000569F File Offset: 0x0000389F
		internal SafeDeviceHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000056D4 File Offset: 0x000038D4
		protected override bool ReleaseHandle()
		{
			return NativeMethods.DeleteDC(this.handle);
		}
	}
}
