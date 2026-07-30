using System;
using System.Text;

namespace System.Collections.Specialized
{
	/// <summary>Provides a simple structure that stores Boolean values and small integers in 32 bits of memory.</summary>
	// Token: 0x020006F2 RID: 1778
	public struct BitVector32
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.BitVector32" /> structure containing the data represented in an integer.</summary>
		/// <param name="data">An integer representing the data of the new <see cref="T:System.Collections.Specialized.BitVector32" />. </param>
		// Token: 0x060037B0 RID: 14256 RVA: 0x000CD4CC File Offset: 0x000CB6CC
		public BitVector32(int data)
		{
			this.data = (uint)data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.BitVector32" /> structure containing the data represented in an existing <see cref="T:System.Collections.Specialized.BitVector32" /> structure.</summary>
		/// <param name="value">A <see cref="T:System.Collections.Specialized.BitVector32" /> structure that contains the data to copy. </param>
		// Token: 0x060037B1 RID: 14257 RVA: 0x000CD4D5 File Offset: 0x000CB6D5
		public BitVector32(BitVector32 value)
		{
			this.data = value.data;
		}

		/// <summary>Gets or sets the state of the bit flag indicated by the specified mask.</summary>
		/// <returns>true if the specified bit flag is on (1); otherwise, false.</returns>
		/// <param name="bit">A mask that indicates the bit to get or set. </param>
		// Token: 0x17000D6E RID: 3438
		public bool this[int bit]
		{
			get
			{
				return ((ulong)this.data & (ulong)((long)bit)) == (ulong)bit;
			}
			set
			{
				if (value)
				{
					this.data |= (uint)bit;
					return;
				}
				this.data &= (uint)(~(uint)bit);
			}
		}

		/// <summary>Gets or sets the value stored in the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
		/// <returns>The value stored in the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
		/// <param name="section">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that contains the value to get or set. </param>
		// Token: 0x17000D6F RID: 3439
		public int this[BitVector32.Section section]
		{
			get
			{
				return (int)((this.data & (uint)((uint)section.Mask << (int)section.Offset)) >> (int)section.Offset);
			}
			set
			{
				value <<= (int)section.Offset;
				int num = (65535 & (int)section.Mask) << (int)section.Offset;
				this.data = (this.data & (uint)(~(uint)num)) | (uint)(value & num);
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Collections.Specialized.BitVector32" /> as an integer.</summary>
		/// <returns>The value of the <see cref="T:System.Collections.Specialized.BitVector32" /> as an integer.</returns>
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000CD583 File Offset: 0x000CB783
		public int Data
		{
			get
			{
				return (int)this.data;
			}
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000CD58C File Offset: 0x000CB78C
		private static short CountBitsSet(short mask)
		{
			short num = 0;
			while ((mask & 1) != 0)
			{
				num += 1;
				mask = (short)(mask >> 1);
			}
			return num;
		}

		/// <summary>Creates the first mask in a series of masks that can be used to retrieve individual bits in a <see cref="T:System.Collections.Specialized.BitVector32" /> that is set up as bit flags.</summary>
		/// <returns>A mask that isolates the first bit flag in the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060037B8 RID: 14264 RVA: 0x000CD5AE File Offset: 0x000CB7AE
		public static int CreateMask()
		{
			return BitVector32.CreateMask(0);
		}

		/// <summary>Creates an additional mask following the specified mask in a series of masks that can be used to retrieve individual bits in a <see cref="T:System.Collections.Specialized.BitVector32" /> that is set up as bit flags.</summary>
		/// <returns>A mask that isolates the bit flag following the one that <paramref name="previous" /> points to in <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <param name="previous">The mask that indicates the previous bit flag. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previous" /> indicates the last bit flag in the <see cref="T:System.Collections.Specialized.BitVector32" />. </exception>
		// Token: 0x060037B9 RID: 14265 RVA: 0x000CD5B6 File Offset: 0x000CB7B6
		public static int CreateMask(int previous)
		{
			if (previous == 0)
			{
				return 1;
			}
			if (previous == -2147483648)
			{
				throw new InvalidOperationException(global::SR.GetString("Bit vector is full."));
			}
			return previous << 1;
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000CD5D8 File Offset: 0x000CB7D8
		private static short CreateMaskFromHighValue(short highValue)
		{
			short num = 16;
			while (((int)highValue & 32768) == 0)
			{
				num -= 1;
				highValue = (short)(highValue << 1);
			}
			ushort num2 = 0;
			while (num > 0)
			{
				num -= 1;
				num2 = (ushort)(num2 << 1);
				num2 |= 1;
			}
			return (short)num2;
		}

		/// <summary>Creates the first <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in a series of sections that contain small integers.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that can hold a number from zero to <paramref name="maxValue" />.</returns>
		/// <param name="maxValue">A 16-bit signed integer that specifies the maximum value for the new <see cref="T:System.Collections.Specialized.BitVector32.Section" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="maxValue" /> is less than 1. </exception>
		// Token: 0x060037BB RID: 14267 RVA: 0x000CD617 File Offset: 0x000CB817
		public static BitVector32.Section CreateSection(short maxValue)
		{
			return BitVector32.CreateSectionHelper(maxValue, 0, 0);
		}

		/// <summary>Creates a new <see cref="T:System.Collections.Specialized.BitVector32.Section" /> following the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in a series of sections that contain small integers.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> that can hold a number from zero to <paramref name="maxValue" />.</returns>
		/// <param name="maxValue">A 16-bit signed integer that specifies the maximum value for the new <see cref="T:System.Collections.Specialized.BitVector32.Section" />. </param>
		/// <param name="previous">The previous <see cref="T:System.Collections.Specialized.BitVector32.Section" /> in the <see cref="T:System.Collections.Specialized.BitVector32" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="maxValue" /> is less than 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previous" /> includes the final bit in the <see cref="T:System.Collections.Specialized.BitVector32" />.-or- <paramref name="maxValue" /> is greater than the highest value that can be represented by the number of bits after <paramref name="previous" />. </exception>
		// Token: 0x060037BC RID: 14268 RVA: 0x000CD621 File Offset: 0x000CB821
		public static BitVector32.Section CreateSection(short maxValue, BitVector32.Section previous)
		{
			return BitVector32.CreateSectionHelper(maxValue, previous.Mask, previous.Offset);
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000CD638 File Offset: 0x000CB838
		private static BitVector32.Section CreateSectionHelper(short maxValue, short priorMask, short priorOffset)
		{
			if (maxValue < 1)
			{
				throw new ArgumentException(global::SR.GetString("Argument {0} should be larger than {1}.", new object[] { "maxValue", 0 }), "maxValue");
			}
			short num = priorOffset + BitVector32.CountBitsSet(priorMask);
			if (num >= 32)
			{
				throw new InvalidOperationException(global::SR.GetString("Bit vector is full."));
			}
			return new BitVector32.Section(BitVector32.CreateMaskFromHighValue(maxValue), num);
		}

		/// <summary>Determines whether the specified object is equal to the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>true if the specified object is equal to the <see cref="T:System.Collections.Specialized.BitVector32" />; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32" />. </param>
		// Token: 0x060037BE RID: 14270 RVA: 0x000CD6A0 File Offset: 0x000CB8A0
		public override bool Equals(object o)
		{
			return o is BitVector32 && this.data == ((BitVector32)o).data;
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A hash code for the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x060037BF RID: 14271 RVA: 0x000CD6BF File Offset: 0x000CB8BF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		/// <param name="value">The <see cref="T:System.Collections.Specialized.BitVector32" /> to represent. </param>
		// Token: 0x060037C0 RID: 14272 RVA: 0x000CD6D4 File Offset: 0x000CB8D4
		public static string ToString(BitVector32 value)
		{
			StringBuilder stringBuilder = new StringBuilder(45);
			stringBuilder.Append("BitVector32{");
			int num = (int)value.data;
			for (int i = 0; i < 32; i++)
			{
				if (((long)num & (long)((ulong)(-2147483648))) != 0L)
				{
					stringBuilder.Append("1");
				}
				else
				{
					stringBuilder.Append("0");
				}
				num <<= 1;
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
		// Token: 0x060037C1 RID: 14273 RVA: 0x000CD744 File Offset: 0x000CB944
		public override string ToString()
		{
			return BitVector32.ToString(this);
		}

		// Token: 0x04002C1F RID: 11295
		private uint data;

		/// <summary>Represents a section of the vector that can contain an integer number.</summary>
		// Token: 0x020006F3 RID: 1779
		public struct Section
		{
			// Token: 0x060037C2 RID: 14274 RVA: 0x000CD751 File Offset: 0x000CB951
			internal Section(short mask, short offset)
			{
				this.mask = mask;
				this.offset = offset;
			}

			/// <summary>Gets a mask that isolates this section within the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
			/// <returns>A mask that isolates this section within the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
			// Token: 0x17000D71 RID: 3441
			// (get) Token: 0x060037C3 RID: 14275 RVA: 0x000CD761 File Offset: 0x000CB961
			public short Mask
			{
				get
				{
					return this.mask;
				}
			}

			/// <summary>Gets the offset of this section from the start of the <see cref="T:System.Collections.Specialized.BitVector32" />.</summary>
			/// <returns>The offset of this section from the start of the <see cref="T:System.Collections.Specialized.BitVector32" />.</returns>
			// Token: 0x17000D72 RID: 3442
			// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000CD769 File Offset: 0x000CB969
			public short Offset
			{
				get
				{
					return this.offset;
				}
			}

			/// <summary>Determines whether the specified object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</summary>
			/// <returns>true if the specified object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object; otherwise, false.</returns>
			/// <param name="o">The object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</param>
			// Token: 0x060037C5 RID: 14277 RVA: 0x000CD771 File Offset: 0x000CB971
			public override bool Equals(object o)
			{
				return o is BitVector32.Section && this.Equals((BitVector32.Section)o);
			}

			/// <summary>Determines whether the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</summary>
			/// <returns>true if the <paramref name="obj" /> parameter is the same as the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object; otherwise false.</returns>
			/// <param name="obj">The <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object to compare with the current <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			// Token: 0x060037C6 RID: 14278 RVA: 0x000CD789 File Offset: 0x000CB989
			public bool Equals(BitVector32.Section obj)
			{
				return obj.mask == this.mask && obj.offset == this.offset;
			}

			/// <summary>Determines whether two specified <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects are equal.</summary>
			/// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters represent the same <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object, otherwise, false.</returns>
			/// <param name="a">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <param name="b">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			// Token: 0x060037C7 RID: 14279 RVA: 0x000CD7A9 File Offset: 0x000CB9A9
			public static bool operator ==(BitVector32.Section a, BitVector32.Section b)
			{
				return a.Equals(b);
			}

			/// <summary>Determines whether two <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects have different values.</summary>
			/// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters represent different <see cref="T:System.Collections.Specialized.BitVector32.Section" /> objects; otherwise, false.</returns>
			/// <param name="a">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			/// <param name="b">A <see cref="T:System.Collections.Specialized.BitVector32.Section" /> object.</param>
			// Token: 0x060037C8 RID: 14280 RVA: 0x000CD7B3 File Offset: 0x000CB9B3
			public static bool operator !=(BitVector32.Section a, BitVector32.Section b)
			{
				return !(a == b);
			}

			/// <summary>Serves as a hash function for the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
			/// <returns>A hash code for the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x060037C9 RID: 14281 RVA: 0x000CD7BF File Offset: 0x000CB9BF
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			/// <summary>Returns a string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
			/// <returns>A string that represents the specified <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			/// <param name="value">The <see cref="T:System.Collections.Specialized.BitVector32.Section" /> to represent.</param>
			// Token: 0x060037CA RID: 14282 RVA: 0x000CD7D4 File Offset: 0x000CB9D4
			public static string ToString(BitVector32.Section value)
			{
				return string.Concat(new string[]
				{
					"Section{0x",
					Convert.ToString(value.Mask, 16),
					", 0x",
					Convert.ToString(value.Offset, 16),
					"}"
				});
			}

			/// <summary>Returns a string that represents the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</summary>
			/// <returns>A string that represents the current <see cref="T:System.Collections.Specialized.BitVector32.Section" />.</returns>
			// Token: 0x060037CB RID: 14283 RVA: 0x000CD826 File Offset: 0x000CBA26
			public override string ToString()
			{
				return BitVector32.Section.ToString(this);
			}

			// Token: 0x04002C20 RID: 11296
			private readonly short mask;

			// Token: 0x04002C21 RID: 11297
			private readonly short offset;
		}
	}
}
