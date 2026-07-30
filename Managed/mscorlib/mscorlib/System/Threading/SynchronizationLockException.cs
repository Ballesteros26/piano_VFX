using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a method requires the caller to own the lock on a given Monitor, and the method is invoked by a caller that does not own that lock.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000481 RID: 1153
	[ComVisible(true)]
	[Serializable]
	public class SynchronizationLockException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with default properties.</summary>
		// Token: 0x06003657 RID: 13911 RVA: 0x000C7FFE File Offset: 0x000C61FE
		public SynchronizationLockException()
			: base(Environment.GetResourceString("Object synchronization method was called from an unsynchronized block of code."))
		{
			base.SetErrorCode(-2146233064);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003658 RID: 13912 RVA: 0x000C801B File Offset: 0x000C621B
		public SynchronizationLockException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233064);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003659 RID: 13913 RVA: 0x000C802F File Offset: 0x000C622F
		public SynchronizationLockException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233064);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600365A RID: 13914 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected SynchronizationLockException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
