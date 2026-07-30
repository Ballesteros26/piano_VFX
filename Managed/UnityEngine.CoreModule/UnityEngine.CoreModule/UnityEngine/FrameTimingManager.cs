using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000E2 RID: 226
	[StaticAccessor("GetUncheckedRealGfxDevice().GetFrameTimingManager()", StaticAccessorType.Dot)]
	public static class FrameTimingManager
	{
		// Token: 0x06000775 RID: 1909
		[MethodImpl(4096)]
		public static extern void CaptureFrameTimings();

		// Token: 0x06000776 RID: 1910
		[MethodImpl(4096)]
		public static extern uint GetLatestTimings(uint numFrames, FrameTiming[] timings);

		// Token: 0x06000777 RID: 1911
		[MethodImpl(4096)]
		public static extern float GetVSyncsPerSecond();

		// Token: 0x06000778 RID: 1912
		[MethodImpl(4096)]
		public static extern ulong GetGpuTimerFrequency();

		// Token: 0x06000779 RID: 1913
		[MethodImpl(4096)]
		public static extern ulong GetCpuTimerFrequency();
	}
}
