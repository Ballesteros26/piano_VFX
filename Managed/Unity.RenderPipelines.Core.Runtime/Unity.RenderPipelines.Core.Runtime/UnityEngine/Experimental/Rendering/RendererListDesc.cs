using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000006 RID: 6
	public struct RendererListDesc
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023F0 File Offset: 0x000005F0
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000023F8 File Offset: 0x000005F8
		internal CullingResults cullingResult { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002401 File Offset: 0x00000601
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002409 File Offset: 0x00000609
		internal Camera camera { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002412 File Offset: 0x00000612
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000241A File Offset: 0x0000061A
		internal ShaderTagId passName { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002423 File Offset: 0x00000623
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000242B File Offset: 0x0000062B
		internal ShaderTagId[] passNames { get; private set; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002434 File Offset: 0x00000634
		public RendererListDesc(ShaderTagId passName, CullingResults cullingResult, Camera camera)
		{
			this = default(RendererListDesc);
			this.passName = passName;
			this.passNames = null;
			this.cullingResult = cullingResult;
			this.camera = camera;
			this.layerMask = -1;
			this.overrideMaterialPassIndex = 0;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002467 File Offset: 0x00000667
		public RendererListDesc(ShaderTagId[] passNames, CullingResults cullingResult, Camera camera)
		{
			this = default(RendererListDesc);
			this.passNames = passNames;
			this.passName = ShaderTagId.none;
			this.cullingResult = cullingResult;
			this.camera = camera;
			this.layerMask = -1;
			this.overrideMaterialPassIndex = 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000249E File Offset: 0x0000069E
		public bool IsValid()
		{
			return !(this.camera == null) && (!(this.passName == ShaderTagId.none) || (this.passNames != null && this.passNames.Length != 0));
		}

		// Token: 0x04000010 RID: 16
		public SortingCriteria sortingCriteria;

		// Token: 0x04000011 RID: 17
		public PerObjectData rendererConfiguration;

		// Token: 0x04000012 RID: 18
		public RenderQueueRange renderQueueRange;

		// Token: 0x04000013 RID: 19
		public RenderStateBlock? stateBlock;

		// Token: 0x04000014 RID: 20
		public Material overrideMaterial;

		// Token: 0x04000015 RID: 21
		public bool excludeObjectMotionVectors;

		// Token: 0x04000016 RID: 22
		public int layerMask;

		// Token: 0x04000017 RID: 23
		public int overrideMaterialPassIndex;
	}
}
