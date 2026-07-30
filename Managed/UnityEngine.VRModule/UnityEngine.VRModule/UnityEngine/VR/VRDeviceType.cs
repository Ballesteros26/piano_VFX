using System;
using System.ComponentModel;

namespace UnityEngine.VR
{
	// Token: 0x02000021 RID: 33
	[Obsolete("VRDeviceType is deprecated. Use XRSettings.supportedDevices instead.", true)]
	public enum VRDeviceType
	{
		// Token: 0x04000052 RID: 82
		[Obsolete("Enum member VRDeviceType.Morpheus has been deprecated. Use VRDeviceType.PlayStationVR instead (UnityUpgradable) -> PlayStationVR", true)]
		[EditorBrowsable(1)]
		Morpheus = -1,
		// Token: 0x04000053 RID: 83
		None,
		// Token: 0x04000054 RID: 84
		Stereo,
		// Token: 0x04000055 RID: 85
		Split,
		// Token: 0x04000056 RID: 86
		Oculus,
		// Token: 0x04000057 RID: 87
		PlayStationVR,
		// Token: 0x04000058 RID: 88
		Unknown
	}
}
