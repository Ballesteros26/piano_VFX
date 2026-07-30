using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007F RID: 127
	public class RfcSearchResultReference : Asn1SequenceOf
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0001260C File Offset: 0x0001080C
		[CLSCompliant(false)]
		public RfcSearchResultReference(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00012617 File Offset: 0x00010817
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 19);
		}
	}
}
