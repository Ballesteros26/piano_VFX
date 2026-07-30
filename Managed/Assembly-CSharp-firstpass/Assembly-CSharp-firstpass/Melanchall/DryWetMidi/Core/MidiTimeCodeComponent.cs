using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200015B RID: 347
	public enum MidiTimeCodeComponent : byte
	{
		// Token: 0x040008BB RID: 2235
		FramesLsb,
		// Token: 0x040008BC RID: 2236
		FramesMsb,
		// Token: 0x040008BD RID: 2237
		SecondsLsb,
		// Token: 0x040008BE RID: 2238
		SecondsMsb,
		// Token: 0x040008BF RID: 2239
		MinutesLsb,
		// Token: 0x040008C0 RID: 2240
		MinutesMsb,
		// Token: 0x040008C1 RID: 2241
		HoursLsb,
		// Token: 0x040008C2 RID: 2242
		HoursMsbAndTimeCodeType
	}
}
