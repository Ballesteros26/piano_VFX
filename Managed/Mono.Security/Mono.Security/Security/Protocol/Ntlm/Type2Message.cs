using System;
using System.Security.Cryptography;
using System.Text;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000073 RID: 115
	public class Type2Message : MessageBase
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x000164C5 File Offset: 0x000146C5
		public Type2Message()
			: base(2)
		{
			this._nonce = new byte[8];
			RandomNumberGenerator.Create().GetBytes(this._nonce);
			base.Flags = NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000164F5 File Offset: 0x000146F5
		public Type2Message(byte[] message)
			: base(2)
		{
			this._nonce = new byte[8];
			this.Decode(message);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00016514 File Offset: 0x00014714
		~Type2Message()
		{
			if (this._nonce != null)
			{
				Array.Clear(this._nonce, 0, this._nonce.Length);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00016558 File Offset: 0x00014758
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0001656A File Offset: 0x0001476A
		public byte[] Nonce
		{
			get
			{
				return (byte[])this._nonce.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Nonce");
				}
				if (value.Length != 8)
				{
					throw new ArgumentException(Locale.GetText("Invalid Nonce Length (should be 8 bytes)."), "Nonce");
				}
				this._nonce = (byte[])value.Clone();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000165A6 File Offset: 0x000147A6
		public string TargetName
		{
			get
			{
				return this._targetName;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x000165AE File Offset: 0x000147AE
		public byte[] TargetInfo
		{
			get
			{
				return (byte[])this._targetInfo.Clone();
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000165C0 File Offset: 0x000147C0
		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			base.Flags = (NtlmFlags)BitConverterLE.ToUInt32(message, 20);
			Buffer.BlockCopy(message, 24, this._nonce, 0, 8);
			ushort num = BitConverterLE.ToUInt16(message, 12);
			ushort num2 = BitConverterLE.ToUInt16(message, 16);
			if (num > 0)
			{
				if ((base.Flags & NtlmFlags.NegotiateOem) != (NtlmFlags)0)
				{
					this._targetName = Encoding.ASCII.GetString(message, (int)num2, (int)num);
				}
				else
				{
					this._targetName = Encoding.Unicode.GetString(message, (int)num2, (int)num);
				}
			}
			if (message.Length >= 48)
			{
				ushort num3 = BitConverterLE.ToUInt16(message, 40);
				ushort num4 = BitConverterLE.ToUInt16(message, 44);
				if (num3 > 0)
				{
					this._targetInfo = new byte[(int)num3];
					Buffer.BlockCopy(message, (int)num4, this._targetInfo, 0, (int)num3);
				}
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00016674 File Offset: 0x00014874
		public override byte[] GetBytes()
		{
			byte[] array = base.PrepareMessage(40);
			short num = (short)array.Length;
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			array[20] = (byte)base.Flags;
			array[21] = (byte)(base.Flags >> 8);
			array[22] = (byte)(base.Flags >> 16);
			array[23] = (byte)(base.Flags >> 24);
			Buffer.BlockCopy(this._nonce, 0, array, 24, this._nonce.Length);
			return array;
		}

		// Token: 0x0400020F RID: 527
		private byte[] _nonce;

		// Token: 0x04000210 RID: 528
		private string _targetName;

		// Token: 0x04000211 RID: 529
		private byte[] _targetInfo;
	}
}
