using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000C1 RID: 193
	internal class RenderPipelineMaterial : Object
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x000372B4 File Offset: 0x000354B4
		public virtual bool IsDefferedMaterial()
		{
			return false;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000372B4 File Offset: 0x000354B4
		public virtual int GetMaterialGBufferCount(HDRenderPipelineAsset asset)
		{
			return 0;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000372B7 File Offset: 0x000354B7
		public virtual void GetMaterialGBufferDescription(HDRenderPipelineAsset asset, out GraphicsFormat[] RTFormat, out GBufferUsage[] gBufferUsage, out bool[] enableWrite)
		{
			RTFormat = null;
			gBufferUsage = null;
			enableWrite = null;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void Build(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void Cleanup()
		{
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void RenderInit(CommandBuffer cmd)
		{
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void Bind(CommandBuffer cmd)
		{
		}
	}
}
