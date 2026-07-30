using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002D RID: 45
	public static class TrackAssetExtensions
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00008378 File Offset: 0x00006578
		public static GroupTrack GetGroup(this TrackAsset asset)
		{
			if (asset == null)
			{
				return null;
			}
			return asset.parent as GroupTrack;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008390 File Offset: 0x00006590
		public static void SetGroup(this TrackAsset asset, GroupTrack group)
		{
			if (asset == null || asset == group || asset.parent == group)
			{
				return;
			}
			if (group != null && asset.timelineAsset != group.timelineAsset)
			{
				throw new InvalidOperationException("Cannot assign to a group in a different timeline");
			}
			TimelineAsset timelineAsset = asset.timelineAsset;
			TrackAsset trackAsset = asset.parent as TrackAsset;
			TimelineAsset timelineAsset2 = asset.parent as TimelineAsset;
			if (trackAsset != null || timelineAsset2 != null)
			{
				if (timelineAsset2 != null)
				{
					timelineAsset2.RemoveTrack(asset);
				}
				else
				{
					trackAsset.RemoveSubTrack(asset);
				}
			}
			if (group == null)
			{
				asset.parent = asset.timelineAsset;
				timelineAsset.AddTrackInternal(asset);
				return;
			}
			group.AddChild(asset);
		}
	}
}
