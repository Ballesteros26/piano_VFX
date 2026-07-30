using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000002 RID: 2
[ExecuteAlways]
public class SceneRenderPipeline : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void OnEnable()
	{
		GraphicsSettings.renderPipelineAsset = this.renderPipelineAsset;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
	private void OnValidate()
	{
		GraphicsSettings.renderPipelineAsset = this.renderPipelineAsset;
	}

	// Token: 0x04000001 RID: 1
	public RenderPipelineAsset renderPipelineAsset;
}
