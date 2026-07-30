using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when an invalid COM object is used.</summary>
	// Token: 0x020008E5 RID: 2277
	[ComVisible(true)]
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		/// <summary>Initializes an instance of the InvalidComObjectException with default properties.</summary>
		// Token: 0x06005576 RID: 21878 RVA: 0x00128E43 File Offset: 0x00127043
		public InvalidComObjectException()
			: base(Environment.GetResourceString("Attempt has been made to use a COM object that does not have a backing class factory."))
		{
			base.SetErrorCode(-2146233049);
		}

		/// <summary>Initializes an instance of the InvalidComObjectException with a message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x06005577 RID: 21879 RVA: 0x00128E60 File Offset: 0x00127060
		public InvalidComObjectException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233049);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InvalidComObjectException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06005578 RID: 21880 RVA: 0x00128E74 File Offset: 0x00127074
		public InvalidComObjectException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233049);
		}

		/// <summary>Initializes a new instance of the COMException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06005579 RID: 21881 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
