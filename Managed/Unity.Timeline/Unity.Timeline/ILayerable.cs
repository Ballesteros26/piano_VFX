using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002F RID: 47
	public interface ILayerable
	{
		// Token: 0x0600024F RID: 591
		Playable CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount);
	}
}
