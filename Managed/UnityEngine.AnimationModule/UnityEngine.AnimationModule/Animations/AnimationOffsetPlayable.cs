using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004C RID: 76
	[RequiredByNativeCode]
	[StaticAccessor("AnimationOffsetPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/Director/AnimationOffsetPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationOffsetPlayable.bindings.h")]
	internal struct AnimationOffsetPlayable : IPlayable, IEquatable<AnimationOffsetPlayable>
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00005640 File Offset: 0x00003840
		public static AnimationOffsetPlayable Null
		{
			get
			{
				return AnimationOffsetPlayable.m_NullPlayable;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00005658 File Offset: 0x00003858
		public static AnimationOffsetPlayable Create(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle playableHandle = AnimationOffsetPlayable.CreateHandle(graph, position, rotation, inputCount);
			return new AnimationOffsetPlayable(playableHandle);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000567C File Offset: 0x0000387C
		private static PlayableHandle CreateHandle(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationOffsetPlayable.CreateHandleInternal(graph, position, rotation, ref @null);
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

		// Token: 0x060003A1 RID: 929 RVA: 0x000056B8 File Offset: 0x000038B8
		internal AnimationOffsetPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationOffsetPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationOffsetPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000056F4 File Offset: 0x000038F4
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000570C File Offset: 0x0000390C
		public static implicit operator Playable(AnimationOffsetPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000572C File Offset: 0x0000392C
		public static explicit operator AnimationOffsetPlayable(Playable playable)
		{
			return new AnimationOffsetPlayable(playable.GetHandle());
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000574C File Offset: 0x0000394C
		public bool Equals(AnimationOffsetPlayable other)
		{
			return this.Equals(other.GetHandle());
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00005778 File Offset: 0x00003978
		public Vector3 GetPosition()
		{
			return AnimationOffsetPlayable.GetPositionInternal(ref this.m_Handle);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00005795 File Offset: 0x00003995
		public void SetPosition(Vector3 value)
		{
			AnimationOffsetPlayable.SetPositionInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000057A8 File Offset: 0x000039A8
		public Quaternion GetRotation()
		{
			return AnimationOffsetPlayable.GetRotationInternal(ref this.m_Handle);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000057C5 File Offset: 0x000039C5
		public void SetRotation(Quaternion value)
		{
			AnimationOffsetPlayable.SetRotationInternal(ref this.m_Handle, value);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000057D5 File Offset: 0x000039D5
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, Vector3 position, Quaternion rotation, ref PlayableHandle handle)
		{
			return AnimationOffsetPlayable.CreateHandleInternal_Injected(ref graph, ref position, ref rotation, ref handle);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000057E4 File Offset: 0x000039E4
		[NativeThrows]
		private static Vector3 GetPositionInternal(ref PlayableHandle handle)
		{
			Vector3 vector;
			AnimationOffsetPlayable.GetPositionInternal_Injected(ref handle, out vector);
			return vector;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000057FA File Offset: 0x000039FA
		[NativeThrows]
		private static void SetPositionInternal(ref PlayableHandle handle, Vector3 value)
		{
			AnimationOffsetPlayable.SetPositionInternal_Injected(ref handle, ref value);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00005804 File Offset: 0x00003A04
		[NativeThrows]
		private static Quaternion GetRotationInternal(ref PlayableHandle handle)
		{
			Quaternion quaternion;
			AnimationOffsetPlayable.GetRotationInternal_Injected(ref handle, out quaternion);
			return quaternion;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000581A File Offset: 0x00003A1A
		[NativeThrows]
		private static void SetRotationInternal(ref PlayableHandle handle, Quaternion value)
		{
			AnimationOffsetPlayable.SetRotationInternal_Injected(ref handle, ref value);
		}

		// Token: 0x060003B0 RID: 944
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref Vector3 position, ref Quaternion rotation, ref PlayableHandle handle);

		// Token: 0x060003B1 RID: 945
		[MethodImpl(4096)]
		private static extern void GetPositionInternal_Injected(ref PlayableHandle handle, out Vector3 ret);

		// Token: 0x060003B2 RID: 946
		[MethodImpl(4096)]
		private static extern void SetPositionInternal_Injected(ref PlayableHandle handle, ref Vector3 value);

		// Token: 0x060003B3 RID: 947
		[MethodImpl(4096)]
		private static extern void GetRotationInternal_Injected(ref PlayableHandle handle, out Quaternion ret);

		// Token: 0x060003B4 RID: 948
		[MethodImpl(4096)]
		private static extern void SetRotationInternal_Injected(ref PlayableHandle handle, ref Quaternion value);

		// Token: 0x0400014D RID: 333
		private PlayableHandle m_Handle;

		// Token: 0x0400014E RID: 334
		private static readonly AnimationOffsetPlayable m_NullPlayable = new AnimationOffsetPlayable(PlayableHandle.Null);
	}
}
