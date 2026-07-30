using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200012F RID: 303
	public abstract class SystemRealTimeEvent : MidiEvent
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x0001E39A File Offset: 0x0001C59A
		protected SystemRealTimeEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00002994 File Offset: 0x00000B94
		internal sealed override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00002994 File Offset: 0x00000B94
		internal sealed override void Write(MidiWriter writer, WritingSettings settings)
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001E512 File Offset: 0x0001C712
		internal sealed override int GetSize(WritingSettings settings)
		{
			return 0;
		}
	}
}
