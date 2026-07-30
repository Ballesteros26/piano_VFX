using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when reading is attempted past the end of a stream.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000391 RID: 913
	[ComVisible(true)]
	[Serializable]
	public class EndOfStreamException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_ENDOFSTREAM.</summary>
		// Token: 0x06002A81 RID: 10881 RVA: 0x00098014 File Offset: 0x00096214
		public EndOfStreamException()
			: base(Environment.GetResourceString("Attempted to read past the end of the stream."))
		{
			base.SetErrorCode(-2147024858);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_ENDOFSTREAM.</summary>
		/// <param name="message">A string that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06002A82 RID: 10882 RVA: 0x00098031 File Offset: 0x00096231
		public EndOfStreamException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024858);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002A83 RID: 10883 RVA: 0x00098045 File Offset: 0x00096245
		public EndOfStreamException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024858);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06002A84 RID: 10884 RVA: 0x00097FED File Offset: 0x000961ED
		protected EndOfStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
