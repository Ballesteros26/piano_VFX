using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is not enough memory to continue the execution of a program.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AC RID: 428
	[ComVisible(true)]
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class.</summary>
		// Token: 0x060011F7 RID: 4599 RVA: 0x00049886 File Offset: 0x00047A86
		public OutOfMemoryException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.SetErrorCode(-2147024882);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060011F8 RID: 4600 RVA: 0x0004989F File Offset: 0x00047A9F
		public OutOfMemoryException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024882);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011F9 RID: 4601 RVA: 0x000498B3 File Offset: 0x00047AB3
		public OutOfMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024882);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060011FA RID: 4602 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
