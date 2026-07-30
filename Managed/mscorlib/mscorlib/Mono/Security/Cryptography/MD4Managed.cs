using System;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000086 RID: 134
	internal class MD4Managed : MD4
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00017C68 File Offset: 0x00015E68
		public MD4Managed()
		{
			this.state = new uint[4];
			this.count = new uint[2];
			this.buffer = new byte[64];
			this.digest = new byte[16];
			this.x = new uint[16];
			this.Initialize();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00017CC0 File Offset: 0x00015EC0
		public override void Initialize()
		{
			this.count[0] = 0U;
			this.count[1] = 0U;
			this.state[0] = 1732584193U;
			this.state[1] = 4023233417U;
			this.state[2] = 2562383102U;
			this.state[3] = 271733878U;
			Array.Clear(this.buffer, 0, 64);
			Array.Clear(this.x, 0, 16);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00017D30 File Offset: 0x00015F30
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			int num = (int)((this.count[0] >> 3) & 63U);
			this.count[0] += (uint)((uint)cbSize << 3);
			if ((ulong)this.count[0] < (ulong)((long)((long)cbSize << 3)))
			{
				this.count[1] += 1U;
			}
			this.count[1] += (uint)(cbSize >> 29);
			int num2 = 64 - num;
			int num3 = 0;
			if (cbSize >= num2)
			{
				Buffer.BlockCopy(array, ibStart, this.buffer, num, num2);
				this.MD4Transform(this.state, this.buffer, 0);
				num3 = num2;
				while (num3 + 63 < cbSize)
				{
					this.MD4Transform(this.state, array, ibStart + num3);
					num3 += 64;
				}
				num = 0;
			}
			Buffer.BlockCopy(array, ibStart + num3, this.buffer, num, cbSize - num3);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00017DF4 File Offset: 0x00015FF4
		protected override byte[] HashFinal()
		{
			byte[] array = new byte[8];
			this.Encode(array, this.count);
			uint num = (this.count[0] >> 3) & 63U;
			int num2 = (int)((num < 56U) ? (56U - num) : (120U - num));
			this.HashCore(this.Padding(num2), 0, num2);
			this.HashCore(array, 0, 8);
			this.Encode(this.digest, this.state);
			this.Initialize();
			return this.digest;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00017E69 File Offset: 0x00016069
		private byte[] Padding(int nLength)
		{
			if (nLength > 0)
			{
				byte[] array = new byte[nLength];
				array[0] = 128;
				return array;
			}
			return null;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00017E7F File Offset: 0x0001607F
		private uint F(uint x, uint y, uint z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00017E89 File Offset: 0x00016089
		private uint G(uint x, uint y, uint z)
		{
			return (x & y) | (x & z) | (y & z);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00017E96 File Offset: 0x00016096
		private uint H(uint x, uint y, uint z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00017E9D File Offset: 0x0001609D
		private uint ROL(uint x, byte n)
		{
			return (x << (int)n) | (x >> (int)(32 - n));
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00017EAF File Offset: 0x000160AF
		private void FF(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += this.F(b, c, d) + x;
			a = this.ROL(a, s);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00017ECF File Offset: 0x000160CF
		private void GG(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += this.G(b, c, d) + x + 1518500249U;
			a = this.ROL(a, s);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00017EF5 File Offset: 0x000160F5
		private void HH(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += this.H(b, c, d) + x + 1859775393U;
			a = this.ROL(a, s);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00017F1C File Offset: 0x0001611C
		private void Encode(byte[] output, uint[] input)
		{
			int num = 0;
			for (int i = 0; i < output.Length; i += 4)
			{
				output[i] = (byte)input[num];
				output[i + 1] = (byte)(input[num] >> 8);
				output[i + 2] = (byte)(input[num] >> 16);
				output[i + 3] = (byte)(input[num] >> 24);
				num++;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00017F68 File Offset: 0x00016168
		private void Decode(uint[] output, byte[] input, int index)
		{
			int i = 0;
			int num = index;
			while (i < output.Length)
			{
				output[i] = (uint)((int)input[num] | ((int)input[num + 1] << 8) | ((int)input[num + 2] << 16) | ((int)input[num + 3] << 24));
				i++;
				num += 4;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00017FAC File Offset: 0x000161AC
		private void MD4Transform(uint[] state, byte[] block, int index)
		{
			uint num = state[0];
			uint num2 = state[1];
			uint num3 = state[2];
			uint num4 = state[3];
			this.Decode(this.x, block, index);
			this.FF(ref num, num2, num3, num4, this.x[0], 3);
			this.FF(ref num4, num, num2, num3, this.x[1], 7);
			this.FF(ref num3, num4, num, num2, this.x[2], 11);
			this.FF(ref num2, num3, num4, num, this.x[3], 19);
			this.FF(ref num, num2, num3, num4, this.x[4], 3);
			this.FF(ref num4, num, num2, num3, this.x[5], 7);
			this.FF(ref num3, num4, num, num2, this.x[6], 11);
			this.FF(ref num2, num3, num4, num, this.x[7], 19);
			this.FF(ref num, num2, num3, num4, this.x[8], 3);
			this.FF(ref num4, num, num2, num3, this.x[9], 7);
			this.FF(ref num3, num4, num, num2, this.x[10], 11);
			this.FF(ref num2, num3, num4, num, this.x[11], 19);
			this.FF(ref num, num2, num3, num4, this.x[12], 3);
			this.FF(ref num4, num, num2, num3, this.x[13], 7);
			this.FF(ref num3, num4, num, num2, this.x[14], 11);
			this.FF(ref num2, num3, num4, num, this.x[15], 19);
			this.GG(ref num, num2, num3, num4, this.x[0], 3);
			this.GG(ref num4, num, num2, num3, this.x[4], 5);
			this.GG(ref num3, num4, num, num2, this.x[8], 9);
			this.GG(ref num2, num3, num4, num, this.x[12], 13);
			this.GG(ref num, num2, num3, num4, this.x[1], 3);
			this.GG(ref num4, num, num2, num3, this.x[5], 5);
			this.GG(ref num3, num4, num, num2, this.x[9], 9);
			this.GG(ref num2, num3, num4, num, this.x[13], 13);
			this.GG(ref num, num2, num3, num4, this.x[2], 3);
			this.GG(ref num4, num, num2, num3, this.x[6], 5);
			this.GG(ref num3, num4, num, num2, this.x[10], 9);
			this.GG(ref num2, num3, num4, num, this.x[14], 13);
			this.GG(ref num, num2, num3, num4, this.x[3], 3);
			this.GG(ref num4, num, num2, num3, this.x[7], 5);
			this.GG(ref num3, num4, num, num2, this.x[11], 9);
			this.GG(ref num2, num3, num4, num, this.x[15], 13);
			this.HH(ref num, num2, num3, num4, this.x[0], 3);
			this.HH(ref num4, num, num2, num3, this.x[8], 9);
			this.HH(ref num3, num4, num, num2, this.x[4], 11);
			this.HH(ref num2, num3, num4, num, this.x[12], 15);
			this.HH(ref num, num2, num3, num4, this.x[2], 3);
			this.HH(ref num4, num, num2, num3, this.x[10], 9);
			this.HH(ref num3, num4, num, num2, this.x[6], 11);
			this.HH(ref num2, num3, num4, num, this.x[14], 15);
			this.HH(ref num, num2, num3, num4, this.x[1], 3);
			this.HH(ref num4, num, num2, num3, this.x[9], 9);
			this.HH(ref num3, num4, num, num2, this.x[5], 11);
			this.HH(ref num2, num3, num4, num, this.x[13], 15);
			this.HH(ref num, num2, num3, num4, this.x[3], 3);
			this.HH(ref num4, num, num2, num3, this.x[11], 9);
			this.HH(ref num3, num4, num, num2, this.x[7], 11);
			this.HH(ref num2, num3, num4, num, this.x[15], 15);
			state[0] += num;
			state[1] += num2;
			state[2] += num3;
			state[3] += num4;
		}

		// Token: 0x04000564 RID: 1380
		private uint[] state;

		// Token: 0x04000565 RID: 1381
		private byte[] buffer;

		// Token: 0x04000566 RID: 1382
		private uint[] count;

		// Token: 0x04000567 RID: 1383
		private uint[] x;

		// Token: 0x04000568 RID: 1384
		private const int S11 = 3;

		// Token: 0x04000569 RID: 1385
		private const int S12 = 7;

		// Token: 0x0400056A RID: 1386
		private const int S13 = 11;

		// Token: 0x0400056B RID: 1387
		private const int S14 = 19;

		// Token: 0x0400056C RID: 1388
		private const int S21 = 3;

		// Token: 0x0400056D RID: 1389
		private const int S22 = 5;

		// Token: 0x0400056E RID: 1390
		private const int S23 = 9;

		// Token: 0x0400056F RID: 1391
		private const int S24 = 13;

		// Token: 0x04000570 RID: 1392
		private const int S31 = 3;

		// Token: 0x04000571 RID: 1393
		private const int S32 = 9;

		// Token: 0x04000572 RID: 1394
		private const int S33 = 11;

		// Token: 0x04000573 RID: 1395
		private const int S34 = 15;

		// Token: 0x04000574 RID: 1396
		private byte[] digest;
	}
}
