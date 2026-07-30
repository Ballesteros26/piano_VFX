using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x0200003A RID: 58
	public class TMP_ScrollbarEventHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000FA70 File Offset: 0x0000DC70
		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log("Scrollbar click...");
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000FA7C File Offset: 0x0000DC7C
		public void OnSelect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar selected");
			this.isSelected = true;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000FA8F File Offset: 0x0000DC8F
		public void OnDeselect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar De-Selected");
			this.isSelected = false;
		}

		// Token: 0x04000235 RID: 565
		public bool isSelected;
	}
}
