using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when the <see cref="Overload:System.Threading.Semaphore.Release" /> method is called on a semaphore whose count is already at the maximum. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200047C RID: 1148
	[ComVisible(false)]
	[TypeForwardedFrom("System, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SemaphoreFullException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreFullException" /> class with default values.</summary>
		// Token: 0x0600363C RID: 13884 RVA: 0x000C7E31 File Offset: 0x000C6031
		public SemaphoreFullException()
			: base(Environment.GetResourceString("Adding the specified count to the semaphore would cause it to exceed its maximum count."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreFullException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600363D RID: 13885 RVA: 0x000C7E43 File Offset: 0x000C6043
		public SemaphoreFullException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreFullException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600363E RID: 13886 RVA: 0x000C7E4C File Offset: 0x000C604C
		public SemaphoreFullException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreFullException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x0600363F RID: 13887 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected SemaphoreFullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
