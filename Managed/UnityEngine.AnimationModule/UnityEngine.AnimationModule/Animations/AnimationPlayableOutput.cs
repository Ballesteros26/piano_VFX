using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200004F RID: 79
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutput.bindings.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[StaticAccessor("AnimationPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/Director/AnimationPlayableOutput.h")]
	public struct AnimationPlayableOutput : IPlayableOutput
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00005878 File Offset: 0x00003A78
		public static AnimationPlayableOutput Create(PlayableGraph graph, string name, Animator target)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !AnimationPlayableGraphExtensions.InternalCreateAnimationOutput(ref graph, name, out playableOutputHandle);
			AnimationPlayableOutput animationPlayableOutput;
			if (flag)
			{
				animationPlayableOutput = AnimationPlayableOutput.Null;
			}
			else
			{
				AnimationPlayableOutput animationPlayableOutput2 = new AnimationPlayableOutput(playableOutputHandle);
				animationPlayableOutput2.SetTarget(target);
				animationPlayableOutput = animationPlayableOutput2;
			}
			return animationPlayableOutput;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000058B8 File Offset: 0x00003AB8
		internal AnimationPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AnimationPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000058F4 File Offset: 0x00003AF4
		public static AnimationPlayableOutput Null
		{
			get
			{
				return new AnimationPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00005910 File Offset: 0x00003B10
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00005928 File Offset: 0x00003B28
		public static implicit operator PlayableOutput(AnimationPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00005948 File Offset: 0x00003B48
		public static explicit operator AnimationPlayableOutput(PlayableOutput output)
		{
			return new AnimationPlayableOutput(output.GetHandle());
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00005968 File Offset: 0x00003B68
		public Animator GetTarget()
		{
			return AnimationPlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00005985 File Offset: 0x00003B85
		public void SetTarget(Animator value)
		{
			AnimationPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x060003C6 RID: 966
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern Animator InternalGetTarget(ref PlayableOutputHandle handle);

		// Token: 0x060003C7 RID: 967
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void InternalSetTarget(ref PlayableOutputHandle handle, Animator target);

		// Token: 0x0400014F RID: 335
		private PlayableOutputHandle m_Handle;
	}
}
