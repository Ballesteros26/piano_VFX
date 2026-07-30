using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000092 RID: 146
	internal class HMACAlgorithm
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0001BA59 File Offset: 0x00019C59
		public HMACAlgorithm(string algoName)
		{
			this.CreateHash(algoName);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001BA68 File Offset: 0x00019C68
		~HMACAlgorithm()
		{
			this.Dispose();
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001BA94 File Offset: 0x00019C94
		private void CreateHash(string algoName)
		{
			this.algo = HashAlgorithm.Create(algoName);
			this.hashName = algoName;
			this.block = new BlockProcessor(this.algo, 8);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001BABB File Offset: 0x00019CBB
		public void Dispose()
		{
			if (this.key != null)
			{
				Array.Clear(this.key, 0, this.key.Length);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001BAD9 File Offset: 0x00019CD9
		public HashAlgorithm Algo
		{
			get
			{
				return this.algo;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001BAE1 File Offset: 0x00019CE1
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0001BAE9 File Offset: 0x00019CE9
		public string HashName
		{
			get
			{
				return this.hashName;
			}
			set
			{
				this.CreateHash(value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0001BAF2 File Offset: 0x00019CF2
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0001BAFA File Offset: 0x00019CFA
		public byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				if (value != null && value.Length > 64)
				{
					this.key = this.algo.ComputeHash(value);
					return;
				}
				this.key = (byte[])value.Clone();
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001BB2C File Offset: 0x00019D2C
		public void Initialize()
		{
			this.hash = null;
			this.block.Initialize();
			byte[] array = this.KeySetup(this.key, 54);
			this.algo.Initialize();
			this.block.Core(array);
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001BB7C File Offset: 0x00019D7C
		private byte[] KeySetup(byte[] key, byte padding)
		{
			byte[] array = new byte[64];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = key[i] ^ padding;
			}
			for (int j = key.Length; j < 64; j++)
			{
				array[j] = padding;
			}
			return array;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001BBBC File Offset: 0x00019DBC
		public void Core(byte[] rgb, int ib, int cb)
		{
			this.block.Core(rgb, ib, cb);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001BBCC File Offset: 0x00019DCC
		public byte[] Final()
		{
			this.block.Final();
			byte[] array = this.algo.Hash;
			byte[] array2 = this.KeySetup(this.key, 92);
			this.algo.Initialize();
			this.algo.TransformBlock(array2, 0, array2.Length, array2, 0);
			this.algo.TransformFinalBlock(array, 0, array.Length);
			this.hash = this.algo.Hash;
			this.algo.Clear();
			Array.Clear(array2, 0, array2.Length);
			Array.Clear(array, 0, array.Length);
			return this.hash;
		}

		// Token: 0x040005AF RID: 1455
		private byte[] key;

		// Token: 0x040005B0 RID: 1456
		private byte[] hash;

		// Token: 0x040005B1 RID: 1457
		private HashAlgorithm algo;

		// Token: 0x040005B2 RID: 1458
		private string hashName;

		// Token: 0x040005B3 RID: 1459
		private BlockProcessor block;
	}
}
