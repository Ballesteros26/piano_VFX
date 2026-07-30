using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioEchoFilter : Behaviour
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EC RID: 236
		// (set) Token: 0x060000ED RID: 237
		public extern float delay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000EE RID: 238
		// (set) Token: 0x060000EF RID: 239
		public extern float decayRatio
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F0 RID: 240
		// (set) Token: 0x060000F1 RID: 241
		public extern float dryMix
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F2 RID: 242
		// (set) Token: 0x060000F3 RID: 243
		public extern float wetMix
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
