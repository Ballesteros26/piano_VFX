using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000055 RID: 85
	public class RfcAddResponse : RfcLdapResult
	{
		// Token: 0x0600032E RID: 814 RVA: 0x000103F5 File Offset: 0x0000E5F5
		[CLSCompliant(false)]
		public RfcAddResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00010400 File Offset: 0x0000E600
		public RfcAddResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001040D File Offset: 0x0000E60D
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 9);
		}
	}
}
