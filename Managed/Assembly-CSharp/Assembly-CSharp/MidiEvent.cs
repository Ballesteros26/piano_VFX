using System;

// Token: 0x02000013 RID: 19
public class MidiEvent
{
	// Token: 0x0600008E RID: 142 RVA: 0x00009890 File Offset: 0x00007A90
	public MidiEvent(byte type, byte key, byte velocity, uint deltaTick)
	{
		this.type = type;
		this.key = key;
		this.velocity = velocity;
		this.deltaTick = deltaTick;
	}

	// Token: 0x0400019B RID: 411
	public byte type;

	// Token: 0x0400019C RID: 412
	public byte key;

	// Token: 0x0400019D RID: 413
	public byte velocity;

	// Token: 0x0400019E RID: 414
	public uint wallTick;

	// Token: 0x0400019F RID: 415
	public uint deltaTick;
}
