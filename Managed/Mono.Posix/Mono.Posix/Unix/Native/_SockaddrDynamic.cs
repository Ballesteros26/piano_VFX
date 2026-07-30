using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200006A RID: 106
	[Map]
	internal struct _SockaddrDynamic
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x0000B654 File Offset: 0x00009854
		public unsafe _SockaddrDynamic(Sockaddr address, byte* data, bool useMaxLength)
		{
			if (data == null)
			{
				this = default(_SockaddrDynamic);
				return;
			}
			byte[] array = address.DynamicData();
			this.type = address.type & (SockaddrType)(-32769);
			this.sa_family = address.sa_family;
			this.data = data;
			if (useMaxLength)
			{
				this.len = (long)array.Length;
				return;
			}
			this.len = address.GetDynamicLength();
			if (this.len < 0L || this.len > (long)array.Length)
			{
				throw new ArgumentException("len < 0 || len > dynData.Length", "address");
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000B6DA File Offset: 0x000098DA
		public void Update(Sockaddr address)
		{
			if (this.data == null)
			{
				return;
			}
			address.sa_family = this.sa_family;
			address.SetDynamicLength(this.len);
		}

		// Token: 0x04000474 RID: 1140
		public SockaddrType type;

		// Token: 0x04000475 RID: 1141
		public UnixAddressFamily sa_family;

		// Token: 0x04000476 RID: 1142
		public unsafe byte* data;

		// Token: 0x04000477 RID: 1143
		public long len;
	}
}
