using System;
using System.Security.AccessControl;
using System.Security.Principal;

namespace System.DirectoryServices
{
	/// <summary>Represents a specific type of access rule that is used to allow or deny an Active Directory object an extended right. Extended rights are special operations that are not covered by the standard set of access rights. An example of an extended right is Send-As, which gives a user the right to send e-mail for another user. For a list of possible extended rights, see the topic Extended Rights in the MSDN Library at http://msdn.microsoft.com/library.  For more information about extended rights, see the topic Control Access Rights, also in the MSDN Library. </summary>
	// Token: 0x02000021 RID: 33
	public sealed class ExtendedRightAccessRule : ActiveDirectoryAccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference and access control type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that  identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		// Token: 0x0600011B RID: 283 RVA: 0x00004064 File Offset: 0x00002264
		public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type)
			: base(identity, 256, type, Guid.Empty, false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, and extended right identifier.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="extendedRightType">The Rights-Guid of the extended right that this access rule applies to. For more information, see the topic Rights-Guid in the MSDN Library at http://msdn.microsoft.com/library.  In the Active Directory schema documentation, this information can be found in the Rights-GUID row on the reference page for each extended right. If this parameter is <see cref="F:System.Guid.Empty" />, the access rule applies to all extended rights. For a list of extended rights, see the topic Extended Rights in the MSDN Library.</param>
		// Token: 0x0600011C RID: 284 RVA: 0x0000408C File Offset: 0x0000228C
		public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, Guid extendedRightType)
			: base(identity, 256, type, extendedRightType, false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, and Active Directory security inheritance.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x0600011D RID: 285 RVA: 0x000040B0 File Offset: 0x000022B0
		public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType)
			: base(identity, 256, type, Guid.Empty, false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, extended right identifier, and Active Directory security inheritance.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="extendedRightType">The Rights-Guid of the extended right that this access rule applies to. For more information, see the topic Rights-Guid in the MSDN Library at http://msdn.microsoft.com/library.  In the Active Directory schema documentation, this information can be found in the Rights-GUID row on the reference page for each extended right. If this parameter is <see cref="F:System.Guid.Empty" />, the access rule applies to all extended rights. For a list of extended rights, see the topic Extended Rights in the MSDN Library.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		// Token: 0x0600011E RID: 286 RVA: 0x000040D8 File Offset: 0x000022D8
		public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, Guid extendedRightType, ActiveDirectorySecurityInheritance inheritanceType)
			: base(identity, 256, type, extendedRightType, false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, Active Directory security inheritance, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x0600011F RID: 287 RVA: 0x000040FC File Offset: 0x000022FC
		public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
			: base(identity, 256, type, Guid.Empty, false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, extended right identifier, Active Directory security inheritance, and inherited object type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
		/// <param name="extendedRightType">The Rights-Guid attribute of the extended right that this access rule applies to. For more information, see the topic Rights-Guid in the MSDN Library at http://msdn.microsoft.com/library. In the Active Directory schema documentation, this information can be found in the Rights-GUID row on the reference page for each extended right. If this parameter is <see cref="F:System.Guid.Empty" />, the access rule applies to all extended rights. For a list of extended rights, see the topic Extended Rights in the MSDN Library.</param>
		/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
		/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
		// Token: 0x06000120 RID: 288 RVA: 0x00004120 File Offset: 0x00002320
		public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, Guid extendedRightType, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
			: base(identity, 256, type, extendedRightType, false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
		{
		}
	}
}
