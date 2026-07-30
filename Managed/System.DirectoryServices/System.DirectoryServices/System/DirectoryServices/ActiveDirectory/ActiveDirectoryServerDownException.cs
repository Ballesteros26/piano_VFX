using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class exception is thrown when a server is unavailable to respond to a service request.</summary>
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class ActiveDirectoryServerDownException : Exception, ISerializable
	{
		/// <summary>Gets the error code for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> object.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that identifies the error.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000208C File Offset: 0x0000028C
		public int ErrorCode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the server that is associated with the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the server that caused this error.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the message that describes the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> object error.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the error.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000208C File Offset: 0x0000028C
		public override string Message
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class with a specified error message, an underlying exception object, a specified error code, and a specified server name.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		/// <param name="errorCode">An error code that identifies the error.</param>
		/// <param name="name">The name of the server that caused the error.</param>
		// Token: 0x0600024E RID: 590 RVA: 0x00004AF2 File Offset: 0x00002CF2
		public ActiveDirectoryServerDownException(string message, Exception inner, int errorCode, string name)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class with a specified error message, a specified error code, and a specified server name.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="errorCode">An error code that identifies the error.</param>
		/// <param name="name">The name of the server that caused the error.</param>
		// Token: 0x0600024F RID: 591 RVA: 0x00004AFC File Offset: 0x00002CFC
		public ActiveDirectoryServerDownException(string message, int errorCode, string name)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class with a specified error message and an underlying exception object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		// Token: 0x06000250 RID: 592 RVA: 0x00004AF2 File Offset: 0x00002CF2
		public ActiveDirectoryServerDownException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x06000251 RID: 593 RVA: 0x00004AFC File Offset: 0x00002CFC
		public ActiveDirectoryServerDownException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class.</summary>
		// Token: 0x06000252 RID: 594 RVA: 0x00004B05 File Offset: 0x00002D05
		public ActiveDirectoryServerDownException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException" /> class, using the specified serialization information and streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object for the exception.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> for the exception.</param>
		// Token: 0x06000253 RID: 595 RVA: 0x00004B0D File Offset: 0x00002D0D
		protected ActiveDirectoryServerDownException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds serialized object data about the exception that is being thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06000254 RID: 596 RVA: 0x0000208C File Offset: 0x0000028C
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}
	}
}
