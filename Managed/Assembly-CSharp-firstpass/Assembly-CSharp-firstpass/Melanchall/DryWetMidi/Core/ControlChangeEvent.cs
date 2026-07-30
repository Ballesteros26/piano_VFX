using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000131 RID: 305
	public sealed class ControlChangeEvent : ChannelEvent
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x0001E57D File Offset: 0x0001C77D
		public ControlChangeEvent()
			: base(MidiEventType.ControlChange, 2)
		{
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001E588 File Offset: 0x0001C788
		public ControlChangeEvent(SevenBitNumber controlNumber, SevenBitNumber controlValue)
			: this()
		{
			this.ControlNumber = controlNumber;
			this.ControlValue = controlValue;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001E52F File Offset: 0x0001C72F
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0001E538 File Offset: 0x0001C738
		public SevenBitNumber ControlNumber
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001E59E File Offset: 0x0001C79E
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0001E5A7 File Offset: 0x0001C7A7
		public SevenBitNumber ControlValue
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

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001E5B1 File Offset: 0x0001C7B1
		protected override MidiEvent CloneEvent()
		{
			return new ControlChangeEvent(this.ControlNumber, this.ControlValue)
			{
				Channel = base.Channel
			};
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		public override string ToString()
		{
			return string.Format("Control Change [{0}] ({1}, {2})", base.Channel, this.ControlNumber, this.ControlValue);
		}

		// Token: 0x04000880 RID: 2176
		private const int ParametersCount = 2;

		// Token: 0x04000881 RID: 2177
		private const int ControlNumberParameterIndex = 0;

		// Token: 0x04000882 RID: 2178
		private const int ControlValueParameterIndex = 1;
	}
}
