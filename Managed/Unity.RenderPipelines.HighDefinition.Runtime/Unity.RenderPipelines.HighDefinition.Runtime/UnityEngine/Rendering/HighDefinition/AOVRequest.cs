using System;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200011E RID: 286
	public struct AOVRequest
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x00048388 File Offset: 0x00046588
		public static AOVRequest NewDefault()
		{
			return new AOVRequest
			{
				m_MaterialProperty = MaterialSharedProperty.None,
				m_LightingProperty = LightingProperty.None,
				m_DebugFullScreen = DebugFullScreen.None,
				m_LightFilterProperty = DebugLightFilterMode.None
			};
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000483C0 File Offset: 0x000465C0
		private unsafe AOVRequest* thisPtr
		{
			get
			{
				fixed (AOVRequest* ptr = &this)
				{
					return ptr;
				}
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000483D1 File Offset: 0x000465D1
		public AOVRequest(AOVRequest other)
		{
			this.m_MaterialProperty = other.m_MaterialProperty;
			this.m_LightingProperty = other.m_LightingProperty;
			this.m_DebugFullScreen = other.m_DebugFullScreen;
			this.m_LightFilterProperty = other.m_LightFilterProperty;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00048403 File Offset: 0x00046603
		public unsafe ref AOVRequest SetFullscreenOutput(MaterialSharedProperty materialProperty)
		{
			this.m_MaterialProperty = materialProperty;
			return ref *this.thisPtr;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00048412 File Offset: 0x00046612
		public unsafe ref AOVRequest SetFullscreenOutput(LightingProperty lightingProperty)
		{
			this.m_LightingProperty = lightingProperty;
			return ref *this.thisPtr;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00048421 File Offset: 0x00046621
		public unsafe ref AOVRequest SetFullscreenOutput(DebugFullScreen debugFullScreen)
		{
			this.m_DebugFullScreen = debugFullScreen;
			return ref *this.thisPtr;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00048430 File Offset: 0x00046630
		public unsafe ref AOVRequest SetLightFilter(DebugLightFilterMode filter)
		{
			this.m_LightFilterProperty = filter;
			return ref *this.thisPtr;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00048440 File Offset: 0x00046640
		public void FillDebugData(DebugDisplaySettings debug)
		{
			debug.SetDebugViewCommonMaterialProperty(this.m_MaterialProperty);
			LightingProperty lightingProperty = this.m_LightingProperty;
			if (lightingProperty != LightingProperty.DiffuseOnly)
			{
				if (lightingProperty != LightingProperty.SpecularOnly)
				{
					debug.SetDebugLightingMode(DebugLightingMode.None);
				}
				else
				{
					debug.SetDebugLightingMode(DebugLightingMode.SpecularLighting);
				}
			}
			else
			{
				debug.SetDebugLightingMode(DebugLightingMode.DiffuseLighting);
			}
			debug.SetDebugLightFilterMode(this.m_LightFilterProperty);
			switch (this.m_DebugFullScreen)
			{
			case DebugFullScreen.None:
				debug.SetFullScreenDebugMode(FullScreenDebugMode.None);
				return;
			case DebugFullScreen.Depth:
				debug.SetFullScreenDebugMode(FullScreenDebugMode.DepthPyramid);
				return;
			case DebugFullScreen.ScreenSpaceAmbientOcclusion:
				debug.SetFullScreenDebugMode(FullScreenDebugMode.SSAO);
				return;
			case DebugFullScreen.MotionVectors:
				debug.SetFullScreenDebugMode(FullScreenDebugMode.MotionVectors);
				return;
			default:
				throw new ArgumentException("Unknown DebugFullScreen");
			}
		}

		// Token: 0x04000D8F RID: 3471
		[Obsolete("Since 2019.3, use AOVRequest.NewDefault() instead.")]
		public static readonly AOVRequest @default;

		// Token: 0x04000D90 RID: 3472
		private MaterialSharedProperty m_MaterialProperty;

		// Token: 0x04000D91 RID: 3473
		private LightingProperty m_LightingProperty;

		// Token: 0x04000D92 RID: 3474
		private DebugLightFilterMode m_LightFilterProperty;

		// Token: 0x04000D93 RID: 3475
		private DebugFullScreen m_DebugFullScreen;
	}
}
