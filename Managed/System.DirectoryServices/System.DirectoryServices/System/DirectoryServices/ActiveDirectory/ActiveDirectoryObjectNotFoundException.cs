using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class exception is thrown when a requested object is not found in the underlying directory store.</summary>
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class ActiveDirectoryObjectNotFoundException : Exception, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class with a specified error message and information about the requested object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="type">A <see cref="T:System.Type" /> object that describes the type of the requested object.</param>
		/// <param name="name">A <see cref="T:System.String" /> that contains the name of the requested object.</param>
		// Token: 0x060001A6 RID: 422 RVA: 0x00004A8A File Offset: 0x00002C8A
		[MonoTODO]
		public ActiveDirectoryObjectNotFoundException(string message, Type type, string name)
			: base(message)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class with a specified error message and an underlying exception object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		// Token: 0x060001A7 RID: 423 RVA: 0x00004A98 File Offset: 0x00002C98
		[MonoTODO]
		public ActiveDirectoryObjectNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x060001A8 RID: 424 RVA: 0x00004A8A File Offset: 0x00002C8A
		[MonoTODO]
		public ActiveDirectoryObjectNotFoundException(string message)
			: base(message)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class.</summary>
		// Token: 0x060001A9 RID: 425 RVA: 0x00004AA7 File Offset: 0x00002CA7
		[MonoTODO]
		public ActiveDirectoryObjectNotFoundException()
			: base("DSUnknownFailure")
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> class, using the specified serialization information and streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object for the exception.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> for the exception.</param>
		// Token: 0x060001AA RID: 426 RVA: 0x00004AB9 File Offset: 0x00002CB9
		[MonoTODO]
		protected ActiveDirectoryObjectNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds serialized object data about the exception that is being thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x060001AB RID: 427 RVA: 0x0000208C File Offset: 0x0000028C
		[MonoTODO]
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the name of the requested object.</summary>
		/// <returns>A string that contains the name of the requested object.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the type of the requested object.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of the requested object.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000208C File Offset: 0x0000028C
		public Type Type
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
