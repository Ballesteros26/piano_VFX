using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006E RID: 110
	public class RfcLdapString : Asn1OctetString
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00012127 File Offset: 0x00010327
		public RfcLdapString(string s)
			: base(s)
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012130 File Offset: 0x00010330
		[CLSCompliant(false)]
		public RfcLdapString(sbyte[] ba)
			: base(ba)
		{
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00012139 File Offset: 0x00010339
		[CLSCompliant(false)]
		public RfcLdapString(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}
	}
}
