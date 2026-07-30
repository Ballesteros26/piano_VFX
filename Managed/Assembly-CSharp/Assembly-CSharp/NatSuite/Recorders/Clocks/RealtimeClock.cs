using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NatSuite.Recorders.Internal;

namespace NatSuite.Recorders.Clocks
{
	// Token: 0x02000052 RID: 82
	[Doc("RealtimeClock")]
	public sealed class RealtimeClock : IClock
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000147F8 File Offset: 0x000129F8
		[Doc("Timestamp")]
		public long timestamp
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				long num = this.stopwatch.Elapsed.Ticks * 100L;
				if (!this.stopwatch.IsRunning)
				{
					this.stopwatch.Start();
				}
				return num;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00014834 File Offset: 0x00012A34
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00014844 File Offset: 0x00012A44
		[Doc("RealtimeClockPaused")]
		public bool paused
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get
			{
				return !this.stopwatch.IsRunning;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				(value ? new Action(this.stopwatch.Stop) : new Action(this.stopwatch.Start))();
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00014872 File Offset: 0x00012A72
		public RealtimeClock()
		{
			this.stopwatch = new Stopwatch();
		}

		// Token: 0x040003F9 RID: 1017
		private readonly Stopwatch stopwatch;
	}
}
