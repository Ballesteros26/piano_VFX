using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
	// Token: 0x02000282 RID: 642
	[ComVisible(true)]
	[Serializable]
	internal abstract class EncodingNLS : Encoding
	{
		// Token: 0x06001D85 RID: 7557 RVA: 0x0006E63F File Offset: 0x0006C83F
		protected EncodingNLS(int codePage)
			: base(codePage)
		{
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0006E648 File Offset: 0x0006C848
		[SecuritySafeCritical]
		public unsafe override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("Index and count must refer to a location within the buffer."));
			}
			if (chars.Length == 0)
			{
				return 0;
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			return this.GetByteCount(ptr + index, count, null);
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0006E6E0 File Offset: 0x0006C8E0
		[SecuritySafeCritical]
		public unsafe override int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.GetByteCount(ptr, s.Length, null);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00069A45 File Offset: 0x00067C45
		[SecurityCritical]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			return this.GetByteCount(chars, count, null);
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0006E71C File Offset: 0x0006C91C
		[SecuritySafeCritical]
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null || bytes == null)
			{
				throw new ArgumentNullException((s == null) ? "s" : "bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("Non-negative number required."));
			}
			if (s.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("s", Environment.GetResourceString("Index and count must refer to a location within the string."));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			int num = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			byte[] array;
			byte* ptr2;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, null);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0006E810 File Offset: 0x0006CA10
		[SecuritySafeCritical]
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("Non-negative number required."));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("Index and count must refer to a location within the buffer."));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (chars.Length == 0)
			{
				return 0;
			}
			int num = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			byte[] array;
			byte* ptr2;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, null);
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0006E90C File Offset: 0x0006CB0C
		[SecurityCritical]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			return this.GetBytes(chars, charCount, bytes, byteCount, null);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0006E97C File Offset: 0x0006CB7C
		[SecuritySafeCritical]
		public unsafe override int GetCharCount(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("Index and count must refer to a location within the buffer."));
			}
			if (bytes.Length == 0)
			{
				return 0;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return this.GetCharCount(ptr + index, count, null);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x00069D77 File Offset: 0x00067F77
		[SecurityCritical]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			return this.GetCharCount(bytes, count, null);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0006EA10 File Offset: 0x0006CC10
		[SecuritySafeCritical]
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("Index and count must refer to a location within the buffer."));
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (bytes.Length == 0)
			{
				return 0;
			}
			int num = chars.Length - charIndex;
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			char[] array;
			char* ptr2;
			if ((array = chars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, num, null);
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0006EB0C File Offset: 0x0006CD0C
		[SecurityCritical]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			return this.GetChars(bytes, byteCount, chars, charCount, null);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0006EB7C File Offset: 0x0006CD7C
		[SecuritySafeCritical]
		public unsafe override string GetString(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("Index and count must refer to a location within the buffer."));
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return string.CreateStringFromEncoding(ptr + index, count, this);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0006A632 File Offset: 0x00068832
		public override Decoder GetDecoder()
		{
			return new DecoderNLS(this);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0006A63A File Offset: 0x0006883A
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}
	}
}
