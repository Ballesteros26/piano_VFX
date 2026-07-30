using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Audio
{
	// Token: 0x02000028 RID: 40
	[NativeHeader("Modules/Audio/Public/AudioMixerSnapshot.h")]
	public class AudioMixerSnapshot : Object, ISubAssetNotDuplicatable
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00003346 File Offset: 0x00001546
		internal AudioMixerSnapshot()
		{
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B6 RID: 438
		[NativeProperty]
		public extern AudioMixer audioMixer
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000034F0 File Offset: 0x000016F0
		public void TransitionTo(float timeToReach)
		{
			this.audioMixer.TransitionToSnapshot(this, timeToReach);
		}
	}
}
