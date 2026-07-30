using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000119 RID: 281
	internal static class ChunksConverterFactory
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x0001D220 File Offset: 0x0001B420
		public static IChunksConverter GetConverter(MidiFileFormat format)
		{
			ThrowIfArgument.IsInvalidEnumValue<MidiFileFormat>("format", format);
			switch (format)
			{
			case MidiFileFormat.SingleTrack:
				return new SingleTrackChunksConverter();
			case MidiFileFormat.MultiTrack:
				return new MultiTrackChunksConverter();
			case MidiFileFormat.MultiSequence:
				return new MultiSequenceChunksConverter();
			default:
				throw new NotSupportedException(string.Format("Converter for the {0} format is not supported.", format));
			}
		}
	}
}
