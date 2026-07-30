using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200009B RID: 155
	internal class DnsResourceRecordPTR : DnsResourceRecord
	{
		// Token: 0x06000378 RID: 888 RVA: 0x0000AD3C File Offset: 0x00008F3C
		internal DnsResourceRecordPTR(DnsResourceRecord rr)
		{
			base.CopyFrom(rr);
			int offset = rr.Data.Offset;
			this.dname = DnsPacket.ReadName(rr.Data.Array, ref offset);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000AD80 File Offset: 0x00008F80
		public string DName
		{
			get
			{
				return this.dname;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000AD88 File Offset: 0x00008F88
		public override string ToString()
		{
			return base.ToString() + " DNAME: " + this.dname.ToString();
		}

		// Token: 0x040008A5 RID: 2213
		private string dname;
	}
}
