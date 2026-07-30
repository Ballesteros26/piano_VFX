using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200016F RID: 367
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeType(Header = "Runtime/Math/Vector3.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[NativeClass("Vector3f")]
	[NativeHeader("Runtime/Math/Vector3.h")]
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x00019614 File Offset: 0x00017814
		[FreeFunction("VectorScripting::Slerp", IsThreadSafe = true)]
		public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
		{
			Vector3 vector;
			Vector3.Slerp_Injected(ref a, ref b, t, out vector);
			return vector;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00019630 File Offset: 0x00017830
		[FreeFunction("VectorScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			Vector3 vector;
			Vector3.SlerpUnclamped_Injected(ref a, ref b, t, out vector);
			return vector;
		}

		// Token: 0x060010C7 RID: 4295
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void OrthoNormalize2(ref Vector3 a, ref Vector3 b);

		// Token: 0x060010C8 RID: 4296 RVA: 0x0001964A File Offset: 0x0001784A
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			Vector3.OrthoNormalize2(ref normal, ref tangent);
		}

		// Token: 0x060010C9 RID: 4297
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

		// Token: 0x060010CA RID: 4298 RVA: 0x00019655 File Offset: 0x00017855
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			Vector3.OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00019664 File Offset: 0x00017864
		[FreeFunction(IsThreadSafe = true)]
		public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			Vector3 vector;
			Vector3.RotateTowards_Injected(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out vector);
			return vector;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00019680 File Offset: 0x00017880
		[MethodImpl(256)]
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000196E4 File Offset: 0x000178E4
		[MethodImpl(256)]
		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00019740 File Offset: 0x00017940
		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = target.z - current.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			bool flag = num4 == 0f || (maxDistanceDelta >= 0f && num4 <= maxDistanceDelta * maxDistanceDelta);
			Vector3 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num5 = (float)Math.Sqrt((double)num4);
				vector = new Vector3(current.x + num / num5 * maxDistanceDelta, current.y + num2 / num5 * maxDistanceDelta, current.z + num3 / num5 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000197EC File Offset: 0x000179EC
		[ExcludeFromDocs]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00019810 File Offset: 0x00017A10
		[ExcludeFromDocs]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0001983C File Offset: 0x00017A3C
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current.x - target.x;
			float num5 = current.y - target.y;
			float num6 = current.z - target.z;
			Vector3 vector = target;
			float num7 = maxSpeed * smoothTime;
			float num8 = num7 * num7;
			float num9 = num4 * num4 + num5 * num5 + num6 * num6;
			bool flag = num9 > num8;
			if (flag)
			{
				float num10 = (float)Math.Sqrt((double)num9);
				num4 = num4 / num10 * num7;
				num5 = num5 / num10 * num7;
				num6 = num6 / num10 * num7;
			}
			target.x = current.x - num4;
			target.y = current.y - num5;
			target.z = current.z - num6;
			float num11 = (currentVelocity.x + num * num4) * deltaTime;
			float num12 = (currentVelocity.y + num * num5) * deltaTime;
			float num13 = (currentVelocity.z + num * num6) * deltaTime;
			currentVelocity.x = (currentVelocity.x - num * num11) * num3;
			currentVelocity.y = (currentVelocity.y - num * num12) * num3;
			currentVelocity.z = (currentVelocity.z - num * num13) * num3;
			float num14 = target.x + (num4 + num11) * num3;
			float num15 = target.y + (num5 + num12) * num3;
			float num16 = target.z + (num6 + num13) * num3;
			float num17 = vector.x - current.x;
			float num18 = vector.y - current.y;
			float num19 = vector.z - current.z;
			float num20 = num14 - vector.x;
			float num21 = num15 - vector.y;
			float num22 = num16 - vector.z;
			bool flag2 = num17 * num20 + num18 * num21 + num19 * num22 > 0f;
			if (flag2)
			{
				num14 = vector.x;
				num15 = vector.y;
				num16 = vector.z;
				currentVelocity.x = (num14 - vector.x) / deltaTime;
				currentVelocity.y = (num15 - vector.y) / deltaTime;
				currentVelocity.z = (num16 - vector.z) / deltaTime;
			}
			return new Vector3(num14, num15, num16);
		}

		// Token: 0x17000366 RID: 870
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00019B5A File Offset: 0x00017D5A
		[MethodImpl(256)]
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00019B72 File Offset: 0x00017D72
		[MethodImpl(256)]
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00019B5A File Offset: 0x00017D5A
		[MethodImpl(256)]
		public void Set(float newX, float newY, float newZ)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00019B90 File Offset: 0x00017D90
		[MethodImpl(256)]
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00019BCE File Offset: 0x00017DCE
		[MethodImpl(256)]
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00019C0C File Offset: 0x00017E0C
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00019C74 File Offset: 0x00017E74
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00019CB0 File Offset: 0x00017EB0
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector3);
			return !flag && this.Equals((Vector3)other);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00019CE4 File Offset: 0x00017EE4
		public bool Equals(Vector3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00019D24 File Offset: 0x00017F24
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			float num = -2f * Vector3.Dot(inNormal, inDirection);
			return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00019D78 File Offset: 0x00017F78
		public static Vector3 Normalize(Vector3 value)
		{
			float num = Vector3.Magnitude(value);
			bool flag = num > 1E-05f;
			Vector3 vector;
			if (flag)
			{
				vector = value / num;
			}
			else
			{
				vector = Vector3.zero;
			}
			return vector;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00019DAC File Offset: 0x00017FAC
		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			bool flag = num > 1E-05f;
			if (flag)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00019DF4 File Offset: 0x00017FF4
		public Vector3 normalized
		{
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00019E14 File Offset: 0x00018014
		[MethodImpl(256)]
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00019E50 File Offset: 0x00018050
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot(onNormal, onNormal);
			bool flag = num < Mathf.Epsilon;
			Vector3 vector2;
			if (flag)
			{
				vector2 = Vector3.zero;
			}
			else
			{
				float num2 = Vector3.Dot(vector, onNormal);
				vector2 = new Vector3(onNormal.x * num2 / num, onNormal.y * num2 / num, onNormal.z * num2 / num);
			}
			return vector2;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00019EAC File Offset: 0x000180AC
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			float num = Vector3.Dot(planeNormal, planeNormal);
			bool flag = num < Mathf.Epsilon;
			Vector3 vector2;
			if (flag)
			{
				vector2 = vector;
			}
			else
			{
				float num2 = Vector3.Dot(vector, planeNormal);
				vector2 = new Vector3(vector.x - planeNormal.x * num2 / num, vector.y - planeNormal.y * num2 / num, vector.z - planeNormal.z * num2 / num);
			}
			return vector2;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00019F18 File Offset: 0x00018118
		public static float Angle(Vector3 from, Vector3 to)
		{
			float num = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
			bool flag = num < 1E-15f;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = Mathf.Clamp(Vector3.Dot(from, to) / num, -1f, 1f);
				num2 = (float)Math.Acos((double)num3) * 57.29578f;
			}
			return num2;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00019F7C File Offset: 0x0001817C
		public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
		{
			float num = Vector3.Angle(from, to);
			float num2 = from.y * to.z - from.z * to.y;
			float num3 = from.z * to.x - from.x * to.z;
			float num4 = from.x * to.y - from.y * to.x;
			float num5 = Mathf.Sign(axis.x * num2 + axis.y * num3 + axis.z * num4);
			return num * num5;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0001A014 File Offset: 0x00018214
		public static float Distance(Vector3 a, Vector3 b)
		{
			float num = a.x - b.x;
			float num2 = a.y - b.y;
			float num3 = a.z - b.z;
			return (float)Math.Sqrt((double)(num * num + num2 * num2 + num3 * num3));
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0001A064 File Offset: 0x00018264
		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude > maxLength * maxLength;
			Vector3 vector2;
			if (flag)
			{
				float num = (float)Math.Sqrt((double)sqrMagnitude);
				float num2 = vector.x / num;
				float num3 = vector.y / num;
				float num4 = vector.z / num;
				vector2 = new Vector3(num2 * maxLength, num3 * maxLength, num4 * maxLength);
			}
			else
			{
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0001A0C8 File Offset: 0x000182C8
		[MethodImpl(256)]
		public static float Magnitude(Vector3 vector)
		{
			return (float)Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0001A10C File Offset: 0x0001830C
		public float magnitude
		{
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0001A150 File Offset: 0x00018350
		[MethodImpl(256)]
		public static float SqrMagnitude(Vector3 vector)
		{
			return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0001A18C File Offset: 0x0001838C
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0001A1C8 File Offset: 0x000183C8
		[MethodImpl(256)]
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0001A214 File Offset: 0x00018414
		[MethodImpl(256)]
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0001A260 File Offset: 0x00018460
		public static Vector3 zero
		{
			get
			{
				return Vector3.zeroVector;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0001A278 File Offset: 0x00018478
		public static Vector3 one
		{
			get
			{
				return Vector3.oneVector;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0001A290 File Offset: 0x00018490
		public static Vector3 forward
		{
			get
			{
				return Vector3.forwardVector;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0001A2A8 File Offset: 0x000184A8
		public static Vector3 back
		{
			get
			{
				return Vector3.backVector;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0001A2C0 File Offset: 0x000184C0
		public static Vector3 up
		{
			get
			{
				return Vector3.upVector;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0001A2D8 File Offset: 0x000184D8
		public static Vector3 down
		{
			get
			{
				return Vector3.downVector;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0001A2F0 File Offset: 0x000184F0
		public static Vector3 left
		{
			get
			{
				return Vector3.leftVector;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0001A308 File Offset: 0x00018508
		public static Vector3 right
		{
			get
			{
				return Vector3.rightVector;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0001A320 File Offset: 0x00018520
		public static Vector3 positiveInfinity
		{
			get
			{
				return Vector3.positiveInfinityVector;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0001A338 File Offset: 0x00018538
		public static Vector3 negativeInfinity
		{
			get
			{
				return Vector3.negativeInfinityVector;
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0001A350 File Offset: 0x00018550
		[MethodImpl(256)]
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0001A390 File Offset: 0x00018590
		[MethodImpl(256)]
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0001A3D0 File Offset: 0x000185D0
		[MethodImpl(256)]
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0001A3FC File Offset: 0x000185FC
		[MethodImpl(256)]
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0001A42C File Offset: 0x0001862C
		[MethodImpl(256)]
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0001A45C File Offset: 0x0001865C
		[MethodImpl(256)]
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0001A48C File Offset: 0x0001868C
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			float num3 = lhs.z - rhs.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			return num4 < 9.9999994E-11f;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0001A4E0 File Offset: 0x000186E0
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0001A4FC File Offset: 0x000186FC
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0001A520 File Offset: 0x00018720
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0001A544 File Offset: 0x00018744
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F1";
			}
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0001A5A8 File Offset: 0x000187A8
		[Obsolete("Use Vector3.forward instead.")]
		public static Vector3 fwd
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0001A5D0 File Offset: 0x000187D0
		[Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3 from, Vector3 to)
		{
			return (float)Math.Acos((double)Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f));
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0001A60C File Offset: 0x0001880C
		[Obsolete("Use Vector3.ProjectOnPlane instead.")]
		public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
		{
			return Vector3.ProjectOnPlane(fromThat, excludeThis);
		}

		// Token: 0x06001107 RID: 4359
		[MethodImpl(4096)]
		private static extern void Slerp_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x06001108 RID: 4360
		[MethodImpl(4096)]
		private static extern void SlerpUnclamped_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x06001109 RID: 4361
		[MethodImpl(4096)]
		private static extern void RotateTowards_Injected(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta, out Vector3 ret);

		// Token: 0x040005D3 RID: 1491
		public const float kEpsilon = 1E-05f;

		// Token: 0x040005D4 RID: 1492
		public const float kEpsilonNormalSqrt = 1E-15f;

		// Token: 0x040005D5 RID: 1493
		public float x;

		// Token: 0x040005D6 RID: 1494
		public float y;

		// Token: 0x040005D7 RID: 1495
		public float z;

		// Token: 0x040005D8 RID: 1496
		private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

		// Token: 0x040005D9 RID: 1497
		private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

		// Token: 0x040005DA RID: 1498
		private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

		// Token: 0x040005DB RID: 1499
		private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

		// Token: 0x040005DC RID: 1500
		private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

		// Token: 0x040005DD RID: 1501
		private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

		// Token: 0x040005DE RID: 1502
		private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

		// Token: 0x040005DF RID: 1503
		private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

		// Token: 0x040005E0 RID: 1504
		private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040005E1 RID: 1505
		private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
