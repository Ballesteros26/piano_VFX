using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000135 RID: 309
	public sealed class NoteOnEvent : NoteEvent
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x0001E6ED File Offset: 0x0001C8ED
		public NoteOnEvent()
			: base(MidiEventType.NoteOn)
		{
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001E6F7 File Offset: 0x0001C8F7
		public NoteOnEvent(SevenBitNumber noteNumber, SevenBitNumber velocity)
			: base(MidiEventType.NoteOn, noteNumber, velocity)
		{
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001E703 File Offset: 0x0001C903
		protected override MidiEvent CloneEvent()
		{
			return new NoteOnEvent(base.NoteNumber, base.Velocity)
			{
				Channel = base.Channel
			};
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001E722 File Offset: 0x0001C922
		public override string ToString()
		{
			return string.Format("Note On [{0}] ({1}, {2})", base.Channel, base.NoteNumber, base.Velocity);
		}
	}
}
