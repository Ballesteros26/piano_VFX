using System;
using System.Linq;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x0200024A RID: 586
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[UsedByNativeCode]
	[NativeHeader("PlatformDependent/Win/Webcam/CameraParameters.h")]
	public struct CameraParameters
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x00028370 File Offset: 0x00026570
		// (set) Token: 0x06001911 RID: 6417 RVA: 0x00028388 File Offset: 0x00026588
		public float hologramOpacity
		{
			get
			{
				return this.m_HologramOpacity;
			}
			set
			{
				this.m_HologramOpacity = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x00028394 File Offset: 0x00026594
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x000283AC File Offset: 0x000265AC
		public float frameRate
		{
			get
			{
				return this.m_FrameRate;
			}
			set
			{
				this.m_FrameRate = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x000283B8 File Offset: 0x000265B8
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x000283D0 File Offset: 0x000265D0
		public int cameraResolutionWidth
		{
			get
			{
				return this.m_CameraResolutionWidth;
			}
			set
			{
				this.m_CameraResolutionWidth = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x000283DC File Offset: 0x000265DC
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x000283F4 File Offset: 0x000265F4
		public int cameraResolutionHeight
		{
			get
			{
				return this.m_CameraResolutionHeight;
			}
			set
			{
				this.m_CameraResolutionHeight = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x00028400 File Offset: 0x00026600
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x00028418 File Offset: 0x00026618
		public CapturePixelFormat pixelFormat
		{
			get
			{
				return this.m_PixelFormat;
			}
			set
			{
				this.m_PixelFormat = value;
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00028424 File Offset: 0x00026624
		public CameraParameters(WebCamMode webCamMode)
		{
			this.m_HologramOpacity = 1f;
			this.m_PixelFormat = CapturePixelFormat.BGRA32;
			this.m_FrameRate = 0f;
			this.m_CameraResolutionWidth = 0;
			this.m_CameraResolutionHeight = 0;
			bool flag = webCamMode == WebCamMode.PhotoMode;
			if (flag)
			{
				Resolution resolution = Enumerable.First<Resolution>(Enumerable.OrderByDescending<Resolution, int>(PhotoCapture.SupportedResolutions, (Resolution res) => res.width * res.height));
				this.m_CameraResolutionWidth = resolution.width;
				this.m_CameraResolutionHeight = resolution.height;
			}
			else
			{
				bool flag2 = webCamMode == WebCamMode.VideoMode;
				if (flag2)
				{
					Resolution resolution2 = Enumerable.First<Resolution>(Enumerable.OrderByDescending<Resolution, int>(VideoCapture.SupportedResolutions, (Resolution res) => res.width * res.height));
					float num = Enumerable.First<float>(Enumerable.OrderByDescending<float, float>(VideoCapture.GetSupportedFrameRatesForResolution(resolution2), (float fps) => fps));
					this.m_CameraResolutionWidth = resolution2.width;
					this.m_CameraResolutionHeight = resolution2.height;
					this.m_FrameRate = num;
				}
			}
		}

		// Token: 0x040007BA RID: 1978
		private float m_HologramOpacity;

		// Token: 0x040007BB RID: 1979
		private float m_FrameRate;

		// Token: 0x040007BC RID: 1980
		private int m_CameraResolutionWidth;

		// Token: 0x040007BD RID: 1981
		private int m_CameraResolutionHeight;

		// Token: 0x040007BE RID: 1982
		private CapturePixelFormat m_PixelFormat;
	}
}
