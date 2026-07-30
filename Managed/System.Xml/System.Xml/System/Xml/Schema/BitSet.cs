using System;

namespace System.Xml.Schema
{
	// Token: 0x0200038C RID: 908
	internal sealed class BitSet
	{
		// Token: 0x060024CE RID: 9422 RVA: 0x000020FD File Offset: 0x000002FD
		private BitSet()
		{
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000DF148 File Offset: 0x000DD348
		public BitSet(int count)
		{
			this.count = count;
			this.bits = new uint[this.Subscript(count + 31)];
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000DF16C File Offset: 0x000DD36C
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000752 RID: 1874
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000DF180 File Offset: 0x000DD380
		public void Clear()
		{
			int num = this.bits.Length;
			while (num-- > 0)
			{
				this.bits[num] = 0U;
			}
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000DF1AC File Offset: 0x000DD3AC
		public void Clear(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] &= ~(1U << index);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000DF1E4 File Offset: 0x000DD3E4
		public void Set(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] |= 1U << index;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000DF21C File Offset: 0x000DD41C
		public bool Get(int index)
		{
			bool flag = false;
			if (index < this.count)
			{
				int num = this.Subscript(index);
				flag = ((ulong)this.bits[num] & (ulong)(1L << (index & 31 & 31))) > 0UL;
			}
			return flag;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000DF258 File Offset: 0x000DD458
		public int NextSet(int startFrom)
		{
			int num = startFrom + 1;
			if (num == this.count)
			{
				return -1;
			}
			int num2 = this.Subscript(num);
			num &= 31;
			uint num3;
			for (num3 = this.bits[num2] >> num; num3 == 0U; num3 = this.bits[num2])
			{
				if (++num2 == this.bits.Length)
				{
					return -1;
				}
				num = 0;
			}
			while ((num3 & 1U) == 0U)
			{
				num3 >>= 1;
				num++;
			}
			return (num2 << 5) + num;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000DF2C4 File Offset: 0x000DD4C4
		public void And(BitSet other)
		{
			if (this == other)
			{
				return;
			}
			int num = this.bits.Length;
			int num2 = other.bits.Length;
			int i = ((num > num2) ? num2 : num);
			int num3 = i;
			while (num3-- > 0)
			{
				this.bits[num3] &= other.bits[num3];
			}
			while (i < num)
			{
				this.bits[i] = 0U;
				i++;
			}
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000DF328 File Offset: 0x000DD528
		public void Or(BitSet other)
		{
			if (this == other)
			{
				return;
			}
			int num = other.bits.Length;
			this.EnsureLength(num);
			int num2 = num;
			while (num2-- > 0)
			{
				this.bits[num2] |= other.bits[num2];
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000DF370 File Offset: 0x000DD570
		public override int GetHashCode()
		{
			int num = 1234;
			int num2 = this.bits.Length;
			while (--num2 >= 0)
			{
				num ^= (int)(this.bits[num2] * (uint)(num2 + 1));
			}
			return num ^ num;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000DF3A8 File Offset: 0x000DD5A8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			BitSet bitSet = (BitSet)obj;
			int num = this.bits.Length;
			int num2 = bitSet.bits.Length;
			int num3 = ((num > num2) ? num2 : num);
			int num4 = num3;
			while (num4-- > 0)
			{
				if (this.bits[num4] != bitSet.bits[num4])
				{
					return false;
				}
			}
			if (num > num3)
			{
				int num5 = num;
				while (num5-- > num3)
				{
					if (this.bits[num5] != 0U)
					{
						return false;
					}
				}
			}
			else
			{
				int num6 = num2;
				while (num6-- > num3)
				{
					if (bitSet.bits[num6] != 0U)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000DF449 File Offset: 0x000DD649
		public BitSet Clone()
		{
			return new BitSet
			{
				count = this.count,
				bits = (uint[])this.bits.Clone()
			};
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x000DF474 File Offset: 0x000DD674
		public bool IsEmpty
		{
			get
			{
				uint num = 0U;
				for (int i = 0; i < this.bits.Length; i++)
				{
					num |= this.bits[i];
				}
				return num == 0U;
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000DF4A8 File Offset: 0x000DD6A8
		public bool Intersects(BitSet other)
		{
			int num = Math.Min(this.bits.Length, other.bits.Length);
			while (--num >= 0)
			{
				if ((this.bits[num] & other.bits[num]) != 0U)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000DF4EB File Offset: 0x000DD6EB
		private int Subscript(int bitIndex)
		{
			return bitIndex >> 5;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000DF4F0 File Offset: 0x000DD6F0
		private void EnsureLength(int nRequiredLength)
		{
			if (nRequiredLength > this.bits.Length)
			{
				int num = 2 * this.bits.Length;
				if (num < nRequiredLength)
				{
					num = nRequiredLength;
				}
				uint[] array = new uint[num];
				Array.Copy(this.bits, array, this.bits.Length);
				this.bits = array;
			}
		}

		// Token: 0x040018F2 RID: 6386
		private const int bitSlotShift = 5;

		// Token: 0x040018F3 RID: 6387
		private const int bitSlotMask = 31;

		// Token: 0x040018F4 RID: 6388
		private int count;

		// Token: 0x040018F5 RID: 6389
		private uint[] bits;
	}
}
