using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x02000013 RID: 19
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[UsedByNativeCode]
	public struct CPUTextureStackRequestParameters
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000026E8 File Offset: 0x000008E8
		public CPUTextureStackRequestLayerParameters GetLayer(int index)
		{
			CPUTextureStackRequestLayerParameters cputextureStackRequestLayerParameters;
			switch (index)
			{
			case 0:
				cputextureStackRequestLayerParameters = this.layer0;
				break;
			case 1:
				cputextureStackRequestLayerParameters = this.layer1;
				break;
			case 2:
				cputextureStackRequestLayerParameters = this.layer2;
				break;
			case 3:
				cputextureStackRequestLayerParameters = this.layer3;
				break;
			default:
				throw new IndexOutOfRangeException();
			}
			return cputextureStackRequestLayerParameters;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000273C File Offset: 0x0000093C
		public void CopyPixelDataToLayer<T>(NativeArray<T> colorData, int layerIdx) where T : struct
		{
			CPUTextureStackRequestLayerParameters layer = this.GetLayer(layerIdx);
			NativeArray<T> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(layer.data, layer.dataSize, Allocator.Temp);
			int num = layer.scanlineSize / UnsafeUtility.SizeOf<T>();
			for (int i = 0; i < this.height; i++)
			{
				NativeArray<T>.Copy(colorData, i * this.width, nativeArray, i * num, this.width);
			}
			nativeArray.Dispose();
		}

		// Token: 0x04000033 RID: 51
		public int level;

		// Token: 0x04000034 RID: 52
		public int x;

		// Token: 0x04000035 RID: 53
		public int y;

		// Token: 0x04000036 RID: 54
		public int width;

		// Token: 0x04000037 RID: 55
		public int height;

		// Token: 0x04000038 RID: 56
		public int numLayers;

		// Token: 0x04000039 RID: 57
		private CPUTextureStackRequestLayerParameters layer0;

		// Token: 0x0400003A RID: 58
		private CPUTextureStackRequestLayerParameters layer1;

		// Token: 0x0400003B RID: 59
		private CPUTextureStackRequestLayerParameters layer2;

		// Token: 0x0400003C RID: 60
		private CPUTextureStackRequestLayerParameters layer3;
	}
}
