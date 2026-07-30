using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioHighPassFilter : Behaviour
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E4 RID: 228
		// (set) Token: 0x060000E5 RID: 229
		public extern float cutoffFrequency
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E6 RID: 230
		// (set) Token: 0x060000E7 RID: 231
		public extern float highpassResonanceQ
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
