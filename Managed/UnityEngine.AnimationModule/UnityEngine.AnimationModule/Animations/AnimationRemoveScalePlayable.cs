using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000051 RID: 81
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationRemoveScalePlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationRemoveScalePlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationRemoveScalePlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	internal struct AnimationRemoveScalePlayable : IPlayable, IEquatable<AnimationRemoveScalePlayable>
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00005B68 File Offset: 0x00003D68
		public static AnimationRemoveScalePlayable Null
		{
			get
			{
				return AnimationRemoveScalePlayable.m_NullPlayable;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00005B80 File Offset: 0x00003D80
		public static AnimationRemoveScalePlayable Create(PlayableGraph graph, int inputCount)
		{
			PlayableHandle playableHandle = AnimationRemoveScalePlayable.CreateHandle(graph, inputCount);
			return new AnimationRemoveScalePlayable(playableHandle);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00005BA0 File Offset: 0x00003DA0
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationRemoveScalePlayable.CreateHandleInternal(graph, ref @null);
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

		// Token: 0x060003E2 RID: 994 RVA: 0x00005BDC File Offset: 0x00003DDC
		internal AnimationRemoveScalePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationRemoveScalePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationRemoveScalePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00005C18 File Offset: 0x00003E18
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00005C30 File Offset: 0x00003E30
		public static implicit operator Playable(AnimationRemoveScalePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00005C50 File Offset: 0x00003E50
		public static explicit operator AnimationRemoveScalePlayable(Playable playable)
		{
			return new AnimationRemoveScalePlayable(playable.GetHandle());
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00005C70 File Offset: 0x00003E70
		public bool Equals(AnimationRemoveScalePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00005C9A File Offset: 0x00003E9A
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationRemoveScalePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x060003E9 RID: 1001
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000152 RID: 338
		private PlayableHandle m_Handle;

		// Token: 0x04000153 RID: 339
		private static readonly AnimationRemoveScalePlayable m_NullPlayable = new AnimationRemoveScalePlayable(PlayableHandle.Null);
	}
}
