using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown when something has gone wrong during remoting.</summary>
	// Token: 0x02000759 RID: 1881
	[ComVisible(true)]
	[Serializable]
	public class RemotingException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingException" /> class with default properties.</summary>
		// Token: 0x06004DBD RID: 19901 RVA: 0x000D9764 File Offset: 0x000D7964
		public RemotingException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingException" /> class with a specified message.</summary>
		/// <param name="message">The error message that explains why the exception occurred. </param>
		// Token: 0x06004DBE RID: 19902 RVA: 0x000C7E43 File Offset: 0x000C6043
		public RemotingException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		// Token: 0x06004DBF RID: 19903 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected RemotingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains why the exception occurred. </param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06004DC0 RID: 19904 RVA: 0x000C7E4C File Offset: 0x000C604C
		public RemotingException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}
	}
}
