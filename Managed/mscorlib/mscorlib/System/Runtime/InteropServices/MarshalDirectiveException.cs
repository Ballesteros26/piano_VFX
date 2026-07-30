using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception that is thrown by the marshaler when it encounters a <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> it does not support.</summary>
	// Token: 0x020008EA RID: 2282
	[ComVisible(true)]
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		/// <summary>Initializes a new instance of the MarshalDirectiveException class with default properties.</summary>
		// Token: 0x06005586 RID: 21894 RVA: 0x00128ECF File Offset: 0x001270CF
		public MarshalDirectiveException()
			: base(Environment.GetResourceString("Marshaling directives are invalid."))
		{
			base.SetErrorCode(-2146233035);
		}

		/// <summary>Initializes a new instance of the MarshalDirectiveException class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		// Token: 0x06005587 RID: 21895 RVA: 0x00128EEC File Offset: 0x001270EC
		public MarshalDirectiveException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233035);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalDirectiveException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06005588 RID: 21896 RVA: 0x00128F00 File Offset: 0x00127100
		public MarshalDirectiveException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233035);
		}

		/// <summary>Initializes a new instance of the MarshalDirectiveException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06005589 RID: 21897 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
