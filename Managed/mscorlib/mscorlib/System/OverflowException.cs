using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an arithmetic, casting, or conversion operation in a checked context results in an overflow.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AD RID: 429
	[ComVisible(true)]
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class.</summary>
		// Token: 0x060011FB RID: 4603 RVA: 0x000498C8 File Offset: 0x00047AC8
		public OverflowException()
			: base(Environment.GetResourceString("Arithmetic operation resulted in an overflow."))
		{
			base.SetErrorCode(-2146233066);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060011FC RID: 4604 RVA: 0x000498E5 File Offset: 0x00047AE5
		public OverflowException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233066);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060011FD RID: 4605 RVA: 0x000498F9 File Offset: 0x00047AF9
		public OverflowException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233066);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060011FE RID: 4606 RVA: 0x0003CA1C File Offset: 0x0003AC1C
		protected OverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
