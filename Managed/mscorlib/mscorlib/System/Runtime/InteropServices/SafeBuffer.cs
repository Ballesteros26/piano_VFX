using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a controlled memory buffer that can be used for reading and writing. Attempts to access memory outside the controlled buffer (underruns and overruns) raise exceptions.</summary>
	// Token: 0x02000926 RID: 2342
	public abstract class SafeBuffer : SafeHandleZeroOrMinusOneIsInvalid, IDisposable
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> class, and specifies whether the buffer handle is to be reliably released. </summary>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x060056E5 RID: 22245 RVA: 0x0012A8E5 File Offset: 0x00128AE5
		protected SafeBuffer(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		/// <summary>Defines the allocation size of the memory region in bytes. You must call this method before you use the <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> instance.</summary>
		/// <param name="numBytes">The number of bytes in the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="numBytes" /> is less than zero.-or-<paramref name="numBytes" /> is greater than the available address space.</exception>
		// Token: 0x060056E6 RID: 22246 RVA: 0x0012A8EE File Offset: 0x00128AEE
		[CLSCompliant(false)]
		public unsafe void Initialize(ulong numBytes)
		{
			if (numBytes == 0UL)
			{
				throw new ArgumentOutOfRangeException("numBytes");
			}
			this.inited = true;
			this.byte_length = numBytes;
			this.last_byte = (byte*)(void*)this.handle + numBytes;
		}

		/// <summary>Specifies the allocation size of the memory buffer by using the specified number of elements and element size. You must call this method before you use the <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> instance.</summary>
		/// <param name="numElements">The number of elements in the buffer.</param>
		/// <param name="sizeOfEachElement">The size of each element in the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="numElements" /> is less than zero. -or-<paramref name="sizeOfEachElement" /> is less than zero.-or-<paramref name="numElements" /> multiplied by <paramref name="sizeOfEachElement" /> is greater than the available address space.</exception>
		// Token: 0x060056E7 RID: 22247 RVA: 0x0012A920 File Offset: 0x00128B20
		[CLSCompliant(false)]
		public void Initialize(uint numElements, uint sizeOfEachElement)
		{
			this.Initialize((ulong)(numElements * sizeOfEachElement));
		}

		/// <summary>Defines the allocation size of the memory region by specifying the number of value types. You must call this method before you use the <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> instance.</summary>
		/// <param name="numElements">The number of elements of the value type to allocate memory for.</param>
		/// <typeparam name="T">The value type to allocate memory for.</typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="numElements" /> is less than zero.-or-<paramref name="numElements" /> multiplied by the size of each element is greater than the available address space.</exception>
		// Token: 0x060056E8 RID: 22248 RVA: 0x0012A92C File Offset: 0x00128B2C
		[CLSCompliant(false)]
		public void Initialize<T>(uint numElements) where T : struct
		{
			this.Initialize(numElements, (uint)Marshal.SizeOf(typeof(T)));
		}

		/// <summary>Obtains a pointer from a <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> object for a block of memory.</summary>
		/// <param name="pointer">A byte pointer, passed by reference, to receive the pointer from within the <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> object. You must set this pointer to null before you call this method.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called. </exception>
		// Token: 0x060056E9 RID: 22249 RVA: 0x0012A944 File Offset: 0x00128B44
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void AcquirePointer(ref byte* pointer)
		{
			if (!this.inited)
			{
				throw new InvalidOperationException();
			}
			bool flag = false;
			base.DangerousAddRef(ref flag);
			if (flag)
			{
				pointer = (void*)this.handle;
			}
		}

		/// <summary>Releases a pointer that was obtained by the <see cref="M:System.Runtime.InteropServices.SafeBuffer.AcquirePointer(System.Byte*@)" /> method.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called.</exception>
		// Token: 0x060056EA RID: 22250 RVA: 0x0012A979 File Offset: 0x00128B79
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleasePointer()
		{
			if (!this.inited)
			{
				throw new InvalidOperationException();
			}
			base.DangerousRelease();
		}

		/// <summary>Gets the size of the buffer, in bytes.</summary>
		/// <returns>The number of bytes in the memory buffer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called.</exception>
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060056EB RID: 22251 RVA: 0x0012A98F File Offset: 0x00128B8F
		[CLSCompliant(false)]
		public ulong ByteLength
		{
			get
			{
				return this.byte_length;
			}
		}

		/// <summary>Reads a value type from memory at the specified offset.</summary>
		/// <returns>The value type that was read from memory.</returns>
		/// <param name="byteOffset">The location from which to read the value type. You may have to consider alignment issues.</param>
		/// <typeparam name="T">The value type to read.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called.</exception>
		// Token: 0x060056EC RID: 22252 RVA: 0x0012A998 File Offset: 0x00128B98
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[CLSCompliant(false)]
		public unsafe T Read<T>(ulong byteOffset) where T : struct
		{
			if (!this.inited)
			{
				throw new InvalidOperationException();
			}
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			if (ptr >= this.last_byte || ptr + Marshal.SizeOf(typeof(T)) != this.last_byte)
			{
				throw new ArgumentException("byteOffset");
			}
			return (T)((object)Marshal.PtrToStructure((IntPtr)((void*)ptr), typeof(T)));
		}

		/// <summary>Reads the specified number of value types from memory starting at the offset, and writes them into an array starting at the index. </summary>
		/// <param name="byteOffset">The location from which to start reading.</param>
		/// <param name="array">The output array to write to.</param>
		/// <param name="index">The location in the output array to begin writing to.</param>
		/// <param name="count">The number of value types to read from the input array and to write to the output array.</param>
		/// <typeparam name="T">The value type to read.</typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the array minus the index is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called.</exception>
		// Token: 0x060056ED RID: 22253 RVA: 0x0012AA0C File Offset: 0x00128C0C
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void ReadArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
		{
			if (!this.inited)
			{
				throw new InvalidOperationException();
			}
			int num = Marshal.SizeOf(typeof(T)) * count;
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			if (ptr >= this.last_byte || ptr + num != this.last_byte)
			{
				throw new ArgumentException("byteOffset");
			}
			Marshal.copy_from_unmanaged((IntPtr)((void*)ptr), index, array, count);
		}

		/// <summary>Writes a value type to memory at the given location.</summary>
		/// <param name="byteOffset">The location at which to start writing. You may have to consider alignment issues.</param>
		/// <param name="value">The value to write.</param>
		/// <typeparam name="T">The value type to write.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called.</exception>
		// Token: 0x060056EE RID: 22254 RVA: 0x0012AA78 File Offset: 0x00128C78
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void Write<T>(ulong byteOffset, T value) where T : struct
		{
			if (!this.inited)
			{
				throw new InvalidOperationException();
			}
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			if (ptr >= this.last_byte || ptr + Marshal.SizeOf(typeof(T)) != this.last_byte)
			{
				throw new ArgumentException("byteOffset");
			}
			Marshal.StructureToPtr<T>(value, (IntPtr)((void*)ptr), false);
		}

		/// <summary>Writes the specified number of value types to a memory location by reading bytes starting from the specified location in the input array.</summary>
		/// <param name="byteOffset">The location in memory to write to.</param>
		/// <param name="array">The input array.</param>
		/// <param name="index">The offset in the array to start reading from.</param>
		/// <param name="count">The number of value types to write.</param>
		/// <typeparam name="T">The value type to write.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the input array minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called.</exception>
		// Token: 0x060056EF RID: 22255 RVA: 0x0012AADC File Offset: 0x00128CDC
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe void WriteArray<T>(ulong byteOffset, T[] array, int index, int count) where T : struct
		{
			if (!this.inited)
			{
				throw new InvalidOperationException();
			}
			byte* ptr = (byte*)(void*)this.handle + byteOffset;
			int num = Marshal.SizeOf(typeof(T)) * count;
			if (ptr >= this.last_byte || ptr + num != this.last_byte)
			{
				throw new ArgumentException("would overrite");
			}
			Marshal.copy_to_unmanaged(array, index, (IntPtr)((void*)ptr), count);
		}

		// Token: 0x04002DE4 RID: 11748
		private ulong byte_length;

		// Token: 0x04002DE5 RID: 11749
		private unsafe byte* last_byte;

		// Token: 0x04002DE6 RID: 11750
		private bool inited;
	}
}
