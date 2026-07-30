using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000105 RID: 261
	internal sealed class NotePlaybackEventMetadata
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		public NotePlaybackEventMetadata(Note note, TimeSpan startTime, TimeSpan endTime)
		{
			this.RawNote = note;
			this.StartTime = startTime;
			this.EndTime = endTime;
			this.RawNotePlaybackData = new NotePlaybackData(this.RawNote.NoteNumber, this.RawNote.Velocity, this.RawNote.OffVelocity, this.RawNote.Channel);
			this.NotePlaybackData = this.RawNotePlaybackData;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001B94B File Offset: 0x00019B4B
		public Note RawNote { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001B953 File Offset: 0x00019B53
		public TimeSpan StartTime { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001B95B File Offset: 0x00019B5B
		public TimeSpan EndTime { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001B963 File Offset: 0x00019B63
		public NotePlaybackData RawNotePlaybackData { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001B96B File Offset: 0x00019B6B
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001B973 File Offset: 0x00019B73
		public NotePlaybackData NotePlaybackData { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001B97C File Offset: 0x00019B7C
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001B984 File Offset: 0x00019B84
		public bool IsCustomNotePlaybackDataSet { get; private set; }

		// Token: 0x060006EA RID: 1770 RVA: 0x0001B990 File Offset: 0x00019B90
		public Note GetEffectiveNote()
		{
			NotePlaybackData notePlaybackData = this.NotePlaybackData;
			if (notePlaybackData == null)
			{
				return null;
			}
			Note note = this.RawNote.Clone();
			note.NoteNumber = notePlaybackData.NoteNumber;
			note.Velocity = notePlaybackData.Velocity;
			note.OffVelocity = notePlaybackData.OffVelocity;
			note.Channel = notePlaybackData.Channel;
			return note;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		public void SetCustomNotePlaybackData(NotePlaybackData notePlaybackData)
		{
			this.NotePlaybackData = notePlaybackData;
			this.IsCustomNotePlaybackDataSet = true;
		}
	}
}
