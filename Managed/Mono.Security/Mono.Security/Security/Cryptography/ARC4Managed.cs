using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008C RID: 140
	public class ARC4Managed : RC4, ICryptoTransform, IDisposable
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00017489 File Offset: 0x00015689
		public ARC4Managed()
		{
			this.state = new byte[256];
			this.m_disposed = false;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000174A8 File Offset: 0x000156A8
		~ARC4Managed()
		{
			this.Dispose(true);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000174D8 File Offset: 0x000156D8
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.x = 0;
				this.y = 0;
				if (this.key != null)
				{
					Array.Clear(this.key, 0, this.key.Length);
					this.key = null;
				}
				Array.Clear(this.state, 0, this.state.Length);
				this.state = null;
				GC.SuppressFinalize(this);
				this.m_disposed = true;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00017546 File Offset: 0x00015746
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x00017568 File Offset: 0x00015768
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Key");
				}
				this.KeyValue = (this.key = (byte[])value.Clone());
				this.KeySetup(this.key);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x000175A9 File Offset: 0x000157A9
		public bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000175AC File Offset: 0x000157AC
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000175B6 File Offset: 0x000157B6
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this.CreateEncryptor();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000175C5 File Offset: 0x000157C5
		public override void GenerateIV()
		{
			this.IV = new byte[0];
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000175D3 File Offset: 0x000157D3
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000175E8 File Offset: 0x000157E8
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000175EB File Offset: 0x000157EB
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x000175EE File Offset: 0x000157EE
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000175F4 File Offset: 0x000157F4
		private void KeySetup(byte[] key)
		{
			byte b = 0;
			byte b2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.state[i] = (byte)i;
			}
			this.x = 0;
			this.y = 0;
			for (int j = 0; j < 256; j++)
			{
				b2 = key[(int)b] + this.state[j] + b2;
				byte b3 = this.state[j];
				this.state[j] = this.state[(int)b2];
				this.state[(int)b2] = b3;
				b = (byte)((int)(b + 1) % key.Length);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001767C File Offset: 0x0001587C
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
				throw new ArgumentException(Locale.GetText("Overflow"), "inputBuffer");
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000176DC File Offset: 0x000158DC
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			if (outputOffset > outputBuffer.Length - inputCount)
			{
				throw new ArgumentException(Locale.GetText("Overflow"), "outputBuffer");
			}
			return this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017744 File Offset: 0x00015944
		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = 0; i < inputCount; i++)
			{
				this.x += 1;
				this.y = this.state[(int)this.x] + this.y;
				byte b = this.state[(int)this.x];
				this.state[(int)this.x] = this.state[(int)this.y];
				this.state[(int)this.y] = b;
				byte b2 = this.state[(int)this.x] + this.state[(int)this.y];
				outputBuffer[outputOffset + i] = inputBuffer[inputOffset + i] ^ this.state[(int)b2];
			}
			return inputCount;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000177F8 File Offset: 0x000159F8
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			byte[] array = new byte[inputCount];
			this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x04000395 RID: 917
		private byte[] key;

		// Token: 0x04000396 RID: 918
		private byte[] state;

		// Token: 0x04000397 RID: 919
		private byte x;

		// Token: 0x04000398 RID: 920
		private byte y;

		// Token: 0x04000399 RID: 921
		private bool m_disposed;
	}
}
