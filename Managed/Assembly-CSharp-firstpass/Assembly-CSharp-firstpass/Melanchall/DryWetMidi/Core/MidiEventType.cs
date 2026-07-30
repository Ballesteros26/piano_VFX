using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200012C RID: 300
	public enum MidiEventType : byte
	{
		// Token: 0x04000855 RID: 2133
		NormalSysEx,
		// Token: 0x04000856 RID: 2134
		EscapeSysEx,
		// Token: 0x04000857 RID: 2135
		SequenceNumber,
		// Token: 0x04000858 RID: 2136
		Text,
		// Token: 0x04000859 RID: 2137
		CopyrightNotice,
		// Token: 0x0400085A RID: 2138
		SequenceTrackName,
		// Token: 0x0400085B RID: 2139
		InstrumentName,
		// Token: 0x0400085C RID: 2140
		Lyric,
		// Token: 0x0400085D RID: 2141
		Marker,
		// Token: 0x0400085E RID: 2142
		CuePoint,
		// Token: 0x0400085F RID: 2143
		ProgramName,
		// Token: 0x04000860 RID: 2144
		DeviceName,
		// Token: 0x04000861 RID: 2145
		ChannelPrefix,
		// Token: 0x04000862 RID: 2146
		PortPrefix,
		// Token: 0x04000863 RID: 2147
		EndOfTrack,
		// Token: 0x04000864 RID: 2148
		SetTempo,
		// Token: 0x04000865 RID: 2149
		SmpteOffset,
		// Token: 0x04000866 RID: 2150
		TimeSignature,
		// Token: 0x04000867 RID: 2151
		KeySignature,
		// Token: 0x04000868 RID: 2152
		SequencerSpecific,
		// Token: 0x04000869 RID: 2153
		UnknownMeta,
		// Token: 0x0400086A RID: 2154
		CustomMeta,
		// Token: 0x0400086B RID: 2155
		NoteOff,
		// Token: 0x0400086C RID: 2156
		NoteOn,
		// Token: 0x0400086D RID: 2157
		NoteAftertouch,
		// Token: 0x0400086E RID: 2158
		ControlChange,
		// Token: 0x0400086F RID: 2159
		ProgramChange,
		// Token: 0x04000870 RID: 2160
		ChannelAftertouch,
		// Token: 0x04000871 RID: 2161
		PitchBend,
		// Token: 0x04000872 RID: 2162
		TimingClock,
		// Token: 0x04000873 RID: 2163
		Start,
		// Token: 0x04000874 RID: 2164
		Continue,
		// Token: 0x04000875 RID: 2165
		Stop,
		// Token: 0x04000876 RID: 2166
		ActiveSensing,
		// Token: 0x04000877 RID: 2167
		Reset,
		// Token: 0x04000878 RID: 2168
		MidiTimeCode,
		// Token: 0x04000879 RID: 2169
		SongPositionPointer,
		// Token: 0x0400087A RID: 2170
		SongSelect,
		// Token: 0x0400087B RID: 2171
		TuneRequest
	}
}
