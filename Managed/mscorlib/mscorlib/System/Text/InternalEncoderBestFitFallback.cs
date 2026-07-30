using System;

namespace System.Text
{
	// Token: 0x02000272 RID: 626
	[Serializable]
	internal class InternalEncoderBestFitFallback : EncoderFallback
	{
		// Token: 0x06001CA5 RID: 7333 RVA: 0x0006BE43 File Offset: 0x0006A043
		internal InternalEncoderBestFitFallback(Encoding encoding)
		{
			this.encoding = encoding;
			this.bIsMicrosoftBestFitFallback = true;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0006BE59 File Offset: 0x0006A059
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalEncoderBestFitFallbackBuffer(this);
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00003B29 File Offset: 0x00001D29
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0006BE64 File Offset: 0x0006A064
		public override bool Equals(object value)
		{
			InternalEncoderBestFitFallback internalEncoderBestFitFallback = value as InternalEncoderBestFitFallback;
			return internalEncoderBestFitFallback != null && this.encoding.CodePage == internalEncoderBestFitFallback.encoding.CodePage;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006BE95 File Offset: 0x0006A095
		public override int GetHashCode()
		{
			return this.encoding.CodePage;
		}

		// Token: 0x04000FD9 RID: 4057
		internal Encoding encoding;

		// Token: 0x04000FDA RID: 4058
		internal char[] arrayBestFit;
	}
}
