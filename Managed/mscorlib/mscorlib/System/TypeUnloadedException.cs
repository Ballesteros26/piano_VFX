using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to access an unloaded class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E2 RID: 482
	[ComVisible(true)]
	[Serializable]
	public class TypeUnloadedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class.</summary>
		// Token: 0x06001609 RID: 5641 RVA: 0x00058AE1 File Offset: 0x00056CE1
		public TypeUnloadedException()
			: base(Environment.GetResourceString("Type had been unloaded."))
		{
			base.SetErrorCode(-2146234349);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600160A RID: 5642 RVA: 0x00058AFE File Offset: 0x00056CFE
		public TypeUnloadedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146234349);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600160B RID: 5643 RVA: 0x00058B12 File Offset: 0x00056D12
		public TypeUnloadedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146234349);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600160C RID: 5644 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected TypeUnloadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
