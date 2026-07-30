using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000039 RID: 57
	public static class MidiFileSplitter
	{
		// Token: 0x06000170 RID: 368 RVA: 0x000088E4 File Offset: 0x00006AE4
		public static IEnumerable<MidiFile> SplitByChannel(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			List<TimedEvent>[] array = FourBitNumber.Values.Select((FourBitNumber channel) => new List<TimedEvent>()).ToArray<List<TimedEvent>>();
			foreach (TimedEvent timedEvent in midiFile.GetTimedEvents())
			{
				ChannelEvent channelEvent = timedEvent.Event as ChannelEvent;
				if (channelEvent != null)
				{
					array[(int)channelEvent.Channel].Add(timedEvent.Clone());
				}
				else
				{
					List<TimedEvent>[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].Add(timedEvent.Clone());
					}
				}
			}
			return array.Where((List<TimedEvent> events) => events.Select((TimedEvent e) => e.Event).OfType<ChannelEvent>().Any<ChannelEvent>()).Select(delegate(List<TimedEvent> events)
			{
				MidiFile midiFile2 = events.ToFile();
				midiFile2.TimeDivision = midiFile.TimeDivision.Clone();
				return midiFile2;
			});
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008A04 File Offset: 0x00006C04
		public static IEnumerable<MidiFile> SplitByNotes(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			Dictionary<NoteId, List<TimedEvent>> dictionary = new HashSet<NoteId>(from e in (from e in midiFile.GetTimedEvents()
					select e.Event).OfType<NoteEvent>()
				select e.GetNoteId()).ToDictionary((NoteId id) => id, (NoteId id) => new List<TimedEvent>());
			foreach (TimedEvent timedEvent in midiFile.GetTimedEvents())
			{
				NoteEvent noteEvent = timedEvent.Event as NoteEvent;
				if (noteEvent != null)
				{
					dictionary[noteEvent.GetNoteId()].Add(timedEvent);
				}
				else
				{
					foreach (List<TimedEvent> list in dictionary.Values)
					{
						list.Add(timedEvent);
					}
				}
			}
			foreach (List<TimedEvent> list2 in dictionary.Values)
			{
				MidiFile midiFile2 = list2.ToFile();
				midiFile2.TimeDivision = midiFile.TimeDivision.Clone();
				yield return midiFile2;
			}
			Dictionary<NoteId, List<TimedEvent>>.ValueCollection.Enumerator enumerator3 = default(Dictionary<NoteId, List<TimedEvent>>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008A14 File Offset: 0x00006C14
		public static IEnumerable<MidiFile> SplitByGrid(this MidiFile midiFile, IGrid grid, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			if (!midiFile.GetEvents().Any<MidiEvent>())
			{
				yield break;
			}
			settings = settings ?? new SliceMidiFileSettings();
			midiFile = MidiFileSplitter.PrepareMidiFileForSlicing(midiFile, grid, settings);
			TempoMap tempoMap = midiFile.GetTempoMap();
			using (MidiFileSlicer slicer = MidiFileSlicer.CreateFromFile(midiFile))
			{
				foreach (long num in grid.GetTimes(tempoMap))
				{
					if (num != 0L)
					{
						yield return slicer.GetNextSlice(num, settings);
						if (slicer.AllEventsProcessed)
						{
							break;
						}
					}
				}
				IEnumerator<long> enumerator = null;
				if (!slicer.AllEventsProcessed)
				{
					yield return slicer.GetNextSlice(long.MaxValue, settings);
				}
			}
			MidiFileSlicer slicer = null;
			yield break;
			yield break;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008A34 File Offset: 0x00006C34
		public static MidiFile SkipPart(this MidiFile midiFile, ITimeSpan partLength, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("partLength", partLength);
			ArbitraryGrid arbitraryGrid = new ArbitraryGrid(new ITimeSpan[] { partLength });
			settings = settings ?? new SliceMidiFileSettings();
			midiFile = MidiFileSplitter.PrepareMidiFileForSlicing(midiFile, arbitraryGrid, settings);
			TempoMap tempoMap = midiFile.GetTempoMap();
			long num = arbitraryGrid.GetTimes(tempoMap).First<long>();
			MidiFile nextSlice;
			using (MidiFileSlicer midiFileSlicer = MidiFileSlicer.CreateFromFile(midiFile))
			{
				midiFileSlicer.GetNextSlice(num, settings);
				nextSlice = midiFileSlicer.GetNextSlice(long.MaxValue, settings);
			}
			return nextSlice;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008AD4 File Offset: 0x00006CD4
		public static MidiFile TakePart(this MidiFile midiFile, ITimeSpan partLength, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("partLength", partLength);
			ArbitraryGrid arbitraryGrid = new ArbitraryGrid(new ITimeSpan[] { partLength });
			settings = settings ?? new SliceMidiFileSettings();
			midiFile = MidiFileSplitter.PrepareMidiFileForSlicing(midiFile, arbitraryGrid, settings);
			TempoMap tempoMap = midiFile.GetTempoMap();
			long num = arbitraryGrid.GetTimes(tempoMap).First<long>();
			MidiFile nextSlice;
			using (MidiFileSlicer midiFileSlicer = MidiFileSlicer.CreateFromFile(midiFile))
			{
				nextSlice = midiFileSlicer.GetNextSlice(num, settings);
			}
			return nextSlice;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008B60 File Offset: 0x00006D60
		public static MidiFile TakePart(this MidiFile midiFile, ITimeSpan partStart, ITimeSpan partLength, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("partStart", partStart);
			ThrowIfArgument.IsNull("partLength", partLength);
			ArbitraryGrid arbitraryGrid = new ArbitraryGrid(new ITimeSpan[]
			{
				partStart,
				partStart.Add(partLength, TimeSpanMode.TimeLength)
			});
			settings = settings ?? new SliceMidiFileSettings();
			midiFile = MidiFileSplitter.PrepareMidiFileForSlicing(midiFile, arbitraryGrid, settings);
			TempoMap tempoMap = midiFile.GetTempoMap();
			long[] array = arbitraryGrid.GetTimes(tempoMap).ToArray<long>();
			MidiFile nextSlice;
			using (MidiFileSlicer midiFileSlicer = MidiFileSlicer.CreateFromFile(midiFile))
			{
				midiFileSlicer.GetNextSlice(array[0], settings);
				nextSlice = midiFileSlicer.GetNextSlice(array[1], settings);
			}
			return nextSlice;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008C10 File Offset: 0x00006E10
		private static MidiFile PrepareMidiFileForSlicing(MidiFile midiFile, IGrid grid, SliceMidiFileSettings settings)
		{
			if (settings.SplitNotes)
			{
				midiFile = midiFile.Clone();
				midiFile.SplitNotesByGrid(grid);
			}
			return midiFile;
		}
	}
}
