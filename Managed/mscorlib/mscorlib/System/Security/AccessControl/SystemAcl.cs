using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a System Access Control List (SACL).</summary>
	// Token: 0x02000612 RID: 1554
	public sealed class SystemAcl : CommonAcl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SystemAcl" /> class with the specified values.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a container.</param>
		/// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.SystemAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x060043E7 RID: 17383 RVA: 0x000EABBC File Offset: 0x000E8DBC
		public SystemAcl(bool isContainer, bool isDS, int capacity)
			: base(isContainer, isDS, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SystemAcl" /> class with the specified values from the specified <see cref="T:System.Security.AccessControl.RawAcl" /> object.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a container.</param>
		/// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="rawAcl">The underlying <see cref="T:System.Security.AccessControl.RawAcl" /> object for the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object. Specify null to create an empty ACL.</param>
		// Token: 0x060043E8 RID: 17384 RVA: 0x000EABC7 File Offset: 0x000E8DC7
		public SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl)
			: base(isContainer, isDS, rawAcl)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SystemAcl" /> class with the specified values.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a container.</param>
		/// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="revision">The revision level of the new <see cref="T:System.Security.AccessControl.SystemAcl" /> object.</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.SystemAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x060043E9 RID: 17385 RVA: 0x000EABD2 File Offset: 0x000E8DD2
		public SystemAcl(bool isContainer, bool isDS, byte revision, int capacity)
			: base(isContainer, isDS, revision, capacity)
		{
		}

		/// <summary>Adds an audit rule to the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object.</summary>
		/// <param name="auditFlags">The type of audit rule to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an audit rule.</param>
		/// <param name="accessMask">The access mask for the new audit rule.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new audit rule.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new audit rule.</param>
		// Token: 0x060043EA RID: 17386 RVA: 0x000EE286 File Offset: 0x000EC486
		public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.AddAce(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
		}

		/// <summary>Adds an audit rule with the specified settings to the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type for the new audit rule.</summary>
		/// <param name="auditFlags">The type of audit rule to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an audit rule.</param>
		/// <param name="accessMask">The access mask for the new audit rule.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new audit rule.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new audit rule.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-null values.</param>
		/// <param name="objectType">The identity of the class of objects to which the new audit rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new audit rule.</param>
		// Token: 0x060043EB RID: 17387 RVA: 0x000EE298 File Offset: 0x000EC498
		public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.AddAce(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x000EE2BC File Offset: 0x000EC4BC
		public void AddAudit(SecurityIdentifier sid, ObjectAuditRule rule)
		{
			this.AddAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object.</summary>
		/// <returns>true if this method successfully removes the specified audit rule; otherwise, false.</returns>
		/// <param name="auditFlags">The type of audit rule to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		// Token: 0x060043ED RID: 17389 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.SystemAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type.</summary>
		/// <returns>true if this method successfully removes the specified audit rule; otherwise, false.</returns>
		/// <param name="auditFlags">The type of audit rule to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-null values.</param>
		/// <param name="objectType">The identity of the class of objects to which the removed audit control rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the removed audit rule.</param>
		// Token: 0x060043EE RID: 17390 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x000EE2FC File Offset: 0x000EC4FC
		public bool RemoveAudit(SecurityIdentifier sid, ObjectAuditRule rule)
		{
			return this.RemoveAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="auditFlags">The type of audit rule to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		// Token: 0x060043F0 RID: 17392 RVA: 0x000EE33A File Offset: 0x000EC53A
		public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.RemoveAceSpecific(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
		}

		/// <summary>Removes the specified audit rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type.</summary>
		/// <param name="auditFlags">The type of audit rule to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an audit rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-null values.</param>
		/// <param name="objectType">The identity of the class of objects to which the removed audit control rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the removed audit rule.</param>
		// Token: 0x060043F1 RID: 17393 RVA: 0x000EE34C File Offset: 0x000EC54C
		public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.RemoveAceSpecific(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x000EE370 File Offset: 0x000EC570
		public void RemoveAuditSpecific(SecurityIdentifier sid, ObjectAuditRule rule)
		{
			this.RemoveAuditSpecific(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Sets the specified audit rule for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="auditFlags">The audit condition to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an audit rule.</param>
		/// <param name="accessMask">The access mask for the new audit rule.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new audit rule.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new audit rule.</param>
		// Token: 0x060043F3 RID: 17395 RVA: 0x000EE3AE File Offset: 0x000EC5AE
		public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.SetAce(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
		}

		/// <summary>Sets the specified audit rule for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type.</summary>
		/// <param name="auditFlags">The audit condition to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an audit rule.</param>
		/// <param name="accessMask">The access mask for the new audit rule.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new audit rule.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new audit rule.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-null values.</param>
		/// <param name="objectType">The identity of the class of objects to which the new audit rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new audit rule.</param>
		// Token: 0x060043F4 RID: 17396 RVA: 0x000EE3C0 File Offset: 0x000EC5C0
		public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.SetAce(AceQualifier.SystemAudit, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x000EE3E4 File Offset: 0x000EC5E4
		public void SetAudit(SecurityIdentifier sid, ObjectAuditRule rule)
		{
			this.SetAudit(rule.AuditFlags, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000EE424 File Offset: 0x000EC624
		internal override void ApplyCanonicalSortToExplicitAces()
		{
			int canonicalExplicitAceCount = base.GetCanonicalExplicitAceCount();
			base.ApplyCanonicalSortToExplicitAces(0, canonicalExplicitAceCount);
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal override int GetAceInsertPosition(AceQualifier aceQualifier)
		{
			return 0;
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000EE440 File Offset: 0x000EC640
		internal override bool IsAceMeaningless(GenericAce ace)
		{
			if (base.IsAceMeaningless(ace))
			{
				return true;
			}
			if (!SystemAcl.IsValidAuditFlags(ace.AuditFlags))
			{
				return true;
			}
			QualifiedAce qualifiedAce = ace as QualifiedAce;
			return null != qualifiedAce && AceQualifier.SystemAudit != qualifiedAce.AceQualifier && AceQualifier.SystemAlarm != qualifiedAce.AceQualifier;
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x000EE48C File Offset: 0x000EC68C
		private static bool IsValidAuditFlags(AuditFlags auditFlags)
		{
			return auditFlags != AuditFlags.None && auditFlags == ((AuditFlags.Success | AuditFlags.Failure) & auditFlags);
		}
	}
}
