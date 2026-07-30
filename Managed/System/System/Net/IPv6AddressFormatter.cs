using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000536 RID: 1334
	internal struct IPv6AddressFormatter
	{
		// Token: 0x06002952 RID: 10578 RVA: 0x0009F858 File Offset: 0x0009DA58
		public IPv6AddressFormatter(ushort[] addr, long scopeId)
		{
			this.address = addr;
			this.scopeId = scopeId;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x0009F868 File Offset: 0x0009DA68
		private static ushort SwapUShort(ushort number)
		{
			return (ushort)(((number >> 8) & 255) + (((int)number << 8) & 65280));
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0009F87E File Offset: 0x0009DA7E
		private uint AsIPv4Int()
		{
			return (uint)(((int)IPv6AddressFormatter.SwapUShort(this.address[7]) << 16) + (int)IPv6AddressFormatter.SwapUShort(this.address[6]));
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0009F8A0 File Offset: 0x0009DAA0
		private bool IsIPv4Compatible()
		{
			for (int i = 0; i < 6; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.address[6] != 0 && this.AsIPv4Int() > 1U;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x0009F8DC File Offset: 0x0009DADC
		private bool IsIPv4Mapped()
		{
			for (int i = 0; i < 5; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.address[6] != 0 && this.address[5] == ushort.MaxValue;
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x0009F91C File Offset: 0x0009DB1C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsIPv4Compatible() || this.IsIPv4Mapped())
			{
				stringBuilder.Append("::");
				if (this.IsIPv4Mapped())
				{
					stringBuilder.Append("ffff:");
				}
				stringBuilder.Append(new IPAddress((long)((ulong)this.AsIPv4Int())).ToString());
				return stringBuilder.ToString();
			}
			int num = -1;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 8; i++)
			{
				if (this.address[i] != 0)
				{
					if (num3 > num2 && num3 > 1)
					{
						num2 = num3;
						num = i - num3;
					}
					num3 = 0;
				}
				else
				{
					num3++;
				}
			}
			if (num3 > num2 && num3 > 1)
			{
				num2 = num3;
				num = 8 - num3;
			}
			if (num == 0)
			{
				stringBuilder.Append(":");
			}
			for (int j = 0; j < 8; j++)
			{
				if (j == num)
				{
					stringBuilder.Append(":");
					j += num2 - 1;
				}
				else
				{
					stringBuilder.AppendFormat("{0:x}", this.address[j]);
					if (j < 7)
					{
						stringBuilder.Append(':');
					}
				}
			}
			if (this.scopeId != 0L)
			{
				stringBuilder.Append('%').Append(this.scopeId);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002271 RID: 8817
		private ushort[] address;

		// Token: 0x04002272 RID: 8818
		private long scopeId;
	}
}
