using System;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000E9 RID: 233
	public sealed class ErrorOccurredEventArgs : EventArgs
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x00018E2B File Offset: 0x0001702B
		internal ErrorOccurredEventArgs(Exception exception)
		{
			this.Exception = exception;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00018E3A File Offset: 0x0001703A
		public Exception Exception { get; }
	}
}
