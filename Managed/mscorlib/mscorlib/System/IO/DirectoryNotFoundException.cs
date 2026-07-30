using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when part of a file or directory cannot be found.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200038F RID: 911
	[ComVisible(true)]
	[Serializable]
	public class DirectoryNotFoundException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryNotFoundException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_DIRECTORYNOTFOUND.</summary>
		// Token: 0x06002A79 RID: 10873 RVA: 0x00097FA7 File Offset: 0x000961A7
		public DirectoryNotFoundException()
			: base(Environment.GetResourceString("Attempted to access a path that is not on the disk."))
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryNotFoundException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_DIRECTORYNOTFOUND.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06002A7A RID: 10874 RVA: 0x00097FC4 File Offset: 0x000961C4
		public DirectoryNotFoundException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002A7B RID: 10875 RVA: 0x00097FD8 File Offset: 0x000961D8
		public DirectoryNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024893);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryNotFoundException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06002A7C RID: 10876 RVA: 0x00097FED File Offset: 0x000961ED
		protected DirectoryNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
