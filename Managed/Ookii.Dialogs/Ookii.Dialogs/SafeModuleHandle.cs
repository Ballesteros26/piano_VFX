using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs
{
	// Token: 0x02000018 RID: 24
	internal class SafeModuleHandle : SafeHandle
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000056F1 File Offset: 0x000038F1
		public SafeModuleHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005704 File Offset: 0x00003904
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005728 File Offset: 0x00003928
		[ReliabilityContract(3, 1)]
		protected override bool ReleaseHandle()
		{
			return NativeMethods.FreeLibrary(this.handle);
		}
	}
}
