using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when the number of parameters for an invocation does not match the number expected. This class cannot be inherited.</summary>
	// Token: 0x02000302 RID: 770
	[ComVisible(true)]
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with an empty message string and the root cause of the exception.</summary>
		// Token: 0x0600211B RID: 8475 RVA: 0x0007F0E5 File Offset: 0x0007D2E5
		public TargetParameterCountException()
			: base(Environment.GetResourceString("Number of parameters specified does not match the expected number."))
		{
			base.SetErrorCode(-2147352562);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with its message string set to the given message and the root cause exception.</summary>
		/// <param name="message">A String describing the reason this exception was thrown. </param>
		// Token: 0x0600211C RID: 8476 RVA: 0x0007F102 File Offset: 0x0007D302
		public TargetParameterCountException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147352562);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600211D RID: 8477 RVA: 0x0007F116 File Offset: 0x0007D316
		public TargetParameterCountException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147352562);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0007E05D File Offset: 0x0007C25D
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
