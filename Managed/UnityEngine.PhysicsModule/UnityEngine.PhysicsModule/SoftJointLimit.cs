using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	public struct SoftJointLimit
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002104 File Offset: 0x00000304
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000211C File Offset: 0x0000031C
		public float limit
		{
			get
			{
				return this.m_Limit;
			}
			set
			{
				this.m_Limit = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("Spring has been moved to SoftJointLimitSpring class in Unity 5", true)]
		[EditorBrowsable(1)]
		public float spring
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000213F File Offset: 0x0000033F
		[EditorBrowsable(1)]
		[Obsolete("Damper has been moved to SoftJointLimitSpring class in Unity 5", true)]
		public float damper
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000215C File Offset: 0x0000035C
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002174 File Offset: 0x00000374
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002180 File Offset: 0x00000380
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002198 File Offset: 0x00000398
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021A4 File Offset: 0x000003A4
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002174 File Offset: 0x00000374
		[EditorBrowsable(1)]
		[Obsolete("Use SoftJointLimit.bounciness instead", true)]
		public float bouncyness
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

		// Token: 0x04000026 RID: 38
		private float m_Limit;

		// Token: 0x04000027 RID: 39
		private float m_Bounciness;

		// Token: 0x04000028 RID: 40
		private float m_ContactDistance;
	}
}
