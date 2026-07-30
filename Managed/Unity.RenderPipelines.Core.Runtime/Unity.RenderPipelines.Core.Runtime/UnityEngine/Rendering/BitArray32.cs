using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x02000052 RID: 82
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray32 : IBitArray
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000093B5 File Offset: 0x000075B5
		public uint capacity
		{
			get
			{
				return 32U;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000093B9 File Offset: 0x000075B9
		public bool allFalse
		{
			get
			{
				return this.data == 0U;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000093C4 File Offset: 0x000075C4
		public bool allTrue
		{
			get
			{
				return this.data == uint.MaxValue;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000093CF File Offset: 0x000075CF
		private string humanizedVersion
		{
			get
			{
				return Convert.ToString((long)((ulong)this.data), 2);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000093E0 File Offset: 0x000075E0
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity + "}", Convert.ToString((long)((ulong)this.data), 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd(new char[] { '.' });
			}
		}

		// Token: 0x1700005B RID: 91
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get32(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set32(index, ref this.data, value);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000945E File Offset: 0x0000765E
		public BitArray32(uint initValue)
		{
			this.data = initValue;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009468 File Offset: 0x00007668
		public BitArray32(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0U;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= 1U << (int)num;
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000094B7 File Offset: 0x000076B7
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray32)other;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000094CF File Offset: 0x000076CF
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray32)other;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000094E7 File Offset: 0x000076E7
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000094F9 File Offset: 0x000076F9
		public static BitArray32 operator ~(BitArray32 a)
		{
			return new BitArray32(~a.data);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009507 File Offset: 0x00007707
		public static BitArray32 operator |(BitArray32 a, BitArray32 b)
		{
			return new BitArray32(a.data | b.data);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000951B File Offset: 0x0000771B
		public static BitArray32 operator &(BitArray32 a, BitArray32 b)
		{
			return new BitArray32(a.data & b.data);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000952F File Offset: 0x0000772F
		public static bool operator ==(BitArray32 a, BitArray32 b)
		{
			return a.data == b.data;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000953F File Offset: 0x0000773F
		public static bool operator !=(BitArray32 a, BitArray32 b)
		{
			return a.data != b.data;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009552 File Offset: 0x00007752
		public override bool Equals(object obj)
		{
			return obj is BitArray32 && ((BitArray32)obj).data == this.data;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00009571 File Offset: 0x00007771
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000169 RID: 361
		[SerializeField]
		private uint data;
	}
}
