using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Unity;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DeleteChildAccessRule" /> class represents a specific type of access rule that is used to allow or deny an Active Directory Domain Services object the right to delete child objects.</summary>
	// Token: 0x02000090 RID: 144
	public sealed class DeleteChildAccessRule : ActiveDirectoryAccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteChildAccessRule" /> class with the specified identity reference and access control type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		// Token: 0x06000487 RID: 1159 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteChildAccessRule(IdentityReference identity, AccessControlType type)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteChildAccessRule" /> class with the specified identity reference, access control type, and Active Directory Domain Services security inheritance.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x06000488 RID: 1160 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteChildAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteChildAccessRule" /> class with the specified identity reference, access control type, Active Directory Domain Services security inheritance, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x06000489 RID: 1161 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteChildAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteChildAccessRule" /> class with the specified identity reference, access control type, and child type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="childType">The schema GUID of the type of child objects that can or cannot be deleted. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all child types.</param>
		// Token: 0x0600048A RID: 1162 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteChildAccessRule(IdentityReference identity, AccessControlType type, Guid childType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteChildAccessRule" /> class with the specified identity reference, access control type, child type, and Active Directory Domain Services security inheritance.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="childType">The schema GUID of the type of child objects that can or cannot be deleted. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all child types.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x0600048B RID: 1163 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteChildAccessRule(IdentityReference identity, AccessControlType type, Guid childType, ActiveDirectorySecurityInheritance inheritanceType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteChildAccessRule" /> class with the specified identity reference, access control type, child type, Active Directory Domain Services security inheritance, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="childType">The schema GUID of the type of child objects that can or cannot be deleted. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all child types.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x0600048C RID: 1164 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteChildAccessRule(IdentityReference identity, AccessControlType type, Guid childType, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
