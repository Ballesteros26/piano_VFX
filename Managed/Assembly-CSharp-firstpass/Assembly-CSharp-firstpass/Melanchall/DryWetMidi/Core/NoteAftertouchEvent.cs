using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000132 RID: 306
	public sealed class NoteAftertouchEvent : ChannelEvent
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x0001E5FD File Offset: 0x0001C7FD
		public NoteAftertouchEvent()
			: base(MidiEventType.NoteAftertouch, 2)
		{
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001E608 File Offset: 0x0001C808
		public NoteAftertouchEvent(SevenBitNumber noteNumber, SevenBitNumber aftertouchValue)
			: this()
		{
			this.NoteNumber = noteNumber;
			this.AftertouchValue = aftertouchValue;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001E52F File Offset: 0x0001C72F
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0001E538 File Offset: 0x0001C738
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001E59E File Offset: 0x0001C79E
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0001E5A7 File Offset: 0x0001C7A7
		public SevenBitNumber AftertouchValue
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

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001E61E File Offset: 0x0001C81E
		protected override MidiEvent CloneEvent()
		{
			return new NoteAftertouchEvent(this.NoteNumber, this.AftertouchValue)
			{
				Channel = base.Channel
			};
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001E63D File Offset: 0x0001C83D
		public override string ToString()
		{
			return string.Format("Note Aftertouch [{0}] ({1}, {2})", base.Channel, this.NoteNumber, this.AftertouchValue);
		}

		// Token: 0x04000883 RID: 2179
		private const int ParametersCount = 2;

		// Token: 0x04000884 RID: 2180
		private const int NoteNumberParameterIndex = 0;

		// Token: 0x04000885 RID: 2181
		private const int AftertouchValueParameterIndex = 1;
	}
}
