using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a non-fatal application error occurs.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000124 RID: 292
	[ComVisible(true)]
	[Serializable]
	public class ApplicationException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class.</summary>
		// Token: 0x06000A36 RID: 2614 RVA: 0x00032596 File Offset: 0x00030796
		public ApplicationException()
			: base(Environment.GetResourceString("Error in the application."))
		{
			base.SetErrorCode(-2146232832);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error. </param>
		// Token: 0x06000A37 RID: 2615 RVA: 0x000325B3 File Offset: 0x000307B3
		public ApplicationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232832);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000A38 RID: 2616 RVA: 0x000325C7 File Offset: 0x000307C7
		public ApplicationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232832);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000A39 RID: 2617 RVA: 0x000325DC File Offset: 0x000307DC
		protected ApplicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
