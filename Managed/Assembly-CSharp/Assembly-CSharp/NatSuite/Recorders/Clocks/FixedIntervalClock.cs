using System;
using System.Runtime.CompilerServices;
using NatSuite.Recorders.Internal;

namespace NatSuite.Recorders.Clocks
{
	// Token: 0x02000050 RID: 80
	[Doc("FixedIntervalClock")]
	public sealed class FixedIntervalClock : IClock
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00014760 File Offset: 0x00012960
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00014768 File Offset: 0x00012968
		[Doc("FixedIntervalClockInterval")]
		public double interval { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00014774 File Offset: 0x00012974
		[Doc("Timestamp")]
		public long timestamp
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				double num;
				if (!this.autoTick)
				{
					num = (double)this.ticks;
				}
				else
				{
					int num2 = this.ticks;
					this.ticks = num2 + 1;
					num = (double)num2;
				}
				return (long)(num * this.interval * 1000000000.0);
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000147B5 File Offset: 0x000129B5
		[Doc("FixedIntervalClockCtorFramerate")]
		public FixedIntervalClock(int framerate, bool autoTick = true)
			: this(1.0 / (double)framerate, autoTick)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000147CA File Offset: 0x000129CA
		[Doc("FixedIntervalClockCtorInterval")]
		public FixedIntervalClock(double interval, bool autoTick = true)
		{
			this.interval = interval;
			this.ticks = 0;
			this.autoTick = autoTick;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000147E7 File Offset: 0x000129E7
		[Doc("FixedIntervalClockTick")]
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Tick()
		{
			this.ticks++;
		}

		// Token: 0x040003F7 RID: 1015
		private readonly bool autoTick;

		// Token: 0x040003F8 RID: 1016
		private int ticks;
	}
}
