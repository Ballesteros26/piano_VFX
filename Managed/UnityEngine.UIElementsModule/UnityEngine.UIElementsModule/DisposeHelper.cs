using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000F RID: 15
	internal class DisposeHelper
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000030B0 File Offset: 0x000012B0
		[Conditional("UNITY_UIELEMENTS_DEBUG_DISPOSE")]
		public static void NotifyMissingDispose(IDisposable disposable)
		{
			bool flag = disposable == null;
			if (!flag)
			{
				Debug.LogError("An IDisposable instance of type '" + disposable.GetType().FullName + "' has not been disposed.");
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000030E8 File Offset: 0x000012E8
		public static void NotifyDisposedUsed(IDisposable disposable)
		{
			Debug.LogError("An instance of type '" + disposable.GetType().FullName + "' is being used although it has been disposed.");
		}
	}
}
