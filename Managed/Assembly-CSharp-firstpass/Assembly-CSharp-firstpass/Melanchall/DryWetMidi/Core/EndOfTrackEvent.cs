using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000143 RID: 323
	internal sealed class EndOfTrackEvent : MetaEvent
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x0001EDF4 File Offset: 0x0001CFF4
		public EndOfTrackEvent()
			: base(MidiEventType.EndOfTrack)
		{
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00002994 File Offset: 0x00000B94
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00002994 File Offset: 0x00000B94
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001E512 File Offset: 0x0001C712
		protected override int GetContentSize(WritingSettings settings)
		{
			return 0;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001EDFE File Offset: 0x0001CFFE
		protected override MidiEvent CloneEvent()
		{
			return new EndOfTrackEvent();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001EE05 File Offset: 0x0001D005
		public override string ToString()
		{
			return "End Of Track";
		}
	}
}
