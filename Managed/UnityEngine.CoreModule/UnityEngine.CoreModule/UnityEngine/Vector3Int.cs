using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000175 RID: 373
	[UsedByNativeCode]
	public struct Vector3Int : IEquatable<Vector3Int>, IFormattable
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
		public int x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x0001D114 File Offset: 0x0001B314
		public int y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0001D120 File Offset: 0x0001B320
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x0001D138 File Offset: 0x0001B338
		public int z
		{
			get
			{
				return this.m_Z;
			}
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0001D142 File Offset: 0x0001B342
		[MethodImpl(256)]
		public Vector3Int(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0001D142 File Offset: 0x0001B342
		[MethodImpl(256)]
		public void Set(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x17000393 RID: 915
		public int this[int index]
		{
			get
			{
				int num;
				switch (index)
				{
				case 0:
					num = this.x;
					break;
				case 1:
					num = this.y;
					break;
				case 2:
					num = this.z;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[] { index }));
				}
				return num;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[] { index }));
				}
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0001D220 File Offset: 0x0001B420
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0001D264 File Offset: 0x0001B464
		public int sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
		[MethodImpl(256)]
		public static float Distance(Vector3Int a, Vector3Int b)
		{
			return (a - b).magnitude;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0001D2C4 File Offset: 0x0001B4C4
		[MethodImpl(256)]
		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0001D314 File Offset: 0x0001B514
		[MethodImpl(256)]
		public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0001D364 File Offset: 0x0001B564
		[MethodImpl(256)]
		public static Vector3Int Scale(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		[MethodImpl(256)]
		public void Scale(Vector3Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0001D3F8 File Offset: 0x0001B5F8
		[MethodImpl(256)]
		public void Clamp(Vector3Int min, Vector3Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
			this.z = Math.Max(min.z, this.z);
			this.z = Math.Min(max.z, this.z);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0001D49C File Offset: 0x0001B69C
		[MethodImpl(256)]
		public static implicit operator Vector3(Vector3Int v)
		{
			return new Vector3((float)v.x, (float)v.y, (float)v.z);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0001D4CC File Offset: 0x0001B6CC
		[MethodImpl(256)]
		public static explicit operator Vector2Int(Vector3Int v)
		{
			return new Vector2Int(v.x, v.y);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		[MethodImpl(256)]
		public static Vector3Int FloorToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0001D52C File Offset: 0x0001B72C
		[MethodImpl(256)]
		public static Vector3Int CeilToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0001D564 File Offset: 0x0001B764
		[MethodImpl(256)]
		public static Vector3Int RoundToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0001D59C File Offset: 0x0001B79C
		[MethodImpl(256)]
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		[MethodImpl(256)]
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0001D624 File Offset: 0x0001B824
		[MethodImpl(256)]
		public static Vector3Int operator *(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0001D668 File Offset: 0x0001B868
		[MethodImpl(256)]
		public static Vector3Int operator -(Vector3Int a)
		{
			return new Vector3Int(-a.x, -a.y, -a.z);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0001D698 File Offset: 0x0001B898
		[MethodImpl(256)]
		public static Vector3Int operator *(Vector3Int a, int b)
		{
			return new Vector3Int(a.x * b, a.y * b, a.z * b);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		[MethodImpl(256)]
		public static Vector3Int operator *(int a, Vector3Int b)
		{
			return new Vector3Int(a * b.x, a * b.y, a * b.z);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0001D700 File Offset: 0x0001B900
		[MethodImpl(256)]
		public static Vector3Int operator /(Vector3Int a, int b)
		{
			return new Vector3Int(a.x / b, a.y / b, a.z / b);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0001D734 File Offset: 0x0001B934
		[MethodImpl(256)]
		public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0001D77C File Offset: 0x0001B97C
		[MethodImpl(256)]
		public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0001D798 File Offset: 0x0001B998
		[MethodImpl(256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector3Int);
			return !flag && this.Equals((Vector3Int)other);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		[MethodImpl(256)]
		public bool Equals(Vector3Int other)
		{
			return this == other;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0001D7EC File Offset: 0x0001B9EC
		public override int GetHashCode()
		{
			int hashCode = this.y.GetHashCode();
			int hashCode2 = this.z.GetHashCode();
			return this.x.GetHashCode() ^ (hashCode << 4) ^ (hashCode >> 28) ^ (hashCode2 >> 4) ^ (hashCode2 << 28);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0001D83C File Offset: 0x0001BA3C
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0001D860 File Offset: 0x0001BA60
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0001D884 File Offset: 0x0001BA84
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		public static Vector3Int zero
		{
			get
			{
				return Vector3Int.s_Zero;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0001D8F8 File Offset: 0x0001BAF8
		public static Vector3Int one
		{
			get
			{
				return Vector3Int.s_One;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0001D910 File Offset: 0x0001BB10
		public static Vector3Int up
		{
			get
			{
				return Vector3Int.s_Up;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0001D928 File Offset: 0x0001BB28
		public static Vector3Int down
		{
			get
			{
				return Vector3Int.s_Down;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0001D940 File Offset: 0x0001BB40
		public static Vector3Int left
		{
			get
			{
				return Vector3Int.s_Left;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0001D958 File Offset: 0x0001BB58
		public static Vector3Int right
		{
			get
			{
				return Vector3Int.s_Right;
			}
		}

		// Token: 0x04000603 RID: 1539
		private int m_X;

		// Token: 0x04000604 RID: 1540
		private int m_Y;

		// Token: 0x04000605 RID: 1541
		private int m_Z;

		// Token: 0x04000606 RID: 1542
		private static readonly Vector3Int s_Zero = new Vector3Int(0, 0, 0);

		// Token: 0x04000607 RID: 1543
		private static readonly Vector3Int s_One = new Vector3Int(1, 1, 1);

		// Token: 0x04000608 RID: 1544
		private static readonly Vector3Int s_Up = new Vector3Int(0, 1, 0);

		// Token: 0x04000609 RID: 1545
		private static readonly Vector3Int s_Down = new Vector3Int(0, -1, 0);

		// Token: 0x0400060A RID: 1546
		private static readonly Vector3Int s_Left = new Vector3Int(-1, 0, 0);

		// Token: 0x0400060B RID: 1547
		private static readonly Vector3Int s_Right = new Vector3Int(1, 0, 0);
	}
}
