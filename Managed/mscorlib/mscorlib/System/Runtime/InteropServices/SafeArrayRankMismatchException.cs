using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when the rank of an incoming SAFEARRAY does not match the rank specified in the managed signature.</summary>
	// Token: 0x020008ED RID: 2285
	[ComVisible(true)]
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class with default values.</summary>
		// Token: 0x06005597 RID: 21911 RVA: 0x00128FB8 File Offset: 0x001271B8
		public SafeArrayRankMismatchException()
			: base(Environment.GetResourceString("Specified array was not of the expected rank."))
		{
			base.SetErrorCode(-2146233032);
		}

		/// <summary>Initializes a new instance of the SafeArrayRankMismatchException class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x06005598 RID: 21912 RVA: 0x00128FD5 File Offset: 0x001271D5
		public SafeArrayRankMismatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233032);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeArrayRankMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06005599 RID: 21913 RVA: 0x00128FE9 File Offset: 0x001271E9
		public SafeArrayRankMismatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233032);
		}

		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x0600559A RID: 21914 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
