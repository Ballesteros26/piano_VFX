using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007D RID: 125
	public class RfcSearchResultDone : RfcLdapResult
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x000125B9 File Offset: 0x000107B9
		[CLSCompliant(false)]
		public RfcSearchResultDone(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000125C4 File Offset: 0x000107C4
		public RfcSearchResultDone(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000125D1 File Offset: 0x000107D1
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 5);
		}
	}
}
