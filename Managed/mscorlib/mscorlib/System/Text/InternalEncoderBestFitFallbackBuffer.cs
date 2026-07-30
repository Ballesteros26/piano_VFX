using System;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000273 RID: 627
	internal sealed class InternalEncoderBestFitFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0006BEA4 File Offset: 0x0006A0A4
		private static object InternalSyncObject
		{
			get
			{
				if (InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0006BED0 File Offset: 0x0006A0D0
		public InternalEncoderBestFitFallbackBuffer(InternalEncoderBestFitFallback fallback)
		{
			this.oFallback = fallback;
			if (this.oFallback.arrayBestFit == null)
			{
				object internalSyncObject = InternalEncoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this.oFallback.arrayBestFit == null)
					{
						this.oFallback.arrayBestFit = fallback.encoding.GetBestFitUnicodeToBytesData();
					}
				}
			}
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0006BF50 File Offset: 0x0006A150
		public override bool Fallback(char charUnknown, int index)
		{
			this.iCount = (this.iSize = 1);
			this.cBestFit = this.TryBestFit(charUnknown);
			if (this.cBestFit == '\0')
			{
				this.cBestFit = '?';
			}
			return true;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0006BF8C File Offset: 0x0006A18C
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 55296, 56319 }));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 56320, 57343 }));
			}
			this.cBestFit = '?';
			this.iCount = (this.iSize = 2);
			return true;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0006C02C File Offset: 0x0006A22C
		public override char GetNextChar()
		{
			this.iCount--;
			if (this.iCount < 0)
			{
				return '\0';
			}
			if (this.iCount == 2147483647)
			{
				this.iCount = -1;
				return '\0';
			}
			return this.cBestFit;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0006C063 File Offset: 0x0006A263
		public override bool MovePrevious()
		{
			if (this.iCount >= 0)
			{
				this.iCount++;
			}
			return this.iCount >= 0 && this.iCount <= this.iSize;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0006C098 File Offset: 0x0006A298
		public override int Remaining
		{
			get
			{
				if (this.iCount <= 0)
				{
					return 0;
				}
				return this.iCount;
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0006C0AB File Offset: 0x0006A2AB
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.iCount = -1;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0006C0C4 File Offset: 0x0006A2C4
		private char TryBestFit(char cUnknown)
		{
			int num = 0;
			int num2 = this.oFallback.arrayBestFit.Length;
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c = this.oFallback.arrayBestFit[i];
				if (c == cUnknown)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
				if (c < cUnknown)
				{
					num = i;
				}
				else
				{
					num2 = i;
				}
			}
			for (int i = num; i < num2; i += 2)
			{
				if (this.oFallback.arrayBestFit[i] == cUnknown)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04000FDB RID: 4059
		private char cBestFit;

		// Token: 0x04000FDC RID: 4060
		private InternalEncoderBestFitFallback oFallback;

		// Token: 0x04000FDD RID: 4061
		private int iCount = -1;

		// Token: 0x04000FDE RID: 4062
		private int iSize;

		// Token: 0x04000FDF RID: 4063
		private static object s_InternalSyncObject;
	}
}
