using System;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public struct CreationParameters
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002218 File Offset: 0x00000418
		internal void Validate()
		{
			bool flag = this.width <= 0 || this.height <= 0 || this.tilesize <= 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Zero sized dimensions are invalid (width: {0}, height: {1}, tilesize {2}", this.width, this.height, this.tilesize));
			}
			bool flag2 = this.layers == null || this.layers.Length > 4;
			if (flag2)
			{
				throw new ArgumentException(string.Format("layers is either invalid or has to many layers (maxNumLayers: {0})", 4));
			}
			GraphicsFormat[] array = new GraphicsFormat[]
			{
				GraphicsFormat.R8G8B8A8_SRGB,
				GraphicsFormat.R8G8B8A8_UNorm,
				GraphicsFormat.R32G32B32A32_SFloat,
				GraphicsFormat.R8G8_SRGB,
				GraphicsFormat.R8G8_UNorm,
				GraphicsFormat.R32_SFloat,
				GraphicsFormat.A2B10G10R10_UNormPack32
			};
			for (int i = 0; i < this.layers.Length; i++)
			{
				bool flag3 = false;
				for (int j = 0; j < array.Length; j++)
				{
					bool flag4 = this.layers[i] == array[j];
					if (flag4)
					{
						flag3 = true;
						break;
					}
				}
				bool flag5 = !flag3;
				if (flag5)
				{
					throw new ArgumentException(string.Format("Invalid textureformat on layer: {0}. Supported formats are: {1}", i, array));
				}
			}
			bool flag6 = this.maxActiveRequests > 4095 || this.maxActiveRequests <= 0;
			if (flag6)
			{
				throw new ArgumentException(string.Format("Invalid requests per frame (maxActiveRequests: ]0, {0}])", this.maxActiveRequests));
			}
		}

		// Token: 0x04000016 RID: 22
		public const int MaxNumLayers = 4;

		// Token: 0x04000017 RID: 23
		public const int MaxRequestsPerFrameSupported = 4095;

		// Token: 0x04000018 RID: 24
		public int width;

		// Token: 0x04000019 RID: 25
		public int height;

		// Token: 0x0400001A RID: 26
		public int maxActiveRequests;

		// Token: 0x0400001B RID: 27
		public int tilesize;

		// Token: 0x0400001C RID: 28
		public GraphicsFormat[] layers;

		// Token: 0x0400001D RID: 29
		internal int borderSize;

		// Token: 0x0400001E RID: 30
		internal int gpuGeneration;
	}
}
