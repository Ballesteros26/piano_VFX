using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	// Token: 0x02000220 RID: 544
	public static class CrashReporting
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600182E RID: 6190
		public static extern string crashReportFolder
		{
			[NativeHeader("PlatformDependent/WinPlayer/Bindings/CrashReportingBindings.h")]
			[ThreadSafe]
			[MethodImpl(4096)]
			get;
		}
	}
}
