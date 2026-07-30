using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a set of access rights to be audited for a user or group. This class cannot be inherited.</summary>
	// Token: 0x0200060C RID: 1548
	public sealed class RegistryAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> class, specifying the user or group to audit, the rights to audit, whether to take inheritance into account, and whether to audit success, failure, or both.</summary>
		/// <param name="identity">The user or group the rule applies to. Must be of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> or a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="registryRights">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the kinds of access to audit.</param>
		/// <param name="inheritanceFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values specifying whether the audit rule applies to subkeys of the current key.</param>
		/// <param name="propagationFlags">A bitwise combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that affect the way an inherited audit rule is propagated to subkeys of the current key.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values specifying whether to audit success, failure, or both.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="eventRights" /> specifies an invalid value.-or-<paramref name="flags" /> specifies an invalid value.-or-<paramref name="inheritanceFlags" /> specifies an invalid value.-or-<paramref name="propagationFlags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null. -or-<paramref name="registryRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x060043C7 RID: 17351 RVA: 0x000EDD0E File Offset: 0x000EBF0E
		public RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(identity, registryRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000E8D5E File Offset: 0x000E6F5E
		internal RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, (int)registryRights, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RegistryAuditRule" /> class, specifying the name of the user or group to audit, the rights to audit, whether to take inheritance into account, and whether to audit success, failure, or both.</summary>
		/// <param name="identity">The name of the user or group the rule applies to.</param>
		/// <param name="registryRights">A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values specifying the kinds of access to audit.</param>
		/// <param name="inheritanceFlags">A combination of <see cref="T:System.Security.AccessControl.InheritanceFlags" /> flags that specifies whether the audit rule applies to subkeys of the current key.</param>
		/// <param name="propagationFlags">A combination of <see cref="T:System.Security.AccessControl.PropagationFlags" /> flags that affect the way an inherited audit rule is propagated to subkeys of the current key.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values specifying whether to audit success, failure, or both.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="eventRights" /> specifies an invalid value.-or-<paramref name="flags" /> specifies an invalid value.-or-<paramref name="inheritanceFlags" /> specifies an invalid value.-or-<paramref name="propagationFlags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="registryRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is null.-or-<paramref name="identity" /> is a zero-length string.-or-<paramref name="identity" /> is longer than 512 characters.</exception>
		// Token: 0x060043C9 RID: 17353 RVA: 0x000EDD1E File Offset: 0x000EBF1E
		public RegistryAuditRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(new NTAccount(identity), registryRights, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Gets the access rights affected by the audit rule.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Security.AccessControl.RegistryRights" /> values that indicates the rights affected by the audit rule.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060043CA RID: 17354 RVA: 0x000EA476 File Offset: 0x000E8676
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
