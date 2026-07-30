using System;
using System.Runtime.InteropServices;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E5 RID: 229
	internal static class MidiTimerWinApi
	{
		// Token: 0x060005A0 RID: 1440
		[DllImport("winmm.dll", SetLastError = true)]
		public static extern uint timeGetDevCaps(ref MidiTimerWinApi.TIMECAPS timeCaps, uint sizeTimeCaps);

		// Token: 0x060005A1 RID: 1441
		[DllImport("winmm.dll")]
		public static extern uint timeBeginPeriod(uint uPeriod);

		// Token: 0x060005A2 RID: 1442
		[DllImport("winmm.dll")]
		public static extern uint timeEndPeriod(uint uPeriod);

		// Token: 0x060005A3 RID: 1443
		[DllImport("winmm.dll", SetLastError = true)]
		public static extern uint timeSetEvent(uint uDelay, uint uResolution, MidiTimerWinApi.TimeProc lpTimeProc, IntPtr dwUser, uint fuEvent);

		// Token: 0x060005A4 RID: 1444
		[DllImport("winmm.dll")]
		public static extern uint timeKillEvent(uint uTimerID);

		// Token: 0x04000749 RID: 1865
		public const uint TIME_ONESHOT = 0U;

		// Token: 0x0400074A RID: 1866
		public const uint TIME_PERIODIC = 1U;

		// Token: 0x02000265 RID: 613
		public struct TIMECAPS
		{
			// Token: 0x04000D2E RID: 3374
			public uint wPeriodMin;

			// Token: 0x04000D2F RID: 3375
			public uint wPeriodMax;
		}

		// Token: 0x02000266 RID: 614
		// (Invoke) Token: 0x06000E46 RID: 3654
		public delegate void TimeProc(uint uID, uint uMsg, uint dwUser, uint dw1, uint dw2);
	}
}
