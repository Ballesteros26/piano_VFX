using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Audio
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Audio/Public/AudioMixerGroup.h")]
	public class AudioMixerGroup : Object, ISubAssetNotDuplicatable
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00003346 File Offset: 0x00001546
		internal AudioMixerGroup()
		{
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001AC RID: 428
		[NativeProperty]
		public extern AudioMixer audioMixer
		{
			[MethodImpl(4096)]
			get;
		}
	}
}
