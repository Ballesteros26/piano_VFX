using System;
using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	// Token: 0x0200000E RID: 14
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00009534 File Offset: 0x00007734
		public static Vector3 Zero
		{
			get
			{
				return default(Vector3);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000954A File Offset: 0x0000774A
		public static Vector3 One
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00009560 File Offset: 0x00007760
		public static Vector3 UnitX
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00009576 File Offset: 0x00007776
		public static Vector3 UnitY
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000958C File Offset: 0x0000778C
		public static Vector3 UnitZ
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000095A2 File Offset: 0x000077A2
		public override int GetHashCode()
		{
			return HashHelpers.Combine(HashHelpers.Combine(this.X.GetHashCode(), this.Y.GetHashCode()), this.Z.GetHashCode());
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000095CF File Offset: 0x000077CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return obj is Vector3 && this.Equals((Vector3)obj);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000095E7 File Offset: 0x000077E7
		public override string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000095F9 File Offset: 0x000077F9
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00009608 File Offset: 0x00007808
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(((IFormattable)this.X).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)this.Y).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)this.Z).ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000096AC File Offset: 0x000078AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return MathF.Sqrt(Vector3.Dot(this, this));
			}
			return MathF.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00009708 File Offset: 0x00007908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(this, this);
			}
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00009758 File Offset: 0x00007958
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 value1, Vector3 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 - value2;
				return MathF.Sqrt(Vector3.Dot(vector, vector));
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return MathF.Sqrt(num * num + num2 * num2 + num3 * num3);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000097B8 File Offset: 0x000079B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector3 value1, Vector3 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 - value2;
				return Vector3.Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return num * num + num2 * num2 + num3 * num3;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00009810 File Offset: 0x00007A10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = value.Length();
				return value / num;
			}
			float num2 = MathF.Sqrt(value.X * value.X + value.Y * value.Y + value.Z * value.Z);
			return new Vector3(value.X / num2, value.Y / num2, value.Z / num2);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00009880 File Offset: 0x00007A80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
		{
			return new Vector3(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000098E4 File Offset: 0x00007AE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Reflect(Vector3 vector, Vector3 normal)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = Vector3.Dot(vector, normal);
				Vector3 vector2 = normal * num * 2f;
				return vector - vector2;
			}
			float num2 = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
			float num3 = normal.X * num2 * 2f;
			float num4 = normal.Y * num2 * 2f;
			float num5 = normal.Z * num2 * 2f;
			return new Vector3(vector.X - num3, vector.Y - num4, vector.Z - num5);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00009994 File Offset: 0x00007B94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
		{
			float num = value1.X;
			num = ((num > max.X) ? max.X : num);
			num = ((num < min.X) ? min.X : num);
			float num2 = value1.Y;
			num2 = ((num2 > max.Y) ? max.Y : num2);
			num2 = ((num2 < min.Y) ? min.Y : num2);
			float num3 = value1.Z;
			num3 = ((num3 > max.Z) ? max.Z : num3);
			num3 = ((num3 < min.Z) ? min.Z : num3);
			return new Vector3(num, num2, num3);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00009A30 File Offset: 0x00007C30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 * (1f - amount);
				Vector3 vector2 = value2 * amount;
				return vector + vector2;
			}
			return new Vector3(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount, value1.Z + (value2.Z - value1.Z) * amount);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00009AA8 File Offset: 0x00007CA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector3(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00009B4C File Offset: 0x00007D4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
		{
			return new Vector3(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31, normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32, normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00009BDC File Offset: 0x00007DDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector3(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00009CF3 File Offset: 0x00007EF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Add(Vector3 left, Vector3 right)
		{
			return left + right;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009CFC File Offset: 0x00007EFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Subtract(Vector3 left, Vector3 right)
		{
			return left - right;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009D05 File Offset: 0x00007F05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, Vector3 right)
		{
			return left * right;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00009D0E File Offset: 0x00007F0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, float right)
		{
			return left * right;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009D17 File Offset: 0x00007F17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(float left, Vector3 right)
		{
			return left * right;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009D20 File Offset: 0x00007F20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, Vector3 right)
		{
			return left / right;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00009D29 File Offset: 0x00007F29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, float divisor)
		{
			return left / divisor;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00009D32 File Offset: 0x00007F32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Negate(Vector3 value)
		{
			return -value;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009D3A File Offset: 0x00007F3A
		[JitIntrinsic]
		public Vector3(float value)
		{
			this = new Vector3(value, value, value);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009D45 File Offset: 0x00007F45
		public Vector3(Vector2 value, float z)
		{
			this = new Vector3(value.X, value.Y, z);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009D5A File Offset: 0x00007F5A
		[JitIntrinsic]
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009D71 File Offset: 0x00007F71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009D7C File Offset: 0x00007F7C
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException("The method was called with a null array argument.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.Format("Index was out of bounds:", index));
			}
			if (array.Length - index < 3)
			{
				throw new ArgumentException(SR.Format("Number of elements in source vector is greater than the destination array", index));
			}
			array[index] = this.X;
			array[index + 1] = this.Y;
			array[index + 2] = this.Z;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00009DF9 File Offset: 0x00007FF9
		[JitIntrinsic]
		public bool Equals(Vector3 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00009E27 File Offset: 0x00008027
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector3 vector1, Vector3 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00009E54 File Offset: 0x00008054
		[JitIntrinsic]
		public static Vector3 Min(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00009EBC File Offset: 0x000080BC
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Max(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009F22 File Offset: 0x00008122
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Abs(Vector3 value)
		{
			return new Vector3(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00009F4A File Offset: 0x0000814A
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SquareRoot(Vector3 value)
		{
			return new Vector3(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00009F72 File Offset: 0x00008172
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00009FA0 File Offset: 0x000081A0
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009FCE File Offset: 0x000081CE
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009FFC File Offset: 0x000081FC
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 left, float right)
		{
			return left * new Vector3(right);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000A00A File Offset: 0x0000820A
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(float left, Vector3 right)
		{
			return new Vector3(left) * right;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000A018 File Offset: 0x00008218
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000A048 File Offset: 0x00008248
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 value1, float value2)
		{
			float num = 1f / value2;
			return new Vector3(value1.X * num, value1.Y * num, value1.Z * num);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000A07A File Offset: 0x0000827A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 value)
		{
			return Vector3.Zero - value;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009DF9 File Offset: 0x00007FF9
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3 left, Vector3 right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000A087 File Offset: 0x00008287
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3 left, Vector3 right)
		{
			return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
		}

		// Token: 0x04000063 RID: 99
		public float X;

		// Token: 0x04000064 RID: 100
		public float Y;

		// Token: 0x04000065 RID: 101
		public float Z;
	}
}
