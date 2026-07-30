using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x0200008E RID: 142
	public sealed class ChordsCollection : TimedObjectsCollection<Chord>
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002EE RID: 750 RVA: 0x0001050C File Offset: 0x0000E70C
		// (remove) Token: 0x060002EF RID: 751 RVA: 0x00010544 File Offset: 0x0000E744
		public event ChordsCollectionChangedEventHandler CollectionChanged;

		// Token: 0x060002F0 RID: 752 RVA: 0x00010579 File Offset: 0x0000E779
		internal ChordsCollection(IEnumerable<Chord> chords)
			: base(chords)
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00010582 File Offset: 0x0000E782
		protected override void OnObjectsAdded(IEnumerable<Chord> addedObjects)
		{
			base.OnObjectsAdded(addedObjects);
			this.OnCollectionChanged(addedObjects, null);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00010593 File Offset: 0x0000E793
		protected override void OnObjectsRemoved(IEnumerable<Chord> removedObjects)
		{
			base.OnObjectsRemoved(removedObjects);
			this.OnCollectionChanged(null, removedObjects);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000105A4 File Offset: 0x0000E7A4
		private void OnCollectionChanged(IEnumerable<Chord> addedChords, IEnumerable<Chord> removedChords)
		{
			ChordsCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, new ChordsCollectionChangedEventArgs(addedChords, removedChords));
		}
	}
}
