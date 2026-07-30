using System;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x0200002B RID: 43
	public abstract class MaskableGraphic : Graphic, IClippable, IMaskable, IMaterialModifier
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000F096 File Offset: 0x0000D296
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000F09E File Offset: 0x0000D29E
		public MaskableGraphic.CullStateChangedEvent onCullStateChanged
		{
			get
			{
				return this.m_OnCullStateChanged;
			}
			set
			{
				this.m_OnCullStateChanged = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000F0A7 File Offset: 0x0000D2A7
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000F0AF File Offset: 0x0000D2AF
		public bool maskable
		{
			get
			{
				return this.m_Maskable;
			}
			set
			{
				if (value == this.m_Maskable)
				{
					return;
				}
				this.m_Maskable = value;
				this.m_ShouldRecalculateStencil = true;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			Material material = baseMaterial;
			if (this.m_ShouldRecalculateStencil)
			{
				Transform transform = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
				this.m_StencilValue = (this.maskable ? MaskUtilities.GetStencilDepth(base.transform, transform) : 0);
				this.m_ShouldRecalculateStencil = false;
			}
			Mask component = base.GetComponent<Mask>();
			if (this.m_StencilValue > 0 && (component == null || !component.IsActive()))
			{
				Material material2 = StencilMaterial.Add(material, (1 << this.m_StencilValue) - 1, StencilOp.Keep, CompareFunction.Equal, ColorWriteMask.All, (1 << this.m_StencilValue) - 1, 0);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = material2;
				material = this.m_MaskMaterial;
			}
			return material;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000F178 File Offset: 0x0000D378
		public virtual void Cull(Rect clipRect, bool validRect)
		{
			bool flag = !validRect || !clipRect.Overlaps(this.rootCanvasRect, true);
			this.UpdateCull(flag);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
		private void UpdateCull(bool cull)
		{
			if (base.canvasRenderer.cull != cull)
			{
				base.canvasRenderer.cull = cull;
				UISystemProfilerApi.AddMarker("MaskableGraphic.cullingChanged", this);
				this.m_OnCullStateChanged.Invoke(cull);
				this.OnCullingChanged();
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000F1DD File Offset: 0x0000D3DD
		public virtual void SetClipRect(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.EnableRectClipping(clipRect);
				return;
			}
			base.canvasRenderer.DisableRectClipping();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000F1FA File Offset: 0x0000D3FA
		public virtual void SetClipSoftness(Vector2 clipSoftness)
		{
			base.canvasRenderer.clippingSoftness = clipSoftness;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F208 File Offset: 0x0000D408
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
			if (base.GetComponent<Mask>() != null)
			{
				MaskUtilities.NotifyStencilStateChanged(this);
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000F238 File Offset: 0x0000D438
		protected override void OnDisable()
		{
			base.OnDisable();
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
			this.UpdateClipParent();
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			if (base.GetComponent<Mask>() != null)
			{
				MaskUtilities.NotifyStencilStateChanged(this);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000F284 File Offset: 0x0000D484
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00004C7A File Offset: 0x00002E7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore.", true)]
		public virtual void ParentMaskStateChanged()
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000F2A8 File Offset: 0x0000D4A8
		protected override void OnCanvasHierarchyChanged()
		{
			base.OnCanvasHierarchyChanged();
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.UpdateClipParent();
			this.SetMaterialDirty();
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		private Rect rootCanvasRect
		{
			get
			{
				base.rectTransform.GetWorldCorners(this.m_Corners);
				if (base.canvas)
				{
					Matrix4x4 worldToLocalMatrix = base.canvas.rootCanvas.transform.worldToLocalMatrix;
					for (int i = 0; i < 4; i++)
					{
						this.m_Corners[i] = worldToLocalMatrix.MultiplyPoint(this.m_Corners[i]);
					}
				}
				Vector2 vector = this.m_Corners[0];
				Vector2 vector2 = this.m_Corners[0];
				for (int j = 1; j < 4; j++)
				{
					vector.x = Mathf.Min(this.m_Corners[j].x, vector.x);
					vector.y = Mathf.Min(this.m_Corners[j].y, vector.y);
					vector2.x = Mathf.Max(this.m_Corners[j].x, vector2.x);
					vector2.y = Mathf.Max(this.m_Corners[j].y, vector2.y);
				}
				return new Rect(vector, vector2 - vector);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000F410 File Offset: 0x0000D610
		private void UpdateClipParent()
		{
			RectMask2D rectMask2D = ((this.maskable && this.IsActive()) ? MaskUtilities.GetRectMaskForClippable(this) : null);
			if (this.m_ParentMask != null && (rectMask2D != this.m_ParentMask || !rectMask2D.IsActive()))
			{
				this.m_ParentMask.RemoveClippable(this);
				this.UpdateCull(false);
			}
			if (rectMask2D != null && rectMask2D.IsActive())
			{
				rectMask2D.AddClippable(this);
			}
			this.m_ParentMask = rectMask2D;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000F48D File Offset: 0x0000D68D
		public virtual void RecalculateClipping()
		{
			this.UpdateClipParent();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000F495 File Offset: 0x0000D695
		public virtual void RecalculateMasking()
		{
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000F4EA File Offset: 0x0000D6EA
		GameObject IClippable.get_gameObject()
		{
			return base.gameObject;
		}

		// Token: 0x040000F0 RID: 240
		[NonSerialized]
		protected bool m_ShouldRecalculateStencil = true;

		// Token: 0x040000F1 RID: 241
		[NonSerialized]
		protected Material m_MaskMaterial;

		// Token: 0x040000F2 RID: 242
		[NonSerialized]
		private RectMask2D m_ParentMask;

		// Token: 0x040000F3 RID: 243
		[NonSerialized]
		private bool m_Maskable = true;

		// Token: 0x040000F4 RID: 244
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore.", true)]
		[NonSerialized]
		protected bool m_IncludeForMasking;

		// Token: 0x040000F5 RID: 245
		[SerializeField]
		private MaskableGraphic.CullStateChangedEvent m_OnCullStateChanged = new MaskableGraphic.CullStateChangedEvent();

		// Token: 0x040000F6 RID: 246
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Not used anymore", true)]
		[NonSerialized]
		protected bool m_ShouldRecalculate = true;

		// Token: 0x040000F7 RID: 247
		[NonSerialized]
		protected int m_StencilValue;

		// Token: 0x040000F8 RID: 248
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x0200009D RID: 157
		[Serializable]
		public class CullStateChangedEvent : UnityEvent<bool>
		{
		}
	}
}
