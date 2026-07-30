using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200016E RID: 366
	[NativeType(Header = "Runtime/Math/Matrix4x4.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("Matrix4x4f")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	public struct Matrix4x4 : IEquatable<Matrix4x4>, IFormattable
	{
		// Token: 0x06001082 RID: 4226 RVA: 0x00017F04 File Offset: 0x00016104
		[ThreadSafe]
		private Quaternion GetRotation()
		{
			Quaternion quaternion;
			Matrix4x4.GetRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00017F1C File Offset: 0x0001611C
		[ThreadSafe]
		private Vector3 GetLossyScale()
		{
			Vector3 vector;
			Matrix4x4.GetLossyScale_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00017F32 File Offset: 0x00016132
		[ThreadSafe]
		private bool IsIdentity()
		{
			return Matrix4x4.IsIdentity_Injected(ref this);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00017F3A File Offset: 0x0001613A
		[ThreadSafe]
		private float GetDeterminant()
		{
			return Matrix4x4.GetDeterminant_Injected(ref this);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00017F44 File Offset: 0x00016144
		[ThreadSafe]
		private FrustumPlanes DecomposeProjection()
		{
			FrustumPlanes frustumPlanes;
			Matrix4x4.DecomposeProjection_Injected(ref this, out frustumPlanes);
			return frustumPlanes;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00017F5C File Offset: 0x0001615C
		public Quaternion rotation
		{
			get
			{
				return this.GetRotation();
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x00017F74 File Offset: 0x00016174
		public Vector3 lossyScale
		{
			get
			{
				return this.GetLossyScale();
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x00017F8C File Offset: 0x0001618C
		public bool isIdentity
		{
			get
			{
				return this.IsIdentity();
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00017FA4 File Offset: 0x000161A4
		public float determinant
		{
			get
			{
				return this.GetDeterminant();
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00017FBC File Offset: 0x000161BC
		public FrustumPlanes decomposeProjection
		{
			get
			{
				return this.DecomposeProjection();
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00017FD4 File Offset: 0x000161D4
		[ThreadSafe]
		public bool ValidTRS()
		{
			return Matrix4x4.ValidTRS_Injected(ref this);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00017FDC File Offset: 0x000161DC
		public static float Determinant(Matrix4x4 m)
		{
			return m.determinant;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00017FF8 File Offset: 0x000161F8
		[FreeFunction("MatrixScripting::TRS", IsThreadSafe = true)]
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.TRS_Injected(ref pos, ref q, ref s, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00018013 File Offset: 0x00016213
		public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			this = Matrix4x4.TRS(pos, q, s);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00018024 File Offset: 0x00016224
		[FreeFunction("MatrixScripting::Inverse3DAffine", IsThreadSafe = true)]
		public static bool Inverse3DAffine(Matrix4x4 input, ref Matrix4x4 result)
		{
			return Matrix4x4.Inverse3DAffine_Injected(ref input, ref result);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00018030 File Offset: 0x00016230
		[FreeFunction("MatrixScripting::Inverse", IsThreadSafe = true)]
		public static Matrix4x4 Inverse(Matrix4x4 m)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Inverse_Injected(ref m, out matrix4x);
			return matrix4x;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00018048 File Offset: 0x00016248
		public Matrix4x4 inverse
		{
			get
			{
				return Matrix4x4.Inverse(this);
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00018068 File Offset: 0x00016268
		[FreeFunction("MatrixScripting::Transpose", IsThreadSafe = true)]
		public static Matrix4x4 Transpose(Matrix4x4 m)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Transpose_Injected(ref m, out matrix4x);
			return matrix4x;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00018080 File Offset: 0x00016280
		public Matrix4x4 transpose
		{
			get
			{
				return Matrix4x4.Transpose(this);
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000180A0 File Offset: 0x000162A0
		[FreeFunction("MatrixScripting::Ortho", IsThreadSafe = true)]
		public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Ortho_Injected(left, right, bottom, top, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000180C0 File Offset: 0x000162C0
		[FreeFunction("MatrixScripting::Perspective", IsThreadSafe = true)]
		public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Perspective_Injected(fov, aspect, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000180DC File Offset: 0x000162DC
		[FreeFunction("MatrixScripting::LookAt", IsThreadSafe = true)]
		public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.LookAt_Injected(ref from, ref to, ref up, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000180F8 File Offset: 0x000162F8
		[FreeFunction("MatrixScripting::Frustum", IsThreadSafe = true)]
		public static Matrix4x4 Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Frustum_Injected(left, right, bottom, top, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00018118 File Offset: 0x00016318
		public static Matrix4x4 Frustum(FrustumPlanes fp)
		{
			return Matrix4x4.Frustum(fp.left, fp.right, fp.bottom, fp.top, fp.zNear, fp.zFar);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00018154 File Offset: 0x00016354
		public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
		{
			this.m00 = column0.x;
			this.m01 = column1.x;
			this.m02 = column2.x;
			this.m03 = column3.x;
			this.m10 = column0.y;
			this.m11 = column1.y;
			this.m12 = column2.y;
			this.m13 = column3.y;
			this.m20 = column0.z;
			this.m21 = column1.z;
			this.m22 = column2.z;
			this.m23 = column3.z;
			this.m30 = column0.w;
			this.m31 = column1.w;
			this.m32 = column2.w;
			this.m33 = column3.w;
		}

		// Token: 0x17000362 RID: 866
		public float this[int row, int column]
		{
			get
			{
				return this[row + column * 4];
			}
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x17000363 RID: 867
		public float this[int index]
		{
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.m00;
					break;
				case 1:
					num = this.m10;
					break;
				case 2:
					num = this.m20;
					break;
				case 3:
					num = this.m30;
					break;
				case 4:
					num = this.m01;
					break;
				case 5:
					num = this.m11;
					break;
				case 6:
					num = this.m21;
					break;
				case 7:
					num = this.m31;
					break;
				case 8:
					num = this.m02;
					break;
				case 9:
					num = this.m12;
					break;
				case 10:
					num = this.m22;
					break;
				case 11:
					num = this.m32;
					break;
				case 12:
					num = this.m03;
					break;
				case 13:
					num = this.m13;
					break;
				case 14:
					num = this.m23;
					break;
				case 15:
					num = this.m33;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
				return num;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00018458 File Offset: 0x00016658
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2) ^ (this.GetColumn(2).GetHashCode() >> 2) ^ (this.GetColumn(3).GetHashCode() >> 1);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000184C8 File Offset: 0x000166C8
		public override bool Equals(object other)
		{
			bool flag = !(other is Matrix4x4);
			return !flag && this.Equals((Matrix4x4)other);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000184FC File Offset: 0x000166FC
		public bool Equals(Matrix4x4 other)
		{
			return this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) && this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00018574 File Offset: 0x00016774
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			matrix4x.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			matrix4x.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			matrix4x.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
			matrix4x.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			matrix4x.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			matrix4x.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			matrix4x.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
			matrix4x.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			matrix4x.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			matrix4x.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			matrix4x.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
			matrix4x.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			matrix4x.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			matrix4x.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			matrix4x.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
			return matrix4x;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00018968 File Offset: 0x00016B68
		public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
		{
			Vector4 vector2;
			vector2.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
			vector2.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
			vector2.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
			vector2.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
			return vector2;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00018A74 File Offset: 0x00016C74
		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00018AE4 File Offset: 0x00016CE4
		public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00018B00 File Offset: 0x00016D00
		public Vector4 GetColumn(int index)
		{
			Vector4 vector;
			switch (index)
			{
			case 0:
				vector = new Vector4(this.m00, this.m10, this.m20, this.m30);
				break;
			case 1:
				vector = new Vector4(this.m01, this.m11, this.m21, this.m31);
				break;
			case 2:
				vector = new Vector4(this.m02, this.m12, this.m22, this.m32);
				break;
			case 3:
				vector = new Vector4(this.m03, this.m13, this.m23, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid column index!");
			}
			return vector;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00018BB8 File Offset: 0x00016DB8
		public Vector4 GetRow(int index)
		{
			Vector4 vector;
			switch (index)
			{
			case 0:
				vector = new Vector4(this.m00, this.m01, this.m02, this.m03);
				break;
			case 1:
				vector = new Vector4(this.m10, this.m11, this.m12, this.m13);
				break;
			case 2:
				vector = new Vector4(this.m20, this.m21, this.m22, this.m23);
				break;
			case 3:
				vector = new Vector4(this.m30, this.m31, this.m32, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid row index!");
			}
			return vector;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00018C6F File Offset: 0x00016E6F
		public void SetColumn(int index, Vector4 column)
		{
			this[0, index] = column.x;
			this[1, index] = column.y;
			this[2, index] = column.z;
			this[3, index] = column.w;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00018CAE File Offset: 0x00016EAE
		public void SetRow(int index, Vector4 row)
		{
			this[index, 0] = row.x;
			this[index, 1] = row.y;
			this[index, 2] = row.z;
			this[index, 3] = row.w;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00018CF0 File Offset: 0x00016EF0
		public Vector3 MultiplyPoint(Vector3 point)
		{
			Vector3 vector;
			vector.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			vector.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			vector.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			float num = this.m30 * point.x + this.m31 * point.y + this.m32 * point.z + this.m33;
			num = 1f / num;
			vector.x *= num;
			vector.y *= num;
			vector.z *= num;
			return vector;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00018E08 File Offset: 0x00017008
		public Vector3 MultiplyPoint3x4(Vector3 point)
		{
			Vector3 vector;
			vector.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			vector.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			vector.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			return vector;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00018EC0 File Offset: 0x000170C0
		public Vector3 MultiplyVector(Vector3 vector)
		{
			Vector3 vector2;
			vector2.x = this.m00 * vector.x + this.m01 * vector.y + this.m02 * vector.z;
			vector2.y = this.m10 * vector.x + this.m11 * vector.y + this.m12 * vector.z;
			vector2.z = this.m20 * vector.x + this.m21 * vector.y + this.m22 * vector.z;
			return vector2;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00018F64 File Offset: 0x00017164
		public Plane TransformPlane(Plane plane)
		{
			Matrix4x4 inverse = this.inverse;
			float x = plane.normal.x;
			float y = plane.normal.y;
			float z = plane.normal.z;
			float distance = plane.distance;
			float num = inverse.m00 * x + inverse.m10 * y + inverse.m20 * z + inverse.m30 * distance;
			float num2 = inverse.m01 * x + inverse.m11 * y + inverse.m21 * z + inverse.m31 * distance;
			float num3 = inverse.m02 * x + inverse.m12 * y + inverse.m22 * z + inverse.m32 * distance;
			float num4 = inverse.m03 * x + inverse.m13 * y + inverse.m23 * z + inverse.m33 * distance;
			return new Plane(new Vector3(num, num2, num3), num4);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0001905C File Offset: 0x0001725C
		public static Matrix4x4 Scale(Vector3 vector)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = vector.x;
			matrix4x.m01 = 0f;
			matrix4x.m02 = 0f;
			matrix4x.m03 = 0f;
			matrix4x.m10 = 0f;
			matrix4x.m11 = vector.y;
			matrix4x.m12 = 0f;
			matrix4x.m13 = 0f;
			matrix4x.m20 = 0f;
			matrix4x.m21 = 0f;
			matrix4x.m22 = vector.z;
			matrix4x.m23 = 0f;
			matrix4x.m30 = 0f;
			matrix4x.m31 = 0f;
			matrix4x.m32 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00019134 File Offset: 0x00017334
		public static Matrix4x4 Translate(Vector3 vector)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = 1f;
			matrix4x.m01 = 0f;
			matrix4x.m02 = 0f;
			matrix4x.m03 = vector.x;
			matrix4x.m10 = 0f;
			matrix4x.m11 = 1f;
			matrix4x.m12 = 0f;
			matrix4x.m13 = vector.y;
			matrix4x.m20 = 0f;
			matrix4x.m21 = 0f;
			matrix4x.m22 = 1f;
			matrix4x.m23 = vector.z;
			matrix4x.m30 = 0f;
			matrix4x.m31 = 0f;
			matrix4x.m32 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0001920C File Offset: 0x0001740C
		public static Matrix4x4 Rotate(Quaternion q)
		{
			float num = q.x * 2f;
			float num2 = q.y * 2f;
			float num3 = q.z * 2f;
			float num4 = q.x * num;
			float num5 = q.y * num2;
			float num6 = q.z * num3;
			float num7 = q.x * num2;
			float num8 = q.x * num3;
			float num9 = q.y * num3;
			float num10 = q.w * num;
			float num11 = q.w * num2;
			float num12 = q.w * num3;
			Matrix4x4 matrix4x;
			matrix4x.m00 = 1f - (num5 + num6);
			matrix4x.m10 = num7 + num12;
			matrix4x.m20 = num8 - num11;
			matrix4x.m30 = 0f;
			matrix4x.m01 = num7 - num12;
			matrix4x.m11 = 1f - (num4 + num6);
			matrix4x.m21 = num9 + num10;
			matrix4x.m31 = 0f;
			matrix4x.m02 = num8 + num11;
			matrix4x.m12 = num9 - num10;
			matrix4x.m22 = 1f - (num4 + num5);
			matrix4x.m32 = 0f;
			matrix4x.m03 = 0f;
			matrix4x.m13 = 0f;
			matrix4x.m23 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00019374 File Offset: 0x00017574
		public static Matrix4x4 zero
		{
			get
			{
				return Matrix4x4.zeroMatrix;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0001938C File Offset: 0x0001758C
		public static Matrix4x4 identity
		{
			get
			{
				return Matrix4x4.identityMatrix;
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000193A4 File Offset: 0x000175A4
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000193C8 File Offset: 0x000175C8
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000193EC File Offset: 0x000175EC
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F5";
			}
			return UnityString.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
			{
				this.m00.ToString(format, formatProvider),
				this.m01.ToString(format, formatProvider),
				this.m02.ToString(format, formatProvider),
				this.m03.ToString(format, formatProvider),
				this.m10.ToString(format, formatProvider),
				this.m11.ToString(format, formatProvider),
				this.m12.ToString(format, formatProvider),
				this.m13.ToString(format, formatProvider),
				this.m20.ToString(format, formatProvider),
				this.m21.ToString(format, formatProvider),
				this.m22.ToString(format, formatProvider),
				this.m23.ToString(format, formatProvider),
				this.m30.ToString(format, formatProvider),
				this.m31.ToString(format, formatProvider),
				this.m32.ToString(format, formatProvider),
				this.m33.ToString(format, formatProvider)
			});
		}

		// Token: 0x060010B7 RID: 4279
		[MethodImpl(4096)]
		private static extern void GetRotation_Injected(ref Matrix4x4 _unity_self, out Quaternion ret);

		// Token: 0x060010B8 RID: 4280
		[MethodImpl(4096)]
		private static extern void GetLossyScale_Injected(ref Matrix4x4 _unity_self, out Vector3 ret);

		// Token: 0x060010B9 RID: 4281
		[MethodImpl(4096)]
		private static extern bool IsIdentity_Injected(ref Matrix4x4 _unity_self);

		// Token: 0x060010BA RID: 4282
		[MethodImpl(4096)]
		private static extern float GetDeterminant_Injected(ref Matrix4x4 _unity_self);

		// Token: 0x060010BB RID: 4283
		[MethodImpl(4096)]
		private static extern void DecomposeProjection_Injected(ref Matrix4x4 _unity_self, out FrustumPlanes ret);

		// Token: 0x060010BC RID: 4284
		[MethodImpl(4096)]
		private static extern bool ValidTRS_Injected(ref Matrix4x4 _unity_self);

		// Token: 0x060010BD RID: 4285
		[MethodImpl(4096)]
		private static extern void TRS_Injected(ref Vector3 pos, ref Quaternion q, ref Vector3 s, out Matrix4x4 ret);

		// Token: 0x060010BE RID: 4286
		[MethodImpl(4096)]
		private static extern bool Inverse3DAffine_Injected(ref Matrix4x4 input, ref Matrix4x4 result);

		// Token: 0x060010BF RID: 4287
		[MethodImpl(4096)]
		private static extern void Inverse_Injected(ref Matrix4x4 m, out Matrix4x4 ret);

		// Token: 0x060010C0 RID: 4288
		[MethodImpl(4096)]
		private static extern void Transpose_Injected(ref Matrix4x4 m, out Matrix4x4 ret);

		// Token: 0x060010C1 RID: 4289
		[MethodImpl(4096)]
		private static extern void Ortho_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x060010C2 RID: 4290
		[MethodImpl(4096)]
		private static extern void Perspective_Injected(float fov, float aspect, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x060010C3 RID: 4291
		[MethodImpl(4096)]
		private static extern void LookAt_Injected(ref Vector3 from, ref Vector3 to, ref Vector3 up, out Matrix4x4 ret);

		// Token: 0x060010C4 RID: 4292
		[MethodImpl(4096)]
		private static extern void Frustum_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x040005C1 RID: 1473
		[NativeName("m_Data[0]")]
		public float m00;

		// Token: 0x040005C2 RID: 1474
		[NativeName("m_Data[1]")]
		public float m10;

		// Token: 0x040005C3 RID: 1475
		[NativeName("m_Data[2]")]
		public float m20;

		// Token: 0x040005C4 RID: 1476
		[NativeName("m_Data[3]")]
		public float m30;

		// Token: 0x040005C5 RID: 1477
		[NativeName("m_Data[4]")]
		public float m01;

		// Token: 0x040005C6 RID: 1478
		[NativeName("m_Data[5]")]
		public float m11;

		// Token: 0x040005C7 RID: 1479
		[NativeName("m_Data[6]")]
		public float m21;

		// Token: 0x040005C8 RID: 1480
		[NativeName("m_Data[7]")]
		public float m31;

		// Token: 0x040005C9 RID: 1481
		[NativeName("m_Data[8]")]
		public float m02;

		// Token: 0x040005CA RID: 1482
		[NativeName("m_Data[9]")]
		public float m12;

		// Token: 0x040005CB RID: 1483
		[NativeName("m_Data[10]")]
		public float m22;

		// Token: 0x040005CC RID: 1484
		[NativeName("m_Data[11]")]
		public float m32;

		// Token: 0x040005CD RID: 1485
		[NativeName("m_Data[12]")]
		public float m03;

		// Token: 0x040005CE RID: 1486
		[NativeName("m_Data[13]")]
		public float m13;

		// Token: 0x040005CF RID: 1487
		[NativeName("m_Data[14]")]
		public float m23;

		// Token: 0x040005D0 RID: 1488
		[NativeName("m_Data[15]")]
		public float m33;

		// Token: 0x040005D1 RID: 1489
		private static readonly Matrix4x4 zeroMatrix = new Matrix4x4(new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));

		// Token: 0x040005D2 RID: 1490
		private static readonly Matrix4x4 identityMatrix = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));
	}
}
