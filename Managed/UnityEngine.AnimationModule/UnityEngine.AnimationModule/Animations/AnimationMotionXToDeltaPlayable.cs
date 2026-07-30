using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004B RID: 75
	[StaticAccessor("AnimationMotionXToDeltaPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMotionXToDeltaPlayable.bindings.h")]
	internal struct AnimationMotionXToDeltaPlayable : IPlayable, IEquatable<AnimationMotionXToDeltaPlayable>
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000054CC File Offset: 0x000036CC
		public static AnimationMotionXToDeltaPlayable Null
		{
			get
			{
				return AnimationMotionXToDeltaPlayable.m_NullPlayable;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000054E4 File Offset: 0x000036E4
		public static AnimationMotionXToDeltaPlayable Create(PlayableGraph graph)
		{
			PlayableHandle playableHandle = AnimationMotionXToDeltaPlayable.CreateHandle(graph);
			return new AnimationMotionXToDeltaPlayable(playableHandle);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00005504 File Offset: 0x00003704
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationMotionXToDeltaPlayable.CreateHandleInternal(graph, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(1);
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00005540 File Offset: 0x00003740
		private AnimationMotionXToDeltaPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationMotionXToDeltaPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationMotionXToDeltaPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000557C File Offset: 0x0000377C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00005594 File Offset: 0x00003794
		public static implicit operator Playable(AnimationMotionXToDeltaPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000055B4 File Offset: 0x000037B4
		public static explicit operator AnimationMotionXToDeltaPlayable(Playable playable)
		{
			return new AnimationMotionXToDeltaPlayable(playable.GetHandle());
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000055D4 File Offset: 0x000037D4
		public bool Equals(AnimationMotionXToDeltaPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000055F8 File Offset: 0x000037F8
		public bool IsAbsoluteMotion()
		{
			return AnimationMotionXToDeltaPlayable.IsAbsoluteMotionInternal(ref this.m_Handle);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00005615 File Offset: 0x00003815
		public void SetAbsoluteMotion(bool value)
		{
			AnimationMotionXToDeltaPlayable.SetAbsoluteMotionInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00005625 File Offset: 0x00003825
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationMotionXToDeltaPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x0600039A RID: 922
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool IsAbsoluteMotionInternal(ref PlayableHandle handle);

		// Token: 0x0600039B RID: 923
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetAbsoluteMotionInternal(ref PlayableHandle handle, bool value);

		// Token: 0x0600039D RID: 925
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x0400014B RID: 331
		private PlayableHandle m_Handle;

		// Token: 0x0400014C RID: 332
		private static readonly AnimationMotionXToDeltaPlayable m_NullPlayable = new AnimationMotionXToDeltaPlayable(PlayableHandle.Null);
	}
}
