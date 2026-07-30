using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> exception is thrown when the request to synchronize from all servers fails. </summary>
	// Token: 0x02000083 RID: 131
	[Serializable]
	public class SyncFromAllServersOperationException : ActiveDirectoryOperationException, ISerializable
	{
		/// <summary>Gets an array of <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersErrorInformation" /> objects that describe the errors.</summary>
		/// <returns>An array of one or more <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersErrorInformation" /> objects that describe the errors.</returns>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000208C File Offset: 0x0000028C
		public SyncFromAllServersErrorInformation[] ErrorInformation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> class with a specified error message, an underlying exception object, and a specified <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersErrorInformation" /> object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		/// <param name="errors">An array of one or more <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersErrorInformation" /> objects that describe the errors.</param>
		// Token: 0x06000464 RID: 1124 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public SyncFromAllServersOperationException(string message, Exception inner, SyncFromAllServersErrorInformation[] errors)
			: base(message, inner)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> class with a specified error message and an underlying exception object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		// Token: 0x06000465 RID: 1125 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public SyncFromAllServersOperationException(string message, Exception inner)
			: base(message, inner)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x06000466 RID: 1126 RVA: 0x00004C28 File Offset: 0x00002E28
		public SyncFromAllServersOperationException(string message)
			: base(message)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> class.</summary>
		// Token: 0x06000467 RID: 1127 RVA: 0x00004C36 File Offset: 0x00002E36
		public SyncFromAllServersOperationException()
			: base("DSSyncAllFailure")
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" /> class, using the specified serialization information and streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object for the exception.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object for the exception.</param>
		// Token: 0x06000468 RID: 1128 RVA: 0x00004C48 File Offset: 0x00002E48
		protected SyncFromAllServersOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds serialized object data about the exception being thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06000469 RID: 1129 RVA: 0x0000208C File Offset: 0x0000028C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}
	}
}
