using System;
using System.Net;

namespace Mono.Net.Dns
{
	// Token: 0x0200009A RID: 154
	internal abstract class DnsResourceRecordIPAddress : DnsResourceRecord
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		internal DnsResourceRecordIPAddress(DnsResourceRecord rr, int address_size)
		{
			base.CopyFrom(rr);
			ArraySegment<byte> data = rr.Data;
			byte[] array = new byte[address_size];
			Buffer.BlockCopy(data.Array, data.Offset, array, 0, address_size);
			this.address = new IPAddress(array);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000AD1A File Offset: 0x00008F1A
		public override string ToString()
		{
			return base.ToString() + " Address: " + this.address;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000AD32 File Offset: 0x00008F32
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x040008A4 RID: 2212
		private IPAddress address;
	}
}
