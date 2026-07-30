using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002E RID: 46
	[TrackClipType(typeof(TrackAsset))]
	[SupportsChildTracks(null, 2147483647)]
	[Serializable]
	public class GroupTrack : TrackAsset
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00002167 File Offset: 0x00000367
		internal override bool CanCompileClips()
		{
			return false;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00008454 File Offset: 0x00006654
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}
	}
}
