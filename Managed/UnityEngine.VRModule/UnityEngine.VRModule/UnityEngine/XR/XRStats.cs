using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	// Token: 0x0200000D RID: 13
	[NativeConditional("ENABLE_VR")]
	public static class XRStats
	{
		// Token: 0x0600004E RID: 78
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(4096)]
		public static extern bool TryGetGPUTimeLastFrame(out float gpuTimeLastFrame);

		// Token: 0x0600004F RID: 79
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(4096)]
		public static extern bool TryGetDroppedFrameCount(out int droppedFrameCount);

		// Token: 0x06000050 RID: 80
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(4096)]
		public static extern bool TryGetFramePresentCount(out int framePresentCount);

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002248 File Offset: 0x00000448
		[Obsolete("gpuTimeLastFrame is deprecated. Use XRStats.TryGetGPUTimeLastFrame instead.", false)]
		public static float gpuTimeLastFrame
		{
			get
			{
				float num;
				bool flag = XRStats.TryGetGPUTimeLastFrame(out num);
				float num2;
				if (flag)
				{
					num2 = num;
				}
				else
				{
					num2 = 0f;
				}
				return num2;
			}
		}
	}
}
