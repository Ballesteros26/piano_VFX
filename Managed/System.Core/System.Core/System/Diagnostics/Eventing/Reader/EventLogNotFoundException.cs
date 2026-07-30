using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents the exception that is thrown when a requested event log (usually specified by the name of the event log or the path to the event log file) does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000395 RID: 917
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogNotFoundException : EventLogException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogNotFoundException" /> class.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B1C RID: 6940 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogNotFoundException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogNotFoundException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001B1D RID: 6941 RVA: 0x0000220F File Offset: 0x0000040F
		protected EventLogNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogNotFoundException" /> class by specifying the error message that describes the current exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B1E RID: 6942 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogNotFoundException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogNotFoundException" /> class with an error message and inner exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B1F RID: 6943 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogNotFoundException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
