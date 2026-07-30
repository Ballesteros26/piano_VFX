using System;

namespace System.Net
{
	// Token: 0x02000499 RID: 1177
	internal class ScatterGatherBuffers
	{
		// Token: 0x060022D0 RID: 8912 RVA: 0x00086BAC File Offset: 0x00084DAC
		internal ScatterGatherBuffers()
		{
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00086BBF File Offset: 0x00084DBF
		internal ScatterGatherBuffers(long totalSize)
		{
			if (totalSize > 0L)
			{
				this.currentChunk = this.AllocateMemoryChunk((totalSize > 2147483647L) ? int.MaxValue : ((int)totalSize));
			}
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x00086BF8 File Offset: 0x00084DF8
		internal BufferOffsetSize[] GetBuffers()
		{
			if (this.Empty)
			{
				return null;
			}
			BufferOffsetSize[] array = new BufferOffsetSize[this.chunkCount];
			int num = 0;
			for (ScatterGatherBuffers.MemoryChunk next = this.headChunk; next != null; next = next.Next)
			{
				array[num] = new BufferOffsetSize(next.Buffer, 0, next.FreeOffset, false);
				num++;
			}
			return array;
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x00086C4B File Offset: 0x00084E4B
		private bool Empty
		{
			get
			{
				return this.headChunk == null || this.chunkCount == 0;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x00086C60 File Offset: 0x00084E60
		internal int Length
		{
			get
			{
				return this.totalLength;
			}
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00086C68 File Offset: 0x00084E68
		internal void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = (this.Empty ? 0 : (this.currentChunk.Buffer.Length - this.currentChunk.FreeOffset));
				if (num == 0)
				{
					ScatterGatherBuffers.MemoryChunk memoryChunk = this.AllocateMemoryChunk(count);
					if (this.currentChunk != null)
					{
						this.currentChunk.Next = memoryChunk;
					}
					this.currentChunk = memoryChunk;
				}
				int num2 = ((count < num) ? count : num);
				Buffer.BlockCopy(buffer, offset, this.currentChunk.Buffer, this.currentChunk.FreeOffset, num2);
				offset += num2;
				count -= num2;
				this.totalLength += num2;
				this.currentChunk.FreeOffset += num2;
			}
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00086D20 File Offset: 0x00084F20
		private ScatterGatherBuffers.MemoryChunk AllocateMemoryChunk(int newSize)
		{
			if (newSize > this.nextChunkLength)
			{
				this.nextChunkLength = newSize;
			}
			ScatterGatherBuffers.MemoryChunk memoryChunk = new ScatterGatherBuffers.MemoryChunk(this.nextChunkLength);
			if (this.Empty)
			{
				this.headChunk = memoryChunk;
			}
			this.nextChunkLength *= 2;
			this.chunkCount++;
			return memoryChunk;
		}

		// Token: 0x04001F29 RID: 7977
		private ScatterGatherBuffers.MemoryChunk headChunk;

		// Token: 0x04001F2A RID: 7978
		private ScatterGatherBuffers.MemoryChunk currentChunk;

		// Token: 0x04001F2B RID: 7979
		private int nextChunkLength = 1024;

		// Token: 0x04001F2C RID: 7980
		private int totalLength;

		// Token: 0x04001F2D RID: 7981
		private int chunkCount;

		// Token: 0x0200049A RID: 1178
		private class MemoryChunk
		{
			// Token: 0x060022D7 RID: 8919 RVA: 0x00086D75 File Offset: 0x00084F75
			internal MemoryChunk(int bufferSize)
			{
				this.Buffer = new byte[bufferSize];
			}

			// Token: 0x04001F2E RID: 7982
			internal byte[] Buffer;

			// Token: 0x04001F2F RID: 7983
			internal int FreeOffset;

			// Token: 0x04001F30 RID: 7984
			internal ScatterGatherBuffers.MemoryChunk Next;
		}
	}
}
