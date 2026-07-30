using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an internal error in the execution engine of the common language runtime. This class cannot be inherited.  </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000160 RID: 352
	[ComVisible(true)]
	[Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class.</summary>
		// Token: 0x06000F31 RID: 3889 RVA: 0x0003EA6B File Offset: 0x0003CC6B
		public ExecutionEngineException()
			: base(Environment.GetResourceString("Internal error in the runtime."))
		{
			base.SetErrorCode(-2146233082);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000F32 RID: 3890 RVA: 0x0003EA88 File Offset: 0x0003CC88
		public ExecutionEngineException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233082);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000F33 RID: 3891 RVA: 0x0003EA9C File Offset: 0x0003CC9C
		public ExecutionEngineException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233082);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal ExecutionEngineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
