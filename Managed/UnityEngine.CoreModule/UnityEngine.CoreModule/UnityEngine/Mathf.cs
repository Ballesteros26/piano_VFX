using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000171 RID: 369
	[NativeHeader("Runtime/Math/PerlinNoise.h")]
	[NativeHeader("Runtime/Math/FloatConversion.h")]
	[NativeHeader("Runtime/Math/ColorSpaceConversion.h")]
	[NativeHeader("Runtime/Utilities/BitUtility.h")]
	public struct Mathf
	{
		// Token: 0x0600114F RID: 4431
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern int ClosestPowerOfTwo(int value);

		// Token: 0x06001150 RID: 4432
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsPowerOfTwo(int value);

		// Token: 0x06001151 RID: 4433
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern int NextPowerOfTwo(int value);

		// Token: 0x06001152 RID: 4434
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern float GammaToLinearSpace(float value);

		// Token: 0x06001153 RID: 4435
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern float LinearToGammaSpace(float value);

		// Token: 0x06001154 RID: 4436 RVA: 0x0001B20C File Offset: 0x0001940C
		[FreeFunction(IsThreadSafe = true)]
		public static Color CorrelatedColorTemperatureToRGB(float kelvin)
		{
			Color color;
			Mathf.CorrelatedColorTemperatureToRGB_Injected(kelvin, out color);
			return color;
		}

		// Token: 0x06001155 RID: 4437
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern ushort FloatToHalf(float val);

		// Token: 0x06001156 RID: 4438
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern float HalfToFloat(ushort val);

		// Token: 0x06001157 RID: 4439
		[FreeFunction("PerlinNoise::NoiseNormalized", IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern float PerlinNoise(float x, float y);

		// Token: 0x06001158 RID: 4440 RVA: 0x0001B224 File Offset: 0x00019424
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0001B240 File Offset: 0x00019440
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0001B25C File Offset: 0x0001945C
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0001B278 File Offset: 0x00019478
		public static float Asin(float f)
		{
			return (float)Math.Asin((double)f);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0001B294 File Offset: 0x00019494
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0001B2B0 File Offset: 0x000194B0
		public static float Atan(float f)
		{
			return (float)Math.Atan((double)f);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0001B2CC File Offset: 0x000194CC
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0001B2E8 File Offset: 0x000194E8
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0001B304 File Offset: 0x00019504
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0001B320 File Offset: 0x00019520
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0001B338 File Offset: 0x00019538
		public static float Min(float a, float b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0001B354 File Offset: 0x00019554
		public static float Min(params float[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] < num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0001B3AC File Offset: 0x000195AC
		public static int Min(int a, int b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0001B3C8 File Offset: 0x000195C8
		public static int Min(params int[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			int num2;
			if (flag)
			{
				num2 = 0;
			}
			else
			{
				int num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] < num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0001B41C File Offset: 0x0001961C
		public static float Max(float a, float b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0001B438 File Offset: 0x00019638
		public static float Max(params float[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] > num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0001B490 File Offset: 0x00019690
		public static int Max(int a, int b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0001B4AC File Offset: 0x000196AC
		public static int Max(params int[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			int num2;
			if (flag)
			{
				num2 = 0;
			}
			else
			{
				int num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] > num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0001B500 File Offset: 0x00019700
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0001B51C File Offset: 0x0001971C
		public static float Exp(float power)
		{
			return (float)Math.Exp((double)power);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0001B538 File Offset: 0x00019738
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0001B554 File Offset: 0x00019754
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0001B570 File Offset: 0x00019770
		public static float Log10(float f)
		{
			return (float)Math.Log10((double)f);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0001B58C File Offset: 0x0001978C
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0001B5C4 File Offset: 0x000197C4
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0001B5E0 File Offset: 0x000197E0
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0001B5FC File Offset: 0x000197FC
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0001B618 File Offset: 0x00019818
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0001B634 File Offset: 0x00019834
		public static float Sign(float f)
		{
			return (f >= 0f) ? 1f : (-1f);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0001B65C File Offset: 0x0001985C
		public static float Clamp(float value, float min, float max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0001B688 File Offset: 0x00019888
		public static int Clamp(int value, int min, int max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0001B6B4 File Offset: 0x000198B4
		public static float Clamp01(float value)
		{
			bool flag = value < 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = value > 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					num = value;
				}
			}
			return num;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0001B6F0 File Offset: 0x000198F0
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Mathf.Clamp01(t);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0001B710 File Offset: 0x00019910
		public static float LerpUnclamped(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0001B72C File Offset: 0x0001992C
		public static float LerpAngle(float a, float b, float t)
		{
			float num = Mathf.Repeat(b - a, 360f);
			bool flag = num > 180f;
			if (flag)
			{
				num -= 360f;
			}
			return a + num * Mathf.Clamp01(t);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0001B76C File Offset: 0x0001996C
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			bool flag = Mathf.Abs(target - current) <= maxDelta;
			float num;
			if (flag)
			{
				num = target;
			}
			else
			{
				num = current + Mathf.Sign(target - current) * maxDelta;
			}
			return num;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0001B7A0 File Offset: 0x000199A0
		public static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			float num = Mathf.DeltaAngle(current, target);
			bool flag = -maxDelta < num && num < maxDelta;
			float num2;
			if (flag)
			{
				num2 = target;
			}
			else
			{
				target = current + num;
				num2 = Mathf.MoveTowards(current, target, maxDelta);
			}
			return num2;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0001B7DC File Offset: 0x000199DC
		public static float SmoothStep(float from, float to, float t)
		{
			t = Mathf.Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0001B81C File Offset: 0x00019A1C
		public static float Gamma(float value, float absmax, float gamma)
		{
			bool flag = value < 0f;
			float num = Mathf.Abs(value);
			bool flag2 = num > absmax;
			float num2;
			if (flag2)
			{
				num2 = (flag ? (-num) : num);
			}
			else
			{
				float num3 = Mathf.Pow(num / absmax, gamma) * absmax;
				num2 = (flag ? (-num3) : num3);
			}
			return num2;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0001B868 File Offset: 0x00019A68
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0001B8AC File Offset: 0x00019AAC
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0001B8D0 File Offset: 0x00019AD0
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0001B8FC File Offset: 0x00019AFC
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			bool flag = num5 - current > 0f == num8 > num5;
			if (flag)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0001B9DC File Offset: 0x00019BDC
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0001BA08 File Offset: 0x00019C08
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0001BA34 File Offset: 0x00019C34
		public static float Repeat(float t, float length)
		{
			return Mathf.Clamp(t - Mathf.Floor(t / length) * length, 0f, length);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0001BA60 File Offset: 0x00019C60
		public static float PingPong(float t, float length)
		{
			t = Mathf.Repeat(t, length * 2f);
			return length - Mathf.Abs(t - length);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0001BA8C File Offset: 0x00019C8C
		public static float InverseLerp(float a, float b, float value)
		{
			bool flag = a != b;
			float num;
			if (flag)
			{
				num = Mathf.Clamp01((value - a) / (b - a));
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0001BAC0 File Offset: 0x00019CC0
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 360f);
			bool flag = num > 180f;
			if (flag)
			{
				num -= 360f;
			}
			return num;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0001BAF8 File Offset: 0x00019CF8
		internal static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool flag = num5 == 0f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				result.x = p1.x + num8 * num;
				result.y = p1.y + num8 * num2;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		internal static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool flag = num5 == 0f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				bool flag3 = num8 < 0f || num8 > 1f;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					float num9 = (num6 * num2 - num7 * num) / num5;
					bool flag4 = num9 < 0f || num9 > 1f;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						result.x = p1.x + num8 * num;
						result.y = p1.y + num8 * num2;
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0001BCC4 File Offset: 0x00019EC4
		internal static long RandomToLong(Random r)
		{
			byte[] array = new byte[8];
			r.NextBytes(array);
			return (long)(BitConverter.ToUInt64(array, 0) & 9223372036854775807UL);
		}

		// Token: 0x0600118F RID: 4495
		[MethodImpl(4096)]
		private static extern void CorrelatedColorTemperatureToRGB_Injected(float kelvin, out Color ret);

		// Token: 0x040005E8 RID: 1512
		public const float PI = 3.1415927f;

		// Token: 0x040005E9 RID: 1513
		public const float Infinity = float.PositiveInfinity;

		// Token: 0x040005EA RID: 1514
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x040005EB RID: 1515
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x040005EC RID: 1516
		public const float Rad2Deg = 57.29578f;

		// Token: 0x040005ED RID: 1517
		public static readonly float Epsilon = (MathfInternal.IsFlushToZeroEnabled ? MathfInternal.FloatMinNormal : MathfInternal.FloatMinDenormal);
	}
}
