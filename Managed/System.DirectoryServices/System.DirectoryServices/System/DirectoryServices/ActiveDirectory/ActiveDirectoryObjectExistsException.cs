using System;
using System.Runtime.Serialization;
using Unity;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException" /> class exception is thrown when an Active Directory Domain Services object is created and that object already exists in the underlying directory store.</summary>
	// Token: 0x02000093 RID: 147
	[Serializable]
	public class ActiveDirectoryObjectExistsException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException" /> class.</summary>
		// Token: 0x0600049D RID: 1181 RVA: 0x00002644 File Offset: 0x00000844
		public ActiveDirectoryObjectExistsException()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException" /> class, using the specified serialization information and streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object for the exception.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object for the exception.</param>
		// Token: 0x0600049E RID: 1182 RVA: 0x00002644 File Offset: 0x00000844
		protected ActiveDirectoryObjectExistsException(SerializationInfo info, StreamingContext context)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException" /> class, using a specified error message.</summary>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x0600049F RID: 1183 RVA: 0x00002644 File Offset: 0x00000844
		public ActiveDirectoryObjectExistsException(string message)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException" /> class, using a specified error message and an underlying exception object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		// Token: 0x060004A0 RID: 1184 RVA: 0x00002644 File Offset: 0x00000844
		public ActiveDirectoryObjectExistsException(string message, Exception inner)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
