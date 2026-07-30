using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("RaycastHit2D", "struct RaycastHit2D;")]
	[NativeHeader("Runtime/Interfaces/IPhysics2D.h")]
	public struct RaycastHit2D
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00005E5C File Offset: 0x0000405C
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00005E74 File Offset: 0x00004074
		public Vector2 centroid
		{
			get
			{
				return this.m_Centroid;
			}
			set
			{
				this.m_Centroid = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00005E80 File Offset: 0x00004080
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00005E98 File Offset: 0x00004098
		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00005EA4 File Offset: 0x000040A4
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00005EBC File Offset: 0x000040BC
		public Vector2 normal
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00005EC8 File Offset: 0x000040C8
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00005EE0 File Offset: 0x000040E0
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00005EEC File Offset: 0x000040EC
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00005F04 File Offset: 0x00004104
		public float fraction
		{
			get
			{
				return this.m_Fraction;
			}
			set
			{
				this.m_Fraction = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00005F10 File Offset: 0x00004110
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00005F34 File Offset: 0x00004134
		public Rigidbody2D rigidbody
		{
			get
			{
				return (this.collider != null) ? this.collider.attachedRigidbody : null;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00005F64 File Offset: 0x00004164
		public Transform transform
		{
			get
			{
				Rigidbody2D rigidbody = this.rigidbody;
				bool flag = rigidbody != null;
				Transform transform;
				if (flag)
				{
					transform = rigidbody.transform;
				}
				else
				{
					bool flag2 = this.collider != null;
					if (flag2)
					{
						transform = this.collider.transform;
					}
					else
					{
						transform = null;
					}
				}
				return transform;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00005FB0 File Offset: 0x000041B0
		public static implicit operator bool(RaycastHit2D hit)
		{
			return hit.collider != null;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00005FD0 File Offset: 0x000041D0
		public int CompareTo(RaycastHit2D other)
		{
			bool flag = this.collider == null;
			int num;
			if (flag)
			{
				num = 1;
			}
			else
			{
				bool flag2 = other.collider == null;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = this.fraction.CompareTo(other.fraction);
				}
			}
			return num;
		}

		// Token: 0x04000065 RID: 101
		[NativeName("centroid")]
		private Vector2 m_Centroid;

		// Token: 0x04000066 RID: 102
		[NativeName("point")]
		private Vector2 m_Point;

		// Token: 0x04000067 RID: 103
		[NativeName("normal")]
		private Vector2 m_Normal;

		// Token: 0x04000068 RID: 104
		[NativeName("distance")]
		private float m_Distance;

		// Token: 0x04000069 RID: 105
		[NativeName("fraction")]
		private float m_Fraction;

		// Token: 0x0400006A RID: 106
		[NativeName("collider")]
		private int m_Collider;
	}
}
