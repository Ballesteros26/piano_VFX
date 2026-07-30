using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000047 RID: 71
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClipPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationClipPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("AnimationClipPlayableBindings", StaticAccessorType.DoubleColon)]
	public struct AnimationClipPlayable : IPlayable, IEquatable<AnimationClipPlayable>
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000482C File Offset: 0x00002A2C
		public static AnimationClipPlayable Create(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle playableHandle = AnimationClipPlayable.CreateHandle(graph, clip);
			return new AnimationClipPlayable(playableHandle);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000484C File Offset: 0x00002A4C
		private static PlayableHandle CreateHandle(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationClipPlayable.CreateHandleInternal(graph, clip, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00004880 File Offset: 0x00002A80
		internal AnimationClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000048BC File Offset: 0x00002ABC
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000048D4 File Offset: 0x00002AD4
		public static implicit operator Playable(AnimationClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000048F4 File Offset: 0x00002AF4
		public static explicit operator AnimationClipPlayable(Playable playable)
		{
			return new AnimationClipPlayable(playable.GetHandle());
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00004914 File Offset: 0x00002B14
		public bool Equals(AnimationClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00004938 File Offset: 0x00002B38
		public AnimationClip GetAnimationClip()
		{
			return AnimationClipPlayable.GetAnimationClipInternal(ref this.m_Handle);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00004958 File Offset: 0x00002B58
		public bool GetApplyFootIK()
		{
			return AnimationClipPlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00004975 File Offset: 0x00002B75
		public void SetApplyFootIK(bool value)
		{
			AnimationClipPlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00004988 File Offset: 0x00002B88
		public bool GetApplyPlayableIK()
		{
			return AnimationClipPlayable.GetApplyPlayableIKInternal(ref this.m_Handle);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000049A5 File Offset: 0x00002BA5
		public void SetApplyPlayableIK(bool value)
		{
			AnimationClipPlayable.SetApplyPlayableIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000049B8 File Offset: 0x00002BB8
		internal bool GetRemoveStartOffset()
		{
			return AnimationClipPlayable.GetRemoveStartOffsetInternal(ref this.m_Handle);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000049D5 File Offset: 0x00002BD5
		internal void SetRemoveStartOffset(bool value)
		{
			AnimationClipPlayable.SetRemoveStartOffsetInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000049E8 File Offset: 0x00002BE8
		internal bool GetOverrideLoopTime()
		{
			return AnimationClipPlayable.GetOverrideLoopTimeInternal(ref this.m_Handle);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00004A05 File Offset: 0x00002C05
		internal void SetOverrideLoopTime(bool value)
		{
			AnimationClipPlayable.SetOverrideLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00004A18 File Offset: 0x00002C18
		internal bool GetLoopTime()
		{
			return AnimationClipPlayable.GetLoopTimeInternal(ref this.m_Handle);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00004A35 File Offset: 0x00002C35
		internal void SetLoopTime(bool value)
		{
			AnimationClipPlayable.SetLoopTimeInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00004A48 File Offset: 0x00002C48
		internal float GetSampleRate()
		{
			return AnimationClipPlayable.GetSampleRateInternal(ref this.m_Handle);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00004A65 File Offset: 0x00002C65
		internal void SetSampleRate(float value)
		{
			AnimationClipPlayable.SetSampleRateInternal(ref this.m_Handle, value);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00004A75 File Offset: 0x00002C75
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle)
		{
			return AnimationClipPlayable.CreateHandleInternal_Injected(ref graph, clip, ref handle);
		}

		// Token: 0x060002ED RID: 749
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern AnimationClip GetAnimationClipInternal(ref PlayableHandle handle);

		// Token: 0x060002EE RID: 750
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		// Token: 0x060002EF RID: 751
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002F0 RID: 752
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetApplyPlayableIKInternal(ref PlayableHandle handle);

		// Token: 0x060002F1 RID: 753
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetApplyPlayableIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002F2 RID: 754
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetRemoveStartOffsetInternal(ref PlayableHandle handle);

		// Token: 0x060002F3 RID: 755
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetRemoveStartOffsetInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002F4 RID: 756
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetOverrideLoopTimeInternal(ref PlayableHandle handle);

		// Token: 0x060002F5 RID: 757
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetOverrideLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002F6 RID: 758
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetLoopTimeInternal(ref PlayableHandle handle);

		// Token: 0x060002F7 RID: 759
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetLoopTimeInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060002F8 RID: 760
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern float GetSampleRateInternal(ref PlayableHandle handle);

		// Token: 0x060002F9 RID: 761
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetSampleRateInternal(ref PlayableHandle handle, float value);

		// Token: 0x060002FA RID: 762
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle);

		// Token: 0x04000145 RID: 325
		private PlayableHandle m_Handle;
	}
}
