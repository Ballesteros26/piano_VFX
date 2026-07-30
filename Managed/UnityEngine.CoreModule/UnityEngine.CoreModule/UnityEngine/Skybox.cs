using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000104 RID: 260
	[NativeHeader("Runtime/Camera/Skybox.h")]
	public sealed class Skybox : Behaviour
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000BB2 RID: 2994
		// (set) Token: 0x06000BB3 RID: 2995
		public extern Material material
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
