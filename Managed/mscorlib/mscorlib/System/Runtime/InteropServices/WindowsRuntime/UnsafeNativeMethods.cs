using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x0200096F RID: 2415
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06005990 RID: 22928
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int WindowsCreateString(string sourceString, int length, IntPtr* hstring);

		// Token: 0x06005991 RID: 22929
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int WindowsDeleteString(IntPtr hstring);

		// Token: 0x06005992 RID: 22930
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern char* WindowsGetStringRawBuffer(IntPtr hstring, uint* length);

		// Token: 0x06005993 RID: 22931
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RoOriginateLanguageException(int error, string message, IntPtr languageException);

		// Token: 0x06005994 RID: 22932
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RoReportUnhandledError(IRestrictedErrorInfo error);

		// Token: 0x06005995 RID: 22933
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IRestrictedErrorInfo GetRestrictedErrorInfo();
	}
}
