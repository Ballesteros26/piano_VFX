using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200007B RID: 123
	internal static class HDProbeSystem
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0002BCF1 File Offset: 0x00029EF1
		private static void DisposeStaticInstance()
		{
			HDProbeSystem.s_Instance.Dispose();
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0002BCFD File Offset: 0x00029EFD
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0002BD09 File Offset: 0x00029F09
		public static ReflectionSystemParameters Parameters
		{
			get
			{
				return HDProbeSystem.s_Instance.Parameters;
			}
			set
			{
				HDProbeSystem.s_Instance.Parameters = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0002BD16 File Offset: 0x00029F16
		public static IList<HDProbe> realtimeViewDependentProbes
		{
			get
			{
				return HDProbeSystem.s_Instance.realtimeViewDependentProbes;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0002BD22 File Offset: 0x00029F22
		public static IList<HDProbe> realtimeViewIndependentProbes
		{
			get
			{
				return HDProbeSystem.s_Instance.realtimeViewIndependentProbes;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0002BD2E File Offset: 0x00029F2E
		public static IList<HDProbe> bakedProbes
		{
			get
			{
				return HDProbeSystem.s_Instance.bakedProbes;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0002BD3A File Offset: 0x00029F3A
		public static void RegisterProbe(HDProbe probe)
		{
			HDProbeSystem.s_Instance.RegisterProbe(probe);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0002BD47 File Offset: 0x00029F47
		public static void UnregisterProbe(HDProbe probe)
		{
			HDProbeSystem.s_Instance.UnregisterProbe(probe);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0002BD54 File Offset: 0x00029F54
		public static void Render(HDProbe probe, Transform viewerTransform, Texture outTarget, out HDProbe.RenderData outRenderData, bool forceFlipY = false, float referenceFieldOfView = 90f, float referenceAspect = 1f)
		{
			ProbeCapturePositionSettings probeCapturePositionSettings = ProbeCapturePositionSettings.ComputeFrom(probe, viewerTransform);
			CameraSettings cameraSettings;
			CameraPositionSettings cameraPositionSettings;
			HDRenderUtilities.Render(probe.settings, probeCapturePositionSettings, outTarget, out cameraSettings, out cameraPositionSettings, forceFlipY, false, 0U, referenceFieldOfView, referenceAspect);
			outRenderData = new HDProbe.RenderData(cameraSettings, cameraPositionSettings);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0002BD8F File Offset: 0x00029F8F
		public static void AssignRenderData(HDProbe probe, HDProbe.RenderData renderData, ProbeSettings.Mode targetMode)
		{
			switch (targetMode)
			{
			case ProbeSettings.Mode.Baked:
				probe.bakedRenderData = renderData;
				return;
			case ProbeSettings.Mode.Realtime:
				probe.realtimeRenderData = renderData;
				return;
			case ProbeSettings.Mode.Custom:
				probe.customRenderData = renderData;
				return;
			default:
				return;
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002BDBB File Offset: 0x00029FBB
		public static HDProbeCullState PrepareCull(Camera camera)
		{
			return HDProbeSystem.s_Instance.PrepareCull(camera);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002BDC8 File Offset: 0x00029FC8
		public static void QueryCullResults(HDProbeCullState state, ref HDProbeCullingResults results)
		{
			HDProbeSystem.s_Instance.QueryCullResults(state, ref results);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0002BDD8 File Offset: 0x00029FD8
		public static Texture CreateRenderTargetForMode(HDProbe probe, ProbeSettings.Mode targetMode)
		{
			Texture texture = null;
			HDRenderPipeline hdrenderPipeline = (HDRenderPipeline)RenderPipelineManager.currentPipeline;
			ProbeSettings settings = probe.settings;
			switch (targetMode)
			{
			case ProbeSettings.Mode.Baked:
			case ProbeSettings.Mode.Custom:
			{
				ProbeSettings.ProbeType probeType = settings.type;
				if (probeType != ProbeSettings.ProbeType.ReflectionProbe)
				{
					if (probeType == ProbeSettings.ProbeType.PlanarProbe)
					{
						texture = HDRenderUtilities.CreatePlanarProbeRenderTarget((int)probe.resolution);
					}
				}
				else
				{
					texture = HDRenderUtilities.CreateReflectionProbeRenderTarget((int)hdrenderPipeline.currentPlatformRenderPipelineSettings.lightLoopSettings.reflectionCubemapSize);
				}
				break;
			}
			case ProbeSettings.Mode.Realtime:
			{
				ProbeSettings.ProbeType probeType = settings.type;
				if (probeType != ProbeSettings.ProbeType.ReflectionProbe)
				{
					if (probeType == ProbeSettings.ProbeType.PlanarProbe)
					{
						texture = HDRenderUtilities.CreatePlanarProbeRenderTarget((int)probe.resolution);
					}
				}
				else
				{
					texture = HDRenderUtilities.CreateReflectionProbeRenderTarget((int)hdrenderPipeline.currentPlatformRenderPipelineSettings.lightLoopSettings.reflectionCubemapSize);
				}
				break;
			}
			}
			return texture;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0002BE74 File Offset: 0x0002A074
		private static Texture CreateAndSetRenderTargetIfRequired(HDProbe probe, ProbeSettings.Mode targetMode)
		{
			ProbeSettings settings = probe.settings;
			Texture texture = probe.GetTexture(targetMode);
			if (texture != null)
			{
				return texture;
			}
			texture = HDProbeSystem.CreateRenderTargetForMode(probe, targetMode);
			probe.SetTexture(targetMode, texture);
			return texture;
		}

		// Token: 0x04000527 RID: 1319
		private static HDProbeSystemInternal s_Instance = new HDProbeSystemInternal();
	}
}
