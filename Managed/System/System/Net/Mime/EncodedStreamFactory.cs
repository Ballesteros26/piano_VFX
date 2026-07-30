using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200059B RID: 1435
	internal class EncodedStreamFactory
	{
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000B07A6 File Offset: 0x000AE9A6
		internal static int DefaultMaxLineLength
		{
			get
			{
				return 70;
			}
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000B07AA File Offset: 0x000AE9AA
		internal IEncodableStream GetEncoder(TransferEncoding encoding, Stream stream)
		{
			if (encoding == TransferEncoding.Base64)
			{
				return new Base64Stream(stream, new Base64WriteStateInfo());
			}
			if (encoding == TransferEncoding.QuotedPrintable)
			{
				return new QuotedPrintableStream(stream, true);
			}
			if (encoding == TransferEncoding.SevenBit || encoding == TransferEncoding.EightBit)
			{
				return new EightBitStream(stream);
			}
			throw new NotSupportedException("Encoding Stream");
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000B07E0 File Offset: 0x000AE9E0
		internal IEncodableStream GetEncoderForHeader(Encoding encoding, bool useBase64Encoding, int headerTextLength)
		{
			byte[] array = this.CreateHeader(encoding, useBase64Encoding);
			byte[] array2 = this.CreateFooter();
			if (useBase64Encoding)
			{
				return new Base64Stream((Base64WriteStateInfo)new Base64WriteStateInfo(1024, array, array2, EncodedStreamFactory.DefaultMaxLineLength, headerTextLength));
			}
			return new QEncodedStream(new WriteStateInfoBase(1024, array, array2, EncodedStreamFactory.DefaultMaxLineLength, headerTextLength));
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000B0834 File Offset: 0x000AEA34
		protected byte[] CreateHeader(Encoding encoding, bool useBase64Encoding)
		{
			string text = string.Format("=?{0}?{1}?", encoding.HeaderName, useBase64Encoding ? "B" : "Q");
			return Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000B086C File Offset: 0x000AEA6C
		protected byte[] CreateFooter()
		{
			return new byte[] { 63, 61 };
		}

		// Token: 0x04002504 RID: 9476
		private const int defaultMaxLineLength = 70;

		// Token: 0x04002505 RID: 9477
		private const int initialBufferSize = 1024;
	}
}
