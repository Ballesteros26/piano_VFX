using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000002 RID: 2
	[StaticAccessor("VirtualTexturing::System", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public static class System
	{
		// Token: 0x06000001 RID: 1
		[MethodImpl(4096)]
		public static extern void Update();

		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		[NativeThrows]
		public static void RequestRegion([NotNull] Material mat, int stackNameId, Rect r, int mipMap, int numMips)
		{
			UnityEngine.Rendering.VirtualTexturing.System.RequestRegion_Injected(mat, stackNameId, ref r, mipMap, numMips);
		}

		// Token: 0x06000003 RID: 3
		[NativeThrows]
		[MethodImpl(4096)]
		public static extern void GetTextureStackSize([NotNull] Material mat, int stackNameId, out int width, out int height);

		// Token: 0x06000004 RID: 4
		[NativeThrows]
		[MethodImpl(4096)]
		public static extern void ApplyVirtualTexturingSettings(VirtualTexturingSettings settings);

		// Token: 0x06000005 RID: 5
		[MethodImpl(4096)]
		private static extern void RequestRegion_Injected(Material mat, int stackNameId, ref Rect r, int mipMap, int numMips);

		// Token: 0x04000001 RID: 1
		public const int AllMips = 2147483647;
	}
}
