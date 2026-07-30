using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace UnityEngine.UI
{
	// Token: 0x02000029 RID: 41
	[AddComponentMenu("UI/Mask", 13)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class Mask : UIBehaviour, ICanvasRaycastFilter, IMaterialModifier
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000E9EC File Offset: 0x0000CBEC
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000EA12 File Offset: 0x0000CC12
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000EA1A File Offset: 0x0000CC1A
		public bool showMaskGraphic
		{
			get
			{
				return this.m_ShowMaskGraphic;
			}
			set
			{
				if (this.m_ShowMaskGraphic == value)
				{
					return;
				}
				this.m_ShowMaskGraphic = value;
				if (this.graphic != null)
				{
					this.graphic.SetMaterialDirty();
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000EA48 File Offset: 0x0000CC48
		public Graphic graphic
		{
			get
			{
				Graphic graphic;
				if ((graphic = this.m_Graphic) == null)
				{
					graphic = (this.m_Graphic = base.GetComponent<Graphic>());
				}
				return graphic;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000EA6E File Offset: 0x0000CC6E
		protected Mask()
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000EA7D File Offset: 0x0000CC7D
		public virtual bool MaskEnabled()
		{
			return this.IsActive() && this.graphic != null;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00004C7A File Offset: 0x00002E7A
		[Obsolete("Not used anymore.")]
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000EA95 File Offset: 0x0000CC95
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.canvasRenderer.hasPopInstruction = true;
				this.graphic.SetMaterialDirty();
			}
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.graphic != null)
			{
				this.graphic.SetMaterialDirty();
				this.graphic.canvasRenderer.hasPopInstruction = false;
				this.graphic.canvasRenderer.popMaterialCount = 0;
			}
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = null;
			MaskUtilities.NotifyStencilStateChanged(this);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000EB48 File Offset: 0x0000CD48
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return !base.isActiveAndEnabled || RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000EB64 File Offset: 0x0000CD64
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			if (!this.MaskEnabled())
			{
				return baseMaterial;
			}
			Transform transform = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
			int stencilDepth = MaskUtilities.GetStencilDepth(base.transform, transform);
			if (stencilDepth >= 8)
			{
				Debug.LogWarning("Attempting to use a stencil mask with depth > 8", base.gameObject);
				return baseMaterial;
			}
			int num = 1 << stencilDepth;
			if (num == 1)
			{
				Material material = StencilMaterial.Add(baseMaterial, 1, StencilOp.Replace, CompareFunction.Always, this.m_ShowMaskGraphic ? ColorWriteMask.All : ((ColorWriteMask)0));
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = material;
				Material material2 = StencilMaterial.Add(baseMaterial, 1, StencilOp.Zero, CompareFunction.Always, (ColorWriteMask)0);
				StencilMaterial.Remove(this.m_UnmaskMaterial);
				this.m_UnmaskMaterial = material2;
				this.graphic.canvasRenderer.popMaterialCount = 1;
				this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
				return this.m_MaskMaterial;
			}
			Material material3 = StencilMaterial.Add(baseMaterial, num | (num - 1), StencilOp.Replace, CompareFunction.Equal, this.m_ShowMaskGraphic ? ColorWriteMask.All : ((ColorWriteMask)0), num - 1, num | (num - 1));
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = material3;
			this.graphic.canvasRenderer.hasPopInstruction = true;
			Material material4 = StencilMaterial.Add(baseMaterial, num - 1, StencilOp.Replace, CompareFunction.Equal, (ColorWriteMask)0, num - 1, num | (num - 1));
			StencilMaterial.Remove(this.m_UnmaskMaterial);
			this.m_UnmaskMaterial = material4;
			this.graphic.canvasRenderer.popMaterialCount = 1;
			this.graphic.canvasRenderer.SetPopMaterial(this.m_UnmaskMaterial, 0);
			return this.m_MaskMaterial;
		}

		// Token: 0x040000EB RID: 235
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x040000EC RID: 236
		[SerializeField]
		private bool m_ShowMaskGraphic = true;

		// Token: 0x040000ED RID: 237
		[NonSerialized]
		private Graphic m_Graphic;

		// Token: 0x040000EE RID: 238
		[NonSerialized]
		private Material m_MaskMaterial;

		// Token: 0x040000EF RID: 239
		[NonSerialized]
		private Material m_UnmaskMaterial;
	}
}
