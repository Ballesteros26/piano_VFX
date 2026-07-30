using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a method call is invalid for the object's current state.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000193 RID: 403
	[ComVisible(true)]
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class.</summary>
		// Token: 0x06001119 RID: 4377 RVA: 0x00047AF8 File Offset: 0x00045CF8
		public InvalidOperationException()
			: base(Environment.GetResourceString("Operation is not valid due to the current state of the object."))
		{
			base.SetErrorCode(-2146233079);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600111A RID: 4378 RVA: 0x00047B15 File Offset: 0x00045D15
		public InvalidOperationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233079);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600111B RID: 4379 RVA: 0x00047B29 File Offset: 0x00045D29
		public InvalidOperationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233079);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600111C RID: 4380 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected InvalidOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
