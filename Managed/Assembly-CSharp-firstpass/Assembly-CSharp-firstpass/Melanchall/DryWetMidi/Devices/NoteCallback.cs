using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000FB RID: 251
	// (Invoke) Token: 0x0600065F RID: 1631
	public delegate NotePlaybackData NoteCallback(NotePlaybackData rawNoteData, long rawTime, long rawLength, TimeSpan playbackTime);
}
