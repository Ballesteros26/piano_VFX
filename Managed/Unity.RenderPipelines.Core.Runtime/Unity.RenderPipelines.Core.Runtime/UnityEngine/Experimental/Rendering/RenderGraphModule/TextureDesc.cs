using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000016 RID: 22
	public struct TextureDesc
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003476 File Offset: 0x00001676
		private void InitDefaultValues(bool dynamicResolution, bool xrReady)
		{
			this.useDynamicScale = dynamicResolution;
			if (xrReady)
			{
				this.slices = TextureXR.slices;
				this.dimension = TextureXR.dimension;
				return;
			}
			this.slices = 1;
			this.dimension = TextureDimension.Tex2D;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034A7 File Offset: 0x000016A7
		public TextureDesc(int width, int height, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Explicit;
			this.width = width;
			this.height = height;
			this.msaaSamples = MSAASamples.None;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034D5 File Offset: 0x000016D5
		public TextureDesc(Vector2 scale, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Scale;
			this.scale = scale;
			this.msaaSamples = MSAASamples.None;
			this.dimension = TextureDimension.Tex2D;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003502 File Offset: 0x00001702
		public TextureDesc(ScaleFunc func, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Functor;
			this.func = func;
			this.msaaSamples = MSAASamples.None;
			this.dimension = TextureDimension.Tex2D;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000352F File Offset: 0x0000172F
		public TextureDesc(TextureDesc input)
		{
			this = input;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003538 File Offset: 0x00001738
		public override int GetHashCode()
		{
			int num = 17;
			switch (this.sizeMode)
			{
			case TextureSizeMode.Explicit:
				num = num * 23 + this.width;
				num = num * 23 + this.height;
				num = (int)(num * 23 + this.msaaSamples);
				break;
			case TextureSizeMode.Scale:
				num = num * 23 + this.scale.x.GetHashCode();
				num = num * 23 + this.scale.y.GetHashCode();
				num = num * 23 + (this.enableMSAA ? 1 : 0);
				break;
			case TextureSizeMode.Functor:
				if (this.func != null)
				{
					num = num * 23 + this.func.GetHashCode();
				}
				num = num * 23 + (this.enableMSAA ? 1 : 0);
				break;
			}
			num = num * 23 + this.mipMapBias.GetHashCode();
			num = num * 23 + this.slices;
			num = (int)(num * 23 + this.depthBufferBits);
			num = (int)(num * 23 + this.colorFormat);
			num = (int)(num * 23 + this.filterMode);
			num = (int)(num * 23 + this.wrapMode);
			num = (int)(num * 23 + this.dimension);
			num = (int)(num * 23 + this.memoryless);
			num = num * 23 + this.anisoLevel;
			num = num * 23 + (this.enableRandomWrite ? 1 : 0);
			num = num * 23 + (this.useMipMap ? 1 : 0);
			num = num * 23 + (this.autoGenerateMips ? 1 : 0);
			num = num * 23 + (this.isShadowMap ? 1 : 0);
			num = num * 23 + (this.bindTextureMS ? 1 : 0);
			return num * 23 + (this.useDynamicScale ? 1 : 0);
		}

		// Token: 0x04000050 RID: 80
		public TextureSizeMode sizeMode;

		// Token: 0x04000051 RID: 81
		public int width;

		// Token: 0x04000052 RID: 82
		public int height;

		// Token: 0x04000053 RID: 83
		public int slices;

		// Token: 0x04000054 RID: 84
		public Vector2 scale;

		// Token: 0x04000055 RID: 85
		public ScaleFunc func;

		// Token: 0x04000056 RID: 86
		public DepthBits depthBufferBits;

		// Token: 0x04000057 RID: 87
		public GraphicsFormat colorFormat;

		// Token: 0x04000058 RID: 88
		public FilterMode filterMode;

		// Token: 0x04000059 RID: 89
		public TextureWrapMode wrapMode;

		// Token: 0x0400005A RID: 90
		public TextureDimension dimension;

		// Token: 0x0400005B RID: 91
		public bool enableRandomWrite;

		// Token: 0x0400005C RID: 92
		public bool useMipMap;

		// Token: 0x0400005D RID: 93
		public bool autoGenerateMips;

		// Token: 0x0400005E RID: 94
		public bool isShadowMap;

		// Token: 0x0400005F RID: 95
		public int anisoLevel;

		// Token: 0x04000060 RID: 96
		public float mipMapBias;

		// Token: 0x04000061 RID: 97
		public bool enableMSAA;

		// Token: 0x04000062 RID: 98
		public MSAASamples msaaSamples;

		// Token: 0x04000063 RID: 99
		public bool bindTextureMS;

		// Token: 0x04000064 RID: 100
		public bool useDynamicScale;

		// Token: 0x04000065 RID: 101
		public RenderTextureMemoryless memoryless;

		// Token: 0x04000066 RID: 102
		public string name;

		// Token: 0x04000067 RID: 103
		public bool clearBuffer;

		// Token: 0x04000068 RID: 104
		public Color clearColor;
	}
}
