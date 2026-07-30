using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200032F RID: 815
	public struct SubMeshDescriptor
	{
		// Token: 0x06001AF0 RID: 6896 RVA: 0x0002C180 File Offset: 0x0002A380
		public SubMeshDescriptor(int indexStart, int indexCount, MeshTopology topology = MeshTopology.Triangles)
		{
			this.indexStart = indexStart;
			this.indexCount = indexCount;
			this.topology = topology;
			this.bounds = default(Bounds);
			this.baseVertex = 0;
			this.firstVertex = 0;
			this.vertexCount = 0;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0002C1CE File Offset: 0x0002A3CE
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0002C1D6 File Offset: 0x0002A3D6
		public Bounds bounds { get; set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0002C1DF File Offset: 0x0002A3DF
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x0002C1E7 File Offset: 0x0002A3E7
		public MeshTopology topology { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0002C1F0 File Offset: 0x0002A3F0
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0002C1F8 File Offset: 0x0002A3F8
		public int indexStart { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0002C201 File Offset: 0x0002A401
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x0002C209 File Offset: 0x0002A409
		public int indexCount { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0002C212 File Offset: 0x0002A412
		// (set) Token: 0x06001AFA RID: 6906 RVA: 0x0002C21A File Offset: 0x0002A41A
		public int baseVertex { get; set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0002C223 File Offset: 0x0002A423
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0002C22B File Offset: 0x0002A42B
		public int firstVertex { get; set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0002C234 File Offset: 0x0002A434
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x0002C23C File Offset: 0x0002A43C
		public int vertexCount { get; set; }

		// Token: 0x06001AFF RID: 6911 RVA: 0x0002C248 File Offset: 0x0002A448
		public override string ToString()
		{
			return string.Format("(topo={0} indices={1},{2} vertices={3},{4} basevtx={5} bounds={6})", new object[] { this.topology, this.indexStart, this.indexCount, this.firstVertex, this.vertexCount, this.baseVertex, this.bounds });
		}
	}
}
