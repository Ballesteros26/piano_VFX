using System;

// Token: 0x0200000D RID: 13
public enum MetaEventName : byte
{
	// Token: 0x040000A5 RID: 165
	MetaSequence,
	// Token: 0x040000A6 RID: 166
	MetaText,
	// Token: 0x040000A7 RID: 167
	MetaCopyright,
	// Token: 0x040000A8 RID: 168
	MetaTrackName,
	// Token: 0x040000A9 RID: 169
	MetaInstrumentName,
	// Token: 0x040000AA RID: 170
	MetaLyrics,
	// Token: 0x040000AB RID: 171
	MetaMarker,
	// Token: 0x040000AC RID: 172
	MetaCuePoint,
	// Token: 0x040000AD RID: 173
	MetaChannelPrefix = 32,
	// Token: 0x040000AE RID: 174
	MetaEndOfTrack = 47,
	// Token: 0x040000AF RID: 175
	MetaSetTempo = 81,
	// Token: 0x040000B0 RID: 176
	MetaSMTEOffset = 84,
	// Token: 0x040000B1 RID: 177
	MetaTimeSignature = 88,
	// Token: 0x040000B2 RID: 178
	MetaKeySignature,
	// Token: 0x040000B3 RID: 179
	MetaSequencerSpecific = 127
}
