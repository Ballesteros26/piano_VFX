using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000101 RID: 257
	public sealed class NotesEventArgs : EventArgs
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x0001A860 File Offset: 0x00018A60
		internal NotesEventArgs(params Note[] notes)
		{
			this.Notes = notes;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001A86F File Offset: 0x00018A6F
		public IEnumerable<Note> Notes { get; }
	}
}
