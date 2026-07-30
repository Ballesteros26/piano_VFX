using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown when the server or the client cannot be reached for a previously specified period of time.</summary>
	// Token: 0x0200075C RID: 1884
	[ComVisible(true)]
	[Serializable]
	public class RemotingTimeoutException : RemotingException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with default properties.</summary>
		// Token: 0x06004DFF RID: 19967 RVA: 0x0011A181 File Offset: 0x00118381
		public RemotingTimeoutException()
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason why the exception occurred. </param>
		// Token: 0x06004E00 RID: 19968 RVA: 0x0011A189 File Offset: 0x00118389
		public RemotingTimeoutException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06004E01 RID: 19969 RVA: 0x0011A192 File Offset: 0x00118392
		public RemotingTimeoutException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x0011A19C File Offset: 0x0011839C
		internal RemotingTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
