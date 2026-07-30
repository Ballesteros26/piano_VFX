using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006D RID: 109
	internal interface IUIElementsUtility
	{
		// Token: 0x0600028D RID: 653
		bool TakeCapture();

		// Token: 0x0600028E RID: 654
		bool ReleaseCapture();

		// Token: 0x0600028F RID: 655
		bool ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled);

		// Token: 0x06000290 RID: 656
		bool CleanupRoots();

		// Token: 0x06000291 RID: 657
		bool EndContainerGUIFromException(Exception exception);

		// Token: 0x06000292 RID: 658
		bool MakeCurrentIMGUIContainerDirty();

		// Token: 0x06000293 RID: 659
		void UpdateSchedulers();

		// Token: 0x06000294 RID: 660
		void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback);
	}
}
