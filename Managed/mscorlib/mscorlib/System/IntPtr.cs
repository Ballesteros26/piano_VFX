using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>A platform-specific type that is used to represent a pointer or a handle.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000217 RID: 535
	[ComVisible(true)]
	[Serializable]
	public struct IntPtr : ISerializable
	{
		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified 32-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 32-bit signed integer. </param>
		// Token: 0x06001953 RID: 6483 RVA: 0x0005F16B File Offset: 0x0005D36B
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public IntPtr(int value)
		{
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified 64-bit pointer.</summary>
		/// <param name="value">A pointer or handle contained in a 64-bit signed integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large or too small to represent as an <see cref="T:System.IntPtr" />. </exception>
		// Token: 0x06001954 RID: 6484 RVA: 0x0005F175 File Offset: 0x0005D375
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public IntPtr(long value)
		{
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.IntPtr" /> using the specified pointer to an unspecified type.</summary>
		/// <param name="value">A pointer to an unspecified type. </param>
		// Token: 0x06001955 RID: 6485 RVA: 0x0005F17F File Offset: 0x0005D37F
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[CLSCompliant(false)]
		public unsafe IntPtr(void* value)
		{
			this.m_value = value;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0005F188 File Offset: 0x0005D388
		private IntPtr(SerializationInfo info, StreamingContext context)
		{
			long @int = info.GetInt64("value");
			this.m_value = @int;
		}

		/// <summary>Gets the size of this instance.</summary>
		/// <returns>The size of a pointer or handle in this process, measured in bytes. The value of this property is 4 in a 32-bit process, and 8 in a 64-bit process. You can define the process type by setting the /platform switch when you compile your code with the C# and Visual Basic compilers.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0005F1A9 File Offset: 0x0005D3A9
		public unsafe static int Size
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return sizeof(void*);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.IntPtr" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data. </param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06001958 RID: 6488 RVA: 0x0005F1B1 File Offset: 0x0005D3B1
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("value", this.ToInt64());
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.IntPtr" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001959 RID: 6489 RVA: 0x0005F1D2 File Offset: 0x0005D3D2
		public override bool Equals(object obj)
		{
			return obj is IntPtr && ((IntPtr)obj).m_value == this.m_value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600195A RID: 6490 RVA: 0x0005F1F1 File Offset: 0x0005D3F1
		public override int GetHashCode()
		{
			return this.m_value;
		}

		/// <summary>Converts the value of this instance to a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer equal to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of this instance is too large or too small to represent as a 32-bit signed integer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600195B RID: 6491 RVA: 0x0005F1F1 File Offset: 0x0005D3F1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int ToInt32()
		{
			return this.m_value;
		}

		/// <summary>Converts the value of this instance to a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer equal to the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600195C RID: 6492 RVA: 0x0005F1FA File Offset: 0x0005D3FA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public long ToInt64()
		{
			if (IntPtr.Size == 4)
			{
				return (long)this.m_value;
			}
			return this.m_value;
		}

		/// <summary>Converts the value of this instance to a pointer to an unspecified type.</summary>
		/// <returns>A pointer to <see cref="T:System.Void" />; that is, a pointer to memory containing data of an unspecified type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600195D RID: 6493 RVA: 0x0005F214 File Offset: 0x0005D414
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe void* ToPointer()
		{
			return this.m_value;
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.IntPtr" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600195E RID: 6494 RVA: 0x0005F21C File Offset: 0x0005D41C
		public override string ToString()
		{
			return this.ToString(null);
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.IntPtr" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of the current <see cref="T:System.IntPtr" /> object.</returns>
		/// <param name="format">A format specification that governs how the current <see cref="T:System.IntPtr" /> object is converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600195F RID: 6495 RVA: 0x0005F228 File Offset: 0x0005D428
		public string ToString(string format)
		{
			if (IntPtr.Size == 4)
			{
				return this.m_value.ToString(format, null);
			}
			return this.m_value.ToString(format, null);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.IntPtr" /> are equal.</summary>
		/// <returns>true if <paramref name="value1" /> equals <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">The first pointer or handle to compare.</param>
		/// <param name="value2">The second pointer or handle to compare.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001960 RID: 6496 RVA: 0x0005F260 File Offset: 0x0005D460
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool operator ==(IntPtr value1, IntPtr value2)
		{
			return value1.m_value == value2.m_value;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.IntPtr" /> are not equal.</summary>
		/// <returns>true if <paramref name="value1" /> does not equal <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">The first pointer or handle to compare. </param>
		/// <param name="value2">The second pointer or handle to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001961 RID: 6497 RVA: 0x0005F272 File Offset: 0x0005D472
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static bool operator !=(IntPtr value1, IntPtr value2)
		{
			return value1.m_value != value2.m_value;
		}

		/// <summary>Converts the value of a 32-bit signed integer to an <see cref="T:System.IntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 32-bit signed integer. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001962 RID: 6498 RVA: 0x0005F287 File Offset: 0x0005D487
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static explicit operator IntPtr(int value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the value of a 64-bit signed integer to an <see cref="T:System.IntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 64-bit signed integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.IntPtr" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001963 RID: 6499 RVA: 0x0005F28F File Offset: 0x0005D48F
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static explicit operator IntPtr(long value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the specified pointer to an unspecified type to an <see cref="T:System.IntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.IntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A pointer to an unspecified type. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001964 RID: 6500 RVA: 0x0005F297 File Offset: 0x0005D497
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		[CLSCompliant(false)]
		public unsafe static explicit operator IntPtr(void* value)
		{
			return new IntPtr(value);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a 32-bit signed integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of <paramref name="value" /> is too large to represent as a 32-bit signed integer. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001965 RID: 6501 RVA: 0x0005F29F File Offset: 0x0005D49F
		public static explicit operator int(IntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a 64-bit signed integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001966 RID: 6502 RVA: 0x0005F2A9 File Offset: 0x0005D4A9
		public static explicit operator long(IntPtr value)
		{
			return value.ToInt64();
		}

		/// <summary>Converts the value of the specified <see cref="T:System.IntPtr" /> to a pointer to an unspecified type.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001967 RID: 6503 RVA: 0x0005F2B2 File Offset: 0x0005D4B2
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(IntPtr value)
		{
			return value.m_value;
		}

		/// <summary>Adds an offset to the value of a pointer.</summary>
		/// <returns>A new pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		/// <param name="pointer">The pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		// Token: 0x06001968 RID: 6504 RVA: 0x0005F2BB File Offset: 0x0005D4BB
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr Add(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		/// <summary>Subtracts an offset from the value of a pointer.</summary>
		/// <returns>A new pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		/// <param name="pointer">The pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		// Token: 0x06001969 RID: 6505 RVA: 0x0005F2CA File Offset: 0x0005D4CA
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr Subtract(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		/// <summary>Adds an offset to the value of a pointer.</summary>
		/// <returns>A new pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		/// <param name="pointer">The pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		// Token: 0x0600196A RID: 6506 RVA: 0x0005F2BB File Offset: 0x0005D4BB
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr operator +(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		/// <summary>Subtracts an offset from the value of a pointer.</summary>
		/// <returns>A new pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		/// <param name="pointer">The pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		// Token: 0x0600196B RID: 6507 RVA: 0x0005F2CA File Offset: 0x0005D4CA
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public unsafe static IntPtr operator -(IntPtr pointer, int offset)
		{
			return (IntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0005F2D9 File Offset: 0x0005D4D9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal bool IsNull()
		{
			return this.m_value == null;
		}

		// Token: 0x04000CBA RID: 3258
		private unsafe void* m_value;

		/// <summary>A read-only field that represents a pointer or handle that has been initialized to zero.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000CBB RID: 3259
		public static readonly IntPtr Zero;
	}
}
