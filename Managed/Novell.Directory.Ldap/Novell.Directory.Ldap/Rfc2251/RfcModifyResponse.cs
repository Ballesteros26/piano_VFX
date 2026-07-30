using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000076 RID: 118
	public class RfcModifyResponse : RfcLdapResult
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x00012486 File Offset: 0x00010686
		[CLSCompliant(false)]
		public RfcModifyResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012491 File Offset: 0x00010691
		public RfcModifyResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001249E File Offset: 0x0001069E
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 7);
		}
	}
}
