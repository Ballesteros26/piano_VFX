using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200019C RID: 412
	public static class MidiFileUtilities
	{
		// Token: 0x060009F4 RID: 2548 RVA: 0x00021F20 File Offset: 0x00020120
		public static IEnumerable<FourBitNumber> GetChannels(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return midiFile.GetTrackChunks().GetChannels();
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00021F38 File Offset: 0x00020138
		internal static IEnumerable<MidiEvent> GetEvents(this MidiFile midiFile)
		{
			return midiFile.GetTrackChunks().SelectMany((TrackChunk c) => c.Events);
		}
	}
}
