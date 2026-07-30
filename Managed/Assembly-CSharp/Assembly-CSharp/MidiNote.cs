using System;

// Token: 0x02000015 RID: 21
public class MidiNote
{
	// Token: 0x06000095 RID: 149 RVA: 0x0000A19B File Offset: 0x0000839B
	public MidiNote(byte key, byte velocity, uint startTime, uint duration)
	{
		this.key = key;
		this.velocity = velocity;
		this.velocity = velocity;
		this.startTime = startTime;
		this.duration = duration;
	}

	// Token: 0x040001A9 RID: 425
	public byte key;

	// Token: 0x040001AA RID: 426
	public byte velocity;

	// Token: 0x040001AB RID: 427
	public uint startTime;

	// Token: 0x040001AC RID: 428
	public uint duration;
}
