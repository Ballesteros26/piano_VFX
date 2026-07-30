using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000096 RID: 150
	internal class DnsResourceRecord
	{
		// Token: 0x06000367 RID: 871 RVA: 0x000020EB File Offset: 0x000002EB
		internal DnsResourceRecord()
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000AA98 File Offset: 0x00008C98
		internal void CopyFrom(DnsResourceRecord rr)
		{
			this.name = rr.name;
			this.type = rr.type;
			this.klass = rr.klass;
			this.ttl = rr.ttl;
			this.rdlength = rr.rdlength;
			this.m_rdata = rr.m_rdata;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		internal static DnsResourceRecord CreateFromBuffer(DnsPacket packet, int size, ref int offset)
		{
			string text = packet.ReadName(ref offset);
			DnsType dnsType = (DnsType)packet.ReadUInt16(ref offset);
			DnsClass dnsClass = (DnsClass)packet.ReadUInt16(ref offset);
			int num = packet.ReadInt32(ref offset);
			ushort num2 = packet.ReadUInt16(ref offset);
			DnsResourceRecord dnsResourceRecord = new DnsResourceRecord();
			dnsResourceRecord.name = text;
			dnsResourceRecord.type = dnsType;
			dnsResourceRecord.klass = dnsClass;
			dnsResourceRecord.ttl = num;
			dnsResourceRecord.rdlength = num2;
			dnsResourceRecord.m_rdata = new ArraySegment<byte>(packet.Packet, offset, (int)num2);
			offset += (int)num2;
			if (dnsClass == DnsClass.Internet)
			{
				if (dnsType <= DnsType.CNAME)
				{
					if (dnsType != DnsType.A)
					{
						if (dnsType == DnsType.CNAME)
						{
							dnsResourceRecord = new DnsResourceRecordCName(dnsResourceRecord);
						}
					}
					else
					{
						dnsResourceRecord = new DnsResourceRecordA(dnsResourceRecord);
					}
				}
				else if (dnsType != DnsType.PTR)
				{
					if (dnsType == DnsType.AAAA)
					{
						dnsResourceRecord = new DnsResourceRecordAAAA(dnsResourceRecord);
					}
				}
				else
				{
					dnsResourceRecord = new DnsResourceRecordPTR(dnsResourceRecord);
				}
			}
			return dnsResourceRecord;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000ABBD File Offset: 0x00008DBD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		public DnsType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000ABCD File Offset: 0x00008DCD
		public DnsClass Class
		{
			get
			{
				return this.klass;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000ABD5 File Offset: 0x00008DD5
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000ABDD File Offset: 0x00008DDD
		public ArraySegment<byte> Data
		{
			get
			{
				return this.m_rdata;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public override string ToString()
		{
			return string.Format("Name: {0}, Type: {1}, Class: {2}, Ttl: {3}, Data length: {4}", new object[]
			{
				this.name,
				this.type,
				this.klass,
				this.ttl,
				this.Data.Count
			});
		}

		// Token: 0x0400089D RID: 2205
		private string name;

		// Token: 0x0400089E RID: 2206
		private DnsType type;

		// Token: 0x0400089F RID: 2207
		private DnsClass klass;

		// Token: 0x040008A0 RID: 2208
		private int ttl;

		// Token: 0x040008A1 RID: 2209
		private ushort rdlength;

		// Token: 0x040008A2 RID: 2210
		private ArraySegment<byte> m_rdata;
	}
}
