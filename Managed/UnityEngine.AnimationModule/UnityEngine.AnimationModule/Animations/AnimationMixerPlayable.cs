using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004A RID: 74
	[StaticAccessor("AnimationMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMixerPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationMixerPlayable.h")]
	[RequiredByNativeCode]
	public struct AnimationMixerPlayable : IPlayable, IEquatable<AnimationMixerPlayable>
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00005380 File Offset: 0x00003580
		public static AnimationMixerPlayable Null
		{
			get
			{
				return AnimationMixerPlayable.m_NullPlayable;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00005398 File Offset: 0x00003598
		public static AnimationMixerPlayable Create(PlayableGraph graph, int inputCount = 0, bool normalizeWeights = false)
		{
			PlayableHandle playableHandle = AnimationMixerPlayable.CreateHandle(graph, inputCount, normalizeWeights);
			return new AnimationMixerPlayable(playableHandle);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000053BC File Offset: 0x000035BC
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0, bool normalizeWeights = false)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationMixerPlayable.CreateHandleInternal(graph, normalizeWeights, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000053F8 File Offset: 0x000035F8
		internal AnimationMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00005434 File Offset: 0x00003634
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000544C File Offset: 0x0000364C
		public static implicit operator Playable(AnimationMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000546C File Offset: 0x0000366C
		public static explicit operator AnimationMixerPlayable(Playable playable)
		{
			return new AnimationMixerPlayable(playable.GetHandle());
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000548C File Offset: 0x0000368C
		public bool Equals(AnimationMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000054B0 File Offset: 0x000036B0
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, bool normalizeWeights, ref PlayableHandle handle)
		{
			return AnimationMixerPlayable.CreateHandleInternal_Injected(ref graph, normalizeWeights, ref handle);
		}

		// Token: 0x0600038E RID: 910
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, bool normalizeWeights, ref PlayableHandle handle);

		// Token: 0x04000149 RID: 329
		private PlayableHandle m_Handle;

		// Token: 0x0400014A RID: 330
		private static readonly AnimationMixerPlayable m_NullPlayable = new AnimationMixerPlayable(PlayableHandle.Null);
	}
}
