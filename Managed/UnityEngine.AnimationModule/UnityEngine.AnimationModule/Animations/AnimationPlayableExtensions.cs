using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x0200004D RID: 77
	[NativeHeader("Modules/Animation/Director/AnimationPlayableExtensions.h")]
	[NativeHeader("Modules/Animation/AnimationClip.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public static class AnimationPlayableExtensions
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00005838 File Offset: 0x00003A38
		public static void SetAnimatedProperties<U>(this U playable, AnimationClip clip) where U : struct, IPlayable
		{
			PlayableHandle handle = playable.GetHandle();
			AnimationPlayableExtensions.SetAnimatedPropertiesInternal(ref handle, clip);
		}

		// Token: 0x060003B6 RID: 950
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void SetAnimatedPropertiesInternal(ref PlayableHandle playable, AnimationClip animatedProperties);
	}
}
