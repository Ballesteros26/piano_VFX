using System;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Animations
{
	// Token: 0x0200003A RID: 58
	public interface IAnimationWindowPreview
	{
		// Token: 0x06000284 RID: 644
		void StartPreview();

		// Token: 0x06000285 RID: 645
		void StopPreview();

		// Token: 0x06000286 RID: 646
		void UpdatePreviewGraph(PlayableGraph graph);

		// Token: 0x06000287 RID: 647
		Playable BuildPreviewGraph(PlayableGraph graph, Playable inputPlayable);
	}
}
