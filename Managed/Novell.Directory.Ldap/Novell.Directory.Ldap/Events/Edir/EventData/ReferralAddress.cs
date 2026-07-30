using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000C4 RID: 196
	public class ReferralAddress
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00015F88 File Offset: 0x00014188
		public int AddressType
		{
			get
			{
				return this.address_type;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00015F90 File Offset: 0x00014190
		public string Address
		{
			get
			{
				return this.strAddress;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00015F98 File Offset: 0x00014198
		public ReferralAddress(Asn1Sequence dseObject)
		{
			this.address_type = ((Asn1Integer)dseObject.get_Renamed(0)).intValue();
			this.strAddress = ((Asn1OctetString)dseObject.get_Renamed(1)).stringValue();
		}

		// Token: 0x0400045D RID: 1117
		protected int address_type;

		// Token: 0x0400045E RID: 1118
		protected string strAddress;
	}
}
