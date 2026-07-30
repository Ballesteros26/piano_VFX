using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(2, UnmanagedCode = true)]
	internal class SafeGDIHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005673 File Offset: 0x00003873
		internal SafeGDIHandle()
			: base(true)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000569F File Offset: 0x0000389F
		internal SafeGDIHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000056B4 File Offset: 0x000038B4
		protected override bool ReleaseHandle()
		{
			return NativeMethods.DeleteObject(this.handle);
		}
	}
}
