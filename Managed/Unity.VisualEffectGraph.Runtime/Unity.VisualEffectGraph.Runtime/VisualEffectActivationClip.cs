using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// Token: 0x02000004 RID: 4
[Serializable]
internal class VisualEffectActivationClip : PlayableAsset, ITimelineClipAsset
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000B RID: 11 RVA: 0x000022C8 File Offset: 0x000004C8
	public ClipCaps clipCaps
	{
		get
		{
			return ClipCaps.None;
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000022CC File Offset: 0x000004CC
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		ScriptPlayable<VisualEffectActivationBehaviour> scriptPlayable = ScriptPlayable<VisualEffectActivationBehaviour>.Create(graph, this.activationBehavior, 0);
		scriptPlayable.GetBehaviour();
		return scriptPlayable;
	}

	// Token: 0x04000008 RID: 8
	public VisualEffectActivationBehaviour activationBehavior = new VisualEffectActivationBehaviour();
}
