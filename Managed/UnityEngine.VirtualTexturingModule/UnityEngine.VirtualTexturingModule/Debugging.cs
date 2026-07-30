using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[StaticAccessor("VirtualTexturing::Debugging", StaticAccessorType.DoubleColon)]
	public static class Debugging
	{
		// Token: 0x06000009 RID: 9
		[MethodImpl(4096)]
		public static extern int GetNumHandles();

		// Token: 0x0600000A RID: 10
		[MethodImpl(4096)]
		public static extern void GrabHandleInfo(out Debugging.Handle debugHandle, int index);

		// Token: 0x0600000B RID: 11
		[MethodImpl(4096)]
		public static extern string GetInfoDump();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12
		// (set) Token: 0x0600000D RID: 13
		public static extern bool debugTilesEnabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14
		// (set) Token: 0x0600000F RID: 15
		public static extern bool resolvingEnabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16
		// (set) Token: 0x06000011 RID: 17
		public static extern bool flushEveryTickEnabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x02000005 RID: 5
		[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingDebugHandle.h")]
		[UsedByNativeCode]
		public struct Handle
		{
			// Token: 0x04000002 RID: 2
			public long handle;

			// Token: 0x04000003 RID: 3
			public string group;

			// Token: 0x04000004 RID: 4
			public string name;

			// Token: 0x04000005 RID: 5
			public int numLayers;

			// Token: 0x04000006 RID: 6
			public Material material;
		}
	}
}
