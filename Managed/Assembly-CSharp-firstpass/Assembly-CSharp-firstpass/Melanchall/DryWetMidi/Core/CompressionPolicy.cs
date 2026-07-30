using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x020001A1 RID: 417
	[Flags]
	public enum CompressionPolicy
	{
		// Token: 0x04000963 RID: 2403
		NoCompression = 0,
		// Token: 0x04000964 RID: 2404
		Default = 31,
		// Token: 0x04000965 RID: 2405
		UseRunningStatus = 1,
		// Token: 0x04000966 RID: 2406
		NoteOffAsSilentNoteOn = 2,
		// Token: 0x04000967 RID: 2407
		DeleteDefaultTimeSignature = 4,
		// Token: 0x04000968 RID: 2408
		DeleteDefaultKeySignature = 8,
		// Token: 0x04000969 RID: 2409
		DeleteDefaultSetTempo = 16,
		// Token: 0x0400096A RID: 2410
		DeleteUnknownMetaEvents = 32,
		// Token: 0x0400096B RID: 2411
		DeleteUnknownChunks = 64
	}
}
