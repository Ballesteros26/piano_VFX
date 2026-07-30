using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when an attempt is made to open a system mutex or semaphore that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004A0 RID: 1184
	[ComVisible(false)]
	[Serializable]
	public class WaitHandleCannotBeOpenedException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with default values.</summary>
		// Token: 0x0600379E RID: 14238 RVA: 0x000CABEE File Offset: 0x000C8DEE
		public WaitHandleCannotBeOpenedException()
			: base(Environment.GetResourceString("No handle of the given name exists."))
		{
			base.SetErrorCode(-2146233044);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600379F RID: 14239 RVA: 0x000CAC0B File Offset: 0x000C8E0B
		public WaitHandleCannotBeOpenedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233044);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060037A0 RID: 14240 RVA: 0x000CAC1F File Offset: 0x000C8E1F
		public WaitHandleCannotBeOpenedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233044);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x060037A1 RID: 14241 RVA: 0x0007E05D File Offset: 0x0007C25D
		protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
