using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x0200026E RID: 622
	[Serializable]
	internal class DecoderNLS : Decoder, ISerializable
	{
		// Token: 0x06001C7A RID: 7290 RVA: 0x0006B2DE File Offset: 0x000694DE
		internal DecoderNLS(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Direct deserialization of type '{0}' is not supported."), base.GetType()));
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0006B305 File Offset: 0x00069505
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeDecoder(info);
			info.AddValue("encoding", this.m_encoding);
			info.SetType(typeof(Encoding.DefaultDecoder));
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0006B32F File Offset: 0x0006952F
		internal DecoderNLS(Encoding encoding)
		{
			this.m_encoding = encoding;
			this.m_fallback = this.m_encoding.DecoderFallback;
			this.Reset();
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0006B355 File Offset: 0x00069555
		internal DecoderNLS()
		{
			this.m_encoding = null;
			this.Reset();
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0006B36A File Offset: 0x0006956A
		public override void Reset()
		{
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0006B37F File Offset: 0x0006957F
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0006B38C File Offset: 0x0006958C
		[SecuritySafeCritical]
		public unsafe override int GetCharCount(byte[] bytes, int index, int count, bool flush)
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
				bytes = new byte[1];
			}
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return this.GetCharCount(ptr + index, count, flush);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0006B428 File Offset: 0x00069628
		[SecurityCritical]
		public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = true;
			return this.m_encoding.GetCharCount(bytes, count, this);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x0006B484 File Offset: 0x00069684
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0006B494 File Offset: 0x00069694
		[SecuritySafeCritical]
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
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
				bytes = new byte[1];
			}
			int num = chars.Length - charIndex;
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char[] array2;
			char* ptr2;
			if ((array2 = chars) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, num, flush);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0006B598 File Offset: 0x00069798
		[SecurityCritical]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("Non-negative number required."));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = true;
			return this.m_encoding.GetChars(bytes, byteCount, chars, charCount, this);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0006B61C File Offset: 0x0006981C
		[SecuritySafeCritical]
		public unsafe override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("Non-negative number required."));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("Index and count must refer to a location within the buffer."));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("Index and count must refer to a location within the buffer."));
			}
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char[] array2;
			char* ptr2;
			if ((array2 = chars) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			this.Convert(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
			array2 = null;
			array = null;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0006B748 File Offset: 0x00069948
		[SecurityCritical]
		public unsafe override void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("Non-negative number required."));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = false;
			this.m_bytesUsed = 0;
			charsUsed = this.m_encoding.GetChars(bytes, byteCount, chars, charCount, this);
			bytesUsed = this.m_bytesUsed;
			completed = bytesUsed == byteCount && (!flush || !this.HasState) && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0);
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0006B80D File Offset: 0x00069A0D
		public bool MustFlush
		{
			get
			{
				return this.m_mustFlush;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal virtual bool HasState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0006B815 File Offset: 0x00069A15
		internal void ClearMustFlush()
		{
			this.m_mustFlush = false;
		}

		// Token: 0x04000FCF RID: 4047
		protected Encoding m_encoding;

		// Token: 0x04000FD0 RID: 4048
		[NonSerialized]
		protected bool m_mustFlush;

		// Token: 0x04000FD1 RID: 4049
		[NonSerialized]
		internal bool m_throwOnOverflow;

		// Token: 0x04000FD2 RID: 4050
		[NonSerialized]
		internal int m_bytesUsed;
	}
}
