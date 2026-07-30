using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x02000054 RID: 84
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray128 : IBitArray
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009746 File Offset: 0x00007946
		public uint capacity
		{
			get
			{
				return 128U;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000974D File Offset: 0x0000794D
		public bool allFalse
		{
			get
			{
				return this.data1 == 0UL && this.data2 == 0UL;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009763 File Offset: 0x00007963
		public bool allTrue
		{
			get
			{
				return this.data1 == ulong.MaxValue && this.data2 == ulong.MaxValue;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000977C File Offset: 0x0000797C
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + 64U + "}", Convert.ToString((long)this.data2, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U + "}", Convert.ToString((long)this.data1, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd(new char[] { '.' });
			}
		}

		// Token: 0x17000065 RID: 101
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get128(index, this.data1, this.data2);
			}
			set
			{
				BitArrayUtilities.Set128(index, ref this.data1, ref this.data2, value);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009845 File Offset: 0x00007A45
		public BitArray128(ulong initValue1, ulong initValue2)
		{
			this.data1 = initValue1;
			this.data2 = initValue2;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009858 File Offset: 0x00007A58
		public BitArray128(IEnumerable<uint> bitIndexTrue)
		{
			this.data1 = (this.data2 = 0UL);
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
				else if (num < this.capacity)
				{
					this.data2 |= 1UL << (int)(num - 64U);
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000098D0 File Offset: 0x00007AD0
		public static BitArray128 operator ~(BitArray128 a)
		{
			return new BitArray128(~a.data1, ~a.data2);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000098E5 File Offset: 0x00007AE5
		public static BitArray128 operator |(BitArray128 a, BitArray128 b)
		{
			return new BitArray128(a.data1 | b.data1, a.data2 | b.data2);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009906 File Offset: 0x00007B06
		public static BitArray128 operator &(BitArray128 a, BitArray128 b)
		{
			return new BitArray128(a.data1 & b.data1, a.data2 & b.data2);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009927 File Offset: 0x00007B27
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray128)other;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000993F File Offset: 0x00007B3F
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray128)other;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009957 File Offset: 0x00007B57
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009969 File Offset: 0x00007B69
		public static bool operator ==(BitArray128 a, BitArray128 b)
		{
			return a.data1 == b.data1 && a.data2 == b.data2;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009989 File Offset: 0x00007B89
		public static bool operator !=(BitArray128 a, BitArray128 b)
		{
			return a.data1 != b.data1 || a.data2 != b.data2;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000099AC File Offset: 0x00007BAC
		public override bool Equals(object obj)
		{
			return obj is BitArray128 && this.data1.Equals(((BitArray128)obj).data1) && this.data2.Equals(((BitArray128)obj).data2);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000099E6 File Offset: 0x00007BE6
		public override int GetHashCode()
		{
			return (1755735569 * -1521134295 + this.data1.GetHashCode()) * -1521134295 + this.data2.GetHashCode();
		}

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private ulong data1;

		// Token: 0x0400016C RID: 364
		[SerializeField]
		private ulong data2;
	}
}
