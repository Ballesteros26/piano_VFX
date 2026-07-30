using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003C6 RID: 966
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	public class RenderSettings
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002194 RID: 8596
		// (set) Token: 0x06002195 RID: 8597
		public static extern bool useRadianceAmbientProbe
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
