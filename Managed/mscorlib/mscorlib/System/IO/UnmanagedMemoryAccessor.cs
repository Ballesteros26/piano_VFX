using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	/// <summary>Provides random access to unmanaged blocks of memory from managed code.</summary>
	// Token: 0x020003C4 RID: 964
	public class UnmanagedMemoryAccessor : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryAccessor" /> class. </summary>
		// Token: 0x06002D13 RID: 11539 RVA: 0x000A09CE File Offset: 0x0009EBCE
		protected UnmanagedMemoryAccessor()
		{
			this._isOpen = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryAccessor" /> class with a specified buffer, offset, and capacity.</summary>
		/// <param name="buffer">The buffer to contain the accessor.</param>
		/// <param name="offset">The byte at which to start the accessor.</param>
		/// <param name="capacity">The size, in bytes, of memory to allocate.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> is greater than <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="capacity" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> would wrap around the high end of the address space.</exception>
		// Token: 0x06002D14 RID: 11540 RVA: 0x000A09DD File Offset: 0x0009EBDD
		[SecuritySafeCritical]
		public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity)
		{
			this.Initialize(buffer, offset, capacity, FileAccess.Read);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.UnmanagedMemoryAccessor" /> class with a specified buffer, offset, capacity, and access right.</summary>
		/// <param name="buffer">The buffer to contain the accessor.</param>
		/// <param name="offset">The byte at which to start the accessor.</param>
		/// <param name="capacity">The size, in bytes, of memory to allocate.</param>
		/// <param name="access">The type of access allowed to the memory. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> is greater than <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="capacity" /> is less than zero.-or-<paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> would wrap around the high end of the address space.</exception>
		// Token: 0x06002D15 RID: 11541 RVA: 0x000A09EF File Offset: 0x0009EBEF
		[SecuritySafeCritical]
		public UnmanagedMemoryAccessor(SafeBuffer buffer, long offset, long capacity, FileAccess access)
		{
			this.Initialize(buffer, offset, capacity, access);
		}

		/// <summary>Sets the initial values for the accessor.</summary>
		/// <param name="buffer">The buffer to contain the accessor.</param>
		/// <param name="offset">The byte at which to start the accessor.</param>
		/// <param name="capacity">The size, in bytes, of memory to allocate.</param>
		/// <param name="access">The type of access allowed to the memory. The default is <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileAccess.ReadWrite" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> is greater than <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="capacity" /> is less than zero.-or-<paramref name="access" /> is not a valid <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileAccess" /> enumeration value.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="offset" /> plus <paramref name="capacity" /> would wrap around the high end of the address space.</exception>
		// Token: 0x06002D16 RID: 11542 RVA: 0x000A0A04 File Offset: 0x0009EC04
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected unsafe void Initialize(SafeBuffer buffer, long offset, long capacity, FileAccess access)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.ByteLength < (ulong)(offset + capacity))
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and capacity were greater than the size of the view."));
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The method cannot be called twice on the same instance."));
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + capacity < ptr)
				{
					throw new ArgumentException(Environment.GetResourceString("The UnmanagedMemoryAccessor capacity and offset would wrap around the high end of the address space."));
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
			this._canRead = (this._access & FileAccess.Read) > (FileAccess)0;
			this._canWrite = (this._access & FileAccess.Write) > (FileAccess)0;
		}

		/// <summary>Gets the capacity of the accessor.</summary>
		/// <returns>The capacity of the accessor.</returns>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x000A0B2C File Offset: 0x0009ED2C
		public long Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		/// <summary>Determines whether the accessor is readable.</summary>
		/// <returns>true if the accessor is readable; otherwise, false. </returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000A0B34 File Offset: 0x0009ED34
		public bool CanRead
		{
			get
			{
				return this._isOpen && this._canRead;
			}
		}

		/// <summary>Determines whether the accessory is writable.</summary>
		/// <returns>true if the accessor is writable; otherwise, false. </returns>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000A0B46 File Offset: 0x0009ED46
		public bool CanWrite
		{
			get
			{
				return this._isOpen && this._canWrite;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.UnmanagedMemoryAccessor" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002D1A RID: 11546 RVA: 0x000A0B58 File Offset: 0x0009ED58
		protected virtual void Dispose(bool disposing)
		{
			this._isOpen = false;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.UnmanagedMemoryAccessor" />. </summary>
		// Token: 0x06002D1B RID: 11547 RVA: 0x000A0B61 File Offset: 0x0009ED61
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Determines whether the accessor is currently open by a process.</summary>
		/// <returns>true if the accessor is open; otherwise, false. </returns>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000A0B70 File Offset: 0x0009ED70
		protected bool IsOpen
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Reads a Boolean value from the accessor.</summary>
		/// <returns>true or false.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading. </param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D1D RID: 11549 RVA: 0x000A0B78 File Offset: 0x0009ED78
		public bool ReadBoolean(long position)
		{
			int num = 1;
			this.EnsureSafeToRead(position, num);
			return this.InternalReadByte(position) > 0;
		}

		/// <summary>Reads a byte value from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D1E RID: 11550 RVA: 0x000A0B9C File Offset: 0x0009ED9C
		public byte ReadByte(long position)
		{
			int num = 1;
			this.EnsureSafeToRead(position, num);
			return this.InternalReadByte(position);
		}

		/// <summary>Reads a character from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D1F RID: 11551 RVA: 0x000A0BBC File Offset: 0x0009EDBC
		[SecuritySafeCritical]
		public unsafe char ReadChar(long position)
		{
			int num = 2;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			char c;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				c = (char)(*(ushort*)ptr);
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return c;
		}

		/// <summary>Reads a 16-bit integer from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D20 RID: 11552 RVA: 0x000A0C1C File Offset: 0x0009EE1C
		[SecuritySafeCritical]
		public unsafe short ReadInt16(long position)
		{
			int num = 2;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			short num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num2 = *(short*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a 32-bit integer from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D21 RID: 11553 RVA: 0x000A0C7C File Offset: 0x0009EE7C
		[SecuritySafeCritical]
		public unsafe int ReadInt32(long position)
		{
			int num = 4;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			int num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num2 = *(int*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a 64-bit integer from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D22 RID: 11554 RVA: 0x000A0CDC File Offset: 0x0009EEDC
		[SecuritySafeCritical]
		public unsafe long ReadInt64(long position)
		{
			int num = 8;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			long num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num2 = *(long*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a decimal value from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.-or-The decimal to read is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D23 RID: 11555 RVA: 0x000A0D3C File Offset: 0x0009EF3C
		[SecuritySafeCritical]
		public decimal ReadDecimal(long position)
		{
			int num = 16;
			this.EnsureSafeToRead(position, num);
			int[] array = new int[4];
			this.ReadArray<int>(position, array, 0, array.Length);
			return new decimal(array);
		}

		/// <summary>Reads a single-precision floating-point value from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D24 RID: 11556 RVA: 0x000A0D70 File Offset: 0x0009EF70
		[SecuritySafeCritical]
		public unsafe float ReadSingle(long position)
		{
			int num = 4;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			float num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num2 = *(float*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a double-precision floating-point value from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D25 RID: 11557 RVA: 0x000A0DD0 File Offset: 0x0009EFD0
		[SecuritySafeCritical]
		public unsafe double ReadDouble(long position)
		{
			int num = 8;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			double num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num2 = *(double*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads an 8-bit signed integer from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D26 RID: 11558 RVA: 0x000A0E30 File Offset: 0x0009F030
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe sbyte ReadSByte(long position)
		{
			int num = 1;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			sbyte b;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				b = *(sbyte*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return b;
		}

		/// <summary>Reads an unsigned 16-bit integer from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D27 RID: 11559 RVA: 0x000A0E90 File Offset: 0x0009F090
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe ushort ReadUInt16(long position)
		{
			int num = 2;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			ushort num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num2 = *(ushort*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads an unsigned 32-bit integer from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D28 RID: 11560 RVA: 0x000A0EF0 File Offset: 0x0009F0F0
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe uint ReadUInt32(long position)
		{
			int num = 4;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			uint num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num2 = *(uint*)ptr;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads an unsigned 64-bit integer from the accessor.</summary>
		/// <returns>The value that was read.</returns>
		/// <param name="position">The number of bytes into the accessor at which to begin reading.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D29 RID: 11561 RVA: 0x000A0F50 File Offset: 0x0009F150
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public unsafe ulong ReadUInt64(long position)
		{
			int num = 8;
			this.EnsureSafeToRead(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			ulong num2;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				num2 = (ulong)(*(long*)ptr);
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return num2;
		}

		/// <summary>Reads a structure of type <paramref name="T" /> from the accessor into a provided reference.</summary>
		/// <param name="position">The position in the accessor at which to begin reading.</param>
		/// <param name="structure">The structure to contain the read data.</param>
		/// <typeparam name="T">The type of structure.</typeparam>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to read in a structure of type <paramref name="T" />.-or-T is a value type that contains one or more reference types.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D2A RID: 11562 RVA: 0x000A0FB0 File Offset: 0x0009F1B0
		[SecurityCritical]
		public void Read<T>(long position, out T structure) where T : struct
		{
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("Non-negative number required."));
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("Cannot access a closed accessor."));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("Accessor does not support reading."));
			}
			uint num = Marshal.SizeOfType(typeof(T));
			if (position <= this._capacity - (long)((ulong)num))
			{
				structure = this._buffer.Read<T>((ulong)(this._offset + position));
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("The position may not be greater or equal to the capacity of the accessor."));
			}
			throw new ArgumentException(Environment.GetResourceString("There are not enough bytes remaining in the accessor to read at this position.", new object[] { typeof(T).FullName }), "position");
		}

		/// <summary>Reads structures of type <paramref name="T" /> from the accessor into an array of type <paramref name="T" />.</summary>
		/// <returns>The number of structures read into <paramref name="array" />. This value can be less than <paramref name="count" /> if there are fewer structures available, or zero if the end of the accessor is reached.</returns>
		/// <param name="position">The number of bytes in the accessor at which to begin reading.</param>
		/// <param name="array">The array to contain the structures read from the accessor.</param>
		/// <param name="offset">The index in <paramref name="array" /> in which to place the first copied structure. </param>
		/// <param name="count">The number of structures of type <paramref name="T" /> to read from the accessor.</param>
		/// <typeparam name="T">The type of structure.</typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not large enough to contain <paramref name="count" /> of structures (starting from <paramref name="position" />). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D2B RID: 11563 RVA: 0x000A108C File Offset: 0x0009F28C
		[SecurityCritical]
		public int ReadArray<T>(long position, T[] array, int offset, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (!this.CanRead)
			{
				if (!this._isOpen)
				{
					throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("Cannot access a closed accessor."));
				}
				throw new NotSupportedException(Environment.GetResourceString("Accessor does not support reading."));
			}
			else
			{
				if (position < 0L)
				{
					throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("Non-negative number required."));
				}
				uint num = Marshal.AlignedSizeOf<T>();
				if (position >= this._capacity)
				{
					throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("The position may not be greater or equal to the capacity of the accessor."));
				}
				int num2 = count;
				long num3 = this._capacity - position;
				if (num3 < 0L)
				{
					num2 = 0;
				}
				else
				{
					ulong num4 = (ulong)num * (ulong)((long)count);
					if (num3 < (long)num4)
					{
						num2 = (int)(num3 / (long)((ulong)num));
					}
				}
				this._buffer.ReadArray<T>((ulong)(this._offset + position), array, offset, num2);
				return num2;
			}
		}

		/// <summary>Writes a Boolean value into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D2C RID: 11564 RVA: 0x000A11A8 File Offset: 0x0009F3A8
		public void Write(long position, bool value)
		{
			int num = 1;
			this.EnsureSafeToWrite(position, num);
			byte b = (value ? 1 : 0);
			this.InternalWrite(position, b);
		}

		/// <summary>Writes a byte value into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D2D RID: 11565 RVA: 0x000A11D0 File Offset: 0x0009F3D0
		public void Write(long position, byte value)
		{
			int num = 1;
			this.EnsureSafeToWrite(position, num);
			this.InternalWrite(position, value);
		}

		/// <summary>Writes a character into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D2E RID: 11566 RVA: 0x000A11F0 File Offset: 0x0009F3F0
		[SecuritySafeCritical]
		public unsafe void Write(long position, char value)
		{
			int num = 2;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(short*)ptr = (short)value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a 16-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D2F RID: 11567 RVA: 0x000A1250 File Offset: 0x0009F450
		[SecuritySafeCritical]
		public unsafe void Write(long position, short value)
		{
			int num = 2;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(short*)ptr = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a 32-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D30 RID: 11568 RVA: 0x000A12B0 File Offset: 0x0009F4B0
		[SecuritySafeCritical]
		public unsafe void Write(long position, int value)
		{
			int num = 4;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(int*)ptr = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a 64-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after position to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D31 RID: 11569 RVA: 0x000A1310 File Offset: 0x0009F510
		[SecuritySafeCritical]
		public unsafe void Write(long position, long value)
		{
			int num = 8;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(long*)ptr = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a decimal value into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.-or-The decimal is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D32 RID: 11570 RVA: 0x000A1370 File Offset: 0x0009F570
		[SecuritySafeCritical]
		public void Write(long position, decimal value)
		{
			int num = 16;
			this.EnsureSafeToWrite(position, num);
			byte[] array = new byte[16];
			decimal.GetBytes(value, array);
			int[] array2 = new int[4];
			int num2 = (int)array[12] | ((int)array[13] << 8) | ((int)array[14] << 16) | ((int)array[15] << 24);
			int num3 = (int)array[0] | ((int)array[1] << 8) | ((int)array[2] << 16) | ((int)array[3] << 24);
			int num4 = (int)array[4] | ((int)array[5] << 8) | ((int)array[6] << 16) | ((int)array[7] << 24);
			int num5 = (int)array[8] | ((int)array[9] << 8) | ((int)array[10] << 16) | ((int)array[11] << 24);
			array2[0] = num3;
			array2[1] = num4;
			array2[2] = num5;
			array2[3] = num2;
			this.WriteArray<int>(position, array2, 0, array2.Length);
		}

		/// <summary>Writes a Single into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D33 RID: 11571 RVA: 0x000A1428 File Offset: 0x0009F628
		[SecuritySafeCritical]
		public unsafe void Write(long position, float value)
		{
			int num = 4;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(float*)ptr = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a Double value into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D34 RID: 11572 RVA: 0x000A1488 File Offset: 0x0009F688
		[SecuritySafeCritical]
		public unsafe void Write(long position, double value)
		{
			int num = 8;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(double*)ptr = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes an 8-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D35 RID: 11573 RVA: 0x000A14E8 File Offset: 0x0009F6E8
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe void Write(long position, sbyte value)
		{
			int num = 1;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*ptr = (byte)value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes an unsigned 16-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D36 RID: 11574 RVA: 0x000A1548 File Offset: 0x0009F748
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public unsafe void Write(long position, ushort value)
		{
			int num = 2;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(short*)ptr = (short)value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes an unsigned 32-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D37 RID: 11575 RVA: 0x000A15A8 File Offset: 0x0009F7A8
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe void Write(long position, uint value)
		{
			int num = 4;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(int*)ptr = (int)value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes an unsigned 64-bit integer into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes after <paramref name="position" /> to write a value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D38 RID: 11576 RVA: 0x000A1608 File Offset: 0x0009F808
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		public unsafe void Write(long position, ulong value)
		{
			int num = 8;
			this.EnsureSafeToWrite(position, num);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				ptr += this._offset + position;
				*(long*)ptr = (long)value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		/// <summary>Writes a structure into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="structure">The structure to write.</param>
		/// <typeparam name="T">The type of structure.</typeparam>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes in the accessor after <paramref name="position" /> to write a structure of type <paramref name="T" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D39 RID: 11577 RVA: 0x000A1668 File Offset: 0x0009F868
		[SecurityCritical]
		public void Write<T>(long position, ref T structure) where T : struct
		{
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("Non-negative number required."));
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("Cannot access a closed accessor."));
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("Accessor does not support writing."));
			}
			uint num = Marshal.SizeOfType(typeof(T));
			if (position <= this._capacity - (long)((ulong)num))
			{
				this._buffer.Write<T>((ulong)(this._offset + position), structure);
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("The position may not be greater or equal to the capacity of the accessor."));
			}
			throw new ArgumentException(Environment.GetResourceString("There are not enough bytes remaining in the accessor to write at this position.", new object[] { typeof(T).FullName }), "position");
		}

		/// <summary>Writes structures from an array of type <paramref name="T" /> into the accessor.</summary>
		/// <param name="position">The number of bytes into the accessor at which to begin writing.</param>
		/// <param name="array">The array to write into the accessor.</param>
		/// <param name="offset">The index in <paramref name="array" /> to start writing from.</param>
		/// <param name="count">The number of structures in <paramref name="array" /> to write.</param>
		/// <typeparam name="T">The type of structure.</typeparam>
		/// <exception cref="T:System.ArgumentException">There are not enough bytes in the accessor after <paramref name="position" /> to write the number of structures specified by <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> is less than zero or greater than the capacity of the accessor.-or-<paramref name="offset" /> or <paramref name="count" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The accessor does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The accessor has been disposed.</exception>
		// Token: 0x06002D3A RID: 11578 RVA: 0x000A1744 File Offset: 0x0009F944
		[SecurityCritical]
		public void WriteArray<T>(long position, T[] array, int offset, int count) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("Non-negative number required."));
			}
			if (position >= this.Capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("The position may not be greater or equal to the capacity of the accessor."));
			}
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("Cannot access a closed accessor."));
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("Accessor does not support writing."));
			}
			this._buffer.WriteArray<T>((ulong)(this._offset + position), array, offset, count);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000A1834 File Offset: 0x0009FA34
		[SecuritySafeCritical]
		private unsafe byte InternalReadByte(long position)
		{
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte b;
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				b = (ptr + this._offset)[position];
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
			return b;
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000A1888 File Offset: 0x0009FA88
		[SecuritySafeCritical]
		private unsafe void InternalWrite(long position, byte value)
		{
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._buffer.AcquirePointer(ref ptr);
				(ptr + this._offset)[position] = value;
			}
			finally
			{
				if (ptr != null)
				{
					this._buffer.ReleasePointer();
				}
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000A18DC File Offset: 0x0009FADC
		private void EnsureSafeToRead(long position, int sizeOfType)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("Cannot access a closed accessor."));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("Accessor does not support reading."));
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("Non-negative number required."));
			}
			if (position <= this._capacity - (long)sizeOfType)
			{
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("The position may not be greater or equal to the capacity of the accessor."));
			}
			throw new ArgumentException(Environment.GetResourceString("There are not enough bytes remaining in the accessor to read at this position."), "position");
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000A1978 File Offset: 0x0009FB78
		private void EnsureSafeToWrite(long position, int sizeOfType)
		{
			if (!this._isOpen)
			{
				throw new ObjectDisposedException("UnmanagedMemoryAccessor", Environment.GetResourceString("Cannot access a closed accessor."));
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("Accessor does not support writing."));
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("Non-negative number required."));
			}
			if (position <= this._capacity - (long)sizeOfType)
			{
				return;
			}
			if (position >= this._capacity)
			{
				throw new ArgumentOutOfRangeException("position", Environment.GetResourceString("The position may not be greater or equal to the capacity of the accessor."));
			}
			throw new ArgumentException(Environment.GetResourceString("There are not enough bytes remaining in the accessor to write at this position.", new object[] { "Byte" }), "position");
		}

		// Token: 0x04001782 RID: 6018
		[SecurityCritical]
		private SafeBuffer _buffer;

		// Token: 0x04001783 RID: 6019
		private long _offset;

		// Token: 0x04001784 RID: 6020
		private long _capacity;

		// Token: 0x04001785 RID: 6021
		private FileAccess _access;

		// Token: 0x04001786 RID: 6022
		private bool _isOpen;

		// Token: 0x04001787 RID: 6023
		private bool _canRead;

		// Token: 0x04001788 RID: 6024
		private bool _canWrite;
	}
}
