using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001A5 RID: 421
	internal sealed class AddNoteAction : PatternAction
	{
		// Token: 0x06000A26 RID: 2598 RVA: 0x000225B5 File Offset: 0x000207B5
		public AddNoteAction(NoteDescriptor noteDescriptor)
		{
			this.NoteDescriptor = noteDescriptor;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x000225C4 File Offset: 0x000207C4
		public NoteDescriptor NoteDescriptor { get; }

		// Token: 0x06000A28 RID: 2600 RVA: 0x000225CC File Offset: 0x000207CC
		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State == PatternActionState.Excluded)
			{
				return PatternActionResult.DoNothing;
			}
			context.SaveTime(time);
			long num = LengthConverter.ConvertFrom(this.NoteDescriptor.Length, time, context.TempoMap);
			if (base.State == PatternActionState.Disabled)
			{
				return new PatternActionResult(new long?(time + num));
			}
			Note note = new Note(this.NoteDescriptor.Note.NoteNumber, num, time)
			{
				Channel = context.Channel,
				Velocity = this.NoteDescriptor.Velocity
			};
			return new PatternActionResult(new long?(time + num), new Note[] { note });
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002266A File Offset: 0x0002086A
		public override PatternAction Clone()
		{
			return new AddNoteAction(new NoteDescriptor(this.NoteDescriptor.Note, this.NoteDescriptor.Velocity, this.NoteDescriptor.Length.Clone()));
		}
	}
}
