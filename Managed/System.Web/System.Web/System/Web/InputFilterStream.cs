using System;
using System.IO;

namespace System.Web
{
	// Token: 0x020000C3 RID: 195
	internal class InputFilterStream : Stream
	{
		// Token: 0x170003CA RID: 970
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00019F35 File Offset: 0x00018135
		internal Stream BaseStream
		{
			set
			{
				this.stream = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00019F3E File Offset: 0x0001813E
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x00019F4B File Offset: 0x0001814B
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00019F59 File Offset: 0x00018159
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00019F66 File Offset: 0x00018166
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00019F76 File Offset: 0x00018176
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00019F83 File Offset: 0x00018183
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this.stream.Seek(offset, loc);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00019F92 File Offset: 0x00018192
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00019F92 File Offset: 0x00018192
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00019F92 File Offset: 0x00018192
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("This stream can not change its size");
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0000393A File Offset: 0x00001B3A
		public override void Flush()
		{
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00019F9E File Offset: 0x0001819E
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x04001069 RID: 4201
		private Stream stream;
	}
}
