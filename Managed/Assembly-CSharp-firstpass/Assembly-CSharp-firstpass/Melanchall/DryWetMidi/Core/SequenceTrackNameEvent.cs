using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200014B RID: 331
	public sealed class SequenceTrackNameEvent : BaseTextEvent
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0001F100 File Offset: 0x0001D300
		public SequenceTrackNameEvent()
			: base(MidiEventType.SequenceTrackName)
		{
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001F109 File Offset: 0x0001D309
		public SequenceTrackNameEvent(string name)
			: base(MidiEventType.SequenceTrackName, name)
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001F113 File Offset: 0x0001D313
		protected override MidiEvent CloneEvent()
		{
			return new SequenceTrackNameEvent(base.Text);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001F120 File Offset: 0x0001D320
		public override string ToString()
		{
			return "Sequence/Track Name (" + base.Text + ")";
		}
	}
}
