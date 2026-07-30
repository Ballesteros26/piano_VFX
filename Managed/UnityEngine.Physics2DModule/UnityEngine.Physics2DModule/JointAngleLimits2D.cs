using System;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	public struct JointAngleLimits2D
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00005D18 File Offset: 0x00003F18
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00005D30 File Offset: 0x00003F30
		public float min
		{
			get
			{
				return this.m_LowerAngle;
			}
			set
			{
				this.m_LowerAngle = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00005D3C File Offset: 0x00003F3C
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00005D54 File Offset: 0x00003F54
		public float max
		{
			get
			{
				return this.m_UpperAngle;
			}
			set
			{
				this.m_UpperAngle = value;
			}
		}

		// Token: 0x0400005C RID: 92
		private float m_LowerAngle;

		// Token: 0x0400005D RID: 93
		private float m_UpperAngle;
	}
}
