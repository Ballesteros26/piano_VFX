using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when the type of the incoming SAFEARRAY does not match the type specified in the managed signature.</summary>
	// Token: 0x020008EE RID: 2286
	[ComVisible(true)]
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class with default values.</summary>
		// Token: 0x0600559B RID: 21915 RVA: 0x00128FFE File Offset: 0x001271FE
		public SafeArrayTypeMismatchException()
			: base(Environment.GetResourceString("Specified array was not of the expected type."))
		{
			base.SetErrorCode(-2146233037);
		}

		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x0600559C RID: 21916 RVA: 0x0012901B File Offset: 0x0012721B
		public SafeArrayTypeMismatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233037);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeArrayTypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600559D RID: 21917 RVA: 0x0012902F File Offset: 0x0012722F
		public SafeArrayTypeMismatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233037);
		}

		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x0600559E RID: 21918 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
