using System;
using Unity.Collections;
using UnityEngine.Experimental.GlobalIllumination;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000047 RID: 71
	internal class GlobalIlluminationUtils
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000B634 File Offset: 0x00009834
		public static bool LightDataGIExtract(Light light, ref LightDataGI lightDataGI)
		{
			HDAdditionalLightData hdadditionalLightData = light.GetComponent<HDAdditionalLightData>();
			if (hdadditionalLightData == null)
			{
				hdadditionalLightData = HDUtils.s_DefaultHDAdditionalLightData;
			}
			Color color = new Color(1f, 1f, 1f);
			lightDataGI.instanceID = light.GetInstanceID();
			LinearColor linearColor = (hdadditionalLightData.affectDiffuse ? LinearColor.Convert(light.color, light.intensity) : LinearColor.Black());
			linearColor.red *= color.r;
			linearColor.green *= color.g;
			linearColor.blue *= color.b;
			LinearColor linearColor2 = (hdadditionalLightData.affectDiffuse ? LightmapperUtils.ExtractIndirect(light) : LinearColor.Black());
			linearColor2.red *= color.r;
			linearColor2.green *= color.g;
			linearColor2.blue *= color.b;
			LightMode lightMode = LightmapperUtils.Extract(light.bakingOutput.lightmapBakeType);
			lightDataGI.color = linearColor;
			lightDataGI.indirectColor = linearColor2;
			lightDataGI.mode = LightmapperUtils.Extract(light.bakingOutput.lightmapBakeType);
			lightDataGI.shadow = ((light.shadows != LightShadows.None) ? 1 : 0);
			HDLightType hdlightType = hdadditionalLightData.ComputeLightType(light);
			if (hdlightType != HDLightType.Area)
			{
				lightDataGI.color.intensity = lightDataGI.color.intensity / 3.1415927f;
				lightDataGI.indirectColor.intensity = lightDataGI.indirectColor.intensity / 3.1415927f;
				linearColor.intensity /= 3.1415927f;
				linearColor2.intensity /= 3.1415927f;
			}
			switch (hdlightType)
			{
			case HDLightType.Spot:
				switch (hdadditionalLightData.spotLightShape)
				{
				case SpotLightShape.Cone:
				{
					SpotLight spotLight;
					spotLight.instanceID = light.GetInstanceID();
					spotLight.shadow = light.shadows > LightShadows.None;
					spotLight.mode = lightMode;
					spotLight.sphereRadius = 0f;
					spotLight.position = light.transform.position;
					spotLight.orientation = light.transform.rotation;
					spotLight.color = linearColor;
					spotLight.indirectColor = linearColor2;
					spotLight.range = light.range;
					spotLight.coneAngle = light.spotAngle * 0.017453292f;
					spotLight.innerConeAngle = light.spotAngle * 0.017453292f * hdadditionalLightData.innerSpotPercent01;
					spotLight.falloff = (hdadditionalLightData.applyRangeAttenuation ? FalloffType.InverseSquared : FalloffType.InverseSquaredNoRangeAttenuation);
					spotLight.angularFalloff = AngularFalloffType.AnalyticAndInnerAngle;
					lightDataGI.Init(ref spotLight);
					lightDataGI.shape1 = 1f;
					break;
				}
				case SpotLightShape.Pyramid:
				{
					SpotLightPyramidShape spotLightPyramidShape;
					spotLightPyramidShape.instanceID = light.GetInstanceID();
					spotLightPyramidShape.shadow = light.shadows > LightShadows.None;
					spotLightPyramidShape.mode = lightMode;
					spotLightPyramidShape.position = light.transform.position;
					spotLightPyramidShape.orientation = light.transform.rotation;
					spotLightPyramidShape.color = linearColor;
					spotLightPyramidShape.indirectColor = linearColor2;
					spotLightPyramidShape.range = light.range;
					spotLightPyramidShape.angle = light.spotAngle * 0.017453292f;
					spotLightPyramidShape.aspectRatio = hdadditionalLightData.aspectRatio;
					spotLightPyramidShape.falloff = (hdadditionalLightData.applyRangeAttenuation ? FalloffType.InverseSquared : FalloffType.InverseSquaredNoRangeAttenuation);
					lightDataGI.Init(ref spotLightPyramidShape);
					break;
				}
				case SpotLightShape.Box:
				{
					SpotLightBoxShape spotLightBoxShape;
					spotLightBoxShape.instanceID = light.GetInstanceID();
					spotLightBoxShape.shadow = light.shadows > LightShadows.None;
					spotLightBoxShape.mode = lightMode;
					spotLightBoxShape.position = light.transform.position;
					spotLightBoxShape.orientation = light.transform.rotation;
					spotLightBoxShape.color = linearColor;
					spotLightBoxShape.indirectColor = linearColor2;
					spotLightBoxShape.range = light.range;
					spotLightBoxShape.width = hdadditionalLightData.shapeWidth;
					spotLightBoxShape.height = hdadditionalLightData.shapeHeight;
					lightDataGI.Init(ref spotLightBoxShape);
					break;
				}
				}
				break;
			case HDLightType.Directional:
				lightDataGI.orientation.SetLookRotation(light.transform.forward, Vector3.up);
				lightDataGI.position = Vector3.zero;
				lightDataGI.range = 0f;
				lightDataGI.coneAngle = 0f;
				lightDataGI.innerConeAngle = 0f;
				lightDataGI.shape0 = 0f;
				lightDataGI.shape1 = 0f;
				lightDataGI.type = LightType.Directional;
				lightDataGI.falloff = FalloffType.Undefined;
				break;
			case HDLightType.Point:
				lightDataGI.orientation = Quaternion.identity;
				lightDataGI.position = light.transform.position;
				lightDataGI.range = light.range;
				lightDataGI.coneAngle = 0f;
				lightDataGI.innerConeAngle = 0f;
				lightDataGI.shape0 = 0f;
				lightDataGI.shape1 = 0f;
				lightDataGI.type = LightType.Point;
				lightDataGI.falloff = (hdadditionalLightData.applyRangeAttenuation ? FalloffType.InverseSquared : FalloffType.InverseSquaredNoRangeAttenuation);
				break;
			case HDLightType.Area:
				switch (hdadditionalLightData.areaLightShape)
				{
				case AreaLightShape.Rectangle:
					lightDataGI.orientation = light.transform.rotation;
					lightDataGI.position = light.transform.position;
					lightDataGI.range = light.range;
					lightDataGI.coneAngle = 0f;
					lightDataGI.innerConeAngle = 0f;
					lightDataGI.shape0 = 0f;
					lightDataGI.shape1 = 0f;
					lightDataGI.type = LightType.Rectangle;
					lightDataGI.falloff = (hdadditionalLightData.applyRangeAttenuation ? FalloffType.InverseSquared : FalloffType.InverseSquaredNoRangeAttenuation);
					break;
				case AreaLightShape.Tube:
					lightDataGI.InitNoBake(lightDataGI.instanceID);
					break;
				case AreaLightShape.Disc:
					lightDataGI.orientation = light.transform.rotation;
					lightDataGI.position = light.transform.position;
					lightDataGI.range = light.range;
					lightDataGI.coneAngle = 0f;
					lightDataGI.innerConeAngle = 0f;
					lightDataGI.shape0 = 0f;
					lightDataGI.shape1 = 0f;
					lightDataGI.type = LightType.Disc;
					lightDataGI.falloff = (hdadditionalLightData.applyRangeAttenuation ? FalloffType.InverseSquared : FalloffType.InverseSquaredNoRangeAttenuation);
					break;
				}
				break;
			}
			return true;
		}

		// Token: 0x040001CA RID: 458
		public static Lightmapping.RequestLightsDelegate hdLightsDelegate = delegate(Light[] requests, NativeArray<LightDataGI> lightsOutput)
		{
			LightDataGI lightDataGI = default(LightDataGI);
			for (int i = 0; i < requests.Length; i++)
			{
				Light light = requests[i];
				if (LightmapperUtils.Extract(light.bakingOutput.lightmapBakeType) == LightMode.Realtime)
				{
					GlobalIlluminationUtils.LightDataGIExtract(light, ref lightDataGI);
				}
				else
				{
					lightDataGI.InitNoBake(light.GetInstanceID());
				}
				lightsOutput[i] = lightDataGI;
			}
		};
	}
}
