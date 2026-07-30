using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200001A RID: 26
	internal struct DiscreteTime : IComparable
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006EC9 File Offset: 0x000050C9
		public static double tickValue
		{
			get
			{
				return 1E-12;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006ED4 File Offset: 0x000050D4
		public DiscreteTime(DiscreteTime time)
		{
			this.m_DiscreteTime = time.m_DiscreteTime;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006EE2 File Offset: 0x000050E2
		private DiscreteTime(long time)
		{
			this.m_DiscreteTime = time;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006EEB File Offset: 0x000050EB
		public DiscreteTime(double time)
		{
			this.m_DiscreteTime = DiscreteTime.DoubleToDiscreteTime(time);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006EF9 File Offset: 0x000050F9
		public DiscreteTime(float time)
		{
			this.m_DiscreteTime = DiscreteTime.FloatToDiscreteTime(time);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006F07 File Offset: 0x00005107
		public DiscreteTime(int time)
		{
			this.m_DiscreteTime = DiscreteTime.IntToDiscreteTime(time);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006F15 File Offset: 0x00005115
		public DiscreteTime(int frame, double fps)
		{
			this.m_DiscreteTime = DiscreteTime.DoubleToDiscreteTime((double)frame * fps);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006F26 File Offset: 0x00005126
		public DiscreteTime OneTickBefore()
		{
			return new DiscreteTime(this.m_DiscreteTime - 1L);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006F36 File Offset: 0x00005136
		public DiscreteTime OneTickAfter()
		{
			return new DiscreteTime(this.m_DiscreteTime + 1L);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006F46 File Offset: 0x00005146
		public long GetTick()
		{
			return this.m_DiscreteTime;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006F4E File Offset: 0x0000514E
		public static DiscreteTime FromTicks(long ticks)
		{
			return new DiscreteTime(ticks);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006F58 File Offset: 0x00005158
		public int CompareTo(object obj)
		{
			if (obj is DiscreteTime)
			{
				return this.m_DiscreteTime.CompareTo(((DiscreteTime)obj).m_DiscreteTime);
			}
			return 1;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006F88 File Offset: 0x00005188
		public bool Equals(DiscreteTime other)
		{
			return this.m_DiscreteTime == other.m_DiscreteTime;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006F98 File Offset: 0x00005198
		public override bool Equals(object obj)
		{
			return obj is DiscreteTime && this.Equals((DiscreteTime)obj);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006FB0 File Offset: 0x000051B0
		private static long DoubleToDiscreteTime(double time)
		{
			double num = time / 1E-12 + 0.5;
			if (num < 9.223372036854776E+18 && num > -9.223372036854776E+18)
			{
				return (long)num;
			}
			throw new ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006FF8 File Offset: 0x000051F8
		private static long FloatToDiscreteTime(float time)
		{
			float num = time / 1E-12f + 0.5f;
			if (num < 9.223372E+18f && num > -9.223372E+18f)
			{
				return (long)num;
			}
			throw new ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007030 File Offset: 0x00005230
		private static long IntToDiscreteTime(int time)
		{
			return DiscreteTime.DoubleToDiscreteTime((double)time);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007039 File Offset: 0x00005239
		private static double ToDouble(long time)
		{
			return (double)time * 1E-12;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007047 File Offset: 0x00005247
		private static float ToFloat(long time)
		{
			return (float)DiscreteTime.ToDouble(time);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007050 File Offset: 0x00005250
		public static explicit operator double(DiscreteTime b)
		{
			return DiscreteTime.ToDouble(b.m_DiscreteTime);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000705D File Offset: 0x0000525D
		public static explicit operator float(DiscreteTime b)
		{
			return DiscreteTime.ToFloat(b.m_DiscreteTime);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006F46 File Offset: 0x00005146
		public static explicit operator long(DiscreteTime b)
		{
			return b.m_DiscreteTime;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000706A File Offset: 0x0000526A
		public static explicit operator DiscreteTime(double time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007072 File Offset: 0x00005272
		public static explicit operator DiscreteTime(float time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000707A File Offset: 0x0000527A
		public static implicit operator DiscreteTime(int time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006F4E File Offset: 0x0000514E
		public static explicit operator DiscreteTime(long time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006F88 File Offset: 0x00005188
		public static bool operator ==(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime == rhs.m_DiscreteTime;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007082 File Offset: 0x00005282
		public static bool operator !=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000708E File Offset: 0x0000528E
		public static bool operator >(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime > rhs.m_DiscreteTime;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000709E File Offset: 0x0000529E
		public static bool operator <(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime < rhs.m_DiscreteTime;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000070AE File Offset: 0x000052AE
		public static bool operator <=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime <= rhs.m_DiscreteTime;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000070C1 File Offset: 0x000052C1
		public static bool operator >=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime >= rhs.m_DiscreteTime;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000070D4 File Offset: 0x000052D4
		public static DiscreteTime operator +(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(lhs.m_DiscreteTime + rhs.m_DiscreteTime);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000070E8 File Offset: 0x000052E8
		public static DiscreteTime operator -(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(lhs.m_DiscreteTime - rhs.m_DiscreteTime);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000070FC File Offset: 0x000052FC
		public override string ToString()
		{
			return this.m_DiscreteTime.ToString();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007118 File Offset: 0x00005318
		public override int GetHashCode()
		{
			return this.m_DiscreteTime.GetHashCode();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007133 File Offset: 0x00005333
		public static DiscreteTime Min(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(Math.Min(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000714B File Offset: 0x0000534B
		public static DiscreteTime Max(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(Math.Max(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007163 File Offset: 0x00005363
		public static double SnapToNearestTick(double time)
		{
			return DiscreteTime.ToDouble(DiscreteTime.DoubleToDiscreteTime(time));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007170 File Offset: 0x00005370
		public static float SnapToNearestTick(float time)
		{
			return DiscreteTime.ToFloat(DiscreteTime.FloatToDiscreteTime(time));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000717D File Offset: 0x0000537D
		public static long GetNearestTick(double time)
		{
			return DiscreteTime.DoubleToDiscreteTime(time);
		}

		// Token: 0x040000A9 RID: 169
		private const double k_Tick = 1E-12;

		// Token: 0x040000AA RID: 170
		public static readonly DiscreteTime kMaxTime = new DiscreteTime(long.MaxValue);

		// Token: 0x040000AB RID: 171
		private readonly long m_DiscreteTime;
	}
}
