using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[ExcludeFromObjectFactory]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	public class RuntimeAnimatorController : Object
	{
		// Token: 0x06000282 RID: 642 RVA: 0x000039AF File Offset: 0x00001BAF
		protected RuntimeAnimatorController()
		{
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000283 RID: 643
		public extern AnimationClip[] animationClips
		{
			[MethodImpl(4096)]
			get;
		}
	}
}
