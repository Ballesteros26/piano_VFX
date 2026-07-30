using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000097 RID: 151
	internal class DnsResourceRecordA : DnsResourceRecordIPAddress
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000AC4E File Offset: 0x00008E4E
		internal DnsResourceRecordA(DnsResourceRecord rr)
			: base(rr, 4)
		{
		}
	}
}
