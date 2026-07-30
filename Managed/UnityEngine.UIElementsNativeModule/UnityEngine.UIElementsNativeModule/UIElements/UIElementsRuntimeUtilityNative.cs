using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/UIElementsNative/UIElementsRuntimeUtilityNative.h")]
	[VisibleToOtherModules(new string[] { "Unity.UIElements" })]
	internal static class UIElementsRuntimeUtilityNative
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00003E61 File Offset: 0x00002061
		[RequiredByNativeCode]
		public static void RepaintOverlayPanels()
		{
			Action repaintOverlayPanelsCallback = UIElementsRuntimeUtilityNative.RepaintOverlayPanelsCallback;
			if (repaintOverlayPanelsCallback != null)
			{
				repaintOverlayPanelsCallback.Invoke();
			}
		}

		// Token: 0x0600016B RID: 363
		[MethodImpl(4096)]
		public static extern void RegisterPlayerloopCallback();

		// Token: 0x0600016C RID: 364
		[MethodImpl(4096)]
		public static extern void UnregisterPlayerloopCallback();

		// Token: 0x0400006A RID: 106
		internal static Action RepaintOverlayPanelsCallback;
	}
}
