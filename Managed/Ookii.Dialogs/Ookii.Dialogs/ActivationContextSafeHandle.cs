using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(2, UnmanagedCode = true)]
	internal class ActivationContextSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00005673 File Offset: 0x00003873
		public ActivationContextSafeHandle()
			: base(true)
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005680 File Offset: 0x00003880
		[ReliabilityContract(3, 1)]
		protected override bool ReleaseHandle()
		{
			NativeMethods.ReleaseActCtx(this.handle);
			return true;
		}
	}
}
