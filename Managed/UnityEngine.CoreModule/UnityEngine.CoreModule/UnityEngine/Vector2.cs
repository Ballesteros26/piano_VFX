using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000173 RID: 371
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("Vector2f")]
	public struct Vector2 : IEquatable<Vector2>, IFormattable
	{
		// Token: 0x17000379 RID: 889
		public float this[int index]
		{
			get
			{
				float num;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
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
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0001BD88 File Offset: 0x00019F88
		[MethodImpl(256)]
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0001BD88 File Offset: 0x00019F88
		[MethodImpl(256)]
		public void Set(float newX, float newY)
		{
			this.x = newX;
			this.y = newY;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0001BD9C File Offset: 0x00019F9C
		[MethodImpl(256)]
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		[MethodImpl(256)]
		public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
		{
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0001BE2C File Offset: 0x0001A02C
		public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = num * num + num2 * num2;
			bool flag = num3 == 0f || (maxDistanceDelta >= 0f && num3 <= maxDistanceDelta * maxDistanceDelta);
			Vector2 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num4 = (float)Math.Sqrt((double)num3);
				vector = new Vector2(current.x + num / num4 * maxDistanceDelta, current.y + num2 / num4 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0001BEB4 File Offset: 0x0001A0B4
		[MethodImpl(256)]
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0001BEE5 File Offset: 0x0001A0E5
		[MethodImpl(256)]
		public void Scale(Vector2 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0001BF10 File Offset: 0x0001A110
		public void Normalize()
		{
			float magnitude = this.magnitude;
			bool flag = magnitude > 1E-05f;
			if (flag)
			{
				this /= magnitude;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0001BF50 File Offset: 0x0001A150
		public Vector2 normalized
		{
			get
			{
				Vector2 vector = new Vector2(this.x, this.y);
				vector.Normalize();
				return vector;
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0001BF80 File Offset: 0x0001A180
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0001BFA4 File Offset: 0x0001A1A4
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F1";
			}
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0001C01C File Offset: 0x0001A21C
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0001C048 File Offset: 0x0001A248
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector2);
			return !flag && this.Equals((Vector2)other);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0001C07C File Offset: 0x0001A27C
		[MethodImpl(256)]
		public bool Equals(Vector2 other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0001C0B0 File Offset: 0x0001A2B0
		public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
		{
			float num = -2f * Vector2.Dot(inNormal, inDirection);
			return new Vector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0001C0F4 File Offset: 0x0001A2F4
		[MethodImpl(256)]
		public static Vector2 Perpendicular(Vector2 inDirection)
		{
			return new Vector2(-inDirection.y, inDirection.x);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0001C118 File Offset: 0x0001A318
		[MethodImpl(256)]
		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0001C148 File Offset: 0x0001A348
		public float magnitude
		{
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0001C17C File Offset: 0x0001A37C
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		[MethodImpl(256)]
		public static float Angle(Vector2 from, Vector2 to)
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
				float num3 = Mathf.Clamp(Vector2.Dot(from, to) / num, -1f, 1f);
				num2 = (float)Math.Acos((double)num3) * 57.29578f;
			}
			return num2;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0001C210 File Offset: 0x0001A410
		public static float SignedAngle(Vector2 from, Vector2 to)
		{
			float num = Vector2.Angle(from, to);
			float num2 = Mathf.Sign(from.x * to.y - from.y * to.x);
			return num * num2;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0001C250 File Offset: 0x0001A450
		public static float Distance(Vector2 a, Vector2 b)
		{
			float num = a.x - b.x;
			float num2 = a.y - b.y;
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0001C28C File Offset: 0x0001A48C
		public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude > maxLength * maxLength;
			Vector2 vector2;
			if (flag)
			{
				float num = (float)Math.Sqrt((double)sqrMagnitude);
				float num2 = vector.x / num;
				float num3 = vector.y / num;
				vector2 = new Vector2(num2 * maxLength, num3 * maxLength);
			}
			else
			{
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0001C2E0 File Offset: 0x0001A4E0
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0001C310 File Offset: 0x0001A510
		public float SqrMagnitude()
		{
			return this.x * this.x + this.y * this.y;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0001C340 File Offset: 0x0001A540
		public static Vector2 Min(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0001C37C File Offset: 0x0001A57C
		public static Vector2 Max(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		[ExcludeFromDocs]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		[ExcludeFromDocs]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0001C408 File Offset: 0x0001A608
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current.x - target.x;
			float num5 = current.y - target.y;
			Vector2 vector = target;
			float num6 = maxSpeed * smoothTime;
			float num7 = num6 * num6;
			float num8 = num4 * num4 + num5 * num5;
			bool flag = num8 > num7;
			if (flag)
			{
				float num9 = (float)Math.Sqrt((double)num8);
				num4 = num4 / num9 * num6;
				num5 = num5 / num9 * num6;
			}
			target.x = current.x - num4;
			target.y = current.y - num5;
			float num10 = (currentVelocity.x + num * num4) * deltaTime;
			float num11 = (currentVelocity.y + num * num5) * deltaTime;
			currentVelocity.x = (currentVelocity.x - num * num10) * num3;
			currentVelocity.y = (currentVelocity.y - num * num11) * num3;
			float num12 = target.x + (num4 + num10) * num3;
			float num13 = target.y + (num5 + num11) * num3;
			float num14 = vector.x - current.x;
			float num15 = vector.y - current.y;
			float num16 = num12 - vector.x;
			float num17 = num13 - vector.y;
			bool flag2 = num14 * num16 + num15 * num17 > 0f;
			if (flag2)
			{
				num12 = vector.x;
				num13 = vector.y;
				currentVelocity.x = (num12 - vector.x) / deltaTime;
				currentVelocity.y = (num13 - vector.y) / deltaTime;
			}
			return new Vector2(num12, num13);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
		[MethodImpl(256)]
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0001C608 File Offset: 0x0001A808
		[MethodImpl(256)]
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0001C63C File Offset: 0x0001A83C
		[MethodImpl(256)]
		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0001C670 File Offset: 0x0001A870
		[MethodImpl(256)]
		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		[MethodImpl(256)]
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0001C6CC File Offset: 0x0001A8CC
		[MethodImpl(256)]
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0001C6F4 File Offset: 0x0001A8F4
		[MethodImpl(256)]
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0001C71C File Offset: 0x0001A91C
		[MethodImpl(256)]
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0001C744 File Offset: 0x0001A944
		[MethodImpl(256)]
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			return num * num + num2 * num2 < 9.9999994E-11f;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0001C780 File Offset: 0x0001A980
		[MethodImpl(256)]
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0001C79C File Offset: 0x0001A99C
		[MethodImpl(256)]
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		[MethodImpl(256)]
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public static Vector2 zero
		{
			get
			{
				return Vector2.zeroVector;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0001C800 File Offset: 0x0001AA00
		public static Vector2 one
		{
			get
			{
				return Vector2.oneVector;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0001C818 File Offset: 0x0001AA18
		public static Vector2 up
		{
			get
			{
				return Vector2.upVector;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0001C830 File Offset: 0x0001AA30
		public static Vector2 down
		{
			get
			{
				return Vector2.downVector;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0001C848 File Offset: 0x0001AA48
		public static Vector2 left
		{
			get
			{
				return Vector2.leftVector;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0001C860 File Offset: 0x0001AA60
		public static Vector2 right
		{
			get
			{
				return Vector2.rightVector;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0001C878 File Offset: 0x0001AA78
		public static Vector2 positiveInfinity
		{
			get
			{
				return Vector2.positiveInfinityVector;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0001C890 File Offset: 0x0001AA90
		public static Vector2 negativeInfinity
		{
			get
			{
				return Vector2.negativeInfinityVector;
			}
		}

		// Token: 0x040005EF RID: 1519
		public float x;

		// Token: 0x040005F0 RID: 1520
		public float y;

		// Token: 0x040005F1 RID: 1521
		private static readonly Vector2 zeroVector = new Vector2(0f, 0f);

		// Token: 0x040005F2 RID: 1522
		private static readonly Vector2 oneVector = new Vector2(1f, 1f);

		// Token: 0x040005F3 RID: 1523
		private static readonly Vector2 upVector = new Vector2(0f, 1f);

		// Token: 0x040005F4 RID: 1524
		private static readonly Vector2 downVector = new Vector2(0f, -1f);

		// Token: 0x040005F5 RID: 1525
		private static readonly Vector2 leftVector = new Vector2(-1f, 0f);

		// Token: 0x040005F6 RID: 1526
		private static readonly Vector2 rightVector = new Vector2(1f, 0f);

		// Token: 0x040005F7 RID: 1527
		private static readonly Vector2 positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040005F8 RID: 1528
		private static readonly Vector2 negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

		// Token: 0x040005F9 RID: 1529
		public const float kEpsilon = 1E-05f;

		// Token: 0x040005FA RID: 1530
		public const float kEpsilonNormalSqrt = 1E-15f;
	}
}
