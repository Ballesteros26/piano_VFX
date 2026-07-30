using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004B RID: 75
	public interface IPropertyPreview
	{
		// Token: 0x060002CC RID: 716
		void GatherProperties(PlayableDirector director, IPropertyCollector driver);
	}
}
