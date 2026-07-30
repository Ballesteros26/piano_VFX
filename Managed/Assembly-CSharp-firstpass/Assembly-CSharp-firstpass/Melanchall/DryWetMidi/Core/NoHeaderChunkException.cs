using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000175 RID: 373
	public sealed class NoHeaderChunkException : MidiException
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x0002072D File Offset: 0x0001E92D
		internal NoHeaderChunkException()
			: base("MIDI file doesn't contain the header chunk.")
		{
		}
	}
}
