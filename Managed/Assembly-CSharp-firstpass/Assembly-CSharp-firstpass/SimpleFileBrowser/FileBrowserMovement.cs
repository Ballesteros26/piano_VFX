using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleFileBrowser
{
	// Token: 0x02000009 RID: 9
	public class FileBrowserMovement : MonoBehaviour
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003F40 File Offset: 0x00002140
		public void Initialize(FileBrowser fileBrowser)
		{
			this.fileBrowser = fileBrowser;
			this.canvasTR = fileBrowser.GetComponent<RectTransform>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003F58 File Offset: 0x00002158
		public void OnDragStarted(BaseEventData data)
		{
			PointerEventData pointerEventData = (PointerEventData)data;
			this.canvasCam = pointerEventData.pressEventCamera;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.window, pointerEventData.pressPosition, this.canvasCam, out this.initialTouchPos);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003F98 File Offset: 0x00002198
		public void OnDrag(BaseEventData data)
		{
			PointerEventData pointerEventData = (PointerEventData)data;
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.window, pointerEventData.position, this.canvasCam, out vector);
			this.window.anchoredPosition += vector - this.initialTouchPos;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003FE8 File Offset: 0x000021E8
		public void OnEndDrag(BaseEventData data)
		{
			this.fileBrowser.EnsureWindowIsWithinBounds();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003FF8 File Offset: 0x000021F8
		public void OnResizeStarted(BaseEventData data)
		{
			PointerEventData pointerEventData = (PointerEventData)data;
			this.canvasCam = pointerEventData.pressEventCamera;
			this.initialAnchoredPos = this.window.anchoredPosition;
			this.initialSizeDelta = this.window.sizeDelta;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasTR, pointerEventData.pressPosition, this.canvasCam, out this.initialTouchPos);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004058 File Offset: 0x00002258
		public void OnResize(BaseEventData data)
		{
			PointerEventData pointerEventData = (PointerEventData)data;
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvasTR, pointerEventData.position, this.canvasCam, out vector);
			Vector2 vector2 = vector - this.initialTouchPos;
			Vector2 vector3 = this.initialSizeDelta + new Vector2(vector2.x, -vector2.y);
			if (vector3.x < (float)this.fileBrowser.minWidth)
			{
				vector3.x = (float)this.fileBrowser.minWidth;
			}
			if (vector3.y < (float)this.fileBrowser.minHeight)
			{
				vector3.y = (float)this.fileBrowser.minHeight;
			}
			vector3.x = (float)((int)vector3.x);
			vector3.y = (float)((int)vector3.y);
			vector2 = vector3 - this.initialSizeDelta;
			this.window.anchoredPosition = this.initialAnchoredPos + new Vector2(vector2.x * 0.5f, vector2.y * -0.5f);
			this.window.sizeDelta = vector3;
			this.listView.OnViewportDimensionsChanged();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003FE8 File Offset: 0x000021E8
		public void OnEndResize(BaseEventData data)
		{
			this.fileBrowser.EnsureWindowIsWithinBounds();
		}

		// Token: 0x04000056 RID: 86
		private FileBrowser fileBrowser;

		// Token: 0x04000057 RID: 87
		private RectTransform canvasTR;

		// Token: 0x04000058 RID: 88
		private Camera canvasCam;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private RectTransform window;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private RecycledListView listView;

		// Token: 0x0400005B RID: 91
		private Vector2 initialTouchPos = Vector2.zero;

		// Token: 0x0400005C RID: 92
		private Vector2 initialAnchoredPos;

		// Token: 0x0400005D RID: 93
		private Vector2 initialSizeDelta;
	}
}
