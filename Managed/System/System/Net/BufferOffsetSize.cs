using System;

namespace System.Net
{
	// Token: 0x02000480 RID: 1152
	internal class BufferOffsetSize
	{
		// Token: 0x06002227 RID: 8743 RVA: 0x00085084 File Offset: 0x00083284
		internal BufferOffsetSize(byte[] buffer, int offset, int size, bool copyBuffer)
		{
			if (copyBuffer)
			{
				byte[] array = new byte[size];
				global::System.Buffer.BlockCopy(buffer, offset, array, 0, size);
				offset = 0;
				buffer = array;
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000850C7 File Offset: 0x000832C7
		internal BufferOffsetSize(byte[] buffer, bool copyBuffer)
			: this(buffer, 0, buffer.Length, copyBuffer)
		{
		}

		// Token: 0x04001E9B RID: 7835
		internal byte[] Buffer;

		// Token: 0x04001E9C RID: 7836
		internal int Offset;

		// Token: 0x04001E9D RID: 7837
		internal int Size;
	}
}
