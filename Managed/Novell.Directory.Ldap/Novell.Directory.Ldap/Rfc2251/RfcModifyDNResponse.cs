using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000074 RID: 116
	public class RfcModifyDNResponse : RfcLdapResult
	{
		// Token: 0x060003CF RID: 975 RVA: 0x000123F7 File Offset: 0x000105F7
		[CLSCompliant(false)]
		public RfcModifyDNResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00012402 File Offset: 0x00010602
		public RfcModifyDNResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001240F File Offset: 0x0001060F
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 13);
		}
	}
}
