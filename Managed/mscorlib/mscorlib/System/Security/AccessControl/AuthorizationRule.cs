using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Determines access to securable objects. The derived classes <see cref="T:System.Security.AccessControl.AccessRule" /> and <see cref="T:System.Security.AccessControl.AuditRule" /> offer specializations for access and audit functionality.</summary>
	// Token: 0x020005CF RID: 1487
	public abstract class AuthorizationRule
	{
		// Token: 0x06004183 RID: 16771 RVA: 0x00002111 File Offset: 0x00000311
		internal AuthorizationRule()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AuthorizationControl.AccessRule" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.  This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true to inherit this rule from a parent container.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is zero, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x06004184 RID: 16772 RVA: 0x000E8D70 File Offset: 0x000E6F70
		protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			if (!(identity is SecurityIdentifier) && !(identity is NTAccount))
			{
				throw new ArgumentException("identity");
			}
			if (accessMask == 0)
			{
				throw new ArgumentException("accessMask");
			}
			if ((inheritanceFlags & ~(InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit)) != InheritanceFlags.None)
			{
				throw new ArgumentOutOfRangeException();
			}
			if ((propagationFlags & ~(PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly)) != PropagationFlags.None)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.identity = identity;
			this.accessMask = accessMask;
			this.isInherited = isInherited;
			this.inheritanceFlags = inheritanceFlags;
			this.propagationFlags = propagationFlags;
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.IdentityReference" /> to which this rule applies.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IdentityReference" /> to which this rule applies.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x000E8DFF File Offset: 0x000E6FFF
		public IdentityReference IdentityReference
		{
			get
			{
				return this.identity;
			}
		}

		/// <summary>Gets the value of flags that determine how this rule is inherited by child objects.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x000E8E07 File Offset: 0x000E7007
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				return this.inheritanceFlags;
			}
		}

		/// <summary>Gets a value indicating whether this rule is explicitly set or is inherited from a parent container object.</summary>
		/// <returns>true if this rule is not explicitly set but is instead inherited from a parent container.</returns>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x000E8E0F File Offset: 0x000E700F
		public bool IsInherited
		{
			get
			{
				return this.isInherited;
			}
		}

		/// <summary>Gets the value of the propagation flags, which determine how inheritance of this rule is propagated to child objects. This property is significant only when the value of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> enumeration is not <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06004188 RID: 16776 RVA: 0x000E8E17 File Offset: 0x000E7017
		public PropagationFlags PropagationFlags
		{
			get
			{
				return this.propagationFlags;
			}
		}

		/// <summary>Gets the access mask for this rule.</summary>
		/// <returns>The access mask for this rule.</returns>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x000E8E1F File Offset: 0x000E701F
		protected internal int AccessMask
		{
			get
			{
				return this.accessMask;
			}
		}

		// Token: 0x0400214D RID: 8525
		private IdentityReference identity;

		// Token: 0x0400214E RID: 8526
		private int accessMask;

		// Token: 0x0400214F RID: 8527
		private bool isInherited;

		// Token: 0x04002150 RID: 8528
		private InheritanceFlags inheritanceFlags;

		// Token: 0x04002151 RID: 8529
		private PropagationFlags propagationFlags;
	}
}
