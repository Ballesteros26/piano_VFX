using System;
using System.ComponentModel;

namespace UnityEngine.VR
{
	// Token: 0x02000024 RID: 36
	[EditorBrowsable(1)]
	[Obsolete("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead (UnityUpgradable)", true)]
	public static class VRSettings
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000276F File Offset: 0x0000096F
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000276F File Offset: 0x0000096F
		public static bool enabled
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
			set
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000276F File Offset: 0x0000096F
		public static bool isDeviceActive
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000276F File Offset: 0x0000096F
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000276F File Offset: 0x0000096F
		public static bool showDeviceView
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
			set
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000277C File Offset: 0x0000097C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000277C File Offset: 0x0000097C
		public static float renderScale
		{
			get
			{
				throw new NotSupportedException("VRSettings.renderScale has been moved and renamed.  Use UnityEngine.XR.XRSettings.eyeTextureResolutionScale instead.");
			}
			set
			{
				throw new NotSupportedException("VRSettings.renderScale has been moved and renamed.  Use UnityEngine.XR.XRSettings.eyeTextureResolutionScale instead.");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000276F File Offset: 0x0000096F
		public static int eyeTextureWidth
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000276F File Offset: 0x0000096F
		public static int eyeTextureHeight
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000276F File Offset: 0x0000096F
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000276F File Offset: 0x0000096F
		public static float renderViewportScale
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
			set
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000276F File Offset: 0x0000096F
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000276F File Offset: 0x0000096F
		public static float occlusionMaskScale
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
			set
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000276F File Offset: 0x0000096F
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000276F File Offset: 0x0000096F
		[Obsolete("loadedDevice is deprecated.  Use loadedDeviceName and LoadDeviceByName instead.", true)]
		public static VRDeviceType loadedDevice
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
			set
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000276F File Offset: 0x0000096F
		public static string loadedDeviceName
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000276F File Offset: 0x0000096F
		public static void LoadDeviceByName(string deviceName)
		{
			throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000276F File Offset: 0x0000096F
		public static void LoadDeviceByName(string[] prioritizedDeviceNameList)
		{
			throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000276F File Offset: 0x0000096F
		public static string[] supportedDevices
		{
			get
			{
				throw new NotSupportedException("VRSettings has been moved and renamed.  Use UnityEngine.XR.XRSettings instead.");
			}
		}
	}
}
