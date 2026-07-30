using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
	/// <summary>Represents an exception for a principal whose identity could not be mapped to a known identity.</summary>
	// Token: 0x02000622 RID: 1570
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class.</summary>
		// Token: 0x0600443E RID: 17470 RVA: 0x000EFE73 File Offset: 0x000EE073
		public IdentityNotMappedException()
			: base(Locale.GetText("Couldn't translate some identities."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600443F RID: 17471 RVA: 0x000C7E43 File Offset: 0x000C6043
		public IdentityNotMappedException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityNotMappedException" /> class by using the specified error message and inner exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If <paramref name="inner" /> is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06004440 RID: 17472 RVA: 0x000C7E4C File Offset: 0x000C604C
		public IdentityNotMappedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Represents the collection of unmapped identities for an <see cref="T:System.Security.Principal.IdentityNotMappedException" /> exception.</summary>
		/// <returns>The collection of unmapped identities.</returns>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x000EFE85 File Offset: 0x000EE085
		public IdentityReferenceCollection UnmappedIdentities
		{
			get
			{
				if (this._coll == null)
				{
					this._coll = new IdentityReferenceCollection();
				}
				return this._coll;
			}
		}

		/// <summary>Gets serialization information with the data needed to create an instance of this <see cref="T:System.Security.Principal.IdentityNotMappedException" /> object. </summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization." /><see cref="SerializationInfoobject" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.SerializationInfo." /><see cref="StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06004442 RID: 17474 RVA: 0x00002194 File Offset: 0x00000394
		[MonoTODO("not implemented")]
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		// Token: 0x04002269 RID: 8809
		private IdentityReferenceCollection _coll;
	}
}
