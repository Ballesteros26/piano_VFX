using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents the exception thrown when an event provider publishes invalid data in an event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000393 RID: 915
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogInvalidDataException : EventLogException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInvalidDataException" /> class.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B14 RID: 6932 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogInvalidDataException()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInvalidDataException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001B15 RID: 6933 RVA: 0x0000220F File Offset: 0x0000040F
		protected EventLogInvalidDataException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInvalidDataException" /> class by specifying the error message that describes the current exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B16 RID: 6934 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogInvalidDataException(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInvalidDataException" /> class with an error message and inner exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B17 RID: 6935 RVA: 0x0000220F File Offset: 0x0000040F
		public EventLogInvalidDataException(string message, Exception innerException)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
