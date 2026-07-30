using System;
using UnityEngine.XR;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class XRGraphics
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005939 File Offset: 0x00003B39
		public static float eyeTextureResolutionScale
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.eyeTextureResolutionScale;
				}
				return 1f;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000594D File Offset: 0x00003B4D
		public static float renderViewportScale
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.renderViewportScale;
				}
				return 1f;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005961 File Offset: 0x00003B61
		public static bool enabled
		{
			get
			{
				return XRSettings.enabled;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005968 File Offset: 0x00003B68
		public static bool isDeviceActive
		{
			get
			{
				return XRGraphics.enabled && XRSettings.isDeviceActive;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005978 File Offset: 0x00003B78
		public static string loadedDeviceName
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.loadedDeviceName;
				}
				return "No XR device loaded";
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000598C File Offset: 0x00003B8C
		public static string[] supportedDevices
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.supportedDevices;
				}
				return new string[1];
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000059A1 File Offset: 0x00003BA1
		public static XRGraphics.StereoRenderingMode stereoRenderingMode
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return (XRGraphics.StereoRenderingMode)XRSettings.stereoRenderingMode;
				}
				return XRGraphics.StereoRenderingMode.SinglePass;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000059B1 File Offset: 0x00003BB1
		public static RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.eyeTextureDesc;
				}
				return new RenderTextureDescriptor(0, 0);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000059C7 File Offset: 0x00003BC7
		public static int eyeTextureWidth
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.eyeTextureWidth;
				}
				return 0;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000059D7 File Offset: 0x00003BD7
		public static int eyeTextureHeight
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.eyeTextureHeight;
				}
				return 0;
			}
		}

		// Token: 0x020000BE RID: 190
		public enum StereoRenderingMode
		{
			// Token: 0x0400026E RID: 622
			MultiPass,
			// Token: 0x0400026F RID: 623
			SinglePass,
			// Token: 0x04000270 RID: 624
			SinglePassInstanced,
			// Token: 0x04000271 RID: 625
			SinglePassMultiView
		}
	}
}
