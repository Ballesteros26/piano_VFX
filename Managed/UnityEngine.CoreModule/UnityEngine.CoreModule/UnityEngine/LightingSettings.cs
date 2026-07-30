using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000CF RID: 207
	[NativeHeader("Runtime/Graphics/LightingSettings.h")]
	public sealed class LightingSettings : Object
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x00002EC3 File Offset: 0x000010C3
		[RequiredByNativeCode]
		internal void LightingSettingsDontStripMe()
		{
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00009812 File Offset: 0x00007A12
		public LightingSettings()
		{
			LightingSettings.Internal_Create(this);
		}

		// Token: 0x060005BE RID: 1470
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] LightingSettings self);

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005BF RID: 1471
		// (set) Token: 0x060005C0 RID: 1472
		[NativeName("EnableBakedLightmaps")]
		public extern bool bakedGI
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005C1 RID: 1473
		// (set) Token: 0x060005C2 RID: 1474
		[NativeName("EnableRealtimeLightmaps")]
		public extern bool realtimeGI
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005C3 RID: 1475
		// (set) Token: 0x060005C4 RID: 1476
		[NativeName("RealtimeEnvironmentLighting")]
		public extern bool realtimeEnvironmentLighting
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
