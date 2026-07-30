using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.Provider
{
	// Token: 0x0200002E RID: 46
	public static class XRStats
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00004A58 File Offset: 0x00002C58
		public static bool TryGetStat(IntegratedSubsystem xrSubsystem, string tag, out float value)
		{
			return XRStats.TryGetStat_Internal(xrSubsystem.m_Ptr, tag, out value);
		}

		// Token: 0x06000151 RID: 337
		[NativeConditional("ENABLE_XR")]
		[NativeMethod("TryGetStatByName_Internal")]
		[NativeHeader("Modules/XR/Stats/XRStats.h")]
		[StaticAccessor("XRStats::Get()", StaticAccessorType.Dot)]
		[MethodImpl(4096)]
		private static extern bool TryGetStat_Internal(IntPtr ptr, string tag, out float value);
	}
}
