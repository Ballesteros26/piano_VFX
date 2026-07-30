using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003B RID: 59
	public interface LdapUnsolicitedNotificationListener
	{
		// Token: 0x06000245 RID: 581
		void messageReceived(LdapExtendedResponse msg);
	}
}
