using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x02000027 RID: 39
	[RequiredByNativeCode]
	[NativeHeader("Modules/Audio/Public/Director/AudioMixerPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixerPlayable.bindings.h")]
	[StaticAccessor("AudioMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	public struct AudioMixerPlayable : IPlayable, IEquatable<AudioMixerPlayable>
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000033D8 File Offset: 0x000015D8
		public static AudioMixerPlayable Create(PlayableGraph graph, int inputCount = 0, bool normalizeInputVolumes = false)
		{
			PlayableHandle playableHandle = AudioMixerPlayable.CreateHandle(graph, inputCount, normalizeInputVolumes);
			return new AudioMixerPlayable(playableHandle);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000033FC File Offset: 0x000015FC
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount, bool normalizeInputVolumes)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AudioMixerPlayable.CreateAudioMixerPlayableInternal(ref graph, normalizeInputVolumes, ref @null);
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

		// Token: 0x060001AF RID: 431 RVA: 0x00003438 File Offset: 0x00001638
		internal AudioMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AudioMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00003474 File Offset: 0x00001674
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000348C File Offset: 0x0000168C
		public static implicit operator Playable(AudioMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000034AC File Offset: 0x000016AC
		public static explicit operator AudioMixerPlayable(Playable playable)
		{
			return new AudioMixerPlayable(playable.GetHandle());
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000034CC File Offset: 0x000016CC
		public bool Equals(AudioMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060001B4 RID: 436
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool CreateAudioMixerPlayableInternal(ref PlayableGraph graph, bool normalizeInputVolumes, ref PlayableHandle handle);

		// Token: 0x04000069 RID: 105
		private PlayableHandle m_Handle;
	}
}
