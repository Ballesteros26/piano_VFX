using System;
using System.Runtime.CompilerServices;
using UnityEngine.Animations;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Animations
{
	// Token: 0x0200003C RID: 60
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutputExtensions.bindings.h")]
	[NativeHeader("Modules/Animation/AnimatorDefines.h")]
	[StaticAccessor("AnimationPlayableOutputExtensionsBindings", StaticAccessorType.DoubleColon)]
	public static class AnimationPlayableOutputExtensions
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000432C File Offset: 0x0000252C
		public static AnimationStreamSource GetAnimationStreamSource(this AnimationPlayableOutput output)
		{
			return AnimationPlayableOutputExtensions.InternalGetAnimationStreamSource(output.GetHandle());
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000434A File Offset: 0x0000254A
		public static void SetAnimationStreamSource(this AnimationPlayableOutput output, AnimationStreamSource streamSource)
		{
			AnimationPlayableOutputExtensions.InternalSetAnimationStreamSource(output.GetHandle(), streamSource);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000435C File Offset: 0x0000255C
		public static ushort GetSortingOrder(this AnimationPlayableOutput output)
		{
			return (ushort)AnimationPlayableOutputExtensions.InternalGetSortingOrder(output.GetHandle());
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000437B File Offset: 0x0000257B
		public static void SetSortingOrder(this AnimationPlayableOutput output, ushort sortingOrder)
		{
			AnimationPlayableOutputExtensions.InternalSetSortingOrder(output.GetHandle(), (int)sortingOrder);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000438C File Offset: 0x0000258C
		[NativeThrows]
		private static AnimationStreamSource InternalGetAnimationStreamSource(PlayableOutputHandle output)
		{
			return AnimationPlayableOutputExtensions.InternalGetAnimationStreamSource_Injected(ref output);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00004395 File Offset: 0x00002595
		[NativeThrows]
		private static void InternalSetAnimationStreamSource(PlayableOutputHandle output, AnimationStreamSource streamSource)
		{
			AnimationPlayableOutputExtensions.InternalSetAnimationStreamSource_Injected(ref output, streamSource);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000439F File Offset: 0x0000259F
		[NativeThrows]
		private static int InternalGetSortingOrder(PlayableOutputHandle output)
		{
			return AnimationPlayableOutputExtensions.InternalGetSortingOrder_Injected(ref output);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000043A8 File Offset: 0x000025A8
		[NativeThrows]
		private static void InternalSetSortingOrder(PlayableOutputHandle output, int sortingOrder)
		{
			AnimationPlayableOutputExtensions.InternalSetSortingOrder_Injected(ref output, sortingOrder);
		}

		// Token: 0x06000290 RID: 656
		[MethodImpl(4096)]
		private static extern AnimationStreamSource InternalGetAnimationStreamSource_Injected(ref PlayableOutputHandle output);

		// Token: 0x06000291 RID: 657
		[MethodImpl(4096)]
		private static extern void InternalSetAnimationStreamSource_Injected(ref PlayableOutputHandle output, AnimationStreamSource streamSource);

		// Token: 0x06000292 RID: 658
		[MethodImpl(4096)]
		private static extern int InternalGetSortingOrder_Injected(ref PlayableOutputHandle output);

		// Token: 0x06000293 RID: 659
		[MethodImpl(4096)]
		private static extern void InternalSetSortingOrder_Injected(ref PlayableOutputHandle output, int sortingOrder);
	}
}
