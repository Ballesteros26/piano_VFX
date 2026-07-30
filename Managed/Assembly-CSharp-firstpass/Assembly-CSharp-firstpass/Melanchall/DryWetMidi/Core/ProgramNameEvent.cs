using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000149 RID: 329
	public sealed class ProgramNameEvent : BaseTextEvent
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x0001F059 File Offset: 0x0001D259
		public ProgramNameEvent()
			: base(MidiEventType.ProgramName)
		{
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001F063 File Offset: 0x0001D263
		public ProgramNameEvent(string programName)
			: base(MidiEventType.ProgramName, programName)
		{
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001F06E File Offset: 0x0001D26E
		protected override MidiEvent CloneEvent()
		{
			return new ProgramNameEvent(base.Text);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001F07B File Offset: 0x0001D27B
		public override string ToString()
		{
			return "Program Name (" + base.Text + ")";
		}
	}
}
