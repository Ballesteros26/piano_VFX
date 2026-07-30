using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x0200010F RID: 271
	public static class RecordingUtilities
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x0001C7D9 File Offset: 0x0001A9D9
		public static TrackChunk ToTrackChunk(this Recording recording)
		{
			ThrowIfArgument.IsNull("recording", recording);
			if (recording.IsRunning)
			{
				throw new ArgumentException("Recording is in progress.", "recording");
			}
			return recording.GetEvents().ToTrackChunk();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001C80C File Offset: 0x0001AA0C
		public static MidiFile ToFile(this Recording recording)
		{
			ThrowIfArgument.IsNull("recording", recording);
			if (recording.IsRunning)
			{
				throw new ArgumentException("Recording is in progress.", "recording");
			}
			TrackChunk trackChunk = recording.ToTrackChunk();
			MidiFile midiFile = new MidiFile(new MidiChunk[] { trackChunk });
			midiFile.ReplaceTempoMap(recording.TempoMap);
			return midiFile;
		}
	}
}
