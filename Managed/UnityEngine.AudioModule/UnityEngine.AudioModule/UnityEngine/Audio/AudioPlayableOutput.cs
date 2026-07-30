using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x0200002B RID: 43
	[RequiredByNativeCode]
	[StaticAccessor("AudioPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	[NativeHeader("Modules/Audio/Public/Director/AudioPlayableOutput.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableOutput.bindings.h")]
	public struct AudioPlayableOutput : IPlayableOutput
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00003554 File Offset: 0x00001754
		public static AudioPlayableOutput Create(PlayableGraph graph, string name, AudioSource target)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !AudioPlayableGraphExtensions.InternalCreateAudioOutput(ref graph, name, out playableOutputHandle);
			AudioPlayableOutput audioPlayableOutput;
			if (flag)
			{
				audioPlayableOutput = AudioPlayableOutput.Null;
			}
			else
			{
				AudioPlayableOutput audioPlayableOutput2 = new AudioPlayableOutput(playableOutputHandle);
				audioPlayableOutput2.SetTarget(target);
				audioPlayableOutput = audioPlayableOutput2;
			}
			return audioPlayableOutput;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00003594 File Offset: 0x00001794
		internal AudioPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AudioPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000035D0 File Offset: 0x000017D0
		public static AudioPlayableOutput Null
		{
			get
			{
				return new AudioPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000035EC File Offset: 0x000017EC
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00003604 File Offset: 0x00001804
		public static implicit operator PlayableOutput(AudioPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00003624 File Offset: 0x00001824
		public static explicit operator AudioPlayableOutput(PlayableOutput output)
		{
			return new AudioPlayableOutput(output.GetHandle());
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00003644 File Offset: 0x00001844
		public AudioSource GetTarget()
		{
			return AudioPlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00003661 File Offset: 0x00001861
		public void SetTarget(AudioSource value)
		{
			AudioPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00003674 File Offset: 0x00001874
		public bool GetEvaluateOnSeek()
		{
			return AudioPlayableOutput.InternalGetEvaluateOnSeek(ref this.m_Handle);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00003691 File Offset: 0x00001891
		public void SetEvaluateOnSeek(bool value)
		{
			AudioPlayableOutput.InternalSetEvaluateOnSeek(ref this.m_Handle, value);
		}

		// Token: 0x060001C5 RID: 453
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern AudioSource InternalGetTarget(ref PlayableOutputHandle output);

		// Token: 0x060001C6 RID: 454
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void InternalSetTarget(ref PlayableOutputHandle output, AudioSource target);

		// Token: 0x060001C7 RID: 455
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool InternalGetEvaluateOnSeek(ref PlayableOutputHandle output);

		// Token: 0x060001C8 RID: 456
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void InternalSetEvaluateOnSeek(ref PlayableOutputHandle output, bool value);

		// Token: 0x0400006A RID: 106
		private PlayableOutputHandle m_Handle;
	}
}
