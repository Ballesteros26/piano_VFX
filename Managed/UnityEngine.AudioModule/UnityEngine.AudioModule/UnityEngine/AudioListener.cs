using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	[RequireComponent(typeof(Transform))]
	[StaticAccessor("AudioListenerBindings", StaticAccessorType.DoubleColon)]
	public sealed class AudioListener : AudioBehaviour
	{
		// Token: 0x0600004D RID: 77
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void GetOutputDataHelper([Out] float[] samples, int channel);

		// Token: 0x0600004E RID: 78
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void GetSpectrumDataHelper([Out] float[] samples, int channel, FFTWindow window);

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004F RID: 79
		// (set) Token: 0x06000050 RID: 80
		public static extern float volume
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000051 RID: 81
		// (set) Token: 0x06000052 RID: 82
		[NativeProperty("ListenerPause")]
		public static extern bool pause
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000053 RID: 83
		// (set) Token: 0x06000054 RID: 84
		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002674 File Offset: 0x00000874
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			AudioListener.GetOutputDataHelper(array, channel);
			return array;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002696 File Offset: 0x00000896
		public static void GetOutputData(float[] samples, int channel)
		{
			AudioListener.GetOutputDataHelper(samples, channel);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000026A4 File Offset: 0x000008A4
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public static float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			AudioListener.GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026C7 File Offset: 0x000008C7
		public static void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioListener.GetSpectrumDataHelper(samples, channel, window);
		}
	}
}
