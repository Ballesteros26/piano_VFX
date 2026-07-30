using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public class PlayableTrack : TrackAsset
	{
		// Token: 0x06000296 RID: 662 RVA: 0x000092C6 File Offset: 0x000074C6
		protected override void OnCreateClip(TimelineClip clip)
		{
			if (clip.asset != null)
			{
				clip.displayName = clip.asset.GetType().Name;
			}
		}
	}
}
