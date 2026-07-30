using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000075 RID: 117
	internal class LightUtils
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0002ACD5 File Offset: 0x00028ED5
		public static float ConvertPointLightLumenToCandela(float intensity)
		{
			return intensity / 12.566371f;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0002ACDE File Offset: 0x00028EDE
		public static float ConvertPointLightCandelaToLumen(float intensity)
		{
			return intensity * 12.566371f;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0002ACE7 File Offset: 0x00028EE7
		public static float ConvertSpotLightLumenToCandela(float intensity, float angle, bool exact)
		{
			if (!exact)
			{
				return intensity / 3.1415927f;
			}
			return intensity / (2f * (1f - Mathf.Cos(angle / 2f)) * 3.1415927f);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0002AD14 File Offset: 0x00028F14
		public static float ConvertSpotLightCandelaToLumen(float intensity, float angle, bool exact)
		{
			if (!exact)
			{
				return intensity * 3.1415927f;
			}
			return intensity * (2f * (1f - Mathf.Cos(angle / 2f)) * 3.1415927f);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0002AD41 File Offset: 0x00028F41
		public static float ConvertFrustrumLightLumenToCandela(float intensity, float angleA, float angleB)
		{
			return intensity / (4f * Mathf.Asin(Mathf.Sin(angleA / 2f) * Mathf.Sin(angleB / 2f)));
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0002AD69 File Offset: 0x00028F69
		public static float ConvertFrustrumLightCandelaToLumen(float intensity, float angleA, float angleB)
		{
			return intensity * (4f * Mathf.Asin(Mathf.Sin(angleA / 2f) * Mathf.Sin(angleB / 2f)));
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0002AD91 File Offset: 0x00028F91
		public static float ConvertSphereLightLumenToLuminance(float intensity, float sphereRadius)
		{
			return intensity / (12.566371f * sphereRadius * sphereRadius * 3.1415927f);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0002ADA4 File Offset: 0x00028FA4
		public static float ConvertSphereLightLuminanceToLumen(float intensity, float sphereRadius)
		{
			return intensity * (12.566371f * sphereRadius * sphereRadius * 3.1415927f);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0002ADB7 File Offset: 0x00028FB7
		public static float ConvertDiscLightLumenToLuminance(float intensity, float discRadius)
		{
			return intensity / (discRadius * discRadius * 3.1415927f * 3.1415927f);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0002ADCA File Offset: 0x00028FCA
		public static float ConvertDiscLightLuminanceToLumen(float intensity, float discRadius)
		{
			return intensity * (discRadius * discRadius * 3.1415927f * 3.1415927f);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0002ADDD File Offset: 0x00028FDD
		public static float ConvertRectLightLumenToLuminance(float intensity, float width, float height)
		{
			return intensity / (width * height * 3.1415927f);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0002ADEA File Offset: 0x00028FEA
		public static float ConvertRectLightLuminanceToLumen(float intensity, float width, float height)
		{
			return intensity * (width * height * 3.1415927f);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0002ADF7 File Offset: 0x00028FF7
		public static float ConvertLuxToCandela(float lux, float distance)
		{
			return lux * distance * distance;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0002ADFE File Offset: 0x00028FFE
		public static float ConvertCandelaToLux(float candela, float distance)
		{
			return candela / (distance * distance);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0002AE05 File Offset: 0x00029005
		public static float ConvertEvToLuminance(float ev)
		{
			return Mathf.Pow(2f, ev - 3f);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0002AE18 File Offset: 0x00029018
		public static float ConvertEvToCandela(float ev)
		{
			return LightUtils.ConvertEvToLuminance(ev);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0002AE20 File Offset: 0x00029020
		public static float ConvertEvToLux(float ev, float distance)
		{
			return LightUtils.ConvertCandelaToLux(LightUtils.ConvertEvToLuminance(ev), distance);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0002AE2E File Offset: 0x0002902E
		public static float ConvertLuminanceToEv(float luminance)
		{
			return (float)Math.Log((double)(luminance * 100f / 12.5f), 2.0);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0002AE4D File Offset: 0x0002904D
		public static float ConvertCandelaToEv(float candela)
		{
			return LightUtils.ConvertLuminanceToEv(candela);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0002AE55 File Offset: 0x00029055
		public static float ConvertLuxToEv(float lux, float distance)
		{
			return LightUtils.ConvertLuminanceToEv(LightUtils.ConvertLuxToCandela(lux, distance));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0002AE63 File Offset: 0x00029063
		public static float ConvertPunctualLightLumenToCandela(HDLightType lightType, float lumen, float initialIntensity, bool enableSpotReflector)
		{
			if (lightType == HDLightType.Spot && enableSpotReflector)
			{
				return initialIntensity;
			}
			return LightUtils.ConvertPointLightLumenToCandela(lumen);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0002AE75 File Offset: 0x00029075
		public static float ConvertPunctualLightLumenToLux(HDLightType lightType, float lumen, float initialIntensity, bool enableSpotReflector, float distance)
		{
			return LightUtils.ConvertCandelaToLux(LightUtils.ConvertPunctualLightLumenToCandela(lightType, lumen, initialIntensity, enableSpotReflector), distance);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0002AE88 File Offset: 0x00029088
		public static float ConvertPunctualLightCandelaToLumen(HDLightType lightType, SpotLightShape spotLightShape, float candela, bool enableSpotReflector, float spotAngle, float aspectRatio)
		{
			if (lightType != HDLightType.Spot || !enableSpotReflector)
			{
				return LightUtils.ConvertPointLightCandelaToLumen(candela);
			}
			if (spotLightShape == SpotLightShape.Cone)
			{
				return LightUtils.ConvertSpotLightCandelaToLumen(candela, spotAngle * 0.017453292f, true);
			}
			if (spotLightShape == SpotLightShape.Pyramid)
			{
				float num;
				float num2;
				LightUtils.CalculateAnglesForPyramid(aspectRatio, spotAngle * 0.017453292f, out num, out num2);
				return LightUtils.ConvertFrustrumLightCandelaToLumen(candela, num, num2);
			}
			return LightUtils.ConvertPointLightCandelaToLumen(candela);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0002AEE0 File Offset: 0x000290E0
		public static float ConvertPunctualLightLuxToLumen(HDLightType lightType, SpotLightShape spotLightShape, float lux, bool enableSpotReflector, float spotAngle, float aspectRatio, float distance)
		{
			float num = LightUtils.ConvertLuxToCandela(lux, distance);
			return LightUtils.ConvertPunctualLightCandelaToLumen(lightType, spotLightShape, num, enableSpotReflector, spotAngle, aspectRatio);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0002AF04 File Offset: 0x00029104
		public static float ConvertPunctualLightEvToLumen(HDLightType lightType, SpotLightShape spotLightShape, float ev, bool enableSpotReflector, float spotAngle, float aspectRatio)
		{
			float num = LightUtils.ConvertEvToCandela(ev);
			return LightUtils.ConvertPunctualLightCandelaToLumen(lightType, spotLightShape, num, enableSpotReflector, spotAngle, aspectRatio);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0002AF25 File Offset: 0x00029125
		public static float ConvertPunctualLightLumenToEv(HDLightType lightType, float lumen, float initialIntensity, bool enableSpotReflector)
		{
			return LightUtils.ConvertCandelaToEv(LightUtils.ConvertPunctualLightLumenToCandela(lightType, lumen, initialIntensity, enableSpotReflector));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0002AF35 File Offset: 0x00029135
		public static float ConvertAreaLightLumenToLuminance(AreaLightShape areaLightShape, float lumen, float width, float height = 0f)
		{
			switch (areaLightShape)
			{
			case AreaLightShape.Rectangle:
				return LightUtils.ConvertRectLightLumenToLuminance(lumen, width, height);
			case AreaLightShape.Tube:
				return LightUtils.CalculateLineLightLumenToLuminance(lumen, width);
			case AreaLightShape.Disc:
				return LightUtils.ConvertDiscLightLumenToLuminance(lumen, width);
			default:
				return lumen;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0002AF65 File Offset: 0x00029165
		public static float ConvertAreaLightLuminanceToLumen(AreaLightShape areaLightShape, float luminance, float width, float height = 0f)
		{
			switch (areaLightShape)
			{
			case AreaLightShape.Rectangle:
				return LightUtils.ConvertRectLightLuminanceToLumen(luminance, width, height);
			case AreaLightShape.Tube:
				return LightUtils.CalculateLineLightLuminanceToLumen(luminance, width);
			case AreaLightShape.Disc:
				return LightUtils.ConvertDiscLightLuminanceToLumen(luminance, width);
			default:
				return luminance;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0002AF95 File Offset: 0x00029195
		public static float ConvertAreaLightLumenToEv(AreaLightShape AreaLightShape, float lumen, float width, float height)
		{
			return LightUtils.ConvertLuminanceToEv(LightUtils.ConvertAreaLightLumenToLuminance(AreaLightShape, lumen, width, height));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0002AFA8 File Offset: 0x000291A8
		public static float ConvertAreaLightEvToLumen(AreaLightShape AreaLightShape, float ev, float width, float height)
		{
			float num = LightUtils.ConvertEvToLuminance(ev);
			return LightUtils.ConvertAreaLightLuminanceToLumen(AreaLightShape, num, width, height);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0002AFC5 File Offset: 0x000291C5
		public static float CalculateLineLightLumenToLuminance(float intensity, float lineWidth)
		{
			return intensity / (12.566371f * lineWidth);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0002AFD0 File Offset: 0x000291D0
		public static float CalculateLineLightLuminanceToLumen(float intensity, float lineWidth)
		{
			return intensity * (12.566371f * lineWidth);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0002AFDC File Offset: 0x000291DC
		public static void CalculateAnglesForPyramid(float aspectRatio, float spotAngle, out float angleA, out float angleB)
		{
			if (aspectRatio < 1f)
			{
				aspectRatio = 1f / aspectRatio;
			}
			angleA = spotAngle;
			float num = angleA * 0.5f;
			num = Mathf.Atan(Mathf.Tan(num) * aspectRatio);
			angleB = num * 2f;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0002B020 File Offset: 0x00029220
		internal static void ConvertLightIntensity(LightUnit oldLightUnit, LightUnit newLightUnit, HDAdditionalLightData hdLight, Light light)
		{
			float num = hdLight.intensity;
			float luxAtDistance = hdLight.luxAtDistance;
			HDLightType hdlightType = hdLight.ComputeLightType(light);
			if (hdlightType != HDLightType.Area)
			{
				if (oldLightUnit == LightUnit.Lumen && newLightUnit == LightUnit.Candela)
				{
					num = LightUtils.ConvertPunctualLightLumenToCandela(hdlightType, num, light.intensity, hdLight.enableSpotReflector);
				}
				else if (oldLightUnit == LightUnit.Lumen && newLightUnit == LightUnit.Lux)
				{
					num = LightUtils.ConvertPunctualLightLumenToLux(hdlightType, num, light.intensity, hdLight.enableSpotReflector, hdLight.luxAtDistance);
				}
				else if (oldLightUnit == LightUnit.Lumen && newLightUnit == LightUnit.Ev100)
				{
					num = LightUtils.ConvertPunctualLightLumenToEv(hdlightType, num, light.intensity, hdLight.enableSpotReflector);
				}
				else if (oldLightUnit == LightUnit.Candela && newLightUnit == LightUnit.Lumen)
				{
					num = LightUtils.ConvertPunctualLightCandelaToLumen(hdlightType, hdLight.spotLightShape, num, hdLight.enableSpotReflector, light.spotAngle, hdLight.aspectRatio);
				}
				else if (oldLightUnit == LightUnit.Candela && newLightUnit == LightUnit.Lux)
				{
					num = LightUtils.ConvertCandelaToLux(num, hdLight.luxAtDistance);
				}
				else if (oldLightUnit == LightUnit.Candela && newLightUnit == LightUnit.Ev100)
				{
					num = LightUtils.ConvertCandelaToEv(num);
				}
				else if (oldLightUnit == LightUnit.Lux && newLightUnit == LightUnit.Lumen)
				{
					num = LightUtils.ConvertPunctualLightLuxToLumen(hdlightType, hdLight.spotLightShape, num, hdLight.enableSpotReflector, light.spotAngle, hdLight.aspectRatio, hdLight.luxAtDistance);
				}
				else if (oldLightUnit == LightUnit.Lux && newLightUnit == LightUnit.Candela)
				{
					num = LightUtils.ConvertLuxToCandela(num, hdLight.luxAtDistance);
				}
				else if (oldLightUnit == LightUnit.Lux && newLightUnit == LightUnit.Ev100)
				{
					num = LightUtils.ConvertLuxToEv(num, hdLight.luxAtDistance);
				}
				else if (oldLightUnit == LightUnit.Ev100 && newLightUnit == LightUnit.Lumen)
				{
					num = LightUtils.ConvertPunctualLightEvToLumen(hdlightType, hdLight.spotLightShape, num, hdLight.enableSpotReflector, light.spotAngle, hdLight.aspectRatio);
				}
				else if (oldLightUnit == LightUnit.Ev100 && newLightUnit == LightUnit.Candela)
				{
					num = LightUtils.ConvertEvToCandela(num);
				}
				else if (oldLightUnit == LightUnit.Ev100 && newLightUnit == LightUnit.Lux)
				{
					num = LightUtils.ConvertEvToLux(num, hdLight.luxAtDistance);
				}
			}
			else
			{
				if (oldLightUnit == LightUnit.Lumen && newLightUnit == LightUnit.Nits)
				{
					num = LightUtils.ConvertAreaLightLumenToLuminance(hdLight.areaLightShape, num, hdLight.shapeWidth, hdLight.shapeHeight);
				}
				if (oldLightUnit == LightUnit.Nits && newLightUnit == LightUnit.Lumen)
				{
					num = LightUtils.ConvertAreaLightLuminanceToLumen(hdLight.areaLightShape, num, hdLight.shapeWidth, hdLight.shapeHeight);
				}
				if (oldLightUnit == LightUnit.Nits && newLightUnit == LightUnit.Ev100)
				{
					num = LightUtils.ConvertLuminanceToEv(num);
				}
				if (oldLightUnit == LightUnit.Ev100 && newLightUnit == LightUnit.Nits)
				{
					num = LightUtils.ConvertEvToLuminance(num);
				}
				if (oldLightUnit == LightUnit.Ev100 && newLightUnit == LightUnit.Lumen)
				{
					num = LightUtils.ConvertAreaLightEvToLumen(hdLight.areaLightShape, num, hdLight.shapeWidth, hdLight.shapeHeight);
				}
				if (oldLightUnit == LightUnit.Lumen && newLightUnit == LightUnit.Ev100)
				{
					num = LightUtils.ConvertAreaLightLumenToEv(hdLight.areaLightShape, num, hdLight.shapeWidth, hdLight.shapeHeight);
				}
			}
			hdLight.intensity = num;
		}
	}
}
