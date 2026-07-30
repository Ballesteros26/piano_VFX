using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000C6 RID: 198
	public class ValueEventData : BaseEdirEventData
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00016193 File Offset: 0x00014393
		public string Attribute
		{
			get
			{
				return this.strAttribute;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001619B File Offset: 0x0001439B
		public string ClassId
		{
			get
			{
				return this.strClassId;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x000161A3 File Offset: 0x000143A3
		public string Data
		{
			get
			{
				return this.strData;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000161AB File Offset: 0x000143AB
		public byte[] BinaryData
		{
			get
			{
				return this.binData;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x000161B3 File Offset: 0x000143B3
		public string Entry
		{
			get
			{
				return this.strEntry;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000161BB File Offset: 0x000143BB
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000161C3 File Offset: 0x000143C3
		public string Syntax
		{
			get
			{
				return this.strSyntax;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000161CB File Offset: 0x000143CB
		public DSETimeStamp TimeStamp
		{
			get
			{
				return this.timeStampObj;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000161D3 File Offset: 0x000143D3
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000161DC File Offset: 0x000143DC
		public ValueEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strEntry = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strAttribute = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strSyntax = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strClassId = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.timeStampObj = new DSETimeStamp((Asn1Sequence)this.decoder.decode(this.decodedData, array));
			Asn1OctetString asn1OctetString = (Asn1OctetString)this.decoder.decode(this.decodedData, array);
			this.strData = asn1OctetString.stringValue();
			this.binData = SupportClass.ToByteArray(asn1OctetString.byteValue());
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			base.DataInitDone();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00016324 File Offset: 0x00014524
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ValueEventData");
			stringBuilder.AppendFormat("(Attribute={0})", this.strAttribute);
			stringBuilder.AppendFormat("(Classid={0})", this.strClassId);
			stringBuilder.AppendFormat("(Data={0})", this.strData);
			stringBuilder.AppendFormat("(Data={0})", this.binData);
			stringBuilder.AppendFormat("(Entry={0})", this.strEntry);
			stringBuilder.AppendFormat("(Perpetrator={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Syntax={0})", this.strSyntax);
			stringBuilder.AppendFormat("(TimeStamp={0})", this.timeStampObj);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000464 RID: 1124
		protected string strAttribute;

		// Token: 0x04000465 RID: 1125
		protected string strClassId;

		// Token: 0x04000466 RID: 1126
		protected string strData;

		// Token: 0x04000467 RID: 1127
		protected byte[] binData;

		// Token: 0x04000468 RID: 1128
		protected string strEntry;

		// Token: 0x04000469 RID: 1129
		protected string strPerpetratorDN;

		// Token: 0x0400046A RID: 1130
		protected string strSyntax;

		// Token: 0x0400046B RID: 1131
		protected DSETimeStamp timeStampObj;

		// Token: 0x0400046C RID: 1132
		protected int nVerb;
	}
}
