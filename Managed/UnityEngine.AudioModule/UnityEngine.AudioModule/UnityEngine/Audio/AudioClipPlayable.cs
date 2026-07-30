using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Audio/Public/Director/AudioClipPlayable.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AudioClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	public struct AudioClipPlayable : IPlayable, IEquatable<AudioClipPlayable>
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00002ED0 File Offset: 0x000010D0
		public static AudioClipPlayable Create(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle playableHandle = AudioClipPlayable.CreateHandle(graph, clip, looping);
			AudioClipPlayable audioClipPlayable = new AudioClipPlayable(playableHandle);
			bool flag = clip != null;
			if (flag)
			{
				audioClipPlayable.SetDuration((double)clip.length);
			}
			return audioClipPlayable;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00002F10 File Offset: 0x00001110
		private static PlayableHandle CreateHandle(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AudioClipPlayable.InternalCreateAudioClipPlayable(ref graph, clip, looping, ref @null);
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

		// Token: 0x06000176 RID: 374 RVA: 0x00002F44 File Offset: 0x00001144
		internal AudioClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AudioClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00002F80 File Offset: 0x00001180
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00002F98 File Offset: 0x00001198
		public static implicit operator Playable(AudioClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00002FB8 File Offset: 0x000011B8
		public static explicit operator AudioClipPlayable(Playable playable)
		{
			return new AudioClipPlayable(playable.GetHandle());
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00002FD8 File Offset: 0x000011D8
		public bool Equals(AudioClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00002FFC File Offset: 0x000011FC
		public AudioClip GetClip()
		{
			return AudioClipPlayable.GetClipInternal(ref this.m_Handle);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003019 File Offset: 0x00001219
		public void SetClip(AudioClip value)
		{
			AudioClipPlayable.SetClipInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000302C File Offset: 0x0000122C
		public bool GetLooped()
		{
			return AudioClipPlayable.GetLoopedInternal(ref this.m_Handle);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003049 File Offset: 0x00001249
		public void SetLooped(bool value)
		{
			AudioClipPlayable.SetLoopedInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000305C File Offset: 0x0000125C
		internal float GetVolume()
		{
			return AudioClipPlayable.GetVolumeInternal(ref this.m_Handle);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000307C File Offset: 0x0000127C
		internal void SetVolume(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable volume outside of range (0.0 - 1.0): " + value);
			}
			AudioClipPlayable.SetVolumeInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000030C4 File Offset: 0x000012C4
		internal float GetStereoPan()
		{
			return AudioClipPlayable.GetStereoPanInternal(ref this.m_Handle);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000030E4 File Offset: 0x000012E4
		internal void SetStereoPan(float value)
		{
			bool flag = value < -1f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable stereo pan outside of range (-1.0 - 1.0): " + value);
			}
			AudioClipPlayable.SetStereoPanInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000312C File Offset: 0x0000132C
		internal float GetSpatialBlend()
		{
			return AudioClipPlayable.GetSpatialBlendInternal(ref this.m_Handle);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000314C File Offset: 0x0000134C
		internal void SetSpatialBlend(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable spatial blend outside of range (0.0 - 1.0): " + value);
			}
			AudioClipPlayable.SetSpatialBlendInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003194 File Offset: 0x00001394
		[Obsolete("IsPlaying() has been deprecated. Use IsChannelPlaying() instead (UnityUpgradable) -> IsChannelPlaying()", true)]
		[EditorBrowsable(1)]
		public bool IsPlaying()
		{
			return this.IsChannelPlaying();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000031AC File Offset: 0x000013AC
		public bool IsChannelPlaying()
		{
			return AudioClipPlayable.GetIsChannelPlayingInternal(ref this.m_Handle);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000031CC File Offset: 0x000013CC
		public double GetStartDelay()
		{
			return AudioClipPlayable.GetStartDelayInternal(ref this.m_Handle);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000031E9 File Offset: 0x000013E9
		internal void SetStartDelay(double value)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, value);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000031FC File Offset: 0x000013FC
		public double GetPauseDelay()
		{
			return AudioClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000321C File Offset: 0x0000141C
		internal void GetPauseDelay(double value)
		{
			double pauseDelayInternal = AudioClipPlayable.GetPauseDelayInternal(ref this.m_Handle);
			bool flag = this.m_Handle.GetPlayState() == PlayState.Playing && (value < 0.05 || (pauseDelayInternal != 0.0 && pauseDelayInternal < 0.05));
			if (flag)
			{
				throw new ArgumentException("AudioClipPlayable.pauseDelay: Setting new delay when existing delay is too small or 0.0 (" + pauseDelayInternal + "), audio system will not be able to change in time");
			}
			AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000329D File Offset: 0x0000149D
		public void Seek(double startTime, double startDelay)
		{
			this.Seek(startTime, startDelay, 0.0);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000032B4 File Offset: 0x000014B4
		public void Seek(double startTime, double startDelay, [DefaultValue("0")] double duration)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, startDelay);
			bool flag = duration > 0.0;
			if (flag)
			{
				this.m_Handle.SetDuration(duration + startTime);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, startDelay + duration);
			}
			else
			{
				this.m_Handle.SetDuration(double.MaxValue);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, 0.0);
			}
			this.m_Handle.SetTime(startTime);
			this.m_Handle.Play();
		}

		// Token: 0x0600018D RID: 397
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern AudioClip GetClipInternal(ref PlayableHandle hdl);

		// Token: 0x0600018E RID: 398
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetClipInternal(ref PlayableHandle hdl, AudioClip clip);

		// Token: 0x0600018F RID: 399
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetLoopedInternal(ref PlayableHandle hdl);

		// Token: 0x06000190 RID: 400
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetLoopedInternal(ref PlayableHandle hdl, bool looped);

		// Token: 0x06000191 RID: 401
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern float GetVolumeInternal(ref PlayableHandle hdl);

		// Token: 0x06000192 RID: 402
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetVolumeInternal(ref PlayableHandle hdl, float volume);

		// Token: 0x06000193 RID: 403
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern float GetStereoPanInternal(ref PlayableHandle hdl);

		// Token: 0x06000194 RID: 404
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetStereoPanInternal(ref PlayableHandle hdl, float stereoPan);

		// Token: 0x06000195 RID: 405
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern float GetSpatialBlendInternal(ref PlayableHandle hdl);

		// Token: 0x06000196 RID: 406
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetSpatialBlendInternal(ref PlayableHandle hdl, float spatialBlend);

		// Token: 0x06000197 RID: 407
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool GetIsChannelPlayingInternal(ref PlayableHandle hdl);

		// Token: 0x06000198 RID: 408
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern double GetStartDelayInternal(ref PlayableHandle hdl);

		// Token: 0x06000199 RID: 409
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetStartDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x0600019A RID: 410
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern double GetPauseDelayInternal(ref PlayableHandle hdl);

		// Token: 0x0600019B RID: 411
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetPauseDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x0600019C RID: 412
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool InternalCreateAudioClipPlayable(ref PlayableGraph graph, AudioClip clip, bool looping, ref PlayableHandle handle);

		// Token: 0x0600019D RID: 413
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x04000065 RID: 101
		private PlayableHandle m_Handle;
	}
}
