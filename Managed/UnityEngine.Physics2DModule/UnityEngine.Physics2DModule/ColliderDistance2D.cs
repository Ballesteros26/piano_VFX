using System;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	public struct ColliderDistance2D
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000055EC File Offset: 0x000037EC
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00005604 File Offset: 0x00003804
		public Vector2 pointA
		{
			get
			{
				return this.m_PointA;
			}
			set
			{
				this.m_PointA = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005610 File Offset: 0x00003810
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00005628 File Offset: 0x00003828
		public Vector2 pointB
		{
			get
			{
				return this.m_PointB;
			}
			set
			{
				this.m_PointB = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005634 File Offset: 0x00003834
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000564C File Offset: 0x0000384C
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00005664 File Offset: 0x00003864
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005670 File Offset: 0x00003870
		public bool isOverlapped
		{
			get
			{
				return this.m_Distance < 0f;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00005690 File Offset: 0x00003890
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000056AB File Offset: 0x000038AB
		public bool isValid
		{
			get
			{
				return this.m_IsValid != 0;
			}
			set
			{
				this.m_IsValid = (value ? 1 : 0);
			}
		}

		// Token: 0x04000037 RID: 55
		private Vector2 m_PointA;

		// Token: 0x04000038 RID: 56
		private Vector2 m_PointB;

		// Token: 0x04000039 RID: 57
		private Vector2 m_Normal;

		// Token: 0x0400003A RID: 58
		private float m_Distance;

		// Token: 0x0400003B RID: 59
		private int m_IsValid;
	}
}
