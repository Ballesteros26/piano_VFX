using System;
using UnityEngine.Bindings;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/VR/HoloLens/PerceptionRemoting.h")]
	public enum HolographicStreamerConnectionFailureReason
	{
		// Token: 0x04000040 RID: 64
		None,
		// Token: 0x04000041 RID: 65
		Unknown,
		// Token: 0x04000042 RID: 66
		Unreachable,
		// Token: 0x04000043 RID: 67
		HandshakeFailed,
		// Token: 0x04000044 RID: 68
		ProtocolVersionMismatch,
		// Token: 0x04000045 RID: 69
		ConnectionLost
	}
}
