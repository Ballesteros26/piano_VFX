using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B9 RID: 441
	public static class PatternUtilities
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x00023BAF File Offset: 0x00021DAF
		public static Pattern TransformNotes(this Pattern pattern, NoteTransformation noteTransformation, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("noteTransformation", noteTransformation);
			return pattern.TransformNotes(PatternUtilities.AllNotesSelection, noteTransformation, recursive);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00023BD4 File Offset: 0x00021DD4
		public static Pattern TransformNotes(this Pattern pattern, NoteSelection noteSelection, NoteTransformation noteTransformation, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("noteSelection", noteSelection);
			ThrowIfArgument.IsNull("noteTransformation", noteTransformation);
			ObjectWrapper<int> objectWrapper = new ObjectWrapper<int>();
			return PatternUtilities.TransformNotes(pattern, objectWrapper, noteSelection, noteTransformation, recursive);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00023C12 File Offset: 0x00021E12
		public static Pattern TransformChords(this Pattern pattern, ChordTransformation chordTransformation, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("chordTransformation", chordTransformation);
			return pattern.TransformChords(PatternUtilities.AllChordsSelection, chordTransformation, recursive);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00023C38 File Offset: 0x00021E38
		public static Pattern TransformChords(this Pattern pattern, ChordSelection chordSelection, ChordTransformation chordTransformation, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("chordSelection", chordSelection);
			ThrowIfArgument.IsNull("chordTransformation", chordTransformation);
			ObjectWrapper<int> objectWrapper = new ObjectWrapper<int>();
			return PatternUtilities.TransformChords(pattern, objectWrapper, chordSelection, chordTransformation, recursive);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00023C78 File Offset: 0x00021E78
		public static IEnumerable<Pattern> SplitAtAnchor(this Pattern pattern, object anchor, bool removeEmptyPatterns = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("anchor", anchor);
			return PatternUtilities.SplitAtActions(pattern, delegate(PatternAction a)
			{
				AddAnchorAction addAnchorAction = a as AddAnchorAction;
				return ((addAnchorAction != null) ? addAnchorAction.Anchor : null) == anchor;
			}, removeEmptyPatterns);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00023CC0 File Offset: 0x00021EC0
		public static IEnumerable<Pattern> SplitAtAllAnchors(this Pattern pattern, bool removeEmptyPatterns = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			return PatternUtilities.SplitAtActions(pattern, (PatternAction a) => a is AddAnchorAction, removeEmptyPatterns);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00023CF4 File Offset: 0x00021EF4
		public static IEnumerable<Pattern> SplitAtMarker(this Pattern pattern, string marker, bool removeEmptyPatterns = true, StringComparison stringComparison = StringComparison.CurrentCulture)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("marker", marker);
			ThrowIfArgument.IsInvalidEnumValue<StringComparison>("stringComparison", stringComparison);
			return PatternUtilities.SplitAtActions(pattern, delegate(PatternAction a)
			{
				AddTextEventAction<MarkerEvent> addTextEventAction = a as AddTextEventAction<MarkerEvent>;
				return addTextEventAction != null && addTextEventAction.Text.Equals(marker, stringComparison);
			}, removeEmptyPatterns);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00023D53 File Offset: 0x00021F53
		public static IEnumerable<Pattern> SplitAtAllMarkers(this Pattern pattern, bool removeEmptyPatterns = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			return PatternUtilities.SplitAtActions(pattern, (PatternAction a) => a is AddTextEventAction<MarkerEvent>, removeEmptyPatterns);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00023D88 File Offset: 0x00021F88
		public static Pattern CombineInSequence(this IEnumerable<Pattern> patterns)
		{
			ThrowIfArgument.IsNull("patterns", patterns);
			PatternBuilder patternBuilder = new PatternBuilder();
			foreach (Pattern pattern in patterns.Where((Pattern p) => p != null))
			{
				patternBuilder.Pattern(pattern);
			}
			return patternBuilder.Build();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00023E0C File Offset: 0x0002200C
		public static Pattern CombineInParallel(this IEnumerable<Pattern> patterns)
		{
			ThrowIfArgument.IsNull("patterns", patterns);
			PatternBuilder patternBuilder = new PatternBuilder();
			foreach (Pattern pattern in patterns.Where((Pattern p) => p != null))
			{
				patternBuilder.Pattern(pattern).MoveToPreviousTime();
			}
			return patternBuilder.Build();
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00023E98 File Offset: 0x00022098
		public static void SetNotesState(this Pattern pattern, NoteSelection noteSelection, PatternActionState state, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("noteSelection", noteSelection);
			ThrowIfArgument.IsInvalidEnumValue<PatternActionState>("state", state);
			ObjectWrapper<int> objectWrapper = new ObjectWrapper<int>();
			PatternUtilities.SetNotesState(pattern, objectWrapper, noteSelection, state, recursive);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00023ED8 File Offset: 0x000220D8
		public static void SetChordsState(this Pattern pattern, ChordSelection chordSelection, PatternActionState state, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("chordSelection", chordSelection);
			ThrowIfArgument.IsInvalidEnumValue<PatternActionState>("state", state);
			ObjectWrapper<int> objectWrapper = new ObjectWrapper<int>();
			PatternUtilities.SetChordsState(pattern, objectWrapper, chordSelection, state, recursive);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00023F16 File Offset: 0x00022116
		private static IEnumerable<Pattern> SplitAtActions(Pattern pattern, Predicate<PatternAction> actionSelector, bool removeEmptyPatterns)
		{
			List<PatternAction> list = new List<PatternAction>();
			foreach (PatternAction patternAction in pattern.Actions)
			{
				if (!actionSelector(patternAction))
				{
					list.Add(patternAction);
				}
				else
				{
					if (list.Any<PatternAction>() || !removeEmptyPatterns)
					{
						yield return new Pattern(list.AsReadOnly());
					}
					list = new List<PatternAction>();
				}
			}
			IEnumerator<PatternAction> enumerator = null;
			if (list.Any<PatternAction>())
			{
				yield return new Pattern(list.AsReadOnly());
			}
			yield break;
			yield break;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00023F34 File Offset: 0x00022134
		private static Pattern TransformNotes(Pattern pattern, ObjectWrapper<int> noteIndexWrapper, NoteSelection noteSelection, NoteTransformation noteTransformation, bool recursive)
		{
			return new Pattern(pattern.Actions.Select(delegate(PatternAction a)
			{
				AddNoteAction addNoteAction = a as AddNoteAction;
				if (addNoteAction != null)
				{
					NoteSelection noteSelection2 = noteSelection;
					ObjectWrapper<int> noteIndexWrapper2 = noteIndexWrapper;
					int @object = noteIndexWrapper2.Object;
					noteIndexWrapper2.Object = @object + 1;
					if (noteSelection2(@object, addNoteAction.NoteDescriptor))
					{
						return new AddNoteAction(noteTransformation(addNoteAction.NoteDescriptor));
					}
				}
				AddPatternAction addPatternAction = a as AddPatternAction;
				if ((addPatternAction != null) & recursive)
				{
					return new AddPatternAction(PatternUtilities.TransformNotes(addPatternAction.Pattern, noteIndexWrapper, noteSelection, noteTransformation, recursive));
				}
				return a.Clone();
			}).ToList<PatternAction>());
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00023F88 File Offset: 0x00022188
		private static Pattern TransformChords(Pattern pattern, ObjectWrapper<int> chordIndexWrapper, ChordSelection chordSelection, ChordTransformation chordTransformation, bool recursive)
		{
			return new Pattern(pattern.Actions.Select(delegate(PatternAction a)
			{
				AddChordAction addChordAction = a as AddChordAction;
				if (addChordAction != null)
				{
					ChordSelection chordSelection2 = chordSelection;
					ObjectWrapper<int> chordIndexWrapper2 = chordIndexWrapper;
					int @object = chordIndexWrapper2.Object;
					chordIndexWrapper2.Object = @object + 1;
					if (chordSelection2(@object, addChordAction.ChordDescriptor))
					{
						return new AddChordAction(chordTransformation(addChordAction.ChordDescriptor));
					}
				}
				AddPatternAction addPatternAction = a as AddPatternAction;
				if ((addPatternAction != null) & recursive)
				{
					return new AddPatternAction(PatternUtilities.TransformChords(addPatternAction.Pattern, chordIndexWrapper, chordSelection, chordTransformation, recursive));
				}
				return a.Clone();
			}).ToList<PatternAction>());
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00023FDC File Offset: 0x000221DC
		private static void SetNotesState(Pattern pattern, ObjectWrapper<int> noteIndexWrapper, NoteSelection noteSelection, PatternActionState state, bool recursive)
		{
			foreach (PatternAction patternAction in pattern.Actions)
			{
				AddNoteAction addNoteAction = patternAction as AddNoteAction;
				if (addNoteAction != null)
				{
					int @object = noteIndexWrapper.Object;
					noteIndexWrapper.Object = @object + 1;
					if (noteSelection(@object, addNoteAction.NoteDescriptor))
					{
						addNoteAction.State = state;
					}
				}
				AddPatternAction addPatternAction = patternAction as AddPatternAction;
				if (addPatternAction != null && recursive)
				{
					PatternUtilities.SetNotesState(addPatternAction.Pattern, noteIndexWrapper, noteSelection, state, recursive);
				}
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00024070 File Offset: 0x00022270
		private static void SetChordsState(Pattern pattern, ObjectWrapper<int> chordIndexWrapper, ChordSelection chordSelection, PatternActionState state, bool recursive)
		{
			foreach (PatternAction patternAction in pattern.Actions)
			{
				AddChordAction addChordAction = patternAction as AddChordAction;
				if (addChordAction != null)
				{
					int @object = chordIndexWrapper.Object;
					chordIndexWrapper.Object = @object + 1;
					if (chordSelection(@object, addChordAction.ChordDescriptor))
					{
						addChordAction.State = state;
					}
				}
				AddPatternAction addPatternAction = patternAction as AddPatternAction;
				if (addPatternAction != null && recursive)
				{
					PatternUtilities.SetChordsState(addPatternAction.Pattern, chordIndexWrapper, chordSelection, state, recursive);
				}
			}
		}

		// Token: 0x040009A2 RID: 2466
		private static readonly NoteSelection AllNotesSelection = (int i, NoteDescriptor d) => true;

		// Token: 0x040009A3 RID: 2467
		private static readonly ChordSelection AllChordsSelection = (int i, ChordDescriptor d) => true;
	}
}
