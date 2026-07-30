using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000352 RID: 850
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Math/SphericalHarmonicsL2.bindings.h")]
	public struct SphericalHarmonicsL2 : IEquatable<SphericalHarmonicsL2>
	{
		// Token: 0x06001CF6 RID: 7414 RVA: 0x0002F6F0 File Offset: 0x0002D8F0
		public void Clear()
		{
			this.SetZero();
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0002F6FA File Offset: 0x0002D8FA
		private void SetZero()
		{
			SphericalHarmonicsL2.SetZero_Injected(ref this);
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0002F702 File Offset: 0x0002D902
		public void AddAmbientLight(Color color)
		{
			SphericalHarmonicsL2.AddAmbientLight_Injected(ref this, ref color);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0002F70C File Offset: 0x0002D90C
		public void AddDirectionalLight(Vector3 direction, Color color, float intensity)
		{
			Color color2 = color * (2f * intensity);
			SphericalHarmonicsL2.AddDirectionalLightInternal(ref this, direction, color2);
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0002F731 File Offset: 0x0002D931
		[FreeFunction]
		private static void AddDirectionalLightInternal(ref SphericalHarmonicsL2 sh, Vector3 direction, Color color)
		{
			SphericalHarmonicsL2.AddDirectionalLightInternal_Injected(ref sh, ref direction, ref color);
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0002F740 File Offset: 0x0002D940
		public void Evaluate(Vector3[] directions, Color[] results)
		{
			bool flag = directions == null;
			if (flag)
			{
				throw new ArgumentNullException("directions");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentNullException("results");
			}
			bool flag3 = directions.Length == 0;
			if (!flag3)
			{
				bool flag4 = directions.Length != results.Length;
				if (flag4)
				{
					throw new ArgumentException("Length of the directions array and the results array must match.");
				}
				SphericalHarmonicsL2.EvaluateInternal(ref this, directions, results);
			}
		}

		// Token: 0x06001CFC RID: 7420
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void EvaluateInternal(ref SphericalHarmonicsL2 sh, Vector3[] directions, [Out] Color[] results);

		// Token: 0x17000545 RID: 1349
		public float this[int rgb, int coefficient]
		{
			get
			{
				float num;
				switch (rgb * 9 + coefficient)
				{
				case 0:
					num = this.shr0;
					break;
				case 1:
					num = this.shr1;
					break;
				case 2:
					num = this.shr2;
					break;
				case 3:
					num = this.shr3;
					break;
				case 4:
					num = this.shr4;
					break;
				case 5:
					num = this.shr5;
					break;
				case 6:
					num = this.shr6;
					break;
				case 7:
					num = this.shr7;
					break;
				case 8:
					num = this.shr8;
					break;
				case 9:
					num = this.shg0;
					break;
				case 10:
					num = this.shg1;
					break;
				case 11:
					num = this.shg2;
					break;
				case 12:
					num = this.shg3;
					break;
				case 13:
					num = this.shg4;
					break;
				case 14:
					num = this.shg5;
					break;
				case 15:
					num = this.shg6;
					break;
				case 16:
					num = this.shg7;
					break;
				case 17:
					num = this.shg8;
					break;
				case 18:
					num = this.shb0;
					break;
				case 19:
					num = this.shb1;
					break;
				case 20:
					num = this.shb2;
					break;
				case 21:
					num = this.shb3;
					break;
				case 22:
					num = this.shb4;
					break;
				case 23:
					num = this.shb5;
					break;
				case 24:
					num = this.shb6;
					break;
				case 25:
					num = this.shb7;
					break;
				case 26:
					num = this.shb8;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
				return num;
			}
			set
			{
				switch (rgb * 9 + coefficient)
				{
				case 0:
					this.shr0 = value;
					break;
				case 1:
					this.shr1 = value;
					break;
				case 2:
					this.shr2 = value;
					break;
				case 3:
					this.shr3 = value;
					break;
				case 4:
					this.shr4 = value;
					break;
				case 5:
					this.shr5 = value;
					break;
				case 6:
					this.shr6 = value;
					break;
				case 7:
					this.shr7 = value;
					break;
				case 8:
					this.shr8 = value;
					break;
				case 9:
					this.shg0 = value;
					break;
				case 10:
					this.shg1 = value;
					break;
				case 11:
					this.shg2 = value;
					break;
				case 12:
					this.shg3 = value;
					break;
				case 13:
					this.shg4 = value;
					break;
				case 14:
					this.shg5 = value;
					break;
				case 15:
					this.shg6 = value;
					break;
				case 16:
					this.shg7 = value;
					break;
				case 17:
					this.shg8 = value;
					break;
				case 18:
					this.shb0 = value;
					break;
				case 19:
					this.shb1 = value;
					break;
				case 20:
					this.shb2 = value;
					break;
				case 21:
					this.shb3 = value;
					break;
				case 22:
					this.shb4 = value;
					break;
				case 23:
					this.shb5 = value;
					break;
				case 24:
					this.shb6 = value;
					break;
				case 25:
					this.shb7 = value;
					break;
				case 26:
					this.shb8 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0002FB14 File Offset: 0x0002DD14
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.shr0.GetHashCode();
			num = num * 23 + this.shr1.GetHashCode();
			num = num * 23 + this.shr2.GetHashCode();
			num = num * 23 + this.shr3.GetHashCode();
			num = num * 23 + this.shr4.GetHashCode();
			num = num * 23 + this.shr5.GetHashCode();
			num = num * 23 + this.shr6.GetHashCode();
			num = num * 23 + this.shr7.GetHashCode();
			num = num * 23 + this.shr8.GetHashCode();
			num = num * 23 + this.shg0.GetHashCode();
			num = num * 23 + this.shg1.GetHashCode();
			num = num * 23 + this.shg2.GetHashCode();
			num = num * 23 + this.shg3.GetHashCode();
			num = num * 23 + this.shg4.GetHashCode();
			num = num * 23 + this.shg5.GetHashCode();
			num = num * 23 + this.shg6.GetHashCode();
			num = num * 23 + this.shg7.GetHashCode();
			num = num * 23 + this.shg8.GetHashCode();
			num = num * 23 + this.shb0.GetHashCode();
			num = num * 23 + this.shb1.GetHashCode();
			num = num * 23 + this.shb2.GetHashCode();
			num = num * 23 + this.shb3.GetHashCode();
			num = num * 23 + this.shb4.GetHashCode();
			num = num * 23 + this.shb5.GetHashCode();
			num = num * 23 + this.shb6.GetHashCode();
			num = num * 23 + this.shb7.GetHashCode();
			return num * 23 + this.shb8.GetHashCode();
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0002FCF8 File Offset: 0x0002DEF8
		public override bool Equals(object other)
		{
			return other is SphericalHarmonicsL2 && this.Equals((SphericalHarmonicsL2)other);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0002FD24 File Offset: 0x0002DF24
		public bool Equals(SphericalHarmonicsL2 other)
		{
			return this == other;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0002FD44 File Offset: 0x0002DF44
		public static SphericalHarmonicsL2 operator *(SphericalHarmonicsL2 lhs, float rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = lhs.shr0 * rhs,
				shr1 = lhs.shr1 * rhs,
				shr2 = lhs.shr2 * rhs,
				shr3 = lhs.shr3 * rhs,
				shr4 = lhs.shr4 * rhs,
				shr5 = lhs.shr5 * rhs,
				shr6 = lhs.shr6 * rhs,
				shr7 = lhs.shr7 * rhs,
				shr8 = lhs.shr8 * rhs,
				shg0 = lhs.shg0 * rhs,
				shg1 = lhs.shg1 * rhs,
				shg2 = lhs.shg2 * rhs,
				shg3 = lhs.shg3 * rhs,
				shg4 = lhs.shg4 * rhs,
				shg5 = lhs.shg5 * rhs,
				shg6 = lhs.shg6 * rhs,
				shg7 = lhs.shg7 * rhs,
				shg8 = lhs.shg8 * rhs,
				shb0 = lhs.shb0 * rhs,
				shb1 = lhs.shb1 * rhs,
				shb2 = lhs.shb2 * rhs,
				shb3 = lhs.shb3 * rhs,
				shb4 = lhs.shb4 * rhs,
				shb5 = lhs.shb5 * rhs,
				shb6 = lhs.shb6 * rhs,
				shb7 = lhs.shb7 * rhs,
				shb8 = lhs.shb8 * rhs
			};
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0002FEF4 File Offset: 0x0002E0F4
		public static SphericalHarmonicsL2 operator *(float lhs, SphericalHarmonicsL2 rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = rhs.shr0 * lhs,
				shr1 = rhs.shr1 * lhs,
				shr2 = rhs.shr2 * lhs,
				shr3 = rhs.shr3 * lhs,
				shr4 = rhs.shr4 * lhs,
				shr5 = rhs.shr5 * lhs,
				shr6 = rhs.shr6 * lhs,
				shr7 = rhs.shr7 * lhs,
				shr8 = rhs.shr8 * lhs,
				shg0 = rhs.shg0 * lhs,
				shg1 = rhs.shg1 * lhs,
				shg2 = rhs.shg2 * lhs,
				shg3 = rhs.shg3 * lhs,
				shg4 = rhs.shg4 * lhs,
				shg5 = rhs.shg5 * lhs,
				shg6 = rhs.shg6 * lhs,
				shg7 = rhs.shg7 * lhs,
				shg8 = rhs.shg8 * lhs,
				shb0 = rhs.shb0 * lhs,
				shb1 = rhs.shb1 * lhs,
				shb2 = rhs.shb2 * lhs,
				shb3 = rhs.shb3 * lhs,
				shb4 = rhs.shb4 * lhs,
				shb5 = rhs.shb5 * lhs,
				shb6 = rhs.shb6 * lhs,
				shb7 = rhs.shb7 * lhs,
				shb8 = rhs.shb8 * lhs
			};
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000300A4 File Offset: 0x0002E2A4
		public static SphericalHarmonicsL2 operator +(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = lhs.shr0 + rhs.shr0,
				shr1 = lhs.shr1 + rhs.shr1,
				shr2 = lhs.shr2 + rhs.shr2,
				shr3 = lhs.shr3 + rhs.shr3,
				shr4 = lhs.shr4 + rhs.shr4,
				shr5 = lhs.shr5 + rhs.shr5,
				shr6 = lhs.shr6 + rhs.shr6,
				shr7 = lhs.shr7 + rhs.shr7,
				shr8 = lhs.shr8 + rhs.shr8,
				shg0 = lhs.shg0 + rhs.shg0,
				shg1 = lhs.shg1 + rhs.shg1,
				shg2 = lhs.shg2 + rhs.shg2,
				shg3 = lhs.shg3 + rhs.shg3,
				shg4 = lhs.shg4 + rhs.shg4,
				shg5 = lhs.shg5 + rhs.shg5,
				shg6 = lhs.shg6 + rhs.shg6,
				shg7 = lhs.shg7 + rhs.shg7,
				shg8 = lhs.shg8 + rhs.shg8,
				shb0 = lhs.shb0 + rhs.shb0,
				shb1 = lhs.shb1 + rhs.shb1,
				shb2 = lhs.shb2 + rhs.shb2,
				shb3 = lhs.shb3 + rhs.shb3,
				shb4 = lhs.shb4 + rhs.shb4,
				shb5 = lhs.shb5 + rhs.shb5,
				shb6 = lhs.shb6 + rhs.shb6,
				shb7 = lhs.shb7 + rhs.shb7,
				shb8 = lhs.shb8 + rhs.shb8
			};
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000302DC File Offset: 0x0002E4DC
		public static bool operator ==(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return lhs.shr0 == rhs.shr0 && lhs.shr1 == rhs.shr1 && lhs.shr2 == rhs.shr2 && lhs.shr3 == rhs.shr3 && lhs.shr4 == rhs.shr4 && lhs.shr5 == rhs.shr5 && lhs.shr6 == rhs.shr6 && lhs.shr7 == rhs.shr7 && lhs.shr8 == rhs.shr8 && lhs.shg0 == rhs.shg0 && lhs.shg1 == rhs.shg1 && lhs.shg2 == rhs.shg2 && lhs.shg3 == rhs.shg3 && lhs.shg4 == rhs.shg4 && lhs.shg5 == rhs.shg5 && lhs.shg6 == rhs.shg6 && lhs.shg7 == rhs.shg7 && lhs.shg8 == rhs.shg8 && lhs.shb0 == rhs.shb0 && lhs.shb1 == rhs.shb1 && lhs.shb2 == rhs.shb2 && lhs.shb3 == rhs.shb3 && lhs.shb4 == rhs.shb4 && lhs.shb5 == rhs.shb5 && lhs.shb6 == rhs.shb6 && lhs.shb7 == rhs.shb7 && lhs.shb8 == rhs.shb8;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000304A4 File Offset: 0x0002E6A4
		public static bool operator !=(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001D07 RID: 7431
		[MethodImpl(4096)]
		private static extern void SetZero_Injected(ref SphericalHarmonicsL2 _unity_self);

		// Token: 0x06001D08 RID: 7432
		[MethodImpl(4096)]
		private static extern void AddAmbientLight_Injected(ref SphericalHarmonicsL2 _unity_self, ref Color color);

		// Token: 0x06001D09 RID: 7433
		[MethodImpl(4096)]
		private static extern void AddDirectionalLightInternal_Injected(ref SphericalHarmonicsL2 sh, ref Vector3 direction, ref Color color);

		// Token: 0x04000A00 RID: 2560
		private float shr0;

		// Token: 0x04000A01 RID: 2561
		private float shr1;

		// Token: 0x04000A02 RID: 2562
		private float shr2;

		// Token: 0x04000A03 RID: 2563
		private float shr3;

		// Token: 0x04000A04 RID: 2564
		private float shr4;

		// Token: 0x04000A05 RID: 2565
		private float shr5;

		// Token: 0x04000A06 RID: 2566
		private float shr6;

		// Token: 0x04000A07 RID: 2567
		private float shr7;

		// Token: 0x04000A08 RID: 2568
		private float shr8;

		// Token: 0x04000A09 RID: 2569
		private float shg0;

		// Token: 0x04000A0A RID: 2570
		private float shg1;

		// Token: 0x04000A0B RID: 2571
		private float shg2;

		// Token: 0x04000A0C RID: 2572
		private float shg3;

		// Token: 0x04000A0D RID: 2573
		private float shg4;

		// Token: 0x04000A0E RID: 2574
		private float shg5;

		// Token: 0x04000A0F RID: 2575
		private float shg6;

		// Token: 0x04000A10 RID: 2576
		private float shg7;

		// Token: 0x04000A11 RID: 2577
		private float shg8;

		// Token: 0x04000A12 RID: 2578
		private float shb0;

		// Token: 0x04000A13 RID: 2579
		private float shb1;

		// Token: 0x04000A14 RID: 2580
		private float shb2;

		// Token: 0x04000A15 RID: 2581
		private float shb3;

		// Token: 0x04000A16 RID: 2582
		private float shb4;

		// Token: 0x04000A17 RID: 2583
		private float shb5;

		// Token: 0x04000A18 RID: 2584
		private float shb6;

		// Token: 0x04000A19 RID: 2585
		private float shb7;

		// Token: 0x04000A1A RID: 2586
		private float shb8;
	}
}
