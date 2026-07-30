using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000027 RID: 39
	public struct RealTimeSignum : IEquatable<RealTimeSignum>
	{
		// Token: 0x0600034F RID: 847 RVA: 0x000093C8 File Offset: 0x000075C8
		public RealTimeSignum(int offset)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("Offset cannot be negative");
			}
			if (offset > RealTimeSignum.MaxOffset)
			{
				throw new ArgumentOutOfRangeException("Offset greater than maximum supported SIGRT");
			}
			this.rt_offset = offset;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000093F3 File Offset: 0x000075F3
		public int Offset
		{
			get
			{
				return this.rt_offset;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000093FB File Offset: 0x000075FB
		public override int GetHashCode()
		{
			return this.rt_offset.GetHashCode();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009408 File Offset: 0x00007608
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && this.Equals((RealTimeSignum)obj);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00009438 File Offset: 0x00007638
		public bool Equals(RealTimeSignum value)
		{
			return this.Offset == value.Offset;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009449 File Offset: 0x00007649
		public static bool operator ==(RealTimeSignum lhs, RealTimeSignum rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00009453 File Offset: 0x00007653
		public static bool operator !=(RealTimeSignum lhs, RealTimeSignum rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0400009F RID: 159
		private int rt_offset;

		// Token: 0x040000A0 RID: 160
		private static readonly int MaxOffset = UnixSignal.GetSIGRTMAX() - UnixSignal.GetSIGRTMIN() - 1;

		// Token: 0x040000A1 RID: 161
		public static readonly RealTimeSignum MinValue = new RealTimeSignum(0);

		// Token: 0x040000A2 RID: 162
		public static readonly RealTimeSignum MaxValue = new RealTimeSignum(RealTimeSignum.MaxOffset);
	}
}
