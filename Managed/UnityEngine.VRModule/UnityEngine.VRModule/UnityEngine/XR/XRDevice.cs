using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000C RID: 12
	[NativeConditional("ENABLE_VR")]
	public static class XRDevice
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003D RID: 61
		[NativeName("DeviceConnected")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool isPresent
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003E RID: 62
		public static extern UserPresenceState userPresence
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003F RID: 63
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("DeviceName")]
		[Obsolete("family is deprecated.  Use XRSettings.loadedDeviceName instead.", false)]
		public static extern string family
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("DeviceModel")]
		public static extern string model
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000041 RID: 65
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("DeviceRefreshRate")]
		public static extern float refreshRate
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000042 RID: 66
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(4096)]
		public static extern IntPtr GetNativePtr();

		// Token: 0x06000043 RID: 67
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(4096)]
		public static extern TrackingSpaceType GetTrackingSpaceType();

		// Token: 0x06000044 RID: 68
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(4096)]
		public static extern bool SetTrackingSpaceType(TrackingSpaceType trackingSpaceType);

		// Token: 0x06000045 RID: 69
		[NativeName("DisableAutoVRCameraTracking")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(4096)]
		public static extern void DisableAutoXRCameraTracking([NotNull] Camera camera, bool disabled);

		// Token: 0x06000046 RID: 70
		[NativeName("UpdateEyeTextureMSAASetting")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(4096)]
		public static extern void UpdateEyeTextureMSAASetting();

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000047 RID: 71
		// (set) Token: 0x06000048 RID: 72
		public static extern float fovZoomFactor
		{
			[MethodImpl(4096)]
			get;
			[NativeName("SetProjectionZoomFactor")]
			[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000049 RID: 73
		public static extern TrackingOriginMode trackingOriginMode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600004A RID: 74 RVA: 0x000021B0 File Offset: 0x000003B0
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x000021E4 File Offset: 0x000003E4
		[field: DebuggerBrowsable(0)]
		public static event Action<string> deviceLoaded;

		// Token: 0x0600004C RID: 76 RVA: 0x00002218 File Offset: 0x00000418
		[RequiredByNativeCode]
		private static void InvokeDeviceLoaded(string loadedDeviceName)
		{
			bool flag = XRDevice.deviceLoaded != null;
			if (flag)
			{
				XRDevice.deviceLoaded.Invoke(loadedDeviceName);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002240 File Offset: 0x00000440
		// Note: this type is marked as 'beforefieldinit'.
		static XRDevice()
		{
			XRDevice.deviceLoaded = null;
		}
	}
}
