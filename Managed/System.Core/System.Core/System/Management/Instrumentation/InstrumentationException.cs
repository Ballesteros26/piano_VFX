using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>Represents a provider-related exception.</summary>
	// Token: 0x02000367 RID: 871
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InstrumentationException : InstrumentationBaseException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class. This is the default constructor.</summary>
		// Token: 0x06001A50 RID: 6736 RVA: 0x0000220F File Offset: 0x0000040F
		public InstrumentationException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class with the System.Exception that caused the current exception.</summary>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		// Token: 0x06001A51 RID: 6737 RVA: 0x0000220F File Offset: 0x0000040F
		public InstrumentationException(Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x06001A52 RID: 6738 RVA: 0x0000220F File Offset: 0x0000040F
		protected InstrumentationException(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class with a message that describes the exception.</summary>
		/// <param name="message">Message that describes the exception.</param>
		// Token: 0x06001A53 RID: 6739 RVA: 0x0000220F File Offset: 0x0000040F
		public InstrumentationException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class with the specified string and exception.</summary>
		/// <param name="message">Message that describes the exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		// Token: 0x06001A54 RID: 6740 RVA: 0x0000220F File Offset: 0x0000040F
		public InstrumentationException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
