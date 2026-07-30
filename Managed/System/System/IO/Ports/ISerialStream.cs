using System;

namespace System.IO.Ports
{
	// Token: 0x020003F0 RID: 1008
	internal interface ISerialStream : IDisposable
	{
		// Token: 0x06001E7B RID: 7803
		int Read(byte[] buffer, int offset, int count);

		// Token: 0x06001E7C RID: 7804
		void Write(byte[] buffer, int offset, int count);

		// Token: 0x06001E7D RID: 7805
		void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs);

		// Token: 0x06001E7E RID: 7806
		void DiscardInBuffer();

		// Token: 0x06001E7F RID: 7807
		void DiscardOutBuffer();

		// Token: 0x06001E80 RID: 7808
		SerialSignal GetSignals();

		// Token: 0x06001E81 RID: 7809
		void SetSignal(SerialSignal signal, bool value);

		// Token: 0x06001E82 RID: 7810
		void SetBreakState(bool value);

		// Token: 0x06001E83 RID: 7811
		void Close();

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001E84 RID: 7812
		int BytesToRead { get; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001E85 RID: 7813
		int BytesToWrite { get; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001E86 RID: 7814
		// (set) Token: 0x06001E87 RID: 7815
		int ReadTimeout { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001E88 RID: 7816
		// (set) Token: 0x06001E89 RID: 7817
		int WriteTimeout { get; set; }
	}
}
