using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x0200009C RID: 156
	public sealed class NotesCollection : TimedObjectsCollection<Note>
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000359 RID: 857 RVA: 0x00011538 File Offset: 0x0000F738
		// (remove) Token: 0x0600035A RID: 858 RVA: 0x00011570 File Offset: 0x0000F770
		public event NotesCollectionChangedEventHandler CollectionChanged;

		// Token: 0x0600035B RID: 859 RVA: 0x000115A5 File Offset: 0x0000F7A5
		internal NotesCollection(IEnumerable<Note> notes)
			: base(notes)
		{
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000115AE File Offset: 0x0000F7AE
		protected override void OnObjectsAdded(IEnumerable<Note> addedObjects)
		{
			base.OnObjectsAdded(addedObjects);
			this.OnCollectionChanged(addedObjects, null);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000115BF File Offset: 0x0000F7BF
		protected override void OnObjectsRemoved(IEnumerable<Note> removedObjects)
		{
			base.OnObjectsRemoved(removedObjects);
			this.OnCollectionChanged(null, removedObjects);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000115D0 File Offset: 0x0000F7D0
		private void OnCollectionChanged(IEnumerable<Note> addedNotes, IEnumerable<Note> removedNotes)
		{
			NotesCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, new NotesCollectionChangedEventArgs(addedNotes, removedNotes));
		}
	}
}
