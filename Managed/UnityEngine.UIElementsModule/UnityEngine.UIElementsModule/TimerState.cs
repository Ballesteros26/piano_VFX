using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000049 RID: 73
	public struct TimerState : IEquatable<TimerState>
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000775A File Offset: 0x0000595A
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00007762 File Offset: 0x00005962
		public long start { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000776B File Offset: 0x0000596B
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00007773 File Offset: 0x00005973
		public long now { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000777C File Offset: 0x0000597C
		public long deltaTime
		{
			get
			{
				return this.now - this.start;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000779C File Offset: 0x0000599C
		public override bool Equals(object obj)
		{
			return obj is TimerState && this.Equals((TimerState)obj);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000077C8 File Offset: 0x000059C8
		public bool Equals(TimerState other)
		{
			return this.start == other.start && this.now == other.now && this.deltaTime == other.deltaTime;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000780C File Offset: 0x00005A0C
		public override int GetHashCode()
		{
			int num = 540054806;
			num = num * -1521134295 + this.start.GetHashCode();
			num = num * -1521134295 + this.now.GetHashCode();
			return num * -1521134295 + this.deltaTime.GetHashCode();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000786C File Offset: 0x00005A6C
		public static bool operator ==(TimerState state1, TimerState state2)
		{
			return state1.Equals(state2);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007888 File Offset: 0x00005A88
		public static bool operator !=(TimerState state1, TimerState state2)
		{
			return !(state1 == state2);
		}
	}
}
