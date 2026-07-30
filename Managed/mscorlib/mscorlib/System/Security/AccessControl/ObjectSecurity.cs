using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to objects without direct manipulation of Access Control Lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> and <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> classes.</summary>
	// Token: 0x02000604 RID: 1540
	public abstract class ObjectSecurity
	{
		// Token: 0x0600433B RID: 17211 RVA: 0x00002111 File Offset: 0x00000311
		protected ObjectSecurity()
		{
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x000EC9E0 File Offset: 0x000EABE0
		protected ObjectSecurity(CommonSecurityDescriptor securityDescriptor)
		{
			if (securityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptor");
			}
			this.descriptor = securityDescriptor;
			this.rw_lock = new ReaderWriterLock();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class.</summary>
		/// <param name="isContainer">true if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</param>
		/// <param name="isDS">True if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</param>
		// Token: 0x0600433D RID: 17213 RVA: 0x000ECA08 File Offset: 0x000EAC08
		protected ObjectSecurity(bool isContainer, bool isDS)
			: this(new CommonSecurityDescriptor(isContainer, isDS, ControlFlags.None, null, null, null, new DiscretionaryAcl(isContainer, isDS, 0)))
		{
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>The type of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600433E RID: 17214
		public abstract Type AccessRightType { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600433F RID: 17215
		public abstract Type AccessRuleType { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004340 RID: 17216
		public abstract Type AuditRuleType { get; }

		/// <summary>Gets a Boolean value that specifies whether the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object are in canonical order.</summary>
		/// <returns>true if the access rules are in canonical order; otherwise, false.</returns>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x000ECA30 File Offset: 0x000EAC30
		public bool AreAccessRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isDiscretionaryAclCanonical;
				try
				{
					isDiscretionaryAclCanonical = this.descriptor.IsDiscretionaryAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isDiscretionaryAclCanonical;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>true if the DACL is protected; otherwise, false.</returns>
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x000ECA6C File Offset: 0x000EAC6C
		public bool AreAccessRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this.descriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object are in canonical order.</summary>
		/// <returns>true if the audit rules are in canonical order; otherwise, false.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x000ECAB0 File Offset: 0x000EACB0
		public bool AreAuditRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isSystemAclCanonical;
				try
				{
					isSystemAclCanonical = this.descriptor.IsSystemAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isSystemAclCanonical;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>true if the SACL is protected; otherwise, false.</returns>
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06004344 RID: 17220 RVA: 0x000ECAEC File Offset: 0x000EACEC
		public bool AreAuditRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this.descriptor.ControlFlags & ControlFlags.SystemAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x000ECB30 File Offset: 0x000EAD30
		// (set) Token: 0x06004346 RID: 17222 RVA: 0x000ECB3E File Offset: 0x000EAD3E
		internal AccessControlSections AccessControlSectionsModified
		{
			get
			{
				this.Reading();
				return this.sections_modified;
			}
			set
			{
				this.Writing();
				this.sections_modified = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified.</summary>
		/// <returns>true if the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified; otherwise, false.</returns>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x000ECB4D File Offset: 0x000EAD4D
		// (set) Token: 0x06004348 RID: 17224 RVA: 0x000ECB56 File Offset: 0x000EAD56
		protected bool AccessRulesModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Access);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Access, value);
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified.</summary>
		/// <returns>true if the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified; otherwise, false.</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x000ECB60 File Offset: 0x000EAD60
		// (set) Token: 0x0600434A RID: 17226 RVA: 0x000ECB69 File Offset: 0x000EAD69
		protected bool AuditRulesModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Audit);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Audit, value);
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the group associated with the securable object has been modified. </summary>
		/// <returns>true if the group associated with the securable object has been modified; otherwise, false.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x000ECB73 File Offset: 0x000EAD73
		// (set) Token: 0x0600434C RID: 17228 RVA: 0x000ECB7C File Offset: 0x000EAD7C
		protected bool GroupModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Group);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Group, value);
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</summary>
		/// <returns>true if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object; otherwise, false.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x000ECB86 File Offset: 0x000EAD86
		protected bool IsContainer
		{
			get
			{
				return this.descriptor.IsContainer;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</summary>
		/// <returns>true if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object; otherwise, false.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x000ECB93 File Offset: 0x000EAD93
		protected bool IsDS
		{
			get
			{
				return this.descriptor.IsDS;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the owner of the securable object has been modified.</summary>
		/// <returns>true if the owner of the securable object has been modified; otherwise, false.</returns>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x000ECBA0 File Offset: 0x000EADA0
		// (set) Token: 0x06004350 RID: 17232 RVA: 0x000ECBA9 File Offset: 0x000EADA9
		protected bool OwnerModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Owner);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Owner, value);
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
		// Token: 0x06004351 RID: 17233
		public abstract AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity to which the audit rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited audit rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">Specifies the conditions for which the rule is audited.</param>
		// Token: 0x06004352 RID: 17234
		public abstract AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);

		/// <summary>Gets the primary group associated with the specified owner.</summary>
		/// <returns>The primary group associated with the specified owner.</returns>
		/// <param name="targetType">The owner for which to get the primary group. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06004353 RID: 17235 RVA: 0x000ECBB4 File Offset: 0x000EADB4
		public IdentityReference GetGroup(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this.descriptor.Group == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this.descriptor.Group.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		/// <summary>Gets the owner associated with the specified primary group.</summary>
		/// <returns>The owner associated with the specified group.</returns>
		/// <param name="targetType">The primary group for which to get the owner.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06004354 RID: 17236 RVA: 0x000ECC0C File Offset: 0x000EAE0C
		public IdentityReference GetOwner(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this.descriptor.Owner == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this.descriptor.Owner.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		/// <summary>Returns an array of byte values that represents the security descriptor information for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>An array of byte values that represents the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. This method returns null if there is no security information in this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x06004355 RID: 17237 RVA: 0x000ECC64 File Offset: 0x000EAE64
		public byte[] GetSecurityDescriptorBinaryForm()
		{
			this.ReadLock();
			byte[] array2;
			try
			{
				byte[] array = new byte[this.descriptor.BinaryLength];
				this.descriptor.GetBinaryForm(array, 0);
				array2 = array;
			}
			finally
			{
				this.ReadUnlock();
			}
			return array2;
		}

		/// <summary>Returns the Security Descriptor Definition Language (SDDL) representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>The SDDL representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		/// <param name="includeSections">Specifies which sections (access rules, audit rules, primary group, owner) of the security descriptor to get.</param>
		// Token: 0x06004356 RID: 17238 RVA: 0x000ECCB4 File Offset: 0x000EAEB4
		public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
		{
			this.ReadLock();
			string sddlForm;
			try
			{
				sddlForm = this.descriptor.GetSddlForm(includeSections);
			}
			finally
			{
				this.ReadUnlock();
			}
			return sddlForm;
		}

		/// <summary>Returns a Boolean value that specifies whether the security descriptor associated with this  <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object can be converted to the Security Descriptor Definition Language (SDDL) format.</summary>
		/// <returns>true if the security descriptor associated with this  <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object can be converted to the Security Descriptor Definition Language (SDDL) format; otherwise, false.</returns>
		// Token: 0x06004357 RID: 17239 RVA: 0x000ECCF0 File Offset: 0x000EAEF0
		public static bool IsSddlConversionSupported()
		{
			return GenericSecurityDescriptor.IsSddlConversionSupported();
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x06004358 RID: 17240 RVA: 0x000ECCF7 File Offset: 0x000EAEF7
		public virtual bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException("rule");
			}
			return this.ModifyAccess(modification, rule, out modified);
		}

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x06004359 RID: 17241 RVA: 0x000ECD2E File Offset: 0x000EAF2E
		public virtual bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException("rule");
			}
			return this.ModifyAudit(modification, rule, out modified);
		}

		/// <summary>Removes all access rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all access rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All access rules are not in canonical order.</exception>
		// Token: 0x0600435A RID: 17242 RVA: 0x000ECD68 File Offset: 0x000EAF68
		public virtual void PurgeAccessRules(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this.descriptor.PurgeAccessControl(ObjectSecurity.SidFromIR(identity));
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Removes all audit rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all audit rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All audit rules are not in canonical order.</exception>
		// Token: 0x0600435B RID: 17243 RVA: 0x000ECDBC File Offset: 0x000EAFBC
		public virtual void PurgeAuditRules(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this.descriptor.PurgeAudit(ObjectSecurity.SidFromIR(identity));
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets or removes protection of the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. Protected access rules cannot be modified by parent objects through inheritance.</summary>
		/// <param name="isProtected">true to protect the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from inheritance; false to allow inheritance.</param>
		/// <param name="preserveInheritance">true to preserve inherited access rules; false to remove inherited access rules. This parameter is ignored if <paramref name="isProtected" /> is false.</param>
		/// <exception cref="T:System.InvalidOperationException">This method attempts to remove inherited rules from a non-canonical Discretionary Access Control List (DACL).</exception>
		// Token: 0x0600435C RID: 17244 RVA: 0x000ECE10 File Offset: 0x000EB010
		public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this.descriptor.SetDiscretionaryAclProtection(isProtected, preserveInheritance);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets or removes protection of the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. Protected audit rules cannot be modified by parent objects through inheritance.</summary>
		/// <param name="isProtected">true to protect the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from inheritance; false to allow inheritance.</param>
		/// <param name="preserveInheritance">true to preserve inherited audit rules; false to remove inherited audit rules. This parameter is ignored if <paramref name="isProtected" /> is false.</param>
		/// <exception cref="T:System.InvalidOperationException">This method attempts to remove inherited rules from a non-canonical System Access Control List (SACL).</exception>
		// Token: 0x0600435D RID: 17245 RVA: 0x000ECE4C File Offset: 0x000EB04C
		public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this.descriptor.SetSystemAclProtection(isProtected, preserveInheritance);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets the primary group for the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="identity">The primary group to set.</param>
		// Token: 0x0600435E RID: 17246 RVA: 0x000ECE88 File Offset: 0x000EB088
		public void SetGroup(IdentityReference identity)
		{
			this.WriteLock();
			try
			{
				this.descriptor.Group = ObjectSecurity.SidFromIR(identity);
				this.GroupModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets the owner for the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="identity">The owner to set.</param>
		// Token: 0x0600435F RID: 17247 RVA: 0x000ECECC File Offset: 0x000EB0CC
		public void SetOwner(IdentityReference identity)
		{
			this.WriteLock();
			try
			{
				this.descriptor.Owner = ObjectSecurity.SidFromIR(identity);
				this.OwnerModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Sets the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		// Token: 0x06004360 RID: 17248 RVA: 0x000ECF10 File Offset: 0x000EB110
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
		{
			this.SetSecurityDescriptorBinaryForm(binaryForm, AccessControlSections.All);
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x06004361 RID: 17249 RVA: 0x000ECF1B File Offset: 0x000EB11B
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
		{
			this.CopySddlForm(new CommonSecurityDescriptor(this.IsContainer, this.IsDS, binaryForm, 0), includeSections);
		}

		/// <summary>Sets the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to set the security descriptor.</param>
		// Token: 0x06004362 RID: 17250 RVA: 0x000ECF37 File Offset: 0x000EB137
		public void SetSecurityDescriptorSddlForm(string sddlForm)
		{
			this.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.All);
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x06004363 RID: 17251 RVA: 0x000ECF42 File Offset: 0x000EB142
		public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
		{
			this.CopySddlForm(new CommonSecurityDescriptor(this.IsContainer, this.IsDS, sddlForm), includeSections);
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x000ECF60 File Offset: 0x000EB160
		private void CopySddlForm(CommonSecurityDescriptor sourceDescriptor, AccessControlSections includeSections)
		{
			this.WriteLock();
			try
			{
				this.AccessControlSectionsModified |= includeSections;
				if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
				{
					this.descriptor.SystemAcl = sourceDescriptor.SystemAcl;
				}
				if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
				{
					this.descriptor.DiscretionaryAcl = sourceDescriptor.DiscretionaryAcl;
				}
				if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
				{
					this.descriptor.Owner = sourceDescriptor.Owner;
				}
				if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
				{
					this.descriptor.Group = sourceDescriptor.Group;
				}
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the DACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">true if the DACL is successfully modified; otherwise, false.</param>
		// Token: 0x06004365 RID: 17253
		protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>true if the SACL is successfully modified; otherwise, false.</returns>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">true if the SACL is successfully modified; otherwise, false.</param>
		// Token: 0x06004366 RID: 17254
		protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

		// Token: 0x06004367 RID: 17255 RVA: 0x00084117 File Offset: 0x00082317
		private Exception GetNotImplementedException()
		{
			return new NotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="handle">The handle used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x06004368 RID: 17256 RVA: 0x000ECFF4 File Offset: 0x000EB1F4
		protected virtual void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			throw this.GetNotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="name">The name used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x06004369 RID: 17257 RVA: 0x000ECFF4 File Offset: 0x000EB1F4
		protected virtual void Persist(string name, AccessControlSections includeSections)
		{
			throw this.GetNotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical. For more information, see Remarks.</summary>
		/// <param name="enableOwnershipPrivilege">true to enable the privilege that allows the caller to take ownership of the object.</param>
		/// <param name="name">The name used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x0600436A RID: 17258 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000ECFFC File Offset: 0x000EB1FC
		private void Reading()
		{
			if (!this.rw_lock.IsReaderLockHeld && !this.rw_lock.IsWriterLockHeld)
			{
				throw new InvalidOperationException("Either a read or a write lock must be held.");
			}
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x0600436C RID: 17260 RVA: 0x000ED023 File Offset: 0x000EB223
		protected void ReadLock()
		{
			this.rw_lock.AcquireReaderLock(-1);
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x0600436D RID: 17261 RVA: 0x000ED031 File Offset: 0x000EB231
		protected void ReadUnlock()
		{
			this.rw_lock.ReleaseReaderLock();
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000ED03E File Offset: 0x000EB23E
		private void Writing()
		{
			if (!this.rw_lock.IsWriterLockHeld)
			{
				throw new InvalidOperationException("Write lock must be held.");
			}
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x0600436F RID: 17263 RVA: 0x000ED058 File Offset: 0x000EB258
		protected void WriteLock()
		{
			this.rw_lock.AcquireWriterLock(-1);
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x06004370 RID: 17264 RVA: 0x000ED066 File Offset: 0x000EB266
		protected void WriteUnlock()
		{
			this.rw_lock.ReleaseWriterLock();
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000ED074 File Offset: 0x000EB274
		internal AuthorizationRuleCollection InternalGetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			List<AuthorizationRule> list = new List<AuthorizationRule>();
			this.ReadLock();
			try
			{
				foreach (GenericAce genericAce in this.descriptor.DiscretionaryAcl)
				{
					QualifiedAce qualifiedAce = genericAce as QualifiedAce;
					if (!(null == qualifiedAce) && (!qualifiedAce.IsInherited || includeInherited) && (qualifiedAce.IsInherited || includeExplicit))
					{
						AccessControlType accessControlType;
						if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
						{
							accessControlType = AccessControlType.Allow;
						}
						else
						{
							if (AceQualifier.AccessDenied != qualifiedAce.AceQualifier)
							{
								continue;
							}
							accessControlType = AccessControlType.Deny;
						}
						AccessRule accessRule = this.InternalAccessRuleFactory(qualifiedAce, targetType, accessControlType);
						list.Add(accessRule);
					}
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return new AuthorizationRuleCollection(list.ToArray());
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x000ED124 File Offset: 0x000EB324
		internal virtual AccessRule InternalAccessRuleFactory(QualifiedAce ace, Type targetType, AccessControlType type)
		{
			return this.AccessRuleFactory(ace.SecurityIdentifier.Translate(targetType), ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, type);
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x000ED154 File Offset: 0x000EB354
		internal AuthorizationRuleCollection InternalGetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			List<AuthorizationRule> list = new List<AuthorizationRule>();
			this.ReadLock();
			try
			{
				if (this.descriptor.SystemAcl != null)
				{
					foreach (GenericAce genericAce in this.descriptor.SystemAcl)
					{
						QualifiedAce qualifiedAce = genericAce as QualifiedAce;
						if (!(null == qualifiedAce) && (!qualifiedAce.IsInherited || includeInherited) && (qualifiedAce.IsInherited || includeExplicit) && AceQualifier.SystemAudit == qualifiedAce.AceQualifier)
						{
							AuditRule auditRule = this.InternalAuditRuleFactory(qualifiedAce, targetType);
							list.Add(auditRule);
						}
					}
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return new AuthorizationRuleCollection(list.ToArray());
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x000ED200 File Offset: 0x000EB400
		internal virtual AuditRule InternalAuditRuleFactory(QualifiedAce ace, Type targetType)
		{
			return this.AuditRuleFactory(ace.SecurityIdentifier.Translate(targetType), ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, ace.AuditFlags);
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x000ED232 File Offset: 0x000EB432
		internal static SecurityIdentifier SidFromIR(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			return (SecurityIdentifier)identity.Translate(typeof(SecurityIdentifier));
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x000ED25D File Offset: 0x000EB45D
		private bool AreAccessControlSectionsModified(AccessControlSections mask)
		{
			return (this.AccessControlSectionsModified & mask) > AccessControlSections.None;
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x000ED26A File Offset: 0x000EB46A
		private void SetAccessControlSectionsModified(AccessControlSections mask, bool modified)
		{
			if (modified)
			{
				this.AccessControlSectionsModified |= mask;
				return;
			}
			this.AccessControlSectionsModified &= ~mask;
		}

		// Token: 0x040021E1 RID: 8673
		internal CommonSecurityDescriptor descriptor;

		// Token: 0x040021E2 RID: 8674
		private AccessControlSections sections_modified;

		// Token: 0x040021E3 RID: 8675
		private ReaderWriterLock rw_lock;
	}
}
