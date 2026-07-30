using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000178 RID: 376
	public static class HDRenderUtilities
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0005349C File Offset: 0x0005169C
		public static void Render(CameraSettings settings, CameraPositionSettings position, Texture target, uint staticFlags = 0U)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			RenderTexture renderTexture = target as RenderTexture;
			Cubemap cubemap = target as Cubemap;
			TextureDimension textureDimension = target.dimension;
			if (textureDimension != TextureDimension.Tex2D)
			{
				if (textureDimension != TextureDimension.Cube)
				{
					throw new ArgumentException("Rendering into a target of dimension " + string.Format("{0} is not supported", target.dimension));
				}
			}
			else if (renderTexture == null)
			{
				throw new ArgumentException("'target' must be a RenderTexture when rendering into a 2D texture");
			}
			Camera camera = HDRenderUtilities.NewRenderingCamera();
			try
			{
				camera.ApplySettings(settings);
				camera.ApplySettings(position);
				textureDimension = target.dimension;
				if (textureDimension != TextureDimension.Tex2D)
				{
					if (textureDimension == TextureDimension.Cube)
					{
						bool flag = false;
						if (!flag || staticFlags == 0U)
						{
							if (!flag && staticFlags != 0U)
							{
								Debug.LogWarning("A static flags bitmask was provided but this is ignored in player builds");
							}
							if (renderTexture != null)
							{
								camera.RenderToCubemap(renderTexture);
							}
							if (cubemap != null)
							{
								camera.RenderToCubemap(cubemap);
							}
						}
						target.IncrementUpdateCount();
					}
				}
				else
				{
					camera.targetTexture = renderTexture;
					camera.Render();
					camera.targetTexture = null;
					target.IncrementUpdateCount();
				}
			}
			finally
			{
				CoreUtils.Destroy(camera.gameObject);
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000535B8 File Offset: 0x000517B8
		public static void Render(ProbeSettings settings, ProbeCapturePositionSettings position, Texture target, bool forceFlipY = false, bool forceInvertBackfaceCulling = false, uint staticFlags = 0U, float referenceFieldOfView = 90f, float referenceAspect = 1f)
		{
			CameraSettings cameraSettings;
			CameraPositionSettings cameraPositionSettings;
			HDRenderUtilities.Render(settings, position, target, out cameraSettings, out cameraPositionSettings, forceFlipY, forceInvertBackfaceCulling, staticFlags, referenceFieldOfView, referenceAspect);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000535DC File Offset: 0x000517DC
		public static void GenerateRenderingSettingsFor(ProbeSettings settings, ProbeCapturePositionSettings position, List<CameraSettings> cameras, List<CameraPositionSettings> cameraPositions, ulong overrideSceneCullingMask, bool forceFlipY = false, float referenceFieldOfView = 90f, float referenceAspect = 1f)
		{
			CameraSettings cameraSettings;
			CameraPositionSettings cameraPositionSettings;
			HDRenderUtilities.ComputeCameraSettingsFromProbeSettings(settings, position, out cameraSettings, out cameraPositionSettings, overrideSceneCullingMask, referenceFieldOfView, referenceAspect);
			if (forceFlipY)
			{
				cameraSettings.flipYMode = HDAdditionalCameraData.FlipYMode.ForceFlipY;
			}
			ProbeSettings.ProbeType type = settings.type;
			if (type != ProbeSettings.ProbeType.ReflectionProbe)
			{
				if (type == ProbeSettings.ProbeType.PlanarProbe)
				{
					cameras.Add(cameraSettings);
					cameraPositions.Add(cameraPositionSettings);
					return;
				}
			}
			else
			{
				for (int i = 0; i < 6; i++)
				{
					CameraPositionSettings cameraPositionSettings2 = cameraPositionSettings;
					cameraPositionSettings2.rotation *= Quaternion.Euler(HDRenderUtilities.s_GenerateRenderingSettingsFor_Rotations[i]);
					cameras.Add(cameraSettings);
					cameraPositions.Add(cameraPositionSettings2);
				}
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00053664 File Offset: 0x00051864
		public static void ComputeCameraSettingsFromProbeSettings(ProbeSettings settings, ProbeCapturePositionSettings position, out CameraSettings cameraSettings, out CameraPositionSettings cameraPositionSettings, ulong overrideSceneCullingMask, float referenceFieldOfView = 90f, float referenceAspect = 1f)
		{
			cameraSettings = settings.cameraSettings;
			cameraPositionSettings = CameraPositionSettings.NewDefault();
			ProbeSettingsUtilities.ApplySettings(ref settings, ref position, ref cameraSettings, ref cameraPositionSettings, referenceFieldOfView, referenceAspect);
			cameraSettings.culling.sceneCullingMaskOverride = overrideSceneCullingMask;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00053699 File Offset: 0x00051899
		public static void Render(ProbeSettings settings, ProbeCapturePositionSettings position, Texture target, out CameraSettings cameraSettings, out CameraPositionSettings cameraPositionSettings, bool forceFlipY = false, bool forceInvertBackfaceCulling = false, uint staticFlags = 0U, float referenceFieldOfView = 90f, float referenceAspect = 1f)
		{
			HDRenderUtilities.ComputeCameraSettingsFromProbeSettings(settings, position, out cameraSettings, out cameraPositionSettings, 0UL, referenceFieldOfView, referenceAspect);
			if (forceFlipY)
			{
				cameraSettings.flipYMode = HDAdditionalCameraData.FlipYMode.ForceFlipY;
			}
			if (forceInvertBackfaceCulling)
			{
				cameraSettings.invertFaceCulling = true;
			}
			HDRenderUtilities.Render(cameraSettings, cameraPositionSettings, target, staticFlags);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000536D6 File Offset: 0x000518D6
		public static RenderTexture CreateReflectionProbeRenderTarget(int cubemapSize)
		{
			return new RenderTexture(cubemapSize, cubemapSize, 1, GraphicsFormat.R16G16B16A16_SFloat)
			{
				dimension = TextureDimension.Cube,
				enableRandomWrite = true,
				useMipMap = true,
				autoGenerateMips = false
			};
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000536FE File Offset: 0x000518FE
		public static RenderTexture CreatePlanarProbeRenderTarget(int planarSize)
		{
			return new RenderTexture(planarSize, planarSize, 1, GraphicsFormat.R16G16B16A16_SFloat)
			{
				dimension = TextureDimension.Tex2D,
				enableRandomWrite = true,
				useMipMap = true,
				autoGenerateMips = false
			};
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00053726 File Offset: 0x00051926
		public static Cubemap CreateReflectionProbeTarget(int cubemapSize)
		{
			return new Cubemap(cubemapSize, GraphicsFormat.R16G16B16A16_SFloat, TextureCreationFlags.None);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00053734 File Offset: 0x00051934
		private static Camera NewRenderingCamera()
		{
			GameObject gameObject = new GameObject("__Render Camera");
			Camera camera = gameObject.AddComponent<Camera>();
			camera.cameraType = CameraType.Reflection;
			gameObject.AddComponent<HDAdditionalCameraData>();
			return camera;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00053764 File Offset: 0x00051964
		private static void FixSettings(Texture target, ref ProbeSettings settings, ref ProbeCapturePositionSettings position, ref CameraSettings cameraSettings, ref CameraPositionSettings cameraPositionSettings)
		{
			RenderTexture renderTexture;
			if ((renderTexture = target as RenderTexture) != null && renderTexture.dimension == TextureDimension.Cube && settings.type == ProbeSettings.ProbeType.ReflectionProbe && SystemInfo.graphicsUVStartsAtTop)
			{
				cameraSettings.flipYMode = HDAdditionalCameraData.FlipYMode.ForceFlipY;
			}
		}

		// Token: 0x0400103E RID: 4158
		private static readonly Vector3[] s_GenerateRenderingSettingsFor_Rotations = new Vector3[]
		{
			new Vector3(0f, 90f, 0f),
			new Vector3(0f, 270f, 0f),
			new Vector3(270f, 0f, 0f),
			new Vector3(90f, 0f, 0f),
			new Vector3(0f, 0f, 0f),
			new Vector3(0f, 180f, 0f)
		};
	}
}
