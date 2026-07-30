using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000E1 RID: 225
	[NativeHeader("Runtime/GfxDevice/FrameTiming.h")]
	public struct FrameTiming
	{
		// Token: 0x04000274 RID: 628
		[NativeName("m_CPUTimePresentCalled")]
		public ulong cpuTimePresentCalled;

		// Token: 0x04000275 RID: 629
		[NativeName("m_CPUFrameTime")]
		public double cpuFrameTime;

		// Token: 0x04000276 RID: 630
		[NativeName("m_CPUTimeFrameComplete")]
		public ulong cpuTimeFrameComplete;

		// Token: 0x04000277 RID: 631
		[NativeName("m_GPUFrameTime")]
		public double gpuFrameTime;

		// Token: 0x04000278 RID: 632
		[NativeName("m_HeightScale")]
		public float heightScale;

		// Token: 0x04000279 RID: 633
		[NativeName("m_WidthScale")]
		public float widthScale;

		// Token: 0x0400027A RID: 634
		[NativeName("m_SyncInterval")]
		public uint syncInterval;
	}
}
