using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a Discretionary Access Control List (DACL).</summary>
	// Token: 0x020005E5 RID: 1509
	public sealed class DiscretionaryAcl : CommonAcl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> class with the specified values.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
		/// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x0600423D RID: 16957 RVA: 0x000EABBC File Offset: 0x000E8DBC
		public DiscretionaryAcl(bool isContainer, bool isDS, int capacity)
			: base(isContainer, isDS, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> class with the specified values from the specified <see cref="T:System.Security.AccessControl.RawAcl" /> object.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
		/// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="rawAcl">The underlying <see cref="T:System.Security.AccessControl.RawAcl" /> object for the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Specify null to create an empty ACL.</param>
		// Token: 0x0600423E RID: 16958 RVA: 0x000EABC7 File Offset: 0x000E8DC7
		public DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl)
			: base(isContainer, isDS, rawAcl)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> class with the specified values.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
		/// <param name="isDS">true if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="revision">The revision level of the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x0600423F RID: 16959 RVA: 0x000EABD2 File Offset: 0x000E8DD2
		public DiscretionaryAcl(bool isContainer, bool isDS, byte revision, int capacity)
			: base(isContainer, isDS, revision, capacity)
		{
		}

		/// <summary>Adds an Access Control Entry (ACE) with the specified settings to the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an ACE.</param>
		/// <param name="accessMask">The access rule for the new ACE.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new ACE.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new ACE.</param>
		// Token: 0x06004240 RID: 16960 RVA: 0x000EABDF File Offset: 0x000E8DDF
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.AddAce(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None);
		}

		/// <summary>Adds an Access Control Entry (ACE) with the specified settings to the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type for the new ACE.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an ACE.</param>
		/// <param name="accessMask">The access rule for the new ACE.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new ACE.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new ACE.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-null values.</param>
		/// <param name="objectType">The identity of the class of objects to which the new ACE applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new ACE.</param>
		// Token: 0x06004241 RID: 16961 RVA: 0x000EABF4 File Offset: 0x000E8DF4
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.AddAce(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000EAC1C File Offset: 0x000E8E1C
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.AddAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Removes the specified access control rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <returns>true if this method successfully removes the specified access; otherwise, false.</returns>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an access control rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		// Token: 0x06004243 RID: 16963 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified access control rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type.</summary>
		/// <returns>true if this method successfully removes the specified access; otherwise, false.</returns>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an access control rule.</param>
		/// <param name="accessMask">The access mask for the access control rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the access control rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the access control rule to be removed.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-null values.</param>
		/// <param name="objectType">The identity of the class of objects to which the removed access control rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the removed access control rule.</param>
		// Token: 0x06004244 RID: 16964 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000EAC58 File Offset: 0x000E8E58
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			return this.RemoveAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Removes the specified Access Control Entry (ACE) from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an ACE.</param>
		/// <param name="accessMask">The access mask for the ACE to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the ACE to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the ACE to be removed.</param>
		// Token: 0x06004246 RID: 16966 RVA: 0x000EAC91 File Offset: 0x000E8E91
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.RemoveAceSpecific(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None);
		}

		/// <summary>Removes the specified Access Control Entry (ACE) from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type for the ACE to be removed.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an ACE.</param>
		/// <param name="accessMask">The access mask for the ACE to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the ACE to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the ACE to be removed.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-null values.</param>
		/// <param name="objectType">The identity of the class of objects to which the removed ACE applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the removed ACE.</param>
		// Token: 0x06004247 RID: 16967 RVA: 0x000EACA8 File Offset: 0x000E8EA8
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.RemoveAceSpecific(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x000EACD0 File Offset: 0x000E8ED0
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.RemoveAccessSpecific(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Sets the specified access control for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an ACE.</param>
		/// <param name="accessMask">The access rule for the new ACE.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new ACE.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new ACE.</param>
		// Token: 0x06004249 RID: 16969 RVA: 0x000EAD09 File Offset: 0x000E8F09
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.SetAce(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None);
		}

		/// <summary>Sets the specified access control for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an ACE.</param>
		/// <param name="accessMask">The access rule for the new ACE.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new ACE.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new ACE.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-null values.</param>
		/// <param name="objectType">The identity of the class of objects to which the new ACE applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new ACE.</param>
		// Token: 0x0600424A RID: 16970 RVA: 0x000EAD20 File Offset: 0x000E8F20
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.SetAce(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x000EAD48 File Offset: 0x000E8F48
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.SetAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x000EAD84 File Offset: 0x000E8F84
		internal override void ApplyCanonicalSortToExplicitAces()
		{
			int canonicalExplicitAceCount = base.GetCanonicalExplicitAceCount();
			int canonicalExplicitDenyAceCount = base.GetCanonicalExplicitDenyAceCount();
			base.ApplyCanonicalSortToExplicitAces(0, canonicalExplicitDenyAceCount);
			base.ApplyCanonicalSortToExplicitAces(canonicalExplicitDenyAceCount, canonicalExplicitAceCount - canonicalExplicitDenyAceCount);
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x000EADB1 File Offset: 0x000E8FB1
		internal override int GetAceInsertPosition(AceQualifier aceQualifier)
		{
			if (aceQualifier == AceQualifier.AccessAllowed)
			{
				return base.GetCanonicalExplicitDenyAceCount();
			}
			return 0;
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000EADBE File Offset: 0x000E8FBE
		private static AceQualifier GetAceQualifier(AccessControlType accessType)
		{
			if (accessType == AccessControlType.Allow)
			{
				return AceQualifier.AccessAllowed;
			}
			if (AccessControlType.Deny == accessType)
			{
				return AceQualifier.AccessDenied;
			}
			throw new ArgumentOutOfRangeException("accessType");
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x000EADD8 File Offset: 0x000E8FD8
		internal override bool IsAceMeaningless(GenericAce ace)
		{
			if (base.IsAceMeaningless(ace))
			{
				return true;
			}
			if (ace.AuditFlags != AuditFlags.None)
			{
				return true;
			}
			QualifiedAce qualifiedAce = ace as QualifiedAce;
			return null != qualifiedAce && qualifiedAce.AceQualifier != AceQualifier.AccessAllowed && AceQualifier.AccessDenied != qualifiedAce.AceQualifier;
		}
	}
}
