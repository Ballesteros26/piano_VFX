using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[NativeHeader("Modules/Audio/Public/AudioReverbZone.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class AudioReverbZone : Behaviour
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B8 RID: 184
		// (set) Token: 0x060000B9 RID: 185
		public extern float minDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BA RID: 186
		// (set) Token: 0x060000BB RID: 187
		public extern float maxDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000BC RID: 188
		// (set) Token: 0x060000BD RID: 189
		public extern AudioReverbPreset reverbPreset
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BE RID: 190
		// (set) Token: 0x060000BF RID: 191
		public extern int room
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C0 RID: 192
		// (set) Token: 0x060000C1 RID: 193
		public extern int roomHF
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C2 RID: 194
		// (set) Token: 0x060000C3 RID: 195
		public extern int roomLF
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C4 RID: 196
		// (set) Token: 0x060000C5 RID: 197
		public extern float decayTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C6 RID: 198
		// (set) Token: 0x060000C7 RID: 199
		public extern float decayHFRatio
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C8 RID: 200
		// (set) Token: 0x060000C9 RID: 201
		public extern int reflections
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CA RID: 202
		// (set) Token: 0x060000CB RID: 203
		public extern float reflectionsDelay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CC RID: 204
		// (set) Token: 0x060000CD RID: 205
		public extern int reverb
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CE RID: 206
		// (set) Token: 0x060000CF RID: 207
		public extern float reverbDelay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D0 RID: 208
		// (set) Token: 0x060000D1 RID: 209
		public extern float HFReference
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D2 RID: 210
		// (set) Token: 0x060000D3 RID: 211
		public extern float LFReference
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002958 File Offset: 0x00000B58
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000297A File Offset: 0x00000B7A
		[Obsolete("Warning! roomRolloffFactor is no longer supported.")]
		public float roomRolloffFactor
		{
			get
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
				return 10f;
			}
			set
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D6 RID: 214
		// (set) Token: 0x060000D7 RID: 215
		public extern float diffusion
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D8 RID: 216
		// (set) Token: 0x060000D9 RID: 217
		public extern float density
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
