using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x0200004E RID: 78
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableGraphExtensions.bindings.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[StaticAccessor("AnimationPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	internal static class AnimationPlayableGraphExtensions
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000585D File Offset: 0x00003A5D
		internal static void SyncUpdateAndTimeMode(this PlayableGraph graph, Animator animator)
		{
			AnimationPlayableGraphExtensions.InternalSyncUpdateAndTimeMode(ref graph, animator);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00005869 File Offset: 0x00003A69
		internal static void DestroyOutput(this PlayableGraph graph, PlayableOutputHandle handle)
		{
			AnimationPlayableGraphExtensions.InternalDestroyOutput(ref graph, ref handle);
		}

		// Token: 0x060003B9 RID: 953
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool InternalCreateAnimationOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle);

		// Token: 0x060003BA RID: 954
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void InternalSyncUpdateAndTimeMode(ref PlayableGraph graph, [NotNull] Animator animator);

		// Token: 0x060003BB RID: 955
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void InternalDestroyOutput(ref PlayableGraph graph, ref PlayableOutputHandle handle);

		// Token: 0x060003BC RID: 956
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int InternalAnimationOutputCount(ref PlayableGraph graph);

		// Token: 0x060003BD RID: 957
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool InternalGetAnimationOutput(ref PlayableGraph graph, int index, out PlayableOutputHandle handle);
	}
}
