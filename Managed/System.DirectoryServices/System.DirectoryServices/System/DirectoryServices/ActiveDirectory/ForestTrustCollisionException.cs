using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException" /> class exception is thrown when a trust collision occurs during a trust relationship management request.</summary>
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class ForestTrustCollisionException : ActiveDirectoryOperationException, ISerializable
	{
		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollisionCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision" /> objects that describe the trust relationship collision errors.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollisionCollection" /> that contains one or more <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision" /> objects that describe the trust relationship collision errors.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000208C File Offset: 0x0000028C
		public ForestTrustRelationshipCollisionCollection Collisions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException" /> class with a specified error message, an underlying exception object, and a specified <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollisionCollection" /> object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		/// <param name="collisions">A <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollisionCollection" /> object that contains one or more <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision" /> objects that describe the trust relationship collision errors.</param>
		// Token: 0x060003AC RID: 940 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public ForestTrustCollisionException(string message, Exception inner, ForestTrustRelationshipCollisionCollection collisions)
			: base(message, inner)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException" /> class with a specified error message and an underlying exception object.</summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="inner">An <see cref="T:System.Exception" /> object that contains underlying exception information.</param>
		// Token: 0x060003AD RID: 941 RVA: 0x00004BB6 File Offset: 0x00002DB6
		public ForestTrustCollisionException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x060003AE RID: 942 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public ForestTrustCollisionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException" /> class.</summary>
		// Token: 0x060003AF RID: 943 RVA: 0x00004BC9 File Offset: 0x00002DC9
		public ForestTrustCollisionException()
			: base("ForestTrustCollision")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException" /> class, using the specified serialization information and streaming context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object for the exception.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> for the exception.</param>
		// Token: 0x060003B0 RID: 944 RVA: 0x00004BD6 File Offset: 0x00002DD6
		protected ForestTrustCollisionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data about the exception being thrown. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.  </param>
		// Token: 0x060003B1 RID: 945 RVA: 0x0000208C File Offset: 0x0000028C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}
	}
}
