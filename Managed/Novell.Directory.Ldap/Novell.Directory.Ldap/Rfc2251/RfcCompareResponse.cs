using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000061 RID: 97
	public class RfcCompareResponse : RfcLdapResult
	{
		// Token: 0x06000359 RID: 857 RVA: 0x000107B0 File Offset: 0x0000E9B0
		[CLSCompliant(false)]
		public RfcCompareResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000107BB File Offset: 0x0000E9BB
		public RfcCompareResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000107C8 File Offset: 0x0000E9C8
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 15);
		}
	}
}
