using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown to communicate errors to the client when the client connects to non-.NET Framework applications that cannot throw exceptions.</summary>
	// Token: 0x0200075D RID: 1885
	[ComVisible(true)]
	[Serializable]
	public class ServerException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with default properties.</summary>
		// Token: 0x06004E03 RID: 19971 RVA: 0x000D9764 File Offset: 0x000D7964
		public ServerException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with a specified message.</summary>
		/// <param name="message">The message that describes the exception </param>
		// Token: 0x06004E04 RID: 19972 RVA: 0x000C7E43 File Offset: 0x000C6043
		public ServerException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06004E05 RID: 19973 RVA: 0x000C7E4C File Offset: 0x000C604C
		public ServerException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal ServerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
