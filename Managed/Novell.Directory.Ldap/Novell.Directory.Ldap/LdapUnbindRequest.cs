using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003A RID: 58
	public class LdapUnbindRequest : LdapMessage
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000AEEE File Offset: 0x000090EE
		public LdapUnbindRequest(LdapControl[] cont)
			: base(2, new RfcUnbindRequest(), cont)
		{
		}
	}
}
