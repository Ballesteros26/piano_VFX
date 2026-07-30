using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200019F RID: 415
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[UsedByNativeCode]
	public class Behaviour : Component
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600130B RID: 4875
		// (set) Token: 0x0600130C RID: 4876
		[NativeProperty]
		[RequiredByNativeCode]
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x0600130D RID: 4877
		[NativeProperty]
		public extern bool isActiveAndEnabled
		{
			[NativeMethod("IsAddedToManager")]
			[MethodImpl(4096)]
			get;
		}
	}
}
