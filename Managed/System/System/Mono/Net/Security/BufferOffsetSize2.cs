using System;

namespace Mono.Net.Security
{
	// Token: 0x02000063 RID: 99
	internal class BufferOffsetSize2 : BufferOffsetSize
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x000058B1 File Offset: 0x00003AB1
		public BufferOffsetSize2(int size)
			: base(new byte[size], 0, 0)
		{
			this.InitialSize = size;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000058C8 File Offset: 0x00003AC8
		public void Reset()
		{
			this.Offset = (this.Size = 0);
			this.TotalBytes = 0;
			if (this.Buffer.Length <= this.InitialSize)
			{
				Array.Clear(this.Buffer, 0, this.Buffer.Length);
			}
			else
			{
				this.Buffer = new byte[this.InitialSize];
			}
			this.Complete = false;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000592C File Offset: 0x00003B2C
		public void MakeRoom(int size)
		{
			if (base.Remaining >= size)
			{
				return;
			}
			int num = size - base.Remaining;
			if (this.Offset == 0 && this.Size == 0)
			{
				this.Buffer = new byte[size];
				return;
			}
			byte[] array = new byte[this.Buffer.Length + num];
			this.Buffer.CopyTo(array, 0);
			this.Buffer = array;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000598D File Offset: 0x00003B8D
		public void AppendData(byte[] buffer, int offset, int size)
		{
			this.MakeRoom(size);
			global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, base.EndOffset, size);
			this.Size += size;
		}

		// Token: 0x04000783 RID: 1923
		public readonly int InitialSize;
	}
}
