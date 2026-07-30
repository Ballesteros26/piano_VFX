using System;
using System.Text;

namespace Mono.Net.Dns
{
	// Token: 0x0200008E RID: 142
	internal class DnsHeader
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000A021 File Offset: 0x00008221
		public DnsHeader(byte[] bytes)
			: this(bytes, 0)
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A02B File Offset: 0x0000822B
		public DnsHeader(byte[] bytes, int offset)
			: this(new ArraySegment<byte>(bytes, offset, 12))
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000A03C File Offset: 0x0000823C
		public DnsHeader(ArraySegment<byte> segment)
		{
			if (segment.Count != 12)
			{
				throw new ArgumentException("Count must be 12", "segment");
			}
			this.bytes = segment;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000A068 File Offset: 0x00008268
		public void Clear()
		{
			for (int i = 0; i < 12; i++)
			{
				this.bytes.Array[i + this.bytes.Offset] = 0;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000A09C File Offset: 0x0000829C
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000A0D8 File Offset: 0x000082D8
		public ushort ID
		{
			get
			{
				return (ushort)((int)this.bytes.Array[this.bytes.Offset] * 256 + (int)this.bytes.Array[this.bytes.Offset + 1]);
			}
			set
			{
				this.bytes.Array[this.bytes.Offset] = (byte)((value & 65280) >> 8);
				this.bytes.Array[this.bytes.Offset + 1] = (byte)(value & 255);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000A127 File Offset: 0x00008327
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000A14C File Offset: 0x0000834C
		public bool IsQuery
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 128) > 0;
			}
			set
			{
				if (!value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 128;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 127;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000A1A8 File Offset: 0x000083A8
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000A1CC File Offset: 0x000083CC
		public DnsOpCode OpCode
		{
			get
			{
				return (DnsOpCode)((this.bytes.Array[2 + this.bytes.Offset] & 120) >> 3);
			}
			set
			{
				if (!Enum.IsDefined(typeof(DnsOpCode), value))
				{
					throw new ArgumentOutOfRangeException("value", "Invalid DnsOpCode value");
				}
				int num = (int)((int)value << 3);
				int num2 = (int)(this.bytes.Array[2 + this.bytes.Offset] & 135);
				num |= num2;
				this.bytes.Array[2 + this.bytes.Offset] = (byte)num;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000A245 File Offset: 0x00008445
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000A268 File Offset: 0x00008468
		public bool AuthoritativeAnswer
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 4) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 4;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 251;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000A2C3 File Offset: 0x000084C3
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000A2E4 File Offset: 0x000084E4
		public bool Truncation
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 2) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 2;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 253;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A33F File Offset: 0x0000853F
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000A360 File Offset: 0x00008560
		public bool RecursionDesired
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 1) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 1;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 254;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A3BB File Offset: 0x000085BB
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000A3E0 File Offset: 0x000085E0
		public bool RecursionAvailable
		{
			get
			{
				return (this.bytes.Array[3 + this.bytes.Offset] & 128) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 3 + this.bytes.Offset;
					array[num] |= 128;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] &= 127;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A43C File Offset: 0x0000863C
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000A45C File Offset: 0x0000865C
		public int ZReserved
		{
			get
			{
				return (this.bytes.Array[3 + this.bytes.Offset] & 112) >> 4;
			}
			set
			{
				if (value < 0 || value > 7)
				{
					throw new ArgumentOutOfRangeException("value", "Must be between 0 and 7");
				}
				byte[] array = this.bytes.Array;
				int num = 3 + this.bytes.Offset;
				array[num] &= 143;
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] |= (byte)((value << 4) & 112);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000A4D1 File Offset: 0x000086D1
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public DnsRCode RCode
		{
			get
			{
				return (DnsRCode)(this.bytes.Array[3 + this.bytes.Offset] & 15);
			}
			set
			{
				if (value < DnsRCode.NoError || value > (DnsRCode)15)
				{
					throw new ArgumentOutOfRangeException("value", "Must be between 0 and 15");
				}
				byte[] array = this.bytes.Array;
				int num = 3 + this.bytes.Offset;
				array[num] &= 15;
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] |= (byte)value;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000A560 File Offset: 0x00008760
		private static ushort GetUInt16(byte[] bytes, int offset)
		{
			return (ushort)((int)bytes[offset] * 256 + (int)bytes[offset + 1]);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000A572 File Offset: 0x00008772
		private static void SetUInt16(byte[] bytes, int offset, ushort val)
		{
			bytes[offset] = (byte)((val & 65280) >> 8);
			bytes[offset + 1] = (byte)(val & 255);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000A58E File Offset: 0x0000878E
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000A5A1 File Offset: 0x000087A1
		public ushort QuestionCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 4);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 4, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000A5B5 File Offset: 0x000087B5
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000A5C8 File Offset: 0x000087C8
		public ushort AnswerCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 6);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 6, value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000A5DC File Offset: 0x000087DC
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000A5EF File Offset: 0x000087EF
		public ushort AuthorityCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 8);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 8, value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000A603 File Offset: 0x00008803
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000A617 File Offset: 0x00008817
		public ushort AdditionalCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 10);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 10, value);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000A62C File Offset: 0x0000882C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("ID: {0} QR: {1} Opcode: {2} AA: {3} TC: {4} RD: {5} RA: {6} \r\nRCode: {7} ", new object[] { this.ID, this.IsQuery, this.OpCode, this.AuthoritativeAnswer, this.Truncation, this.RecursionDesired, this.RecursionAvailable, this.RCode });
			stringBuilder.AppendFormat("Q: {0} A: {1} NS: {2} AR: {3}\r\n", new object[] { this.QuestionCount, this.AnswerCount, this.AuthorityCount, this.AdditionalCount });
			return stringBuilder.ToString();
		}

		// Token: 0x04000829 RID: 2089
		public const int DnsHeaderLength = 12;

		// Token: 0x0400082A RID: 2090
		private ArraySegment<byte> bytes;
	}
}
