using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000CB RID: 203
	public class LdapSortResponse : LdapControl
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001692E File Offset: 0x00014B2E
		public virtual string FailedAttribute
		{
			get
			{
				return this.failedAttribute;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00016936 File Offset: 0x00014B36
		public virtual int ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00016940 File Offset: 0x00014B40
		[CLSCompliant(false)]
		public LdapSortResponse(string oid, bool critical, sbyte[] values)
			: base(oid, critical, values)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object = lberdecoder.decode(values);
			if (asn1Object == null || !(asn1Object is Asn1Sequence))
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object2 = ((Asn1Sequence)asn1Object).get_Renamed(0);
			if (asn1Object2 != null && asn1Object2 is Asn1Enumerated)
			{
				this.resultCode = ((Asn1Enumerated)asn1Object2).intValue();
			}
			if (((Asn1Sequence)asn1Object).size() > 1)
			{
				Asn1Object asn1Object3 = ((Asn1Sequence)asn1Object).get_Renamed(1);
				if (asn1Object3 != null && asn1Object3 is Asn1OctetString)
				{
					this.failedAttribute = ((Asn1OctetString)asn1Object3).stringValue();
				}
			}
		}

		// Token: 0x04000488 RID: 1160
		private string failedAttribute;

		// Token: 0x04000489 RID: 1161
		private int resultCode;
	}
}
