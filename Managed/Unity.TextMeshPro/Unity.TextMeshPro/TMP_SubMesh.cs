using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000045 RID: 69
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	[ExecuteAlways]
	public class TMP_SubMesh : MonoBehaviour
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000118D2 File Offset: 0x0000FAD2
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x000118DA File Offset: 0x0000FADA
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000118E3 File Offset: 0x0000FAE3
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x000118EB File Offset: 0x0000FAEB
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000118F4 File Offset: 0x0000FAF4
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00011904 File Offset: 0x0000FB04
		public Material material
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0001194D File Offset: 0x0000FB4D
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00011955 File Offset: 0x0000FB55
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0001195E File Offset: 0x0000FB5E
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00011968 File Offset: 0x0000FB68
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000119C9 File Offset: 0x0000FBC9
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x000119D1 File Offset: 0x0000FBD1
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000119DA File Offset: 0x0000FBDA
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x000119E2 File Offset: 0x0000FBE2
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000119EB File Offset: 0x0000FBEB
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x000119F3 File Offset: 0x0000FBF3
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000119FC File Offset: 0x0000FBFC
		public Renderer renderer
		{
			get
			{
				if (this.m_renderer == null)
				{
					this.m_renderer = base.GetComponent<Renderer>();
				}
				return this.m_renderer;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00011A1E File Offset: 0x0000FC1E
		public MeshFilter meshFilter
		{
			get
			{
				if (this.m_meshFilter == null)
				{
					this.m_meshFilter = base.GetComponent<MeshFilter>();
				}
				return this.m_meshFilter;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00011A40 File Offset: 0x0000FC40
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00011A7F File Offset: 0x0000FC7F
		public Mesh mesh
		{
			get
			{
				if (this.m_mesh == null)
				{
					this.m_mesh = new Mesh();
					this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
					this.meshFilter.mesh = this.m_mesh;
				}
				return this.m_mesh;
			}
			set
			{
				this.m_mesh = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00011A88 File Offset: 0x0000FC88
		public TMP_Text textComponent
		{
			get
			{
				if (this.m_TextComponent == null)
				{
					this.m_TextComponent = base.GetComponentInParent<TextMeshPro>();
				}
				return this.m_TextComponent;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00011AAC File Offset: 0x0000FCAC
		private void OnEnable()
		{
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			this.meshFilter.sharedMesh = this.mesh;
			if (this.m_sharedMaterial != null)
			{
				this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, new Vector4(-32767f, -32767f, 32767f, 32767f));
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00011B10 File Offset: 0x0000FD10
		private void OnDisable()
		{
			this.m_meshFilter.sharedMesh = null;
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00011B40 File Offset: 0x0000FD40
		private void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
			this.m_isRegisteredForEvents = false;
			this.m_TextComponent.havePropertiesChanged = true;
			this.m_TextComponent.SetAllDirty();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		public static TMP_SubMesh AddSubTextObject(TextMeshPro textComponent, MaterialReference materialReference)
		{
			GameObject gameObject = new GameObject("TMP SubMesh [" + materialReference.material.name + "]", new Type[] { typeof(TMP_SubMesh) });
			TMP_SubMesh component = gameObject.GetComponent<TMP_SubMesh>();
			gameObject.transform.SetParent(textComponent.transform, false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = textComponent.gameObject.layer;
			component.m_meshFilter = gameObject.GetComponent<MeshFilter>();
			component.m_TextComponent = textComponent;
			component.m_fontAsset = materialReference.fontAsset;
			component.m_spriteAsset = materialReference.spriteAsset;
			component.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			component.SetSharedMaterial(materialReference.material);
			component.renderer.sortingLayerID = textComponent.renderer.sortingLayerID;
			component.renderer.sortingOrder = textComponent.renderer.sortingOrder;
			return component;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00011CAC File Offset: 0x0000FEAC
		public void DestroySelf()
		{
			global::UnityEngine.Object.Destroy(base.gameObject, 1f);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		private Material GetMaterial(Material mat)
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
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

		// Token: 0x06000300 RID: 768 RVA: 0x00011D3F File Offset: 0x0000FF3F
		private Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00011D69 File Offset: 0x0000FF69
		private Material GetSharedMaterial()
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
			return this.m_renderer.sharedMaterial;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00011D90 File Offset: 0x0000FF90
		private void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00011DAB File Offset: 0x0000FFAB
		public float GetPaddingForMaterial()
		{
			return ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011DCE File Offset: 0x0000FFCE
		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00011DE3 File Offset: 0x0000FFE3
		public void SetVerticesDirty()
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.havePropertiesChanged = true;
				this.m_TextComponent.SetVerticesDirty();
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00011E13 File Offset: 0x00010013
		public void SetMaterialDirty()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00011E1C File Offset: 0x0001001C
		protected void UpdateMaterial()
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = this.renderer;
			}
			this.m_renderer.sharedMaterial = this.m_sharedMaterial;
			if (this.m_sharedMaterial == null)
			{
				return;
			}
			float @float = this.textComponent.fontSharedMaterial.GetFloat(ShaderUtilities.ShaderTag_CullMode);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_CullMode, @float);
		}

		// Token: 0x040002B5 RID: 693
		[SerializeField]
		private TMP_FontAsset m_fontAsset;

		// Token: 0x040002B6 RID: 694
		[SerializeField]
		private TMP_SpriteAsset m_spriteAsset;

		// Token: 0x040002B7 RID: 695
		[SerializeField]
		private Material m_material;

		// Token: 0x040002B8 RID: 696
		[SerializeField]
		private Material m_sharedMaterial;

		// Token: 0x040002B9 RID: 697
		private Material m_fallbackMaterial;

		// Token: 0x040002BA RID: 698
		private Material m_fallbackSourceMaterial;

		// Token: 0x040002BB RID: 699
		[SerializeField]
		private bool m_isDefaultMaterial;

		// Token: 0x040002BC RID: 700
		[SerializeField]
		private float m_padding;

		// Token: 0x040002BD RID: 701
		[SerializeField]
		private Renderer m_renderer;

		// Token: 0x040002BE RID: 702
		[SerializeField]
		private MeshFilter m_meshFilter;

		// Token: 0x040002BF RID: 703
		private Mesh m_mesh;

		// Token: 0x040002C0 RID: 704
		[SerializeField]
		private TextMeshPro m_TextComponent;

		// Token: 0x040002C1 RID: 705
		[NonSerialized]
		private bool m_isRegisteredForEvents;
	}
}
