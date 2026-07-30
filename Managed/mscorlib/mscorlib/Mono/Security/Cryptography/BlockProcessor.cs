using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000081 RID: 129
	internal class BlockProcessor
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x000170D4 File Offset: 0x000152D4
		public BlockProcessor(ICryptoTransform transform)
			: this(transform, transform.InputBlockSize)
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000170E3 File Offset: 0x000152E3
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

		// Token: 0x060003F5 RID: 1013 RVA: 0x00017124 File Offset: 0x00015324
		~BlockProcessor()
		{
			Array.Clear(this.block, 0, this.blockSize);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001715C File Offset: 0x0001535C
		public void Initialize()
		{
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00017177 File Offset: 0x00015377
		public void Core(byte[] rgb)
		{
			this.Core(rgb, 0, rgb.Length);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00017184 File Offset: 0x00015384
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

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001725D File Offset: 0x0001545D
		public byte[] Final()
		{
			return this.transform.TransformFinalBlock(this.block, 0, this.blockCount);
		}

		// Token: 0x04000551 RID: 1361
		private ICryptoTransform transform;

		// Token: 0x04000552 RID: 1362
		private byte[] block;

		// Token: 0x04000553 RID: 1363
		private int blockSize;

		// Token: 0x04000554 RID: 1364
		private int blockCount;
	}
}
