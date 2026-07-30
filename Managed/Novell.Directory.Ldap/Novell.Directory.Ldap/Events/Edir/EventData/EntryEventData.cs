using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000C0 RID: 192
	public class EntryEventData : BaseEdirEventData
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00015766 File Offset: 0x00013966
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001576E File Offset: 0x0001396E
		public string Entry
		{
			get
			{
				return this.strEntry;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00015776 File Offset: 0x00013976
		public string NewDN
		{
			get
			{
				return this.strNewDN;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0001577E File Offset: 0x0001397E
		public string ClassId
		{
			get
			{
				return this.strClassId;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00015786 File Offset: 0x00013986
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001578E File Offset: 0x0001398E
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00015796 File Offset: 0x00013996
		public DSETimeStamp TimeStamp
		{
			get
			{
				return this.timeStampObj;
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000157A0 File Offset: 0x000139A0
		public EntryEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strEntry = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strClassId = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.timeStampObj = new DSETimeStamp((Asn1Sequence)this.decoder.decode(this.decodedData, array));
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strNewDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000158B0 File Offset: 0x00013AB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntryEventData[");
			stringBuilder.AppendFormat("(Entry={0})", this.strEntry);
			stringBuilder.AppendFormat("(Prepetrator={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(ClassId={0})", this.strClassId);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(Flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(NewDN={0})", this.strNewDN);
			stringBuilder.AppendFormat("(TimeStamp={0})", this.timeStampObj);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000448 RID: 1096
		protected string strPerpetratorDN;

		// Token: 0x04000449 RID: 1097
		protected string strEntry;

		// Token: 0x0400044A RID: 1098
		protected string strNewDN;

		// Token: 0x0400044B RID: 1099
		protected string strClassId;

		// Token: 0x0400044C RID: 1100
		protected int nVerb;

		// Token: 0x0400044D RID: 1101
		protected int nFlags;

		// Token: 0x0400044E RID: 1102
		protected DSETimeStamp timeStampObj;
	}
}
