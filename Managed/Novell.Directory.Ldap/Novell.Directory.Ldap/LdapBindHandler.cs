using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000011 RID: 17
	public interface LdapBindHandler : LdapReferralHandler
	{
		// Token: 0x060000A2 RID: 162
		LdapConnection Bind(string[] ldapurl, LdapConnection conn);
	}
}
