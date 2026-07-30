using System;
using System.IO;

namespace System.Web
{
	// Token: 0x020000C9 RID: 201
	internal sealed class OutputFilterStream : Stream
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x0001CEE3 File Offset: 0x0001B0E3
		public OutputFilterStream(HttpResponseStream stream)
		{
			this.stream = stream;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00003A01 File Offset: 0x00001C01
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x00003A01 File Offset: 0x00001C01
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00003A01 File Offset: 0x00001C01
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00003A01 File Offset: 0x00001C01
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00003A01 File Offset: 0x00001C01
		public override int ReadByte()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00003A01 File Offset: 0x00001C01
		public override long Seek(long offset, SeekOrigin loc)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00019F92 File Offset: 0x00018192
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001CEF2 File Offset: 0x0001B0F2
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void Flush()
		{
		}

		// Token: 0x04001071 RID: 4209
		private HttpResponseStream stream;
	}
}
