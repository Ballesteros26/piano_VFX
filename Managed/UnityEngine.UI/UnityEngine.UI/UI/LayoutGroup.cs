using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000025 RID: 37
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public abstract class LayoutGroup : UIBehaviour, ILayoutElement, ILayoutGroup, ILayoutController
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000DCDB File Offset: 0x0000BEDB
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000DCE3 File Offset: 0x0000BEE3
		public RectOffset padding
		{
			get
			{
				return this.m_Padding;
			}
			set
			{
				this.SetProperty<RectOffset>(ref this.m_Padding, value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000DCF2 File Offset: 0x0000BEF2
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000DCFA File Offset: 0x0000BEFA
		public TextAnchor childAlignment
		{
			get
			{
				return this.m_ChildAlignment;
			}
			set
			{
				this.SetProperty<TextAnchor>(ref this.m_ChildAlignment, value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000DD09 File Offset: 0x0000BF09
		protected RectTransform rectTransform
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000DD2B File Offset: 0x0000BF2B
		protected List<RectTransform> rectChildren
		{
			get
			{
				return this.m_RectChildren;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000DD34 File Offset: 0x0000BF34
		public virtual void CalculateLayoutInputHorizontal()
		{
			this.m_RectChildren.Clear();
			List<Component> list = ListPool<Component>.Get();
			for (int i = 0; i < this.rectTransform.childCount; i++)
			{
				RectTransform rectTransform = this.rectTransform.GetChild(i) as RectTransform;
				if (!(rectTransform == null) && rectTransform.gameObject.activeInHierarchy)
				{
					rectTransform.GetComponents(typeof(ILayoutIgnorer), list);
					if (list.Count == 0)
					{
						this.m_RectChildren.Add(rectTransform);
					}
					else
					{
						for (int j = 0; j < list.Count; j++)
						{
							if (!((ILayoutIgnorer)list[j]).ignoreLayout)
							{
								this.m_RectChildren.Add(rectTransform);
								break;
							}
						}
					}
				}
			}
			ListPool<Component>.Release(list);
			this.m_Tracker.Clear();
		}

		// Token: 0x0600027D RID: 637
		public abstract void CalculateLayoutInputVertical();

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000DE00 File Offset: 0x0000C000
		public virtual float minWidth
		{
			get
			{
				return this.GetTotalMinSize(0);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000DE09 File Offset: 0x0000C009
		public virtual float preferredWidth
		{
			get
			{
				return this.GetTotalPreferredSize(0);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000DE12 File Offset: 0x0000C012
		public virtual float flexibleWidth
		{
			get
			{
				return this.GetTotalFlexibleSize(0);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000DE1B File Offset: 0x0000C01B
		public virtual float minHeight
		{
			get
			{
				return this.GetTotalMinSize(1);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000DE24 File Offset: 0x0000C024
		public virtual float preferredHeight
		{
			get
			{
				return this.GetTotalPreferredSize(1);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000DE2D File Offset: 0x0000C02D
		public virtual float flexibleHeight
		{
			get
			{
				return this.GetTotalFlexibleSize(1);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00008CC2 File Offset: 0x00006EC2
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000285 RID: 645
		public abstract void SetLayoutHorizontal();

		// Token: 0x06000286 RID: 646
		public abstract void SetLayoutVertical();

		// Token: 0x06000287 RID: 647 RVA: 0x0000DE38 File Offset: 0x0000C038
		protected LayoutGroup()
		{
			if (this.m_Padding == null)
			{
				this.m_Padding = new RectOffset();
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000DE95 File Offset: 0x0000C095
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000DEA3 File Offset: 0x0000C0A3
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000DEC1 File Offset: 0x0000C0C1
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000DEC9 File Offset: 0x0000C0C9
		protected float GetTotalMinSize(int axis)
		{
			return this.m_TotalMinSize[axis];
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000DED7 File Offset: 0x0000C0D7
		protected float GetTotalPreferredSize(int axis)
		{
			return this.m_TotalPreferredSize[axis];
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000DEE5 File Offset: 0x0000C0E5
		protected float GetTotalFlexibleSize(int axis)
		{
			return this.m_TotalFlexibleSize[axis];
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		protected float GetStartOffset(int axis, float requiredSpaceWithoutPadding)
		{
			float num = requiredSpaceWithoutPadding + (float)((axis == 0) ? this.padding.horizontal : this.padding.vertical);
			float num2 = this.rectTransform.rect.size[axis] - num;
			float alignmentOnAxis = this.GetAlignmentOnAxis(axis);
			return (float)((axis == 0) ? this.padding.left : this.padding.top) + num2 * alignmentOnAxis;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000DF68 File Offset: 0x0000C168
		protected float GetAlignmentOnAxis(int axis)
		{
			if (axis == 0)
			{
				return (float)(this.childAlignment % TextAnchor.MiddleLeft) * 0.5f;
			}
			return (float)(this.childAlignment / TextAnchor.MiddleLeft) * 0.5f;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000DF8C File Offset: 0x0000C18C
		protected void SetLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis)
		{
			this.m_TotalMinSize[axis] = totalMin;
			this.m_TotalPreferredSize[axis] = totalPreferred;
			this.m_TotalFlexibleSize[axis] = totalFlexible;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		protected void SetChildAlongAxis(RectTransform rect, int axis, float pos)
		{
			if (rect == null)
			{
				return;
			}
			this.SetChildAlongAxisWithScale(rect, axis, pos, 1f);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		protected void SetChildAlongAxisWithScale(RectTransform rect, int axis, float pos, float scaleFactor)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.Anchors | ((axis == 0) ? DrivenTransformProperties.AnchoredPositionX : DrivenTransformProperties.AnchoredPositionY));
			rect.anchorMin = Vector2.up;
			rect.anchorMax = Vector2.up;
			Vector2 anchoredPosition = rect.anchoredPosition;
			anchoredPosition[axis] = ((axis == 0) ? (pos + rect.sizeDelta[axis] * rect.pivot[axis] * scaleFactor) : (-pos - rect.sizeDelta[axis] * (1f - rect.pivot[axis]) * scaleFactor));
			rect.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E085 File Offset: 0x0000C285
		protected void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
		{
			if (rect == null)
			{
				return;
			}
			this.SetChildAlongAxisWithScale(rect, axis, pos, size, 1f);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		protected void SetChildAlongAxisWithScale(RectTransform rect, int axis, float pos, float size, float scaleFactor)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.Anchors | ((axis == 0) ? (DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.SizeDeltaX) : (DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.SizeDeltaY)));
			rect.anchorMin = Vector2.up;
			rect.anchorMax = Vector2.up;
			Vector2 sizeDelta = rect.sizeDelta;
			sizeDelta[axis] = size;
			rect.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = rect.anchoredPosition;
			anchoredPosition[axis] = ((axis == 0) ? (pos + size * rect.pivot[axis] * scaleFactor) : (-pos - size * (1f - rect.pivot[axis]) * scaleFactor));
			rect.anchoredPosition = anchoredPosition;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000E15B File Offset: 0x0000C35B
		private bool isRootLayoutGroup
		{
			get
			{
				return base.transform.parent == null || base.transform.parent.GetComponent(typeof(ILayoutGroup)) == null;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000E192 File Offset: 0x0000C392
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (this.isRootLayoutGroup)
			{
				this.SetDirty();
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000DEC1 File Offset: 0x0000C0C1
		protected virtual void OnTransformChildrenChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		protected void SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return;
			}
			currentValue = newValue;
			this.SetDirty();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000E1F9 File Offset: 0x0000C3F9
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			if (!CanvasUpdateRegistry.IsRebuildingLayout())
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
				return;
			}
			base.StartCoroutine(this.DelayedSetDirty(this.rectTransform));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E22A File Offset: 0x0000C42A
		private IEnumerator DelayedSetDirty(RectTransform rectTransform)
		{
			yield return null;
			LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
			yield break;
		}

		// Token: 0x040000E0 RID: 224
		[SerializeField]
		protected RectOffset m_Padding = new RectOffset();

		// Token: 0x040000E1 RID: 225
		[SerializeField]
		protected TextAnchor m_ChildAlignment;

		// Token: 0x040000E2 RID: 226
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040000E3 RID: 227
		protected DrivenRectTransformTracker m_Tracker;

		// Token: 0x040000E4 RID: 228
		private Vector2 m_TotalMinSize = Vector2.zero;

		// Token: 0x040000E5 RID: 229
		private Vector2 m_TotalPreferredSize = Vector2.zero;

		// Token: 0x040000E6 RID: 230
		private Vector2 m_TotalFlexibleSize = Vector2.zero;

		// Token: 0x040000E7 RID: 231
		[NonSerialized]
		private List<RectTransform> m_RectChildren = new List<RectTransform>();
	}
}
