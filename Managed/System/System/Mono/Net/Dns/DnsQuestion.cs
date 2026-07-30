using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000094 RID: 148
	internal class DnsQuestion
	{
		// Token: 0x06000361 RID: 865 RVA: 0x000020EB File Offset: 0x000002EB
		internal DnsQuestion()
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000AA29 File Offset: 0x00008C29
		internal int Init(DnsPacket packet, int offset)
		{
			this.name = packet.ReadName(ref offset);
			this.type = (DnsQType)packet.ReadUInt16(ref offset);
			this._class = (DnsQClass)packet.ReadUInt16(ref offset);
			return offset;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000AA56 File Offset: 0x00008C56
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000AA5E File Offset: 0x00008C5E
		public DnsQType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000AA66 File Offset: 0x00008C66
		public DnsQClass Class
		{
			get
			{
				return this._class;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000AA6E File Offset: 0x00008C6E
		public override string ToString()
		{
			return string.Format("Name: {0} Type: {1} Class: {2}", this.Name, this.Type, this.Class);
		}

		// Token: 0x04000886 RID: 2182
		private string name;

		// Token: 0x04000887 RID: 2183
		private DnsQType type;

		// Token: 0x04000888 RID: 2184
		private DnsQClass _class;
	}
}
