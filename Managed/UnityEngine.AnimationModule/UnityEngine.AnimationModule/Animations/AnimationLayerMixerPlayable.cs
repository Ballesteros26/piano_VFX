using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000049 RID: 73
	[RequiredByNativeCode]
	[StaticAccessor("AnimationLayerMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationLayerMixerPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationLayerMixerPlayable.h")]
	public struct AnimationLayerMixerPlayable : IPlayable, IEquatable<AnimationLayerMixerPlayable>
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000050FC File Offset: 0x000032FC
		public static AnimationLayerMixerPlayable Null
		{
			get
			{
				return AnimationLayerMixerPlayable.m_NullPlayable;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00005114 File Offset: 0x00003314
		public static AnimationLayerMixerPlayable Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle playableHandle = AnimationLayerMixerPlayable.CreateHandle(graph, inputCount);
			return new AnimationLayerMixerPlayable(playableHandle);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00005134 File Offset: 0x00003334
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationLayerMixerPlayable.CreateHandleInternal(graph, ref @null);
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

		// Token: 0x06000376 RID: 886 RVA: 0x00005170 File Offset: 0x00003370
		internal AnimationLayerMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationLayerMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationLayerMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000051AC File Offset: 0x000033AC
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000051C4 File Offset: 0x000033C4
		public static implicit operator Playable(AnimationLayerMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000051E4 File Offset: 0x000033E4
		public static explicit operator AnimationLayerMixerPlayable(Playable playable)
		{
			return new AnimationLayerMixerPlayable(playable.GetHandle());
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00005204 File Offset: 0x00003404
		public bool Equals(AnimationLayerMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00005228 File Offset: 0x00003428
		public bool IsLayerAdditive(uint layerIndex)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			return AnimationLayerMixerPlayable.IsLayerAdditiveInternal(ref this.m_Handle, layerIndex);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000528C File Offset: 0x0000348C
		public void SetLayerAdditive(uint layerIndex, bool value)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			AnimationLayerMixerPlayable.SetLayerAdditiveInternal(ref this.m_Handle, layerIndex, value);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000052EC File Offset: 0x000034EC
		public void SetLayerMaskFromAvatarMask(uint layerIndex, AvatarMask mask)
		{
			bool flag = (ulong)layerIndex >= (ulong)((long)this.m_Handle.GetInputCount());
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layerIndex", string.Format("layerIndex {0} must be in the range of 0 to {1}.", layerIndex, this.m_Handle.GetInputCount() - 1));
			}
			bool flag2 = mask == null;
			if (flag2)
			{
				throw new ArgumentNullException("mask");
			}
			AnimationLayerMixerPlayable.SetLayerMaskFromAvatarMaskInternal(ref this.m_Handle, layerIndex, mask);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00005362 File Offset: 0x00003562
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return AnimationLayerMixerPlayable.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x0600037F RID: 895
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool IsLayerAdditiveInternal(ref PlayableHandle handle, uint layerIndex);

		// Token: 0x06000380 RID: 896
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetLayerAdditiveInternal(ref PlayableHandle handle, uint layerIndex, bool value);

		// Token: 0x06000381 RID: 897
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetLayerMaskFromAvatarMaskInternal(ref PlayableHandle handle, uint layerIndex, AvatarMask mask);

		// Token: 0x06000383 RID: 899
		[MethodImpl(4096)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);

		// Token: 0x04000147 RID: 327
		private PlayableHandle m_Handle;

		// Token: 0x04000148 RID: 328
		private static readonly AnimationLayerMixerPlayable m_NullPlayable = new AnimationLayerMixerPlayable(PlayableHandle.Null);
	}
}
