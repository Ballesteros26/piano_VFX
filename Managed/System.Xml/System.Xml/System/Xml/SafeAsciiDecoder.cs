using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000291 RID: 657
	internal class SafeAsciiDecoder : Decoder
	{
		// Token: 0x06001884 RID: 6276 RVA: 0x000182C5 File Offset: 0x000164C5
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0008E988 File Offset: 0x0008CB88
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			int i = byteIndex;
			int num = charIndex;
			while (i < byteIndex + byteCount)
			{
				chars[num++] = (char)bytes[i++];
			}
			return byteCount;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0008E9B4 File Offset: 0x0008CBB4
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (charCount < byteCount)
			{
				byteCount = charCount;
				completed = false;
			}
			else
			{
				completed = true;
			}
			int i = byteIndex;
			int num = charIndex;
			int num2 = byteIndex + byteCount;
			while (i < num2)
			{
				chars[num++] = (char)bytes[i++];
			}
			charsUsed = byteCount;
			bytesUsed = byteCount;
		}
	}
}
