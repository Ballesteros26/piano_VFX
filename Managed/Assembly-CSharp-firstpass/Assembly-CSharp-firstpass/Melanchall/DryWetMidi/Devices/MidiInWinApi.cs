using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000EF RID: 239
	internal static class MidiInWinApi
	{
		// Token: 0x060005EC RID: 1516
		[DllImport("winmm.dll", SetLastError = true)]
		public static extern uint midiInGetDevCaps(IntPtr uDeviceID, ref MidiInWinApi.MIDIINCAPS caps, uint cbMidiInCaps);

		// Token: 0x060005ED RID: 1517
		[DllImport("winmm.dll")]
		public static extern uint midiInGetErrorText(uint wError, StringBuilder lpText, uint cchText);

		// Token: 0x060005EE RID: 1518
		[DllImport("winmm.dll")]
		public static extern uint midiInGetNumDevs();

		// Token: 0x060005EF RID: 1519
		[DllImport("winmm.dll")]
		public static extern uint midiInOpen(out IntPtr lphMidiIn, int uDeviceID, MidiWinApi.MidiMessageCallback dwCallback, IntPtr dwInstance, uint dwFlags);

		// Token: 0x060005F0 RID: 1520
		[DllImport("winmm.dll")]
		public static extern uint midiInClose(IntPtr hMidiIn);

		// Token: 0x060005F1 RID: 1521
		[DllImport("winmm.dll")]
		public static extern uint midiInStart(IntPtr hMidiIn);

		// Token: 0x060005F2 RID: 1522
		[DllImport("winmm.dll")]
		public static extern uint midiInStop(IntPtr hMidiIn);

		// Token: 0x060005F3 RID: 1523
		[DllImport("winmm.dll")]
		public static extern uint midiInReset(IntPtr hMidiIn);

		// Token: 0x060005F4 RID: 1524
		[DllImport("winmm.dll")]
		public static extern uint midiInPrepareHeader(IntPtr hMidiIn, IntPtr lpMidiInHdr, int cbMidiInHdr);

		// Token: 0x060005F5 RID: 1525
		[DllImport("winmm.dll")]
		public static extern uint midiInUnprepareHeader(IntPtr hMidiIn, IntPtr lpMidiInHdr, int cbMidiInHdr);

		// Token: 0x060005F6 RID: 1526
		[DllImport("winmm.dll")]
		public static extern uint midiInAddBuffer(IntPtr hMidiIn, IntPtr lpMidiInHdr, int cbMidiInHdr);

		// Token: 0x02000269 RID: 617
		internal struct MIDIINCAPS
		{
			// Token: 0x04000D36 RID: 3382
			public ushort wMid;

			// Token: 0x04000D37 RID: 3383
			public ushort wPid;

			// Token: 0x04000D38 RID: 3384
			public uint vDriverVersion;

			// Token: 0x04000D39 RID: 3385
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string szPname;

			// Token: 0x04000D3A RID: 3386
			public uint dwSupport;
		}
	}
}
