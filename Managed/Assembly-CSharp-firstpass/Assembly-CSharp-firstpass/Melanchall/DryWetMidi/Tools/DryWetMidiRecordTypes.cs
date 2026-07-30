using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000027 RID: 39
	internal static class DryWetMidiRecordTypes
	{
		// Token: 0x040000B3 RID: 179
		public const string Note = "Note";

		// Token: 0x020001E8 RID: 488
		public static class File
		{
			// Token: 0x04000B1A RID: 2842
			public const string Header = "Header";
		}

		// Token: 0x020001E9 RID: 489
		public static class Events
		{
			// Token: 0x04000B1B RID: 2843
			public const string SequenceTrackName = "Sequence/Track Name";

			// Token: 0x04000B1C RID: 2844
			public const string CopyrightNotice = "Copyright Notice";

			// Token: 0x04000B1D RID: 2845
			public const string InstrumentName = "Instrument Name";

			// Token: 0x04000B1E RID: 2846
			public const string Marker = "Marker";

			// Token: 0x04000B1F RID: 2847
			public const string CuePoint = "Cue Point";

			// Token: 0x04000B20 RID: 2848
			public const string Lyric = "Lyric";

			// Token: 0x04000B21 RID: 2849
			public const string Text = "Text";

			// Token: 0x04000B22 RID: 2850
			public const string SequenceNumber = "Sequence Number";

			// Token: 0x04000B23 RID: 2851
			public const string PortPrefix = "Port Prefix";

			// Token: 0x04000B24 RID: 2852
			public const string ChannelPrefix = "Channel Prefix";

			// Token: 0x04000B25 RID: 2853
			public const string TimeSignature = "Time Signature";

			// Token: 0x04000B26 RID: 2854
			public const string KeySignature = "Key Signature";

			// Token: 0x04000B27 RID: 2855
			public const string SetTempo = "Set Tempo";

			// Token: 0x04000B28 RID: 2856
			public const string SmpteOffset = "SMPTE Offset";

			// Token: 0x04000B29 RID: 2857
			public const string SequencerSpecific = "Sequencer Specific";

			// Token: 0x04000B2A RID: 2858
			public const string UnknownMeta = "Unknown Meta";

			// Token: 0x04000B2B RID: 2859
			public const string NoteOn = "Note On";

			// Token: 0x04000B2C RID: 2860
			public const string NoteOff = "Note Off";

			// Token: 0x04000B2D RID: 2861
			public const string PitchBend = "Pitch Bend";

			// Token: 0x04000B2E RID: 2862
			public const string ControlChange = "Control Change";

			// Token: 0x04000B2F RID: 2863
			public const string ProgramChange = "Program Change";

			// Token: 0x04000B30 RID: 2864
			public const string ChannelAftertouch = "Channel Aftertouch";

			// Token: 0x04000B31 RID: 2865
			public const string NoteAftertouch = "Note Aftertouch";

			// Token: 0x04000B32 RID: 2866
			public const string SysExCompleted = "System Exclusive";

			// Token: 0x04000B33 RID: 2867
			public const string SysExIncompleted = "System Exclusive Packet";
		}
	}
}
