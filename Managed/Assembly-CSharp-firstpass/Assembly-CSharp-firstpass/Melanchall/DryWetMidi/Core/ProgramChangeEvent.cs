using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000137 RID: 311
	public sealed class ProgramChangeEvent : ChannelEvent
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x0001E7D5 File Offset: 0x0001C9D5
		public ProgramChangeEvent()
			: base(MidiEventType.ProgramChange, 1)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		public ProgramChangeEvent(SevenBitNumber programNumber)
			: this()
		{
			this.ProgramNumber = programNumber;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001E52F File Offset: 0x0001C72F
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0001E538 File Offset: 0x0001C738
		public SevenBitNumber ProgramNumber
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

		// Token: 0x0600080B RID: 2059 RVA: 0x0001E7EF File Offset: 0x0001C9EF
		protected override MidiEvent CloneEvent()
		{
			return new ProgramChangeEvent(this.ProgramNumber)
			{
				Channel = base.Channel
			};
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001E808 File Offset: 0x0001CA08
		public override string ToString()
		{
			return string.Format("Program Change [{0}] ({1})", base.Channel, this.ProgramNumber);
		}

		// Token: 0x0400088C RID: 2188
		private const int ParametersCount = 1;

		// Token: 0x0400088D RID: 2189
		private const int ProgramNumberParameterIndex = 0;
	}
}
