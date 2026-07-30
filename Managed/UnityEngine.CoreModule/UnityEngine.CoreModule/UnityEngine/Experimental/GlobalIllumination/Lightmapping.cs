using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003C3 RID: 963
	public static class Lightmapping
	{
		// Token: 0x06002188 RID: 8584 RVA: 0x00038EC8 File Offset: 0x000370C8
		[RequiredByNativeCode]
		public static void SetDelegate(Lightmapping.RequestLightsDelegate del)
		{
			Lightmapping.s_RequestLightsDelegate = ((del != null) ? del : Lightmapping.s_DefaultDelegate);
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x00038EDC File Offset: 0x000370DC
		[RequiredByNativeCode]
		public static Lightmapping.RequestLightsDelegate GetDelegate()
		{
			return Lightmapping.s_RequestLightsDelegate;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x00038EF3 File Offset: 0x000370F3
		[RequiredByNativeCode]
		public static void ResetDelegate()
		{
			Lightmapping.s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00038F00 File Offset: 0x00037100
		[RequiredByNativeCode]
		internal unsafe static void RequestLights(Light[] lights, IntPtr outLightsPtr, int outLightsCount)
		{
			NativeArray<LightDataGI> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<LightDataGI>((void*)outLightsPtr, outLightsCount, Allocator.None);
			Lightmapping.s_RequestLightsDelegate(lights, nativeArray);
		}

		// Token: 0x04000C40 RID: 3136
		[RequiredByNativeCode]
		private static readonly Lightmapping.RequestLightsDelegate s_DefaultDelegate = delegate(Light[] requests, NativeArray<LightDataGI> lightsOutput)
		{
			DirectionalLight directionalLight = default(DirectionalLight);
			PointLight pointLight = default(PointLight);
			SpotLight spotLight = default(SpotLight);
			RectangleLight rectangleLight = default(RectangleLight);
			DiscLight discLight = default(DiscLight);
			Cookie cookie = default(Cookie);
			LightDataGI lightDataGI = default(LightDataGI);
			for (int i = 0; i < requests.Length; i++)
			{
				Light light = requests[i];
				switch (light.type)
				{
				case LightType.Spot:
					LightmapperUtils.Extract(light, ref spotLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref spotLight, ref cookie);
					break;
				case LightType.Directional:
					LightmapperUtils.Extract(light, ref directionalLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref directionalLight, ref cookie);
					break;
				case LightType.Point:
					LightmapperUtils.Extract(light, ref pointLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref pointLight, ref cookie);
					break;
				case LightType.Area:
					LightmapperUtils.Extract(light, ref rectangleLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref rectangleLight, ref cookie);
					break;
				case LightType.Disc:
					LightmapperUtils.Extract(light, ref discLight);
					LightmapperUtils.Extract(light, out cookie);
					lightDataGI.Init(ref discLight, ref cookie);
					break;
				default:
					lightDataGI.InitNoBake(light.GetInstanceID());
					break;
				}
				lightsOutput[i] = lightDataGI;
			}
		};

		// Token: 0x04000C41 RID: 3137
		[RequiredByNativeCode]
		private static Lightmapping.RequestLightsDelegate s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;

		// Token: 0x020003C4 RID: 964
		// (Invoke) Token: 0x0600218E RID: 8590
		public delegate void RequestLightsDelegate(Light[] requests, NativeArray<LightDataGI> lightsOutput);
	}
}
