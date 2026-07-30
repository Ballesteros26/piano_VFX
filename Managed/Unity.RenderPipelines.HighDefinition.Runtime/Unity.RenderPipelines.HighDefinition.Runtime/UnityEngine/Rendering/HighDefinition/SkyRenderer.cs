using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000162 RID: 354
	public abstract class SkyRenderer
	{
		// Token: 0x06000A7A RID: 2682
		public abstract void Build();

		// Token: 0x06000A7B RID: 2683
		public abstract void Cleanup();

		// Token: 0x06000A7C RID: 2684 RVA: 0x000372B4 File Offset: 0x000354B4
		protected virtual bool Update(BuiltinSkyParameters builtinParams)
		{
			return false;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void PreRenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk)
		{
		}

		// Token: 0x06000A7E RID: 2686
		public abstract void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk);

		// Token: 0x06000A7F RID: 2687 RVA: 0x00052314 File Offset: 0x00050514
		protected static float GetSkyIntensity(SkySettings skySettings, DebugDisplaySettings debugSettings)
		{
			float num = 1f;
			if (debugSettings != null && debugSettings.DebugNeedsExposure())
			{
				num *= ColorUtils.ConvertEV100ToExposure(-debugSettings.data.lightingDebugSettings.debugExposure);
			}
			switch (skySettings.skyIntensityMode.value)
			{
			case SkyIntensityMode.Exposure:
				num *= ColorUtils.ConvertEV100ToExposure(-skySettings.exposure.value);
				break;
			case SkyIntensityMode.Lux:
				num *= skySettings.desiredLuxValue.value / skySettings.upperHemisphereLuxValue.value;
				break;
			case SkyIntensityMode.Multiplier:
				num *= skySettings.multiplier.value;
				break;
			}
			return num;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void SetGlobalSkyData(CommandBuffer cmd, BuiltinSkyParameters builtinParams)
		{
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000523AC File Offset: 0x000505AC
		internal bool DoUpdate(BuiltinSkyParameters parameters)
		{
			if (this.m_LastFrameUpdate < parameters.frameIndex)
			{
				this.m_LastFrameUpdate = parameters.frameIndex;
				return this.Update(parameters);
			}
			return false;
		}

		// Token: 0x04000FDF RID: 4063
		private int m_LastFrameUpdate = -1;
	}
}
