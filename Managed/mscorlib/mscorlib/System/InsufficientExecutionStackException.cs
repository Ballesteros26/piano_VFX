using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is insufficient execution stack available to allow most methods to execute.</summary>
	// Token: 0x0200018D RID: 397
	[Serializable]
	public sealed class InsufficientExecutionStackException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class. </summary>
		// Token: 0x060010AC RID: 4268 RVA: 0x000473A9 File Offset: 0x000455A9
		public InsufficientExecutionStackException()
			: base(Environment.GetResourceString("Insufficient stack to continue executing the program safely. This can happen from having too many functions on the call stack or function on the stack using too much stack space."))
		{
			base.SetErrorCode(-2146232968);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060010AD RID: 4269 RVA: 0x000473C6 File Offset: 0x000455C6
		public InsufficientExecutionStackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232968);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060010AE RID: 4270 RVA: 0x000473DA File Offset: 0x000455DA
		public InsufficientExecutionStackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232968);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00031FC1 File Offset: 0x000301C1
		private InsufficientExecutionStackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
