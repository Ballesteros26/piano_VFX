using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x02000012 RID: 18
	[UsedByNativeCode]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public struct GPUTextureStackRequestParameters
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002694 File Offset: 0x00000894
		public GPUTextureStackRequestLayerParameters GetLayer(int index)
		{
			GPUTextureStackRequestLayerParameters gputextureStackRequestLayerParameters;
			switch (index)
			{
			case 0:
				gputextureStackRequestLayerParameters = this.layer0;
				break;
			case 1:
				gputextureStackRequestLayerParameters = this.layer1;
				break;
			case 2:
				gputextureStackRequestLayerParameters = this.layer2;
				break;
			case 3:
				gputextureStackRequestLayerParameters = this.layer3;
				break;
			default:
				throw new IndexOutOfRangeException();
			}
			return gputextureStackRequestLayerParameters;
		}

		// Token: 0x04000029 RID: 41
		public int level;

		// Token: 0x0400002A RID: 42
		public int x;

		// Token: 0x0400002B RID: 43
		public int y;

		// Token: 0x0400002C RID: 44
		public int width;

		// Token: 0x0400002D RID: 45
		public int height;

		// Token: 0x0400002E RID: 46
		public int numLayers;

		// Token: 0x0400002F RID: 47
		private GPUTextureStackRequestLayerParameters layer0;

		// Token: 0x04000030 RID: 48
		private GPUTextureStackRequestLayerParameters layer1;

		// Token: 0x04000031 RID: 49
		private GPUTextureStackRequestLayerParameters layer2;

		// Token: 0x04000032 RID: 50
		private GPUTextureStackRequestLayerParameters layer3;
	}
}
