using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	/// <summary>The exception that is thrown when an unexpected operation occurs during a cryptographic operation.</summary>
	// Token: 0x0200064F RID: 1615
	[ComVisible(true)]
	[Serializable]
	public class CryptographicUnexpectedOperationException : CryptographicException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with default properties.</summary>
		// Token: 0x060045DC RID: 17884 RVA: 0x000F5309 File Offset: 0x000F3509
		public CryptographicUnexpectedOperationException()
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060045DD RID: 17885 RVA: 0x000F531C File Offset: 0x000F351C
		public CryptographicUnexpectedOperationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message in the specified format.</summary>
		/// <param name="format">The format used to output the error message. </param>
		/// <param name="insert">The error message that explains the reason for the exception. </param>
		// Token: 0x060045DE RID: 17886 RVA: 0x000F5330 File Offset: 0x000F3530
		public CryptographicUnexpectedOperationException(string format, string insert)
			: base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060045DF RID: 17887 RVA: 0x000F534F File Offset: 0x000F354F
		public CryptographicUnexpectedOperationException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060045E0 RID: 17888 RVA: 0x000F5364 File Offset: 0x000F3564
		protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
