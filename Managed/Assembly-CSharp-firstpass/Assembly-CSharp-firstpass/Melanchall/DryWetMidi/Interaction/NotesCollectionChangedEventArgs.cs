using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x0200009D RID: 157
	public sealed class NotesCollectionChangedEventArgs : EventArgs
	{
		// Token: 0x0600035F RID: 863 RVA: 0x000115EA File Offset: 0x0000F7EA
		public NotesCollectionChangedEventArgs(IEnumerable<Note> addedNotes, IEnumerable<Note> removedNotes)
		{
			this.AddedNotes = addedNotes;
			this.RemovedNotes = removedNotes;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00011600 File Offset: 0x0000F800
		public IEnumerable<Note> AddedNotes { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00011608 File Offset: 0x0000F808
		public IEnumerable<Note> RemovedNotes { get; }
	}
}
