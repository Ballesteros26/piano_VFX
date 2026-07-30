using System;
using System.Collections.Generic;

namespace UnityEngine.Playables
{
	// Token: 0x0200039D RID: 925
	public interface IPlayableAsset
	{
		// Token: 0x06002008 RID: 8200
		Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002009 RID: 8201
		double duration { get; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600200A RID: 8202
		IEnumerable<PlayableBinding> outputs { get; }
	}
}
