using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000023 RID: 35
	public interface IMarker
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600020F RID: 527
		// (set) Token: 0x06000210 RID: 528
		double time { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000211 RID: 529
		TrackAsset parent { get; }

		// Token: 0x06000212 RID: 530
		void Initialize(TrackAsset parent);
	}
}
