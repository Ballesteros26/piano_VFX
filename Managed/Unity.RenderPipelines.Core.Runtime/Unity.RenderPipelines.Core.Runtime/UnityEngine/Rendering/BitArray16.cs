using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x02000051 RID: 81
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray16 : IBitArray
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000091ED File Offset: 0x000073ED
		public uint capacity
		{
			get
			{
				return 16U;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000091F1 File Offset: 0x000073F1
		public bool allFalse
		{
			get
			{
				return this.data == 0;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000091FC File Offset: 0x000073FC
		public bool allTrue
		{
			get
			{
				return this.data == ushort.MaxValue;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000920C File Offset: 0x0000740C
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity + "}", Convert.ToString((int)this.data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd(new char[] { '.' });
			}
		}

		// Token: 0x17000055 RID: 85
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get16(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set16(index, ref this.data, value);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009289 File Offset: 0x00007489
		public BitArray16(ushort initValue)
		{
			this.data = initValue;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009294 File Offset: 0x00007494
		public BitArray16(IEnumerable<uint> bitIndexTrue)
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
					this.data |= (ushort)(1 << (int)num);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000092E5 File Offset: 0x000074E5
		public static BitArray16 operator ~(BitArray16 a)
		{
			return new BitArray16(~a.data);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000092F4 File Offset: 0x000074F4
		public static BitArray16 operator |(BitArray16 a, BitArray16 b)
		{
			return new BitArray16(a.data | b.data);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009309 File Offset: 0x00007509
		public static BitArray16 operator &(BitArray16 a, BitArray16 b)
		{
			return new BitArray16(a.data & b.data);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000931E File Offset: 0x0000751E
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray16)other;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009336 File Offset: 0x00007536
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray16)other;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000934E File Offset: 0x0000754E
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009360 File Offset: 0x00007560
		public static bool operator ==(BitArray16 a, BitArray16 b)
		{
			return a.data == b.data;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009370 File Offset: 0x00007570
		public static bool operator !=(BitArray16 a, BitArray16 b)
		{
			return a.data != b.data;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009383 File Offset: 0x00007583
		public override bool Equals(object obj)
		{
			return obj is BitArray16 && ((BitArray16)obj).data == this.data;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000093A2 File Offset: 0x000075A2
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000168 RID: 360
		[SerializeField]
		private ushort data;
	}
}
