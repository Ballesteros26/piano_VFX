using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioChorusFilter : Behaviour
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F5 RID: 245
		// (set) Token: 0x060000F6 RID: 246
		public extern float dryMix
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F7 RID: 247
		// (set) Token: 0x060000F8 RID: 248
		public extern float wetMix1
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F9 RID: 249
		// (set) Token: 0x060000FA RID: 250
		public extern float wetMix2
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000FB RID: 251
		// (set) Token: 0x060000FC RID: 252
		public extern float wetMix3
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FD RID: 253
		// (set) Token: 0x060000FE RID: 254
		public extern float delay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FF RID: 255
		// (set) Token: 0x06000100 RID: 256
		public extern float rate
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000101 RID: 257
		// (set) Token: 0x06000102 RID: 258
		public extern float depth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000029AC File Offset: 0x00000BAC
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000029CE File Offset: 0x00000BCE
		[Obsolete("Warning! Feedback is deprecated. This property does nothing.")]
		public float feedback
		{
			get
			{
				Debug.LogWarning("Warning! Feedback is deprecated. This property does nothing.");
				return 0f;
			}
			set
			{
				Debug.LogWarning("Warning! Feedback is deprecated. This property does nothing.");
			}
		}
	}
}
