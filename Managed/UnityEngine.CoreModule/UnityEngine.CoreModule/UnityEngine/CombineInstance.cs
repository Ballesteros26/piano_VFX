using System;

namespace UnityEngine
{
	// Token: 0x0200014A RID: 330
	public struct CombineInstance
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x000124EC File Offset: 0x000106EC
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x00012509 File Offset: 0x00010709
		public Mesh mesh
		{
			get
			{
				return Mesh.FromInstanceID(this.m_MeshInstanceID);
			}
			set
			{
				this.m_MeshInstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00012524 File Offset: 0x00010724
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0001253C File Offset: 0x0001073C
		public int subMeshIndex
		{
			get
			{
				return this.m_SubMeshIndex;
			}
			set
			{
				this.m_SubMeshIndex = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00012548 File Offset: 0x00010748
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x00012560 File Offset: 0x00010760
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0001256C File Offset: 0x0001076C
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x00012584 File Offset: 0x00010784
		public Vector4 lightmapScaleOffset
		{
			get
			{
				return this.m_LightmapScaleOffset;
			}
			set
			{
				this.m_LightmapScaleOffset = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00012590 File Offset: 0x00010790
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x000125A8 File Offset: 0x000107A8
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				return this.m_RealtimeLightmapScaleOffset;
			}
			set
			{
				this.m_RealtimeLightmapScaleOffset = value;
			}
		}

		// Token: 0x04000428 RID: 1064
		private int m_MeshInstanceID;

		// Token: 0x04000429 RID: 1065
		private int m_SubMeshIndex;

		// Token: 0x0400042A RID: 1066
		private Matrix4x4 m_Transform;

		// Token: 0x0400042B RID: 1067
		private Vector4 m_LightmapScaleOffset;

		// Token: 0x0400042C RID: 1068
		private Vector4 m_RealtimeLightmapScaleOffset;
	}
}
