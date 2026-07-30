using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown for errors in an arithmetic, casting, or conversion operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000129 RID: 297
	[ComVisible(true)]
	[Serializable]
	public class ArithmeticException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class.</summary>
		// Token: 0x06000A5D RID: 2653 RVA: 0x00032B6A File Offset: 0x00030D6A
		public ArithmeticException()
			: base(Environment.GetResourceString("Overflow or underflow in the arithmetic operation."))
		{
			base.SetErrorCode(-2147024362);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000A5E RID: 2654 RVA: 0x00032B87 File Offset: 0x00030D87
		public ArithmeticException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024362);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000A5F RID: 2655 RVA: 0x00032B9B File Offset: 0x00030D9B
		public ArithmeticException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024362);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000A60 RID: 2656 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected ArithmeticException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
