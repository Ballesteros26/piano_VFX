using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000292 RID: 658
	internal class Ucs4Encoding : Encoding
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x0008E9FA File Offset: 0x0008CBFA
		public override string WebName
		{
			get
			{
				return this.EncodingName;
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0008EA02 File Offset: 0x0008CC02
		public override Decoder GetDecoder()
		{
			return this.ucs4Decoder;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0008EA0A File Offset: 0x0008CC0A
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return checked(count * 4);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0008EA0F File Offset: 0x0008CC0F
		public override int GetByteCount(char[] chars)
		{
			return chars.Length * 4;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0000365F File Offset: 0x0000185F
		public override byte[] GetBytes(string s)
		{
			return null;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0000226C File Offset: 0x0000046C
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return 0;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0000226C File Offset: 0x0000046C
		public override int GetMaxByteCount(int charCount)
		{
			return 0;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0008EA16 File Offset: 0x0008CC16
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.ucs4Decoder.GetCharCount(bytes, index, count);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0008EA26 File Offset: 0x0008CC26
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.ucs4Decoder.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0008EA3A File Offset: 0x0008CC3A
		public override int GetMaxCharCount(int byteCount)
		{
			return (byteCount + 3) / 4;
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0000226C File Offset: 0x0000046C
		public override int CodePage
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0008EA41 File Offset: 0x0008CC41
		public override int GetCharCount(byte[] bytes)
		{
			return bytes.Length / 4;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0000365F File Offset: 0x0000185F
		public override Encoder GetEncoder()
		{
			return null;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0008EA48 File Offset: 0x0008CC48
		internal static Encoding UCS4_Littleendian
		{
			get
			{
				return new Ucs4Encoding4321();
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0008EA4F File Offset: 0x0008CC4F
		internal static Encoding UCS4_Bigendian
		{
			get
			{
				return new Ucs4Encoding1234();
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x0008EA56 File Offset: 0x0008CC56
		internal static Encoding UCS4_2143
		{
			get
			{
				return new Ucs4Encoding2143();
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0008EA5D File Offset: 0x0008CC5D
		internal static Encoding UCS4_3412
		{
			get
			{
				return new Ucs4Encoding3412();
			}
		}

		// Token: 0x0400101A RID: 4122
		internal Ucs4Decoder ucs4Decoder;
	}
}
