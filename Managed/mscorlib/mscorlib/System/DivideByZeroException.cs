using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to divide an integral or decimal value by zero.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000150 RID: 336
	[ComVisible(true)]
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class.</summary>
		// Token: 0x06000E6E RID: 3694 RVA: 0x0003C9D6 File Offset: 0x0003ABD6
		public DivideByZeroException()
			: base(Environment.GetResourceString("Attempted to divide by zero."))
		{
			base.SetErrorCode(-2147352558);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000E6F RID: 3695 RVA: 0x0003C9F3 File Offset: 0x0003ABF3
		public DivideByZeroException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147352558);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000E70 RID: 3696 RVA: 0x0003CA07 File Offset: 0x0003AC07
		public DivideByZeroException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147352558);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000E71 RID: 3697 RVA: 0x0003CA1C File Offset: 0x0003AC1C
		protected DivideByZeroException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
