using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000BD RID: 189
	public class ConnectionStateEventData : BaseEdirEventData
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001516E File Offset: 0x0001336E
		public string ConnectionDN
		{
			get
			{
				return this.strConnectionDN;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00015176 File Offset: 0x00013376
		public int OldFlags
		{
			get
			{
				return this.old_flags;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001517E File Offset: 0x0001337E
		public int NewFlags
		{
			get
			{
				return this.new_flags;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00015186 File Offset: 0x00013386
		public string SourceModule
		{
			get
			{
				return this.source_module;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00015190 File Offset: 0x00013390
		public ConnectionStateEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strConnectionDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.old_flags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.new_flags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.source_module = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001523C File Offset: 0x0001343C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ConnectionStateEvent");
			stringBuilder.AppendFormat("(ConnectionDN={0})", this.strConnectionDN);
			stringBuilder.AppendFormat("(oldFlags={0})", this.old_flags);
			stringBuilder.AppendFormat("(newFlags={0})", this.new_flags);
			stringBuilder.AppendFormat("(SourceModule={0})", this.source_module);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400043B RID: 1083
		protected string strConnectionDN;

		// Token: 0x0400043C RID: 1084
		protected int old_flags;

		// Token: 0x0400043D RID: 1085
		protected int new_flags;

		// Token: 0x0400043E RID: 1086
		protected string source_module;
	}
}
