using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200016B RID: 363
	internal class SkyUpdateContext
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00052745 File Offset: 0x00050945
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0005274D File Offset: 0x0005094D
		public SkyRenderer skyRenderer { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00052756 File Offset: 0x00050956
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00052760 File Offset: 0x00050960
		public SkySettings skySettings
		{
			get
			{
				return this.m_SkySettings;
			}
			set
			{
				if (this.m_SkySettings == value)
				{
					return;
				}
				this.skyParametersHash = -1;
				this.m_SkySettings = value;
				this.currentUpdateTime = 0f;
				if (this.m_SkySettings != null && (this.skyRenderer == null || this.m_SkySettings.GetSkyRendererType() != this.skyRenderer.GetType()))
				{
					if (this.skyRenderer != null)
					{
						this.skyRenderer.Cleanup();
					}
					Type skyRendererType = this.m_SkySettings.GetSkyRendererType();
					this.skyRenderer = (SkyRenderer)Activator.CreateInstance(skyRendererType);
					this.skyRenderer.Build();
				}
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00052803 File Offset: 0x00050A03
		public void Cleanup()
		{
			if (this.skyRenderer != null)
			{
				this.skyRenderer.Cleanup();
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00052818 File Offset: 0x00050A18
		public bool IsValid()
		{
			return this.m_SkySettings != null;
		}

		// Token: 0x04000FFA RID: 4090
		private SkySettings m_SkySettings;

		// Token: 0x04000FFC RID: 4092
		public int cachedSkyRenderingContextId = -1;

		// Token: 0x04000FFD RID: 4093
		public int skyParametersHash = -1;

		// Token: 0x04000FFE RID: 4094
		public float currentUpdateTime;
	}
}
