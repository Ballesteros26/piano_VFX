using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Unity;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.CreateChildAccessRule" /> class represents a specific type of access rule that is used to allow or deny an Active Directory Domain Services object the right to create child objects.</summary>
	// Token: 0x0200008F RID: 143
	public sealed class CreateChildAccessRule : ActiveDirectoryAccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.CreateChildAccessRule" /> class with the specified identity reference and access control type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		// Token: 0x06000481 RID: 1153 RVA: 0x00002644 File Offset: 0x00000844
		public CreateChildAccessRule(IdentityReference identity, AccessControlType type)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.CreateChildAccessRule" /> class with the specified identity reference, access control type, and Active Directory Domain Services security inheritance information.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x06000482 RID: 1154 RVA: 0x00002644 File Offset: 0x00000844
		public CreateChildAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.CreateChildAccessRule" /> class with the specified identity reference, access control type, Active Directory Domain Services security inheritance information, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x06000483 RID: 1155 RVA: 0x00002644 File Offset: 0x00000844
		public CreateChildAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.CreateChildAccessRule" /> class with the specified identity reference, access control type, and child object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="childType">The schema GUID of the type of child objects that can or cannot be created. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all child types.</param>
		// Token: 0x06000484 RID: 1156 RVA: 0x00002644 File Offset: 0x00000844
		public CreateChildAccessRule(IdentityReference identity, AccessControlType type, Guid childType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.CreateChildAccessRule" /> class with the specified identity reference, access control type, child object type, and Active Directory Domain Services security inheritance information.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="childType">The schema GUID of the type of child objects that can or cannot be created. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all child types.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x06000485 RID: 1157 RVA: 0x00002644 File Offset: 0x00000844
		public CreateChildAccessRule(IdentityReference identity, AccessControlType type, Guid childType, ActiveDirectorySecurityInheritance inheritanceType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.CreateChildAccessRule" /> class with the specified identity reference, access control type, child object type, Active Directory Domain Services security inheritance information, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="childType">The schema GUID of the type of child objects that can or cannot be created. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all child types.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x06000486 RID: 1158 RVA: 0x00002644 File Offset: 0x00000844
		public CreateChildAccessRule(IdentityReference identity, AccessControlType type, Guid childType, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
