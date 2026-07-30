using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000093 RID: 147
	internal class DnsQuery : DnsPacket
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0000A984 File Offset: 0x00008B84
		public DnsQuery(string name, DnsQType qtype, DnsQClass qclass)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int num = DnsUtil.GetEncodedLength(name);
			if (num == -1)
			{
				throw new ArgumentException("Invalid DNS name", "name");
			}
			num += 16;
			this.packet = new byte[num];
			this.header = new DnsHeader(this.packet, 0);
			this.position = 12;
			base.WriteDnsName(name);
			base.WriteUInt16((ushort)qtype);
			base.WriteUInt16((ushort)qclass);
			base.Header.QuestionCount = 1;
			base.Header.IsQuery = true;
			base.Header.RecursionDesired = true;
		}
	}
}
