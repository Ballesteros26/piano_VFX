using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000093 RID: 147
	internal class MACAlgorithm
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0001BC68 File Offset: 0x00019E68
		public MACAlgorithm(SymmetricAlgorithm algorithm)
		{
			this.algo = algorithm;
			this.algo.Mode = CipherMode.CBC;
			this.blockSize = this.algo.BlockSize >> 3;
			this.algo.IV = new byte[this.blockSize];
			this.block = new byte[this.blockSize];
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001BCC8 File Offset: 0x00019EC8
		public void Initialize(byte[] key)
		{
			this.algo.Key = key;
			if (this.enc == null)
			{
				this.enc = this.algo.CreateEncryptor();
			}
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001BD08 File Offset: 0x00019F08
		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = Math.Min(this.blockSize - this.blockCount, cb);
			Array.Copy(rgb, ib, this.block, this.blockCount, num);
			this.blockCount += num;
			if (this.blockCount == this.blockSize)
			{
				this.enc.TransformBlock(this.block, 0, this.blockSize, this.block, 0);
				int num2 = (cb - num) / this.blockSize;
				for (int i = 0; i < num2; i++)
				{
					this.enc.TransformBlock(rgb, num, this.blockSize, this.block, 0);
					num += this.blockSize;
				}
				this.blockCount = cb - num;
				if (this.blockCount > 0)
				{
					Array.Copy(rgb, num, this.block, 0, this.blockCount);
				}
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		public byte[] Final()
		{
			byte[] array;
			if (this.blockCount > 0 || (this.algo.Padding != PaddingMode.Zeros && this.algo.Padding != PaddingMode.None))
			{
				array = this.enc.TransformFinalBlock(this.block, 0, this.blockCount);
			}
			else
			{
				array = (byte[])this.block.Clone();
			}
			if (!this.enc.CanReuseTransform)
			{
				this.enc.Dispose();
				this.enc = null;
			}
			return array;
		}

		// Token: 0x040005B4 RID: 1460
		private SymmetricAlgorithm algo;

		// Token: 0x040005B5 RID: 1461
		private ICryptoTransform enc;

		// Token: 0x040005B6 RID: 1462
		private byte[] block;

		// Token: 0x040005B7 RID: 1463
		private int blockSize;

		// Token: 0x040005B8 RID: 1464
		private int blockCount;
	}
}
