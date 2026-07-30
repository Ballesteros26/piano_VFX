using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x02000400 RID: 1024
	[StructLayout(LayoutKind.Sequential)]
	internal class DCB
	{
		// Token: 0x06001F48 RID: 8008 RVA: 0x0007ABFC File Offset: 0x00078DFC
		public void SetValues(int baud_rate, Parity parity, int byte_size, StopBits sb, Handshake hs)
		{
			switch (sb)
			{
			case StopBits.One:
				this.stop_bits = 0;
				break;
			case StopBits.Two:
				this.stop_bits = 2;
				break;
			case StopBits.OnePointFive:
				this.stop_bits = 1;
				break;
			}
			this.baud_rate = baud_rate;
			this.parity = (byte)parity;
			this.byte_size = (byte)byte_size;
			this.flags &= -8965;
			switch (hs)
			{
			case Handshake.None:
				break;
			case Handshake.XOnXOff:
				this.flags |= 768;
				return;
			case Handshake.RequestToSend:
				this.flags |= 8196;
				return;
			case Handshake.RequestToSendXOnXOff:
				this.flags |= 8964;
				break;
			default:
				return;
			}
		}

		// Token: 0x04001B55 RID: 6997
		public int dcb_length;

		// Token: 0x04001B56 RID: 6998
		public int baud_rate;

		// Token: 0x04001B57 RID: 6999
		public int flags;

		// Token: 0x04001B58 RID: 7000
		public short w_reserved;

		// Token: 0x04001B59 RID: 7001
		public short xon_lim;

		// Token: 0x04001B5A RID: 7002
		public short xoff_lim;

		// Token: 0x04001B5B RID: 7003
		public byte byte_size;

		// Token: 0x04001B5C RID: 7004
		public byte parity;

		// Token: 0x04001B5D RID: 7005
		public byte stop_bits;

		// Token: 0x04001B5E RID: 7006
		public byte xon_char;

		// Token: 0x04001B5F RID: 7007
		public byte xoff_char;

		// Token: 0x04001B60 RID: 7008
		public byte error_char;

		// Token: 0x04001B61 RID: 7009
		public byte eof_char;

		// Token: 0x04001B62 RID: 7010
		public byte evt_char;

		// Token: 0x04001B63 RID: 7011
		public short w_reserved1;

		// Token: 0x04001B64 RID: 7012
		private const int fOutxCtsFlow = 4;

		// Token: 0x04001B65 RID: 7013
		private const int fOutX = 256;

		// Token: 0x04001B66 RID: 7014
		private const int fInX = 512;

		// Token: 0x04001B67 RID: 7015
		private const int fRtsControl2 = 8192;
	}
}
