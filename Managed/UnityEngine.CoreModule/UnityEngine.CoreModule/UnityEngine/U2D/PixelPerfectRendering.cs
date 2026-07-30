using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x0200020A RID: 522
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeHeader("Runtime/2D/Common/PixelSnapping.h")]
	public static class PixelPerfectRendering
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001736 RID: 5942
		// (set) Token: 0x06001737 RID: 5943
		public static extern float pixelSnapSpacing
		{
			[FreeFunction("GetPixelSnapSpacing")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("SetPixelSnapSpacing")]
			[MethodImpl(4096)]
			set;
		}
	}
}
