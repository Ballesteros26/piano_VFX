using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000098 RID: 152
	internal class DnsResourceRecordAAAA : DnsResourceRecordIPAddress
	{
		// Token: 0x06000371 RID: 881 RVA: 0x0000AC58 File Offset: 0x00008E58
		internal DnsResourceRecordAAAA(DnsResourceRecord rr)
			: base(rr, 16)
		{
		}
	}
}
