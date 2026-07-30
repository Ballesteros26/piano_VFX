using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000136 RID: 310
	public sealed class PitchBendEvent : ChannelEvent
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x0001E74F File Offset: 0x0001C94F
		public PitchBendEvent()
			: base(MidiEventType.PitchBend, 2)
		{
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001E75A File Offset: 0x0001C95A
		public PitchBendEvent(ushort pitchValue)
			: this()
		{
			this.PitchValue = pitchValue;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001E769 File Offset: 0x0001C969
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0001E77E File Offset: 0x0001C97E
		public ushort PitchValue
		{
			get
			{
				return DataTypesUtilities.Combine(base[1], base[0]);
			}
			set
			{
				base[0] = value.GetTail();
				base[1] = value.GetHead();
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001E79A File Offset: 0x0001C99A
		protected override MidiEvent CloneEvent()
		{
			return new PitchBendEvent(this.PitchValue)
			{
				Channel = base.Channel
			};
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001E7B3 File Offset: 0x0001C9B3
		public override string ToString()
		{
			return string.Format("Pitch Bend [{0}] ({1})", base.Channel, this.PitchValue);
		}

		// Token: 0x04000889 RID: 2185
		private const int ParametersCount = 2;

		// Token: 0x0400088A RID: 2186
		private const int PitchValueLsbParameterIndex = 0;

		// Token: 0x0400088B RID: 2187
		private const int PitchValueMsbParameterIndex = 1;
	}
}
