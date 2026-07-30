using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a check for sufficient available memory fails. This class cannot be inherited.</summary>
	// Token: 0x0200018E RID: 398
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060010B0 RID: 4272 RVA: 0x000473EF File Offset: 0x000455EF
		public InsufficientMemoryException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.SetErrorCode(-2146233027);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060010B1 RID: 4273 RVA: 0x00047408 File Offset: 0x00045608
		public InsufficientMemoryException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233027);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060010B2 RID: 4274 RVA: 0x0004741C File Offset: 0x0004561C
		public InsufficientMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233027);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00047431 File Offset: 0x00045631
		private InsufficientMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
