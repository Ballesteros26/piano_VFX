using System;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	public struct JointSuspension2D
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00005DF0 File Offset: 0x00003FF0
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00005E08 File Offset: 0x00004008
		public float dampingRatio
		{
			get
			{
				return this.m_DampingRatio;
			}
			set
			{
				this.m_DampingRatio = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00005E14 File Offset: 0x00004014
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00005E2C File Offset: 0x0000402C
		public float frequency
		{
			get
			{
				return this.m_Frequency;
			}
			set
			{
				this.m_Frequency = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00005E38 File Offset: 0x00004038
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00005E50 File Offset: 0x00004050
		public float angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x04000062 RID: 98
		private float m_DampingRatio;

		// Token: 0x04000063 RID: 99
		private float m_Frequency;

		// Token: 0x04000064 RID: 100
		private float m_Angle;
	}
}
