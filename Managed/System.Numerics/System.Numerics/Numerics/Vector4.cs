using System;
using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	// Token: 0x0200000F RID: 15
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public static Vector4 Zero
		{
			get
			{
				return default(Vector4);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000A0CE File Offset: 0x000082CE
		public static Vector4 One
		{
			get
			{
				return new Vector4(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000A0E9 File Offset: 0x000082E9
		public static Vector4 UnitX
		{
			get
			{
				return new Vector4(1f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000A104 File Offset: 0x00008304
		public static Vector4 UnitY
		{
			get
			{
				return new Vector4(0f, 1f, 0f, 0f);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000A11F File Offset: 0x0000831F
		public static Vector4 UnitZ
		{
			get
			{
				return new Vector4(0f, 0f, 1f, 0f);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000A13A File Offset: 0x0000833A
		public static Vector4 UnitW
		{
			get
			{
				return new Vector4(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000A155 File Offset: 0x00008355
		public override int GetHashCode()
		{
			return HashHelpers.Combine(HashHelpers.Combine(HashHelpers.Combine(this.X.GetHashCode(), this.Y.GetHashCode()), this.Z.GetHashCode()), this.W.GetHashCode());
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000A192 File Offset: 0x00008392
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return obj is Vector4 && this.Equals((Vector4)obj);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000A1AA File Offset: 0x000083AA
		public override string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000A1BC File Offset: 0x000083BC
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000A1CC File Offset: 0x000083CC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(this.X.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.Y.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.Z.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(this.W.ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000A284 File Offset: 0x00008484
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return MathF.Sqrt(Vector4.Dot(this, this));
			}
			return MathF.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000A2EC File Offset: 0x000084EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector4.Dot(this, this);
			}
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000A34C File Offset: 0x0000854C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 value1, Vector4 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector4 vector = value1 - value2;
				return MathF.Sqrt(Vector4.Dot(vector, vector));
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			return MathF.Sqrt(num * num + num2 * num2 + num3 * num3 + num4 * num4);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000A3C0 File Offset: 0x000085C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector4 value1, Vector4 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector4 vector = value1 - value2;
				return Vector4.Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			return num * num + num2 * num2 + num3 * num3 + num4 * num4;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000A428 File Offset: 0x00008628
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 vector)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = vector.Length();
				return vector / num;
			}
			float num2 = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
			float num3 = 1f / MathF.Sqrt(num2);
			return new Vector4(vector.X * num3, vector.Y * num3, vector.Z * num3, vector.W * num3);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000A4B8 File Offset: 0x000086B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
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
			float num4 = value1.W;
			num4 = ((num4 > max.W) ? max.W : num4);
			num4 = ((num4 < min.W) ? min.W : num4);
			return new Vector4(num, num2, num3, num4);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000A584 File Offset: 0x00008784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount, value1.Z + (value2.Z - value1.Z) * amount, value1.W + (value2.W - value1.W) * amount);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A5F0 File Offset: 0x000087F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000A68C File Offset: 0x0000888C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000A760 File Offset: 0x00008960
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 vector, Matrix4x4 matrix)
		{
			return new Vector4(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000A850 File Offset: 0x00008A50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 value, Quaternion rotation)
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
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A940 File Offset: 0x00008B40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 value, Quaternion rotation)
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
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000AA5C File Offset: 0x00008C5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 value, Quaternion rotation)
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
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000AB79 File Offset: 0x00008D79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Add(Vector4 left, Vector4 right)
		{
			return left + right;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000AB82 File Offset: 0x00008D82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Subtract(Vector4 left, Vector4 right)
		{
			return left - right;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000AB8B File Offset: 0x00008D8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, Vector4 right)
		{
			return left * right;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000AB94 File Offset: 0x00008D94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, float right)
		{
			return left * new Vector4(right, right, right, right);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000ABA5 File Offset: 0x00008DA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(float left, Vector4 right)
		{
			return new Vector4(left, left, left, left) * right;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000ABB6 File Offset: 0x00008DB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, Vector4 right)
		{
			return left / right;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000ABBF File Offset: 0x00008DBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, float divisor)
		{
			return left / divisor;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Negate(Vector4 value)
		{
			return -value;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		[JitIntrinsic]
		public Vector4(float value)
		{
			this = new Vector4(value, value, value, value);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000ABDC File Offset: 0x00008DDC
		[JitIntrinsic]
		public Vector4(float x, float y, float z, float w)
		{
			this.W = w;
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000ABFB File Offset: 0x00008DFB
		public Vector4(Vector2 value, float z, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000AC23 File Offset: 0x00008E23
		public Vector4(Vector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000AC50 File Offset: 0x00008E50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000AC5C File Offset: 0x00008E5C
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
			if (array.Length - index < 4)
			{
				throw new ArgumentException(SR.Format("Number of elements in source vector is greater than the destination array", index));
			}
			array[index] = this.X;
			array[index + 1] = this.Y;
			array[index + 2] = this.Z;
			array[index + 3] = this.W;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		[JitIntrinsic]
		public bool Equals(Vector4 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000AD20 File Offset: 0x00008F20
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector4 vector1, Vector4 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000AD5C File Offset: 0x00008F5C
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z, (value1.W < value2.W) ? value1.W : value2.W);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z, (value1.W > value2.W) ? value1.W : value2.W);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000AE62 File Offset: 0x00009062
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Abs(Vector4 value)
		{
			return new Vector4(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z), MathF.Abs(value.W));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000AE95 File Offset: 0x00009095
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 SquareRoot(Vector4 value)
		{
			return new Vector4(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z), MathF.Sqrt(value.W));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000AEC8 File Offset: 0x000090C8
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator +(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000AF03 File Offset: 0x00009103
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000AF3E File Offset: 0x0000913E
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000AF79 File Offset: 0x00009179
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 left, float right)
		{
			return left * new Vector4(right);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000AF87 File Offset: 0x00009187
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(float left, Vector4 right)
		{
			return new Vector4(left) * right;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000AF95 File Offset: 0x00009195
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000AFD0 File Offset: 0x000091D0
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 value1, float value2)
		{
			float num = 1f / value2;
			return new Vector4(value1.X * num, value1.Y * num, value1.Z * num, value1.W * num);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000B00A File Offset: 0x0000920A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 value)
		{
			return Vector4.Zero - value;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000B017 File Offset: 0x00009217
		[JitIntrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector4 left, Vector4 right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000B021 File Offset: 0x00009221
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4 left, Vector4 right)
		{
			return !(left == right);
		}

		// Token: 0x04000066 RID: 102
		public float X;

		// Token: 0x04000067 RID: 103
		public float Y;

		// Token: 0x04000068 RID: 104
		public float Z;

		// Token: 0x04000069 RID: 105
		public float W;
	}
}
