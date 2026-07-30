using System;
using System.IO;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020000C0 RID: 192
	[SecurityCritical]
	internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00021E29 File Offset: 0x00020029
		[SecurityCritical]
		internal SafeFindHandle()
			: base(true)
		{
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00021E63 File Offset: 0x00020063
		internal SafeFindHandle(IntPtr preexistingHandle)
			: base(true)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00021E73 File Offset: 0x00020073
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return MonoIO.FindCloseFile(this.handle);
		}
	}
}
