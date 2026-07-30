using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006C RID: 108
	public class RfcLdapOID : Asn1OctetString
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0001201E File Offset: 0x0001021E
		public RfcLdapOID(string s)
			: base(s)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012027 File Offset: 0x00010227
		[CLSCompliant(false)]
		public RfcLdapOID(sbyte[] s)
			: base(s)
		{
		}
	}
}
