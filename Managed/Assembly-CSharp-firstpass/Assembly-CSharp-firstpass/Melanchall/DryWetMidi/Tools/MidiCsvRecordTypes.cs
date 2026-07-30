using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000028 RID: 40
	internal static class MidiCsvRecordTypes
	{
		// Token: 0x020001EA RID: 490
		public static class File
		{
			// Token: 0x04000B34 RID: 2868
			public const string Header = "Header";

			// Token: 0x04000B35 RID: 2869
			public const string TrackChunkStart = "Start_track";

			// Token: 0x04000B36 RID: 2870
			public const string TrackChunkEnd = "End_track";

			// Token: 0x04000B37 RID: 2871
			public const string FileEnd = "End_of_file";
		}

		// Token: 0x020001EB RID: 491
		public static class Events
		{
			// Token: 0x04000B38 RID: 2872
			public const string SequenceTrackName = "Title_t";

			// Token: 0x04000B39 RID: 2873
			public const string CopyrightNotice = "Copyright_t";

			// Token: 0x04000B3A RID: 2874
			public const string InstrumentName = "Instrument_name_t";

			// Token: 0x04000B3B RID: 2875
			public const string Marker = "Marker_t";

			// Token: 0x04000B3C RID: 2876
			public const string CuePoint = "Cue_point_t";

			// Token: 0x04000B3D RID: 2877
			public const string Lyric = "Lyric_t";

			// Token: 0x04000B3E RID: 2878
			public const string Text = "Text_t";

			// Token: 0x04000B3F RID: 2879
			public const string SequenceNumber = "Sequence_number";

			// Token: 0x04000B40 RID: 2880
			public const string PortPrefix = "MIDI_port";

			// Token: 0x04000B41 RID: 2881
			public const string ChannelPrefix = "Channel_prefix";

			// Token: 0x04000B42 RID: 2882
			public const string TimeSignature = "Time_signature";

			// Token: 0x04000B43 RID: 2883
			public const string KeySignature = "Key_signature";

			// Token: 0x04000B44 RID: 2884
			public const string SetTempo = "Tempo";

			// Token: 0x04000B45 RID: 2885
			public const string SmpteOffset = "SMPTE_offset";

			// Token: 0x04000B46 RID: 2886
			public const string SequencerSpecific = "Sequencer_specific";

			// Token: 0x04000B47 RID: 2887
			public const string UnknownMeta = "Unknown_meta_event";

			// Token: 0x04000B48 RID: 2888
			public const string NoteOn = "Note_on_c";

			// Token: 0x04000B49 RID: 2889
			public const string NoteOff = "Note_off_c";

			// Token: 0x04000B4A RID: 2890
			public const string PitchBend = "Pitch_bend_c";

			// Token: 0x04000B4B RID: 2891
			public const string ControlChange = "Control_c";

			// Token: 0x04000B4C RID: 2892
			public const string ProgramChange = "Program_c";

			// Token: 0x04000B4D RID: 2893
			public const string ChannelAftertouch = "Channel_aftertouch_c";

			// Token: 0x04000B4E RID: 2894
			public const string NoteAftertouch = "Poly_aftertouch_c";

			// Token: 0x04000B4F RID: 2895
			public const string SysExCompleted = "System_exclusive";

			// Token: 0x04000B50 RID: 2896
			public const string SysExIncompleted = "System_exclusive_packet";
		}
	}
}
