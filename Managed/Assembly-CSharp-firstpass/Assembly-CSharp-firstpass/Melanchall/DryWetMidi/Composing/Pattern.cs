using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B3 RID: 435
	public sealed class Pattern
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x00022D6D File Offset: 0x00020F6D
		internal Pattern(IEnumerable<PatternAction> actions)
		{
			this.Actions = actions;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00022D7C File Offset: 0x00020F7C
		internal IEnumerable<PatternAction> Actions { get; }

		// Token: 0x06000A68 RID: 2664 RVA: 0x00022D84 File Offset: 0x00020F84
		public TrackChunk ToTrackChunk(TempoMap tempoMap, FourBitNumber channel)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			PatternContext patternContext = new PatternContext(tempoMap, channel);
			PatternActionResult patternActionResult = this.InvokeActions(0L, patternContext);
			TrackChunk trackChunk = new TrackChunk();
			using (TimedEventsManager timedEventsManager = trackChunk.ManageTimedEvents(null))
			{
				timedEventsManager.Events.Add(patternActionResult.Events ?? Enumerable.Empty<TimedEvent>());
			}
			using (NotesManager notesManager = trackChunk.ManageNotes(null))
			{
				notesManager.Notes.Add(patternActionResult.Notes ?? Enumerable.Empty<Note>());
			}
			return trackChunk;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00022E34 File Offset: 0x00021034
		public TrackChunk ToTrackChunk(TempoMap tempoMap)
		{
			return this.ToTrackChunk(tempoMap, FourBitNumber.MinValue);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00022E44 File Offset: 0x00021044
		public MidiFile ToFile(TempoMap tempoMap, FourBitNumber channel)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			TrackChunk trackChunk = this.ToTrackChunk(tempoMap, channel);
			MidiFile midiFile = new MidiFile(new MidiChunk[] { trackChunk });
			midiFile.ReplaceTempoMap(tempoMap);
			return midiFile;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00022E7B File Offset: 0x0002107B
		public MidiFile ToFile(TempoMap tempoMap)
		{
			return this.ToFile(tempoMap, FourBitNumber.MinValue);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00022E89 File Offset: 0x00021089
		public Pattern Clone()
		{
			return new Pattern(this.Actions.Select((PatternAction a) => a.Clone()).ToList<PatternAction>());
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00022EC0 File Offset: 0x000210C0
		internal PatternActionResult InvokeActions(long time, PatternContext context)
		{
			List<Note> list = new List<Note>();
			List<TimedEvent> list2 = new List<TimedEvent>();
			foreach (PatternAction patternAction in this.Actions)
			{
				PatternActionResult patternActionResult = patternAction.Invoke(time, context);
				long? time2 = patternActionResult.Time;
				if (time2 != null)
				{
					time = time2.Value;
				}
				IEnumerable<Note> notes = patternActionResult.Notes;
				if (notes != null)
				{
					list.AddRange(notes);
				}
				IEnumerable<TimedEvent> events = patternActionResult.Events;
				if (events != null)
				{
					list2.AddRange(events);
				}
			}
			return new PatternActionResult(new long?(time), list, list2);
		}
	}
}
