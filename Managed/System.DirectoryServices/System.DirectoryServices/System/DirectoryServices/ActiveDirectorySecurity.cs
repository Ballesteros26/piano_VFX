using System;
using System.Security.AccessControl;
using System.Security.Principal;

namespace System.DirectoryServices
{
	/// <summary>Uses the object security layer of the managed ACL library to wrap access control functionality for directory objects.</summary>
	// Token: 0x0200000A RID: 10
	public class ActiveDirectorySecurity : DirectoryObjectSecurity
	{
		/// <summary>Gets the <see cref="T:System.Type" /> object that represents an access right for this object.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the access right.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000208C File Offset: 0x0000028C
		public override Type AccessRightType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that represents an access rule for this object.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the access rule.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000208C File Offset: 0x0000028C
		public override Type AccessRuleType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that represents an audit rule for this object.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the audit rule.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000208C File Offset: 0x0000028C
		public override Type AuditRuleType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Adds an access rule to the DACL of an object.</summary>
		/// <param name="rule">The <see cref="M:System.Security.AccessControl.DirectoryObjectSecurity.AddAccessRule(System.Security.AccessControl.ObjectAccessRule)" /> object to which this operation applies. </param>
		// Token: 0x06000010 RID: 16 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddAccessRule(ActiveDirectoryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches an object's DACL for all access rules that contain a security identifier (SID) that matches the SID specified in the <paramref name="rule" /> object, and an access control type (Allow or Deny) that matches the type specified in the <paramref name="rule" /> object, and replaces all of those access rules with the access rules that are contained in the <paramref name="rule" /> object.</summary>
		/// <param name="rule">The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> object to which this operation applies.</param>
		// Token: 0x06000011 RID: 17 RVA: 0x0000208C File Offset: 0x0000028C
		public void SetAccessRule(ActiveDirectoryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Searches an object's DACL for all access rules that contain a security identifier (SID) that matches the SID specified in the <paramref name="rule" /> object, and replaces all of those access rules with the access rules that are contained in the <paramref name="rule" /> object.</summary>
		/// <param name="rule">The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> object to which this operation applies.</param>
		// Token: 0x06000012 RID: 18 RVA: 0x0000208C File Offset: 0x0000028C
		public void ResetAccessRule(ActiveDirectoryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that have the specified <see cref="T:System.Security.Principal.IdentityReference" /> object and <see cref="T:System.Security.AccessControl.AccessControlType" /> object from the DACL of an object.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object such as an NTAccount object, that resolves to a security identifier (SID).</param>
		/// <param name="type">An <see cref="T:System.Security.AccessControl.AccessControlType" /> object that contains the ACE type.</param>
		// Token: 0x06000013 RID: 19 RVA: 0x0000208C File Offset: 0x0000028C
		public void RemoveAccess(IdentityReference identity, AccessControlType type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that contain the same security identifier and access mask as the specified access rule from the DACL of an object.</summary>
		/// <returns>true if the operation is successful; otherwise, false.</returns>
		/// <param name="rule">The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> object to which this operation applies.</param>
		// Token: 0x06000014 RID: 20 RVA: 0x0000208C File Offset: 0x0000028C
		public bool RemoveAccessRule(ActiveDirectoryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules that exactly match the specified access rule from the DACL of an object.</summary>
		/// <param name="rule">The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> object to which this operation applies.</param>
		// Token: 0x06000015 RID: 21 RVA: 0x0000208C File Offset: 0x0000028C
		public void RemoveAccessRuleSpecific(ActiveDirectoryAccessRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the specified <see cref="T:System.Security.AccessControl.AccessRule" />.</summary>
		/// <returns>true if successfully modified; otherwise, false.</returns>
		/// <param name="modification">The type of access control modification to perform.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if successfully modified; otherwise, false.</param>
		// Token: 0x06000016 RID: 22 RVA: 0x0000208C File Offset: 0x0000028C
		public override bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all access rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All access rules are not in canonical order.</exception>
		// Token: 0x06000017 RID: 23 RVA: 0x0000208C File Offset: 0x0000028C
		public override void PurgeAccessRules(IdentityReference identity)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds an audit rule to the SACL of an object.</summary>
		/// <param name="rule">The <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" /> object to which this operation applies.</param>
		// Token: 0x06000018 RID: 24 RVA: 0x0000208C File Offset: 0x0000028C
		public void AddAuditRule(ActiveDirectoryAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary> Replaces all audit rules that contain the same security identifier as the specified audit rule in the SACL of an object with the specified audit rule.</summary>
		/// <param name="rule">The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> object to which this operation applies.</param>
		// Token: 0x06000019 RID: 25 RVA: 0x0000208C File Offset: 0x0000028C
		public void SetAuditRule(ActiveDirectoryAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary> Removes all audit rules that have the specified <see cref="T:System.Security.Principal.IdentityReference" /> object from the SACL of an object.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object such as an NTAccount object, that resolves to a security identifier (SID).</param>
		// Token: 0x0600001A RID: 26 RVA: 0x0000208C File Offset: 0x0000028C
		public void RemoveAudit(IdentityReference identity)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that contain the same security identifier and access mask as the specified audit rule from the System Access Control List (SACL) of an object.</summary>
		/// <returns>true if the operation is successful; otherwise, false.</returns>
		/// <param name="rule">The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> object to which this operation applies.</param>
		// Token: 0x0600001B RID: 27 RVA: 0x0000208C File Offset: 0x0000028C
		public bool RemoveAuditRule(ActiveDirectoryAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules that exactly match the specified audit rule from the SACL of an object.</summary>
		/// <param name="rule">The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> object to which this operation applies.</param>
		// Token: 0x0600001C RID: 28 RVA: 0x0000208C File Offset: 0x0000028C
		public void RemoveAuditRuleSpecific(ActiveDirectoryAuditRule rule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies the specified modification to the specified <see cref="T:System.Security.AccessControl.AuditRule" />.</summary>
		/// <returns>true if successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if successfully modified; otherwise, false.</param>
		// Token: 0x0600001D RID: 29 RVA: 0x0000208C File Offset: 0x0000028C
		public override bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all audit rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all audit rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All audit rules are not in canonical order.</exception>
		// Token: 0x0600001E RID: 30 RVA: 0x0000208C File Offset: 0x0000028C
		public override void PurgeAuditRules(IdentityReference identity)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an <see cref="T:System.Security.AccessControl.AccessRule" /> object with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> that corresponds to the <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object such as an NTAccount object that resolves to a security identifier (SID).</param>
		/// <param name="accessMask">An <see cref="T:System.Int32" /> bitmask that shows the access privileges to use.</param>
		/// <param name="isInherited">A <see cref="T:System.Boolean" /> object that indicates whether ACEs are inherited. true if ACEs are inherited; otherwise, false.</param>
		/// <param name="inheritanceFlags">An <see cref="T:System.Security.AccessControl.InheritanceFlags" /> object that contains inheritance flags on a directory object.</param>
		/// <param name="propagationFlags">A <see cref="T:System.Security.AccessControl.PropagationFlags" /> object that contains inheritance propagation flags on a directory object.</param>
		/// <param name="type">An <see cref="T:System.Security.AccessControl.AccessControlType" /> object that contains the ACE type.</param>
		// Token: 0x0600001F RID: 31 RVA: 0x0000208C File Offset: 0x0000028C
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an <see cref="T:System.Security.AccessControl.AccessRule" /> object with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> that corresponds to the <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object, such as an NTAccount object, that resolves to a security identifier (SID).</param>
		/// <param name="accessMask">An <see cref="T:System.Int32" /> bitmask that shows the access privileges to use.</param>
		/// <param name="isInherited">A <see cref="T:System.Boolean" /> object that indicates if ACEs are inherited. true if ACEs are inherited; otherwise, false.</param>
		/// <param name="inheritanceFlags">An <see cref="T:System.Security.AccessControl.InheritanceFlags" /> object that contains inheritance flags for a directory object.</param>
		/// <param name="propagationFlags">A <see cref="T:System.Security.AccessControl.PropagationFlags" /> object that contains inheritance propagation flags for a directory object.</param>
		/// <param name="type">An <see cref="T:System.Security.AccessControl.AccessControlType" /> object that contains the ACE type.</param>
		/// <param name="objectGuid">A <see cref="T:System.Guid" /> object that contains the GUID of the directory object.</param>
		/// <param name="inheritedObjectGuid">A <see cref="T:System.Guid" /> object that contains the GUID of the inherited directory object.</param>
		// Token: 0x06000020 RID: 32 RVA: 0x0000208C File Offset: 0x0000028C
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type, Guid objectGuid, Guid inheritedObjectGuid)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an <see cref="T:System.Security.AccessControl.AuditRule" /> object with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> that corresponds to the <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object such as an NTAccount object, that resolves to a security identifier (SID).</param>
		/// <param name="accessMask">An <see cref="T:System.Int32" /> bitmask that shows the access privileges to use.</param>
		/// <param name="isInherited">A <see cref="T:System.Boolean" /> object that indicates if ACEs are inherited. true if ACEs are inherited; otherwise, false.</param>
		/// <param name="inheritanceFlags">An <see cref="T:System.Security.AccessControl.InheritanceFlags" /> object that contains inheritance flags on a directory object.</param>
		/// <param name="propagationFlags">A <see cref="T:System.Security.AccessControl.PropagationFlags" /> object that contains inheritance propagation flags on a directory object.</param>
		/// <param name="flags">An <see cref="T:System.Security.AccessControl.AuditFlags" /> object that contains the audit flags for this <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object.</param>
		// Token: 0x06000021 RID: 33 RVA: 0x0000208C File Offset: 0x0000028C
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an <see cref="T:System.Security.AccessControl.AuditRule" /> object with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> that corresponds to the <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object such as an NTAccount object that resolves to a security identifier (SID).</param>
		/// <param name="accessMask">An <see cref="T:System.Int32" /> bitmask that shows the access privileges to use.</param>
		/// <param name="isInherited">A <see cref="T:System.Boolean" /> object that indicates whether ACEs are inherited. true if ACEs are inherited; otherwise, false.</param>
		/// <param name="inheritanceFlags">An <see cref="T:System.Security.AccessControl.InheritanceFlags" /> object that contains inheritance flags on a directory object.</param>
		/// <param name="propagationFlags">A <see cref="T:System.Security.AccessControl.PropagationFlags" /> object that contains inheritance propagation flags on a directory object.</param>
		/// <param name="flags">An <see cref="T:System.Security.AccessControl.AuditFlags" /> object that contains the audit flags for this <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object.</param>
		/// <param name="objectGuid">A <see cref="T:System.Guid" /> object that contains the Guido of the directory object.</param>
		/// <param name="inheritedObjectGuid">A <see cref="T:System.Guid" /> object that contains the Guido of the inherited directory object.</param>
		// Token: 0x06000022 RID: 34 RVA: 0x0000208C File Offset: 0x0000028C
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags, Guid objectGuid, Guid inheritedObjectGuid)
		{
			throw new NotImplementedException();
		}
	}
}
