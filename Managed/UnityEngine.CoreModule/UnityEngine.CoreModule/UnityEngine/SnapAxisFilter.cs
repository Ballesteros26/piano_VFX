using System;

namespace UnityEngine
{
	// Token: 0x020001DB RID: 475
	internal struct SnapAxisFilter : IEquatable<SnapAxisFilter>
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00022340 File Offset: 0x00020540
		public float x
		{
			get
			{
				return ((this.m_Mask & SnapAxis.X) == SnapAxis.X) ? 1f : 0f;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0002236C File Offset: 0x0002056C
		public float y
		{
			get
			{
				return ((this.m_Mask & SnapAxis.Y) == SnapAxis.Y) ? 1f : 0f;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00022398 File Offset: 0x00020598
		public float z
		{
			get
			{
				return ((this.m_Mask & SnapAxis.Z) == SnapAxis.Z) ? 1f : 0f;
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000223C4 File Offset: 0x000205C4
		public SnapAxisFilter(Vector3 v)
		{
			this.m_Mask = SnapAxis.None;
			float num = 1E-06f;
			bool flag = Mathf.Abs(v.x) > num;
			if (flag)
			{
				this.m_Mask |= SnapAxis.X;
			}
			bool flag2 = Mathf.Abs(v.y) > num;
			if (flag2)
			{
				this.m_Mask |= SnapAxis.Y;
			}
			bool flag3 = Mathf.Abs(v.z) > num;
			if (flag3)
			{
				this.m_Mask |= SnapAxis.Z;
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00022440 File Offset: 0x00020640
		public SnapAxisFilter(SnapAxis axis)
		{
			this.m_Mask = SnapAxis.None;
			bool flag = (axis & SnapAxis.X) == SnapAxis.X;
			if (flag)
			{
				this.m_Mask |= SnapAxis.X;
			}
			bool flag2 = (axis & SnapAxis.Y) == SnapAxis.Y;
			if (flag2)
			{
				this.m_Mask |= SnapAxis.Y;
			}
			bool flag3 = (axis & SnapAxis.Z) == SnapAxis.Z;
			if (flag3)
			{
				this.m_Mask |= SnapAxis.Z;
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000224A0 File Offset: 0x000206A0
		public override string ToString()
		{
			return string.Format("{{{0}, {1}, {2}}}", this.x, this.y, this.z);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x000224E0 File Offset: 0x000206E0
		public int active
		{
			get
			{
				int num = 0;
				bool flag = (this.m_Mask & SnapAxis.X) > SnapAxis.None;
				if (flag)
				{
					num++;
				}
				bool flag2 = (this.m_Mask & SnapAxis.Y) > SnapAxis.None;
				if (flag2)
				{
					num++;
				}
				bool flag3 = (this.m_Mask & SnapAxis.Z) > SnapAxis.None;
				if (flag3)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00022530 File Offset: 0x00020730
		public static implicit operator Vector3(SnapAxisFilter mask)
		{
			return new Vector3(mask.x, mask.y, mask.z);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0002255C File Offset: 0x0002075C
		public static explicit operator SnapAxisFilter(Vector3 v)
		{
			return new SnapAxisFilter(v);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00022574 File Offset: 0x00020774
		public static explicit operator SnapAxis(SnapAxisFilter mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0002258C File Offset: 0x0002078C
		public static SnapAxisFilter operator |(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask | right.m_Mask);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x000225B0 File Offset: 0x000207B0
		public static SnapAxisFilter operator &(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask & right.m_Mask);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000225D4 File Offset: 0x000207D4
		public static SnapAxisFilter operator ^(SnapAxisFilter left, SnapAxisFilter right)
		{
			return new SnapAxisFilter(left.m_Mask ^ right.m_Mask);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000225F8 File Offset: 0x000207F8
		public static SnapAxisFilter operator ~(SnapAxisFilter left)
		{
			return new SnapAxisFilter(~left.m_Mask);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00022618 File Offset: 0x00020818
		public static Vector3 operator *(SnapAxisFilter mask, float value)
		{
			return new Vector3(mask.x * value, mask.y * value, mask.z * value);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0002264C File Offset: 0x0002084C
		public static Vector3 operator *(SnapAxisFilter mask, Vector3 right)
		{
			return new Vector3(mask.x * right.x, mask.y * right.y, mask.z * right.z);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00022690 File Offset: 0x00020890
		public static Vector3 operator *(Quaternion rotation, SnapAxisFilter mask)
		{
			int active = mask.active;
			bool flag = active > 2;
			Vector3 vector;
			if (flag)
			{
				vector = mask;
			}
			else
			{
				Vector3 vector2 = rotation * mask;
				vector2 = new Vector3(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y), Mathf.Abs(vector2.z));
				bool flag2 = active > 1;
				if (flag2)
				{
					vector = new Vector3((float)((vector2.x > vector2.y || vector2.x > vector2.z) ? 1 : 0), (float)((vector2.y > vector2.x || vector2.y > vector2.z) ? 1 : 0), (float)((vector2.z > vector2.x || vector2.z > vector2.y) ? 1 : 0));
				}
				else
				{
					vector = new Vector3((float)((vector2.x > vector2.y && vector2.x > vector2.z) ? 1 : 0), (float)((vector2.y > vector2.z && vector2.y > vector2.x) ? 1 : 0), (float)((vector2.z > vector2.x && vector2.z > vector2.y) ? 1 : 0));
				}
			}
			return vector;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x000227D4 File Offset: 0x000209D4
		public static bool operator ==(SnapAxisFilter left, SnapAxisFilter right)
		{
			return left.m_Mask == right.m_Mask;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000227F4 File Offset: 0x000209F4
		public static bool operator !=(SnapAxisFilter left, SnapAxisFilter right)
		{
			return !(left == right);
		}

		// Token: 0x170003EB RID: 1003
		public float this[int i]
		{
			get
			{
				bool flag = i < 0 || i > 2;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return (float)(SnapAxis.X & (this.m_Mask >> (i & 31))) * 1f;
			}
			set
			{
				bool flag = i < 0 || i > 2;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				this.m_Mask &= (SnapAxis)(~(SnapAxis)(1 << i));
				this.m_Mask |= (SnapAxis)(((value > 0f) ? 1 : 0) << (i & 31));
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000228A4 File Offset: 0x00020AA4
		public bool Equals(SnapAxisFilter other)
		{
			return this.m_Mask == other.m_Mask;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000228C4 File Offset: 0x00020AC4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SnapAxisFilter && this.Equals((SnapAxisFilter)obj);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000228FC File Offset: 0x00020AFC
		public override int GetHashCode()
		{
			return this.m_Mask.GetHashCode();
		}

		// Token: 0x04000695 RID: 1685
		private const SnapAxis X = SnapAxis.X;

		// Token: 0x04000696 RID: 1686
		private const SnapAxis Y = SnapAxis.Y;

		// Token: 0x04000697 RID: 1687
		private const SnapAxis Z = SnapAxis.Z;

		// Token: 0x04000698 RID: 1688
		public static readonly SnapAxisFilter all = new SnapAxisFilter(SnapAxis.All);

		// Token: 0x04000699 RID: 1689
		private SnapAxis m_Mask;
	}
}
