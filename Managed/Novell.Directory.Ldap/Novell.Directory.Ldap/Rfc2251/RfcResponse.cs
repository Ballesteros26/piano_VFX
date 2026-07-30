using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007A RID: 122
	public interface RfcResponse
	{
		// Token: 0x060003DF RID: 991
		Asn1Enumerated getResultCode();

		// Token: 0x060003E0 RID: 992
		RfcLdapDN getMatchedDN();

		// Token: 0x060003E1 RID: 993
		RfcLdapString getErrorMessage();

		// Token: 0x060003E2 RID: 994
		RfcReferral getReferral();
	}
}
