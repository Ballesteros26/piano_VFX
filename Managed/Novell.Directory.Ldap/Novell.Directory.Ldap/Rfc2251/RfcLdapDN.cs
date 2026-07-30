using System;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006A RID: 106
	public class RfcLdapDN : RfcLdapString
	{
		// Token: 0x0600039D RID: 925 RVA: 0x00011C8B File Offset: 0x0000FE8B
		public RfcLdapDN(string s)
			: base(s)
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00011C94 File Offset: 0x0000FE94
		[CLSCompliant(false)]
		public RfcLdapDN(sbyte[] s)
			: base(s)
		{
		}
	}
}
