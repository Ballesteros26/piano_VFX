using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000066 RID: 102
	internal class TextureLerper
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0001020C File Offset: 0x0000E40C
		internal static TextureLerper instance
		{
			get
			{
				if (TextureLerper.m_Instance == null)
				{
					TextureLerper.m_Instance = new TextureLerper();
				}
				return TextureLerper.m_Instance;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00010224 File Offset: 0x0000E424
		private TextureLerper()
		{
			this.m_Recycled = new List<RenderTexture>();
			this.m_Actives = new List<RenderTexture>();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00010242 File Offset: 0x0000E442
		internal void BeginFrame(PostProcessRenderContext context)
		{
			this.m_Command = context.command;
			this.m_PropertySheets = context.propertySheets;
			this.m_Resources = context.resources;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00010268 File Offset: 0x0000E468
		internal void EndFrame()
		{
			if (this.m_Recycled.Count > 0)
			{
				foreach (RenderTexture renderTexture in this.m_Recycled)
				{
					RuntimeUtilities.Destroy(renderTexture);
				}
				this.m_Recycled.Clear();
			}
			if (this.m_Actives.Count > 0)
			{
				foreach (RenderTexture renderTexture2 in this.m_Actives)
				{
					this.m_Recycled.Add(renderTexture2);
				}
				this.m_Actives.Clear();
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00010334 File Offset: 0x0000E534
		private RenderTexture Get(RenderTextureFormat format, int w, int h, int d = 1, bool enableRandomWrite = false, bool force3D = false)
		{
			RenderTexture renderTexture = null;
			int count = this.m_Recycled.Count;
			int i;
			for (i = 0; i < count; i++)
			{
				RenderTexture renderTexture2 = this.m_Recycled[i];
				if (renderTexture2.width == w && renderTexture2.height == h && renderTexture2.volumeDepth == d && renderTexture2.format == format && renderTexture2.enableRandomWrite == enableRandomWrite && (!force3D || renderTexture2.dimension == TextureDimension.Tex3D))
				{
					renderTexture = renderTexture2;
					break;
				}
			}
			if (renderTexture == null)
			{
				TextureDimension textureDimension = ((d > 1 || force3D) ? TextureDimension.Tex3D : TextureDimension.Tex2D);
				renderTexture = new RenderTexture(w, h, 0, format)
				{
					dimension = textureDimension,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0,
					volumeDepth = d,
					enableRandomWrite = enableRandomWrite
				};
				renderTexture.Create();
			}
			else
			{
				this.m_Recycled.RemoveAt(i);
			}
			this.m_Actives.Add(renderTexture);
			return renderTexture;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0001041C File Offset: 0x0000E61C
		internal Texture Lerp(Texture from, Texture to, float t)
		{
			if (from == to)
			{
				return from;
			}
			if (t <= 0f)
			{
				return from;
			}
			if (t >= 1f)
			{
				return to;
			}
			RenderTexture renderTexture;
			if (from is Texture3D || (from is RenderTexture && ((RenderTexture)from).volumeDepth > 1))
			{
				int num = ((from is Texture3D) ? ((Texture3D)from).depth : ((RenderTexture)from).volumeDepth);
				int num2 = Mathf.Max(Mathf.Max(from.width, from.height), num);
				renderTexture = this.Get(RenderTextureFormat.ARGBHalf, from.width, from.height, num, true, true);
				ComputeShader texture3dLerp = this.m_Resources.computeShaders.texture3dLerp;
				int num3 = texture3dLerp.FindKernel("KTexture3DLerp");
				this.m_Command.SetComputeVectorParam(texture3dLerp, "_DimensionsAndLerp", new Vector4((float)from.width, (float)from.height, (float)num, t));
				this.m_Command.SetComputeTextureParam(texture3dLerp, num3, "_Output", renderTexture);
				this.m_Command.SetComputeTextureParam(texture3dLerp, num3, "_From", from);
				this.m_Command.SetComputeTextureParam(texture3dLerp, num3, "_To", to);
				uint num4;
				uint num5;
				uint num6;
				texture3dLerp.GetKernelThreadGroupSizes(num3, out num4, out num5, out num6);
				int num7 = Mathf.CeilToInt((float)num2 / num4);
				int num8 = Mathf.CeilToInt((float)num2 / num6);
				this.m_Command.DispatchCompute(texture3dLerp, num3, num7, num7, num8);
				return renderTexture;
			}
			RenderTextureFormat uncompressedRenderTextureFormat = TextureFormatUtilities.GetUncompressedRenderTextureFormat(to);
			renderTexture = this.Get(uncompressedRenderTextureFormat, to.width, to.height, 1, false, false);
			PropertySheet propertySheet = this.m_PropertySheets.Get(this.m_Resources.shaders.texture2dLerp);
			propertySheet.properties.SetTexture(ShaderIDs.To, to);
			propertySheet.properties.SetFloat(ShaderIDs.Interp, t);
			this.m_Command.BlitFullscreenTriangle(from, renderTexture, propertySheet, 0, false, null);
			return renderTexture;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00010618 File Offset: 0x0000E818
		internal Texture Lerp(Texture from, Color to, float t)
		{
			if ((double)t < 1E-05)
			{
				return from;
			}
			RenderTexture renderTexture;
			if (from is Texture3D || (from is RenderTexture && ((RenderTexture)from).volumeDepth > 1))
			{
				int num = ((from is Texture3D) ? ((Texture3D)from).depth : ((RenderTexture)from).volumeDepth);
				float num2 = (float)Mathf.Max(Mathf.Max(from.width, from.height), num);
				renderTexture = this.Get(RenderTextureFormat.ARGBHalf, from.width, from.height, num, true, true);
				ComputeShader texture3dLerp = this.m_Resources.computeShaders.texture3dLerp;
				int num3 = texture3dLerp.FindKernel("KTexture3DLerpToColor");
				this.m_Command.SetComputeVectorParam(texture3dLerp, "_DimensionsAndLerp", new Vector4((float)from.width, (float)from.height, (float)num, t));
				this.m_Command.SetComputeVectorParam(texture3dLerp, "_TargetColor", new Vector4(to.r, to.g, to.b, to.a));
				this.m_Command.SetComputeTextureParam(texture3dLerp, num3, "_Output", renderTexture);
				this.m_Command.SetComputeTextureParam(texture3dLerp, num3, "_From", from);
				int num4 = Mathf.CeilToInt(num2 / 4f);
				this.m_Command.DispatchCompute(texture3dLerp, num3, num4, num4, num4);
				return renderTexture;
			}
			RenderTextureFormat uncompressedRenderTextureFormat = TextureFormatUtilities.GetUncompressedRenderTextureFormat(from);
			renderTexture = this.Get(uncompressedRenderTextureFormat, from.width, from.height, 1, false, false);
			PropertySheet propertySheet = this.m_PropertySheets.Get(this.m_Resources.shaders.texture2dLerp);
			propertySheet.properties.SetVector(ShaderIDs.TargetColor, new Vector4(to.r, to.g, to.b, to.a));
			propertySheet.properties.SetFloat(ShaderIDs.Interp, t);
			this.m_Command.BlitFullscreenTriangle(from, renderTexture, propertySheet, 1, false, null);
			return renderTexture;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0001081C File Offset: 0x0000EA1C
		internal void Clear()
		{
			foreach (RenderTexture renderTexture in this.m_Actives)
			{
				RuntimeUtilities.Destroy(renderTexture);
			}
			foreach (RenderTexture renderTexture2 in this.m_Recycled)
			{
				RuntimeUtilities.Destroy(renderTexture2);
			}
			this.m_Actives.Clear();
			this.m_Recycled.Clear();
		}

		// Token: 0x04000241 RID: 577
		private static TextureLerper m_Instance;

		// Token: 0x04000242 RID: 578
		private CommandBuffer m_Command;

		// Token: 0x04000243 RID: 579
		private PropertySheetFactory m_PropertySheets;

		// Token: 0x04000244 RID: 580
		private PostProcessResources m_Resources;

		// Token: 0x04000245 RID: 581
		private List<RenderTexture> m_Recycled;

		// Token: 0x04000246 RID: 582
		private List<RenderTexture> m_Actives;
	}
}
