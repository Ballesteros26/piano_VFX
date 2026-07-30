using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>Represents the base provider-related exception.</summary>
	// Token: 0x02000368 RID: 872
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InstrumentationBaseException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationBaseException" />. class. This is the default constructor.</summary>
		// Token: 0x06001A55 RID: 6741 RVA: 0x0000220F File Offset: 0x0000040F
		public InstrumentationBaseException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationBaseException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x06001A56 RID: 6742 RVA: 0x0000220F File Offset: 0x0000040F
		protected InstrumentationBaseException(SerializationInfo info, StreamingContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationBaseException" /> class with a message that describes the exception.</summary>
		/// <param name="message">Message that describes the exception.</param>
		// Token: 0x06001A57 RID: 6743 RVA: 0x0000220F File Offset: 0x0000040F
		public InstrumentationBaseException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Management.Instrumentation.InstrumentationBaseException" /> class with the specified string and exception.</summary>
		/// <param name="message">Message that describes the exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		// Token: 0x06001A58 RID: 6744 RVA: 0x0000220F File Offset: 0x0000040F
		public InstrumentationBaseException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
