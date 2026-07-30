using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[StaticAccessor("UI::SystemProfilerApi", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/UI/Canvas.h")]
	public static class UISystemProfilerApi
	{
		// Token: 0x06000084 RID: 132
		[MethodImpl(4096)]
		public static extern void BeginSample(UISystemProfilerApi.SampleType type);

		// Token: 0x06000085 RID: 133
		[MethodImpl(4096)]
		public static extern void EndSample(UISystemProfilerApi.SampleType type);

		// Token: 0x06000086 RID: 134
		[MethodImpl(4096)]
		public static extern void AddMarker(string name, Object obj);

		// Token: 0x0200000B RID: 11
		public enum SampleType
		{
			// Token: 0x04000010 RID: 16
			Layout,
			// Token: 0x04000011 RID: 17
			Render
		}
	}
}
