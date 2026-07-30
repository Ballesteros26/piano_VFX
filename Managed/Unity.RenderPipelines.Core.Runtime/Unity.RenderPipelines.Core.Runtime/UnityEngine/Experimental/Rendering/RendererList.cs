using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000005 RID: 5
	public struct RendererList
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002254 File Offset: 0x00000454
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000225C File Offset: 0x0000045C
		public bool isValid { get; private set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002268 File Offset: 0x00000468
		public static RendererList Create(in RendererListDesc desc)
		{
			RendererList rendererList = default(RendererList);
			RendererListDesc rendererListDesc = desc;
			if (!rendererListDesc.IsValid())
			{
				return rendererList;
			}
			rendererListDesc = desc;
			SortingSettings sortingSettings = new SortingSettings(rendererListDesc.camera)
			{
				criteria = desc.sortingCriteria
			};
			DrawingSettings drawingSettings = new DrawingSettings(RendererList.s_EmptyName, sortingSettings)
			{
				perObjectData = desc.rendererConfiguration
			};
			rendererListDesc = desc;
			if (rendererListDesc.passName != ShaderTagId.none)
			{
				int num = 0;
				rendererListDesc = desc;
				drawingSettings.SetShaderPassName(num, rendererListDesc.passName);
			}
			else
			{
				int num2 = 0;
				for (;;)
				{
					int num3 = num2;
					rendererListDesc = desc;
					if (num3 >= rendererListDesc.passNames.Length)
					{
						break;
					}
					int num4 = num2;
					rendererListDesc = desc;
					drawingSettings.SetShaderPassName(num4, rendererListDesc.passNames[num2]);
					num2++;
				}
			}
			if (desc.overrideMaterial != null)
			{
				drawingSettings.overrideMaterial = desc.overrideMaterial;
				drawingSettings.overrideMaterialPassIndex = desc.overrideMaterialPassIndex;
			}
			FilteringSettings filteringSettings = new FilteringSettings(new RenderQueueRange?(desc.renderQueueRange), desc.layerMask, uint.MaxValue, 0)
			{
				excludeMotionVectorObjects = desc.excludeObjectMotionVectors
			};
			rendererList.isValid = true;
			rendererListDesc = desc;
			rendererList.cullingResult = rendererListDesc.cullingResult;
			rendererList.drawSettings = drawingSettings;
			rendererList.filteringSettings = filteringSettings;
			rendererList.stateBlock = desc.stateBlock;
			return rendererList;
		}

		// Token: 0x04000009 RID: 9
		private static readonly ShaderTagId s_EmptyName = new ShaderTagId("");

		// Token: 0x0400000A RID: 10
		public static readonly RendererList nullRendererList = default(RendererList);

		// Token: 0x0400000C RID: 12
		public CullingResults cullingResult;

		// Token: 0x0400000D RID: 13
		public DrawingSettings drawSettings;

		// Token: 0x0400000E RID: 14
		public FilteringSettings filteringSettings;

		// Token: 0x0400000F RID: 15
		public RenderStateBlock? stateBlock;
	}
}
