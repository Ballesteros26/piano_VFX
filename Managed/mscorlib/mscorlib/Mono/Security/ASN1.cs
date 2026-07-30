using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Mono.Security
{
	// Token: 0x0200003F RID: 63
	internal class ASN1
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00009338 File Offset: 0x00007538
		public ASN1()
			: this(0, null)
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00009342 File Offset: 0x00007542
		public ASN1(byte tag)
			: this(tag, null)
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000934C File Offset: 0x0000754C
		public ASN1(byte tag, byte[] data)
		{
			this.m_nTag = tag;
			this.m_aValue = data;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00009364 File Offset: 0x00007564
		public ASN1(byte[] data)
		{
			this.m_nTag = data[0];
			int num = 0;
			int num2 = (int)data[1];
			if (num2 > 128)
			{
				num = num2 - 128;
				num2 = 0;
				for (int i = 0; i < num; i++)
				{
					num2 *= 256;
					num2 += (int)data[i + 2];
				}
			}
			else if (num2 == 128)
			{
				throw new NotSupportedException("Undefined length encoding.");
			}
			this.m_aValue = new byte[num2];
			Buffer.BlockCopy(data, 2 + num, this.m_aValue, 0, num2);
			if ((this.m_nTag & 32) == 32)
			{
				int num3 = 2 + num;
				this.Decode(data, ref num3, data.Length);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009403 File Offset: 0x00007603
		public int Count
		{
			get
			{
				if (this.elist == null)
				{
					return 0;
				}
				return this.elist.Count;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000941A File Offset: 0x0000761A
		public byte Tag
		{
			get
			{
				return this.m_nTag;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00009422 File Offset: 0x00007622
		public int Length
		{
			get
			{
				if (this.m_aValue != null)
				{
					return this.m_aValue.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009436 File Offset: 0x00007636
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00009457 File Offset: 0x00007657
		public byte[] Value
		{
			get
			{
				if (this.m_aValue == null)
				{
					this.GetBytes();
				}
				return (byte[])this.m_aValue.Clone();
			}
			set
			{
				if (value != null)
				{
					this.m_aValue = (byte[])value.Clone();
				}
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009470 File Offset: 0x00007670
		private bool CompareArray(byte[] array1, byte[] array2)
		{
			bool flag = array1.Length == array2.Length;
			if (flag)
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000094A2 File Offset: 0x000076A2
		public bool Equals(byte[] asn1)
		{
			return this.CompareArray(this.GetBytes(), asn1);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000094B1 File Offset: 0x000076B1
		public bool CompareValue(byte[] value)
		{
			return this.CompareArray(this.m_aValue, value);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000094C0 File Offset: 0x000076C0
		public ASN1 Add(ASN1 asn1)
		{
			if (asn1 != null)
			{
				if (this.elist == null)
				{
					this.elist = new ArrayList();
				}
				this.elist.Add(asn1);
			}
			return asn1;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000094E8 File Offset: 0x000076E8
		public virtual byte[] GetBytes()
		{
			byte[] array = null;
			if (this.Count > 0)
			{
				int num = 0;
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.elist)
				{
					byte[] bytes = ((ASN1)obj).GetBytes();
					arrayList.Add(bytes);
					num += bytes.Length;
				}
				array = new byte[num];
				int num2 = 0;
				for (int i = 0; i < this.elist.Count; i++)
				{
					byte[] array2 = (byte[])arrayList[i];
					Buffer.BlockCopy(array2, 0, array, num2, array2.Length);
					num2 += array2.Length;
				}
			}
			else if (this.m_aValue != null)
			{
				array = this.m_aValue;
			}
			int num3 = 0;
			byte[] array3;
			if (array != null)
			{
				int num4 = array.Length;
				if (num4 > 127)
				{
					if (num4 <= 255)
					{
						array3 = new byte[3 + num4];
						Buffer.BlockCopy(array, 0, array3, 3, num4);
						num3 = 129;
						array3[2] = (byte)num4;
					}
					else if (num4 <= 65535)
					{
						array3 = new byte[4 + num4];
						Buffer.BlockCopy(array, 0, array3, 4, num4);
						num3 = 130;
						array3[2] = (byte)(num4 >> 8);
						array3[3] = (byte)num4;
					}
					else if (num4 <= 16777215)
					{
						array3 = new byte[5 + num4];
						Buffer.BlockCopy(array, 0, array3, 5, num4);
						num3 = 131;
						array3[2] = (byte)(num4 >> 16);
						array3[3] = (byte)(num4 >> 8);
						array3[4] = (byte)num4;
					}
					else
					{
						array3 = new byte[6 + num4];
						Buffer.BlockCopy(array, 0, array3, 6, num4);
						num3 = 132;
						array3[2] = (byte)(num4 >> 24);
						array3[3] = (byte)(num4 >> 16);
						array3[4] = (byte)(num4 >> 8);
						array3[5] = (byte)num4;
					}
				}
				else
				{
					array3 = new byte[2 + num4];
					Buffer.BlockCopy(array, 0, array3, 2, num4);
					num3 = num4;
				}
				if (this.m_aValue == null)
				{
					this.m_aValue = array;
				}
			}
			else
			{
				array3 = new byte[2];
			}
			array3[0] = this.m_nTag;
			array3[1] = (byte)num3;
			return array3;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00009704 File Offset: 0x00007904
		protected void Decode(byte[] asn1, ref int anPos, int anLength)
		{
			while (anPos < anLength - 1)
			{
				byte b;
				int num;
				byte[] array;
				this.DecodeTLV(asn1, ref anPos, out b, out num, out array);
				if (b != 0)
				{
					ASN1 asn2 = this.Add(new ASN1(b, array));
					if ((b & 32) == 32)
					{
						int num2 = anPos;
						asn2.Decode(asn1, ref num2, num2 + num);
					}
					anPos += num;
				}
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009758 File Offset: 0x00007958
		protected void DecodeTLV(byte[] asn1, ref int pos, out byte tag, out int length, out byte[] content)
		{
			int num = pos;
			pos = num + 1;
			tag = asn1[num];
			num = pos;
			pos = num + 1;
			length = (int)asn1[num];
			if ((length & 128) == 128)
			{
				int num2 = length & 127;
				length = 0;
				for (int i = 0; i < num2; i++)
				{
					int num3 = length * 256;
					num = pos;
					pos = num + 1;
					length = num3 + (int)asn1[num];
				}
			}
			content = new byte[length];
			Buffer.BlockCopy(asn1, pos, content, 0, length);
		}

		// Token: 0x17000022 RID: 34
		public ASN1 this[int index]
		{
			get
			{
				ASN1 asn;
				try
				{
					if (this.elist == null || index >= this.elist.Count)
					{
						asn = null;
					}
					else
					{
						asn = (ASN1)this.elist[index];
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					asn = null;
				}
				return asn;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009830 File Offset: 0x00007A30
		public ASN1 Element(int index, byte anTag)
		{
			ASN1 asn;
			try
			{
				if (this.elist == null || index >= this.elist.Count)
				{
					asn = null;
				}
				else
				{
					ASN1 asn2 = (ASN1)this.elist[index];
					if (asn2.Tag == anTag)
					{
						asn = asn2;
					}
					else
					{
						asn = null;
					}
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				asn = null;
			}
			return asn;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00009890 File Offset: 0x00007A90
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Tag: {0} {1}", this.m_nTag.ToString("X2"), Environment.NewLine);
			stringBuilder.AppendFormat("Length: {0} {1}", this.Value.Length, Environment.NewLine);
			stringBuilder.Append("Value: ");
			stringBuilder.Append(Environment.NewLine);
			for (int i = 0; i < this.Value.Length; i++)
			{
				stringBuilder.AppendFormat("{0} ", this.Value[i].ToString("X2"));
				if ((i + 1) % 16 == 0)
				{
					stringBuilder.AppendFormat(Environment.NewLine, Array.Empty<object>());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00009950 File Offset: 0x00007B50
		public void SaveToFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			using (FileStream fileStream = File.Create(filename))
			{
				byte[] bytes = this.GetBytes();
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x0400043C RID: 1084
		private byte m_nTag;

		// Token: 0x0400043D RID: 1085
		private byte[] m_aValue;

		// Token: 0x0400043E RID: 1086
		private ArrayList elist;
	}
}
