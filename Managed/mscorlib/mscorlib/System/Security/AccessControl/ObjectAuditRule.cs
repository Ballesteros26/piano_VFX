using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user's identity, an access mask, and audit conditions. An <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object also contains information about the type of object to which the rule applies, the type of child object that can inherit the rule, how the rule is inherited by child objects, and how that inheritance is propagated.</summary>
	// Token: 0x02000603 RID: 1539
	public abstract class ObjectAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> class.</summary>
		/// <param name="identity">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="objectType">The type of object to which the rule applies.</param>
		/// <param name="inheritedObjectType">The type of child object that can inherit the rule.</param>
		/// <param name="auditFlags">The audit conditions.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />, or the <paramref name="type" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is 0, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x06004337 RID: 17207 RVA: 0x000EC972 File Offset: 0x000EAB72
		protected ObjectAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AuditFlags auditFlags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
		{
			this.object_type = objectType;
			this.inherited_object_type = inheritedObjectType;
		}

		/// <summary>Gets the type of child object that can inherit the <see cref="System.Security.AccessControl.ObjectAuditRule" /> object.</summary>
		/// <returns>The type of child object that can inherit the <see cref="System.Security.AccessControl.ObjectAuditRule" /> object.</returns>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x000EC993 File Offset: 0x000EAB93
		public Guid InheritedObjectType
		{
			get
			{
				return this.inherited_object_type;
			}
		}

		/// <summary>
		///   <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> and <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> properties of the <see cref="System.Security.AccessControl.ObjectAuditRule" /> object contain valid values.</summary>
		/// <returns>
		///   <see cref="F:System.Security.AccessControl.ObjectAceFlags.ObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> property contains a valid value. <see cref="F:System.Security.AccessControl.ObjectAceFlags.InheritedObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> property contains a valid value. These values can be combined with a logical OR.</returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06004339 RID: 17209 RVA: 0x000EC99C File Offset: 0x000EAB9C
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				ObjectAceFlags objectAceFlags = ObjectAceFlags.None;
				if (this.object_type != Guid.Empty)
				{
					objectAceFlags |= ObjectAceFlags.ObjectAceTypePresent;
				}
				if (this.inherited_object_type != Guid.Empty)
				{
					objectAceFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
				}
				return objectAceFlags;
			}
		}

		/// <summary>Gets the type of object to which the <see cref="System.Security.AccessControl.ObjectAuditRule" /> applies.</summary>
		/// <returns>The type of object to which the <see cref="System.Security.AccessControl.ObjectAuditRule" /> applies.</returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x000EC9D8 File Offset: 0x000EABD8
		public Guid ObjectType
		{
			get
			{
				return this.object_type;
			}
		}

		// Token: 0x040021DF RID: 8671
		private Guid inherited_object_type;

		// Token: 0x040021E0 RID: 8672
		private Guid object_type;
	}
}
