using System;

namespace System.Security.AccessControl
{
	/// <summary>Controls access to objects without direct manipulation of access control lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> class.</summary>
	// Token: 0x020005D9 RID: 1497
	public abstract class CommonObjectSecurity : ObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> class.</summary>
		/// <param name="isContainer">true if the new object is a container object.</param>
		// Token: 0x060041D2 RID: 16850 RVA: 0x000E9C3E File Offset: 0x000E7E3E
		protected CommonObjectSecurity(bool isContainer)
			: base(isContainer, false)
		{
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000E9C48 File Offset: 0x000E7E48
		internal CommonObjectSecurity(CommonSecurityDescriptor securityDescriptor)
			: base(securityDescriptor)
		{
		}

		/// <summary>Gets a collection of the access rules associated with the specified security identifier.</summary>
		/// <returns>The collection of access rules associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <param name="includeExplicit">true to include access rules explicitly set for the object.</param>
		/// <param name="includeInherited">true to include inherited access rules.</param>
		/// <param name="targetType">Specifies whether the security identifier for which to retrieve access rules is of type T:System.Security.Principal.SecurityIdentifier or type T:System.Security.Principal.NTAccount. The value of this parameter must be a type that can be translated to  the <see cref="T:System.Security.Principal.SecurityIdentifier" /> type.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060041D4 RID: 16852 RVA: 0x000E9C51 File Offset: 0x000E7E51
		public AuthorizationRuleCollection GetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			return base.InternalGetAccessRules(includeExplicit, includeInherited, targetType);
		}

		/// <summary>Gets a collection of the audit rules associated with the specified security identifier.</summary>
		/// <returns>The collection of audit rules associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <param name="includeExplicit">true to include audit rules explicitly set for the object.</param>
		/// <param name="includeInherited">true to include inherited audit rules.</param>
		/// <param name="targetType">The security identifier for which to retrieve audit rules. This must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060041D5 RID: 16853 RVA: 0x000E9C5C File Offset: 0x000E7E5C
		public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			return base.InternalGetAuditRules(includeExplicit, includeInherited, targetType);
		}

		/// <summary>Adds the specified access rule to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		// Token: 0x060041D6 RID: 16854 RVA: 0x000E9C68 File Offset: 0x000E7E68
		protected void AddAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Add, rule, out flag);
		}

		/// <summary>Removes access rules that contain the same security identifier and access mask as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the access rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060041D7 RID: 16855 RVA: 0x000E9C80 File Offset: 0x000E7E80
		protected bool RemoveAccessRule(AccessRule rule)
		{
			bool flag;
			return this.ModifyAccess(AccessControlModification.Remove, rule, out flag);
		}

		/// <summary>Removes all access rules that have the same security identifier as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060041D8 RID: 16856 RVA: 0x000E9C98 File Offset: 0x000E7E98
		protected void RemoveAccessRuleAll(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.RemoveAll, rule, out flag);
		}

		/// <summary>Removes all access rules that exactly match the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x060041D9 RID: 16857 RVA: 0x000E9CB0 File Offset: 0x000E7EB0
		protected void RemoveAccessRuleSpecific(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.RemoveSpecific, rule, out flag);
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to reset.</param>
		// Token: 0x060041DA RID: 16858 RVA: 0x000E9CC8 File Offset: 0x000E7EC8
		protected void ResetAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Reset, rule, out flag);
		}

		/// <summary>Removes all access rules that contain the same security identifier and qualifier as the specified access rule in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to set.</param>
		// Token: 0x060041DB RID: 16859 RVA: 0x000E9CE0 File Offset: 0x000E7EE0
		protected void SetAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Set, rule, out flag);
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x060041DC RID: 16860 RVA: 0x000E9CF8 File Offset: 0x000E7EF8
		protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			modified = true;
			base.WriteLock();
			try
			{
				switch (modification)
				{
				case AccessControlModification.Add:
					break;
				case AccessControlModification.Set:
					this.descriptor.DiscretionaryAcl.SetAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_013D;
				case AccessControlModification.Reset:
					this.PurgeAccessRules(rule.IdentityReference);
					break;
				case AccessControlModification.Remove:
					modified = this.descriptor.DiscretionaryAcl.RemoveAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_013D;
				case AccessControlModification.RemoveAll:
					this.PurgeAccessRules(rule.IdentityReference);
					goto IL_013D;
				case AccessControlModification.RemoveSpecific:
					this.descriptor.DiscretionaryAcl.RemoveAccessSpecific(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_013D;
				default:
					throw new ArgumentOutOfRangeException("modification");
				}
				this.descriptor.DiscretionaryAcl.AddAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
				IL_013D:
				if (modified)
				{
					base.AccessRulesModified = true;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
			return modified;
		}

		/// <summary>Adds the specified audit rule to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		// Token: 0x060041DD RID: 16861 RVA: 0x000E9E74 File Offset: 0x000E8074
		protected void AddAuditRule(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.Add, rule, out flag);
		}

		/// <summary>Removes audit rules that contain the same security identifier and access mask as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the audit rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060041DE RID: 16862 RVA: 0x000E9E8C File Offset: 0x000E808C
		protected bool RemoveAuditRule(AuditRule rule)
		{
			bool flag;
			return this.ModifyAudit(AccessControlModification.Remove, rule, out flag);
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060041DF RID: 16863 RVA: 0x000E9EA4 File Offset: 0x000E80A4
		protected void RemoveAuditRuleAll(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.RemoveAll, rule, out flag);
		}

		/// <summary>Removes all audit rules that exactly match the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x060041E0 RID: 16864 RVA: 0x000E9EBC File Offset: 0x000E80BC
		protected void RemoveAuditRuleSpecific(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.RemoveSpecific, rule, out flag);
		}

		/// <summary>Removes all audit rules that contain the same security identifier and qualifier as the specified audit rule in the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object and then adds the specified audit rule.</summary>
		/// <param name="rule">The audit rule to set.</param>
		// Token: 0x060041E1 RID: 16865 RVA: 0x000E9ED4 File Offset: 0x000E80D4
		protected void SetAuditRule(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.Set, rule, out flag);
		}

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x060041E2 RID: 16866 RVA: 0x000E9EEC File Offset: 0x000E80EC
		protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			modified = true;
			base.WriteLock();
			try
			{
				switch (modification)
				{
				case AccessControlModification.Add:
					if (this.descriptor.SystemAcl == null)
					{
						this.descriptor.SystemAcl = new SystemAcl(base.IsContainer, base.IsDS, 1);
					}
					this.descriptor.SystemAcl.AddAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					break;
				case AccessControlModification.Set:
					if (this.descriptor.SystemAcl == null)
					{
						this.descriptor.SystemAcl = new SystemAcl(base.IsContainer, base.IsDS, 1);
					}
					this.descriptor.SystemAcl.SetAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					break;
				case AccessControlModification.Reset:
					break;
				case AccessControlModification.Remove:
					if (this.descriptor.SystemAcl == null)
					{
						modified = false;
					}
					else
					{
						modified = this.descriptor.SystemAcl.RemoveAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					}
					break;
				case AccessControlModification.RemoveAll:
					this.PurgeAuditRules(rule.IdentityReference);
					break;
				case AccessControlModification.RemoveSpecific:
					if (this.descriptor.SystemAcl != null)
					{
						this.descriptor.SystemAcl.RemoveAuditSpecific(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("modification");
				}
				if (modified)
				{
					base.AuditRulesModified = true;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
			return modified;
		}
	}
}
