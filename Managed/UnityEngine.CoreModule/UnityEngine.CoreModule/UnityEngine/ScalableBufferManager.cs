using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000E0 RID: 224
	[StaticAccessor("ScalableBufferManager::GetInstance()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/GfxDevice/ScalableBufferManager.h")]
	public static class ScalableBufferManager
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000772 RID: 1906
		public static extern float widthScaleFactor
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000773 RID: 1907
		public static extern float heightScaleFactor
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000774 RID: 1908
		[MethodImpl(4096)]
		public static extern void ResizeBuffers(float widthScale, float heightScale);
	}
}
