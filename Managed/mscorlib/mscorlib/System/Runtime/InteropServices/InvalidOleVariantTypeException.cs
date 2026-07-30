using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown by the marshaler when it encounters an argument of a variant type that can not be marshaled to managed code.</summary>
	// Token: 0x020008E6 RID: 2278
	[ComVisible(true)]
	[Serializable]
	public class InvalidOleVariantTypeException : SystemException
	{
		/// <summary>Initializes a new instance of the InvalidOleVariantTypeException class with default values.</summary>
		// Token: 0x0600557A RID: 21882 RVA: 0x00128E89 File Offset: 0x00127089
		public InvalidOleVariantTypeException()
			: base(Environment.GetResourceString("Specified OLE variant was invalid."))
		{
			base.SetErrorCode(-2146233039);
		}

		/// <summary>Initializes a new instance of the InvalidOleVariantTypeException class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x0600557B RID: 21883 RVA: 0x00128EA6 File Offset: 0x001270A6
		public InvalidOleVariantTypeException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233039);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600557C RID: 21884 RVA: 0x00128EBA File Offset: 0x001270BA
		public InvalidOleVariantTypeException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233039);
		}

		/// <summary>Initializes a new instance of the InvalidOleVariantTypeException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x0600557D RID: 21885 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
