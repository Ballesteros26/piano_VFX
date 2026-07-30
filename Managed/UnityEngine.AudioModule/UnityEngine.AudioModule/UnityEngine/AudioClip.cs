using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	[StaticAccessor("AudioClipBindings", StaticAccessorType.DoubleColon)]
	public sealed class AudioClip : Object
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000022E3 File Offset: 0x000004E3
		private AudioClip()
		{
		}

		// Token: 0x06000025 RID: 37
		[MethodImpl(4096)]
		private static extern bool GetData(AudioClip clip, [Out] float[] data, int numSamples, int samplesOffset);

		// Token: 0x06000026 RID: 38
		[MethodImpl(4096)]
		private static extern bool SetData(AudioClip clip, float[] data, int numsamples, int samplesOffset);

		// Token: 0x06000027 RID: 39
		[MethodImpl(4096)]
		private static extern AudioClip Construct_Internal();

		// Token: 0x06000028 RID: 40
		[MethodImpl(4096)]
		private extern string GetName();

		// Token: 0x06000029 RID: 41
		[MethodImpl(4096)]
		private extern void CreateUserSound(string name, int lengthSamples, int channels, int frequency, bool stream);

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42
		[NativeProperty("LengthSec")]
		public extern float length
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43
		[NativeProperty("SampleCount")]
		public extern int samples
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44
		[NativeProperty("ChannelCount")]
		public extern int channels
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45
		public extern int frequency
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46
		[Obsolete("Use AudioClip.loadState instead to get more detailed information about the loading process.")]
		public extern bool isReadyToPlay
		{
			[NativeName("ReadyToPlay")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47
		public extern AudioClipLoadType loadType
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000030 RID: 48
		[MethodImpl(4096)]
		public extern bool LoadAudioData();

		// Token: 0x06000031 RID: 49
		[MethodImpl(4096)]
		public extern bool UnloadAudioData();

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50
		public extern bool preloadAudioData
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51
		public extern bool ambisonic
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52
		public extern bool loadInBackground
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000035 RID: 53
		public extern AudioDataLoadState loadState
		{
			[NativeMethod(Name = "AudioClipBindings::GetLoadState", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000022FC File Offset: 0x000004FC
		public bool GetData(float[] data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.GetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				int num = ((data != null) ? (data.Length / this.channels) : 0);
				flag2 = AudioClip.GetData(this, data, num, offsetSamples);
			}
			return flag2;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002358 File Offset: 0x00000558
		public bool SetData(float[] data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.SetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				bool flag3 = offsetSamples < 0 || offsetSamples >= this.samples;
				if (flag3)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid offsetSamples");
				}
				bool flag4 = data == null || data.Length == 0;
				if (flag4)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid data");
				}
				flag2 = AudioClip.SetData(this, data, data.Length / this.channels, offsetSamples);
			}
			return flag2;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000023E8 File Offset: 0x000005E8
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002408 File Offset: 0x00000608
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002428 File Offset: 0x00000628
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, pcmsetpositioncallback);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000244C File Offset: 0x0000064C
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, null, null);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002470 File Offset: 0x00000670
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002494 File Offset: 0x00000694
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = lengthSamples <= 0;
			if (flag2)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			bool flag3 = channels <= 0;
			if (flag3)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			bool flag4 = frequency <= 0;
			if (flag4)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = AudioClip.Construct_Internal();
			bool flag5 = pcmreadercallback != null;
			if (flag5)
			{
				audioClip.m_PCMReaderCallback += pcmreadercallback;
			}
			bool flag6 = pcmsetpositioncallback != null;
			if (flag6)
			{
				audioClip.m_PCMSetPositionCallback += pcmsetpositioncallback;
			}
			audioClip.CreateUserSound(name, lengthSamples, channels, frequency, stream);
			return audioClip;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00002538 File Offset: 0x00000738
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00002570 File Offset: 0x00000770
		[field: DebuggerBrowsable(0)]
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback = null;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000040 RID: 64 RVA: 0x000025A8 File Offset: 0x000007A8
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x000025E0 File Offset: 0x000007E0
		[field: DebuggerBrowsable(0)]
		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback = null;

		// Token: 0x06000042 RID: 66 RVA: 0x00002618 File Offset: 0x00000818
		[RequiredByNativeCode]
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			bool flag = this.m_PCMReaderCallback != null;
			if (flag)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002640 File Offset: 0x00000840
		[RequiredByNativeCode]
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			bool flag = this.m_PCMSetPositionCallback != null;
			if (flag)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x06000045 RID: 69
		public delegate void PCMReaderCallback(float[] data);

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x06000049 RID: 73
		public delegate void PCMSetPositionCallback(int position);
	}
}
