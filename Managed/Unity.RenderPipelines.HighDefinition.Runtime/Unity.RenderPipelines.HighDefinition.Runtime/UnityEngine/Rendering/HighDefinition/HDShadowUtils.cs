using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200009B RID: 155
	internal static class HDShadowUtils
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x0003248F File Offset: 0x0003068F
		public unsafe static float Asfloat(uint val)
		{
			return *(float*)(&val);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0003248F File Offset: 0x0003068F
		public unsafe static float Asfloat(int val)
		{
			return *(float*)(&val);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00032495 File Offset: 0x00030695
		public unsafe static int Asint(float val)
		{
			return *(int*)(&val);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0003249B File Offset: 0x0003069B
		public unsafe static uint Asuint(float val)
		{
			return *(uint*)(&val);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000324A4 File Offset: 0x000306A4
		private static float GetPunctualFilterWidthInTexels()
		{
			HDRenderPipelineAsset currentAsset = HDRenderPipeline.currentAsset;
			if (currentAsset == null)
			{
				return 1f;
			}
			HDShadowFilteringQuality shadowFilteringQuality = currentAsset.currentPlatformRenderPipelineSettings.hdShadowInitParams.shadowFilteringQuality;
			if (shadowFilteringQuality == HDShadowFilteringQuality.Low)
			{
				return 3f;
			}
			if (shadowFilteringQuality != HDShadowFilteringQuality.Medium)
			{
				return 1f;
			}
			return 5f;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000324F4 File Offset: 0x000306F4
		public static void ExtractPointLightData(VisibleLight visibleLight, Vector2 viewportSize, float nearPlane, float normalBiasMax, uint faceIndex, out Matrix4x4 view, out Matrix4x4 invViewProjection, out Matrix4x4 projection, out Matrix4x4 deviceProjection, out ShadowSplitData splitData)
		{
			float num = HDShadowUtils.CalcGuardAnglePerspective(90f, viewportSize.x, HDShadowUtils.GetPunctualFilterWidthInTexels(), normalBiasMax, 79f);
			Vector4 vector;
			HDShadowUtils.ExtractPointLightMatrix(visibleLight, faceIndex, nearPlane, num, out view, out projection, out deviceProjection, out invViewProjection, out vector, out splitData);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00032534 File Offset: 0x00030734
		public static void ExtractSpotLightData(SpotLightShape shape, float spotAngle, float nearPlane, float aspectRatio, float shapeWidth, float shapeHeight, VisibleLight visibleLight, Vector2 viewportSize, float normalBiasMax, out Matrix4x4 view, out Matrix4x4 invViewProjection, out Matrix4x4 projection, out Matrix4x4 deviceProjection, out ShadowSplitData splitData)
		{
			if (shape != SpotLightShape.Pyramid)
			{
				aspectRatio = 1f;
			}
			float num = HDShadowUtils.CalcGuardAnglePerspective(spotAngle, viewportSize.x, HDShadowUtils.GetPunctualFilterWidthInTexels(), normalBiasMax, 180f - spotAngle);
			Vector4 vector;
			HDShadowUtils.ExtractSpotLightMatrix(visibleLight, spotAngle, nearPlane, num, aspectRatio, out view, out projection, out deviceProjection, out invViewProjection, out vector, out splitData);
			if (shape == SpotLightShape.Box)
			{
				projection = HDShadowUtils.ExtractBoxLightProjectionMatrix(visibleLight.range, shapeWidth, shapeHeight, nearPlane);
				deviceProjection = GL.GetGPUProjectionMatrix(projection, false);
				projection = GL.GetGPUProjectionMatrix(projection, true);
				HDShadowUtils.InvertOrthographic(ref projection, ref view, out invViewProjection);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000325D0 File Offset: 0x000307D0
		public static void ExtractDirectionalLightData(VisibleLight visibleLight, Vector2 viewportSize, uint cascadeIndex, int cascadeCount, float[] cascadeRatios, float nearPlaneOffset, CullingResults cullResults, int lightIndex, out Matrix4x4 view, out Matrix4x4 invViewProjection, out Matrix4x4 projection, out Matrix4x4 deviceProjection, out ShadowSplitData splitData)
		{
			splitData = default(ShadowSplitData);
			splitData.cullingSphere.Set(0f, 0f, 0f, float.NegativeInfinity);
			splitData.cullingPlaneCount = 0;
			splitData.shadowCascadeBlendCullingFactor = 0.6f;
			visibleLight.light.transform.forward;
			Vector3 vector = default(Vector3);
			int i = 0;
			int num = ((cascadeRatios.Length < 3) ? cascadeRatios.Length : 3);
			while (i < num)
			{
				vector[i] = cascadeRatios[i];
				i++;
			}
			cullResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(lightIndex, (int)cascadeIndex, cascadeCount, vector, (int)viewportSize.x, nearPlaneOffset, out view, out projection, out splitData);
			deviceProjection = GL.GetGPUProjectionMatrix(projection, false);
			projection = GL.GetGPUProjectionMatrix(projection, true);
			HDShadowUtils.InvertOrthographic(ref deviceProjection, ref view, out invViewProjection);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000326B4 File Offset: 0x000308B4
		public static void ExtractRectangleAreaLightData(VisibleLight visibleLight, Vector3 shadowPosition, float areaLightShadowCone, float shadowNearPlane, Vector2 shapeSize, Vector2 viewportSize, float normalBiasMax, out Matrix4x4 view, out Matrix4x4 invViewProjection, out Matrix4x4 projection, out Matrix4x4 deviceProjection, out ShadowSplitData splitData)
		{
			float num = shapeSize.x / shapeSize.y;
			visibleLight.spotAngle = areaLightShadowCone;
			float num2 = HDShadowUtils.CalcGuardAnglePerspective(visibleLight.spotAngle, viewportSize.x, HDShadowUtils.GetPunctualFilterWidthInTexels(), normalBiasMax, 180f - visibleLight.spotAngle);
			Vector4 vector;
			HDShadowUtils.ExtractSpotLightMatrix(visibleLight, visibleLight.spotAngle, shadowNearPlane, num2, num, out view, out projection, out deviceProjection, out invViewProjection, out vector, out splitData);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00032720 File Offset: 0x00030920
		private static void InvertView(ref Matrix4x4 view, out Matrix4x4 invview)
		{
			invview = Matrix4x4.zero;
			invview.m00 = view.m00;
			invview.m01 = view.m10;
			invview.m02 = view.m20;
			invview.m10 = view.m01;
			invview.m11 = view.m11;
			invview.m12 = view.m21;
			invview.m20 = view.m02;
			invview.m21 = view.m12;
			invview.m22 = view.m22;
			invview.m33 = 1f;
			invview.m03 = -(invview.m00 * view.m03 + invview.m01 * view.m13 + invview.m02 * view.m23);
			invview.m13 = -(invview.m10 * view.m03 + invview.m11 * view.m13 + invview.m12 * view.m23);
			invview.m23 = -(invview.m20 * view.m03 + invview.m21 * view.m13 + invview.m22 * view.m23);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00032840 File Offset: 0x00030A40
		private static void InvertOrthographic(ref Matrix4x4 proj, ref Matrix4x4 view, out Matrix4x4 vpinv)
		{
			Matrix4x4 matrix4x;
			HDShadowUtils.InvertView(ref view, out matrix4x);
			Matrix4x4 zero = Matrix4x4.zero;
			zero.m00 = 1f / proj.m00;
			zero.m11 = 1f / proj.m11;
			zero.m22 = 1f / proj.m22;
			zero.m33 = 1f;
			zero.m03 = proj.m03 * zero.m00;
			zero.m13 = proj.m13 * zero.m11;
			zero.m23 = -proj.m23 * zero.m22;
			vpinv = matrix4x * zero;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000328EC File Offset: 0x00030AEC
		private static void InvertPerspective(ref Matrix4x4 proj, ref Matrix4x4 view, out Matrix4x4 vpinv)
		{
			Matrix4x4 matrix4x;
			HDShadowUtils.InvertView(ref view, out matrix4x);
			Matrix4x4 zero = Matrix4x4.zero;
			zero.m00 = 1f / proj.m00;
			zero.m03 = proj.m02 * zero.m00;
			zero.m11 = 1f / proj.m11;
			zero.m13 = proj.m12 * zero.m11;
			zero.m22 = 0f;
			zero.m23 = -1f;
			zero.m33 = proj.m22 / proj.m23;
			zero.m32 = zero.m33 / proj.m22;
			vpinv = matrix4x * zero;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000329A4 File Offset: 0x00030BA4
		public static Matrix4x4 ExtractSpotLightProjectionMatrix(float range, float spotAngle, float nearPlane, float aspectRatio, float guardAngle)
		{
			float num = spotAngle + guardAngle;
			float num2 = Mathf.Max(nearPlane, HDShadowUtils.k_MinShadowNearPlane);
			float num3 = 1f / Mathf.Tan(num / 180f * 3.1415927f / 2f);
			float num4 = num2;
			float num5 = num4 + range;
			Matrix4x4 matrix4x = default(Matrix4x4);
			if (aspectRatio < 1f)
			{
				matrix4x.m00 = num3;
				matrix4x.m11 = num3 * aspectRatio;
			}
			else
			{
				matrix4x.m00 = num3 / aspectRatio;
				matrix4x.m11 = num3;
			}
			matrix4x.m22 = -(num5 + num4) / (num5 - num4);
			matrix4x.m23 = -2f * num5 * num4 / (num5 - num4);
			matrix4x.m32 = -1f;
			return matrix4x;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00032A54 File Offset: 0x00030C54
		public static Matrix4x4 ExtractBoxLightProjectionMatrix(float range, float width, float height, float nearPlane)
		{
			float num = Mathf.Max(nearPlane, HDShadowUtils.k_MinShadowNearPlane);
			return Matrix4x4.Ortho(-width / 2f, width / 2f, -height / 2f, height / 2f, num, range);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00032A94 File Offset: 0x00030C94
		private static Matrix4x4 ExtractSpotLightMatrix(VisibleLight vl, float spotAngle, float nearPlane, float guardAngle, float aspectRatio, out Matrix4x4 view, out Matrix4x4 proj, out Matrix4x4 deviceProj, out Matrix4x4 vpinverse, out Vector4 lightDir, out ShadowSplitData splitData)
		{
			splitData = default(ShadowSplitData);
			splitData.cullingSphere.Set(0f, 0f, 0f, float.NegativeInfinity);
			splitData.cullingPlaneCount = 0;
			lightDir = vl.light.transform.forward;
			Matrix4x4 identity = Matrix4x4.identity;
			identity.m22 = -1f;
			view = identity * vl.localToWorldMatrix.inverse;
			proj = HDShadowUtils.ExtractSpotLightProjectionMatrix(vl.range, spotAngle, nearPlane, aspectRatio, guardAngle);
			deviceProj = GL.GetGPUProjectionMatrix(proj, false);
			proj = GL.GetGPUProjectionMatrix(proj, true);
			HDShadowUtils.InvertPerspective(ref deviceProj, ref view, out vpinverse);
			return deviceProj * view;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00032B80 File Offset: 0x00030D80
		private static Matrix4x4 ExtractPointLightMatrix(VisibleLight vl, uint faceIdx, float nearPlane, float guardAngle, out Matrix4x4 view, out Matrix4x4 proj, out Matrix4x4 deviceProj, out Matrix4x4 vpinverse, out Vector4 lightDir, out ShadowSplitData splitData)
		{
			if (faceIdx > 5U)
			{
				Debug.LogError("Tried to extract cubemap face " + faceIdx + ".");
			}
			splitData = default(ShadowSplitData);
			splitData.cullingSphere.Set(0f, 0f, 0f, float.NegativeInfinity);
			lightDir = vl.light.transform.forward;
			Vector3 position = vl.light.transform.position;
			view = HDShadowUtils.kCubemapFaces[(int)faceIdx];
			Vector3 vector = HDShadowUtils.kCubemapFaces[(int)faceIdx].MultiplyPoint(-position);
			view.SetColumn(3, new Vector4(vector.x, vector.y, vector.z, 1f));
			float num = Mathf.Max(nearPlane, HDShadowUtils.k_MinShadowNearPlane);
			proj = Matrix4x4.Perspective(90f + guardAngle, 1f, num, vl.range);
			deviceProj = GL.GetGPUProjectionMatrix(proj, false);
			proj = GL.GetGPUProjectionMatrix(proj, true);
			HDShadowUtils.InvertPerspective(ref deviceProj, ref view, out vpinverse);
			GeometryUtility.CalculateFrustumPlanes(proj * view, HDShadowUtils.s_CachedPlanes);
			splitData.cullingPlaneCount = 6;
			for (int i = 0; i < 6; i++)
			{
				splitData.SetCullingPlane(i, HDShadowUtils.s_CachedPlanes[i]);
			}
			return deviceProj * view;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00032D10 File Offset: 0x00030F10
		private static float CalcGuardAnglePerspective(float angleInDeg, float resolution, float filterWidth, float normalBiasMax, float guardAngleMaxInDeg)
		{
			float num = angleInDeg * 0.5f * 0.017453292f;
			float num2 = 2f / resolution;
			float num3 = Mathf.Cos(num) * num2;
			float num4 = Mathf.Atan(normalBiasMax * num3 * 1.4142135f);
			num3 = Mathf.Tan(num + num4) * num2;
			num4 = Mathf.Atan((resolution + Mathf.Ceil(filterWidth)) * num3 * 0.5f) * 2f * 57.29578f - angleInDeg;
			num4 *= 2f;
			if (num4 >= guardAngleMaxInDeg)
			{
				return guardAngleMaxInDeg;
			}
			return num4;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00032D89 File Offset: 0x00030F89
		public static float GetSlopeBias(float baseBias, float normalizedSlopeBias)
		{
			return normalizedSlopeBias * baseBias;
		}

		// Token: 0x04000657 RID: 1623
		public static readonly float k_MinShadowNearPlane = 0.0001f;

		// Token: 0x04000658 RID: 1624
		public static readonly float k_MaxShadowNearPlane = 10f;

		// Token: 0x04000659 RID: 1625
		private static Plane[] s_CachedPlanes = new Plane[6];

		// Token: 0x0400065A RID: 1626
		public static readonly Matrix4x4[] kCubemapFaces = new Matrix4x4[]
		{
			new Matrix4x4(new Vector4(0f, 0f, -1f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(-1f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 1f)),
			new Matrix4x4(new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 1f)),
			new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 0f, -1f, 0f), new Vector4(0f, -1f, 0f, 0f), new Vector4(0f, 0f, 0f, 1f)),
			new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 0f, 1f)),
			new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, -1f, 0f), new Vector4(0f, 0f, 0f, 1f)),
			new Matrix4x4(new Vector4(-1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f))
		};
	}
}
