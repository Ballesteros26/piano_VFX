using System;
using System.Security.Permissions;
using Unity;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> class represents a schema property definition that is contained in the schema partition.</summary>
	// Token: 0x0200003D RID: 61
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ActiveDirectorySchemaProperty : IDisposable
	{
		/// <summary>Gets the ldapDisplayName of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object. For more information, see the topic LDAP-Display-Name in the MSDN Library at http://msdn.microsoft.com/library. </summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the ldapDisplayName of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object. For more information, see the topic LDAP-Display-Name in the MSDN Library at http://msdn.microsoft.com/library.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the Common Name (CN) of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that gets or sets the CN of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000208C File Offset: 0x0000028C
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

		/// <summary>Gets or sets the OID of the schema property.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the OID of the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000208C File Offset: 0x0000028C
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

		/// <summary>Gets or sets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySyntax" /> object indicating the property type (syntax) of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySyntax" /> object that defines the property type of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified type is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySyntax" /> value (applies to set only).</exception>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySyntax Syntax
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

		/// <summary>Gets or sets a description of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that gets or sets a description of the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000208C File Offset: 0x0000028C
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

		/// <summary>Gets or sets a value indicating whether the schema property is single-valued.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the schema property is single valued. true if it is single-valued; otherwise, false.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsSingleValued
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

		/// <summary>Gets or sets a value indicating whether the schema property is indexed in the Active Directory Domain Services store.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether  the current schema property is indexed in the Active Directory Domain Services store. true if the property is indexed; otherwise, false.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsIndexed
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

		/// <summary>Gets or sets a value indicating whether the schema property is indexed in all containers.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the schema property is indexed in all containers.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsIndexedOverContainer
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

		/// <summary>Gets or sets a value indicating whether the schema property is in the ANR set.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the schema property is in the ANR set. true if it is in the ANR set; otherwise, false.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsInAnr
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

		/// <summary>Gets or sets a value indicating whether the schema property is in the tombstone object that contains deleted properties.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the schema property is contained in the tombstone object. true if it is contained in the tombstone object; otherwise, false. </returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsOnTombstonedObject
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

		/// <summary>Gets or sets a value indicating whether there is a tuple index for this schema property.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the schema property has a tuple index. true if there is a tuple index for the property; otherwise, false.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsTupleIndexed
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

		/// <summary>Gets or sets a value indicating whether the schema property is contained in the global catalog.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the schema property is contained in the global catalog. true if it is contained in the global catalog; otherwise, false. </returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsInGlobalCatalog
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

		/// <summary>Gets or sets a value that represents the minimum value or length that the schema property can have.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that represents the minimum value or length of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object value.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">There is no lower range for this property.</exception>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000208C File Offset: 0x0000028C
		public int RangeLower
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

		/// <summary>Gets or sets a value that represents the maximum value or length that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object can have.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that indicates the maximum value or length of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object value.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">There is no upper range for this property.</exception>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000208C File Offset: 0x0000028C
		public int RangeUpper
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object is defunct.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether  the current schema property object is defunct. true if the object is defunct; otherwise, false.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000208C File Offset: 0x0000028C
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

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> that links to the current schema property.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object that is linked to the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaProperty Link
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the value for the link identifier when the schema property is linked.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that represents the linkID value when the schema property is linked. For more information, see the topic Link-ID in the MSDN Library at http://msdn.microsoft.com/library. </returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000208C File Offset: 0x0000028C
		public int? LinkId
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

		/// <summary>Gets or sets the schemaIDGuid for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object. For more information, see the topic Schema-ID-GUID in the MSDN Library at http://msdn.microsoft.com/library. </summary>
		/// <returns>A <see cref="T:System.Guid" /> that represents the schemaIDGuid for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object. For more information, see the topic Schema-ID-GUID in the MSDN Library at http://msdn.microsoft.com/library.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000208C File Offset: 0x0000028C
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

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> class.</summary>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</param>
		/// <param name="ldapDisplayName">A <see cref="T:System.String" /> that represents the LDAP display name for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> does not refer to a valid <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" />, or <paramref name="ldapDisplayName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="ldapDisplayName" /> is null.</exception>
		// Token: 0x06000231 RID: 561 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public ActiveDirectorySchemaProperty(DirectoryContext context, string ldapDisplayName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
		// Token: 0x06000232 RID: 562 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
		}

		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object and, optionally, releases unmanaged resources.</summary>
		/// <param name="disposing">true if the managed resources should be released; false if only the unmanaged resources should be released.</param>
		// Token: 0x06000233 RID: 563 RVA: 0x00004060 File Offset: 0x00002260
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object in the Active Directory Domain Services schema partition that matches a given directory context and name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object for the schema property that is found. null if the property is not found.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for the search.</param>
		/// <param name="ldapDisplayName">A <see cref="T:System.String" /> that specifies the LDAP display name of the schema property to search for.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The object was not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> does not refer to a valid <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" />, or <paramref name="ldapDisplayName" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="ldapDisplayName" /> is null.</exception>
		// Token: 0x06000234 RID: 564 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySchemaProperty FindByName(DirectoryContext context, string ldapDisplayName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Commits all changes to the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object to the underlying directory store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object of the same name already exists.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x06000235 RID: 565 RVA: 0x0000208C File Offset: 0x0000028C
		public void Save()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the LDAP display name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the LDAP display name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</returns>
		// Token: 0x06000236 RID: 566 RVA: 0x0000208C File Offset: 0x0000028C
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory entry for the schema property.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object is not a valid instance.</exception>
		// Token: 0x06000237 RID: 567 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00002644 File Offset: 0x00000844
		public void set_RangeLower(int? value)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00002644 File Offset: 0x00000844
		public void set_RangeUpper(int? value)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
