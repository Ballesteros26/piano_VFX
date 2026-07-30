using System;
using UnityEngine.Bindings;

namespace UnityEngine.XR.WSA
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/VR/HoloLens/HolographicEmulation/HolographicEmulationManager.h")]
	internal enum SimulatedControllerPress
	{
		// Token: 0x04000029 RID: 41
		PressButton,
		// Token: 0x0400002A RID: 42
		ReleaseButton,
		// Token: 0x0400002B RID: 43
		Grip,
		// Token: 0x0400002C RID: 44
		TouchPadPress,
		// Token: 0x0400002D RID: 45
		Select,
		// Token: 0x0400002E RID: 46
		TouchPadTouch,
		// Token: 0x0400002F RID: 47
		ThumbStick
	}
}
