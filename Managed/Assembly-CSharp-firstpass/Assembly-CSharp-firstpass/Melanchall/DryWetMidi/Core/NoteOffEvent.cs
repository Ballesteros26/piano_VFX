using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000134 RID: 308
	public sealed class NoteOffEvent : NoteEvent
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x0001E68B File Offset: 0x0001C88B
		public NoteOffEvent()
			: base(MidiEventType.NoteOff)
		{
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001E695 File Offset: 0x0001C895
		public NoteOffEvent(SevenBitNumber noteNumber, SevenBitNumber velocity)
			: base(MidiEventType.NoteOff, noteNumber, velocity)
		{
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001E6A1 File Offset: 0x0001C8A1
		protected override MidiEvent CloneEvent()
		{
			return new NoteOffEvent(base.NoteNumber, base.Velocity)
			{
				Channel = base.Channel
			};
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		public override string ToString()
		{
			return string.Format("Note Off [{0}] ({1}, {2})", base.Channel, base.NoteNumber, base.Velocity);
		}
	}
}
