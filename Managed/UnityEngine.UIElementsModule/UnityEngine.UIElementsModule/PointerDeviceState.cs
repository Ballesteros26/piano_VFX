using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000174 RID: 372
	internal static class PointerDeviceState
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x0002706C File Offset: 0x0002526C
		internal static void Reset()
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				PointerDeviceState.m_Positions[i] = Vector2.zero;
				PointerDeviceState.m_Panels[i] = null;
				PointerDeviceState.m_PressedButtons[i] = 0;
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000270B0 File Offset: 0x000252B0
		public static void SavePointerPosition(int pointerId, Vector2 position, IPanel panel)
		{
			PointerDeviceState.m_Positions[pointerId] = position;
			PointerDeviceState.m_Panels[pointerId] = panel;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000270C7 File Offset: 0x000252C7
		public static void PressButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0);
			Debug.Assert(buttonId < 32);
			PointerDeviceState.m_PressedButtons[pointerId] |= 1 << buttonId;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000270F7 File Offset: 0x000252F7
		public static void ReleaseButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0);
			Debug.Assert(buttonId < 32);
			PointerDeviceState.m_PressedButtons[pointerId] &= ~(1 << buttonId);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00027128 File Offset: 0x00025328
		public static void ReleaseAllButtons(int pointerId)
		{
			PointerDeviceState.m_PressedButtons[pointerId] = 0;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00027134 File Offset: 0x00025334
		public static Vector2 GetPointerPosition(int pointerId)
		{
			return PointerDeviceState.m_Positions[pointerId];
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00027154 File Offset: 0x00025354
		public static IPanel GetPanel(int pointerId)
		{
			return PointerDeviceState.m_Panels[pointerId];
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00027170 File Offset: 0x00025370
		public static int GetPressedButtons(int pointerId)
		{
			return PointerDeviceState.m_PressedButtons[pointerId];
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002718C File Offset: 0x0002538C
		internal static bool HasAdditionalPressedButtons(int pointerId, int exceptButtonId)
		{
			return (PointerDeviceState.m_PressedButtons[pointerId] & ~(1 << exceptButtonId)) != 0;
		}

		// Token: 0x04000445 RID: 1093
		private static Vector2[] m_Positions = new Vector2[PointerId.maxPointers];

		// Token: 0x04000446 RID: 1094
		private static IPanel[] m_Panels = new IPanel[PointerId.maxPointers];

		// Token: 0x04000447 RID: 1095
		private static int[] m_PressedButtons = new int[PointerId.maxPointers];
	}
}
