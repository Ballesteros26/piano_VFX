using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	/// <summary>The exception that is thrown when an operation in isolated storage fails.</summary>
	// Token: 0x020003EA RID: 1002
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with default properties.</summary>
		// Token: 0x06002F13 RID: 12051 RVA: 0x000A84A4 File Offset: 0x000A66A4
		public IsolatedStorageException()
			: base(Locale.GetText("An Isolated storage operation failed."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06002F14 RID: 12052 RVA: 0x00047B84 File Offset: 0x00045D84
		public IsolatedStorageException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002F15 RID: 12053 RVA: 0x00047B8D File Offset: 0x00045D8D
		public IsolatedStorageException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06002F16 RID: 12054 RVA: 0x000325DC File Offset: 0x000307DC
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
