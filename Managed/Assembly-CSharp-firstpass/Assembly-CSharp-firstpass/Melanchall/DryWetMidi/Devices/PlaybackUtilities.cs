using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Standards;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000108 RID: 264
	public static class PlaybackUtilities
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001BA72 File Offset: 0x00019C72
		public static Playback GetPlayback(this TrackChunk trackChunk, TempoMap tempoMap, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return new Playback(trackChunk.Events, tempoMap, outputDevice, clockSettings);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001BAA3 File Offset: 0x00019CA3
		public static Playback GetPlayback(this TrackChunk trackChunk, TempoMap tempoMap, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return new Playback(trackChunk.Events, tempoMap, clockSettings);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		public static Playback GetPlayback(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return new Playback(trackChunks.Select((TrackChunk c) => c.Events), tempoMap, outputDevice, clockSettings);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001BB24 File Offset: 0x00019D24
		public static Playback GetPlayback(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return new Playback(trackChunks.Select((TrackChunk c) => c.Events), tempoMap, clockSettings);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001BB73 File Offset: 0x00019D73
		public static Playback GetPlayback(this MidiFile midiFile, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return midiFile.GetTrackChunks().GetPlayback(midiFile.GetTempoMap(), outputDevice, clockSettings);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001BB9E File Offset: 0x00019D9E
		public static Playback GetPlayback(this MidiFile midiFile, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return midiFile.GetTrackChunks().GetPlayback(midiFile.GetTempoMap(), clockSettings);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001BBBD File Offset: 0x00019DBD
		public static Playback GetPlayback(this Pattern pattern, TempoMap tempoMap, FourBitNumber channel, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return pattern.ToTrackChunk(tempoMap, channel).GetPlayback(tempoMap, outputDevice, clockSettings);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001BBF1 File Offset: 0x00019DF1
		public static Playback GetPlayback(this Pattern pattern, TempoMap tempoMap, FourBitNumber channel, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return pattern.ToTrackChunk(tempoMap, channel).GetPlayback(tempoMap, clockSettings);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001BC18 File Offset: 0x00019E18
		public static Playback GetPlayback<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, SevenBitNumber programNumber, MidiClockSettings clockSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return PlaybackUtilities.GetMusicalObjectsPlayback<TObject>(objects, tempoMap, outputDevice, (FourBitNumber channel) => new ProgramChangeEvent[]
			{
				new ProgramChangeEvent(programNumber)
				{
					Channel = channel
				}
			}, clockSettings);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001BC6C File Offset: 0x00019E6C
		public static Playback GetPlayback<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, GeneralMidiProgram generalMidiProgram, MidiClockSettings clockSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidiProgram>("generalMidiProgram", generalMidiProgram);
			return PlaybackUtilities.GetMusicalObjectsPlayback<TObject>(objects, tempoMap, outputDevice, (FourBitNumber channel) => new MidiEvent[] { generalMidiProgram.GetProgramEvent(channel) }, clockSettings);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		public static Playback GetPlayback<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, GeneralMidi2Program generalMidi2Program, MidiClockSettings clockSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2Program>("generalMidi2Program", generalMidi2Program);
			return PlaybackUtilities.GetMusicalObjectsPlayback<TObject>(objects, tempoMap, outputDevice, (FourBitNumber channel) => generalMidi2Program.GetProgramEvents(channel), clockSettings);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001BD34 File Offset: 0x00019F34
		public static void Play(this TrackChunk trackChunk, TempoMap tempoMap, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			using (Playback playback = trackChunk.GetPlayback(tempoMap, outputDevice, clockSettings))
			{
				playback.Play();
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001BD90 File Offset: 0x00019F90
		public static void Play(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			using (Playback playback = trackChunks.GetPlayback(tempoMap, outputDevice, clockSettings))
			{
				playback.Play();
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001BDEC File Offset: 0x00019FEC
		public static void Play(this MidiFile midiFile, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			midiFile.GetTrackChunks().Play(midiFile.GetTempoMap(), outputDevice, clockSettings);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001BE17 File Offset: 0x0001A017
		public static void Play(this Pattern pattern, TempoMap tempoMap, FourBitNumber channel, IOutputDevice outputDevice, MidiClockSettings clockSettings = null)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			pattern.ToTrackChunk(tempoMap, channel).Play(tempoMap, outputDevice, clockSettings);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001BE4C File Offset: 0x0001A04C
		public static void Play<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, SevenBitNumber programNumber, MidiClockSettings clockSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			using (Playback playback = objects.GetPlayback(tempoMap, outputDevice, programNumber, clockSettings))
			{
				playback.Play();
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		public static void Play<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, GeneralMidiProgram generalMidiProgram, MidiClockSettings clockSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidiProgram>("generalMidiProgram", generalMidiProgram);
			using (Playback playback = objects.GetPlayback(tempoMap, outputDevice, generalMidiProgram, clockSettings))
			{
				playback.Play();
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001BF10 File Offset: 0x0001A110
		public static void Play<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, GeneralMidi2Program generalMidi2Program, MidiClockSettings clockSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2Program>("generalMidi2Program", generalMidi2Program);
			using (Playback playback = objects.GetPlayback(tempoMap, outputDevice, generalMidi2Program, clockSettings))
			{
				playback.Play();
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001BF78 File Offset: 0x0001A178
		private static Playback GetMusicalObjectsPlayback<TObject>(IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, Func<FourBitNumber, IEnumerable<MidiEvent>> programChangeEventsGetter, MidiClockSettings clockSettings) where TObject : IMusicalObject, ITimedObject
		{
			return new Playback((from e in objects.Select((TObject n) => n.Channel).Distinct<FourBitNumber>().SelectMany(programChangeEventsGetter)
				select new TimedEvent(e)).Concat((IEnumerable<ITimedObject>)objects), tempoMap, outputDevice, clockSettings);
		}
	}
}
