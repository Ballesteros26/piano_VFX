using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000176 RID: 374
	[NativeHeader("Runtime/Math/Vector4.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("Vector4f")]
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		// Token: 0x1700039C RID: 924
		public float this[int index]
		{
			get
			{
				float num;
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
				case 3:
					num = this.w;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
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
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0001DA7B File Offset: 0x0001BC7B
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0001DA9B File Offset: 0x0001BC9B
		public Vector4(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0f;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0001DABE File Offset: 0x0001BCBE
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0001DA7B File Offset: 0x0001BC7B
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0001DAE8 File Offset: 0x0001BCE8
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0001DB60 File Offset: 0x0001BD60
		public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
		{
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0001DBD0 File Offset: 0x0001BDD0
		[MethodImpl(256)]
		public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = target.z - current.z;
			float num4 = target.w - current.w;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			bool flag = num5 == 0f || (maxDistanceDelta >= 0f && num5 <= maxDistanceDelta * maxDistanceDelta);
			Vector4 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num6 = (float)Math.Sqrt((double)num5);
				vector = new Vector4(current.x + num / num6 * maxDistanceDelta, current.y + num2 / num6 * maxDistanceDelta, current.z + num3 / num6 * maxDistanceDelta, current.w + num4 / num6 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
		public static Vector4 Scale(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		public void Scale(Vector4 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
			this.w *= scale.w;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0001DD48 File Offset: 0x0001BF48
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0001DD90 File Offset: 0x0001BF90
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector4);
			return !flag && this.Equals((Vector4)other);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		[MethodImpl(256)]
		public bool Equals(Vector4 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z && this.w == other.w;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0001DE14 File Offset: 0x0001C014
		public static Vector4 Normalize(Vector4 a)
		{
			float num = Vector4.Magnitude(a);
			bool flag = num > 1E-05f;
			Vector4 vector;
			if (flag)
			{
				vector = a / num;
			}
			else
			{
				vector = Vector4.zero;
			}
			return vector;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0001DE48 File Offset: 0x0001C048
		public void Normalize()
		{
			float num = Vector4.Magnitude(this);
			bool flag = num > 1E-05f;
			if (flag)
			{
				this /= num;
			}
			else
			{
				this = Vector4.zero;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0001DE90 File Offset: 0x0001C090
		public Vector4 normalized
		{
			get
			{
				return Vector4.Normalize(this);
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0001DEFC File Offset: 0x0001C0FC
		[MethodImpl(256)]
		public static Vector4 Project(Vector4 a, Vector4 b)
		{
			return b * (Vector4.Dot(a, b) / Vector4.Dot(b, b));
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0001DF24 File Offset: 0x0001C124
		[MethodImpl(256)]
		public static float Distance(Vector4 a, Vector4 b)
		{
			return Vector4.Magnitude(a - b);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0001DF44 File Offset: 0x0001C144
		public static float Magnitude(Vector4 a)
		{
			return (float)Math.Sqrt((double)Vector4.Dot(a, a));
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0001DF64 File Offset: 0x0001C164
		public float magnitude
		{
			get
			{
				return (float)Math.Sqrt((double)Vector4.Dot(this, this));
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0001DF90 File Offset: 0x0001C190
		public float sqrMagnitude
		{
			get
			{
				return Vector4.Dot(this, this);
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		[MethodImpl(256)]
		public static Vector4 Min(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0001E010 File Offset: 0x0001C210
		[MethodImpl(256)]
		public static Vector4 Max(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0001E06C File Offset: 0x0001C26C
		public static Vector4 zero
		{
			get
			{
				return Vector4.zeroVector;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0001E084 File Offset: 0x0001C284
		public static Vector4 one
		{
			get
			{
				return Vector4.oneVector;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0001E09C File Offset: 0x0001C29C
		public static Vector4 positiveInfinity
		{
			get
			{
				return Vector4.positiveInfinityVector;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0001E0B4 File Offset: 0x0001C2B4
		public static Vector4 negativeInfinity
		{
			get
			{
				return Vector4.negativeInfinityVector;
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0001E0CC File Offset: 0x0001C2CC
		[MethodImpl(256)]
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0001E118 File Offset: 0x0001C318
		[MethodImpl(256)]
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0001E164 File Offset: 0x0001C364
		[MethodImpl(256)]
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0001E198 File Offset: 0x0001C398
		[MethodImpl(256)]
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0001E1D0 File Offset: 0x0001C3D0
		[MethodImpl(256)]
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0001E208 File Offset: 0x0001C408
		[MethodImpl(256)]
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0001E240 File Offset: 0x0001C440
		[MethodImpl(256)]
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			float num3 = lhs.z - rhs.z;
			float num4 = lhs.w - rhs.w;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			return num5 < 9.9999994E-11f;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		[MethodImpl(256)]
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0001E2C4 File Offset: 0x0001C4C4
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0001E2F4 File Offset: 0x0001C4F4
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0001E320 File Offset: 0x0001C520
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0001E350 File Offset: 0x0001C550
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0001E374 File Offset: 0x0001C574
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0001E398 File Offset: 0x0001C598
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0001E3BC File Offset: 0x0001C5BC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F1";
			}
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0001E430 File Offset: 0x0001C630
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0001E44C File Offset: 0x0001C64C
		public float SqrMagnitude()
		{
			return Vector4.Dot(this, this);
		}

		// Token: 0x0400060C RID: 1548
		public const float kEpsilon = 1E-05f;

		// Token: 0x0400060D RID: 1549
		public float x;

		// Token: 0x0400060E RID: 1550
		public float y;

		// Token: 0x0400060F RID: 1551
		public float z;

		// Token: 0x04000610 RID: 1552
		public float w;

		// Token: 0x04000611 RID: 1553
		private static readonly Vector4 zeroVector = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000612 RID: 1554
		private static readonly Vector4 oneVector = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04000613 RID: 1555
		private static readonly Vector4 positiveInfinityVector = new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x04000614 RID: 1556
		private static readonly Vector4 negativeInfinityVector = new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
