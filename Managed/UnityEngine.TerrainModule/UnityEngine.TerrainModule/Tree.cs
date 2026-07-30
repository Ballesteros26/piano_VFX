using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/Terrain/Public/Tree.h")]
	[ExcludeFromPreset]
	public sealed class Tree : Component
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000072 RID: 114
		// (set) Token: 0x06000073 RID: 115
		[NativeProperty("TreeData")]
		public extern ScriptableObject data
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000074 RID: 116
		public extern bool hasSpeedTreeWind
		{
			[NativeMethod("HasSpeedTreeWind")]
			[MethodImpl(4096)]
			get;
		}
	}
}
