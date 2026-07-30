using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents an exception that is thrown when an error occurred while reading, querying, or subscribing to the events in an event log. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039D RID: 925
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogReadingException : EventLogException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReadingException" /> class.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B61 RID: 7009 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogReadingException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReadingException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001B62 RID: 7010 RVA: 0x0000220F File Offset: 0x0000040F
		protected EventLogReadingException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReadingException" /> class by specifying the error message that describes the current exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B63 RID: 7011 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogReadingException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReadingException" /> class with an error message and inner exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B64 RID: 7012 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogReadingException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
