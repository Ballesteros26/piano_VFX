using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Class)]
	public class TrackColorAttribute : Attribute
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000618F File Offset: 0x0000438F
		public Color color
		{
			get
			{
				return this.m_Color;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006197 File Offset: 0x00004397
		public TrackColorAttribute(float r, float g, float b)
		{
			this.m_Color = new Color(r, g, b);
		}

		// Token: 0x04000084 RID: 132
		private Color m_Color;
	}
}
