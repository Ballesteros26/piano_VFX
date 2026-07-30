using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x02000249 RID: 585
	[NativeHeader("PlatformDependent/Win/Webcam/WebCam.h")]
	[StaticAccessor("WebCam::GetInstance()", StaticAccessorType.Dot)]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	public class WebCam
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600190E RID: 6414
		public static extern WebCamMode Mode
		{
			[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
			[NativeName("GetWebCamMode")]
			[MethodImpl(4096)]
			get;
		}
	}
}
