using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Unity;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DeleteTreeAccessRule" /> class represents a specific type of access rule that is used to allow or deny an Active Directory Domain Services object the right to delete all child objects, regardless of the permissions that the child objects have. </summary>
	// Token: 0x02000091 RID: 145
	public sealed class DeleteTreeAccessRule : ActiveDirectoryAccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteTreeAccessRule" /> class with the specified identity reference and access control type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		// Token: 0x0600048D RID: 1165 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteTreeAccessRule(IdentityReference identity, AccessControlType type)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteTreeAccessRule" /> class with the specified identity reference, access control type, and Active Directory Domain Services security inheritance.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x0600048E RID: 1166 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteTreeAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DeleteTreeAccessRule" /> class with the specified identity reference, access control type, Active Directory Domain Services security inheritance, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x0600048F RID: 1167 RVA: 0x00002644 File Offset: 0x00000844
		public DeleteTreeAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
