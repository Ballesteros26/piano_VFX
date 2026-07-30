using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000020 RID: 32
	public class LdapExtendedRequest : LdapMessage
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007BBC File Offset: 0x00005DBC
		public virtual LdapExtendedOperation ExtendedOperation
		{
			get
			{
				RfcExtendedRequest rfcExtendedRequest = (RfcExtendedRequest)this.Asn1Object.get_Renamed(1);
				string text = ((RfcLdapOID)((Asn1Tagged)rfcExtendedRequest.get_Renamed(0)).taggedValue()).stringValue();
				sbyte[] array = null;
				if (rfcExtendedRequest.size() >= 2)
				{
					array = ((Asn1OctetString)((Asn1Tagged)rfcExtendedRequest.get_Renamed(1)).taggedValue()).byteValue();
				}
				return new LdapExtendedOperation(text, array);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007C23 File Offset: 0x00005E23
		public LdapExtendedRequest(LdapExtendedOperation op, LdapControl[] cont)
			: base(23, new RfcExtendedRequest(new RfcLdapOID(op.getID()), (op.getValue() != null) ? new Asn1OctetString(op.getValue()) : null), cont)
		{
		}
	}
}
