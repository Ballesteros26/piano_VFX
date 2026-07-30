using System;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000079 RID: 121
	public interface RfcRequest
	{
		// Token: 0x060003DD RID: 989
		RfcRequest dupRequest(string base_Renamed, string filter, bool reference);

		// Token: 0x060003DE RID: 990
		string getRequestDN();
	}
}
