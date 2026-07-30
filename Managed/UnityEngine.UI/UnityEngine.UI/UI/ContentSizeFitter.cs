using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200001B RID: 27
	[AddComponentMenu("Layout/Content Size Fitter", 141)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class ContentSizeFitter : UIBehaviour, ILayoutSelfController, ILayoutController
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000CD14 File Offset: 0x0000AF14
		public ContentSizeFitter.FitMode horizontalFit
		{
			get
			{
				return this.m_HorizontalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_HorizontalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000CD2A File Offset: 0x0000AF2A
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000CD32 File Offset: 0x0000AF32
		public ContentSizeFitter.FitMode verticalFit
		{
			get
			{
				return this.m_VerticalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_VerticalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000CD48 File Offset: 0x0000AF48
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

		// Token: 0x0600021E RID: 542 RVA: 0x0000CD6A File Offset: 0x0000AF6A
		protected ContentSizeFitter()
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000CD72 File Offset: 0x0000AF72
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CD80 File Offset: 0x0000AF80
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		private void HandleSelfFittingAlongAxis(int axis)
		{
			ContentSizeFitter.FitMode fitMode = ((axis == 0) ? this.horizontalFit : this.verticalFit);
			if (fitMode == ContentSizeFitter.FitMode.Unconstrained)
			{
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.None);
				return;
			}
			this.m_Tracker.Add(this, this.rectTransform, (axis == 0) ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);
			if (fitMode == ContentSizeFitter.FitMode.MinSize)
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.m_Rect, axis));
				return;
			}
			this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(this.m_Rect, axis));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000CE34 File Offset: 0x0000B034
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000CE48 File Offset: 0x0000B048
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000CE51 File Offset: 0x0000B051
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x040000C6 RID: 198
		[SerializeField]
		protected ContentSizeFitter.FitMode m_HorizontalFit;

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		protected ContentSizeFitter.FitMode m_VerticalFit;

		// Token: 0x040000C8 RID: 200
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040000C9 RID: 201
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000096 RID: 150
		public enum FitMode
		{
			// Token: 0x0400029C RID: 668
			Unconstrained,
			// Token: 0x0400029D RID: 669
			MinSize,
			// Token: 0x0400029E RID: 670
			PreferredSize
		}
	}
}
