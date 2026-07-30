using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000080 RID: 128
	public class RfcSubstringFilter : Asn1Sequence
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00012622 File Offset: 0x00010822
		public RfcSubstringFilter(RfcAttributeDescription type, Asn1SequenceOf substrings)
			: base(2)
		{
			base.add(type);
			base.add(substrings);
		}
	}
}
