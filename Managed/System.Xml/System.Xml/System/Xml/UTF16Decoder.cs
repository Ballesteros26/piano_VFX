using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000290 RID: 656
	internal class UTF16Decoder : Decoder
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x0008E6D8 File Offset: 0x0008C8D8
		public UTF16Decoder(bool bigEndian)
		{
			this.lastByte = -1;
			this.bigEndian = bigEndian;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0008E6EE File Offset: 0x0008C8EE
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0008E6FC File Offset: 0x0008C8FC
		public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			int num = count + ((this.lastByte >= 0) ? 1 : 0);
			if (flush && num % 2 != 0)
			{
				throw new ArgumentException(Res.GetString("Invalid byte was found at index {0}.", new object[] { -1 }), null);
			}
			return num / 2;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0008E748 File Offset: 0x0008C948
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			int charCount = this.GetCharCount(bytes, byteIndex, byteCount);
			if (this.lastByte >= 0)
			{
				if (byteCount == 0)
				{
					return charCount;
				}
				int num = (int)bytes[byteIndex++];
				byteCount--;
				chars[charIndex++] = (this.bigEndian ? ((char)((this.lastByte << 8) | num)) : ((char)((num << 8) | this.lastByte)));
				this.lastByte = -1;
			}
			if ((byteCount & 1) != 0)
			{
				this.lastByte = (int)bytes[byteIndex + --byteCount];
			}
			if (this.bigEndian == BitConverter.IsLittleEndian)
			{
				int num2 = byteIndex + byteCount;
				if (this.bigEndian)
				{
					while (byteIndex < num2)
					{
						int num3 = (int)bytes[byteIndex++];
						int num4 = (int)bytes[byteIndex++];
						chars[charIndex++] = (char)((num3 << 8) | num4);
					}
				}
				else
				{
					while (byteIndex < num2)
					{
						int num5 = (int)bytes[byteIndex++];
						int num6 = (int)bytes[byteIndex++];
						chars[charIndex++] = (char)((num6 << 8) | num5);
					}
				}
			}
			else
			{
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, byteCount);
			}
			return charCount;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0008E844 File Offset: 0x0008CA44
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			charsUsed = 0;
			bytesUsed = 0;
			if (this.lastByte >= 0)
			{
				if (byteCount == 0)
				{
					completed = true;
					return;
				}
				int num = (int)bytes[byteIndex++];
				byteCount--;
				bytesUsed++;
				chars[charIndex++] = (this.bigEndian ? ((char)((this.lastByte << 8) | num)) : ((char)((num << 8) | this.lastByte)));
				charCount--;
				charsUsed++;
				this.lastByte = -1;
			}
			if (charCount * 2 < byteCount)
			{
				byteCount = charCount * 2;
				completed = false;
			}
			else
			{
				completed = true;
			}
			if (this.bigEndian == BitConverter.IsLittleEndian)
			{
				int i = byteIndex;
				int num2 = i + (byteCount & -2);
				if (this.bigEndian)
				{
					while (i < num2)
					{
						int num3 = (int)bytes[i++];
						int num4 = (int)bytes[i++];
						chars[charIndex++] = (char)((num3 << 8) | num4);
					}
				}
				else
				{
					while (i < num2)
					{
						int num5 = (int)bytes[i++];
						int num6 = (int)bytes[i++];
						chars[charIndex++] = (char)((num6 << 8) | num5);
					}
				}
			}
			else
			{
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, byteCount & -2);
			}
			charsUsed += byteCount / 2;
			bytesUsed += byteCount;
			if ((byteCount & 1) != 0)
			{
				this.lastByte = (int)bytes[byteIndex + byteCount - 1];
			}
		}

		// Token: 0x04001017 RID: 4119
		private bool bigEndian;

		// Token: 0x04001018 RID: 4120
		private int lastByte;

		// Token: 0x04001019 RID: 4121
		private const int CharSize = 2;
	}
}
