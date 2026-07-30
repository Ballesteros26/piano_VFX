using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000BB RID: 187
	public class BinderyObjectEventData : BaseEdirEventData
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00014E16 File Offset: 0x00013016
		public string EntryDN
		{
			get
			{
				return this.strEntryDN;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00014E1E File Offset: 0x0001301E
		public int ValueType
		{
			get
			{
				return this.nType;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00014E26 File Offset: 0x00013026
		public int EmuObjFlags
		{
			get
			{
				return this.nEmuObjFlags;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00014E2E File Offset: 0x0001302E
		public int Security
		{
			get
			{
				return this.nSecurity;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00014E36 File Offset: 0x00013036
		public string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00014E40 File Offset: 0x00013040
		public BinderyObjectEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strEntryDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.nType = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.nEmuObjFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.nSecurity = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strName = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00014F0C File Offset: 0x0001310C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BinderyObjectEvent");
			stringBuilder.AppendFormat("(EntryDn={0})", this.strEntryDN);
			stringBuilder.AppendFormat("(Type={0})", this.nType);
			stringBuilder.AppendFormat("(EnumOldFlags={0})", this.nEmuObjFlags);
			stringBuilder.AppendFormat("(Secuirty={0})", this.nSecurity);
			stringBuilder.AppendFormat("(Name={0})", this.strName);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000430 RID: 1072
		protected string strEntryDN;

		// Token: 0x04000431 RID: 1073
		protected int nType;

		// Token: 0x04000432 RID: 1074
		protected int nEmuObjFlags;

		// Token: 0x04000433 RID: 1075
		protected int nSecurity;

		// Token: 0x04000434 RID: 1076
		protected string strName;
	}
}
