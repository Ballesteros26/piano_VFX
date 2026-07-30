using System;
using System.Collections;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000BE RID: 190
	public class DebugEventData : BaseEdirEventData
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000152BD File Offset: 0x000134BD
		public int DSTime
		{
			get
			{
				return this.ds_time;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000152C5 File Offset: 0x000134C5
		public int MilliSeconds
		{
			get
			{
				return this.milli_seconds;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000152CD File Offset: 0x000134CD
		public string PerpetratorDN
		{
			get
			{
				return this.strPerpetratorDN;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x000152D5 File Offset: 0x000134D5
		public string FormatString
		{
			get
			{
				return this.strFormatString;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000152DD File Offset: 0x000134DD
		public int Verb
		{
			get
			{
				return this.nVerb;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x000152E5 File Offset: 0x000134E5
		public int ParameterCount
		{
			get
			{
				return this.parameter_count;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x000152ED File Offset: 0x000134ED
		public ArrayList Parameters
		{
			get
			{
				return this.parameter_collection;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000152F8 File Offset: 0x000134F8
		public DebugEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.ds_time = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.milli_seconds = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strPerpetratorDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strFormatString = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.nVerb = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.parameter_count = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.parameter_collection = new ArrayList();
			if (this.parameter_count > 0)
			{
				Asn1Sequence asn1Sequence = (Asn1Sequence)this.decoder.decode(this.decodedData, array);
				for (int i = 0; i < this.parameter_count; i++)
				{
					this.parameter_collection.Add(new DebugParameter((Asn1Tagged)asn1Sequence.get_Renamed(i)));
				}
			}
			base.DataInitDone();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00015440 File Offset: 0x00013640
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DebugEventData");
			stringBuilder.AppendFormat("(Millseconds={0})", this.milli_seconds);
			stringBuilder.AppendFormat("(DSTime={0})", this.ds_time);
			stringBuilder.AppendFormat("(PerpetratorDN={0})", this.strPerpetratorDN);
			stringBuilder.AppendFormat("(Verb={0})", this.nVerb);
			stringBuilder.AppendFormat("(ParameterCount={0})", this.parameter_count);
			for (int i = 0; i < this.parameter_count; i++)
			{
				stringBuilder.AppendFormat("(Parameter[{0}]={1})", i, this.parameter_collection[i]);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400043F RID: 1087
		protected int ds_time;

		// Token: 0x04000440 RID: 1088
		protected int milli_seconds;

		// Token: 0x04000441 RID: 1089
		protected string strPerpetratorDN;

		// Token: 0x04000442 RID: 1090
		protected string strFormatString;

		// Token: 0x04000443 RID: 1091
		protected int nVerb;

		// Token: 0x04000444 RID: 1092
		protected int parameter_count;

		// Token: 0x04000445 RID: 1093
		protected ArrayList parameter_collection;
	}
}
