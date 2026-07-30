using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008F RID: 143
	internal abstract class SymmetricTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x0001A400 File Offset: 0x00018600
		public SymmetricTransform(SymmetricAlgorithm symmAlgo, bool encryption, byte[] rgbIV)
		{
			this.algo = symmAlgo;
			this.encrypt = encryption;
			this.BlockSizeByte = this.algo.BlockSize >> 3;
			if (rgbIV == null)
			{
				rgbIV = KeyBuilder.IV(this.BlockSizeByte);
			}
			else
			{
				rgbIV = (byte[])rgbIV.Clone();
			}
			if (rgbIV.Length < this.BlockSizeByte)
			{
				throw new CryptographicException(Locale.GetText("IV is too small ({0} bytes), it should be {1} bytes long.", new object[] { rgbIV.Length, this.BlockSizeByte }));
			}
			this.padmode = this.algo.Padding;
			this.temp = new byte[this.BlockSizeByte];
			Buffer.BlockCopy(rgbIV, 0, this.temp, 0, Math.Min(this.BlockSizeByte, rgbIV.Length));
			this.temp2 = new byte[this.BlockSizeByte];
			this.FeedBackByte = this.algo.FeedbackSize >> 3;
			this.workBuff = new byte[this.BlockSizeByte];
			this.workout = new byte[this.BlockSizeByte];
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001A514 File Offset: 0x00018714
		~SymmetricTransform()
		{
			this.Dispose(false);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001A544 File Offset: 0x00018744
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001A554 File Offset: 0x00018754
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					Array.Clear(this.temp, 0, this.BlockSizeByte);
					this.temp = null;
					Array.Clear(this.temp2, 0, this.BlockSizeByte);
					this.temp2 = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001A5A5 File Offset: 0x000187A5
		public virtual int InputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001A5A5 File Offset: 0x000187A5
		public virtual int OutputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001A5B0 File Offset: 0x000187B0
		protected virtual void Transform(byte[] input, byte[] output)
		{
			switch (this.algo.Mode)
			{
			case CipherMode.CBC:
				this.CBC(input, output);
				return;
			case CipherMode.ECB:
				this.ECB(input, output);
				return;
			case CipherMode.OFB:
				this.OFB(input, output);
				return;
			case CipherMode.CFB:
				this.CFB(input, output);
				return;
			case CipherMode.CTS:
				this.CTS(input, output);
				return;
			default:
				throw new NotImplementedException("Unkown CipherMode" + this.algo.Mode.ToString());
			}
		}

		// Token: 0x06000494 RID: 1172
		protected abstract void ECB(byte[] input, byte[] output);

		// Token: 0x06000495 RID: 1173 RVA: 0x0001A63C File Offset: 0x0001883C
		protected virtual void CBC(byte[] input, byte[] output)
		{
			if (this.encrypt)
			{
				for (int i = 0; i < this.BlockSizeByte; i++)
				{
					byte[] array = this.temp;
					int num = i;
					array[num] ^= input[i];
				}
				this.ECB(this.temp, output);
				Buffer.BlockCopy(output, 0, this.temp, 0, this.BlockSizeByte);
				return;
			}
			Buffer.BlockCopy(input, 0, this.temp2, 0, this.BlockSizeByte);
			this.ECB(input, output);
			for (int j = 0; j < this.BlockSizeByte; j++)
			{
				int num2 = j;
				output[num2] ^= this.temp[j];
			}
			Buffer.BlockCopy(this.temp2, 0, this.temp, 0, this.BlockSizeByte);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001A6F4 File Offset: 0x000188F4
		protected virtual void CFB(byte[] input, byte[] output)
		{
			if (this.encrypt)
			{
				for (int i = 0; i < this.BlockSizeByte; i++)
				{
					this.ECB(this.temp, this.temp2);
					output[i] = this.temp2[0] ^ input[i];
					Buffer.BlockCopy(this.temp, 1, this.temp, 0, this.BlockSizeByte - 1);
					Buffer.BlockCopy(output, i, this.temp, this.BlockSizeByte - 1, 1);
				}
				return;
			}
			for (int j = 0; j < this.BlockSizeByte; j++)
			{
				this.encrypt = true;
				this.ECB(this.temp, this.temp2);
				this.encrypt = false;
				Buffer.BlockCopy(this.temp, 1, this.temp, 0, this.BlockSizeByte - 1);
				Buffer.BlockCopy(input, j, this.temp, this.BlockSizeByte - 1, 1);
				output[j] = this.temp2[0] ^ input[j];
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001A7E0 File Offset: 0x000189E0
		protected virtual void OFB(byte[] input, byte[] output)
		{
			throw new CryptographicException("OFB isn't supported by the framework");
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001A7EC File Offset: 0x000189EC
		protected virtual void CTS(byte[] input, byte[] output)
		{
			throw new CryptographicException("CTS isn't supported by the framework");
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001A7F8 File Offset: 0x000189F8
		private void CheckInput(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputCount < 0)
			{
				throw new ArgumentOutOfRangeException("inputCount", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException("inputBuffer", Locale.GetText("Overflow"));
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001A858 File Offset: 0x00018A58
		public virtual int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Object is disposed");
			}
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			int num = outputBuffer.Length - inputCount - outputOffset;
			if (!this.encrypt && 0 > num && (this.padmode == PaddingMode.None || this.padmode == PaddingMode.Zeros))
			{
				throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
			}
			if (this.KeepLastBlock)
			{
				if (0 > num + this.BlockSizeByte)
				{
					throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
				}
			}
			else if (0 > num)
			{
				if (inputBuffer.Length - inputOffset - outputBuffer.Length != this.BlockSizeByte)
				{
					throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
				}
				inputCount = outputBuffer.Length - outputOffset;
			}
			return this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0001A94D File Offset: 0x00018B4D
		private bool KeepLastBlock
		{
			get
			{
				return !this.encrypt && this.padmode != PaddingMode.None && this.padmode != PaddingMode.Zeros;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001A970 File Offset: 0x00018B70
		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			int num = inputOffset;
			int num2;
			if (inputCount != this.BlockSizeByte)
			{
				if (inputCount % this.BlockSizeByte != 0)
				{
					throw new CryptographicException("Invalid input block size.");
				}
				num2 = inputCount / this.BlockSizeByte;
			}
			else
			{
				num2 = 1;
			}
			if (this.KeepLastBlock)
			{
				num2--;
			}
			int num3 = 0;
			if (this.lastBlock)
			{
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, outputBuffer, outputOffset, this.BlockSizeByte);
				outputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
				this.lastBlock = false;
			}
			for (int i = 0; i < num2; i++)
			{
				Buffer.BlockCopy(inputBuffer, num, this.workBuff, 0, this.BlockSizeByte);
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, outputBuffer, outputOffset, this.BlockSizeByte);
				num += this.BlockSizeByte;
				outputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
			}
			if (this.KeepLastBlock)
			{
				Buffer.BlockCopy(inputBuffer, num, this.workBuff, 0, this.BlockSizeByte);
				this.lastBlock = true;
			}
			return num3;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001AA8C File Offset: 0x00018C8C
		private void Random(byte[] buffer, int start, int length)
		{
			if (this._rng == null)
			{
				this._rng = RandomNumberGenerator.Create();
			}
			byte[] array = new byte[length];
			this._rng.GetBytes(array);
			Buffer.BlockCopy(array, 0, buffer, start, length);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001AACC File Offset: 0x00018CCC
		private void ThrowBadPaddingException(PaddingMode padding, int length, int position)
		{
			string text = string.Format(Locale.GetText("Bad {0} padding."), padding);
			if (length >= 0)
			{
				text += string.Format(Locale.GetText(" Invalid length {0}."), length);
			}
			if (position >= 0)
			{
				text += string.Format(Locale.GetText(" Error found at position {0}."), position);
			}
			throw new CryptographicException(text);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001AB38 File Offset: 0x00018D38
		protected virtual byte[] FinalEncrypt(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = inputCount / this.BlockSizeByte * this.BlockSizeByte;
			int num2 = inputCount - num;
			int i = num;
			PaddingMode paddingMode = this.padmode;
			if (paddingMode == PaddingMode.PKCS7 || paddingMode - PaddingMode.ANSIX923 <= 1)
			{
				i += this.BlockSizeByte;
			}
			else
			{
				if (inputCount == 0)
				{
					return new byte[0];
				}
				if (num2 != 0)
				{
					if (this.padmode == PaddingMode.None)
					{
						throw new CryptographicException("invalid block length");
					}
					byte[] array = new byte[num + this.BlockSizeByte];
					Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
					inputBuffer = array;
					inputOffset = 0;
					inputCount = array.Length;
					i = inputCount;
				}
			}
			byte[] array2 = new byte[i];
			int num3 = 0;
			while (i > this.BlockSizeByte)
			{
				this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array2, num3);
				inputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
				i -= this.BlockSizeByte;
			}
			byte b = (byte)(this.BlockSizeByte - num2);
			switch (this.padmode)
			{
			case PaddingMode.PKCS7:
			{
				int num4 = array2.Length;
				while (--num4 >= array2.Length - (int)b)
				{
					array2[num4] = b;
				}
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			}
			case PaddingMode.ANSIX923:
				array2[array2.Length - 1] = b;
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			case PaddingMode.ISO10126:
				this.Random(array2, array2.Length - (int)b, (int)(b - 1));
				array2[array2.Length - 1] = b;
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			}
			this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array2, num3);
			return array2;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001ACE8 File Offset: 0x00018EE8
		protected virtual byte[] FinalDecrypt(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int i = inputCount;
			int num = inputCount;
			if (this.lastBlock)
			{
				num += this.BlockSizeByte;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			while (i > 0)
			{
				int num3 = this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array, num2);
				inputOffset += this.BlockSizeByte;
				num2 += num3;
				i -= this.BlockSizeByte;
			}
			if (this.lastBlock)
			{
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, array, num2, this.BlockSizeByte);
				num2 += this.BlockSizeByte;
				this.lastBlock = false;
			}
			byte b = ((num > 0) ? array[num - 1] : 0);
			switch (this.padmode)
			{
			case PaddingMode.PKCS7:
			{
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.padmode, (int)b, -1);
				}
				for (int j = (int)(b - 1); j > 0; j--)
				{
					if (array[num - 1 - j] != b)
					{
						this.ThrowBadPaddingException(this.padmode, -1, j);
					}
				}
				num -= (int)b;
				break;
			}
			case PaddingMode.ANSIX923:
			{
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.padmode, (int)b, -1);
				}
				for (int k = (int)(b - 1); k > 0; k--)
				{
					if (array[num - 1 - k] != 0)
					{
						this.ThrowBadPaddingException(this.padmode, -1, k);
					}
				}
				num -= (int)b;
				break;
			}
			case PaddingMode.ISO10126:
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.padmode, (int)b, -1);
				}
				num -= (int)b;
				break;
			}
			if (num > 0)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				Array.Clear(array, 0, array.Length);
				return array2;
			}
			return new byte[0];
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001AEA7 File Offset: 0x000190A7
		public virtual byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Object is disposed");
			}
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (this.encrypt)
			{
				return this.FinalEncrypt(inputBuffer, inputOffset, inputCount);
			}
			return this.FinalDecrypt(inputBuffer, inputOffset, inputCount);
		}

		// Token: 0x04000595 RID: 1429
		protected SymmetricAlgorithm algo;

		// Token: 0x04000596 RID: 1430
		protected bool encrypt;

		// Token: 0x04000597 RID: 1431
		protected int BlockSizeByte;

		// Token: 0x04000598 RID: 1432
		protected byte[] temp;

		// Token: 0x04000599 RID: 1433
		protected byte[] temp2;

		// Token: 0x0400059A RID: 1434
		private byte[] workBuff;

		// Token: 0x0400059B RID: 1435
		private byte[] workout;

		// Token: 0x0400059C RID: 1436
		protected PaddingMode padmode;

		// Token: 0x0400059D RID: 1437
		protected int FeedBackByte;

		// Token: 0x0400059E RID: 1438
		private bool m_disposed;

		// Token: 0x0400059F RID: 1439
		protected bool lastBlock;

		// Token: 0x040005A0 RID: 1440
		private RandomNumberGenerator _rng;
	}
}
