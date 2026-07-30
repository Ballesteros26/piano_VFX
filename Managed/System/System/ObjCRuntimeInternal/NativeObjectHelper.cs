using System;

namespace ObjCRuntimeInternal
{
	// Token: 0x02000004 RID: 4
	internal static class NativeObjectHelper
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020AE File Offset: 0x000002AE
		public static IntPtr GetHandle(this INativeObject self)
		{
			if (self != null)
			{
				return self.Handle;
			}
			return IntPtr.Zero;
		}
	}
}
