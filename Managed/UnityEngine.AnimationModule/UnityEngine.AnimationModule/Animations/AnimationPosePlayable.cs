using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000050 RID: 80
	[StaticAccessor("AnimationPosePlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/Director/AnimationPosePlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPosePlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	internal struct AnimationPosePlayable : IPlayable, IEquatable<AnimationPosePlayable>
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00005998 File Offset: 0x00003B98
		public static AnimationPosePlayable Null
		{
			get
			{
				return AnimationPosePlayable.m_NullPlayable;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000059B0 File Offset: 0x00003BB0
		public static AnimationPosePlayable Create(PlayableGraph graph)
		{
			PlayableHandle playableHandle = AnimationPosePlayable.CreateHandle(graph);
			return new AnimationPosePlayable(playableHandle);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000059D0 File Offset: 0x00003BD0
		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationPosePlayable.CreateHandleInternal(graph, ref @null);
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

		// Token: 0x060003CB RID: 971 RVA: 0x00005A00 File Offset: 0x00003C00
		internal AnimationPosePlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationPosePlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPosePlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00005A3C File Offset: 0x00003C3C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00005A54 File Offset: 0x00003C54
		public static implicit operator Playable(AnimationPosePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00005A74 File Offset: 0x00003C74
		public static explicit operator AnimationPosePlayable(Playable playable)
		{
			return new AnimationPosePlayable(playable.GetHandle());
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00005A94 File Offset: 0x00003C94
		public bool Equals(AnimationPosePlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public bool GetMustReadPreviousPose()
		{
			return AnimationPosePlayable.GetMustReadPreviousPoseInternal(ref this.m_Handle);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00005ADD File Offset: 0x00003CDD
		public void SetMustReadPreviousPose(bool value)
		{
			AnimationPosePlayable.SetMustReadPreviousPoseInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public bool GetReadDefaultPose()
		{
			return AnimationPosePlayable.GetReadDefaultPoseInternal(ref this.m_Handle);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00005B0D File Offset: 0x00003D0D
		public void SetReadDefaultPose(bool value)
		{
			AnimationPosePlayable.SetReadDefaultPoseInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00005B20 File Offset: 0x00003D20
		public bool GetApplyFootIK()
		{
			return AnimationPosePlayable.GetApplyFootIKInternal(ref this.m_Handle);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00005B3D File Offset: 0x00003D3D
		public void SetApplyFootIK(bool value)
		{
			AnimationPosePlayable.SetApplyFootIKInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00005B4D File Offset: 0x00003D4D
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationPosePlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x060003D7 RID: 983
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetMustReadPreviousPoseInternal(ref PlayableHandle handle);

		// Token: 0x060003D8 RID: 984
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetMustReadPreviousPoseInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060003D9 RID: 985
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetReadDefaultPoseInternal(ref PlayableHandle handle);

		// Token: 0x060003DA RID: 986
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetReadDefaultPoseInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060003DB RID: 987
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		// Token: 0x060003DC RID: 988
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		// Token: 0x060003DE RID: 990
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000150 RID: 336
		private PlayableHandle m_Handle;

		// Token: 0x04000151 RID: 337
		private static readonly AnimationPosePlayable m_NullPlayable = new AnimationPosePlayable(PlayableHandle.Null);
	}
}
