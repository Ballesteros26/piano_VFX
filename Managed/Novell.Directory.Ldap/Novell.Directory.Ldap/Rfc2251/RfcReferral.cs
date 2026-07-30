using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000077 RID: 119
	public class RfcReferral : Asn1SequenceOf
	{
		// Token: 0x060003DB RID: 987 RVA: 0x000124A8 File Offset: 0x000106A8
		[CLSCompliant(false)]
		public RfcReferral(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}
	}
}
