using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000A3 RID: 163
	public class DebugUIHandlerPanel : MonoBehaviour
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000FB2A File Offset: 0x0000DD2A
		private void OnEnable()
		{
			this.m_ScrollTransform = this.scrollRect.GetComponent<RectTransform>();
			this.m_ContentTransform = base.GetComponent<DebugUIHandlerContainer>().contentHolder;
			this.m_MaskTransform = base.GetComponentInChildren<Mask>(true).rectTransform;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000FB60 File Offset: 0x0000DD60
		internal void SetPanel(DebugUI.Panel panel)
		{
			this.m_Panel = panel;
			this.nameLabel.text = "< " + panel.displayName + " >";
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000FB89 File Offset: 0x0000DD89
		internal DebugUI.Panel GetPanel()
		{
			return this.m_Panel;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000FB94 File Offset: 0x0000DD94
		internal void ScrollTo(DebugUIHandlerWidget target)
		{
			if (target == null)
			{
				return;
			}
			RectTransform component = target.GetComponent<RectTransform>();
			float yposInScroll = this.GetYPosInScroll(component);
			float num = (this.GetYPosInScroll(this.m_MaskTransform) - yposInScroll) / (this.m_ContentTransform.rect.size.y - this.m_ScrollTransform.rect.size.y);
			float num2 = this.scrollRect.verticalNormalizedPosition - num;
			num2 = Mathf.Clamp01(num2);
			this.scrollRect.verticalNormalizedPosition = Mathf.Lerp(this.scrollRect.verticalNormalizedPosition, num2, Time.deltaTime * 10f);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000FC3C File Offset: 0x0000DE3C
		private float GetYPosInScroll(RectTransform target)
		{
			Vector3 vector = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
			Vector3 vector2 = target.localPosition + vector;
			Vector3 vector3 = target.parent.TransformPoint(vector2);
			return this.m_ScrollTransform.TransformPoint(vector3).y;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000FCCA File Offset: 0x0000DECA
		internal DebugUIHandlerWidget GetFirstItem()
		{
			return base.GetComponent<DebugUIHandlerContainer>().GetFirstItem();
		}

		// Token: 0x04000212 RID: 530
		public Text nameLabel;

		// Token: 0x04000213 RID: 531
		public ScrollRect scrollRect;

		// Token: 0x04000214 RID: 532
		public RectTransform viewport;

		// Token: 0x04000215 RID: 533
		private RectTransform m_ScrollTransform;

		// Token: 0x04000216 RID: 534
		private RectTransform m_ContentTransform;

		// Token: 0x04000217 RID: 535
		private RectTransform m_MaskTransform;

		// Token: 0x04000218 RID: 536
		protected internal DebugUI.Panel m_Panel;
	}
}
