using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.Class)]
	[Obsolete("TrackMediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
	public class TrackMediaType : Attribute
	{
		// Token: 0x06000298 RID: 664 RVA: 0x000092EC File Offset: 0x000074EC
		public TrackMediaType(TimelineAsset.MediaType mt)
		{
			this.m_MediaType = mt;
		}

		// Token: 0x040000E4 RID: 228
		public readonly TimelineAsset.MediaType m_MediaType;
	}
}
