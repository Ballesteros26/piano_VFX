using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000072 RID: 114
	public class Type1Message : MessageBase
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x00016255 File Offset: 0x00014455
		public Type1Message()
			: base(1)
		{
			this._domain = Environment.UserDomainName;
			this._host = Environment.MachineName;
			base.Flags = NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateOem | NtlmFlags.RequestTarget | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateDomainSupplied | NtlmFlags.NegotiateWorkstationSupplied | NtlmFlags.NegotiateAlwaysSign;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001627F File Offset: 0x0001447F
		public Type1Message(byte[] message)
			: base(1)
		{
			this.Decode(message);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001628F File Offset: 0x0001448F
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00016298 File Offset: 0x00014498
		public string Domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == "")
				{
					base.Flags &= ~NtlmFlags.NegotiateDomainSupplied;
				}
				else
				{
					base.Flags |= NtlmFlags.NegotiateDomainSupplied;
				}
				this._domain = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000162E9 File Offset: 0x000144E9
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x000162F4 File Offset: 0x000144F4
		public string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == "")
				{
					base.Flags &= ~NtlmFlags.NegotiateWorkstationSupplied;
				}
				else
				{
					base.Flags |= NtlmFlags.NegotiateWorkstationSupplied;
				}
				this._host = value;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00016348 File Offset: 0x00014548
		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			base.Flags = (NtlmFlags)BitConverterLE.ToUInt32(message, 12);
			int num = (int)BitConverterLE.ToUInt16(message, 16);
			int num2 = (int)BitConverterLE.ToUInt16(message, 20);
			this._domain = Encoding.ASCII.GetString(message, num2, num);
			int num3 = (int)BitConverterLE.ToUInt16(message, 24);
			this._host = Encoding.ASCII.GetString(message, 32, num3);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000163AC File Offset: 0x000145AC
		public override byte[] GetBytes()
		{
			short num = (short)this._domain.Length;
			short num2 = (short)this._host.Length;
			byte[] array = base.PrepareMessage((int)(32 + num + num2));
			array[12] = (byte)base.Flags;
			array[13] = (byte)(base.Flags >> 8);
			array[14] = (byte)(base.Flags >> 16);
			array[15] = (byte)(base.Flags >> 24);
			short num3 = 32 + num2;
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			array[18] = array[16];
			array[19] = array[17];
			array[20] = (byte)num3;
			array[21] = (byte)(num3 >> 8);
			array[24] = (byte)num2;
			array[25] = (byte)(num2 >> 8);
			array[26] = array[24];
			array[27] = array[25];
			array[28] = 32;
			array[29] = 0;
			byte[] bytes = Encoding.ASCII.GetBytes(this._host.ToUpper(CultureInfo.InvariantCulture));
			Buffer.BlockCopy(bytes, 0, array, 32, bytes.Length);
			byte[] bytes2 = Encoding.ASCII.GetBytes(this._domain.ToUpper(CultureInfo.InvariantCulture));
			Buffer.BlockCopy(bytes2, 0, array, (int)num3, bytes2.Length);
			return array;
		}

		// Token: 0x0400020D RID: 525
		private string _host;

		// Token: 0x0400020E RID: 526
		private string _domain;
	}
}
