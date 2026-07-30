using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a program contains invalid Microsoft intermediate language (MSIL) or metadata. Generally this indicates a bug in the compiler that generated the program.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000194 RID: 404
	[ComVisible(true)]
	[Serializable]
	public sealed class InvalidProgramException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with default properties.</summary>
		// Token: 0x0600111D RID: 4381 RVA: 0x00047B3E File Offset: 0x00045D3E
		public InvalidProgramException()
			: base(Environment.GetResourceString("Common Language Runtime detected an invalid program."))
		{
			base.SetErrorCode(-2146233030);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x0600111E RID: 4382 RVA: 0x00047B5B File Offset: 0x00045D5B
		public InvalidProgramException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233030);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600111F RID: 4383 RVA: 0x00047B6F File Offset: 0x00045D6F
		public InvalidProgramException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233030);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal InvalidProgramException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
