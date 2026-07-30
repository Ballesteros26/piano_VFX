using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x02000050 RID: 80
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray8 : IBitArray
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009050 File Offset: 0x00007250
		public uint capacity
		{
			get
			{
				return 8U;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00009053 File Offset: 0x00007253
		public bool allFalse
		{
			get
			{
				return this.data == 0;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000905E File Offset: 0x0000725E
		public bool allTrue
		{
			get
			{
				return this.data == byte.MaxValue;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000906D File Offset: 0x0000726D
		public string humanizedData
		{
			get
			{
				return string.Format("{0, " + this.capacity + "}", Convert.ToString(this.data, 2)).Replace(' ', '0');
			}
		}

		// Token: 0x17000050 RID: 80
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get8(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set8(index, ref this.data, value);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000090C0 File Offset: 0x000072C0
		public BitArray8(byte initValue)
		{
			this.data = initValue;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000090CC File Offset: 0x000072CC
		public BitArray8(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= (byte)(1 << (int)num);
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000911D File Offset: 0x0000731D
		public static BitArray8 operator ~(BitArray8 a)
		{
			return new BitArray8(~a.data);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000912C File Offset: 0x0000732C
		public static BitArray8 operator |(BitArray8 a, BitArray8 b)
		{
			return new BitArray8(a.data | b.data);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009141 File Offset: 0x00007341
		public static BitArray8 operator &(BitArray8 a, BitArray8 b)
		{
			return new BitArray8(a.data & b.data);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009156 File Offset: 0x00007356
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray8)other;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000916E File Offset: 0x0000736E
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray8)other;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009186 File Offset: 0x00007386
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009198 File Offset: 0x00007398
		public static bool operator ==(BitArray8 a, BitArray8 b)
		{
			return a.data == b.data;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000091A8 File Offset: 0x000073A8
		public static bool operator !=(BitArray8 a, BitArray8 b)
		{
			return a.data != b.data;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000091BB File Offset: 0x000073BB
		public override bool Equals(object obj)
		{
			return obj is BitArray8 && ((BitArray8)obj).data == this.data;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000091DA File Offset: 0x000073DA
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000167 RID: 359
		[SerializeField]
		private byte data;
	}
}
