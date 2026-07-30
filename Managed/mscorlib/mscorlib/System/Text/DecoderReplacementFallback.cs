using System;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an output character. The fallback emits a user-specified replacement string instead of a decoded input byte sequence. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200026F RID: 623
	[Serializable]
	public sealed class DecoderReplacementFallback : DecoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderReplacementFallback" /> class. </summary>
		// Token: 0x06001C8A RID: 7306 RVA: 0x0006B81E File Offset: 0x00069A1E
		public DecoderReplacementFallback()
			: this("?")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderReplacementFallback" /> class using a specified replacement string.</summary>
		/// <param name="replacement">A string that is emitted in a decoding operation in place of an input byte sequence that cannot be decoded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacement" /> contains an invalid surrogate pair. In other words, the surrogate pair does not consist of one high surrogate component followed by one low surrogate component.</exception>
		// Token: 0x06001C8B RID: 7307 RVA: 0x0006B82C File Offset: 0x00069A2C
		public DecoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			bool flag = false;
			for (int i = 0; i < replacement.Length; i++)
			{
				if (char.IsSurrogate(replacement, i))
				{
					if (char.IsHighSurrogate(replacement, i))
					{
						if (flag)
						{
							break;
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							break;
						}
						flag = false;
					}
				}
				else if (flag)
				{
					break;
				}
			}
			if (flag)
			{
				throw new ArgumentException(Environment.GetResourceString("String contains invalid Unicode code points.", new object[] { "replacement" }));
			}
			this.strDefault = replacement;
		}

		/// <summary>Gets the replacement string that is the value of the <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>A substitute string that is emitted in place of an input byte sequence that cannot be decoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0006B8AF File Offset: 0x00069AAF
		public string DefaultString
		{
			get
			{
				return this.strDefault;
			}
		}

		/// <summary>Creates a <see cref="T:System.Text.DecoderFallbackBuffer" /> object that is initialized with the replacement string of this <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.DecoderFallbackBuffer" /> object that specifies a string to use instead of the original decoding operation input.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C8D RID: 7309 RVA: 0x0006B8B7 File Offset: 0x00069AB7
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderReplacementFallbackBuffer(this);
		}

		/// <summary>Gets the number of characters in the replacement string for the <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>The number of characters in the string that is emitted in place of a byte sequence that cannot be decoded, that is, the length of the string returned by the <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0006B8BF File Offset: 0x00069ABF
		public override int MaxCharCount
		{
			get
			{
				return this.strDefault.Length;
			}
		}

		/// <summary>Indicates whether the value of a specified object is equal to the <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Text.DecoderReplacementFallback" /> object having a <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> property that is equal to the <see cref="P:System.Text.DecoderReplacementFallback.DefaultString" /> property of the current <see cref="T:System.Text.DecoderReplacementFallback" /> object; otherwise, false. </returns>
		/// <param name="value">A <see cref="T:System.Text.DecoderReplacementFallback" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C8F RID: 7311 RVA: 0x0006B8CC File Offset: 0x00069ACC
		public override bool Equals(object value)
		{
			DecoderReplacementFallback decoderReplacementFallback = value as DecoderReplacementFallback;
			return decoderReplacementFallback != null && this.strDefault == decoderReplacementFallback.strDefault;
		}

		/// <summary>Retrieves the hash code for the value of the <see cref="T:System.Text.DecoderReplacementFallback" /> object.</summary>
		/// <returns>The hash code of the value of the object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C90 RID: 7312 RVA: 0x0006B8F6 File Offset: 0x00069AF6
		public override int GetHashCode()
		{
			return this.strDefault.GetHashCode();
		}

		// Token: 0x04000FD3 RID: 4051
		private string strDefault;
	}
}
