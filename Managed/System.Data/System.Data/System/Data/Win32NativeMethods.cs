using System;

namespace System.Data
{
	// Token: 0x02000117 RID: 279
	internal static class Win32NativeMethods
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x0004B921 File Offset: 0x00049B21
		internal static bool IsTokenRestrictedWrapper(IntPtr token)
		{
			throw new PlatformNotSupportedException("Win32NativeMethods.IsTokenRestrictedWrapper is not supported on non-Windows platforms");
		}
	}
}
