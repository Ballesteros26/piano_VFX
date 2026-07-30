using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200017D RID: 381
	[Serializable]
	public struct ProbeSettings
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x00053C6C File Offset: 0x00051E6C
		public static ProbeSettings NewDefault()
		{
			return new ProbeSettings
			{
				type = ProbeSettings.ProbeType.ReflectionProbe,
				realtimeMode = ProbeSettings.RealtimeMode.EveryFrame,
				mode = ProbeSettings.Mode.Baked,
				cameraSettings = CameraSettings.NewDefault(),
				influence = null,
				lighting = ProbeSettings.Lighting.NewDefault(),
				proxy = null,
				proxySettings = ProbeSettings.ProxySettings.NewDefault(),
				frustum = ProbeSettings.Frustum.NewDefault(),
				resolution = PlanarReflectionAtlasResolution.PlanarReflectionResolution512
			};
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00053CE8 File Offset: 0x00051EE8
		public Hash128 ComputeHash()
		{
			Hash128 hash = default(Hash128);
			Hash128 hash2 = default(Hash128);
			HashUtilities.ComputeHash128<ProbeSettings.ProbeType>(ref this.type, ref hash);
			HashUtilities.ComputeHash128<ProbeSettings.Mode>(ref this.mode, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<ProbeSettings.Lighting>(ref this.lighting, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<ProbeSettings.ProxySettings>(ref this.proxySettings, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			HashUtilities.ComputeHash128<CameraSettings>(ref this.cameraSettings, ref hash2);
			HashUtilities.AppendHash(ref hash2, ref hash);
			if (this.influence != null)
			{
				hash2 = this.influence.ComputeHash();
				HashUtilities.AppendHash(ref hash2, ref hash);
			}
			if (this.proxy != null)
			{
				hash2 = this.proxy.ComputeHash();
				HashUtilities.AppendHash(ref hash2, ref hash);
			}
			return hash;
		}

		// Token: 0x0400105A RID: 4186
		[Obsolete("Since 2019.3, use ProbeSettings.NewDefault() instead.")]
		public static ProbeSettings @default;

		// Token: 0x0400105B RID: 4187
		public ProbeSettings.Frustum frustum;

		// Token: 0x0400105C RID: 4188
		public ProbeSettings.ProbeType type;

		// Token: 0x0400105D RID: 4189
		public ProbeSettings.Mode mode;

		// Token: 0x0400105E RID: 4190
		public ProbeSettings.RealtimeMode realtimeMode;

		// Token: 0x0400105F RID: 4191
		public ProbeSettings.Lighting lighting;

		// Token: 0x04001060 RID: 4192
		public InfluenceVolume influence;

		// Token: 0x04001061 RID: 4193
		public ProxyVolume proxy;

		// Token: 0x04001062 RID: 4194
		public ProbeSettings.ProxySettings proxySettings;

		// Token: 0x04001063 RID: 4195
		public PlanarReflectionAtlasResolution resolution;

		// Token: 0x04001064 RID: 4196
		[FormerlySerializedAs("camera")]
		public CameraSettings cameraSettings;

		// Token: 0x0200029B RID: 667
		public enum ProbeType
		{
			// Token: 0x0400170A RID: 5898
			ReflectionProbe,
			// Token: 0x0400170B RID: 5899
			PlanarProbe
		}

		// Token: 0x0200029C RID: 668
		public enum Mode
		{
			// Token: 0x0400170D RID: 5901
			Baked,
			// Token: 0x0400170E RID: 5902
			Realtime,
			// Token: 0x0400170F RID: 5903
			Custom
		}

		// Token: 0x0200029D RID: 669
		public enum RealtimeMode
		{
			// Token: 0x04001711 RID: 5905
			EveryFrame,
			// Token: 0x04001712 RID: 5906
			OnEnable,
			// Token: 0x04001713 RID: 5907
			OnDemand
		}

		// Token: 0x0200029E RID: 670
		[Serializable]
		public struct Lighting
		{
			// Token: 0x06000CD7 RID: 3287 RVA: 0x0005A5C0 File Offset: 0x000587C0
			public static ProbeSettings.Lighting NewDefault()
			{
				return new ProbeSettings.Lighting
				{
					multiplier = 1f,
					weight = 1f,
					lightLayer = LightLayerEnum.LightLayerDefault,
					fadeDistance = 10000f,
					rangeCompressionFactor = 1f
				};
			}

			// Token: 0x04001714 RID: 5908
			[Obsolete("Since 2019.3, use Lighting.NewDefault() instead.")]
			public static readonly ProbeSettings.Lighting @default;

			// Token: 0x04001715 RID: 5909
			public float multiplier;

			// Token: 0x04001716 RID: 5910
			[Range(0f, 1f)]
			public float weight;

			// Token: 0x04001717 RID: 5911
			public LightLayerEnum lightLayer;

			// Token: 0x04001718 RID: 5912
			public float fadeDistance;

			// Token: 0x04001719 RID: 5913
			public float rangeCompressionFactor;
		}

		// Token: 0x0200029F RID: 671
		[Serializable]
		public struct ProxySettings
		{
			// Token: 0x06000CD9 RID: 3289 RVA: 0x0005A610 File Offset: 0x00058810
			public static ProbeSettings.ProxySettings NewDefault()
			{
				return new ProbeSettings.ProxySettings
				{
					capturePositionProxySpace = Vector3.zero,
					captureRotationProxySpace = Quaternion.identity,
					useInfluenceVolumeAsProxyVolume = false
				};
			}

			// Token: 0x0400171A RID: 5914
			[Obsolete("Since 2019.3, use ProxySettings.NewDefault() instead.")]
			public static readonly ProbeSettings.ProxySettings @default;

			// Token: 0x0400171B RID: 5915
			public bool useInfluenceVolumeAsProxyVolume;

			// Token: 0x0400171C RID: 5916
			public Vector3 capturePositionProxySpace;

			// Token: 0x0400171D RID: 5917
			public Quaternion captureRotationProxySpace;

			// Token: 0x0400171E RID: 5918
			public Vector3 mirrorPositionProxySpace;

			// Token: 0x0400171F RID: 5919
			public Quaternion mirrorRotationProxySpace;
		}

		// Token: 0x020002A0 RID: 672
		[Serializable]
		public struct Frustum
		{
			// Token: 0x06000CDB RID: 3291 RVA: 0x0005A648 File Offset: 0x00058848
			public static ProbeSettings.Frustum NewDefault()
			{
				return new ProbeSettings.Frustum
				{
					fieldOfViewMode = ProbeSettings.Frustum.FOVMode.Viewer,
					fixedValue = 90f,
					automaticScale = 1f,
					viewerScale = 1f
				};
			}

			// Token: 0x04001720 RID: 5920
			[Obsolete("Since 2019.3, use Frustum.NewDefault() instead.")]
			public static readonly ProbeSettings.Frustum @default;

			// Token: 0x04001721 RID: 5921
			public ProbeSettings.Frustum.FOVMode fieldOfViewMode;

			// Token: 0x04001722 RID: 5922
			[Range(0f, 179f)]
			public float fixedValue;

			// Token: 0x04001723 RID: 5923
			[Min(0f)]
			public float automaticScale;

			// Token: 0x04001724 RID: 5924
			[Min(0f)]
			public float viewerScale;

			// Token: 0x020002B4 RID: 692
			public enum FOVMode
			{
				// Token: 0x04001747 RID: 5959
				Fixed,
				// Token: 0x04001748 RID: 5960
				Viewer,
				// Token: 0x04001749 RID: 5961
				Automatic
			}
		}
	}
}
