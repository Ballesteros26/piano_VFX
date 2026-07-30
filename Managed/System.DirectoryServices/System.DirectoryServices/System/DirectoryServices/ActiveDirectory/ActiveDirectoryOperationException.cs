using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException" /> class exception is thrown when an underlying directory operation fails.</summary>
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class ActiveDirectoryOperationException : Exception, ISerializable
	{
		/// <summary>Gets the error code for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException" /> object.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that identifies the error.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000208C File Offset: 0x0000028C
		public int ErrorCode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException" /> class with a specified error message, an underlying exception object, and a specified error code.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		/// <param name="errorCode">An error code that identifies the error.</param>
		// Token: 0x060001AF RID: 431 RVA: 0x00004A98 File Offset: 0x00002C98
		public ActiveDirectoryOperationException(string message, Exception inner, int errorCode)
			: base(message, inner)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException" /> class with a specified error message and a specified error code.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="errorCode">An error code that identifies the error.</param>
		// Token: 0x060001B0 RID: 432 RVA: 0x00004A8A File Offset: 0x00002C8A
		public ActiveDirectoryOperationException(string message, int errorCode)
			: base(message)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException" /> class with a specified error message and an underlying exception object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		// Token: 0x060001B1 RID: 433 RVA: 0x00004A98 File Offset: 0x00002C98
		public ActiveDirectoryOperationException(string message, Exception inner)
			: base(message, inner)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x060001B2 RID: 434 RVA: 0x00004A8A File Offset: 0x00002C8A
		public ActiveDirectoryOperationException(string message)
			: base(message)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException" /> class.</summary>
		// Token: 0x060001B3 RID: 435 RVA: 0x00004AA7 File Offset: 0x00002CA7
		public ActiveDirectoryOperationException()
			: base("DSUnknownFailure")
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException" /> class, using the specified serialization information and streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object for the exception.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object for the exception.</param>
		// Token: 0x060001B4 RID: 436 RVA: 0x00004AB9 File Offset: 0x00002CB9
		protected ActiveDirectoryOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds serialized object data about the exception being thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x060001B5 RID: 437 RVA: 0x0000208C File Offset: 0x0000028C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}
	}
}
