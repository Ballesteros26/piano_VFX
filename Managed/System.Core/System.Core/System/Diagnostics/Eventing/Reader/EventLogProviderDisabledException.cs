using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents the exception that is thrown when a specified event provider name references a disabled event provider. A disabled event provider cannot publish events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000397 RID: 919
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogProviderDisabledException : EventLogException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException" /> class.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B23 RID: 6947 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogProviderDisabledException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001B24 RID: 6948 RVA: 0x0000220F File Offset: 0x0000040F
		protected EventLogProviderDisabledException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException" /> class by specifying the error message that describes the current exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B25 RID: 6949 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogProviderDisabledException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException" /> class with an error message and inner exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B26 RID: 6950 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogProviderDisabledException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
