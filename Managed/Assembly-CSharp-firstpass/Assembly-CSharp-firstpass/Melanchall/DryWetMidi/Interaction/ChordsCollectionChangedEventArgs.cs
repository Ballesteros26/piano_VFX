using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x0200008F RID: 143
	public sealed class ChordsCollectionChangedEventArgs : EventArgs
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x000105BE File Offset: 0x0000E7BE
		public ChordsCollectionChangedEventArgs(IEnumerable<Chord> addedChords, IEnumerable<Chord> removedChords)
		{
			this.AddedChords = addedChords;
			this.RemovedChords = removedChords;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public IEnumerable<Chord> AddedChords { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000105DC File Offset: 0x0000E7DC
		public IEnumerable<Chord> RemovedChords { get; }
	}
}
