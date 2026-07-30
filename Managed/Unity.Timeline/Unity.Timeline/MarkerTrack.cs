using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000027 RID: 39
	[TrackBindingType(typeof(GameObject))]
	[HideInMenu]
	[Serializable]
	public class MarkerTrack : TrackAsset
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007F80 File Offset: 0x00006180
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				if (!(this == base.timelineAsset.markerTrack))
				{
					return base.outputs;
				}
				return new List<PlayableBinding> { ScriptPlayableBinding.Create(base.name, null, typeof(GameObject)) };
			}
		}
	}
}
