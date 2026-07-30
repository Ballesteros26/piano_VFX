using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000ED RID: 237
	public sealed class InvalidSysExEventReceivedEventArgs : EventArgs
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x000196FF File Offset: 0x000178FF
		internal InvalidSysExEventReceivedEventArgs(byte[] data)
		{
			this.Data = data;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001970E File Offset: 0x0001790E
		public byte[] Data { get; }
	}
}
