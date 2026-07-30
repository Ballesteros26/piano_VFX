using System;
using System.Linq;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001A4 RID: 420
	internal sealed class AddChordAction : PatternAction
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x000224A9 File Offset: 0x000206A9
		public AddChordAction(ChordDescriptor chordDescriptor)
		{
			this.ChordDescriptor = chordDescriptor;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000224B8 File Offset: 0x000206B8
		public ChordDescriptor ChordDescriptor { get; }

		// Token: 0x06000A24 RID: 2596 RVA: 0x000224C0 File Offset: 0x000206C0
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State == PatternActionState.Excluded)
			{
				return PatternActionResult.DoNothing;
			}
			context.SaveTime(time);
			long chordLength = LengthConverter.ConvertFrom(this.ChordDescriptor.Length, time, context.TempoMap);
			if (base.State == PatternActionState.Disabled)
			{
				return new PatternActionResult(new long?(time + chordLength));
			}
			return new PatternActionResult(new long?(time + chordLength), this.ChordDescriptor.Notes.Select((Melanchall.DryWetMidi.MusicTheory.Note d) => new Melanchall.DryWetMidi.Interaction.Note(d.NoteNumber, chordLength, time)
			{
				Channel = context.Channel,
				Velocity = this.ChordDescriptor.Velocity
			}));
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00022583 File Offset: 0x00020783
		public override PatternAction Clone()
		{
			return new AddChordAction(new ChordDescriptor(this.ChordDescriptor.Notes, this.ChordDescriptor.Velocity, this.ChordDescriptor.Length.Clone()));
		}
	}
}
