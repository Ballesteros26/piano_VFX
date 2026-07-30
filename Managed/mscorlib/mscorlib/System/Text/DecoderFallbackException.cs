using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when a decoder fallback operation fails. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200026B RID: 619
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. </summary>
		// Token: 0x06001C61 RID: 7265 RVA: 0x0006AF8D File Offset: 0x0006918D
		public DecoderFallbackException()
			: base(Environment.GetResourceString("Value does not fall within the expected range."))
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x06001C62 RID: 7266 RVA: 0x0006AFAA File Offset: 0x000691AA
		public DecoderFallbackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x06001C63 RID: 7267 RVA: 0x0006AFBE File Offset: 0x000691BE
		public DecoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00032A15 File Offset: 0x00030C15
		internal DecoderFallbackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message, the array of bytes being decoded, and the index of the byte that cannot be decoded.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="bytesUnknown">The input byte array.</param>
		/// <param name="index">The index position in <paramref name="bytesUnknown" /> of the byte that cannot be decoded.</param>
		// Token: 0x06001C65 RID: 7269 RVA: 0x0006AFD3 File Offset: 0x000691D3
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
			: base(message)
		{
			this.bytesUnknown = bytesUnknown;
			this.index = index;
		}

		/// <summary>Gets the input byte sequence that caused the exception.</summary>
		/// <returns>The input byte array that cannot be decoded. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x0006AFEA File Offset: 0x000691EA
		public byte[] BytesUnknown
		{
			get
			{
				return this.bytesUnknown;
			}
		}

		/// <summary>Gets the index position in the input byte sequence of the byte that caused the exception.</summary>
		/// <returns>The index position in the input byte array of the byte that cannot be decoded. The index position is zero-based. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0006AFF2 File Offset: 0x000691F2
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04000FC7 RID: 4039
		private byte[] bytesUnknown;

		// Token: 0x04000FC8 RID: 4040
		private int index;
	}
}
