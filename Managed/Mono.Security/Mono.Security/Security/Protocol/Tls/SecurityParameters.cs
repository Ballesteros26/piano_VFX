using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000041 RID: 65
	internal class SecurityParameters
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000FC9A File Offset: 0x0000DE9A
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000FCA2 File Offset: 0x0000DEA2
		public CipherSuite Cipher
		{
			get
			{
				return this.cipher;
			}
			set
			{
				this.cipher = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000FCAB File Offset: 0x0000DEAB
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
		public byte[] ClientWriteMAC
		{
			get
			{
				return this.clientWriteMAC;
			}
			set
			{
				this.clientWriteMAC = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000FCBC File Offset: 0x0000DEBC
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		public byte[] ServerWriteMAC
		{
			get
			{
				return this.serverWriteMAC;
			}
			set
			{
				this.serverWriteMAC = value;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000FCCD File Offset: 0x0000DECD
		public void Clear()
		{
			this.cipher = null;
		}

		// Token: 0x04000181 RID: 385
		private CipherSuite cipher;

		// Token: 0x04000182 RID: 386
		private byte[] clientWriteMAC;

		// Token: 0x04000183 RID: 387
		private byte[] serverWriteMAC;
	}
}
