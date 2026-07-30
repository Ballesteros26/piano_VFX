using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000FC RID: 252
	[NativeHeader("Runtime/Camera/OcclusionPortal.h")]
	public sealed class OcclusionPortal : Component
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B2A RID: 2858
		// (set) Token: 0x06000B2B RID: 2859
		[NativeProperty("IsOpen")]
		public extern bool open
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
