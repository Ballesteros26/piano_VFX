using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000019 RID: 25
	[AddComponentMenu("Layout/Aspect Ratio Fitter", 142)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class AspectRatioFitter : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000C592 File Offset: 0x0000A792
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000C59A File Offset: 0x0000A79A
		public AspectRatioFitter.AspectMode aspectMode
		{
			get
			{
				return this.m_AspectMode;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<AspectRatioFitter.AspectMode>(ref this.m_AspectMode, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		public float aspectRatio
		{
			get
			{
				return this.m_AspectRatio;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_AspectRatio, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000C5CE File Offset: 0x0000A7CE
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		protected AspectRatioFitter()
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000C603 File Offset: 0x0000A803
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000C611 File Offset: 0x0000A811
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000C62F File Offset: 0x0000A82F
		protected virtual void Update()
		{
			if (this.m_DelayedSetDirty)
			{
				this.m_DelayedSetDirty = false;
				this.SetDirty();
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C646 File Offset: 0x0000A846
		protected override void OnRectTransformDimensionsChange()
		{
			this.UpdateRect();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C650 File Offset: 0x0000A850
		private void UpdateRect()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_Tracker.Clear();
			switch (this.m_AspectMode)
			{
			case AspectRatioFitter.AspectMode.WidthControlsHeight:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.rectTransform.rect.width / this.m_AspectRatio);
				return;
			case AspectRatioFitter.AspectMode.HeightControlsWidth:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaX);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.rectTransform.rect.height * this.m_AspectRatio);
				return;
			case AspectRatioFitter.AspectMode.FitInParent:
			case AspectRatioFitter.AspectMode.EnvelopeParent:
			{
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.anchorMin = Vector2.zero;
				this.rectTransform.anchorMax = Vector2.one;
				this.rectTransform.anchoredPosition = Vector2.zero;
				Vector2 zero = Vector2.zero;
				Vector2 parentSize = this.GetParentSize();
				if ((parentSize.y * this.aspectRatio < parentSize.x) ^ (this.m_AspectMode == AspectRatioFitter.AspectMode.FitInParent))
				{
					zero.y = this.GetSizeDeltaToProduceSize(parentSize.x / this.aspectRatio, 1);
				}
				else
				{
					zero.x = this.GetSizeDeltaToProduceSize(parentSize.y * this.aspectRatio, 0);
				}
				this.rectTransform.sizeDelta = zero;
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		private float GetSizeDeltaToProduceSize(float size, int axis)
		{
			return size - this.GetParentSize()[axis] * (this.rectTransform.anchorMax[axis] - this.rectTransform.anchorMin[axis]);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C810 File Offset: 0x0000AA10
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = this.rectTransform.parent as RectTransform;
			if (rectTransform)
			{
				return rectTransform.rect.size;
			}
			return Vector2.zero;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void SetLayoutHorizontal()
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void SetLayoutVertical()
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C646 File Offset: 0x0000A846
		protected void SetDirty()
		{
			this.UpdateRect();
		}

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private AspectRatioFitter.AspectMode m_AspectMode;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		private float m_AspectRatio = 1f;

		// Token: 0x040000B5 RID: 181
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040000B6 RID: 182
		private bool m_DelayedSetDirty;

		// Token: 0x040000B7 RID: 183
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000092 RID: 146
		public enum AspectMode
		{
			// Token: 0x04000288 RID: 648
			None,
			// Token: 0x04000289 RID: 649
			WidthControlsHeight,
			// Token: 0x0400028A RID: 650
			HeightControlsWidth,
			// Token: 0x0400028B RID: 651
			FitInParent,
			// Token: 0x0400028C RID: 652
			EnvelopeParent
		}
	}
}
