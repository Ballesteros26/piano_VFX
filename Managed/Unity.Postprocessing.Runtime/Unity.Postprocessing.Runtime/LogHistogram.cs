using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005D RID: 93
	internal sealed class LogHistogram
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000E16E File Offset: 0x0000C36E
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000E176 File Offset: 0x0000C376
		public ComputeBuffer data { get; private set; }

		// Token: 0x060001BD RID: 445 RVA: 0x0000E180 File Offset: 0x0000C380
		public void Generate(PostProcessRenderContext context)
		{
			if (this.data == null)
			{
				this.data = new ComputeBuffer(128, 4);
			}
			Vector4 histogramScaleOffsetRes = this.GetHistogramScaleOffsetRes(context);
			ComputeShader exposureHistogram = context.resources.computeShaders.exposureHistogram;
			CommandBuffer command = context.command;
			command.BeginSample("LogHistogram");
			int num = exposureHistogram.FindKernel("KEyeHistogramClear");
			command.SetComputeBufferParam(exposureHistogram, num, "_HistogramBuffer", this.data);
			uint num2;
			uint num3;
			uint num4;
			exposureHistogram.GetKernelThreadGroupSizes(num, out num2, out num3, out num4);
			command.DispatchCompute(exposureHistogram, num, Mathf.CeilToInt(128f / num2), 1, 1);
			num = exposureHistogram.FindKernel("KEyeHistogram");
			command.SetComputeBufferParam(exposureHistogram, num, "_HistogramBuffer", this.data);
			command.SetComputeTextureParam(exposureHistogram, num, "_Source", context.source);
			command.SetComputeVectorParam(exposureHistogram, "_ScaleOffsetRes", histogramScaleOffsetRes);
			exposureHistogram.GetKernelThreadGroupSizes(num, out num2, out num3, out num4);
			command.DispatchCompute(exposureHistogram, num, Mathf.CeilToInt(histogramScaleOffsetRes.z / 2f / num2), Mathf.CeilToInt(histogramScaleOffsetRes.w / 2f / num3), 1);
			command.EndSample("LogHistogram");
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		public Vector4 GetHistogramScaleOffsetRes(PostProcessRenderContext context)
		{
			float num = 18f;
			float num2 = 1f / num;
			float num3 = 9f * num2;
			return new Vector4(num2, num3, (float)context.width, (float)context.height);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public void Release()
		{
			if (this.data != null)
			{
				this.data.Release();
			}
			this.data = null;
		}

		// Token: 0x04000198 RID: 408
		public const int rangeMin = -9;

		// Token: 0x04000199 RID: 409
		public const int rangeMax = 9;

		// Token: 0x0400019A RID: 410
		private const int k_Bins = 128;
	}
}
