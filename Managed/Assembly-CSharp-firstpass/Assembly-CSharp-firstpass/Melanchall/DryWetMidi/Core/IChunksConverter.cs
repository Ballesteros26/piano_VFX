using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200011A RID: 282
	internal interface IChunksConverter
	{
		// Token: 0x06000771 RID: 1905
		IEnumerable<MidiChunk> Convert(IEnumerable<MidiChunk> chunks);
	}
}
