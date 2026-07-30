using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E6 RID: 486
	[ComVisible(true)]
	[Serializable]
	public class UnauthorizedAccessException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class.</summary>
		// Token: 0x0600166C RID: 5740 RVA: 0x000590C7 File Offset: 0x000572C7
		public UnauthorizedAccessException()
			: base(Environment.GetResourceString("Attempted to perform an unauthorized operation."))
		{
			base.SetErrorCode(-2147024891);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600166D RID: 5741 RVA: 0x000590E4 File Offset: 0x000572E4
		public UnauthorizedAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024891);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600166E RID: 5742 RVA: 0x000590F8 File Offset: 0x000572F8
		public UnauthorizedAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147024891);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600166F RID: 5743 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
