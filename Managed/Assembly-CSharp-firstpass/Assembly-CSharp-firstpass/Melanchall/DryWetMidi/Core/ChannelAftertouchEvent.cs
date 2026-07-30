using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000130 RID: 304
	public sealed class ChannelAftertouchEvent : ChannelEvent
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x0001E515 File Offset: 0x0001C715
		public ChannelAftertouchEvent()
			: base(MidiEventType.ChannelAftertouch, 1)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001E520 File Offset: 0x0001C720
		public ChannelAftertouchEvent(SevenBitNumber aftertouchValue)
			: this()
		{
			this.AftertouchValue = aftertouchValue;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0001E52F File Offset: 0x0001C72F
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0001E538 File Offset: 0x0001C738
		public SevenBitNumber AftertouchValue
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

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001E542 File Offset: 0x0001C742
		protected override MidiEvent CloneEvent()
		{
			return new ChannelAftertouchEvent(this.AftertouchValue)
			{
				Channel = base.Channel
			};
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001E55B File Offset: 0x0001C75B
		public override string ToString()
		{
			return string.Format("Channel Aftertouch [{0}] ({1})", base.Channel, this.AftertouchValue);
		}

		// Token: 0x0400087E RID: 2174
		private const int ParametersCount = 1;

		// Token: 0x0400087F RID: 2175
		private const int AftertouchValueParameterIndex = 0;
	}
}
