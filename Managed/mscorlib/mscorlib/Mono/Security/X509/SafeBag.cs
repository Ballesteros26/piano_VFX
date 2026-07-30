using System;

namespace Mono.Security.X509
{
	// Token: 0x02000055 RID: 85
	internal class SafeBag
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public SafeBag(string bagOID, ASN1 asn1)
		{
			this._bagOID = bagOID;
			this._asn1 = asn1;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000E2FA File Offset: 0x0000C4FA
		public string BagOID
		{
			get
			{
				return this._bagOID;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000E302 File Offset: 0x0000C502
		public ASN1 ASN1
		{
			get
			{
				return this._asn1;
			}
		}

		// Token: 0x040004AD RID: 1197
		private string _bagOID;

		// Token: 0x040004AE RID: 1198
		private ASN1 _asn1;
	}
}
