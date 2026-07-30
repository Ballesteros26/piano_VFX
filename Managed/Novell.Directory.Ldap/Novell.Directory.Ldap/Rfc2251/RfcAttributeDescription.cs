using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000057 RID: 87
	public class RfcAttributeDescription : RfcLdapString
	{
		// Token: 0x06000332 RID: 818 RVA: 0x00010421 File Offset: 0x0000E621
		public RfcAttributeDescription(string s)
			: base(s)
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001042A File Offset: 0x0000E62A
		[CLSCompliant(false)]
		public RfcAttributeDescription(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}
	}
}
