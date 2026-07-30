using System;
using System.Collections.Generic;

// Token: 0x02000016 RID: 22
public class MidiTrack
{
	// Token: 0x040001AD RID: 429
	public string name;

	// Token: 0x040001AE RID: 430
	public string instrument;

	// Token: 0x040001AF RID: 431
	public List<MidiEvent> events = new List<MidiEvent>();

	// Token: 0x040001B0 RID: 432
	public List<MidiNote> notes = new List<MidiNote>();

	// Token: 0x040001B1 RID: 433
	public byte maxNote = 64;

	// Token: 0x040001B2 RID: 434
	public byte minNote = 64;
}
