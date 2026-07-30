using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000090 RID: 144
	internal abstract class DnsPacket
	{
		// Token: 0x06000352 RID: 850 RVA: 0x000020EB File Offset: 0x000002EB
		protected DnsPacket()
		{
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000A711 File Offset: 0x00008911
		protected DnsPacket(int length)
			: this(new byte[length], length)
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000A720 File Offset: 0x00008920
		protected DnsPacket(byte[] buffer, int length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", "Must be greater than zero.");
			}
			this.packet = buffer;
			this.position = length;
			this.header = new DnsHeader(new ArraySegment<byte>(this.packet, 0, 12));
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000A77C File Offset: 0x0000897C
		public byte[] Packet
		{
			get
			{
				return this.packet;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000A784 File Offset: 0x00008984
		public int Length
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000A78C File Offset: 0x0000898C
		public DnsHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000A794 File Offset: 0x00008994
		protected void WriteUInt16(ushort v)
		{
			byte[] array = this.packet;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)((v & 65280) >> 8);
			byte[] array2 = this.packet;
			num = this.position;
			this.position = num + 1;
			array2[num] = (byte)(v & 255);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A7E4 File Offset: 0x000089E4
		protected void WriteStringBytes(string str, int offset, int count)
		{
			int num = offset;
			int i = 0;
			while (i < count)
			{
				byte[] array = this.packet;
				int num2 = this.position;
				this.position = num2 + 1;
				array[num2] = (byte)str[num];
				i++;
				num++;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A824 File Offset: 0x00008A24
		protected void WriteLabel(string str, int offset, int count)
		{
			byte[] array = this.packet;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)count;
			this.WriteStringBytes(str, offset, count);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A854 File Offset: 0x00008A54
		protected void WriteDnsName(string name)
		{
			if (!DnsUtil.IsValidDnsName(name))
			{
				throw new ArgumentException("Invalid DNS name");
			}
			if (!string.IsNullOrEmpty(name))
			{
				int length = name.Length;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < length; i++)
				{
					if (name[i] != '.')
					{
						num2++;
					}
					else
					{
						if (i == 0)
						{
							break;
						}
						this.WriteLabel(name, num, num2);
						num += num2 + 1;
						num2 = 0;
					}
				}
				if (num2 > 0)
				{
					this.WriteLabel(name, num, num2);
				}
			}
			byte[] array = this.packet;
			int num3 = this.position;
			this.position = num3 + 1;
			array[num3] = 0;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000A8E1 File Offset: 0x00008AE1
		protected internal string ReadName(ref int offset)
		{
			return DnsUtil.ReadName(this.packet, ref offset);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000A8EF File Offset: 0x00008AEF
		protected internal static string ReadName(byte[] buffer, ref int offset)
		{
			return DnsUtil.ReadName(buffer, ref offset);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		protected internal ushort ReadUInt16(ref int offset)
		{
			byte[] array = this.packet;
			int num = offset;
			offset = num + 1;
			ushort num2 = array[num] << 8;
			byte[] array2 = this.packet;
			num = offset;
			offset = num + 1;
			return num2 + array2[num];
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A92C File Offset: 0x00008B2C
		protected internal int ReadInt32(ref int offset)
		{
			byte[] array = this.packet;
			int num = offset;
			offset = num + 1;
			int num2 = array[num] << 24;
			byte[] array2 = this.packet;
			num = offset;
			offset = num + 1;
			int num3 = num2 + (array2[num] << 16);
			byte[] array3 = this.packet;
			num = offset;
			offset = num + 1;
			int num4 = num3 + (array3[num] << 8);
			byte[] array4 = this.packet;
			num = offset;
			offset = num + 1;
			return num4 + array4[num];
		}

		// Token: 0x04000831 RID: 2097
		protected byte[] packet;

		// Token: 0x04000832 RID: 2098
		protected int position;

		// Token: 0x04000833 RID: 2099
		protected DnsHeader header;
	}
}
