using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>The exception thrown to indicate that no instances are returned by a provider.</summary>
	// Token: 0x02000366 RID: 870
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InstanceNotFoundException : InstrumentationException
	{
		/// <summary>Initializes a new instance of the InstanceNotFoundException class.</summary>
		// Token: 0x06001A4C RID: 6732 RVA: 0x0000220F File Offset: 0x0000040F
		public InstanceNotFoundException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the InstanceNotFoundException class with the specified serialization information and streaming context.</summary>
		/// <param name="info">The SerializationInfo that contains all the data required to serialize the exception.</param>
		/// <param name="context">The StreamingContext that specifies the source and destination of the stream.</param>
		// Token: 0x06001A4D RID: 6733 RVA: 0x0000220F File Offset: 0x0000040F
		protected InstanceNotFoundException(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the InstanceNotFoundException class with its message string set to message.</summary>
		/// <param name="message">A string that contains the error message that explains the reason for the exception.</param>
		// Token: 0x06001A4E RID: 6734 RVA: 0x0000220F File Offset: 0x0000040F
		public InstanceNotFoundException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the InstanceNotFoundException class with the specified error message and the inner exception.</summary>
		/// <param name="message">A string that contains the error message that explains the reason for the exception.</param>
		/// <param name="innerException">The Exception that caused the current exception to be thrown.</param>
		// Token: 0x06001A4F RID: 6735 RVA: 0x0000220F File Offset: 0x0000040F
		public InstanceNotFoundException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
