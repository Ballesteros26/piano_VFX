using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000120 RID: 288
	internal static class MidiChunkEquality
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x0001DA70 File Offset: 0x0001BC70
		public static bool Equals(MidiChunk midiChunk1, MidiChunk midiChunk2, MidiChunkEqualityCheckSettings settings, out string message)
		{
			message = null;
			if (midiChunk1 == midiChunk2)
			{
				return true;
			}
			if (midiChunk1 == null || midiChunk2 == null)
			{
				message = "One of chunks is null.";
				return false;
			}
			Type type = midiChunk1.GetType();
			Type type2 = midiChunk2.GetType();
			if (type != type2)
			{
				message = string.Format("Types of chunks are different ({0} vs {1}).", type, type2);
				return false;
			}
			TrackChunk trackChunk = midiChunk1 as TrackChunk;
			if (trackChunk != null)
			{
				TrackChunk trackChunk2 = (TrackChunk)midiChunk2;
				EventsCollection events = trackChunk.Events;
				EventsCollection events2 = trackChunk2.Events;
				if (events.Count != events2.Count)
				{
					message = string.Format("Counts of events in track chunks are different ({0} vs {1}).", events.Count, events2.Count);
					return false;
				}
				for (int i = 0; i < events.Count; i++)
				{
					MidiEvent midiEvent = events[i];
					MidiEvent midiEvent2 = events2[i];
					string text;
					if (!MidiEvent.Equals(midiEvent, midiEvent2, settings.EventEqualityCheckSettings, out text))
					{
						message = string.Format("Events at position {0} in track chunks are different. {1}", i, text);
						return false;
					}
				}
				return true;
			}
			else
			{
				UnknownChunk unknownChunk = midiChunk1 as UnknownChunk;
				if (unknownChunk == null)
				{
					return midiChunk1.Equals(midiChunk2);
				}
				UnknownChunk unknownChunk2 = (UnknownChunk)midiChunk2;
				string chunkId = unknownChunk.ChunkId;
				string chunkId2 = unknownChunk2.ChunkId;
				if (chunkId != chunkId2)
				{
					message = string.Concat(new string[] { "IDs of unknown chunks are different (", chunkId, " vs ", chunkId2, ")." });
					return false;
				}
				if (!ArrayUtilities.Equals<byte>(unknownChunk.Data, unknownChunk2.Data))
				{
					message = "Unknown chunks data are different.";
					return false;
				}
				return true;
			}
		}
	}
}
