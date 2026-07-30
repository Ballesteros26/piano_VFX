using System;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	// Token: 0x02000020 RID: 32
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	public struct XRMirrorViewBlitModeDesc
	{
		// Token: 0x040000D3 RID: 211
		public int blitMode;

		// Token: 0x040000D4 RID: 212
		public string blitModeDesc;
	}
}
