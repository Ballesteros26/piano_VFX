using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001E5 RID: 485
	internal sealed class SqlUnicodeEncoding : UnicodeEncoding
	{
		// Token: 0x0600166E RID: 5742 RVA: 0x0006F854 File Offset: 0x0006DA54
		private SqlUnicodeEncoding()
			: base(false, false, false)
		{
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0006F85F File Offset: 0x0006DA5F
		public override Decoder GetDecoder()
		{
			return new SqlUnicodeEncoding.SqlUnicodeDecoder();
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0006F866 File Offset: 0x0006DA66
		public override int GetMaxByteCount(int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0006F86B File Offset: 0x0006DA6B
		public static Encoding SqlUnicodeEncodingInstance
		{
			get
			{
				return SqlUnicodeEncoding.s_singletonEncoding;
			}
		}

		// Token: 0x04000EDE RID: 3806
		private static SqlUnicodeEncoding s_singletonEncoding = new SqlUnicodeEncoding();

		// Token: 0x020001E6 RID: 486
		private sealed class SqlUnicodeDecoder : Decoder
		{
			// Token: 0x06001673 RID: 5747 RVA: 0x0006F87E File Offset: 0x0006DA7E
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return count / 2;
			}

			// Token: 0x06001674 RID: 5748 RVA: 0x0006F884 File Offset: 0x0006DA84
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				int num;
				int num2;
				bool flag;
				this.Convert(bytes, byteIndex, byteCount, chars, charIndex, chars.Length - charIndex, true, out num, out num2, out flag);
				return num2;
			}

			// Token: 0x06001675 RID: 5749 RVA: 0x0006F8AD File Offset: 0x0006DAAD
			public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
			{
				charsUsed = Math.Min(charCount, byteCount / 2);
				bytesUsed = charsUsed * 2;
				completed = bytesUsed == byteCount;
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, bytesUsed);
			}
		}
	}
}
