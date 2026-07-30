using System;
using System.Security.AccessControl;
using System.Security.Principal;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> class is used to represent an access control entry (ACE) in the discretionary access control list (DACL) of an Active Directory Domain Services object.</summary>
	// Token: 0x0200000D RID: 13
	public class ActiveDirectoryAccessRule : ObjectAccessRule
	{
		/// <summary>Gets the Active Directory Domain Services rights for this access rule.</summary>
		/// <returns>A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration values that specify the Active Directory Domain Services access rights that are included in this rule.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectoryRights ActiveDirectoryRights
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the inheritance type for this access rule.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type for this access rule.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySecurityInheritance InheritanceType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> class with the specified identity reference, Active Directory Domain Services rights, and access rule type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object which identifies the trustee of the access rule.</param>
		/// <param name="adRights">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration values that specifies the rights of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		// Token: 0x06000033 RID: 51 RVA: 0x0000209C File Offset: 0x0000029C
		public ActiveDirectoryAccessRule(IdentityReference identity, ActiveDirectoryRights adRights, AccessControlType type)
			: this(identity, (int)adRights, type, Guid.Empty, false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> class with the specified identity reference, Active Directory Domain Services rights, access rule type, and object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="adRights">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration values that specifies the rights of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="objectType">The schema GUID of the object to which the access rule applies.</param>
		// Token: 0x06000034 RID: 52 RVA: 0x000020C0 File Offset: 0x000002C0
		public ActiveDirectoryAccessRule(IdentityReference identity, ActiveDirectoryRights adRights, AccessControlType type, Guid objectType)
			: this(identity, (int)adRights, type, objectType, false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> class with the specified identity reference, Active Directory Domain Services rights, access rule type, and inheritance type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="adRights">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration values that specifies the rights of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x06000035 RID: 53 RVA: 0x000020E0 File Offset: 0x000002E0
		public ActiveDirectoryAccessRule(IdentityReference identity, ActiveDirectoryRights adRights, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType)
			: this(identity, (int)adRights, type, Guid.Empty, false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> class with the specified identity reference, Active Directory Domain Services rights, access rule type, object type, and inheritance type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="adRights">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration values that specifies the rights of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="objectType">The schema GUID of the object to which the access rule applies.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x06000036 RID: 54 RVA: 0x00002104 File Offset: 0x00000304
		public ActiveDirectoryAccessRule(IdentityReference identity, ActiveDirectoryRights adRights, AccessControlType type, Guid objectType, ActiveDirectorySecurityInheritance inheritanceType)
			: this(identity, (int)adRights, type, objectType, false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> class with the specified identity reference, Active Directory Domain Services rights, access rule type, inheritance type, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object which identifies the trustee of the access rule.</param>
		/// <param name="adRights">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration values that specifies the rights of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x06000037 RID: 55 RVA: 0x00002124 File Offset: 0x00000324
		public ActiveDirectoryAccessRule(IdentityReference identity, ActiveDirectoryRights adRights, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
			: this(identity, (int)adRights, type, Guid.Empty, false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> class with the specified identity reference, Active Directory Domain Services rights, access rule type, object type, inheritance type, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="adRights">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration values that specifies the rights of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="objectType">The schema GUID of the object to which the access rule applies.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x06000038 RID: 56 RVA: 0x00002144 File Offset: 0x00000344
		public ActiveDirectoryAccessRule(IdentityReference identity, ActiveDirectoryRights adRights, AccessControlType type, Guid objectType, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
			: this(identity, (int)adRights, type, objectType, false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002164 File Offset: 0x00000364
		internal ActiveDirectoryAccessRule(IdentityReference identity, int accessMask, AccessControlType type, Guid objectType, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid inheritedObjectType)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, objectType, inheritedObjectType, type)
		{
		}
	}
}
