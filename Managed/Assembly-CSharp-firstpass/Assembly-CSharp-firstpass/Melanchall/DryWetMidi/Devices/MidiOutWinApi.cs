using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F6 RID: 246
	internal static class MidiOutWinApi
	{
		// Token: 0x0600061C RID: 1564
		[DllImport("winmm.dll", SetLastError = true)]
		public static extern uint midiOutGetDevCaps(IntPtr uDeviceID, ref MidiOutWinApi.MIDIOUTCAPS lpMidiOutCaps, uint cbMidiOutCaps);

		// Token: 0x0600061D RID: 1565
		[DllImport("winmm.dll")]
		public static extern uint midiOutGetErrorText(uint mmrError, StringBuilder pszText, uint cchText);

		// Token: 0x0600061E RID: 1566
		[DllImport("winmm.dll")]
		public static extern uint midiOutGetNumDevs();

		// Token: 0x0600061F RID: 1567
		[DllImport("winmm.dll")]
		public static extern uint midiOutOpen(out IntPtr lphmo, int uDeviceID, MidiWinApi.MidiMessageCallback dwCallback, IntPtr dwInstance, uint dwFlags);

		// Token: 0x06000620 RID: 1568
		[DllImport("winmm.dll")]
		public static extern uint midiOutClose(IntPtr hmo);

		// Token: 0x06000621 RID: 1569
		[DllImport("winmm.dll")]
		public static extern uint midiOutShortMsg(IntPtr hMidiOut, uint dwMsg);

		// Token: 0x06000622 RID: 1570
		[DllImport("winmm.dll")]
		public static extern uint midiOutGetVolume(IntPtr hmo, ref uint lpdwVolume);

		// Token: 0x06000623 RID: 1571
		[DllImport("winmm.dll")]
		public static extern uint midiOutSetVolume(IntPtr hmo, uint dwVolume);

		// Token: 0x06000624 RID: 1572
		[DllImport("winmm.dll")]
		public static extern uint midiOutPrepareHeader(IntPtr hmo, IntPtr lpMidiOutHdr, int cbMidiOutHdr);

		// Token: 0x06000625 RID: 1573
		[DllImport("winmm.dll")]
		public static extern uint midiOutUnprepareHeader(IntPtr hmo, IntPtr lpMidiOutHdr, int cbMidiOutHdr);

		// Token: 0x06000626 RID: 1574
		[DllImport("winmm.dll")]
		public static extern uint midiOutLongMsg(IntPtr hmo, IntPtr lpMidiOutHdr, int cbMidiOutHdr);

		// Token: 0x0200026A RID: 618
		public struct MIDIOUTCAPS
		{
			// Token: 0x04000D3B RID: 3387
			public ushort wMid;

			// Token: 0x04000D3C RID: 3388
			public ushort wPid;

			// Token: 0x04000D3D RID: 3389
			public uint vDriverVersion;

			// Token: 0x04000D3E RID: 3390
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string szPname;

			// Token: 0x04000D3F RID: 3391
			public ushort wTechnology;

			// Token: 0x04000D40 RID: 3392
			public ushort wVoices;

			// Token: 0x04000D41 RID: 3393
			public ushort wNotes;

			// Token: 0x04000D42 RID: 3394
			public ushort wChannelMask;

			// Token: 0x04000D43 RID: 3395
			public uint dwSupport;
		}

		// Token: 0x0200026B RID: 619
		[Flags]
		public enum MIDICAPS : uint
		{
			// Token: 0x04000D45 RID: 3397
			MIDICAPS_VOLUME = 1U,
			// Token: 0x04000D46 RID: 3398
			MIDICAPS_LRVOLUME = 2U,
			// Token: 0x04000D47 RID: 3399
			MIDICAPS_CACHE = 4U,
			// Token: 0x04000D48 RID: 3400
			MIDICAPS_STREAM = 8U
		}
	}
}
