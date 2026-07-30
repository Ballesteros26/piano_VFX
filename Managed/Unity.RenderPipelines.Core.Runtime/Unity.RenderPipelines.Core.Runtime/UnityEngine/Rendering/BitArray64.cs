using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x02000053 RID: 83
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray64 : IBitArray
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009584 File Offset: 0x00007784
		public uint capacity
		{
			get
			{
				return 64U;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00009588 File Offset: 0x00007788
		public bool allFalse
		{
			get
			{
				return this.data == 0UL;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009594 File Offset: 0x00007794
		public bool allTrue
		{
			get
			{
				return this.data == ulong.MaxValue;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000095A0 File Offset: 0x000077A0
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity + "}", Convert.ToString((long)this.data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd(new char[] { '.' });
			}
		}

		// Token: 0x17000060 RID: 96
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get64(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set64(index, ref this.data, value);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000961D File Offset: 0x0000781D
		public BitArray64(ulong initValue)
		{
			this.data = initValue;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009628 File Offset: 0x00007828
		public BitArray64(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0UL;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= 1UL << (int)num;
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009679 File Offset: 0x00007879
		public static BitArray64 operator ~(BitArray64 a)
		{
			return new BitArray64(~a.data);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009687 File Offset: 0x00007887
		public static BitArray64 operator |(BitArray64 a, BitArray64 b)
		{
			return new BitArray64(a.data | b.data);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000969B File Offset: 0x0000789B
		public static BitArray64 operator &(BitArray64 a, BitArray64 b)
		{
			return new BitArray64(a.data & b.data);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000096AF File Offset: 0x000078AF
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray64)other;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000096C7 File Offset: 0x000078C7
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray64)other;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000096DF File Offset: 0x000078DF
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000096F1 File Offset: 0x000078F1
		public static bool operator ==(BitArray64 a, BitArray64 b)
		{
			return a.data == b.data;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009701 File Offset: 0x00007901
		public static bool operator !=(BitArray64 a, BitArray64 b)
		{
			return a.data != b.data;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009714 File Offset: 0x00007914
		public override bool Equals(object obj)
		{
			return obj is BitArray64 && ((BitArray64)obj).data == this.data;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009733 File Offset: 0x00007933
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private ulong data;
	}
}
