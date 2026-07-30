using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown in <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> when the filter criteria is not valid for the type of filter you are using.</summary>
	// Token: 0x020002E2 RID: 738
	[ComVisible(true)]
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the default properties.</summary>
		// Token: 0x0600205A RID: 8282 RVA: 0x0007E017 File Offset: 0x0007C217
		public InvalidFilterCriteriaException()
			: base(Environment.GetResourceString("Specified filter criteria was invalid."))
		{
			base.SetErrorCode(-2146232831);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the given HRESULT and message string.</summary>
		/// <param name="message">The message text for the exception. </param>
		// Token: 0x0600205B RID: 8283 RVA: 0x0007E034 File Offset: 0x0007C234
		public InvalidFilterCriteriaException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232831);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600205C RID: 8284 RVA: 0x0007E048 File Offset: 0x0007C248
		public InvalidFilterCriteriaException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232831);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">A SerializationInfo object that contains the information required to serialize this instance. </param>
		/// <param name="context">A StreamingContext object that contains the source and destination of the serialized stream associated with this instance. </param>
		// Token: 0x0600205D RID: 8285 RVA: 0x0007E05D File Offset: 0x0007C25D
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
