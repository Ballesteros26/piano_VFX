using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000031 RID: 49
	[AddComponentMenu("UI/Rect Mask 2D", 13)]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	public class RectMask2D : UIBehaviour, IClipper, ICanvasRaycastFilter
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000FA03 File Offset: 0x0000DC03
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000FA0B File Offset: 0x0000DC0B
		public Vector4 padding
		{
			get
			{
				return this.m_Padding;
			}
			set
			{
				this.m_Padding = value;
				MaskUtilities.Notify2DMaskStateChanged(this);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000FA1A File Offset: 0x0000DC1A
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000FA22 File Offset: 0x0000DC22
		public Vector2Int softness
		{
			get
			{
				return this.m_Softness;
			}
			set
			{
				this.m_Softness.x = Mathf.Max(0, value.x);
				this.m_Softness.y = Mathf.Max(0, value.y);
				MaskUtilities.Notify2DMaskStateChanged(this);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		private Canvas Canvas
		{
			get
			{
				if (this.m_Canvas == null)
				{
					List<Canvas> list = ListPool<Canvas>.Get();
					base.gameObject.GetComponentsInParent<Canvas>(false, list);
					if (list.Count > 0)
					{
						this.m_Canvas = list[list.Count - 1];
					}
					else
					{
						this.m_Canvas = null;
					}
					ListPool<Canvas>.Release(list);
				}
				return this.m_Canvas;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000FABC File Offset: 0x0000DCBC
		public Rect canvasRect
		{
			get
			{
				return this.m_VertexClipper.GetCanvasRect(this.rectTransform, this.Canvas);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		public RectTransform rectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this.m_RectTransform) == null)
				{
					rectTransform = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000FAFE File Offset: 0x0000DCFE
		protected RectMask2D()
		{
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000FB3E File Offset: 0x0000DD3E
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculateClipRects = true;
			ClipperRegistry.Register(this);
			MaskUtilities.Notify2DMaskStateChanged(this);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000FB59 File Offset: 0x0000DD59
		protected override void OnDisable()
		{
			base.OnDisable();
			this.m_ClipTargets.Clear();
			this.m_MaskableTargets.Clear();
			this.m_Clippers.Clear();
			ClipperRegistry.Unregister(this);
			MaskUtilities.Notify2DMaskStateChanged(this);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000FB8E File Offset: 0x0000DD8E
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera, this.m_Padding);
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		private Rect rootCanvasRect
		{
			get
			{
				this.rectTransform.GetWorldCorners(this.m_Corners);
				if (this.Canvas != null)
				{
					Canvas rootCanvas = this.Canvas.rootCanvas;
					for (int i = 0; i < 4; i++)
					{
						this.m_Corners[i] = rootCanvas.transform.InverseTransformPoint(this.m_Corners[i]);
					}
				}
				return new Rect(this.m_Corners[0].x, this.m_Corners[0].y, this.m_Corners[2].x - this.m_Corners[0].x, this.m_Corners[2].y - this.m_Corners[0].y);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000FC80 File Offset: 0x0000DE80
		public virtual void PerformClipping()
		{
			if (this.Canvas == null)
			{
				return;
			}
			if (this.m_ShouldRecalculateClipRects)
			{
				MaskUtilities.GetRectMasksForClip(this, this.m_Clippers);
				this.m_ShouldRecalculateClipRects = false;
			}
			bool flag = true;
			Rect rect = Clipping.FindCullAndClipWorldRect(this.m_Clippers, out flag);
			RenderMode renderMode = this.Canvas.rootCanvas.renderMode;
			if ((renderMode == RenderMode.ScreenSpaceCamera || renderMode == RenderMode.ScreenSpaceOverlay) && !rect.Overlaps(this.rootCanvasRect, true))
			{
				rect = Rect.zero;
				flag = false;
			}
			if (rect != this.m_LastClipRectCanvasSpace)
			{
				foreach (IClippable clippable in this.m_ClipTargets)
				{
					clippable.SetClipRect(rect, flag);
				}
				using (HashSet<MaskableGraphic>.Enumerator enumerator2 = this.m_MaskableTargets.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						MaskableGraphic maskableGraphic = enumerator2.Current;
						maskableGraphic.SetClipRect(rect, flag);
						maskableGraphic.Cull(rect, flag);
					}
					goto IL_01B5;
				}
			}
			if (this.m_ForceClip)
			{
				foreach (IClippable clippable2 in this.m_ClipTargets)
				{
					clippable2.SetClipRect(rect, flag);
				}
				using (HashSet<MaskableGraphic>.Enumerator enumerator2 = this.m_MaskableTargets.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						MaskableGraphic maskableGraphic2 = enumerator2.Current;
						maskableGraphic2.SetClipRect(rect, flag);
						if (maskableGraphic2.canvasRenderer.hasMoved)
						{
							maskableGraphic2.Cull(rect, flag);
						}
					}
					goto IL_01B5;
				}
			}
			foreach (MaskableGraphic maskableGraphic3 in this.m_MaskableTargets)
			{
				maskableGraphic3.Cull(rect, flag);
			}
			IL_01B5:
			this.m_LastClipRectCanvasSpace = rect;
			this.m_ForceClip = false;
			this.UpdateClipSoftness();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000FE98 File Offset: 0x0000E098
		public virtual void UpdateClipSoftness()
		{
			if (this.Canvas == null)
			{
				return;
			}
			foreach (IClippable clippable in this.m_ClipTargets)
			{
				clippable.SetClipSoftness(this.m_Softness);
			}
			foreach (MaskableGraphic maskableGraphic in this.m_MaskableTargets)
			{
				maskableGraphic.SetClipSoftness(this.m_Softness);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000FF48 File Offset: 0x0000E148
		public void AddClippable(IClippable clippable)
		{
			if (clippable == null)
			{
				return;
			}
			this.m_ShouldRecalculateClipRects = true;
			MaskableGraphic maskableGraphic = clippable as MaskableGraphic;
			if (maskableGraphic == null)
			{
				this.m_ClipTargets.Add(clippable);
			}
			else
			{
				this.m_MaskableTargets.Add(maskableGraphic);
			}
			this.m_ForceClip = true;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000FF94 File Offset: 0x0000E194
		public void RemoveClippable(IClippable clippable)
		{
			if (clippable == null)
			{
				return;
			}
			this.m_ShouldRecalculateClipRects = true;
			clippable.SetClipRect(default(Rect), false);
			MaskableGraphic maskableGraphic = clippable as MaskableGraphic;
			if (maskableGraphic == null)
			{
				this.m_ClipTargets.Remove(clippable);
			}
			else
			{
				this.m_MaskableTargets.Remove(maskableGraphic);
			}
			this.m_ForceClip = true;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000FFEF File Offset: 0x0000E1EF
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_ShouldRecalculateClipRects = true;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000FFFE File Offset: 0x0000E1FE
		protected override void OnCanvasHierarchyChanged()
		{
			this.m_Canvas = null;
			base.OnCanvasHierarchyChanged();
			this.m_ShouldRecalculateClipRects = true;
		}

		// Token: 0x04000100 RID: 256
		[NonSerialized]
		private readonly RectangularVertexClipper m_VertexClipper = new RectangularVertexClipper();

		// Token: 0x04000101 RID: 257
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x04000102 RID: 258
		[NonSerialized]
		private HashSet<MaskableGraphic> m_MaskableTargets = new HashSet<MaskableGraphic>();

		// Token: 0x04000103 RID: 259
		[NonSerialized]
		private HashSet<IClippable> m_ClipTargets = new HashSet<IClippable>();

		// Token: 0x04000104 RID: 260
		[NonSerialized]
		private bool m_ShouldRecalculateClipRects;

		// Token: 0x04000105 RID: 261
		[NonSerialized]
		private List<RectMask2D> m_Clippers = new List<RectMask2D>();

		// Token: 0x04000106 RID: 262
		[NonSerialized]
		private Rect m_LastClipRectCanvasSpace;

		// Token: 0x04000107 RID: 263
		[NonSerialized]
		private bool m_ForceClip;

		// Token: 0x04000108 RID: 264
		[SerializeField]
		private Vector4 m_Padding;

		// Token: 0x04000109 RID: 265
		[SerializeField]
		private Vector2Int m_Softness;

		// Token: 0x0400010A RID: 266
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x0400010B RID: 267
		private Vector3[] m_Corners = new Vector3[4];
	}
}
