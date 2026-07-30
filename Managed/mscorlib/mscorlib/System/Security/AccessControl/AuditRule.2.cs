using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user’s identity and an access mask.</summary>
	/// <typeparam name="T"></typeparam>
	// Token: 0x020005CE RID: 1486
	public class AuditRule<T> : AuditRule where T : struct
	{
		/// <summary>Initializes a new instance of the AuditRule’1 class by using the specified values.</summary>
		/// <param name="identity">The identity to which the audit rule applies.</param>
		/// <param name="rights">The rights of the audit rule.</param>
		/// <param name="flags">The properties of the audit rule.</param>
		// Token: 0x0600417D RID: 16765 RVA: 0x000E8D13 File Offset: 0x000E6F13
		public AuditRule(string identity, T rights, AuditFlags flags)
			: this(new NTAccount(identity), rights, flags)
		{
		}

		/// <summary>Initializes a new instance of the AuditRule’1 class by using the specified values.</summary>
		/// <param name="identity">The identity to which this audit rule applies.</param>
		/// <param name="rights">The rights of the audit rule.</param>
		/// <param name="flags">The conditions for which the fule is audited.</param>
		// Token: 0x0600417E RID: 16766 RVA: 0x000E8D23 File Offset: 0x000E6F23
		public AuditRule(IdentityReference identity, T rights, AuditFlags flags)
			: this(identity, rights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		/// <summary>Initializes a new instance of the AuditRule’1 class by using the specified values.</summary>
		/// <param name="identity">The identity to which the audit rule applies.</param>
		/// <param name="rights">The rights of the audit rule.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Whether inherited audit rules are automatically propagated.</param>
		/// <param name="flags">The conditions for which the rule is audited.</param>
		// Token: 0x0600417F RID: 16767 RVA: 0x000E8D30 File Offset: 0x000E6F30
		public AuditRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(new NTAccount(identity), rights, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Initializes a new instance of the AuditRule’1 class by using the specified values.</summary>
		/// <param name="identity">The identity to which the audit rule applies. </param>
		/// <param name="rights">The rights of the audit rule.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Whether inherited audit rules are automatically propagated.</param>
		/// <param name="flags">The conditions for which the rule is audited.</param>
		// Token: 0x06004180 RID: 16768 RVA: 0x000E8D44 File Offset: 0x000E6F44
		public AuditRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(identity, (int)((object)rights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x000E8D5E File Offset: 0x000E6F5E
		internal AuditRule(IdentityReference identity, int rights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, rights, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>The rights of the audit rule.</summary>
		/// <returns>Returns <see cref="{0}" />.</returns>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x000E8C5D File Offset: 0x000E6E5D
		public T Rights
		{
			get
			{
				return (T)((object)base.AccessMask);
			}
		}
	}
}
