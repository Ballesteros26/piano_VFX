using System;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.ReferralChasingOption" /> enumeration specifies if and how referral chasing is pursued.          </summary>
	// Token: 0x02000029 RID: 41
	[Serializable]
	public enum ReferralChasingOption
	{
		/// <summary>Chase referrals of either the subordinate or external type.</summary>
		// Token: 0x0400009E RID: 158
		All = 96,
		/// <summary>Chase external referrals.  If no referral chasing option is specified for a directory search, the type of referral chasing performed is  <see cref="F:System.DirectoryServices.ReferralChasingOption.External" />.</summary>
		// Token: 0x0400009F RID: 159
		External = 64,
		/// <summary>Never chase the referred-to server. Setting this option prevents a client from contacting other servers in a referral process.</summary>
		// Token: 0x040000A0 RID: 160
		None = 0,
		/// <summary>Chase only subordinate referrals that are a subordinate naming context in a directory tree. The ADSI LDAP provider always turns off this flag for paged searches.</summary>
		// Token: 0x040000A1 RID: 161
		Subordinate = 32
	}
}
