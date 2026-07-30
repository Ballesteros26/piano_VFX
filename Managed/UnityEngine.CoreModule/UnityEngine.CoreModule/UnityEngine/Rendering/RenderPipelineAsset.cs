using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000370 RID: 880
	public abstract class RenderPipelineAsset : ScriptableObject
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x00033358 File Offset: 0x00031558
		internal RenderPipeline InternalCreatePipeline()
		{
			RenderPipeline renderPipeline = null;
			try
			{
				renderPipeline = this.CreatePipeline();
			}
			catch (Exception ex)
			{
				bool flag = !ex.Data.Contains("InvalidImport") || !(ex.Data["InvalidImport"] is int) || (int)ex.Data["InvalidImport"] != 1;
				if (flag)
				{
					Debug.LogException(ex);
				}
			}
			return renderPipeline;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual string[] renderingLayerMaskNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Material defaultMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader autodeskInteractiveShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader autodeskInteractiveTransparentShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader autodeskInteractiveMaskedShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader terrainDetailLitShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader terrainDetailGrassShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader terrainDetailGrassBillboardShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Material defaultParticleMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Material defaultLineMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Material defaultTerrainMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Material defaultUIMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Material defaultUIOverdrawMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Material defaultUIETC1SupportedMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Material default2DMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader defaultShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader defaultSpeedTree7Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000333E0 File Offset: 0x000315E0
		public virtual Shader defaultSpeedTree8Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001E39 RID: 7737
		protected abstract RenderPipeline CreatePipeline();

		// Token: 0x06001E3A RID: 7738 RVA: 0x000333E3 File Offset: 0x000315E3
		protected virtual void OnValidate()
		{
			RenderPipelineManager.CleanupRenderPipeline();
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000333E3 File Offset: 0x000315E3
		protected virtual void OnDisable()
		{
			RenderPipelineManager.CleanupRenderPipeline();
		}
	}
}
