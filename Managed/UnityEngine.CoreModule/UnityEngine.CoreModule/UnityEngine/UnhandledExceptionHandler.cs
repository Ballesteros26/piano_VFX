using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C1 RID: 449
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	internal sealed class UnhandledExceptionHandler
	{
		// Token: 0x0600140E RID: 5134 RVA: 0x00020E49 File Offset: 0x0001F049
		[RequiredByNativeCode]
		private static void RegisterUECatcher()
		{
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
			{
				Debug.LogException(e.ExceptionObject as Exception);
			};
		}
	}
}
