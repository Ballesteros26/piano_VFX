using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace System.Security.Cryptography
{
	/// <summary>The exception that is thrown when an error occurs during a cryptographic operation.</summary>
	// Token: 0x0200064E RID: 1614
	[ComVisible(true)]
	[Serializable]
	public class CryptographicException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with default properties.</summary>
		// Token: 0x060045D5 RID: 17877 RVA: 0x000F5267 File Offset: 0x000F3467
		public CryptographicException()
			: base(Environment.GetResourceString("Error occurred during a cryptographic operation."))
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060045D6 RID: 17878 RVA: 0x000F5284 File Offset: 0x000F3484
		public CryptographicException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message in the specified format.</summary>
		/// <param name="format">The format used to output the error message. </param>
		/// <param name="insert">The error message that explains the reason for the exception. </param>
		// Token: 0x060045D7 RID: 17879 RVA: 0x000F5298 File Offset: 0x000F3498
		public CryptographicException(string format, string insert)
			: base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060045D8 RID: 17880 RVA: 0x000F52B7 File Offset: 0x000F34B7
		public CryptographicException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233296);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with the specified HRESULT error code.</summary>
		/// <param name="hr">The HRESULT error code. </param>
		// Token: 0x060045D9 RID: 17881 RVA: 0x000F52CC File Offset: 0x000F34CC
		[SecuritySafeCritical]
		public CryptographicException(int hr)
			: this(Win32Native.GetMessage(hr))
		{
			if (((long)hr & (long)((ulong)(-2147483648))) != (long)((ulong)(-2147483648)))
			{
				hr = (hr & 65535) | -2147024896;
			}
			base.SetErrorCode(hr);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060045DA RID: 17882 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected CryptographicException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x000F5301 File Offset: 0x000F3501
		private static void ThrowCryptographicException(int hr)
		{
			throw new CryptographicException(hr);
		}

		// Token: 0x040023E5 RID: 9189
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x040023E6 RID: 9190
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x040023E7 RID: 9191
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
	}
}
