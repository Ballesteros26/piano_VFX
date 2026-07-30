using System;
using UnityEngine.Animations;

namespace UnityEngine.Playables
{
	// Token: 0x0200003D RID: 61
	public static class AnimationPlayableUtilities
	{
		// Token: 0x06000294 RID: 660 RVA: 0x000043B4 File Offset: 0x000025B4
		public static void Play(Animator animator, Playable playable, PlayableGraph graph)
		{
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "AnimationClip", animator);
			animationPlayableOutput.SetSourcePlayable(playable, 0);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000043E8 File Offset: 0x000025E8
		public static AnimationClipPlayable PlayClip(Animator animator, AnimationClip clip, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "AnimationClip", animator);
			AnimationClipPlayable animationClipPlayable = AnimationClipPlayable.Create(graph, clip);
			animationPlayableOutput.SetSourcePlayable(animationClipPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationClipPlayable;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00004444 File Offset: 0x00002644
		public static AnimationMixerPlayable PlayMixer(Animator animator, int inputCount, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "Mixer", animator);
			AnimationMixerPlayable animationMixerPlayable = AnimationMixerPlayable.Create(graph, inputCount, false);
			animationPlayableOutput.SetSourcePlayable(animationMixerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationMixerPlayable;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000044A0 File Offset: 0x000026A0
		public static AnimationLayerMixerPlayable PlayLayerMixer(Animator animator, int inputCount, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "Mixer", animator);
			AnimationLayerMixerPlayable animationLayerMixerPlayable = AnimationLayerMixerPlayable.Create(graph, inputCount);
			animationPlayableOutput.SetSourcePlayable(animationLayerMixerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationLayerMixerPlayable;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000044FC File Offset: 0x000026FC
		public static AnimatorControllerPlayable PlayAnimatorController(Animator animator, RuntimeAnimatorController controller, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "AnimatorControllerPlayable", animator);
			AnimatorControllerPlayable animatorControllerPlayable = AnimatorControllerPlayable.Create(graph, controller);
			animationPlayableOutput.SetSourcePlayable(animatorControllerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animatorControllerPlayable;
		}
	}
}
