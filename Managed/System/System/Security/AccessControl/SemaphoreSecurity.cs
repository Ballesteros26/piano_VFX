using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a named semaphore. This class cannot be inherited.</summary>
	// Token: 0x02000371 RID: 881
	[ComVisible(false)]
	public sealed class SemaphoreSecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class with default values.</summary>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06001AE4 RID: 6884 RVA: 0x0006BE94 File Offset: 0x0006A094
		public SemaphoreSecurity()
			: base(false, ResourceType.KernelObject)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class with the specified sections of the access control security rules from the system semaphore with the specified name.</summary>
		/// <param name="name">The name of the system semaphore whose access control security rules are to be retrieved.</param>
		/// <param name="includeSections">A combination of <see cref="T:System.Security.AccessControl.AccessControlSections" /> flags specifying the sections to retrieve.</param>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006BE9E File Offset: 0x0006A09E
		public SemaphoreSecurity(string name, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, name, includeSections)
		{
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0006BEAA File Offset: 0x0006A0AA
		internal SemaphoreSecurity(SafeHandle handle, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, handle, includeSections)
		{
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreRights" /> enumeration.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0006BEB6 File Offset: 0x0006A0B6
		public override Type AccessRightType
		{
			get
			{
				return typeof(SemaphoreRights);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> class.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0006BEC2 File Offset: 0x0006A0C2
		public override Type AccessRuleType
		{
			get
			{
				return typeof(SemaphoreAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> class.</returns>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0006BECE File Offset: 0x0006A0CE
		public override Type AuditRuleType
		{
			get
			{
				return typeof(SemaphoreAuditRule);
			}
		}

		/// <summary>Creates a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> object representing the specified rights for the specified user.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values specifying the access rights to allow or deny, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06001AEA RID: 6890 RVA: 0x0006BEDA File Offset: 0x0006A0DA
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new SemaphoreAccessRule(identityReference, (SemaphoreRights)accessMask, type);
		}

		/// <summary>Searches for a matching rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The access control rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AEB RID: 6891 RVA: 0x0006BEE5 File Offset: 0x0006A0E5
		public void AddAccessRule(SemaphoreAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Searches for an access control rule with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and with compatible inheritance and propagation flags; if such a rule is found, the rights contained in the specified access rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AEC RID: 6892 RVA: 0x0006BEEE File Offset: 0x0006A0EE
		public bool RemoveAccessRule(SemaphoreAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Searches for all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AED RID: 6893 RVA: 0x0006BEF7 File Offset: 0x0006A0F7
		public void RemoveAccessRuleAll(SemaphoreAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Searches for an access control rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AEE RID: 6894 RVA: 0x0006BF00 File Offset: 0x0006A100
		public void RemoveAccessRuleSpecific(SemaphoreAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Removes all access control rules with the same user as the specified rule, regardless of <see cref="T:System.Security.AccessControl.AccessControlType" />, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AEF RID: 6895 RVA: 0x0006BF09 File Offset: 0x0006A109
		public void ResetAccessRule(SemaphoreAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to add. The user and <see cref="T:System.Security.AccessControl.AccessControlType" /> of this rule determine the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AF0 RID: 6896 RVA: 0x0006BF12 File Offset: 0x0006A112
		public void SetAccessRule(SemaphoreAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Creates a new audit rule, specifying the user the rule applies to, the access rights to audit, and the outcome that triggers the audit rule.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> object representing the specified audit rule for the specified user. The return type of the method is the base class, <see cref="T:System.Security.AccessControl.AuditRule" />, but the return value can be cast safely to the derived class.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values specifying the access rights to audit, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specify whether to audit successful access, failed access, or both.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06001AF1 RID: 6897 RVA: 0x0006BF1B File Offset: 0x0006A11B
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new SemaphoreAuditRule(identityReference, (SemaphoreRights)accessMask, flags);
		}

		/// <summary>Searches for an audit rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The audit rule to add. The user specified by this rule determines the search.</param>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x0006BF26 File Offset: 0x0006A126
		public void AddAuditRule(SemaphoreAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Searches for an audit control rule with the same user as the specified rule, and with compatible inheritance and propagation flags; if a compatible rule is found, the rights contained in the specified rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise, false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> that specifies the user to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0006BF2F File Offset: 0x0006A12F
		public bool RemoveAuditRule(SemaphoreAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Searches for all audit rules with the same user as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> that specifies the user to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x0006BF38 File Offset: 0x0006A138
		public void RemoveAuditRuleAll(SemaphoreAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Searches for an audit rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x0006BF41 File Offset: 0x0006A141
		public void RemoveAuditRuleSpecific(SemaphoreAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Removes all audit rules with the same user as the specified rule, regardless of the <see cref="T:System.Security.AccessControl.AuditFlags" /> value, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x06001AF6 RID: 6902 RVA: 0x0006BF4A File Offset: 0x0006A14A
		public void SetAuditRule(SemaphoreAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0006BF54 File Offset: 0x0006A154
		internal void Persist(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				base.Persist(handle, (base.AccessRulesModified ? AccessControlSections.Access : AccessControlSections.None) | (base.AuditRulesModified ? AccessControlSections.Audit : AccessControlSections.None) | (base.OwnerModified ? AccessControlSections.Owner : AccessControlSections.None) | (base.GroupModified ? AccessControlSections.Group : AccessControlSections.None), null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}
	}
}
