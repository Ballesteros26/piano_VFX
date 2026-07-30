using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200013A RID: 314
	internal static class EventStatusBytes
	{
		// Token: 0x02000293 RID: 659
		internal static class Global
		{
			// Token: 0x04000DB5 RID: 3509
			public const byte Meta = 255;

			// Token: 0x04000DB6 RID: 3510
			public const byte NormalSysEx = 240;

			// Token: 0x04000DB7 RID: 3511
			public const byte EscapeSysEx = 247;
		}

		// Token: 0x02000294 RID: 660
		internal static class Meta
		{
			// Token: 0x04000DB8 RID: 3512
			public const byte SequenceNumber = 0;

			// Token: 0x04000DB9 RID: 3513
			public const byte Text = 1;

			// Token: 0x04000DBA RID: 3514
			public const byte CopyrightNotice = 2;

			// Token: 0x04000DBB RID: 3515
			public const byte SequenceTrackName = 3;

			// Token: 0x04000DBC RID: 3516
			public const byte InstrumentName = 4;

			// Token: 0x04000DBD RID: 3517
			public const byte Lyric = 5;

			// Token: 0x04000DBE RID: 3518
			public const byte Marker = 6;

			// Token: 0x04000DBF RID: 3519
			public const byte CuePoint = 7;

			// Token: 0x04000DC0 RID: 3520
			public const byte ProgramName = 8;

			// Token: 0x04000DC1 RID: 3521
			public const byte DeviceName = 9;

			// Token: 0x04000DC2 RID: 3522
			public const byte ChannelPrefix = 32;

			// Token: 0x04000DC3 RID: 3523
			public const byte PortPrefix = 33;

			// Token: 0x04000DC4 RID: 3524
			public const byte EndOfTrack = 47;

			// Token: 0x04000DC5 RID: 3525
			public const byte SetTempo = 81;

			// Token: 0x04000DC6 RID: 3526
			public const byte SmpteOffset = 84;

			// Token: 0x04000DC7 RID: 3527
			public const byte TimeSignature = 88;

			// Token: 0x04000DC8 RID: 3528
			public const byte KeySignature = 89;

			// Token: 0x04000DC9 RID: 3529
			public const byte SequencerSpecific = 127;
		}

		// Token: 0x02000295 RID: 661
		internal static class Channel
		{
			// Token: 0x04000DCA RID: 3530
			public const byte NoteOff = 8;

			// Token: 0x04000DCB RID: 3531
			public const byte NoteOn = 9;

			// Token: 0x04000DCC RID: 3532
			public const byte NoteAftertouch = 10;

			// Token: 0x04000DCD RID: 3533
			public const byte ControlChange = 11;

			// Token: 0x04000DCE RID: 3534
			public const byte ProgramChange = 12;

			// Token: 0x04000DCF RID: 3535
			public const byte ChannelAftertouch = 13;

			// Token: 0x04000DD0 RID: 3536
			public const byte PitchBend = 14;
		}

		// Token: 0x02000296 RID: 662
		internal static class SystemRealTime
		{
			// Token: 0x04000DD1 RID: 3537
			public const byte TimingClock = 248;

			// Token: 0x04000DD2 RID: 3538
			public const byte Start = 250;

			// Token: 0x04000DD3 RID: 3539
			public const byte Continue = 251;

			// Token: 0x04000DD4 RID: 3540
			public const byte Stop = 252;

			// Token: 0x04000DD5 RID: 3541
			public const byte ActiveSensing = 254;

			// Token: 0x04000DD6 RID: 3542
			public const byte Reset = 255;
		}

		// Token: 0x02000297 RID: 663
		internal static class SystemCommon
		{
			// Token: 0x04000DD7 RID: 3543
			public const byte MtcQuarterFrame = 241;

			// Token: 0x04000DD8 RID: 3544
			public const byte SongPositionPointer = 242;

			// Token: 0x04000DD9 RID: 3545
			public const byte SongSelect = 243;

			// Token: 0x04000DDA RID: 3546
			public const byte TuneRequest = 246;
		}
	}
}
