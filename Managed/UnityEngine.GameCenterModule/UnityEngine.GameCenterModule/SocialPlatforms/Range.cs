using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000010 RID: 16
	public struct Range
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002D37 File Offset: 0x00000F37
		public Range(int fromValue, int valueCount)
		{
			this.from = fromValue;
			this.count = valueCount;
		}

		// Token: 0x04000019 RID: 25
		public int from;

		// Token: 0x0400001A RID: 26
		public int count;
	}
}
