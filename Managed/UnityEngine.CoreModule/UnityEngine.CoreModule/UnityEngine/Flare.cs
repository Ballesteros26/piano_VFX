using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000FE RID: 254
	[NativeHeader("Runtime/Camera/Flare.h")]
	public sealed class Flare : Object
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0000F370 File Offset: 0x0000D570
		public Flare()
		{
			Flare.Internal_Create(this);
		}

		// Token: 0x06000B37 RID: 2871
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] Flare self);
	}
}
