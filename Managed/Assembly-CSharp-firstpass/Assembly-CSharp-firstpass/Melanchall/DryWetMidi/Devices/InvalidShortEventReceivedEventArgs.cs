using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000EC RID: 236
	public sealed class InvalidShortEventReceivedEventArgs : EventArgs
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x000196CA File Offset: 0x000178CA
		internal InvalidShortEventReceivedEventArgs(byte statusByte, byte firstDataByte, byte secondDataByte)
		{
			this.StatusByte = statusByte;
			this.FirstDataByte = firstDataByte;
			this.SecondDataByte = secondDataByte;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000196E7 File Offset: 0x000178E7
		public byte StatusByte { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000196EF File Offset: 0x000178EF
		public byte FirstDataByte { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000196F7 File Offset: 0x000178F7
		public byte SecondDataByte { get; }
	}
}
