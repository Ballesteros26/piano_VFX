using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x020005DA RID: 1498
	public sealed class CommonSecurityDescriptor : GenericSecurityDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="rawSecurityDescriptor">The <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object from which to create the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		// Token: 0x060041E3 RID: 16867 RVA: 0x000EA0D0 File Offset: 0x000E82D0
		public CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
		{
			this.Init(isContainer, isDS, rawSecurityDescriptor);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="sddlForm">The SDDL string from which to create the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		// Token: 0x060041E4 RID: 16868 RVA: 0x000EA0E1 File Offset: 0x000E82E1
		public CommonSecurityDescriptor(bool isContainer, bool isDS, string sddlForm)
		{
			this.Init(isContainer, isDS, new RawSecurityDescriptor(sddlForm));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified array of byte values.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="binaryForm">The array of byte values from which to create the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="offset">The offset in the <paramref name="binaryForm" /> array at which to begin copying.</param>
		// Token: 0x060041E5 RID: 16869 RVA: 0x000EA0F7 File Offset: 0x000E82F7
		public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset)
		{
			this.Init(isContainer, isDS, new RawSecurityDescriptor(binaryForm, offset));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> class from the specified information.</summary>
		/// <param name="isContainer">true if the new security descriptor is associated with a container object.</param>
		/// <param name="isDS">true if the new security descriptor is associated with a directory object.</param>
		/// <param name="flags">Flags that specify behavior of the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="owner">The owner for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="group">The primary group for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="systemAcl">The System Access Control List (SACL) for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		/// <param name="discretionaryAcl">The Discretionary Access Control List (DACL) for the new <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</param>
		// Token: 0x060041E6 RID: 16870 RVA: 0x000EA10F File Offset: 0x000E830F
		public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.Init(isContainer, isDS, flags, owner, group, systemAcl, discretionaryAcl);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000EA128 File Offset: 0x000E8328
		private void Init(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
		{
			if (rawSecurityDescriptor == null)
			{
				throw new ArgumentNullException("rawSecurityDescriptor");
			}
			SystemAcl systemAcl = null;
			if (rawSecurityDescriptor.SystemAcl != null)
			{
				systemAcl = new SystemAcl(isContainer, isDS, rawSecurityDescriptor.SystemAcl);
			}
			DiscretionaryAcl discretionaryAcl = null;
			if (rawSecurityDescriptor.DiscretionaryAcl != null)
			{
				discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, rawSecurityDescriptor.DiscretionaryAcl);
			}
			this.Init(isContainer, isDS, rawSecurityDescriptor.ControlFlags, rawSecurityDescriptor.Owner, rawSecurityDescriptor.Group, systemAcl, discretionaryAcl);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000EA18F File Offset: 0x000E838F
		private void Init(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.flags = flags & ~ControlFlags.SystemAclPresent;
			this.is_container = isContainer;
			this.is_ds = isDS;
			this.Owner = owner;
			this.Group = group;
			this.SystemAcl = systemAcl;
			this.DiscretionaryAcl = discretionaryAcl;
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x000EA1CC File Offset: 0x000E83CC
		public override ControlFlags ControlFlags
		{
			get
			{
				ControlFlags controlFlags = this.flags;
				controlFlags |= ControlFlags.DiscretionaryAclPresent;
				controlFlags |= ControlFlags.SelfRelative;
				if (this.SystemAcl != null)
				{
					controlFlags |= ControlFlags.SystemAclPresent;
				}
				return controlFlags;
			}
		}

		/// <summary>Gets or sets the discretionary access control list (DACL) for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. The DACL contains access rules.</summary>
		/// <returns>The DACL for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x000EA1FA File Offset: 0x000E83FA
		// (set) Token: 0x060041EB RID: 16875 RVA: 0x000EA204 File Offset: 0x000E8404
		public DiscretionaryAcl DiscretionaryAcl
		{
			get
			{
				return this.discretionary_acl;
			}
			set
			{
				if (value == null)
				{
					value = new DiscretionaryAcl(this.IsContainer, this.IsDS, 1);
					value.AddAccess(AccessControlType.Allow, new SecurityIdentifier("WD"), -1, this.IsContainer ? (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit) : InheritanceFlags.None, PropagationFlags.None);
					value.IsAefa = true;
				}
				this.CheckAclConsistency(value);
				this.discretionary_acl = value;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x000EA25C File Offset: 0x000E845C
		internal override GenericAcl InternalDacl
		{
			get
			{
				return this.DiscretionaryAcl;
			}
		}

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x000EA264 File Offset: 0x000E8464
		// (set) Token: 0x060041EE RID: 16878 RVA: 0x000EA26C File Offset: 0x000E846C
		public override SecurityIdentifier Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a container object.</summary>
		/// <returns>true if the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a container object; otherwise, false.</returns>
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x000EA275 File Offset: 0x000E8475
		public bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is in canonical order.</summary>
		/// <returns>true if the DACL associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is in canonical order; otherwise, false.</returns>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060041F0 RID: 16880 RVA: 0x000EA27D File Offset: 0x000E847D
		public bool IsDiscretionaryAclCanonical
		{
			get
			{
				return this.DiscretionaryAcl.IsCanonical;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a directory object.</summary>
		/// <returns>true if the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is a directory object; otherwise, false.</returns>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060041F1 RID: 16881 RVA: 0x000EA28A File Offset: 0x000E848A
		public bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is in canonical order.</summary>
		/// <returns>true if the SACL associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object is in canonical order; otherwise, false.</returns>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060041F2 RID: 16882 RVA: 0x000EA292 File Offset: 0x000E8492
		public bool IsSystemAclCanonical
		{
			get
			{
				return this.SystemAcl == null || this.SystemAcl.IsCanonical;
			}
		}

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x000EA2A9 File Offset: 0x000E84A9
		// (set) Token: 0x060041F4 RID: 16884 RVA: 0x000EA2B1 File Offset: 0x000E84B1
		public override SecurityIdentifier Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		/// <summary>Gets or sets the System Access Control List (SACL) for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. The SACL contains audit rules.</summary>
		/// <returns>The SACL for this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</returns>
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x000EA2BA File Offset: 0x000E84BA
		// (set) Token: 0x060041F6 RID: 16886 RVA: 0x000EA2C2 File Offset: 0x000E84C2
		public SystemAcl SystemAcl
		{
			get
			{
				return this.system_acl;
			}
			set
			{
				if (value != null)
				{
					this.CheckAclConsistency(value);
				}
				this.system_acl = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x000EA2D5 File Offset: 0x000E84D5
		internal override GenericAcl InternalSacl
		{
			get
			{
				return this.SystemAcl;
			}
		}

		/// <summary>Removes all access rules for the specified security identifier from the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <param name="sid">The security identifier for which to remove access rules.</param>
		// Token: 0x060041F8 RID: 16888 RVA: 0x000EA2DD File Offset: 0x000E84DD
		public void PurgeAccessControl(SecurityIdentifier sid)
		{
			this.DiscretionaryAcl.Purge(sid);
		}

		/// <summary>Removes all audit rules for the specified security identifier from the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object.</summary>
		/// <param name="sid">The security identifier for which to remove audit rules.</param>
		// Token: 0x060041F9 RID: 16889 RVA: 0x000EA2EB File Offset: 0x000E84EB
		public void PurgeAudit(SecurityIdentifier sid)
		{
			if (this.SystemAcl != null)
			{
				this.SystemAcl.Purge(sid);
			}
		}

		/// <summary>Sets the inheritance protection for the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. DACLs that are protected do not inherit access rules from parent containers.</summary>
		/// <param name="isProtected">true to protect the DACL from inheritance.</param>
		/// <param name="preserveInheritance">true to keep inherited access rules in the DACL; false to remove inherited access rules from the DACL.</param>
		// Token: 0x060041FA RID: 16890 RVA: 0x000EA304 File Offset: 0x000E8504
		public void SetDiscretionaryAclProtection(bool isProtected, bool preserveInheritance)
		{
			this.DiscretionaryAcl.IsAefa = false;
			if (!isProtected)
			{
				this.flags &= ~ControlFlags.DiscretionaryAclProtected;
				return;
			}
			this.flags |= ControlFlags.DiscretionaryAclProtected;
			if (!preserveInheritance)
			{
				this.DiscretionaryAcl.RemoveInheritedAces();
			}
		}

		/// <summary>Sets the inheritance protection for the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> object. SACLs that are protected do not inherit audit rules from parent containers.</summary>
		/// <param name="isProtected">true to protect the SACL from inheritance.</param>
		/// <param name="preserveInheritance">true to keep inherited audit rules in the SACL; false to remove inherited audit rules from the SACL.</param>
		// Token: 0x060041FB RID: 16891 RVA: 0x000EA353 File Offset: 0x000E8553
		public void SetSystemAclProtection(bool isProtected, bool preserveInheritance)
		{
			if (!isProtected)
			{
				this.flags &= ~ControlFlags.SystemAclProtected;
				return;
			}
			this.flags |= ControlFlags.SystemAclProtected;
			if (!preserveInheritance && this.SystemAcl != null)
			{
				this.SystemAcl.RemoveInheritedAces();
			}
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000EA393 File Offset: 0x000E8593
		public void AddDiscretionaryAcl(byte revision, int trusted)
		{
			this.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, revision, trusted);
			this.flags |= ControlFlags.DiscretionaryAclPresent;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000EA3BC File Offset: 0x000E85BC
		public void AddSystemAcl(byte revision, int trusted)
		{
			this.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, revision, trusted);
			this.flags |= ControlFlags.SystemAclPresent;
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000EA3E6 File Offset: 0x000E85E6
		private void CheckAclConsistency(CommonAcl acl)
		{
			if (this.IsContainer != acl.IsContainer)
			{
				throw new ArgumentException("IsContainer must match between descriptor and ACL.");
			}
			if (this.IsDS != acl.IsDS)
			{
				throw new ArgumentException("IsDS must match between descriptor and ACL.");
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x000EA41A File Offset: 0x000E861A
		internal override bool DaclIsUnmodifiedAefa
		{
			get
			{
				return this.DiscretionaryAcl.IsAefa;
			}
		}

		// Token: 0x0400216A RID: 8554
		private bool is_container;

		// Token: 0x0400216B RID: 8555
		private bool is_ds;

		// Token: 0x0400216C RID: 8556
		private ControlFlags flags;

		// Token: 0x0400216D RID: 8557
		private SecurityIdentifier owner;

		// Token: 0x0400216E RID: 8558
		private SecurityIdentifier group;

		// Token: 0x0400216F RID: 8559
		private SystemAcl system_acl;

		// Token: 0x04002170 RID: 8560
		private DiscretionaryAcl discretionary_acl;
	}
}
