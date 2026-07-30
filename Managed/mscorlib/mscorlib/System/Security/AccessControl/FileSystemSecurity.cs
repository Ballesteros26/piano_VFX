using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Represents the access control and audit security for a file or directory.</summary>
	// Token: 0x020005EE RID: 1518
	public abstract class FileSystemSecurity : NativeObjectSecurity
	{
		// Token: 0x06004277 RID: 17015 RVA: 0x000EAF4F File Offset: 0x000E914F
		internal FileSystemSecurity(bool isContainer)
			: base(isContainer, ResourceType.FileObject)
		{
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000EAF59 File Offset: 0x000E9159
		internal FileSystemSecurity(bool isContainer, string name, AccessControlSections includeSections)
			: base(isContainer, ResourceType.FileObject, name, includeSections)
		{
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x000EAF65 File Offset: 0x000E9165
		internal FileSystemSecurity(bool isContainer, SafeHandle handle, AccessControlSections includeSections)
			: base(isContainer, ResourceType.FileObject, handle, includeSections)
		{
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemRights" /> enumeration.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x000EAF71 File Offset: 0x000E9171
		public override Type AccessRightType
		{
			get
			{
				return typeof(FileSystemRights);
			}
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class.</returns>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x000EAF7D File Offset: 0x000E917D
		public override Type AccessRuleType
		{
			get
			{
				return typeof(FileSystemAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class.</returns>
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x000EAF89 File Offset: 0x000E9189
		public override Type AuditRuleType
		{
			get
			{
				return typeof(FileSystemAuditRule);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class that represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <returns>A new <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object that represents a user account.</param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">true if the access rule is inherited; otherwise, false.    </param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether access is allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> parameters specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> parameter is null. -or-The <paramref name="accessMask" /> parameter is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> parameter is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600427D RID: 17021 RVA: 0x000EAF95 File Offset: 0x000E9195
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new FileSystemAccessRule(identityReference, (FileSystemRights)accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Adds the specified access control list (ACL) permission to the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to add to a file or directory. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x0600427E RID: 17022 RVA: 0x000EA4DF File Offset: 0x000E86DF
		public void AddAccessRule(FileSystemAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Removes all matching allow or deny access control list (ACL) permissions from the current file or directory.</summary>
		/// <returns>true if the access rule was removed; otherwise, false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to remove from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x0600427F RID: 17023 RVA: 0x000EA4E8 File Offset: 0x000E86E8
		public bool RemoveAccessRule(FileSystemAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Removes all access control list (ACL) permissions for the specified user from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that specifies a user whose access control list (ACL) permissions should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004280 RID: 17024 RVA: 0x000EA4F1 File Offset: 0x000E86F1
		public void RemoveAccessRuleAll(FileSystemAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Removes a single matching allow or deny access control list (ACL) permission from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that specifies a user whose access control list (ACL) permissions should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004281 RID: 17025 RVA: 0x000EA4FA File Offset: 0x000E86FA
		public void RemoveAccessRuleSpecific(FileSystemAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Adds the specified access control list (ACL) permission to the current file or directory and removes all matching ACL permissions.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004282 RID: 17026 RVA: 0x000EA503 File Offset: 0x000E8703
		public void ResetAccessRule(FileSystemAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Sets the specified access control list (ACL) permission for the current file or directory. </summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to set for a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004283 RID: 17027 RVA: 0x000EA50C File Offset: 0x000E870C
		public void SetAccessRule(FileSystemAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class representing the specified audit rule for the specified user.</summary>
		/// <returns>A new <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object representing the specified audit rule for the specified user.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object that represents a user account.</param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">true if the access rule is inherited; otherwise, false.    </param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies the type of auditing to perform.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> properties specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> property is null. -or-The <paramref name="accessMask" /> property is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> property is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06004284 RID: 17028 RVA: 0x000EAFA5 File Offset: 0x000E91A5
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new FileSystemAuditRule(identityReference, (FileSystemRights)accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		/// <summary>Adds the specified audit rule to the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  object that represents an audit rule to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004285 RID: 17029 RVA: 0x000EA520 File Offset: 0x000E8720
		public void AddAuditRule(FileSystemAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Removes all matching allow or deny audit rules from the current file or directory.</summary>
		/// <returns>true if the audit rule was removed; otherwise, false</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  object that represents an audit rule to remove from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004286 RID: 17030 RVA: 0x000EA529 File Offset: 0x000E8729
		public bool RemoveAuditRule(FileSystemAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Removes all audit rules for the specified user from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that specifies a user whose audit rules should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004287 RID: 17031 RVA: 0x000EA532 File Offset: 0x000E8732
		public void RemoveAuditRuleAll(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Removes a single matching allow or deny audit rule from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" />  object that represents an audit rule to remove from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004288 RID: 17032 RVA: 0x000EA53B File Offset: 0x000E873B
		public void RemoveAuditRuleSpecific(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Sets the specified audit rule for the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to set for a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x06004289 RID: 17033 RVA: 0x000EA544 File Offset: 0x000E8744
		public void SetAuditRule(FileSystemAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal FileSystemSecurity()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
