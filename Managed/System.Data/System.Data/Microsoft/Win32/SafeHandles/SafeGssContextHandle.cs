using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020003C7 RID: 967
	internal sealed class SafeGssContextHandle : SafeHandle
	{
		// Token: 0x06002E55 RID: 11861 RVA: 0x000C85DC File Offset: 0x000C67DC
		public SafeGssContextHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000C85A1 File Offset: 0x000C67A1
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000C8694 File Offset: 0x000C6894
		protected override bool ReleaseHandle()
		{
			Interop.NetSecurityNative.Status status;
			int num = (int)Interop.NetSecurityNative.DeleteSecContext(out status, ref this.handle);
			base.SetHandle(IntPtr.Zero);
			return num == 0;
		}
	}
}
