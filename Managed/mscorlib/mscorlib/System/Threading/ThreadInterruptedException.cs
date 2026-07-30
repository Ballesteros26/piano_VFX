using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Threading.Thread" /> is interrupted while it is in a waiting state.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000487 RID: 1159
	[ComVisible(true)]
	[Serializable]
	public class ThreadInterruptedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with default properties.</summary>
		// Token: 0x060036F7 RID: 14071 RVA: 0x000C8D9B File Offset: 0x000C6F9B
		public ThreadInterruptedException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadInterrupted))
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060036F8 RID: 14072 RVA: 0x000C8DB4 File Offset: 0x000C6FB4
		public ThreadInterruptedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060036F9 RID: 14073 RVA: 0x000C8DC8 File Offset: 0x000C6FC8
		public ThreadInterruptedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x060036FA RID: 14074 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected ThreadInterruptedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
