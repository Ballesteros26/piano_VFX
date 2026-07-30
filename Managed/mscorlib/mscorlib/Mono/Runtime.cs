using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000012 RID: 18
	internal static class Runtime
	{
		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void mono_runtime_install_handlers();

		// Token: 0x0600006F RID: 111 RVA: 0x00003B22 File Offset: 0x00001D22
		internal static void InstallSignalHandlers()
		{
			Runtime.mono_runtime_install_handlers();
		}

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDisplayName();

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetNativeStackTrace(Exception exception);

		// Token: 0x06000072 RID: 114 RVA: 0x00003B29 File Offset: 0x00001D29
		public static bool SetGCAllowSynchronousMajor(bool flag)
		{
			return true;
		}
	}
}
