using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioLowPassFilter : Behaviour
	{
		// Token: 0x060000DB RID: 219
		[MethodImpl(4096)]
		private extern AnimationCurve GetCustomLowpassLevelCurveCopy();

		// Token: 0x060000DC RID: 220
		[NativeThrows]
		[NativeMethod(Name = "AudioLowPassFilterBindings::SetCustomLowpassLevelCurveHelper", IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void SetCustomLowpassLevelCurveHelper(AudioLowPassFilter source, AnimationCurve curve);

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002988 File Offset: 0x00000B88
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000029A0 File Offset: 0x00000BA0
		public AnimationCurve customCutoffCurve
		{
			get
			{
				return this.GetCustomLowpassLevelCurveCopy();
			}
			set
			{
				AudioLowPassFilter.SetCustomLowpassLevelCurveHelper(this, value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000DF RID: 223
		// (set) Token: 0x060000E0 RID: 224
		public extern float cutoffFrequency
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E1 RID: 225
		// (set) Token: 0x060000E2 RID: 226
		public extern float lowpassResonanceQ
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
