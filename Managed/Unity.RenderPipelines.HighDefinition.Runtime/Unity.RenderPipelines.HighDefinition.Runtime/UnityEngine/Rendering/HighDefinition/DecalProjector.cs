using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000AF RID: 175
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Decal-Projector.html")]
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Decal Projector")]
	public class DecalProjector : MonoBehaviour, IVersionable<DecalProjector.Version>
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x000348C6 File Offset: 0x00032AC6
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x000348CE File Offset: 0x00032ACE
		DecalProjector.Version IVersionable<DecalProjector.Version>.version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000348D8 File Offset: 0x00032AD8
		private void Awake()
		{
			DecalProjector.k_Migration.Migrate(this);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x000348F4 File Offset: 0x00032AF4
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x000348FC File Offset: 0x00032AFC
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0003490B File Offset: 0x00032B0B
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00034913 File Offset: 0x00032B13
		public float drawDistance
		{
			get
			{
				return this.m_DrawDistance;
			}
			set
			{
				this.m_DrawDistance = Mathf.Max(0f, value);
				this.OnValidate();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0003492C File Offset: 0x00032B2C
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00034934 File Offset: 0x00032B34
		public float fadeScale
		{
			get
			{
				return this.m_FadeScale;
			}
			set
			{
				this.m_FadeScale = Mathf.Clamp01(value);
				this.OnValidate();
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00034948 File Offset: 0x00032B48
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x00034950 File Offset: 0x00032B50
		public Vector2 uvScale
		{
			get
			{
				return this.m_UVScale;
			}
			set
			{
				this.m_UVScale = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0003495F File Offset: 0x00032B5F
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00034967 File Offset: 0x00032B67
		public Vector2 uvBias
		{
			get
			{
				return this.m_UVBias;
			}
			set
			{
				this.m_UVBias = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00034976 File Offset: 0x00032B76
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x0003497E File Offset: 0x00032B7E
		public bool affectsTransparency
		{
			get
			{
				return this.m_AffectsTransparency;
			}
			set
			{
				this.m_AffectsTransparency = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0003498D File Offset: 0x00032B8D
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x00034995 File Offset: 0x00032B95
		internal Vector3 offset
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				this.m_Offset = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x000349A4 File Offset: 0x00032BA4
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x000349AC File Offset: 0x00032BAC
		public Vector3 size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x000349BB File Offset: 0x00032BBB
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x000349C3 File Offset: 0x00032BC3
		public float fadeFactor
		{
			get
			{
				return this.m_FadeFactor;
			}
			set
			{
				this.m_FadeFactor = Mathf.Clamp01(value);
				this.OnValidate();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000349D7 File Offset: 0x00032BD7
		internal Quaternion rotation
		{
			get
			{
				return base.transform.rotation * DecalProjector.k_MinusYtoZRotation;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000349EE File Offset: 0x00032BEE
		internal Vector3 position
		{
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000349FB File Offset: 0x00032BFB
		internal Vector3 decalSize
		{
			get
			{
				return new Vector3(this.m_Size.x, this.m_Size.z, this.m_Size.y);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00034A23 File Offset: 0x00032C23
		internal Vector3 decalOffset
		{
			get
			{
				return new Vector3(this.m_Offset.x, -this.m_Offset.z, this.m_Offset.y);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00034A4C File Offset: 0x00032C4C
		internal Vector4 uvScaleBias
		{
			get
			{
				return new Vector4(this.m_UVScale.x, this.m_UVScale.y, this.m_UVBias.x, this.m_UVBias.y);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00034A7F File Offset: 0x00032C7F
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00034A87 File Offset: 0x00032C87
		internal DecalSystem.DecalHandle Handle
		{
			get
			{
				return this.m_Handle;
			}
			set
			{
				this.m_Handle = value;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00034A90 File Offset: 0x00032C90
		private void OnEnable()
		{
			if (this.m_Material == null)
			{
				this.m_Material = null;
			}
			if (this.m_Handle != null)
			{
				DecalSystem.instance.RemoveDecal(this.m_Handle);
				this.m_Handle = null;
			}
			Matrix4x4 matrix4x = Matrix4x4.Translate(this.decalOffset) * Matrix4x4.Scale(this.decalSize);
			this.m_Handle = DecalSystem.instance.AddDecal(this.position, this.rotation, Vector3.one, matrix4x, this.m_DrawDistance, this.m_FadeScale, this.uvScaleBias, this.m_AffectsTransparency, this.m_Material, base.gameObject.layer, this.m_FadeFactor);
			this.m_OldMaterial = this.m_Material;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00034B4A File Offset: 0x00032D4A
		private void OnDisable()
		{
			if (this.m_Handle != null)
			{
				DecalSystem.instance.RemoveDecal(this.m_Handle);
				this.m_Handle = null;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600068A RID: 1674 RVA: 0x00034B6C File Offset: 0x00032D6C
		// (remove) Token: 0x0600068B RID: 1675 RVA: 0x00034BA4 File Offset: 0x00032DA4
		public event Action OnMaterialChange;

		// Token: 0x0600068C RID: 1676 RVA: 0x00034BDC File Offset: 0x00032DDC
		internal void OnValidate()
		{
			if (this.m_Handle != null)
			{
				if (this.m_Material == null)
				{
					DecalSystem.instance.RemoveDecal(this.m_Handle);
				}
				Matrix4x4 matrix4x = Matrix4x4.Translate(this.decalOffset) * Matrix4x4.Scale(this.decalSize);
				if (this.m_OldMaterial != this.m_Material)
				{
					DecalSystem.instance.RemoveDecal(this.m_Handle);
					if (this.m_Material != null)
					{
						this.m_Handle = DecalSystem.instance.AddDecal(this.position, this.rotation, Vector3.one, matrix4x, this.m_DrawDistance, this.m_FadeScale, this.uvScaleBias, this.m_AffectsTransparency, this.m_Material, base.gameObject.layer, this.m_FadeFactor);
						if (!DecalSystem.IsHDRenderPipelineDecal(this.m_Material.shader))
						{
							this.m_AffectsTransparency = false;
						}
					}
					if (this.OnMaterialChange != null)
					{
						this.OnMaterialChange();
					}
					this.m_OldMaterial = this.m_Material;
					return;
				}
				DecalSystem.instance.UpdateCachedData(this.position, this.rotation, matrix4x, this.m_DrawDistance, this.m_FadeScale, this.uvScaleBias, this.m_AffectsTransparency, this.m_Handle, base.gameObject.layer, this.m_FadeFactor);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00034D34 File Offset: 0x00032F34
		private void LateUpdate()
		{
			if (this.m_Handle != null && base.transform.hasChanged)
			{
				Matrix4x4 matrix4x = Matrix4x4.Translate(this.decalOffset) * Matrix4x4.Scale(this.decalSize);
				DecalSystem.instance.UpdateCachedData(this.position, this.rotation, matrix4x, this.m_DrawDistance, this.m_FadeScale, this.uvScaleBias, this.m_AffectsTransparency, this.m_Handle, base.gameObject.layer, this.m_FadeFactor);
				base.transform.hasChanged = false;
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00034DC4 File Offset: 0x00032FC4
		public bool IsValid()
		{
			return !(this.m_Material == null);
		}

		// Token: 0x040006C1 RID: 1729
		private static readonly MigrationDescription<DecalProjector.Version, DecalProjector> k_Migration = MigrationDescription.New<DecalProjector.Version, DecalProjector>(new MigrationStep<DecalProjector.Version, DecalProjector>[]
		{
			MigrationStep.New<DecalProjector.Version, DecalProjector>(DecalProjector.Version.UseZProjectionAxisAndScaleIndependance, delegate(DecalProjector decal)
			{
				decal.m_Size.Scale(decal.transform.lossyScale);
				decal.transform.RotateAround(decal.transform.position, decal.transform.right, 90f);
				foreach (object obj in decal.transform)
				{
					((Transform)obj).RotateAround(decal.transform.position, decal.transform.right, -90f);
				}
				float num = decal.m_Size.y;
				decal.m_Size.y = decal.m_Size.z;
				decal.m_Size.z = num;
				num = -decal.m_Offset.y * decal.transform.lossyScale.y;
				decal.m_Offset.y = decal.m_Offset.z * decal.transform.lossyScale.z;
				decal.m_Offset.z = num;
				decal.m_Offset.x = decal.m_Offset.x * decal.transform.lossyScale.x;
				if (decal.m_Handle != null)
				{
					DecalSystem.instance.RemoveDecal(decal.m_Handle);
				}
				Matrix4x4 matrix4x = Matrix4x4.Translate(decal.decalOffset) * Matrix4x4.Scale(decal.decalSize);
				decal.m_Handle = DecalSystem.instance.AddDecal(decal.position, decal.rotation, Vector3.one, matrix4x, decal.m_DrawDistance, decal.m_FadeScale, decal.uvScaleBias, decal.m_AffectsTransparency, decal.m_Material, decal.gameObject.layer, decal.m_FadeFactor);
			}),
			MigrationStep.New<DecalProjector.Version, DecalProjector>(DecalProjector.Version.FixPivotPosition, delegate(DecalProjector decal)
			{
				Vector3 vector = decal.m_Offset - new Vector3(0f, 0f, decal.m_Size.z * 0.5f);
				decal.transform.Translate(vector);
				decal.m_Offset.x = 0f;
				decal.m_Offset.y = 0f;
				decal.m_Offset.z = decal.m_Size.z * 0.5f;
				Transform parent = decal.transform.parent;
				if (parent != null)
				{
					vector.x *= parent.transform.lossyScale.x;
					vector.y *= parent.transform.lossyScale.y;
					vector.z *= parent.transform.lossyScale.z;
					vector = decal.transform.rotation * -vector;
				}
				foreach (object obj2 in decal.transform)
				{
					((Transform)obj2).Translate(vector, Space.World);
				}
				if (decal.m_Handle != null)
				{
					DecalSystem.instance.RemoveDecal(decal.m_Handle);
				}
				Matrix4x4 matrix4x2 = Matrix4x4.Translate(decal.decalOffset) * Matrix4x4.Scale(decal.decalSize);
				decal.m_Handle = DecalSystem.instance.AddDecal(decal.position, decal.rotation, Vector3.one, matrix4x2, decal.m_DrawDistance, decal.m_FadeScale, decal.uvScaleBias, decal.m_AffectsTransparency, decal.m_Material, decal.gameObject.layer, decal.m_FadeFactor);
			})
		});

		// Token: 0x040006C2 RID: 1730
		[SerializeField]
		private DecalProjector.Version m_Version = MigrationDescription.LastVersion<DecalProjector.Version>();

		// Token: 0x040006C3 RID: 1731
		internal static readonly Quaternion k_MinusYtoZRotation = Quaternion.Euler(-90f, 0f, 0f);

		// Token: 0x040006C4 RID: 1732
		[SerializeField]
		private Material m_Material;

		// Token: 0x040006C5 RID: 1733
		[SerializeField]
		private float m_DrawDistance = 1000f;

		// Token: 0x040006C6 RID: 1734
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FadeScale = 0.9f;

		// Token: 0x040006C7 RID: 1735
		[SerializeField]
		private Vector2 m_UVScale = new Vector2(1f, 1f);

		// Token: 0x040006C8 RID: 1736
		[SerializeField]
		private Vector2 m_UVBias = new Vector2(0f, 0f);

		// Token: 0x040006C9 RID: 1737
		[SerializeField]
		private bool m_AffectsTransparency;

		// Token: 0x040006CA RID: 1738
		[SerializeField]
		private Vector3 m_Offset = new Vector3(0f, 0f, 0.5f);

		// Token: 0x040006CB RID: 1739
		[SerializeField]
		private Vector3 m_Size = new Vector3(1f, 1f, 1f);

		// Token: 0x040006CC RID: 1740
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FadeFactor = 1f;

		// Token: 0x040006CD RID: 1741
		private Material m_OldMaterial;

		// Token: 0x040006CE RID: 1742
		private DecalSystem.DecalHandle m_Handle;

		// Token: 0x0200022B RID: 555
		private enum Version
		{
			// Token: 0x04001431 RID: 5169
			Initial,
			// Token: 0x04001432 RID: 5170
			UseZProjectionAxisAndScaleIndependance,
			// Token: 0x04001433 RID: 5171
			FixPivotPosition
		}
	}
}
