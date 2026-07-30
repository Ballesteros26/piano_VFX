using System;

namespace Mono.Net.Security
{
	// Token: 0x02000062 RID: 98
	internal class BufferOffsetSize
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00005804 File Offset: 0x00003A04
		public int EndOffset
		{
			get
			{
				return this.Offset + this.Size;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00005813 File Offset: 0x00003A13
		public int Remaining
		{
			get
			{
				return this.Buffer.Length - this.Offset - this.Size;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000582C File Offset: 0x00003A2C
		public BufferOffsetSize(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
			this.Complete = false;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000588F File Offset: 0x00003A8F
		public override string ToString()
		{
			return string.Format("[BufferOffsetSize: {0} {1}]", this.Offset, this.Size);
		}

		// Token: 0x0400077E RID: 1918
		public byte[] Buffer;

		// Token: 0x0400077F RID: 1919
		public int Offset;

		// Token: 0x04000780 RID: 1920
		public int Size;

		// Token: 0x04000781 RID: 1921
		public int TotalBytes;

		// Token: 0x04000782 RID: 1922
		public bool Complete;
	}
}
