using System;
using System.Globalization;

namespace System.Numerics
{
	// Token: 0x0200000C RID: 12
	public struct Quaternion : IEquatable<Quaternion>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00007BE2 File Offset: 0x00005DE2
		public static Quaternion Identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00007BFD File Offset: 0x00005DFD
		public bool IsIdentity
		{
			get
			{
				return this.X == 0f && this.Y == 0f && this.Z == 0f && this.W == 1f;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00007C35 File Offset: 0x00005E35
		public Quaternion(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00007C54 File Offset: 0x00005E54
		public Quaternion(Vector3 vectorPart, float scalarPart)
		{
			this.X = vectorPart.X;
			this.Y = vectorPart.Y;
			this.Z = vectorPart.Z;
			this.W = scalarPart;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00007C81 File Offset: 0x00005E81
		public float Length()
		{
			return MathF.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00007CBF File Offset: 0x00005EBF
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00007CF8 File Offset: 0x00005EF8
		public static Quaternion Normalize(Quaternion value)
		{
			float num = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
			float num2 = 1f / MathF.Sqrt(num);
			Quaternion quaternion;
			quaternion.X = value.X * num2;
			quaternion.Y = value.Y * num2;
			quaternion.Z = value.Z * num2;
			quaternion.W = value.W * num2;
			return quaternion;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00007D88 File Offset: 0x00005F88
		public static Quaternion Conjugate(Quaternion value)
		{
			Quaternion quaternion;
			quaternion.X = -value.X;
			quaternion.Y = -value.Y;
			quaternion.Z = -value.Z;
			quaternion.W = value.W;
			return quaternion;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00007DD0 File Offset: 0x00005FD0
		public static Quaternion Inverse(Quaternion value)
		{
			float num = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
			float num2 = 1f / num;
			Quaternion quaternion;
			quaternion.X = -value.X * num2;
			quaternion.Y = -value.Y * num2;
			quaternion.Z = -value.Z * num2;
			quaternion.W = value.W * num2;
			return quaternion;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00007E60 File Offset: 0x00006060
		public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
		{
			float num = angle * 0.5f;
			float num2 = MathF.Sin(num);
			float num3 = MathF.Cos(num);
			Quaternion quaternion;
			quaternion.X = axis.X * num2;
			quaternion.Y = axis.Y * num2;
			quaternion.Z = axis.Z * num2;
			quaternion.W = num3;
			return quaternion;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00007EB8 File Offset: 0x000060B8
		public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			float num = roll * 0.5f;
			float num2 = MathF.Sin(num);
			float num3 = MathF.Cos(num);
			float num4 = pitch * 0.5f;
			float num5 = MathF.Sin(num4);
			float num6 = MathF.Cos(num4);
			float num7 = yaw * 0.5f;
			float num8 = MathF.Sin(num7);
			float num9 = MathF.Cos(num7);
			Quaternion quaternion;
			quaternion.X = num9 * num5 * num3 + num8 * num6 * num2;
			quaternion.Y = num8 * num6 * num3 - num9 * num5 * num2;
			quaternion.Z = num9 * num6 * num2 - num8 * num5 * num3;
			quaternion.W = num9 * num6 * num3 + num8 * num5 * num2;
			return quaternion;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00007F58 File Offset: 0x00006158
		public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
		{
			float num = matrix.M11 + matrix.M22 + matrix.M33;
			Quaternion quaternion = default(Quaternion);
			if (num > 0f)
			{
				float num2 = MathF.Sqrt(num + 1f);
				quaternion.W = num2 * 0.5f;
				num2 = 0.5f / num2;
				quaternion.X = (matrix.M23 - matrix.M32) * num2;
				quaternion.Y = (matrix.M31 - matrix.M13) * num2;
				quaternion.Z = (matrix.M12 - matrix.M21) * num2;
			}
			else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
			{
				float num3 = MathF.Sqrt(1f + matrix.M11 - matrix.M22 - matrix.M33);
				float num4 = 0.5f / num3;
				quaternion.X = 0.5f * num3;
				quaternion.Y = (matrix.M12 + matrix.M21) * num4;
				quaternion.Z = (matrix.M13 + matrix.M31) * num4;
				quaternion.W = (matrix.M23 - matrix.M32) * num4;
			}
			else if (matrix.M22 > matrix.M33)
			{
				float num5 = MathF.Sqrt(1f + matrix.M22 - matrix.M11 - matrix.M33);
				float num6 = 0.5f / num5;
				quaternion.X = (matrix.M21 + matrix.M12) * num6;
				quaternion.Y = 0.5f * num5;
				quaternion.Z = (matrix.M32 + matrix.M23) * num6;
				quaternion.W = (matrix.M31 - matrix.M13) * num6;
			}
			else
			{
				float num7 = MathF.Sqrt(1f + matrix.M33 - matrix.M11 - matrix.M22);
				float num8 = 0.5f / num7;
				quaternion.X = (matrix.M31 + matrix.M13) * num8;
				quaternion.Y = (matrix.M32 + matrix.M23) * num8;
				quaternion.Z = 0.5f * num7;
				quaternion.W = (matrix.M12 - matrix.M21) * num8;
			}
			return quaternion;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000081A9 File Offset: 0x000063A9
		public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
		{
			return quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000081E4 File Offset: 0x000063E4
		public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			float num = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
			bool flag = false;
			if (num < 0f)
			{
				flag = true;
				num = -num;
			}
			float num2;
			float num3;
			if (num > 0.999999f)
			{
				num2 = 1f - amount;
				num3 = (flag ? (-amount) : amount);
			}
			else
			{
				float num4 = MathF.Acos(num);
				float num5 = 1f / MathF.Sin(num4);
				num2 = MathF.Sin((1f - amount) * num4) * num5;
				num3 = (flag ? (-MathF.Sin(amount * num4) * num5) : (MathF.Sin(amount * num4) * num5));
			}
			Quaternion quaternion3;
			quaternion3.X = num2 * quaternion1.X + num3 * quaternion2.X;
			quaternion3.Y = num2 * quaternion1.Y + num3 * quaternion2.Y;
			quaternion3.Z = num2 * quaternion1.Z + num3 * quaternion2.Z;
			quaternion3.W = num2 * quaternion1.W + num3 * quaternion2.W;
			return quaternion3;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00008308 File Offset: 0x00006508
		public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			float num = 1f - amount;
			Quaternion quaternion3 = default(Quaternion);
			if (quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W >= 0f)
			{
				quaternion3.X = num * quaternion1.X + amount * quaternion2.X;
				quaternion3.Y = num * quaternion1.Y + amount * quaternion2.Y;
				quaternion3.Z = num * quaternion1.Z + amount * quaternion2.Z;
				quaternion3.W = num * quaternion1.W + amount * quaternion2.W;
			}
			else
			{
				quaternion3.X = num * quaternion1.X - amount * quaternion2.X;
				quaternion3.Y = num * quaternion1.Y - amount * quaternion2.Y;
				quaternion3.Z = num * quaternion1.Z - amount * quaternion2.Z;
				quaternion3.W = num * quaternion1.W - amount * quaternion2.W;
			}
			float num2 = quaternion3.X * quaternion3.X + quaternion3.Y * quaternion3.Y + quaternion3.Z * quaternion3.Z + quaternion3.W * quaternion3.W;
			float num3 = 1f / MathF.Sqrt(num2);
			quaternion3.X *= num3;
			quaternion3.Y *= num3;
			quaternion3.Z *= num3;
			quaternion3.W *= num3;
			return quaternion3;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000084A4 File Offset: 0x000066A4
		public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
		{
			float x = value2.X;
			float y = value2.Y;
			float z = value2.Z;
			float w = value2.W;
			float x2 = value1.X;
			float y2 = value1.Y;
			float z2 = value1.Z;
			float w2 = value1.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion quaternion;
			quaternion.X = x * w2 + x2 * w + num;
			quaternion.Y = y * w2 + y2 * w + num2;
			quaternion.Z = z * w2 + z2 * w + num3;
			quaternion.W = w * w2 - num4;
			return quaternion;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000856C File Offset: 0x0000676C
		public static Quaternion Negate(Quaternion value)
		{
			Quaternion quaternion;
			quaternion.X = -value.X;
			quaternion.Y = -value.Y;
			quaternion.Z = -value.Z;
			quaternion.W = -value.W;
			return quaternion;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000085B4 File Offset: 0x000067B4
		public static Quaternion Add(Quaternion value1, Quaternion value2)
		{
			Quaternion quaternion;
			quaternion.X = value1.X + value2.X;
			quaternion.Y = value1.Y + value2.Y;
			quaternion.Z = value1.Z + value2.Z;
			quaternion.W = value1.W + value2.W;
			return quaternion;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00008614 File Offset: 0x00006814
		public static Quaternion Subtract(Quaternion value1, Quaternion value2)
		{
			Quaternion quaternion;
			quaternion.X = value1.X - value2.X;
			quaternion.Y = value1.Y - value2.Y;
			quaternion.Z = value1.Z - value2.Z;
			quaternion.W = value1.W - value2.W;
			return quaternion;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00008674 File Offset: 0x00006874
		public static Quaternion Multiply(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion quaternion;
			quaternion.X = x * w2 + x2 * w + num;
			quaternion.Y = y * w2 + y2 * w + num2;
			quaternion.Z = z * w2 + z2 * w + num3;
			quaternion.W = w * w2 - num4;
			return quaternion;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000873C File Offset: 0x0000693C
		public static Quaternion Multiply(Quaternion value1, float value2)
		{
			Quaternion quaternion;
			quaternion.X = value1.X * value2;
			quaternion.Y = value1.Y * value2;
			quaternion.Z = value1.Z * value2;
			quaternion.W = value1.W * value2;
			return quaternion;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00008788 File Offset: 0x00006988
		public static Quaternion Divide(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float num = value2.X * value2.X + value2.Y * value2.Y + value2.Z * value2.Z + value2.W * value2.W;
			float num2 = 1f / num;
			float num3 = -value2.X * num2;
			float num4 = -value2.Y * num2;
			float num5 = -value2.Z * num2;
			float num6 = value2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			Quaternion quaternion;
			quaternion.X = x * num6 + num3 * w + num7;
			quaternion.Y = y * num6 + num4 * w + num8;
			quaternion.Z = z * num6 + num5 * w + num9;
			quaternion.W = w * num6 - num10;
			return quaternion;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000088A4 File Offset: 0x00006AA4
		public static Quaternion operator -(Quaternion value)
		{
			Quaternion quaternion;
			quaternion.X = -value.X;
			quaternion.Y = -value.Y;
			quaternion.Z = -value.Z;
			quaternion.W = -value.W;
			return quaternion;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000088EC File Offset: 0x00006AEC
		public static Quaternion operator +(Quaternion value1, Quaternion value2)
		{
			Quaternion quaternion;
			quaternion.X = value1.X + value2.X;
			quaternion.Y = value1.Y + value2.Y;
			quaternion.Z = value1.Z + value2.Z;
			quaternion.W = value1.W + value2.W;
			return quaternion;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000894C File Offset: 0x00006B4C
		public static Quaternion operator -(Quaternion value1, Quaternion value2)
		{
			Quaternion quaternion;
			quaternion.X = value1.X - value2.X;
			quaternion.Y = value1.Y - value2.Y;
			quaternion.Z = value1.Z - value2.Z;
			quaternion.W = value1.W - value2.W;
			return quaternion;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000089AC File Offset: 0x00006BAC
		public static Quaternion operator *(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion quaternion;
			quaternion.X = x * w2 + x2 * w + num;
			quaternion.Y = y * w2 + y2 * w + num2;
			quaternion.Z = z * w2 + z2 * w + num3;
			quaternion.W = w * w2 - num4;
			return quaternion;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00008A74 File Offset: 0x00006C74
		public static Quaternion operator *(Quaternion value1, float value2)
		{
			Quaternion quaternion;
			quaternion.X = value1.X * value2;
			quaternion.Y = value1.Y * value2;
			quaternion.Z = value1.Z * value2;
			quaternion.W = value1.W * value2;
			return quaternion;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public static Quaternion operator /(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float num = value2.X * value2.X + value2.Y * value2.Y + value2.Z * value2.Z + value2.W * value2.W;
			float num2 = 1f / num;
			float num3 = -value2.X * num2;
			float num4 = -value2.Y * num2;
			float num5 = -value2.Z * num2;
			float num6 = value2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			Quaternion quaternion;
			quaternion.X = x * num6 + num3 * w + num7;
			quaternion.Y = y * num6 + num4 * w + num8;
			quaternion.Z = z * num6 + num5 * w + num9;
			quaternion.W = w * num6 - num10;
			return quaternion;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00008BD9 File Offset: 0x00006DD9
		public static bool operator ==(Quaternion value1, Quaternion value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.W == value2.W;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00008C15 File Offset: 0x00006E15
		public static bool operator !=(Quaternion value1, Quaternion value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y || value1.Z != value2.Z || value1.W != value2.W;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00008BD9 File Offset: 0x00006DD9
		public bool Equals(Quaternion other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00008C54 File Offset: 0x00006E54
		public override bool Equals(object obj)
		{
			return obj is Quaternion && this.Equals((Quaternion)obj);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00008C6C File Offset: 0x00006E6C
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", new object[]
			{
				this.X.ToString(currentCulture),
				this.Y.ToString(currentCulture),
				this.Z.ToString(currentCulture),
				this.W.ToString(currentCulture)
			});
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008CCC File Offset: 0x00006ECC
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
		}

		// Token: 0x0400005D RID: 93
		public float X;

		// Token: 0x0400005E RID: 94
		public float Y;

		// Token: 0x0400005F RID: 95
		public float Z;

		// Token: 0x04000060 RID: 96
		public float W;
	}
}
