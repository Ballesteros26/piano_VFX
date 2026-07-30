using System;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> method calls a method that throws an exception.</summary>
	// Token: 0x02000625 RID: 1573
	[Serializable]
	public class PingException : InvalidOperationException
	{
		// Token: 0x06003217 RID: 12823 RVA: 0x0007FA2E File Offset: 0x0007DC2E
		internal PingException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class with serialized data. </summary>
		/// <param name="serializationInfo">The object that holds the serialized object data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the contextual information about the source or destination for this serialization.</param>
		// Token: 0x06003218 RID: 12824 RVA: 0x0007FA3F File Offset: 0x0007DC3F
		protected PingException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class using the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06003219 RID: 12825 RVA: 0x0007FA36 File Offset: 0x0007DC36
		public PingException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" />  class using the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		/// <param name="innerException">The exception that causes the current exception.</param>
		// Token: 0x0600321A RID: 12826 RVA: 0x000BE4E3 File Offset: 0x000BC6E3
		public PingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
