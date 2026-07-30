using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a named mutex. This class cannot be inherited. </summary>
	// Token: 0x020005F7 RID: 1527
	public sealed class MutexSecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class with default values.</summary>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x060042DA RID: 17114 RVA: 0x000EAE53 File Offset: 0x000E9053
		public MutexSecurity()
			: base(false, ResourceType.KernelObject)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class with the specified sections of the access control security rules from the system mutex with the specified name.</summary>
		/// <param name="name">The name of the system mutex whose access control security rules are to be retrieved.</param>
		/// <param name="includeSections">A combination of <see cref="T:System.Security.AccessControl.AccessControlSections" /> flags specifying the sections to retrieve.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">There is no system object with the specified name.</exception>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x060042DB RID: 17115 RVA: 0x000EBDBA File Offset: 0x000E9FBA
		public MutexSecurity(string name, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity.MutexExceptionFromErrorCode), null)
		{
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x000EBDD3 File Offset: 0x000E9FD3
		internal MutexSecurity(SafeHandle handle, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity.MutexExceptionFromErrorCode), null)
		{
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.MutexRights" /> enumeration.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x000EBDEC File Offset: 0x000E9FEC
		public override Type AccessRightType
		{
			get
			{
				return typeof(MutexRights);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.MutexAccessRule" /> class.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x000EBDF8 File Offset: 0x000E9FF8
		public override Type AccessRuleType
		{
			get
			{
				return typeof(MutexAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.MutexAuditRule" /> class.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x000EBE04 File Offset: 0x000EA004
		public override Type AuditRuleType
		{
			get
			{
				return typeof(MutexAuditRule);
			}
		}

		/// <summary>Creates a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.MutexAccessRule" /> object representing the specified rights for the specified user.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.MutexRights" /> values specifying the access rights to allow or deny, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named mutexes, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named mutexes, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named mutexes, because they have no hierarchy.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x060042E0 RID: 17120 RVA: 0x000EBE10 File Offset: 0x000EA010
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new MutexAccessRule(identityReference, (MutexRights)accessMask, type);
		}

		/// <summary>Searches for a matching access control rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The access control rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		/// <exception cref="T:System.Security.Principal.IdentityNotMappedException">
		///   <paramref name="rule " />cannot be mapped to a known identity.</exception>
		// Token: 0x060042E1 RID: 17121 RVA: 0x000EA4DF File Offset: 0x000E86DF
		public void AddAccessRule(MutexAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Searches for an access control rule with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and with compatible inheritance and propagation flags; if such a rule is found, the rights contained in the specified access rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.MutexAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042E2 RID: 17122 RVA: 0x000EA4E8 File Offset: 0x000E86E8
		public bool RemoveAccessRule(MutexAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Searches for all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.MutexAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042E3 RID: 17123 RVA: 0x000EA4F1 File Offset: 0x000E86F1
		public void RemoveAccessRuleAll(MutexAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Searches for an access control rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAccessRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042E4 RID: 17124 RVA: 0x000EA4FA File Offset: 0x000E86FA
		public void RemoveAccessRuleSpecific(MutexAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Removes all access control rules with the same user as the specified rule, regardless of <see cref="T:System.Security.AccessControl.AccessControlType" />, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAccessRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042E5 RID: 17125 RVA: 0x000EA503 File Offset: 0x000E8703
		public void ResetAccessRule(MutexAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAccessRule" /> to add. The user and <see cref="T:System.Security.AccessControl.AccessControlType" /> of this rule determine the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042E6 RID: 17126 RVA: 0x000EA50C File Offset: 0x000E870C
		public void SetAccessRule(MutexAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Creates a new audit rule, specifying the user the rule applies to, the access rights to audit, and the outcome that triggers the audit rule.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.MutexAuditRule" /> object representing the specified audit rule for the specified user. The return type of the method is the base class, <see cref="T:System.Security.AccessControl.AuditRule" />, but the return value can be cast safely to the derived class.</returns>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.MutexRights" /> values specifying the access rights to audit, cast to an integer.</param>
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
		// Token: 0x060042E7 RID: 17127 RVA: 0x000EBE1B File Offset: 0x000EA01B
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new MutexAuditRule(identityReference, (MutexRights)accessMask, flags);
		}

		/// <summary>Searches for an audit rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The audit rule to add. The user specified by this rule determines the search.</param>
		// Token: 0x060042E8 RID: 17128 RVA: 0x000EA520 File Offset: 0x000E8720
		public void AddAuditRule(MutexAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Searches for an audit control rule with the same user as the specified rule, and with compatible inheritance and propagation flags; if a compatible rule is found, the rights contained in the specified rule are removed from it.</summary>
		/// <returns>true if a compatible rule is found; otherwise, false.</returns>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.MutexAuditRule" /> that specifies the user to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042E9 RID: 17129 RVA: 0x000EA529 File Offset: 0x000E8729
		public bool RemoveAuditRule(MutexAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Searches for all audit rules with the same user as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.MutexAuditRule" /> that specifies the user to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042EA RID: 17130 RVA: 0x000EA532 File Offset: 0x000E8732
		public void RemoveAuditRuleAll(MutexAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Searches for an audit rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAuditRule" /> to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042EB RID: 17131 RVA: 0x000EA53B File Offset: 0x000E873B
		public void RemoveAuditRuleSpecific(MutexAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Removes all audit rules with the same user as the specified rule, regardless of the <see cref="T:System.Security.AccessControl.AuditFlags" /> value, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAuditRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is null.</exception>
		// Token: 0x060042EC RID: 17132 RVA: 0x000EA544 File Offset: 0x000E8744
		public void SetAuditRule(MutexAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x000EBE26 File Offset: 0x000EA026
		private static Exception MutexExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			if (errorCode == 2)
			{
				return new WaitHandleCannotBeOpenedException();
			}
			return NativeObjectSecurity.DefaultExceptionFromErrorCode(errorCode, name, handle, context);
		}
	}
}
