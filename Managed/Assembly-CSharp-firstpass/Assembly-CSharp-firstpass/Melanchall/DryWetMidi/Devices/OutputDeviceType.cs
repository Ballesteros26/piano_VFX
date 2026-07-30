using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F8 RID: 248
	public enum OutputDeviceType : ushort
	{
		// Token: 0x040007CC RID: 1996
		MidiPort = 1,
		// Token: 0x040007CD RID: 1997
		Synth,
		// Token: 0x040007CE RID: 1998
		SquareWaveSynth,
		// Token: 0x040007CF RID: 1999
		FmSynth,
		// Token: 0x040007D0 RID: 2000
		MidiMapper,
		// Token: 0x040007D1 RID: 2001
		WavetableSynth,
		// Token: 0x040007D2 RID: 2002
		SoftwareSynth
	}
}
