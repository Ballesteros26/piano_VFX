using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000133 RID: 307
	public abstract class NoteEvent : ChannelEvent
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x0001E66A File Offset: 0x0001C86A
		protected NoteEvent(MidiEventType eventType)
			: base(eventType, 2)
		{
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001E674 File Offset: 0x0001C874
		protected NoteEvent(MidiEventType eventType, SevenBitNumber noteNumber, SevenBitNumber velocity)
			: this(eventType)
		{
			this.NoteNumber = noteNumber;
			this.Velocity = velocity;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001E52F File Offset: 0x0001C72F
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0001E538 File Offset: 0x0001C738
		public SevenBitNumber NoteNumber
		{
			get
			{
				return base[0];
			}
			set
			{
				base[0] = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0001E59E File Offset: 0x0001C79E
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001E5A7 File Offset: 0x0001C7A7
		public SevenBitNumber Velocity
		{
			get
			{
				return base[1];
			}
			set
			{
				base[1] = value;
			}
		}

		// Token: 0x04000886 RID: 2182
		private const int ParametersCount = 2;

		// Token: 0x04000887 RID: 2183
		private const int NoteNumberParameterIndex = 0;

		// Token: 0x04000888 RID: 2184
		private const int VelocityParameterIndex = 1;
	}
}
