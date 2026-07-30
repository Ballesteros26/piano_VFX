using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000067 RID: 103
	public class RfcExtendedResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00010A50 File Offset: 0x0000EC50
		public virtual RfcLdapOID ResponseName
		{
			get
			{
				if (this.responseNameIndex == 0)
				{
					return null;
				}
				return (RfcLdapOID)base.get_Renamed(this.responseNameIndex);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00010A6D File Offset: 0x0000EC6D
		[CLSCompliant(false)]
		public virtual Asn1OctetString Response
		{
			get
			{
				if (this.responseIndex == 0)
				{
					return null;
				}
				return (Asn1OctetString)base.get_Renamed(this.responseIndex);
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010A8C File Offset: 0x0000EC8C
		[CLSCompliant(false)]
		public RfcExtendedResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			if (base.size() > 3)
			{
				for (int i = 3; i < base.size(); i++)
				{
					Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(i);
					int tag = asn1Tagged.getIdentifier().Tag;
					if (tag != 3)
					{
						if (tag != 10)
						{
							if (tag == 11)
							{
								base.set_Renamed(i, asn1Tagged.taggedValue());
								this.responseIndex = i;
							}
						}
						else
						{
							base.set_Renamed(i, new RfcLdapOID(((Asn1OctetString)asn1Tagged.taggedValue()).byteValue()));
							this.responseNameIndex = i;
						}
					}
					else
					{
						sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
						MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
						base.set_Renamed(i, new RfcReferral(dec, memoryStream, array.Length));
						this.referralIndex = i;
					}
				}
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00010B60 File Offset: 0x0000ED60
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00010B6E File Offset: 0x0000ED6E
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010B86 File Offset: 0x0000ED86
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010B9E File Offset: 0x0000ED9E
		public RfcReferral getReferral()
		{
			if (this.referralIndex == 0)
			{
				return null;
			}
			return (RfcReferral)base.get_Renamed(this.referralIndex);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00010BBB File Offset: 0x0000EDBB
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 24);
		}

		// Token: 0x0400022E RID: 558
		public const int RESPONSE_NAME = 10;

		// Token: 0x0400022F RID: 559
		public const int RESPONSE = 11;

		// Token: 0x04000230 RID: 560
		private int referralIndex;

		// Token: 0x04000231 RID: 561
		private int responseNameIndex;

		// Token: 0x04000232 RID: 562
		private int responseIndex;
	}
}
