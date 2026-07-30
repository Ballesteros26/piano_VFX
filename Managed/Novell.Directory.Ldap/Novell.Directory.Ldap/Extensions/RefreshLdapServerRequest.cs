using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000099 RID: 153
	public class RefreshLdapServerRequest : LdapExtendedOperation
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00013A8C File Offset: 0x00011C8C
		public RefreshLdapServerRequest()
			: base("2.16.840.1.113719.1.27.100.9", null)
		{
		}
	}
}
