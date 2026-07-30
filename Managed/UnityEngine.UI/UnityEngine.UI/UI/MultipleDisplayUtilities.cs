using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200002E RID: 46
	internal static class MultipleDisplayUtilities
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000F54C File Offset: 0x0000D74C
		public static bool GetRelativeMousePositionForDrag(PointerEventData eventData, ref Vector2 position)
		{
			int displayIndex = eventData.pointerPressRaycast.displayIndex;
			Vector3 vector = Display.RelativeMouseAt(eventData.position);
			if ((int)vector.z != displayIndex)
			{
				return false;
			}
			position = ((displayIndex != 0) ? vector : eventData.position);
			return true;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000F59C File Offset: 0x0000D79C
		public static Vector2 GetMousePositionRelativeToMainDisplayResolution()
		{
			Vector3 mousePosition = Input.mousePosition;
			if (Display.main.renderingHeight != Display.main.systemHeight && (mousePosition.y < 0f || mousePosition.y > (float)Display.main.renderingHeight || mousePosition.x < 0f || mousePosition.x > (float)Display.main.renderingWidth))
			{
				mousePosition.y += (float)(Display.main.systemHeight - Display.main.renderingHeight);
			}
			return mousePosition;
		}
	}
}
