using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.WSA
{
	// Token: 0x0200001C RID: 28
	[StaticAccessor("HolographicEmulation::HolographicEmulationManager::Get()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/VR/HoloLens/HolographicEmulation/HolographicEmulationManager.h")]
	internal class HolographicEmulationHelper
	{
		// Token: 0x060000A3 RID: 163
		[NativeConditional("ENABLE_HOLOLENS_MODULE", StubReturnStatement = "HolographicEmulation::EmulationMode_None")]
		[NativeName("GetEmulationMode")]
		[MethodImpl(4096)]
		internal static extern EmulationMode GetEmulationMode();
	}
}
