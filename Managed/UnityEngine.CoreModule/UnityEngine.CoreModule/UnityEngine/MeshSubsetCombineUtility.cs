using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x020001E2 RID: 482
	internal class MeshSubsetCombineUtility
	{
		// Token: 0x020001E3 RID: 483
		public struct MeshInstance
		{
			// Token: 0x0400069F RID: 1695
			public int meshInstanceID;

			// Token: 0x040006A0 RID: 1696
			public int rendererInstanceID;

			// Token: 0x040006A1 RID: 1697
			public int additionalVertexStreamsMeshInstanceID;

			// Token: 0x040006A2 RID: 1698
			public int enlightenVertexStreamMeshInstanceID;

			// Token: 0x040006A3 RID: 1699
			public Matrix4x4 transform;

			// Token: 0x040006A4 RID: 1700
			public Vector4 lightmapScaleOffset;

			// Token: 0x040006A5 RID: 1701
			public Vector4 realtimeLightmapScaleOffset;
		}

		// Token: 0x020001E4 RID: 484
		public struct SubMeshInstance
		{
			// Token: 0x040006A6 RID: 1702
			public int meshInstanceID;

			// Token: 0x040006A7 RID: 1703
			public int vertexOffset;

			// Token: 0x040006A8 RID: 1704
			public int gameObjectInstanceID;

			// Token: 0x040006A9 RID: 1705
			public int subMeshIndex;

			// Token: 0x040006AA RID: 1706
			public Matrix4x4 transform;
		}

		// Token: 0x020001E5 RID: 485
		public struct MeshContainer
		{
			// Token: 0x040006AB RID: 1707
			public GameObject gameObject;

			// Token: 0x040006AC RID: 1708
			public MeshSubsetCombineUtility.MeshInstance instance;

			// Token: 0x040006AD RID: 1709
			public List<MeshSubsetCombineUtility.SubMeshInstance> subMeshInstances;
		}
	}
}
