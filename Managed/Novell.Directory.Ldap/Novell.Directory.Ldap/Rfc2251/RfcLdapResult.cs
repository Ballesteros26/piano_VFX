using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006D RID: 109
	public class RfcLdapResult : Asn1Sequence, RfcResponse
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00012030 File Offset: 0x00010230
		public RfcLdapResult(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage)
			: this(resultCode, matchedDN, errorMessage, null)
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001203C File Offset: 0x0001023C
		public RfcLdapResult(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(4)
		{
			base.add(resultCode);
			base.add(matchedDN);
			base.add(errorMessage);
			if (referral != null)
			{
				base.add(referral);
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012068 File Offset: 0x00010268
		[CLSCompliant(false)]
		public RfcLdapResult(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			if (base.size() > 3)
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(3);
				if (asn1Tagged.getIdentifier().Tag == 3)
				{
					sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
					MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
					base.set_Renamed(3, new RfcReferral(dec, memoryStream, array.Length));
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000120D0 File Offset: 0x000102D0
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000120DE File Offset: 0x000102DE
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000120F6 File Offset: 0x000102F6
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001210E File Offset: 0x0001030E
		public RfcReferral getReferral()
		{
			if (base.size() <= 3)
			{
				return null;
			}
			return (RfcReferral)base.get_Renamed(3);
		}

		// Token: 0x0400024B RID: 587
		public const int REFERRAL = 3;
	}
}
