using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when an encoder fallback operation fails. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000276 RID: 630
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class.</summary>
		// Token: 0x06001CBE RID: 7358 RVA: 0x0006AF8D File Offset: 0x0006918D
		public EncoderFallbackException()
			: base(Environment.GetResourceString("Value does not fall within the expected range."))
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x06001CBF RID: 7359 RVA: 0x0006AFAA File Offset: 0x000691AA
		public EncoderFallbackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x06001CC0 RID: 7360 RVA: 0x0006AFBE File Offset: 0x000691BE
		public EncoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00032A15 File Offset: 0x00030C15
		internal EncoderFallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0006C265 File Offset: 0x0006A465
		internal EncoderFallbackException(string message, char charUnknown, int index)
			: base(message)
		{
			this.charUnknown = charUnknown;
			this.index = index;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0006C27C File Offset: 0x0006A47C
		internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index)
			: base(message)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 55296, 56319 }));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 56320, 57343 }));
			}
			this.charUnknownHigh = charUnknownHigh;
			this.charUnknownLow = charUnknownLow;
			this.index = index;
		}

		/// <summary>Gets the input character that caused the exception.</summary>
		/// <returns>The character that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0006C320 File Offset: 0x0006A520
		public char CharUnknown
		{
			get
			{
				return this.charUnknown;
			}
		}

		/// <summary>Gets the high component character of the surrogate pair that caused the exception.</summary>
		/// <returns>The high component character of the surrogate pair that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0006C328 File Offset: 0x0006A528
		public char CharUnknownHigh
		{
			get
			{
				return this.charUnknownHigh;
			}
		}

		/// <summary>Gets the low component character of the surrogate pair that caused the exception.</summary>
		/// <returns>The low component character of the surrogate pair that cannot be encoded.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0006C330 File Offset: 0x0006A530
		public char CharUnknownLow
		{
			get
			{
				return this.charUnknownLow;
			}
		}

		/// <summary>Gets the index position in the input buffer of the character that caused the exception.</summary>
		/// <returns>The index position in the input buffer of the character that cannot be encoded.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0006C338 File Offset: 0x0006A538
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Indicates whether the input that caused the exception is a surrogate pair.</summary>
		/// <returns>true if the input was a surrogate pair; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CC8 RID: 7368 RVA: 0x0006C340 File Offset: 0x0006A540
		public bool IsUnknownSurrogate()
		{
			return this.charUnknownHigh > '\0';
		}

		// Token: 0x04000FE0 RID: 4064
		private char charUnknown;

		// Token: 0x04000FE1 RID: 4065
		private char charUnknownHigh;

		// Token: 0x04000FE2 RID: 4066
		private char charUnknownLow;

		// Token: 0x04000FE3 RID: 4067
		private int index;
	}
}
