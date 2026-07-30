using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	/// <summary>Represents a mutable string of characters. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000288 RID: 648
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class StringBuilder : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class.</summary>
		// Token: 0x06001DB4 RID: 7604 RVA: 0x0006F4CA File Offset: 0x0006D6CA
		public StringBuilder()
			: this(16)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified capacity.</summary>
		/// <param name="capacity">The suggested starting size of this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06001DB5 RID: 7605 RVA: 0x0006F4D4 File Offset: 0x0006D6D4
		public StringBuilder(int capacity)
			: this(string.Empty, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		// Token: 0x06001DB6 RID: 7606 RVA: 0x0006F4E2 File Offset: 0x0006D6E2
		public StringBuilder(string value)
			: this(value, 16)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class using the specified string and capacity.</summary>
		/// <param name="value">The string used to initialize the value of the instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06001DB7 RID: 7607 RVA: 0x0006F4ED File Offset: 0x0006D6ED
		public StringBuilder(string value, int capacity)
			: this(value, 0, (value != null) ? value.Length : 0, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class from the specified substring and capacity.</summary>
		/// <param name="value">The string that contains the substring used to initialize the value of this instance. If <paramref name="value" /> is null, the new <see cref="T:System.Text.StringBuilder" /> will contain the empty string (that is, it contains <see cref="F:System.String.Empty" />). </param>
		/// <param name="startIndex">The position within <paramref name="value" /> where the substring begins. </param>
		/// <param name="length">The number of characters in the substring. </param>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="length" /> is not a position within <paramref name="value" />. </exception>
		// Token: 0x06001DB8 RID: 7608 RVA: 0x0006F504 File Offset: 0x0006D704
		[SecuritySafeCritical]
		public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("'{0}' must be greater than zero.", new object[] { "capacity" }));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("'{0}' must be non-negative.", new object[] { "length" }));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Index and length must refer to a location within the string."));
			}
			this.m_MaxCapacity = int.MaxValue;
			if (capacity == 0)
			{
				capacity = 16;
			}
			if (capacity < length)
			{
				capacity = length;
			}
			this.m_ChunkChars = new char[capacity];
			this.m_ChunkLength = length;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				StringBuilder.ThreadSafeCopy(ptr + startIndex, this.m_ChunkChars, 0, length);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.StringBuilder" /> class that starts with a specified capacity and can grow to a specified maximum.</summary>
		/// <param name="capacity">The suggested starting size of the <see cref="T:System.Text.StringBuilder" />. </param>
		/// <param name="maxCapacity">The maximum number of characters the current string can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxCapacity" /> is less than one, <paramref name="capacity" /> is less than zero, or <paramref name="capacity" /> is greater than <paramref name="maxCapacity" />. </exception>
		// Token: 0x06001DB9 RID: 7609 RVA: 0x0006F600 File Offset: 0x0006D800
		public StringBuilder(int capacity, int maxCapacity)
		{
			if (capacity > maxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("Capacity exceeds maximum capacity."));
			}
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", Environment.GetResourceString("MaxCapacity must be one or greater."));
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("'{0}' must be greater than zero.", new object[] { "capacity" }));
			}
			if (capacity == 0)
			{
				capacity = Math.Min(16, maxCapacity);
			}
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkChars = new char[capacity];
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0006F68C File Offset: 0x0006D88C
		[SecurityCritical]
		private StringBuilder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int num = 0;
			string text = null;
			int num2 = int.MaxValue;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "m_MaxCapacity"))
				{
					if (!(name == "m_StringValue"))
					{
						if (name == "Capacity")
						{
							num = info.GetInt32("Capacity");
							flag = true;
						}
					}
					else
					{
						text = info.GetString("m_StringValue");
					}
				}
				else
				{
					num2 = info.GetInt32("m_MaxCapacity");
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (num2 < 1 || text.Length > num2)
			{
				throw new SerializationException(Environment.GetResourceString("The serialized MaxCapacity property of StringBuilder must be positive and greater than or equal to the String length."));
			}
			if (!flag)
			{
				num = 16;
				if (num < text.Length)
				{
					num = text.Length;
				}
				if (num > num2)
				{
					num = num2;
				}
			}
			if (num < 0 || num < text.Length || num > num2)
			{
				throw new SerializationException(Environment.GetResourceString("The serialized Capacity property of StringBuilder must be positive, less than or equal to MaxCapacity and greater than or equal to the String length."));
			}
			this.m_MaxCapacity = num2;
			this.m_ChunkChars = new char[num];
			text.CopyTo(0, this.m_ChunkChars, 0, text.Length);
			this.m_ChunkLength = text.Length;
			this.m_ChunkPrevious = null;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="info">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06001DBB RID: 7611 RVA: 0x0006F7C8 File Offset: 0x0006D9C8
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
			info.AddValue("Capacity", this.Capacity);
			info.AddValue("m_StringValue", this.ToString());
			info.AddValue("m_currentThread", 0);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0006F824 File Offset: 0x0006DA24
		[Conditional("_DEBUG")]
		private void VerifyClassInvariant()
		{
			StringBuilder stringBuilder = this;
			int maxCapacity = this.m_MaxCapacity;
			for (;;)
			{
				StringBuilder chunkPrevious = stringBuilder.m_ChunkPrevious;
				if (chunkPrevious == null)
				{
					break;
				}
				stringBuilder = chunkPrevious;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that can be contained in the memory allocated by the current instance.</summary>
		/// <returns>The maximum number of characters that can be contained in the memory allocated by the current instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than the current length of this instance.-or- The value specified for a set operation is greater than the maximum capacity. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0006F848 File Offset: 0x0006DA48
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x0006F85C File Offset: 0x0006DA5C
		public int Capacity
		{
			get
			{
				return this.m_ChunkChars.Length + this.m_ChunkOffset;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Capacity must be positive."));
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Capacity exceeds maximum capacity."));
				}
				if (value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("capacity was less than the current size."));
				}
				if (this.Capacity != value)
				{
					char[] array = new char[value - this.m_ChunkOffset];
					Array.Copy(this.m_ChunkChars, array, this.m_ChunkLength);
					this.m_ChunkChars = array;
				}
			}
		}

		/// <summary>Gets the maximum capacity of this instance.</summary>
		/// <returns>The maximum number of characters this instance can hold.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0006F8EE File Offset: 0x0006DAEE
		public int MaxCapacity
		{
			get
			{
				return this.m_MaxCapacity;
			}
		}

		/// <summary>Ensures that the capacity of this instance of <see cref="T:System.Text.StringBuilder" /> is at least the specified value.</summary>
		/// <returns>The new capacity of this instance.</returns>
		/// <param name="capacity">The minimum capacity to ensure. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DC0 RID: 7616 RVA: 0x0006F8F6 File Offset: 0x0006DAF6
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("Capacity must be positive."));
			}
			if (this.Capacity < capacity)
			{
				this.Capacity = capacity;
			}
			return this.Capacity;
		}

		/// <summary>Converts the value of this instance to a <see cref="T:System.String" />.</summary>
		/// <returns>A string whose value is the same as this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DC1 RID: 7617 RVA: 0x0006F928 File Offset: 0x0006DB28
		[SecuritySafeCritical]
		public unsafe override string ToString()
		{
			if (this.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(this.Length);
			StringBuilder stringBuilder = this;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				for (;;)
				{
					if (stringBuilder.m_ChunkLength > 0)
					{
						char[] chunkChars = stringBuilder.m_ChunkChars;
						int chunkOffset = stringBuilder.m_ChunkOffset;
						int chunkLength = stringBuilder.m_ChunkLength;
						if ((ulong)(chunkLength + chunkOffset) > (ulong)((long)text.Length) || chunkLength > chunkChars.Length)
						{
							break;
						}
						char[] array;
						char* ptr2;
						if ((array = chunkChars) == null || array.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array[0];
						}
						string.wstrcpy(ptr + chunkOffset, ptr2, chunkLength);
						array = null;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_7;
					}
				}
				throw new ArgumentOutOfRangeException("chunkLength", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				Block_7:;
			}
			return text;
		}

		/// <summary>Converts the value of a substring of this instance to a <see cref="T:System.String" />.</summary>
		/// <returns>A string whose value is the same as the specified substring of this instance.</returns>
		/// <param name="startIndex">The starting position of the substring in this instance. </param>
		/// <param name="length">The length of the substring. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- The sum of <paramref name="startIndex" /> and <paramref name="length" /> is greater than the length of the current instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DC2 RID: 7618 RVA: 0x0006F9F4 File Offset: 0x0006DBF4
		[SecuritySafeCritical]
		public unsafe string ToString(int startIndex, int length)
		{
			int length2 = this.Length;
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			if (startIndex > length2)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("startIndex cannot be larger than length of string."));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Length cannot be less than zero."));
			}
			if (startIndex > length2 - length)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Index and length must refer to a location within the string."));
			}
			StringBuilder stringBuilder = this;
			int num = startIndex + length;
			string text = string.FastAllocateString(length);
			int i = length;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				while (i > 0)
				{
					int num2 = num - stringBuilder.m_ChunkOffset;
					if (num2 >= 0)
					{
						if (num2 > stringBuilder.m_ChunkLength)
						{
							num2 = stringBuilder.m_ChunkLength;
						}
						int num3 = i;
						int num4 = num3;
						int num5 = num2 - num3;
						if (num5 < 0)
						{
							num4 += num5;
							num5 = 0;
						}
						i -= num4;
						if (num4 > 0)
						{
							char[] chunkChars = stringBuilder.m_ChunkChars;
							if ((ulong)(num4 + i) > (ulong)((long)length) || num4 + num5 > chunkChars.Length)
							{
								throw new ArgumentOutOfRangeException("chunkCount", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
							}
							fixed (char* ptr2 = &chunkChars[num5])
							{
								char* ptr3 = ptr2;
								string.wstrcpy(ptr + i, ptr3, num4);
							}
						}
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
				}
			}
			return text;
		}

		/// <summary>Removes all characters from the current <see cref="T:System.Text.StringBuilder" /> instance.</summary>
		/// <returns>An object whose <see cref="P:System.Text.StringBuilder.Length" /> is 0 (zero).</returns>
		// Token: 0x06001DC3 RID: 7619 RVA: 0x0006FB56 File Offset: 0x0006DD56
		public StringBuilder Clear()
		{
			this.Length = 0;
			return this;
		}

		/// <summary>Gets or sets the length of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>The length of this instance.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than zero or greater than <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0006FB60 File Offset: 0x0006DD60
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0006FB70 File Offset: 0x0006DD70
		public int Length
		{
			get
			{
				return this.m_ChunkOffset + this.m_ChunkLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Length cannot be less than zero."));
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("capacity was less than the current size."));
				}
				int capacity = this.Capacity;
				if (value == 0 && this.m_ChunkPrevious == null)
				{
					this.m_ChunkLength = 0;
					this.m_ChunkOffset = 0;
					return;
				}
				int num = value - this.Length;
				if (num > 0)
				{
					this.Append('\0', num);
					return;
				}
				StringBuilder stringBuilder = this.FindChunkForIndex(value);
				if (stringBuilder != this)
				{
					char[] array = new char[capacity - stringBuilder.m_ChunkOffset];
					Array.Copy(stringBuilder.m_ChunkChars, array, stringBuilder.m_ChunkLength);
					this.m_ChunkChars = array;
					this.m_ChunkPrevious = stringBuilder.m_ChunkPrevious;
					this.m_ChunkOffset = stringBuilder.m_ChunkOffset;
				}
				this.m_ChunkLength = value - stringBuilder.m_ChunkOffset;
			}
		}

		/// <summary>Gets or sets the character at the specified character position in this instance.</summary>
		/// <returns>The Unicode character at position <paramref name="index" />.</returns>
		/// <param name="index">The position of the character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while setting a character. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is outside the bounds of this instance while getting a character. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700042C RID: 1068
		[IndexerName("Chars")]
		public char this[int index]
		{
			get
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new IndexOutOfRangeException();
				}
				return stringBuilder.m_ChunkChars[num];
				Block_3:
				throw new IndexOutOfRangeException();
			}
			set
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				stringBuilder.m_ChunkChars[num] = value;
				return;
				Block_3:
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
		}

		/// <summary>Appends a specified number of copies of the string representation of a Unicode character to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The character to append. </param>
		/// <param name="repeatCount">The number of times to append <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="repeatCount" /> is less than zero.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DC8 RID: 7624 RVA: 0x0006FCF0 File Offset: 0x0006DEF0
		public StringBuilder Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException("repeatCount", Environment.GetResourceString("Count cannot be less than zero."));
			}
			if (repeatCount == 0)
			{
				return this;
			}
			int num = this.m_ChunkLength;
			while (repeatCount > 0)
			{
				if (num < this.m_ChunkChars.Length)
				{
					this.m_ChunkChars[num++] = value;
					repeatCount--;
				}
				else
				{
					this.m_ChunkLength = num;
					this.ExpandByABlock(repeatCount);
					num = 0;
				}
			}
			this.m_ChunkLength = num;
			return this;
		}

		/// <summary>Appends the string representation of a specified subarray of Unicode characters to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">A character array. </param>
		/// <param name="startIndex">The starting position in <paramref name="value" />. </param>
		/// <param name="charCount">The number of characters to append. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero.-or- <paramref name="startIndex" /> is less than zero.-or- <paramref name="startIndex" /> + <paramref name="charCount" /> is greater than the length of <paramref name="value" />.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DC9 RID: 7625 RVA: 0x0006FD60 File Offset: 0x0006DF60
		[SecuritySafeCritical]
		public unsafe StringBuilder Append(char[] value, int startIndex, int charCount)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Value must be positive."));
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Value must be positive."));
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && charCount == 0)
			{
				return this;
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (charCount > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (charCount == 0)
				{
					return this;
				}
				fixed (char* ptr = &value[startIndex])
				{
					char* ptr2 = ptr;
					this.Append(ptr2, charCount);
				}
				return this;
			}
		}

		/// <summary>Appends a copy of the specified string to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The string to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DCA RID: 7626 RVA: 0x0006FDFC File Offset: 0x0006DFFC
		[SecuritySafeCritical]
		public unsafe StringBuilder Append(string value)
		{
			if (value != null)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				int length = value.Length;
				int num = chunkLength + length;
				if (num < chunkChars.Length)
				{
					if (length <= 2)
					{
						if (length > 0)
						{
							chunkChars[chunkLength] = value[0];
						}
						if (length > 1)
						{
							chunkChars[chunkLength + 1] = value[1];
						}
					}
					else
					{
						fixed (string text = value)
						{
							char* ptr = text;
							if (ptr != null)
							{
								ptr += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (char* ptr2 = &chunkChars[chunkLength])
							{
								string.wstrcpy(ptr2, ptr, length);
							}
						}
					}
					this.m_ChunkLength = num;
				}
				else
				{
					this.AppendHelper(value);
				}
			}
			return this;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0006FE94 File Offset: 0x0006E094
		[SecuritySafeCritical]
		private unsafe void AppendHelper(string value)
		{
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Append(ptr, value.Length);
			}
		}

		/// <summary>Appends a copy of a specified substring to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The string that contains the substring to append. </param>
		/// <param name="startIndex">The starting position of the substring within <paramref name="value" />. </param>
		/// <param name="count">The number of characters in <paramref name="value" /> to append. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="startIndex" /> and <paramref name="count" /> are not zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> less than zero.-or- <paramref name="startIndex" /> less than zero.-or- <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="value" />.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DCC RID: 7628 RVA: 0x0006FEC4 File Offset: 0x0006E0C4
		[SecuritySafeCritical]
		public unsafe StringBuilder Append(string value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Value must be positive."));
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && count == 0)
			{
				return this;
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (startIndex > value.Length - count)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.Append(ptr + startIndex, count);
				}
				return this;
			}
		}

		/// <summary>Appends the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DCD RID: 7629 RVA: 0x0006FF6B File Offset: 0x0006E16B
		[ComVisible(false)]
		public StringBuilder AppendLine()
		{
			return this.Append(Environment.NewLine);
		}

		/// <summary>Appends a copy of the specified string followed by the default line terminator to the end of the current <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The string to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DCE RID: 7630 RVA: 0x0006FF78 File Offset: 0x0006E178
		[ComVisible(false)]
		public StringBuilder AppendLine(string value)
		{
			this.Append(value);
			return this.Append(Environment.NewLine);
		}

		/// <summary>Copies the characters from a specified segment of this instance to a specified segment of a destination <see cref="T:System.Char" /> array.</summary>
		/// <param name="sourceIndex">The starting position in this instance where characters will be copied from. The index is zero-based.</param>
		/// <param name="destination">The array where characters will be copied.</param>
		/// <param name="destinationIndex">The starting position in <paramref name="destination" /> where characters will be copied. The index is zero-based.</param>
		/// <param name="count">The number of characters to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" />, <paramref name="destinationIndex" />, or <paramref name="count" />, is less than zero.-or-<paramref name="sourceIndex" /> is greater than the length of this instance.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceIndex" /> + <paramref name="count" /> is greater than the length of this instance.-or-<paramref name="destinationIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="destination" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DCF RID: 7631 RVA: 0x0006FF90 File Offset: 0x0006E190
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Argument count must not be negative."));
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("'{0}' must be non-negative.", new object[] { "destinationIndex" }));
			}
			if (destinationIndex > destination.Length - count)
			{
				throw new ArgumentException(Environment.GetResourceString("Either offset did not refer to a position in the string, or there is an insufficient length of destination character array."));
			}
			if (sourceIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (sourceIndex > this.Length - count)
			{
				throw new ArgumentException(Environment.GetResourceString("Source string was not long enough. Check sourceIndex and count."));
			}
			StringBuilder stringBuilder = this;
			int num = sourceIndex + count;
			int num2 = destinationIndex + count;
			while (count > 0)
			{
				int num3 = num - stringBuilder.m_ChunkOffset;
				if (num3 >= 0)
				{
					if (num3 > stringBuilder.m_ChunkLength)
					{
						num3 = stringBuilder.m_ChunkLength;
					}
					int num4 = count;
					int num5 = num3 - count;
					if (num5 < 0)
					{
						num4 += num5;
						num5 = 0;
					}
					num2 -= num4;
					count -= num4;
					StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num5, destination, num2, num4);
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
		}

		/// <summary>Inserts one or more copies of a specified string into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after insertion has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The string to insert. </param>
		/// <param name="count">The number of times to insert <paramref name="value" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.OutOfMemoryException">The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> times <paramref name="count" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD0 RID: 7632 RVA: 0x000700AC File Offset: 0x0006E2AC
		[SecuritySafeCritical]
		public unsafe StringBuilder Insert(int index, string value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (value == null || value.Length == 0 || count == 0)
			{
				return this;
			}
			long num = (long)value.Length * (long)count;
			if (num > (long)(this.MaxCapacity - this.Length))
			{
				throw new OutOfMemoryException();
			}
			StringBuilder stringBuilder;
			int num2;
			this.MakeRoom(index, (int)num, out stringBuilder, out num2, false);
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				while (count > 0)
				{
					this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
					count--;
				}
			}
			return this;
		}

		/// <summary>Removes the specified range of characters from this instance.</summary>
		/// <returns>A reference to this instance after the excise operation has completed.</returns>
		/// <param name="startIndex">The zero-based position in this instance where removal begins. </param>
		/// <param name="length">The number of characters to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">If <paramref name="startIndex" /> or <paramref name="length" /> is less than zero, or <paramref name="startIndex" /> + <paramref name="length" /> is greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD1 RID: 7633 RVA: 0x0007016C File Offset: 0x0006E36C
		public StringBuilder Remove(int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Length cannot be less than zero."));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			if (length > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (this.Length == length && startIndex == 0)
			{
				this.Length = 0;
				return this;
			}
			if (length > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.Remove(startIndex, length, out stringBuilder, out num);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified Boolean value to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The Boolean value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD2 RID: 7634 RVA: 0x000701F1 File Offset: 0x0006E3F1
		public StringBuilder Append(bool value)
		{
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of a specified 8-bit signed integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD3 RID: 7635 RVA: 0x00070200 File Offset: 0x0006E400
		[CLSCompliant(false)]
		public StringBuilder Append(sbyte value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 8-bit unsigned integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD4 RID: 7636 RVA: 0x00070214 File Offset: 0x0006E414
		public StringBuilder Append(byte value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified Unicode character to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The Unicode character to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD5 RID: 7637 RVA: 0x00070228 File Offset: 0x0006E428
		public StringBuilder Append(char value)
		{
			if (this.m_ChunkLength < this.m_ChunkChars.Length)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				this.m_ChunkLength = chunkLength + 1;
				chunkChars[chunkLength] = value;
			}
			else
			{
				this.Append(value, 1);
			}
			return this;
		}

		/// <summary>Appends the string representation of a specified 16-bit signed integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD6 RID: 7638 RVA: 0x0007026A File Offset: 0x0006E46A
		public StringBuilder Append(short value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 32-bit signed integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD7 RID: 7639 RVA: 0x0007027E File Offset: 0x0006E47E
		public StringBuilder Append(int value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 64-bit signed integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD8 RID: 7640 RVA: 0x00070292 File Offset: 0x0006E492
		public StringBuilder Append(long value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified single-precision floating-point number to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD9 RID: 7641 RVA: 0x000702A6 File Offset: 0x0006E4A6
		public StringBuilder Append(float value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified double-precision floating-point number to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DDA RID: 7642 RVA: 0x000702BA File Offset: 0x0006E4BA
		public StringBuilder Append(double value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified decimal number to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DDB RID: 7643 RVA: 0x000702CE File Offset: 0x0006E4CE
		public StringBuilder Append(decimal value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 16-bit unsigned integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DDC RID: 7644 RVA: 0x000702E2 File Offset: 0x0006E4E2
		[CLSCompliant(false)]
		public StringBuilder Append(ushort value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 32-bit unsigned integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DDD RID: 7645 RVA: 0x000702F6 File Offset: 0x0006E4F6
		[CLSCompliant(false)]
		public StringBuilder Append(uint value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified 64-bit unsigned integer to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The value to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DDE RID: 7646 RVA: 0x0007030A File Offset: 0x0006E50A
		[CLSCompliant(false)]
		public StringBuilder Append(ulong value)
		{
			return this.Append(value.ToString(CultureInfo.CurrentCulture));
		}

		/// <summary>Appends the string representation of a specified object to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The object to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DDF RID: 7647 RVA: 0x0007031E File Offset: 0x0006E51E
		public StringBuilder Append(object value)
		{
			if (value == null)
			{
				return this;
			}
			return this.Append(value.ToString());
		}

		/// <summary>Appends the string representation of the Unicode characters in a specified array to this instance.</summary>
		/// <returns>A reference to this instance after the append operation has completed.</returns>
		/// <param name="value">The array of characters to append. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE0 RID: 7648 RVA: 0x00070334 File Offset: 0x0006E534
		[SecuritySafeCritical]
		public unsafe StringBuilder Append(char[] value)
		{
			if (value != null && value.Length != 0)
			{
				fixed (char* ptr = &value[0])
				{
					char* ptr2 = ptr;
					this.Append(ptr2, value.Length);
				}
			}
			return this;
		}

		/// <summary>Inserts a string into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The string to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the current length of this instance. -or-The current length of this <see cref="T:System.Text.StringBuilder" /> object plus the length of <paramref name="value" /> exceeds <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE1 RID: 7649 RVA: 0x00070364 File Offset: 0x0006E564
		[SecuritySafeCritical]
		public unsafe StringBuilder Insert(int index, string value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (value != null)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.Insert(index, ptr, value.Length);
				}
			}
			return this;
		}

		/// <summary>Inserts the string representation of a Boolean value into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE2 RID: 7650 RVA: 0x000703B3 File Offset: 0x0006E5B3
		public StringBuilder Insert(int index, bool value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Inserts the string representation of a specified 8-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE3 RID: 7651 RVA: 0x000703C4 File Offset: 0x0006E5C4
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, sbyte value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a specified 8-bit unsigned integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x000703DA File Offset: 0x0006E5DA
		public StringBuilder Insert(int index, byte value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a specified 16-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x000703F0 File Offset: 0x0006E5F0
		public StringBuilder Insert(int index, short value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a specified Unicode character into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x00070406 File Offset: 0x0006E606
		[SecuritySafeCritical]
		public unsafe StringBuilder Insert(int index, char value)
		{
			this.Insert(index, &value, 1);
			return this;
		}

		/// <summary>Inserts the string representation of a specified array of Unicode characters into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The character array to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE7 RID: 7655 RVA: 0x00070414 File Offset: 0x0006E614
		public StringBuilder Insert(int index, char[] value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (value != null)
			{
				this.Insert(index, value, 0, value.Length);
			}
			return this;
		}

		/// <summary>Inserts the string representation of a specified subarray of Unicode characters into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">A character array. </param>
		/// <param name="startIndex">The starting index within <paramref name="value" />. </param>
		/// <param name="charCount">The number of characters to insert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null, and <paramref name="startIndex" /> and <paramref name="charCount" /> are not zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" />, <paramref name="startIndex" />, or <paramref name="charCount" /> is less than zero.-or- <paramref name="index" /> is greater than the length of this instance.-or- <paramref name="startIndex" /> plus <paramref name="charCount" /> is not a position within <paramref name="value" />.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE8 RID: 7656 RVA: 0x00070448 File Offset: 0x0006E648
		[SecuritySafeCritical]
		public unsafe StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
		{
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException(Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
				}
				if (charCount < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Value must be positive."));
				}
				if (startIndex > value.Length - charCount)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (charCount > 0)
				{
					fixed (char* ptr = &value[startIndex])
					{
						char* ptr2 = ptr;
						this.Insert(index, ptr2, charCount);
					}
				}
				return this;
			}
		}

		/// <summary>Inserts the string representation of a specified 32-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DE9 RID: 7657 RVA: 0x00070500 File Offset: 0x0006E700
		public StringBuilder Insert(int index, int value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a 64-bit signed integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DEA RID: 7658 RVA: 0x00070516 File Offset: 0x0006E716
		public StringBuilder Insert(int index, long value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a single-precision floating point number into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DEB RID: 7659 RVA: 0x0007052C File Offset: 0x0006E72C
		public StringBuilder Insert(int index, float value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a double-precision floating-point number into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DEC RID: 7660 RVA: 0x00070542 File Offset: 0x0006E742
		public StringBuilder Insert(int index, double value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a decimal number into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DED RID: 7661 RVA: 0x00070558 File Offset: 0x0006E758
		public StringBuilder Insert(int index, decimal value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a 16-bit unsigned integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DEE RID: 7662 RVA: 0x0007056E File Offset: 0x0006E76E
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ushort value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a 32-bit unsigned integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DEF RID: 7663 RVA: 0x00070584 File Offset: 0x0006E784
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, uint value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of a 64-bit unsigned integer into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The value to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DF0 RID: 7664 RVA: 0x0007059A File Offset: 0x0006E79A
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ulong value)
		{
			return this.Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
		}

		/// <summary>Inserts the string representation of an object into this instance at the specified character position.</summary>
		/// <returns>A reference to this instance after the insert operation has completed.</returns>
		/// <param name="index">The position in this instance where insertion begins. </param>
		/// <param name="value">The object to insert, or null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the length of this instance. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DF1 RID: 7665 RVA: 0x000705B0 File Offset: 0x0006E7B0
		public StringBuilder Insert(int index, object value)
		{
			if (value == null)
			{
				return this;
			}
			return this.Insert(index, value.ToString(), 1);
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a single argument.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">An object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to 1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DF2 RID: 7666 RVA: 0x000705C5 File Offset: 0x0006E7C5
		public StringBuilder AppendFormat(string format, object arg0)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of two arguments.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or-The index of a format item is less than 0 (zero), or greater than or equal to 2. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DF3 RID: 7667 RVA: 0x000705D5 File Offset: 0x0006E7D5
		public StringBuilder AppendFormat(string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of either of three arguments.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <param name="arg2">The third object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or-The index of a format item is less than 0 (zero), or greater than or equal to 3.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DF4 RID: 7668 RVA: 0x000705E6 File Offset: 0x0006E7E6
		public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array.</summary>
		/// <returns>A reference to this instance with <paramref name="format" /> appended. Each format item in <paramref name="format" /> is replaced by the string representation of the corresponding object argument.</returns>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="args">An array of objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DF5 RID: 7669 RVA: 0x000705F9 File Offset: 0x0006E7F9
		public StringBuilder AppendFormat(string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(null, format, new ParamsArray(args));
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00070621 File Offset: 0x0006E821
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0));
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00070631 File Offset: 0x0006E831
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00070643 File Offset: 0x0006E843
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. Each format item is replaced by the string representation of a corresponding argument in a parameter array using a specified format provider.</summary>
		/// <returns>A reference to this instance after the append operation has completed. After the append operation, this instance contains any data that existed before the operation, suffixed by a copy of <paramref name="format" /> where any format specification is replaced by the string representation of the corresponding object argument. </returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="args">An array of objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. -or-The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="args" /> array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the expanded string would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DF9 RID: 7673 RVA: 0x00070657 File Offset: 0x0006E857
		public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return this.AppendFormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0007067F File Offset: 0x0006E87F
		private static void FormatError()
		{
			throw new FormatException(Environment.GetResourceString("Input string was not in a correct format."));
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00070690 File Offset: 0x0006E890
		internal StringBuilder AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			int length = format.Length;
			char c = '\0';
			ICustomFormatter customFormatter = null;
			if (provider != null)
			{
				customFormatter = (ICustomFormatter)provider.GetFormat(typeof(ICustomFormatter));
			}
			for (;;)
			{
				if (num < length)
				{
					c = format[num];
					num++;
					if (c == '}')
					{
						if (num < length && format[num] == '}')
						{
							num++;
						}
						else
						{
							StringBuilder.FormatError();
						}
					}
					if (c == '{')
					{
						if (num >= length || format[num] != '{')
						{
							num--;
							goto IL_008D;
						}
						num++;
					}
					this.Append(c);
					continue;
				}
				IL_008D:
				if (num == length)
				{
					return this;
				}
				num++;
				if (num == length || (c = format[num]) < '0' || c > '9')
				{
					StringBuilder.FormatError();
				}
				int num2 = 0;
				do
				{
					num2 = num2 * 10 + (int)c - 48;
					num++;
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
				}
				while (c >= '0' && c <= '9' && num2 < 1000000);
				if (num2 >= args.Length)
				{
					break;
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				bool flag = false;
				int num3 = 0;
				if (c == ',')
				{
					num++;
					while (num < length && format[num] == ' ')
					{
						num++;
					}
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
					if (c == '-')
					{
						flag = true;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
					}
					if (c < '0' || c > '9')
					{
						StringBuilder.FormatError();
					}
					do
					{
						num3 = num3 * 10 + (int)c - 48;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						if (c < '0' || c > '9')
						{
							break;
						}
					}
					while (num3 < 1000000);
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				object obj = args[num2];
				StringBuilder stringBuilder = null;
				if (c == ':')
				{
					num++;
					for (;;)
					{
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						num++;
						if (c == '{')
						{
							if (num < length && format[num] == '{')
							{
								num++;
							}
							else
							{
								StringBuilder.FormatError();
							}
						}
						else if (c == '}')
						{
							if (num >= length || format[num] != '}')
							{
								break;
							}
							num++;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append(c);
					}
					num--;
				}
				if (c != '}')
				{
					StringBuilder.FormatError();
				}
				num++;
				string text = null;
				string text2 = null;
				if (customFormatter != null)
				{
					if (stringBuilder != null)
					{
						text = stringBuilder.ToString();
					}
					text2 = customFormatter.Format(text, obj, provider);
				}
				if (text2 == null)
				{
					IFormattable formattable = obj as IFormattable;
					if (formattable != null)
					{
						if (text == null && stringBuilder != null)
						{
							text = stringBuilder.ToString();
						}
						text2 = formattable.ToString(text, provider);
					}
					else if (obj != null)
					{
						text2 = obj.ToString();
					}
				}
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				int num4 = num3 - text2.Length;
				if (!flag && num4 > 0)
				{
					this.Append(' ', num4);
				}
				this.Append(text2);
				if (flag && num4 > 0)
				{
					this.Append(' ', num4);
				}
			}
			throw new FormatException(Environment.GetResourceString("Index (zero based) must be greater than or equal to zero and less than the size of the argument list."));
		}

		/// <summary>Replaces all occurrences of a specified string in this instance with another specified string.</summary>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" />.</returns>
		/// <param name="oldValue">The string to replace. </param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldValue" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DFC RID: 7676 RVA: 0x000709AB File Offset: 0x0006EBAB
		public StringBuilder Replace(string oldValue, string newValue)
		{
			return this.Replace(oldValue, newValue, 0, this.Length);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance and <paramref name="sb" /> have equal string, <see cref="P:System.Text.StringBuilder.Capacity" />, and <see cref="P:System.Text.StringBuilder.MaxCapacity" /> values; otherwise, false.</returns>
		/// <param name="sb">An object to compare with this instance, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DFD RID: 7677 RVA: 0x000709BC File Offset: 0x0006EBBC
		public bool Equals(StringBuilder sb)
		{
			if (sb == null)
			{
				return false;
			}
			if (this.Capacity != sb.Capacity || this.MaxCapacity != sb.MaxCapacity || this.Length != sb.Length)
			{
				return false;
			}
			if (sb == this)
			{
				return true;
			}
			StringBuilder stringBuilder = this;
			int i = stringBuilder.m_ChunkLength;
			StringBuilder stringBuilder2 = sb;
			int j = stringBuilder2.m_ChunkLength;
			for (;;)
			{
				IL_0049:
				i--;
				j--;
				while (i < 0)
				{
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder != null)
					{
						i = stringBuilder.m_ChunkLength + i;
					}
					else
					{
						IL_007F:
						while (j < 0)
						{
							stringBuilder2 = stringBuilder2.m_ChunkPrevious;
							if (stringBuilder2 == null)
							{
								break;
							}
							j = stringBuilder2.m_ChunkLength + j;
						}
						if (i < 0)
						{
							goto Block_8;
						}
						if (j < 0)
						{
							return false;
						}
						if (stringBuilder.m_ChunkChars[i] != stringBuilder2.m_ChunkChars[j])
						{
							return false;
						}
						goto IL_0049;
					}
				}
				goto IL_007F;
			}
			Block_8:
			return j < 0;
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified string with another specified string.</summary>
		/// <returns>A reference to this instance with all instances of <paramref name="oldValue" /> replaced by <paramref name="newValue" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> - 1.</returns>
		/// <param name="oldValue">The string to replace. </param>
		/// <param name="newValue">The string that replaces <paramref name="oldValue" />, or null. </param>
		/// <param name="startIndex">The position in this instance where the substring begins. </param>
		/// <param name="count">The length of the substring. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="oldValue" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="count" /> indicates a character position not within this instance.-or- Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DFE RID: 7678 RVA: 0x00070A70 File Offset: 0x0006EC70
		public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "oldValue");
			}
			if (newValue == null)
			{
				newValue = "";
			}
			int[] array = null;
			int num = 0;
			StringBuilder stringBuilder = this.FindChunkForIndex(startIndex);
			int num2 = startIndex - stringBuilder.m_ChunkOffset;
			while (count > 0)
			{
				if (this.StartsWith(stringBuilder, num2, count, oldValue))
				{
					if (array == null)
					{
						array = new int[5];
					}
					else if (num >= array.Length)
					{
						int[] array2 = new int[array.Length * 3 / 2 + 4];
						Array.Copy(array, array2, array.Length);
						array = array2;
					}
					array[num++] = num2;
					num2 += oldValue.Length;
					count -= oldValue.Length;
				}
				else
				{
					num2++;
					count--;
				}
				if (num2 >= stringBuilder.m_ChunkLength || count == 0)
				{
					int num3 = num2 + stringBuilder.m_ChunkOffset;
					this.ReplaceAllInChunk(array, num, stringBuilder, oldValue.Length, newValue);
					num3 += (newValue.Length - oldValue.Length) * num;
					num = 0;
					stringBuilder = this.FindChunkForIndex(num3);
					num2 = num3 - stringBuilder.m_ChunkOffset;
				}
			}
			return this;
		}

		/// <summary>Replaces all occurrences of a specified character in this instance with another specified character.</summary>
		/// <returns>A reference to this instance with <paramref name="oldChar" /> replaced by <paramref name="newChar" />.</returns>
		/// <param name="oldChar">The character to replace. </param>
		/// <param name="newChar">The character that replaces <paramref name="oldChar" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DFF RID: 7679 RVA: 0x00070BD5 File Offset: 0x0006EDD5
		public StringBuilder Replace(char oldChar, char newChar)
		{
			return this.Replace(oldChar, newChar, 0, this.Length);
		}

		/// <summary>Replaces, within a substring of this instance, all occurrences of a specified character with another specified character.</summary>
		/// <returns>A reference to this instance with <paramref name="oldChar" /> replaced by <paramref name="newChar" /> in the range from <paramref name="startIndex" /> to <paramref name="startIndex" /> + <paramref name="count" /> -1.</returns>
		/// <param name="oldChar">The character to replace. </param>
		/// <param name="newChar">The character that replaces <paramref name="oldChar" />. </param>
		/// <param name="startIndex">The position in this instance where the substring begins. </param>
		/// <param name="count">The length of the substring. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of the value of this instance.-or- <paramref name="startIndex" /> or <paramref name="count" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E00 RID: 7680 RVA: 0x00070BE8 File Offset: 0x0006EDE8
		public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			int num = startIndex + count;
			StringBuilder stringBuilder = this;
			for (;;)
			{
				int num2 = num - stringBuilder.m_ChunkOffset;
				int num3 = startIndex - stringBuilder.m_ChunkOffset;
				if (num2 >= 0)
				{
					int i = Math.Max(num3, 0);
					int num4 = Math.Min(stringBuilder.m_ChunkLength, num2);
					while (i < num4)
					{
						if (stringBuilder.m_ChunkChars[i] == oldChar)
						{
							stringBuilder.m_ChunkChars[i] = newChar;
						}
						i++;
					}
				}
				if (num3 >= 0)
				{
					break;
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return this;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00070CA0 File Offset: 0x0006EEA0
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe StringBuilder Append(char* value, int valueCount)
		{
			if (valueCount < 0)
			{
				throw new ArgumentOutOfRangeException("valueCount", Environment.GetResourceString("Count cannot be less than zero."));
			}
			int num = valueCount + this.m_ChunkLength;
			if (num <= this.m_ChunkChars.Length)
			{
				StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
				this.m_ChunkLength = num;
			}
			else
			{
				int num2 = this.m_ChunkChars.Length - this.m_ChunkLength;
				if (num2 > 0)
				{
					StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, num2);
					this.m_ChunkLength = this.m_ChunkChars.Length;
				}
				int num3 = valueCount - num2;
				this.ExpandByABlock(num3);
				StringBuilder.ThreadSafeCopy(value + num2, this.m_ChunkChars, 0, num3);
				this.m_ChunkLength = num3;
			}
			return this;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00070D54 File Offset: 0x0006EF54
		[SecurityCritical]
		private unsafe void Insert(int index, char* value, int valueCount)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (valueCount > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.MakeRoom(index, valueCount, out stringBuilder, out num, false);
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num, value, valueCount);
			}
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00070D9C File Offset: 0x0006EF9C
		[SecuritySafeCritical]
		private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
		{
			if (replacementsCount <= 0)
			{
				return;
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num = (value.Length - removeCount) * replacementsCount;
				StringBuilder stringBuilder = sourceChunk;
				int num2 = replacements[0];
				if (num > 0)
				{
					this.MakeRoom(stringBuilder.m_ChunkOffset + num2, num, out stringBuilder, out num2, true);
				}
				int num3 = 0;
				for (;;)
				{
					this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr, value.Length);
					int num4 = replacements[num3] + removeCount;
					num3++;
					if (num3 >= replacementsCount)
					{
						break;
					}
					int num5 = replacements[num3];
					if (num != 0)
					{
						fixed (char* ptr2 = &sourceChunk.m_ChunkChars[num4])
						{
							char* ptr3 = ptr2;
							this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, ptr3, num5 - num4);
						}
					}
					else
					{
						num2 += num5 - num4;
					}
				}
				if (num < 0)
				{
					this.Remove(stringBuilder.m_ChunkOffset + num2, -num, out stringBuilder, out num2);
				}
			}
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00070E70 File Offset: 0x0006F070
		private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (count == 0)
				{
					return false;
				}
				if (indexInChunk >= chunk.m_ChunkLength)
				{
					chunk = this.Next(chunk);
					if (chunk == null)
					{
						return false;
					}
					indexInChunk = 0;
				}
				if (value[i] != chunk.m_ChunkChars[indexInChunk])
				{
					return false;
				}
				indexInChunk++;
				count--;
			}
			return true;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00070ED0 File Offset: 0x0006F0D0
		[SecurityCritical]
		private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
		{
			if (count != 0)
			{
				for (;;)
				{
					int num = Math.Min(chunk.m_ChunkLength - indexInChunk, count);
					StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, num);
					indexInChunk += num;
					if (indexInChunk >= chunk.m_ChunkLength)
					{
						chunk = this.Next(chunk);
						indexInChunk = 0;
					}
					count -= num;
					if (count == 0)
					{
						break;
					}
					value += num;
				}
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00070F38 File Offset: 0x0006F138
		[SecurityCritical]
		private unsafe static void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (destinationIndex <= destination.Length && destinationIndex + count <= destination.Length)
			{
				fixed (char* ptr = &destination[destinationIndex])
				{
					string.wstrcpy(ptr, sourcePtr, count);
				}
				return;
			}
			throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00070F84 File Offset: 0x0006F184
		[SecurityCritical]
		private unsafe static void ThreadSafeCopy(char[] source, int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (sourceIndex <= source.Length && sourceIndex + count <= source.Length)
			{
				fixed (char* ptr = &source[sourceIndex])
				{
					StringBuilder.ThreadSafeCopy(ptr, destination, destinationIndex, count);
				}
				return;
			}
			throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00070FD4 File Offset: 0x0006F1D4
		private StringBuilder FindChunkForIndex(int index)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset > index)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00070FF8 File Offset: 0x0006F1F8
		private StringBuilder FindChunkForByte(int byteIndex)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset * 2 > byteIndex)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0007101C File Offset: 0x0006F21C
		private StringBuilder Next(StringBuilder chunk)
		{
			if (chunk == this)
			{
				return null;
			}
			return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00071038 File Offset: 0x0006F238
		private void ExpandByABlock(int minBlockCharCount)
		{
			if (minBlockCharCount + this.Length < minBlockCharCount || minBlockCharCount + this.Length > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("capacity was less than the current size."));
			}
			int num = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
			this.m_ChunkPrevious = new StringBuilder(this);
			this.m_ChunkOffset += this.m_ChunkLength;
			this.m_ChunkLength = 0;
			if (this.m_ChunkOffset + num < num)
			{
				this.m_ChunkChars = null;
				throw new OutOfMemoryException();
			}
			this.m_ChunkChars = new char[num];
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000710D8 File Offset: 0x0006F2D8
		private StringBuilder(StringBuilder from)
		{
			this.m_ChunkLength = from.m_ChunkLength;
			this.m_ChunkOffset = from.m_ChunkOffset;
			this.m_ChunkChars = from.m_ChunkChars;
			this.m_ChunkPrevious = from.m_ChunkPrevious;
			this.m_MaxCapacity = from.m_MaxCapacity;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00071128 File Offset: 0x0006F328
		[SecuritySafeCritical]
		private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doneMoveFollowingChars)
		{
			if (count + this.Length < count || count + this.Length > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("capacity was less than the current size."));
			}
			chunk = this;
			while (chunk.m_ChunkOffset > index)
			{
				chunk.m_ChunkOffset += count;
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = index - chunk.m_ChunkOffset;
			if (!doneMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
			{
				int i = chunk.m_ChunkLength;
				while (i > indexInChunk)
				{
					i--;
					chunk.m_ChunkChars[i + count] = chunk.m_ChunkChars[i];
				}
				chunk.m_ChunkLength += count;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
			stringBuilder.m_ChunkLength = count;
			int num = Math.Min(count, indexInChunk);
			if (num > 0)
			{
				char[] array;
				char* ptr;
				if ((array = chunk.m_ChunkChars) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				StringBuilder.ThreadSafeCopy(ptr, stringBuilder.m_ChunkChars, 0, num);
				int num2 = indexInChunk - num;
				if (num2 >= 0)
				{
					StringBuilder.ThreadSafeCopy(ptr + num, chunk.m_ChunkChars, 0, num2);
					indexInChunk = num2;
				}
				array = null;
			}
			chunk.m_ChunkPrevious = stringBuilder;
			chunk.m_ChunkOffset += count;
			if (num < count)
			{
				chunk = stringBuilder;
				indexInChunk = num;
			}
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0007129E File Offset: 0x0006F49E
		private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
		{
			this.m_ChunkChars = new char[size];
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkPrevious = previousBlock;
			if (previousBlock != null)
			{
				this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
			}
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000712D8 File Offset: 0x0006F4D8
		[SecuritySafeCritical]
		private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
		{
			int num = startIndex + count;
			chunk = this;
			StringBuilder stringBuilder = null;
			int num2 = 0;
			for (;;)
			{
				if (num - chunk.m_ChunkOffset >= 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = chunk;
						num2 = num - stringBuilder.m_ChunkOffset;
					}
					if (startIndex - chunk.m_ChunkOffset >= 0)
					{
						break;
					}
				}
				else
				{
					chunk.m_ChunkOffset -= count;
				}
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = startIndex - chunk.m_ChunkOffset;
			int num3 = indexInChunk;
			int num4 = stringBuilder.m_ChunkLength - num2;
			if (stringBuilder != chunk)
			{
				num3 = 0;
				chunk.m_ChunkLength = indexInChunk;
				stringBuilder.m_ChunkPrevious = chunk;
				stringBuilder.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
				if (indexInChunk == 0)
				{
					stringBuilder.m_ChunkPrevious = chunk.m_ChunkPrevious;
					chunk = stringBuilder;
				}
			}
			stringBuilder.m_ChunkLength -= num2 - num3;
			if (num3 != num2)
			{
				StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num2, stringBuilder.m_ChunkChars, num3, num4);
			}
		}

		// Token: 0x0400105E RID: 4190
		internal char[] m_ChunkChars;

		// Token: 0x0400105F RID: 4191
		internal StringBuilder m_ChunkPrevious;

		// Token: 0x04001060 RID: 4192
		internal int m_ChunkLength;

		// Token: 0x04001061 RID: 4193
		internal int m_ChunkOffset;

		// Token: 0x04001062 RID: 4194
		internal int m_MaxCapacity;

		// Token: 0x04001063 RID: 4195
		internal const int DefaultCapacity = 16;

		// Token: 0x04001064 RID: 4196
		private const string CapacityField = "Capacity";

		// Token: 0x04001065 RID: 4197
		private const string MaxCapacityField = "m_MaxCapacity";

		// Token: 0x04001066 RID: 4198
		private const string StringValueField = "m_StringValue";

		// Token: 0x04001067 RID: 4199
		private const string ThreadIDField = "m_currentThread";

		// Token: 0x04001068 RID: 4200
		internal const int MaxChunkSize = 8000;
	}
}
