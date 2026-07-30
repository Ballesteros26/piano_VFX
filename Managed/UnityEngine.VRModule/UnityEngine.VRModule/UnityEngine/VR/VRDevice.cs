using System;
using System.ComponentModel;
using UnityEngine.XR;

namespace UnityEngine.VR
{
	// Token: 0x02000025 RID: 37
	[Obsolete("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead (UnityUpgradable) -> UnityEngine.XR.XRDevice", true)]
	[EditorBrowsable(1)]
	public static class VRDevice
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002789 File Offset: 0x00000989
		public static bool isPresent
		{
			get
			{
				throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002789 File Offset: 0x00000989
		public static UserPresenceState userPresence
		{
			get
			{
				throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002789 File Offset: 0x00000989
		[Obsolete("family is deprecated.  Use XRSettings.loadedDeviceName instead.", true)]
		public static string family
		{
			get
			{
				throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002789 File Offset: 0x00000989
		public static string model
		{
			get
			{
				throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002789 File Offset: 0x00000989
		public static float refreshRate
		{
			get
			{
				throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002789 File Offset: 0x00000989
		public static TrackingSpaceType GetTrackingSpaceType()
		{
			throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002789 File Offset: 0x00000989
		public static bool SetTrackingSpaceType(TrackingSpaceType trackingSpaceType)
		{
			throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002789 File Offset: 0x00000989
		public static IntPtr GetNativePtr()
		{
			throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002789 File Offset: 0x00000989
		[EditorBrowsable(1)]
		[Obsolete("DisableAutoVRCameraTracking has been moved and renamed.  Use UnityEngine.XR.XRDevice.DisableAutoXRCameraTracking instead (UnityUpgradable) -> UnityEngine.XR.XRDevice.DisableAutoXRCameraTracking(*)", true)]
		public static void DisableAutoVRCameraTracking(Camera camera, bool disabled)
		{
			throw new NotSupportedException("VRDevice has been moved and renamed.  Use UnityEngine.XR.XRDevice instead.");
		}
	}
}
