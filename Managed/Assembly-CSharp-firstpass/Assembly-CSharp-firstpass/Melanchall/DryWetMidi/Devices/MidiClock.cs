using System;
using System.Diagnostics;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E0 RID: 224
	public sealed class MidiClock : IDisposable
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000578 RID: 1400 RVA: 0x00018588 File Offset: 0x00016788
		// (remove) Token: 0x06000579 RID: 1401 RVA: 0x000185C0 File Offset: 0x000167C0
		public event EventHandler Ticked;

		// Token: 0x0600057A RID: 1402 RVA: 0x000185F8 File Offset: 0x000167F8
		public MidiClock(bool startImmediately, ITickGenerator tickGenerator)
		{
			this._startImmediately = startImmediately;
			this._tickGenerator = tickGenerator;
			if (this._tickGenerator != null)
			{
				this._tickGenerator.TickGenerated += this.OnTickGenerated;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00018668 File Offset: 0x00016868
		~MidiClock()
		{
			this.Dispose(false);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00018698 File Offset: 0x00016898
		public bool IsRunning
		{
			get
			{
				return this._stopwatch.IsRunning;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000186A5 File Offset: 0x000168A5
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x000186AD File Offset: 0x000168AD
		public TimeSpan CurrentTime { get; private set; } = TimeSpan.Zero;

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000186B6 File Offset: 0x000168B6
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x000186C0 File Offset: 0x000168C0
		public double Speed
		{
			get
			{
				return this._speed;
			}
			set
			{
				this.EnsureIsNotDisposed();
				ThrowIfArgument.IsNegative("value", value, "Speed is negative.");
				bool isRunning = this.IsRunning;
				this.Stop();
				this._startTime = this._stopwatch.Elapsed;
				this._speed = value;
				if (isRunning)
				{
					this.Start();
				}
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00018710 File Offset: 0x00016910
		public void Start()
		{
			this.EnsureIsNotDisposed();
			if (this.IsRunning)
			{
				return;
			}
			if (!this._started)
			{
				ITickGenerator tickGenerator = this._tickGenerator;
				if (tickGenerator != null)
				{
					tickGenerator.TryStart();
				}
			}
			this._stopwatch.Start();
			if (this._startImmediately)
			{
				this.OnTicked();
			}
			this._started = true;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00018765 File Offset: 0x00016965
		public void Stop()
		{
			this.EnsureIsNotDisposed();
			this._stopwatch.Stop();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00018778 File Offset: 0x00016978
		public void Restart()
		{
			this.EnsureIsNotDisposed();
			this.Stop();
			this.ResetCurrentTime();
			this.Start();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00018792 File Offset: 0x00016992
		public void ResetCurrentTime()
		{
			this.EnsureIsNotDisposed();
			this.SetCurrentTime(TimeSpan.Zero);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000187A5 File Offset: 0x000169A5
		public void SetCurrentTime(TimeSpan time)
		{
			this.EnsureIsNotDisposed();
			this._stopwatch.Reset();
			this._startTime = time;
			this.CurrentTime = time;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000187C8 File Offset: 0x000169C8
		public void Tick()
		{
			if (!this.IsRunning || this._disposed)
			{
				return;
			}
			this.CurrentTime = this._startTime + new TimeSpan(MathUtilities.RoundToLong((double)this._stopwatch.Elapsed.Ticks * this.Speed));
			this.OnTicked();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00018822 File Offset: 0x00016A22
		private void OnTickGenerated(object sender, EventArgs e)
		{
			this.Tick();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001882A File Offset: 0x00016A2A
		private void OnTicked()
		{
			EventHandler ticked = this.Ticked;
			if (ticked == null)
			{
				return;
			}
			ticked(this, EventArgs.Empty);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00018842 File Offset: 0x00016A42
		private void EnsureIsNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("MIDI clock is disposed.");
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00018857 File Offset: 0x00016A57
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00018860 File Offset: 0x00016A60
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing && this._tickGenerator != null)
			{
				this._tickGenerator.TickGenerated -= this.OnTickGenerated;
				this._tickGenerator.Dispose();
			}
			this._disposed = true;
		}

		// Token: 0x04000735 RID: 1845
		private const double DefaultSpeed = 1.0;

		// Token: 0x04000737 RID: 1847
		private bool _disposed;

		// Token: 0x04000738 RID: 1848
		private readonly bool _startImmediately;

		// Token: 0x04000739 RID: 1849
		private readonly Stopwatch _stopwatch = new Stopwatch();

		// Token: 0x0400073A RID: 1850
		private TimeSpan _startTime = TimeSpan.Zero;

		// Token: 0x0400073B RID: 1851
		private double _speed = 1.0;

		// Token: 0x0400073C RID: 1852
		private bool _started;

		// Token: 0x0400073D RID: 1853
		private readonly ITickGenerator _tickGenerator;
	}
}
