using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000032 RID: 50
	public static class MouseCaptureController
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public static bool IsMouseCaptured()
		{
			bool flag = !MouseCaptureController.m_IsMouseCapturedWarningEmitted;
			if (flag)
			{
				Debug.LogError("MouseCaptureController.IsMouseCaptured() can not be used in playmode. Please use PointerCaptureHelper.GetCapturingElement() instead.");
				MouseCaptureController.m_IsMouseCapturedWarningEmitted = true;
			}
			return false;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005CD8 File Offset: 0x00003ED8
		public static bool HasMouseCapture(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			return visualElement.HasPointerCapture(PointerId.mousePointerId);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005CFC File Offset: 0x00003EFC
		public static void CaptureMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.CapturePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005D38 File Offset: 0x00003F38
		public static void ReleaseMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.ReleasePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005D74 File Offset: 0x00003F74
		public static void ReleaseMouse()
		{
			bool flag = !MouseCaptureController.m_ReleaseMouseWarningEmitted;
			if (flag)
			{
				Debug.LogError("MouseCaptureController.ReleaseMouse() can not be used in playmode. Please use PointerCaptureHelper.GetCapturingElement() instead.");
				MouseCaptureController.m_ReleaseMouseWarningEmitted = true;
			}
		}

		// Token: 0x0400007E RID: 126
		private static bool m_IsMouseCapturedWarningEmitted = false;

		// Token: 0x0400007F RID: 127
		private static bool m_ReleaseMouseWarningEmitted = false;
	}
}
