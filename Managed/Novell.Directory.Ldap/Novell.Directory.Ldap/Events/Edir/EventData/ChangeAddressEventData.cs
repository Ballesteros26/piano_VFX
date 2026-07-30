using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000BC RID: 188
	public class ChangeAddressEventData : BaseEdirEventData
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00014FA4 File Offset: 0x000131A4
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00014FAC File Offset: 0x000131AC
		public int Proto
		{
			get
			{
				return this.nProto;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00014FB4 File Offset: 0x000131B4
		public int AddressFamily
		{
			get
			{
				return this.address_family;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00014FBC File Offset: 0x000131BC
		public string Address
		{
			get
			{
				return this.strAddress;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00014FC4 File Offset: 0x000131C4
		public string PstkName
		{
			get
			{
				return this.pstk_name;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00014FCC File Offset: 0x000131CC
		public string SourceModule
		{
			get
			{
				return this.source_module;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014FD4 File Offset: 0x000131D4
		public ChangeAddressEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.nProto = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.address_family = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strAddress = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.pstk_name = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.source_module = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000150C4 File Offset: 0x000132C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ChangeAddresssEvent");
			stringBuilder.AppendFormat("(flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(proto={0})", this.nProto);
			stringBuilder.AppendFormat("(addrFamily={0})", this.address_family);
			stringBuilder.AppendFormat("(address={0})", this.strAddress);
			stringBuilder.AppendFormat("(pstkName={0})", this.pstk_name);
			stringBuilder.AppendFormat("(source={0})", this.source_module);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000435 RID: 1077
		protected int nFlags;

		// Token: 0x04000436 RID: 1078
		protected int nProto;

		// Token: 0x04000437 RID: 1079
		protected int address_family;

		// Token: 0x04000438 RID: 1080
		protected string strAddress;

		// Token: 0x04000439 RID: 1081
		protected string pstk_name;

		// Token: 0x0400043A RID: 1082
		protected string source_module;
	}
}
