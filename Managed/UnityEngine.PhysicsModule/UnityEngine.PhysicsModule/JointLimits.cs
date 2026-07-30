using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	public struct JointLimits
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022F8 File Offset: 0x000004F8
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002310 File Offset: 0x00000510
		public float min
		{
			get
			{
				return this.m_Min;
			}
			set
			{
				this.m_Min = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000231C File Offset: 0x0000051C
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002334 File Offset: 0x00000534
		public float max
		{
			get
			{
				return this.m_Max;
			}
			set
			{
				this.m_Max = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002340 File Offset: 0x00000540
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002358 File Offset: 0x00000558
		public float bounciness
		{
			get
			{
				return this.m_Bounciness;
			}
			set
			{
				this.m_Bounciness = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002364 File Offset: 0x00000564
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000237C File Offset: 0x0000057C
		public float bounceMinVelocity
		{
			get
			{
				return this.m_BounceMinVelocity;
			}
			set
			{
				this.m_BounceMinVelocity = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000023A0 File Offset: 0x000005A0
		public float contactDistance
		{
			get
			{
				return this.m_ContactDistance;
			}
			set
			{
				this.m_ContactDistance = value;
			}
		}

		// Token: 0x04000038 RID: 56
		private float m_Min;

		// Token: 0x04000039 RID: 57
		private float m_Max;

		// Token: 0x0400003A RID: 58
		private float m_Bounciness;

		// Token: 0x0400003B RID: 59
		private float m_BounceMinVelocity;

		// Token: 0x0400003C RID: 60
		private float m_ContactDistance;

		// Token: 0x0400003D RID: 61
		[EditorBrowsable(1)]
		[Obsolete("minBounce and maxBounce are replaced by a single JointLimits.bounciness for both limit ends.", true)]
		public float minBounce;

		// Token: 0x0400003E RID: 62
		[Obsolete("minBounce and maxBounce are replaced by a single JointLimits.bounciness for both limit ends.", true)]
		[EditorBrowsable(1)]
		public float maxBounce;
	}
}
