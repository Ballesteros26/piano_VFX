using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to store an element of the wrong type within an array. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012C RID: 300
	[ComVisible(true)]
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class.</summary>
		// Token: 0x06000A7F RID: 2687 RVA: 0x00032FAF File Offset: 0x000311AF
		public ArrayTypeMismatchException()
			: base(Environment.GetResourceString("Attempted to access an element as a type incompatible with the array."))
		{
			base.SetErrorCode(-2146233085);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000A80 RID: 2688 RVA: 0x00032FCC File Offset: 0x000311CC
		public ArrayTypeMismatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233085);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000A81 RID: 2689 RVA: 0x00032FE0 File Offset: 0x000311E0
		public ArrayTypeMismatchException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233085);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000A82 RID: 2690 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
