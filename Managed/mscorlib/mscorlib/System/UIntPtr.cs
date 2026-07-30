using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>A platform-specific type that is used to represent a pointer or a handle.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200024D RID: 589
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public struct UIntPtr : ISerializable
	{
		/// <summary>Initializes a new instance of <see cref="T:System.UIntPtr" /> using the specified 64-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 64-bit unsigned integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.UIntPtr" />. </exception>
		// Token: 0x06001B76 RID: 7030 RVA: 0x00067E4B File Offset: 0x0006604B
		public UIntPtr(ulong value)
		{
			if (value > (ulong)(-1) && UIntPtr.Size < 8)
			{
				throw new OverflowException(Locale.GetText("This isn't a 64bits machine."));
			}
			this._pointer = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UIntPtr" /> structure using the specified 32-bit pointer or handle.</summary>
		/// <param name="value">A pointer or handle contained in a 32-bit unsigned integer. </param>
		// Token: 0x06001B77 RID: 7031 RVA: 0x00067E72 File Offset: 0x00066072
		public UIntPtr(uint value)
		{
			this._pointer = value;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.UIntPtr" /> using the specified pointer to an unspecified type.</summary>
		/// <param name="value">A pointer to an unspecified type. </param>
		// Token: 0x06001B78 RID: 7032 RVA: 0x00067E7C File Offset: 0x0006607C
		[CLSCompliant(false)]
		public unsafe UIntPtr(void* value)
		{
			this._pointer = value;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.UIntPtr" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B79 RID: 7033 RVA: 0x00067E88 File Offset: 0x00066088
		public override bool Equals(object obj)
		{
			if (obj is UIntPtr)
			{
				UIntPtr uintPtr = (UIntPtr)obj;
				return this._pointer == uintPtr._pointer;
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B7A RID: 7034 RVA: 0x00067EB5 File Offset: 0x000660B5
		public override int GetHashCode()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer equal to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of this instance is too large to represent as a 32-bit unsigned integer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B7B RID: 7035 RVA: 0x00067EBE File Offset: 0x000660BE
		public uint ToUInt32()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer equal to the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B7C RID: 7036 RVA: 0x00067EC7 File Offset: 0x000660C7
		public ulong ToUInt64()
		{
			return this._pointer;
		}

		/// <summary>Converts the value of this instance to a pointer to an unspecified type.</summary>
		/// <returns>A pointer to <see cref="T:System.Void" />; that is, a pointer to memory containing data of an unspecified type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B7D RID: 7037 RVA: 0x00067ED0 File Offset: 0x000660D0
		[CLSCompliant(false)]
		public unsafe void* ToPointer()
		{
			return this._pointer;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B7E RID: 7038 RVA: 0x00067ED8 File Offset: 0x000660D8
		public override string ToString()
		{
			if (UIntPtr.Size >= 8)
			{
				return this._pointer.ToString();
			}
			return this._pointer.ToString();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.UIntPtr" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data. </param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06001B7F RID: 7039 RVA: 0x00067F0C File Offset: 0x0006610C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("pointer", this._pointer);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.UIntPtr" /> are equal.</summary>
		/// <returns>true if <paramref name="value1" /> equals <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">The first pointer or handle to compare. </param>
		/// <param name="value2">The second pointer or handle to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001B80 RID: 7040 RVA: 0x00067F2E File Offset: 0x0006612E
		public static bool operator ==(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer == value2._pointer;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.UIntPtr" /> are not equal.</summary>
		/// <returns>true if <paramref name="value1" /> does not equal <paramref name="value2" />; otherwise, false.</returns>
		/// <param name="value1">The first pointer or handle to compare. </param>
		/// <param name="value2">The second pointer or handle to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001B81 RID: 7041 RVA: 0x00067F40 File Offset: 0x00066140
		public static bool operator !=(UIntPtr value1, UIntPtr value2)
		{
			return value1._pointer != value2._pointer;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a 64-bit unsigned integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001B82 RID: 7042 RVA: 0x00067F55 File Offset: 0x00066155
		public static explicit operator ulong(UIntPtr value)
		{
			return value._pointer;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a 32-bit unsigned integer.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert. </param>
		/// <exception cref="T:System.OverflowException">On a 64-bit platform, the value of <paramref name="value" /> is too large to represent as a 32-bit unsigned integer. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001B83 RID: 7043 RVA: 0x00067F5F File Offset: 0x0006615F
		public static explicit operator uint(UIntPtr value)
		{
			return value._pointer;
		}

		/// <summary>Converts the value of a 64-bit unsigned integer to an <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 64-bit unsigned integer. </param>
		/// <exception cref="T:System.OverflowException">On a 32-bit platform, <paramref name="value" /> is too large to represent as an <see cref="T:System.UIntPtr" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001B84 RID: 7044 RVA: 0x00067F69 File Offset: 0x00066169
		public static explicit operator UIntPtr(ulong value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the specified pointer to an unspecified type to a <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A pointer to an unspecified type. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001B85 RID: 7045 RVA: 0x00067F71 File Offset: 0x00066171
		[CLSCompliant(false)]
		public unsafe static explicit operator UIntPtr(void* value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.UIntPtr" /> to a pointer to an unspecified type.</summary>
		/// <returns>The contents of <paramref name="value" />.</returns>
		/// <param name="value">The pointer or handle to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001B86 RID: 7046 RVA: 0x00067F79 File Offset: 0x00066179
		[CLSCompliant(false)]
		public unsafe static explicit operator void*(UIntPtr value)
		{
			return value.ToPointer();
		}

		/// <summary>Converts the value of a 32-bit unsigned integer to an <see cref="T:System.UIntPtr" />.</summary>
		/// <returns>A new instance of <see cref="T:System.UIntPtr" /> initialized to <paramref name="value" />.</returns>
		/// <param name="value">A 32-bit unsigned integer. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001B87 RID: 7047 RVA: 0x00067F82 File Offset: 0x00066182
		public static explicit operator UIntPtr(uint value)
		{
			return new UIntPtr(value);
		}

		/// <summary>Gets the size of this instance.</summary>
		/// <returns>The size of a pointer or handle on this platform, measured in bytes. The value of this property is 4 on a 32-bit platform, and 8 on a 64-bit platform.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0005F1A9 File Offset: 0x0005D3A9
		public unsafe static int Size
		{
			get
			{
				return sizeof(void*);
			}
		}

		/// <summary>Adds an offset to the value of an unsigned pointer.</summary>
		/// <returns>A new unsigned pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		/// <param name="pointer">The unsigned pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		// Token: 0x06001B89 RID: 7049 RVA: 0x00067F8A File Offset: 0x0006618A
		public unsafe static UIntPtr Add(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		/// <summary>Subtracts an offset from the value of an unsigned pointer.</summary>
		/// <returns>A new unsigned pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		/// <param name="pointer">The unsigned pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		// Token: 0x06001B8A RID: 7050 RVA: 0x00067F99 File Offset: 0x00066199
		public unsafe static UIntPtr Subtract(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		/// <summary>Adds an offset to the value of an unsigned pointer.</summary>
		/// <returns>A new unsigned pointer that reflects the addition of <paramref name="offset" /> to <paramref name="pointer" />.</returns>
		/// <param name="pointer">The unsigned pointer to add the offset to.</param>
		/// <param name="offset">The offset to add.</param>
		// Token: 0x06001B8B RID: 7051 RVA: 0x00067F8A File Offset: 0x0006618A
		public unsafe static UIntPtr operator +(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer + offset));
		}

		/// <summary>Subtracts an offset from the value of an unsigned pointer.</summary>
		/// <returns>A new unsigned pointer that reflects the subtraction of <paramref name="offset" /> from <paramref name="pointer" />.</returns>
		/// <param name="pointer">The unsigned pointer to subtract the offset from.</param>
		/// <param name="offset">The offset to subtract.</param>
		// Token: 0x06001B8C RID: 7052 RVA: 0x00067F99 File Offset: 0x00066199
		public unsafe static UIntPtr operator -(UIntPtr pointer, int offset)
		{
			return (UIntPtr)((void*)((byte*)(void*)pointer - offset));
		}

		/// <summary>A read-only field that represents a pointer or handle that has been initialized to zero.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000F68 RID: 3944
		public static readonly UIntPtr Zero = new UIntPtr(0U);

		// Token: 0x04000F69 RID: 3945
		private unsafe void* _pointer;
	}
}
