using System;
using System.Diagnostics;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200007E RID: 126
	internal struct BinXmlSqlDecimal
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public bool IsPositive
		{
			get
			{
				return this.m_bSign == 0;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public BinXmlSqlDecimal(byte[] data, int offset, bool trim)
		{
			byte b = data[offset];
			if (b <= 11)
			{
				if (b == 7)
				{
					this.m_bLen = 1;
					goto IL_0050;
				}
				if (b == 11)
				{
					this.m_bLen = 2;
					goto IL_0050;
				}
			}
			else
			{
				if (b == 15)
				{
					this.m_bLen = 3;
					goto IL_0050;
				}
				if (b == 19)
				{
					this.m_bLen = 4;
					goto IL_0050;
				}
			}
			throw new XmlException("Unable to parse data as SQL_DECIMAL.", null);
			IL_0050:
			this.m_bPrec = data[offset + 1];
			this.m_bScale = data[offset + 2];
			this.m_bSign = ((data[offset + 3] == 0) ? 1 : 0);
			this.m_data1 = BinXmlSqlDecimal.UIntFromByteArray(data, offset + 4);
			this.m_data2 = ((this.m_bLen > 1) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 8) : 0U);
			this.m_data3 = ((this.m_bLen > 2) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 12) : 0U);
			this.m_data4 = ((this.m_bLen > 3) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 16) : 0U);
			if (this.m_bLen == 4 && this.m_data4 == 0U)
			{
				this.m_bLen = 3;
			}
			if (this.m_bLen == 3 && this.m_data3 == 0U)
			{
				this.m_bLen = 2;
			}
			if (this.m_bLen == 2 && this.m_data2 == 0U)
			{
				this.m_bLen = 1;
			}
			if (trim)
			{
				this.TrimTrailingZeros();
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		public void Write(Stream strm)
		{
			strm.WriteByte(this.m_bLen * 4 + 3);
			strm.WriteByte(this.m_bPrec);
			strm.WriteByte(this.m_bScale);
			strm.WriteByte((this.m_bSign == 0) ? 1 : 0);
			this.WriteUI4(this.m_data1, strm);
			if (this.m_bLen > 1)
			{
				this.WriteUI4(this.m_data2, strm);
				if (this.m_bLen > 2)
				{
					this.WriteUI4(this.m_data3, strm);
					if (this.m_bLen > 3)
					{
						this.WriteUI4(this.m_data4, strm);
					}
				}
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000EE04 File Offset: 0x0000D004
		private void WriteUI4(uint val, Stream strm)
		{
			strm.WriteByte((byte)(val & 255U));
			strm.WriteByte((byte)((val >> 8) & 255U));
			strm.WriteByte((byte)((val >> 16) & 255U));
			strm.WriteByte((byte)((val >> 24) & 255U));
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000EE51 File Offset: 0x0000D051
		private static uint UIntFromByteArray(byte[] data, int offset)
		{
			return (uint)((int)data[offset] | ((int)data[offset + 1] << 8) | ((int)data[offset + 2] << 16) | ((int)data[offset + 3] << 24));
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000EE70 File Offset: 0x0000D070
		private bool FZero()
		{
			return this.m_data1 == 0U && this.m_bLen <= 1;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000EE88 File Offset: 0x0000D088
		private void StoreFromWorkingArray(uint[] rguiData)
		{
			this.m_data1 = rguiData[0];
			this.m_data2 = rguiData[1];
			this.m_data3 = rguiData[2];
			this.m_data4 = rguiData[3];
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		private bool FGt10_38(uint[] rglData)
		{
			return (ulong)rglData[3] >= 1262177448UL && ((ulong)rglData[3] > 1262177448UL || (ulong)rglData[2] > 1518781562UL || ((ulong)rglData[2] == 1518781562UL && (ulong)rglData[1] >= 160047680UL));
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000EF04 File Offset: 0x0000D104
		private static void MpDiv1(uint[] rgulU, ref int ciulU, uint iulD, out uint iulR)
		{
			uint num = 0U;
			ulong num2 = (ulong)iulD;
			int i = ciulU;
			while (i > 0)
			{
				i--;
				ulong num3 = ((ulong)num << 32) + (ulong)rgulU[i];
				rgulU[i] = (uint)(num3 / num2);
				num = (uint)(num3 - (ulong)rgulU[i] * num2);
			}
			iulR = num;
			BinXmlSqlDecimal.MpNormalize(rgulU, ref ciulU);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000EF49 File Offset: 0x0000D149
		private static void MpNormalize(uint[] rgulU, ref int ciulU)
		{
			while (ciulU > 1 && rgulU[ciulU - 1] == 0U)
			{
				ciulU--;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000EF60 File Offset: 0x0000D160
		internal void AdjustScale(int digits, bool fRound)
		{
			bool flag = false;
			int i = digits;
			if (i + (int)this.m_bScale < 0)
			{
				throw new XmlException("Numeric arithmetic causes truncation.", null);
			}
			if (i + (int)this.m_bScale > (int)BinXmlSqlDecimal.NUMERIC_MAX_PRECISION)
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			byte b = (byte)(i + (int)this.m_bScale);
			byte b2 = (byte)Math.Min((int)BinXmlSqlDecimal.NUMERIC_MAX_PRECISION, Math.Max(1, i + (int)this.m_bPrec));
			if (i > 0)
			{
				this.m_bScale = b;
				this.m_bPrec = b2;
				while (i > 0)
				{
					uint num;
					if (i >= 9)
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[8];
						i -= 9;
					}
					else
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[i - 1];
						i = 0;
					}
					this.MultByULong(num);
				}
			}
			else if (i < 0)
			{
				uint num;
				uint num2;
				do
				{
					if (i <= -9)
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[8];
						i += 9;
					}
					else
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[-i - 1];
						i = 0;
					}
					num2 = this.DivByULong(num);
				}
				while (i < 0);
				flag = num2 >= num / 2U;
				this.m_bScale = b;
				this.m_bPrec = b2;
			}
			if (flag && fRound)
			{
				this.AddULong(1U);
				return;
			}
			if (this.FZero())
			{
				this.m_bSign = 0;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000F084 File Offset: 0x0000D284
		private void AddULong(uint ulAdd)
		{
			ulong num = (ulong)ulAdd;
			int bLen = (int)this.m_bLen;
			uint[] array = new uint[] { this.m_data1, this.m_data2, this.m_data3, this.m_data4 };
			int num2 = 0;
			for (;;)
			{
				num += (ulong)array[num2];
				array[num2] = (uint)num;
				num >>= 32;
				if (num == 0UL)
				{
					break;
				}
				num2++;
				if (num2 >= bLen)
				{
					goto Block_2;
				}
			}
			this.StoreFromWorkingArray(array);
			return;
			Block_2:
			if (num2 == BinXmlSqlDecimal.x_cNumeMax)
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			array[num2] = (uint)num;
			this.m_bLen += 1;
			if (this.FGt10_38(array))
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000F130 File Offset: 0x0000D330
		private void MultByULong(uint uiMultiplier)
		{
			int bLen = (int)this.m_bLen;
			ulong num = 0UL;
			uint[] array = new uint[] { this.m_data1, this.m_data2, this.m_data3, this.m_data4 };
			for (int i = 0; i < bLen; i++)
			{
				ulong num2 = (ulong)array[i] * (ulong)uiMultiplier;
				num += num2;
				if (num < num2)
				{
					num2 = BinXmlSqlDecimal.x_ulInt32Base;
				}
				else
				{
					num2 = 0UL;
				}
				array[i] = (uint)num;
				num = (num >> 32) + num2;
			}
			if (num != 0UL)
			{
				if (bLen == BinXmlSqlDecimal.x_cNumeMax)
				{
					throw new XmlException("Arithmetic Overflow.", null);
				}
				array[bLen] = (uint)num;
				this.m_bLen += 1;
			}
			if (this.FGt10_38(array))
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		internal uint DivByULong(uint iDivisor)
		{
			ulong num = (ulong)iDivisor;
			ulong num2 = 0UL;
			bool flag = true;
			if (num == 0UL)
			{
				throw new XmlException("Divide by zero error encountered.", null);
			}
			uint[] array = new uint[] { this.m_data1, this.m_data2, this.m_data3, this.m_data4 };
			for (int i = (int)this.m_bLen; i > 0; i--)
			{
				num2 = (num2 << 32) + (ulong)array[i - 1];
				uint num3 = (uint)(num2 / num);
				array[i - 1] = num3;
				num2 %= num;
				flag = flag && num3 == 0U;
				if (flag)
				{
					this.m_bLen -= 1;
				}
			}
			this.StoreFromWorkingArray(array);
			if (flag)
			{
				this.m_bLen = 1;
			}
			return (uint)num2;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000F2AF File Offset: 0x0000D4AF
		private static byte CLenFromPrec(byte bPrec)
		{
			return BinXmlSqlDecimal.rgCLenFromPrec[(int)(bPrec - 1)];
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000F2BA File Offset: 0x0000D4BA
		private static char ChFromDigit(uint uiDigit)
		{
			return (char)(uiDigit + 48U);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		public decimal ToDecimal()
		{
			if (this.m_data4 != 0U || this.m_bScale > 28)
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			return new decimal((int)this.m_data1, (int)this.m_data2, (int)this.m_data3, !this.IsPositive, this.m_bScale);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000F318 File Offset: 0x0000D518
		private void TrimTrailingZeros()
		{
			uint[] array = new uint[] { this.m_data1, this.m_data2, this.m_data3, this.m_data4 };
			int bLen = (int)this.m_bLen;
			if (bLen == 1 && array[0] == 0U)
			{
				this.m_bScale = 0;
				return;
			}
			while (this.m_bScale > 0 && (bLen > 1 || array[0] != 0U))
			{
				uint num;
				BinXmlSqlDecimal.MpDiv1(array, ref bLen, 10U, out num);
				if (num != 0U)
				{
					break;
				}
				this.m_data1 = array[0];
				this.m_data2 = array[1];
				this.m_data3 = array[2];
				this.m_data4 = array[3];
				this.m_bScale -= 1;
			}
			if (this.m_bLen == 4 && this.m_data4 == 0U)
			{
				this.m_bLen = 3;
			}
			if (this.m_bLen == 3 && this.m_data3 == 0U)
			{
				this.m_bLen = 2;
			}
			if (this.m_bLen == 2 && this.m_data2 == 0U)
			{
				this.m_bLen = 1;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000F404 File Offset: 0x0000D604
		public override string ToString()
		{
			uint[] array = new uint[] { this.m_data1, this.m_data2, this.m_data3, this.m_data4 };
			int bLen = (int)this.m_bLen;
			char[] array2 = new char[(int)(BinXmlSqlDecimal.NUMERIC_MAX_PRECISION + 1)];
			int i = 0;
			while (bLen > 1 || array[0] != 0U)
			{
				uint num;
				BinXmlSqlDecimal.MpDiv1(array, ref bLen, 10U, out num);
				array2[i++] = BinXmlSqlDecimal.ChFromDigit(num);
			}
			while (i <= (int)this.m_bScale)
			{
				array2[i++] = BinXmlSqlDecimal.ChFromDigit(0U);
			}
			bool isPositive = this.IsPositive;
			int num2 = (isPositive ? i : (i + 1));
			if (this.m_bScale > 0)
			{
				num2++;
			}
			char[] array3 = new char[num2];
			int num3 = 0;
			if (!isPositive)
			{
				array3[num3++] = '-';
			}
			while (i > 0)
			{
				if (i-- == (int)this.m_bScale)
				{
					array3[num3++] = '.';
				}
				array3[num3++] = array2[i];
			}
			return new string(array3);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000F4FC File Offset: 0x0000D6FC
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			object obj = (new uint[] { this.m_data1, this.m_data2, this.m_data3, this.m_data4 })[(int)(this.m_bLen - 1)];
			for (int i = (int)this.m_bLen; i < BinXmlSqlDecimal.x_cNumeMax; i++)
			{
			}
		}

		// Token: 0x04000281 RID: 641
		internal byte m_bLen;

		// Token: 0x04000282 RID: 642
		internal byte m_bPrec;

		// Token: 0x04000283 RID: 643
		internal byte m_bScale;

		// Token: 0x04000284 RID: 644
		internal byte m_bSign;

		// Token: 0x04000285 RID: 645
		internal uint m_data1;

		// Token: 0x04000286 RID: 646
		internal uint m_data2;

		// Token: 0x04000287 RID: 647
		internal uint m_data3;

		// Token: 0x04000288 RID: 648
		internal uint m_data4;

		// Token: 0x04000289 RID: 649
		private static readonly byte NUMERIC_MAX_PRECISION = 38;

		// Token: 0x0400028A RID: 650
		private static readonly byte MaxPrecision = BinXmlSqlDecimal.NUMERIC_MAX_PRECISION;

		// Token: 0x0400028B RID: 651
		private static readonly byte MaxScale = BinXmlSqlDecimal.NUMERIC_MAX_PRECISION;

		// Token: 0x0400028C RID: 652
		private static readonly int x_cNumeMax = 4;

		// Token: 0x0400028D RID: 653
		private static readonly long x_lInt32Base = 4294967296L;

		// Token: 0x0400028E RID: 654
		private static readonly ulong x_ulInt32Base = 4294967296UL;

		// Token: 0x0400028F RID: 655
		private static readonly ulong x_ulInt32BaseForMod = BinXmlSqlDecimal.x_ulInt32Base - 1UL;

		// Token: 0x04000290 RID: 656
		internal static readonly ulong x_llMax = 9223372036854775807UL;

		// Token: 0x04000291 RID: 657
		private static readonly double DUINT_BASE = (double)BinXmlSqlDecimal.x_lInt32Base;

		// Token: 0x04000292 RID: 658
		private static readonly double DUINT_BASE2 = BinXmlSqlDecimal.DUINT_BASE * BinXmlSqlDecimal.DUINT_BASE;

		// Token: 0x04000293 RID: 659
		private static readonly double DUINT_BASE3 = BinXmlSqlDecimal.DUINT_BASE2 * BinXmlSqlDecimal.DUINT_BASE;

		// Token: 0x04000294 RID: 660
		private static readonly uint[] x_rgulShiftBase = new uint[] { 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U };

		// Token: 0x04000295 RID: 661
		private static readonly byte[] rgCLenFromPrec = new byte[]
		{
			1, 1, 1, 1, 1, 1, 1, 1, 1, 2,
			2, 2, 2, 2, 2, 2, 2, 2, 2, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4
		};
	}
}
