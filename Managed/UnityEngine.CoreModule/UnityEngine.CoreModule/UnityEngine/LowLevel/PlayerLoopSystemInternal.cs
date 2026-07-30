using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x02000278 RID: 632
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	[NativeType(Header = "Runtime/Misc/PlayerLoop.h")]
	[RequiredByNativeCode]
	internal struct PlayerLoopSystemInternal
	{
		// Token: 0x04000812 RID: 2066
		public Type type;

		// Token: 0x04000813 RID: 2067
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x04000814 RID: 2068
		public IntPtr updateFunction;

		// Token: 0x04000815 RID: 2069
		public IntPtr loopConditionFunction;

		// Token: 0x04000816 RID: 2070
		public int numSubSystems;
	}
}
