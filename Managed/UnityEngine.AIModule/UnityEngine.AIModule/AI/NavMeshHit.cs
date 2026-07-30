using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000C RID: 12
	[MovedFrom("UnityEngine")]
	public struct NavMeshHit
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002570 File Offset: 0x00000770
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00002588 File Offset: 0x00000788
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002594 File Offset: 0x00000794
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000025AC File Offset: 0x000007AC
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000025B8 File Offset: 0x000007B8
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000025D0 File Offset: 0x000007D0
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000025DC File Offset: 0x000007DC
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000025F4 File Offset: 0x000007F4
		public int mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002600 File Offset: 0x00000800
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000261B File Offset: 0x0000081B
		public bool hit
		{
			get
			{
				return this.m_Hit != 0;
			}
			set
			{
				this.m_Hit = (value ? 1 : 0);
			}
		}

		// Token: 0x0400001A RID: 26
		private Vector3 m_Position;

		// Token: 0x0400001B RID: 27
		private Vector3 m_Normal;

		// Token: 0x0400001C RID: 28
		private float m_Distance;

		// Token: 0x0400001D RID: 29
		private int m_Mask;

		// Token: 0x0400001E RID: 30
		private int m_Hit;
	}
}
