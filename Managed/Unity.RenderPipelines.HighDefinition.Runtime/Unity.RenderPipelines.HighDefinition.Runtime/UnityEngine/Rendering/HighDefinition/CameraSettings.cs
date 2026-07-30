using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	public struct CameraSettings
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00052E1C File Offset: 0x0005101C
		public static CameraSettings NewDefault()
		{
			return new CameraSettings
			{
				bufferClearing = CameraSettings.BufferClearing.NewDefault(),
				culling = CameraSettings.Culling.NewDefault(),
				renderingPathCustomFrameSettings = FrameSettings.NewDefaultCamera(),
				frustum = CameraSettings.Frustum.NewDefault(),
				customRenderingSettings = false,
				volumes = CameraSettings.Volumes.NewDefault(),
				flipYMode = HDAdditionalCameraData.FlipYMode.Automatic,
				invertFaceCulling = false,
				probeLayerMask = -1,
				probeRangeCompressionFactor = 1f
			};
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00052EA0 File Offset: 0x000510A0
		public unsafe static CameraSettings From(HDCamera hdCamera)
		{
			CameraSettings cameraSettings = CameraSettings.defaultCameraSettingsNonAlloc;
			cameraSettings.culling.cullingMask = hdCamera.camera.cullingMask;
			cameraSettings.culling.useOcclusionCulling = hdCamera.camera.useOcclusionCulling;
			cameraSettings.culling.sceneCullingMaskOverride = HDUtils.GetSceneCullingMaskFromCamera(hdCamera.camera);
			cameraSettings.frustum.aspect = hdCamera.camera.aspect;
			cameraSettings.frustum.farClipPlaneRaw = hdCamera.camera.farClipPlane;
			cameraSettings.frustum.nearClipPlaneRaw = hdCamera.camera.nearClipPlane;
			cameraSettings.frustum.fieldOfView = hdCamera.camera.fieldOfView;
			cameraSettings.frustum.mode = CameraSettings.Frustum.Mode.UseProjectionMatrixField;
			cameraSettings.frustum.projectionMatrix = hdCamera.camera.projectionMatrix;
			cameraSettings.invertFaceCulling = false;
			HDAdditionalCameraData hdadditionalCameraData;
			if (hdCamera.camera.TryGetComponent<HDAdditionalCameraData>(out hdadditionalCameraData))
			{
				cameraSettings.customRenderingSettings = hdadditionalCameraData.customRenderingSettings;
				cameraSettings.bufferClearing.backgroundColorHDR = hdadditionalCameraData.backgroundColorHDR;
				cameraSettings.bufferClearing.clearColorMode = hdadditionalCameraData.clearColorMode;
				cameraSettings.bufferClearing.clearDepth = hdadditionalCameraData.clearDepth;
				cameraSettings.flipYMode = hdadditionalCameraData.flipYMode;
				cameraSettings.renderingPathCustomFrameSettings = *hdadditionalCameraData.renderingPathCustomFrameSettings;
				cameraSettings.renderingPathCustomFrameSettingsOverrideMask = hdadditionalCameraData.renderingPathCustomFrameSettingsOverrideMask;
				cameraSettings.volumes = new CameraSettings.Volumes
				{
					anchorOverride = hdadditionalCameraData.volumeAnchorOverride,
					layerMask = hdadditionalCameraData.volumeLayerMask
				};
				cameraSettings.probeLayerMask = hdadditionalCameraData.probeLayerMask;
				cameraSettings.invertFaceCulling = hdadditionalCameraData.invertFaceCulling;
			}
			bool flag = hdCamera.camera.worldToCameraMatrix.determinant > 0f;
			bool flag2 = Mathf.Approximately(hdCamera.camera.projectionMatrix.m32, -1f);
			bool flag3 = Mathf.Approximately(hdCamera.camera.projectionMatrix.m00, 1f) && Mathf.Approximately(hdCamera.camera.projectionMatrix.m11, 1f);
			if (flag && flag2 && flag3)
			{
				cameraSettings.invertFaceCulling = true;
			}
			return cameraSettings;
		}

		// Token: 0x0400102C RID: 4140
		[Obsolete("Since 2019.3, use CameraSettings.defaultCameraSettingsNonAlloc instead.")]
		public static readonly CameraSettings @default = default(CameraSettings);

		// Token: 0x0400102D RID: 4141
		public static readonly CameraSettings defaultCameraSettingsNonAlloc = CameraSettings.NewDefault();

		// Token: 0x0400102E RID: 4142
		public bool customRenderingSettings;

		// Token: 0x0400102F RID: 4143
		public FrameSettings renderingPathCustomFrameSettings;

		// Token: 0x04001030 RID: 4144
		public FrameSettingsOverrideMask renderingPathCustomFrameSettingsOverrideMask;

		// Token: 0x04001031 RID: 4145
		public CameraSettings.BufferClearing bufferClearing;

		// Token: 0x04001032 RID: 4146
		public CameraSettings.Volumes volumes;

		// Token: 0x04001033 RID: 4147
		public CameraSettings.Frustum frustum;

		// Token: 0x04001034 RID: 4148
		public CameraSettings.Culling culling;

		// Token: 0x04001035 RID: 4149
		public bool invertFaceCulling;

		// Token: 0x04001036 RID: 4150
		public HDAdditionalCameraData.FlipYMode flipYMode;

		// Token: 0x04001037 RID: 4151
		public LayerMask probeLayerMask;

		// Token: 0x04001038 RID: 4152
		public FrameSettingsRenderType defaultFrameSettings;

		// Token: 0x04001039 RID: 4153
		internal float probeRangeCompressionFactor;

		// Token: 0x0400103A RID: 4154
		[SerializeField]
		[FormerlySerializedAs("renderingPath")]
		[Obsolete("For data migration")]
		internal int m_ObsoleteRenderingPath;

		// Token: 0x0400103B RID: 4155
		[SerializeField]
		[FormerlySerializedAs("frameSettings")]
		[Obsolete("For data migration")]
		internal ObsoleteFrameSettings m_ObsoleteFrameSettings;

		// Token: 0x02000296 RID: 662
		[Serializable]
		public struct BufferClearing
		{
			// Token: 0x06000CCB RID: 3275 RVA: 0x0005A444 File Offset: 0x00058644
			public static CameraSettings.BufferClearing NewDefault()
			{
				return new CameraSettings.BufferClearing
				{
					clearColorMode = HDAdditionalCameraData.ClearColorMode.Sky,
					backgroundColorHDR = new Color32(6, 18, 48, 0),
					clearDepth = true
				};
			}

			// Token: 0x040016F3 RID: 5875
			[Obsolete("Since 2019.3, use BufferClearing.NewDefault() instead.")]
			public static readonly CameraSettings.BufferClearing @default;

			// Token: 0x040016F4 RID: 5876
			public HDAdditionalCameraData.ClearColorMode clearColorMode;

			// Token: 0x040016F5 RID: 5877
			[ColorUsage(true, true)]
			public Color backgroundColorHDR;

			// Token: 0x040016F6 RID: 5878
			public bool clearDepth;
		}

		// Token: 0x02000297 RID: 663
		[Serializable]
		public struct Volumes
		{
			// Token: 0x06000CCD RID: 3277 RVA: 0x0005A484 File Offset: 0x00058684
			public static CameraSettings.Volumes NewDefault()
			{
				return new CameraSettings.Volumes
				{
					layerMask = -1,
					anchorOverride = null
				};
			}

			// Token: 0x040016F7 RID: 5879
			[Obsolete("Since 2019.3, use Volumes.NewDefault() instead.")]
			public static readonly CameraSettings.Volumes @default;

			// Token: 0x040016F8 RID: 5880
			public LayerMask layerMask;

			// Token: 0x040016F9 RID: 5881
			public Transform anchorOverride;
		}

		// Token: 0x02000298 RID: 664
		[Serializable]
		public struct Frustum
		{
			// Token: 0x06000CCF RID: 3279 RVA: 0x0005A4B0 File Offset: 0x000586B0
			public static CameraSettings.Frustum NewDefault()
			{
				return new CameraSettings.Frustum
				{
					mode = CameraSettings.Frustum.Mode.ComputeProjectionMatrix,
					aspect = 1f,
					farClipPlaneRaw = 1000f,
					nearClipPlaneRaw = 0.1f,
					fieldOfView = 90f,
					projectionMatrix = Matrix4x4.identity
				};
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0005A50A File Offset: 0x0005870A
			public float farClipPlane
			{
				get
				{
					return Mathf.Max(this.nearClipPlaneRaw + 0.0001f, this.farClipPlaneRaw);
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0005A523 File Offset: 0x00058723
			public float nearClipPlane
			{
				get
				{
					return Mathf.Max(1E-05f, this.nearClipPlaneRaw);
				}
			}

			// Token: 0x06000CD2 RID: 3282 RVA: 0x0005A535 File Offset: 0x00058735
			public Matrix4x4 ComputeProjectionMatrix()
			{
				return Matrix4x4.Perspective(HDUtils.ClampFOV(this.fieldOfView), this.aspect, this.nearClipPlane, this.farClipPlane);
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x0005A55C File Offset: 0x0005875C
			public Matrix4x4 GetUsedProjectionMatrix()
			{
				CameraSettings.Frustum.Mode mode = this.mode;
				if (mode == CameraSettings.Frustum.Mode.ComputeProjectionMatrix)
				{
					return this.ComputeProjectionMatrix();
				}
				if (mode != CameraSettings.Frustum.Mode.UseProjectionMatrixField)
				{
					throw new ArgumentException();
				}
				return this.projectionMatrix;
			}

			// Token: 0x040016FA RID: 5882
			public const float MinNearClipPlane = 1E-05f;

			// Token: 0x040016FB RID: 5883
			public const float MinFarClipPlane = 0.0001f;

			// Token: 0x040016FC RID: 5884
			[Obsolete("Since 2019.3, use Frustum.NewDefault() instead.")]
			public static readonly CameraSettings.Frustum @default;

			// Token: 0x040016FD RID: 5885
			public CameraSettings.Frustum.Mode mode;

			// Token: 0x040016FE RID: 5886
			public float aspect;

			// Token: 0x040016FF RID: 5887
			[FormerlySerializedAs("farClipPlane")]
			public float farClipPlaneRaw;

			// Token: 0x04001700 RID: 5888
			[FormerlySerializedAs("nearClipPlane")]
			public float nearClipPlaneRaw;

			// Token: 0x04001701 RID: 5889
			[Range(1f, 179f)]
			public float fieldOfView;

			// Token: 0x04001702 RID: 5890
			public Matrix4x4 projectionMatrix;

			// Token: 0x020002B3 RID: 691
			public enum Mode
			{
				// Token: 0x04001744 RID: 5956
				ComputeProjectionMatrix,
				// Token: 0x04001745 RID: 5957
				UseProjectionMatrixField
			}
		}

		// Token: 0x02000299 RID: 665
		[Serializable]
		public struct Culling
		{
			// Token: 0x06000CD5 RID: 3285 RVA: 0x0005A58C File Offset: 0x0005878C
			public static CameraSettings.Culling NewDefault()
			{
				return new CameraSettings.Culling
				{
					cullingMask = -1,
					useOcclusionCulling = true,
					sceneCullingMaskOverride = 0UL
				};
			}

			// Token: 0x04001703 RID: 5891
			[Obsolete("Since 2019.3, use Culling.NewDefault() instead.")]
			public static readonly CameraSettings.Culling @default;

			// Token: 0x04001704 RID: 5892
			public bool useOcclusionCulling;

			// Token: 0x04001705 RID: 5893
			public LayerMask cullingMask;

			// Token: 0x04001706 RID: 5894
			public ulong sceneCullingMaskOverride;
		}
	}
}
