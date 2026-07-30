using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000099 RID: 153
	internal class DnsResourceRecordCName : DnsResourceRecord
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000AC64 File Offset: 0x00008E64
		internal DnsResourceRecordCName(DnsResourceRecord rr)
		{
			base.CopyFrom(rr);
			int offset = rr.Data.Offset;
			this.cname = DnsPacket.ReadName(rr.Data.Array, ref offset);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		public string CName
		{
			get
			{
				return this.cname;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public override string ToString()
		{
			return base.ToString() + " CNAME: " + this.cname.ToString();
		}

		// Token: 0x040008A3 RID: 2211
		private string cname;
	}
}
