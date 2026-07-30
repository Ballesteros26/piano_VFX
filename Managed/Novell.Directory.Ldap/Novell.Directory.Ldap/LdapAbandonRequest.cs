using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200000A RID: 10
	public class LdapAbandonRequest : LdapMessage
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003644 File Offset: 0x00001844
		public LdapAbandonRequest(int id, LdapControl[] cont)
			: base(16, new RfcAbandonRequest(id), cont)
		{
		}
	}
}
