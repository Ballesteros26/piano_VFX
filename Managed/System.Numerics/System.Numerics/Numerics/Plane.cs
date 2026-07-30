using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x0200000B RID: 11
	public struct Plane : IEquatable<Plane>
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000746B File Offset: 0x0000566B
		public Plane(float x, float y, float z, float d)
		{
			this.Normal = new Vector3(x, y, z);
			this.D = d;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00007483 File Offset: 0x00005683
		public Plane(Vector3 normal, float d)
		{
			this.Normal = normal;
			this.D = d;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00007493 File Offset: 0x00005693
		public Plane(Vector4 value)
		{
			this.Normal = new Vector3(value.X, value.Y, value.Z);
			this.D = value.W;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000074C0 File Offset: 0x000056C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane CreateFromVertices(Vector3 point1, Vector3 point2, Vector3 point3)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = point2 - point1;
				Vector3 vector2 = point3 - point1;
				Vector3 vector3 = Vector3.Normalize(Vector3.Cross(vector, vector2));
				float num = -Vector3.Dot(vector3, point1);
				return new Plane(vector3, num);
			}
			float num2 = point2.X - point1.X;
			float num3 = point2.Y - point1.Y;
			float num4 = point2.Z - point1.Z;
			float num5 = point3.X - point1.X;
			float num6 = point3.Y - point1.Y;
			float num7 = point3.Z - point1.Z;
			float num8 = num3 * num7 - num4 * num6;
			float num9 = num4 * num5 - num2 * num7;
			float num10 = num2 * num6 - num3 * num5;
			float num11 = num8 * num8 + num9 * num9 + num10 * num10;
			float num12 = 1f / MathF.Sqrt(num11);
			Vector3 vector4 = new Vector3(num8 * num12, num9 * num12, num10 * num12);
			return new Plane(vector4, -(vector4.X * point1.X + vector4.Y * point1.Y + vector4.Z * point1.Z));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000075E8 File Offset: 0x000057E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Normalize(Plane value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float num = value.Normal.LengthSquared();
				if (MathF.Abs(num - 1f) < 1.1920929E-07f)
				{
					return value;
				}
				float num2 = MathF.Sqrt(num);
				return new Plane(value.Normal / num2, value.D / num2);
			}
			else
			{
				float num3 = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;
				if (MathF.Abs(num3 - 1f) < 1.1920929E-07f)
				{
					return value;
				}
				float num4 = 1f / MathF.Sqrt(num3);
				return new Plane(value.Normal.X * num4, value.Normal.Y * num4, value.Normal.Z * num4, value.D * num4);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000076E0 File Offset: 0x000058E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Transform(Plane plane, Matrix4x4 matrix)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Invert(matrix, out matrix4x);
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float d = plane.D;
			return new Plane(x * matrix4x.M11 + y * matrix4x.M12 + z * matrix4x.M13 + d * matrix4x.M14, x * matrix4x.M21 + y * matrix4x.M22 + z * matrix4x.M23 + d * matrix4x.M24, x * matrix4x.M31 + y * matrix4x.M32 + z * matrix4x.M33 + d * matrix4x.M34, x * matrix4x.M41 + y * matrix4x.M42 + z * matrix4x.M43 + d * matrix4x.M44);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000077B8 File Offset: 0x000059B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Transform(Plane plane, Quaternion rotation)
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
			float num13 = 1f - num10 - num12;
			float num14 = num8 - num6;
			float num15 = num9 + num5;
			float num16 = num8 + num6;
			float num17 = 1f - num7 - num12;
			float num18 = num11 - num4;
			float num19 = num9 - num5;
			float num20 = num11 + num4;
			float num21 = 1f - num7 - num10;
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			return new Plane(x * num13 + y * num14 + z * num15, x * num16 + y * num17 + z * num18, x * num19 + y * num20 + z * num21, plane.D);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000078F8 File Offset: 0x00005AF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Plane plane, Vector4 value)
		{
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D * value.W;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000794C File Offset: 0x00005B4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DotCoordinate(Plane plane, Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(plane.Normal, value) + plane.D;
			}
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z + plane.D;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000079B4 File Offset: 0x00005BB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DotNormal(Plane plane, Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Vector3.Dot(plane.Normal, value);
			}
			return plane.Normal.X * value.X + plane.Normal.Y * value.Y + plane.Normal.Z * value.Z;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00007A10 File Offset: 0x00005C10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Plane value1, Plane value2)
		{
			return value1.Normal.X == value2.Normal.X && value1.Normal.Y == value2.Normal.Y && value1.Normal.Z == value2.Normal.Z && value1.D == value2.D;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00007A78 File Offset: 0x00005C78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Plane value1, Plane value2)
		{
			return value1.Normal.X != value2.Normal.X || value1.Normal.Y != value2.Normal.Y || value1.Normal.Z != value2.Normal.Z || value1.D != value2.D;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00007AE0 File Offset: 0x00005CE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Plane other)
		{
			if (Vector.IsHardwareAccelerated)
			{
				return this.Normal.Equals(other.Normal) && this.D == other.D;
			}
			return this.Normal.X == other.Normal.X && this.Normal.Y == other.Normal.Y && this.Normal.Z == other.Normal.Z && this.D == other.D;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00007B70 File Offset: 0x00005D70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return obj is Plane && this.Equals((Plane)obj);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00007B88 File Offset: 0x00005D88
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{Normal:{0} D:{1}}}", this.Normal.ToString(), this.D.ToString(currentCulture));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00007BC3 File Offset: 0x00005DC3
		public override int GetHashCode()
		{
			return this.Normal.GetHashCode() + this.D.GetHashCode();
		}

		// Token: 0x0400005B RID: 91
		public Vector3 Normal;

		// Token: 0x0400005C RID: 92
		public float D;
	}
}
