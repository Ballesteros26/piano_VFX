using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200000F RID: 15
	public interface LdapAuthHandler : LdapReferralHandler
	{
		// Token: 0x0600009E RID: 158
		LdapAuthProvider getAuthProvider(string host, int port);
	}
}
