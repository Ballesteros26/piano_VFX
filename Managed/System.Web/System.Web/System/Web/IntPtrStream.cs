using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x020000C4 RID: 196
	internal class IntPtrStream : Stream
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x00019FAB File Offset: 0x000181AB
		public unsafe IntPtrStream(IntPtr base_address, int size)
		{
			this.base_address = (byte*)(void*)base_address;
			this.size = size;
			this.owns = true;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00019FD0 File Offset: 0x000181D0
		public IntPtrStream(Stream stream)
		{
			IntPtrStream intPtrStream = (IntPtrStream)stream;
			this.size = intPtrStream.size;
			this.base_address = intPtrStream.base_address;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0001A002 File Offset: 0x00018202
		protected unsafe IntPtr BaseAddress
		{
			get
			{
				return (IntPtr)((void*)this.base_address);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0001A00F File Offset: 0x0001820F
		protected int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0001A017 File Offset: 0x00018217
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0001A020 File Offset: 0x00018220
		public override long Position
		{
			get
			{
				return (long)this.position;
			}
			set
			{
				if (this.position < 0)
				{
					throw new ArgumentOutOfRangeException("Position", "Can not be negative");
				}
				if (this.position > this.size)
				{
					throw new ArgumentOutOfRangeException("Position", "Pointer falls out of range");
				}
				this.position = (int)value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0001A06C File Offset: 0x0001826C
		public override long Length
		{
			get
			{
				return (long)this.size;
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001A078 File Offset: 0x00018278
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (this.base_address == null)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (this.position >= this.size || count == 0)
			{
				return 0;
			}
			if (this.position > this.size - count)
			{
				count = this.size - this.position;
			}
			Marshal.Copy((IntPtr)((void*)(this.base_address + this.position)), buffer, offset, count);
			this.position += count;
			return count;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001A130 File Offset: 0x00018330
		public unsafe override int ReadByte()
		{
			if (this.position >= this.size)
			{
				return -1;
			}
			if (this.base_address == null)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			int num = this.base_address;
			int num2 = this.position;
			this.position = num2 + 1;
			return (int)(*(num + num2));
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001A17C File Offset: 0x0001837C
		public override long Seek(long offset, SeekOrigin loc)
		{
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("Offset out of range. " + offset);
			}
			if (this.base_address == null)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			int num;
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("Attempted to seek before start of MemoryStream.");
				}
				num = 0;
				break;
			case SeekOrigin.Current:
				num = this.position;
				break;
			case SeekOrigin.End:
				num = this.size;
				break;
			default:
				throw new ArgumentException("loc", "Invalid SeekOrigin");
			}
			checked
			{
				try
				{
					num += (int)offset;
				}
				catch
				{
					throw new ArgumentOutOfRangeException("Too large seek destination");
				}
				if (num < 0)
				{
					throw new IOException("Attempted to seek before start of MemoryStream.");
				}
				this.position = num;
			}
			return (long)this.position;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00019F92 File Offset: 0x00018192
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00019F92 File Offset: 0x00018192
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00019F92 File Offset: 0x00018192
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void Flush()
		{
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001A248 File Offset: 0x00018448
		public unsafe override void Close()
		{
			if (this.owns)
			{
				IntPtr intPtr = (IntPtr)((void*)this.base_address);
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				this.base_address = null;
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001A284 File Offset: 0x00018484
		protected unsafe override void Finalize()
		{
			try
			{
				if (this.owns)
				{
					IntPtr intPtr = (IntPtr)((void*)this.base_address);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					this.base_address = null;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0400106A RID: 4202
		private unsafe byte* base_address;

		// Token: 0x0400106B RID: 4203
		private int size;

		// Token: 0x0400106C RID: 4204
		private int position;

		// Token: 0x0400106D RID: 4205
		private bool owns;
	}
}
