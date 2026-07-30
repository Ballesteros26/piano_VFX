using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000014 RID: 20
	internal class EncodeBC6H
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000031D4 File Offset: 0x000013D4
		public EncodeBC6H(ComputeShader shader)
		{
			this.m_Shader = shader;
			this.m_KEncodeFastCubemapMip = this.m_Shader.FindKernel("KEncodeFastCubemapMip");
			uint num;
			uint num2;
			uint num3;
			this.m_Shader.GetKernelThreadGroupSizes(this.m_KEncodeFastCubemapMip, out num, out num2, out num3);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000321C File Offset: 0x0000141C
		public void EncodeFastCubemap(CommandBuffer cmb, RenderTargetIdentifier source, int sourceSize, RenderTargetIdentifier target, int fromMip, int toMip, int targetArrayIndex = 0)
		{
			int num = Mathf.Max(0, (int)(Mathf.Log((float)sourceSize) / Mathf.Log(2f)) - 2);
			int num2 = Mathf.Clamp(fromMip, 0, num);
			int num3 = Mathf.Min(num, Mathf.Max(toMip, num2));
			RenderTextureDescriptor renderTextureDescriptor = new RenderTextureDescriptor
			{
				autoGenerateMips = false,
				bindMS = false,
				colorFormat = RenderTextureFormat.ARGBInt,
				depthBufferBits = 0,
				dimension = TextureDimension.Tex2DArray,
				enableRandomWrite = true,
				msaaSamples = 1,
				volumeDepth = 6,
				sRGB = false,
				useMipMap = false
			};
			cmb.SetComputeTextureParam(this.m_Shader, this.m_KEncodeFastCubemapMip, EncodeBC6H._Source, source);
			for (int i = num2; i <= num3; i++)
			{
				int num4 = sourceSize >> i >> 2;
				renderTextureDescriptor.width = num4;
				renderTextureDescriptor.height = num4;
				cmb.GetTemporaryRT(EncodeBC6H.__Tmp_RT[i], renderTextureDescriptor);
			}
			for (int j = num2; j <= num3; j++)
			{
				int num5 = sourceSize >> j >> 2;
				cmb.SetComputeTextureParam(this.m_Shader, this.m_KEncodeFastCubemapMip, EncodeBC6H._Target, EncodeBC6H.__Tmp_RT[j]);
				cmb.SetComputeIntParam(this.m_Shader, EncodeBC6H._MipIndex, j);
				cmb.DispatchCompute(this.m_Shader, this.m_KEncodeFastCubemapMip, num5, num5, 6);
			}
			int num6 = 6 * targetArrayIndex;
			for (int k = num2; k <= num3; k++)
			{
				int num7 = Mathf.Clamp(k, num2, num3);
				for (int l = 0; l < 6; l++)
				{
					cmb.CopyTexture(EncodeBC6H.__Tmp_RT[num7], l, 0, target, num6 + l, k);
				}
			}
			for (int m = num2; m <= num3; m++)
			{
				cmb.ReleaseTemporaryRT(EncodeBC6H.__Tmp_RT[m]);
			}
		}

		// Token: 0x04000053 RID: 83
		public static EncodeBC6H DefaultInstance;

		// Token: 0x04000054 RID: 84
		private static readonly int _Source = Shader.PropertyToID("_Source");

		// Token: 0x04000055 RID: 85
		private static readonly int _Target = Shader.PropertyToID("_Target");

		// Token: 0x04000056 RID: 86
		private static readonly int _MipIndex = Shader.PropertyToID("_MipIndex");

		// Token: 0x04000057 RID: 87
		private static readonly int[] __Tmp_RT = new int[]
		{
			Shader.PropertyToID("__Tmp_RT0"),
			Shader.PropertyToID("__Tmp_RT1"),
			Shader.PropertyToID("__Tmp_RT2"),
			Shader.PropertyToID("__Tmp_RT3"),
			Shader.PropertyToID("__Tmp_RT4"),
			Shader.PropertyToID("__Tmp_RT5"),
			Shader.PropertyToID("__Tmp_RT6"),
			Shader.PropertyToID("__Tmp_RT7"),
			Shader.PropertyToID("__Tmp_RT8"),
			Shader.PropertyToID("__Tmp_RT9"),
			Shader.PropertyToID("__Tmp_RT10"),
			Shader.PropertyToID("__Tmp_RT11"),
			Shader.PropertyToID("__Tmp_RT12"),
			Shader.PropertyToID("__Tmp_RT13")
		};

		// Token: 0x04000058 RID: 88
		private readonly ComputeShader m_Shader;

		// Token: 0x04000059 RID: 89
		private readonly int m_KEncodeFastCubemapMip;
	}
}
