using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003D4 RID: 980
	public static class GraphicsDeviceSettings
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060021E0 RID: 8672
		// (set) Token: 0x060021E1 RID: 8673
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern WaitForPresentSyncPoint waitForPresentSyncPoint
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060021E2 RID: 8674
		// (set) Token: 0x060021E3 RID: 8675
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern GraphicsJobsSyncPoint graphicsJobsSyncPoint
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
