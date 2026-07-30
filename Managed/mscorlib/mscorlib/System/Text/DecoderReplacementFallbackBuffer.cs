using System;
using System.Security;

namespace System.Text
{
	/// <summary>Represents a substitute output string that is emitted when the original input byte sequence cannot be decoded. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000270 RID: 624
	public sealed class DecoderReplacementFallbackBuffer : DecoderFallbackBuffer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> class using the value of a <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <param name="fallback">A <see cref="T:System.Text.DecoderReplacementFallback" /> object that contains a replacement string. </param>
		// Token: 0x06001C91 RID: 7313 RVA: 0x0006B903 File Offset: 0x00069B03
		public DecoderReplacementFallbackBuffer(DecoderReplacementFallback fallback)
		{
			this.strDefault = fallback.DefaultString;
		}

		/// <summary>Prepares the replacement fallback buffer to use the current replacement string.</summary>
		/// <returns>true if the replacement string is not empty; false if the replacement string is empty.</returns>
		/// <param name="bytesUnknown">An input byte sequence. This parameter is ignored unless an exception is thrown.</param>
		/// <param name="index">The index position of the byte in <paramref name="bytesUnknown" />. This parameter is ignored in this operation.</param>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> method has read all the characters in the replacement fallback buffer.  </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C92 RID: 7314 RVA: 0x0006B925 File Offset: 0x00069B25
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			if (this.fallbackCount >= 1)
			{
				base.ThrowLastBytesRecursive(bytesUnknown);
			}
			if (this.strDefault.Length == 0)
			{
				return false;
			}
			this.fallbackCount = this.strDefault.Length;
			this.fallbackIndex = -1;
			return true;
		}

		/// <summary>Retrieves the next character in the replacement fallback buffer.</summary>
		/// <returns>The next character in the replacement fallback buffer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C93 RID: 7315 RVA: 0x0006B960 File Offset: 0x00069B60
		public override char GetNextChar()
		{
			this.fallbackCount--;
			this.fallbackIndex++;
			if (this.fallbackCount < 0)
			{
				return '\0';
			}
			if (this.fallbackCount == 2147483647)
			{
				this.fallbackCount = -1;
				return '\0';
			}
			return this.strDefault[this.fallbackIndex];
		}

		/// <summary>Causes the next call to <see cref="M:System.Text.DecoderReplacementFallbackBuffer.GetNextChar" /> to access the character position in the replacement fallback buffer prior to the current character position.</summary>
		/// <returns>true if the <see cref="M:System.Text.DecoderReplacementFallbackBuffer.MovePrevious" /> operation was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C94 RID: 7316 RVA: 0x0006B9BB File Offset: 0x00069BBB
		public override bool MovePrevious()
		{
			if (this.fallbackCount >= -1 && this.fallbackIndex >= 0)
			{
				this.fallbackIndex--;
				this.fallbackCount++;
				return true;
			}
			return false;
		}

		/// <summary>Gets the number of characters in the replacement fallback buffer that remain to be processed.</summary>
		/// <returns>The number of characters in the replacement fallback buffer that have not yet been processed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0006B9EE File Offset: 0x00069BEE
		public override int Remaining
		{
			get
			{
				if (this.fallbackCount >= 0)
				{
					return this.fallbackCount;
				}
				return 0;
			}
		}

		/// <summary>Initializes all internal state information and data in the <see cref="T:System.Text.DecoderReplacementFallbackBuffer" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C96 RID: 7318 RVA: 0x0006BA01 File Offset: 0x00069C01
		[SecuritySafeCritical]
		public override void Reset()
		{
			this.fallbackCount = -1;
			this.fallbackIndex = -1;
			this.byteStart = null;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0006BA19 File Offset: 0x00069C19
		[SecurityCritical]
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return this.strDefault.Length;
		}

		// Token: 0x04000FD4 RID: 4052
		private string strDefault;

		// Token: 0x04000FD5 RID: 4053
		private int fallbackCount = -1;

		// Token: 0x04000FD6 RID: 4054
		private int fallbackIndex = -1;
	}
}
