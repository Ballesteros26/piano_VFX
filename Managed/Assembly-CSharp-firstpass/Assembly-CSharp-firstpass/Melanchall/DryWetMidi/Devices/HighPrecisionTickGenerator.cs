using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E3 RID: 227
	public sealed class HighPrecisionTickGenerator : ITickGenerator, IDisposable
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000593 RID: 1427 RVA: 0x000188E8 File Offset: 0x00016AE8
		// (remove) Token: 0x06000594 RID: 1428 RVA: 0x00018920 File Offset: 0x00016B20
		public event EventHandler TickGenerated;

		// Token: 0x06000595 RID: 1429 RVA: 0x00018958 File Offset: 0x00016B58
		public HighPrecisionTickGenerator(TimeSpan interval)
		{
			ThrowIfArgument.IsOutOfRange("interval", interval, HighPrecisionTickGenerator.MinInterval, HighPrecisionTickGenerator.MaxInterval, string.Format("Interval is out of [{0}, {1}] range.", HighPrecisionTickGenerator.MinInterval, HighPrecisionTickGenerator.MaxInterval));
			this._interval = (uint)interval.TotalMilliseconds;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000189AC File Offset: 0x00016BAC
		~HighPrecisionTickGenerator()
		{
			this.Dispose(false);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000189DC File Offset: 0x00016BDC
		public void TryStart()
		{
			if (this._timerId != 0U)
			{
				return;
			}
			MidiTimerWinApi.TIMECAPS timecaps = default(MidiTimerWinApi.TIMECAPS);
			HighPrecisionTickGenerator.ProcessMmResult(MidiTimerWinApi.timeGetDevCaps(ref timecaps, (uint)Marshal.SizeOf<MidiTimerWinApi.TIMECAPS>(timecaps)));
			this._resolution = Math.Min(Math.Max(timecaps.wPeriodMin, this._interval), timecaps.wPeriodMax);
			this._tickCallback = new MidiTimerWinApi.TimeProc(this.OnTick);
			HighPrecisionTickGenerator.ProcessMmResult(MidiTimerWinApi.timeBeginPeriod(this._resolution));
			this._timerId = MidiTimerWinApi.timeSetEvent(this._interval, this._resolution, this._tickCallback, IntPtr.Zero, 1U);
			if (this._timerId == 0U)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new MidiDeviceException("Unable to start tick generator.", new Win32Exception(lastWin32Error));
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018A92 File Offset: 0x00016C92
		private void OnTick(uint uID, uint uMsg, uint dwUser, uint dw1, uint dw2)
		{
			if (this._timerId == 0U || this._disposed)
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

		// Token: 0x06000599 RID: 1433 RVA: 0x00018ABB File Offset: 0x00016CBB
		private static void ProcessMmResult(uint mmResult)
		{
			if (mmResult == 1U || mmResult == 97U)
			{
				throw new MidiDeviceException("Error occurred on high precision MIDI tick generator.");
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00018AD1 File Offset: 0x00016CD1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00018AE0 File Offset: 0x00016CE0
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (this._timerId != 0U)
			{
				MidiTimerWinApi.timeEndPeriod(this._resolution);
				MidiTimerWinApi.timeKillEvent(this._timerId);
			}
			this._disposed = true;
		}

		// Token: 0x04000740 RID: 1856
		public static readonly TimeSpan MinInterval = TimeSpan.FromMilliseconds(1.0);

		// Token: 0x04000741 RID: 1857
		public static readonly TimeSpan MaxInterval = TimeSpan.FromMilliseconds(4294967295.0);

		// Token: 0x04000742 RID: 1858
		private const uint NoTimerId = 0U;

		// Token: 0x04000744 RID: 1860
		private bool _disposed;

		// Token: 0x04000745 RID: 1861
		private readonly uint _interval;

		// Token: 0x04000746 RID: 1862
		private uint _resolution;

		// Token: 0x04000747 RID: 1863
		private MidiTimerWinApi.TimeProc _tickCallback;

		// Token: 0x04000748 RID: 1864
		private uint _timerId;
	}
}
