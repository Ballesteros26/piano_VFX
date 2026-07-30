using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Audio;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	[RequireComponent(typeof(Transform))]
	[StaticAccessor("AudioSourceBindings", StaticAccessorType.DoubleColon)]
	public sealed class AudioSource : AudioBehaviour
	{
		// Token: 0x0600005A RID: 90
		[MethodImpl(4096)]
		private static extern float GetPitch(AudioSource source);

		// Token: 0x0600005B RID: 91
		[MethodImpl(4096)]
		private static extern void SetPitch(AudioSource source, float pitch);

		// Token: 0x0600005C RID: 92
		[MethodImpl(4096)]
		private static extern void PlayHelper(AudioSource source, ulong delay);

		// Token: 0x0600005D RID: 93
		[MethodImpl(4096)]
		private extern void Play(double delay);

		// Token: 0x0600005E RID: 94
		[MethodImpl(4096)]
		private static extern void PlayOneShotHelper(AudioSource source, AudioClip clip, float volumeScale);

		// Token: 0x0600005F RID: 95
		[MethodImpl(4096)]
		private extern void Stop(bool stopOneShots);

		// Token: 0x06000060 RID: 96
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void SetCustomCurveHelper(AudioSource source, AudioSourceCurveType type, AnimationCurve curve);

		// Token: 0x06000061 RID: 97
		[MethodImpl(4096)]
		private static extern AnimationCurve GetCustomCurveHelper(AudioSource source, AudioSourceCurveType type);

		// Token: 0x06000062 RID: 98
		[MethodImpl(4096)]
		private static extern void GetOutputDataHelper(AudioSource source, [Out] float[] samples, int channel);

		// Token: 0x06000063 RID: 99
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void GetSpectrumDataHelper(AudioSource source, [Out] float[] samples, int channel, FFTWindow window);

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000064 RID: 100
		// (set) Token: 0x06000065 RID: 101
		public extern float volume
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000026DC File Offset: 0x000008DC
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000026F4 File Offset: 0x000008F4
		public float pitch
		{
			get
			{
				return AudioSource.GetPitch(this);
			}
			set
			{
				AudioSource.SetPitch(this, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104
		// (set) Token: 0x06000069 RID: 105
		[NativeProperty("SecPosition")]
		public extern float time
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006A RID: 106
		// (set) Token: 0x0600006B RID: 107
		[NativeProperty("SamplePosition")]
		public extern int timeSamples
		{
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006C RID: 108
		// (set) Token: 0x0600006D RID: 109
		[NativeProperty("AudioClip")]
		public extern AudioClip clip
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006E RID: 110
		// (set) Token: 0x0600006F RID: 111
		public extern AudioMixerGroup outputAudioMixerGroup
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000026FF File Offset: 0x000008FF
		[ExcludeFromDocs]
		public void Play()
		{
			AudioSource.PlayHelper(this, 0UL);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000270B File Offset: 0x0000090B
		public void Play([DefaultValue("0")] ulong delay)
		{
			AudioSource.PlayHelper(this, delay);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002716 File Offset: 0x00000916
		public void PlayDelayed(float delay)
		{
			this.Play((delay < 0f) ? 0.0 : (-(double)delay));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002736 File Offset: 0x00000936
		public void PlayScheduled(double time)
		{
			this.Play((time < 0.0) ? 0.0 : time);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002758 File Offset: 0x00000958
		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			this.PlayOneShot(clip, 1f);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002768 File Offset: 0x00000968
		public void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] float volumeScale)
		{
			bool flag = clip == null;
			if (flag)
			{
				Debug.LogWarning("PlayOneShot was called with a null AudioClip.");
			}
			else
			{
				AudioSource.PlayOneShotHelper(this, clip, volumeScale);
			}
		}

		// Token: 0x06000076 RID: 118
		[MethodImpl(4096)]
		public extern void SetScheduledStartTime(double time);

		// Token: 0x06000077 RID: 119
		[MethodImpl(4096)]
		public extern void SetScheduledEndTime(double time);

		// Token: 0x06000078 RID: 120 RVA: 0x00002798 File Offset: 0x00000998
		public void Stop()
		{
			this.Stop(true);
		}

		// Token: 0x06000079 RID: 121
		[MethodImpl(4096)]
		public extern void Pause();

		// Token: 0x0600007A RID: 122
		[MethodImpl(4096)]
		public extern void UnPause();

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007B RID: 123
		public extern bool isPlaying
		{
			[NativeName("IsPlayingScripting")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007C RID: 124
		public extern bool isVirtual
		{
			[NativeName("GetLastVirtualState")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000027A3 File Offset: 0x000009A3
		[ExcludeFromDocs]
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
		{
			AudioSource.PlayClipAtPoint(clip, position, 1f);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000027B4 File Offset: 0x000009B4
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [DefaultValue("1.0F")] float volume)
		{
			GameObject gameObject = new GameObject("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = clip;
			audioSource.spatialBlend = 1f;
			audioSource.volume = volume;
			audioSource.Play();
			Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007F RID: 127
		// (set) Token: 0x06000080 RID: 128
		public extern bool loop
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000081 RID: 129
		// (set) Token: 0x06000082 RID: 130
		public extern bool ignoreListenerVolume
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000083 RID: 131
		// (set) Token: 0x06000084 RID: 132
		public extern bool playOnAwake
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000085 RID: 133
		// (set) Token: 0x06000086 RID: 134
		public extern bool ignoreListenerPause
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000087 RID: 135
		// (set) Token: 0x06000088 RID: 136
		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		[NativeProperty("StereoPan")]
		public extern float panStereo
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008B RID: 139
		// (set) Token: 0x0600008C RID: 140
		[NativeProperty("SpatialBlendMix")]
		public extern float spatialBlend
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008D RID: 141
		// (set) Token: 0x0600008E RID: 142
		public extern bool spatialize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008F RID: 143
		// (set) Token: 0x06000090 RID: 144
		public extern bool spatializePostEffects
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002839 File Offset: 0x00000A39
		public void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve)
		{
			AudioSource.SetCustomCurveHelper(this, type, curve);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002848 File Offset: 0x00000A48
		public AnimationCurve GetCustomCurve(AudioSourceCurveType type)
		{
			return AudioSource.GetCustomCurveHelper(this, type);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000093 RID: 147
		// (set) Token: 0x06000094 RID: 148
		public extern float reverbZoneMix
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000095 RID: 149
		// (set) Token: 0x06000096 RID: 150
		public extern bool bypassEffects
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000097 RID: 151
		// (set) Token: 0x06000098 RID: 152
		public extern bool bypassListenerEffects
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000099 RID: 153
		// (set) Token: 0x0600009A RID: 154
		public extern bool bypassReverbZones
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009B RID: 155
		// (set) Token: 0x0600009C RID: 156
		public extern float dopplerLevel
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009D RID: 157
		// (set) Token: 0x0600009E RID: 158
		public extern float spread
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009F RID: 159
		// (set) Token: 0x060000A0 RID: 160
		public extern int priority
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A1 RID: 161
		// (set) Token: 0x060000A2 RID: 162
		public extern bool mute
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A3 RID: 163
		// (set) Token: 0x060000A4 RID: 164
		public extern float minDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A5 RID: 165
		// (set) Token: 0x060000A6 RID: 166
		public extern float maxDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A7 RID: 167
		// (set) Token: 0x060000A8 RID: 168
		public extern AudioRolloffMode rolloffMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002864 File Offset: 0x00000A64
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioSource.GetOutputDataHelper(this, array, channel);
			return array;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002887 File Offset: 0x00000A87
		public void GetOutputData(float[] samples, int channel)
		{
			AudioSource.GetOutputDataHelper(this, samples, channel);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002894 File Offset: 0x00000A94
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioSource.GetSpectrumDataHelper(this, array, channel, window);
			return array;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000028B8 File Offset: 0x00000AB8
		public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioSource.GetSpectrumDataHelper(this, samples, channel, window);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000028C8 File Offset: 0x00000AC8
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000028EA File Offset: 0x00000AEA
		[Obsolete("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float minVolume
		{
			get
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000028F8 File Offset: 0x00000AF8
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000291A File Offset: 0x00000B1A
		[Obsolete("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float maxVolume
		{
			get
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002928 File Offset: 0x00000B28
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000294A File Offset: 0x00000B4A
		[Obsolete("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float rolloffFactor
		{
			get
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x060000B3 RID: 179
		[MethodImpl(4096)]
		public extern bool SetSpatializerFloat(int index, float value);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(4096)]
		public extern bool GetSpatializerFloat(int index, out float value);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(4096)]
		public extern bool GetAmbisonicDecoderFloat(int index, out float value);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(4096)]
		public extern bool SetAmbisonicDecoderFloat(int index, float value);
	}
}
