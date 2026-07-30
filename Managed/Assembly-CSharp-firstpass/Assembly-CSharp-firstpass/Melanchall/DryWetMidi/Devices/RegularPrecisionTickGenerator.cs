using System;
using System.Timers;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E6 RID: 230
	public sealed class RegularPrecisionTickGenerator : ITickGenerator, IDisposable
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060005A5 RID: 1445 RVA: 0x00018B3C File Offset: 0x00016D3C
		// (remove) Token: 0x060005A6 RID: 1446 RVA: 0x00018B74 File Offset: 0x00016D74
		public event EventHandler TickGenerated;

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018BAC File Offset: 0x00016DAC
		public RegularPrecisionTickGenerator(TimeSpan interval)
		{
			ThrowIfArgument.IsOutOfRange("interval", interval, RegularPrecisionTickGenerator.MinInterval, RegularPrecisionTickGenerator.MaxInterval, string.Format("Interval is out of [{0}, {1}] range.", RegularPrecisionTickGenerator.MinInterval, RegularPrecisionTickGenerator.MaxInterval));
			this._timer = new Timer(interval.TotalMilliseconds);
			this._timer.Elapsed += this.OnElapsed;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018C1B File Offset: 0x00016E1B
		public void TryStart()
		{
			if (this._started)
			{
				return;
			}
			this._timer.Start();
			this._started = true;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00018C38 File Offset: 0x00016E38
		private void OnElapsed(object sender, ElapsedEventArgs e)
		{
			if (!this._started || this._disposed)
			{
				return;
			}
			EventHandler tickGenerated = this.TickGenerated;
			if (tickGenerated == null)
			{
				return;
			}
			tickGenerated(this, EventArgs.Empty);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00018C61 File Offset: 0x00016E61
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00018C6C File Offset: 0x00016E6C
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				this._timer.Stop();
				this._timer.Elapsed -= this.OnElapsed;
				this._timer.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x0400074B RID: 1867
		public static readonly TimeSpan MinInterval = TimeSpan.FromMilliseconds(1.0);

		// Token: 0x0400074C RID: 1868
		public static readonly TimeSpan MaxInterval = TimeSpan.FromMilliseconds(2147483647.0);

		// Token: 0x0400074E RID: 1870
		private bool _disposed;

		// Token: 0x0400074F RID: 1871
		private bool _started;

		// Token: 0x04000750 RID: 1872
		private readonly Timer _timer;
	}
}
