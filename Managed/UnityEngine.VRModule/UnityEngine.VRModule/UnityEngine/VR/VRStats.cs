using System;
using System.ComponentModel;

namespace UnityEngine.VR
{
	// Token: 0x02000026 RID: 38
	[EditorBrowsable(1)]
	[Obsolete("VRStats has been moved and renamed.  Use UnityEngine.XR.XRStats instead (UnityUpgradable) -> UnityEngine.XR.XRStats", true)]
	public static class VRStats
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00002796 File Offset: 0x00000996
		public static bool TryGetGPUTimeLastFrame(out float gpuTimeLastFrame)
		{
			gpuTimeLastFrame = 0f;
			throw new NotSupportedException("VRStats has been moved and renamed.  Use UnityEngine.XR.XRStats instead.");
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000027AA File Offset: 0x000009AA
		public static bool TryGetDroppedFrameCount(out int droppedFrameCount)
		{
			droppedFrameCount = 0;
			throw new NotSupportedException("VRStats has been moved and renamed.  Use UnityEngine.XR.XRStats instead.");
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000027AA File Offset: 0x000009AA
		public static bool TryGetFramePresentCount(out int framePresentCount)
		{
			framePresentCount = 0;
			throw new NotSupportedException("VRStats has been moved and renamed.  Use UnityEngine.XR.XRStats instead.");
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000027BA File Offset: 0x000009BA
		[Obsolete("gpuTimeLastFrame is deprecated. Use XRStats.TryGetGPUTimeLastFrame instead.", true)]
		public static float gpuTimeLastFrame
		{
			get
			{
				throw new NotSupportedException("VRStats has been moved and renamed.  Use UnityEngine.XR.XRStats instead.");
			}
		}
	}
}
