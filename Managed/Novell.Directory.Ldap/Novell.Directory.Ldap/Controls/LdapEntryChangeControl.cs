using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000C7 RID: 199
	public class LdapEntryChangeControl : LdapControl
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x000163FA File Offset: 0x000145FA
		public virtual bool HasChangeNumber
		{
			get
			{
				return this.m_hasChangeNumber;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00016402 File Offset: 0x00014602
		public virtual int ChangeNumber
		{
			get
			{
				return this.m_changeNumber;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0001640A File Offset: 0x0001460A
		public virtual int ChangeType
		{
			get
			{
				return this.m_changeType;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00016412 File Offset: 0x00014612
		public virtual string PreviousDN
		{
			get
			{
				return this.m_previousDN;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001641C File Offset: 0x0001461C
		[CLSCompliant(false)]
		public LdapEntryChangeControl(string oid, bool critical, sbyte[] value_Renamed)
			: base(oid, critical, value_Renamed)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error.");
			}
			Asn1Object asn1Object = lberdecoder.decode(value_Renamed);
			if (asn1Object == null || !(asn1Object is Asn1Sequence))
			{
				throw new IOException("Decoding error.");
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)asn1Object;
			Asn1Object asn1Object2 = asn1Sequence.get_Renamed(0);
			if (asn1Object2 == null || !(asn1Object2 is Asn1Enumerated))
			{
				throw new IOException("Decoding error.");
			}
			this.m_changeType = ((Asn1Enumerated)asn1Object2).intValue();
			if (asn1Sequence.size() > 1 && this.m_changeType == 8)
			{
				asn1Object2 = asn1Sequence.get_Renamed(1);
				if (asn1Object2 == null || !(asn1Object2 is Asn1OctetString))
				{
					throw new IOException("Decoding error get previous DN");
				}
				this.m_previousDN = ((Asn1OctetString)asn1Object2).stringValue();
			}
			else
			{
				this.m_previousDN = "";
			}
			if (asn1Sequence.size() != 3)
			{
				this.m_hasChangeNumber = false;
				return;
			}
			asn1Object2 = asn1Sequence.get_Renamed(2);
			if (asn1Object2 == null || !(asn1Object2 is Asn1Integer))
			{
				throw new IOException("Decoding error getting change number");
			}
			this.m_changeNumber = ((Asn1Integer)asn1Object2).intValue();
			this.m_hasChangeNumber = true;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001652D File Offset: 0x0001472D
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0400046D RID: 1133
		private int m_changeType;

		// Token: 0x0400046E RID: 1134
		private string m_previousDN;

		// Token: 0x0400046F RID: 1135
		private bool m_hasChangeNumber;

		// Token: 0x04000470 RID: 1136
		private int m_changeNumber;
	}
}
