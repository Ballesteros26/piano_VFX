using System;

namespace System.Text
{
	// Token: 0x02000267 RID: 615
	[Serializable]
	internal sealed class InternalDecoderBestFitFallback : DecoderFallback
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x0006ABFB File Offset: 0x00068DFB
		internal InternalDecoderBestFitFallback(Encoding encoding)
		{
			this.encoding = encoding;
			this.bIsMicrosoftBestFitFallback = true;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0006AC19 File Offset: 0x00068E19
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalDecoderBestFitFallbackBuffer(this);
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00003B29 File Offset: 0x00001D29
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0006AC24 File Offset: 0x00068E24
		public override bool Equals(object value)
		{
			InternalDecoderBestFitFallback internalDecoderBestFitFallback = value as InternalDecoderBestFitFallback;
			return internalDecoderBestFitFallback != null && this.encoding.CodePage == internalDecoderBestFitFallback.encoding.CodePage;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0006AC55 File Offset: 0x00068E55
		public override int GetHashCode()
		{
			return this.encoding.CodePage;
		}

		// Token: 0x04000FBF RID: 4031
		internal Encoding encoding;

		// Token: 0x04000FC0 RID: 4032
		internal char[] arrayBestFit;

		// Token: 0x04000FC1 RID: 4033
		internal char cReplacement = '?';
	}
}
