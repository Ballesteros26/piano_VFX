using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A7 RID: 167
	internal struct VBufferParameters
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x00033EF4 File Offset: 0x000320F4
		public VBufferParameters(Vector3Int viewportResolution, float depthExtent, float camNear, float camFar, float camVFoV, float sliceDistributionUniformity)
		{
			this.viewportSize = viewportResolution;
			float num = (float)viewportResolution.x / (float)viewportResolution.y;
			float num2 = 2f * Mathf.Tan(0.5f * camVFoV) * camFar;
			float num3 = Mathf.Max(num2 * num, num2);
			float num4 = Mathf.Sqrt(camFar * camFar + 0.25f * num3 * num3);
			float num5 = Math.Min(camNear + depthExtent, num4);
			float num6 = 2f - 2f * sliceDistributionUniformity;
			num6 = Mathf.Max(num6, 0.001f);
			this.depthEncodingParams = VBufferParameters.ComputeLogarithmicDepthEncodingParams(camNear, num5, num6);
			this.depthDecodingParams = VBufferParameters.ComputeLogarithmicDepthDecodingParams(camNear, num5, num6);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00033F9F File Offset: 0x0003219F
		internal Vector4 ComputeUvScaleAndLimit(Vector2Int bufferSize)
		{
			return HDUtils.ComputeUvScaleAndLimit(new Vector2Int(this.viewportSize.x, this.viewportSize.y), bufferSize);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00033FC4 File Offset: 0x000321C4
		internal float ComputeLastSliceDistance(int sliceCount)
		{
			float num = 1f - 0.5f / (float)sliceCount;
			float num2 = 0.6931472f;
			return this.depthDecodingParams.x * Mathf.Exp(num2 * num * this.depthDecodingParams.y) + this.depthDecodingParams.z;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00034014 File Offset: 0x00032214
		private static Vector4 ComputeLogarithmicDepthEncodingParams(float nearPlane, float farPlane, float c)
		{
			Vector4 vector = default(Vector4);
			vector.y = 1f / Mathf.Log(c * (farPlane - nearPlane) + 1f, 2f);
			vector.x = Mathf.Log(c, 2f) * vector.y;
			vector.z = nearPlane - 1f / c;
			vector.w = 0f;
			return vector;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00034088 File Offset: 0x00032288
		private static Vector4 ComputeLogarithmicDepthDecodingParams(float nearPlane, float farPlane, float c)
		{
			return new Vector4
			{
				x = 1f / c,
				y = Mathf.Log(c * (farPlane - nearPlane) + 1f, 2f),
				z = nearPlane - 1f / c,
				w = 0f
			};
		}

		// Token: 0x0400069D RID: 1693
		public Vector3Int viewportSize;

		// Token: 0x0400069E RID: 1694
		public Vector4 depthEncodingParams;

		// Token: 0x0400069F RID: 1695
		public Vector4 depthDecodingParams;
	}
}
