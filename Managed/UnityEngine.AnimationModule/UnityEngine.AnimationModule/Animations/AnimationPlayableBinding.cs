using System;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x0200003E RID: 62
	public static class AnimationPlayableBinding
	{
		// Token: 0x06000299 RID: 665 RVA: 0x00004558 File Offset: 0x00002758
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(Animator), new PlayableBinding.CreateOutputMethod(AnimationPlayableBinding.CreateAnimationOutput));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00004588 File Offset: 0x00002788
		private static PlayableOutput CreateAnimationOutput(PlayableGraph graph, string name)
		{
			return AnimationPlayableOutput.Create(graph, name, null);
		}
	}
}
