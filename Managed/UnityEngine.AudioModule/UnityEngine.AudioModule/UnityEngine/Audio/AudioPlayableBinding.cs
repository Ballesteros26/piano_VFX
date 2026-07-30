using System;
using UnityEngine.Playables;

namespace UnityEngine.Audio
{
	// Token: 0x02000029 RID: 41
	public static class AudioPlayableBinding
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00003504 File Offset: 0x00001704
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(AudioSource), new PlayableBinding.CreateOutputMethod(AudioPlayableBinding.CreateAudioOutput));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00003534 File Offset: 0x00001734
		private static PlayableOutput CreateAudioOutput(PlayableGraph graph, string name)
		{
			return AudioPlayableOutput.Create(graph, name, null);
		}
	}
}
