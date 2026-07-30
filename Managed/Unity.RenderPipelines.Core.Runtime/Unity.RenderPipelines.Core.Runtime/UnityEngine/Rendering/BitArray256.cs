using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x02000055 RID: 85
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray256 : IBitArray
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009A11 File Offset: 0x00007C11
		public uint capacity
		{
			get
			{
				return 256U;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009A18 File Offset: 0x00007C18
		public bool allFalse
		{
			get
			{
				return this.data1 == 0UL && this.data2 == 0UL && this.data3 == 0UL && this.data4 == 0UL;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00009A3E File Offset: 0x00007C3E
		public bool allTrue
		{
			get
			{
				return this.data1 == ulong.MaxValue && this.data2 == ulong.MaxValue && this.data3 == ulong.MaxValue && this.data4 == ulong.MaxValue;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009A6C File Offset: 0x00007C6C
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + 64U + "}", Convert.ToString((long)this.data4, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U + "}", Convert.ToString((long)this.data3, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U + "}", Convert.ToString((long)this.data2, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U + "}", Convert.ToString((long)this.data1, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd(new char[] { '.' });
			}
		}

		// Token: 0x1700006A RID: 106
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get256(index, this.data1, this.data2, this.data3, this.data4);
			}
			set
			{
				BitArrayUtilities.Set256(index, ref this.data1, ref this.data2, ref this.data3, ref this.data4, value);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009BCB File Offset: 0x00007DCB
		public BitArray256(ulong initValue1, ulong initValue2, ulong initValue3, ulong initValue4)
		{
			this.data1 = initValue1;
			this.data2 = initValue2;
			this.data3 = initValue3;
			this.data4 = initValue4;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009BEC File Offset: 0x00007DEC
		public BitArray256(IEnumerable<uint> bitIndexTrue)
		{
			this.data1 = (this.data2 = (this.data3 = (this.data4 = 0UL)));
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < 64U)
				{
					this.data1 |= 1UL << (int)num;
				}
				else if (num < 128U)
				{
					this.data2 |= 1UL << (int)(num - 64U);
				}
				else if (num < 192U)
				{
					this.data3 |= 1UL << (int)(num - 128U);
				}
				else if (num < this.capacity)
				{
					this.data4 |= 1UL << (int)(num - 192U);
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009CC4 File Offset: 0x00007EC4
		public static BitArray256 operator ~(BitArray256 a)
		{
			return new BitArray256(~a.data1, ~a.data2, ~a.data3, ~a.data4);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009CE7 File Offset: 0x00007EE7
		public static BitArray256 operator |(BitArray256 a, BitArray256 b)
		{
			return new BitArray256(a.data1 | b.data1, a.data2 | b.data2, a.data3 | b.data3, a.data4 | b.data4);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009D22 File Offset: 0x00007F22
		public static BitArray256 operator &(BitArray256 a, BitArray256 b)
		{
			return new BitArray256(a.data1 & b.data1, a.data2 & b.data2, a.data3 & b.data3, a.data4 & b.data4);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009D5D File Offset: 0x00007F5D
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray256)other;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009D75 File Offset: 0x00007F75
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray256)other;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009D8D File Offset: 0x00007F8D
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009D9F File Offset: 0x00007F9F
		public static bool operator ==(BitArray256 a, BitArray256 b)
		{
			return a.data1 == b.data1 && a.data2 == b.data2 && a.data3 == b.data3 && a.data4 == b.data4;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009DDB File Offset: 0x00007FDB
		public static bool operator !=(BitArray256 a, BitArray256 b)
		{
			return a.data1 != b.data1 || a.data2 != b.data2 || a.data3 != b.data3 || a.data4 != b.data4;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009E1C File Offset: 0x0000801C
		public override bool Equals(object obj)
		{
			return obj is BitArray256 && this.data1.Equals(((BitArray256)obj).data1) && this.data2.Equals(((BitArray256)obj).data2) && this.data3.Equals(((BitArray256)obj).data3) && this.data4.Equals(((BitArray256)obj).data4);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009E94 File Offset: 0x00008094
		public override int GetHashCode()
		{
			return (((1870826326 * -1521134295 + this.data1.GetHashCode()) * -1521134295 + this.data2.GetHashCode()) * -1521134295 + this.data3.GetHashCode()) * -1521134295 + this.data4.GetHashCode();
		}

		// Token: 0x0400016D RID: 365
		[SerializeField]
		private ulong data1;

		// Token: 0x0400016E RID: 366
		[SerializeField]
		private ulong data2;

		// Token: 0x0400016F RID: 367
		[SerializeField]
		private ulong data3;

		// Token: 0x04000170 RID: 368
		[SerializeField]
		private ulong data4;
	}
}
