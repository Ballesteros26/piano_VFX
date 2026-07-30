using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Standards;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B7 RID: 439
	public sealed class PatternBuilder
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x00022FDC File Offset: 0x000211DC
		public PatternBuilder()
		{
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002303C File Offset: 0x0002123C
		public PatternBuilder(Pattern pattern)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			this.ReplayPattern(pattern);
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x000230AF File Offset: 0x000212AF
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x000230B7 File Offset: 0x000212B7
		public SevenBitNumber Velocity { get; private set; } = PatternBuilder.DefaultVelocity;

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x000230C0 File Offset: 0x000212C0
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x000230C8 File Offset: 0x000212C8
		public ITimeSpan NoteLength { get; private set; } = PatternBuilder.DefaultNoteLength;

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000230D1 File Offset: 0x000212D1
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x000230D9 File Offset: 0x000212D9
		public ITimeSpan Step { get; private set; } = PatternBuilder.DefaultStep;

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000230E2 File Offset: 0x000212E2
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x000230EA File Offset: 0x000212EA
		public Octave Octave { get; private set; } = PatternBuilder.DefaultOctave;

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x000230F3 File Offset: 0x000212F3
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x000230FB File Offset: 0x000212FB
		public Melanchall.DryWetMidi.MusicTheory.Note RootNote { get; private set; } = PatternBuilder.DefaultRootNote;

		// Token: 0x06000A88 RID: 2696 RVA: 0x00023104 File Offset: 0x00021304
		public PatternBuilder Note(Interval interval)
		{
			return this.Note(interval, this.NoteLength, this.Velocity);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00023119 File Offset: 0x00021319
		public PatternBuilder Note(Interval interval, ITimeSpan length)
		{
			return this.Note(interval, length, this.Velocity);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00023129 File Offset: 0x00021329
		public PatternBuilder Note(Interval interval, SevenBitNumber velocity)
		{
			return this.Note(interval, this.NoteLength, velocity);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00023139 File Offset: 0x00021339
		public PatternBuilder Note(Interval interval, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return this.Note(this.RootNote.Transpose(interval), length, velocity);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002315A File Offset: 0x0002135A
		public PatternBuilder Note(NoteName noteName)
		{
			return this.Note(noteName, this.NoteLength, this.Velocity);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002316F File Offset: 0x0002136F
		public PatternBuilder Note(NoteName noteName, ITimeSpan length)
		{
			return this.Note(noteName, length, this.Velocity);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002317F File Offset: 0x0002137F
		public PatternBuilder Note(NoteName noteName, SevenBitNumber velocity)
		{
			return this.Note(noteName, this.NoteLength, velocity);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002318F File Offset: 0x0002138F
		public PatternBuilder Note(NoteName noteName, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsInvalidEnumValue<NoteName>("noteName", noteName);
			return this.Note(this.Octave.GetNote(noteName), length, velocity);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000231B0 File Offset: 0x000213B0
		public PatternBuilder Note(Melanchall.DryWetMidi.MusicTheory.Note note)
		{
			return this.Note(note, this.NoteLength, this.Velocity);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000231C5 File Offset: 0x000213C5
		public PatternBuilder Note(Melanchall.DryWetMidi.MusicTheory.Note note, ITimeSpan length)
		{
			return this.Note(note, length, this.Velocity);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000231D5 File Offset: 0x000213D5
		public PatternBuilder Note(Melanchall.DryWetMidi.MusicTheory.Note note, SevenBitNumber velocity)
		{
			return this.Note(note, this.NoteLength, velocity);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000231E5 File Offset: 0x000213E5
		public PatternBuilder Note(Melanchall.DryWetMidi.MusicTheory.Note note, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("note", note);
			ThrowIfArgument.IsNull("length", length);
			return this.AddAction(new AddNoteAction(new NoteDescriptor(note, velocity, length)));
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00023210 File Offset: 0x00021410
		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return this.Chord(chord.ResolveNotes(this.Octave), this.NoteLength, this.Velocity);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002323B File Offset: 0x0002143B
		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, Octave octave)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			return this.Chord(chord.ResolveNotes(octave), this.NoteLength, this.Velocity);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002326C File Offset: 0x0002146C
		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("length", length);
			return this.Chord(chord.ResolveNotes(this.Octave), length, this.Velocity);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002329D File Offset: 0x0002149D
		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, Octave octave, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			ThrowIfArgument.IsNull("length", length);
			return this.Chord(chord.ResolveNotes(octave), length, this.Velocity);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000232D4 File Offset: 0x000214D4
		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return this.Chord(chord.ResolveNotes(this.Octave), this.NoteLength, velocity);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000232FA File Offset: 0x000214FA
		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, Octave octave, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			return this.Chord(chord.ResolveNotes(octave), this.NoteLength, velocity);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00023326 File Offset: 0x00021526
		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("length", length);
			return this.Chord(chord.ResolveNotes(this.Octave), length, velocity);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00023352 File Offset: 0x00021552
		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, Octave octave, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			ThrowIfArgument.IsNull("length", length);
			return this.Chord(chord.ResolveNotes(octave), length, velocity);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00023385 File Offset: 0x00021585
		public PatternBuilder Chord(IEnumerable<Interval> intervals, NoteName rootNoteName)
		{
			return this.Chord(intervals, rootNoteName, this.NoteLength, this.Velocity);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002339B File Offset: 0x0002159B
		public PatternBuilder Chord(IEnumerable<Interval> intervals, NoteName rootNoteName, ITimeSpan length)
		{
			return this.Chord(intervals, rootNoteName, length, this.Velocity);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000233AC File Offset: 0x000215AC
		public PatternBuilder Chord(IEnumerable<Interval> intervals, NoteName rootNoteName, SevenBitNumber velocity)
		{
			return this.Chord(intervals, rootNoteName, this.NoteLength, velocity);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000233BD File Offset: 0x000215BD
		public PatternBuilder Chord(IEnumerable<Interval> intervals, NoteName rootNoteName, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsInvalidEnumValue<NoteName>("rootNoteName", rootNoteName);
			return this.Chord(intervals, this.Octave.GetNote(rootNoteName), length, velocity);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000233E0 File Offset: 0x000215E0
		public PatternBuilder Chord(IEnumerable<Interval> intervals, Melanchall.DryWetMidi.MusicTheory.Note rootNote)
		{
			return this.Chord(intervals, rootNote, this.NoteLength, this.Velocity);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000233F6 File Offset: 0x000215F6
		public PatternBuilder Chord(IEnumerable<Interval> interval, Melanchall.DryWetMidi.MusicTheory.Note rootNote, ITimeSpan length)
		{
			return this.Chord(interval, rootNote, length, this.Velocity);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00023407 File Offset: 0x00021607
		public PatternBuilder Chord(IEnumerable<Interval> intervals, Melanchall.DryWetMidi.MusicTheory.Note rootNote, SevenBitNumber velocity)
		{
			return this.Chord(intervals, rootNote, this.NoteLength, velocity);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00023418 File Offset: 0x00021618
		public PatternBuilder Chord(IEnumerable<Interval> intervals, Melanchall.DryWetMidi.MusicTheory.Note rootNote, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("intervals", intervals);
			ThrowIfArgument.IsNull("rootNote", rootNote);
			return this.Chord(new Melanchall.DryWetMidi.MusicTheory.Note[] { rootNote }.Concat(intervals.Where((Interval i) => i != null).Select(new Func<Interval, Melanchall.DryWetMidi.MusicTheory.Note>(rootNote.Transpose))), length, velocity);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00023489 File Offset: 0x00021689
		public PatternBuilder Chord(IEnumerable<NoteName> noteNames)
		{
			return this.Chord(noteNames, this.NoteLength, this.Velocity);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002349E File Offset: 0x0002169E
		public PatternBuilder Chord(IEnumerable<NoteName> noteNames, ITimeSpan length)
		{
			return this.Chord(noteNames, length, this.Velocity);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000234AE File Offset: 0x000216AE
		public PatternBuilder Chord(IEnumerable<NoteName> noteNames, SevenBitNumber velocity)
		{
			return this.Chord(noteNames, this.NoteLength, velocity);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000234BE File Offset: 0x000216BE
		public PatternBuilder Chord(IEnumerable<NoteName> noteNames, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("noteNames", noteNames);
			ThrowIfArgument.IsNull("length", length);
			return this.Chord(noteNames.Select((NoteName n) => this.Octave.GetNote(n)), length, velocity);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000234F0 File Offset: 0x000216F0
		public PatternBuilder Chord(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes)
		{
			return this.Chord(notes, this.NoteLength, this.Velocity);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00023505 File Offset: 0x00021705
		public PatternBuilder Chord(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes, ITimeSpan length)
		{
			return this.Chord(notes, length, this.Velocity);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00023515 File Offset: 0x00021715
		public PatternBuilder Chord(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes, SevenBitNumber velocity)
		{
			return this.Chord(notes, this.NoteLength, velocity);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00023525 File Offset: 0x00021725
		public PatternBuilder Chord(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("length", length);
			return this.AddAction(new AddChordAction(new ChordDescriptor(notes, velocity, length)));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00023550 File Offset: 0x00021750
		public PatternBuilder Pattern(Pattern pattern)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			return this.AddAction(new AddPatternAction(pattern));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00023569 File Offset: 0x00021769
		public PatternBuilder Anchor(object anchor)
		{
			ThrowIfArgument.IsNull("anchor", anchor);
			return this.AddAction(new AddAnchorAction(anchor));
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00023582 File Offset: 0x00021782
		public PatternBuilder Anchor()
		{
			return this.AddAction(new AddAnchorAction());
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002358F File Offset: 0x0002178F
		public PatternBuilder MoveToFirstAnchor(object anchor)
		{
			ThrowIfArgument.IsNull("anchor", anchor);
			if (this.GetAnchorCounter(anchor) < 1)
			{
				throw new ArgumentException(string.Format("There are no anchors with the '{0}' key.", anchor), "anchor");
			}
			return this.AddAction(new MoveToAnchorAction(anchor, AnchorPosition.First));
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000235C9 File Offset: 0x000217C9
		public PatternBuilder MoveToFirstAnchor()
		{
			if (this.GetAnchorCounter(null) < 1)
			{
				throw new InvalidOperationException("There are no anchors.");
			}
			return this.AddAction(new MoveToAnchorAction(AnchorPosition.First));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000235EC File Offset: 0x000217EC
		public PatternBuilder MoveToLastAnchor(object anchor)
		{
			ThrowIfArgument.IsNull("anchor", anchor);
			if (this.GetAnchorCounter(anchor) < 1)
			{
				throw new ArgumentException(string.Format("There are no anchors with the '{0}' key.", anchor), "anchor");
			}
			return this.AddAction(new MoveToAnchorAction(anchor, AnchorPosition.Last));
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00023626 File Offset: 0x00021826
		public PatternBuilder MoveToLastAnchor()
		{
			if (this.GetAnchorCounter(null) < 1)
			{
				throw new InvalidOperationException("There are no anchors.");
			}
			return this.AddAction(new MoveToAnchorAction(AnchorPosition.Last));
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002364C File Offset: 0x0002184C
		public PatternBuilder MoveToNthAnchor(object anchor, int index)
		{
			ThrowIfArgument.IsNull("anchor", anchor);
			int anchorCounter = this.GetAnchorCounter(anchor);
			ThrowIfArgument.IsOutOfRange("index", index, 0, anchorCounter - 1, "Index is out of range.");
			return this.AddAction(new MoveToAnchorAction(anchor, AnchorPosition.Nth, index));
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00023690 File Offset: 0x00021890
		public PatternBuilder MoveToNthAnchor(int index)
		{
			int anchorCounter = this.GetAnchorCounter(null);
			ThrowIfArgument.IsOutOfRange("index", index, 0, anchorCounter - 1, "Index is out of range.");
			return this.AddAction(new MoveToAnchorAction(AnchorPosition.Nth, index));
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000236C6 File Offset: 0x000218C6
		public PatternBuilder StepForward(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			return this.AddAction(new StepForwardAction(step));
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000236DF File Offset: 0x000218DF
		public PatternBuilder StepForward()
		{
			return this.AddAction(new StepForwardAction(this.Step));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000236F2 File Offset: 0x000218F2
		public PatternBuilder StepBack(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			return this.AddAction(new StepBackAction(step));
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002370B File Offset: 0x0002190B
		public PatternBuilder StepBack()
		{
			return this.AddAction(new StepBackAction(this.Step));
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002371E File Offset: 0x0002191E
		public PatternBuilder MoveToTime(ITimeSpan time)
		{
			ThrowIfArgument.IsNull("time", time);
			return this.AddAction(new MoveToTimeAction(time));
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00023737 File Offset: 0x00021937
		public PatternBuilder MoveToPreviousTime()
		{
			return this.AddAction(new MoveToTimeAction());
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00023744 File Offset: 0x00021944
		public PatternBuilder Repeat(int actionsCount, int repetitionsCount)
		{
			ThrowIfArgument.IsNegative("actionsCount", actionsCount, "Actions count is negative.");
			ThrowIfArgument.IsGreaterThan("actionsCount", actionsCount, this._actions.Count, "Actions count is greater than existing actions count.");
			ThrowIfArgument.IsNegative("repetitionsCount", repetitionsCount, "Repetitions count is negative.");
			return this.RepeatActions(actionsCount, repetitionsCount);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00023794 File Offset: 0x00021994
		public PatternBuilder Repeat(int repetitionsCount)
		{
			ThrowIfArgument.IsNegative("repetitionsCount", repetitionsCount, "Repetitions count is negative.");
			if (!this._actions.Any<PatternAction>())
			{
				throw new InvalidOperationException("There is no action to repeat.");
			}
			return this.RepeatActions(1, repetitionsCount);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000237C6 File Offset: 0x000219C6
		public PatternBuilder Repeat()
		{
			if (!this._actions.Any<PatternAction>())
			{
				throw new InvalidOperationException("There is no action to repeat.");
			}
			return this.RepeatActions(1, 1);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000237E8 File Offset: 0x000219E8
		public PatternBuilder Lyrics(string text)
		{
			ThrowIfArgument.IsNull("text", text);
			return this.AddAction(new AddTextEventAction<LyricEvent>(text));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00023801 File Offset: 0x00021A01
		public PatternBuilder Marker(string marker)
		{
			ThrowIfArgument.IsNull("marker", marker);
			return this.AddAction(new AddTextEventAction<MarkerEvent>(marker));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002381A File Offset: 0x00021A1A
		public PatternBuilder ProgramChange(SevenBitNumber programNumber)
		{
			return this.AddAction(new SetProgramNumberAction(programNumber));
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00023828 File Offset: 0x00021A28
		public PatternBuilder ProgramChange(GeneralMidiProgram program)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidiProgram>("program", program);
			return this.AddAction(new SetGeneralMidiProgramAction(program));
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00023841 File Offset: 0x00021A41
		public PatternBuilder ProgramChange(GeneralMidi2Program program)
		{
			ThrowIfArgument.IsInvalidEnumValue<GeneralMidi2Program>("program", program);
			return this.AddAction(new SetGeneralMidi2ProgramAction(program));
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002385A File Offset: 0x00021A5A
		public PatternBuilder SetRootNote(Melanchall.DryWetMidi.MusicTheory.Note rootNote)
		{
			ThrowIfArgument.IsNull("rootNote", rootNote);
			this.RootNote = rootNote;
			return this;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002386F File Offset: 0x00021A6F
		public PatternBuilder SetVelocity(SevenBitNumber velocity)
		{
			this.Velocity = velocity;
			return this;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00023879 File Offset: 0x00021A79
		public PatternBuilder SetNoteLength(ITimeSpan length)
		{
			ThrowIfArgument.IsNull("length", length);
			this.NoteLength = length;
			return this;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002388E File Offset: 0x00021A8E
		public PatternBuilder SetStep(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			this.Step = step;
			return this;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000238A3 File Offset: 0x00021AA3
		public PatternBuilder SetOctave(Octave octave)
		{
			ThrowIfArgument.IsNull("octave", octave);
			this.Octave = octave;
			return this;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000238B8 File Offset: 0x00021AB8
		public Pattern Build()
		{
			return new Pattern(this._actions.ToList<PatternAction>());
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000238CC File Offset: 0x00021ACC
		public PatternBuilder ReplayPattern(Pattern pattern)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			foreach (PatternAction patternAction in pattern.Actions)
			{
				this.AddAction(patternAction);
			}
			return this;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00023928 File Offset: 0x00021B28
		private PatternBuilder AddAction(PatternAction patternAction)
		{
			AddAnchorAction addAnchorAction = patternAction as AddAnchorAction;
			if (addAnchorAction != null)
			{
				this.UpdateAnchorsCounters(addAnchorAction.Anchor);
			}
			this._actions.Add(patternAction);
			return this;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00023958 File Offset: 0x00021B58
		private int GetAnchorCounter(object anchor)
		{
			if (anchor == null)
			{
				return this._globalAnchorsCounter;
			}
			int num;
			if (!this._anchorCounters.TryGetValue(anchor, out num))
			{
				throw new ArgumentException(string.Format("Anchor {0} doesn't exist.", anchor), "anchor");
			}
			return num;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00023998 File Offset: 0x00021B98
		private void UpdateAnchorsCounters(object anchor)
		{
			this._globalAnchorsCounter++;
			if (anchor == null)
			{
				return;
			}
			if (!this._anchorCounters.ContainsKey(anchor))
			{
				this._anchorCounters.Add(anchor, 0);
			}
			Dictionary<object, int> anchorCounters = this._anchorCounters;
			int num = anchorCounters[anchor];
			anchorCounters[anchor] = num + 1;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000239EC File Offset: 0x00021BEC
		private PatternBuilder RepeatActions(int actionsCount, int repetitionsCount)
		{
			List<PatternAction> actionsToRepeat = this._actions.Skip(this._actions.Count - actionsCount).ToList<PatternAction>();
			foreach (PatternAction patternAction in Enumerable.Range(0, repetitionsCount).SelectMany((int i) => actionsToRepeat))
			{
				this.AddAction(patternAction);
			}
			return this;
		}

		// Token: 0x04000990 RID: 2448
		public static readonly SevenBitNumber DefaultVelocity = Melanchall.DryWetMidi.Interaction.Note.DefaultVelocity;

		// Token: 0x04000991 RID: 2449
		public static readonly ITimeSpan DefaultNoteLength = MusicalTimeSpan.Quarter;

		// Token: 0x04000992 RID: 2450
		public static readonly ITimeSpan DefaultStep = MusicalTimeSpan.Quarter;

		// Token: 0x04000993 RID: 2451
		public static readonly Octave DefaultOctave = Octave.Middle;

		// Token: 0x04000994 RID: 2452
		public static readonly Melanchall.DryWetMidi.MusicTheory.Note DefaultRootNote = Octave.Middle.C;

		// Token: 0x04000995 RID: 2453
		private readonly List<PatternAction> _actions = new List<PatternAction>();

		// Token: 0x04000996 RID: 2454
		private readonly Dictionary<object, int> _anchorCounters = new Dictionary<object, int>();

		// Token: 0x04000997 RID: 2455
		private int _globalAnchorsCounter;
	}
}
