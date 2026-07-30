using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> class represents a schema class definition that is contained in the schema partition.</summary>
	// Token: 0x0200003B RID: 59
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ActiveDirectorySchemaClass : IDisposable
	{
		/// <summary>Gets the ldapDisplayName of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object. For more information, see the topic LDAP-Display-Name in the MSDN Library at http://msdn.microsoft.com.  </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the ldapDisplayName of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object. For more information, see the topic LDAP-Display-Name in the MSDN Library at http://msdn.microsoft.com.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the Common Name (CN) of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the CN of the current class object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000208C File Offset: 0x0000028C
		public string CommonName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the OID for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the OID of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000208C File Offset: 0x0000028C
		public string Oid
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a description of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a description of the current class object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000208C File Offset: 0x0000028C
		public string Description
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object is defunct.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates if the current schema class object is defunct. true if the object is defunct; otherwise, false.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsDefunct
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects that can contain this class.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects that can contain this class.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaClassCollection PossibleSuperiors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> class can contain.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects that this class can contain.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyActiveDirectorySchemaClassCollection PossibleInferiors
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaPropertyCollection" /> object that contains the properties that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> must contain.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaPropertyCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects that represent the properties that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object must contain.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaPropertyCollection MandatoryProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaPropertyCollection" /> object that contains the properties that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> can contain.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaPropertyCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object can contain.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaPropertyCollection OptionalProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> object that contains the auxiliary classes of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClassCollection" /> object that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects that represent the auxiliary classes of the current schema class object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaClassCollection AuxiliaryClasses
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the schema class from which the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object is derived.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object that contains the schema class from which the current schema class is derived. null if no schema class was specified during the creation of the current class.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaClass SubClassOf
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.DirectoryServices.ActiveDirectory.SchemaClassType" /> type for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.SchemaClassType" /> value that contains the type of the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified type is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.SchemaClassType" /> value (applies to set only).</exception>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000208C File Offset: 0x0000028C
		public SchemaClassType Type
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the schemaIDGUID for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object. For more information, see the topic "schemaIDGUID" or "Schema-ID-GUID" in the MSDN Library at http://msdn.microsoft.com/library.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object that contains the schemaIDGUID for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object. For more information, see the topic "schemaIDGUID" or "Schema-ID-GUID" in the MSDN Library at http://msdn.microsoft.com/library.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000208C File Offset: 0x0000028C
		public Guid SchemaGuid
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the default <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> descriptor of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object that represents the default security descriptor for the current schema class object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySecurity DefaultObjectSecurityDescriptor
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</param>
		/// <param name="ldapDisplayName">A <see cref="T:System.String" /> that represents the LDAP display name for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> does not refer to a valid <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" />, <paramref name="ldapDisplayName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="ldapDisplayName" /> is null.</exception>
		// Token: 0x060001F6 RID: 502 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public ActiveDirectorySchemaClass(DirectoryContext context, string ldapDisplayName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		// Token: 0x060001F7 RID: 503 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
		}

		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object and, optionally, releases unmanaged resources.</summary>
		/// <param name="disposing">true if the managed resources should be released; false if only the unmanaged resources should be released.</param>
		// Token: 0x060001F8 RID: 504 RVA: 0x00004060 File Offset: 0x00002260
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object in the Active Directory Domain Services schema partition that matches a given directory context and name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object for the schema class that was found. Returns null if the class was not found.</returns>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for the search.</param>
		/// <param name="ldapDisplayName">A <see cref="T:System.String" /> that specifies the LDAP display name of the schema class for the search.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The object was not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> does not refer to a valid <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" />, <paramref name="ldapDisplayName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="ldapDisplayName" /> is null.</exception>
		// Token: 0x060001F9 RID: 505 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySchemaClass FindByName(DirectoryContext context, string ldapDisplayName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> collection that contains all properties, including ancestor properties, of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> collection of the properties of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" />, including ancestor properties.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x060001FA RID: 506 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyActiveDirectorySchemaPropertyCollection GetAllProperties()
		{
			throw new NotImplementedException();
		}

		/// <summary>Commits all changes to the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object to the underlying directory store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The context is an AD LDS configuration set and the AD LDS instance could not be found.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		// Token: 0x060001FB RID: 507 RVA: 0x0000208C File Offset: 0x0000028C
		public void Save()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the LDAP display name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the LDAP display name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</returns>
		// Token: 0x060001FC RID: 508 RVA: 0x0000208C File Offset: 0x0000028C
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory entry for the schema class.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object is not a valid instance.</exception>
		// Token: 0x060001FD RID: 509 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}
	}
}
