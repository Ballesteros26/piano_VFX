using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public sealed class VectorscopeMonitor : Monitor
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00008DC3 File Offset: 0x00006FC3
		internal override void OnDisable()
		{
			base.OnDisable();
			if (this.m_Data != null)
			{
				this.m_Data.Release();
			}
			this.m_Data = null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004DDA File Offset: 0x00002FDA
		internal override bool NeedsHalfRes()
		{
			return true;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00008DE5 File Offset: 0x00006FE5
		internal override bool ShaderResourcesAvailable(PostProcessRenderContext context)
		{
			return context.resources.computeShaders.vectorscope;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008DFC File Offset: 0x00006FFC
		internal override void Render(PostProcessRenderContext context)
		{
			base.CheckOutput(this.size, this.size);
			this.exposure = Mathf.Max(0f, this.exposure);
			int num = this.size * this.size;
			if (this.m_Data == null)
			{
				this.m_Data = new ComputeBuffer(num, 4);
			}
			else if (this.m_Data.count != num)
			{
				this.m_Data.Release();
				this.m_Data = new ComputeBuffer(num, 4);
			}
			ComputeShader vectorscope = context.resources.computeShaders.vectorscope;
			CommandBuffer command = context.command;
			command.BeginSample("Vectorscope");
			Vector4 vector = new Vector4((float)(context.width / 2), (float)(context.height / 2), (float)this.size, (float)(RuntimeUtilities.isLinearColorSpace ? 1 : 0));
			int num2 = vectorscope.FindKernel("KVectorscopeClear");
			command.SetComputeBufferParam(vectorscope, num2, "_VectorscopeBuffer", this.m_Data);
			command.SetComputeVectorParam(vectorscope, "_Params", vector);
			command.DispatchCompute(vectorscope, num2, Mathf.CeilToInt((float)this.size / 16f), Mathf.CeilToInt((float)this.size / 16f), 1);
			num2 = vectorscope.FindKernel("KVectorscopeGather");
			command.SetComputeBufferParam(vectorscope, num2, "_VectorscopeBuffer", this.m_Data);
			command.SetComputeTextureParam(vectorscope, num2, "_Source", ShaderIDs.HalfResFinalCopy);
			command.DispatchCompute(vectorscope, num2, Mathf.CeilToInt(vector.x / 16f), Mathf.CeilToInt(vector.y / 16f), 1);
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.vectorscope);
			propertySheet.properties.SetVector(ShaderIDs.Params, new Vector4((float)this.size, (float)this.size, this.exposure, 0f));
			propertySheet.properties.SetBuffer(ShaderIDs.VectorscopeBuffer, this.m_Data);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, base.output, propertySheet, 0, false, null);
			command.EndSample("Vectorscope");
		}

		// Token: 0x040000ED RID: 237
		public int size = 256;

		// Token: 0x040000EE RID: 238
		public float exposure = 0.12f;

		// Token: 0x040000EF RID: 239
		private ComputeBuffer m_Data;

		// Token: 0x040000F0 RID: 240
		private const int k_ThreadGroupSizeX = 16;

		// Token: 0x040000F1 RID: 241
		private const int k_ThreadGroupSizeY = 16;
	}
}
