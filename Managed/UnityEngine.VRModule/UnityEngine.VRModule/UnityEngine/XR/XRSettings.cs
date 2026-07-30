using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.XR
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/VR/ScriptBindings/XR.bindings.h")]
	[NativeHeader("Runtime/Interfaces/IVRDevice.h")]
	[NativeHeader("Modules/VR/VRModule.h")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	[NativeConditional("ENABLE_VR")]
	public static class XRSettings
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32
		// (set) Token: 0x06000021 RID: 33
		public static extern bool enabled
		{
			[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34
		// (set) Token: 0x06000023 RID: 35
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern GameViewRenderMode gameViewRenderMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("Active")]
		public static extern bool isDeviceActive
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37
		// (set) Token: 0x06000026 RID: 38
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool showDeviceView
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39
		// (set) Token: 0x06000028 RID: 40
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[Obsolete("renderScale is deprecated, use XRSettings.eyeTextureResolutionScale instead (UnityUpgradable) -> eyeTextureResolutionScale", false)]
		public static extern float renderScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41
		// (set) Token: 0x0600002A RID: 42
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("RenderScale")]
		public static extern float eyeTextureResolutionScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern int eyeTextureWidth
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern int eyeTextureHeight
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002130 File Offset: 0x00000330
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("IntermediateEyeTextureDesc")]
		[NativeConditional("ENABLE_VR", "RenderTextureDesc()")]
		public static RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				RenderTextureDescriptor renderTextureDescriptor;
				XRSettings.get_eyeTextureDesc_Injected(out renderTextureDescriptor);
				return renderTextureDescriptor;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46
		[NativeName("DeviceEyeTextureDimension")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern TextureDimension deviceEyeTextureDimension
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002148 File Offset: 0x00000348
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002160 File Offset: 0x00000360
		public static float renderViewportScale
		{
			get
			{
				return XRSettings.renderViewportScaleInternal;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value", "Render viewport scale should be between 0 and 1.");
				}
				XRSettings.renderViewportScaleInternal = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49
		// (set) Token: 0x06000032 RID: 50
		[NativeName("RenderViewportScale")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		internal static extern float renderViewportScaleInternal
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51
		// (set) Token: 0x06000034 RID: 52
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern float occlusionMaskScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000035 RID: 53
		// (set) Token: 0x06000036 RID: 54
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool useOcclusionMesh
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55
		[NativeName("DeviceName")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern string loadedDeviceName
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000219C File Offset: 0x0000039C
		public static void LoadDeviceByName(string deviceName)
		{
			XRSettings.LoadDeviceByName(new string[] { deviceName });
		}

		// Token: 0x06000039 RID: 57
		[MethodImpl(4096)]
		public static extern void LoadDeviceByName(string[] prioritizedDeviceNameList);

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58
		public static extern string[] supportedDevices
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003B RID: 59
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern XRSettings.StereoRenderingMode stereoRenderingMode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600003C RID: 60
		[MethodImpl(4096)]
		private static extern void get_eyeTextureDesc_Injected(out RenderTextureDescriptor ret);

		// Token: 0x02000009 RID: 9
		public enum StereoRenderingMode
		{
			// Token: 0x0400000F RID: 15
			MultiPass,
			// Token: 0x04000010 RID: 16
			SinglePass,
			// Token: 0x04000011 RID: 17
			SinglePassInstanced,
			// Token: 0x04000012 RID: 18
			SinglePassMultiview
		}
	}
}
