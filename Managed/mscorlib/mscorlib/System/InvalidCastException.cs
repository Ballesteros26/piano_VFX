using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown for invalid casting or explicit conversion.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000192 RID: 402
	[ComVisible(true)]
	[Serializable]
	public class InvalidCastException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class.</summary>
		// Token: 0x06001114 RID: 4372 RVA: 0x00047AA2 File Offset: 0x00045CA2
		public InvalidCastException()
			: base(Environment.GetResourceString("Specified cast is not valid."))
		{
			base.SetErrorCode(-2147467262);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001115 RID: 4373 RVA: 0x00047ABF File Offset: 0x00045CBF
		public InvalidCastException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467262);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001116 RID: 4374 RVA: 0x00047AD3 File Offset: 0x00045CD3
		public InvalidCastException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467262);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001117 RID: 4375 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected InvalidCastException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified message and error code.</summary>
		/// <param name="message">The message that indicates the reason the exception occurred.</param>
		/// <param name="errorCode">The error code (HRESULT) value associated with the exception.</param>
		// Token: 0x06001118 RID: 4376 RVA: 0x00047AE8 File Offset: 0x00045CE8
		public InvalidCastException(string message, int errorCode)
			: base(message)
		{
			base.SetErrorCode(errorCode);
		}
	}
}
