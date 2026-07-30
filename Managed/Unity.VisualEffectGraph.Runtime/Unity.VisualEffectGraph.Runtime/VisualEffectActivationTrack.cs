using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.VFX;

// Token: 0x02000006 RID: 6
[TrackColor(0.5990566f, 0.9038978f, 1f)]
[TrackClipType(typeof(VisualEffectActivationClip))]
[TrackBindingType(typeof(VisualEffect))]
internal class VisualEffectActivationTrack : TrackAsset
{
	// Token: 0x06000012 RID: 18 RVA: 0x000023AE File Offset: 0x000005AE
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		return ScriptPlayable<VisualEffectActivationMixerBehaviour>.Create(graph, inputCount);
	}
}
