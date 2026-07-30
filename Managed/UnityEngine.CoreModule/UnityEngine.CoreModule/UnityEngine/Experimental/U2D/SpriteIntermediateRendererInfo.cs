using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x020003B2 RID: 946
	[RequiredByNativeCode]
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	internal struct SpriteIntermediateRendererInfo
	{
		// Token: 0x04000BBB RID: 3003
		public int SpriteID;

		// Token: 0x04000BBC RID: 3004
		public int TextureID;

		// Token: 0x04000BBD RID: 3005
		public int MaterialID;

		// Token: 0x04000BBE RID: 3006
		public Color Color;

		// Token: 0x04000BBF RID: 3007
		public Matrix4x4 Transform;

		// Token: 0x04000BC0 RID: 3008
		public Bounds Bounds;

		// Token: 0x04000BC1 RID: 3009
		public int Layer;

		// Token: 0x04000BC2 RID: 3010
		public int SortingLayer;

		// Token: 0x04000BC3 RID: 3011
		public int SortingOrder;

		// Token: 0x04000BC4 RID: 3012
		public ulong SceneCullingMask;

		// Token: 0x04000BC5 RID: 3013
		public IntPtr IndexData;

		// Token: 0x04000BC6 RID: 3014
		public IntPtr VertexData;

		// Token: 0x04000BC7 RID: 3015
		public int IndexCount;

		// Token: 0x04000BC8 RID: 3016
		public int VertexCount;

		// Token: 0x04000BC9 RID: 3017
		public int ShaderChannelMask;
	}
}
