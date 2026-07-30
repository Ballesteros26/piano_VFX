using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the time allotted for a process or operation has expired.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CD RID: 461
	[ComVisible(true)]
	[Serializable]
	public class TimeoutException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class.</summary>
		// Token: 0x06001432 RID: 5170 RVA: 0x00051CEE File Offset: 0x0004FEEE
		public TimeoutException()
			: base(Environment.GetResourceString("The operation has timed out."))
		{
			base.SetErrorCode(-2146233083);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001433 RID: 5171 RVA: 0x00051D0B File Offset: 0x0004FF0B
		public TimeoutException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233083);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">The message that describes the error. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001434 RID: 5172 RVA: 0x00051D1F File Offset: 0x0004FF1F
		public TimeoutException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233083);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. The <paramref name="context" /> parameter is reserved for future use, and can be specified as null.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null, or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
		// Token: 0x06001435 RID: 5173 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected TimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
