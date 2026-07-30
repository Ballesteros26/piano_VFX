using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents structured exception handling (SEH) errors. </summary>
	// Token: 0x020008F1 RID: 2289
	[ComVisible(true)]
	[Serializable]
	public class SEHException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SEHException" /> class. </summary>
		// Token: 0x060055AF RID: 21935 RVA: 0x00129284 File Offset: 0x00127484
		public SEHException()
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SEHException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x060055B0 RID: 21936 RVA: 0x00128AC0 File Offset: 0x00126CC0
		public SEHException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SEHException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060055B1 RID: 21937 RVA: 0x00128AD4 File Offset: 0x00126CD4
		public SEHException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SEHException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x060055B2 RID: 21938 RVA: 0x00128B1F File Offset: 0x00126D1F
		protected SEHException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Indicates whether the exception can be recovered from, and whether the code can continue from the point at which the exception was thrown.</summary>
		/// <returns>Always false, because resumable exceptions are not implemented.</returns>
		// Token: 0x060055B3 RID: 21939 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool CanResume()
		{
			return false;
		}
	}
}
