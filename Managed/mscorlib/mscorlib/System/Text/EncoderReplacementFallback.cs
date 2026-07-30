using System;

namespace System.Text
{
	/// <summary>Provides a failure handling mechanism, called a fallback, for an input character that cannot be converted to an output byte sequence. The fallback uses a user-specified replacement string instead of the original input character. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027A RID: 634
	[Serializable]
	public sealed class EncoderReplacementFallback : EncoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallback" /> class.</summary>
		// Token: 0x06001CEA RID: 7402 RVA: 0x0006CB35 File Offset: 0x0006AD35
		public EncoderReplacementFallback()
			: this("?")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallback" /> class using a specified replacement string.</summary>
		/// <param name="replacement">A string that is converted in an encoding operation in place of an input character that cannot be encoded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacement" /> contains an invalid surrogate pair. In other words, the surrogate does not consist of one high surrogate component followed by one low surrogate component.</exception>
		// Token: 0x06001CEB RID: 7403 RVA: 0x0006CB44 File Offset: 0x0006AD44
		public EncoderReplacementFallback(string replacement)
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

		/// <summary>Gets the replacement string that is the value of the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>A substitute string that is used in place of an input character that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0006CBC7 File Offset: 0x0006ADC7
		public string DefaultString
		{
			get
			{
				return this.strDefault;
			}
		}

		/// <summary>Creates a <see cref="T:System.Text.EncoderFallbackBuffer" /> object that is initialized with the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>A <see cref="T:System.Text.EncoderFallbackBuffer" /> object equal to this <see cref="T:System.Text.EncoderReplacementFallback" /> object. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CED RID: 7405 RVA: 0x0006CBCF File Offset: 0x0006ADCF
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		/// <summary>Gets the number of characters in the replacement string for the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>The number of characters in the string used in place of an input character that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0006CBD7 File Offset: 0x0006ADD7
		public override int MaxCharCount
		{
			get
			{
				return this.strDefault.Length;
			}
		}

		/// <summary>Indicates whether the value of a specified object is equal to the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>true if the <paramref name="value" /> parameter specifies an <see cref="T:System.Text.EncoderReplacementFallback" /> object and the replacement string of that object is equal to the replacement string of this <see cref="T:System.Text.EncoderReplacementFallback" /> object; otherwise, false. </returns>
		/// <param name="value">A <see cref="T:System.Text.EncoderReplacementFallback" /> object.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CEF RID: 7407 RVA: 0x0006CBE4 File Offset: 0x0006ADE4
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this.strDefault == encoderReplacementFallback.strDefault;
		}

		/// <summary>Retrieves the hash code for the value of the <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <returns>The hash code of the value of the object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CF0 RID: 7408 RVA: 0x0006CC0E File Offset: 0x0006AE0E
		public override int GetHashCode()
		{
			return this.strDefault.GetHashCode();
		}

		// Token: 0x04000FF5 RID: 4085
		private string strDefault;
	}
}
