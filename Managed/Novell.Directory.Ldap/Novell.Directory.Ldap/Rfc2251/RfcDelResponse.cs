using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000065 RID: 101
	public class RfcDelResponse : RfcLdapResult
	{
		// Token: 0x0600036F RID: 879 RVA: 0x000109C6 File Offset: 0x0000EBC6
		[CLSCompliant(false)]
		public RfcDelResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000109D1 File Offset: 0x0000EBD1
		public RfcDelResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000109DE File Offset: 0x0000EBDE
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 11);
		}
	}
}
