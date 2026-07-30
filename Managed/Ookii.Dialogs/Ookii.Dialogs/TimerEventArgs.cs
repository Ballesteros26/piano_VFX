using System;

namespace Ookii.Dialogs
{
	// Token: 0x02000023 RID: 35
	public class TimerEventArgs : EventArgs
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x000081FE File Offset: 0x000063FE
		public TimerEventArgs(int tickCount)
		{
			this._tickCount = tickCount;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008210 File Offset: 0x00006410
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00008228 File Offset: 0x00006428
		public bool ResetTickCount
		{
			get
			{
				return this._resetTickCount;
			}
			set
			{
				this._resetTickCount = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008234 File Offset: 0x00006434
		public int TickCount
		{
			get
			{
				return this._tickCount;
			}
		}

		// Token: 0x040000A4 RID: 164
		private int _tickCount;

		// Token: 0x040000A5 RID: 165
		private bool _resetTickCount;
	}
}
