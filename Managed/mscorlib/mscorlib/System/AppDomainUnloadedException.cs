using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to access an unloaded application domain. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000123 RID: 291
	[ComVisible(true)]
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class.</summary>
		// Token: 0x06000A32 RID: 2610 RVA: 0x00032550 File Offset: 0x00030750
		public AppDomainUnloadedException()
			: base(Environment.GetResourceString("Attempted to access an unloaded AppDomain."))
		{
			base.SetErrorCode(-2146234348);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000A33 RID: 2611 RVA: 0x0003256D File Offset: 0x0003076D
		public AppDomainUnloadedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146234348);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the error. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000A34 RID: 2612 RVA: 0x00032581 File Offset: 0x00030781
		public AppDomainUnloadedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146234348);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainUnloadedException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000A35 RID: 2613 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
