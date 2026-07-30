using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a registry key. This class cannot be inherited.</summary>
	// Token: 0x0200060E RID: 1550
	public sealed class RegistrySecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class with default values.</summary>
		// Token: 0x060043CB RID: 17355 RVA: 0x000EDD32 File Offset: 0x000EBF32
		public RegistrySecurity()
			: base(true, ResourceType.RegistryKey)
		{
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x000EDD3C File Offset: 0x000EBF3C
		internal RegistrySecurity(string name, AccessControlSections includeSections)
			: base(true, ResourceType.RegistryKey, name, includeSections)
		{
		}

		/// <summary>Gets the enumeration type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryRights" /> enumeration.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x000EDD48 File Offset: 0x000EBF48
		public override Type AccessRightType
		{
			get
			{
				return typeof(RegistryRights);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> class.</returns>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x000EDD54 File Offset: 0x000EBF54
		public override Type AccessRuleType
		{
			get
			{
				return typeof(RegistryAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.RegistrySecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> class.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x000EDD60 File Offset: 0x000EBF60
		public override Type AuditRuleType
		{
			get
			{
				return typeof(RegistryAuditRule);
			}
		}

		/// <summary>Creates a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> object representing the specified rights for the specified user.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the access rights to allow or deny, cast to an integer.</param>
		/// <param name="isInherited">A Boolean value specifying whether the rule is inherited.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values specifying how the rule is inherited by subkeys.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that modify the way the rule is inherited by subkeys. Meaningless if the value of <paramref name="inheritanceFlags" /> is <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x060043D0 RID: 17360 RVA: 0x000EDD6C File Offset: 0x000EBF6C
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new RegistryAccessRule(identityReference, (RegistryRights)accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Searches for a matching access control with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The access control rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043D1 RID: 17361 RVA: 0x000EA4DF File Offset: 0x000E86DF
		public void AddAccessRule(RegistryAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Searches for an access control rule with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified access rule, and with compatible inheritance and propagation flags; if such a rule is found, the rights contained in the specified access rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043D2 RID: 17362 RVA: 0x000EA4E8 File Offset: 0x000E86E8
		public bool RemoveAccessRule(RegistryAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Searches for all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for. Any rights, inheritance flags, or propagation flags specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043D3 RID: 17363 RVA: 0x000EA4F1 File Offset: 0x000E86F1
		public void RemoveAccessRuleAll(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Searches for an access control rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043D4 RID: 17364 RVA: 0x000EA4FA File Offset: 0x000E86FA
		public void RemoveAccessRuleSpecific(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Removes all access control rules with the same user as the specified rule, regardless of <see cref="T:System.Security.AccessControl.AccessControlType" />, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		// Token: 0x060043D5 RID: 17365 RVA: 0x000EA503 File Offset: 0x000E8703
		public void ResetAccessRule(RegistryAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAccessRule" /> to add. The user and <see cref="T:System.Security.AccessControl.AccessControlType" /> of this rule determine the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043D6 RID: 17366 RVA: 0x000EA50C File Offset: 0x000E870C
		public void SetAccessRule(RegistryAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Creates a new audit rule, specifying the user the rule applies to, the access rights to audit, the inheritance and propagation of the rule, and the outcome that triggers the rule.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> object representing the specified audit rule for the specified user, with the specified flags. The return type of the method is the base class, <see cref="T:System.Security.AccessControl.AuditRule" />, but the return value can be cast safely to the derived class.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the access rights to audit, cast to an integer.</param>
		/// <param name="isInherited">A Boolean value specifying whether the rule is inherited.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values specifying how the rule is inherited by subkeys.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that modify the way the rule is inherited by subkeys. Meaningless if the value of <paramref name="inheritanceFlags" /> is <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values specifying whether to audit successful access, failed access, or both.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x060043D7 RID: 17367 RVA: 0x000EDD7C File Offset: 0x000EBF7C
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new RegistryAuditRule(identityReference, (RegistryRights)accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		/// <summary>Searches for an audit rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The audit rule to add. The user specified by this rule determines the search.</param>
		// Token: 0x060043D8 RID: 17368 RVA: 0x000EA520 File Offset: 0x000E8720
		public void AddAuditRule(RegistryAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Searches for an audit control rule with the same user as the specified rule, and with compatible inheritance and propagation flags; if a compatible rule is found, the rights contained in the specified rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise, false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> that specifies the user to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043D9 RID: 17369 RVA: 0x000EA529 File Offset: 0x000E8729
		public bool RemoveAuditRule(RegistryAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Searches for all audit rules with the same user as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> that specifies the user to search for. Any rights, inheritance flags, or propagation flags specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043DA RID: 17370 RVA: 0x000EA532 File Offset: 0x000E8732
		public void RemoveAuditRuleAll(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Searches for an audit rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043DB RID: 17371 RVA: 0x000EA53B File Offset: 0x000E873B
		public void RemoveAuditRuleSpecific(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Removes all audit rules with the same user as the specified rule, regardless of the <see cref="T:System.Security.AccessControl.AuditFlags" /> value, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060043DC RID: 17372 RVA: 0x000EA544 File Offset: 0x000E8744
		public void SetAuditRule(RegistryAuditRule rule)
		{
			base.SetAuditRule(rule);
		}
	}
}
