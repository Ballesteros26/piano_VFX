using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000069 RID: 105
	public class RfcIntermediateResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00011B2C File Offset: 0x0000FD2C
		[CLSCompliant(false)]
		public RfcIntermediateResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			this.m_responseNameIndex = (this.m_responseValueIndex = 0);
			int i;
			if (base.size() >= 3)
			{
				i = 3;
			}
			else
			{
				i = 0;
			}
			while (i < base.size())
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(i);
				int tag = asn1Tagged.getIdentifier().Tag;
				if (tag != 0)
				{
					if (tag == 1)
					{
						base.set_Renamed(i, asn1Tagged.taggedValue());
						this.m_responseValueIndex = i;
					}
				}
				else
				{
					base.set_Renamed(i, new RfcLdapOID(((Asn1OctetString)asn1Tagged.taggedValue()).byteValue()));
					this.m_responseNameIndex = i;
				}
				i++;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011BCD File Offset: 0x0000FDCD
		public Asn1Enumerated getResultCode()
		{
			if (base.size() > 3)
			{
				return (Asn1Enumerated)base.get_Renamed(0);
			}
			return null;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011BE6 File Offset: 0x0000FDE6
		public RfcLdapDN getMatchedDN()
		{
			if (base.size() > 3)
			{
				return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
			}
			return null;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011C09 File Offset: 0x0000FE09
		public RfcLdapString getErrorMessage()
		{
			if (base.size() > 3)
			{
				return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
			}
			return null;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public RfcReferral getReferral()
		{
			if (base.size() <= 3)
			{
				return null;
			}
			return (RfcReferral)base.get_Renamed(3);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011C45 File Offset: 0x0000FE45
		public RfcLdapOID getResponseName()
		{
			if (this.m_responseNameIndex < 0)
			{
				return null;
			}
			return (RfcLdapOID)base.get_Renamed(this.m_responseNameIndex);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00011C63 File Offset: 0x0000FE63
		public Asn1OctetString getResponse()
		{
			if (this.m_responseValueIndex == 0)
			{
				return null;
			}
			return (Asn1OctetString)base.get_Renamed(this.m_responseValueIndex);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011C80 File Offset: 0x0000FE80
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 25);
		}

		// Token: 0x04000243 RID: 579
		public const int TAG_RESPONSE_NAME = 0;

		// Token: 0x04000244 RID: 580
		public const int TAG_RESPONSE = 1;

		// Token: 0x04000245 RID: 581
		private int m_referralIndex;

		// Token: 0x04000246 RID: 582
		private int m_responseNameIndex;

		// Token: 0x04000247 RID: 583
		private int m_responseValueIndex;
	}
}
