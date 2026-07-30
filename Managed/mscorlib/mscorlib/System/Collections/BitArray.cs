using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections
{
	/// <summary>Manages a compact array of bit values, which are represented as Booleans, where true indicates that the bit is on (1) and false indicates the bit is off (0).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020009B9 RID: 2489
	[ComVisible(true)]
	[Serializable]
	public sealed class BitArray : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06005BEE RID: 23534 RVA: 0x00002111 File Offset: 0x00000311
		private BitArray()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that can hold the specified number of bit values, which are initially set to false.</summary>
		/// <param name="length">The number of bit values in the new <see cref="T:System.Collections.BitArray" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero. </exception>
		// Token: 0x06005BEF RID: 23535 RVA: 0x0013027F File Offset: 0x0012E47F
		public BitArray(int length)
			: this(length, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that can hold the specified number of bit values, which are initially set to the specified value.</summary>
		/// <param name="length">The number of bit values in the new <see cref="T:System.Collections.BitArray" />. </param>
		/// <param name="defaultValue">The Boolean value to assign to each bit. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is less than zero. </exception>
		// Token: 0x06005BF0 RID: 23536 RVA: 0x0013028C File Offset: 0x0012E48C
		public BitArray(int length, bool defaultValue)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Non-negative number required."));
			}
			this.m_array = new int[BitArray.GetArrayLength(length, 32)];
			this.m_length = length;
			int num = (defaultValue ? (-1) : 0);
			for (int i = 0; i < this.m_array.Length; i++)
			{
				this.m_array[i] = num;
			}
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of bytes.</summary>
		/// <param name="bytes">An array of bytes containing the values to copy, where each byte represents eight consecutive bits. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="bytes" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06005BF1 RID: 23537 RVA: 0x00130300 File Offset: 0x0012E500
		public BitArray(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (bytes.Length > 268435455)
			{
				throw new ArgumentException(Environment.GetResourceString("The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.", new object[] { 8 }), "bytes");
			}
			this.m_array = new int[BitArray.GetArrayLength(bytes.Length, 4)];
			this.m_length = bytes.Length * 8;
			int num = 0;
			int num2 = 0;
			while (bytes.Length - num2 >= 4)
			{
				this.m_array[num++] = (int)(bytes[num2] & byte.MaxValue) | ((int)(bytes[num2 + 1] & byte.MaxValue) << 8) | ((int)(bytes[num2 + 2] & byte.MaxValue) << 16) | ((int)(bytes[num2 + 3] & byte.MaxValue) << 24);
				num2 += 4;
			}
			switch (bytes.Length - num2)
			{
			case 1:
				goto IL_0103;
			case 2:
				break;
			case 3:
				this.m_array[num] = (int)(bytes[num2 + 2] & byte.MaxValue) << 16;
				break;
			default:
				goto IL_011C;
			}
			this.m_array[num] |= (int)(bytes[num2 + 1] & byte.MaxValue) << 8;
			IL_0103:
			this.m_array[num] |= (int)(bytes[num2] & byte.MaxValue);
			IL_011C:
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of Booleans.</summary>
		/// <param name="values">An array of Booleans to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x06005BF2 RID: 23538 RVA: 0x00130430 File Offset: 0x0012E630
		public BitArray(bool[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.m_array = new int[BitArray.GetArrayLength(values.Length, 32)];
			this.m_length = values.Length;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i])
				{
					this.m_array[i / 32] |= 1 << i % 32;
				}
			}
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified array of 32-bit integers.</summary>
		/// <param name="values">An array of integers containing the values to copy, where each integer represents 32 consecutive bits. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="values" /> is greater than <see cref="F:System.Int32.MaxValue" /></exception>
		// Token: 0x06005BF3 RID: 23539 RVA: 0x001304A8 File Offset: 0x0012E6A8
		public BitArray(int[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length > 67108863)
			{
				throw new ArgumentException(Environment.GetResourceString("The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.", new object[] { 32 }), "values");
			}
			this.m_array = new int[values.Length];
			this.m_length = values.Length * 32;
			Array.Copy(values, this.m_array, values.Length);
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.BitArray" /> class that contains bit values copied from the specified <see cref="T:System.Collections.BitArray" />.</summary>
		/// <param name="bits">The <see cref="T:System.Collections.BitArray" /> to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bits" /> is null. </exception>
		// Token: 0x06005BF4 RID: 23540 RVA: 0x00130528 File Offset: 0x0012E728
		public BitArray(BitArray bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
			this.m_array = new int[arrayLength];
			this.m_length = bits.m_length;
			Array.Copy(bits.m_array, this.m_array, arrayLength);
			this._version = bits._version;
		}

		/// <summary>Gets or sets the value of the bit at a specific position in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The value of the bit at position <paramref name="index" />.</returns>
		/// <param name="index">The zero-based index of the value to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.BitArray.Count" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001005 RID: 4101
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		/// <summary>Gets the value of the bit at a specific position in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The value of the bit at position <paramref name="index" />.</returns>
		/// <param name="index">The zero-based index of the value to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005BF7 RID: 23543 RVA: 0x001305A0 File Offset: 0x0012E7A0
		public bool Get(int index)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			return (this.m_array[index / 32] & (1 << index % 32)) != 0;
		}

		/// <summary>Sets the bit at a specific position in the <see cref="T:System.Collections.BitArray" /> to the specified value.</summary>
		/// <param name="index">The zero-based index of the bit to set. </param>
		/// <param name="value">The Boolean value to assign to the bit. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005BF8 RID: 23544 RVA: 0x001305DC File Offset: 0x0012E7DC
		public void Set(int index, bool value)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (value)
			{
				this.m_array[index / 32] |= 1 << index % 32;
			}
			else
			{
				this.m_array[index / 32] &= ~(1 << index % 32);
			}
			this._version++;
		}

		/// <summary>Sets all bits in the <see cref="T:System.Collections.BitArray" /> to the specified value.</summary>
		/// <param name="value">The Boolean value to assign to all bits. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005BF9 RID: 23545 RVA: 0x00130658 File Offset: 0x0012E858
		public void SetAll(bool value)
		{
			int num = (value ? (-1) : 0);
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = num;
			}
			this._version++;
		}

		/// <summary>Performs the bitwise AND operation on the elements in the current <see cref="T:System.Collections.BitArray" /> against the corresponding elements in the specified <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The current instance containing the result of the bitwise AND operation on the elements in the current <see cref="T:System.Collections.BitArray" /> against the corresponding elements in the specified <see cref="T:System.Collections.BitArray" />.</returns>
		/// <param name="value">The <see cref="T:System.Collections.BitArray" /> with which to perform the bitwise AND operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005BFA RID: 23546 RVA: 0x001306A0 File Offset: 0x0012E8A0
		public BitArray And(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Array lengths must be the same."));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] &= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Performs the bitwise OR operation on the elements in the current <see cref="T:System.Collections.BitArray" /> against the corresponding elements in the specified <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The current instance containing the result of the bitwise OR operation on the elements in the current <see cref="T:System.Collections.BitArray" /> against the corresponding elements in the specified <see cref="T:System.Collections.BitArray" />.</returns>
		/// <param name="value">The <see cref="T:System.Collections.BitArray" /> with which to perform the bitwise OR operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005BFB RID: 23547 RVA: 0x0013071C File Offset: 0x0012E91C
		public BitArray Or(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Array lengths must be the same."));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] |= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Performs the bitwise exclusive OR operation on the elements in the current <see cref="T:System.Collections.BitArray" /> against the corresponding elements in the specified <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The current instance containing the result of the bitwise exclusive OR operation on the elements in the current <see cref="T:System.Collections.BitArray" /> against the corresponding elements in the specified <see cref="T:System.Collections.BitArray" />. </returns>
		/// <param name="value">The <see cref="T:System.Collections.BitArray" /> with which to perform the bitwise exclusive OR operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005BFC RID: 23548 RVA: 0x00130798 File Offset: 0x0012E998
		public BitArray Xor(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Array lengths must be the same."));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] ^= value.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Inverts all the bit values in the current <see cref="T:System.Collections.BitArray" />, so that elements set to true are changed to false, and elements set to false are changed to true.</summary>
		/// <returns>The current instance with inverted bit values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005BFD RID: 23549 RVA: 0x00130814 File Offset: 0x0012EA14
		public BitArray Not()
		{
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = ~this.m_array[i];
			}
			this._version++;
			return this;
		}

		/// <summary>Gets or sets the number of elements in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a value that is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06005BFE RID: 23550 RVA: 0x0013085B File Offset: 0x0012EA5B
		// (set) Token: 0x06005BFF RID: 23551 RVA: 0x00130864 File Offset: 0x0012EA64
		public int Length
		{
			get
			{
				return this.m_length;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Non-negative number required."));
				}
				int arrayLength = BitArray.GetArrayLength(value, 32);
				if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
				{
					int[] array = new int[arrayLength];
					Array.Copy(this.m_array, array, (arrayLength > this.m_array.Length) ? this.m_array.Length : arrayLength);
					this.m_array = array;
				}
				if (value > this.m_length)
				{
					int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
					int num2 = this.m_length % 32;
					if (num2 > 0)
					{
						this.m_array[num] &= (1 << num2) - 1;
					}
					Array.Clear(this.m_array, num + 1, arrayLength - num - 1);
				}
				this.m_length = value;
				this._version++;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.BitArray" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.BitArray" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.BitArray" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.BitArray" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C00 RID: 23552 RVA: 0x00130948 File Offset: 0x0012EB48
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			if (array is int[])
			{
				Array.Copy(this.m_array, 0, array, index, BitArray.GetArrayLength(this.m_length, 32));
				return;
			}
			if (array is byte[])
			{
				int arrayLength = BitArray.GetArrayLength(this.m_length, 8);
				if (array.Length - index < arrayLength)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				byte[] array2 = (byte[])array;
				for (int i = 0; i < arrayLength; i++)
				{
					array2[index + i] = (byte)((this.m_array[i / 4] >> i % 4 * 8) & 255);
				}
				return;
			}
			else
			{
				if (!(array is bool[]))
				{
					throw new ArgumentException(Environment.GetResourceString("Only supported array types for CopyTo on BitArrays are Boolean[], Int32[] and Byte[]."));
				}
				if (array.Length - index < this.m_length)
				{
					throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
				}
				bool[] array3 = (bool[])array;
				for (int j = 0; j < this.m_length; j++)
				{
					array3[index + j] = ((this.m_array[j / 32] >> j % 32) & 1) != 0;
				}
				return;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06005C01 RID: 23553 RVA: 0x0013085B File Offset: 0x0012EA5B
		public int Count
		{
			get
			{
				return this.m_length;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C02 RID: 23554 RVA: 0x00130A90 File Offset: 0x0012EC90
		public object Clone()
		{
			return new BitArray(this.m_array)
			{
				_version = this._version,
				m_length = this.m_length
			};
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.BitArray" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06005C03 RID: 23555 RVA: 0x00130AB5 File Offset: 0x0012ECB5
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.BitArray" /> is read-only.</summary>
		/// <returns>This property is always false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06005C04 RID: 23556 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.BitArray" /> is synchronized (thread safe).</summary>
		/// <returns>This property is always false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06005C05 RID: 23557 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.BitArray" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.BitArray" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C06 RID: 23558 RVA: 0x00130AD7 File Offset: 0x0012ECD7
		public IEnumerator GetEnumerator()
		{
			return new BitArray.BitArrayEnumeratorSimple(this);
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x00130ADF File Offset: 0x0012ECDF
		private static int GetArrayLength(int n, int div)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / div + 1;
		}

		// Token: 0x04002F24 RID: 12068
		private const int BitsPerInt32 = 32;

		// Token: 0x04002F25 RID: 12069
		private const int BytesPerInt32 = 4;

		// Token: 0x04002F26 RID: 12070
		private const int BitsPerByte = 8;

		// Token: 0x04002F27 RID: 12071
		private int[] m_array;

		// Token: 0x04002F28 RID: 12072
		private int m_length;

		// Token: 0x04002F29 RID: 12073
		private int _version;

		// Token: 0x04002F2A RID: 12074
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04002F2B RID: 12075
		private const int _ShrinkThreshold = 256;

		// Token: 0x020009BA RID: 2490
		[Serializable]
		private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06005C08 RID: 23560 RVA: 0x00130AEE File Offset: 0x0012ECEE
			internal BitArrayEnumeratorSimple(BitArray bitarray)
			{
				this.bitarray = bitarray;
				this.index = -1;
				this.version = bitarray._version;
			}

			// Token: 0x06005C09 RID: 23561 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005C0A RID: 23562 RVA: 0x00130B10 File Offset: 0x0012ED10
			public virtual bool MoveNext()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				if (this.index < this.bitarray.Count - 1)
				{
					this.index++;
					this.currentElement = this.bitarray.Get(this.index);
					return true;
				}
				this.index = this.bitarray.Count;
				return false;
			}

			// Token: 0x1700100B RID: 4107
			// (get) Token: 0x06005C0B RID: 23563 RVA: 0x00130B90 File Offset: 0x0012ED90
			public virtual object Current
			{
				get
				{
					if (this.index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this.index >= this.bitarray.Count)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06005C0C RID: 23564 RVA: 0x00130BE4 File Offset: 0x0012EDE4
			public void Reset()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Collection was modified; enumeration operation may not execute."));
				}
				this.index = -1;
			}

			// Token: 0x04002F2C RID: 12076
			private BitArray bitarray;

			// Token: 0x04002F2D RID: 12077
			private int index;

			// Token: 0x04002F2E RID: 12078
			private int version;

			// Token: 0x04002F2F RID: 12079
			private bool currentElement;
		}
	}
}
