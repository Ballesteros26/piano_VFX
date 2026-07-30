using System;
using System.Security;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000268 RID: 616
	internal sealed class InternalDecoderBestFitFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0006AC64 File Offset: 0x00068E64
		private static object InternalSyncObject
		{
			get
			{
				if (InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0006AC90 File Offset: 0x00068E90
		public InternalDecoderBestFitFallbackBuffer(InternalDecoderBestFitFallback fallback)
		{
			this.oFallback = fallback;
			if (this.oFallback.arrayBestFit == null)
			{
				object internalSyncObject = InternalDecoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this.oFallback.arrayBestFit == null)
					{
						this.oFallback.arrayBestFit = fallback.encoding.GetBestFitBytesToUnicodeData();
					}
				}
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0006AD10 File Offset: 0x00068F10
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.cBestFit = this.TryBestFit(bytesUnknown);
			if (this.cBestFit == '\0')
			{
				this.cBestFit = this.oFallback.cReplacement;
			}
			this.iCount = (this.iSize = 1);
			return true;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0006AD54 File Offset: 0x00068F54
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

		// Token: 0x06001C51 RID: 7249 RVA: 0x0006AD8B File Offset: 0x00068F8B
		public override bool MovePrevious()
		{
			if (this.iCount >= 0)
			{
				this.iCount++;
			}
			return this.iCount >= 0 && this.iCount <= this.iSize;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x0006ADC0 File Offset: 0x00068FC0
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

		// Token: 0x06001C53 RID: 7251 RVA: 0x0006ADD3 File Offset: 0x00068FD3
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.iCount = -1;
			this.byteStart = null;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00003B29 File Offset: 0x00001D29
		[SecurityCritical]
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return 1;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0006ADE4 File Offset: 0x00068FE4
		private char TryBestFit(byte[] bytesCheck)
		{
			int num = 0;
			int num2 = this.oFallback.arrayBestFit.Length;
			if (num2 == 0)
			{
				return '\0';
			}
			if (bytesCheck.Length == 0 || bytesCheck.Length > 2)
			{
				return '\0';
			}
			char c;
			if (bytesCheck.Length == 1)
			{
				c = (char)bytesCheck[0];
			}
			else
			{
				c = (char)(((int)bytesCheck[0] << 8) + (int)bytesCheck[1]);
			}
			if (c < this.oFallback.arrayBestFit[0] || c > this.oFallback.arrayBestFit[num2 - 2])
			{
				return '\0';
			}
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c2 = this.oFallback.arrayBestFit[i];
				if (c2 == c)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
				if (c2 < c)
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
				if (this.oFallback.arrayBestFit[i] == c)
				{
					return this.oFallback.arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04000FC2 RID: 4034
		internal char cBestFit;

		// Token: 0x04000FC3 RID: 4035
		internal int iCount = -1;

		// Token: 0x04000FC4 RID: 4036
		internal int iSize;

		// Token: 0x04000FC5 RID: 4037
		private InternalDecoderBestFitFallback oFallback;

		// Token: 0x04000FC6 RID: 4038
		private static object s_InternalSyncObject;
	}
}
