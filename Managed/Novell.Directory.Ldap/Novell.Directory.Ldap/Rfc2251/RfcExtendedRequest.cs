using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000066 RID: 102
	public class RfcExtendedRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000109E9 File Offset: 0x0000EBE9
		public RfcExtendedRequest(RfcLdapOID requestName)
			: this(requestName, null)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000109F3 File Offset: 0x0000EBF3
		public RfcExtendedRequest(RfcLdapOID requestName, Asn1OctetString requestValue)
			: base(2)
		{
			base.add(new Asn1Tagged(new Asn1Identifier(2, false, 0), requestName, false));
			if (requestValue != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), requestValue, false));
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00010A29 File Offset: 0x0000EC29
		public RfcExtendedRequest(Asn1Object[] origRequest)
			: base(origRequest, origRequest.Length)
		{
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00010A35 File Offset: 0x0000EC35
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 23);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00010A40 File Offset: 0x0000EC40
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcExtendedRequest(base.toArray());
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010A4D File Offset: 0x0000EC4D
		public string getRequestDN()
		{
			return null;
		}

		// Token: 0x0400022C RID: 556
		public const int REQUEST_NAME = 0;

		// Token: 0x0400022D RID: 557
		public const int REQUEST_VALUE = 1;
	}
}
