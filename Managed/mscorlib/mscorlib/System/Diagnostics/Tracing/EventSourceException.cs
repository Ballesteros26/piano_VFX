using System;
using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
	/// <summary>The exception that is thrown when an error occurs during event tracing for Windows (ETW).</summary>
	// Token: 0x02000B18 RID: 2840
	[Serializable]
	public class EventSourceException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class.</summary>
		// Token: 0x060065EA RID: 26090 RVA: 0x0014FC80 File Offset: 0x0014DE80
		public EventSourceException()
			: base(Environment.GetResourceString("An error occurred when writing to a listener."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060065EB RID: 26091 RVA: 0x00047B84 File Offset: 0x00045D84
		public EventSourceException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception, or null if no inner exception is specified. </param>
		// Token: 0x060065EC RID: 26092 RVA: 0x00047B8D File Offset: 0x00045D8D
		public EventSourceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060065ED RID: 26093 RVA: 0x000325DC File Offset: 0x000307DC
		protected EventSourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x0014FC92 File Offset: 0x0014DE92
		internal EventSourceException(Exception innerException)
			: base(Environment.GetResourceString("An error occurred when writing to a listener."), innerException)
		{
		}
	}
}
