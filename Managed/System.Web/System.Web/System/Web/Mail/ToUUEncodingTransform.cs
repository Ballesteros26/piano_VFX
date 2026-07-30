using System;
using System.Security.Cryptography;

namespace System.Web.Mail
{
	// Token: 0x02000101 RID: 257
	internal class ToUUEncodingTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00024D60 File Offset: 0x00022F60
		public int InputBlockSize
		{
			get
			{
				return 45;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00024D64 File Offset: 0x00022F64
		public int OutputBlockSize
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00008B66 File Offset: 0x00006D66
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00024D68 File Offset: 0x00022F68
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			outputBuffer[0] = 77;
			for (int i = 0; i < 15; i++)
			{
				this.TransformTriplet(inputBuffer, inputOffset + i * 3, 3, outputBuffer, outputOffset + i * 4 + 1);
			}
			return this.OutputBlockSize;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00024DA8 File Offset: 0x00022FA8
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = inputCount / 3 + 1;
			byte[] array = new byte[num * 3];
			Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			byte[] array2 = new byte[num * 4 + 1];
			array2[0] = (byte)(inputCount + 32);
			for (int i = 0; i < num; i++)
			{
				this.TransformTriplet(inputBuffer, inputOffset + i * 3, 3, array2, i * 4 + 1);
			}
			return array2;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00024E04 File Offset: 0x00023004
		protected int TransformTriplet(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			byte b = inputBuffer[inputOffset];
			byte b2 = inputBuffer[inputOffset + 1];
			byte b3 = inputBuffer[inputOffset + 2];
			outputBuffer[outputOffset] = (byte)(32 + ((b >> 2) & 63));
			outputBuffer[outputOffset + 1] = (byte)(32 + ((((int)b << 4) | ((b2 >> 4) & 15)) & 63));
			outputBuffer[outputOffset + 2] = (byte)(32 + ((((int)b2 << 2) | ((b3 >> 6) & 3)) & 63));
			outputBuffer[outputOffset + 3] = 32 + (b3 & 63);
			for (int i = 0; i < 4; i++)
			{
				if (outputBuffer[outputOffset + i] == 32)
				{
					outputBuffer[outputOffset + i] = 96;
				}
			}
			return 4;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0000393A File Offset: 0x00001B3A
		public void Dispose()
		{
		}
	}
}
