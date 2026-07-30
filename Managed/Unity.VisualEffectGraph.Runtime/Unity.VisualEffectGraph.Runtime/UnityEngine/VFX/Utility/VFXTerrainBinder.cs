using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200001C RID: 28
	[AddComponentMenu("VFX/Property Binders/Terrain Binder")]
	[VFXBinder("Utility/Terrain")]
	internal class VFXTerrainBinder : VFXBinderBase
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004131 File Offset: 0x00002331
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000413E File Offset: 0x0000233E
		public string Property
		{
			get
			{
				return (string)this.m_Property;
			}
			set
			{
				this.m_Property = value;
				this.UpdateSubProperties();
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004152 File Offset: 0x00002352
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateSubProperties();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004160 File Offset: 0x00002360
		private void OnValidate()
		{
			this.UpdateSubProperties();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004168 File Offset: 0x00002368
		private void UpdateSubProperties()
		{
			this.Terrain_Bounds_center = this.m_Property + "_Bounds_center";
			this.Terrain_Bounds_size = this.m_Property + "_Bounds_size";
			this.Terrain_HeightMap = this.m_Property + "_HeightMap";
			this.Terrain_Height = this.m_Property + "_Height";
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000041E4 File Offset: 0x000023E4
		public override bool IsValid(VisualEffect component)
		{
			return this.Terrain != null && component.HasVector3(this.Terrain_Bounds_center) && component.HasVector3(this.Terrain_Bounds_size) && component.HasTexture(this.Terrain_HeightMap) && component.HasFloat(this.Terrain_Height);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000424C File Offset: 0x0000244C
		public override void UpdateBinding(VisualEffect component)
		{
			Bounds bounds = this.Terrain.terrainData.bounds;
			component.SetVector3(this.Terrain_Bounds_center, bounds.center);
			component.SetVector3(this.Terrain_Bounds_size, bounds.size);
			component.SetTexture(this.Terrain_HeightMap, this.Terrain.terrainData.heightmapTexture);
			component.SetFloat(this.Terrain_Height, this.Terrain.terrainData.heightmapScale.y);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000042E1 File Offset: 0x000024E1
		public override string ToString()
		{
			return string.Format("Sphere : '{0}' -> {1}", this.m_Property, (this.Terrain == null) ? "(null)" : this.Terrain.name);
		}

		// Token: 0x04000078 RID: 120
		[VFXPropertyBinding(new string[] { "UnityEditor.VFX.TerrainType" })]
		[FormerlySerializedAs("TerrainParameter")]
		public ExposedProperty m_Property;

		// Token: 0x04000079 RID: 121
		public Terrain Terrain;

		// Token: 0x0400007A RID: 122
		private ExposedProperty Terrain_Bounds_center;

		// Token: 0x0400007B RID: 123
		private ExposedProperty Terrain_Bounds_size;

		// Token: 0x0400007C RID: 124
		private ExposedProperty Terrain_HeightMap;

		// Token: 0x0400007D RID: 125
		private ExposedProperty Terrain_Height;
	}
}
