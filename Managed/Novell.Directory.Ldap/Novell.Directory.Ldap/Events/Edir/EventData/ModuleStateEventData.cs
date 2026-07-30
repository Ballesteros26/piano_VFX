using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000C2 RID: 194
	public class ModuleStateEventData : BaseEdirEventData
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00015D31 File Offset: 0x00013F31
		public string ConnectionDN
		{
			get
			{
				return this.strConnectionDN;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00015D39 File Offset: 0x00013F39
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00015D41 File Offset: 0x00013F41
		public string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00015D49 File Offset: 0x00013F49
		public string Description
		{
			get
			{
				return this.strDescription;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00015D51 File Offset: 0x00013F51
		public string Source
		{
			get
			{
				return this.strSource;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00015D5C File Offset: 0x00013F5C
		public ModuleStateEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strConnectionDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strName = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strDescription = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strSource = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00015E28 File Offset: 0x00014028
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ModuleStateEvent");
			stringBuilder.AppendFormat("(connectionDN={0})", this.strConnectionDN);
			stringBuilder.AppendFormat("(flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(Name={0})", this.strName);
			stringBuilder.AppendFormat("(Description={0})", this.strDescription);
			stringBuilder.AppendFormat("(Source={0})", this.strSource);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000456 RID: 1110
		protected string strConnectionDN;

		// Token: 0x04000457 RID: 1111
		protected int nFlags;

		// Token: 0x04000458 RID: 1112
		protected string strName;

		// Token: 0x04000459 RID: 1113
		protected string strDescription;

		// Token: 0x0400045A RID: 1114
		protected string strSource;
	}
}
