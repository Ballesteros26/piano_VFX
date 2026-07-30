using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200007E RID: 126
	internal class ARC4Managed : RC4, ICryptoTransform, IDisposable
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x00015DB2 File Offset: 0x00013FB2
		public ARC4Managed()
		{
			this.state = new byte[256];
			this.m_disposed = false;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00015DD4 File Offset: 0x00013FD4
		~ARC4Managed()
		{
			this.Dispose(true);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00015E04 File Offset: 0x00014004
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00015E72 File Offset: 0x00014072
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00015E94 File Offset: 0x00014094
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00015ED5 File Offset: 0x000140D5
		public bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00015ED8 File Offset: 0x000140D8
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00015EE2 File Offset: 0x000140E2
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this.CreateEncryptor();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00015EF1 File Offset: 0x000140F1
		public override void GenerateIV()
		{
			this.IV = new byte[0];
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00015EFF File Offset: 0x000140FF
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00003B29 File Offset: 0x00001D29
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00003B29 File Offset: 0x00001D29
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00003B29 File Offset: 0x00001D29
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00015F14 File Offset: 0x00014114
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

		// Token: 0x060003D0 RID: 976 RVA: 0x00015F9C File Offset: 0x0001419C
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

		// Token: 0x060003D1 RID: 977 RVA: 0x00015FFC File Offset: 0x000141FC
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

		// Token: 0x060003D2 RID: 978 RVA: 0x00016064 File Offset: 0x00014264
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

		// Token: 0x060003D3 RID: 979 RVA: 0x00016118 File Offset: 0x00014318
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			byte[] array = new byte[inputCount];
			this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x0400054B RID: 1355
		private byte[] key;

		// Token: 0x0400054C RID: 1356
		private byte[] state;

		// Token: 0x0400054D RID: 1357
		private byte x;

		// Token: 0x0400054E RID: 1358
		private byte y;

		// Token: 0x0400054F RID: 1359
		private bool m_disposed;
	}
}
