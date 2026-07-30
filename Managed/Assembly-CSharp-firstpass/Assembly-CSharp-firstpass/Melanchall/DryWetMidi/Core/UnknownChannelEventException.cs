using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200017A RID: 378
	public sealed class UnknownChannelEventException : MidiException
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x000207E5 File Offset: 0x0001E9E5
		internal UnknownChannelEventException(FourBitNumber statusByte, FourBitNumber channel)
			: base(string.Format("Unknown channel event (status byte is {0} and channel is {1}).", statusByte, channel))
		{
			this.StatusByte = statusByte;
			this.Channel = channel;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00020811 File Offset: 0x0001EA11
		public FourBitNumber Channel { get; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00020819 File Offset: 0x0001EA19
		public FourBitNumber StatusByte { get; }
	}
}
