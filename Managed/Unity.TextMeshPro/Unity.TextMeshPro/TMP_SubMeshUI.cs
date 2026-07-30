using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x02000046 RID: 70
	[ExecuteAlways]
	public class TMP_SubMeshUI : MaskableGraphic, IClippable, IMaskable, IMaterialModifier
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00011E8A File Offset: 0x0001008A
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00011E92 File Offset: 0x00010092
		public TMP_FontAsset fontAsset
		{
			get
			{
				return this.m_fontAsset;
			}
			set
			{
				this.m_fontAsset = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00011E9B File Offset: 0x0001009B
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00011EA3 File Offset: 0x000100A3
		public TMP_SpriteAsset spriteAsset
		{
			get
			{
				return this.m_spriteAsset;
			}
			set
			{
				this.m_spriteAsset = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00011EAC File Offset: 0x000100AC
		public override Texture mainTexture
		{
			get
			{
				if (this.sharedMaterial != null)
				{
					return this.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex);
				}
				return null;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00011ECE File Offset: 0x000100CE
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00011EDC File Offset: 0x000100DC
		public override Material material
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial != null && this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
				{
					return;
				}
				this.m_material = value;
				this.m_sharedMaterial = value;
				this.m_padding = this.GetPaddingForMaterial();
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00011F33 File Offset: 0x00010133
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00011F3B File Offset: 0x0001013B
		public Material sharedMaterial
		{
			get
			{
				return this.m_sharedMaterial;
			}
			set
			{
				this.SetSharedMaterial(value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00011F44 File Offset: 0x00010144
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00011F4C File Offset: 0x0001014C
		public Material fallbackMaterial
		{
			get
			{
				return this.m_fallbackMaterial;
			}
			set
			{
				if (this.m_fallbackMaterial == value)
				{
					return;
				}
				if (this.m_fallbackMaterial != null && this.m_fallbackMaterial != value)
				{
					TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				}
				this.m_fallbackMaterial = value;
				TMP_MaterialManager.AddFallbackMaterialReference(this.m_fallbackMaterial);
				this.SetSharedMaterial(this.m_fallbackMaterial);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00011FAD File Offset: 0x000101AD
		// (set) Token: 0x06000315 RID: 789 RVA: 0x00011FB5 File Offset: 0x000101B5
		public Material fallbackSourceMaterial
		{
			get
			{
				return this.m_fallbackSourceMaterial;
			}
			set
			{
				this.m_fallbackSourceMaterial = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00011FBE File Offset: 0x000101BE
		public override Material materialForRendering
		{
			get
			{
				return TMP_MaterialManager.GetMaterialForRendering(this, this.m_sharedMaterial);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00011FCC File Offset: 0x000101CC
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00011FD4 File Offset: 0x000101D4
		public bool isDefaultMaterial
		{
			get
			{
				return this.m_isDefaultMaterial;
			}
			set
			{
				this.m_isDefaultMaterial = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00011FDD File Offset: 0x000101DD
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00011FE5 File Offset: 0x000101E5
		public float padding
		{
			get
			{
				return this.m_padding;
			}
			set
			{
				this.m_padding = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00011FEE File Offset: 0x000101EE
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0001201C File Offset: 0x0001021C
		public Mesh mesh
		{
			get
			{
				if (this.m_mesh == null)
				{
					this.m_mesh = new Mesh();
					this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_mesh;
			}
			set
			{
				this.m_mesh = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00012025 File Offset: 0x00010225
		public TMP_Text textComponent
		{
			get
			{
				if (this.m_TextComponent == null)
				{
					this.m_TextComponent = base.GetComponentInParent<TextMeshProUGUI>();
				}
				return this.m_TextComponent;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00012048 File Offset: 0x00010248
		public static TMP_SubMeshUI AddSubTextObject(TextMeshProUGUI textComponent, MaterialReference materialReference)
		{
			GameObject gameObject = new GameObject("TMP UI SubObject [" + materialReference.material.name + "]", new Type[] { typeof(RectTransform) });
			gameObject.transform.SetParent(textComponent.transform, false);
			gameObject.layer = textComponent.gameObject.layer;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			component.sizeDelta = Vector2.zero;
			component.pivot = textComponent.rectTransform.pivot;
			TMP_SubMeshUI tmp_SubMeshUI = gameObject.AddComponent<TMP_SubMeshUI>();
			tmp_SubMeshUI.m_TextComponent = textComponent;
			tmp_SubMeshUI.m_materialReferenceIndex = materialReference.index;
			tmp_SubMeshUI.m_fontAsset = materialReference.fontAsset;
			tmp_SubMeshUI.m_spriteAsset = materialReference.spriteAsset;
			tmp_SubMeshUI.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			tmp_SubMeshUI.SetSharedMaterial(materialReference.material);
			return tmp_SubMeshUI;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00012129 File Offset: 0x00010329
		protected override void OnEnable()
		{
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			this.m_ShouldRecalculateStencil = true;
			this.RecalculateClipping();
			this.RecalculateMasking();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00012150 File Offset: 0x00010350
		protected override void OnDisable()
		{
			TMP_UpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_MaskMaterial != null)
			{
				TMP_MaterialManager.ReleaseStencilMaterial(this.m_MaskMaterial);
				this.m_MaskMaterial = null;
			}
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
			base.OnDisable();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000121AC File Offset: 0x000103AC
		protected override void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			if (this.m_MaskMaterial != null)
			{
				TMP_MaterialManager.ReleaseStencilMaterial(this.m_MaskMaterial);
			}
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
			this.m_isRegisteredForEvents = false;
			this.RecalculateClipping();
			this.m_TextComponent.havePropertiesChanged = true;
			this.m_TextComponent.SetAllDirty();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001222F File Offset: 0x0001042F
		protected override void OnTransformParentChanged()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.RecalculateClipping();
			this.RecalculateMasking();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00012250 File Offset: 0x00010450
		public override Material GetModifiedMaterial(Material baseMaterial)
		{
			Material material = baseMaterial;
			if (this.m_ShouldRecalculateStencil)
			{
				this.m_StencilValue = TMP_MaterialManager.GetStencilID(base.gameObject);
				this.m_ShouldRecalculateStencil = false;
			}
			if (this.m_StencilValue > 0)
			{
				material = TMP_MaterialManager.GetStencilMaterial(baseMaterial, this.m_StencilValue);
				if (this.m_MaskMaterial != null)
				{
					TMP_MaterialManager.ReleaseStencilMaterial(this.m_MaskMaterial);
				}
				this.m_MaskMaterial = material;
			}
			return material;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000122B6 File Offset: 0x000104B6
		public float GetPaddingForMaterial()
		{
			return ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000122D9 File Offset: 0x000104D9
		public float GetPaddingForMaterial(Material mat)
		{
			return ShaderUtilities.GetPadding(mat, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000122F7 File Offset: 0x000104F7
		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000027BA File Offset: 0x000009BA
		public override void SetAllDirty()
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001230C File Offset: 0x0001050C
		public override void SetVerticesDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.havePropertiesChanged = true;
				this.m_TextComponent.SetVerticesDirty();
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000027BA File Offset: 0x000009BA
		public override void SetLayoutDirty()
		{
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001233C File Offset: 0x0001053C
		public override void SetMaterialDirty()
		{
			this.m_materialDirty = true;
			this.UpdateMaterial();
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001235E File Offset: 0x0001055E
		public void SetPivotDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			base.rectTransform.pivot = this.m_TextComponent.rectTransform.pivot;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000FAAA File Offset: 0x0000DCAA
		public override void Cull(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.cull = false;
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
				return;
			}
			base.Cull(clipRect, validRect);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000027BA File Offset: 0x000009BA
		protected override void UpdateGeometry()
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00012384 File Offset: 0x00010584
		public override void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.PreRender)
			{
				if (!this.m_materialDirty)
				{
					return;
				}
				this.UpdateMaterial();
				this.m_materialDirty = false;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000123A0 File Offset: 0x000105A0
		public void RefreshMaterial()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000123A8 File Offset: 0x000105A8
		protected override void UpdateMaterial()
		{
			if (this.m_sharedMaterial == null)
			{
				return;
			}
			float @float = this.textComponent.fontSharedMaterial.GetFloat(ShaderUtilities.ShaderTag_CullMode);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_CullMode, @float);
			base.canvasRenderer.materialCount = 1;
			base.canvasRenderer.SetMaterial(this.materialForRendering, 0);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00012409 File Offset: 0x00010609
		public override void RecalculateClipping()
		{
			base.RecalculateClipping();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00012411 File Offset: 0x00010611
		public override void RecalculateMasking()
		{
			this.m_ShouldRecalculateStencil = true;
			this.SetMaterialDirty();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00011F33 File Offset: 0x00010133
		private Material GetMaterial()
		{
			return this.m_sharedMaterial;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00012420 File Offset: 0x00010620
		private Material GetMaterial(Material mat)
		{
			if (this.m_material == null || this.m_material.GetInstanceID() != mat.GetInstanceID())
			{
				this.m_material = this.CreateMaterialInstance(mat);
			}
			this.m_sharedMaterial = this.m_material;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
			return this.m_sharedMaterial;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00011D3F File Offset: 0x0000FF3F
		private Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00012485 File Offset: 0x00010685
		private Material GetSharedMaterial()
		{
			return base.canvasRenderer.GetMaterial();
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00012492 File Offset: 0x00010692
		private void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_Material = this.m_sharedMaterial;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x040002C2 RID: 706
		[SerializeField]
		private TMP_FontAsset m_fontAsset;

		// Token: 0x040002C3 RID: 707
		[SerializeField]
		private TMP_SpriteAsset m_spriteAsset;

		// Token: 0x040002C4 RID: 708
		[SerializeField]
		private Material m_material;

		// Token: 0x040002C5 RID: 709
		[SerializeField]
		private Material m_sharedMaterial;

		// Token: 0x040002C6 RID: 710
		private Material m_fallbackMaterial;

		// Token: 0x040002C7 RID: 711
		private Material m_fallbackSourceMaterial;

		// Token: 0x040002C8 RID: 712
		[SerializeField]
		private bool m_isDefaultMaterial;

		// Token: 0x040002C9 RID: 713
		[SerializeField]
		private float m_padding;

		// Token: 0x040002CA RID: 714
		private Mesh m_mesh;

		// Token: 0x040002CB RID: 715
		[SerializeField]
		private TextMeshProUGUI m_TextComponent;

		// Token: 0x040002CC RID: 716
		[NonSerialized]
		private bool m_isRegisteredForEvents;

		// Token: 0x040002CD RID: 717
		private bool m_materialDirty;

		// Token: 0x040002CE RID: 718
		[SerializeField]
		private int m_materialReferenceIndex;
	}
}
