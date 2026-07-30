using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to a cryptographic key object without direct manipulation of  an Access Control List (ACL).</summary>
	// Token: 0x020005E1 RID: 1505
	public sealed class CryptoKeySecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> class.</summary>
		// Token: 0x0600420C RID: 16908 RVA: 0x000EA49C File Offset: 0x000E869C
		public CryptoKeySecurity()
			: base(false, ResourceType.Unknown)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> class by using the specified security descriptor.</summary>
		/// <param name="securityDescriptor">The security descriptor from which to create the new <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</param>
		// Token: 0x0600420D RID: 16909 RVA: 0x000EA4A6 File Offset: 0x000E86A6
		public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor)
			: base(securityDescriptor, ResourceType.Unknown)
		{
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <returns>The type of the securable object associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</returns>
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x000EA4B0 File Offset: 0x000E86B0
		public override Type AccessRightType
		{
			get
			{
				return typeof(CryptoKeyRights);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</returns>
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x000EA4BC File Offset: 0x000E86BC
		public override Type AccessRuleType
		{
			get
			{
				return typeof(CryptoKeyAccessRule);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the audit rules of this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</returns>
		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x000EA4C8 File Offset: 0x000E86C8
		public override Type AuditRuleType
		{
			get
			{
				return typeof(CryptoKeyAuditRule);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">Specifies the valid access control type.</param>
		// Token: 0x06004211 RID: 16913 RVA: 0x000EA4D4 File Offset: 0x000E86D4
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new CryptoKeyAccessRule(identityReference, (CryptoKeyRights)accessMask, type);
		}

		/// <summary>Adds the specified access rule to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		// Token: 0x06004212 RID: 16914 RVA: 0x000EA4DF File Offset: 0x000E86DF
		public void AddAccessRule(CryptoKeyAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Removes access rules that contain the same security identifier and access mask as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <returns>true if the access rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x06004213 RID: 16915 RVA: 0x000EA4E8 File Offset: 0x000E86E8
		public bool RemoveAccessRule(CryptoKeyAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Removes all access rules that have the same security identifier as the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x06004214 RID: 16916 RVA: 0x000EA4F1 File Offset: 0x000E86F1
		public void RemoveAccessRuleAll(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Removes all access rules that exactly match the specified access rule from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		// Token: 0x06004215 RID: 16917 RVA: 0x000EA4FA File Offset: 0x000E86FA
		public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to reset.</param>
		// Token: 0x06004216 RID: 16918 RVA: 0x000EA503 File Offset: 0x000E8703
		public void ResetAccessRule(CryptoKeyAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Removes all access rules that contain the same security identifier and qualifier as the specified access rule in the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to set.</param>
		// Token: 0x06004217 RID: 16919 RVA: 0x000EA50C File Offset: 0x000E870C
		public void SetAccessRule(CryptoKeyAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity to which the audit rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited audit rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">Specifies the conditions for which the rule is audited.</param>
		// Token: 0x06004218 RID: 16920 RVA: 0x000EA515 File Offset: 0x000E8715
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new CryptoKeyAuditRule(identityReference, (CryptoKeyRights)accessMask, flags);
		}

		/// <summary>Adds the specified audit rule to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		// Token: 0x06004219 RID: 16921 RVA: 0x000EA520 File Offset: 0x000E8720
		public void AddAuditRule(CryptoKeyAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Removes audit rules that contain the same security identifier and access mask as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <returns>true if the audit rule was successfully removed; otherwise, false.</returns>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x0600421A RID: 16922 RVA: 0x000EA529 File Offset: 0x000E8729
		public bool RemoveAuditRule(CryptoKeyAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x0600421B RID: 16923 RVA: 0x000EA532 File Offset: 0x000E8732
		public void RemoveAuditRuleAll(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Removes all audit rules that exactly match the specified audit rule from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		// Token: 0x0600421C RID: 16924 RVA: 0x000EA53B File Offset: 0x000E873B
		public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Removes all audit rules that contain the same security identifier and qualifier as the specified audit rule in the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object and then adds the specified audit rule.</summary>
		/// <param name="rule">The audit rule to set.</param>
		// Token: 0x0600421D RID: 16925 RVA: 0x000EA544 File Offset: 0x000E8744
		public void SetAuditRule(CryptoKeyAuditRule rule)
		{
			base.SetAuditRule(rule);
		}
	}
}
