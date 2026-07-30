using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008F RID: 143
	public class BlockProcessor
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x000187E0 File Offset: 0x000169E0
		public BlockProcessor(ICryptoTransform transform)
			: this(transform, transform.InputBlockSize)
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000187EF File Offset: 0x000169EF
		public BlockProcessor(ICryptoTransform transform, int blockSize)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			if (blockSize <= 0)
			{
				throw new ArgumentOutOfRangeException("blockSize");
			}
			this.transform = transform;
			this.blockSize = blockSize;
			this.block = new byte[blockSize];
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018830 File Offset: 0x00016A30
		~BlockProcessor()
		{
			Array.Clear(this.block, 0, this.blockSize);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018868 File Offset: 0x00016A68
		public void Initialize()
		{
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00018883 File Offset: 0x00016A83
		public void Core(byte[] rgb)
		{
			this.Core(rgb, 0, rgb.Length);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00018890 File Offset: 0x00016A90
		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = Math.Min(this.blockSize - this.blockCount, cb);
			Buffer.BlockCopy(rgb, ib, this.block, this.blockCount, num);
			this.blockCount += num;
			if (this.blockCount == this.blockSize)
			{
				this.transform.TransformBlock(this.block, 0, this.blockSize, this.block, 0);
				int num2 = (cb - num) / this.blockSize;
				for (int i = 0; i < num2; i++)
				{
					this.transform.TransformBlock(rgb, num + ib, this.blockSize, this.block, 0);
					num += this.blockSize;
				}
				this.blockCount = cb - num;
				if (this.blockCount > 0)
				{
					Buffer.BlockCopy(rgb, num + ib, this.block, 0, this.blockCount);
				}
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018969 File Offset: 0x00016B69
		public byte[] Final()
		{
			return this.transform.TransformFinalBlock(this.block, 0, this.blockCount);
		}

		// Token: 0x0400039B RID: 923
		private ICryptoTransform transform;

		// Token: 0x0400039C RID: 924
		private byte[] block;

		// Token: 0x0400039D RID: 925
		private int blockSize;

		// Token: 0x0400039E RID: 926
		private int blockCount;
	}
}
