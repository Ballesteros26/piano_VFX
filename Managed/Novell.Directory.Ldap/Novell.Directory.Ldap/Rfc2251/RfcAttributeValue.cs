using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200005B RID: 91
	public class RfcAttributeValue : Asn1OctetString
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0001049B File Offset: 0x0000E69B
		public RfcAttributeValue(string value_Renamed)
			: base(value_Renamed)
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000104A4 File Offset: 0x0000E6A4
		[CLSCompliant(false)]
		public RfcAttributeValue(sbyte[] value_Renamed)
			: base(value_Renamed)
		{
		}
	}
}
