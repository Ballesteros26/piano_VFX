using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public sealed class HistogramMonitor : Monitor
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000088D3 File Offset: 0x00006AD3
		internal override void OnDisable()
		{
			base.OnDisable();
			if (this.m_Data != null)
			{
				this.m_Data.Release();
			}
			this.m_Data = null;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004DDA File Offset: 0x00002FDA
		internal override bool NeedsHalfRes()
		{
			return true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000088F5 File Offset: 0x00006AF5
		internal override bool ShaderResourcesAvailable(PostProcessRenderContext context)
		{
			return context.resources.computeShaders.gammaHistogram;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000890C File Offset: 0x00006B0C
		internal override void Render(PostProcessRenderContext context)
		{
			base.CheckOutput(this.width, this.height);
			if (this.m_Data == null)
			{
				this.m_Data = new ComputeBuffer(256, 4);
			}
			ComputeShader gammaHistogram = context.resources.computeShaders.gammaHistogram;
			CommandBuffer command = context.command;
			command.BeginSample("GammaHistogram");
			int num = gammaHistogram.FindKernel("KHistogramClear");
			command.SetComputeBufferParam(gammaHistogram, num, "_HistogramBuffer", this.m_Data);
			command.DispatchCompute(gammaHistogram, num, Mathf.CeilToInt(16f), 1, 1);
			num = gammaHistogram.FindKernel("KHistogramGather");
			Vector4 vector = new Vector4((float)(context.width / 2), (float)(context.height / 2), (float)(RuntimeUtilities.isLinearColorSpace ? 1 : 0), (float)this.channel);
			command.SetComputeVectorParam(gammaHistogram, "_Params", vector);
			command.SetComputeTextureParam(gammaHistogram, num, "_Source", ShaderIDs.HalfResFinalCopy);
			command.SetComputeBufferParam(gammaHistogram, num, "_HistogramBuffer", this.m_Data);
			command.DispatchCompute(gammaHistogram, num, Mathf.CeilToInt(vector.x / 16f), Mathf.CeilToInt(vector.y / 16f), 1);
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.gammaHistogram);
			propertySheet.properties.SetVector(ShaderIDs.Params, new Vector4((float)this.width, (float)this.height, 0f, 0f));
			propertySheet.properties.SetBuffer(ShaderIDs.HistogramBuffer, this.m_Data);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, base.output, propertySheet, 0, false, null);
			command.EndSample("GammaHistogram");
		}

		// Token: 0x040000DC RID: 220
		public int width = 512;

		// Token: 0x040000DD RID: 221
		public int height = 256;

		// Token: 0x040000DE RID: 222
		public HistogramMonitor.Channel channel = HistogramMonitor.Channel.Master;

		// Token: 0x040000DF RID: 223
		private ComputeBuffer m_Data;

		// Token: 0x040000E0 RID: 224
		private const int k_NumBins = 256;

		// Token: 0x040000E1 RID: 225
		private const int k_ThreadGroupSizeX = 16;

		// Token: 0x040000E2 RID: 226
		private const int k_ThreadGroupSizeY = 16;

		// Token: 0x02000076 RID: 118
		public enum Channel
		{
			// Token: 0x04000299 RID: 665
			Red,
			// Token: 0x0400029A RID: 666
			Green,
			// Token: 0x0400029B RID: 667
			Blue,
			// Token: 0x0400029C RID: 668
			Master
		}
	}
}
