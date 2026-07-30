using System;
using System.Globalization;

namespace System.Text
{
	/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> when an encoded input byte sequence cannot be converted to a decoded output character. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200026A RID: 618
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> when the input byte sequence cannot be decoded. The nominal return value is not used. </summary>
		/// <returns>None. No value is returned because the <see cref="M:System.Text.DecoderExceptionFallbackBuffer.Fallback(System.Byte[],System.Int32)" /> method always throws an exception. The nominal return value is true. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <param name="bytesUnknown">An input array of bytes.</param>
		/// <param name="index">The index position of a byte in the input.</param>
		/// <exception cref="T:System.Text.DecoderFallbackException">This method always throws an exception that reports the value and index position of the input byte that cannot be decoded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C5B RID: 7259 RVA: 0x0006AEE2 File Offset: 0x000690E2
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.Throw(bytesUnknown, index);
			return true;
		}

		/// <summary>Retrieves the next character in the exception data buffer.</summary>
		/// <returns>The return value is always the Unicode character NULL (U+0000). A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C5C RID: 7260 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override char GetNextChar()
		{
			return '\0';
		}

		/// <summary>Causes the next call to <see cref="M:System.Text.DecoderExceptionFallbackBuffer.GetNextChar" /> to access the exception data buffer character position that is prior to the current position.</summary>
		/// <returns>The return value is always false. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001C5D RID: 7261 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool MovePrevious()
		{
			return false;
		}

		/// <summary>Gets the number of characters in the current <see cref="T:System.Text.DecoderExceptionFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The return value is always zero. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0006AEF0 File Offset: 0x000690F0
		private void Throw(byte[] bytesUnknown, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(bytesUnknown[num].ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append("]");
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new DecoderFallbackException(Environment.GetResourceString("Unable to translate bytes {0} at index {1} from specified code page to Unicode.", new object[] { stringBuilder, index }), bytesUnknown, index);
		}
	}
}
