using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000174 RID: 372
	[UsedByNativeCode]
	public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0001C958 File Offset: 0x0001AB58
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x0001C970 File Offset: 0x0001AB70
		public int x
		{
			[MethodImpl(256)]
			get
			{
				return this.m_X;
			}
			[MethodImpl(256)]
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0001C97C File Offset: 0x0001AB7C
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x0001C994 File Offset: 0x0001AB94
		public int y
		{
			[MethodImpl(256)]
			get
			{
				return this.m_Y;
			}
			[MethodImpl(256)]
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0001C99E File Offset: 0x0001AB9E
		[MethodImpl(256)]
		public Vector2Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0001C99E File Offset: 0x0001AB9E
		[MethodImpl(256)]
		public void Set(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x17000387 RID: 903
		public int this[int index]
		{
			get
			{
				int num;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					num = this.y;
				}
				else
				{
					num = this.x;
				}
				return num;
			}
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0001CA70 File Offset: 0x0001AC70
		public int sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		[MethodImpl(256)]
		public static float Distance(Vector2Int a, Vector2Int b)
		{
			float num = (float)(a.x - b.x);
			float num2 = (float)(a.y - b.y);
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		[MethodImpl(256)]
		public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0001CB24 File Offset: 0x0001AD24
		[MethodImpl(256)]
		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0001CB64 File Offset: 0x0001AD64
		[MethodImpl(256)]
		public static Vector2Int Scale(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0001CB99 File Offset: 0x0001AD99
		[MethodImpl(256)]
		public void Scale(Vector2Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		[MethodImpl(256)]
		public void Clamp(Vector2Int min, Vector2Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0001CC3C File Offset: 0x0001AE3C
		public static implicit operator Vector2(Vector2Int v)
		{
			return new Vector2((float)v.x, (float)v.y);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0001CC64 File Offset: 0x0001AE64
		public static explicit operator Vector3Int(Vector2Int v)
		{
			return new Vector3Int(v.x, v.y, 0);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0001CC8C File Offset: 0x0001AE8C
		[MethodImpl(256)]
		public static Vector2Int FloorToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		[MethodImpl(256)]
		public static Vector2Int CeilToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0001CCEC File Offset: 0x0001AEEC
		[MethodImpl(256)]
		public static Vector2Int RoundToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0001CD1C File Offset: 0x0001AF1C
		[MethodImpl(256)]
		public static Vector2Int operator -(Vector2Int v)
		{
			return new Vector2Int(-v.x, -v.y);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0001CD44 File Offset: 0x0001AF44
		[MethodImpl(256)]
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		[MethodImpl(256)]
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		[MethodImpl(256)]
		public static Vector2Int operator *(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public static Vector2Int operator *(int a, Vector2Int b)
		{
			return new Vector2Int(a * b.x, a * b.y);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0001CE18 File Offset: 0x0001B018
		[MethodImpl(256)]
		public static Vector2Int operator *(Vector2Int a, int b)
		{
			return new Vector2Int(a.x * b, a.y * b);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0001CE44 File Offset: 0x0001B044
		[MethodImpl(256)]
		public static Vector2Int operator /(Vector2Int a, int b)
		{
			return new Vector2Int(a.x / b, a.y / b);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0001CE70 File Offset: 0x0001B070
		[MethodImpl(256)]
		public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0001CEA8 File Offset: 0x0001B0A8
		[MethodImpl(256)]
		public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0001CEC4 File Offset: 0x0001B0C4
		[MethodImpl(256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector2Int);
			return !flag && this.Equals((Vector2Int)other);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0001CEF8 File Offset: 0x0001B0F8
		[MethodImpl(256)]
		public bool Equals(Vector2Int other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0001CF2C File Offset: 0x0001B12C
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0001CF60 File Offset: 0x0001B160
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0001CF84 File Offset: 0x0001B184
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0001CFA8 File Offset: 0x0001B1A8
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		public static Vector2Int zero
		{
			get
			{
				return Vector2Int.s_Zero;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0001D008 File Offset: 0x0001B208
		public static Vector2Int one
		{
			get
			{
				return Vector2Int.s_One;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0001D020 File Offset: 0x0001B220
		public static Vector2Int up
		{
			get
			{
				return Vector2Int.s_Up;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0001D038 File Offset: 0x0001B238
		public static Vector2Int down
		{
			get
			{
				return Vector2Int.s_Down;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0001D050 File Offset: 0x0001B250
		public static Vector2Int left
		{
			get
			{
				return Vector2Int.s_Left;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0001D068 File Offset: 0x0001B268
		public static Vector2Int right
		{
			get
			{
				return Vector2Int.s_Right;
			}
		}

		// Token: 0x040005FB RID: 1531
		private int m_X;

		// Token: 0x040005FC RID: 1532
		private int m_Y;

		// Token: 0x040005FD RID: 1533
		private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);

		// Token: 0x040005FE RID: 1534
		private static readonly Vector2Int s_One = new Vector2Int(1, 1);

		// Token: 0x040005FF RID: 1535
		private static readonly Vector2Int s_Up = new Vector2Int(0, 1);

		// Token: 0x04000600 RID: 1536
		private static readonly Vector2Int s_Down = new Vector2Int(0, -1);

		// Token: 0x04000601 RID: 1537
		private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);

		// Token: 0x04000602 RID: 1538
		private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
	}
}
