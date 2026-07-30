using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000078 RID: 120
	[ExecuteAlways]
	public abstract class HDProbe : MonoBehaviour, IVersionable<HDProbe.Version>
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0002B510 File Offset: 0x00029710
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0002B518 File Offset: 0x00029718
		HDProbe.Version IVersionable<HDProbe.Version>.version
		{
			get
			{
				return this.m_HDProbeVersion;
			}
			set
			{
				this.m_HDProbeVersion = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0002B524 File Offset: 0x00029724
		internal bool requiresRealtimeUpdate
		{
			get
			{
				if (this.mode != ProbeSettings.Mode.Realtime)
				{
					return false;
				}
				switch (this.realtimeMode)
				{
				case ProbeSettings.RealtimeMode.EveryFrame:
					return true;
				case ProbeSettings.RealtimeMode.OnEnable:
					return !this.wasRenderedAfterOnEnable;
				case ProbeSettings.RealtimeMode.OnDemand:
					return !this.m_WasRenderedSinceLastOnDemandRequest;
				default:
					throw new ArgumentOutOfRangeException("realtimeMode");
				}
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0002B578 File Offset: 0x00029778
		internal bool HasValidRenderedData()
		{
			bool flag = this.texture != null;
			if (this.mode != ProbeSettings.Mode.Realtime)
			{
				return flag;
			}
			return this.lastRenderedFrame != int.MinValue && flag;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0002B5AF File Offset: 0x000297AF
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0002B5B7 File Offset: 0x000297B7
		public Texture bakedTexture
		{
			get
			{
				return this.m_BakedTexture;
			}
			set
			{
				this.m_BakedTexture = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0002B5C0 File Offset: 0x000297C0
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0002B5C8 File Offset: 0x000297C8
		public Texture customTexture
		{
			get
			{
				return this.m_CustomTexture;
			}
			set
			{
				this.m_CustomTexture = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0002B5D1 File Offset: 0x000297D1
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0002B5D9 File Offset: 0x000297D9
		public RenderTexture realtimeTexture
		{
			get
			{
				return this.m_RealtimeTexture;
			}
			set
			{
				this.m_RealtimeTexture = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0002B5E2 File Offset: 0x000297E2
		public Texture texture
		{
			get
			{
				return this.GetTexture(this.mode);
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0002B5F0 File Offset: 0x000297F0
		public Texture GetTexture(ProbeSettings.Mode targetMode)
		{
			switch (targetMode)
			{
			case ProbeSettings.Mode.Baked:
				return this.m_BakedTexture;
			case ProbeSettings.Mode.Realtime:
				return this.m_RealtimeTexture;
			case ProbeSettings.Mode.Custom:
				return this.m_CustomTexture;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0002B620 File Offset: 0x00029820
		public Texture SetTexture(ProbeSettings.Mode targetMode, Texture texture)
		{
			if (targetMode == ProbeSettings.Mode.Realtime && !(texture is RenderTexture))
			{
				throw new ArgumentException("'texture' must be a RenderTexture for the Realtime mode.");
			}
			switch (targetMode)
			{
			case ProbeSettings.Mode.Baked:
				this.m_BakedTexture = texture;
				return texture;
			case ProbeSettings.Mode.Realtime:
				return this.m_RealtimeTexture = (RenderTexture)texture;
			case ProbeSettings.Mode.Custom:
				this.m_CustomTexture = texture;
				return texture;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0002B683 File Offset: 0x00029883
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0002B68B File Offset: 0x0002988B
		public HDProbe.RenderData bakedRenderData
		{
			get
			{
				return this.m_BakedRenderData;
			}
			set
			{
				this.m_BakedRenderData = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0002B694 File Offset: 0x00029894
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0002B69C File Offset: 0x0002989C
		public HDProbe.RenderData customRenderData
		{
			get
			{
				return this.m_CustomRenderData;
			}
			set
			{
				this.m_CustomRenderData = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0002B6A5 File Offset: 0x000298A5
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0002B6AD File Offset: 0x000298AD
		public HDProbe.RenderData realtimeRenderData
		{
			get
			{
				return this.m_RealtimeRenderData;
			}
			set
			{
				this.m_RealtimeRenderData = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0002B6B6 File Offset: 0x000298B6
		public HDProbe.RenderData renderData
		{
			get
			{
				return this.GetRenderData(this.mode);
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0002B6C4 File Offset: 0x000298C4
		public HDProbe.RenderData GetRenderData(ProbeSettings.Mode targetMode)
		{
			switch (targetMode)
			{
			case ProbeSettings.Mode.Baked:
				return this.bakedRenderData;
			case ProbeSettings.Mode.Realtime:
				return this.realtimeRenderData;
			case ProbeSettings.Mode.Custom:
				return this.customRenderData;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0002B6F4 File Offset: 0x000298F4
		public void SetRenderData(ProbeSettings.Mode targetMode, HDProbe.RenderData renderData)
		{
			switch (targetMode)
			{
			case ProbeSettings.Mode.Baked:
				this.bakedRenderData = renderData;
				return;
			case ProbeSettings.Mode.Realtime:
				this.realtimeRenderData = renderData;
				return;
			case ProbeSettings.Mode.Custom:
				this.customRenderData = renderData;
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0002B727 File Offset: 0x00029927
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0002B734 File Offset: 0x00029934
		public ProbeSettings.ProbeType type
		{
			get
			{
				return this.m_ProbeSettings.type;
			}
			protected set
			{
				this.m_ProbeSettings.type = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0002B742 File Offset: 0x00029942
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0002B74F File Offset: 0x0002994F
		public ProbeSettings.Mode mode
		{
			get
			{
				return this.m_ProbeSettings.mode;
			}
			set
			{
				this.m_ProbeSettings.mode = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0002B75D File Offset: 0x0002995D
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0002B76A File Offset: 0x0002996A
		public ProbeSettings.RealtimeMode realtimeMode
		{
			get
			{
				return this.m_ProbeSettings.realtimeMode;
			}
			set
			{
				this.m_ProbeSettings.realtimeMode = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0002B778 File Offset: 0x00029978
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0002B785 File Offset: 0x00029985
		public PlanarReflectionAtlasResolution resolution
		{
			get
			{
				return this.m_ProbeSettings.resolution;
			}
			set
			{
				this.m_ProbeSettings.resolution = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0002B793 File Offset: 0x00029993
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0002B7A5 File Offset: 0x000299A5
		public LightLayerEnum lightLayers
		{
			get
			{
				return this.m_ProbeSettings.lighting.lightLayer;
			}
			set
			{
				this.m_ProbeSettings.lighting.lightLayer = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0002B7B8 File Offset: 0x000299B8
		public uint lightLayersAsUInt
		{
			get
			{
				if (this.lightLayers >= LightLayerEnum.Nothing)
				{
					return (uint)this.lightLayers;
				}
				return 255U;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0002B7CF File Offset: 0x000299CF
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0002B7E1 File Offset: 0x000299E1
		public float multiplier
		{
			get
			{
				return this.m_ProbeSettings.lighting.multiplier;
			}
			set
			{
				this.m_ProbeSettings.lighting.multiplier = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0002B7F4 File Offset: 0x000299F4
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0002B806 File Offset: 0x00029A06
		public float weight
		{
			get
			{
				return this.m_ProbeSettings.lighting.weight;
			}
			set
			{
				this.m_ProbeSettings.lighting.weight = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0002B819 File Offset: 0x00029A19
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0002B82B File Offset: 0x00029A2B
		public float fadeDistance
		{
			get
			{
				return this.m_ProbeSettings.lighting.fadeDistance;
			}
			set
			{
				this.m_ProbeSettings.lighting.fadeDistance = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0002B83E File Offset: 0x00029A3E
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0002B850 File Offset: 0x00029A50
		public float rangeCompressionFactor
		{
			get
			{
				return this.m_ProbeSettings.lighting.rangeCompressionFactor;
			}
			set
			{
				this.m_ProbeSettings.lighting.rangeCompressionFactor = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0002B863 File Offset: 0x00029A63
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x0002B86B File Offset: 0x00029A6B
		public ReflectionProxyVolumeComponent proxyVolume
		{
			get
			{
				return this.m_ProxyVolume;
			}
			set
			{
				this.m_ProxyVolume = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0002B874 File Offset: 0x00029A74
		public bool useInfluenceVolumeAsProxyVolume
		{
			get
			{
				return this.m_ProbeSettings.proxySettings.useInfluenceVolumeAsProxyVolume;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0002B888 File Offset: 0x00029A88
		public bool isProjectionInfinite
		{
			get
			{
				return (this.m_ProxyVolume != null && this.m_ProxyVolume.proxyVolume.shape == ProxyShape.Infinite) || (this.m_ProxyVolume == null && !this.m_ProbeSettings.proxySettings.useInfluenceVolumeAsProxyVolume);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0002B8DC File Offset: 0x00029ADC
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x0002B90B File Offset: 0x00029B0B
		public InfluenceVolume influenceVolume
		{
			get
			{
				InfluenceVolume influenceVolume;
				if ((influenceVolume = this.m_ProbeSettings.influence) == null)
				{
					influenceVolume = (this.m_ProbeSettings.influence = new InfluenceVolume());
				}
				return influenceVolume;
			}
			private set
			{
				this.m_ProbeSettings.influence = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0002B919 File Offset: 0x00029B19
		public ref FrameSettings frameSettings
		{
			get
			{
				return ref this.m_ProbeSettings.cameraSettings.renderingPathCustomFrameSettings;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0002B92B File Offset: 0x00029B2B
		public ref FrameSettingsOverrideMask frameSettingsOverrideMask
		{
			get
			{
				return ref this.m_ProbeSettings.cameraSettings.renderingPathCustomFrameSettingsOverrideMask;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0002B93D File Offset: 0x00029B3D
		public Vector3 proxyExtents
		{
			get
			{
				if (!(this.proxyVolume != null))
				{
					return this.influenceExtents;
				}
				return this.proxyVolume.proxyVolume.extents;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0002B964 File Offset: 0x00029B64
		public BoundingSphere boundingSphere
		{
			get
			{
				return this.influenceVolume.GetBoundingSphereAt(base.transform.position);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0002B97C File Offset: 0x00029B7C
		public Bounds bounds
		{
			get
			{
				return this.influenceVolume.GetBoundsAt(base.transform.position);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0002B994 File Offset: 0x00029B94
		public ref ProbeSettings settingsRaw
		{
			get
			{
				return ref this.m_ProbeSettings;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0002B99C File Offset: 0x00029B9C
		public ProbeSettings settings
		{
			get
			{
				ProbeSettings probeSettings = this.m_ProbeSettings;
				ReflectionProxyVolumeComponent proxyVolume = this.m_ProxyVolume;
				probeSettings.proxy = ((proxyVolume != null) ? proxyVolume.proxyVolume : null);
				probeSettings.influence = probeSettings.influence ?? new InfluenceVolume();
				return probeSettings;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0002B9E0 File Offset: 0x00029BE0
		internal Matrix4x4 influenceToWorld
		{
			get
			{
				return Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0002BA02 File Offset: 0x00029C02
		internal Vector3 influenceExtents
		{
			get
			{
				return this.influenceVolume.extents;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0002BA10 File Offset: 0x00029C10
		internal Matrix4x4 proxyToWorld
		{
			get
			{
				if (!(this.proxyVolume != null))
				{
					return this.influenceToWorld;
				}
				return Matrix4x4.TRS(this.proxyVolume.transform.position, this.proxyVolume.transform.rotation, Vector3.one);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0002BA5C File Offset: 0x00029C5C
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0002BA64 File Offset: 0x00029C64
		internal bool wasRenderedAfterOnEnable { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0002BA6D File Offset: 0x00029C6D
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0002BA75 File Offset: 0x00029C75
		internal int lastRenderedFrame { get; private set; } = int.MinValue;

		// Token: 0x060004DC RID: 1244 RVA: 0x0002BA7E File Offset: 0x00029C7E
		internal void SetIsRendered(int frame)
		{
			this.m_WasRenderedSinceLastOnDemandRequest = true;
			this.wasRenderedAfterOnEnable = true;
			this.lastRenderedFrame = frame;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00002646 File Offset: 0x00000846
		public virtual void PrepareCulling()
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0002BA95 File Offset: 0x00029C95
		public void RequestRenderNextUpdate()
		{
			this.m_WasRenderedSinceLastOnDemandRequest = false;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0002BAA0 File Offset: 0x00029CA0
		private void UpdateProbeName()
		{
			if (this.settings.type == ProbeSettings.ProbeType.PlanarProbe)
			{
				for (int i = 0; i < 6; i++)
				{
					this.probeName[i] = string.Format("Reflection Probe RenderCamera ({0}: {1})", base.name, (CubemapFace)i);
				}
				return;
			}
			this.probeName[0] = "Planar Probe RenderCamera (" + base.name + ")";
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0002BB03 File Offset: 0x00029D03
		private void OnEnable()
		{
			this.wasRenderedAfterOnEnable = false;
			this.PrepareCulling();
			HDProbeSystem.RegisterProbe(this);
			this.UpdateProbeName();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0002BB1E File Offset: 0x00029D1E
		private void OnDisable()
		{
			HDProbeSystem.UnregisterProbe(this);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0002BB26 File Offset: 0x00029D26
		private void OnValidate()
		{
			HDProbeSystem.UnregisterProbe(this);
			if (base.isActiveAndEnabled)
			{
				this.PrepareCulling();
				HDProbeSystem.RegisterProbe(this);
			}
		}

		// Token: 0x0400050A RID: 1290
		protected static readonly MigrationDescription<HDProbe.Version, HDProbe> k_Migration = MigrationDescription.New<HDProbe.Version, HDProbe>(new MigrationStep<HDProbe.Version, HDProbe>[]
		{
			MigrationStep.New<HDProbe.Version, HDProbe>(HDProbe.Version.ProbeSettings, delegate(HDProbe p)
			{
				p.m_ProbeSettings.proxySettings.useInfluenceVolumeAsProxyVolume = !p.m_ObsoleteInfiniteProjection;
				p.m_ProbeSettings.influence = new InfluenceVolume();
				if (p.m_ObsoleteInfluenceVolume != null)
				{
					p.m_ObsoleteInfluenceVolume.CopyTo(p.m_ProbeSettings.influence);
				}
				p.m_ProbeSettings.cameraSettings.m_ObsoleteFrameSettings = p.m_ObsoleteFrameSettings;
				p.m_ProbeSettings.lighting.multiplier = p.m_ObsoleteMultiplier;
				p.m_ProbeSettings.lighting.weight = p.m_ObsoleteWeight;
				p.m_ProbeSettings.lighting.lightLayer = p.m_ObsoleteLightLayers;
				p.m_ProbeSettings.mode = p.m_ObsoleteMode;
				p.m_ProbeSettings.cameraSettings.bufferClearing.clearColorMode = p.m_ObsoleteCaptureSettings.clearColorMode;
				p.m_ProbeSettings.cameraSettings.bufferClearing.backgroundColorHDR = p.m_ObsoleteCaptureSettings.backgroundColorHDR;
				p.m_ProbeSettings.cameraSettings.bufferClearing.clearDepth = p.m_ObsoleteCaptureSettings.clearDepth;
				p.m_ProbeSettings.cameraSettings.culling.cullingMask = p.m_ObsoleteCaptureSettings.cullingMask;
				p.m_ProbeSettings.cameraSettings.culling.useOcclusionCulling = p.m_ObsoleteCaptureSettings.useOcclusionCulling;
				p.m_ProbeSettings.cameraSettings.frustum.nearClipPlaneRaw = p.m_ObsoleteCaptureSettings.nearClipPlane;
				p.m_ProbeSettings.cameraSettings.frustum.farClipPlaneRaw = p.m_ObsoleteCaptureSettings.farClipPlane;
				p.m_ProbeSettings.cameraSettings.volumes.layerMask = p.m_ObsoleteCaptureSettings.volumeLayerMask;
				p.m_ProbeSettings.cameraSettings.volumes.anchorOverride = p.m_ObsoleteCaptureSettings.volumeAnchorOverride;
				p.m_ProbeSettings.cameraSettings.frustum.fieldOfView = p.m_ObsoleteCaptureSettings.fieldOfView;
				p.m_ProbeSettings.cameraSettings.m_ObsoleteRenderingPath = p.m_ObsoleteCaptureSettings.renderingPath;
			}),
			MigrationStep.New<HDProbe.Version, HDProbe>(HDProbe.Version.SeparatePassThrough, delegate(HDProbe p)
			{
				p.m_ProbeSettings.cameraSettings.customRenderingSettings = p.m_ProbeSettings.cameraSettings.m_ObsoleteRenderingPath == 1;
			}),
			MigrationStep.New<HDProbe.Version, HDProbe>(HDProbe.Version.UpgradeFrameSettingsToStruct, delegate(HDProbe data)
			{
				if (data.m_ObsoleteFrameSettings != null)
				{
					FrameSettings.MigrateFromClassVersion(ref data.m_ProbeSettings.cameraSettings.m_ObsoleteFrameSettings, ref data.m_ProbeSettings.cameraSettings.renderingPathCustomFrameSettings, ref data.m_ProbeSettings.cameraSettings.renderingPathCustomFrameSettingsOverrideMask);
				}
			}),
			MigrationStep.New<HDProbe.Version, HDProbe>(HDProbe.Version.AddReflectionFrameSetting, delegate(HDProbe data)
			{
				FrameSettings.MigrateToNoReflectionSettings(ref data.m_ProbeSettings.cameraSettings.renderingPathCustomFrameSettings);
			}),
			MigrationStep.New<HDProbe.Version, HDProbe>(HDProbe.Version.AddFrameSettingDirectSpecularLighting, delegate(HDProbe data)
			{
				FrameSettings.MigrateToNoDirectSpecularLighting(ref data.m_ProbeSettings.cameraSettings.renderingPathCustomFrameSettings);
			})
		});

		// Token: 0x0400050B RID: 1291
		[SerializeField]
		private HDProbe.Version m_HDProbeVersion;

		// Token: 0x0400050C RID: 1292
		[SerializeField]
		[FormerlySerializedAs("m_InfiniteProjection")]
		[Obsolete("For Data Migration")]
		protected bool m_ObsoleteInfiniteProjection = true;

		// Token: 0x0400050D RID: 1293
		[SerializeField]
		[FormerlySerializedAs("m_InfluenceVolume")]
		[Obsolete("For Data Migration")]
		protected InfluenceVolume m_ObsoleteInfluenceVolume;

		// Token: 0x0400050E RID: 1294
		[SerializeField]
		[FormerlySerializedAs("m_FrameSettings")]
		[Obsolete("For Data Migration")]
		private ObsoleteFrameSettings m_ObsoleteFrameSettings;

		// Token: 0x0400050F RID: 1295
		[SerializeField]
		[FormerlySerializedAs("m_Multiplier")]
		[FormerlySerializedAs("dimmer")]
		[FormerlySerializedAs("m_Dimmer")]
		[FormerlySerializedAs("multiplier")]
		[Obsolete("For Data Migration")]
		protected float m_ObsoleteMultiplier = 1f;

		// Token: 0x04000510 RID: 1296
		[SerializeField]
		[FormerlySerializedAs("m_Weight")]
		[FormerlySerializedAs("weight")]
		[Obsolete("For Data Migration")]
		[Range(0f, 1f)]
		protected float m_ObsoleteWeight = 1f;

		// Token: 0x04000511 RID: 1297
		[SerializeField]
		[FormerlySerializedAs("m_Mode")]
		[Obsolete("For Data Migration")]
		protected ProbeSettings.Mode m_ObsoleteMode;

		// Token: 0x04000512 RID: 1298
		[SerializeField]
		[FormerlySerializedAs("lightLayers")]
		[Obsolete("For Data Migration")]
		private LightLayerEnum m_ObsoleteLightLayers = LightLayerEnum.LightLayerDefault;

		// Token: 0x04000513 RID: 1299
		[SerializeField]
		[FormerlySerializedAs("m_CaptureSettings")]
		[Obsolete("For Data Migration")]
		internal ObsoleteCaptureSettings m_ObsoleteCaptureSettings;

		// Token: 0x04000514 RID: 1300
		[SerializeField]
		protected ProbeSettings m_ProbeSettings = ProbeSettings.NewDefault();

		// Token: 0x04000515 RID: 1301
		[SerializeField]
		private ProbeSettingsOverride m_ProbeSettingsOverride;

		// Token: 0x04000516 RID: 1302
		[SerializeField]
		private ReflectionProxyVolumeComponent m_ProxyVolume;

		// Token: 0x04000517 RID: 1303
		[SerializeField]
		private Texture m_BakedTexture;

		// Token: 0x04000518 RID: 1304
		[SerializeField]
		private Texture m_CustomTexture;

		// Token: 0x04000519 RID: 1305
		[SerializeField]
		private HDProbe.RenderData m_BakedRenderData;

		// Token: 0x0400051A RID: 1306
		[SerializeField]
		private HDProbe.RenderData m_CustomRenderData;

		// Token: 0x0400051B RID: 1307
		[SerializeField]
		private uint m_EditorOnlyData;

		// Token: 0x0400051C RID: 1308
		private RenderTexture m_RealtimeTexture;

		// Token: 0x0400051D RID: 1309
		private HDProbe.RenderData m_RealtimeRenderData;

		// Token: 0x0400051E RID: 1310
		private bool m_WasRenderedSinceLastOnDemandRequest = true;

		// Token: 0x0400051F RID: 1311
		internal string[] probeName = new string[6];

		// Token: 0x02000202 RID: 514
		protected enum Version
		{
			// Token: 0x04001370 RID: 4976
			Initial,
			// Token: 0x04001371 RID: 4977
			ProbeSettings,
			// Token: 0x04001372 RID: 4978
			SeparatePassThrough,
			// Token: 0x04001373 RID: 4979
			UpgradeFrameSettingsToStruct,
			// Token: 0x04001374 RID: 4980
			AddFrameSettingSpecularLighting,
			// Token: 0x04001375 RID: 4981
			AddReflectionFrameSetting,
			// Token: 0x04001376 RID: 4982
			AddFrameSettingDirectSpecularLighting
		}

		// Token: 0x02000203 RID: 515
		[Serializable]
		public struct RenderData
		{
			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00056F24 File Offset: 0x00055124
			public Matrix4x4 worldToCameraRHS
			{
				get
				{
					return this.m_WorldToCameraRHS;
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00056F2C File Offset: 0x0005512C
			public Matrix4x4 projectionMatrix
			{
				get
				{
					return this.m_ProjectionMatrix;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00056F34 File Offset: 0x00055134
			public Vector3 capturePosition
			{
				get
				{
					return this.m_CapturePosition;
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00056F3C File Offset: 0x0005513C
			public Quaternion captureRotation
			{
				get
				{
					return this.m_CaptureRotation;
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00056F44 File Offset: 0x00055144
			public float fieldOfView
			{
				get
				{
					return this.m_FieldOfView;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00056F4C File Offset: 0x0005514C
			public float aspect
			{
				get
				{
					return this.m_Aspect;
				}
			}

			// Token: 0x06000BE9 RID: 3049 RVA: 0x00056F54 File Offset: 0x00055154
			public RenderData(CameraSettings camera, CameraPositionSettings position)
			{
				this = new HDProbe.RenderData(position.GetUsedWorldToCameraMatrix(), camera.frustum.GetUsedProjectionMatrix(), position.position, position.rotation, camera.frustum.fieldOfView, camera.frustum.aspect);
			}

			// Token: 0x06000BEA RID: 3050 RVA: 0x00056F91 File Offset: 0x00055191
			public RenderData(Matrix4x4 worldToCameraRHS, Matrix4x4 projectionMatrix, Vector3 capturePosition, Quaternion captureRotation, float fov, float aspect)
			{
				this.m_WorldToCameraRHS = worldToCameraRHS;
				this.m_ProjectionMatrix = projectionMatrix;
				this.m_CapturePosition = capturePosition;
				this.m_CaptureRotation = captureRotation;
				this.m_FieldOfView = fov;
				this.m_Aspect = aspect;
			}

			// Token: 0x04001377 RID: 4983
			[SerializeField]
			[FormerlySerializedAs("worldToCameraRHS")]
			private Matrix4x4 m_WorldToCameraRHS;

			// Token: 0x04001378 RID: 4984
			[SerializeField]
			[FormerlySerializedAs("projectionMatrix")]
			private Matrix4x4 m_ProjectionMatrix;

			// Token: 0x04001379 RID: 4985
			[SerializeField]
			[FormerlySerializedAs("capturePosition")]
			private Vector3 m_CapturePosition;

			// Token: 0x0400137A RID: 4986
			[SerializeField]
			private Quaternion m_CaptureRotation;

			// Token: 0x0400137B RID: 4987
			[SerializeField]
			private float m_FieldOfView;

			// Token: 0x0400137C RID: 4988
			[SerializeField]
			private float m_Aspect;
		}
	}
}
