using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200002A RID: 42
	public class DebugDisplaySettings : IDebugData
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00005239 File Offset: 0x00003439
		public DebugDisplaySettings.DebugData data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00005241 File Offset: 0x00003441
		public static GUIContent[] renderingFullScreenDebugStrings
		{
			get
			{
				return DebugDisplaySettings.s_RenderingFullScreenDebugStrings;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00005248 File Offset: 0x00003448
		public static int[] renderingFullScreenDebugValues
		{
			get
			{
				return DebugDisplaySettings.s_RenderingFullScreenDebugValues;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005250 File Offset: 0x00003450
		internal DebugDisplaySettings()
		{
			this.FillFullScreenDebugEnum(ref DebugDisplaySettings.s_LightingFullScreenDebugStrings, ref DebugDisplaySettings.s_LightingFullScreenDebugValues, FullScreenDebugMode.MinLightingFullScreenDebug, FullScreenDebugMode.MaxLightingFullScreenDebug);
			this.FillFullScreenDebugEnum(ref DebugDisplaySettings.s_RenderingFullScreenDebugStrings, ref DebugDisplaySettings.s_RenderingFullScreenDebugValues, FullScreenDebugMode.MinRenderingFullScreenDebug, FullScreenDebugMode.MaxRenderingFullScreenDebug);
			this.FillFullScreenDebugEnum(ref DebugDisplaySettings.s_MaterialFullScreenDebugStrings, ref DebugDisplaySettings.s_MaterialFullScreenDebugValues, FullScreenDebugMode.MinMaterialFullScreenDebug, FullScreenDebugMode.MaxMaterialFullScreenDebug);
			DebugDisplaySettings.s_MaterialFullScreenDebugStrings[1] = new GUIContent("Diffuse Color");
			DebugDisplaySettings.s_MaterialFullScreenDebugStrings[2] = new GUIContent("Metal or SpecularColor");
			DebugDisplaySettings.s_MsaaSamplesDebugStrings = (from t in Enum.GetNames(typeof(MSAASamples))
				select new GUIContent(t)).ToArray<GUIContent>();
			DebugDisplaySettings.s_MsaaSamplesDebugValues = (int[])Enum.GetValues(typeof(MSAASamples));
			this.m_Data = new DebugDisplaySettings.DebugData();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000532C File Offset: 0x0000352C
		Action IDebugData.GetReset()
		{
			return delegate
			{
				this.m_Data = new DebugDisplaySettings.DebugData();
			};
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000533A File Offset: 0x0000353A
		internal float[] GetDebugMaterialIndexes()
		{
			return this.data.materialDebugSettings.GetDebugMaterialIndexes();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000534C File Offset: 0x0000354C
		public DebugLightFilterMode GetDebugLightFilterMode()
		{
			return this.data.lightingDebugSettings.debugLightFilterMode;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000535E File Offset: 0x0000355E
		public DebugLightingMode GetDebugLightingMode()
		{
			return this.data.lightingDebugSettings.debugLightingMode;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005370 File Offset: 0x00003570
		public ShadowMapDebugMode GetDebugShadowMapMode()
		{
			return this.data.lightingDebugSettings.shadowDebugMode;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005382 File Offset: 0x00003582
		public DebugMipMapMode GetDebugMipMapMode()
		{
			return this.data.mipMapDebugSettings.debugMipMapMode;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005394 File Offset: 0x00003594
		public DebugMipMapModeTerrainTexture GetDebugMipMapModeTerrainTexture()
		{
			return this.data.mipMapDebugSettings.terrainTexture;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000053A6 File Offset: 0x000035A6
		public ColorPickerDebugMode GetDebugColorPickerMode()
		{
			return this.data.colorPickerDebugSettings.colorPickerMode;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000053B8 File Offset: 0x000035B8
		public bool IsCameraFreezeEnabled()
		{
			return this.data.debugCameraToFreeze != 0;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000053C8 File Offset: 0x000035C8
		public bool IsCameraFrozen(Camera camera)
		{
			return this.IsCameraFreezeEnabled() && camera.name.Equals(DebugDisplaySettings.s_CameraNamesStrings[this.data.debugCameraToFreeze].text);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000053F5 File Offset: 0x000035F5
		public bool IsDebugDisplayEnabled()
		{
			return this.data.materialDebugSettings.IsDebugDisplayEnabled() || this.data.lightingDebugSettings.IsDebugDisplayEnabled() || this.data.mipMapDebugSettings.IsDebugDisplayEnabled() || this.IsDebugFullScreenEnabled();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005435 File Offset: 0x00003635
		public bool IsDebugMaterialDisplayEnabled()
		{
			return this.data.materialDebugSettings.IsDebugDisplayEnabled();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005447 File Offset: 0x00003647
		public bool IsDebugFullScreenEnabled()
		{
			return this.data.fullScreenDebugMode > FullScreenDebugMode.None;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005457 File Offset: 0x00003657
		public bool IsMaterialValidationEnabled()
		{
			return this.data.fullScreenDebugMode == FullScreenDebugMode.ValidateDiffuseColor || this.data.fullScreenDebugMode == FullScreenDebugMode.ValidateSpecularColor;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005479 File Offset: 0x00003679
		public bool IsDebugMipMapDisplayEnabled()
		{
			return this.data.mipMapDebugSettings.IsDebugDisplayEnabled();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000548B File Offset: 0x0000368B
		public bool IsMatcapViewEnabled(HDCamera camera)
		{
			return CoreUtils.IsSceneLightingDisabled(camera.camera) || this.GetDebugLightingMode() == DebugLightingMode.MatcapView;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000054A5 File Offset: 0x000036A5
		private void DisableNonMaterialDebugSettings()
		{
			this.data.fullScreenDebugMode = FullScreenDebugMode.None;
			this.data.lightingDebugSettings.debugLightingMode = DebugLightingMode.None;
			this.data.mipMapDebugSettings.debugMipMapMode = DebugMipMapMode.None;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000054D5 File Offset: 0x000036D5
		public void SetDebugViewCommonMaterialProperty(MaterialSharedProperty value)
		{
			if (value != MaterialSharedProperty.None)
			{
				this.DisableNonMaterialDebugSettings();
			}
			this.data.materialDebugSettings.SetDebugViewCommonMaterialProperty(value);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000054F1 File Offset: 0x000036F1
		public void SetDebugViewMaterial(int value)
		{
			if (value != 0)
			{
				this.DisableNonMaterialDebugSettings();
			}
			this.data.materialDebugSettings.SetDebugViewMaterial(value);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000550D File Offset: 0x0000370D
		public void SetDebugViewEngine(int value)
		{
			if (value != 0)
			{
				this.DisableNonMaterialDebugSettings();
			}
			this.data.materialDebugSettings.SetDebugViewEngine(value);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005529 File Offset: 0x00003729
		public void SetDebugViewVarying(DebugViewVarying value)
		{
			if (value != DebugViewVarying.None)
			{
				this.DisableNonMaterialDebugSettings();
			}
			this.data.materialDebugSettings.SetDebugViewVarying(value);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005545 File Offset: 0x00003745
		public void SetDebugViewProperties(DebugViewProperties value)
		{
			if (value != DebugViewProperties.None)
			{
				this.DisableNonMaterialDebugSettings();
			}
			this.data.materialDebugSettings.SetDebugViewProperties(value);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005561 File Offset: 0x00003761
		public void SetDebugViewGBuffer(int value)
		{
			if (value != 0)
			{
				this.DisableNonMaterialDebugSettings();
			}
			this.data.materialDebugSettings.SetDebugViewGBuffer(value);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005580 File Offset: 0x00003780
		public void SetFullScreenDebugMode(FullScreenDebugMode value)
		{
			if (this.data.lightingDebugSettings.shadowDebugMode == ShadowMapDebugMode.SingleShadow)
			{
				value = FullScreenDebugMode.None;
			}
			if (value != FullScreenDebugMode.None)
			{
				this.data.lightingDebugSettings.debugLightingMode = DebugLightingMode.None;
				this.data.materialDebugSettings.DisableMaterialDebug();
			}
			this.data.fullScreenDebugMode = value;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000055D3 File Offset: 0x000037D3
		public void SetShadowDebugMode(ShadowMapDebugMode value)
		{
			if (value == ShadowMapDebugMode.SingleShadow)
			{
				this.data.fullScreenDebugMode = FullScreenDebugMode.None;
			}
			this.data.lightingDebugSettings.shadowDebugMode = value;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000055F6 File Offset: 0x000037F6
		public void SetDebugLightFilterMode(DebugLightFilterMode value)
		{
			if (value != DebugLightFilterMode.None)
			{
				this.data.materialDebugSettings.DisableMaterialDebug();
				this.data.mipMapDebugSettings.debugMipMapMode = DebugMipMapMode.None;
			}
			this.data.lightingDebugSettings.debugLightFilterMode = value;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005630 File Offset: 0x00003830
		public void SetDebugLightingMode(DebugLightingMode value)
		{
			if (value != DebugLightingMode.None)
			{
				this.data.fullScreenDebugMode = FullScreenDebugMode.None;
				this.data.materialDebugSettings.DisableMaterialDebug();
				this.data.mipMapDebugSettings.debugMipMapMode = DebugMipMapMode.None;
			}
			this.data.lightingDebugSettings.debugLightingMode = value;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000567E File Offset: 0x0000387E
		public void SetMipMapMode(DebugMipMapMode value)
		{
			if (value != DebugMipMapMode.None)
			{
				this.data.materialDebugSettings.DisableMaterialDebug();
				this.data.lightingDebugSettings.debugLightingMode = DebugLightingMode.None;
			}
			this.data.mipMapDebugSettings.debugMipMapMode = value;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000056B8 File Offset: 0x000038B8
		private void EnableProfilingRecorders()
		{
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.HDRenderPipelineAllRenderRequest));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.VolumeUpdate));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.ClearBuffers));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderShadowMaps));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.GBuffer));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.PrepareLightsForGPU));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.VolumeVoxelization));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.VolumetricLighting));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderDeferredLightingCompute));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardOpaque));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardTransparent));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.ForwardPreRefraction));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.ColorPyramid));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.DepthPyramid));
			this.m_RecordedSamplers.Add(ProfilingSampler.Get<HDProfileId>(HDProfileId.PostProcessing));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000057D4 File Offset: 0x000039D4
		private void DisableProfilingRecorders()
		{
			foreach (ProfilingSampler profilingSampler in this.m_RecordedSamplers)
			{
				profilingSampler.enableRecording = false;
			}
			this.m_RecordedSamplers.Clear();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005830 File Offset: 0x00003A30
		private ObservableList<DebugUI.Widget> BuildProfilingSamplerList(DebugDisplaySettings.DebugProfilingType type)
		{
			ObservableList<DebugUI.Widget> observableList = new ObservableList<DebugUI.Widget>();
			using (List<ProfilingSampler>.Enumerator enumerator = this.m_RecordedSamplers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProfilingSampler sampler = enumerator.Current;
					sampler.enableRecording = true;
					observableList.Add(new DebugUI.Value
					{
						displayName = sampler.name,
						getter = () => string.Format("{0:F2}", (type == DebugDisplaySettings.DebugProfilingType.CPU) ? sampler.cpuElapsedTime : ((type == DebugDisplaySettings.DebugProfilingType.GPU) ? sampler.gpuElapsedTime : sampler.inlineCpuElapsedTime)),
						refreshRate = 0.2f
					});
				}
			}
			return observableList;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000058E8 File Offset: 0x00003AE8
		private void RegisterDisplayStatsDebug()
		{
			List<DebugUI.Widget> list = new List<DebugUI.Widget>();
			List<DebugUI.Widget> list2 = list;
			DebugUI.Value value13 = new DebugUI.Value();
			value13.displayName = "Frame Rate (fps)";
			value13.getter = () => 1f / Time.smoothDeltaTime;
			value13.refreshRate = 0.2f;
			list2.Add(value13);
			List<DebugUI.Widget> list3 = list;
			DebugUI.Value value2 = new DebugUI.Value();
			value2.displayName = "Frame Time (ms)";
			value2.getter = () => Time.smoothDeltaTime * 1000f;
			value2.refreshRate = 0.2f;
			list3.Add(value2);
			this.EnableProfilingRecorders();
			list.Add(new DebugUI.Foldout("CPU timings (Command Buffers)", this.BuildProfilingSamplerList(DebugDisplaySettings.DebugProfilingType.CPU), null));
			list.Add(new DebugUI.Foldout("GPU timings", this.BuildProfilingSamplerList(DebugDisplaySettings.DebugProfilingType.GPU), null));
			list.Add(new DebugUI.Foldout("Inline CPU timings", this.BuildProfilingSamplerList(DebugDisplaySettings.DebugProfilingType.InlineCPU), null));
			list.Add(new DebugUI.BoolField
			{
				displayName = "Count Rays (MRays/Frame)",
				getter = () => this.data.countRays,
				setter = delegate(bool value)
				{
					this.data.countRays = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshDisplayStatsDebug<bool>)
			});
			if (this.data.countRays)
			{
				List<DebugUI.Widget> list4 = list;
				DebugUI.Container container = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.Value value3 = new DebugUI.Value();
				value3.displayName = "Ambient Occlusion";
				value3.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.AmbientOcclusion) / 1000000f;
				value3.refreshRate = 0.033333335f;
				children.Add(value3);
				ObservableList<DebugUI.Widget> children2 = container.children;
				DebugUI.Value value4 = new DebugUI.Value();
				value4.displayName = "Shadows Directional";
				value4.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.ShadowDirectional) / 1000000f;
				value4.refreshRate = 0.033333335f;
				children2.Add(value4);
				ObservableList<DebugUI.Widget> children3 = container.children;
				DebugUI.Value value5 = new DebugUI.Value();
				value5.displayName = "Shadows Area";
				value5.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.ShadowAreaLight) / 1000000f;
				value5.refreshRate = 0.033333335f;
				children3.Add(value5);
				ObservableList<DebugUI.Widget> children4 = container.children;
				DebugUI.Value value6 = new DebugUI.Value();
				value6.displayName = "Shadows Point/Spot";
				value6.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.ShadowPointSpot) / 1000000f;
				value6.refreshRate = 0.033333335f;
				children4.Add(value6);
				ObservableList<DebugUI.Widget> children5 = container.children;
				DebugUI.Value value7 = new DebugUI.Value();
				value7.displayName = "Reflections Forward ";
				value7.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.ReflectionForward) / 1000000f;
				value7.refreshRate = 0.033333335f;
				children5.Add(value7);
				ObservableList<DebugUI.Widget> children6 = container.children;
				DebugUI.Value value8 = new DebugUI.Value();
				value8.displayName = "Reflections Deferred";
				value8.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.ReflectionDeferred) / 1000000f;
				value8.refreshRate = 0.033333335f;
				children6.Add(value8);
				ObservableList<DebugUI.Widget> children7 = container.children;
				DebugUI.Value value9 = new DebugUI.Value();
				value9.displayName = "Diffuse GI Forward";
				value9.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.DiffuseGI_Forward) / 1000000f;
				value9.refreshRate = 0.033333335f;
				children7.Add(value9);
				ObservableList<DebugUI.Widget> children8 = container.children;
				DebugUI.Value value10 = new DebugUI.Value();
				value10.displayName = "Diffuse GI Deferred";
				value10.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.DiffuseGI_Deferred) / 1000000f;
				value10.refreshRate = 0.033333335f;
				children8.Add(value10);
				ObservableList<DebugUI.Widget> children9 = container.children;
				DebugUI.Value value11 = new DebugUI.Value();
				value11.displayName = "Recursive Rendering";
				value11.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.Recursive) / 1000000f;
				value11.refreshRate = 0.033333335f;
				children9.Add(value11);
				ObservableList<DebugUI.Widget> children10 = container.children;
				DebugUI.Value value12 = new DebugUI.Value();
				value12.displayName = "Total";
				value12.getter = () => (RenderPipelineManager.currentPipeline as HDRenderPipeline).GetRaysPerFrame(RayCountValues.Total) / 1000000f;
				value12.refreshRate = 0.033333335f;
				children10.Add(value12);
				list4.Add(container);
			}
			this.m_DebugDisplayStatsItems = list.ToArray();
			DebugUI.Panel panel = DebugManager.instance.GetPanel(DebugDisplaySettings.k_PanelDisplayStats, true, 0, false);
			panel.flags = DebugUI.Flags.RuntimeOnly;
			panel.children.Add(this.m_DebugDisplayStatsItems);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005D60 File Offset: 0x00003F60
		private void RegisterMaterialDebug()
		{
			List<DebugUI.Widget> list = new List<DebugUI.Widget>();
			list.Add(new DebugUI.EnumField
			{
				displayName = "Common Material Property",
				getter = () => (int)this.data.materialDebugSettings.debugViewMaterialCommonValue,
				setter = delegate(int value)
				{
					this.SetDebugViewCommonMaterialProperty((MaterialSharedProperty)value);
				},
				autoEnum = typeof(MaterialSharedProperty),
				getIndex = () => (int)this.data.materialDebugSettings.debugViewMaterialCommonValue,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.materialDebugSettings.debugViewMaterialCommonValue = (MaterialSharedProperty)value;
				}
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "Material",
				getter = delegate
				{
					if (this.data.materialDebugSettings.debugViewMaterial[0] != 0)
					{
						return this.data.materialDebugSettings.debugViewMaterial[1];
					}
					return 0;
				},
				setter = delegate(int value)
				{
					this.SetDebugViewMaterial(value);
				},
				enumNames = MaterialDebugSettings.debugViewMaterialStrings,
				enumValues = MaterialDebugSettings.debugViewMaterialValues,
				getIndex = () => this.data.materialDebugSettings.materialEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.materialDebugSettings.materialEnumIndex = value;
				}
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "Engine",
				getter = () => this.data.materialDebugSettings.debugViewEngine,
				setter = delegate(int value)
				{
					this.SetDebugViewEngine(value);
				},
				enumNames = MaterialDebugSettings.debugViewEngineStrings,
				enumValues = MaterialDebugSettings.debugViewEngineValues,
				getIndex = () => this.data.engineEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.engineEnumIndex = value;
				}
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "Attributes",
				getter = () => (int)this.data.materialDebugSettings.debugViewVarying,
				setter = delegate(int value)
				{
					this.SetDebugViewVarying((DebugViewVarying)value);
				},
				autoEnum = typeof(DebugViewVarying),
				getIndex = () => this.data.attributesEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.attributesEnumIndex = value;
				}
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "Properties",
				getter = () => (int)this.data.materialDebugSettings.debugViewProperties,
				setter = delegate(int value)
				{
					this.SetDebugViewProperties((DebugViewProperties)value);
				},
				autoEnum = typeof(DebugViewProperties),
				getIndex = () => this.data.propertiesEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.propertiesEnumIndex = value;
				}
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "GBuffer",
				getter = () => this.data.materialDebugSettings.debugViewGBuffer,
				setter = delegate(int value)
				{
					this.SetDebugViewGBuffer(value);
				},
				enumNames = MaterialDebugSettings.debugViewMaterialGBufferStrings,
				enumValues = MaterialDebugSettings.debugViewMaterialGBufferValues,
				getIndex = () => this.data.gBufferEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.gBufferEnumIndex = value;
				}
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "Material Validator",
				getter = () => (int)this.data.fullScreenDebugMode,
				setter = delegate(int value)
				{
					this.SetFullScreenDebugMode((FullScreenDebugMode)value);
				},
				enumNames = DebugDisplaySettings.s_MaterialFullScreenDebugStrings,
				enumValues = DebugDisplaySettings.s_MaterialFullScreenDebugValues,
				onValueChanged = new Action<DebugUI.Field<int>, int>(this.RefreshMaterialDebug<int>),
				getIndex = () => this.data.materialValidatorDebugModeEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.materialValidatorDebugModeEnumIndex = value;
				}
			});
			if (this.data.fullScreenDebugMode == FullScreenDebugMode.ValidateDiffuseColor || this.data.fullScreenDebugMode == FullScreenDebugMode.ValidateSpecularColor)
			{
				list.Add(new DebugUI.Container
				{
					children = 
					{
						new DebugUI.ColorField
						{
							displayName = "Too High Color",
							getter = () => this.data.materialDebugSettings.materialValidateHighColor,
							setter = delegate(Color value)
							{
								this.data.materialDebugSettings.materialValidateHighColor = value;
							},
							showAlpha = false,
							hdr = true
						},
						new DebugUI.ColorField
						{
							displayName = "Too Low Color",
							getter = () => this.data.materialDebugSettings.materialValidateLowColor,
							setter = delegate(Color value)
							{
								this.data.materialDebugSettings.materialValidateLowColor = value;
							},
							showAlpha = false,
							hdr = true
						},
						new DebugUI.ColorField
						{
							displayName = "Not A Pure Metal Color",
							getter = () => this.data.materialDebugSettings.materialValidateTrueMetalColor,
							setter = delegate(Color value)
							{
								this.data.materialDebugSettings.materialValidateTrueMetalColor = value;
							},
							showAlpha = false,
							hdr = true
						},
						new DebugUI.BoolField
						{
							displayName = "Pure Metals",
							getter = () => this.data.materialDebugSettings.materialValidateTrueMetal,
							setter = delegate(bool v)
							{
								this.data.materialDebugSettings.materialValidateTrueMetal = v;
							}
						}
					}
				});
			}
			this.m_DebugMaterialItems = list.ToArray();
			DebugManager.instance.GetPanel(DebugDisplaySettings.k_PanelMaterials, true, 0, false).children.Add(this.m_DebugMaterialItems);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000621F File Offset: 0x0000441F
		private void RefreshDisplayStatsDebug<T>(DebugUI.Field<T> field, T value)
		{
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelDisplayStats, this.m_DebugDisplayStatsItems);
			this.RegisterDisplayStatsDebug();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006238 File Offset: 0x00004438
		private void RefreshLightingDebug<T>(DebugUI.Field<T> field, T value)
		{
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelLighting, this.m_DebugLightingItems);
			this.RegisterLightingDebug();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006251 File Offset: 0x00004451
		private void RefreshDecalsDebug<T>(DebugUI.Field<T> field, T value)
		{
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelDecals, this.m_DebugDecalsItems);
			this.RegisterDecalsDebug();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000626A File Offset: 0x0000446A
		private void RefreshRenderingDebug<T>(DebugUI.Field<T> field, T value)
		{
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelRendering, this.m_DebugRenderingItems);
			this.RegisterRenderingDebug();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006283 File Offset: 0x00004483
		private void RefreshMaterialDebug<T>(DebugUI.Field<T> field, T value)
		{
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelMaterials, this.m_DebugMaterialItems);
			this.RegisterMaterialDebug();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000629C File Offset: 0x0000449C
		private void RegisterLightingDebug()
		{
			List<DebugUI.Widget> list = new List<DebugUI.Widget>();
			list.Add(new DebugUI.Foldout
			{
				displayName = "Show Light By Type",
				children = 
				{
					new DebugUI.BoolField
					{
						displayName = "Show Directional Lights",
						getter = () => this.data.lightingDebugSettings.showDirectionalLight,
						setter = delegate(bool value)
						{
							this.data.lightingDebugSettings.showDirectionalLight = value;
						}
					},
					new DebugUI.BoolField
					{
						displayName = "Show Punctual Lights",
						getter = () => this.data.lightingDebugSettings.showPunctualLight,
						setter = delegate(bool value)
						{
							this.data.lightingDebugSettings.showPunctualLight = value;
						}
					},
					new DebugUI.BoolField
					{
						displayName = "Show Area Lights",
						getter = () => this.data.lightingDebugSettings.showAreaLight,
						setter = delegate(bool value)
						{
							this.data.lightingDebugSettings.showAreaLight = value;
						}
					},
					new DebugUI.BoolField
					{
						displayName = "Show Reflection Probe",
						getter = () => this.data.lightingDebugSettings.showReflectionProbe,
						setter = delegate(bool value)
						{
							this.data.lightingDebugSettings.showReflectionProbe = value;
						}
					}
				}
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "Shadow Debug Mode",
				getter = () => (int)this.data.lightingDebugSettings.shadowDebugMode,
				setter = delegate(int value)
				{
					this.SetShadowDebugMode((ShadowMapDebugMode)value);
				},
				autoEnum = typeof(ShadowMapDebugMode),
				onValueChanged = new Action<DebugUI.Field<int>, int>(this.RefreshLightingDebug<int>),
				getIndex = () => this.data.shadowDebugModeEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.shadowDebugModeEnumIndex = value;
				}
			});
			if (this.data.lightingDebugSettings.shadowDebugMode == ShadowMapDebugMode.VisualizeShadowMap || this.data.lightingDebugSettings.shadowDebugMode == ShadowMapDebugMode.SingleShadow)
			{
				DebugUI.Container container = new DebugUI.Container();
				container.children.Add(new DebugUI.BoolField
				{
					displayName = "Use Selection",
					getter = () => this.data.lightingDebugSettings.shadowDebugUseSelection,
					setter = delegate(bool value)
					{
						this.data.lightingDebugSettings.shadowDebugUseSelection = value;
					},
					flags = DebugUI.Flags.EditorOnly,
					onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
				});
				if (!this.data.lightingDebugSettings.shadowDebugUseSelection)
				{
					ObservableList<DebugUI.Widget> children = container.children;
					DebugUI.UIntField uintField = new DebugUI.UIntField();
					uintField.displayName = "Shadow Map Index";
					uintField.getter = () => this.data.lightingDebugSettings.shadowMapIndex;
					uintField.setter = delegate(uint value)
					{
						this.data.lightingDebugSettings.shadowMapIndex = value;
					};
					uintField.min = () => 0U;
					uintField.max = () => (uint)Math.Max(0L, (long)(RenderPipelineManager.currentPipeline as HDRenderPipeline).GetCurrentShadowCount() - 1L);
					children.Add(uintField);
				}
				list.Add(container);
			}
			List<DebugUI.Widget> list2 = list;
			DebugUI.FloatField floatField = new DebugUI.FloatField();
			floatField.displayName = "Global Shadow Scale Factor";
			floatField.getter = () => this.data.lightingDebugSettings.shadowResolutionScaleFactor;
			floatField.setter = delegate(float v)
			{
				this.data.lightingDebugSettings.shadowResolutionScaleFactor = v;
			};
			floatField.min = () => 0.01f;
			floatField.max = () => 4f;
			list2.Add(floatField);
			list.Add(new DebugUI.BoolField
			{
				displayName = "Clear Shadow Atlas",
				getter = () => this.data.lightingDebugSettings.clearShadowAtlas,
				setter = delegate(bool v)
				{
					this.data.lightingDebugSettings.clearShadowAtlas = v;
				}
			});
			list.Add(new DebugUI.FloatField
			{
				displayName = "Shadow Range Minimum Value",
				getter = () => this.data.lightingDebugSettings.shadowMinValue,
				setter = delegate(float value)
				{
					this.data.lightingDebugSettings.shadowMinValue = value;
				}
			});
			list.Add(new DebugUI.FloatField
			{
				displayName = "Shadow Range Maximum Value",
				getter = () => this.data.lightingDebugSettings.shadowMaxValue,
				setter = delegate(float value)
				{
					this.data.lightingDebugSettings.shadowMaxValue = value;
				}
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "Lighting Debug Mode",
				getter = () => (int)this.data.lightingDebugSettings.debugLightingMode,
				setter = delegate(int value)
				{
					this.SetDebugLightingMode((DebugLightingMode)value);
				},
				autoEnum = typeof(DebugLightingMode),
				onValueChanged = new Action<DebugUI.Field<int>, int>(this.RefreshLightingDebug<int>),
				getIndex = () => this.data.lightingDebugModeEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.lightingDebugModeEnumIndex = value;
				}
			});
			list.Add(new DebugUI.BitField
			{
				displayName = "Light Hierarchy Debug Mode",
				getter = () => this.data.lightingDebugSettings.debugLightFilterMode,
				setter = delegate(Enum value)
				{
					this.SetDebugLightFilterMode((DebugLightFilterMode)value);
				},
				enumType = typeof(DebugLightFilterMode),
				onValueChanged = new Action<DebugUI.Field<Enum>, Enum>(this.RefreshLightingDebug<Enum>)
			});
			list.Add(new DebugUI.EnumField
			{
				displayName = "Fullscreen Debug Mode",
				getter = () => (int)this.data.fullScreenDebugMode,
				setter = delegate(int value)
				{
					this.SetFullScreenDebugMode((FullScreenDebugMode)value);
				},
				enumNames = DebugDisplaySettings.s_LightingFullScreenDebugStrings,
				enumValues = DebugDisplaySettings.s_LightingFullScreenDebugValues,
				onValueChanged = new Action<DebugUI.Field<int>, int>(this.RefreshLightingDebug<int>),
				getIndex = () => this.data.lightingFulscreenDebugModeEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.lightingFulscreenDebugModeEnumIndex = value;
				}
			});
			if (this.data.fullScreenDebugMode == FullScreenDebugMode.ScreenSpaceShadows)
			{
				List<DebugUI.Widget> list3 = list;
				DebugUI.UIntField uintField2 = new DebugUI.UIntField();
				uintField2.displayName = "Screen Space Shadow Index";
				uintField2.getter = () => this.data.screenSpaceShadowIndex;
				uintField2.setter = delegate(uint value)
				{
					this.data.screenSpaceShadowIndex = value;
				};
				uintField2.min = () => 0U;
				uintField2.max = () => (uint)(RenderPipelineManager.currentPipeline as HDRenderPipeline).GetMaxScreenSpaceShadows();
				list3.Add(uintField2);
			}
			FullScreenDebugMode fullScreenDebugMode = this.data.fullScreenDebugMode;
			if (fullScreenDebugMode != FullScreenDebugMode.ContactShadows)
			{
				if (fullScreenDebugMode - FullScreenDebugMode.PreRefractionColorPyramid <= 2)
				{
					List<DebugUI.Widget> list4 = list;
					DebugUI.Container container2 = new DebugUI.Container();
					ObservableList<DebugUI.Widget> children2 = container2.children;
					DebugUI.UIntField uintField3 = new DebugUI.UIntField();
					uintField3.displayName = "Fullscreen Debug Mip";
					uintField3.getter = delegate
					{
						FullScreenDebugMode fullScreenDebugMode2 = this.data.fullScreenDebugMode;
						int num;
						if (fullScreenDebugMode2 == FullScreenDebugMode.PreRefractionColorPyramid || fullScreenDebugMode2 == FullScreenDebugMode.FinalColorPyramid)
						{
							num = HDShaderIDs._ColorPyramidScale;
						}
						else
						{
							num = HDShaderIDs._DepthPyramidScale;
						}
						float z = Shader.GetGlobalVector(num).z;
						return (uint)(this.data.fullscreenDebugMip * z);
					};
					uintField3.setter = delegate(uint value)
					{
						FullScreenDebugMode fullScreenDebugMode3 = this.data.fullScreenDebugMode;
						int num2;
						if (fullScreenDebugMode3 == FullScreenDebugMode.PreRefractionColorPyramid || fullScreenDebugMode3 == FullScreenDebugMode.FinalColorPyramid)
						{
							num2 = HDShaderIDs._ColorPyramidScale;
						}
						else
						{
							num2 = HDShaderIDs._DepthPyramidScale;
						}
						float z2 = Shader.GetGlobalVector(num2).z;
						this.data.fullscreenDebugMip = (float)Convert.ChangeType(value, typeof(float)) / z2;
					};
					uintField3.min = () => 0U;
					uintField3.max = delegate
					{
						FullScreenDebugMode fullScreenDebugMode4 = this.data.fullScreenDebugMode;
						int num3;
						if (fullScreenDebugMode4 == FullScreenDebugMode.PreRefractionColorPyramid || fullScreenDebugMode4 == FullScreenDebugMode.FinalColorPyramid)
						{
							num3 = HDShaderIDs._ColorPyramidScale;
						}
						else
						{
							num3 = HDShaderIDs._DepthPyramidScale;
						}
						return (uint)Shader.GetGlobalVector(num3).z;
					};
					children2.Add(uintField3);
					list4.Add(container2);
				}
				else
				{
					this.data.fullscreenDebugMip = 0f;
				}
			}
			else
			{
				List<DebugUI.Widget> list5 = list;
				DebugUI.Container container3 = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children3 = container3.children;
				DebugUI.IntField intField = new DebugUI.IntField();
				intField.displayName = "Light Index";
				intField.getter = () => this.data.fullScreenContactShadowLightIndex;
				intField.setter = delegate(int value)
				{
					this.data.fullScreenContactShadowLightIndex = value;
				};
				intField.min = () => -1;
				intField.max = () => LightDefinitions.s_LightListMaxPrunedEntries - 1;
				children3.Add(intField);
				list5.Add(container3);
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Override Smoothness",
				getter = () => this.data.lightingDebugSettings.overrideSmoothness,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.overrideSmoothness = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.overrideSmoothness)
			{
				List<DebugUI.Widget> list6 = list;
				DebugUI.Container container4 = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children4 = container4.children;
				DebugUI.FloatField floatField2 = new DebugUI.FloatField();
				floatField2.displayName = "Smoothness";
				floatField2.getter = () => this.data.lightingDebugSettings.overrideSmoothnessValue;
				floatField2.setter = delegate(float value)
				{
					this.data.lightingDebugSettings.overrideSmoothnessValue = value;
				};
				floatField2.min = () => 0f;
				floatField2.max = () => 1f;
				floatField2.incStep = 0.025f;
				children4.Add(floatField2);
				list6.Add(container4);
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Override Albedo",
				getter = () => this.data.lightingDebugSettings.overrideAlbedo,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.overrideAlbedo = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.overrideAlbedo)
			{
				list.Add(new DebugUI.Container
				{
					children = 
					{
						new DebugUI.ColorField
						{
							displayName = "Albedo",
							getter = () => this.data.lightingDebugSettings.overrideAlbedoValue,
							setter = delegate(Color value)
							{
								this.data.lightingDebugSettings.overrideAlbedoValue = value;
							},
							showAlpha = false,
							hdr = false
						}
					}
				});
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Override Normal",
				getter = () => this.data.lightingDebugSettings.overrideNormal,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.overrideNormal = value;
				}
			});
			list.Add(new DebugUI.BoolField
			{
				displayName = "Override Specular Color",
				getter = () => this.data.lightingDebugSettings.overrideSpecularColor,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.overrideSpecularColor = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.overrideSpecularColor)
			{
				list.Add(new DebugUI.Container
				{
					children = 
					{
						new DebugUI.ColorField
						{
							displayName = "Specular Color",
							getter = () => this.data.lightingDebugSettings.overrideSpecularColorValue,
							setter = delegate(Color value)
							{
								this.data.lightingDebugSettings.overrideSpecularColorValue = value;
							},
							showAlpha = false,
							hdr = false
						}
					}
				});
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Override AmbientOcclusion",
				getter = () => this.data.lightingDebugSettings.overrideAmbientOcclusion,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.overrideAmbientOcclusion = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.overrideAmbientOcclusion)
			{
				List<DebugUI.Widget> list7 = list;
				DebugUI.Container container5 = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children5 = container5.children;
				DebugUI.FloatField floatField3 = new DebugUI.FloatField();
				floatField3.displayName = "AmbientOcclusion";
				floatField3.getter = () => this.data.lightingDebugSettings.overrideAmbientOcclusionValue;
				floatField3.setter = delegate(float value)
				{
					this.data.lightingDebugSettings.overrideAmbientOcclusionValue = value;
				};
				floatField3.min = () => 0f;
				floatField3.max = () => 1f;
				floatField3.incStep = 0.025f;
				children5.Add(floatField3);
				list7.Add(container5);
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Override Emissive Color",
				getter = () => this.data.lightingDebugSettings.overrideEmissiveColor,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.overrideEmissiveColor = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.overrideEmissiveColor)
			{
				list.Add(new DebugUI.Container
				{
					children = 
					{
						new DebugUI.ColorField
						{
							displayName = "Emissive Color",
							getter = () => this.data.lightingDebugSettings.overrideEmissiveColorValue,
							setter = delegate(Color value)
							{
								this.data.lightingDebugSettings.overrideEmissiveColorValue = value;
							},
							showAlpha = false,
							hdr = true
						}
					}
				});
			}
			list.Add(new DebugUI.EnumField
			{
				displayName = "Tile/Cluster Debug",
				getter = () => (int)this.data.lightingDebugSettings.tileClusterDebug,
				setter = delegate(int value)
				{
					this.data.lightingDebugSettings.tileClusterDebug = (TileClusterDebug)value;
				},
				autoEnum = typeof(TileClusterDebug),
				onValueChanged = new Action<DebugUI.Field<int>, int>(this.RefreshLightingDebug<int>),
				getIndex = () => this.data.tileClusterDebugEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.tileClusterDebugEnumIndex = value;
				}
			});
			if (this.data.lightingDebugSettings.tileClusterDebug != TileClusterDebug.None && this.data.lightingDebugSettings.tileClusterDebug != TileClusterDebug.MaterialFeatureVariants)
			{
				list.Add(new DebugUI.Container
				{
					children = 
					{
						new DebugUI.EnumField
						{
							displayName = "Tile/Cluster Debug By Category",
							getter = () => (int)this.data.lightingDebugSettings.tileClusterDebugByCategory,
							setter = delegate(int value)
							{
								this.data.lightingDebugSettings.tileClusterDebugByCategory = (TileClusterCategoryDebug)value;
							},
							autoEnum = typeof(TileClusterCategoryDebug),
							getIndex = () => this.data.tileClusterDebugByCategoryEnumIndex,
							setIndex = delegate(int value)
							{
								this.data.tileClusterDebugByCategoryEnumIndex = value;
							}
						}
					}
				});
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Display Sky Reflection",
				getter = () => this.data.lightingDebugSettings.displaySkyReflection,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.displaySkyReflection = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.displaySkyReflection)
			{
				List<DebugUI.Widget> list8 = list;
				DebugUI.Container container6 = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children6 = container6.children;
				DebugUI.FloatField floatField4 = new DebugUI.FloatField();
				floatField4.displayName = "Sky Reflection Mipmap";
				floatField4.getter = () => this.data.lightingDebugSettings.skyReflectionMipmap;
				floatField4.setter = delegate(float value)
				{
					this.data.lightingDebugSettings.skyReflectionMipmap = value;
				};
				floatField4.min = () => 0f;
				floatField4.max = () => 1f;
				floatField4.incStep = 0.05f;
				children6.Add(floatField4);
				list8.Add(container6);
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Display Light Volumes",
				getter = () => this.data.lightingDebugSettings.displayLightVolumes,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.displayLightVolumes = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.displayLightVolumes)
			{
				List<DebugUI.Widget> list9 = list;
				DebugUI.Container container7 = new DebugUI.Container();
				container7.children.Add(new DebugUI.EnumField
				{
					displayName = "Light Volume Debug Type",
					getter = () => (int)this.data.lightingDebugSettings.lightVolumeDebugByCategory,
					setter = delegate(int value)
					{
						this.data.lightingDebugSettings.lightVolumeDebugByCategory = (LightVolumeDebug)value;
					},
					autoEnum = typeof(LightVolumeDebug),
					getIndex = () => this.data.lightVolumeDebugTypeEnumIndex,
					setIndex = delegate(int value)
					{
						this.data.lightVolumeDebugTypeEnumIndex = value;
					}
				});
				ObservableList<DebugUI.Widget> children7 = container7.children;
				DebugUI.UIntField uintField4 = new DebugUI.UIntField();
				uintField4.displayName = "Max Debug Light Count";
				uintField4.getter = () => this.data.lightingDebugSettings.maxDebugLightCount;
				uintField4.setter = delegate(uint value)
				{
					this.data.lightingDebugSettings.maxDebugLightCount = value;
				};
				uintField4.min = () => 0U;
				uintField4.max = () => 24U;
				uintField4.incStep = 1U;
				children7.Add(uintField4);
				list9.Add(container7);
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Display Cookie Atlas",
				getter = () => this.data.lightingDebugSettings.displayCookieAtlas,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.displayCookieAtlas = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.displayCookieAtlas)
			{
				List<DebugUI.Widget> list10 = list;
				DebugUI.Container container8 = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children8 = container8.children;
				DebugUI.UIntField uintField5 = new DebugUI.UIntField();
				uintField5.displayName = "Mip Level";
				uintField5.getter = () => this.data.lightingDebugSettings.cookieAtlasMipLevel;
				uintField5.setter = delegate(uint value)
				{
					this.data.lightingDebugSettings.cookieAtlasMipLevel = value;
				};
				uintField5.min = () => 0U;
				uintField5.max = () => (uint)(RenderPipelineManager.currentPipeline as HDRenderPipeline).GetCookieAtlasMipCount();
				children8.Add(uintField5);
				container8.children.Add(new DebugUI.Button
				{
					displayName = "Reset Cookie Atlas",
					action = delegate
					{
						this.data.lightingDebugSettings.clearCookieAtlas = true;
					}
				});
				list10.Add(container8);
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Display Point Light Cookie Array",
				getter = () => this.data.lightingDebugSettings.displayCookieCubeArray,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.displayCookieCubeArray = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.displayCookieCubeArray)
			{
				List<DebugUI.Widget> list11 = list;
				DebugUI.Container container9 = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children9 = container9.children;
				DebugUI.UIntField uintField6 = new DebugUI.UIntField();
				uintField6.displayName = "Slice Index";
				uintField6.getter = () => this.data.lightingDebugSettings.cookieCubeArraySliceIndex;
				uintField6.setter = delegate(uint value)
				{
					this.data.lightingDebugSettings.cookieCubeArraySliceIndex = value;
				};
				uintField6.min = () => 0U;
				uintField6.max = () => (uint)((RenderPipelineManager.currentPipeline as HDRenderPipeline).GetCookieCubeArraySize() - 1);
				children9.Add(uintField6);
				list11.Add(container9);
			}
			list.Add(new DebugUI.BoolField
			{
				displayName = "Display Planar Reflection Atlas",
				getter = () => this.data.lightingDebugSettings.displayPlanarReflectionProbeAtlas,
				setter = delegate(bool value)
				{
					this.data.lightingDebugSettings.displayPlanarReflectionProbeAtlas = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshLightingDebug<bool>)
			});
			if (this.data.lightingDebugSettings.displayPlanarReflectionProbeAtlas)
			{
				List<DebugUI.Widget> list12 = list;
				DebugUI.Container container10 = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children10 = container10.children;
				DebugUI.UIntField uintField7 = new DebugUI.UIntField();
				uintField7.displayName = "Mip Level";
				uintField7.getter = () => this.data.lightingDebugSettings.planarReflectionProbeMipLevel;
				uintField7.setter = delegate(uint value)
				{
					this.data.lightingDebugSettings.planarReflectionProbeMipLevel = value;
				};
				uintField7.min = () => 0U;
				uintField7.max = () => (uint)(RenderPipelineManager.currentPipeline as HDRenderPipeline).GetPlanarReflectionProbeMipCount();
				children10.Add(uintField7);
				container10.children.Add(new DebugUI.Button
				{
					displayName = "Reset Planar Atlas",
					action = delegate
					{
						this.data.lightingDebugSettings.clearPlanarReflectionProbeAtlas = true;
					}
				});
				list12.Add(container10);
			}
			List<DebugUI.Widget> list13 = list;
			DebugUI.FloatField floatField5 = new DebugUI.FloatField();
			floatField5.displayName = "Debug Overlay Screen Ratio";
			floatField5.getter = () => this.data.debugOverlayRatio;
			floatField5.setter = delegate(float v)
			{
				this.data.debugOverlayRatio = v;
			};
			floatField5.min = () => 0.1f;
			floatField5.max = () => 1f;
			list13.Add(floatField5);
			if (this.DebugNeedsExposure() || this.data.lightingDebugSettings.displaySkyReflection || this.data.lightingDebugSettings.displayPlanarReflectionProbeAtlas || this.data.lightingDebugSettings.displayCookieAtlas || this.data.lightingDebugSettings.displayCookieCubeArray)
			{
				list.Add(new DebugUI.FloatField
				{
					displayName = "Debug Exposure",
					getter = () => this.data.lightingDebugSettings.debugExposure,
					setter = delegate(float value)
					{
						this.data.lightingDebugSettings.debugExposure = value;
					}
				});
			}
			this.m_DebugLightingItems = list.ToArray();
			DebugManager.instance.GetPanel(DebugDisplaySettings.k_PanelLighting, true, 0, false).children.Add(this.m_DebugLightingItems);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007644 File Offset: 0x00005844
		private void RegisterRenderingDebug()
		{
			List<DebugUI.Widget> list = new List<DebugUI.Widget>();
			list.Add(new DebugUI.EnumField
			{
				displayName = "Fullscreen Debug Mode",
				getter = () => (int)this.data.fullScreenDebugMode,
				setter = delegate(int value)
				{
					this.SetFullScreenDebugMode((FullScreenDebugMode)value);
				},
				onValueChanged = new Action<DebugUI.Field<int>, int>(this.RefreshRenderingDebug<int>),
				enumNames = DebugDisplaySettings.s_RenderingFullScreenDebugStrings,
				enumValues = DebugDisplaySettings.s_RenderingFullScreenDebugValues,
				getIndex = () => this.data.renderingFulscreenDebugModeEnumIndex,
				setIndex = delegate(int value)
				{
					this.data.ResetExclusiveEnumIndices();
					this.data.renderingFulscreenDebugModeEnumIndex = value;
				}
			});
			if (this.data.fullScreenDebugMode == FullScreenDebugMode.TransparencyOverdraw)
			{
				List<DebugUI.Widget> list2 = list;
				DebugUI.Container container = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.FloatField floatField = new DebugUI.FloatField();
				floatField.displayName = "Max Pixel Cost";
				floatField.getter = () => this.data.transparencyDebugSettings.maxPixelCost;
				floatField.setter = delegate(float value)
				{
					this.data.transparencyDebugSettings.maxPixelCost = value;
				};
				floatField.min = () => 0.25f;
				floatField.max = () => 2048f;
				children.Add(floatField);
				list2.Add(container);
			}
			list.AddRange(new DebugUI.Widget[]
			{
				new DebugUI.EnumField
				{
					displayName = "MipMaps",
					getter = () => (int)this.data.mipMapDebugSettings.debugMipMapMode,
					setter = delegate(int value)
					{
						this.SetMipMapMode((DebugMipMapMode)value);
					},
					autoEnum = typeof(DebugMipMapMode),
					onValueChanged = new Action<DebugUI.Field<int>, int>(this.RefreshRenderingDebug<int>),
					getIndex = () => this.data.mipMapsEnumIndex,
					setIndex = delegate(int value)
					{
						this.data.ResetExclusiveEnumIndices();
						this.data.mipMapsEnumIndex = value;
					}
				}
			});
			if (this.data.mipMapDebugSettings.debugMipMapMode != DebugMipMapMode.None)
			{
				list.Add(new DebugUI.Container
				{
					children = 
					{
						new DebugUI.EnumField
						{
							displayName = "Terrain Texture",
							getter = () => (int)this.data.mipMapDebugSettings.terrainTexture,
							setter = delegate(int value)
							{
								this.data.mipMapDebugSettings.terrainTexture = (DebugMipMapModeTerrainTexture)value;
							},
							autoEnum = typeof(DebugMipMapModeTerrainTexture),
							getIndex = () => this.data.terrainTextureEnumIndex,
							setIndex = delegate(int value)
							{
								this.data.terrainTextureEnumIndex = value;
							}
						}
					}
				});
			}
			list.AddRange(new DebugUI.Container[]
			{
				new DebugUI.Container
				{
					displayName = "Color Picker",
					flags = DebugUI.Flags.EditorOnly,
					children = 
					{
						new DebugUI.EnumField
						{
							displayName = "Debug Mode",
							getter = () => (int)this.data.colorPickerDebugSettings.colorPickerMode,
							setter = delegate(int value)
							{
								this.data.colorPickerDebugSettings.colorPickerMode = (ColorPickerDebugMode)value;
							},
							autoEnum = typeof(ColorPickerDebugMode),
							getIndex = () => this.data.colorPickerDebugModeEnumIndex,
							setIndex = delegate(int value)
							{
								this.data.colorPickerDebugModeEnumIndex = value;
							}
						},
						new DebugUI.ColorField
						{
							displayName = "Font Color",
							flags = DebugUI.Flags.EditorOnly,
							getter = () => this.data.colorPickerDebugSettings.fontColor,
							setter = delegate(Color value)
							{
								this.data.colorPickerDebugSettings.fontColor = value;
							}
						}
					}
				}
			});
			list.Add(new DebugUI.BoolField
			{
				displayName = "False Color Mode",
				getter = () => this.data.falseColorDebugSettings.falseColor,
				setter = delegate(bool value)
				{
					this.data.falseColorDebugSettings.falseColor = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshRenderingDebug<bool>)
			});
			if (this.data.falseColorDebugSettings.falseColor)
			{
				list.Add(new DebugUI.Container
				{
					flags = DebugUI.Flags.EditorOnly,
					children = 
					{
						new DebugUI.FloatField
						{
							displayName = "Range Threshold 0",
							getter = () => this.data.falseColorDebugSettings.colorThreshold0,
							setter = delegate(float value)
							{
								this.data.falseColorDebugSettings.colorThreshold0 = Mathf.Min(value, this.data.falseColorDebugSettings.colorThreshold1);
							}
						},
						new DebugUI.FloatField
						{
							displayName = "Range Threshold 1",
							getter = () => this.data.falseColorDebugSettings.colorThreshold1,
							setter = delegate(float value)
							{
								this.data.falseColorDebugSettings.colorThreshold1 = Mathf.Clamp(value, this.data.falseColorDebugSettings.colorThreshold0, this.data.falseColorDebugSettings.colorThreshold2);
							}
						},
						new DebugUI.FloatField
						{
							displayName = "Range Threshold 2",
							getter = () => this.data.falseColorDebugSettings.colorThreshold2,
							setter = delegate(float value)
							{
								this.data.falseColorDebugSettings.colorThreshold2 = Mathf.Clamp(value, this.data.falseColorDebugSettings.colorThreshold1, this.data.falseColorDebugSettings.colorThreshold3);
							}
						},
						new DebugUI.FloatField
						{
							displayName = "Range Threshold 3",
							getter = () => this.data.falseColorDebugSettings.colorThreshold3,
							setter = delegate(float value)
							{
								this.data.falseColorDebugSettings.colorThreshold3 = Mathf.Max(value, this.data.falseColorDebugSettings.colorThreshold2);
							}
						}
					}
				});
			}
			list.AddRange(new DebugUI.Widget[]
			{
				new DebugUI.EnumField
				{
					displayName = "MSAA Samples",
					getter = () => (int)this.data.msaaSamples,
					setter = delegate(int value)
					{
						this.data.msaaSamples = (MSAASamples)value;
					},
					enumNames = DebugDisplaySettings.s_MsaaSamplesDebugStrings,
					enumValues = DebugDisplaySettings.s_MsaaSamplesDebugValues,
					getIndex = () => this.data.msaaSampleDebugModeEnumIndex,
					setIndex = delegate(int value)
					{
						this.data.msaaSampleDebugModeEnumIndex = value;
					}
				}
			});
			list.AddRange(new DebugUI.Widget[]
			{
				new DebugUI.EnumField
				{
					displayName = "Freeze Camera for culling",
					getter = () => this.data.debugCameraToFreeze,
					setter = delegate(int value)
					{
						this.data.debugCameraToFreeze = value;
					},
					enumNames = DebugDisplaySettings.s_CameraNamesStrings,
					enumValues = DebugDisplaySettings.s_CameraNamesValues,
					getIndex = () => this.data.debugCameraToFreezeEnumIndex,
					setIndex = delegate(int value)
					{
						this.data.debugCameraToFreezeEnumIndex = value;
					}
				}
			});
			if (XRSystem.testModeEnabled)
			{
				list.Add(new DebugUI.BoolField
				{
					displayName = "XR single-pass test mode",
					getter = () => this.data.xrSinglePassTestMode,
					setter = delegate(bool value)
					{
						this.data.xrSinglePassTestMode = value;
					}
				});
			}
			this.m_DebugRenderingItems = list.ToArray();
			DebugManager.instance.GetPanel(DebugDisplaySettings.k_PanelRendering, true, 0, false).children.Add(this.m_DebugRenderingItems);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007C54 File Offset: 0x00005E54
		private void RegisterDecalsDebug()
		{
			DebugUI.Widget[] array = new DebugUI.Widget[2];
			array[0] = new DebugUI.BoolField
			{
				displayName = "Display Atlas",
				getter = () => this.data.decalsDebugSettings.displayAtlas,
				setter = delegate(bool value)
				{
					this.data.decalsDebugSettings.displayAtlas = value;
				}
			};
			int num = 1;
			DebugUI.UIntField uintField = new DebugUI.UIntField();
			uintField.displayName = "Mip Level";
			uintField.getter = () => this.data.decalsDebugSettings.mipLevel;
			uintField.setter = delegate(uint value)
			{
				this.data.decalsDebugSettings.mipLevel = value;
			};
			uintField.min = () => 0U;
			uintField.max = () => (uint)(RenderPipelineManager.currentPipeline as HDRenderPipeline).GetDecalAtlasMipCount();
			array[num] = uintField;
			this.m_DebugDecalsItems = array;
			DebugManager.instance.GetPanel(DebugDisplaySettings.k_PanelDecals, true, 0, false).children.Add(this.m_DebugDecalsItems);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007D47 File Offset: 0x00005F47
		internal void RegisterDebug()
		{
			this.RegisterDecalsDebug();
			this.RegisterDisplayStatsDebug();
			this.RegisterMaterialDebug();
			this.RegisterLightingDebug();
			this.RegisterRenderingDebug();
			DebugManager.instance.RegisterData(this);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007D74 File Offset: 0x00005F74
		internal void UnregisterDebug()
		{
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelDecals, this.m_DebugDecalsItems);
			this.DisableProfilingRecorders();
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelDisplayStats, this.m_DebugDisplayStatsItems);
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelMaterials, this.m_DebugMaterialItems);
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelLighting, this.m_DebugLightingItems);
			this.UnregisterDebugItems(DebugDisplaySettings.k_PanelRendering, this.m_DebugRenderingItems);
			DebugManager.instance.UnregisterData(this);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007DE8 File Offset: 0x00005FE8
		private void UnregisterDebugItems(string panelName, DebugUI.Widget[] items)
		{
			DebugUI.Panel panel = DebugManager.instance.GetPanel(panelName, false, 0, false);
			if (panel != null)
			{
				panel.children.Remove(items);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007E14 File Offset: 0x00006014
		private void FillFullScreenDebugEnum(ref GUIContent[] strings, ref int[] values, FullScreenDebugMode min, FullScreenDebugMode max)
		{
			int num = max - min - 1;
			strings = new GUIContent[num + 1];
			values = new int[num + 1];
			strings[0] = new GUIContent(FullScreenDebugMode.None.ToString());
			values[0] = 0;
			int num2 = 1;
			for (int i = (int)(min + 1); i < (int)max; i++)
			{
				GUIContent[] array = strings;
				int num3 = num2;
				FullScreenDebugMode fullScreenDebugMode = (FullScreenDebugMode)i;
				array[num3] = new GUIContent(fullScreenDebugMode.ToString());
				values[num2] = i;
				num2++;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007E8B File Offset: 0x0000608B
		private static string FormatVector(Vector3 v)
		{
			return string.Format("({0:F6}, {1:F6}, {2:F6})", v.x, v.y, v.z);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007EB8 File Offset: 0x000060B8
		internal static void RegisterCamera(IFrameSettingsHistoryContainer container)
		{
			string name = container.panelName;
			if (DebugDisplaySettings.s_CameraNames.FindIndex((GUIContent x) => x.text.Equals(name)) < 0)
			{
				DebugDisplaySettings.s_CameraNames.Add(new GUIContent(name));
				DebugDisplaySettings.needsRefreshingCameraFreezeList = true;
			}
			if (!FrameSettingsHistory.IsRegistered(container, false))
			{
				IDebugData debugData = FrameSettingsHistory.RegisterDebug(container, false);
				DebugManager.instance.RegisterData(debugData);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007F28 File Offset: 0x00006128
		internal static void UnRegisterCamera(IFrameSettingsHistoryContainer container)
		{
			string name = container.panelName;
			int num = DebugDisplaySettings.s_CameraNames.FindIndex((GUIContent x) => x.text.Equals(name));
			if (num > 0)
			{
				DebugDisplaySettings.s_CameraNames.RemoveAt(num);
				DebugDisplaySettings.needsRefreshingCameraFreezeList = true;
			}
			if (FrameSettingsHistory.IsRegistered(container, false))
			{
				DebugManager.instance.UnregisterData(container);
				FrameSettingsHistory.UnRegisterDebug(container);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007F8D File Offset: 0x0000618D
		internal bool IsDebugDisplayRemovePostprocess()
		{
			return this.data.materialDebugSettings.IsDebugDisplayEnabled() || this.data.lightingDebugSettings.IsDebugDisplayRemovePostprocess() || this.data.mipMapDebugSettings.IsDebugDisplayEnabled();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007FC5 File Offset: 0x000061C5
		internal void UpdateMaterials()
		{
			if (this.data.mipMapDebugSettings.debugMipMapMode != DebugMipMapMode.None)
			{
				Texture.SetStreamingTextureMaterialDebugProperties();
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007FE0 File Offset: 0x000061E0
		internal void UpdateCameraFreezeOptions()
		{
			if (DebugDisplaySettings.needsRefreshingCameraFreezeList)
			{
				DebugDisplaySettings.s_CameraNames.Insert(0, new GUIContent("None"));
				DebugDisplaySettings.s_CameraNamesStrings = DebugDisplaySettings.s_CameraNames.ToArray();
				DebugDisplaySettings.s_CameraNamesValues = Enumerable.Range(0, DebugDisplaySettings.s_CameraNames.Count<GUIContent>()).ToArray<int>();
				this.UnregisterDebugItems(DebugDisplaySettings.k_PanelRendering, this.m_DebugRenderingItems);
				this.RegisterRenderingDebug();
				DebugDisplaySettings.needsRefreshingCameraFreezeList = false;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008050 File Offset: 0x00006250
		internal bool DebugNeedsExposure()
		{
			DebugLightingMode debugLightingMode = this.data.lightingDebugSettings.debugLightingMode;
			DebugViewGbuffer debugViewGBuffer = (DebugViewGbuffer)this.data.materialDebugSettings.debugViewGBuffer;
			return debugLightingMode == DebugLightingMode.DiffuseLighting || debugLightingMode == DebugLightingMode.SpecularLighting || debugLightingMode == DebugLightingMode.VisualizeCascade || this.data.lightingDebugSettings.overrideAlbedo || this.data.lightingDebugSettings.overrideNormal || this.data.lightingDebugSettings.overrideSmoothness || this.data.lightingDebugSettings.overrideSpecularColor || this.data.lightingDebugSettings.overrideEmissiveColor || this.data.lightingDebugSettings.overrideAmbientOcclusion || debugViewGBuffer == DebugViewGbuffer.BakeDiffuseLightingWithAlbedoPlusEmissive || this.data.fullScreenDebugMode == FullScreenDebugMode.PreRefractionColorPyramid || this.data.fullScreenDebugMode == FullScreenDebugMode.FinalColorPyramid || this.data.fullScreenDebugMode == FullScreenDebugMode.ScreenSpaceReflections || this.data.fullScreenDebugMode == FullScreenDebugMode.LightCluster || this.data.fullScreenDebugMode == FullScreenDebugMode.ScreenSpaceShadows || this.data.fullScreenDebugMode == FullScreenDebugMode.NanTracker || this.data.fullScreenDebugMode == FullScreenDebugMode.ColorLog || this.data.fullScreenDebugMode == FullScreenDebugMode.RayTracedGlobalIllumination;
		}

		// Token: 0x040000C3 RID: 195
		private static string k_PanelDisplayStats = "Display Stats";

		// Token: 0x040000C4 RID: 196
		private static string k_PanelMaterials = "Material";

		// Token: 0x040000C5 RID: 197
		private static string k_PanelLighting = "Lighting";

		// Token: 0x040000C6 RID: 198
		private static string k_PanelRendering = "Rendering";

		// Token: 0x040000C7 RID: 199
		private static string k_PanelDecals = "Decals";

		// Token: 0x040000C8 RID: 200
		private DebugUI.Widget[] m_DebugDisplayStatsItems;

		// Token: 0x040000C9 RID: 201
		private DebugUI.Widget[] m_DebugMaterialItems;

		// Token: 0x040000CA RID: 202
		private DebugUI.Widget[] m_DebugLightingItems;

		// Token: 0x040000CB RID: 203
		private DebugUI.Widget[] m_DebugRenderingItems;

		// Token: 0x040000CC RID: 204
		private DebugUI.Widget[] m_DebugDecalsItems;

		// Token: 0x040000CD RID: 205
		private static GUIContent[] s_LightingFullScreenDebugStrings = null;

		// Token: 0x040000CE RID: 206
		private static int[] s_LightingFullScreenDebugValues = null;

		// Token: 0x040000CF RID: 207
		private static GUIContent[] s_RenderingFullScreenDebugStrings = null;

		// Token: 0x040000D0 RID: 208
		private static int[] s_RenderingFullScreenDebugValues = null;

		// Token: 0x040000D1 RID: 209
		private static GUIContent[] s_MaterialFullScreenDebugStrings = null;

		// Token: 0x040000D2 RID: 210
		private static int[] s_MaterialFullScreenDebugValues = null;

		// Token: 0x040000D3 RID: 211
		private static GUIContent[] s_MsaaSamplesDebugStrings = null;

		// Token: 0x040000D4 RID: 212
		private static int[] s_MsaaSamplesDebugValues = null;

		// Token: 0x040000D5 RID: 213
		private static List<GUIContent> s_CameraNames = new List<GUIContent>();

		// Token: 0x040000D6 RID: 214
		private static GUIContent[] s_CameraNamesStrings = null;

		// Token: 0x040000D7 RID: 215
		private static int[] s_CameraNamesValues = null;

		// Token: 0x040000D8 RID: 216
		private static bool needsRefreshingCameraFreezeList = true;

		// Token: 0x040000D9 RID: 217
		private List<ProfilingSampler> m_RecordedSamplers = new List<ProfilingSampler>();

		// Token: 0x040000DA RID: 218
		private DebugDisplaySettings.DebugData m_Data;

		// Token: 0x0200018F RID: 399
		private enum DebugProfilingType
		{
			// Token: 0x040010B0 RID: 4272
			CPU,
			// Token: 0x040010B1 RID: 4273
			GPU,
			// Token: 0x040010B2 RID: 4274
			InlineCPU
		}

		// Token: 0x02000190 RID: 400
		public class DebugData
		{
			// Token: 0x06000B06 RID: 2822 RVA: 0x00054848 File Offset: 0x00052A48
			internal void ResetExclusiveEnumIndices()
			{
				this.materialDebugSettings.materialEnumIndex = 0;
				this.lightingDebugModeEnumIndex = 0;
				this.mipMapsEnumIndex = 0;
				this.engineEnumIndex = 0;
				this.attributesEnumIndex = 0;
				this.propertiesEnumIndex = 0;
				this.gBufferEnumIndex = 0;
				this.lightingFulscreenDebugModeEnumIndex = 0;
				this.renderingFulscreenDebugModeEnumIndex = 0;
			}

			// Token: 0x040010B3 RID: 4275
			public float debugOverlayRatio = 0.33f;

			// Token: 0x040010B4 RID: 4276
			public FullScreenDebugMode fullScreenDebugMode;

			// Token: 0x040010B5 RID: 4277
			public float fullscreenDebugMip;

			// Token: 0x040010B6 RID: 4278
			public int fullScreenContactShadowLightIndex;

			// Token: 0x040010B7 RID: 4279
			public bool xrSinglePassTestMode;

			// Token: 0x040010B8 RID: 4280
			public MaterialDebugSettings materialDebugSettings = new MaterialDebugSettings();

			// Token: 0x040010B9 RID: 4281
			public LightingDebugSettings lightingDebugSettings = new LightingDebugSettings();

			// Token: 0x040010BA RID: 4282
			public MipMapDebugSettings mipMapDebugSettings = new MipMapDebugSettings();

			// Token: 0x040010BB RID: 4283
			public ColorPickerDebugSettings colorPickerDebugSettings = new ColorPickerDebugSettings();

			// Token: 0x040010BC RID: 4284
			public FalseColorDebugSettings falseColorDebugSettings = new FalseColorDebugSettings();

			// Token: 0x040010BD RID: 4285
			public DecalsDebugSettings decalsDebugSettings = new DecalsDebugSettings();

			// Token: 0x040010BE RID: 4286
			public TransparencyDebugSettings transparencyDebugSettings = new TransparencyDebugSettings();

			// Token: 0x040010BF RID: 4287
			public MSAASamples msaaSamples = MSAASamples.None;

			// Token: 0x040010C0 RID: 4288
			public uint screenSpaceShadowIndex;

			// Token: 0x040010C1 RID: 4289
			public bool countRays;

			// Token: 0x040010C2 RID: 4290
			public int debugCameraToFreeze;

			// Token: 0x040010C3 RID: 4291
			internal int lightingDebugModeEnumIndex;

			// Token: 0x040010C4 RID: 4292
			internal int lightingFulscreenDebugModeEnumIndex;

			// Token: 0x040010C5 RID: 4293
			internal int materialValidatorDebugModeEnumIndex;

			// Token: 0x040010C6 RID: 4294
			internal int tileClusterDebugEnumIndex;

			// Token: 0x040010C7 RID: 4295
			internal int mipMapsEnumIndex;

			// Token: 0x040010C8 RID: 4296
			internal int engineEnumIndex;

			// Token: 0x040010C9 RID: 4297
			internal int attributesEnumIndex;

			// Token: 0x040010CA RID: 4298
			internal int propertiesEnumIndex;

			// Token: 0x040010CB RID: 4299
			internal int gBufferEnumIndex;

			// Token: 0x040010CC RID: 4300
			internal int shadowDebugModeEnumIndex;

			// Token: 0x040010CD RID: 4301
			internal int tileClusterDebugByCategoryEnumIndex;

			// Token: 0x040010CE RID: 4302
			internal int lightVolumeDebugTypeEnumIndex;

			// Token: 0x040010CF RID: 4303
			internal int renderingFulscreenDebugModeEnumIndex;

			// Token: 0x040010D0 RID: 4304
			internal int terrainTextureEnumIndex;

			// Token: 0x040010D1 RID: 4305
			internal int colorPickerDebugModeEnumIndex;

			// Token: 0x040010D2 RID: 4306
			internal int msaaSampleDebugModeEnumIndex;

			// Token: 0x040010D3 RID: 4307
			internal int debugCameraToFreezeEnumIndex;
		}
	}
}
