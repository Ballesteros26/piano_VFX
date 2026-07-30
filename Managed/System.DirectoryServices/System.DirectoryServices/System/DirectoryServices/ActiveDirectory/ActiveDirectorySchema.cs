using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> class represents the schema partition for a particular domain.</summary>
	// Token: 0x0200003A RID: 58
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ActiveDirectorySchema : ActiveDirectoryPartition
	{
		/// <summary>Gets the schema master role owner.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object that represents the server that is the schema master.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryServer SchemaRoleOwner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases the managed resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> object and, optionally, releases unmanaged resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060001D0 RID: 464 RVA: 0x00004060 File Offset: 0x00002260
		protected override void Dispose(bool disposing)
		{
		}

		/// <summary>Retrieves the schema object for the specified context.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> object that represents the schema for the specified context.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object. The target of the context must be a forest, directory server, or configuration set.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the target specified in <paramref name="context" /> cannot be made.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> is invalid.</exception>
		// Token: 0x060001D1 RID: 465 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySchema GetSchema(DirectoryContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the schema cache on the client computer.</summary>
		// Token: 0x060001D2 RID: 466 RVA: 0x0000208C File Offset: 0x0000028C
		public void RefreshSchema()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the class with the specified name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object that represents the class with the specified name.</returns>
		/// <param name="ldapDisplayName">The LDAP display name of the class to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A class with the specified name cannot be found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ldapDisplayName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ldapDisplayName" /> is zero length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001D3 RID: 467 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaClass FindClass(string ldapDisplayName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the defunct class that has the specified common name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object that represents the class with the specified common name.</returns>
		/// <param name="commonName">The common name of the defunct class to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A class with the specified name cannot be found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="commonName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="commonName" /> is zero length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001D4 RID: 468 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaClass FindDefunctClass(string commonName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves all Active Directory Domain Services classes in the schema.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects for the classes that were retrieved.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001D5 RID: 469 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyActiveDirectorySchemaClassCollection FindAllClasses()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves all Active Directory Domain Services classes in the schema that are of the specified type.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects for the classes that were retrieved.</returns>
		/// <param name="type">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.SchemaClassType" /> members that identifies which type of classes to retrieve.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001D6 RID: 470 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyActiveDirectorySchemaClassCollection FindAllClasses(SchemaClassType type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves all of the defunct Active Directory Domain Services classes in the schema.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects for the classes that were retrieved.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001D7 RID: 471 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyActiveDirectorySchemaClassCollection FindAllDefunctClasses()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the property with the specified name.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object that represents the property with the specified name.</returns>
		/// <param name="ldapDisplayName">The LDAP display name of the property to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A property with the specified name cannot be found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyName" /> is zero length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001D8 RID: 472 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaProperty FindProperty(string ldapDisplayName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the defunct property that has the specified common name.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object that represents the property.</returns>
		/// <param name="commonName">The common name of the defunct property to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A property with the specified name cannot be found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="commonName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="commonName" /> is zero length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001D9 RID: 473 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchemaProperty FindDefunctProperty(string commonName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves all of the Active Directory Domain Services properties in the schema.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects for the properties that were retrieved.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001DA RID: 474 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllProperties()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves all of the Active Directory Domain Services properties in the schema of the specified type.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects for the properties that were retrieved.</returns>
		/// <param name="type">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.PropertyTypes" /> members that identifies which type of properties to retrieve.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001DB RID: 475 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllProperties(PropertyTypes type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves all of the defunct Active Directory Domain Services properties in the schema.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects for the properties that were retrieved.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001DC RID: 476 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllDefunctProperties()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory partition.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x060001DD RID: 477 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the schema object for the forest that the currently logged-on user is a member of.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> object that represents the schema for the domain that the local computer is a member of.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the local domain cannot be made.</exception>
		// Token: 0x060001DE RID: 478 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySchema GetCurrentSchema()
		{
			throw new NotImplementedException();
		}
	}
}
