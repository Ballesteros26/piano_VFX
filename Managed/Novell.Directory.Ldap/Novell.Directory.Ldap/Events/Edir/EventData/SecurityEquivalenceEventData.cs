using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000C5 RID: 197
	public class SecurityEquivalenceEventData : BaseEdirEventData
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00015FCE File Offset: 0x000141CE
		public string EntryDN
		{
			get
			{
				return this.strEntryDN;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00015FD6 File Offset: 0x000141D6
		public int RetryCount
		{
			get
			{
				return this.retry_count;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00015FDE File Offset: 0x000141DE
		public string ValueDN
		{
			get
			{
				return this.strValueDN;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00015FE6 File Offset: 0x000141E6
		public int ReferralCount
		{
			get
			{
				return this.referral_count;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00015FEE File Offset: 0x000141EE
		public ArrayList ReferralList
		{
			get
			{
				return this.referral_list;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00015FF8 File Offset: 0x000141F8
		public SecurityEquivalenceEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strEntryDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.retry_count = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strValueDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			Asn1Sequence asn1Sequence = (Asn1Sequence)this.decoder.decode(this.decodedData, array);
			this.referral_count = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			this.referral_list = new ArrayList();
			if (this.referral_count > 0)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Sequence.get_Renamed(1);
				for (int i = 0; i < this.referral_count; i++)
				{
					this.referral_list.Add(new ReferralAddress((Asn1Sequence)asn1Sequence2.get_Renamed(i)));
				}
			}
			base.DataInitDone();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00016100 File Offset: 0x00014300
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SecurityEquivalenceEventData");
			stringBuilder.AppendFormat("(EntryDN={0})", this.strEntryDN);
			stringBuilder.AppendFormat("(RetryCount={0})", this.retry_count);
			stringBuilder.AppendFormat("(valueDN={0})", this.strValueDN);
			stringBuilder.AppendFormat("(referralCount={0})", this.referral_count);
			stringBuilder.AppendFormat("(Referral Lists={0})", this.referral_list);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400045F RID: 1119
		protected string strEntryDN;

		// Token: 0x04000460 RID: 1120
		protected int retry_count;

		// Token: 0x04000461 RID: 1121
		protected string strValueDN;

		// Token: 0x04000462 RID: 1122
		protected int referral_count;

		// Token: 0x04000463 RID: 1123
		protected ArrayList referral_list;
	}
}
