using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000170 RID: 368
	[UsedByNativeCode]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[NativeType(Header = "Runtime/Math/Quaternion.h")]
	public struct Quaternion : IEquatable<Quaternion>, IFormattable
	{
		// Token: 0x0600110A RID: 4362 RVA: 0x0001A730 File Offset: 0x00018930
		[FreeFunction("FromToQuaternionSafe", IsThreadSafe = true)]
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			Quaternion quaternion;
			Quaternion.FromToRotation_Injected(ref fromDirection, ref toDirection, out quaternion);
			return quaternion;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0001A74C File Offset: 0x0001894C
		[FreeFunction(IsThreadSafe = true)]
		public static Quaternion Inverse(Quaternion rotation)
		{
			Quaternion quaternion;
			Quaternion.Inverse_Injected(ref rotation, out quaternion);
			return quaternion;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0001A764 File Offset: 0x00018964
		[FreeFunction("QuaternionScripting::Slerp", IsThreadSafe = true)]
		public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.Slerp_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0001A780 File Offset: 0x00018980
		[FreeFunction("QuaternionScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.SlerpUnclamped_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0001A79C File Offset: 0x0001899C
		[FreeFunction("QuaternionScripting::Lerp", IsThreadSafe = true)]
		public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.Lerp_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0001A7B8 File Offset: 0x000189B8
		[FreeFunction("QuaternionScripting::LerpUnclamped", IsThreadSafe = true)]
		public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.LerpUnclamped_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0001A7D4 File Offset: 0x000189D4
		[FreeFunction("EulerToQuaternion", IsThreadSafe = true)]
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			Quaternion quaternion;
			Quaternion.Internal_FromEulerRad_Injected(ref euler, out quaternion);
			return quaternion;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0001A7EC File Offset: 0x000189EC
		[FreeFunction("QuaternionScripting::ToEuler", IsThreadSafe = true)]
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			Vector3 vector;
			Quaternion.Internal_ToEulerRad_Injected(ref rotation, out vector);
			return vector;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0001A803 File Offset: 0x00018A03
		[FreeFunction("QuaternionScripting::ToAxisAngle", IsThreadSafe = true)]
		private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad_Injected(ref q, out axis, out angle);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0001A810 File Offset: 0x00018A10
		[FreeFunction("QuaternionScripting::AngleAxis", IsThreadSafe = true)]
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			Quaternion quaternion;
			Quaternion.AngleAxis_Injected(angle, ref axis, out quaternion);
			return quaternion;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0001A828 File Offset: 0x00018A28
		[FreeFunction("QuaternionScripting::LookRotation", IsThreadSafe = true)]
		public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
		{
			Quaternion quaternion;
			Quaternion.LookRotation_Injected(ref forward, ref upwards, out quaternion);
			return quaternion;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0001A844 File Offset: 0x00018A44
		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			return Quaternion.LookRotation(forward, Vector3.up);
		}

		// Token: 0x17000375 RID: 885
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0001A913 File Offset: 0x00018B13
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0001A913 File Offset: 0x00018B13
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0001A934 File Offset: 0x00018B34
		public static Quaternion identity
		{
			get
			{
				return Quaternion.identityQuaternion;
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0001A94C File Offset: 0x00018B4C
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0001AA40 File Offset: 0x00018C40
		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 vector;
			vector.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			vector.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			vector.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return vector;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0001AB70 File Offset: 0x00018D70
		private static bool IsEqualUsingDot(float dot)
		{
			return dot > 0.999999f;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0001AB8C File Offset: 0x00018D8C
		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0001ABAC File Offset: 0x00018DAC
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0001ABC8 File Offset: 0x00018DC8
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0001AC14 File Offset: 0x00018E14
		[ExcludeFromDocs]
		public void SetLookRotation(Vector3 view)
		{
			Vector3 up = Vector3.up;
			this.SetLookRotation(view, up);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0001AC31 File Offset: 0x00018E31
		public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
		{
			this = Quaternion.LookRotation(view, up);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0001AC44 File Offset: 0x00018E44
		[MethodImpl(256)]
		public static float Angle(Quaternion a, Quaternion b)
		{
			float num = Quaternion.Dot(a, b);
			return Quaternion.IsEqualUsingDot(num) ? 0f : (Mathf.Acos(Mathf.Min(Mathf.Abs(num), 1f)) * 2f * 57.29578f);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0001AC90 File Offset: 0x00018E90
		private static Vector3 Internal_MakePositive(Vector3 euler)
		{
			float num = -0.005729578f;
			float num2 = 360f + num;
			bool flag = euler.x < num;
			if (flag)
			{
				euler.x += 360f;
			}
			else
			{
				bool flag2 = euler.x > num2;
				if (flag2)
				{
					euler.x -= 360f;
				}
			}
			bool flag3 = euler.y < num;
			if (flag3)
			{
				euler.y += 360f;
			}
			else
			{
				bool flag4 = euler.y > num2;
				if (flag4)
				{
					euler.y -= 360f;
				}
			}
			bool flag5 = euler.z < num;
			if (flag5)
			{
				euler.z += 360f;
			}
			else
			{
				bool flag6 = euler.z > num2;
				if (flag6)
				{
					euler.z -= 360f;
				}
			}
			return euler;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0001AD70 File Offset: 0x00018F70
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x0001AD9C File Offset: 0x00018F9C
		public Vector3 eulerAngles
		{
			get
			{
				return Quaternion.Internal_MakePositive(Quaternion.Internal_ToEulerRad(this) * 57.29578f);
			}
			set
			{
				this = Quaternion.Internal_FromEulerRad(value * 0.017453292f);
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.017453292f);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0001AE06 File Offset: 0x00019006
		[MethodImpl(256)]
		public void ToAngleAxis(out float angle, out Vector3 axis)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
			angle *= 57.29578f;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0001AE21 File Offset: 0x00019021
		public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			this = Quaternion.FromToRotation(fromDirection, toDirection);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0001AE34 File Offset: 0x00019034
		public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			float num = Quaternion.Angle(from, to);
			bool flag = num == 0f;
			Quaternion quaternion;
			if (flag)
			{
				quaternion = to;
			}
			else
			{
				quaternion = Quaternion.SlerpUnclamped(from, to, Mathf.Min(1f, maxDegreesDelta / num));
			}
			return quaternion;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0001AE74 File Offset: 0x00019074
		public static Quaternion Normalize(Quaternion q)
		{
			float num = Mathf.Sqrt(Quaternion.Dot(q, q));
			bool flag = num < Mathf.Epsilon;
			Quaternion quaternion;
			if (flag)
			{
				quaternion = Quaternion.identity;
			}
			else
			{
				quaternion = new Quaternion(q.x / num, q.y / num, q.z / num, q.w / num);
			}
			return quaternion;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0001AECC File Offset: 0x000190CC
		public void Normalize()
		{
			this = Quaternion.Normalize(this);
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0001AEE0 File Offset: 0x000190E0
		public Quaternion normalized
		{
			get
			{
				return Quaternion.Normalize(this);
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0001AF00 File Offset: 0x00019100
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0001AF48 File Offset: 0x00019148
		public override bool Equals(object other)
		{
			bool flag = !(other is Quaternion);
			return !flag && this.Equals((Quaternion)other);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0001AF7C File Offset: 0x0001917C
		public bool Equals(Quaternion other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0001AFDC File Offset: 0x000191DC
		public override string ToString()
		{
			return this.ToString(null, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0001B000 File Offset: 0x00019200
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0001B024 File Offset: 0x00019224
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

		// Token: 0x06001135 RID: 4405 RVA: 0x0001B098 File Offset: 0x00019298
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Quaternion EulerRotation(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0001B0B8 File Offset: 0x000192B8
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Quaternion EulerRotation(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0001B0D0 File Offset: 0x000192D0
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetEulerRotation(float x, float y, float z)
		{
			this = Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0001B0E6 File Offset: 0x000192E6
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetEulerRotation(Vector3 euler)
		{
			this = Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0001B0F8 File Offset: 0x000192F8
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		public Vector3 ToEuler()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0001B118 File Offset: 0x00019318
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Quaternion EulerAngles(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0001B138 File Offset: 0x00019338
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Quaternion EulerAngles(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0001B150 File Offset: 0x00019350
		[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
		public void ToAxisAngle(out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0001B161 File Offset: 0x00019361
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetEulerAngles(float x, float y, float z)
		{
			this.SetEulerRotation(new Vector3(x, y, z));
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0001B173 File Offset: 0x00019373
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetEulerAngles(Vector3 euler)
		{
			this = Quaternion.EulerRotation(euler);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0001B184 File Offset: 0x00019384
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		public static Vector3 ToEulerAngles(Quaternion rotation)
		{
			return Quaternion.Internal_ToEulerRad(rotation);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0001B19C File Offset: 0x0001939C
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		public Vector3 ToEulerAngles()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0001B1B9 File Offset: 0x000193B9
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
		public void SetAxisAngle(Vector3 axis, float angle)
		{
			this = Quaternion.AxisAngle(axis, angle);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0001B1CC File Offset: 0x000193CC
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion AxisAngle(Vector3 axis, float angle)
		{
			return Quaternion.AngleAxis(57.29578f * angle, axis);
		}

		// Token: 0x06001144 RID: 4420
		[MethodImpl(4096)]
		private static extern void FromToRotation_Injected(ref Vector3 fromDirection, ref Vector3 toDirection, out Quaternion ret);

		// Token: 0x06001145 RID: 4421
		[MethodImpl(4096)]
		private static extern void Inverse_Injected(ref Quaternion rotation, out Quaternion ret);

		// Token: 0x06001146 RID: 4422
		[MethodImpl(4096)]
		private static extern void Slerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001147 RID: 4423
		[MethodImpl(4096)]
		private static extern void SlerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001148 RID: 4424
		[MethodImpl(4096)]
		private static extern void Lerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001149 RID: 4425
		[MethodImpl(4096)]
		private static extern void LerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x0600114A RID: 4426
		[MethodImpl(4096)]
		private static extern void Internal_FromEulerRad_Injected(ref Vector3 euler, out Quaternion ret);

		// Token: 0x0600114B RID: 4427
		[MethodImpl(4096)]
		private static extern void Internal_ToEulerRad_Injected(ref Quaternion rotation, out Vector3 ret);

		// Token: 0x0600114C RID: 4428
		[MethodImpl(4096)]
		private static extern void Internal_ToAxisAngleRad_Injected(ref Quaternion q, out Vector3 axis, out float angle);

		// Token: 0x0600114D RID: 4429
		[MethodImpl(4096)]
		private static extern void AngleAxis_Injected(float angle, ref Vector3 axis, out Quaternion ret);

		// Token: 0x0600114E RID: 4430
		[MethodImpl(4096)]
		private static extern void LookRotation_Injected(ref Vector3 forward, [DefaultValue("Vector3.up")] ref Vector3 upwards, out Quaternion ret);

		// Token: 0x040005E2 RID: 1506
		public float x;

		// Token: 0x040005E3 RID: 1507
		public float y;

		// Token: 0x040005E4 RID: 1508
		public float z;

		// Token: 0x040005E5 RID: 1509
		public float w;

		// Token: 0x040005E6 RID: 1510
		private static readonly Quaternion identityQuaternion = new Quaternion(0f, 0f, 0f, 1f);

		// Token: 0x040005E7 RID: 1511
		public const float kEpsilon = 1E-06f;
	}
}
