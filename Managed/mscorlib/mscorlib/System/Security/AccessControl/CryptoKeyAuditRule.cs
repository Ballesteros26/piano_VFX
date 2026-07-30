using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an audit rule for a cryptographic key. An audit rule represents a combination of a user's identity and an access mask. An audit rule also contains information about the how the rule is inherited by child objects, how that inheritance is propagated, and for what conditions it is audited.</summary>
	// Token: 0x020005DF RID: 1503
	public sealed class CryptoKeyAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> class using the specified values. </summary>
		/// <param name="identity">The identity to which the audit rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation for which this audit rule generates audits.</param>
		/// <param name="flags">The conditions that generate audits.</param>
		// Token: 0x06004209 RID: 16905 RVA: 0x000EA47E File Offset: 0x000E867E
		public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
			: base(identity, (int)cryptoKeyRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> class using the specified values. </summary>
		/// <param name="identity">The identity to which the audit rule applies.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation for which this audit rule generates audits.</param>
		/// <param name="flags">The conditions that generate audits.</param>
		// Token: 0x0600420A RID: 16906 RVA: 0x000EA48C File Offset: 0x000E868C
		public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
			: this(new NTAccount(identity), cryptoKeyRights, flags)
		{
		}

		/// <summary>Gets the cryptographic key operation for which this audit rule generates audits.</summary>
		/// <returns>The cryptographic key operation for which this audit rule generates audits.</returns>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x000EA476 File Offset: 0x000E8676
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return (CryptoKeyRights)base.AccessMask;
			}
		}
	}
}
